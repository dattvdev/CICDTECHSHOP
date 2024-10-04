using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.TrackingDataModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechShop.Core.Repositories
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly TechshopDbContext _context;
        public TrackingRepository(TechshopDbContext context) 
        {
            _context = context;
        } 

        public async Task<object> DAChartInvoices()
        {
            var dbSetLabel = new List<string> { "Invoice status" };
            // 1: Prepare| 2: Delivering| 3: Complete
            var invoices = await _context.invoices.ToListAsync();

            var results = invoices.GroupBy(i => i.Status).Select(i => new
            {
                lable = i.Key.Value == 1 ? "Prepare" : i.Key.Value == 2 ? "Delivering" : i.Key.Value == 0 ? "Cancel" : "Complete",
                data0 = i.Count()
            });

            return new
            {
                dbSetLable = dbSetLabel,
                result = results
            };
        }

        public async Task<object> DAChartProductSoldAndNotSold()
        {
            var dbSetLabel = new List<string> 
            { 
                "Product sold",
                "Not sold"
            };

            // Get distinct count of products sold
            var countProductSold = await _context.InvoiceProducts
                .Select(i => i.ProductColor.ProductHardware.Product.Id)
                .Distinct()
                .CountAsync();

            // Get count of products not sold
            var countProductNotSold = await _context.Products
                .Where(p => !_context.InvoiceProducts.Select(i => i.ProductColor.ProductHardware.ProductId).Distinct().Contains(p.Id))
                .CountAsync();

            var result = new
            {
                dbSetLable = dbSetLabel,
                result = new List<object>() {
                    new {
                        lable = "",
                        data0 = countProductSold,
                        data1 = countProductNotSold
                    }
                }
            };

            return result;
        }

        public Task<object> DAChartProductHaveSoldInMonth()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var products = _context.invoices
                .Where(i => i.CreatedAt.HasValue && i.CreatedAt.Value.Month == currentMonth && i.CreatedAt.Value.Year == currentYear);

            var dbSetLabel = new List<string>
            {
                "Sales in month"
            };

            var countProductInDayOfMonth = products.GroupBy(p => p.CreatedAt.Value.Day)
                .Select(i => new
                {
                    lable = i.Key + "/" + currentMonth, 
                    data0 = i.Count()
                }).ToList();

            var result = new
            {
                dbSetLable = dbSetLabel, 
                result = countProductInDayOfMonth
            };

            return Task.FromResult((object)result);
        }

        public async Task<object> GetCollaboratorsInDashBoards()
        {
            var collaborators = await (from collaborator in _context.Collaborators
                                       join user in _context.Users
                                       on collaborator.UserID equals user.Id
                                       select new
                                       {
                                           name = user.FullName,
                                           email = user.Email,
                                           isActive = collaborator.StartDate.HasValue && collaborator.EndDate.HasValue &&
                                                       collaborator.StartDate.Value < DateTime.Now && DateTime.Now < collaborator.EndDate.Value,
                                           EndDate = collaborator.EndDate
                                       }).ToListAsync();

            var result = collaborators
                .Select(c => new
                {
                    c.name,
                    c.email,
                    c.isActive,
                    dayActive = c.EndDate.HasValue ? (c.EndDate.Value - DateTime.Now).Days : 0
                })
                .OrderBy(i => i.dayActive)
                .Take(5)
                .ToList();

            return result;
        }


        public async Task<object> GetChartTopSellingProducts()
        {
            var dbSetLabel = new List<string>
            {
                "Top Selling products",
            };

            var products = await _context.InvoiceProducts
                                .Include(i => i.ProductColor)
                                    .ThenInclude(i => i.ProductHardware)
                                    .ThenInclude(i => i.Product)
                                .Select(i => new
                                {
                                    productName = i.ProductColor.ProductHardware.Product.Name,
                                    sellNumber = i.Quantity
                                }).GroupBy(i => i.productName).Select(i => new
                                {
                                    lable = i.Key,
                                    data0 = i.Sum(i => i.sellNumber)
                                }).OrderByDescending(i => i.data0).Take(5).ToListAsync();

            return new
            {
                dbSetLable = dbSetLabel,
                result = products
            };
        }

        public async Task<object?> GetItem(TrackingDataModel model)
        {
            if (model.ItemType == null)
                return null;

            IQueryable<ItemModel> query = Enumerable.Empty<ItemModel>().AsQueryable();

            object? recommend = null;

            if (model.ItemType.Equals("oSells"))
            {
                query = _context.InvoiceProducts
                    .Select(i => new
                    {
                        id = i.ProductColor.ProductHardware.ProductId,
                        name = i.ProductColor.ProductHardware.Product.Name,
                    })
                    .GroupBy(i => i.id)
                    .Select(i => new ItemModel
                    {
                        Id = i.Key.ToString(),
                        Name = i.Select(i => i.name).Distinct().FirstOrDefault(),
                        Count = i.Count()
                    })
                    .OrderByDescending(i => i.Count)
                    .AsQueryable();
                if (query.Count() > 0)
                {
                    var productWithBiggestCount = query.FirstOrDefault();
                    var productWithLowestCount = query.OrderBy(p => p.Count).FirstOrDefault();
                    if (productWithBiggestCount != null && productWithLowestCount != null)
                    {
                        recommend = new
                        {
                            Highest = productWithBiggestCount,
                            Lowest = productWithLowestCount
                        };
                    }
                }

            }
            else if (model.ItemType.Equals("oVotes"))
            {
                query = _context.Comments.Include(c => c.Product)
                    .Where(c => c.Rate > 0)
                    .Select(i => new
                    {
                        id = i.ProductId,
                        name = i.Product.Name,
                        rate = i.Rate,
                    })
                    .GroupBy(p => p.id)
                    .Select(p => new ItemModel
                    {
                        Id = p.Key.ToString(),
                        Name = p.Select(p => p.name).Distinct().FirstOrDefault(),
                        Count = p.Sum(c => c.rate) / p.Count()
                    })
                    .OrderByDescending(p => p.Count)
                    .AsQueryable();

                if(query.Count() > 0)
                {
                    var productWithBiggestCount = query.FirstOrDefault();
                    var productWithLowestCount = query.OrderBy(p => p.Count).FirstOrDefault();
                    if (productWithBiggestCount != null && productWithLowestCount != null)
                    {
                        recommend = new
                        {
                            Highest = productWithBiggestCount,
                            Lowest = productWithLowestCount
                        };
                    }
                }

            }
            else if (model.ItemType.Equals("oCustomerLoyal"))
            {
                query = _context.Customers.Include(c => c.User)
                    .Select(u => new ItemModel
                    {
                        Id = u.ID.ToString(),
                        Name = u.User.FullName,
                        Count = u.LoyaltyPoints
                    })
                    .OrderByDescending(p => p.Count)
                    .AsQueryable();

                if (query.Count() > 0)
                {
                    var cusotmerWithBiggestCount = query.FirstOrDefault();
                    var cusotmerWithLowestCount = query.OrderBy(p => p.Count).FirstOrDefault();
                    if (cusotmerWithBiggestCount != null && cusotmerWithLowestCount != null)
                    {
                        recommend = new
                        {
                            Highest = cusotmerWithBiggestCount,
                            Lowest = cusotmerWithLowestCount
                        };
                    }
                }
            }
            else if (model.ItemType.Equals("oInvoiceDates"))
            {
                query = _context.invoices.Select(i => new
                {
                    Id = i.Id.ToString(),
                    Name = new
                    {
                        Year = i.CreatedAt.Value.Year,
                        Month = i.CreatedAt.Value.Month,
                        Day = i.CreatedAt.Value.Day
                    },
                    Count = i.CreatedAt
                }).OrderByDescending(i => i.Count).GroupBy(i => i.Name).Select(g => new ItemModel()
                {
                    Id = g.Key.Month.ToString() + "/" + g.Key.Day.ToString() + "/" + g.Key.Year.ToString(),
                    Name = g.Key.Month.ToString() + "/" + g.Key.Day.ToString() + "/" + g.Key.Year.ToString(),
                });

                if (query.Count() > 0)
                {
                    recommend = _context.invoices
                            .GroupBy(i => i.CreatedAt.Value.Date) 
                            .Select(i => new
                            {
                                RawDate = i.Key, 
                                DateRecommend = i.Key.ToString("MM/dd/yyyy"), 
                                Product_best_seller_In_Date = i.SelectMany(i => i.InvoiceProducts)
                                                                .GroupBy(i => i.ProductColor.ProductHardware.ProductId)
                                                                .Select(i => new
                                                                {
                                                                    Id = i.Key,
                                                                    Name = i.First().ProductColor.ProductHardware.Product.Name,
                                                                    Count = i.Count(),
                                                                }).ToList()
                            })
                            .AsEnumerable() 
                            .Select(i => new
                            {
                                i.RawDate,
                                i.DateRecommend,
                                Product_best_seller_In_Date = i.Product_best_seller_In_Date
                                                                .OrderByDescending(p => p.Count)
                                                                .FirstOrDefault()
                            })
                            .Where(i => i.Product_best_seller_In_Date != null)
                            .OrderByDescending(i => i.Product_best_seller_In_Date.Count)
                            .ThenByDescending(i => i.RawDate)
                            .Take(5)
                            .ToList();
                }
            }
            else if (model.ItemType.Equals("oInvoice"))
            {
                query = _context.invoices.Select(i => new
                {
                    Id = i.Id.ToString(),
                    name = i.Status
                }).GroupBy(i => i.name).Select(i => new ItemModel()
                {
                    Id = i.Key.ToString(),
                    Name = i.Key.Value == 1 ? "Prepare" : i.Key.Value == 2 ? "Delivering" : i.Key.Value == 0 ? "Cancel" : "Complete",
                    Count = i.Count()
                }).OrderByDescending(i => i.Count);
            }

            if (model.ItemSelected is not null && model.ItemSelected.Any())
            {
                var selectItemId = model.ItemSelected.Select(i => i.Id);
                query = query.Where(q => !selectItemId.Contains(q.Id));
            }

            if (!string.IsNullOrEmpty(model.SearchItem))
            {
                query = query.Where(p => p.Name.Contains(model.SearchItem));
            }
            int totalItem = query.Count();
            var totalPages = (int)Math.Ceiling(totalItem / (double)model.PageSizeItemType.Value);


            var resultData = new List<ItemModel>();

            if (totalItem != 0 && totalItem != 0)
            {
                if (model.CurrentPageItemType.HasValue && model.PageSizeItemType.HasValue && model.PageSizeItemType.Value > 0)
                {
                    query = query.Skip((model.CurrentPageItemType.Value - 1) * model.PageSizeItemType.Value)
                                 .Take(model.PageSizeItemType.Value);
                }
                resultData = await query.ToListAsync();
            }

            return new
            {
                currentPage = model.CurrentPageItemType.Value,
                pageSize = model.PageSizeItemType.Value,
                totalPage = totalPages,
                data = resultData,
                recommend = recommend
            };
        }

        public async Task<object?> DAChartData(TrackingDataModel model)
        {
            if (model.ItemType == null)
                return null;

            IQueryable<DataLableTracking> query = Enumerable.Empty<DataLableTracking>().AsQueryable();
            var dbSetLabel = new List<string>();

            if (model.ItemType.Equals("oSells"))
            {
                dbSetLabel.Add("Best product sell");
                query = _context.InvoiceProducts
                    .Select(i => new
                    {
                        id = i.ProductColor.ProductHardware.ProductId,
                        name = i.ProductColor.ProductHardware.Product.Name,
                    })
                    .GroupBy(i => i.id)
                    .Select(i => new DataLableTracking
                    {
                        Id = i.Key.ToString(),
                        Lable = i.Select(i => i.name).Distinct().FirstOrDefault(),
                        Data0 = i.Count()
                    })
                    .OrderByDescending(i => i.Data0)
                    .AsQueryable();
            }
            else if (model.ItemType.Equals("oVotes"))
            {
                dbSetLabel.Add("Best product votes");
                query = _context.Comments.Include(c => c.Product)
                    .Where(c => c.Rate > 0)
                    .Select(i => new
                    {
                        id = i.ProductId,
                        name = i.Product.Name,
                        rate = i.Rate,
                    })
                    .GroupBy(p => p.id)
                    .Select(p => new DataLableTracking
                    {
                        Id = p.Key.ToString(),
                        Lable = p.Select(p => p.name).Distinct().FirstOrDefault(),
                        Data0 = p.Sum(c => c.rate) / p.Count()
                    })
                    .OrderByDescending(p => p.Data0)
                    .AsQueryable();
            }
            else if (model.ItemType.Equals("oCustomerLoyal"))
            {
                dbSetLabel.Add("Best Cusotmer");
                query = _context.Customers.Include(c => c.User)
                    .Select(u => new DataLableTracking
                    {
                        Id = u.ID.ToString(),
                        Lable = u.User.FullName,
                        Data0 = u.LoyaltyPoints
                    })
                    .OrderByDescending(p => p.Data0)
                    .AsQueryable();
            }
            else if (model.ItemType.Equals("oInvoiceDates"))
            {
                dbSetLabel.Add("Number invoice of date");
                query = _context.invoices.Select(i => new
                {
                    Id = i.Id.ToString(),
                    Name = new
                    {
                        Year = i.CreatedAt.Value.Year,
                        Month = i.CreatedAt.Value.Month,
                        Day = i.CreatedAt.Value.Day
                    }
                }).GroupBy(i => i.Name).Select(g => new DataLableTracking
                {
                    Id = g.Key.Month.ToString() + "/" + g.Key.Day.ToString() + "/" + g.Key.Year.ToString(),
                    Lable = g.Key.Month.ToString() + "/" + g.Key.Day.ToString() + "/" + g.Key.Year.ToString(),
                    Data0 = g.Count()
                }).OrderByDescending(i => i.Data0);
            }
            else if (model.ItemType.Equals("oInvoice"))
            {
                dbSetLabel.Add("Number status of invoice");
                query = _context.invoices.Select(i => new
                {
                    Id = i.Id.ToString(),
                    name = i.Status
                }).GroupBy(i => i.name).Select(i => new DataLableTracking
                {
                    Id = i.Key.ToString(),
                    Lable = i.Key.Value == 1 ? "Prepare" : i.Key.Value == 2 ? "Delivering" : i.Key.Value == 0 ? "Cancel" : "Complete",
                    Data0 = i.Count()
                }).OrderByDescending(i => i.Data0);
            }

            if (model.ItemSelected is not null && model.ItemSelected.Any())
            {
                var selectItemId = model.ItemSelected.Select(i => i.Id);
                query = query.Where(q => selectItemId.Contains(q.Id));
            }

            if(model.ChartSize is not null)
            {
                if (model.ChartSize.Value != 0)
                {
                    query = query.Take(model.ChartSize.Value);
                }
            }

            return new
            {
                dbSetLable = dbSetLabel,
                result = query
            };
        }

        public async Task<object?> DAChartProductWithDateRange(TrackingDataModel model)
        {
            if (!model.ItemType.Equals("oInvoiceDates"))
                return null;
            var dbSetLable = new List<string>();
            dbSetLable.Add("Number product sold in range date");

            var invoices = _context.invoices.AsQueryable();

            if (model.StartDate is not null && model.EndDate is null)
            {
                invoices = invoices.Where(i => i.CreatedAt >= model.StartDate && i.CreatedAt <= DateTime.Now);
            }
            else if (model.StartDate is null && model.EndDate is not null)
            {
                invoices = invoices.Where(i => i.CreatedAt <= model.EndDate);
            }
            else if (model.StartDate is not null && model.EndDate is not null)
            {
                invoices = invoices.Where(i => i.CreatedAt.Value.Date >= model.StartDate.Value.Date && i.CreatedAt.Value.Date <= model.EndDate.Value.Date);
            }
            var productColorCounts = await invoices
               .SelectMany(i => i.ProductColors)
               .GroupBy(i => i.ProductHardware.ProductId)
               .OrderByDescending(i => i.Count())
               .Select(group => new
               {
                   Lable = group.First().ProductHardware.Product.Name,
                   Data0 = group.Count()
               })
               .ToListAsync();

            return new
            {
                dbSetLable,
                result = productColorCounts
            };
        }
    }
}

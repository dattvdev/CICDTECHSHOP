using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using Order = TechShop.Core.Models.DataTableModel.Order;

namespace TechShop.Core.Repositories
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly TechshopDbContext _context;

        public CollaboratorRepository(TechshopDbContext context)
        {
            _context = context;
        }


        public IList<Collaborator> GetAllCollaborators()
        {
            return _context.Collaborators.Include(c => c.User)
                        .ToList();
        }

        public void AddCollaborator(Collaborator collaborator)
        {
            _context.Collaborators.Add(collaborator);
            _context.SaveChanges();
        }

        public void UpdateCollaborator(Collaborator collaborator)
        {
            _context.Collaborators.Update(collaborator);
            _context.SaveChanges();
        }

        public void DeleteCollaborator(Guid id)
        {
            var collaborator = _context.Collaborators.Find(id);
            if (collaborator != null)
            {
                _context.Collaborators.Remove(collaborator);
                _context.SaveChanges();
            }
        }

        public Collaborator FindCollaborator(Guid? id)
        {
            return _context.Collaborators.Find(id);
        }

        public Collaborator GetCollaboratorByUserId(string userId)
        {
            return _context.Collaborators.FirstOrDefault(c => c.UserID == userId);
        }

        public async Task<Collaborator> GetCollaboratorByUserIdAsync(string userId)
        {
            return await _context.Collaborators.FirstOrDefaultAsync(c => c.UserID == userId);
        }

        public async Task<int> Count()
        {
            return await _context.Collaborators.CountAsync();
        }

        public async Task<object> GetDataCollaborators(DataTableParameters parameters)
        {
            // Check order
            if (parameters.Order == null || !parameters.Order.Any())
            {
                parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
            }

            // Take list comments from database
            var comments = _context.Collaborators.AsQueryable();

            // mapping data
            var query = comments
                .Select(i => new
                {
                    id = i.ID,
                    user = i.User.FullName,
                    startDate = i.StartDate.HasValue
                        ? $"{i.StartDate.Value.Day}/{i.StartDate.Value.Month}/{i.StartDate.Value.Year}"
                        : "Unknown",
                    endDate = i.EndDate.HasValue
                        ? $"{i.EndDate.Value.Day}/{i.EndDate.Value.Month}/{i.EndDate.Value.Year}"
                        : "Unknown",
                    rawStartDate = i.StartDate,
                    rawEndDate = i.EndDate
                });

            // search for DataTableParameters 
            if (!string.IsNullOrEmpty(parameters.Search?.Value))
            {
                query = query.Where(i => i.user.Contains(parameters.Search.Value));
            }

            // sort for DataTableParameters
            var sortColumn = parameters.Order[0].Column;
            var sortDirection = parameters.Order[0].Dir;
            switch (sortColumn)
            {
                case 1:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.user)
                        : query.OrderByDescending(i => i.user);
                    break;
                case 2:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.rawStartDate)
                        : query.OrderByDescending(i => i.rawStartDate);
                    break;
                case 3:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.rawEndDate)
                        : query.OrderByDescending(i => i.rawEndDate);
                    break;
                default:
                    query = query.OrderBy(i => i.user);
                    break;
            }

            // Count total records
            var totalRecords = await query.CountAsync();

            parameters.Length = parameters.Length == 0 ? 5 : parameters.Length;
            //Paging
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();

            // end
            var result = new
            {
                draw = parameters.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = data,
            };

            return result;
        }

    }
}

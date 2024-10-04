using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TechShop.Core.Data;
using TechShop.Core.Repositories;
using TechShop.Core.Models.DataTableModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using TechShop.Core.Models;
using TechShop.Core.Models.InvoiceParamesters;
using System.Linq.Expressions;

namespace TechShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ExcelPackage _excelPackage;
        public InvoicesController(IInvoiceRepository invoiceRepository, ExcelPackage excelPackage) 
        {
            _invoiceRepository = invoiceRepository;
            _excelPackage = excelPackage;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(string id)
        {
            if (id is null)
                return NotFound();

            var invoice = await _invoiceRepository.GetDetailInvoice(id);

            if (invoice is null)
                return NotFound();

            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataInvoices(DataTableParameters parameters, [FromQuery] int? status, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            await Task.Delay(500); // để cho xịn xịn chút
            if(parameters is null)
            {
                return BadRequest("Invalid request");
            }
            var result = await _invoiceRepository.GetInvoiceDetailsAsync(parameters, status, startDate, endDate);

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductInInvoice(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid invoice ID.");
            }

            var results = await _invoiceRepository.GetProductInInvoice(id);

            if(results is null)
            {
                return NotFound();
            }

            return Json(results);
        }

        [HttpGet]
        public async Task<IActionResult> CancelInvoice(string id)
        {
            var result = await _invoiceRepository.CancelInvoice(id);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDAUserShoppingInMonth(string userId, string invoiceId)
        {
            var result = await _invoiceRepository.GetDAUserShoppingInMonth(userId, invoiceId);
            if (result is null)
            {
                return NotFound();
            }

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> ExportExcelForInvoice(string id)
        {
            if(id is null)
            {
                return NotFound();
            }
            var invoice = await _invoiceRepository.GetDetailInvoice(id);

            var workSheet = _excelPackage.Workbook.Worksheets.Add("Sheet1");
            workSheet.DefaultRowHeight = 12;

            // Title
            workSheet.Row(1).Height = 35;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Row(1).Style.Font.Italic = true;
            workSheet.Row(1).Style.Font.Size = 26;
            workSheet.Row(1).Style.Font.Color.SetColor(System.Drawing.Color.Red);
            workSheet.Cells[1, 5].Value = "TechShop - All invoices";
            // Sub title
            workSheet.Row(2).Height = 25;
            workSheet.Row(2).Style.Font.Bold = true;
            workSheet.Row(2).Style.Font.Italic = true;
            workSheet.Row(2).Style.Font.Size = 23;
            workSheet.Cells[2, 5].Value = "Support by: Nguyen Duy Phuc (+84 123 4564 1234)";
            // Customer name
            workSheet.Row(3).Height = 22;
            workSheet.Row(3).Style.Font.Bold = true;
            workSheet.Row(3).Style.Font.Italic = true;
            workSheet.Row(3).Style.Font.Size = 11;
            workSheet.Cells[3, 4].Value = $"Customer name: ";
            workSheet.Cells[3, 5].Value = invoice.Customer.User.FullName;
            workSheet.Cells[3, 5].AutoFitColumns();
            // Create at
            workSheet.Row(4).Height = 22;
            workSheet.Row(4).Style.Font.Bold = true;
            workSheet.Row(4).Style.Font.Italic = true;
            workSheet.Row(4).Style.Font.Size = 11;
            workSheet.Cells[4, 4].Value = $"Create at: ";
            workSheet.Cells[4, 5].Style.Numberformat.Format = "dd/MM/yyyy";
            workSheet.Cells[4, 5].Value = invoice.CreatedAt;
            workSheet.Cells[4, 5].AutoFitColumns();
            // Status
            workSheet.Cells[4, 8].Value = "Status: ";
            workSheet.Cells[4, 9].Value = invoice.Status == 1 ? "Prepare" : invoice.Status == 2 ? "Delivering" : invoice.Status == 3 ? "Complete" : "Cancel";
            workSheet.Cells[4, 9].AutoFitColumns();

            // Set style header
            workSheet.Row(5).Height = 20;
            workSheet.Row(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(5).Style.Font.Bold = true;

            workSheet.Cells[5, 1].Value = "Product id";
            workSheet.Cells[5, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 2].Value = "Product name";
            workSheet.Cells[5, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 3].Value = "Quantity";
            workSheet.Cells[5, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 4].Value = "Total price";
            workSheet.Cells[5, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

   

            int recordIndex = 6;
            int indexId = 1;
            foreach (var item in invoice.InvoiceProducts)
            {
                ExcelRange Rng = workSheet.Cells[recordIndex, 1];
                Rng.Hyperlink = new Uri($"https://localhost:7061/Admin/Product/Details?id={item.ProductColor.ProductHardware.Product.Id}", UriKind.Absolute);
                Rng.Value = indexId;
                Rng.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                //workSheet.Cells[recordIndex, 1].Value = item.ProductId;
                workSheet.Cells[recordIndex, 2].Value = item.ProductColor.ProductHardware.Product.Name;
                workSheet.Cells[recordIndex, 3].Value = item.Quantity;
                workSheet.Cells[recordIndex, 4].Value = (item.ProductPrice * item.Quantity);
     
                recordIndex++;
                indexId++;
            }

            var modelCells = workSheet.Cells["A5"];
            var modelRange = "A5:D" + (recordIndex - 1).ToString();
            var modelTable = workSheet.Cells[modelRange];
            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            modelTable.AutoFitColumns();


            workSheet.Cells[$"B{recordIndex}"].Value = $"Total";
            workSheet.Cells[$"B{recordIndex}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"B{recordIndex}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"B{recordIndex}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"B{recordIndex}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"B{recordIndex}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[$"B{recordIndex}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.AliceBlue);

            workSheet.Cells[$"C{recordIndex}"].Formula = $"=SUM(C6:C{(recordIndex - 1)})";
            workSheet.Cells[$"C{recordIndex}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"C{recordIndex}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"C{recordIndex}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"C{recordIndex}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"C{recordIndex}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[$"C{recordIndex}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.AliceBlue);

            workSheet.Cells[$"D{recordIndex}"].Formula = $"=SUM(D6:D{(recordIndex - 1)})";
            workSheet.Cells[$"D{recordIndex}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"D{recordIndex}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"D{recordIndex}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"D{recordIndex}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"D{recordIndex}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[$"D{recordIndex}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.AliceBlue);

            workSheet.Column(3).AutoFit();
            workSheet.Column(4).Style.Numberformat.Format = "$#,##0.00";
            workSheet.Column(4).AutoFit();


            var stream = new MemoryStream();
            _excelPackage.SaveAs(stream);
            stream.Position = 0;
            var fileName = $"Invoices_{DateTime.Now.Day}_{DateTime.Now.Month}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);   
        }

        [HttpPost]
        public async Task<IActionResult> ExporterExcelAllInvoices([FromBody] InvoiceParamesterOption invoiceParamesterOption)
        {
            var invoices = await _invoiceRepository.GetInvoiceWithOptions(invoiceParamesterOption);

            var workSheet = _excelPackage.Workbook.Worksheets.Add("Invoices");
            workSheet.DefaultRowHeight = 12;

            // Title
            workSheet.Row(1).Height = 35;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Row(1).Style.Font.Italic = true;
            workSheet.Row(1).Style.Font.Size = 26;
            workSheet.Row(1).Style.Font.Color.SetColor(System.Drawing.Color.Red);
            workSheet.Cells[1, 4].Value = "TechShop - All invoices";
            // Sub title
            workSheet.Row(2).Height = 25;
            workSheet.Row(2).Style.Font.Bold = true;
            workSheet.Row(2).Style.Font.Italic = true;
            workSheet.Row(2).Style.Font.Size = 11;
            workSheet.Cells[2, 4].Value = "Support by: Nguyen Duy Phuc (+84 123 4564 1234)";

            if(invoiceParamesterOption is not null)
            {
                if (invoiceParamesterOption.StartDate is not null && invoiceParamesterOption.EndDate is null)
                {
                    workSheet.Row(3).Height = 25;
                    workSheet.Row(3).Style.Font.Italic = true;
                    workSheet.Row(3).Style.Font.Size = 11;
                    workSheet.Cells[3, 4].Value = $"The invoices from this date {invoiceParamesterOption.StartDate.Value.Day}/{invoiceParamesterOption.StartDate.Value.Month}/{invoiceParamesterOption.StartDate.Value.Year} to now";
                    workSheet.Cells[3, 4].AutoFitColumns();
                }
                else if (invoiceParamesterOption.StartDate is null && invoiceParamesterOption.EndDate is not null)
                {
                    workSheet.Row(3).Height = 25;
                    workSheet.Row(3).Style.Font.Italic = true;
                    workSheet.Row(3).Style.Font.Size = 11;
                    workSheet.Cells[3, 4].Value = $"The invoices from this date {invoiceParamesterOption.EndDate.Value.Day}/{invoiceParamesterOption.EndDate.Value.Month}/{invoiceParamesterOption.EndDate.Value.Year} to past";
                    workSheet.Cells[3, 4].AutoFitColumns();
                }
                else if (invoiceParamesterOption.StartDate is not null && invoiceParamesterOption.EndDate is not null)
                {
                    workSheet.Row(3).Height = 25;
                    workSheet.Row(3).Style.Font.Italic = true;
                    workSheet.Row(3).Style.Font.Size = 11;
                    workSheet.Cells[3, 4].Value = $"The invoices from this date {invoiceParamesterOption.StartDate.Value.Day}/{invoiceParamesterOption.StartDate.Value.Month}/{invoiceParamesterOption.StartDate.Value.Year} to {invoiceParamesterOption.EndDate.Value.Day}/{invoiceParamesterOption.EndDate.Value.Month}/{invoiceParamesterOption.EndDate.Value.Year}";
                    workSheet.Cells[3, 4].AutoFitColumns();
                }
            }
            else
            {
                workSheet.Row(3).Height = 25;
                workSheet.Row(3).Style.Font.Italic = true;
                workSheet.Row(3).Style.Font.Size = 11;
                workSheet.Cells[3, 4].Value = $"All of invoices";
                workSheet.Cells[3, 4].AutoFitColumns();
            }

            // set style for header
            workSheet.Row(5).Height = 20;
            workSheet.Row(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(5).Style.Font.Bold = true;

            workSheet.Cells[5, 1].Value = "Invoice id";
            workSheet.Cells[5, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 2].Value = "Customer name";
            workSheet.Cells[5, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 3].Value = "Create at";
            workSheet.Cells[5, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 4].Value = "Status";
            workSheet.Cells[5, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 5].Value = "Products";
            workSheet.Cells[5, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 6].Value = "Product price";
            workSheet.Cells[5, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 6].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 7].Value = "Quantity";
            workSheet.Cells[5, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            workSheet.Cells[5, 8].Value = "Total price";
            workSheet.Cells[5, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[5, 8].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
            // set border for header
            var modelTable = workSheet.Cells["A5:H5"];
            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            modelTable.AutoFitColumns();

            int recordIndex = 6;
            int recordId = 1;
            foreach (var item in invoices)
            {
                ExcelRange Rng = workSheet.Cells[recordIndex, 1];
                Rng.Hyperlink = new Uri($"https://localhost:7061/Admin/Invoices/Detail?id={item.Id}", UriKind.Absolute);
                Rng.Value = recordId;
                Rng.Style.Font.Color.SetColor(System.Drawing.Color.Blue);

                //workSheet.Cells[recordIndex, 1].Value = item.Id;
                workSheet.Cells[recordIndex, 2].Value = item.Customer.User.FullName;
                workSheet.Cells[recordIndex, 3].Value = item.CreatedAt;
                workSheet.Cells[recordIndex, 4].Value = item.Status == 1 ? "Prepare" : item.Status == 2 ? "Delivering" : item.Status == 3 ? "Complete" : "Cancel";
                workSheet.Cells[recordIndex, 4].AutoFitColumns();
                var products = item.InvoiceProducts.Select(ip => new
                {
                    productId = ip.ProductColor.ProductHardware.ProductId,
                    productName = ip.ProductColor.ProductHardware.Product.Name,
                    quantity = ip.Quantity,
                    productPrice = ip.ProductPrice
                }).ToArray();
                var productCount = products.Count();

                ExcelRange RngFirtProduct = workSheet.Cells[recordIndex, 5];
                RngFirtProduct.Hyperlink = new Uri($"https://localhost:7061/Admin/Product/Details?id={products[0].productId}", UriKind.Absolute);
                RngFirtProduct.Value = products[0].productName;
                RngFirtProduct.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                //workSheet.Cells[recordIndex, 5].Value = products[0].productName;
                workSheet.Cells[recordIndex, 6].Value = products[0].productPrice;
                workSheet.Cells[recordIndex, 7].Value = products[0].quantity;
                workSheet.Cells[recordIndex, 8].Formula = $"=F{recordIndex}*G{recordIndex}";

                var modelRowTable = workSheet.Cells[$"A{recordIndex}:H{recordIndex}"];
                modelRowTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelRowTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelRowTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelRowTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                modelRowTable.AutoFitColumns();

                for (int i = 1; i < productCount; i++)
                {
                    recordIndex++;
                    ExcelRange RngSubProduct = workSheet.Cells[recordIndex, 5];
                    RngSubProduct.Hyperlink = new Uri($"https://localhost:7061/Admin/Product/Details?id={products[0].productId}", UriKind.Absolute);
                    RngSubProduct.Value = products[0].productName;
                    RngSubProduct.Style.Font.Color.SetColor(System.Drawing.Color.Blue);

                    //workSheet.Cells[recordIndex, 5].Value = products[i].productName;
                    workSheet.Cells[recordIndex, 6].Value = products[i].productPrice;
                    workSheet.Cells[recordIndex, 7].Value = products[i].quantity;
                    workSheet.Cells[recordIndex, 8].Formula = $"=F{recordIndex}*G{recordIndex}";

                    var modelSubRowTable = workSheet.Cells[$"E{recordIndex}:H{recordIndex}"];
                    modelSubRowTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    modelSubRowTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    modelSubRowTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    modelSubRowTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    modelSubRowTable.AutoFitColumns();
                }
                recordIndex++;
                workSheet.Cells[recordIndex, 6].Value = "Total";
                workSheet.Cells[recordIndex, 6].Style.Font.Bold = true;
                workSheet.Cells[recordIndex, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[recordIndex, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[recordIndex, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[recordIndex, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                workSheet.Cells[recordIndex, 7].Formula = $"=SUM(G{(recordIndex - productCount)}:G{recordIndex - 1})";
                workSheet.Cells[recordIndex, 7].Style.Font.Bold = true;
                workSheet.Cells[recordIndex, 8].Formula = $"=SUM(H{(recordIndex-productCount)}:H{recordIndex-1})";
                workSheet.Cells[recordIndex, 8].Style.Font.Bold = true;

                var modelEndRowTable = workSheet.Cells[$"G{recordIndex}:H{recordIndex}"];
                modelEndRowTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelEndRowTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelEndRowTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelEndRowTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                modelEndRowTable.AutoFitColumns();

                recordIndex++;
                recordId++;
            }

            for(int i = 1; i<= 8; i++)
            {
                if(i != 4)
                    workSheet.Column(i).AutoFit();
            }

            workSheet.Column(3).Style.Numberformat.Format = "dd/MM/yyyy";
            workSheet.Column(6).Style.Numberformat.Format = "$#,##0.00";
            workSheet.Column(8).Style.Numberformat.Format = "$#,##0.00";

            var stream = new MemoryStream();
            _excelPackage.SaveAs(stream);
            stream.Position = 0;
            var fileName = $"Invoices_{DateTime.Now.Second}_{DateTime.Now.Minute}_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

            
       
        }

    }
}

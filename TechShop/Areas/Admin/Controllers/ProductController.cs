using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Net;
using System.Net.WebSockets;
using TechShop.Areas.Admin.ViewModels;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using TechShop.Core.Repositories;
using TechShop.Dtos;
using TechShop.Helper;

namespace TechShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IProductFilterRepository _productFilterRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductHardwareRepository _productHardwareRepository;
        private readonly IProductColorRepository _productColorRepository;
        private readonly Cloudinary _cloudinary;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository, IProductFilterRepository productFilterRepository, IProductImageRepository productImageRepository, IProductHardwareRepository productHardwareRepository, IProductColorRepository productColorRepository, Cloudinary cloudinary)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _productFilterRepository = productFilterRepository;
            _productImageRepository = productImageRepository;
            _productHardwareRepository = productHardwareRepository;
            _productColorRepository = productColorRepository;
            _cloudinary = cloudinary;
        }
        public async Task<IActionResult> Index()
        {
            var products = _productRepository.GetProducts();
            var categories = _categoryRepository.GetAllCategories();
            var brands = _brandRepository.GetAllBrands();
            var productViewModels = new ProductListViewModel
            {
                Categories = categories,
                Products = products,
                Brands = brands
            };
            return View(productViewModels);
        }

        public IActionResult GetProductsByCategoryBrand(string categoryId, string brandId)
        {
            List<Product> products = null;
            if (categoryId == "all" && brandId == "all")
            {
                products = _productRepository.GetProducts();
            }
            else if (categoryId == "all")
            {
                products = _productRepository.GetProductsByBrandId(new Guid(brandId));
            }
            else if (brandId == "all")
            {
                products = _productRepository.GetProductsByCategoryId(new Guid(categoryId));
            }
            else
            {
                products = _productRepository.GetProductsByCategoryBrandId(new Guid(categoryId), new Guid(brandId));
            }
            return PartialView("_ProductTable", products);
        }

        [HttpPost]
        public IActionResult Create(string productName, string brandId, string categoryId)
        {
            try
            {
                var product = _productRepository.NewProduct(productName, Utilities.Utilities.ConvertToSlug(productName), new Guid(brandId), new Guid(categoryId));

                // Assuming your Product entity has an Id property
                return Json(new { isSuccess = true, message = "Created Success", redirectUrl = Url.Action("Details", "Product", new { area = "Admin", id = product.Id }) });
            }
            catch (Exception ex)
            {
                // Log the error
                Debug.WriteLine("Error: " + ex.Message);
                return Json(new { isSuccess = false, message = "Failed to create product." });
            }
        }


        [HttpPost]
        public JsonResult IsProductNameAvailable(string name)
        {
            return Json(_productRepository.IsExistProductName(name, Utilities.Utilities.ConvertToSlug(name), null));
        }

        [HttpPost]
        public JsonResult IsProductNameAvailableWithProductId(string name, string productId)
        {
            return Json(_productRepository.IsExistProductName(name, Utilities.Utilities.ConvertToSlug(name), Guid.Parse(productId)));
        }


        [HttpPost]
        public async Task<JsonResult> IsProductHardwareNameAvailable(string nameHardwareProductInput, string productId)
        {
            return Json(await _productHardwareRepository.ExistProductHardwareName(nameHardwareProductInput, Guid.Parse(productId)));
        }

        [Route("Admin/[controller]/[action]/{id?}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductById(new Guid(id));

            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_brandRepository.GetAllBrands(), "Id", "Name", product.BrandId);

            return View(product);
        }

        [HttpPost]
        public JsonResult ConvertURLSlug(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                return Json("");
            }
            return Json(Utilities.Utilities.ConvertToSlug(productName));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInformationProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateProduct(product);
                return Json(new { isSuccess = true, message = "Update Product success." });
            }
            return Json(new { isSuccess = false, message = "Failed to update product." });
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string productId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                var product = await _productRepository.GetProductById(new Guid(productId));
                if (product != null)
                {
                    product.Image = uploadResult.SecureUri.ToString();
                    await _productRepository.UpdateProduct(product);
                    return Json(new { isSuccess = true, message = "Update Product Image success." });
                }
                else return NotFound();
            }
            else
            {
                return Json(new { isSuccess = false, message = "Update Product Image failed." });
            }

        }

        [HttpPost]
        public async Task<IActionResult> SaveContent(string productId, string content)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return NotFound();
            }

            if (!Guid.TryParse(productId, out var productGuid))
            {
                return BadRequest("Invalid product ID format.");
            }

            var product = await _productRepository.GetProductById(productGuid);

            if (product != null)
            {
                product.Description = content;
                await _productRepository.UpdateProduct(product);
                return Json(new { isSuccess = true, message = "Update Product Content success." });
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public async Task<IActionResult> SaveFilters(Guid productId, string selectedFilters)
        {
            if (productId == Guid.Empty)
            {
                return NotFound();
            }

            List<Guid> listSelectedFilter = new List<Guid>();

            if (!string.IsNullOrEmpty(selectedFilters))
            {
                foreach (string id in selectedFilters.Split(','))
                {
                    listSelectedFilter.Add(Guid.Parse(id));
                }
            }

            bool result = await _productFilterRepository.UpdateFilters(productId, listSelectedFilter);

            if (result)
            {
                return Json(new { isSuccess = result, message = "Update Product Filter success." });
            }
            else
            {
                return Json(new { isSuccess = result, message = "Update Product Filter failed." });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddContentImage(IFormFile imageInput, string altInput, string titleInput, string productId)
        {
            if (imageInput != null && imageInput.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imageInput.FileName, imageInput.OpenReadStream()),
                    Overwrite = false
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ProductImage productImage = new ProductImage()
                    {
                        ProductId = Guid.Parse(productId),
                        Alt = altInput,
                        Title = titleInput,
                        Type = 2,
                        UrlImage = uploadResult.SecureUri.ToString(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };

                    bool result = await _productImageRepository.AddProductImage(productImage);

                    if (result)
                    {
                        int count = await _productImageRepository.CountListImageContentByProductId(productImage.ProductId);
                        return Json(new { isSuccess = true, message = "Update Product Content Image Success.", title = productImage.Title, alt = productImage.Alt, imageUrl = productImage.UrlImage, countProductImage = count, id = productImage.Id });
                    }
                    else
                    {
                        return Json(new { isSuccess = false, message = "Update Product Content Image failed." });
                    }
                }
            }
            return Json(new { isSuccess = false, message = "Something wrong happens" });
        }

        [HttpPost]
        public async Task<IActionResult> AddSlideImage(List<IFormFile> slideImageInput, string productId)
        {
            if (slideImageInput == null || slideImageInput.Count == 0)
                return BadRequest("No files were uploaded.");

            var uploadedImages = new List<object>();

            foreach (var slideImage in slideImageInput)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(slideImage.FileName, slideImage.OpenReadStream()),
                    Overwrite = false
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    ProductImage productImage = new ProductImage()
                    {
                        ProductId = Guid.Parse(productId),
                        Alt = "", 
                        Title = "",
                        Type = 1,
                        UrlImage = uploadResult.SecureUri.ToString(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };

                    bool result = await _productImageRepository.AddProductImage(productImage);

                    if (result)
                    {
                        // Add the image details to the list
                        uploadedImages.Add(new
                        {
                            isSuccess = true,
                            urlImage = productImage.UrlImage,
                            alt = productImage.Alt,
                            title = productImage.Title,
                            id = productImage.Id
                        });
                    }
                }
            }

            return Json(new { images = uploadedImages, message = "Slide images uploaded successfully!" });
        }


        [HttpPost]
        public async Task<IActionResult> AddProductHardware(string productId, List<string> filters, string name)
        {
            if (productId == null)
            {
                return NotFound();
            }
            Console.WriteLine(filters.Count());
            Product product = await _productRepository.GetProductById(Guid.Parse(productId));
            if (filters.Count > 0)
            {
                ProductHardware newHardware = new ProductHardware();
                newHardware.ProductId = Guid.Parse(productId);
                newHardware.Name = name;
                if (await _productHardwareRepository.AddProductHardware(newHardware))
                {
                    List<Guid> filtersGuid = new List<Guid>();
                    foreach (var filter in filters) {
                        filtersGuid.Add(Guid.Parse(filter));
                        Console.WriteLine(filter);
                    }
                    await _productFilterRepository.UpdateFilters(newHardware.Id, filtersGuid);
                    return Json(new { isSuccess = true, message = "Create Hardware Success", name = name, id = newHardware.Id });
                } else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductColorsByHardwareId(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var hardwareProdcuct = await _productHardwareRepository.GetProductHardwareById(id);
            if (hardwareProdcuct is null)
            {
                return NotFound();
            }
            var productColors = await _productColorRepository.GetProductColorsByHardwareId(id);

            if (productColors is null)
            {
                return BadRequest();
            }

            var resuls = productColors.Select(p => new
            {
                id = p.Id,
                RGB = p.RGB,
                Quantity = p.Quantity,
                ProductHardwareId = p.ProductHardwareId,
                Price = p.Price
            });

            return Json(new
            {
                hardWareName = hardwareProdcuct.Name,
                productColors = resuls
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNewListProductColor([FromBody] UpdateProductColorRequest request)
        {
            var hardwareProdcuct = await _productHardwareRepository.GetProductHardwareById(request.ProductHardwareId);
            if (hardwareProdcuct is null)
            {
                return NotFound();
            }

            var updateProductColors = request.ProductColors;
            var cloneColorList = updateProductColors.Select(c => new
            {
                RGB = c.RGB
            }).ToList();

            var countProductColors = updateProductColors.Count;
            var isSimilar = false;

            for (int i = 0; i < countProductColors; i++)
            {
                for (int j = 0; j < countProductColors; j++)

                    if (i != j)
                    {
                        isSimilar = ColorHelper.IsSimilarColor(updateProductColors[i].RGB, cloneColorList[j].RGB);
                        if (isSimilar == true)
                            break;
                    }
                if (isSimilar == true)
                    break;
            }

            if (isSimilar)
            {
                return Json(new { isSuccess = false, message = "Duplicate color." });
            }


            var updateHardware = await _productHardwareRepository.UpdateProducthardwareName(request.ProductHardwareId, request.ProductHardwareName);
            var result = await _productColorRepository.UpdateListProductColor(request.ProductColors, request.ProductHardwareId);
            if (result == true)
            {
                return Json(new
                {
                    isSuccess = result,
                    message = "Update new product colors Hardware Success.",
                    nameHardware = request.ProductHardwareName,
                    productColors = request.ProductColors.Select(p => new
                    {
                        id = p.Id,
                        RGB = p.RGB,
                        Quantity = p.Quantity,
                        ProductHardwareId = p.ProductHardwareId,
                        Price = p.Price
                    })
                });
            }
            else
            {
                return Json(new { isSuccess = result, message = "Update product colors Failed." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProductHardware(string id)
        {
            var result = await _productHardwareRepository.DeleteProductHardware(id);
            return result == true ?
                Json(new { isSuccess = result, message = "Delete Product Hardware Success." })
                : Json(new { isSuccess = result, message = "Delete Product Hardware failed." });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(string productImageId)
        {
            if (string.IsNullOrEmpty(productImageId))
            {
                return NotFound();
            }

            if (Guid.TryParse(productImageId, out var id))
            {
                var productImage = await _productImageRepository.GetProductImageById(id);

                if (productImage != null)
                {
                    var publicId = Utilities.Utilities.GetPublicIdFromUrl(productImage.UrlImage);

                    var deletionParams = new DeletionParams(publicId);
                    var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

                    if (deletionResult.StatusCode == HttpStatusCode.OK)
                    {
                        var result = await _productImageRepository.DeleteProductImage(productImage);

                        if (result)
                        {
                            return Json(new { isSuccess = true, message = "Delete Image Success." });
                        }
                        else
                        {
                            return Json(new { isSuccess = false, message = "Failed to delete image from database." });
                        }
                    }
                    else
                    {
                        return Json(new { isSuccess = false, message = "Failed to delete image from Cloudinary." });
                    }
                }
                else
                {
                    return NotFound("Product image not found.");
                }
            }
            else
            {
                return BadRequest("Invalid product image ID.");
            }
        }

        public async Task<IActionResult> RenderContentImage(string productId)
        {
            var product = await _productRepository.GetProductById(Guid.Parse(productId));

            return ViewComponent("UpdateContentImageProduct", new { product = product });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductConfirm(Guid id)
        {
            var product = await _productRepository.GetProductById(id);
            _productRepository.DeleteProduct(product);
            return Json(new { isSuccess = true, message = "Delete Success", redirectUrl = Url.Action("Index", "Product") });
        }
        [HttpPost]
        public async Task<IActionResult> GetDataProduct(DataTableParameters parameters)
        {
            await Task.Delay(200); // để cho xịn xịn chút
                                   // Check DataTableParameters 
            if (parameters == null)
            {
                return BadRequest("Invalid parameters");
            }

            var result = await _productRepository.GetDataProduct(parameters);

            return Json(result);
        }
    }
}

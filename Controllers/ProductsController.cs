using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using SportProduct.Data;
using System; 
using SportProduct.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace SportProduct.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SportProductContext _context;

        // Dependency Injection: The DbContext is injected into the controller.
        public ProductsController(SportProductContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.OrderBy(p => p.NamePro).ToListAsync();
            return View(products);
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            var viewModel = new ProductCreateViewModel();
            return View(viewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NamePro,DecriptionPro,Category,Price,ImagePro,ManufacturingDate")] ProductCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new Product
                {
                    NamePro = viewModel.NamePro,
                    DecriptionPro = viewModel.DecriptionPro,
                    Category = viewModel.Category,
                    Price = viewModel.Price,
                    ImagePro = viewModel.ImagePro,
                    ManufacturingDate = DateOnly.FromDateTime(viewModel.ManufacturingDate)
                };

                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                // Chuyển hướng đến trang chi tiết của sản phẩm vừa tạo
                return RedirectToAction(nameof(Details), new { id = newProduct.ProductId });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // Tái sử dụng view FirstProductDetails để hiển thị chi tiết
            return View("FirstProductDetails", product);
        }


        // GET: /Products/FirstProductDetails
        // This action fetches the very first product from the database.
        public async Task<IActionResult> FirstProductDetails()
        {
            // Query the database for the first product, ordered by its primary key.
            // FirstOrDefaultAsync is efficient as it stops searching once a record is found.
            var firstProduct = await _context.Products
                                             .OrderBy(p => p.ProductId)
                                             .FirstOrDefaultAsync();

           
            if (firstProduct == null)
            {
                return NotFound("Không tìm thấy sản phẩm nào trong cơ sở dữ liệu.");
            }

            // Pass the found product object to the View.
            return View(firstProduct);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Create and populate the ViewModel from the Product entity
            var viewModel = new ProductEditViewModel
            {
                ProductId = product.ProductId,
                NamePro = product.NamePro,
                DecriptionPro = product.DecriptionPro,
                Category = product.Category,
                Price = product.Price,
                ImagePro = product.ImagePro,
                ManufacturingDate = product.ManufacturingDate.ToDateTime(TimeOnly.MinValue)
                // The responsibility of getting categories is now handled by the ViewComponent.
            };

            return View(viewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,NamePro,DecriptionPro,Category,Price,ImagePro,ManufacturingDate")] ProductEditViewModel viewModel)
        {
            if (id != viewModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productToUpdate = await _context.Products.FindAsync(id);
                    if (productToUpdate == null)
                    {
                        return NotFound();
                    }

                    productToUpdate.NamePro = viewModel.NamePro;
                    productToUpdate.DecriptionPro = viewModel.DecriptionPro;
                    productToUpdate.Category = viewModel.Category;
                    productToUpdate.Price = viewModel.Price;
                    productToUpdate.ImagePro = viewModel.ImagePro;
                    productToUpdate.ManufacturingDate = DateOnly.FromDateTime(viewModel.ManufacturingDate);

                    _context.Update(productToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(viewModel.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("FirstProductDetails"); 
            }

            
            return View(viewModel);
        }

       

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}

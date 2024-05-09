using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Migrations;
using WebApplication1.Models;
using WebApplication1.Models.Pagination;
using WebApplication1.Models.Products;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [HttpPost]
        public IActionResult Index(string email=null, int pg = 1)
        {

            if(email!=null)
            {
                var products = _context.Product.Where(p => p.UserEmail == email).ToList();
                ViewBag.userEmail = email;
                const int pageSize = 8;
                if (pg < 1)
                {
                    pg = 1;
                }
                int recsCount = products.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View(data);
            }
            else
            {
                if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                {
                    var products = _context.Product.ToList();
                    const int pageSize = 8;
                    if (pg < 1)
                    {
                        pg = 1;
                    }
                    int recsCount = products.Count();
                    var pager = new Pager(recsCount, pg, pageSize);
                    int recSkip = (pg - 1) * pageSize;
                    var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
                    this.ViewBag.Pager = pager;
                    return View(data);
                }
                else
                {
                    string userEmail = User.FindFirstValue(ClaimTypes.Email);
                    Console.WriteLine(userEmail);
                    var products = _context.Product.Where(p => p.UserEmail == userEmail).ToList();
                    const int pageSize = 8;
                    if (pg < 1)
                    {
                        pg = 1;
                    }
                    int recsCount = products.Count();
                    var pager = new Pager(recsCount, pg, pageSize);
                    int recSkip = (pg - 1) * pageSize;
                    var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
                    this.ViewBag.Pager = pager;
                    return View(data);
                }
                
            }
            
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ViewProduct(int id)
        {
            var product = _context.Product.FirstOrDefault(p => p.Id == id);
            return View("~/Views/Products/ViewProduct.cshtml", product);
        }
        [Authorize(Roles = "User,Admin")]
        public IActionResult AddProduct()
        {
            return View("~/Views/Products/AddProduct.cshtml");
        }
        [HttpPost]
        public IActionResult Create([FromForm] WebApplication1.Models.Products.Products product)
        {
            try
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    var Shape = HttpContext.Request.Form.Files[0];
                    if (Shape != null)
                    {
                        byte[] logoBytes = ConvertLogoToBytes(Shape);
                        product.Shape = logoBytes;
                    }
                }
                else
                {
                    ViewBag.Image = "No Image Uploaded!! please upload the image again in .png,.jpg \n";
                    Console.WriteLine("No files uploaded.");
                }

                _context.Product.Add(product);
                _context.SaveChanges();

                Console.WriteLine("Product saved successfully.");

                return RedirectToAction("Index", "Products");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                throw;
            }
        }

        private byte[] ConvertLogoToBytes(IFormFile Shape)
        {
            try
            {
                using (var binaryReader = new BinaryReader(Shape.OpenReadStream()))
                {
                    var bytes = binaryReader.ReadBytes((int)Shape.Length);
                    return bytes;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public IActionResult Update(Products product)
        {
            try
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    var Shape = HttpContext.Request.Form.Files[0];
                    if (Shape != null)
                    {
                        byte[] logoBytes = ConvertLogoToBytes(Shape);
                        product.Shape = logoBytes;
                    }
                }
                else
                {
                    Console.WriteLine("No files uploaded.");
                }

                _context.Update(product);
                _context.SaveChanges();
                Console.WriteLine("Product updates successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                throw;
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Console.WriteLine($"This id is {id}\n");
            var product = _context.Product.Find(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [HttpGet]
         public IActionResult SearchProduct(string searchTerm = null, string email = null)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                IQueryable<Products> productsQuery = _context.Product;
                string userEmail = User.Identity.Name;
                if (email != null)
                {
                    userEmail = email;
                    productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm)&&p.UserEmail==userEmail);
                    var products = productsQuery.ToList();
                    if (products.Count == 0)
                    {
                        ViewBag.Message = "No products found.";
                    }
                    return View("~/Views/Products/Index.cshtml", products);
                }
                if (User.IsInRole("User"))
                {
                    productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm) && p.UserEmail == userEmail);
                    var products = productsQuery.ToList();
                    if (products.Count == 0)
                    {
                        ViewBag.Message = "No products found.";
                    }
                    return View("~/Views/Products/Index.cshtml", products);
                }
                else if(User.IsInRole("Admin"))
                {
                    productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm));
                    var products = productsQuery.ToList();
                    if (products.Count == 0)
                    {
                        ViewBag.Message = "No products found.";
                    }
                    return View("~/Views/Products/Index.cshtml", products);
                }
                return View("~/Views/Products/Index.cshtml");

            }
            return View();
        }
        [HttpGet]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SearchAllProduct(string searchTerm = null)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                IQueryable<Products> productsQuery = _context.Product;
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm));
                var products = productsQuery.ToList();
                if (products.Count == 0)
                {
                    ViewBag.NoProductMessageFound = "No products found Against this keyword please login " +
                        "add this product.";
                }
                return View("~/Views/Home/Index.cshtml", products);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

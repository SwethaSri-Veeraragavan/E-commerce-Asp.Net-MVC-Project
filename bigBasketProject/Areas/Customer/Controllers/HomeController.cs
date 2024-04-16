using Bigbasket.DataAccess.Repository.IRepository;
using Bigbasket.Models;
using Bigbasket.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace bigBasketProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly BigBasketDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }

        public IActionResult Privacy()
        {
            ViewData["IsValid"] = true;
            ViewBag.IsValid = true;
            return View();
        }

        public IActionResult Shop()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }
        public IActionResult CategoryList(string categoryName)
        {
            // Retrieve the category based on the category name
            Category category = _unitOfWork.Category.Get(c => c.Name == categoryName);

            if (category == null)
            {
                return NotFound(); // Handle the case where the category is not found
            }

            // Retrieve products for the specified category ID
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll();

            // Filter the products based on the category ID
            List<Product> filteredProducts = new List<Product>();
            foreach (var product in productList)
            {
                if (product.CategoryId == category.Id)
                {
                    filteredProducts.Add(product);
                }
            }

            // Pass the filtered product list to the view
            return View(filteredProducts);
        }



        public IActionResult Details(int id)
        {
            Product? product = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (product == null)
            {
                return NotFound(); // Handle the case where the product is not found
            }
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}

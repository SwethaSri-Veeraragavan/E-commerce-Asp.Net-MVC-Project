using Bigbasket.DataAccess.Repository;
using Bigbasket.DataAccess.Repository.IRepository;
using Bigbasket.Models;
using Bigbasket.Models.ViewModels;
using Bigbasket.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace bigBasketProject.Areas.Customer.Controllers
{
    [Area("customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShoppingCartVM = new ShoppingCartVM
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                                                                    includeProperties: "Product"),
                OrderHeader = new OrderHeader()
            };

            IEnumerable<Product> products = _unitOfWork.Product.GetAll();

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                var product = products.FirstOrDefault(p => p.ProductId == cart.ProductId);
                if (product != null)
                {
                    cart.Product.ImageUrl = product.ImageUrl;
                    cart.Price = GetPriceBasedOnQuantity(cart);
                    ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
                }
            }

            return View(ShoppingCartVM);
        }


        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            return shoppingCart.Product.Price;
        }

        // Other action methods can go here...

        [HttpPost]
        [Authorize]
        public IActionResult AddToCart(int productId)
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Update the session variable with the count of items in the cart
            var cartItemCount = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count();
            HttpContext.Session.SetInt32(SD.SessionCart, cartItemCount);

            // Check if the product is already in the user's shopping cart
            var existingCartItem = _unitOfWork.ShoppingCart.Get(
                sc => sc.ApplicationUserId == userId && sc.ProductId == productId);

            if (existingCartItem != null)
            {
                // If the cart item already exists, increment the quantity
                existingCartItem.Count++;
                _unitOfWork.ShoppingCart.Update(existingCartItem);
            }
            else
            {
                // Create a new shopping cart item for the user
                var newCartItem = new ShoppingCart
                {
                    ApplicationUserId = userId, // Set the ApplicationUserId
                    ProductId = productId,
                    Count = 1 // Set the initial count to 1
                };
                _unitOfWork.ShoppingCart.Add(newCartItem);
            }

            _unitOfWork.Save();


            // Redirect back to the cart index page or any other page as needed
            return RedirectToAction("Index", "Cart");
        }

        // CartController.cs

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFromDb.Count <= 1)
            {
                // Remove item from cart if count is 1
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


    }



}

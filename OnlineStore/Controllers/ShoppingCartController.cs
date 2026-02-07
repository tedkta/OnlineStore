using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.ShoppingCart;
using OnlineStore.Services.ShoppingCart;
using OnlineStore.Services.Users;

public class ShoppingCartController : Controller
{
    private readonly IShoppingCartService shoppingCartService;
    private readonly LoggedUserService loggedUserService;

    public ShoppingCartController(IShoppingCartService shoppingCartService, LoggedUserService loggedUserService)
    {
        this.shoppingCartService = shoppingCartService;
        this.loggedUserService = loggedUserService;
    }

    public IActionResult ViewCart()
    {
        User user = loggedUserService.User;

        if (user == null)
        {
            return RedirectToAction("Login", "User");
        }
        int userId = user.Id;
        
        ShoppingCartViewModel cart = shoppingCartService.GetCart(userId);
        return View(cart);
    }

    public IActionResult AddToCart(int id)
    {
        User user = loggedUserService.User;
        if (user == null)
        {
            return RedirectToAction("Login", "User");
        }

        shoppingCartService.AddToCart(id, user.Id);
        return RedirectToAction("ViewCart");
    }
    
    public IActionResult RemoveFromCart(int id)
    {
        User user = loggedUserService.User;
        shoppingCartService.RemoveFromCart(id, user.Id);
        return RedirectToAction("ViewCart");
    }
}
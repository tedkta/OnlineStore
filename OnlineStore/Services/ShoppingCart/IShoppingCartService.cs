using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.Product;
using OnlineStore.Data.Models.ShoppingCart;

namespace OnlineStore.Services.ShoppingCart;

public interface IShoppingCartService
{
    void AddToCart(int id, int userId);
    void RemoveFromCart(int id, int userId);
    public ShoppingCartViewModel GetCart(int userId);
    public void ClearCart(int userId);
}
using OnlineStore.Data.Models.Entities;

namespace OnlineStore.Data.Models.ShoppingCart;

public class ShoppingCartViewModel
{
    public List<CartItem> CartItems { get; set; }
    public decimal  TotalPrice { get; set; }
    public int TotalQuantity { get; set; }
}
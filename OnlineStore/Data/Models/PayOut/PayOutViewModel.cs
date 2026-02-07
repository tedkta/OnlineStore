using OnlineStore.Data.Models.Entities;
namespace OnlineStore.Data.Models.PayOut;

public class PayOutViewModel
{
    
    public List<CartItem> CartItems { get; set; }
    public decimal TotalPrice => 
        CartItems?.Sum(i => (i.Product?.Price ?? 0m) * i.Quantity) ?? 0m;


    public string FullName   { get; set; } 
    public string Address    { get; set; }
    public string Email { get; set; }

    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CVV        { get; set; }
}
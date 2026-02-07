using OnlineStore.Data;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.PayOut;
using OnlineStore.Data.Models.ShoppingCart;
using OnlineStore.Services.ShoppingCart;

namespace OnlineStore.Services.PayOut;

public class PayOutService : IPayOutService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IShoppingCartService shoppingCartService;

    public PayOutService(ApplicationDbContext dbContext, IShoppingCartService shoppingCartService)
    {
        this.dbContext = dbContext;
        this.shoppingCartService = shoppingCartService;
    }


    public PayOutViewModel BuildPayoutModel(int userId)
    {
        ShoppingCartViewModel cart = shoppingCartService.GetCart(userId);
        return new PayOutViewModel
        {
            CartItems = cart.CartItems
        };
    }

    public void PlacePayout(int userId, PayOutViewModel model)
    {
        
        foreach (CartItem item in model.CartItems)
        {
            var product = dbContext.Products.Find(item.ProductId);
            Data.Models.Entities.PayOut payout = new Data.Models.Entities.PayOut
            {
                UserId = userId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                TotalPrice = product.Price * item.Quantity,
                CreatedAt = DateTime.UtcNow
            };
            dbContext.PayOuts.Add(payout);
        }
        
        dbContext.SaveChanges();
        shoppingCartService.ClearCart(userId);

    }
}
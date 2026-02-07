using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.ShoppingCart;
using OnlineStore.Services.ShoppingCart;

public class ShoppingCartService : IShoppingCartService
{
    private readonly ApplicationDbContext dbContext;

    public ShoppingCartService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void AddToCart(int id, int userId)
    {
        ProductsModel product = dbContext.Products.Find(id);
        if (product == null) return;

        CartItem existingItem = dbContext.CartItems
            .FirstOrDefault(c => c.ProductId == id && c.UserId == userId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            dbContext.CartItems.Add(new CartItem
            {
                ProductId = id,
                Quantity = 1,
                UserId = userId
            });
        }

        dbContext.SaveChanges();
    }

    public ShoppingCartViewModel GetCart(int userId)
    {
        List<CartItem> items = dbContext.CartItems
                .Where(c => c.UserId == userId)
            .Include(i => i.Product)
            .ToList();

        decimal totalPrice = items.Sum(i => i.Product.Price * i.Quantity);
        int totalQuantity = items.Sum(i => i.Quantity);

        return new ShoppingCartViewModel
        {
            CartItems = items,
            TotalPrice = totalPrice,
            TotalQuantity = totalQuantity
        };
    }

    public void RemoveFromCart(int id, int userId)
    {
        CartItem item = dbContext.CartItems.FirstOrDefault(i => i.ProductId == id && i.UserId == userId);
        if (item != null)
        {
            dbContext.CartItems.Remove(item);
            dbContext.SaveChanges();
        }
    }
    public void ClearCart(int userId)
    {
        var items = dbContext.CartItems.Where(c => c.UserId == userId).ToList();
        dbContext.CartItems.RemoveRange(items);
        dbContext.SaveChanges();
    }
}


using Moq;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.ShoppingCart;
using OnlineStore.Services.PayOut;
using OnlineStore.Services.ShoppingCart;

namespace OnlineStore.Test;

public class PayOutTest
{
    [Fact]
    public void BuildPayoutModel_ReturnsCartItems()
    {
      
        var cartMock = new Mock<IShoppingCartService>();

        cartMock.Setup(x => x.GetCart(1))
            .Returns(new ShoppingCartViewModel
            {
                CartItems = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 2 }
                }
            });

        var service = new PayOutService(null, cartMock.Object);

       
        var result = service.BuildPayoutModel(1);

       
        Assert.Single(result.CartItems); 
    }
}
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineStore.Data;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.Product;
using OnlineStore.Services.Product;
using OnlineStore.Services.Users;

namespace OnlineStore.Test;

public class ProductServiceTests
{
    [Fact]
    public void Create_ThrowsIfNotAdmin()
    {
        
        var dbMock = new Mock<ApplicationDbContext>();
        var userMock = new Mock<LoggedUserService>();
        userMock.Setup(u => u.IsAdmin).Returns(false);
        userMock.Setup(u => u.IsLogged).Returns(true);

        var service = new ProductService(dbMock.Object, userMock.Object);

        var dto = new CreateProductDto
        {
            Name = "Test",
            Description = "TestDesc",
            Price = 10,
            Brand = "Brand",
            Category = "Cat"
        };

     
        Assert.Throws<UnauthorizedAccessException>(() => service.Create(dto));
    }

    [Fact]
    public void Create_AddsProduct_WhenAdmin()
    {
        
        var dbMock = new Mock<ApplicationDbContext>();
        var productsMock = new Mock<DbSet<ProductsModel>>();
        dbMock.Setup(d => d.Products).Returns(productsMock.Object);

        var userMock = new Mock<LoggedUserService>();
        userMock.Setup(u => u.IsAdmin).Returns(true);
        userMock.Setup(u => u.IsLogged).Returns(true);

        var service = new ProductService(dbMock.Object, userMock.Object);

        var dto = new CreateProductDto
        {
            Name = "Test",
            Description = "TestDesc",
            Price = 10,
            Brand = "Brand",
            Category = "Cat"
        };

      
        service.Create(dto);

       
        productsMock.Verify(p => p.Add(It.IsAny<ProductsModel>()), Times.Once);
        dbMock.Verify(d => d.SaveChanges(), Times.Once);
    }
}


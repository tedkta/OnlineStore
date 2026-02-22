using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.Product;
using OnlineStore.Migrations;
using OnlineStore.Services.Users;

namespace OnlineStore.Services.Product;

public class ProductService : IProductService
{
        private readonly ApplicationDbContext dbContext; 
        private readonly LoggedUserService loggedUserService;

        public ProductService(ApplicationDbContext dbContext, LoggedUserService loggedUserService) 
        {
            this.loggedUserService = loggedUserService;
            this.dbContext = dbContext;
        } 

        public List<GetAllProductsViewModel> GetAll() 
        {

            return this.dbContext.Products
                .Select(w =>
                    new GetAllProductsViewModel()
                {
                    Id = w.Id,     //Използва се за да можем да се възползваме от CRUD опреациите
                    Name = w.Name,
                    Description = w.Description,
                    Price = w.Price,
                    Brand = w.Brand,
                    Category = w.Category,
                    Images = w.Images.ToList(),
                    

                })
                .ToList(); 
        }



        public GetProductViewModel Get(long id) //Използваме го за да вземем конкретен продукт и после да го покажем във View.
        {
            //Зареди данните на картинките
            var entity = dbContext.Products
            .Include(p => p.Images)                 
            .FirstOrDefault(p => p.Id == id);

            if (entity == null)
                return null;

            return new GetProductViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Brand = entity.Brand,
                Category = entity.Category,
                Images = entity.Images.Select(i => new ImageModel
                {
                    Id = i.Id,
                    Url = i.Url
                }).ToList()
            };
        }

        public void Create(CreateProductDto productDto) 
        {
            
            if (!loggedUserService.IsAdmin || !loggedUserService.IsLogged)
            {
                throw new UnauthorizedAccessException("Only administrators can delete products.");
            }
            
            ProductsModel newProduct = new ProductsModel();
            newProduct.Name = productDto.Name;
            newProduct.Description = productDto.Description;
            newProduct.Price = productDto.Price;
            newProduct.Brand = productDto.Brand;
            newProduct.Category = productDto.Category;
            dbContext.Products.Add(newProduct);
            dbContext.SaveChanges();
        } 
       

        public void Update(long id, UpdateProductDto updateProduct)
        {
            if (!loggedUserService.IsAdmin || !loggedUserService.IsLogged)
            {
                throw new UnauthorizedAccessException("Only administrators can delete products.");
            }

            ProductsModel newProduct = GetById(id);
            newProduct.Name = updateProduct.Name;
            newProduct.Description = updateProduct.Description;
            newProduct.Price = updateProduct.Price;
            newProduct.Brand = updateProduct.Brand;
            newProduct.Category = updateProduct.Category;
            dbContext.Products.Update(newProduct);
            dbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            if (!loggedUserService.IsAdmin || !loggedUserService.IsLogged)
            {
                throw new UnauthorizedAccessException("Only administrators can delete products.");
            }
            
            ProductsModel product = GetById(id);
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }

        private ProductsModel GetById(long id)
        {
            return dbContext.Products.FirstOrDefault(t => t.Id == id);
        }
}
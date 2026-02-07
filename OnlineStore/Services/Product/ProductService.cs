using OnlineStore.Data;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.Product;
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

                })
                .ToList(); 
        }



        public GetProductViewModel Get(long id) //Използваме го за да вземем конкретна тренировка и после да я покажем във View.
        {
            ProductsModel entity = GetById(id);
            GetProductViewModel model = new GetProductViewModel(); 

            model.Id = entity.Id; //Използва се за да можем да се възползваме от CRUD опреациите
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.Price = entity.Price;
            model.Brand = entity.Brand;
            model.Category = entity.Category;

            return model;
        } 

        public void Create(CreateProductDto productDto) 
        {
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
            ProductsModel product = GetById(id);
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }

        private ProductsModel GetById(long id)
        {
            return dbContext.Products.FirstOrDefault(t => t.Id == id);
        }
}
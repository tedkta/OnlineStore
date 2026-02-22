using OnlineStore.Data;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Services.Users;
using System.Security.Policy;

namespace OnlineStore.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly LoggedUserService loggedUserService;

        public ImageService(ApplicationDbContext dbContext, LoggedUserService loggedUserService)
        {
            this.loggedUserService = loggedUserService;
            this.dbContext = dbContext;
        }

        //Базови CRUD операции подобно на другите service имплементации
        //За по-лесна имплементация ползваме URL на снимка вместо самия файл

        public void Create(long id, string url)
        {
            var image = new ImageModel { ProductId = (int)id, Url = url };
            dbContext.Images.Add(image);
            dbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            ImageModel product = GetById(id);
            dbContext.Images.Remove(product);
            dbContext.SaveChanges();
        }

        public void Update(long id, string url)
        {
            ImageModel newImg = GetById(id);
            newImg.Url = url;
            dbContext.Images.Update(newImg);
            dbContext.SaveChanges();
        }

        private ImageModel GetById(long id)
        {
            return dbContext.Images.FirstOrDefault(t => t.Id == id);
        }
    }
}

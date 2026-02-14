using OnlineStore.Data.Models.Entities;

namespace OnlineStore.Services.Images
{
    public interface IImageService
    {
        void Create(long id, string url);
        void Update(long id, string url);
        void Delete(long id);
    }
}

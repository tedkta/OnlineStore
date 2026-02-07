using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.Product;

namespace OnlineStore.Services.Product;

public interface IProductService
{
  void Create(CreateProductDto productDto);
  void Update(long id, UpdateProductDto updateProduct);
  void Delete(long id);
  GetProductViewModel Get(long id);
  List<GetAllProductsViewModel> GetAll();
}
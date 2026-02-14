using OnlineStore.Data.Models.Entities;

namespace OnlineStore.Data.Models.Product;

public class GetAllProductsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }

    public List<ImageModel> Images { get; set; }
    public string? FirstImageURL { get; set; }

}
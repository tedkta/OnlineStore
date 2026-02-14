using OnlineStore.Data.Models.Entities;

namespace OnlineStore.Data.Models.Product;

public class GetProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }
    public List<ImageModel> Images { get; set; } = new();
}
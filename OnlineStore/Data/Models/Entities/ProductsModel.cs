using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data.Models.Entities;

public class ProductsModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }

    //One to many relationship with images
    public ICollection<ImageModel> Images { get; set; } = new List<ImageModel>();

   
}
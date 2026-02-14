using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data.Models.Entities;
public class ImageModel
{
    [Key]
    public int Id { get; set; }
    public string Url { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public ProductsModel Product { get; set; } = null!;
}

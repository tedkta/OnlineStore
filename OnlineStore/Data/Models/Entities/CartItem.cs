using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data.Models.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")] 
    public ProductsModel Product { get; set; }
    public int Quantity { get; set; }

}
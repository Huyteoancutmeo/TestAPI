using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Data
{
    [Table("cart_item")]
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        public int ProductID { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public Product Product { get; set;}
    }
}

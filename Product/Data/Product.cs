using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Data
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public long Price { get; set; }
        public float Star { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreateAt { get; set; } 
        public DateTime? UpdateAt { get; set; } 

        public ICollection<CartItem> CartItems { get; set; }
        public Category Categories { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

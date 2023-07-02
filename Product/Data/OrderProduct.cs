using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Data
{
    [Table("order_product")]
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Data
{
    [Table("order")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public Guid UserID { get; set; }
        public string Address { get; set; }
        public double Phone { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public User User { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

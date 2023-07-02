using System.ComponentModel.DataAnnotations;

namespace Products.Model
{
    public class CartItemModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Guid UserID { get; set; }
    }
}

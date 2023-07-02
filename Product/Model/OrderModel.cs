namespace Products.Model
{
    public class OrderModel
    {
        public Guid UserID { get; set; }
        public string Address { get; set; }
        public double Phone { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

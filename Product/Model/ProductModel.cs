namespace Products.Model
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public long Price { get; set; }
        public float Star { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}

namespace Products.Model
{
    public class UserModel
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Name { get; set; }
        //public DateTime DateOfBird { get; set; }
        public string UserEmail { get; set; }
        //public int Sex { get; set; }
        public string Address { get; set; }
        public double Phone { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;

    }
}

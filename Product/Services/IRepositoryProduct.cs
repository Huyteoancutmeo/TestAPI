using Products.Data;

namespace Products.Services
{
    public interface IRepositoryProduct
    {
        List<Product> GetAll(string search, double? from, double? to, string sortBy, int page = 1);
        Product GetById(int id);
        Product Add(Product product);
        void Update(Product product, int id);
        void Delete(int id);
    }
}

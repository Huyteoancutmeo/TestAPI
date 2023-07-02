using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Products.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly MyDbContext _context;
        public ProductController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            try
            {
                var products = _context.Products.ToList();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{page}")]
        public IActionResult GetByPage(int pageSize, int page = 1)
        {
            try
            {
                var products = _context.Products.AsQueryable();
                var result = PagingList<Product>.Create(products, page, pageSize);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(p => p.ProductID == id);
                if (product == null)
                {
                    return BadRequest();
                }
                return Ok(product);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]

        public IActionResult AddNewProduct(ProductModel model)
        {
            try
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CategoryID = model.CategoryID,
                    Image = model.Image,
                    Price = model.Price,
                    Star = model.Star,
                    Quantity = model.Quantity,
                    CreateAt = model.CreateAt,
                    UpdateAt = model.UpdateAt
                };
                _context.Products.Add(product);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]

        public IActionResult UpdateProduct(ProductModel model, int id)
        {

            var product = _context.Products.SingleOrDefault(p => p.ProductID == id);
            if (product != null)
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Image = model.Image;
                product.Price = model.Price;
                product.Star = model.Star;
                product.Quantity = model.Quantity;
                product.UpdateAt = model.UpdateAt;

                _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(p =>
            p.ProductID == id);

                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("search")]
        public IActionResult SearchProduct(string search)
        {
            try
            {
                var products = _context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(search))
                {
                    products = products.Where(p => p.Name.Contains(search));
                }
                return Ok(products.ToList());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("filter")]
        public IActionResult Filter(double? from, double? to, string sort)
        {
            try
            {
                var products = _context.Products.AsQueryable();
                if (from.HasValue)
                {
                    products = products.Where(p => p.Price >= from);
                }
                if (to.HasValue)
                {
                    products = products.Where(p => p.Price <= to);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    switch (sort)
                    {
                        case "desc":
                            products = products.OrderByDescending(p => p.Price)
                                ; break;
                        case "esc":
                            products = products.OrderBy(p => p.Price)
                                ; break;
                    }
                }
                return Ok(products.ToList() );
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

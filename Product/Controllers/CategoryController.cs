using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Model;

namespace Products.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : Controller
    {
        private readonly MyDbContext _context;
        public CategoryController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll() 
        {
            var ListCategory = _context.Categories.ToList();
            return Ok(ListCategory);
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryModel model)
        {
            try
            {
                var category = new Category()
                {
                    Name = model.Name,
                    Description = model.Description
                };
                _context.Categories.Add(category);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();
            }
            return View();
        }
        [HttpPut]
        public IActionResult UpdateCategory(int id, CategoryModel model)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(c => c.CategoryID == id);
                if(category != null)
                {
                    category.Name = model.Name;
                    category.Description = model.Description;

                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK);
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
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(c => c.CategoryID == id);
                if (category != null)
                {
                    return Ok(category);
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
    }
}

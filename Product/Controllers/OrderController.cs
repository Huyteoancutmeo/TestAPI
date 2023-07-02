using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Model;

namespace Products.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly MyDbContext _context;
        public OrderController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var orders = _context.Orders.ToList();
                return Ok(orders);
            }
            catch
            {
                return BadRequest();
            }
            
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderByIdUser(Guid id) 
        {
            try
            {
                var orders = _context.Orders.Where(o => o.UserID == id);
                if(orders.Any())
                {
                    return Ok(orders);
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

using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Model;

namespace Products.Controllers
{
    [ApiController]
    [Route("cart_item")]
    public class CartController : Controller
    {
        private readonly MyDbContext _context;
        public CartController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var ListCartItems = _context.CartItems.ToList();
                return Ok(ListCartItems);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdUser(Guid id) 
        {
            try
            {
                var ListCartItems = _context.CartItems.Where(ci => ci.UserID == id);
                if(ListCartItems.Any())
                {
                    return Ok(ListCartItems);
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
        [HttpPost]
        public IActionResult AddCartItem(CartItemModel cartItemModel)
        {
            try
            {
                var cartItem = new CartItem
                {
                    ProductID = cartItemModel.ProductID,
                    Quantity = cartItemModel.Quantity,
                    UserID = cartItemModel.UserID
                };
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCartItem(int id)
        {
            try
            {
                var cartItem = _context.CartItems.SingleOrDefault(ci => ci.CartItemID == id);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
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
    }
}

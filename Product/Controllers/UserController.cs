using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Products.Data;
using Products.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Products.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly MyDbContext _context;
        private readonly AppSettings _appSettings;
        public UserController(MyDbContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var ListUser = _context.Users.ToList();
                return Ok(ListUser);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult AddNewUser(UserModel model)
        {
            try
            {
                var user = new User
                {
                    UserID = model.UserID,
                    UserName = model.UserName,
                    UserPassword = model.UserPassword,
                    Name = model.Name,
                    UserEmail = model.UserEmail,
                    Address = model.Address,
                    Phone = model.Phone,
                    CreateAt = model.CreateAt,
                    UpdateAt = model.UpdateAt,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();
            }   
        }
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            var user = _context.Users.SingleOrDefault(p => p.UserName == model.UserName &&
            p.UserPassword == model.Password);

            if(user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username/password"
                });
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Anthentication success",
                Data = GenerateToken(user)
            });
        }
        [HttpPut]
        [Authorize]
        public IActionResult ChangePassword(Guid id,string OldPassword, string NewPassword)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.UserID == id);
                if(user != null)
                {
                    if(user.UserPassword == OldPassword)
                    {
                        user.UserPassword = NewPassword;
                        _context.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status410Gone);
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch { return BadRequest(); }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.UserID==id);
                if(user != null )
                {
                    _context.Users.Remove(user);
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
        private string GenerateToken(User user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.UserEmail),
                    new Claim("Username", user.UserName),
                    new Claim("Id", user.UserID.ToString()),
                    //role

                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = JwtTokenHandler.CreateToken(tokenDescription);
            return JwtTokenHandler.WriteToken(token);
        }
    }
}

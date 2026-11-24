using Microsoft.AspNetCore.Mvc;
using QuickPay.Api.DTO;
using QuickPay.Api.Models;
using QuickPay.Api.Services;

namespace QuickPay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    { private readonly IUserService _Service;
        public UserController(IUserService Service)

        {
            _Service = Service;

        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUserDto dto)
        {
            //DTO ko user Entity mapping karao
            User user = new User() {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };

            //Service ko await karao
            await _Service.CreateUserAsync(user);

            //Http status return karao 
            return Ok("User Created Successfully");

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var users = await _Service.GetAllUserAsync();
            var response = users.Select(users => new UserResponseDTO
            { Id = users.Id,
                Name = users.Name,
                Email = users.Email,
                Phone = users.Phone

            }).ToList();

            return Ok(response);



        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _Service.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("User Not found");

            }
            var response = new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };
        
            return Ok(response);
        }

       

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id,UpdatedUserDTO dto)
        {
            var usr = await _Service.GetByIdAsync(id);
           
            if(usr == null)
            {
                return NotFound("User Does not Exists");
            }

            usr.Name = dto.Name;
            usr.Email = dto.Email;
            usr.Phone = dto.Phone;

            await _Service.UpdateUserAsync(usr);

            return Ok("SuccessFully User Updated");

        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteUserAsync(int id)
        { var usr = await _Service.GetByIdAsync(id);
            if(usr == null)
            {
                return NotFound("User NNot Found");
            }
            await _Service.DeleteUserAsync(id);
            return NoContent();

        }


    }
}

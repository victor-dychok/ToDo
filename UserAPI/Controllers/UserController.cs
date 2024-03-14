using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using UserServices;
using UserServices.dto;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _users;
        public UserController(IUserService users)
        {
            _users = users;
        }

        [HttpGet]
        public IActionResult GetAll(int? offset, string? name, int? limit)
        {
            return Ok(_users.GetListAsync(offset, name, limit));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {

            var item = _users.GetByIdOrDefaultAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            else return Ok(item);
        }

        [HttpPost]
        public IActionResult Post(UserDto item)
        {
            var postedItem = _users.AddAsync(item);
            if (postedItem != null)
            {
                return Created("/todos", item);
            }
            else return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(UserDto newItem)
        {
            var item = _users.UpdateAsync(newItem);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var item = _users.GetByIdOrDefaultAsync(id);
            var flag = _users.DeleteAsync(id).Result;
            if (flag)
            {
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }
    }
}

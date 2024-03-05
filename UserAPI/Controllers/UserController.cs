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
            return Ok(_users.GetList(offset, name, limit));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {

            var item = _users.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            else return Ok(item);
        }

        [HttpPost]
        public IActionResult Post(UserDto item)
        {
            var postedItem = _users.Add(item);
            if (postedItem != null)
            {
                return Created("/todos", item);
            }
            else return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(User newItem)
        {
            var item = _users.Update(newItem);

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
            var item = _users.GetById(id);
            var flag = _users.Delete(id);
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

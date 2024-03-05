using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using UserServices;

namespace UsersAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
            _userService = service;
        }
 
 
        [HttpGet]
        [Route("users/getAll")]
        public IActionResult GetAll(int? offset, string? name, int? limit)
        {
            return Ok(_userService.GetList(offset, name, limit));
        }
 
        [HttpGet("{id:int}")]
        [Route("users/getById")]
        public IActionResult GetById(int id)
        {
 
            var item = _userService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            else return Ok(item);
        }
 
        [HttpPost]
        [Route("users/addUser")]
        public IActionResult Post(User item)
        {
            var postedItem = _userService.Add(item);
            if (postedItem != null)
            {
                return Created("/users", item);
            }
            else return BadRequest();
 
 
        }
 
        [HttpPut]
        [Route("users/putUser")]
        public IActionResult Put(User newItem)
        {
            var item = _userService.Update(newItem);
 
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
        [Route("users/deleteUser")]
        public IActionResult Delete(int id)
        {
            var item = _userService.GetById(id);
            var flag = _userService.Delete(id);
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

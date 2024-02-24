using Microsoft.AspNetCore.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{

    [ApiController]
    [Route("todos")]
    public class ToDoController : ControllerBase
    {
        private static readonly List<TodoItem> _todoItems = new List<TodoItem> {
            new TodoItem(1, "Lable 1", true, DateTime.UtcNow, DateTime.UtcNow),
            new TodoItem(2, "Lable 2", false, DateTime.UtcNow, DateTime.UtcNow),
            new TodoItem(3, "Lable 3", true, DateTime.UtcNow, DateTime.UtcNow),
            new TodoItem(4, "Lable 4", false, DateTime.UtcNow, DateTime.UtcNow),
            new TodoItem(5, "Lable 5", false, DateTime.UtcNow, DateTime.UtcNow)
        };

        [HttpGet]
        [Route("/todos")]
        public IActionResult GetAll()
        {
            return Ok(_todoItems);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) 
        {
            var item = _todoItems.FirstOrDefault(x=>x.Id==id);
            if(item==null)
            {
                return NotFound();
            }
            else return Ok(item);
        }
        [HttpGet("{id:int}/IsDone")]
        public IActionResult GetByIdIsDone(int id)
        {
            var item = _todoItems.FirstOrDefault(x => x.Id == id && x.IsDone == true);
            if (item == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(item);
            }
        }

        [HttpPost]
        public IActionResult Post(TodoItem item)
        {
            item.CreatedDate = DateTime.UtcNow;
            _todoItems.Add(item);
            return Ok(item);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, TodoItem newItem)
        {
            var item = _todoItems.SingleOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                item.Label = newItem.Label;
                item.UpdatedDate = DateTime.UtcNow;
                item.IsDone = newItem.IsDone;
                return Ok(item);
            }
        }

        [HttpPatch("{id:int}/IsDone")]
        public IActionResult Putch(int id)
        {
            var item = _todoItems.SingleOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                item.IsDone = true;
                return Ok(item);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var item = _todoItems.SingleOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _todoItems.Remove(item);
                return Ok(item);
            }
        }
    }
}

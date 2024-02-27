using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Respounces;

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
        public IActionResult GetAll(int limit, int offset)
        {
            if(Validation.IsOkIntIdValue(limit)&&Validation.IsOkIntIdValue(offset))
            {
                return Ok( //-1
                _todoItems
                .OrderBy(b => b.Id)
                .Skip(offset)
                .Take(limit)
                .ToList());
            }
            else return BadRequest("Incorrect limit or offset value");
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
            var respItem = _todoItems.FirstOrDefault(x => x.Id == id && x.IsDone == true);
            if (respItem == null)
            {
                return NotFound();
            }
            else
            {
                var respounce = new ToDoIdFlagResp(respItem.Id, respItem.IsDone);
                return Ok(respounce);
            }
        }

        [HttpGet("/Undone")]
        public IActionResult GetAllUndone()
        {
            var respData = _todoItems.Where(x => x.IsDone == false);
            if (respData == null)
            {
                return NotFound();
            }
            else
            {
                var respounceList = new List<ToDoIdFlagResp>();
                foreach (var item in respData)
                {
                    respounceList.Add(new ToDoIdFlagResp(item.Id, item.IsDone));
                }
                return Ok(respounceList);
            }
        }

        [HttpPost("/todos/add")]
        
        public IActionResult Post(TodoItem item)
        {
            if(item != null)
            {
                int idToSet = item.Id;
                var sameId = _todoItems.SingleOrDefault(x => x.Id == item.Id); //First??

                if (sameId == null)
                {
                    item.CreatedDate = DateTime.UtcNow;
                    item.UpdatedDate = DateTime.UtcNow;
                    _todoItems.Add(item);
                    return Created("/todos/add", item);
                }
                else return BadRequest();
            }
            else return BadRequest();


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
                ToDoIdFlagResp respounce = new ToDoIdFlagResp(item.Id, item.IsDone); //item.Id, true??
                return Ok(respounce);
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

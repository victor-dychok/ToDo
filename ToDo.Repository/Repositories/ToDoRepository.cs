using ToDo.Interfaces;
using ToDoDomain;

namespace ToDo.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private static readonly List<TodoItem> _todoItems = new List<TodoItem> {
            new TodoItem(1, "Lable 1", true, DateTime.UtcNow, DateTime.UtcNow, 1),
            new TodoItem(2, "Do staf", false, DateTime.UtcNow, DateTime.UtcNow, 2),
            new TodoItem(3, "Lable 3", true, DateTime.UtcNow, DateTime.UtcNow, 3),
            new TodoItem(4, "Lable 4", false, DateTime.UtcNow, DateTime.UtcNow, 1),
            new TodoItem(5, "Lable 5", false, DateTime.UtcNow, DateTime.UtcNow, 2)
        };

        public IEnumerable<TodoItem> GetList(int? offset, string? lable, int? ownerId, int? limit = 10)
        {
            IEnumerable<TodoItem> todos = _todoItems.OrderBy(b => b.Id);

            if (!string.IsNullOrWhiteSpace(lable))
            {
                todos = todos.Where(t => t.Label.Contains(lable, StringComparison.InvariantCultureIgnoreCase));
            }
            if (offset.HasValue)
            {
                todos = todos.Skip(offset.Value);
            }
            if(ownerId.HasValue)
            {
                todos = todos.Where(x => x.OwnerId == ownerId.Value);
            }
            var result = limit.HasValue ? todos.Take(limit.Value) : todos;
            return result;
        }

        public TodoItem? GetById(int id)
        {
            return _todoItems.SingleOrDefault(x => x.Id == id);
        }
        public TodoItem? GetByIdIsDone(int id)
        {
            return _todoItems.FirstOrDefault(x => x.Id == id && x.IsDone == true);
        }

        public bool Delete(int id)
        {
            var item = _todoItems.SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                _todoItems.Remove(item);
            }
            return item != null;
        }

        public TodoItem? Add(TodoItem item)
        {
            if (item != null)
            {
                int idToSet = item.Id;
                var sameId = _todoItems.SingleOrDefault(x => x.Id == item.Id);

                if (sameId == null)
                {
                    item.CreatedDate = DateTime.UtcNow;
                    item.UpdatedDate = DateTime.UtcNow;
                    _todoItems.Add(item);
                }
            }

            return item;
        }

        public TodoItem? Put(int id, TodoItem newItem)
        {
            var item = _todoItems.SingleOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.Label = newItem.Label;
                item.UpdatedDate = DateTime.UtcNow;
                item.IsDone = newItem.IsDone;
            }
            return item;
        }

        public TodoItem? Putch(int id, bool isDone)
        {
            var item = _todoItems.SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsDone = isDone;
            }
            return item;
        }
    }
}

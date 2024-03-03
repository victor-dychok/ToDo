

using ToDoDomain;

namespace ToDoRepository
{
    public interface IToDoRepository
    {
        public IEnumerable<TodoItem> GetList(int? offset, string? lable, int? ownerId, int? limit);
        public TodoItem? GetById(int id);
        public TodoItem? GetByIdIsDone(int id);
        public bool Delete(int id);
        public TodoItem? Add(TodoItem item);
        public TodoItem? Put(int id, TodoItem newItem);
        public TodoItem? Putch(int id, bool isDone);
    }
}

using System.Reflection;
using ToDoBL.dto;
using ToDoDomain;
using ToDo;
using ToDo.Interfaces;
using Common.Repository;

namespace ToDoBL
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IUserRepository _users;
        public ToDoService(IToDoRepository repository, IUserRepository user)
        {
            _toDoRepository = repository;
            _users = user;
        }
        public IEnumerable<TodoItem> GetList(int? offset, string? lable, int? ownerId, int? limit = 10)
        {
            return _toDoRepository.GetList(offset, lable, ownerId, limit);
        }

        public TodoItem? GetById(int id)
        {
            return _toDoRepository.GetById(id);
        }
        public TodoItem? GetByIdIsDone(int id)
        {
            return _toDoRepository.GetByIdIsDone(id);
        }

        public bool Delete(int id)
        {
            return _toDoRepository.Delete(id);
        }

        public TodoItem? Add(TodoItem item)
        {
            int ownerId = item.OwnerId;
            var user = _users.GetById(ownerId);
            if(user != null)
            {
                return _toDoRepository.Add(item);
            }
            return null;
        }

        public TodoItem? Put(int id, TodoItem newItem)
        {
            return _toDoRepository.Put(id, newItem);
        }

        public TodoItem? Putch(int id, bool isDone)
        {
            return _toDoRepository.Putch(id, isDone);
        }
    }
}

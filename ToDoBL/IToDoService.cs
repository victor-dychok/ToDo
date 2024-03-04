﻿using ToDoBL.dto;
using ToDoDomain;

namespace ToDoBL
{
    public interface IToDoService
    {
        public IEnumerable<TodoItem> GetList(int? offset, string? lable, int? limit, int? ownerId);
        public TodoItem? GetById(int id);
        public TodoItem? GetByIdIsDone(int id);
        public bool Delete(int id);
        public TodoItem? Add(CreateToDo item);
        public TodoItem? Update(UpdateToDo newItem);
        public TodoItem? Putch(int id, bool isDone);
    }
}

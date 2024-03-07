﻿using System.Reflection;
using ToDoBL.dto;
using ToDoDomain;
using Common.Repository;
using AutoMapper.Internal;
using AutoMapper;
using Common.Domain;
using System.Collections.Generic;
using System.Linq.Expressions;
using ToDoBL.Validators;
using FluentValidation;
using System.Reflection.Metadata.Ecma335;

namespace ToDoBL
{
    public class ToDoService : IToDoService
    {
        private readonly IRepository<TodoItem> _toDoRepository;
        private readonly IRepository<User> _users;
        private readonly IMapper _mapper;
        public ToDoService(
            IRepository<TodoItem> repository,
            IRepository<User> user,
            IMapper mapper)
        {
            _mapper = mapper;

            _toDoRepository = repository;

            if(_toDoRepository.GetList().Count() == 0)
            {
                _toDoRepository.Add(new TodoItem { Id = 1, Label = "Lable 1", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, OwnerId = 1 });
                _toDoRepository.Add(new TodoItem { Id = 2, Label = "Lable 1 test", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, OwnerId = 2 });
                _toDoRepository.Add(new TodoItem { Id = 3, Label = "Lable 2", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, OwnerId = 3 });
                _toDoRepository.Add(new TodoItem { Id = 4, Label = "Lable 3", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, OwnerId = 1 });
            }

            _users = user;
            if(_users.GetList().Count() == 0)
            {
                _users.Add(new User { Id = 1, Name = "Vasia" });
                _users.Add(new User { Id = 2, Name = "Sasha" });
                _users.Add(new User { Id = 3, Name = "Petia" });
            }
        }
        public IEnumerable<TodoItem> GetList(int? offset, int? ownerId, string? lable, int? limit = 10)
        {
            // Expression<Func<TEntity, bool>>? predicate = null,
            IEnumerable<TodoItem> result = _toDoRepository.GetList(
                offset,
                limit,
                d => (string.IsNullOrWhiteSpace(lable) || d.Label.Contains(lable, StringComparison.InvariantCultureIgnoreCase)) 
                && (ownerId == null || d.OwnerId == ownerId.Value),
                t => t.Id);
            return result;
        }

        public TodoItem? GetById(int id)
        {
            return _toDoRepository.SingleOrDefault(t => t.Id == id);
        }
        public TodoItem? GetByIdIsDone(int id)
        {
            return _toDoRepository.GetList(null, null, t => t.IsDone == true, t => t.Id, false)[0];
        }

        public bool Delete(int id)
        {
            return _toDoRepository.Delete(GetById(id));
        }

        public TodoItem? Add(CreateToDo item)
        {
            int ownerId = item.OwnerId;
            var user = _users.SingleOrDefault(u => u.Id == ownerId);



            if(user == null)
            {
                return null;
            }
            
            var todoEntity = _mapper.Map<CreateToDo, TodoItem>(item);
            todoEntity.CreatedDate = DateTime.UtcNow;
            todoEntity.UpdatedDate = DateTime.UtcNow;

            var list = _toDoRepository.GetList();
            todoEntity.Id = _toDoRepository.GetList().Length == 0 ? 1 : _toDoRepository.GetList().Max(l => l.Id) + 1;

            return _toDoRepository.Add(todoEntity);
        }

        public TodoItem? Update(UpdateToDo updateDto)
        {
            var todoEntity = GetById(updateDto.Id);
            todoEntity = _mapper.Map<UpdateToDo, TodoItem>(updateDto);
            if(todoEntity == null)
            {
                return null;
            }
            _mapper.Map(updateDto, todoEntity);
            todoEntity.UpdatedDate = DateTime.UtcNow;

            return _toDoRepository.Update(todoEntity);
        }

        public TodoItem? Putch(int id, bool isDone)
        {
            var TodoItem = GetById(id);
            if(TodoItem == null)
            {  return null; }

            TodoItem.IsDone = isDone;
            return _toDoRepository.Update(TodoItem);
        }
    }
}

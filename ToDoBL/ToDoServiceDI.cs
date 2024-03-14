﻿using Common.Domain;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoBL.dto;
using ToDoBL.Mapper;
using ToDoBL.Validators;
using ToDoDomain;
using UserServices;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace ToDoBL
{
    public static class ToDoServiceDI
    {
        public static IServiceCollection AddToDoServices(this IServiceCollection services)
        {

            services.AddScoped<IToDoService, ToDoService>();
            services.AddScoped<IRepository<TodoItem>, SqlServerBaseReository<TodoItem>>();
            services.AddScoped<IRepository<User>, SqlServerBaseReository<User>>();
            services.AddScoped<IUserService, UserService>();

            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly()}, includeInternalTypes: true);

            services.AddAutoMapper(typeof(Mapper.AutoMapperProfile));
            return services;
        }
    }
}

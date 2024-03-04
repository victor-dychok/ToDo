using Common.Domain;
using Common.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoBL.Mapper;
using ToDoDomain;
using UserServices;

namespace ToDoBL
{
    public static class ToDoServiceDI
    {
        public static IServiceCollection AddToDoServices(this IServiceCollection services)
        {

            services.AddSingleton<IToDoService, ToDoService>();
            services.AddSingleton<IRepository<TodoItem>, BaseRepository<TodoItem>>();
            services.AddSingleton<IRepository<User>, BaseRepository<User>>();
            services.AddSingleton<IUserService, UserService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }
    }
}

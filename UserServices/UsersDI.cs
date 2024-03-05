using Common.Domain;
using Common.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices
{
    public static class UsersDI
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddSingleton<IRepository<User>, BaseRepository<User>>();
            services.AddSingleton<IUserService, UserService>();
            object value = services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }
    }
}

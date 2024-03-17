using Common.Domain;
using Common.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UserServices
{
    public static class UsersDI
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddTransient<IRepository<User>, SqlServerBaseReository<User>>();
            services.AddTransient<IUserService, UserService>(); 
            services.AddTransient<IAutnService, AuthService>();
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);
            object value = services.AddAutoMapper(typeof(AutoMapperProfile));



            return services;
        }
    }
}

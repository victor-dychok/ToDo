using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetList(int? offset, string? name, int? limit);
        public User? GetById(int id);
        public bool Delete(int id);
        public User? Add(User item);
        public User? Put(int id, User newItem);
    }
}

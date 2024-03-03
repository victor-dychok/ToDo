using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>()
        {
            new User {Id = 1, Name = "Vania"},
            new User {Id = 2, Name = "Sania"},
            new User {Id = 3, Name = "Petia"},
            new User {Id = 11, Name = "Chebupej228"},
        };

        public User? Add(User item)
        {
            if(item != null)
            {
                _users.Add(item);
            }
            return item;
        }

        public bool Delete(int id)
        {
            var user = _users.Single(x => x.Id == id);
            if(user != null)
            {
                _users.Remove(user);
                return true;
            }
            return false;
        }

        public User? GetById(int id)
        {
            var user = _users.SingleOrDefault(x => x.Id == id);
            return user;
        }

        public IEnumerable<User> GetList(int? offset, string? name, int? limit)
        {
            IEnumerable<User> todos = _users.OrderBy(b => b.Id);

            if (!string.IsNullOrWhiteSpace(name))
            {
                todos = todos.Where(t => t.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            }
            if (offset.HasValue)
            {
                todos = todos.Skip(offset.Value);
            }
            var result = limit.HasValue ? todos.Take(limit.Value) : todos;
            return result;
        }

        public User? Put(int id, User newItem)
        {
            var user = _users.Single(x=>x.Id == id);
            if (user != null)
            {
                user.Name = newItem.Name;
            }
            return user;
        }

    }
}

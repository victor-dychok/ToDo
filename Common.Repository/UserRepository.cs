using Common.Domain;

namespace Common.Repository
{
    public class UserRepository : IUserRepository
    {

        private static readonly List<User> _users = new List<User> {
            new User(1, "Vania"),
            new User(2, "Petia"),
            new User(3, "Sasha")
        };
        public User Add(User user)
        {
            _users.Add(user);
            return user;
        }

        public bool Delete(int id)
        {
            var user = _users.SingleOrDefault(x => x.Id == id);
            if(user != null)
            {
                _users.Remove(user);
                return true;
            }
            return false;
        }

        public User? GetById(int id)
        {
            return _users.SingleOrDefault(user => user.Id == id);
        }

        public IEnumerable<User> GetList(int? offset, string? name, int? limit)
        {
            return _users;
        }

        public User? Put(int id, User user)
        {
            var updUser = _users.SingleOrDefault(x => x.Id == id);
            if(updUser != null)
            {
                updUser.Name = user.Name;
            }
            return updUser;
        }

    }
}
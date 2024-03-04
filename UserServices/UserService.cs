using Common.Domain;
using Common.Repository;

namespace UserServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> users)
        {
            _userRepository = users;

        }

        public User? Add(User item)
        {
            item.Id = _userRepository.GetList().Length == 0 ? 1 : _userRepository.GetList().Max(l => l.Id) + 1;
            return _userRepository.Add(item);
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user is null)
            {
                return false;
            }
            return _userRepository.Delete(user);
        }

        public User? GetById(int id)
        {
            return _userRepository.SingleOrDefault(b => b.Id == id);
        }

        public IEnumerable<User> GetList(int? offset, string? name, int? limit)
        {
            return _userRepository.GetList(
                offset,
                limit,
                name == null ? null : u => u.Name.Contains(name),
                u => u.Id);
        }

        public User? Update(int id, User newItem)
        {
            var user = GetById(id);
            if (user is null)
            {
                return null;
            }
            return _userRepository.Update(user);
        }
    }
}

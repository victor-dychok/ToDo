using Common.Domain;
using Common.Repository;

namespace UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userService)
        {
            _userRepository = userService;
        }

        public User? Add(User item)
        {
            return _userRepository.Add(item);
        }

        public bool Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public User? GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetList(int? offset, string? name, int? limit)
        {
            return _userRepository.GetList(offset, name, limit);
        }

        public User? Put(int id, User newItem)
        {
            return _userRepository.Put(id, newItem);
        }
    }
}

using AutoMapper;
using Common.Domain;
using Common.Repository;
using UserServices.dto;

namespace UserServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public UserService(IRepository<User> users, IMapper mapper)
        {
            _userRepository = users;
            _mapper = mapper;

            _userRepository.Add(new User { Id = 1, Name = "Vasia" });
            _userRepository.Add(new User { Id = 2, Name = "Sasha" });
            _userRepository.Add(new User { Id = 3, Name = "Petia" });
        }

        public User? Add(UserDto itemDto)
        {
            var item = new User();
            item = _mapper.Map<UserDto, User>(itemDto);
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

        public User? Update(User newItem)
        {
            return _userRepository.Update(newItem);
        }
    }
}

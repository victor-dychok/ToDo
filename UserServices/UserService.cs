using AutoMapper;
using Common.Api;
using Common.BL;
using Common.Domain;
using Common.Repository;
using ToDoDomain;
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
        }

        public async Task<User> AddAsync(UserDto item, CancellationToken token = default)
        {
            var user = _mapper.Map<UserDto, User>(item);
            user.PasswordHash = "qwe323r43t";
            var addedItem = await _userRepository.AddAsync(user, token);
            if(addedItem is null)
            {
                throw new BadRequestExeption("Can not add user");
            }
            return addedItem;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken token = default)
        {
            var item = await GetByIdOrDefaultAsync(id);
            if(item == null)
            {
                throw new NotFoundExeption(new {Id = id});
            }
            return await _userRepository.DeleteAsync(item, token);
        }

        public async Task<User> GetByIdOrDefaultAsync(int id, CancellationToken token = default)
        {
            var item = await _userRepository.SingleOrDefaultAsync(x => x.Id == id);
            if (item is null)
            {
                throw new NotFoundExeption(new { Id = id });
            }
            return item;
        }

        public async Task<IReadOnlyCollection<User>> GetListAsync(int? offset, string? name, int? limit, CancellationToken token = default)
        {
            return await _userRepository.GetListAsync(
                offset,
                limit,
                name == null ? null : u => u.Login.Contains(name),
                token: token);
        }

        public async Task<User> UpdateAsync(UserDto newItem, CancellationToken token = default)
        {
            var user = _mapper.Map<UserDto, User>(newItem);
            user.PasswordHash = "qwe323r43t";
            var item = await _userRepository.UpdateAsync(user, token);
            if(item is null)
            {
                throw new BadRequestExeption("Can not update user");
            }
            return item;
        }
    }
}

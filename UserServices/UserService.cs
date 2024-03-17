using AutoMapper;
using Common.Api;
using Common.BL;
using Common.Domain;
using Common.Repository;
using System.Collections.Generic;
using ToDoDomain;
using UserServices.dto;
using UserServices.Utils;


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

        public async Task<UserGetDto> AddAsync(UserDto item, CancellationToken token = default)
        {
            if(await _userRepository.SingleOrDefaultAsync(u => u.Login == item.Login.Trim()) is not null)
            {
                throw new BadRequestExeption("This login is alreadi taken");
            }
            var entity = new User()
            {
                Login = item.Login,
                PasswordHash = PasswordHashUtil.Hash(item.Password),
            };
            
            var addedItem = await _userRepository.AddAsync(entity, token);
            if(addedItem is null)
            {
                throw new BadRequestExeption("Can not add user");
            }
            return _mapper.Map<UserGetDto>(addedItem);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken token = default)
        {
            var item = await GetByIdOrDefaultAsync(id);
            if(item == null)
            {
                throw new NotFoundExeption(new {Id = id});
            }
            return await _userRepository.DeleteAsync(_mapper.Map<User>(item), token);
        }

        public async Task<UserGetDto> GetByIdOrDefaultAsync(int id, CancellationToken token = default)
        {
            var item = await _userRepository.SingleOrDefaultAsync(x => x.Id == id);
            if (item is null)
            {
                throw new NotFoundExeption(new { Id = id });
            }
            return _mapper.Map<UserGetDto>(item);
        }

        public async Task<IReadOnlyCollection<UserGetDto>> GetListAsync(int? offset, string? name, int? limit, CancellationToken token = default)
        {
            return _mapper.Map<IReadOnlyCollection<UserGetDto>>(await _userRepository.GetListAsync(
                offset,
                limit,
                name == null ? null : u => u.Login.Contains(name),
                token: token));
        }

        public async Task<UserGetDto> UpdateAsync(UserDto newItem, CancellationToken token = default)
        {
            var user = _mapper.Map<UserDto, User>(newItem);
            user.PasswordHash = "qwe323r43t";
            var item = await _userRepository.UpdateAsync(user, token);
            if(item is null)
            {
                throw new BadRequestExeption("Can not update user");
            }
            return _mapper.Map<UserGetDto>(item);
        }

    }
}

using Common.Domain;
using UserServices.dto;

namespace UserServices
{
    public interface IUserService
    {
        public IEnumerable<User> GetList(int? offset, string? name, int? limit);
        public User? GetById(int id);
        public bool Delete(int id);
        public User? Add(UserDto item);
        public User? Update(User newItem);
    }
}

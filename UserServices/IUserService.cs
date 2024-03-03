using Common.Domain;

namespace UserServices
{
    public interface IUserService
    {
        public IEnumerable<User> GetList(int? offset, string? name, int? limit);
        public User? GetById(int id);
        public bool Delete(int id);
        public User? Add(User item);
        public User? Put(int id, User newItem);
    }
}

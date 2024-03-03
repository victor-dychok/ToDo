using Common.Domain;

namespace Common.Repository
{

    public interface IUserRepository
    {
        public IEnumerable<User> GetList(int? offset, string? name, int? limit);
        public User? Add(User user);
        public User? Put(int id, User user);
        public bool Delete(int id);
        public User? GetById(int id);

    }
}
using Common.Interfaces;

namespace Common.Domain
{
    public class User : IHasId
    {
        public int Id { get; set; } 
        public string Login { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;

        public User() { }
        public User(int id, string name)
        {
            Id = id;
            Login = name;
        }
    }
}

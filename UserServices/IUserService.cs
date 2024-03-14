using Common.Domain;
using UserServices.dto;

namespace UserServices
{
    public interface IUserService
    {
        public Task<IReadOnlyCollection<User>> GetListAsync(int? offset, string? name, int? limit, CancellationToken token = default);
        public Task<User?> GetByIdOrDefaultAsync(int id, CancellationToken token = default);
        public Task<User?> AddAsync(UserDto item, CancellationToken token = default);
        public Task<User?> UpdateAsync(UserDto newItem, CancellationToken token = default);
        public Task<bool> DeleteAsync(int id, CancellationToken token = default);
    }
}

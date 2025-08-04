namespace MusiciansBlog.API.Infrastructure.Users.Common
{
    public interface IUsersRepository
    {
        Task AddAsync(UserModel model, CancellationToken cancellationToken = default);

        Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<UserModel> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(Guid userId, Action<UserEntity> updateUser, CancellationToken cancellationToken = default);

        Task<bool> CheckUserExistence(string userName, string email, CancellationToken cancellationToken = default);
    }
}

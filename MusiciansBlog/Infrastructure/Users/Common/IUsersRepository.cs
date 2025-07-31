namespace MusiciansBlog.API.Infrastructure.Users.Common
{
    public interface IUsersRepository
    {
        Task AddAsync(UserModel model, CancellationToken cancellationToken = default);

        Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}

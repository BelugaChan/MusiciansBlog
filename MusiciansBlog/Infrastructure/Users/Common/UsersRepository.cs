using Mappify;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Authentication.Providers;
using MusiciansBlog.API.Exceptions;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace MusiciansBlog.API.Infrastructure.Users.Common
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMappify _mapper;
        private readonly IRedisProvider _provider;
        private readonly MyDbContext _dbContext;

        public UsersRepository(
            IMappify mapper, 
            IRedisProvider provider,
            MyDbContext dbContext)
        {
            _mapper = mapper;
            _provider = provider;
            _dbContext = dbContext;
        }

        public async Task AddAsync(UserModel model, CancellationToken cancellationToken = default)
        {
            var existingEntity = await _dbContext.Users.FindAsync(model.UserId, cancellationToken);

            if (existingEntity is not null)
            {
                throw new EntityAlreadyExistsException();
            }

            var entity = _mapper.Map<UserEntity>(model);

            _dbContext.Users.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserModel?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {

            var res = await _provider.GetStringByKeyAsync<UserModel>(email);

            if (res is not null)
            {
                return res;
            }

            //cache miss, read from DB
            var existingEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            
            //no data in DB..
            if(existingEntity is null)
            {
                return null;
            }

            var model = _mapper.Map<UserModel>(existingEntity);

            //set data in cache
            await _provider.SetStringAsync(email, model);
            //await _redisDatabase.StringSetAsync(cacheKey, JsonSerializer.Serialize(model), TimeSpan.FromMinutes(_options.MinutesToLive));

            return model;
        }

        public async Task<bool> UpdateAsync(Guid userId, Action<UserEntity> updateUser, CancellationToken cancellationToken)
        {
            var existingUser = await _dbContext.Users.FindAsync(userId, cancellationToken);

            if (existingUser is null)
            {
                return false;
                throw new EntityNotFoundException();
            }

            //update db entity
            updateUser(existingUser);
            await _dbContext.SaveChangesAsync(cancellationToken);

            //invalidate outdated item data in the cache 
            await _provider.SetStringAsync(existingUser.Email, existingUser);

            return true;
        }

        public async Task<bool> CheckUserExistence(string userName, string email, CancellationToken cancellationToken)
        {
            var res = await _dbContext
                .Users
                .AnyAsync(i => i.Username == userName
                                          || i.Email == email, cancellationToken);
            return res;
        }
    }
}

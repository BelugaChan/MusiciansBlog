
using Mappify;
using Microsoft.EntityFrameworkCore;
using MusiciansBlog.API.Exceptions;

namespace MusiciansBlog.API.Infrastructure.Users.Common
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMappify _mapper;
        private readonly MyDbContext _dbContext;
        public UsersRepository(IMappify mapper, MyDbContext dbContext)
        {
            _mapper = mapper;
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

        public async Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var existingEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            
            if(existingEntity is null)
            {
                return null;
            }

            var model = _mapper.Map<UserModel>(existingEntity);

            return model;
        }
    }
}

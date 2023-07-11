using Common.Utilities;
using Entities;
using Entities.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : Repository<User> ,IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<User> GetByUserAndPass(string mobile, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(p => p.Mobile == mobile && p.Password == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.Mobile == user.Mobile);
            //if (exists)
                //throw new BadRequestException("نام کاربری تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.Password = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }
    }
}

﻿using Common.Exceptions;
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
            return Table.Where(p => p.Mobile == mobile && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.Mobile == user.Mobile);
            if (exists)
                throw new BadRequestException("نام کاربری تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }

        public Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken)
        {
            user.SecurityStamp = Guid.NewGuid();
            return UpdateAsync(user, cancellationToken);
        }

        //public override void Update(User entity, bool saveNow = true)
        //{
        //    entity.SecurityStamp = Guid.NewGuid();
        //    base.Update(entity, saveNow);
        //}

        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }

        public Task<User> GetByUserMobile(string mobile, CancellationToken cancellationToken)
        {
            return Table.Where(p => p.Mobile == mobile).SingleOrDefaultAsync(cancellationToken);
        }
    }
}

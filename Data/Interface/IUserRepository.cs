﻿using System.Threading;
using System.Threading.Tasks;
using Data.Interface;
using Entities;
using Entities.Models.User;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);

        Task AddAsync(User user, string password, CancellationToken cancellationToken);
    }
}
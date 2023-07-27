using System.Threading;
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

        Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken);

        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);

        Task<User> GetByUserMobile(string mobile, CancellationToken cancellationToken);
    }
}
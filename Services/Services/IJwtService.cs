using Entities;
using Entities.Models.User;

namespace Services
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}
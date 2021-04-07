using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PirlantaApi.Entities;

namespace PirlantaApi.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUser(string id);
        Task PutUser(User user);
        Task PostUser(User user);
        Task DeleteUser(User user);
        Task<bool> UserExists(string id);
    }
}

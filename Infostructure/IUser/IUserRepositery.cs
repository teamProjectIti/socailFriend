using Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.IUser
{
    public interface IUserRepositery
    {
        Task<bool> GetFirst(string username);
        Task<AppUser> GetObjectAppUser(string username);
    }
}

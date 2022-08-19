using Data.DBContext;
using Data.Models.Users;
using Infostructure.IUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositery.Userrepositery
{
    public class UserRepositry : IUserRepositery
    {
        private readonly DataContext context;

        public UserRepositry(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> GetFirst(string userName)
        {
            return await context.AppUsers.AnyAsync(x => x.UserName == userName.ToLower());
        }
        public async Task<AppUser> GetObjectAppUser(string username)
        {
            return await context.AppUsers.SingleOrDefaultAsync(x => x.UserName == username);
        }


    }
}
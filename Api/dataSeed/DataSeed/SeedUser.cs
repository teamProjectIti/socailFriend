using Data.DBContext;
using Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
 using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.dataSeed.DataSeed
{
    public class SeedUser
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.AppUsers.AnyAsync()) return;
            var userData = await System.IO.File.ReadAllTextAsync("C:/MySelfEcommerce/socailFriend/Api/dataSeed/DataSeed/userSeedDataa.json");
       
    var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var item in users)
            {
                using var hmac = new HMACSHA512();
                item.UserName = item.UserName.ToLower();
                item.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd)"));
                item.passwordSalt = hmac.Key;
                  context.AppUsers.Add(item);
            }
            await context.SaveChangesAsync();
        }
    }
}

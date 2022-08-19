using Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.IUser.IServicesToken
{
    public interface ITokenServices
    {
        string CreateToken(AppUser token);
    }
}

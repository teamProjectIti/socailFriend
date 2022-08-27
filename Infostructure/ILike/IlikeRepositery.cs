using Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.ILike
{
    public interface IlikeRepositery
    {
        Task<UserLike> GetUserLike(int SourceUserId, int LikedUserId);
        Task<AppUser> GetUserWithLikes(int UserId);
        Task<LikedDTO> GetUsersLikes(string predicate, int IUserId);
    }
}

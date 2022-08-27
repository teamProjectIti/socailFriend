using Data.DBContext;
using Data.Models.Users;
using Infostructure.ILike;
using ModeDTO.LikedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositery.likesRepositery
{
    public class likeRepositery //: IlikeRepositery
    {
        private readonly DataContext dataContext;

        public likeRepositery(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<UserLike> GetUserLike(int SourceUserId, int LikedUserId)
        {
            return await dataContext.Likes.FindAsync(SourceUserId, LikedUserId);
        }
        //public async Task<LikedDTO> GetUsersLikes(string predicate, int IUserId)
        //{
        //    return await dataContext.Likes.FindAsync(SourceUserId, LikedUserId);
        //}

        public Task<AppUser> GetUserWithLikes(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}

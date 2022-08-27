using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Users
{
    public class UserLike
    {
        [Key]
        public int Id { get; set; }

        public AppUser SourceUser { get; set; }
        public int SourceUserID { get; set; }
        public AppUser LikedUser { get; set; }
        public int LikedUserID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Users
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string  Url{ get; set; }
        public bool IsMain{ get; set; }
        public string publicID{ get; set; }
        public virtual AppUser appUser { get; set; }
        public int AppUserId { get; set; }

    }
}

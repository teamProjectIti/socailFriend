using Data.Models;
using Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DBContext
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<test> tests { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace musicwithfriends.Models
{
    public class musicwithfriendsContext : DbContext
    {
        public musicwithfriendsContext (DbContextOptions<musicwithfriendsContext> options)
            : base(options)
        {
        }

        public DbSet<Chatroom> Chatroom { get; set; }
    }
}

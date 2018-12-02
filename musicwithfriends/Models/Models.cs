using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace musicwithfriends.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class musicwithfriendsContext : DbContext
    {
        public musicwithfriendsContext(DbContextOptions<musicwithfriendsContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}

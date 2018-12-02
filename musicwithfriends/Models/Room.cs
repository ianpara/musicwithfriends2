
namespace musicwithfriends.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Room
    {
        public int RoomId { get; set; }

        public string RoomName { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}

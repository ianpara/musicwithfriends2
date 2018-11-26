namespace musicwithfriends.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Chatroom
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public IEnumerable<Song> Songs { get; set; }
    }
}

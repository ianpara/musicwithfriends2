namespace musicwithfriends.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Song
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int ChatRoomID { get; set; }
    }
}

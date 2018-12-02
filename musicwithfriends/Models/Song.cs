namespace musicwithfriends.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class Song
    {
        public int SongId { get; set; }

        [NotMapped]
        public IFormFile SongBytes { get; set; }

        public byte[] SongData { get; set; }

        public string SongName { get; set; }

        public string FileLocation { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}

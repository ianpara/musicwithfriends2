using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicwithfriends.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class FileUpload
    {
        [Required]
        [Display(Name = "Title")]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Song")]
        public IFormFile UploadSong { get; set; }
    }
}

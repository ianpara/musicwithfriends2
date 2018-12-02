
namespace musicwithfriends.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class FileUpload
    {
        /*[Display(Name = "Title")]
        //[StringLength(60, MinimumLength = 3)]
        //public string Title { get; set; }*/

        [Required]
        [Display(Name = "Song")]
        public IFormFile UploadSong { get; set; }

        public IFormFile AnotherInput { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using musicwithfriends.Models;

namespace musicwithfriends.Pages.Room
{
    using System.IO;
    using System.Net;

    using musicwithfriends.Utilities;

    using Microsoft.AspNetCore.Http;

    using Room = musicwithfriends.Models.Room;

    public class DetailsModel : PageModel
    {
        private readonly musicwithfriends.Models.musicwithfriendsContext _context;

        public DetailsModel(musicwithfriends.Models.musicwithfriendsContext context)
        {
            _context = context;
        }

        public Room Room { get; set; }

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        [BindProperty]
        public Song Song { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms.FirstOrDefaultAsync(m => m.RoomId == id);

            if (Room == null)
            {
                return NotFound();
            }

            return Page();
        }

        //public ActionResult PlayAudio(int id)
        //{
        //    MemoryStream ms = null;
        //    byte[] bytes = _context.Songs.FirstOrDefault(s => s.SongId == id).SongData;

        //        ms = new MemoryStream(bytes);
            

        //    return File(ms, "audio/mpeg");//if it's mp3
        //}

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var data = FileUpload.UploadSong;
            byte[] songData = new byte[data.Length];
            using (var memoryStream = new MemoryStream())
            {
                await data.CopyToAsync(memoryStream);
                songData = memoryStream.ToArray();
            }

            //var publicScheduleData = await FileHelpers.ProcessFormFile(FileUpload.UploadSong, ModelState);
            
            var schedule = new Song()
                               {
                                   Title = FileUpload.Title,
                                   SongData = songData,
                                   RoomId = id
            };

            _context.Songs.Add(schedule);
            await _context.SaveChangesAsync();
            
            return RedirectToPage("/Songs/Index");
        }
    }
}

namespace musicwithfriends.Pages.Room
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using musicwithfriends.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Room = Models.Room;

    public class DetailsModel : PageModel
    {
        private readonly musicwithfriendsContext _context;
        private IHostingEnvironment _env;
        public IList<Song> Songs { get; set; }
        public Room Room { get; set; }
        [BindProperty]
        public FileUpload FileUpload { get; set; }
        public Song Song { get; set; }
        public SelectList RoomSongs { get; set; }

        private IHostingEnvironment hostingEnvironment;
        private string appRootFolder;

        public DetailsModel(musicwithfriendsContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms.Include(s => s.Songs).FirstOrDefaultAsync(m => m.RoomId == id);

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

        [HttpGet("{id}")]
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var data = FileUpload.UploadSong;
            if (data == null)
            {
                return Page();
            }

            var fileName = WebUtility.HtmlEncode(Path.GetFileName(data.FileName));
            string roomPath = $"{_env.WebRootPath}\\Media\\{id}";
            if (!Directory.Exists(roomPath))
            {
                Directory.CreateDirectory(roomPath);
            }

            string filePath = Path.Combine(roomPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await data.CopyToAsync(fileStream);
            }

            //media/roomid/songname
            Song = new Song()
            {
                SongName = data.FileName,
                RoomId = id,
                FileLocation = $"\\Media\\{id}\\{fileName}"
            };

            _context.Songs.Add(Song);
            await _context.SaveChangesAsync();

            // redirect back to the index action to show the form once again
            return RedirectToAction("Details");
        }
    }
}

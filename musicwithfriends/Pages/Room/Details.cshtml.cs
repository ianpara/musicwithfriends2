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
        public string RoomName { get; set; }
        public int RoomId { get; set; }
        private IHostingEnvironment hostingEnvironment;
        private string appRootFolder;

        public DetailsModel(musicwithfriendsContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> OnGetAsync(string roomName)
        {
            Room = await _context.Rooms.Include(s => s.Songs).FirstOrDefaultAsync(room => room.RoomName == roomName);
            Songs = Room.Songs.ToList();

            if (Room == null)
            {
                return NotFound();
            }

            RoomName = Room.RoomName;
            RoomId = Room.RoomId;

            return Page();
        }

        [HttpGet("{RoomName}")]
        public async Task<IActionResult> OnPostAsync(string RoomName)
        {
            Room = await _context.Rooms.Include(s => s.Songs).FirstOrDefaultAsync(room => room.RoomName == RoomName);
            var data = FileUpload.UploadSong;
            if (data == null)
            {
                return NotFound();
            }

            var fileName = WebUtility.HtmlEncode(Path.GetFileName(data.FileName));
            string roomPath = $"{_env.WebRootPath}/Media/{Room.RoomId}";
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
                RoomId = Room.RoomId,
                FileLocation = $"/Media/{Room.RoomId}/{fileName}"
            };

            _context.Songs.Add(Song);
            await _context.SaveChangesAsync();

            // redirect back to the index action to show the form once again
            //return Page();
            Room = await _context.Rooms.Include(s => s.Songs).FirstOrDefaultAsync(m => m.RoomName == RoomName);
            Songs = Room.Songs.ToList();

            return Page();
        }

        [HttpGet]
        public IActionResult OnGetSongsList(string RoomName)
        {
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            List<Song> songs = _context.Songs.Where(s => s.Room.RoomName == RoomName).ToList();

            return new JsonResult(songs);
        }
    }
}

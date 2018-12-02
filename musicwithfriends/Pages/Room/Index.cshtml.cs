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
    using Room = musicwithfriends.Models.Room;

    public class IndexModel : PageModel
    {
        private readonly musicwithfriends.Models.musicwithfriendsContext _context;

        public IndexModel(musicwithfriends.Models.musicwithfriendsContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; }

        public async Task OnGetAsync()
        {
            Room = await _context.Rooms.ToListAsync();
        }
    }
}

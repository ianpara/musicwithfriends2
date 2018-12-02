
namespace musicwithfriends.Pages.Songs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using musicwithfriends.Models;
    public class IndexModel : PageModel
    {
        private readonly musicwithfriends.Models.musicwithfriendsContext _context;

        public IndexModel(musicwithfriends.Models.musicwithfriendsContext context)
        {
            _context = context;
        }

        public IList<Song> Song { get;set; }

        public async Task OnGetAsync()
        {
            Song = await _context.Songs
                .Include(s => s.Room).ToListAsync();
        }
    }
}

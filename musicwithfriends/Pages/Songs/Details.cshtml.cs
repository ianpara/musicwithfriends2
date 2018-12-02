using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using musicwithfriends.Models;

namespace musicwithfriends.Pages.Songs
{
    public class DetailsModel : PageModel
    {
        private readonly musicwithfriends.Models.musicwithfriendsContext _context;

        public DetailsModel(musicwithfriends.Models.musicwithfriendsContext context)
        {
            _context = context;
        }

        public Song Song { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Song = await _context.Songs
                .Include(s => s.Room).FirstOrDefaultAsync(m => m.SongId == id);

            if (Song == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using musicwithfriends.Models;

namespace musicwithfriends
{
    public class DeleteModel : PageModel
    {
        private readonly musicwithfriends.Models.musicwithfriendsContext _context;

        public DeleteModel(musicwithfriends.Models.musicwithfriendsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Chatroom Chatroom { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chatroom = await _context.Chatroom.FirstOrDefaultAsync(m => m.ID == id);

            if (Chatroom == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chatroom = await _context.Chatroom.FindAsync(id);

            if (Chatroom != null)
            {
                _context.Chatroom.Remove(Chatroom);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

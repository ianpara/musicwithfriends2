using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicwithfriends.Models;

namespace musicwithfriends
{
    public class EditModel : PageModel
    {
        private readonly musicwithfriends.Models.musicwithfriendsContext _context;

        public EditModel(musicwithfriends.Models.musicwithfriendsContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Chatroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatroomExists(Chatroom.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ChatroomExists(int id)
        {
            return _context.Chatroom.Any(e => e.ID == id);
        }
    }
}

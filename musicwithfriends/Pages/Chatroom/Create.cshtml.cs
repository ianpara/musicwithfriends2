using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using musicwithfriends.Models;

namespace musicwithfriends
{
    public class CreateModel : PageModel
    {
        private readonly musicwithfriends.Models.musicwithfriendsContext _context;

        public CreateModel(musicwithfriends.Models.musicwithfriendsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Chatroom Chatroom { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Chatroom.Add(Chatroom);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
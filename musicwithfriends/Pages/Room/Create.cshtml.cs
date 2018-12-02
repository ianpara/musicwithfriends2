using System;
using System.Collections.Generic;
namespace musicwithfriends.Pages.Room
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using musicwithfriends.Models;

    using Room = musicwithfriends.Models.Room;

    public class CreateModel : PageModel
    {
        private readonly musicwithfriendsContext _context;

        public CreateModel(musicwithfriendsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Room Room { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public string roomName
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Room.RoomName = roomName;

            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
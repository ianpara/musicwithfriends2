namespace musicwithfriends
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
        private readonly musicwithfriendsContext _context;

        public IndexModel(musicwithfriends.Models.musicwithfriendsContext context)
        {
            _context = context;
        }

        public IList<Chatroom> Chatroom { get;set; }

        public async Task OnGetAsync()
        {
            Chatroom = await _context.Chatroom.ToListAsync();
        }
    }
}

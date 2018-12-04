using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using musicwithfriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace musicwithfriends.Pages.Room
{
    public class GetSongsModel : PageModel
    {
        private readonly musicwithfriendsContext _context;

        public GetSongsModel(musicwithfriendsContext context)
        {
            _context = context;
        }

        public JsonResult OnGet(int RoomId)
        {
            List<Song> songs = _context.Songs.Where(s => s.RoomId == RoomId).ToList();

            return new JsonResult(songs);
        }
    }
}
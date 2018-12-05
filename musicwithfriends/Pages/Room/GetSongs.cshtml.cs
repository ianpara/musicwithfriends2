using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using musicwithfriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Newtonsoft.Json;

namespace musicwithfriends.Pages.Room
{
    public class GetSongsModel : PageModel
    {
        private readonly musicwithfriendsContext _context;

        public GetSongsModel(musicwithfriendsContext context)
        {
            _context = context;
        }
    }
}
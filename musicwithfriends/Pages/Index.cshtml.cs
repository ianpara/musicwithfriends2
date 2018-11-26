using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace musicwithfriends.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; private set; } = "PageModel in C#";

        public void OnGet()
        {
            Message += $" Server time is { DateTime.Now }";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Perform an initial check to catch FileUpload class attribute violations.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var filePath = "<PATH-AND-FILE-NAME>";

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
               // await FileUpload.UploadSong.CopyToAsync(fileStream);
            }

            return RedirectToPage("./Index");
        }
    }
}

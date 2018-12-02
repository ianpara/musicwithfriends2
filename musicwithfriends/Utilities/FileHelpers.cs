using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicwithfriends.Utilities
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Text;

    using musicwithfriends.Models;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class FileHelpers
    {
        public static async Task<string> ProcessFormFile(IFormFile formFile, ModelStateDictionary modelState)
        {
            var fieldDisplayName = string.Empty;

            MemberInfo property = typeof(Song).GetProperty(formFile.Name.Substring(formFile.Name.IndexOf(".") + 1));

            if (property != null)
            {
                var displayAttribute =
                    property.GetCustomAttribute(typeof(DisplayAttribute))
                        as DisplayAttribute;

                if (displayAttribute != null)
                {
                    fieldDisplayName = $"{displayAttribute.Name} ";
                }
            }
            
            var fileName = WebUtility.HtmlEncode(
                Path.GetFileName(formFile.FileName));

            // Check the file length and don't bother attempting to
            // read it if the file contains no content. This check
            // doesn't catch files that only have a BOM as their
            // content, so a content length check is made later after 
            // reading the file's content to catch a file that only
            // contains a BOM.
            if (formFile.Length == 0)
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fieldDisplayName}file ({fileName}) is empty.");
            }
            else if (formFile.Length > 1048576)
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fieldDisplayName}file ({fileName}) exceeds 1 MB.");
            }
            else
            {
                try
                {
                    string fileContents;

                    // The StreamReader is created to read files that are UTF-8 encoded. 
                    // If uploads require some other encoding, provide the encoding in the 
                    // using statement. To change to 32-bit encoding, change 
                    // new UTF8Encoding(...) to new UTF32Encoding().
                    using (var reader = new StreamReader(
                        formFile.OpenReadStream(),
                        new UTF8Encoding(false, true),
                        true))
                    {
                        fileContents = await reader.ReadToEndAsync();

                        // Check the content length in case the file's only
                        // content was a BOM and the content is actually
                        // empty after removing the BOM.
                        if (fileContents.Length > 0)
                        {
                            return fileContents;
                        }
                        else
                        {
                            modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) is empty.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) upload failed. " + $"Please contact the Help Desk for support. Error: {ex.Message}");
                    // Log the exception
                }
            }

            return string.Empty;
        }

        private readonly IHostingEnvironment _env;

        public FileHelpers(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadAsync(string path, IFormFile content, string nameWithoutExtension = null)
        {
            if (content != null && content.Length > 0)
            {
                string extension = Path.GetExtension(content.FileName);

                // Never trust user's provided file name
                string fileName = $"{ nameWithoutExtension ?? Guid.NewGuid().ToString() }{ extension }";

                // Combine the path with web root and my folder of choice, 
                // "uploads" 
                path = Path.Combine(_env.WebRootPath, "uploads", path).ToLower();

                // If the path doesn't exist, create it.
                // In your case, you might not need it if you're going 
                // to make sure your `keys.json` file is always there.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Combine the path with the file name
                string fullFileLocation = Path.Combine(path, fileName).ToLower();

                // If your case, you might just need to open your 
                // `keys.json` and append text on it.
                // Note that there is FileMode.Append too you might want to
                // take a look.
                using (var fileStream = new FileStream(fullFileLocation, FileMode.Create))
                {
                    await content.CopyToAsync(fileStream);
                }

                // I only want to get its relative path
                return fullFileLocation.Replace(_env.WebRootPath, string.Empty, StringComparison.OrdinalIgnoreCase);
            }

            return string.Empty;
        }
    }
}

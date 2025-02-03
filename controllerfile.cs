using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace YourProject.Controllers
{
    public class DownloadController : Controller
    {
        // The directory where the files are stored
        private readonly string _fileStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");

        // This method will handle the download request
        [HttpGet]
        public IActionResult GetFile(int id)
        {
            // You can replace this with a database or any other source to retrieve the file info
            var fileDetails = GetFileDetails(id);
            
            if (fileDetails == null)
            {
                return Json(new { success = false, message = "File not found!" });
            }

            // Return the file URL and file name as JSON
            return Json(new { success = true, fileUrl = $"/files/{fileDetails.FileName}", fileName = fileDetails.FileName });
        }

        // Sample method to get file details by ID (Replace with actual database or logic)
        private FileDetails GetFileDetails(int id)
        {
            // Example files list
            var files = new[]
            {
                new FileDetails { Id = 1, FileName = "example1.pdf" },
                new FileDetails { Id = 2, FileName = "example2.jpg" },
                new FileDetails { Id = 3, FileName = "example3.zip" }
            };

            return files.FirstOrDefault(f => f.Id == id);
        }

        // This method handles the actual file download request
        [HttpGet]
        [Route("download/{fileName}")]
        public IActionResult Download(string fileName)
        {
            // Combine the file path with the storage path
            var filePath = Path.Combine(_fileStoragePath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Return the file for download
            return PhysicalFile(filePath, "application/octet-stream", fileName);
        }
    }

    // Simple model to hold file details
    public class FileDetails
    {
        public int Id { get; set; }
        public string FileName { get; set; }
    }
}

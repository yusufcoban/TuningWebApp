using Microsoft.AspNetCore.Authorization; // Add this namespace
using Microsoft.AspNetCore.Mvc;
using BaseBackend.Models; // Import your model namespace
using System.Collections.Generic;
using System;

namespace BaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Ensure only authenticated users can access this controller
    public class MyFilesController : ControllerBase
    {
        // GET: api/myfiles
        [HttpGet]
        public IActionResult GetUploadedFiles()
        {
            // Generate a fake list of uploaded files
            var uploadedFiles = GenerateFakeUploadedFiles(20);
            return Ok(uploadedFiles); // Return the list as JSON
        }

        private List<UploadedFile> GenerateFakeUploadedFiles(int count)
        {
            var files = new List<UploadedFile>();
            var random = new Random();

            for (int i = 1; i <= count; i++)
            {
                files.Add(new UploadedFile
                {
                    RequestID = i,
                    CarModelId = random.Next(1, 100), // Random CarModelId
                    UploadDate = DateTime.Now.AddDays(-random.Next(1, 30)), // Random upload date within the last 30 days
                    LastModifiedDate = DateTime.Now.AddDays(-random.Next(0, 30)), // Random last modified date
                    State = random.Next(1, 5), // Random state between 1 and 4
                    Title = $"Car Model {random.Next(1, 100)} - Year {random.Next(2000, 2024)}" // Random title
                });
            }

            return files;
        }
    }
}

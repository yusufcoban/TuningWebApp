using BaseBackend.Models; // Import your model namespace

using Microsoft.AspNetCore.Authorization; // Add this namespace
using Microsoft.AspNetCore.Mvc;

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

        private readonly string _targetFilePath;

        public MyFilesController()
        {
            // Set the path where you want to save uploaded files
            _targetFilePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            // Ensure the directory exists
            if (!Directory.Exists(_targetFilePath))
            {
                Directory.CreateDirectory(_targetFilePath);
            }
        }

        // POST api/fileupload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string[] solutions, [FromForm] string solutionid, [FromForm] string[] dtcList)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var username = User.Identity.Name; // This gets the username
            UserInformation currentUser = new UserInformation(username);

            // Handle the uploaded file
            // Get the file extension
            var fileExtension = Path.GetExtension(file.FileName);

            // Get the base file name without the extension
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

            // Create the timestamp
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

            // Combine the base file name and timestamp
            var fileName = $"{fileNameWithoutExtension}_{timestamp}{fileExtension}";

            // Create the full file path (including the username as a subfolder under UploadedFiles)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", username, fileName);

            // Ensure the directory for the user exists (create it if necessary)
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", username)); using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Handle the solutions array
            foreach (var solution in solutions)
            {
                Console.WriteLine($"Solution: {solution}");
            }

            return Ok(new { Message = "File and solutions uploaded successfully." });
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

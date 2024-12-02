using BaseBackend.Models;

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
            var username = User.Identity.Name; // This gets the username
            UserInformation currentUser = new UserInformation(username);
            var uploadedFiles = _fileHandler.GetAllUploadedFilesAsyncByUserName(username);
            return Ok(uploadedFiles); // Return the list as JSON
        }

        [HttpGet("GetOpenTasksForAutomation")]
        public async Task<IActionResult> GetOpenTasks()
        {
            var result = await _fileHandler.GetOpenTasksAsyncInNullState();
            return Ok(result);
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _fileHandler.GetAllOpenTasksAsync();
            return Ok(result);// Return the list as JSON
        }

        [HttpGet("GetUploadedFilesByID")]
        public IActionResult GetUploadedFilesByID(int id)
        {
            // Generate a fake list of uploaded files
            var username = User.Identity.Name; // This gets the username
            UserInformation currentUser = new UserInformation(username);
            var uploadedFiles = _fileHandler.GetAllUploadedFilesAsyncByUserName(username);
            bool isValidRequest = uploadedFiles.Any() && uploadedFiles.Where(x => x.Id == id).Any();
            if (isValidRequest)
            {
                return Ok(uploadedFiles.Where(x => x.Id == id)); // Return the list as JSON
            }
            else
            {
                return BadRequest();
            }

        }

        private readonly string _targetFilePath;

        private readonly MyUploadedFileHandler _fileHandler;
        private readonly TaskHandler _taskHandler;


        public MyFilesController(MyUploadedFileHandler fileHandler, TaskHandler taskHandler)
        {
            _fileHandler = fileHandler;
            // Set the path where you want to save uploaded files
            _targetFilePath = _fileHandler.getDownloadPathBase();

            // Ensure the directory exists
            if (!Directory.Exists(_targetFilePath))
            {
                Directory.CreateDirectory(_targetFilePath);
            }

            _taskHandler = taskHandler;
        }


        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {

            // Create the full file path (including the username as a subfolder under UploadedFiles)
            var filePath = fetchCurrentPathName(fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Return 404 if the file does not exist
            }

            // Return the file as a downloadable file
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpGet("downloadAdmin/{fileName}")]
        public async Task<IActionResult> DownloadFileAdmin(string fileName, [FromQuery] string userName)
        {

            // Create the full file path (including the username as a subfolder under UploadedFiles)
            var filePath = fetchCurrentPathName(fileName, userName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Return 404 if the file does not exist
            }

            // Return the file as a downloadable file
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }


        // POST api/fileupload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string[] solutions, [FromForm] string solutionid, [FromForm] string[] dtcList, [FromForm] string additionalInfo)
        {

            var username = User.Identity.Name; // This gets the username
            UserInformation currentUser = new UserInformation(username);



            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

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
            var filePath = Path.Combine(_fileHandler.getDownloadPathBaseWithUserName(username), fileName);

            // Ensure the directory for the user exists (create it if necessary)
            Directory.CreateDirectory(_fileHandler.getDownloadPathBaseWithUserName(username)); using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            int index = solutionid.IndexOf("_", solutionid.IndexOf("_") + 1); // Find the second underscore
            string result = solutionid.Substring(0, index);

            MyUploadedFile uploadedFile = new MyUploadedFile()
            {
                FileName = fileName,
                UploadDate = DateTime.UtcNow,
                DTCList = string.Join(", ", dtcList),
                Information = "",
                Username = username,
                SelectedVariants = string.Join(", ", solutions),
                CarmodelId = result,
                TuningVariantId = solutionid,
                additionalInfo = additionalInfo,
                State = 0,
                ModifyDate = DateTime.UtcNow,
                Title = "test"
            };

            _fileHandler.AddUploadedFileAsync(uploadedFile);

            // Handle the solutions array
            foreach (var solution in solutions)
            {
                Console.WriteLine($"Solution: {solution}");
            }

            //firing up automation
            System.Threading.Tasks.Task.Run(async () =>
            {
                // Wait for 15 seconds
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(15));

                // Start the task after the delay
                _taskHandler.CheckForOpenTasksAndHandle();
            });

            return Ok(new { Message = "File and solutions uploaded successfully." });
        }


        [HttpPost("UploadTuningFile")]
        public async Task<IActionResult> UploadTuningFile(IFormFile file, [FromForm] int filesolutionId, [FromForm] string additionalInfo)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            MyUploadedFile originMyUploadFile = await _fileHandler.GetUploadedFileByIdAsync(filesolutionId);
            // Create the full file path (including the username as a subfolder under UploadedFiles)
            var filePath = Path.Combine(_fileHandler.getDownloadPathBaseWithUserName(originMyUploadFile.Username), file.FileName);

            // Ensure the directory for the user exists (create it if necessary)
            Directory.CreateDirectory(_fileHandler.getDownloadPathBaseWithUserName(originMyUploadFile.Username)); using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _taskHandler.UpdateFileName(filesolutionId, file.FileName);
            _taskHandler.UpdateStateMyUploadedFile(filesolutionId, 5, additionalInfo);

            return Ok(new { Message = "Solutions uploaded successfully." });
        }

        private string fetchCurrentPathName(string filename, string userName = "")
        {
            var username = userName;
            if (string.IsNullOrEmpty(userName))
            {
                username = User.Identity.Name; // This gets the username
                UserInformation currentUser = new UserInformation(username);
            }
            else
            {
                //Todo:Check for admin
            }

            // Create the full file path (including the username as a subfolder under UploadedFiles)
            var filePath = Path.Combine(_fileHandler.getDownloadPathBaseWithUserName(username), filename);

            return filePath;

        }

    }
}

using BaseBackend.Models; // Import your model namespace

using Microsoft.AspNetCore.Authorization; // Add this namespace
using Microsoft.AspNetCore.Mvc;

using YourNamespace.Controllers;

namespace BaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Ensure only authenticated users can access this controller
    public class BaseController : ControllerBase
    {
        private readonly EcuHandler _ecuHandler;
        private readonly TaskHandler _taskHandler;

        public BaseController(EcuHandler ecuHandler, TaskHandler taskHandler)
        {
            this._ecuHandler = ecuHandler;
            this._taskHandler = taskHandler;
        }

        // GET: apimyfiles
        [HttpGet("GetEcuList")]
        public IActionResult GetUploadedFiles()
        {
            return Ok(_ecuHandler.GetAllEcus()); // Return the list as JSON
        }

        [HttpGet("FireAutomation")]
        public IActionResult FireAutomation()
        {
            _taskHandler.CheckForOpenTasksAndHandle();
            return Ok(); // Return the list as JSON
        }


    }
}

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

        public BaseController(EcuHandler ecuHandler)
        {
            this._ecuHandler = ecuHandler;
        }

        // GET: apimyfiles
        [HttpGet("GetEcuList")]
        public IActionResult GetUploadedFiles()
        {
            return Ok(_ecuHandler.GetAllEcus()); // Return the list as JSON
        }

    }
}

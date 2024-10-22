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
        private readonly StringReplacementHandler _stringReplacementHandler;


        public BaseController(EcuHandler ecuHandler, StringReplacementHandler stringReplacementHandler)
        {
            this._ecuHandler = ecuHandler;
            this._stringReplacementHandler = stringReplacementHandler;
        }

        // GET: apimyfiles
        [HttpGet("GetEcuList")]
        public IActionResult GetUploadedFiles()
        {
            return Ok(_ecuHandler.GetAllEcus()); // Return the list as JSON
        }


        [HttpGet("TestReplaceMentFunctions")]
        public IActionResult TestFucntion1()
        {
           var testPath= Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles\\user\\bmwx6HW07616431SW07628500.org");
            _stringReplacementHandler.ReplaceStringsInFile(testPath, ["egr"], "5_1_4");

            return Ok(_ecuHandler.GetAllEcus()); // Return the list as JSON
        }


    }
}

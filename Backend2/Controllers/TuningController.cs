using BaseBackend.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Ensure only authenticated users can access this controller
    public class TuningController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TuningDatabaseHandler _tuningDatabaseHandler;


        // Constructor injection for both IConfiguration and TuningDatabaseHandler
        public TuningController(IConfiguration configuration, TuningDatabaseHandler tuningDatabaseHandler)
        {
            _configuration = configuration;
            _tuningDatabaseHandler = tuningDatabaseHandler ?? throw new ArgumentNullException(nameof(tuningDatabaseHandler));
        }


        // 1st API: Get list of all car brands
        [HttpGet("carbrands")]
        public ActionResult<List<CarBrand>> GetCarBrands()
        {
            return Ok(_tuningDatabaseHandler.getCarBrands());
        }

        // 2nd API: Get list of tuning database info by given id
        [HttpGet("tuningdatabase/{id}")]
        public ActionResult<TuningDatabaseInfo> GetTuningDatabaseInfo(string id)
        {

            return Ok(_tuningDatabaseHandler.getTuningDatabaseInfoById(id));
        }

        // 3rd API: Get tuning special info by given id
        [HttpGet("tuningspecial/{id}")]
        public ActionResult<TuningSpecialInfo> GetTuningSpecialInfo(string id)
        {
            return Ok(_tuningDatabaseHandler.getTuningSpecialInfoByTuningId(id));
        }

        [HttpGet("tuningecuListFull")]
        public ActionResult<EcuInfo> GetEcuList()
        {
            return Ok(_tuningDatabaseHandler.GetEcuList());
        }

        [HttpPost("GenerateTuningVariant")]
        public ActionResult<TuningSpecialInfo> GenerateTuningVariant(InputNewVariant inputNewVariant)
        {
            return Ok(_tuningDatabaseHandler.GenerateTuningVariant(inputNewVariant));
        }

        //Todo check for admin
        [HttpPost("DeleteTuningVariant")]
        public ActionResult<TuningSpecialInfo> DeleteTuningVariant(StringInput stringInput)
        {
            _tuningDatabaseHandler.DeleteTuningVariant(stringInput.Input);
            return Ok();
        }
    }
}

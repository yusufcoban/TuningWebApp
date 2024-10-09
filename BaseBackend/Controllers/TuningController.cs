using BaseBackend.Models;

using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TuningController : ControllerBase
    {
        TuningDatabaseHandler tuningDatabaseHandler = new TuningDatabaseHandler();
        // Generate fake car brands data
       

        // 1st API: Get list of all car brands
        [HttpGet("carbrands")]
        public ActionResult<List<CarBrand>> GetCarBrands()
        {
            return Ok(tuningDatabaseHandler.getCarBrands());
        }

        // 2nd API: Get list of tuning database info by given id
        [HttpGet("tuningdatabase/{id}")]
        public ActionResult<TuningDatabaseInfo> GetTuningDatabaseInfo(string id)
        {
            
            return Ok(tuningDatabaseHandler.getTuningDatabaseInfoById(id));
        }

        // 3rd API: Get tuning special info by given id
        [HttpGet("tuningspecial/{id}")]
        public ActionResult<TuningSpecialInfo> GetTuningSpecialInfo(string id)
        {
            return Ok(tuningDatabaseHandler.getTuningSpecialInfoByTuningId(id));
        }
    }
}

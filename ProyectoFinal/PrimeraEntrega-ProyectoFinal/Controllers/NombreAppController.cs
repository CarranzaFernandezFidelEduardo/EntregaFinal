using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrimeraEntrega_ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NombreAppController : ControllerBase
    {
        [HttpPost]
        public ActionResult CrearBombreAPP([FromQuery] string NombreApp)
        {
            return Ok(NombreApp);
            
        }
    }
}

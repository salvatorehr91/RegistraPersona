using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistraPersona.Context;
using RegistraPersona.SpTabla.Models;
using RegistraPersona.SpTabla.Services;

namespace RegistraPersona.SpTabla.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseResponseController : ControllerBase
    {
        private readonly ApplicationDbContext _conexion;
        private readonly ILogger<BaseResponseService> _logger;

        public BaseResponseController(ApplicationDbContext conexion, ILogger<BaseResponseService> logger)
        {
            _conexion = conexion;
            _logger = logger;
        }

        // POST Action
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] BaseResponse request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud no puede estar vacía.");
            }

            var service = new BaseResponseService(_conexion, _logger);
            string response = service.RealizarPeticionBD(request);
            // string response2 = BaseResponseService.RealizarPeticionBD(request);

            
            return Ok(response);
        }
    }
}

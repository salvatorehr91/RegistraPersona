using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistraPersona.Context;
using RegistraPersona.Models;
using RegistraPersona.Services;

namespace RegistraPersona.Controllers;

[ApiController]
[Route("[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly ConexionSqlServer _conexion;
    private readonly ILogger<AlumnosController> _logger;
    public AlumnosController(ILogger<AlumnosController> logger, ConexionSqlServer conexion)
    {
        _conexion = conexion;
        _logger = logger;
    }
    
    // GET all action
    [HttpGet]
    public ActionResult<List<DTOESTUDIANTE>> GetAll()
    {
        var alumnos = _conexion.DTOESTUDIANTE.ToList();
        return Ok(alumnos);
    }

    // GET by Id action
    [HttpGet("{matricula}")]
    public ActionResult<DTOESTUDIANTE> Get(int matricula)
    {
        // var alumno = AlumnosService.Get(matricula);
        var alumno = _conexion.DTOESTUDIANTE.FirstOrDefault(a => a.VMATRICULA == matricula);

        if(alumno is null)
        {
            _logger.LogInformation("El ID {VMATRICULA} no fue encontrado", matricula);
            return NotFound();
        }

        return Ok(alumno);
    }

    // POST action
    [HttpPost]
    public IActionResult Post([FromBody] DTOESTUDIANTE alumno)
    {
        _logger.LogInformation("Alumno recibido: {VNOMBRE} {VAPATERNO} {VAMATERNO}, Edad: {DNACIMIENTO}, Estatus: {VSTATUS}", alumno.VNOMBRE, alumno.VAPATERNO, alumno.VAMATERNO, alumno.DNACIMIENTO, alumno.VSTATUS);

        _conexion.DTOESTUDIANTE.Add(alumno);
        _conexion.SaveChanges();
        return CreatedAtAction(nameof(Get), new { matricula = alumno.VMATRICULA }, alumno);
    }

    // POST action by SP
    [HttpPost("sp")]
    [Consumes("application/json")]
    public IActionResult PostSP([FromBody] DTOESTUDIANTE alumno)
    {
        _logger.LogInformation("Alumno recibido para SP: {VNOMBRE} {VAPATERNO} {VAMATERNO}, Edad: {DNACIMIENTO}, Estatus: {VSTATUS}", alumno.VNOMBRE, alumno.VAPATERNO, alumno.VAMATERNO, alumno.DNACIMIENTO, alumno.VSTATUS);

        var result = _conexion.DTOESTUDIANTE.FromSqlInterpolated($"EXEC SPINS_ESTUDIANTE @VNOMBRE={alumno.VNOMBRE}, @VAPATERNO={alumno.VAPATERNO}, @VAMATERNO={alumno.VAMATERNO}, @DNACIMIENTO={alumno.DNACIMIENTO}, @VSTATUS={alumno.VSTATUS}")
        .ToList()
        .FirstOrDefault();

        if (result != null)
        {
            return Ok(new { Consumes = "application/json", Message = "Matricula generada: " + result.VMATRICULA });
            // return Ok("Alumno insertado correctamente con la matrícula: " + result.VMATRICULA);
        }
        else
        {
            return StatusCode(500, "Error al insertar el alumno mediante SP.");
        }
    }


    // PUT action
    [HttpPut("{matricula}")]
    public IActionResult Put(int matricula, [FromBody] DTOESTUDIANTE alumno)
    {
        var alumnoExistente = _conexion.DTOESTUDIANTE.FirstOrDefault(a => a.VMATRICULA == matricula);
        if (alumnoExistente is null)        
        {
            _logger.LogInformation("El ID {VMATRICULA} no fue encontrado para actualización", matricula);
            return NotFound();
        }
        if (matricula != alumno.VMATRICULA)
            return BadRequest();

        _logger.LogInformation("Actualizando alumno: {VNOMBRE} {VAPATERNO} {VAMATERNO}, Edad: {DNACIMIENTO}, Estatus: {VSTATUS}", alumno.VNOMBRE, alumno.VAPATERNO, alumno.VAMATERNO, alumno.DNACIMIENTO, alumno.VSTATUS);
        
        _conexion.Entry(alumnoExistente).CurrentValues.SetValues(alumno);
        _conexion.SaveChanges();
        
        return Ok(alumno);
    }

    // DELETE action
    [HttpDelete("{matricula}")]
    public IActionResult Delete(int matricula)
    {
        var alumno = _conexion.DTOESTUDIANTE.FirstOrDefault(a => a.VMATRICULA == matricula);
        // var alumno = AlumnosService.Get(matricula);
        if (alumno is null)
        {
            _logger.LogInformation("El ID {VMATRICULA} no fue encontrado", matricula);
            return NotFound();
        }

        _logger.LogInformation("Eliminando alumno: {VNOMBRE} {VAPATERNO} {VAMATERNO}, Edad: {DNACIMIENTO}, Estatus: {VSTATUS}", alumno.VNOMBRE, alumno.VAPATERNO, alumno.VAMATERNO, alumno.DNACIMIENTO, alumno.VSTATUS);

        _conexion.DTOESTUDIANTE.Remove(alumno);
        _conexion.SaveChanges();
        
        return NoContent();
    }
}
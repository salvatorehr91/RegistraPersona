using RegistraPersona.Models;
using RegistraPersona.Services;
using Microsoft.AspNetCore.Mvc;

namespace RegistraPersona.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonaController : ControllerBase
{
    private readonly ILogger<PersonaController> _logger;

    public PersonaController(ILogger<PersonaController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Persona>> GetAll() => PersonaService.GetAll();
    // {
    //     return new List<Persona>
    //     {
    //         new Persona { Id = 1, Nombre = "Juan", APaterno = "Perez", AMaterno = "Gonzalez", Edad = 30 },
    //         new Persona { Id = 2, Nombre = "Maria", APaterno = "Gomez", AMaterno = "Rodriguez", Edad = 25 }
    //     };
    // }

    [HttpGet("{id}")]
    public ActionResult<Persona> Get(int id)
    {
        var persona = PersonaService.Get(id);
        // var persona = new Persona { Id = id, Nombre = "Juan", APaterno = "Perez", AMaterno = "Gonzalez", Edad = 30 };
        if(persona is null)
        {        
            _logger.LogInformation("El ID {Id} no fue encontrado", id);
            return NotFound();
        }
        
        return Ok(persona);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Persona persona)
    {
        // Aquí podrías agregar la lógica para guardar la persona en una base de datos
        _logger.LogInformation("Persona recibida: {Nombre} {APaterno} {AMaterno}, Estatus: {Estatus}", persona.Nombre, persona.APaterno, persona.AMaterno, persona.Estatus);

        PersonaService.Add(persona);
        // return Ok(persona);
        return CreatedAtAction(nameof(Get), new { id = persona.Id }, persona);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Persona persona)
    {
        if (id != persona.Id)
        {
            return BadRequest();
        }

        var existingPersona = PersonaService.Get(id);
        if (existingPersona is null)
        {
            return NotFound();
        }

        // existingPersona.Nombre = persona.Nombre;
        // existingPersona.APaterno = persona.APaterno;
        // existingPersona.AMaterno = persona.AMaterno;
        // existingPersona.Edad = persona.Edad;
        PersonaService.Update(persona);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var persona = PersonaService.Get(id);
        if (persona is null)
        {
            return NotFound();
        }

        PersonaService.Delete(persona);
        return NoContent();
    }
}
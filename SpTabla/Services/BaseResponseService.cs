using Microsoft.EntityFrameworkCore;
using RegistraPersona.SpTabla.Models;
using Newtonsoft.Json;
using RegistraPersona.Context;
using System.Text;
using RegistraPersona.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace RegistraPersona.SpTabla.Services
{
    public class BaseResponseService
    {
        private readonly ApplicationDbContext _conexion;
        private readonly ILogger<BaseResponseService> _logger;
        public BaseResponseService(ApplicationDbContext conexion, ILogger<BaseResponseService> logger)
        {
            _conexion = conexion;
            _logger = logger;
        }

        public string RealizarPeticionBD(BaseResponse request)
        {
            try
            {
                _logger.LogInformation("Valida request recibida");
                string peticionBD = ValidateRequest(request);
                if (peticionBD == null)
                {
                    return JsonConvert.SerializeObject(new { Message = "La solicitud no es válida." });
                }
                // var peticion = conexion.Database.ExecuteSqlRaw(peticionBD);
                var peticion = _conexion.Set<DTOESTUDIANTE>().FromSqlRaw(peticionBD).ToList();
                var response = new object();

                Console.WriteLine($"Peticion: {peticion}");
                
                if(peticion.Count == 0)
                {
                    response = new
                    {
                        Message = "Solicitud recibida correctamente",
                        Data = request
                    };
                }
                else
                {
                    response = new
                    {
                        Message = "Datos de salida de la solicitud",
                        Data = peticion
                    };
                }
                string jsonResponse = JsonConvert.SerializeObject(response);
                _logger.LogInformation("Respuesta generada correctamente");
                _logger.LogInformation($"Respuesta: {jsonResponse}");
                return jsonResponse;
            }
            catch (Exception)
            {
                throw new ArgumentException("La solicitud no es válida.");
            }
        }

        private string ValidateRequest(BaseResponse _request)
        {
            try
            {
                if (_request.Base == null || _request.Objeto == null)
                {
                    throw new ArgumentException("La solicitud debe contener los campos 'Base' y 'Objeto'.");
                }

                StringBuilder peticionBD = new();
                if(_request.Parametros != null && _request.Parametros.Length > 0)
                {
                    peticionBD.Append($"{_request.Objeto} ");
                    foreach (var param in _request.Parametros)
                    {
                        foreach (var subParam in param)
                        {
                            peticionBD.Append($"'{subParam.Value}', ");
                        }
                    }
                }
                Console.WriteLine($"PeticionBD: {peticionBD.ToString().TrimEnd(',', ' ')}");

                return peticionBD.ToString().TrimEnd(',', ' ');
            }
            catch (Exception)
            {
                throw new ArgumentException("La solicitud no es válida.");
            }
        }

    }
}

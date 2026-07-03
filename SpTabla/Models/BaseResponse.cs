
namespace RegistraPersona.SpTabla.Models
{
    public class BaseResponse
    {

        public required string Base { get; set; } = string.Empty;
        public required string Objeto { get; set; } = string.Empty;

        public required Dictionary<string, object>[] Parametros { get; set; }

    }
}

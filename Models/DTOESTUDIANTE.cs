using System.ComponentModel.DataAnnotations;

namespace RegistraPersona.Models
{
    public class DTOESTUDIANTE
    {
        public int IDALUMNO { get; set; }
        public required string VNOMBRE { get; set; }
        public required string VAPATERNO { get; set; }
        public required string VAMATERNO { get; set; }
        
        public DateTime DNACIMIENTO { get; set; }
        public required string VSTATUS { get; set; }
        
        [Key]
        public int VMATRICULA { get; set; }
    }
}
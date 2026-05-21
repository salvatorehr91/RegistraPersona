namespace RegistraPersona.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string APaterno { get; set; }
        public required string AMaterno { get; set; }
        // public int Edad { get; set; }
        public required string Estatus { get; set; }
    }
}
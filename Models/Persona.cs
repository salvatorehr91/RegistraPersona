namespace RegistraPersona.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public int Edad { get; set; }
    }
}
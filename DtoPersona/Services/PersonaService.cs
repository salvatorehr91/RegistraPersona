using RegistraPersona.DtoPersona.Models;

namespace RegistraPersona.DtoPersona.Services
{
    public static class PersonaService
    {
        static List<Persona> Personas { get; }

        static int nextId = 3;

        static PersonaService()
        {
            Personas = new List<Persona>
            {
                new Persona { Id = 1, Nombre = "Juan", APaterno = "Perez", AMaterno = "Gonzalez", Estatus = "Activo" },
                new Persona { Id = 2, Nombre = "Maria", APaterno = "Gomez", AMaterno = "Rodriguez", Estatus = "Inactivo" }
            };
        }

        public static List<Persona> GetAll() => Personas;

        public static Persona? Get(int id) => Personas.FirstOrDefault(p => p.Id == id);

        public static void Add(Persona persona)
        {
            // persona.Id = Personas.Count > 0 ? Personas.Max(p => p.Id) + 1 : 1;
            persona.Id = nextId++;
            Personas.Add(persona);
        }

        public static void Update(Persona persona)
        {
            var index = Personas.FindIndex(p => p.Id == persona.Id);
            if (index == -1)
                return;

            Personas[index] = persona;
        }

        public static void Delete(Persona persona)
        {
            Personas.Remove(persona);
        }
        
    }
}
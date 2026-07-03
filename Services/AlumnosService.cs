using Microsoft.EntityFrameworkCore;
using RegistraPersona.Models;

namespace RegistraPersona.Services
{
    public static class AlumnosService
    {
        static List<DTOESTUDIANTE> Alumnos { get; } = default!;

        // static int nextId = 3;

        static AlumnosService()
        {
            Alumnos = [];
        }

        public static List<DTOESTUDIANTE> GetAll() => Alumnos;

        public static DTOESTUDIANTE? Get(int id) => Alumnos.FirstOrDefault(a => a.VMATRICULA == id);

        public static void Update(DTOESTUDIANTE alumno)
        {
            var index = Alumnos.FindIndex(a => a.IDALUMNO == alumno.IDALUMNO);
            if (index == -1)
                return;

            Alumnos[index] = alumno;
        }

        public static void Delete(DTOESTUDIANTE alumno)
        {
            Alumnos.Remove(alumno);
        }
        
    }
}
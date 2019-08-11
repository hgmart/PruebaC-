using System;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var instance = SessionManager.InstanciaUnica;

            var alumnos = instance.Session.Query<Alumno>();
            alumnos.ToList().ForEach(alumno => Console.WriteLine(alumno.Nombre + " " + alumno.Apellido));

            SessionManager.CerrarConexion();
        }
    }
}

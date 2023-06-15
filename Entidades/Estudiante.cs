using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Estudiante
    {
        private string dni;
        private string nombre;
        private string apellido;
        private int edad;
        private bool asist;

        public Estudiante() { }

        public Estudiante(string dni, string nombre, string apellido, DateTime fecnac, bool asist)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.asist = asist;
            DateTime Fecha = DateTime.Now;
            int edad = Fecha.Year - fecnac.Year;
            if (fecnac.Month > Fecha.Month)
            { --edad; }
        }

        public void agregar(string Dni, string Nombre, string Apellido, DateTime fecnac, bool Asist) 
        {
            dni = Dni;
            nombre = Nombre;
            apellido = Apellido;
            asist = Asist;
            DateTime Fecha = DateTime.Now;
            int edad = Fecha.Year - fecnac.Year;
            if (fecnac.Month > Fecha.Month)
            { --edad; }
        }

        public string getDni()
        {
            return this.dni;
        }

		public string getNombre()
		{
			return this.nombre;
		}
        public string getAp() { return this.apellido; }
        public int getEd() { return this.edad; }
        public bool getAsist() { return this.asist; }
        public string DarDatos() {
            string datos = $"DNI: {dni}\n";
            datos += $"\n Nombre: {nombre}";
            datos += $"\n Apellido: {apellido}";
            datos += $"\n Edad: {edad}";
            datos += $"\n Asistencia: {(asist ? "Presente" : "Ausente")}";

            return datos;
        }
    }
}

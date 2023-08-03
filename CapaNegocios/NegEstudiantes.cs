using CapaDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
	public class NegEstudiantes
	{
		AdministrarProductos DatosEstudiantes = new AdministrarProductos();

		public int abmEstudiantes(string accion, Estudiante objestudiante)
		{
			return DatosEstudiantes.abmStudiantes(accion, objestudiante);
		}

		public DataSet listadoEst(string dni) 
		{
			return DatosEstudiantes.listadoAlumns(dni);
		}
	}
}

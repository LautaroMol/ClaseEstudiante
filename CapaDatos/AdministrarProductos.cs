using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Entidades;
using System.Data.SqlClient;

namespace CapaDatos
{
	public class AdministrarProductos : DatosConexion
	{
		public int abmStudiantes(string accion, Estudiante objEstudiante)
		{
			int result = -1; //controlamos que se realice la operacion
			string orden = string.Empty; //guardamos la consulta
			if (accion == "Alta") 
				orden = "insert into Estudiantes values (" + objEstudiante.getDni() + ",'" +
					objEstudiante.getNombre() + "','" + objEstudiante.getAp() + "'," + objEstudiante.getEd() + "," +
					objEstudiante.getAsist() +");";
			if (accion == "Modificar")
				orden = "update Estudiantes SET Nombre = '" + objEstudiante.getNombre() + "',Apellido ='"
					+ objEstudiante.getAp() + "',Edad=" + objEstudiante.getEd() + ",Asistencia=" +
					objEstudiante.getAsist() + " WHERE Dni = '" + objEstudiante.getDni() + "';";
			if (accion == "Borrar")
				orden = "delete * from Estudiantes WHERE Dni ='" + objEstudiante.getDni() + "'";

			//hacer el borrar
			OleDbCommand cmd = new OleDbCommand(orden, conexion);
			try
			{
				Abrirconexion();
				result = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new Exception("error al tratar de operar con los alumnos", ex);
			}
			finally
			{ CerrarConexion(); cmd.Dispose(); }
			return result;
		}
		public DataSet listadoAlumns(string dni) 
		{
			string orden = string.Empty;
			if (dni != "todos")
				orden = "select * from Estudiantes where Dni = " + dni.ToString() + ";";
			else orden = "select * from Estudiantes;";
			
			OleDbCommand cmd = new OleDbCommand(orden, conexion);
			DataSet ds = new DataSet();
			OleDbDataAdapter da = new OleDbDataAdapter();

			try
			{
				Abrirconexion();
				cmd.ExecuteNonQuery();
				da.SelectCommand = cmd;
				da.Fill(ds);
			}
			catch (Exception e)
			{
				throw new Exception("Error al listar profesionales", e);
			}
			finally
			{
				CerrarConexion() ; cmd.Dispose();
			}
			return ds;
		}
	}
}

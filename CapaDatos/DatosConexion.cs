using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace CapaDatos
{
	public class DatosConexion
	{
		protected OleDbConnection conexion;
		protected string cadenaConexion =  @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = D:\Documentos\facultad\programacion 2\BasededatosAlumno.accdb";
		public DatosConexion() 
		{
			conexion = new OleDbConnection(cadenaConexion);
		}
		public void Abrirconexion()
		{
			try
			{
				if (conexion.State == ConnectionState.Broken || conexion.State == ConnectionState.Closed)
					conexion.Open();
			}
			catch (Exception e)
			{
				throw new Exception("error al tratar de abrir la conexion", e);
			}
		}

		public void CerrarConexion() {
		try
			{
				if (conexion.State == ConnectionState.Open) conexion.Close();
			}
			catch (Exception e)
			{
				throw new Exception("Error al tratar de cerrar la conexion", e);
			}
		}
	}
}

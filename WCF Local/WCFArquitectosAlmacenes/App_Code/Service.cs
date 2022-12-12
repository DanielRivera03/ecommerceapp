using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
public class Service : IService
{
	DataSet dataset = new DataSet(); // REPOSITORIO UNIVERSAL PARA ALMACENAR DATOS EN MEMORIA
	SqlDataAdapter resultado; // EJECUTAR CUALQUIER PROCESO EN BASE DE DATOS
	string Patron = "xmaVwSv5z87580fR^52"; // PATRON DE ENCRIPTACION
	
	// INICIO DE SESION USUARIOS -> VALIDAR CREDENCIALES DE ACCESO
	public DataSet ValidarCredencialesUsuarios(string Usuario, string Contrasenia)
	{
		try
		{
			using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
            {
				resultado = new SqlDataAdapter("SP_ValidarCredencialesUsuarios", oconexion);
				resultado.SelectCommand.CommandType = CommandType.StoredProcedure;
				resultado.SelectCommand.Parameters.AddWithValue("@Usuario", Usuario);
				resultado.SelectCommand.Parameters.AddWithValue("@Contrasenia", Contrasenia);
				resultado.SelectCommand.Parameters.AddWithValue("@Patron", Patron);
				resultado.Fill(dataset, "Usuarios");
				return dataset;
			}
		}
		catch (Exception)
		{
			return null;
		}
	}
}

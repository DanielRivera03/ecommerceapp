using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDatosArquitectos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
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
}

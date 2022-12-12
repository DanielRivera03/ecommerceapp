using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WCFDatosArquitectos
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public DataSet ValidarCredencialesUsuarios(string Usuario, string Contrasenia)
        {
            DataSet dataset = new DataSet(); // REPOSITORIO UNIVERSAL PARA ALMACENAR DATOS EN MEMORIA
            SqlDataAdapter resultado; // EJECUTAR CUALQUIER PROCESO EN BASE DE DATOS
            string Patron = "xmaVwSv5z87580fR^52"; // PATRON DE ENCRIPTACION
            // INICIO DE SESION USUARIOS -> VALIDAR CREDENCIALES DE ACCESO
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

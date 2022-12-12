using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

public class Conexion
{
    // CONEXION A BASE DE DATOS DESDE WEBCONFIG
    public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
}
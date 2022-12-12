
/*
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
░░           ARQUITECTOS ALMACENES S.A DE C.V                                               
░░                      ECOMMERCE APP
░░≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡                       
░░                                                                               
░░   -> AUTOR: DANIEL RIVERA                                                               
░░   -> C# ASP.NET MVC5 RAZOR JAVASCRIPT AJAX JQUERY JSON
░░       WEBSERVICE SOAP, CSS3, BOOTSTRAPT, API REST PAYPAL
░░   -> DESARROLLO EN CAPAS: NEGOCIO, DATOS Y PRESENTACION
░░   -> GITHUB: (danielrivera03)                                             
░░       https://github.com/DanielRivera03                              
░░   -> TODOS LOS DERECHOS RESERVADOS                           
░░       © 2022    
░░                                                      
░░   -> POR FAVOR TOMAR EN CUENTA TODOS LOS COMENTARIOS
░░      Y REALIZAR LOS AJUSTES PERTINENTES ANTES DE INICIAR
░░
░░              ♥♥ HECHO CON MUCHAS TAZAS DE CAFE ♥♥
░░                                                                               
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░

*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace CapaDatos
{
    public class CD_ProblemasPlataforma
    {
        // LISTADO COMPLETO DE TODOS LOS PROBLEMAS DE PLATAFORMA REGISTRADOS POR LOS USUARIOS Y CLIENTES
        public List<ProblemasPlataforma> Listar()
        {
            List<ProblemasPlataforma> lista = new List<ProblemasPlataforma>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ConsultarReportesPloblemasPlataforma", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new ProblemasPlataforma()
                                    {
                                        IdReportePlataforma = Convert.ToInt32(dr["IdReportePlataforma"]),
                                        NombreReporte = dr["NombreReporte"].ToString(),
                                        DescripcionReporte = dr["DescripcionReporte"].ToString(),
                                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]), // OBTENER FECHA REGISTRADA
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                        NombreImagenReporte = dr["NombreImagenReporte"].ToString(),
                                        EstadoReporte = dr["EstadoReporte"].ToString(),
                                        oUsuarios = new Usuarios() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]), Usuario = dr["Usuario"].ToString() },
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<ProblemasPlataforma>();
            }
            return lista;
        }// CIERRE public List<ProblemasPlataforma> Listar()

        // LISTADO COMPLETO DE TODOS LOS PROBLEMAS DE PLATAFORMA REGISTRADOS POR LOS USUARIOS Y CLIENTES [ACTIVOS
        public List<ProblemasPlataforma> ListarProblemasActivos()
        {
            List<ProblemasPlataforma> lista = new List<ProblemasPlataforma>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ConsultarReportesPloblemasPlataformaActivos", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new ProblemasPlataforma()
                                    {
                                        IdReportePlataforma = Convert.ToInt32(dr["IdReportePlataforma"]),
                                        NombreReporte = dr["NombreReporte"].ToString(),
                                        DescripcionReporte = dr["DescripcionReporte"].ToString(),
                                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]), // OBTENER FECHA REGISTRADA
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                        NombreImagenReporte = dr["NombreImagenReporte"].ToString(),
                                        EstadoReporte = dr["EstadoReporte"].ToString(),
                                        oUsuarios = new Usuarios() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]), Usuario = dr["Usuario"].ToString() },
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<ProblemasPlataforma>();
            }
            return lista;
        }// CIERRE public List<ProblemasPlataforma> ListarProblemasActivos()

        // LISTADO COMPLETO DE TODOS LOS PROBLEMAS DE PLATAFORMA REGISTRADOS POR LOS USUARIOS Y CLIENTES [EN CURSO]
        public List<ProblemasPlataforma> ListarProblemasEnCurso()
        {
            List<ProblemasPlataforma> lista = new List<ProblemasPlataforma>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ConsultarReportesPloblemasPlataformaEnCurso", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new ProblemasPlataforma()
                                    {
                                        IdReportePlataforma = Convert.ToInt32(dr["IdReportePlataforma"]),
                                        NombreReporte = dr["NombreReporte"].ToString(),
                                        DescripcionReporte = dr["DescripcionReporte"].ToString(),
                                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]), // OBTENER FECHA REGISTRADA
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                        NombreImagenReporte = dr["NombreImagenReporte"].ToString(),
                                        EstadoReporte = dr["EstadoReporte"].ToString(),
                                        oUsuarios = new Usuarios() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]), Usuario = dr["Usuario"].ToString() },
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<ProblemasPlataforma>();
            }
            return lista;
        }// CIERRE public List<ProblemasPlataforma> ListarProblemasEnCurso()

        // LISTADO COMPLETO DE TODOS LOS PROBLEMAS DE PLATAFORMA REGISTRADOS POR LOS USUARIOS Y CLIENTES [RESUELTOS]
        public List<ProblemasPlataforma> ListarProblemasResueltos()
        {
            List<ProblemasPlataforma> lista = new List<ProblemasPlataforma>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ConsultarReportesPloblemasPlataformaResueltos", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new ProblemasPlataforma()
                                    {
                                        IdReportePlataforma = Convert.ToInt32(dr["IdReportePlataforma"]),
                                        NombreReporte = dr["NombreReporte"].ToString(),
                                        DescripcionReporte = dr["DescripcionReporte"].ToString(),
                                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]), // OBTENER FECHA REGISTRADA
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                        NombreImagenReporte = dr["NombreImagenReporte"].ToString(),
                                        EstadoReporte = dr["EstadoReporte"].ToString(),
                                        oUsuarios = new Usuarios() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]), Usuario = dr["Usuario"].ToString() },
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<ProblemasPlataforma>();
            }
            return lista;
        }// CIERRE public List<ProblemasPlataforma> ListarProblemasResueltos()

        // LISTADO COMPLETO DE TODOS LOS PROBLEMAS DE PLATAFORMA REGISTRADOS POR LOS USUARIOS Y CLIENTES [RESUELTOS]
        public List<ProblemasPlataforma> ListarProblemasCerrados()
        {
            List<ProblemasPlataforma> lista = new List<ProblemasPlataforma>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ConsultarReportesPloblemasPlataformaCerrados", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new ProblemasPlataforma()
                                    {
                                        IdReportePlataforma = Convert.ToInt32(dr["IdReportePlataforma"]),
                                        NombreReporte = dr["NombreReporte"].ToString(),
                                        DescripcionReporte = dr["DescripcionReporte"].ToString(),
                                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]), // OBTENER FECHA REGISTRADA
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                        NombreImagenReporte = dr["NombreImagenReporte"].ToString(),
                                        EstadoReporte = dr["EstadoReporte"].ToString(),
                                        oUsuarios = new Usuarios() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]), Usuario = dr["Usuario"].ToString() },
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<ProblemasPlataforma>();
            }
            return lista;
        }// CIERRE public List<ProblemasPlataforma> ListarProblemasCerrados()


        // REGISTRO DE NUEVOS PROBLEMAS DE PLATAFORMA
        public int Registrar(ProblemasPlataforma obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProblemasPlataforma", oconexion);
                    cmd.Parameters.AddWithValue("NombreReporte", obj.NombreReporte);
                    cmd.Parameters.AddWithValue("DescripcionReporte", obj.DescripcionReporte);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.UsuarioReporteID);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        // EDITAR PROBLEMAS DE PLATAFORMA
        public bool Editar(ProblemasPlataforma obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProblemasPlataforma", oconexion);
                    cmd.Parameters.AddWithValue("IdReportePlataforma", obj.IdReportePlataforma);
                    cmd.Parameters.AddWithValue("DescripcionReporte", obj.DescripcionReporte);
                    cmd.Parameters.AddWithValue("EstadoReporte", obj.EstadoReporte);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        // CONTROLAR RUTA DE IMAGENES
        public bool GuardarDatosImagen(ProblemasPlataforma obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_GuardarImagenProblemasPlataforma", oconexion);
                    cmd.Parameters.AddWithValue("NombreImagenReporte", obj.NombreImagenReporte);
                    cmd.Parameters.AddWithValue("IdReportePlataforma", obj.IdReportePlataforma);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "Lo sentimos, no fue posible actualizar la imagen";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;

        }

    }
}

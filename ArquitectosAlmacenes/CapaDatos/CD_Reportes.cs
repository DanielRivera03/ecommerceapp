
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
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Reportes
    {
        // REPORTE INICIO ADMINISTRADORES
        public ReporteDashboardAdministradores VerDashBoard()
        {
            ReporteDashboardAdministradores objeto = new ReporteDashboardAdministradores();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReportePanelAdministracion_Administradores", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new ReporteDashboardAdministradores()
                            {
                                TotalUsuarios = Convert.ToInt32(dr["TotalUsuarios"]),
                                TotalProductos = Convert.ToInt32(dr["TotalProductos"]),
                                TotalGarantias = Convert.ToInt32(dr["TotalGarantias"]),
                                TotalClientes = Convert.ToInt32(dr["TotalClientes"]),
                            };
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                objeto = new ReporteDashboardAdministradores();
            }
            return objeto;
        }

        // REPORTE INICIO PRESIDENCIA
        public ReporteDashboardPresidencia VerDashBoardPresidencia()
        {
            ReporteDashboardPresidencia objeto = new ReporteDashboardPresidencia();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReportePanelAdministracion_Presidencia", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new ReporteDashboardPresidencia()
                            {
                                TotalEmpleados = Convert.ToInt32(dr["TotalEmpleados"]),
                                TotalClientes = Convert.ToInt32(dr["TotalClientes"]),
                                TotalProductos = Convert.ToInt32(dr["TotalProductos"]),
                                TotalVentasProcesadas = Convert.ToInt32(dr["TotalVentas"]),
                                
                            };
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                objeto = new ReporteDashboardPresidencia();
            }
            return objeto;
        }

        // REPORTE INICIO PRESIDENCIA
        public ReporteDashboardGerencia VerDashBoardGerencia()
        {
            ReporteDashboardGerencia objeto = new ReporteDashboardGerencia();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReportePanelAdministracion_Gerencia", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new ReporteDashboardGerencia()
                            {
                                TotalEmpleados = Convert.ToInt32(dr["TotalEmpleados"]),
                                TotalClientes = Convert.ToInt32(dr["TotalClientes"]),
                                TotalProductos = Convert.ToInt32(dr["TotalProductos"]),
                                TotalProductosVendidos = Convert.ToInt32(dr["TotalProductosVendidos"]),

                            };
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                objeto = new ReporteDashboardGerencia();
            }
            return objeto;
        }


        // REPORTE GRAFICO CANTIDAD DE PRODUCTOS POR CATEGORIAS 
        public ReporteDashboardAdministradores TotalProductosPorCategorias()
        {
            ReporteDashboardAdministradores objeto = new ReporteDashboardAdministradores();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteCantidadProductosPorCategoria", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new ReporteDashboardAdministradores()
                            {
                                TotalJoyeria = Convert.ToInt32(dr["TotalJoyeria"]),
                                TotalRelojeria = Convert.ToInt32(dr["TotalRelojeria"]),
                                TotalCalzado = Convert.ToInt32(dr["TotalCalzado"]),
                                TotalTecnologia = Convert.ToInt32(dr["TotalTecnologia"]),
                                TotalLineaBlanca = Convert.ToInt32(dr["TotalLineaBlanca"]),
                                TotalElectrodomesticos = Convert.ToInt32(dr["TotalElectrodomesticos"]),
                                TotalBelleza = Convert.ToInt32(dr["TotalBelleza"]),
                                TotalJuguetes = Convert.ToInt32(dr["TotalJuguetes"]),
                            };
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                objeto = new ReporteDashboardAdministradores();
            }
            return objeto;
        }

        // REPORTE GRAFICO CANTIDAD DE PRODUCTOS POR CATEGORIAS 
        public ReporteDashboardAdministradores TotalVentasPorMes(int anioactual)
        {
            ReporteDashboardAdministradores objeto = new ReporteDashboardAdministradores();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentasPorMes", oconexion);
                    cmd.Parameters.AddWithValue("AnioActual", 2022); // CAMBIAR POR AÑO EN CURSO
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new ReporteDashboardAdministradores()
                            {
                                // CAMBIAR IDENTIFICADOR POR AÑO EN CURSO -> [2022xx] EN BASE DE DATOS <-
                                
                                TotalEnero = Convert.ToDouble(dr["TotalEnero"]),
                                TotalFebrero = Convert.ToDouble(dr["TotalFebrero"]),
                                TotalMarzo = Convert.ToDouble(dr["TotalMarzo"]),
                                TotalAbril = Convert.ToDouble(dr["TotalAbril"]),
                                TotalMayo = Convert.ToDouble(dr["TotalMayo"]),
                                TotalJunio = Convert.ToDouble(dr["TotalJunio"]),
                                TotalJulio = Convert.ToDouble(dr["TotalJulio"]),
                                TotalAgosto = Convert.ToDouble(dr["TotalAgosto"]),
                                TotalSeptiembre = Convert.ToDouble(dr["TotalSeptiembre"]),
                                TotalOctubre = Convert.ToDouble(dr["TotalOctubre"]),
                                TotalNoviembre = Convert.ToDouble(dr["TotalNoviembre"]),
                                TotalDiciembre = Convert.ToDouble(dr["TotalDiciembre"]),
                                TotalIngresos = Convert.ToDouble(dr["TotalIngresos"])
                            };
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                objeto = new ReporteDashboardAdministradores();
            }
            return objeto;
        }

        // REPORTE DE VENTAS PROCESADAS
        public List<ReporteVentas> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<ReporteVentas> lista = new List<ReporteVentas>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentasProcesadas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new ReporteVentas()
                                    {
                                        FechaVenta = dr["FechaVenta"].ToString(),
                                        Cliente = dr["Cliente"].ToString(),
                                        Producto = dr["Producto"].ToString(),
                                        Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-SV")),
                                        Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                        Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-SV")),
                                        IdTransaccion = dr["IdTransaccion"].ToString()
                                    }
                          );
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<ReporteVentas>();
            }
            return lista;
        }
    }
}

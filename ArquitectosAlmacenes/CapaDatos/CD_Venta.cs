
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



using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Venta
    {
        // REGISTRAR NUEVAS VENTAS
        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarVentasClientes", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("IdDepartamento", obj.oDepartamentos.IdDepartamento);
                    cmd.Parameters.AddWithValue("IdMunicipios", obj.oMunicipios.IdMunicipios);
                    cmd.Parameters.AddWithValue("TotalProducto", obj.TotalProducto);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Dui", obj.Dui);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("IdTransaccion", obj.IdTransaccion);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        // LISTAR TODAS LAS COMPRAR REALIZADAS POR CLIENTES [TABLA GENERAL DE TODAS LAS COMPRAS PROCESADAS]
        public List<Venta> ListarComprasProcesadasClientes(int idusuario)
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoVentasProcesadasClientes", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idusuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                MontoTotal = Convert.ToDouble(dr["MontoTotal"], new CultureInfo("es-SV")),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]), // OBTENER FECHA REGISTRADA
                                // REALIZAR CONVERSION PARA MOSTRAR EN MIS COMPRAS -> TIENDA EN LINEA CLIENTES
                                ConversionFechaVenta = Convert.ToDateTime(dr["FechaVenta"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // LISTAR TODAS LAS COMPRAR REALIZADAS POR CLIENTES [DETALLE GENERAL DE TODOS LOS ARTICULOS ADQUIRIDOS]
        public List<DetalleVenta> ListarDetallesComprasProcesadasClientes(int idusuario, int idventa)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_DetalleVentasProcesadasClientes", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idusuario);
                    cmd.Parameters.AddWithValue("IdVenta", idventa);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new DetalleVenta()
                            {
                                IdDetalleVenta = Convert.ToInt32(dr["IdDetalleVenta"]),
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                oMarcas = new Marcas()
                                {
                                    Descripcion = dr["Descripcion"].ToString()
                                },
                                oProducto = new Productos()
                                {
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["DescripcionProducto"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                },
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                Total = Convert.ToDouble(dr["Total"]),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                                oGarantias = new Garantias()
                                {
                                    DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"])
                                },
                                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]), // OBTENER FECHA REGISTRADA
                                // REALIZAR CONVERSION PARA MOSTRAR EN MIS COMPRAS -> TIENDA EN LINEA CLIENTES
                                ConversionFechaVenta = Convert.ToDateTime(dr["FechaVenta"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                ContadorDiasGarantias = Convert.ToInt32(dr["ContadorGarantias"])
                            }
                            );
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<DetalleVenta>();
            }
            return lista;
        }

        // LISTAR TODAS LAS COMPRAR REALIZADAS POR CLIENTES [DETALLE GENERAL DE TODOS LOS ARTICULOS ADQUIRIDOS]
        // VALIDO EXCLUSIVAMENTE PARA PORTAL ADMINISTRATIVO
        public List<Venta> ListarComprasProcesadasClientes_PortalAdministrativo()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoVentasProcesadas_PortalAdministracion", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                MontoTotal = Convert.ToDouble(dr["MontoTotal"], new CultureInfo("es-SV")),
                                oUsuarios = new Usuarios()
                                {
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                },
                                Telefono = dr["Telefono"].ToString(),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]), // OBTENER FECHA REGISTRADA
                                // REALIZAR CONVERSION PARA MOSTRAR EN MIS COMPRAS -> TIENDA EN LINEA CLIENTES
                                ConversionFechaVenta = Convert.ToDateTime(dr["FechaVenta"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // LISTAR TODOSS LOS DETALLES DE COMPRAS REALIZADAS POR CLIENTES [DETALLE ESPECIFICO DE TODOS LOS ARTICULOS ADQUIRIDOS]
        // VALIDO EXCLUSIVAMENTE PARA PORTAL ADMINISTRATIVO
        public List<DetalleVenta> ListarDetallesComprasProcesadasClientes_PortalAdministrativo()
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_listadodetallesventasprocesadas_administracion", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new DetalleVenta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                                // PRODUCTOS
                                oProducto = new Productos()
                                {
                                    Nombre = dr["Nombre"].ToString(),
                                },
                                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]), // OBTENER FECHA REGISTRADA
                                // REALIZAR CONVERSION PARA MOSTRAR EN MIS COMPRAS -> TIENDA EN LINEA CLIENTES
                                ConversionFechaVenta = Convert.ToDateTime(dr["FechaVenta"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                // MARCAS DE PRODUCTOS
                                oMarcas = new Marcas()
                                {
                                    Descripcion = dr["Descripcion"].ToString(),
                                },
                                // GARANTIAS DE MARCAS DE PRODUCTOS
                                oGarantias = new Garantias()
                                {
                                    DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]),
                                },
                                ContadorDiasGarantias = Convert.ToInt32(dr["ContadorDias"]),
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<DetalleVenta>();
            }
            return lista;
        }

        // OBTENER LISTADO DE VENTAS PROCESADAS CLIENTES -> REPORTES DE PROBLEMAS PEDIDOS / COMPRAS CLIENTES
        // EXCLUSIVAMENTE DE USO PARA LOS CLIENTES DE LA PLATAFORMA
        public List<Venta> ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas(int idusuario)
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_IdTransacciones_VentasProcesadas_ReportesProblemasPedidos", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idusuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Venta()
                                    {
                                        IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                        oUsuarios = new Usuarios() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]) },
                                        IdTransaccion = dr["IdTransaccion"].ToString(),
                                        ContadorDiasVentasProcesadas = Convert.ToInt32(dr["ContadorDias"])
                                    }
                                );
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // OBTENER LISTADO DE VENTAS PROCESADAS CLIENTES -> REPORTES DE PROBLEMAS PEDIDOS / COMPRAS CLIENTES
        // EXCLUSIVAMENTE DE USO PARA LOS EMPLEADOS ADMINISTRATIVOS
        // LA CONDICION ES SI UNA VENTA EXCEDE LOS 60 DIAS DE SER PROCESADA, LOS CLIENTES YA NO PUEDEN SOLICITAR REPORTES DE PROBLEMA DE DICHA VENTA
        // LO ANTERIOR ES UN PROCESO ADMINISTRATIVO, DONDE EL CLIENTE SE ACERCA A LAS SUCURSALES... CUANDO SU OPCION DE REPORTE HA SIDO BLOQUEADA
        public List<Venta> ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas_Administracion()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_IdTransacciones_VentasProcesadas_ReportesProblemasPedidos_Administracion", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Venta()
                                    {
                                        IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                        oUsuarios = new Usuarios() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]) },
                                        Dui = dr["Dui"].ToString(),
                                        IdTransaccion = dr["IdTransaccion"].ToString(),
                                        ContadorDiasVentasProcesadas = Convert.ToInt32(dr["ContadorDias"])
                                    }
                                );
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // OBTENER LISTADO DE DUI REGISTRADOS EN VENTAS PROCESADAS -> CONSULTA VALIDADA A MOSTRAR UN SOLO DUI, SIN DUPLICADOS
        public List<Venta> ListadoDuiRegistrados_VentasClientes()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ListadoDuiRegistradosVentasClientes", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Venta()
                                    {
                                        Dui = dr["Dui"].ToString(),
                                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    }
                                );
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // LISTADO COMPLETO DE TODOS LOS PROBLEMAS DE COMPRAS REGISTRADOS - CLIENTES
        public List<ProblemasVentas> ListarProblemasComprasClientes()
        {
            List<ProblemasVentas> lista = new List<ProblemasVentas>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ListadoProblemasComprasClientes", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new ProblemasVentas()
                                    {
                                        oUsuarios = new Usuarios() { 
                                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]), 
                                            Nombres = dr["Nombres"].ToString(), 
                                            Apellidos = dr["Apellidos"].ToString() 
                                        },
                                        IdReporteProblema = Convert.ToInt32(dr["IdReporteProblema"]),
                                        DescripcionReporteProblemaVenta = dr["DescripcionProblema"].ToString(),
                                        ActualizacionComentariosReporteProblemaVenta = dr["ComentarioRetroalimentacion"].ToString(),
                                        oVentas = new Venta() { 
                                            IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                            TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                            MontoTotal = Convert.ToDouble(dr["MontoTotal"]),
                                            IdTransaccion = dr["IdTransaccion"].ToString(),
                                            
                                        },
                                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]), // OBTENER FECHA REGISTRADA
                                        ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                        FechaActualizacion = Convert.ToDateTime(dr["FechaActualizacion"]), // OBTENER FECHA REGISTRADA
                                        ConversionFechaActualizacion = Convert.ToDateTime(dr["FechaActualizacion"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                        Estado = Convert.ToBoolean(dr["Estado"]),
                                        DuiCliente = dr["Dui"].ToString(),
                                        MunicipioCliente = dr["NombreMunicipio"].ToString(),
                                        DepartamentoCliente = dr["NombreDepartamento"].ToString(),
                                        DireccionCliente = dr["Direccion"].ToString(),
                                        TelefonoCliente = dr["Telefono"].ToString(),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<ProblemasVentas>();
            }
            return lista;
        }// CIERRE public List<Garantias> Listar()


        // REGISTRO DE PROBLEMAS PEDIDOS / COMPRAS CLIENTES -> EXCLUSIVO PARA CLIENTES
        public int Registrar(ProblemasVentas obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProblemasComprasClientes", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("IdVenta", obj.oVentas.IdVenta);
                    cmd.Parameters.AddWithValue("DescripcionProblema", obj.DescripcionReporteProblemaVenta);
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

        // REGISTRO DE PROBLEMAS PEDIDOS / COMPRAS CLIENTES -> EXCLUSIVO PARA EMPLEADOS ADMINISTRATIVOS
        public int RegistrarProblemasPedidos_Administrativo(ProblemasVentas obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProblemasComprasClientes", oconexion);
                    // SOLUCION ID USUARIO -> SEPARAR EN DOS PROCESOS LOS DATOS
                    // SELECT PARA ID TRANSACCIONES 
                    // SELECT REFERENCIADO A DUI DE USUARIO
                    // POSTERIORMENTE, LLAMAR A SUS OBJETOS CON EL CAMPO REFERENCIADO EN CUESTION
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuarios.IdUsuario);
                    cmd.Parameters.AddWithValue("IdVenta", obj.oVentas.IdVenta);
                    cmd.Parameters.AddWithValue("DescripcionProblema", obj.DescripcionReporteProblemaVenta);
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

        // EDITAR PROBLEMAS PEDIDOS / COMPRAS CLIENTES -> EXCLUSIVO PARA EMPLEADOS ADMINISTRATIVOS
        public bool Editar(ProblemasVentas obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarProblemasComprasClientes", oconexion);
                    cmd.Parameters.AddWithValue("IdReporteProblema", obj.IdReporteProblema);
                    cmd.Parameters.AddWithValue("ComentarioRetroalimentacion", obj.ActualizacionComentariosReporteProblemaVenta);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    //cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
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

        // VERIFICAR SI CODIGO UNICO DE CATEGORIA REGISTRADO EXISTE EN BASE DE DATOS
        public bool VerificarExistenciasReporteProblemasVentasClientes(int idventa)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ComprobarExistencias_ReportesProblemasVentas", oconexion);
                    cmd.Parameters.AddWithValue("IdVenta", idventa);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }
            return resultado;
        }

        // LISTAR TODAS LAS COMPRAS PROCESADAS -> GESTION DE ENVIOS [TODOS LOS PEDIDOS SIN FILTROS]
        public List<Venta> ListarComprasPedidos()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoOrdenesRegistradas", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Dui = dr["Dui"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]), // OBTENER FECHA REGISTRADA
                                // REALIZAR CONVERSION PARA MOSTRAR EN MIS COMPRAS -> TIENDA EN LINEA CLIENTES
                                ConversionFechaVenta = Convert.ToDateTime(dr["FechaVenta"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                MontoTotal = Convert.ToDouble(dr["MontoTotal"], new CultureInfo("es-SV")),
                                EstadoPedido = dr["EstadoPedido"].ToString(),
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // LISTAR TODAS LAS COMPRAS PROCESADAS -> GESTION DE ENVIOS [PEDIDOS EN PROCESO]
        public List<Venta> ListarComprasPedidosEnProceso()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoOrdenesEnProceso", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Dui = dr["Dui"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]), // OBTENER FECHA REGISTRADA
                                // REALIZAR CONVERSION PARA MOSTRAR EN MIS COMPRAS -> TIENDA EN LINEA CLIENTES
                                ConversionFechaVenta = Convert.ToDateTime(dr["FechaVenta"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                MontoTotal = Convert.ToDouble(dr["MontoTotal"], new CultureInfo("es-SV")),
                                EstadoPedido = dr["EstadoPedido"].ToString(),
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // LISTAR TODAS LAS COMPRAS PROCESADAS -> GESTION DE ENVIOS [PEDIDOS ENVIADOS]
        public List<Venta> ListarComprasPedidosEnviados()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoOrdenesEnviadas", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Dui = dr["Dui"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]), // OBTENER FECHA REGISTRADA
                                // REALIZAR CONVERSION PARA MOSTRAR EN MIS COMPRAS -> TIENDA EN LINEA CLIENTES
                                ConversionFechaVenta = Convert.ToDateTime(dr["FechaVenta"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                MontoTotal = Convert.ToDouble(dr["MontoTotal"], new CultureInfo("es-SV")),
                                EstadoPedido = dr["EstadoPedido"].ToString(),
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // LISTAR TODAS LAS COMPRAS PROCESADAS -> GESTION DE ENVIOS [PEDIDOS CANCELADAS]
        public List<Venta> ListarComprasPedidosCanceladas()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoOrdenesCanceladas", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Dui = dr["Dui"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]), // OBTENER FECHA REGISTRADA
                                // REALIZAR CONVERSION PARA MOSTRAR EN MIS COMPRAS -> TIENDA EN LINEA CLIENTES
                                ConversionFechaVenta = Convert.ToDateTime(dr["FechaVenta"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                MontoTotal = Convert.ToDouble(dr["MontoTotal"], new CultureInfo("es-SV")),
                                EstadoPedido = dr["EstadoPedido"].ToString(),
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // LISTAR TODAS LAS COMPRAS PROCESADAS -> GESTION DE ENVIOS [PEDIDOS ENVIADOS]
        public List<Venta> ListarComprasPedidosEntregadas()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoOrdenesEntregadas", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Dui = dr["Dui"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]), // OBTENER FECHA REGISTRADA
                                // REALIZAR CONVERSION PARA MOSTRAR EN MIS COMPRAS -> TIENDA EN LINEA CLIENTES
                                ConversionFechaVenta = Convert.ToDateTime(dr["FechaVenta"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                MontoTotal = Convert.ToDouble(dr["MontoTotal"], new CultureInfo("es-SV")),
                                EstadoPedido = dr["EstadoPedido"].ToString(),
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Venta>();
            }
            return lista;
        }

        // MODIFICAR ESTADO PEDIDOS CLIENTES
        public bool EditarEstadoPedido(Venta obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarEstadoPedidosClientes", oconexion);
                    cmd.Parameters.AddWithValue("IdVenta", obj.IdVenta);
                    cmd.Parameters.AddWithValue("EstadoPedido", obj.EstadoPedido);
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
    }
}

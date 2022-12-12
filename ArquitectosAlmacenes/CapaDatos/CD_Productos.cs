
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
// PARA REFERENCIAR EL TIPO DE MONEDA DE CADA PAIS
using System.Globalization;

namespace CapaDatos
{
    public class CD_Productos
    {

        // LISTAR TODOS LOS PRODUCTOS REGISTRADOS [SIN FILTROS]
        public List<Productos> BuscadorProductos(Productos obj, string ValorBusqueda)
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_buscadorproductos", oconexion);
                    cmd.Parameters.AddWithValue("ValorBusqueda", ValorBusqueda);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();

            }
            return lista;
        }// CIERRE public List<Productos> BuscadorProductos()



        // LISTAR TODOS LOS PRODUCTOS REGISTRADOS [SIN FILTROS]
        public List<Productos> Listar()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoCompletoProductosRegistrados", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> Listar()

        // LISTADO DE PRODUCTOS REGISTRADOS [INICIO TIENDA EN LINEA]
        // IMPORTANTE -> ESTE LISTADO SE ENCUENTRA LIMITADO A 30 ELEMENTOS, ASI COMO SU ORDENAMIENTO POR FECHA DE REGISTRO.
        public List<Productos> ListarProductosInicioClientes()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoCompletoProductosRegistrados_InicioTiendaEnLinea", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosInicioClientes()


        /*
            -> LISTADO DE PRODUCTOS POR CATEGORIA ASOCIADA
        */

        // LISTADO DE PRODUCTOS CATEGORIA JOYERIA
        public List<Productos> ListarProductosCategoriaJoyeria()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoProductos_CategoriaJoyeria", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosCategoriaJoyeria()

        // LISTADO DE PRODUCTOS CATEGORIA RELOJERIA
        public List<Productos> ListarProductosCategoriaRelojeria()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoProductos_CategoriaRelojeria", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosCategoriaRelojeria()

        // LISTADO DE PRODUCTOS CATEGORIA CALZADO
        public List<Productos> ListarProductosCategoriaCalzado()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoProductos_CategoriaCalzado", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosCategoriaCalzado()

        // LISTADO DE PRODUCTOS CATEGORIA TECNOLOGIA
        public List<Productos> ListarProductosCategoriaTecnologia()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoProductos_CategoriaTecnologia", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosCategoriaTecnologia()

        // LISTADO DE PRODUCTOS CATEGORIA LINEA BLANCA
        public List<Productos> ListarProductosCategoriaLineaBlanca()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoProductos_CategoriaLineaBlanca", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosCategoriaLineaBlanca()

        // LISTADO DE PRODUCTOS CATEGORIA ELECTRODOMESTICOS
        public List<Productos> ListarProductosCategoriaElectrodomesticos()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoProductos_CategoriaElectrodomesticos", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosCategoriaElectrodomesticos()

        // LISTADO DE PRODUCTOS CATEGORIA BELLEZA
        public List<Productos> ListarProductosCategoriaBelleza()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoProductos_CategoriaBelleza", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosCategoriaBelleza()

        // LISTADO DE PRODUCTOS CATEGORIA JUGUETES
        public List<Productos> ListarProductosCategoriaJuguetes()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoProductos_CategoriaJuguetes", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosCategoriaJuguetes()

        // LISTAR TODOS LOS PRODUCTOS REGISTRADOS SIN STOCK DISPONIBLE
        public List<Productos> ListarProductosSinStock()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoCompletoProductosRegistradosSinStock", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Productos()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Descripcion = dr["DescripcionProducto"].ToString(),
                                        oMarcas = new Marcas() { IdMarcas = Convert.ToInt32(dr["IdMarcas"]), Descripcion = dr["Descripcion"].ToString() },
                                        oCategorias = new Categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), CodigoCategoria = dr["CodigoCategoria"].ToString() },
                                        oGarantias = new Garantias() { IdControlGarantias = Convert.ToInt32(dr["IdControlGarantias"]), DuracionDiasGarantia = Convert.ToInt32(dr["DuracionDiasGarantia"]) },
                                        Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        //ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Productos>();
            }
            return lista;
        }// CIERRE public List<Productos> ListarProductosSinStock()

        // REGISTRO DE NUEVOS PRODUCTOS
        public int Registrar(Productos obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProductos", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarcas", obj.oMarcas.IdMarcas);
                    cmd.Parameters.AddWithValue("IdCategorias", obj.oCategorias.IdCategorias);
                    cmd.Parameters.AddWithValue("IdControlGarantias", obj.oGarantias.IdControlGarantias);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    //cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
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

        // EDITAR PRODUCTOS
        public bool Editar(Productos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarcas", obj.oMarcas.IdMarcas);
                    cmd.Parameters.AddWithValue("IdCategorias", obj.oCategorias.IdCategorias);
                    cmd.Parameters.AddWithValue("IdControlGarantias", obj.oGarantias.IdControlGarantias);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
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

        // GUARDAR FOTOS DE PRODUCTOS
        public bool GuardarFotosProductos(Productos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_GuardarImagenProductos", oconexion);
                    cmd.Parameters.AddWithValue("NombreImagen", obj.NombreImagenProductos);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
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

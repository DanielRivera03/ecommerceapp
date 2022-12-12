
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
    public class CD_CarritoCompras
    {
        // VERIFICAR EXISTENCIAS DE PRODUCTOS AL REGISTRARLOS EN EL CARRITO DE COMPRAS

        public bool ExisteCarrito(int idusuario, int idproducto)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ComprobarExistencias_CarritoCompras", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idusuario);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
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


        // VERIFICAR STOCK DE PRODUCTOS AL REGISTRARLOS EN EL CARRITO DE COMPRAS

        public bool VerificarStockProductosCarrito(int idproducto)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ComprobarStockProductos_CarritoCompras", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
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


        // PROCESAR TODOS LOS PRODUCTOS EN EL CARRITO DE COMPRAS [AGREGAR EN CARRITO DE COMPRAS]

        public bool OperacionCarrito(int idusuario, int idproducto, bool sumar, out string Mensaje)
        {
            bool resultado = true;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistroProductos_CarritoCompras", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idusuario);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.AddWithValue("Sumar", sumar);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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

        // DEVOLVER TODOS LOS PRODUCTOS EN EL CARRITO DE COMPRAS CLIENTES [CANTIDAD DE ARTICULOS AGREGADOS]
        public int CantidadEnCarrito(int idusuario)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoProductosCarritoComprasClientes_CantidadArticulos", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idusuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return resultado;

        }

        // DEVOLVER TODOS LOS PRODUCTOS EN EL CARRITO DE COMPRAS CLIENTES [TOTAL A CANCELAR]
        public double CantidadEnCarrito_TotalCancelar(int idusuario)
        {
            double resultado = 0;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoProductosCarritoComprasClientes_TotalCancelar", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idusuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToDouble(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return resultado;

        }

        // ELIMINAR PRODUCTOS REGISTRADOS EN CARRITO DE COMPRAS
        public bool EliminarCarrito(int idusuario, int idproducto)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProductos_CarritoCompras", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idusuario);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
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


        // LISTADO DE PRODUCTOS EN EL CARRITO DE COMPRAS CLIENTES
        public List<CarritoCompras> ListarProducto(int idusuario)
        {
            List<CarritoCompras> lista = new List<CarritoCompras>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListadoArticulosCompra_CarritoComprasClientes", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idusuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new CarritoCompras()
                            {
                                oProducto = new Productos()
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Precio = Convert.ToDouble(dr["Precio"], new CultureInfo("es-SV")),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                    oMarcas = new Marcas() { Descripcion = dr["DesMarca"].ToString() }
                                },
                                Cantidad = Convert.ToInt32(dr["Cantidad"])
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<CarritoCompras>();
            }
            return lista;
        }


    }
}


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
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Text.RegularExpressions;

namespace CapaDatos
{
    public class CD_Marcas
    {

        // LISTADO COMPLETO DE TODAS LAS MARCAS DE PRODUCTOS REGISTRADOS
        public List<Marcas> Listar()
        {
            List<Marcas> lista = new List<Marcas>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoCompletoMarcasProductos", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Marcas()
                                    {
                                        IdMarcas = Convert.ToInt32(dr["IdMarcas"]),
                                        Descripcion = dr["Descripcion"].ToString(),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Marcas>();
            }
            return lista;
        }// CIERRE public List<Marcas> Listar()

        // LISTADO DE MARCAS POR CATEGORIAS
        public List<Marcas> ListarMarcaPorCategoria(int idcategoria)
        {
            List<Marcas> lista = new List<Marcas>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL MARCAS POR CATEGORIAS
                    SqlCommand cmd = new SqlCommand("sp_ListarMarcasPorCategorias", oconexion);
                    cmd.Parameters.AddWithValue("idcategoria", idcategoria);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(new Marcas()
                            {
                                IdMarcas = Convert.ToInt32(dr["IdMarcas"]),
                                Descripcion = dr["Descripcion"].ToString()
                            }
                            );

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Marcas>();
            }
            return lista;
        }


        // REGISTRO DE NUEVAS MARCAS DE PRODUCTOS
        public int Registrar(Marcas obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMarcas", oconexion);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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

        // EDITAR MARCAS DE PRODUCTOS
        public bool Editar(Marcas obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarMarcas", oconexion);
                    cmd.Parameters.AddWithValue("IdMarcas", obj.IdMarcas);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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

        // VERIFICAR SI NOMBRE DE MARCA SE ENCUENTRA REGISTRADO EN BASE DE DATOS
        public bool VerificarMarcasProductos(string NombreMarca)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_VerificarNombreMarca", oconexion);
                    cmd.Parameters.AddWithValue("Descripcion", NombreMarca);
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

    }
}

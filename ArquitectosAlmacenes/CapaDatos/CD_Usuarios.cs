
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
    public class CD_Usuarios
    {
        string Patron = "xmaVwSv5z87580fR^52";
        // LISTADO COMPLETO DE TODOS LOS USUARIOS Y CLIENTES REGISTRADOS
        public List<Usuarios> Listar()
        {
            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoUsuariosRegistrados", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Usuarios()
                                    {
                                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                        Nombres = dr["Nombres"].ToString(),
                                        Apellidos = dr["Apellidos"].ToString(),
                                        Correo = dr["Correo"].ToString(),
                                        Usuario = dr["Usuario"].ToString(),
                                        //NombreRolUsuario = dr["NombreRolUsuario"].ToString(), -> PUEDEN PROBAR DESCOMENTANDO ESTO Y COMENTANDO LA SOLUCION
                                        // POR FAVOR CONVERTIR A UN OBJETO LO QUE SE QUIERA MOSTRAR AL USUARIO FINAL CUANDO DE TENGA 1 O MAS FK DE LA TABLA PRINCIPAL
                                        // CASO CONTRARIO LA TABLA DARA ERRORES.
                                        oRoles = new Roles() { IdRolUsuario = Convert.ToInt32(dr["IdRolUsuario"]), NombreRolUsuario = dr["NombreRolUsuario"].ToString() },
                                        Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        Fotoperfil = dr["Fotoperfil"].ToString(),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Usuarios>();
            }
            return lista;
        }// CIERRE public List<Usuarios> Listar()

        // LISTADO COMPLETO DE SOLAMENTE CLIENTES REGISTRADOS
        public List<Usuarios> ListarClientes()
        {
            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoClientesRegistrados", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Usuarios()
                                    {
                                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                        Nombres = dr["Nombres"].ToString(),
                                        Apellidos = dr["Apellidos"].ToString(),
                                        Correo = dr["Correo"].ToString(),
                                        Usuario = dr["Usuario"].ToString(),
                                        oRoles = new Roles() { IdRolUsuario = Convert.ToInt32(dr["IdRolUsuario"]), NombreRolUsuario = dr["NombreRolUsuario"].ToString() },
                                        Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        Fotoperfil = dr["Fotoperfil"].ToString(),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Usuarios>();
            }
            return lista;
        }// CIERRE public List<Usuarios> ListarClientes()

        // LISTADO COMPLETO DE SOLAMENTE CLIENTES REGISTRADOS
        public List<Usuarios> ListarEmpleados()
        {
            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // EJECUTA SENTENCIA SQL USUARIOS
                    SqlCommand cmd = new SqlCommand("sp_ConsultarListadoEmpleadosRegistrados", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                            lista.Add(
                                    new Usuarios()
                                    {
                                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                        Nombres = dr["Nombres"].ToString(),
                                        Apellidos = dr["Apellidos"].ToString(),
                                        Correo = dr["Correo"].ToString(),
                                        Usuario = dr["Usuario"].ToString(),
                                        oRoles = new Roles() { IdRolUsuario = Convert.ToInt32(dr["IdRolUsuario"]), NombreRolUsuario = dr["NombreRolUsuario"].ToString() },
                                        Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                        Activo = Convert.ToBoolean(dr["Activo"]),
                                        Fotoperfil = dr["Fotoperfil"].ToString(),
                                        // REALIZAR CONVERSION PARA MOSTRAR EN PORTAL DE ADMINISTRACION
                                        ConversionFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"], CultureInfo.CurrentCulture).ToString("dd-MM-yyyy HH:mm:ss"),
                                    }
                                );
                        }
                    }
                }// CIERRE using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Usuarios>();
            }
            return lista;
        }// CIERRE public List<Usuarios> ListarEmpleados()

        // REGISTRO DE NUEVOS USUARIOS Y CLIENTES
        public int Registrar(Usuarios obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Contrasenia", obj.Contrasenia);
                    cmd.Parameters.AddWithValue("Usuario", obj.Usuario);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("IdRolUsuario", obj.oRoles.IdRolUsuario);
                    cmd.Parameters.AddWithValue("Patron", Patron);
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

        // REGISTRO DE NUEVOS CLIENTES [DESDE TIENDA EN LINEA]
        public int RegistrarNuevosClientes(Usuarios obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Contrasenia", obj.Contrasenia);
                    cmd.Parameters.AddWithValue("Usuario", obj.Usuario);
                    cmd.Parameters.AddWithValue("Activo", 1); // TODOS LOS CLIENTES POR DEFECTO ACTIVOS
                    cmd.Parameters.AddWithValue("IdRolUsuario", 5); // ROL POR DEFECTO 5 -> CLIENTES
                    cmd.Parameters.AddWithValue("Patron", Patron);
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

        // MODIFICAR USUARIOS
        public bool Editar(Usuarios obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Usuario", obj.Usuario);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("IdRolUsuario", obj.oRoles.IdRolUsuario);
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

        // MODIFICAR PERFIL DE USUARIOS
        public bool EditarPerfilUsuarios(Usuarios obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarPerfilUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Contrasenia", obj.Contrasenia);
                    cmd.Parameters.AddWithValue("Patron", Patron);
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

        // CAMBIAR FOTO DE PERFIL USUARIOS
        public bool GuardarFotoPerfilUsuarios(Usuarios obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_GuardarImagenPerfilUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("Fotoperfil", obj.NombreImagenPerfilUsuarios);
                    cmd.Parameters.AddWithValue("IdUsuarios", obj.IdUsuario);
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

        // REESTABLECER CONTRASEÑAS USUARIOS [RECUPERACION DE CUENTAS]
        public bool ReestablecerClave(int IdUsuario, string contrasenia, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReestablecerContraseniaUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.Parameters.AddWithValue("@contrasenia", contrasenia);
                    cmd.Parameters.AddWithValue("Patron", Patron);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;

        }

        // CAMBIAR CONTRASEÑAS USUARIOS
        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CambioContraseniaUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", idusuario);
                    cmd.Parameters.AddWithValue("@contrasenia", nuevaclave);
                    cmd.Parameters.AddWithValue("Patron", Patron);
                    cmd.Parameters.AddWithValue("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;

        }

        // VERIFICAR SI CORREO REGISTRADO EXISTE EN BASE DE DATOS
        public bool VerificarCorreoExistencias(string Correo)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_VerificarCorreoUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("Correo", Correo);
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

        // VERIFICAR SI USUARIO UNICO REGISTRADO EXISTE EN BASE DE DATOS
        public bool VerificarUsuarioUnicoExistencias(string UsuarioUnico)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_VerificarUsuarioUnico", oconexion);
                    cmd.Parameters.AddWithValue("Usuario", UsuarioUnico);
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

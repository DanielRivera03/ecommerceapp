
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
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.Web.Security;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;
// INTEGRACION PASARELA DE PAGOS PAYPAL
using CapaEntidad.Paypal;
// REFERENCIA A CLASE DE CONTROL DE SESIONES USUARIOS
using ArquitectosAlmacenes.Filter;
using System.Net;

namespace ArquitectosAlmacenes.Controllers
{
    public class HomeController : Controller
    {

        /*
            -> LLAMADOS WEBSERVICE
            [ENTORNO DE PRUEBAS Y PRODUCCION DISPONIBLES]
        */

        // INSTANCIAS PRINCIPALES PARA SERVICIO WCF
        DataSet dataset = new DataSet(); // REPOSITORIO UNIVERSAL DE DATOS
        // -> LLAMADO WCF EN PRODUCCION
        //WCFArquitectosAlmacenes.xxxxx resultado = new WCFArquitectosAlmacenes.xxxxx(); // INSTANCIA DE SERVICIO WCF
        // -> LLAMADO WCF PRUEBAS LOCALES
        WCFArquitectosAlmacenes.ServiceClient resultado = new WCFArquitectosAlmacenes.ServiceClient(); // INSTANCIA DE SERVICIO WCF

        // INICIO GENERAL DE TODA LA PLATAFORMA -> TIENDA EN LINEA
        public ActionResult Index()
        {
            return View();
        }
        // PERFIL DE CLIENTES
        /*
            -> PERFIL DE CLIENTES NO TIENEN LA POSIBILIDAD DE CAMBIAR FOTO DE PERFIL. EN SU DEFECTO SE REGISTRAN CON FOTO DE PERFIL POR DEFECTO
         */
        [ValidarSession]
        [Authorize]
        public ActionResult PerfilClientes(int idusuario = 0)
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 5)
            {
                idusuario = (int)Convert.ToUInt32(Session["IdUsuario"]); // CONVERTIR EXPLICITAMENTE OBJETO DE SESION A ENTERO
                Usuarios oUsuario = new Usuarios();
                oUsuario = new CN_Usuarios().Listar().Where(p => p.IdUsuario == idusuario).FirstOrDefault();
                return View(oUsuario);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [ValidarSession]
        [Authorize]
        // EDITAR PERFIL USUARIOS ADMINISTRATIVOS
        [HttpPost]
        public ActionResult EditarMiPerfilClientes(Usuarios objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdUsuario != 0)
            {

                resultado = new CN_Usuarios().EditarPerfilUsuarios(objeto, out mensaje);
            }
            else
            {
                resultado = 0;
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        // TERMINOS Y CONDICIONES DE USO -> ARQUITECTOS ALMACENES S.A DE C.V
        public ActionResult TerminosCondiciones()
        {
            return View();
        }
        // LOGIN PRINCIPAL VALIDO PARA TODOS LOS USUARIOS Y CLIENTES DE ESTA PLATAFORMA
        public ActionResult IniciarSesion()
        {
            return View();
        }
        // RECUPERACION DE CUENTAS USUARIOS Y CLIENTES
        public ActionResult RecuperacionCuentasUsuarios()
        {
            return View();
        }
        // CAMBIO DE CONTRASEÑAS NUEVOS USUARIOS Y PROCESO DE RECUPERACION DE CUENTAS
        [ValidarSession]
        [Authorize]
        public ActionResult CambioCredencialesAcceso()
        {
            return View();
        }
        // REGISTRO DE NUEVOS CLIENTES
        public ActionResult RegistroNuevosClientes()
        {
            return View();
        }
        // F.A.Q SECCION DE AYUDA TIENDA EN LINEA
        public ActionResult FAQ()
        {
            return View();
        }
        [HttpPost]
        // VALIDAR INICIO DE SESION [CONSULTA DE CREDENCIALES INGRESADAS]
        public ActionResult IniciarSesion(string usuario, string contrasenia)
        {
            try
            {
                // OBTENER DATOS DE USUARIO
                Usuarios ousuario = new Usuarios();
                dataset = resultado.ValidarCredencialesUsuarios(usuario, contrasenia);
                // VALIDACION CAMPOS VACIOS
                if(usuario == "")
                {
                    ViewBag.Error = "Lo sentimos, debe ingresar su usuario y/o correo";
                }else if (contrasenia == "")
                {
                    ViewBag.Error = "Lo sentimos, debe ingresar su credencial de acceso";
                }
                // VALIDACIONES ESPECIFICAS DE PROCEDIMIENTOS
                else
                {
                    if (dataset != null) // VALIDACION SI SE EJECUTA SERVICIO
                    {
                        if (dataset.Tables.Count > 0) // VALIDACION SI OBTIENE DATOS DESEADOS
                        {
                            if (dataset.Tables["Usuarios"].Rows.Count > 0) // SI OBTIENE AL MENOS UN REGISTRO
                            {
                                if (dataset.Tables["Usuarios"].Rows[0]["IdUsuario"].ToString() == "-1")
                                {
                                    ViewBag.Error = "Lo sentimos, las credenciales ingresadas no son válidas. Intente otra vez...";
                                }
                                else if (dataset.Tables["Usuarios"].Rows[0]["Activo"].ToString() == "False")
                                {
                                    ViewBag.Error = "Lo sentimos, este usuario se encuentra inactivo.";
                                }
                                else
                                {
                                    // ** CREACION DE VARIABLES DE SESION
                                    // ID UNICO DE USUARIOS
                                    Session["IdUsuario"] = dataset.Tables["Usuarios"].Rows[0]["IdUsuario"].ToString();
                                    // CODIGO USUARIO UNICO DE USUARIOS
                                    Session["UsuarioUnico"] = dataset.Tables["Usuarios"].Rows[0]["Usuario"].ToString();
                                    // NOMBRES DE USUARIOS
                                    Session["Nombres"] = dataset.Tables["Usuarios"].Rows[0]["Nombres"].ToString();
                                    // APELLIDOS DE USUARIOS
                                    Session["Apellidos"] = dataset.Tables["Usuarios"].Rows[0]["Apellidos"].ToString();
                                    // ROL DE USUARIOS [PRIVILEGIOS]
                                    Session["Privilegio"] = dataset.Tables["Usuarios"].Rows[0]["IdRolUsuario"].ToString();
                                    // CORREO DE USUARIOS
                                    Session["Correo"] = dataset.Tables["Usuarios"].Rows[0]["Correo"].ToString();
                                    // USUARIO ACTIVO / INACTIVO -> CONTROL
                                    Session["Activo"] = dataset.Tables["Usuarios"].Rows[0]["Activo"].ToString();
                                    // FOTO PERFIL USUARIOS
                                    Session["FotoPerfil"] = dataset.Tables["Usuarios"].Rows[0]["Fotoperfil"].ToString();
                                    // CREACION DE COOKIES DE USUARIOS
                                    /*
                                        -> COMPROBAR SI USUARIO DESEA RECORDAR SUS DATOS DE ACCESO
                                        
                                    */
                                    string ComprobarEstado_RecordarUsuario = Request["recordarusuario"];
                                    if(ComprobarEstado_RecordarUsuario == "on")
                                    {
                                        // -> USUARIO UNICO
                                        HttpCookie GuardarUsuarioUnico = new HttpCookie("Usuario", Session["UsuarioUnico"].ToString());
                                        Response.Cookies.Add(GuardarUsuarioUnico);
                                        GuardarUsuarioUnico.Expires = DateTime.Now.AddDays(30);
                                        // -> CORREO
                                        HttpCookie GuardarCorreoUnico = new HttpCookie("Correo", Session["Correo"].ToString());
                                        Response.Cookies.Add(GuardarCorreoUnico);
                                        GuardarCorreoUnico.Expires = DateTime.Now.AddDays(30);
                                    }
                                    // CONTROL DE VERIFICACION NUEVOS USUARIOS Y RECUPERACION DE CUENTAS
                                    /*
                                        -> SI EL VALOR ES 1 [UNO] = NUEVO USUARIO; O BIEN, INICIO PROCESO DE RECUPERACION DE CUENTAS
                                     */
                                    bool ValidarCambioCredendial = Convert.ToBoolean(dataset.Tables["Usuarios"].Rows[0]["Reestablecer"].ToString());
                                    // CONTROL DE COMPROBACION SI ES NUEVO USUARIO, O BIEN A INICIADO PROCESO DE RECUPERACION DE CUENTAS
                                    Session["ComprobarRecuperacion"] = ValidarCambioCredendial; //  -> SI ES TRUE - BLOQUEO TOTAL DE PLATAFORMA HASTA QUE REALICE ESE CAMBIO
                                    // VALIDACION SEGUN ROL DE USUARIO ASIGNADO
                                    // OBTENER EL ID DE USUARIO QUE HA INICIADO SESION, PARA VALIDAR ACCESOS DENTRO DEL SISTEMA PARA EL CONTROL DE SESIONES
                                    FormsAuthentication.SetAuthCookie(Session["IdUsuario"].ToString(), false);
                                    if (ValidarCambioCredendial)
                                    {
                                        TempData["IdUsuario"] = Session["IdUsuario"];
                                        return RedirectToAction("CambioCredencialesAcceso", "Home");
                                    }
                                    else // SI NO ES USUARIO NUEVO, O BIEN -> NO INICIO PROCESO DE RECUPERACION. INGRESA SEGUN ROL DE USUARIO ASIGNADO
                                    {
                                        /*
                                           -> ADMINISTRADORES 
                                       */
                                        if (Session["Privilegio"].ToString() == "1")
                                        {
                                            return RedirectToAction("InicioAdministradores", "Administracion");
                                        }
                                        /*
                                            -> PRESIDENCIA 
                                        */
                                        if (Session["Privilegio"].ToString() == "2")
                                        {
                                            return RedirectToAction("InicioPresidencia", "Administracion");
                                        }
                                        /*
                                            -> GERENCIA 
                                        */
                                        if (Session["Privilegio"].ToString() == "3")
                                        {
                                            return RedirectToAction("InicioGerencia", "Administracion");
                                        }
                                        /*
                                            -> ATENCION AL CLIENTE 
                                        */
                                        if (Session["Privilegio"].ToString() == "4")
                                        {
                                            return RedirectToAction("InicioAtencionClientes", "Administracion");
                                        }
                                        /*
                                            -> CLIENTES 
                                        */
                                        if (Session["Privilegio"].ToString() == "5")
                                        {
                                            return RedirectToAction("Index", "Home");
                                        }
                                    }// CIERRE if (ValidarCambioCredendial)

                                }
                            }// CIERRE if (dataset.Tables["Usuarios"].Rows.Count > 0)
                            else
                            {
                                ViewBag.Error = "Lo sentimos, no fue posible encontrar registros";
                            }
                        }
                        else
                        {
                            ViewBag.Error = "Lo sentimos, no fue posible encontrar datos";
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Lo sentimos, no se pudo ejecutar procedimiento";
                    }
                }
                
            }// CIERRE try
            catch (Exception ex)
            {
                ViewBag.Error = "ERROR " + ex.Message;
            }
            return View();
        }

        [ValidarSession]
        [Authorize]
        // CAMBIAR CLAVES DE ACCESO [USUARIOS NUEVOS Y RECUPERACION DE CUENTAS]
        [HttpPost]
        public ActionResult CambioCredencialesAcceso(string idusuario, string nuevaclave, string confirmarclave, string correo)
        {
            Usuarios oUsuario = new Usuarios();
            oUsuario = new CN_Usuarios().Listar().Where(usuarios => usuarios.IdUsuario == int.Parse(idusuario)).FirstOrDefault();
            // VALIDACION DE CAMPOS VACIOS
            if(nuevaclave == "" || confirmarclave == "")
            {
                TempData["IdUsuario"] = idusuario;
                TempData["Correo"] = correo;
                ViewBag.Error = "Lo sentimos, nueva clave y confirmación de clave son obligatorios";
                return View();
            }
            // VERIFICAR SI CLAVE NUEVA ES IGUAL A LA CLAVE DE REPLICACION
            if (nuevaclave != confirmarclave)
            {
                TempData["IdUsuario"] = idusuario;
                TempData["Correo"] = correo;
                ViewBag.Error = "Lo sentimos, las contraseñas ingresadas no son iguales";
                return View();
            }
            // REALIZAR EJECUCION DE NUEVA CLAVE USUARIOS
            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idusuario), nuevaclave, correo, out mensaje);
            if (respuesta)
            {
                ViewBag.Error = null;
                ViewBag.Success = "Su cuenta ha sido recuperada con exito. Por favor inicie sesion nuevamente...";
                return View();
                //return RedirectToAction("IniciarSesion"); // RETORNAR A INICIAR SESION NUEVAMENTE
            }
            else
            {
                TempData["IdUsuario"] = idusuario;
                TempData["Correo"] = correo;
                ViewBag.Error = mensaje;
                return View();
            }// CIERRE if (respuesta)
        }

        // REESTABLECER CLAVES DE ACCESO -> RECUPERACION DE CUENTAS
        [HttpPost]
        public ActionResult RecuperacionCuentasUsuarios(string correo)
        {
            Usuarios ousuario = new Usuarios();
            ousuario = new CN_Usuarios().Listar().Where(usuario => usuario.Correo == correo).FirstOrDefault();
            if(correo == "")
            {
                ViewBag.Error = "Lo sentimos, para poder procesar su peticion, debe ingresar un correo...";
                return View();
            }
            else if (ousuario == null)
            {
                ViewBag.Error = "Lo sentimos, no se ha encontrado un usuario relacionado a ese correo";
                return View();
            }
            else
            {
                string mensaje = string.Empty;
                bool respuesta = new CN_Usuarios().ReestablecerClave(ousuario.IdUsuario, ousuario.Correo, out mensaje);
                if (respuesta)
                {
                    ViewBag.Error = null;
                    ViewBag.Success = "Por favor revise su correo y siga las instrucciones ahí descritas";
                    return View();
                    //return RedirectToAction("Index", "Acceso");
                }
                else
                {
                    ViewBag.Error = mensaje;
                    return View();
                }
            }
        }

        // REGISTRO DE NUEVOS CLIENTES
        [HttpPost]
        public ActionResult RegistroNuevosClientes(Usuarios objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdUsuario == 0)
            {

                resultado = new CN_Usuarios().RegistrarNuevosClientes(objeto, out mensaje);
            }
            else
            {
                resultado = 0;
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // LISTAR TODAS LAS CATEGORIAS
        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categorias> lista = new List<Categorias>();
            lista = new CN_Categoria().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

        }
        // LISTAR MARCAS POR CATEGORIA
        [HttpPost]
        public JsonResult ListaMarcaPorCategoria(int idcategoria)
        {
            List<Marcas> lista = new List<Marcas>();
            lista = new CN_Marcas().ListarMarcaPorCategoria(idcategoria);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

        }

        /*
            -> LISTADO DE PRODUCTOS SEGUN CATEGORIA ASOCIADA
                ESTAS DEBEN SER CREADAS MANUALMENTE, SEGUN NUEVOS REGISTROS DE CATEGORIAS
         */

        // PRODUCTOS CATEGORIA JOYERIA
        public ActionResult ProductosCategoriaJoyeria()
        {
            return View();
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN TIENDA -> CATEGORIA RELOJERIA
        [HttpPost]
        public JsonResult ListarProductosCategoriaJoyeria(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().ListarProductosCategoriaJoyeria().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }



        // PRODUCTOS CATEGORIA RELOJERIA
        public ActionResult ProductosCategoriaRelojeria()
        {
            return View();
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN TIENDA -> CATEGORIA RELOJERIA
        [HttpPost]
        public JsonResult ListarProductosCategoriaRelojeria(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().ListarProductosCategoriaRelojeria().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        // PRODUCTOS CATEGORIA CALZADO
        public ActionResult ProductosCategoriaCalzado()
        {
            return View();
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN TIENDA -> CATEGORIA CALZADO
        [HttpPost]
        public JsonResult ListarProductosCategoriaCalzado(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().ListarProductosCategoriaCalzado().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        // PRODUCTOS CATEGORIA TECNOLOGIA
        public ActionResult ProductosCategoriaTecnologia()
        {
            return View();
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN TIENDA -> CATEGORIA CALZADO
        [HttpPost]
        public JsonResult ListarProductosCategoriaTecnologia(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().ListarProductosCategoriaTecnologia().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        // PRODUCTOS CATEGORIA LINEA BLANCA
        public ActionResult ProductosCategoriaLineaBlanca()
        {
            return View();
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN TIENDA -> CATEGORIA LINEA BLANCA
        [HttpPost]
        public JsonResult ListarProductosCategoriaLineaBlanca(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().ListarProductosCategoriaLineaBlanca().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        // PRODUCTOS CATEGORIA LINEA BLANCA
        public ActionResult ProductosCategoriaElectrodomesticos()
        {
            return View();
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN TIENDA -> CATEGORIA LINEA BLANCA
        [HttpPost]
        public JsonResult ListarProductosCategoriaElectrodomesticos(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().ListarProductosCategoriaElectrodomesticos().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        // PRODUCTOS CATEGORIA BELLEZA
        public ActionResult ProductosCategoriaBelleza()
        {
            return View();
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN TIENDA -> CATEGORIA BELLEZA
        [HttpPost]
        public JsonResult ListarProductosCategoriaBelleza(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().ListarProductosCategoriaBelleza().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        // PRODUCTOS CATEGORIA JUGUETES
        public ActionResult ProductosCategoriaJuguetes()
        {
            return View();
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN TIENDA -> CATEGORIA JUGUETES
        [HttpPost]
        public JsonResult ListarProductosCategoriaJuguetes(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().ListarProductosCategoriaJuguetes().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN TIENDA
        [HttpPost]
        public JsonResult ListarProductos(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().Listar().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR EN INICIO DE TIENDA EN LINEA
        // -> LIMITADO A UN MAXIMO DE 30 ELEMENTOS A MOSTRAR
        [HttpPost]
        public JsonResult ListarProductosInicioClientes(int idcategoria, int idmarca)
        {
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().ListarProductosInicioClientes().Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.oCategorias.IdCategorias == (idcategoria == 0 ? p.oCategorias.IdCategorias : idcategoria) &&
                p.oMarcas.IdMarcas == (idmarca == 0 ? p.oMarcas.IdMarcas : idmarca) &&
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        // RECUPERACION DE CUENTAS USUARIOS Y CLIENTES
        public ActionResult ListarProductosBuscador()
        {
            return View();
        }

        // LISTAR TODOS LOS PRODUCTOS A MOSTRAR [BUSCADOR DE PRODUCTOS]
        [HttpGet]
        public ActionResult ListarProductosBuscador(Productos obj, string search)
        {
            // -> search = NOMBRE DEL INPUT QUE RECIBE LO QUE LOS USUARIOS DESEEN BUSCAR
            List<Productos> lista = new List<Productos>();
            lista = new CN_Productos().BuscadorProductos(obj, search).Select(p => new Productos()  // -> p = IDENTIFICADOR DE TABLA PRODUCTOS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarcas = p.oMarcas,
                oCategorias = p.oCategorias,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo
            }).Where(p =>
                p.Stock > 0 && p.Activo == true
                ).ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            //return jsonresult;
            return View(lista);
        }

        // VISTA DETALLES DE PRODUCTOS
        public ActionResult DetallesProductos(int idproducto = 0)
        {
            if(idproducto == 0)
            {
                return RedirectToAction("Index"); // RETORNAR A PAGINA PRINCIPAL DE TIENDA EN LINEA  
            }
            else
            {
                // SOLO PERMITIR VISTA SI EL PARAMETRO HA SIDO ENVIADO
                Productos oProducto = new Productos();
                oProducto = new CN_Productos().Listar().Where(p => p.IdProducto == idproducto).FirstOrDefault();
                return View(oProducto);
            }
            
        }
        [ValidarSession]
        [Authorize]
        // CARRITO DE COMPRAS [DETALLE COMPLETO DE PRODUCTOS AGREGADOS EN CARRITO DE COMPRAS]
        public ActionResult CarritoComprasClientes()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 5)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [ValidarSession]
        [Authorize]
        // AGREGAR PRODUCTOS AL CARRITO
        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {
            int idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
            bool existe = new CN_CarritoCompras().ExisteCarrito(idusuario, idproducto);
            bool ComprobarStock = new CN_CarritoCompras().VerificarStockProductosCarrito(idproducto);
            bool respuesta = false;
            string mensaje = string.Empty;
            if (existe)
            {
                mensaje = "Lo sentimos, el producto ya existe en el carrito de compras";
            }
            else if (ComprobarStock)
            {
                mensaje = "Lo sentimos, este producto ya no cuenta con stock suficiente";
            }
            else
            {
                respuesta = new CN_CarritoCompras().OperacionCarrito(idusuario, idproducto, true, out mensaje);
            }
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // GESTIONAR TODAS LAS OPERACIONES DE CARRITO DE COMPRAS CLIENTES
        [HttpPost]
        public JsonResult OperacionCarrito(int idproducto, bool sumar)
        {
            int idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_CarritoCompras().OperacionCarrito(idusuario, idproducto, sumar, out mensaje);
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // DEVOLVER CANTIDAD DE PRODUCTOS AGREGADOS AL CARRITO DE COMPRAS
        [HttpGet]
        public JsonResult CantidadEnCarrito()
        {
            int idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
            int cantidad = new CN_CarritoCompras().CantidadEnCarrito(idusuario);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // DEVOLVER CANTIDAD DE PRODUCTOS AGREGADOS AL CARRITO DE COMPRAS [TOTAL A CANCELAR -> SUMATORIA DE PRODUCTOS]
        [HttpGet]
        public JsonResult CantidadEnCarrito_TotalCancelar()
        {
            int idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
            double cantidad = new CN_CarritoCompras().CantidadEnCarrito_TotalCancelar(idusuario);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // LISTAR TODOS LOS PRODUCTOS AGREGADOS AL CARRITO DE COMPRAS CLIENTES [VISTA DETALLE CARRITO DE COMPRAS]
        [HttpPost]
        public JsonResult ListarProductosCarrito()
        {
            int idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
            List<CarritoCompras> oLista = new List<CarritoCompras>();
            oLista = new CN_CarritoCompras().ListarProducto(idusuario).Select(listacompra => new CarritoCompras()
            {
                oProducto = new Productos()
                {
                    IdProducto = listacompra.oProducto.IdProducto,
                    Nombre = listacompra.oProducto.Nombre,
                    oMarcas = listacompra.oProducto.oMarcas,
                    Precio = listacompra.oProducto.Precio,
                    NombreImagen = listacompra.oProducto.NombreImagen
                },
                Cantidad = listacompra.Cantidad
            }).ToList();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // ELIMINAR PRODUCTOS AGREGADOS EN CARRITO DE COMPRAS CLIENTES
        [HttpPost]
        public JsonResult EliminarCarrito(int idproducto)
        {
            int idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_CarritoCompras().EliminarCarrito(idusuario, idproducto);
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // OBTENER LISTADO DE DEPARTAMENTOS
        [HttpPost]
        public JsonResult ObtenerDepartamento()
        {
            List<Departamentos> oLista = new List<Departamentos>();
            oLista = new CN_Direcciones().ObtenerDepartamento();
            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }
        [ValidarSession]
        [Authorize]
        // OBTENER LISTADO DE MUNICIPIOS
        [HttpPost]
        public JsonResult ObtenerMunicipio(string iddepartamento)
        {
            //iddepartamento = "SV-SS";
            List<Municipios> oLista = new List<Municipios>();
            oLista = new CN_Direcciones().ObtenerMunicipio(iddepartamento);
            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // PROCESAR PAGO Y FINALIZAR VENTA DE PRODUCTOS CARRITO DE COMPRAS
        [HttpPost]
        public async Task<JsonResult> ProcesarPago(List<CarritoCompras> oListaCarrito, Venta oVenta)
        {
            double total = 0;
            DataTable detalle_venta = new DataTable();
            detalle_venta.Locale = new CultureInfo("es-SV");
            detalle_venta.Columns.Add("IdProducto", typeof(string));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("Total", typeof(decimal));
            List<Item> oListaItem = new List<Item>();
            foreach (CarritoCompras oCarrito in oListaCarrito)
            {
                double subtotal = Convert.ToDouble(oCarrito.Cantidad.ToString()) * oCarrito.oProducto.Precio;
                total += subtotal;
                oListaItem.Add(new Item()
                {
                    name = oCarrito.oProducto.Nombre,
                    quantity = oCarrito.Cantidad.ToString(),
                    unit_amount = new UnitAmount()
                    {
                        currency_code = "USD",
                        value = oCarrito.oProducto.Precio.ToString("G", new CultureInfo("es-SV"))
                    }
                });
                detalle_venta.Rows.Add(new object[]
                {
                    oCarrito.oProducto.IdProducto,
                    oCarrito.Cantidad,
                    subtotal
                });
            }
            PurchaseUnit purchaseUnit = new PurchaseUnit()
            {
                amount = new Amount()
                {
                    currency_code = "USD",
                    value = total.ToString("G", new CultureInfo("es-SV")),
                    breakdown = new Breakdown()
                    {
                        item_total = new ItemTotal()
                        {
                            currency_code = "USD",
                            value = total.ToString("G", new CultureInfo("es-SV")),
                        }
                    }
                },
                description = "Tienda En Linea - Arquitectos Almacenes S.A de C.V",
                items = oListaItem
            };

            Checkout_Order oCheckOutOrder = new Checkout_Order()
            {
                intent = "CAPTURE",
                purchase_units = new List<PurchaseUnit>() { purchaseUnit },
                application_context = new ApplicationContext()
                {
                    brand_name = "Arquitectos Almacenes S.A de C.V",
                    landing_page = "NO_PREFERENCE",
                    user_action = "PAY_NOW",

                    // -> xxxxx = CAMBIAR AL PUERTO DE SU SERVIDOR

                    // -> PRUEBAS LOCALES
                    return_url = "https://localhost:xxxxx/Home/EstadoPagoCompras",
                    cancel_url = "https://localhost:xxxxx/"
                    // -> PRODUCCION
                    //return_url = "https://xxxxxx/xxxxxx/xxxxxx/xxxxxx",
                    //cancel_url = "https://xxxxxx.xxxxxx/xxxxxx/"
                }
            };
            oVenta.MontoTotal = total;
            oVenta.IdUsuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
            TempData["Venta"] = oVenta;
            TempData["DetalleVenta"] = detalle_venta;
            // EJECUTAR LOS SERVICIOS DE PAYPAL
            CN_Paypal opaypal = new CN_Paypal();
            Response_Paypal<Response_Checkout> response_paypal = new Response_Paypal<Response_Checkout>();
            response_paypal = await opaypal.CrearSolicitud(oCheckOutOrder);
            return Json(response_paypal, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // CONFIRMACION DE COMPRA
        public async Task<ActionResult> EstadoPagoCompras()
        {
            string token = Request.QueryString["token"]; // OBTIENE ID DE TRANSACCION -> GET DE LA URL
            CN_Paypal opaypal = new CN_Paypal(); // EJECUTA CAPA DE NEGOCIO PAYPAL
            Response_Paypal<Response_Capture> response_paypal = new Response_Paypal<Response_Capture>();
            response_paypal = await opaypal.AprobarPago(token);
            bool status = Convert.ToBoolean(Request.QueryString["status"]);
            ViewData["Status"] = response_paypal.Status;
            if (response_paypal.Status)
            {
                Venta oVenta = (Venta)TempData["Venta"];
                DataTable detalle_venta = (DataTable)TempData["DetalleVenta"];
                oVenta.IdTransaccion = response_paypal.Response.purchase_units[0].payments.captures[0].id;
                // REGISTRO DE VENTA PROCESADA EN BASE DE DATOS
                string mensaje = string.Empty;
                bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);
                ViewData["IdTransaccion"] = oVenta.IdTransaccion;
            }
            return View();
        }

        [ValidarSession]
        [Authorize]
        // LISTADO GENERAL DE TODAS LAS COMPRAS PROCESADAS -> CLIENTES
        public ActionResult ListadoMisComprasProcesadas()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 5)
            {
                int idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasProcesadasClientes(idusuario).Select(listacompra => new Venta()
                {
                    IdVenta = listacompra.IdVenta,
                    IdUsuario = listacompra.IdUsuario,
                    TotalProducto = listacompra.TotalProducto,
                    MontoTotal = listacompra.MontoTotal,
                    IdTransaccion = listacompra.IdTransaccion,
                    FechaVenta = listacompra.FechaVenta,
                    ConversionFechaVenta = listacompra.ConversionFechaVenta,
                }).ToList();
                return View(oLista);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        [ValidarSession]
        [Authorize]
        // LISTADO ESPECIFICO DE TODAS LAS COMPRAS PROCESADAS -> VISTA DE DETALLES DE VENTAS PROCESADAS CLIENTES
        public ActionResult ListadoDetallesMisComprasProcesadas(int idventa = 0)
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 5)
            {
                int idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
                // LISTAR VENTA PROCESADA
                Venta oVentas = new Venta();
                oVentas = new CN_Venta().ListarComprasProcesadasClientes(idventa).Where(v => v.IdVenta == idventa).FirstOrDefault();
                // LISTAR DETALLE DE VENTA PROCESADA -> ASOCIADA A VENTA PROCESADA
                List<DetalleVenta> oLista = new List<DetalleVenta>();
                oLista = new CN_Venta().ListarDetallesComprasProcesadasClientes(idusuario, idventa).Select(listacompra => new DetalleVenta()
                {
                    IdVenta = listacompra.IdVenta,
                    IdUsuario = listacompra.IdUsuario,
                    /*
                        -> OBJETOS SEGUN LAS CLASES CREADAS EN LA CAPA ENTIDAD DE CADA UNO DE LOS ELEMENTOS QUE SE HAN LLAMADO ACA 
                    */
                    // MARCAS DE PRODUCTOS
                    oMarcas = new Marcas()
                    {
                        Descripcion = listacompra.oMarcas.Descripcion

                    },
                    // PRODUCTOS
                    oProducto = new Productos()
                    {
                        Nombre = listacompra.oProducto.Nombre,
                        Descripcion = listacompra.oProducto.Descripcion,
                        NombreImagen = listacompra.oProducto.NombreImagen,
                        Precio = listacompra.oProducto.Precio,

                    },
                    // GARAMTIAS DE PRODUCTOS
                    oGarantias = new Garantias()
                    {
                        DuracionDiasGarantia = listacompra.oGarantias.DuracionDiasGarantia

                    },
                    IdTransaccion = listacompra.IdTransaccion,
                    Cantidad = listacompra.Cantidad,
                    Total = listacompra.Total,
                    FechaVenta = listacompra.FechaVenta,
                    // CONVERSION DE FECHA A STRING -> PARA MOSTRAR A USUARIOS EN FORMATO DD/MM/YYYY - HH:MM:SS
                    ConversionFechaVenta = listacompra.ConversionFechaVenta,
                    ContadorDiasGarantias = listacompra.ContadorDiasGarantias
                }).ToList();
                return View(oLista);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [ValidarSession]
        [Authorize]
        // REGISTRO PROBLEMAS PEDIDOS CLIENTES
        /*
            -> IMPORTANTE: UNICAMENTE SE PODRAN REPORTAR PEDIDOS, LOS CUALES NO EXCEDAN SIETE DIAS DESPUES DE PROCESADA LA VENTA.
            PEDIDOS QUE EXCEDAN ESA CONDICION, NO SON POSIBLES DE REGISTRAR EN EL SISTEMA
        */
        public ActionResult ReporteProblemasPedidosClientes()
        {
            int idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
            TempData["IdUsuario"] = idusuario;
            return View();
        }

        [ValidarSession]
        [Authorize]
        // OBTENER LISTADO DE VENTAS PROCESADAS POR CLIENTE -> CUMPLIENDO LA CONDICION ANTES MENCIONADA
        [HttpPost]
        public JsonResult ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas(int idusuario)
        {
            idusuario = Convert.ToInt32(Session["IdUsuario"]); // CONVERTIR LA VARIABLE DE SESION EN UN OBJETO ENTERO
            List<Venta> oLista = new List<Venta>();
            oLista = new CN_Venta().ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas(idusuario);
            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // REGISTRO REPORTES PROBLEMAS PEDIDOS / COMPRAS CLIENTES
        [HttpPost]
        public ActionResult RegistroProblemasVentasClientes(ProblemasVentas objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdReporteProblema == 0)
            {

                resultado = new CN_Venta().RegistrarProblemasPedidos(objeto, out mensaje);
            }
            else
            {
                resultado = 0;
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        // CERRAR SESIONES DE USUARIOS
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index"); // SE RETORNA A PAGINA DE VISTA TIENDA EN LINEA [HOME PRINCIPAL]
        }

        // REDIRECCIONAMIENTO SESION EXPIRADA USUARIOS
        public ActionResult SesionExpiradaUsuarios()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("IniciarSesion"); // SE RETORNA A PAGINA DE VISTA TIENDA EN LINEA [INICIAR SESION]
        }


    }// CIERRE public class HomeController : Controller
}
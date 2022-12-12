
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
using System.Configuration;
// PARA SABER EL TIPO DE MONEDA DE CADA PAIS
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
// REFERENCIA A CLASE DE CONTROL DE SESIONES USUARIOS
using ArquitectosAlmacenes.Filter;


namespace ArquitectosAlmacenes.Controllers
{
    public class AdministracionController : Controller
    {
        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE ADMINISTRACION USUARIOS ADMINISTRADORES [EN CONSTRUCCION]
        public ActionResult InicioAdministradores()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) >= 2 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VER DATOS REPORTES DASHBOARD ADMINISTRADORES [CARTAS DE PRESENTACION]
        [HttpGet]
        public JsonResult VistaDashBoard()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                ReporteDashboardAdministradores objeto = new CN_Reportes().VerDashBoard();
                return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ReporteDashboardAdministradores objeto = new CN_Reportes().VerDashBoard();
                return Json(new { resultado = 0 }, JsonRequestBehavior.AllowGet);
            }
               
        }

        [ValidarSession]
        [Authorize]
        // VER DATOS REPORTES DASHBOARD PRESIDENCIA [CARTAS DE PRESENTACION]
        [HttpGet]
        public JsonResult VistaDashBoardPresidencia()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 2)
            {
                ReporteDashboardPresidencia objeto = new CN_Reportes().VerDashBoardPresidencia();
                return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ReporteDashboardPresidencia objeto = new CN_Reportes().VerDashBoardPresidencia();
                return Json(new { resultado = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        [ValidarSession]
        [Authorize]
        // VER DATOS REPORTES DASHBOARD GERENCIA [CARTAS DE PRESENTACION]
        [HttpGet]
        public JsonResult VistaDashBoardGerencia()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 3)
            {
                ReporteDashboardGerencia objeto = new CN_Reportes().VerDashBoardGerencia();
                return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ReporteDashboardGerencia objeto = new CN_Reportes().VerDashBoardGerencia();
                return Json(new { resultado = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE ADMINISTRACION USUARIOS PRESIDENCIA [EN CONSTRUCCION]
        public ActionResult InicioPresidencia()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 2)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) == 1 || Convert.ToUInt32(Session["Privilegio"]) >=3 && Convert.ToUInt32(Session["Privilegio"]) <=4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE ADMINISTRACION USUARIOS GERENCIA [EN CONSTRUCCION]
        public ActionResult InicioGerencia()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 3)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) == 1 || Convert.ToUInt32(Session["Privilegio"]) == 2 || Convert.ToUInt32(Session["Privilegio"]) == 4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE ADMINISTRACION USUARIOS ATENCION AL CLIENTE
        public ActionResult InicioAtencionClientes()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 4)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) == 1 || Convert.ToUInt32(Session["Privilegio"]) == 2 && Convert.ToUInt32(Session["Privilegio"]) <= 3)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // RETORNO VISTA PRINCIPAL -> VISTAS NO AUTORIZADAS SEGUN ROLES DE USUARIO
        public ActionResult RetornoSistemaArquitectosAlmacenes()
        {
            if (Session["Privilegio"].ToString() == "1")
            {
                return RedirectToAction("InicioAdministradores", "Administracion");
            }
            /*
                -> PRESIDENCIA 
            */
            else if (Session["Privilegio"].ToString() == "2")
            {
                return RedirectToAction("InicioPresidencia", "Administracion");
            }
            /*
                -> GERENCIA 
            */
            else if (Session["Privilegio"].ToString() == "3")
            {
                return RedirectToAction("InicioGerencia", "Administracion");
            }
            /*
                -> ATENCION AL CLIENTE 
            */
            else if (Session["Privilegio"].ToString() == "4")
            {
                return RedirectToAction("InicioAtencionClientes", "Administracion");
            }
            /*
                -> CLIENTES 
            */
            else if (Session["Privilegio"].ToString() == "5")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE PERFIL DE USUARIOS
        public ActionResult MiPerfil(int idusuario = 0)
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                idusuario = (int)Convert.ToUInt32(Session["IdUsuario"]); // CONVERTIR EXPLICITAMENTE OBJETO DE SESION A ENTERO
                Usuarios oUsuario = new Usuarios();
                oUsuario = new CN_Usuarios().Listar().Where(p => p.IdUsuario == idusuario).FirstOrDefault();
                return View(oUsuario);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE USUARIOS
        public ActionResult Usuarios()
        {
            if(Convert.ToUInt32(Session["Privilegio"]) == 1){
                return View();
            }
            else if(Convert.ToUInt32(Session["Privilegio"]) >=2 && Convert.ToUInt32(Session["Privilegio"])<=4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE CONSULTA DE TODOS LOS CLIENTES REGISTRADOS
        public ActionResult ConsultaClientesRegistrados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE CONSULTA DE TODOS LOS EMPLEADOS REGISTRADOS
        public ActionResult ConsultaEmpleadosRegistrados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE ROLES DE USUARIOS
        public ActionResult Roles()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) >= 2 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE CATEGORIAS DE PRODUCTOS
        public ActionResult CategoriasProductos()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE MARCAS DE PRODUCTOS
        public ActionResult MarcasProductos()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE GARANTIA DE PRODUCTOS
        public ActionResult GarantiaProductos()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE PRODUCTOS
        public ActionResult Productos()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE PRODUCTOS SIN STOCK DISPONIBLE
        public ActionResult ListadoProductosSinStock()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE PROBLEMAS COMPRAS CLIENTES
        public ActionResult ProblemasVentasClientes()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE LISTADO GENERAL DE COMPRAS CLIENTES
        public ActionResult ListadoComprasProcesadasClientes()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE LISTADO DETALLES GENERAL DE COMPRAS CLIENTES
        public ActionResult ListadoDetallesComprasProcesadasClientes()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE LISTADO PEDIDOS PROCESADOS [TODOS SIN FILTROS] -> GESTION DE PEDIDOS
        public ActionResult ListadoPedidosProcesados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE LISTADO PEDIDOS PROCESADOS [EN PROCESO] -> GESTION DE PEDIDOS
        public ActionResult ListadoPedidosEnProceso()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE LISTADO PEDIDOS PROCESADOS [ENVIADOS] -> GESTION DE PEDIDOS
        public ActionResult ListadoPedidosEnviados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE LISTADO PEDIDOS PROCESADOS [ENVIADOS] -> GESTION DE PEDIDOS
        public ActionResult ListadoPedidosCancelados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE LISTADO PEDIDOS PROCESADOS [ENTREGADOS] -> GESTION DE PEDIDOS
        public ActionResult ListadoPedidosEntregados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE PROBLEMAS REGISTRADOS -> PLATAFORMA [TODOS SIN FILTROS]
        public ActionResult ProblemasPlataforma()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) >= 2 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE REGISTRO DE PROBLEMAS -> DISPONIBLE UNICAMENTE PARA USUARIOS ADMINISTRATIVOS
        public ActionResult RegistroProblemasPlataformaGeneral()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 2 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return View();
            }
            else if(Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }       
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE PROBLEMAS REGISTRADOS -> PLATAFORMA [TODOS ACTIVOS]
        public ActionResult ProblemasPlataformaActivos()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) >= 2 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE PROBLEMAS REGISTRADOS -> PLATAFORMA [TODOS EN CURSO]
        public ActionResult ProblemasPlataformaEnCurso()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) >= 2 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE PROBLEMAS REGISTRADOS -> PLATAFORMA [TODOS RESUELTOS]
        public ActionResult ProblemasPlataformaResueltos()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) >= 2 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        // VISTA PRINCIPAL DE MANTENIMIENTO DE PROBLEMAS REGISTRADOS -> PLATAFORMA [TODOS CERRADOS]
        public ActionResult ProblemasPlataformaCerrados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                return View();
            }
            else if (Convert.ToUInt32(Session["Privilegio"]) >= 2 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                return RedirectToAction("RetornoSistemaArquitectosAlmacenes", "Administracion");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE USUARIOS
        public JsonResult ListarUsuarios()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                List<Usuarios> oLista = new List<Usuarios>();
                oLista = new CN_Usuarios().Listar();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Usuarios> oLista = new List<Usuarios>();
                oLista = new CN_Usuarios().Listar();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }


        [ValidarSession]
        [Authorize]
        // DEVOLVER LISTA DE CLIENTES
        public JsonResult ListarClientes()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<Usuarios> oLista = new List<Usuarios>();
                oLista = new CN_Usuarios().ListarClientes();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Usuarios> oLista = new List<Usuarios>();
                oLista = new CN_Usuarios().ListarClientes();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidarSession]
        [Authorize]
        // DEVOLVER LISTA DE EMPLEADOS
        public JsonResult ListarEmpleados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 3)
            {
                List<Usuarios> oLista = new List<Usuarios>();
                oLista = new CN_Usuarios().ListarEmpleados();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Usuarios> oLista = new List<Usuarios>();
                oLista = new CN_Usuarios().ListarEmpleados();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
                
        }

        [ValidarSession]
        [Authorize]
        // DEVOLVER LISTA DE ROLES DE USUARIOS
        public JsonResult ListarRolesUsuarios()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                List<Roles> oLista = new List<Roles>();
                oLista = new CN_Roles().Listar();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Roles> oLista = new List<Roles>();
                oLista = new CN_Roles().Listar();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
                
        }

        [ValidarSession]
        [Authorize]
        // DEVOLVER LISTA DE REPORTES PROBLEMAS PLATAFORMA [SIN FILTROS]
        public JsonResult ListarReportesProblemasUsuarios()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().Listar();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().Listar();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
                
        }

        [ValidarSession]
        [Authorize]
        // DEVOLVER LISTA DE REPORTES PROBLEMAS PLATAFORMA [ACTIVOS]
        public JsonResult ListarReportesProblemasUsuariosActivos()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().ListarProblemasActivos();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().ListarProblemasActivos();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
                
        }

        [ValidarSession]
        [Authorize]
        // DEVOLVER LISTA DE REPORTES PROBLEMAS PLATAFORMA [EN CURSO]
        public JsonResult ListarReportesProblemasUsuariosEnCurso()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().ListarProblemasEnCurso();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().ListarProblemasEnCurso();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
                
        }

        [ValidarSession]
        [Authorize]
        // DEVOLVER LISTA DE REPORTES PROBLEMAS PLATAFORMA [RESUELTOS]
        public JsonResult ListarReportesProblemasUsuariosResueltos()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().ListarProblemasResueltos();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().ListarProblemasResueltos();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
                
        }

        [ValidarSession]
        [Authorize]
        // DEVOLVER LISTA DE REPORTES PROBLEMAS PLATAFORMA [CERRADOS]
        public JsonResult ListarReportesProblemasUsuariosCerrados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) == 1)
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().ListarProblemasCerrados();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ProblemasPlataforma> oLista = new List<ProblemasPlataforma>();
                oLista = new CN_ProblemasPlataforma().ListarProblemasCerrados();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
                
        }

        [ValidarSession]
        [Authorize]
        // GUARDAR Y EDITAR USUARIOS
        [HttpPost]
        public JsonResult GuardarUsuario(Usuarios objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdUsuario == 0)
            {
                resultado = new CN_Usuarios().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // GUARDAR FOTOS DE PERFIL USUARIOS
        [HttpPost]
        public ActionResult GuardarFotosPerfilUsuarios(string objeto, HttpPostedFileBase archivoImagen)
        {
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;
            // CONVERTIR USUARIOS A OBJETO
            Usuarios oUsuarios = new Usuarios();
            oUsuarios = JsonConvert.DeserializeObject<Usuarios>(objeto);
            if (operacion_exitosa)
            {
                if (archivoImagen != null)
                {
                    // -> PARA EFECTOS DE PRUEBAS LOCALES
                    //string ruta_guardar = ConfigurationManager.AppSettings["GuardadoFotosPerfilUsuarios"];
                    // -> PARA EFECTOS DE PRODUCCION
                    string ruta_guardar = Server.MapPath("~/images/FotosPerfil/");
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oUsuarios.IdUsuario.ToString(), extension);
                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                    }
                    catch (Exception ex)
                    {
                        string MensajeError = ex.Message;
                        guardar_imagen_exito = false;
                    }
                    if (guardar_imagen_exito)
                    {
                        oUsuarios.RutaImagen = ruta_guardar;
                        // ACTUALIZAR VARIABLE DE SESION CON NUEVO NOMBRE DE IMAGEN
                        oUsuarios.NombreImagenPerfilUsuarios = nombre_imagen;
                        Session["FotoPerfil"] = nombre_imagen;
                        bool respuestaimagen = new CN_Usuarios().GuardarDatosImagen(oUsuarios, out mensaje);
                    }
                    else
                    {
                        mensaje = "Lamentamos informarle que su imagen no pudo ser guardada";
                    }
                }
            }
            return Json(new { operacion_exitosa = operacion_exitosa, idGenerado = oUsuarios.IdUsuario, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // GUARDAR Y EDITAR ROLES DE USUARIOS
        [HttpPost]
        public JsonResult GuardarRolesUsuario(Roles objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdRolUsuario == 0)
            {
                resultado = new CN_Roles().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Roles().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        // GUARDAR Y EDITAR REPORTES PROBLEMAS PLATAFORMA
        [HttpPost]
        public JsonResult GuardarProblemasReportesPlataforma(string objeto, HttpPostedFileBase archivoImagen)
        {
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;
            // CONVERTIR REPORTES PROBLEMAS PLATAFORMA A OBJETO
            ProblemasPlataforma oProblemasPlataforma = new ProblemasPlataforma();
            oProblemasPlataforma = JsonConvert.DeserializeObject<ProblemasPlataforma>(objeto);
            
            if (oProblemasPlataforma.IdReportePlataforma == 0)
            {
                // GUARDAR IMAGEN DE REPORTES PROBLEMAS PLATAFORMA E INFORMACION DE REPORTES EN BASE DE DATOS
                int idReporteGenerado = new CN_ProblemasPlataforma().Registrar(oProblemasPlataforma, out mensaje);
                if (idReporteGenerado != 0)
                {
                    oProblemasPlataforma.IdReportePlataforma = idReporteGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            // EDITAR REPORTES PROBLEMAS PLATAFORMA
            else
            {
                operacion_exitosa = new CN_ProblemasPlataforma().Editar(oProblemasPlataforma, out mensaje);
            }
            if (operacion_exitosa)
            {
                if (archivoImagen != null)
                {
                    // -> PARA EFECTOS DE PRUEBAS LOCALES
                    // string ruta_guardar = ConfigurationManager.AppSettings["GuardadoFotosReportesProblemas"];
                    // -> PARA EFECTOS DE PRODUCCION
                    string ruta_guardar = Server.MapPath("~/images/ReportesPlataforma/");
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProblemasPlataforma.IdReportePlataforma.ToString(), extension);
                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                    }
                    catch (Exception ex)
                    {
                        string MensajeError = ex.Message;
                        guardar_imagen_exito = false;
                    }
                    if (guardar_imagen_exito)
                    {
                        oProblemasPlataforma.RutaImagen = ruta_guardar;
                        oProblemasPlataforma.NombreImagenReporte = nombre_imagen;
                        bool respuestaimagen = new CN_ProblemasPlataforma().GuardarDatosImagen(oProblemasPlataforma, out mensaje);
                    }
                    else
                    {
                        mensaje = "Su reporte fue registrado con exito, pero lamentamos informarle que su imagen no pudo ser guardada";
                    }
                }
            }
            return Json(new { operacion_exitosa = operacion_exitosa, idGenerado = oProblemasPlataforma.IdReportePlataforma, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE CATEGORIAS DE PRODUCTOS
        public JsonResult ListarCategorias()
        {
            List<Categorias> oLista = new List<Categorias>();
            oLista = new CN_Categoria().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        // GUARDAR Y EDITAR CATEGORIAS DE PRODUCTOS
        [ValidarSession]
        [Authorize]
        [HttpPost]
        public JsonResult GuardarCategoria(Categorias objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdCategorias == 0)
            {
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE MARCAS DE PRODUCTOS
        public JsonResult ListarMarcas()
        {
            List<Marcas> oLista = new List<Marcas>();
            oLista = new CN_Marcas().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        // GUARDAR Y EDITAR MARCAS DE PRODUCTOS
        [ValidarSession]
        [Authorize]
        [HttpPost]
        public JsonResult GuardarMarcas(Marcas objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdMarcas == 0)
            {
                resultado = new CN_Marcas().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Marcas().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE GARANTIA DE PRODUCTOS
        public JsonResult ListarGarantiasProductos()
        {
            List<Garantias> oLista = new List<Garantias>();
            oLista = new CN_Garantias().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        // GUARDAR Y EDITAR GARANTIAS DE PRODUCTOS
        [ValidarSession]
        [Authorize]
        [HttpPost]
        public JsonResult GuardarGarantiasProductos(Garantias objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdControlGarantias == 0)
            {
                resultado = new CN_Garantias().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Garantias().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // ELIMINAR GARANTIAS DE PRODUCTOS
        [ValidarSession]
        [Authorize]
        [HttpPost]
        public JsonResult EliminarGarantiasProductos(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Garantias().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        // EDITAR PERFIL USUARIOS ADMINISTRATIVOS
        [ValidarSession]
        [Authorize]
        [HttpPost]
        public ActionResult EditarMiPerfil(Usuarios objeto)
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

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PRODUCTOS REGISTRADOS
        public JsonResult ListarProductos()
        {
            List<Productos> oLista = new List<Productos>();
            oLista = new CN_Productos().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PRODUCTOS REGISTRADOS SIN STOCK DISPONIBLE
        public JsonResult ListarProductosSinStock()
        {
            List<Productos> oLista = new List<Productos>();
            oLista = new CN_Productos().ListarProductosSinStock();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        // GUARDAR Y EDITAR PRODUCTOS
        [ValidarSession]
        [Authorize]
        [HttpPost]
        public JsonResult GuardarNuevosProductos(string objeto, HttpPostedFileBase archivoImagen)
        {
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;
            // CONVERTIR PRODUCTOS A OBJETO
            Productos oProductos = new Productos();
            oProductos = JsonConvert.DeserializeObject<Productos>(objeto);
            
            // VARIABLES GLOBALES DE DESERIALIZACION DATOS JSON A OBJETOS -> PRODUCTOS Y GARANTIAS
            double precio;
            int stock, diasgarantia;
            if (double.TryParse(oProductos.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-SV"), out precio))
            {
                oProductos.Precio = precio;

            }
            if (int.TryParse(oProductos.StockTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-SV"), out stock))
            {
                oProductos.Stock = stock;

            }
            if (int.TryParse(Convert.ToInt32(oProductos.DuracionDiasGarantia).ToString(), NumberStyles.AllowDecimalPoint, new CultureInfo("es-SV"), out diasgarantia))
            {
                oProductos.DuracionDiasGarantia = diasgarantia;

            }
            // INICIO PROCESO DE GUARDADO DE INFORMACION A BASE DE DATOS
            if (oProductos.IdProducto == 0)
            {
                // GUARDAR IMAGEN DE PRODUCTOS E INFORMACION EN BASE DE DATOS
                int idReporteGenerado = new CN_Productos().Registrar(oProductos, out mensaje);
                if (idReporteGenerado != 0)
                {
                    oProductos.IdProducto = idReporteGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            // EDITAR PRODUCTOS
            else
            {
                operacion_exitosa = new CN_Productos().Editar(oProductos, out mensaje);
            }
            if (operacion_exitosa)
            {
                if (archivoImagen != null)
                {
                    // -> PARA EFECTOS DE PRUEBAS LOCALES
                    //string ruta_guardar = ConfigurationManager.AppSettings["GuardadoFotosProductos"];
                    // -> PARA EFECTOS DE PRODUCCION
                    string ruta_guardar = Server.MapPath("~/images/FotosProductos/");
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProductos.IdProducto.ToString(), extension);
                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                    }
                    catch (Exception ex)
                    {
                        string MensajeError = ex.Message;
                        guardar_imagen_exito = false;
                    }
                    if (guardar_imagen_exito)
                    {
                        oProductos.RutaImagen = ruta_guardar;
                        oProductos.NombreImagenProductos = nombre_imagen;
                        bool respuestaimagen = new CN_Productos().GuardarDatosImagenProductos(oProductos, out mensaje);
                    }
                    else
                    {
                        mensaje = "Su producto fue registrado con exito, pero lamentamos informarle que su imagen no pudo ser guardada";
                    }
                }
            }
            return Json(new { operacion_exitosa = operacion_exitosa, idGenerado = oProductos.IdProducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PROBLEMAS COMPRAS CLIENTES
        public JsonResult ListarProblemasComprasClientes()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <=4)
            {
                List<ProblemasVentas> oLista = new List<ProblemasVentas>();
                oLista = new CN_Venta().ListarProblemasComprasClientes();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ProblemasVentas> oLista = new List<ProblemasVentas>();
                oLista = new CN_Venta().ListarProblemasComprasClientes();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PROBLEMAS COMPRAS CLIENTES -> PORTAL ADMINISTRATIVO
        public JsonResult ListarComprasProcesadasClientes_PortalAdministrativo()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasProcesadasClientes_PortalAdministrativo();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasProcesadasClientes_PortalAdministrativo();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PROBLEMAS COMPRAS CLIENTES -> PORTAL ADMINISTRATIVO
        public JsonResult ListarDetallesComprasProcesadasClientes_PortalAdministrativo()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<DetalleVenta> oLista = new List<DetalleVenta>();
                oLista = new CN_Venta().ListarDetallesComprasProcesadasClientes_PortalAdministrativo();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<DetalleVenta> oLista = new List<DetalleVenta>();
                oLista = new CN_Venta().ListarDetallesComprasProcesadasClientes_PortalAdministrativo();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        // OBTENER LISTADO DE VENTAS PROCESADAS POR CLIENTE -> CUMPLIENDO LA CONDICION ANTES MENCIONADA
        [ValidarSession]
        [Authorize]
        [HttpGet]
        public JsonResult ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas_Administracion()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas_Administrativo();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas_Administrativo();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        // OBTENER LISTADO DE DUI REGISTRADOS POR CLIENTE -> VENTAS PROCESADAS
        [ValidarSession]
        [Authorize]
        [HttpGet]
        public JsonResult ListadoUsuariosVentas()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListadoDuiRegistrados_VentasClientes();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListadoDuiRegistrados_VentasClientes();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        // REGISTRO REPORTES PROBLEMAS PEDIDOS / COMPRAS CLIENTES
        [ValidarSession]
        [Authorize]
        [HttpPost]
        public ActionResult RegistroProblemasVentasClientes(ProblemasVentas objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdReporteProblema == 0)
            {

                resultado = new CN_Venta().RegistrarProblemasPedidos_Administrativo(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Venta().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // VER REPORTE DE VENTAS DASHBOARD [DATATABLE] -> TODOS LOS USUARIOS ADMINISTRATIVOS
        [ValidarSession]
        [Authorize]
        [HttpGet]
        public JsonResult ListaReporte(string fechainicio, string fechafin, string idtransaccion)
        {
            List<ReporteVentas> oLista = new List<ReporteVentas>();
            oLista = new CN_Reportes().Ventas(fechainicio, fechafin, idtransaccion);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        // CONTADOR DE PRODUCTOS POR CATEGORIAS
        [HttpGet]
        public JsonResult ConteoProductosPorCategorias()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >=1 && Convert.ToUInt32(Session["Privilegio"]) <=3)
            {
                ReporteDashboardAdministradores objeto = new CN_Reportes().TotalProductosPorCategorias();
                return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ReporteDashboardAdministradores objeto = new CN_Reportes().TotalProductosPorCategorias();
                return Json(new { resultado = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        // CONTADOR DE VENTAS POR MES DE AÑO EN CURSO
        [ValidarSession]
        [Authorize]
        [HttpGet]
        public JsonResult ConteoVentasPorMes()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 3)
            {
                ReporteDashboardAdministradores objeto = new CN_Reportes().TotalVentasPorMes(2022);
                return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ReporteDashboardAdministradores objeto = new CN_Reportes().TotalVentasPorMes(2022);
                return Json(new { resultado = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PEDIDOS PROCESADOS [TODOS SIN FILTRO] -> GESTION DE PEDIDOS
        public JsonResult ListarPedidosProcesados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidos();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidos();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PEDIDOS PROCESADOS [EN PROCESO] -> GESTION DE PEDIDOS
        public JsonResult ListarPedidosProcesadosEnProceso()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidosEnProceso();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidosEnProceso();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PEDIDOS PROCESADOS [ENVIADOS] -> GESTION DE PEDIDOS
        public JsonResult ListarPedidosProcesadosEnviados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidosEnviados();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidosEnviados();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PEDIDOS PROCESADOS [ENTREGADOS] -> GESTION DE PEDIDOS
        public JsonResult ListarPedidosProcesadosEntregados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidosEntregadas();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidosEntregadas();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidarSession]
        [Authorize]
        [HttpGet]
        // DEVOLVER LISTA DE PEDIDOS PROCESADOS [CANCELADOS] -> GESTION DE PEDIDOS
        public JsonResult ListarPedidosProcesadosCancelados()
        {
            if (Convert.ToUInt32(Session["Privilegio"]) >= 1 && Convert.ToUInt32(Session["Privilegio"]) <= 4)
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidosCanceladas();
                return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Venta> oLista = new List<Venta>();
                oLista = new CN_Venta().ListarComprasPedidosCanceladas();
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidarSession]
        [Authorize]
        // EDITAR ESTADO PEDIDO CLIENTES
        [HttpPost]
        public JsonResult EditarEstadoPedidosClientes(Venta objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IdVenta != 0)
            {
                resultado = new CN_Venta().EditarEstadoPedidos(objeto, out mensaje);
            }
            else
            {
                // AL NO EXISTIR PROCESO DE REGISTRO DE PEDIDOS EXPLICITAMENTE, SIMPLEMENTE RETORNA CERO
                resultado = 0;
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }// CIERRE public class AdministracionController : Controller
}
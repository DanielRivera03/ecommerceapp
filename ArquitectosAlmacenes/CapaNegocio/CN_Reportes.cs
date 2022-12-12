
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
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Reportes
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE REPORTES DASHBOARD
        private CD_Reportes objCapaDato = new CD_Reportes();

        // ELEMENTOS DE BIENVENIDA -> CONTEO DE DATOS DE INTERES PORTAL ADMINISTRADORES
        public ReporteDashboardAdministradores VerDashBoard()
        {
            return objCapaDato.VerDashBoard();
        }
        // ELEMENTOS DE BIENVENIDA -> CONTEO DE DATOS DE INTERES PORTAL PRESIDENCIA
        public ReporteDashboardPresidencia VerDashBoardPresidencia()
        {
            return objCapaDato.VerDashBoardPresidencia();
        }
        // ELEMENTOS DE BIENVENIDA -> CONTEO DE DATOS DE INTERES PORTAL GERENCIA
        public ReporteDashboardGerencia VerDashBoardGerencia()
        {
            return objCapaDato.VerDashBoardGerencia();
        }
        // CONTEO DE PRODUCTOS POR CATEGORIAS
        public ReporteDashboardAdministradores TotalProductosPorCategorias()
        {
            return objCapaDato.TotalProductosPorCategorias();
        }
        // CONTEO DE PRODUCTOS POR CATEGORIAS
        public ReporteDashboardAdministradores TotalVentasPorMes(int anioactual)
        {
            return objCapaDato.TotalVentasPorMes(anioactual);
        }
        // LISTAR TODO REPORTE DE VENTAS DASHBOARD
        public List<ReporteVentas> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            return objCapaDato.Ventas(fechainicio, fechafin, idtransaccion);
        }
    }
}

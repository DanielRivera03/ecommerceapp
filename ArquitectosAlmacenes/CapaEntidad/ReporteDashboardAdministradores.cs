
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

namespace CapaEntidad
{
    public class ReporteDashboardAdministradores
    {
        public int TotalUsuarios { get; set; }
        public int TotalProductos { get; set; }
        public int TotalGarantias { get; set; }
        public int TotalClientes { get; set; }
        public int TotalJoyeria { get; set; }
        public int TotalRelojeria { get; set; }
        public int TotalCalzado { get; set; }
        public int TotalTecnologia { get; set; }
        public int TotalLineaBlanca { get; set; }
        public int TotalElectrodomesticos { get; set; }
        public int TotalBelleza { get; set; }
        public int TotalJuguetes { get; set; }
        // VENTAS POR MES
        public double TotalEnero { get; set; }
        public double TotalFebrero { get; set; }
        public double TotalMarzo { get; set; }
        public double TotalAbril { get; set; }
        public double TotalMayo { get; set; }
        public double TotalJunio { get; set; }
        public double TotalJulio { get; set; }
        public double TotalAgosto { get; set; }
        public double TotalSeptiembre { get; set; }
        public double TotalOctubre { get; set; }
        public double TotalNoviembre { get; set; }
        public double TotalDiciembre { get; set; }
        public double TotalIngresos { get; set; }

    }
}

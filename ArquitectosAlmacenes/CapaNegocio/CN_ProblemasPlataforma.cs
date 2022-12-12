
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
    public class CN_ProblemasPlataforma
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE USUARIOS
        private CD_ProblemasPlataforma objCapaDato = new CD_ProblemasPlataforma();

        // DEVOLVER TODO EL LISTADO DE USUARIOS Y CLIENTES REGISTRADOS [SIN FILTROS]
        public List<ProblemasPlataforma> Listar()
        {
            return objCapaDato.Listar();
        }

        // DEVOLVER TODO EL LISTADO DE USUARIOS Y CLIENTES REGISTRADOS [ACTIVOS]
        public List<ProblemasPlataforma> ListarProblemasActivos()
        {
            return objCapaDato.ListarProblemasActivos();
        }

        // DEVOLVER TODO EL LISTADO DE USUARIOS Y CLIENTES REGISTRADOS [EN CURSO]
        public List<ProblemasPlataforma> ListarProblemasEnCurso()
        {
            return objCapaDato.ListarProblemasEnCurso();
        }

        // DEVOLVER TODO EL LISTADO DE USUARIOS Y CLIENTES REGISTRADOS [RESUELTOS]
        public List<ProblemasPlataforma> ListarProblemasResueltos()
        {
            return objCapaDato.ListarProblemasResueltos();
        }

        // DEVOLVER TODO EL LISTADO DE USUARIOS Y CLIENTES REGISTRADOS [CERRADOS]
        public List<ProblemasPlataforma> ListarProblemasCerrados()
        {
            return objCapaDato.ListarProblemasCerrados();
        }

        // REGISTRO DE NUEVOS REPORTES PROBLEMAS PLATAFORMA USUARIOS
        public int Registrar(ProblemasPlataforma obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.NombreReporte) || string.IsNullOrWhiteSpace(obj.NombreReporte))
            {
                Mensaje = "El nombre del reporte no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.DescripcionReporte) || string.IsNullOrWhiteSpace(obj.DescripcionReporte))
            {
                Mensaje = "La descripcion del reporte no puede estar vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            } // CIERRE if (string.IsNullOrEmpty(Mensaje))
            else
            {
                return 0;
            }
        }

        // EDITAR REPORTES PROBLEMAS PLATAFORMA USUARIOS
        public bool Editar(ProblemasPlataforma obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.DescripcionReporte) || string.IsNullOrWhiteSpace(obj.DescripcionReporte))
            {
                Mensaje = "La descripcion del reporte no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }

        }

        // REGISTRO RUTA DE IMAGENES REPORTES PROBLEMAS PLATAFORMA
        public bool GuardarDatosImagen(ProblemasPlataforma obj, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }
    }
}

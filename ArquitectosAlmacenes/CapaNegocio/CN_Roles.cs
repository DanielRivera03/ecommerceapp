
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
    public class CN_Roles
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE ROLES DE USUARIO
        private CD_Roles objCapaDato = new CD_Roles();

        // LISTAR TODOS LOS ROLES DE USUARIO REGISTRADOS
        public List<Roles> Listar()
        {
            return objCapaDato.Listar();
        }

        // REGISTRO DE NUEVOS ROLES DE USUARIO
        public int Registrar(Roles obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.DescripcionCortaRolUsuario) || string.IsNullOrWhiteSpace(obj.DescripcionCortaRolUsuario))
            {
                Mensaje = "El nombre del rol de usuario a crear no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.DescripcionCortaRolUsuario) || string.IsNullOrWhiteSpace(obj.DescripcionCortaRolUsuario))
            {
                Mensaje = "La descripcion del rol de usuario a crear no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            } // CIERRE if (string.IsNullOrEmpty(Mensaje))
            else
            {
                return 0;
            }
        }// CIERRE public int Registrar(Roles obj, out string Mensaje)

        // EDITAR DE NUEVOS ROLES DE USUARIO
        public bool Editar(Roles obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.DescripcionCortaRolUsuario) || string.IsNullOrWhiteSpace(obj.DescripcionCortaRolUsuario))
            {
                Mensaje = "El nombre del rol de usuario a crear no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.DescripcionCortaRolUsuario) || string.IsNullOrWhiteSpace(obj.DescripcionCortaRolUsuario))
            {
                Mensaje = "La descripcion del rol de usuario a crear no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            } // CIERRE if (string.IsNullOrEmpty(Mensaje))
            else
            {
                return false;
            }
        }// CIERRE public bool Editar(Roles obj, out string Mensaje)

    }
}

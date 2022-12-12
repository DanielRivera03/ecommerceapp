
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
    public class CN_Garantias
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE USUARIOS
        private CD_Garantias objCapaDato = new CD_Garantias();

        // DEVOLVER TODO EL LISTADO DE USUARIOS Y CLIENTES REGISTRADOS
        public List<Garantias> Listar()
        {
            return objCapaDato.Listar();
        }

        // REGISTRO DE NUEVAS GARANTIAS DE PRODUCTOS
        public int Registrar(Garantias obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool existeMarcaAsociadaGarantia = new CN_Garantias().VerificarDisponibilidadMarcaGarantia(obj.oMarcas.IdMarcas); // SE ACCEDE AL OBJETO DE LA CLASE MARCAS
            if (obj.oMarcas.IdMarcas == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar una marca de producto";
            }
            else if (obj.DuracionDiasGarantia == 0)
            {
                Mensaje = "Lo sentimos, debe ingresar la duracion en dias de la garantia";
            }
            else if (string.IsNullOrEmpty(obj.DescripcionFabricanteGarantia) || string.IsNullOrWhiteSpace(obj.DescripcionFabricanteGarantia))
            {
                Mensaje = "Lo sentimos, debe ingresar la descripcion de la garantia proporcionada por el fabricante";
            }
            else if (existeMarcaAsociadaGarantia)
            {
                Mensaje = "Lo sentimos, esta marca ya cuenta con una garantia asociada";
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

        // MODIFICAR GARANTIA DE PRODUCTOS
        public bool Editar(Garantias obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.oMarcas.IdMarcas == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar una marca de producto";
            }
            else if (obj.DuracionDiasGarantia == 0)
            {
                Mensaje = "Lo sentimos, debe ingresar la duracion en dias de la garantia";
            }
            else if (string.IsNullOrEmpty(obj.DescripcionFabricanteGarantia) || string.IsNullOrWhiteSpace(obj.DescripcionFabricanteGarantia))
            {
                Mensaje = "Lo sentimos, debe ingresar la descripcion de la garantia proporcionada por el fabricante";
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

        // ELIMINAR GARANTIAS DE PRODUCTOS
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        // VERIFICAR SI MARCA DE PRODUCTO, YA SE ENCUENTRA ASOCIADA A UNA GARANTIA
        public bool VerificarDisponibilidadMarcaGarantia(int IdMarcas)
        {
            return objCapaDato.VerificarMarcasGarantiaProductos(IdMarcas);
        }

    }
}

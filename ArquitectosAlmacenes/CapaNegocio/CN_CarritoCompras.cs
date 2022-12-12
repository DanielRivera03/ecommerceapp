
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
    public class CN_CarritoCompras
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE CARRITO DE COMPRAS
        private CD_CarritoCompras objCapaDato = new CD_CarritoCompras();

        // VERIFICAR EXISTENCIAS DE PRODUCTOS REGISTRADOS EN CARRITO DE COMPRAS [NO PERMITIR PRODUCTOS DUPLICADOS]
        public bool ExisteCarrito(int idusuario, int idproducto)
        {
            return objCapaDato.ExisteCarrito(idusuario, idproducto);
        }

        // VERIFICAR STOCK DE PRODUCTOS REGISTRADOS EN CARRITO DE COMPRAS [NO PERMITIR PRODUCTOS SIN STOCK]
        public bool VerificarStockProductosCarrito(int idproducto)
        {
            return objCapaDato.VerificarStockProductosCarrito(idproducto);
        }

        // GESTION DE TODOS LOS ARTICULOS AGREGADOS POR CLIENTES -> CARRITO DE COMPRAS
        public bool OperacionCarrito(int idusuario, int idproducto, bool sumar, out string Mensaje)
        {
            return objCapaDato.OperacionCarrito(idusuario, idproducto, sumar, out Mensaje);
        }
        // TOTAL DE PRODUCTOS AGREGADOS EN CARRITO DE COMPRAS
        public int CantidadEnCarrito(int idusuario)
        {
            return objCapaDato.CantidadEnCarrito(idusuario);
        }
        // TOTAL DE PRODUCTOS AGREGADOS EN CARRITO DE COMPRAS [TOTAL A CANCELAR] -> SUMATORIA DE PRECIOS PRODUCTOS
        public double CantidadEnCarrito_TotalCancelar(int idusuario)
        {
            return objCapaDato.CantidadEnCarrito_TotalCancelar(idusuario);
        }
        // OBTENER EL LISTADO DE PRODUCTOS CARRITO DE COMPRAS
        public List<CarritoCompras> ListarProducto(int idusuario)
        {
            return objCapaDato.ListarProducto(idusuario);
        }
        // ELIMINAR PRODUCTOS REGISTRADOS EN CARRITO DE COMPRAS
        public bool EliminarCarrito(int idusuario, int idproducto)
        {
            return objCapaDato.EliminarCarrito(idusuario, idproducto);
        }
    }
}

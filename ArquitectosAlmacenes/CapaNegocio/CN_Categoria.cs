
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
    public class CN_Categoria
    {

        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE CATEGORIAS
        private CD_Categoria objCapaDato = new CD_Categoria();

        // LISTADO DE TODAS LAS CATEGORIAS DE PRODUCTOS REGISTRADAS
        public List<Categorias> Listar()
        {
            return objCapaDato.Listar();
        }

        // REGISTRO DE NUEVAS CATEGORIAS DE PRODUCTOS
        public int Registrar(Categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool existeCodigoUnicoCategorias = new CN_Categoria().VerificarCodigoUnicoCategorias(obj.CodigoCategoria);
            bool respuesta = false;
            if (string.IsNullOrEmpty(obj.CodigoCategoria) || string.IsNullOrWhiteSpace(obj.CodigoCategoria))
            {
                Mensaje = "El código de la categoria no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la categoria no puede estar vacia";
            }
            else if (existeCodigoUnicoCategorias)
            {
                Mensaje = "Lo sentimos, este código único de categoría ya se encuentra registrado";
                respuesta = false;
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

        // EDITAR CATEGORIAS DE PRODUCTOS
        public bool Editar(Categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.CodigoCategoria) || string.IsNullOrWhiteSpace(obj.CodigoCategoria))
            {
                Mensaje = "El código de la categoria no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la categoria no puede estar vacia";
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

        // VERIFICAR SI CODIGO UNICO DE CATEGORIA REGISTRADO EXISTE EN BASE DE DATOS
        public bool VerificarCodigoUnicoCategorias(string CodigoUnicoCategoria)
        {
            return objCapaDato.VerificarCodigoUnicoCategoriasProductos(CodigoUnicoCategoria);
        }


    }
}

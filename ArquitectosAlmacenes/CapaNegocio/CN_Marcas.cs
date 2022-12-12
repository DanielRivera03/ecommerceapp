
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Marcas
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE MARCAS DE PRODUCTOS
        private CD_Marcas objCapaDato = new CD_Marcas();

        // LISTADO DE TODAS LAS MARCAS DE PRODUCTOS REGISTRADAS
        public List<Marcas> Listar()
        {
            return objCapaDato.Listar();
        }

        // LISTAR MARCAS POR CATEGORIAS

        public List<Marcas> ListarMarcaPorCategoria(int idcategoria)
        {
            return objCapaDato.ListarMarcaPorCategoria(idcategoria);
        }

        // REGISTRO DE NUEVAS CATEGORIAS DE PRODUCTOS
        public int Registrar(Marcas obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool existeMarcaProductos = new CN_Marcas().VerificarNombresMarcas(obj.Descripcion);
            bool respuesta = false;
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "El nombre de la marca no puede estar vacio";
            }
            else if (existeMarcaProductos)
            {
                Mensaje = "Lo sentimos, esta marca ya se encuentra registrada";
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
        public bool Editar(Marcas obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool existeMarcaProductos = new CN_Marcas().VerificarNombresMarcas(obj.Descripcion);
            bool respuesta = false;
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "El nombre de la marca no puede estar vacio";
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
        public bool VerificarNombresMarcas(string NombreMarca)
        {
            return objCapaDato.VerificarMarcasProductos(NombreMarca);
        }
    }
}


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
    public class CN_Productos
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE PRODUCTOS
        private CD_Productos objCapaDato = new CD_Productos();

        // DEVOLVER TODO EL LISTADO DE PRODUCTOS REGISTRADOS [BUSCADOR DE PRODUCTOS]
        public List<Productos> BuscadorProductos(Productos obj, string ValorBusqueda)
        {
            return objCapaDato.BuscadorProductos(obj, ValorBusqueda);
        }

        // DEVOLVER TODO EL LISTADO DE PRODUCTOS REGISTRADOS [SIN FILTRO]
        public List<Productos> Listar()
        {
            return objCapaDato.Listar();
        }

        // DEVOLVER TODO EL LISTADO DE PRODUCTOS REGISTRADOS ORDENADOS POR FECHA DE REGISTRO [CON LIMITACION A 30 ELEMENTOS UNICAMENTE]
        public List<Productos> ListarProductosInicioClientes()
        {
            return objCapaDato.ListarProductosInicioClientes();
        }

        // LISTADO DE PRODUCTOS CATEGORIA JOYERIA
        public List<Productos> ListarProductosCategoriaJoyeria()
        {
            return objCapaDato.ListarProductosCategoriaJoyeria();
        }

        // LISTADO DE PRODUCTOS CATEGORIA RELOJERIA
        public List<Productos> ListarProductosCategoriaRelojeria()
        {
            return objCapaDato.ListarProductosCategoriaRelojeria();
        }

        // LISTADO DE PRODUCTOS CATEGORIA CALZADO
        public List<Productos> ListarProductosCategoriaCalzado()
        {
            return objCapaDato.ListarProductosCategoriaCalzado();
        }

        // LISTADO DE PRODUCTOS CATEGORIA TECNOLOGIA
        public List<Productos> ListarProductosCategoriaTecnologia()
        {
            return objCapaDato.ListarProductosCategoriaTecnologia();
        }

        // LISTADO DE PRODUCTOS CATEGORIA LINEA BLANCA
        public List<Productos> ListarProductosCategoriaLineaBlanca()
        {
            return objCapaDato.ListarProductosCategoriaLineaBlanca();
        }

        // LISTADO DE PRODUCTOS CATEGORIA ELECTRODOMESTICOS
        public List<Productos> ListarProductosCategoriaElectrodomesticos()
        {
            return objCapaDato.ListarProductosCategoriaElectrodomesticos();
        }

        // LISTADO DE PRODUCTOS CATEGORIA BELLEZA
        public List<Productos> ListarProductosCategoriaBelleza()
        {
            return objCapaDato.ListarProductosCategoriaBelleza();
        }

        // LISTADO DE PRODUCTOS CATEGORIA JUGUETES
        public List<Productos> ListarProductosCategoriaJuguetes()
        {
            return objCapaDato.ListarProductosCategoriaJuguetes();
        }

        // DEVOLVER TODO EL LISTADO DE PRODUCTOS REGISTRADOS [SIN STOCK DISPONIBLE = 0]
        public List<Productos> ListarProductosSinStock()
        {
            return objCapaDato.ListarProductosSinStock();
        }

        // REGISTRO DE NUEVOS PRODUCTOS
        public int Registrar(Productos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "Lo sentimos, el nombre de este producto no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Lo sentimos, la descripcion de este producto no puede estar vacio";
            }
            else if (obj.oMarcas.IdMarcas == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar una marca asociada a este producto";
            }
            else if (obj.oCategorias.IdCategorias == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar una categoria asociada a este producto";
            }
            else if (obj.oGarantias.IdControlGarantias == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar el periodo de garantia asociado a este producto";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Lo sentimos, debe ingresar un precio a este producto";
            }
            else if (string.IsNullOrEmpty(obj.PrecioTexto) || string.IsNullOrWhiteSpace(obj.PrecioTexto))
            {
                Mensaje = "Lo sentimos, debe ingresar un precio a este producto";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Lo sentimos, debe ingresar un stock a este producto";
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

        // MODIFICAR PRODUCTOS
        public bool Editar(Productos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "Lo sentimos, el nombre de este producto no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Lo sentimos, la descripcion de este producto no puede estar vacio";
            }
            else if (obj.oMarcas.IdMarcas == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar una marca asociada a este producto";
            }
            else if (obj.oCategorias.IdCategorias == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar una categoria asociada a este producto";
            }
            else if (obj.oGarantias.IdControlGarantias == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar el periodo de garantia asociado a este producto";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Lo sentimos, debe ingresar un precio a este producto";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Lo sentimos, debe ingresar un stock a este producto";
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

        // REGISTRO DE IMAGENES PRODUCTOS
        public bool GuardarDatosImagenProductos(Productos obj, out string Mensaje)
        {
            return objCapaDato.GuardarFotosProductos(obj, out Mensaje);
        }
    }
}

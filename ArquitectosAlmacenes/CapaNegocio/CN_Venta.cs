
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



using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objCapaDato = new CD_Venta();
        // REGISTRAR DETALLES DE VENTAS
        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, DetalleVenta, out Mensaje);
        }

        // LISTAR TODAS LAS COMPRAS REALIZADAS POR CLIENTES
        public List<Venta> ListarComprasProcesadasClientes(int idusuario)
        {
            return objCapaDato.ListarComprasProcesadasClientes(idusuario);
        }
        // LISTAR TODAS LAS COMPRAS REALIZADAS POR CLIENTES -> PORTAL ADMINISTRATIVO
        public List<Venta> ListarComprasProcesadasClientes_PortalAdministrativo()
        {
            return objCapaDato.ListarComprasProcesadasClientes_PortalAdministrativo();
        }
        // LISTAR TODAS LAS COMPRAS REALIZADAS POR CLIENTES -> PORTAL ADMINISTRATIVO
        public List<DetalleVenta> ListarDetallesComprasProcesadasClientes_PortalAdministrativo()
        {
            return objCapaDato.ListarDetallesComprasProcesadasClientes_PortalAdministrativo();
        }
        // LISTAR TODOS LOS DETALLES DE VENTAS -> COMPRAS REALIZADAS POR CLIENTES
        public List<DetalleVenta> ListarDetallesComprasProcesadasClientes(int idusuario, int idventa)
        {
            return objCapaDato.ListarDetallesComprasProcesadasClientes(idusuario, idventa);
        }
        // LISTAR TODAS LAS COMPRAS REALIZADAS POR CLIENTES -> <SELECT> PARA SELECCIONAR ID DE TRANSACCION
        public List<Venta> ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas(int idusuario)
        {
            return objCapaDato.ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas(idusuario);
        }
        // LISTAR TODAS LAS COMPRAS REALIZADAS POR CLIENTES -> <SELECT> PARA SELECCIONAR ID DE TRANSACCION [EMPLEADOS ADMINISTRATIVOS]
        public List<Venta> ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas_Administrativo()
        {
            return objCapaDato.ObtenerIDTransacciones_VentasProcesadas_ReportesProblemas_Administracion();
        }
        // LISTADO DE DUI REGISTRADOS VENTAS CLIENTES
        public List<Venta> ListadoDuiRegistrados_VentasClientes()
        {
            return objCapaDato.ListadoDuiRegistrados_VentasClientes();
        }
        // LISTAR TODAS LOS PEDIDOS REGISTRADOS [SIN FILTROS]
        public List<Venta> ListarComprasPedidos()
        {
            return objCapaDato.ListarComprasPedidos();
        }
        // LISTAR TODAS LOS PEDIDOS REGISTRADOS [EN PROCESO]
        public List<Venta> ListarComprasPedidosEnProceso()
        {
            return objCapaDato.ListarComprasPedidosEnProceso();
        }
        // LISTAR TODAS LOS PEDIDOS REGISTRADOS [ENVIADOS]
        public List<Venta> ListarComprasPedidosEnviados()
        {
            return objCapaDato.ListarComprasPedidosEnviados();
        }
        // LISTAR TODAS LOS PEDIDOS REGISTRADOS [ENTREGADOS]
        public List<Venta> ListarComprasPedidosEntregadas()
        {
            return objCapaDato.ListarComprasPedidosEntregadas();
        }
        // LISTAR TODAS LOS PEDIDOS REGISTRADOS [CANCELADOS]
        public List<Venta> ListarComprasPedidosCanceladas()
        {
            return objCapaDato.ListarComprasPedidosCanceladas();
        }
        // REGISTRO DE PROBLEMAS PEDIDOS / COMPRAS CLIENTES -> VALIDO PARA CLIENTES
        public int RegistrarProblemasPedidos(ProblemasVentas obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.DescripcionReporteProblemaVenta) || string.IsNullOrWhiteSpace(obj.DescripcionReporteProblemaVenta))
            {
                Mensaje = "Lo sentimos, debe ingresar el detalle del problema que usted presenta en su venta procesada";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }
        // REGISTRO DE PROBLEMAS PEDIDOS / COMPRAS CLIENTES -> VALIDO PARA USUARIOS ADMINISTRATIVOS
        public int RegistrarProblemasPedidos_Administrativo(ProblemasVentas obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool existereportecliente = new CN_Venta().VerificarExistenciasReporteProblemasVentasClientes(obj.oVentas.IdVenta);
            if (string.IsNullOrEmpty(obj.DescripcionReporteProblemaVenta) || string.IsNullOrWhiteSpace(obj.DescripcionReporteProblemaVenta))
            {
                Mensaje = "Lo sentimos, debe ingresar el detalle del problema que usted presenta en su venta procesada";
            }
            else if (existereportecliente)
            {
                Mensaje = "Lo sentimos, ya existe un reporte asociado a esta venta";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.RegistrarProblemasPedidos_Administrativo(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }
        // EDITAR REGISTRO DE PROBLEMAS PEDIDOS / COMPRAS CLIENTES -> VALIDO PARA USUARIOS ADMINISTRATIVOS
        public bool Editar(ProblemasVentas obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            
            if (string.IsNullOrEmpty(obj.ActualizacionComentariosReporteProblemaVenta) || string.IsNullOrWhiteSpace(obj.ActualizacionComentariosReporteProblemaVenta))
            {
                Mensaje = "El comentario de retroalimentacion de este reporte no puede estar vacio";
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
        // LISTAR TODAS LOS REPORTES PROBLEMAS DE COMPRAS REGISTRADOS POR CLIENTES
        public List<ProblemasVentas> ListarProblemasComprasClientes()
        {
            return objCapaDato.ListarProblemasComprasClientes();
        }

        // VERIFICAR SI REPORTE PROBLEMA COMPRAS CLIENTES, YA EXISTE EN BASE DE DATOS
        public bool VerificarExistenciasReporteProblemasVentasClientes(int idventa)
        {
            return objCapaDato.VerificarExistenciasReporteProblemasVentasClientes(idventa);
        }

        // EDITAR ESTADO PEDIDOS CLIENTES
        public bool EditarEstadoPedidos(Venta obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.EstadoPedido) || string.IsNullOrWhiteSpace(obj.EstadoPedido))
            {
                Mensaje = "Debe seleccionar un estado a procesar";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.EditarEstadoPedido(obj, out Mensaje);
            } // CIERRE if (string.IsNullOrEmpty(Mensaje))
            else
            {
                return false;
            }
        }// CIERRE public bool Editar(Roles obj, out string Mensaje)

    }
}

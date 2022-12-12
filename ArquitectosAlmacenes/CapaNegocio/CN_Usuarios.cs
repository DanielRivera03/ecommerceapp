
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
    public class CN_Usuarios
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE USUARIOS
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        // DEVOLVER TODO EL LISTADO DE USUARIOS Y CLIENTES REGISTRADOS
        public List<Usuarios> Listar()
        {
            return objCapaDato.Listar();
        }
        // DEVOLVER TODO EL LISTADO DE CLIENTES REGISTRADOS
        public List<Usuarios> ListarClientes()
        {
            return objCapaDato.ListarClientes();
        }

        // DEVOLVER TODO EL LISTADO DE EMPLEADOS REGISTRADOS
        public List<Usuarios> ListarEmpleados()
        {
            return objCapaDato.ListarEmpleados();
        }

        // REGISTRO DE NUEVOS USUARIOS Y CLIENTES [DESDE PANEL DE ADMINISTRACION]
        public int Registrar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Lo sentimos, los nombres de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Lo sentimos, los apellidos de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Lo sentimos, el correo de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Usuario) || string.IsNullOrWhiteSpace(obj.Usuario))
            {
                Mensaje = "Lo sentimos, el usuario unico no puede estar vacios";
            }
            else if (obj.oRoles.IdRolUsuario == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar un rol de usuario";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                // GENERAR CLAVE DE ACCESO ALEATORIA
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creacion de nuevo usuario en nuestro sistema";
                string mensaje_correo = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0'><head><meta charset='UTF-8'><meta content='width=device-width, initial-scale=1' name='viewport'><meta name='x-apple-disable-message-reformatting'>\r\n  <meta http-equiv='X-UA-Compatible' content='IE=edge'><meta content='telephone=no' name='format-detection'><title>New year's eve</title><link href='https://fonts.googleapis.com/css2?family=Monoton&display=swap' rel='stylesheet'>" 
                    +"<style type='text/css'>#outlook a {padding:0;}.ExternalClass {width:100%;}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div {line-height:100%;}.es-button {text-decoration:none!important;}a[x-apple-data-detectors]{color:inherit!important;text-decoration:none!important;font-size:inherit!important;font-family:inherit!important;font-weight:inherit!important;line-height:inherit!important;}.es-desk-hidden {display:none;float:left;overflow:hidden;width:0;max-height:0;line-height:0;}.es-button-border:hover a.es-button, .es-button-border:hover button.es-button {background:#e5e5e5!important;border-color:#e5e5e5!important;}" 
                    +".es-button-border:hover {border-color:#42d159 #42d159 #42d159 #42d159!important;background:#e5e5e5!important;}[data-ogsb] .es-button {border-width:0!important;padding:10px 35px 10px 35px!important;}@media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150%!important } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:60px!important; text-align:center } h2 { font-size:28px!important; text-align:center } h3 { font-size:20px!important; text-align:center } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:60px!important } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:28px!important }" 
                    +" .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important } .es-menu td a { font-size:16px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:16px!important } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:18px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:16px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class='gmail-fix'] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2," 
                    +" .es-m-txt-c h3 { text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:inline-block!important } .es-btn-fw { border-width:10px 0px!important; text-align:center!important } .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important }" 
                    +" .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0px!important } .es-m-p0r { padding-right:0px!important } .es-m-p0l { padding-left:0px!important } .es-m-p0t { padding-top:0px!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important }" 
                    +" table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } a.es-button, button.es-button { font-size:20px!important; display:inline-block!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important } }</style></head><body style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;font-family:helvetica, arial, verdana, sans-serif;padding:0;Margin:0'>" 
                    +"<div class='es-wrapper-color' style='background-color:#301758'><table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#301758'><tr style='border-collapse:collapse'><td valign='top' style='padding:0;Margin:0'><table class='es-content' cellspacing='0' cellpadding='0' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'><tr style='border-collapse:collapse'>" 
                    +"<td align='center' style='padding:0;Margin:0'><table class='es-content-body' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#1c0b3f;border-top:2px dashed #ffffff;border-right:2px dashed #ffffff;border-left:2px dashed #ffffff;width:600px;border-bottom:2px dashed #ffffff' cellspacing='0' cellpadding='0' align='center' bgcolor='#1c0b3f'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;background-image:url(http://arquitectosalmacenes.somee.com/ImgCorreos/5056978.png);background-repeat:no-repeat;background-position:center top'" 
                    +" background='http://arquitectosalmacenes.somee.com/ImgCorreos/5056978.png'><table cellspacing='0' cellpadding='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td class='es-m-p0r' valign='top' align='center' style='padding:0;Margin:0;width:556px'><table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-top:20px;padding-bottom:20px;font-size:0px'>" 
                    +"<img src='http://arquitectosalmacenes.somee.com/ImgCorreos/logo_administracion.png' alt style='display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic' height='27'></td></tr><tr class='es-mobile-hidden' style='border-collapse:collapse'><td align='center' height='90' style='padding:0;Margin:0'></td></tr><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-bottom:40px'><h1 style='Margin:0;line-height:86px;mso-line-height-rule:exactly;font-family:monoton, cursive;font-size:72px;font-style:normal;font-weight:bold;color:#ffffff'>Bienvenido<br>estimado usuario<br></h1></td></tr>" 
                    +"<tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-bottom:40px'><h2 style='Margin:0;line-height:29px;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;font-size:24px;font-style:normal;font-weight:normal;color:#ffffff'><b>Reciba una cordial bienvenida de parte de Arquitectos Almacenes S.A de C.V. Por favor inicie sesión e ingrese la clave presentada a continuación.&nbsp;</b></h2></td></tr><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-top:10px;padding-bottom:10px'><span class='es-button-border' " 
                    +"style='border-style:solid;border-color:#2CB543;background:#FFFFFF;border-width:0px;display:inline-block;border-radius:0px;width:auto'><a class='es-button' target='_blank' style='mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#163A55;font-size:28px;border-style:solid;border-color:#FFFFFF;border-width:10px 35px 10px 35px;display:inline-block;background:#FFFFFF;border-radius:0px;font-family:helvetica, arial, verdana, sans-serif;font-weight:bold;font-style:normal;line-height:34px;width:auto;text-align:center'>!clave!</a></span></td></tr><tr class='es-mobile-hidden' style='border-collapse:collapse'>" 
                    +"<td align='center' height='90' style='padding:0;Margin:0'></td></tr><tr style='border-collapse:collapse'><td align='right' style='padding:0;Margin:0;padding-bottom:10px'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;line-height:27px;color:#FFFFFF;font-size:18px'></p></td></tr></table></td></tr></table></td></tr></table></td></tr></table><table cellpadding='0' cellspacing='0' class='es-content' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'>" 
                    +"<tr style='border-collapse:collapse'><td class='es-info-area' align='center' style='padding:0;Margin:0'><table bgcolor='#FFFFFF' class='es-content-body' align='center' cellpadding='0' cellspacing='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' valign='top' style='padding:0;Margin:0;width:560px'>" 
                    +"<table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' class='es-infoblock' style='padding:0;Margin:0;padding-top:5px;padding-bottom:5px;line-height:14px;font-size:12px;color:#EFEFEF'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;line-height:14px;color:#EFEFEF;font-size:12px'>Este correo ha sido generado automáticamente, por favor no responda a este remitente.<br>Sí posee problemas, por favor comuniquese a soporte@arquitectosalmacenes.com.sv<br></p>" 
                    +"</td></tr></table></td></tr></table></td></tr><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' valign='top' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'></table></td></tr></table></td></tr></table></td></tr></table><table cellpadding='0' " 
                    +"cellspacing='0' class='es-footer' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top'><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0'><table bgcolor='#ffffff' class='es-footer-body' align='center' cellpadding='0' cellspacing='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px'><tr style='border-collapse:collapse'><td align='left' " 
                    +"style='padding:0;Margin:0;padding-top:10px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' height='0' style='padding:0;Margin:0'></td></tr></table></td></tr></table></td></tr></table></td></tr></table></td></tr></table></div></body></html>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);
                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);
                if (respuesta)
                {
                    obj.Contrasenia = clave;
                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "Lo sentimos, no ha sido posible enviar el correo";
                    return 0;
                }
            } // CIERRE if (string.IsNullOrEmpty(Mensaje))
            else
            {
                return 0;
            }
        }

        public int RegistrarNuevosClientes(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool existeCorreo = new CN_Usuarios().VerificarCorreoExistencias(obj.Correo);
            bool existeUsuarioUnico = new CN_Usuarios().VerificarUsuarioUnicoExistencias(obj.Usuario);
            bool respuesta = false;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Lo sentimos, los nombres de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Lo sentimos, los apellidos de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Lo sentimos, el correo de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Usuario) || string.IsNullOrWhiteSpace(obj.Usuario))
            {
                Mensaje = "Lo sentimos, el usuario unico no puede estar vacio";
            }
            else if (existeCorreo)
            {
                Mensaje = "Lo sentimos, este correo ya se encuentra registrado";
                respuesta = false;
            }
            else if (existeUsuarioUnico)
            {
                Mensaje = "Lo sentimos, este usuario unico ya se encuentra en uso";
                respuesta = false;
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                // GENERAR CLAVE DE ACCESO ALEATORIA
                string clave = CN_Recursos.GenerarClave();
                // CUERPO DEL MENSAJE -> INCLUIDA PLANTILLA DE CORREO
                string asunto = "Creacion de nuevo usuario en nuestro sistema";
                string mensaje_correo = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>\r\n<html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0'><head><meta charset='UTF-8'><meta content='width=device-width, initial-scale=1' name='viewport'><meta name='x-apple-disable-message-reformatting'>\r\n " 
                    +" <meta http-equiv='X-UA-Compatible' content='IE=edge'><meta content='telephone=no' name='format-detection'><title>New year's eve</title><link href='https://fonts.googleapis.com/css2?family=Monoton&display=swap' rel='stylesheet'><style type='text/css'>#outlook a {padding:0;}.ExternalClass {width:100%;}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div {line-height:100%;}.es-button {text-decoration:none!important;}a[x-apple-data-detectors]" 
                    +"{color:inherit!important;text-decoration:none!important;font-size:inherit!important;font-family:inherit!important;font-weight:inherit!important;line-height:inherit!important;}.es-desk-hidden {display:none;float:left;overflow:hidden;width:0;max-height:0;line-height:0;}.es-button-border:hover a.es-button, .es-button-border:hover button.es-button {background:#e5e5e5!important;border-color:#e5e5e5!important;}.es-button-border:hover {border-color:#42d159 #42d159 #42d159 #42d159!important;background:#e5e5e5!important;}" 
                    +"[data-ogsb] .es-button {border-width:0!important;padding:10px 35px 10px 35px!important;}@media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150%!important } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:60px!important; text-align:center } h2 { font-size:28px!important; text-align:center } h3 { font-size:20px!important; text-align:center } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:60px!important } .es-header-body h2 a, " 
                    +".es-content-body h2 a, .es-footer-body h2 a { font-size:28px!important } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important } .es-menu td a { font-size:16px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:16px!important } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:18px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:16px!important }" 
                    +" .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class='gmail-fix'] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:inline-block!important }" 
                    +" .es-btn-fw { border-width:10px 0px!important; text-align:center!important } .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0px!important } .es-m-p0r { padding-right:0px!important } .es-m-p0l { padding-left:0px!important }" 
                    +" .es-m-p0t { padding-top:0px!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important } table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important }" 
                    +" table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } a.es-button, button.es-button { font-size:20px!important; display:inline-block!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important } }</style></head><body style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;font-family:helvetica, arial, verdana, sans-serif;padding:0;Margin:0'><div class='es-wrapper-color'" 
                    +" style='background-color:#301758'><table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#301758'><tr style='border-collapse:collapse'><td valign='top' style='padding:0;Margin:0'><table class='es-content' cellspacing='0' cellpadding='0' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'><tr style='border-collapse:collapse'>" 
                    +"<td align='center' style='padding:0;Margin:0'><table class='es-content-body' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#1c0b3f;border-top:2px dashed #ffffff;border-right:2px dashed #ffffff;border-left:2px dashed #ffffff;width:600px;border-bottom:2px dashed #ffffff' cellspacing='0' cellpadding='0' align='center' bgcolor='#1c0b3f'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;" 
                    +"background-image:url(http://arquitectosalmacenes.somee.com/ImgCorreos/5056978.png);background-repeat:no-repeat;background-position:center top' background='http://arquitectosalmacenes.somee.com/ImgCorreos/5056978.png'><table cellspacing='0' cellpadding='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td class='es-m-p0r' valign='top' align='center' style='padding:0;Margin:0;width:556px'><table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>" 
                    +"<tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-top:20px;padding-bottom:20px;font-size:0px'><img src='http://arquitectosalmacenes.somee.com/ImgCorreos/logo_administracion.png' alt style='display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic' height='27'></td></tr><tr class='es-mobile-hidden' style='border-collapse:collapse'><td align='center' height='90' style='padding:0;Margin:0'></td></tr><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-bottom:40px'><h1 style='Margin:0;line-height:86px;mso-line-height-rule:exactly;" 
                    +"font-family:monoton, cursive;font-size:72px;font-style:normal;font-weight:bold;color:#ffffff'>Bienvenido<br>estimado usuario<br></h1></td></tr><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-bottom:40px'><h2 style='Margin:0;line-height:29px;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;font-size:24px;font-style:normal;font-weight:normal;color:#ffffff'><b>Reciba una cordial bienvenida de parte de Arquitectos Almacenes S.A de C.V. Por favor inicie sesión e ingrese la clave presentada a continuación.&nbsp;</b></h2></td></tr><tr style='border-collapse:collapse'>" 
                    +"<td align='center' style='padding:0;Margin:0;padding-top:10px;padding-bottom:10px'><span class='es-button-border' style='border-style:solid;border-color:#2CB543;background:#FFFFFF;border-width:0px;display:inline-block;border-radius:0px;width:auto'><a class='es-button' target='_blank' style='mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#163A55;font-size:28px;border-style:solid;border-color:#FFFFFF;border-width:10px 35px 10px 35px;display:inline-block;background:#FFFFFF;border-radius:0px;font-family:helvetica, arial, verdana, sans-serif;" 
                    +"font-weight:bold;font-style:normal;line-height:34px;width:auto;text-align:center'>!clave!</a></span></td></tr><tr class='es-mobile-hidden' style='border-collapse:collapse'><td align='center' height='90' style='padding:0;Margin:0'></td></tr><tr style='border-collapse:collapse'><td align='right' style='padding:0;Margin:0;padding-bottom:10px'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;line-height:27px;color:#FFFFFF;font-size:18px'></p></td></tr></table></td></tr></table></td></tr></table></td></tr></table>" 
                    +"<table cellpadding='0' cellspacing='0' class='es-content' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'><tr style='border-collapse:collapse'><td class='es-info-area' align='center' style='padding:0;Margin:0'><table bgcolor='#FFFFFF' class='es-content-body' align='center' cellpadding='0' cellspacing='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px'><tr style='border-collapse:collapse'><td align='left' " 
                    +"style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' valign='top' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' class='es-infoblock' " 
                    +"style='padding:0;Margin:0;padding-top:5px;padding-bottom:5px;line-height:14px;font-size:12px;color:#EFEFEF'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;line-height:14px;color:#EFEFEF;font-size:12px'>Este correo ha sido generado automáticamente, por favor no responda a este remitente.<br>Sí posee problemas, por favor comuniquese a soporte@arquitectosalmacenes.com.sv<br></p></td></tr></table></td></tr></table></td></tr><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px'>" 
                    +"<table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' valign='top' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'></table></td></tr></table></td></tr></table></td></tr></table><table cellpadding='0' cellspacing='0' class='es-footer' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;" 
                    +"table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top'><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0'><table bgcolor='#ffffff' class='es-footer-body' align='center' cellpadding='0' cellspacing='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:10px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%' " 
                    +"style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' height='0' style='padding:0;Margin:0'></td></tr></table></td></tr></table></td></tr></table></td></tr></table></td></tr></table></div></body></html>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);
                respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);
                if (respuesta)
                {
                    obj.Contrasenia = clave;
                    return objCapaDato.RegistrarNuevosClientes(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "Lo sentimos, no ha sido posible enviar el correo";
                    return 0;
                }
            } // CIERRE if (string.IsNullOrEmpty(Mensaje))
            else
            {
                return 0;
            }
        }

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Lo sentimos, los nombres de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Lo sentimos, los apellidos de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Lo sentimos, el correo de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Usuario) || string.IsNullOrWhiteSpace(obj.Usuario))
            {
                Mensaje = "Lo sentimos, el usuario unico no puede estar vacios";
            }
            else if (obj.oRoles.IdRolUsuario == 0)
            {
                Mensaje = "Lo sentimos, debe seleccionar un rol de usuario";
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

        public bool EditarPerfilUsuarios(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Lo sentimos, los nombres de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Lo sentimos, los apellidos de este usuario no pueden estar vacios";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Lo sentimos, el correo de este usuario no pueden estar vacios";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.EditarPerfilUsuarios(obj, out Mensaje);
            }
            else
            {
                return false;
            }

        }

        // REGISTRO RUTA DE IMAGENES FOTO PERFIL USUARIOS
        public bool GuardarDatosImagen(Usuarios obj, out string Mensaje)
        {
            return objCapaDato.GuardarFotoPerfilUsuarios(obj, out Mensaje);
        }

        // RECUPERACION DE CUENTAS -> REESTABLECER CLAVES DE ACCESO USUARIOS Y CLIENTES

        public bool ReestablecerClave(int idusuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            // GENERAR CLAVE DE ACCESO ALEATORIA
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idusuario, nuevaclave, out Mensaje);
            if (resultado)
            {
                string asunto = "Recuperacion Cuentas";
                string mensaje_correo = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0'><head><meta charset='UTF-8'><meta content='width=device-width, initial-scale=1' name='viewport'><meta name='x-apple-disable-message-reformatting'>\r\n  <meta http-equiv='X-UA-Compatible' content='IE=edge'><meta content='telephone=no' name='format-detection'><title>New year's eve</title><link href='https://fonts.googleapis.com/css2?family=Monoton&display=swap' rel='stylesheet'>" 
                    +"<style type='text/css'>#outlook a {padding:0;}.ExternalClass {width:100%;}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div {line-height:100%;}.es-button {text-decoration:none!important;}a[x-apple-data-detectors]{color:inherit!important;text-decoration:none!important;font-size:inherit!important;font-family:inherit!important;font-weight:inherit!important;line-height:inherit!important;}.es-desk-hidden {display:none;float:left;overflow:hidden;width:0;max-height:0;line-height:0;}.es-button-border:hover a.es-button, .es-button-border:hover button.es-button {background:#e5e5e5!important;border-color:#e5e5e5!important;}" 
                    +".es-button-border:hover {border-color:#42d159 #42d159 #42d159 #42d159!important;background:#e5e5e5!important;}[data-ogsb] .es-button {border-width:0!important;padding:10px 35px 10px 35px!important;}@media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150%!important } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:60px!important; text-align:center } h2 { font-size:28px!important; text-align:center } h3 { font-size:20px!important; text-align:center } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:60px!important } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:28px!important } " 
                    +".es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important } .es-menu td a { font-size:16px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:16px!important } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:18px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:16px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class='gmail-fix'] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center!important }" 
                    +" .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:inline-block!important } .es-btn-fw { border-width:10px 0px!important; text-align:center!important } .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important }" 
                    +" .es-m-p0 { padding:0px!important } .es-m-p0r { padding-right:0px!important } .es-m-p0l { padding-left:0px!important } .es-m-p0t { padding-top:0px!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important } table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important }" 
                    +" table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } a.es-button, button.es-button { font-size:20px!important; display:inline-block!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important } }</style></head><body style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;font-family:helvetica, arial, verdana, sans-serif;padding:0;Margin:0'><div class='es-wrapper-color' style='background-color:#301758'><table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;" 
                    +"background-repeat:repeat;background-position:center top;background-color:#301758'><tr style='border-collapse:collapse'><td valign='top' style='padding:0;Margin:0'><table class='es-content' cellspacing='0' cellpadding='0' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0'><table class='es-content-body' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#1c0b3f;border-top:2px dashed #ffffff;border-right:2px dashed #ffffff;border-left:2px dashed #ffffff;width:600px;border-bottom:2px dashed #ffffff' cellspacing='0' cellpadding='0'" 
                    +" align='center' bgcolor='#1c0b3f'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;background-image:url(http://arquitectosalmacenes.somee.com/ImgCorreos/5056978.png);background-repeat:no-repeat;background-position:center top' background='http://arquitectosalmacenes.somee.com/ImgCorreos/5056978.png'><table cellspacing='0' cellpadding='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td class='es-m-p0r' valign='top' align='center' style='padding:0;Margin:0;width:556px'><table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>" 
                    +"<tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-top:20px;padding-bottom:20px;font-size:0px'><img src='http://arquitectosalmacenes.somee.com/ImgCorreos/logo_administracion.png' alt style='display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic' height='27'></td></tr><tr class='es-mobile-hidden' style='border-collapse:collapse'><td align='center' height='90' style='padding:0;Margin:0'></td></tr><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-bottom:40px'><h1 style='Margin:0;line-height:86px;mso-line-height-rule:exactly;font-family:monoton, cursive;font-size:72px;font-style:normal;font-weight:bold;color:#ffffff'>Recuperacion<br>Cuentas<br></h1></td></tr><tr style='border-collapse:collapse'>" 
                    +"<td align='center' style='padding:0;Margin:0;padding-bottom:40px'><h2 style='Margin:0;line-height:29px;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;font-size:24px;font-style:normal;font-weight:normal;color:#ffffff'><b>Estimado(a) usuario, el proceso de recuperación de cuentas se ha completado con éxito. Por favor inicie sesión e ingrese la clave presentada a continuación.&nbsp;</b></h2></td></tr><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-top:10px;padding-bottom:10px'><span class='es-button-border' style='border-style:solid;border-color:#2CB543;background:#FFFFFF;border-width:0px;display:inline-block;border-radius:0px;width:auto'><a class='es-button' target='_blank' style='mso-style-priority:100 !important;text-decoration:none;" 
                    +"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#163A55;font-size:28px;border-style:solid;border-color:#FFFFFF;border-width:10px 35px 10px 35px;display:inline-block;background:#FFFFFF;border-radius:0px;font-family:helvetica, arial, verdana, sans-serif;font-weight:bold;font-style:normal;line-height:34px;width:auto;text-align:center'>!clave!</a></span></td></tr><tr class='es-mobile-hidden' style='border-collapse:collapse'><td align='center' height='90' style='padding:0;Margin:0'></td></tr><tr style='border-collapse:collapse'><td align='right' style='padding:0;Margin:0;padding-bottom:10px'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;line-height:27px;color:#FFFFFF;font-size:18px'>" 
                    +"</p></td></tr></table></td></tr></table></td></tr></table></td></tr></table><table cellpadding='0' cellspacing='0' class='es-content' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'><tr style='border-collapse:collapse'><td class='es-info-area' align='center' style='padding:0;Margin:0'><table bgcolor='#FFFFFF' class='es-content-body' align='center' cellpadding='0' cellspacing='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%'" 
                    +" style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' valign='top' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' class='es-infoblock' style='padding:0;Margin:0;padding-top:5px;padding-bottom:5px;line-height:14px;font-size:12px;color:#EFEFEF'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;line-height:14px;color:#EFEFEF;font-size:12px'>" 
                    +"Este correo ha sido generado automáticamente, por favor no responda a este remitente.<br>Sí posee problemas, por favor comuniquese a soporte@arquitectosalmacenes.com.sv<br></p></td></tr></table></td></tr></table></td></tr><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' valign='top' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'></table>" 
                    +"</td></tr></table></td></tr></table></td></tr></table><table cellpadding='0' cellspacing='0' class='es-footer' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top'><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0'><table bgcolor='#ffffff' class='es-footer-body' align='center' cellpadding='0' cellspacing='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:10px;padding-left:20px;padding-right:20px'>" 
                    +"<table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' height='0' style='padding:0;Margin:0'></td></tr></table></td></tr></table></td></tr></table></td></tr></table></td></tr></table></div></body></html>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);
                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);
                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "Lo sentimos, no fue posible enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "Lo sentimos, no fue posible reestablecer la cuenta de este usuario";
                return false;
            }
        }

        // CAMBIAR CLAVE USUARIOS
        public bool CambiarClave(int idusuario, string nuevaclave, string correo, out string mensaje)
        {
            // ASUNTO DE CORREO
            string asunto = "Recuperacion Cuentas";
            // CUERPO DE CORREO -> MENSAJE QUE RECIBEN LOS USUARIOS [PLANTILLA DE CORREO INCLUIDA]
            string mensaje_correo = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0'>"
                +"<head><meta charset='UTF-8'><meta content='width=device-width, initial-scale=1' name='viewport'><meta name='x-apple-disable-message-reformatting'>\r\n  <meta http-equiv='X-UA-Compatible' content='IE=edge'><meta content='telephone=no' name='format-detection'><title>New year's eve</title><link href='https://fonts.googleapis.com/css2?family=Monoton&display=swap'"
                +"rel='stylesheet'><style type='text/css'>#outlook a {padding:0;}.ExternalClass {width:100%;}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div {line-height:100%;}.es-button {text-decoration:none!important;}a[x-apple-data-detectors]{color:inherit!important;"
                +"text-decoration:none!important;font-size:inherit!important;font-family:inherit!important;font-weight:inherit!important;line-height:inherit!important;}.es-desk-hidden {display:none;float:left;overflow:hidden;width:0;max-height:0;line-height:0;}.es-button-border:hover a.es-button, .es-button-border:hover button.es-button {background:#e5e5e5!important;border-color:#e5e5e5!important;}"
                +".es-button-border:hover {border-color:#42d159 #42d159 #42d159 #42d159!important;background:#e5e5e5!important;}[data-ogsb] .es-button {border-width:0!important;padding:10px 35px 10px 35px!important;}@media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150%!important } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:60px!important; text-align:center }"
                +"h2 { font-size:28px!important; text-align:center } h3 { font-size:20px!important; text-align:center } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:60px!important } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:28px!important } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important } .es-menu td a { font-size:16px!important }"
                +".es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:16px!important } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:18px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:16px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class='gmail-fix'] { display:none!important }"
                +".es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:inline-block!important } .es-btn-fw { border-width:10px 0px!important; text-align:center!important } .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right { width:100%!important }"
                +".es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0px!important } .es-m-p0r { padding-right:0px!important } .es-m-p0l { padding-left:0px!important } .es-m-p0t { padding-top:0px!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important }"
                +"tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important } table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } "
                +"table.es-social td { display:inline-block!important } a.es-button, button.es-button { font-size:20px!important; display:inline-block!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important } }</style></head><body style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;font-family:helvetica, arial, verdana, sans-serif;padding:0;Margin:0'><div class='es-wrapper-color' style='background-color:#301758'><table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0' "
                +"style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#301758'><tr style='border-collapse:collapse'><td valign='top' style='padding:0;Margin:0'><table class='es-content' cellspacing='0' cellpadding='0' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0'>" +"" +
                "<table class='es-content-body' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#1c0b3f;border-top:2px dashed #ffffff;border-right:2px dashed #ffffff;border-left:2px dashed #ffffff;width:600px;border-bottom:2px dashed #ffffff' cellspacing='0' cellpadding='0' align='center' bgcolor='#1c0b3f'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;background-image:url(http://arquitectosalmacenes.somee.com/ImgCorreos/5056978.png);background-repeat:no-repeat;background-position:center top' background='http://arquitectosalmacenes.somee.com/ImgCorreos/5056978.png'>" 
                +"<table cellspacing='0' cellpadding='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td class='es-m-p0r' valign='top' align='center' style='padding:0;Margin:0;width:556px'><table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-top:20px;padding-bottom:20px;font-size:0px'><img src='http://arquitectosalmacenes.somee.com/ImgCorreos/logo_administracion.png' alt style='display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic' height='27'>"
                +"</td></tr><tr class='es-mobile-hidden' style='border-collapse:collapse'><td align='center' height='90' style='padding:0;Margin:0'></td></tr><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-bottom:40px'><h1 style='Margin:0;line-height:86px;mso-line-height-rule:exactly;font-family:monoton, cursive;font-size:72px;font-style:normal;font-weight:bold;color:#ffffff'>Recuperacion<br>Cuentas<br></h1></td></tr><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-bottom:40px'><h2 style='Margin:0;line-height:29px;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;font-size:24px;font-style:normal;font-weight:normal;color:#ffffff'>"
                +"<b>Estimado(a) usuario, el proceso de recuperación de cuentas se ha completado con éxito. Por favor proceda a iniciar sesión con su nueva credencial de acceso&nbsp;</b></h2></td></tr><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0;padding-top:10px;padding-bottom:10px'><span class='es-button-border' style='border-style:solid;border-color:#2CB543;background:#FFFFFF;border-width:0px;display:inline-block;border-radius:0px;width:auto'><a class='es-button' target='_blank' " 
                +"style='mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#163A55;font-size:28px;border-style:solid;border-color:#FFFFFF;border-width:10px 35px 10px 35px;display:inline-block;background:#FFFFFF;border-radius:0px;font-family:helvetica, arial, verdana, sans-serif;font-weight:bold;font-style:normal;line-height:34px;width:auto;text-align:center'></a></span></td></tr><tr class='es-mobile-hidden' style='border-collapse:collapse'><td align='center' height='90' style='padding:0;Margin:0'></td></tr><tr style='border-collapse:collapse'><td align='right' style='padding:0;Margin:0;padding-bottom:10px'>" 
                +"<p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:helvetica, arial, verdana, sans-serif;line-height:27px;color:#FFFFFF;font-size:18px'></p></td></tr></table></td></tr></table></td></tr></table></td></tr></table><table cellpadding='0' cellspacing='0' class='es-content' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'><tr style='border-collapse:collapse'><td class='es-info-area' align='center' style='padding:0;Margin:0'><table bgcolor='#FFFFFF' class='es-content-body' align='center' cellpadding='0' cellspacing='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px'>" 
                +"<tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' valign='top' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' class='es-infoblock' style='padding:0;Margin:0;padding-top:5px;padding-bottom:5px;line-height:14px;font-size:12px;color:#EFEFEF'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;" 
                +"font-family:helvetica, arial, verdana, sans-serif;line-height:14px;color:#EFEFEF;font-size:12px'>Este correo ha sido generado automáticamente, por favor no responda a este remitente.<br>Sí posee problemas, por favor comuniquese a soporte@arquitectosalmacenes.com.sv<br></p></td></tr></table></td></tr></table></td></tr><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' valign='top' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'>" 
                +"</table></td></tr></table></td></tr></table></td></tr></table><table cellpadding='0' cellspacing='0' class='es-footer' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top'><tr style='border-collapse:collapse'><td align='center' style='padding:0;Margin:0'><table bgcolor='#ffffff' class='es-footer-body' align='center' cellpadding='0' cellspacing='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;padding-top:10px;padding-left:20px;padding-right:20px'><table cellpadding='0' cellspacing='0' width='100%'" 
                +" style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='left' style='padding:0;Margin:0;width:560px'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px'><tr style='border-collapse:collapse'><td align='center' height='0' style='padding:0;Margin:0'></td></tr></table></td></tr></table></td></tr></table></td></tr></table></td></tr></table></div></body></html>";
            bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);
            if (respuesta)
            {
                return objCapaDato.CambiarClave(idusuario, nuevaclave, out mensaje);
            }
            else
            {
                mensaje = "Lo sentimos, no fue posible enviar el correo";
                return false;
            }
            
        }

        // VERIFICAR SI CORREO REGISTRADO EXISTE EN BASE DE DATOS
        public bool VerificarCorreoExistencias(string Correo)
        {
            return objCapaDato.VerificarCorreoExistencias(Correo);
        }

        // VERIFICAR SI USUARIO UNICO REGISTRADO EXISTE EN BASE DE DATOS
        public bool VerificarUsuarioUnicoExistencias(string UsuarioUnico)
        {
            return objCapaDato.VerificarUsuarioUnicoExistencias(UsuarioUnico);
        }

    }
}

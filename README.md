# Ecommerce App - Arquitectos Almacenes

![ezgif-5-ed6a6a2e5d](https://user-images.githubusercontent.com/44457989/206928465-f9c6c61a-8a7d-4b6c-80a5-fc4e8142e851.gif)


<h1>Configuración Inicial</h1>

<p>Estimado(a) visitante, reciba una cordial bienvenida a este repositorio. Antes de iniciar y ejecutar este sistema en su servidor, debe realizar algunas configuraciones correspondientes para el óptimo funcionamiento del mismo, las cuales son las siguientes:


<h4>* Ajustes Webservice (WCF SOAP)</h4>

Parte de los procesos, se han realizado desde un webservice, es por ello que usted debe configurar el llamado de este servicio antes de iniciar esta aplicación. 

Por favor diríjase al explorador de la solución y ubique el siguiente apartado (marcado en color rosa y amarillo)

![wcf1](https://user-images.githubusercontent.com/44457989/206930001-27325e9f-00fd-4dbd-a5e1-746a3b058946.png)

Posteriormente haga clic derecho y luego haga clic en la opción <strong>Configurar referencia del servicio</strong>

![wcf2](https://user-images.githubusercontent.com/44457989/206930176-8e17f927-b020-48bd-b6e4-769b41e2c72f.png)


En el apartado <strong>Direccion: usted debe copiar la URL que ha sido generada al ejecutar el respectivo webservice. Sí usted decide ejecutarlo en un hosting. En su lugar debe hacer referencia a la dirección del hosting en cuestión, con el respectivo archivo .asmx en ejecución.</strong>


![wfc3](https://user-images.githubusercontent.com/44457989/206930505-702e542c-cb8c-4eaa-a7aa-6c49fed12e85.png)

Y para finalizar, y que todos los cambios anteriores tengan efecto, haga clic nuevamente en la referencia del servicio (Ver Figura 1 - Configuración Inicial WCF) y luego haga clic en Actualizar referencia del servicio. De esta manera el respectivo webservice está listo y preparado para comunicarse con la aplicación.

¿Qué sucede si se ha configurado mal? Simplemente algunos servicios no podrán ejecutarse, visualize el inicio de sesión e ingrese cualquier credencial. Si obtiene un mensaje de error como la figura, entiendase que el webservice aún no se ha comunicado exitosamente con la aplicación. Lea detenidamente los detalles anteriores y vuelva a intentarlo.


![wcf4](https://user-images.githubusercontent.com/44457989/206930941-2a11f468-1775-456d-806d-0d6c736c48ab.png)



<h4>* Cambio URL servidor (Web.config)</h4>

Por favor realice el cambio de la URL a la de su servidor, caso contrario los estilos y demás referencias de la aplicación no funcionaran. Misma situación lamentamos informarle que la aplicación no se ejecutará de la manera correcta.




![carbon](https://user-images.githubusercontent.com/44457989/206938295-2cff104a-94cb-4028-990f-ef48e3614183.png)



<h4>* Archivo de conexión (Web.config)</h4>

Usted debe de realizar los correspondientes cambios a la cadena de conexión, dicho archivo se llama <strong>Web.config</strong> en dónde Data Source= es el nombre de su servidor, Initial Catalog= el nombre de la base de datos, User ID=;Password= nombre de usuario y contraseña de acceso de su usuario configurado en su SGBD SQL SERVER. <strong>Único punto en dónde usted deberá conectar la base de datos a esta aplicación. NO se utilizan más cadenas de conexiones.</strong>

![carbon](https://user-images.githubusercontent.com/44457989/206928830-e1514021-0a5f-4b6f-a415-90c974488236.png)



<h4>* Conectividad API Pasarela de Pagos PayPal (Web.config)</h4>

Dentro del mismo archivo Web.config, usted encontrará la configuración general de la conexión hacia la API de PayPal. Para ello usted debe crear una cuenta en PayPal Developer ( https://developer.paypal.com ) y configurar su cuenta personal para utilizar el entorno de pruebas PayPal SandBox (Para mayor información específica de la creación de su cuenta, configuración y demás gestiones relacionadas, por favor visite la documentación oficial de PayPal Developer).


Una vez configurada su cuenta, usted debe ubicar los siguientes bloques de código, y sustituir por toda la información que la plataforma antes mencionada le solicita. Dónde ClientId= es su usuario encriptado generado por la plataforma y Secret= su contraseña de acceso generada por la plataforma. <strong>Por favor NO tocar UrlPaypal esa es la URL de comunicación con la API de los servicios de PayPal.</strong>

![carbon (1)](https://user-images.githubusercontent.com/44457989/206928946-1a44d43b-b43f-46dd-bf1b-924c2e87425e.png)




<h4>* URL Redireccionamiento Pagos Procesados / Fallidos - PayPal ( Controllers/HomeController.cs )</h4>


Por favor ubique el siguiente metodo <strong>public async Task<JsonResult> ProcesarPago(List<CarritoCompras> oListaCarrito, Venta oVenta)</strong> Dentro de la carpeta Controllers - Archivo llamado HomeController.cs <strong>Usted debe sustituir únicamente el https inicial el cuál corresponde a su servidor. Todo lo demás por favor NO MODIFICAR</strong> Caso contrario, si modifica de manera incorrecta, o bien no realiza el ajuste pertinente, obtendrá un error 404 al momento de finalizar la compra.


![carbon (2)](https://user-images.githubusercontent.com/44457989/206929034-57a0aa29-8538-4d11-b1e9-8311a17cd37a.png)




<h4>* Ajustes SMTP .NET - Envío de Correos Automáticos ( CapaNegocio/CN_Recursos.cs )</h4>

Usted debe de configurar el SMTP que funciona únicamente para enviar correos automáticos al momento de reestablecer su contraseña si cualquier usuario pierde el acceso a la plataforma. Por favor tome nota de la imagen citada abajo y realizar los respectivos cambios que los comentarios hacen mención. <strong>Únicamente necesita configurar ya sea su correo personal como receptor de envío de correos automáticos para aplicaciones, o bien un correo institucional real dentro de un servicio de hosting.</strong>

![carbon (4)](https://user-images.githubusercontent.com/44457989/206929313-3d306504-ad2e-4202-aead-90875e512b4c.png)


Y de esta manera hemos concluído con éxito toda la configuración necesaria para que este sistema funcione perfectamente en su servidor. Sí tiene problemas, por favor haga uso del Pull & Request o bien siga los pasos antes mencionados nuevamente.


<h4>* Ajustes API Climatológica</h4>

Se ha tomado a bien el compartir la respectiva API KEY de los servicios metereológicos de OpenWeather. Sí usted decide cambiar ubicación, o bien integrar una API de mejores prestaciones, debe leer la documentación oficial de OpenWeather para realizar dicho proceso. <strong>Por favor tome nota que dicha API se ha personalizado, es decir, no se utilizan las respectivas imagenes por defecto que la misma provee. Usted es libre de adecuarlo según sus necesidades. Además de estar válidado según el horario de la zona en que se ejecute esta aplicación.</strong>

![carbon (5)](https://user-images.githubusercontent.com/44457989/206929627-303fed50-8675-4fdb-ac04-fc2c21c549b9.png)



<h1>Información General:</h1>


Siguiendo todos los pasos anteriores, enhorabuena ʕ•́ᴥ•̀ʔっ usted ya tiene todo listo para ejecutar este proyecto en su servidor. A continuación se detallarán aspectos técnicos de esta aplicación.

<img src="https://user-images.githubusercontent.com/44457989/206931151-df0c445e-b3b6-4415-a80a-36e1ab2d4cac.png" width="300">


<h4>Datos generales de la empresa:</h4>

Arquitectos 
Almacenes S.A de C.V es el nombre de la 
empresa que desean implementar una 
tienda en línea para la venta de sus 
diferentes productos al público en 
general. Entre algunos de sus productos 
más destacados por categoría se 
encuentran: joyería, relojería, calzado, 
tecnología, línea blanca, 
electrodomésticos, belleza y juguetes. 
En la actualidad, sus ventas se limitan a ofrecer sus productos en volantes físicos a 
personas en la calle, redes sociales y afiches publicitarios en algunos periódicos de 
mayor circulación en el país. No cuentan con un sistema y página de ventas que les 
permita darse a conocer con la facilidad de tan solo un clic, menos la posibilidad de 
que sus clientes puedan comprar sus productos sin la necesidad de desplazarse a 
la tienda física. Es por ello que se ha abordado su problemática y se ha planteado 
la creación de un sitio web público, en dónde los clientes pueden visitar y ver cada 
uno de sus productos sin la necesidad de registrarse. Ahora bien, para añadir 
productos al carrito de compras y finalmente comprar los mismos, si necesitarían 
registrarse dentro de la plataforma, con cada uno de los datos personales de interés 
de cada uno de los clientes. En el área administrativa se tendrá todo el apartado 
lógico de la empresa, dónde usuarios según el rol asignado, podrán gestionar 
diversas tareas, por mencionar algunas como registrar productos, ver el detalle de 
ventas, ver los nuevos pedidos, ver el estatus de ciertos productos que poseen 
garantía que hayan sido vendidos. Será un proyecto sumamente completo, en 
dónde las necesidades de esta empresa, se solventarán dando un plus hacia la 
competencia y poder vender sus productos de manera fácil, rápida y segura.


<h4>Detalles técnicos:</h4>

En base a la breve historia, Arquitectos Almacenes es una tienda en línea dónde cada uno de sus clientes, pueden realizar compras de los productos que la empresa tiene en existencias al público en general. Dicho sistema se encuentra dividido en cinco roles de usuario los cuales son: Administradores, Presidencia, Gerencia, Atención al Cliente y Clientes. Dónde:

<h4>Roles de usuario administrativos:</h4>

Desde administradores hasta atención al cliente, son considerados roles de usuarios administrativos, este tipo de rol tiene acceso al panel de administración de la empresa, dónde pueden realizar tareas de mantenimientos específicas según su rol de usuario. Estos roles de usuario únicamente pueden ser registrados con autorización interna de la empresa

<h4>Roles de usuario clientes:</h4>

La tienda en línea tiene la disponibilidad de ser vista a todo el público, sin necesidad de registrarse. Ahora bien, si un cliente desea agregar productos al carrito de compras, realizar comprar y demás procesos que se le soliciten de un usuario, este debe registrarse en la plataforma. Cualquier persona puede registrarse respetando la condición de que debe poseer un correo y usuario único al momento de darse de alta.


<h4>Administradores</h4>
o Registro, consulta y gestiones de usuarios
<br>o Registro, consulta y gestiones de roles usuarios
<br>o Registro, consulta y gestiones de categorías de productos
<br>o Registro, consulta y gestiones de marcas de productos
<br>o Registro, consulta y gestiones de garantías de productos
<br>o Registro, consulta y gestiones de problemas de pedidos clientes
<br>o Registro, consulta y gestiones de problemas de plataforma
<br>o Consulta de ventas procesadas (clientes)
<br>o Consulta de vigencia de garantías de productos (ventas procesadas 
clientes)<br><br>


![menu1](https://user-images.githubusercontent.com/44457989/206931965-8e97b4ae-81bf-4e56-8d3e-33ec7f44aec1.png)



<h4>Presidencia</h4>

o Consulta de clientes registrados
<br>o Consulta de empleados registrados
<br>o Consulta y gestiones de categorías de productos
<br>o Consulta y gestiones de marcas de productos
<br>o Consulta y gestiones de productos
<br>o Consulta y gestiones de garantías de productos
<br>o Consulta de problemas pedidos clientes
<br>o Registro de problemas plataforma
<br>o Consulta de ventas procesadas (clientes)
<br>o Consulta de vigencia de garantías de productos (ventas procesadas 
clientes)
<br>o Usuarios autorizados, a través del cliente PayPal, podrán realizar 
gestiones de pedidos procesados, consulta de saldos y reembolsos<br><br>

![menu2](https://user-images.githubusercontent.com/44457989/206932024-6ba2383a-b469-43eb-b377-301e5ae25902.png)


<h4>Gerencia</h4>

o Consulta de clientes registrados
<br>o Consulta de empleados registrados
<br>o Consulta de categorías de productos
<br>o Consulta de marcas de productos
<br>o Consulta de productos
<br>o Consulta de problemas pedidos clientes
<br>o Registro de problemas plataforma
<br>o Consulta de ventas procesadas (clientes)
<br>o Consulta de vigencia de garantías de productos (ventas procesadas 
clientes)<br><br>

![menu3](https://user-images.githubusercontent.com/44457989/206932085-9fa492a1-8323-49c7-8c34-c670ac4d24db.png)


<h4>Atención al cliente</h4>

o Consulta de clientes registrados
<br>o Consulta de categorías de productos
<br>o Consulta de marcas de productos
<br>o Consulta de garantías de productos
<br>o Consulta de productos
<br>o Consulta de ventas procesadas (clientes)
<br>o Consulta de vigencia de garantías de productos (ventas procesadas 
clientes)<br><br>


![menu4](https://user-images.githubusercontent.com/44457989/206932178-51ecd0e2-4dca-4cf1-b396-938bba4bbe99.png)


<h4>Clientes</h4>

o Registro de nuevos clientes
<br>o Consulta de productos registrados, asociados según marcas y 
categorías de productos
<br>o Registro de productos, a través de un carrito de compras
<br>o Consulta y gestión de carrito de compras, dónde proporcionan datos 
personales y de envío de productos adquiridos
<br>o Procesar pagos de productos agregados en carrito de compras, a 
través de la pasarela de pagos PayPal<br><br>


![menu5](https://user-images.githubusercontent.com/44457989/206932248-591cf298-5993-4c39-9d6c-bce9622b5402.png)



- Todos los roles de usuarios, poseen un perfil de usuario, en el cual podrán 
modificar ciertos campos de interés personal, siempre respetando que 
algunos campos únicos, como lo son usuarios únicos, no podrán ser 
modificados a total discreción en el caso de empleados administrativos y 
clientes.

</p>

<p>Este sistema a nivel de código y base de datos se encuentra distribuido de la siguiente manera:<ul><li>Base de Datos (SQL SERVER):</li><ul><li>13 Tablas.</li><li>87 Procedimientos Almacenados.</li><li>33 Vistas.</li><li>1 Objeto SQL (CREATE TYPE).</li><li>2 Jobs (Eventos Automáticos).</li></ul></ul><ul><li>Sistema:</li><ul><li>Lenguaje de Programación C# (C Sharp) ASP.NET MVC5, Razor.</li><li>.NET FRAMEWORK 4.8</li><li>Patrón MVC (Modelo, Vista, Controlador).</li><li>Gestiones AJAX, JQuery, JSON.</li><li>Complementos JQuery, Javascript</li><li>Integración de Dos Plantillas Bootstrap.</li><li>Pasarela de Pagos API REST PayPal</li><li>WEBSERVICE SOAP (WCF)</li><li>Desarrollo basado en capas: Capa de Datos, Capa de Negocio y  Capa de Presentación</li><li>División de cinco roles de usuarios, los cuales son (administradores, presidencia, gerencia, atención al cliente y clientes).</li></ul></ul></p>
<p><b>Respecto a los eventos (Jobs), usted debe configurar su llamado desde el SQL SERVER AGENT y hacer referencia a los siguientes procedimientos almacenados:.</b>

![carbon (6)](https://user-images.githubusercontent.com/44457989/206932910-6cdc2fff-17b9-4059-9e73-3724a3544b1e.png)



</p>


<h2>¿Deseas probar la demo en vivo?</h2>


<p>Se ha habilitado un espacio dentro de un hosting gratuito para efectuar pruebas de este sistema, por favor tome en cuenta que la velocidad de respuesta así como su ancho de banda y cantidad de usuarios se ve limitada al ser un hosting gratuito. Puede acceder a la demo en el siguiente enlace: https://bsite.net/danr03/ A continación se detallan los datos de acceso para ingresar (puede hacerlo por medio de su usuario único o correo electrónico)<br><br>
<ul>
  <li>Administrador: usuario = karina.coca | correo: correo1@correo.com | clave: Lorem123$</li>
  <li>Presidencia: usuario = brahim.pino | correo: correo2@correo.com  | clave: Lorem123$</li>
  <li>Gerencia: usuario = 	yassine.corral | correo: correo3@correo.com  | clave: Lorem123$</li>
  <li>Atención al Cliente: usuario = 	saida.tapia | correo: correo4@correo.com  | clave: Lorem123$</li>
  <li>Clientes: usuario = 	mauricio.roca | correo: correo5@correo.com  | clave: Lorem123$</li>
  <li>Para la recepción de correos automáticos, debe establecer su correo real; de lo contrario lamentamos informarle que no podrá visualizar el contenido deseado.</li>
</ul>Accesos validados dentro de la plataforma, por favor tome nota de cada uno de los roles de usuario asignados.</p>


<h2>¿Datos usuario compra?</h2>

Para realizar una compra en la demo en vivo, por favor ingrese las siguientes credenciales en el cliente PayPal:

Usuario: sb-mid1u15170403@personal.example.com

Contraseña: KoA+18)@



<h2>Modelo Entidad Relación (m-ER) Base de Datos</h2>


![MerNuevo](https://user-images.githubusercontent.com/44457989/206933115-edaeceda-67bc-4b67-9098-076a248c464f.png)



<h2>Capturas</h2>

* Inicio tienda en línea (No inicio de sesión)


![c1](https://user-images.githubusercontent.com/44457989/206935296-962643fe-fc04-473f-bbed-d5417211088f.png)
![c2](https://user-images.githubusercontent.com/44457989/206935300-affc2f42-a5c2-4272-b10d-7449245006e4.png)
![c3](https://user-images.githubusercontent.com/44457989/206935303-9fefec8b-87a0-488d-8248-d36115e26bdc.png)
![c4](https://user-images.githubusercontent.com/44457989/206935305-8f308db5-cc92-4319-8b3a-477eb659a5f5.png)
![c5](https://user-images.githubusercontent.com/44457989/206935307-d51b9abc-b1e9-4b5a-ae52-f30ceea46f8e.png)
![c6](https://user-images.githubusercontent.com/44457989/206935309-78a2d2b5-c536-4c87-9347-508aff638087.png)
![c7](https://user-images.githubusercontent.com/44457989/206935311-6717c260-2877-4564-a818-05c4f04a62b4.png)
![c8](https://user-images.githubusercontent.com/44457989/206935312-f5376501-f063-4856-8a9e-40dd6271469c.png)
![c9](https://user-images.githubusercontent.com/44457989/206935317-73b105c2-1db3-482c-91be-82b6c5782801.png)
![c10](https://user-images.githubusercontent.com/44457989/206935318-65a3b61d-47dc-4b95-adb9-a35d6204b5e3.png)
![c11](https://user-images.githubusercontent.com/44457989/206935320-036dd71b-9483-4d21-98a3-c05d592b254b.png)

* Iniciar sesión

![c12](https://user-images.githubusercontent.com/44457989/206935445-6b371a24-9205-4f93-8cd8-c9d7323456f4.png)



* Inicio tienda en línea (Usuario ha iniciado sesión)


![c13](https://user-images.githubusercontent.com/44457989/206935557-96630724-2a99-4a54-95b6-ff98b9729264.png)
![c14](https://user-images.githubusercontent.com/44457989/206935558-4e40e3de-b0ae-4b54-8d84-47115919d75b.png)
![c15](https://user-images.githubusercontent.com/44457989/206935559-d79e9331-ffe8-45bf-894b-31e7bb0aa8ed.png)
![c16](https://user-images.githubusercontent.com/44457989/206935561-02b7bb9a-1256-4dfd-bb39-363fd62ee420.png)



* Detalles de productos


![Captura web_11-12-2022_173552_bsite net](https://user-images.githubusercontent.com/44457989/206935692-c103c220-e3b8-4c04-aba6-ed8b1d610d9d.jpeg)
![Captura web_11-12-2022_173628_bsite net](https://user-images.githubusercontent.com/44457989/206935694-7a89c44e-f3ac-4dd1-9244-fbde9c387516.jpeg)
![Captura web_11-12-2022_173653_bsite net](https://user-images.githubusercontent.com/44457989/206935695-4d342a08-2a9c-4bf8-beaa-f2b6ef2a0d90.jpeg)


* Producto sin stock (compra del mismo inhabilitado)

![Captura web_11-12-2022_182314_bsite net](https://user-images.githubusercontent.com/44457989/206938132-4c3b34c8-8888-49bf-a167-5388d94e3f30.jpeg)



* Vista de productos por categorías



![Captura web_11-12-2022_173856_bsite net](https://user-images.githubusercontent.com/44457989/206935908-7e35bfc8-25f6-48ae-ba2a-ebb9ddbd7ca2.jpeg)
![Captura web_11-12-2022_174147_bsite net](https://user-images.githubusercontent.com/44457989/206935951-4597db22-1e0a-4cd5-bd02-cb7a46c0f68d.jpeg)
![Captura web_11-12-2022_173926_bsite net](https://user-images.githubusercontent.com/44457989/206935911-66d94442-ab55-4ffe-b2f1-6d2632ce072a.jpeg)
![Captura web_11-12-2022_173939_bsite net](https://user-images.githubusercontent.com/44457989/206935912-8a384b0c-2cdd-4449-9675-b14c30601afa.jpeg)
![Captura web_11-12-2022_173957_bsite net](https://user-images.githubusercontent.com/44457989/206935914-8f5a384d-6cea-4b1f-b43a-767095c78aad.jpeg)
![Captura web_11-12-2022_174012_bsite net](https://user-images.githubusercontent.com/44457989/206935915-ab2e634d-06e9-4b83-911e-86e7b3fca0fd.jpeg)
![Captura web_11-12-2022_174022_bsite net](https://user-images.githubusercontent.com/44457989/206935916-0b2b754b-b7f6-43d2-bcb8-879f44dbcc8c.jpeg)
![Captura web_11-12-2022_174034_bsite net](https://user-images.githubusercontent.com/44457989/206935917-ab171c6d-40b3-4860-986f-745a893c9726.jpeg)




* Carrito de compras vacío

![Captura web_11-12-2022_174331_bsite net](https://user-images.githubusercontent.com/44457989/206936035-1e52a2f6-d2a7-4f98-b129-c3761a01f0b7.jpeg)



* Carrito de compras con productos registrados


![Captura web_11-12-2022_174640_bsite net](https://user-images.githubusercontent.com/44457989/206936228-52f70873-7196-47ae-b6d0-6c0be8678b1d.jpeg)




* Confirmación producto agregado carrito de compras

![confirmacioncompra](https://user-images.githubusercontent.com/44457989/206936085-833c7944-08a0-43cf-8b8f-611e8efc374e.png)



* Error agregar productos carrito de compras (Producto duplicado)


![errorcompra](https://user-images.githubusercontent.com/44457989/206936128-8bb65de6-4b5a-4fa0-9fd9-549e02f1844d.png)


* Proceso de compra



![procesocompra](https://user-images.githubusercontent.com/44457989/206936321-f59a29fe-7dcb-4ec3-a983-0e63d026ab17.png)


![paypal1](https://user-images.githubusercontent.com/44457989/206936458-3cd5b458-fa46-4efb-a8f3-a24c8305b751.png)
![paypal2](https://user-images.githubusercontent.com/44457989/206936460-437f4a13-4187-4740-bd29-c7c0db2fd906.png)
![paypal3](https://user-images.githubusercontent.com/44457989/206936461-5c032044-ea49-4351-a969-36df51dcfeff.png)
![paypal4](https://user-images.githubusercontent.com/44457989/206936462-4f4cd1cf-31d0-48f1-96b0-ea0538a3ab09.png)
![paypal5](https://user-images.githubusercontent.com/44457989/206936463-b0a2f88c-1e19-4dfd-9355-984344b13803.png)



* Compra fallida

![comprafallida](https://user-images.githubusercontent.com/44457989/206936527-14568e53-705c-41f1-a672-fb5283306ee9.png)


* Mis compras procesadas

![Captura web_11-12-2022_175356_bsite net](https://user-images.githubusercontent.com/44457989/206936614-17227fd0-e25b-4d44-8d73-ed1794462e45.jpeg)

![Captura web_11-12-2022_17554_bsite net](https://user-images.githubusercontent.com/44457989/206936654-1fdc8191-dced-48b7-a451-02875b250486.jpeg)


* Reportar problemas compras


![Captura web_11-12-2022_17568_bsite net](https://user-images.githubusercontent.com/44457989/206936718-4570e2ef-ad19-4912-a962-498aeb59a437.jpeg)

* Mi perfil (Clientes)


![Captura web_11-12-2022_175742_bsite net](https://user-images.githubusercontent.com/44457989/206936789-5510a0d5-69dc-4ecc-9549-127352b515a3.jpeg)


* F.A.Q Clientes

![Captura web_11-12-2022_18018_bsite net](https://user-images.githubusercontent.com/44457989/206936911-be46ff8f-b9a1-4806-85b7-2dc98a5b4d23.jpeg)




<h2>Panel de Administración</h2>


* Inicio administradores



![Captura web_11-12-2022_1820_bsite net](https://user-images.githubusercontent.com/44457989/206936991-b181352e-8cd6-47e3-9063-6e6d5e330db7.jpeg)


* Inicio Presidencia



![Captura web_11-12-2022_18411_bsite net](https://user-images.githubusercontent.com/44457989/206937146-37838e6e-9b15-4033-93ca-430e29f979fd.jpeg)


* Inicio Gerencia

![Captura web_11-12-2022_18441_bsite net](https://user-images.githubusercontent.com/44457989/206937154-115e8a09-3634-4245-a0f1-d14af65eda57.jpeg)



* Inicio Atención al Cliente

![Captura web_11-12-2022_18512_bsite net](https://user-images.githubusercontent.com/44457989/206937164-0682d920-d094-47a6-ba52-e58853285f7a.jpeg)

* Garantías de Productos


![Captura web_11-12-2022_18710_bsite net](https://user-images.githubusercontent.com/44457989/206937228-d96548f4-57d1-41bd-9603-00b9ff5e6af2.jpeg)


* Productos


![Captura web_11-12-2022_18747_bsite net](https://user-images.githubusercontent.com/44457989/206937256-f835863b-c627-494b-8000-81ed26e70866.jpeg)



* Productos sin stock

![Captura web_11-12-2022_18849_bsite net](https://user-images.githubusercontent.com/44457989/206937328-8f091dcb-38dd-4874-a312-451bea636d46.jpeg)


* Pedidos clientes (Compras procesadas)

![Captura web_11-12-2022_18945_bsite net](https://user-images.githubusercontent.com/44457989/206937370-bfc4f526-97ab-4426-b1c3-27abea80ab34.jpeg)



* Consulta general de ventas procesadas


![Captura web_11-12-2022_181026_bsite net](https://user-images.githubusercontent.com/44457989/206937402-fe399c5e-798d-4402-9142-64b8d6286319.jpeg)




* Consulta especifica de detalles de productos ventas procesadas



![Captura web_11-12-2022_181039_bsite net](https://user-images.githubusercontent.com/44457989/206937428-bf3039a9-5459-4a61-9f9d-280122733f48.jpeg)



* Problemas ventas


![Captura web_11-12-2022_181212_bsite net](https://user-images.githubusercontent.com/44457989/206937492-94365e6c-6861-47d1-accc-8061f49cdd98.jpeg)



* Perfil de usuarios administrativos



![Captura web_11-12-2022_181249_bsite net](https://user-images.githubusercontent.com/44457989/206937512-f40ec0a1-b3d4-439d-a243-09bcbb999975.jpeg)



<h2>Proceso recuperación de cuentas </h2>

- Si un usuario inicia el proceso de recuperación de cuentas, o bien, es nuevo usuario, debe cambiar obligatoriamente la clave generada automáticamente que le ha sido enviada a su correo. Caso contrario todas las funcionalidades del sistema quedan bloqueadas hasta que cumpla con el requisito anterior.

![Captura web_11-12-2022_181541_bsite net](https://user-images.githubusercontent.com/44457989/206937870-31a5692e-8e87-48a4-a83d-90d2aa6d9a61.jpeg)
![Captura web_11-12-2022_18174_bsite net](https://user-images.githubusercontent.com/44457989/206937872-ad1a3c98-2c88-4c61-a1d2-1b441a2db13f.jpeg)
![recuperacion1](https://user-images.githubusercontent.com/44457989/206937873-06fc7452-6b58-4fd2-bf4f-f99ee2834bf1.png)
![Captura web_11-12-2022_181810_bsite net](https://user-images.githubusercontent.com/44457989/206937876-04985391-e308-4d92-aaf3-ac5c829b1bf6.jpeg)
![Captura web_11-12-2022_181829_bsite net](https://user-images.githubusercontent.com/44457989/206937878-92a9e1ec-af81-41f6-a695-ed3784308a7a.jpeg)
![Captura web_11-12-2022_181840_bsite net](https://user-images.githubusercontent.com/44457989/206937879-952582f4-9de2-4202-a37a-bdeedc0b48b6.jpeg)
![recuperacion2](https://user-images.githubusercontent.com/44457989/206937925-17854594-765c-4128-a5c8-b1cc26f61c07.png)




</p>




<h2>Muchas gracias por obtener este repositorio hecho con muchas tazas de café ☕ ❤️</h2>



![CSharp](https://user-images.githubusercontent.com/44457989/70968018-afb2cf00-205d-11ea-9b79-2ff5a0a100ac.png)<br>




<h4>*** Fecha de Subida: 11 diciembre 2022 ***</h4>







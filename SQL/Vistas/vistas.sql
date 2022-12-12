-- CREACION DE VISTAS SQL


GO
CREATE VIEW vista_consultarlistadousuarios AS
SELECT Usuarios.IdUsuario, Usuarios.Nombres, Usuarios.Apellidos, Usuarios.Correo, Usuarios.Usuario, Usuarios.Reestablecer, Usuarios.Activo, Usuarios.FechaRegistro, Usuarios.Fotoperfil, Roles.IdRolUsuario, Roles.NombreRolUsuario 
FROM Usuarios INNER JOIN Roles Roles ON Usuarios.IdRolUsuario = Roles.IdRolUsuario;

CREATE VIEW vista_consultarclientesregistrados AS
SELECT Usuarios.IdUsuario, Usuarios.Nombres, Usuarios.Apellidos, Usuarios.Correo, Usuarios.Usuario, Usuarios.Reestablecer, Usuarios.Activo, Usuarios.FechaRegistro, Usuarios.Fotoperfil, Roles.IdRolUsuario, Roles.NombreRolUsuario 
FROM Usuarios INNER JOIN Roles Roles ON Usuarios.IdRolUsuario = Roles.IdRolUsuario WHERE Roles.IdRolUsuario=5;

CREATE VIEW vista_consultarempleadosregistrados AS
SELECT Usuarios.IdUsuario, Usuarios.Nombres, Usuarios.Apellidos, Usuarios.Correo, Usuarios.Usuario, Usuarios.Reestablecer, Usuarios.Activo, Usuarios.FechaRegistro, Usuarios.Fotoperfil, Roles.IdRolUsuario, Roles.NombreRolUsuario 
FROM Usuarios INNER JOIN Roles Roles ON Usuarios.IdRolUsuario = Roles.IdRolUsuario WHERE Roles.IdRolUsuario>=2 AND Roles.IdRolUsuario<=4;

GO
CREATE VIEW vista_consultarolesusuarios AS
SELECT * FROM Roles;

GO
CREATE VIEW vista_consultareportesproblemasplataforma AS
SELECT ProblemasPlataforma.IdReportePlataforma, ProblemasPlataforma.NombreReporte, ProblemasPlataforma.DescripcionReporte, ProblemasPlataforma.FechaRegistro,
ProblemasPlataforma.NombreImagenReporte, ProblemasPlataforma.EstadoReporte, ProblemasPlataforma.IdUsuario, usuarios.Usuario FROM ProblemasPlataforma INNER JOIN Usuarios usuarios ON usuarios.IdUsuario = ProblemasPlataforma.IdUsuario

GO
CREATE VIEW vista_consultareportesproblemasplataforma_activos AS
SELECT ProblemasPlataforma.IdReportePlataforma, ProblemasPlataforma.NombreReporte, ProblemasPlataforma.DescripcionReporte, ProblemasPlataforma.FechaRegistro,
ProblemasPlataforma.NombreImagenReporte, ProblemasPlataforma.EstadoReporte, ProblemasPlataforma.IdUsuario, usuarios.Usuario FROM ProblemasPlataforma INNER JOIN Usuarios usuarios ON usuarios.IdUsuario = ProblemasPlataforma.IdUsuario WHERE EstadoReporte='Activo'

GO
CREATE VIEW vista_consultareportesproblemasplataforma_encurso AS
SELECT ProblemasPlataforma.IdReportePlataforma, ProblemasPlataforma.NombreReporte, ProblemasPlataforma.DescripcionReporte, ProblemasPlataforma.FechaRegistro,
ProblemasPlataforma.NombreImagenReporte, ProblemasPlataforma.EstadoReporte, ProblemasPlataforma.IdUsuario, usuarios.Usuario FROM ProblemasPlataforma INNER JOIN Usuarios usuarios ON usuarios.IdUsuario = ProblemasPlataforma.IdUsuario WHERE EstadoReporte='En Curso'

GO
CREATE VIEW vista_consultareportesproblemasplataforma_resueltos AS
SELECT ProblemasPlataforma.IdReportePlataforma, ProblemasPlataforma.NombreReporte, ProblemasPlataforma.DescripcionReporte, ProblemasPlataforma.FechaRegistro,
ProblemasPlataforma.NombreImagenReporte, ProblemasPlataforma.EstadoReporte, ProblemasPlataforma.IdUsuario, usuarios.Usuario FROM ProblemasPlataforma INNER JOIN Usuarios usuarios ON usuarios.IdUsuario = ProblemasPlataforma.IdUsuario WHERE EstadoReporte='Resuelto'


GO
CREATE VIEW vista_consultareportesproblemasplataforma_cerrados AS
SELECT ProblemasPlataforma.IdReportePlataforma, ProblemasPlataforma.NombreReporte, ProblemasPlataforma.DescripcionReporte, ProblemasPlataforma.FechaRegistro,
ProblemasPlataforma.NombreImagenReporte, ProblemasPlataforma.EstadoReporte, ProblemasPlataforma.IdUsuario, usuarios.Usuario FROM ProblemasPlataforma INNER JOIN Usuarios usuarios ON usuarios.IdUsuario = ProblemasPlataforma.IdUsuario WHERE EstadoReporte='Cerrado'

GO
CREATE VIEW vista_consultalistadocategoriasproductos AS
SELECT * FROM Categorias
GO
CREATE VIEW vista_consultalistadomarcasproductos AS
SELECT * FROM Marcas
GO
CREATE VIEW vista_consultalistadogarantiaproductos AS
SELECT GarantiasProductos.IdControlGarantias, GarantiasProductos.IdMarcas, Marcas.Descripcion, GarantiasProductos.DuracionDiasGarantia, GarantiasProductos.DescripcionFabricanteGarantia, GarantiasProductos.Activo 
FROM GarantiasProductos INNER JOIN Marcas marcas ON GarantiasProductos.IdMarcas = Marcas.IdMarcas
GO
CREATE VIEW vista_consultalistadoproductosregistrados AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias
GO
CREATE VIEW vista_consultalistadoproductosregistrados_iniciotiendaenlinea AS
SELECT TOP 30 producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias
ORDER BY producto.FechaRegistro DESC
GO
CREATE VIEW vista_consultalistadoproductosregistrados_sinstock AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias
WHERE producto.Stock = 0
GO
CREATE VIEW vista_consultalistadoproductos_categoriajoyeria AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias WHERE categoria.IdCategorias=2
GO
CREATE VIEW vista_consultalistadoproductos_categoriarelojeria AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias WHERE categoria.IdCategorias=3
GO
CREATE VIEW vista_consultalistadoproductos_categoriacalzado AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias WHERE categoria.IdCategorias=4
GO
CREATE VIEW vista_consultalistadoproductos_categoriatecnologia AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias WHERE categoria.IdCategorias=5
GO
CREATE VIEW vista_consultalistadoproductos_categorialineablanca AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias WHERE categoria.IdCategorias=6
GO
CREATE VIEW vista_consultalistadoproductos_categoriaelectrodomesticos AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias WHERE categoria.IdCategorias=7
GO
CREATE VIEW vista_consultalistadoproductos_categoriabelleza AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias WHERE categoria.IdCategorias=8
GO
CREATE VIEW vista_consultalistadoproductos_categoriajuguetes AS
SELECT producto.IdProducto, producto.Nombre, producto.Descripcion[DescripcionProducto], marca.IdMarcas, marca.Descripcion,categoria.IdCategorias,categoria.CodigoCategoria,
garantias.IdControlGarantias, garantias.DuracionDiasGarantia, garantias.DescripcionFabricanteGarantia,producto.Precio,producto.Stock,producto.NombreImagen,producto.Activo FROM Productos producto 
INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas 
INNER JOIN Categorias categoria ON categoria.IdCategorias = producto.IdCategorias
INNER JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = producto.IdControlGarantias WHERE categoria.IdCategorias=9
GO
CREATE VIEW vista_listadocompleto_detallesventasprocesadasclientes AS
SELECT Detalle_Ventas.IdDetalleVenta, Detalle_Ventas.IdProducto, Ventas.IdVenta, ventas.IdUsuario, productos.Nombre, marcas.Descripcion, productos.Descripcion AS [DescripcionProducto], productos.NombreImagen, 
Detalle_Ventas.Cantidad, Detalle_Ventas.Total, ventas.IdTransaccion, garantias.DuracionDiasGarantia , ventas.FechaVenta, DATEDIFF(DAY,ventas.FechaVenta,GETDATE()) AS ContadorGarantias FROM Detalle_Ventas 
JOIN Ventas ventas ON Detalle_Ventas.IdVenta = ventas.IdVenta
JOIN Productos productos ON productos.IdProducto = Detalle_Ventas.IdProducto 
JOIN Marcas marcas ON marcas.IdMarcas = productos.IdMarcas
JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = productos.IdControlGarantias
GO
CREATE VIEW vista_obteneridtransaccion_ventasprocesadas_reportesproblemaspedidos AS
SELECT VENTAS.IdUsuario, Ventas.IdVenta, VENTAS.IdTransaccion, VENTAS.Dui, DATEDIFF(DAY,ventas.FechaVenta,GETDATE()) AS ContadorDias FROM VENTAS
GO
CREATE VIEW vista_listadoproblemascomprasclientes AS
SELECT ventas.IdUsuario, ventas.IdVenta, IdReporteProblema, usuarios.Nombres, usuarios.Apellidos, DescripcionProblema, FechaActualizacion, ReporteProblemasComprasClientes.FechaRegistro, 
ComentarioRetroalimentacion, Estado, ventas.IdTransaccion, ventas.Dui, ventas.TotalProducto, ventas.MontoTotal, ventas.Telefono, municipios.Descripcion AS [NombreMunicipio], departamentos.Descripcion AS [NombreDepartamento], 
ventas.Direccion FROM ReporteProblemasComprasClientes 
LEFT JOIN Ventas ventas ON ReporteProblemasComprasClientes.IdVenta = ventas.IdVenta 
JOIN Usuarios usuarios ON Ventas.IdUsuario = usuarios.IdUsuario
JOIN Municipios municipios ON municipios.IdMunicipios = ventas.IdMunicipios
JOIN Departamentos departamentos ON departamentos.IdDepartamento = municipios.IdDepartamento
GO
CREATE VIEW vista_listadoduiclientesventas AS
SELECT Dui, IdUsuario FROM Ventas GROUP BY IdUsuario, Dui
GO
CREATE VIEW vista_listadoventasprocesadas_administracion AS
SELECT IdVenta, IdUsuario, TotalProducto, MontoTotal, Nombres, Apellidos, Telefono, IdTransaccion, FechaVenta FROM Ventas
GO
CREATE VIEW vista_listadodetallesventasprocedadas_administracion AS
SELECT VENTAS.IdUsuario, Ventas.IdVenta,VENTAS.IdTransaccion, productos.Nombre, VENTAS.FechaVenta, marcas.Descripcion, garantias.DuracionDiasGarantia,
garantias.DuracionDiasGarantia - DATEDIFF(DAY,ventas.FechaVenta,GETDATE()) AS ContadorDias  FROM VENTAS
LEFT JOIN Detalle_Ventas detalleventas ON Ventas.IdVenta = detalleventas.IdVenta 
JOIN Productos productos ON detalleventas.IdProducto = productos.IdProducto
JOIN Marcas marcas ON marcas.IdMarcas = productos.IdMarcas
JOIN GarantiasProductos garantias ON garantias.IdControlGarantias = productos.IdControlGarantias
GO
CREATE VIEW vista_listadototalesventaspormes_ingresos AS
SELECT YEAR(FechaVenta) AS [yyyy], CONVERT(CHAR(6), FechaVenta, 112) AS [yyyymm], COUNT(*)  AS [TotalVentas], SUM(MontoTotal) AS MontoTotalVenta FROM Ventas
GROUP BY GROUPING SETS ((YEAR(FechaVenta)), CONVERT(CHAR(6), FechaVenta, 112))
GO
CREATE VIEW vista_listadopedidosclientes AS
SELECT IdVenta, IdTransaccion, Nombres, Apellidos, Dui, Telefono, Direccion, MontoTotal, TotalProducto, FechaVenta, EstadoPedido FROM Ventas 
LEFT JOIN Departamentos departamentos ON departamentos.IdDepartamento = Ventas.IdDepartamento 
JOIN Municipios municipios ON municipios.IdMunicipios = Ventas.IdMunicipios 
GO
CREATE VIEW vista_contadorhoras_registrocarritocompras AS
SELECT IdCarrito, IdUsuario, IdProducto, Cantidad, DATEDIFF(hour, FechaRegistro, GETDATE()) AS ContadorHorasRegistro FROM CarritoCompras
GO
CREATE VIEW vista_contadordias_problemasplataforma AS
SELECT DATEDIFF(DAY,FechaRegistro,GETDATE()) AS contador_dias, * FROM ProblemasPlataforma
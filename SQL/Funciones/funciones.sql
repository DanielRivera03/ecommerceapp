-- CREACION DE FUNCIONES
GO
create function fn_obtenerCarritoComprasCliente(
	@idusuario int
	)
	returns table 
	as
	return(
		SELECT producto. IdProducto, marca.Descripcion[DesMarca], producto.Nombre, producto.Precio, carrito.Cantidad,producto.NombreImagen FROM CarritoCompras carrito
		INNER JOIN Productos producto ON producto.IdProducto = carrito.IdProducto
		INNER JOIN Marcas marca ON marca.IdMarcas = producto.IdMarcas
		WHERE carrito.IdUsuario = @idusuario
	)
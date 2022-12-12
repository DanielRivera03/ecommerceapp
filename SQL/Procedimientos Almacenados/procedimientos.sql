-- CREACION DE PROCEDIMIENTOS ALMACENADOS

-- PROCEDIMIENTOS ALMACENADOS DE CONSULTAS
GO
CREATE PROCEDURE sp_ConsultarListadoUsuariosRegistrados
AS
BEGIN
	SELECT * FROM vista_consultarlistadousuarios
END
GO
CREATE PROCEDURE sp_ConsultarListadoClientesRegistrados
AS
BEGIN
	SELECT * FROM vista_consultarclientesregistrados
END
GO
CREATE PROCEDURE sp_ConsultarListadoEmpleadosRegistrados
AS
BEGIN
	SELECT * FROM vista_consultarempleadosregistrados
END
GO
CREATE PROCEDURE sp_ConsultarRolesUsuariosRegistrados
AS
BEGIN
	SELECT * FROM vista_consultarolesusuarios
END
GO
CREATE PROCEDURE sp_ConsultarReportesPloblemasPlataforma
AS
BEGIN
	SELECT * FROM vista_consultareportesproblemasplataforma
END
GO
CREATE PROCEDURE sp_ConsultarReportesPloblemasPlataformaActivos
AS
BEGIN
	SELECT * FROM vista_consultareportesproblemasplataforma_activos
END
GO
CREATE PROCEDURE sp_ConsultarReportesPloblemasPlataformaEnCurso
AS
BEGIN
	SELECT * FROM vista_consultareportesproblemasplataforma_encurso
END
GO
CREATE PROCEDURE sp_ConsultarReportesPloblemasPlataformaResueltos
AS
BEGIN
	SELECT * FROM vista_consultareportesproblemasplataforma_resueltos
END
GO
CREATE PROCEDURE sp_ConsultarReportesPloblemasPlataformaCerrados
AS
BEGIN
	SELECT * FROM vista_consultareportesproblemasplataforma_cerrados
END
GO
CREATE PROCEDURE sp_ConsultarListadoCompletoCategoriasProductos
AS
BEGIN
	SELECT * FROM vista_consultalistadocategoriasproductos
END
GO
CREATE PROCEDURE sp_ConsultarListadoCompletoMarcasProductos
AS
BEGIN
	SELECT * FROM vista_consultalistadomarcasproductos
END
GO
CREATE PROCEDURE sp_ConsultarListadoCompletoGarantiaProductos
AS
BEGIN
	SELECT * FROM vista_consultalistadogarantiaproductos
END
GO
CREATE PROCEDURE sp_ConsultarListadoCompletoProductosRegistrados
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductosregistrados
END
GO
CREATE PROCEDURE sp_ConsultarListadoCompletoProductosRegistrados_InicioTiendaEnLinea
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductosregistrados_iniciotiendaenlinea
END
GO
CREATE PROCEDURE sp_ConsultarListadoCompletoProductosRegistradosSinStock
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductosregistrados_sinstock
END


GO
CREATE PROCEDURE sp_ListadoArticulosCompra_CarritoComprasClientes(
@IdUsuario int
)
AS
BEGIN
	SELECT * FROM fn_obtenerCarritoComprasCliente(@IdUsuario)
END
GO
CREATE PROCEDURE sp_ListadoDepartementos
AS
BEGIN
	SELECT * FROM Departamentos
END
GO
CREATE PROCEDURE sp_ListadoMunicipios(
	@IdDepartamento varchar(10)
)
AS
BEGIN
	SELECT * FROM Municipios WHERE IdDepartamento = @IdDepartamento
END
GO
CREATE PROCEDURE sp_ConsultarListadoProductos_CategoriaJoyeria
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductos_categoriajoyeria
END
GO
CREATE PROCEDURE sp_ConsultarListadoProductos_CategoriaRelojeria
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductos_categoriarelojeria
END
GO
CREATE PROCEDURE sp_ConsultarListadoProductos_CategoriaCalzado
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductos_categoriacalzado
END
GO
CREATE PROCEDURE sp_ConsultarListadoProductos_CategoriaTecnologia
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductos_categoriatecnologia
END
GO
CREATE PROCEDURE sp_ConsultarListadoProductos_CategoriaLineaBlanca
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductos_categorialineablanca
END
GO
CREATE PROCEDURE sp_ConsultarListadoProductos_CategoriaElectrodomesticos
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductos_categoriaelectrodomesticos
END
GO
CREATE PROCEDURE sp_ConsultarListadoProductos_CategoriaBelleza
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductos_categoriabelleza
END
GO
CREATE PROCEDURE sp_ConsultarListadoProductos_CategoriaJuguetes
AS
BEGIN
	SELECT * FROM vista_consultalistadoproductos_categoriajuguetes
END
GO
CREATE PROCEDURE sp_ListadoVentasProcesadasClientes (
	@IdUsuario int
)
AS
BEGIN
	SELECT Ventas.IdVenta, Ventas.IdUsuario, Ventas.TotalProducto, Ventas.MontoTotal, Ventas.IdTransaccion, Ventas.FechaVenta FROM Ventas WHERE IdUsuario = @IdUsuario
END
GO
CREATE PROCEDURE sp_DetalleVentasProcesadasClientes(
	@IdUsuario int,
	@IdVenta int
)
AS
BEGIN
	SELECT * FROM vista_listadocompleto_detallesventasprocesadasclientes WHERE IdUsuario = @IdUsuario AND IdVenta = @IdVenta
END
GO
CREATE PROCEDURE sp_IdTransacciones_VentasProcesadas_ReportesProblemasPedidos(
	@IdUsuario int
)
AS
BEGIN
	SELECT * FROM vista_obteneridtransaccion_ventasprocesadas_reportesproblemaspedidos 
	WHERE vista_obteneridtransaccion_ventasprocesadas_reportesproblemaspedidos.ContadorDias <=7 AND IdUsuario = @IdUsuario
END
GO
CREATE PROCEDURE sp_ListadoProblemasComprasClientes
AS
BEGIN
	SELECT * FROM vista_listadoproblemascomprasclientes
END
GO
CREATE PROCEDURE sp_IdTransacciones_VentasProcesadas_ReportesProblemasPedidos_Administracion
AS
BEGIN
	SELECT * FROM vista_obteneridtransaccion_ventasprocesadas_reportesproblemaspedidos 
	WHERE vista_obteneridtransaccion_ventasprocesadas_reportesproblemaspedidos.ContadorDias <=60
END
GO
CREATE PROCEDURE sp_ListadoDuiRegistradosVentasClientes
AS
BEGIN
	SELECT * FROM vista_listadoduiclientesventas
END
GO
CREATE PROCEDURE sp_ListadoVentasProcesadas_PortalAdministracion
AS
BEGIN
	SELECT * FROM vista_listadoventasprocesadas_administracion
END
GO
CREATE PROCEDURE sp_listadodetallesventasprocesadas_administracion
AS
BEGIN
	SELECT IdTransaccion, IdUsuario, IdVenta, Nombre, FechaVenta, Descripcion, DuracionDiasGarantia, (SELECT IIF(ContadorDias>0, ContadorDias, 0)) AS ContadorDias
	FROM vista_listadodetallesventasprocedadas_administracion
END
CREATE PROCEDURE sp_ListadoOrdenesRegistradas
AS
BEGIN
	SELECT * FROM vista_listadopedidosclientes 
END
GO
CREATE PROCEDURE sp_ListadoOrdenesEnviadas
AS
BEGIN
	SELECT * FROM vista_listadopedidosclientes WHERE EstadoPedido = 'enviado'
END
GO
CREATE PROCEDURE sp_ListadoOrdenesEnProceso
AS
BEGIN
	SELECT * FROM vista_listadopedidosclientes WHERE EstadoPedido = 'en proceso'
END
GO
CREATE PROCEDURE sp_ListadoOrdenesCanceladas
AS
BEGIN
	SELECT * FROM vista_listadopedidosclientes WHERE EstadoPedido = 'cancelado'
END
GO
CREATE PROCEDURE sp_ListadoOrdenesEntregadas
AS
BEGIN
	SELECT * FROM vista_listadopedidosclientes WHERE EstadoPedido = 'entregado'
END
GO
-- SP PARA LA EJECUCION DE JOBS AUTOMATICOS
CREATE PROCEDURE sp_EliminarArticulosCarritoCompras_Clientes
AS
BEGIN
	DELETE FROM vista_contadorhoras_registrocarritocompras WHERE ContadorHorasRegistro >2
END
GO
CREATE PROCEDURE sp_CambioEstadoReportesProblemasPlataforma
AS
BEGIN
	UPDATE vista_contadordias_problemasplataforma SET EstadoReporte = 'Cerrado' WHERE contador_dias > 7
END

-- PROCEDIMIENTOS ALMACENADOS DE MANTENIMIENTOS Y TRANSACCIONALES
GO
CREATE PROCEDURE SP_ValidarCredencialesUsuarios
@Usuario varchar(255),
@Contrasenia varchar(255),
@Patron varchar(255)
As
Begin
    If Exists (Select * From Usuarios Where (Correo=@Usuario OR Usuario=@Usuario)
        And Convert(varchar(255),DecryptByPassPhrase(@Patron,Contrasenia))=@Contrasenia)
        Select * From Usuarios Where (Correo=@Usuario OR Usuario=@Usuario)
        And Convert(varchar(255),DecryptByPassPhrase(@Patron,Contrasenia))=@Contrasenia
    Else
        Select -1 IdUsuario -- CREDENCIALES NO EXISTENTES SI NO ENCUENTRA RESULTADOS
End

GO
create procedure sp_RegistrarUsuarios(
	@Nombres varchar(255),
	@Apellidos varchar(255),
	@Correo varchar(255),
	@Usuario varchar(255),
	@Contrasenia varchar(50),
	@Activo bit,
	@IdRolUsuario int,
	@Patron varchar(50),
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Usuarios WHERE Correo = @Correo)
	begin
	 INSERT INTO Usuarios(Nombres,Apellidos,Correo,Usuario,Contrasenia,Activo,IdRolUsuario) values (@Nombres,@Apellidos,@Correo,@Usuario,EncryptByPassPhrase(@Patron,@Contrasenia),@Activo,@IdRolUsuario)
	 SET @Resultado = scope_identity()
	end
	ELSE
		SET @Mensaje = 'Lo sentimos, el correo y/o usuario registrado ya existen'
end

GO
create procedure sp_EditarUsuarios(
	@IdUsuario int,
	@Nombres varchar(255),
	@Apellidos varchar(255),
	@Correo varchar(255),
	@Usuario varchar(255),
	@Activo bit,
	@IdRolUsuario int,
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Usuarios WHERE Correo = @Correo AND IdUsuario != @IdUsuario)
	begin
		UPDATE TOP(1) Usuarios SET
		Nombres = @Nombres,
		Apellidos = @Apellidos,
		Correo = @Correo,
		Usuario = @Usuario,
		Activo = @Activo,
		IdRolUsuario = @IdRolUsuario
		WHERE IdUsuario = @IdUsuario
		SET @Resultado = 1
	end
	else
	SET @Mensaje = 'Lo sentimos, el correo y/o usuario de esta persona ya existe' 
end
GO
create procedure sp_EditarPerfilUsuarios(
	@IdUsuario int,
	@Nombres varchar(255),
	@Apellidos varchar(255),
	@Correo varchar(255),
	@Contrasenia varchar(50),
	@Patron varchar(50),
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Usuarios WHERE Correo = @Correo AND IdUsuario != @IdUsuario)
	begin
		UPDATE TOP(1) Usuarios SET
		Nombres = @Nombres,
		Apellidos = @Apellidos,
		Correo = @Correo,
		Contrasenia = EncryptByPassPhrase(@Patron,@Contrasenia)
		WHERE IdUsuario = @IdUsuario
		SET @Resultado = 1
	end
	else
	SET @Mensaje = 'Lo sentimos, el correo y/o usuario de esta persona ya existe' 
end

GO
create procedure sp_GuardarImagenPerfilUsuarios(
	@IdUsuarios int,
	@Fotoperfil varchar(255),
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
begin
	SET @Resultado = 0
	UPDATE TOP(1) Usuarios SET Fotoperfil = @Fotoperfil WHERE IdUsuario = @IdUsuarios
	SET @Resultado = 1
end


GO
create procedure sp_RegistrarRolesUsuario(
	@NombreRolUsuario varchar(100),
	@DescripcionCortaRolUsuario varchar(50),
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
	begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Roles WHERE NombreRolUsuario = @NombreRolUsuario)
	begin
		INSERT INTO Roles (NombreRolUsuario,DescripcionCortaRolUsuario) VALUES (@NombreRolUsuario,@DescripcionCortaRolUsuario)
		SET @Resultado = scope_identity()
	end
	else
		SET @Mensaje = 'Lo sentimos, este rol de usuario ya existe'
	end

GO
create procedure sp_EditarRolesUsuario(
	@IdRolUsuario int,
	@NombreRolUsuario varchar(50),
	@DescripcionCortaRolUsuario varchar(100),
	@Mensaje varchar(500) output,
	@Resultado bit output
	)
	as
	begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Roles WHERE NombreRolUsuario = @NombreRolUsuario AND IdRolUsuario != @IdRolUsuario)
	begin
		UPDATE TOP(1) ROLES SET NombreRolUsuario = @NombreRolUsuario, DescripcionCortaRolUsuario = @DescripcionCortaRolUsuario WHERE IdRolUsuario = @IdRolUsuario
		SET @Resultado = 1
	end
	else
		SET @Mensaje = 'Lo sentimos, este rol de usuario ya existe'
	end

GO
create procedure sp_RegistrarProblemasPlataforma(
	@NombreReporte varchar(40),
	@DescripcionReporte varchar(255),
	@IdUsuario int,
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
	begin
		INSERT INTO ProblemasPlataforma (NombreReporte,DescripcionReporte,IdUsuario) VALUES (@NombreReporte,@DescripcionReporte,@IdUsuario)
		SET @Resultado = scope_identity()
	end

GO
create procedure sp_EditarProblemasPlataforma(
	@IdReportePlataforma int,
	@DescripcionReporte varchar(255),
	@EstadoReporte varchar(10),
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
begin
	SET @Resultado = 0
	UPDATE TOP(1) ProblemasPlataforma SET DescripcionReporte = @DescripcionReporte, EstadoReporte = @EstadoReporte WHERE IdReportePlataforma = @IdReportePlataforma
	SET @Resultado = 1
end

GO
create procedure sp_GuardarImagenProblemasPlataforma(
	@IdReportePlataforma int,
	@NombreImagenReporte varchar(255),
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
begin
	SET @Resultado = 0
	UPDATE TOP(1) ProblemasPlataforma SET NombreImagenReporte = @NombreImagenReporte WHERE IdReportePlataforma = @IdReportePlataforma
	SET @Resultado = 1
end

GO
create procedure sp_ReestablecerContraseniaUsuarios(
	@IdUsuario int,
	@Contrasenia varchar(50),
	@Patron varchar(50),
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Usuarios WHERE IdUsuario != IdUsuario)
	begin
		update usuarios set Contrasenia=EncryptByPassPhrase(@Patron,@Contrasenia), reestablecer = 1 where IdUsuario = @IdUsuario
		SET @Resultado = scope_identity()
	end
	else
		SET @Mensaje = 'Lo sentimos, no hay ningun usuario registrado con ese correo'
end

GO
create procedure sp_CambioContraseniaUsuarios(
	@IdUsuario int,
	@Contrasenia varchar(50),
	@Patron varchar(50),
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Usuarios WHERE IdUsuario != IdUsuario)
	begin
		update usuarios set Contrasenia=EncryptByPassPhrase(@Patron,@Contrasenia), reestablecer = 0 where IdUsuario = @IdUsuario
		SET @Resultado = scope_identity()
	end
	else
		SET @Mensaje = 'Lo sentimos, no hay ningun usuario registrado con ese correo'
end

GO
create procedure sp_VerificarCorreoUsuarios(
	@Correo varchar(255),
	@Resultado bit output
)
as
begin
	IF EXISTS (SELECT * FROM Usuarios WHERE Correo = @Correo)
		SET @Resultado = 1
	ELSE
		SET @Resultado = 0
end

GO
create procedure sp_VerificarUsuarioUnico(
	@Usuario varchar(255),
	@Resultado bit output
)
as
begin
	IF EXISTS (SELECT * FROM Usuarios WHERE Usuario = @Usuario)
		SET @Resultado = 1
	ELSE
		SET @Resultado = 0
end

GO
create procedure sp_ListarMarcasPorCategorias (
	@idcategoria int
)
AS
BEGIN
select distinct Marcas.IdMarcas, Marcas.Descripcion from Productos Productos
inner join categorias Categorias on Categorias.IdCategorias = Productos.IdCategorias
inner join marcas Marcas on Marcas.IdMarcas = Productos.IdMarcas and  Marcas.Activo = 1
where Categorias.IdCategorias = iif(@idcategoria = 0, Categorias.IdCategorias, @idcategoria)
END

GO
create procedure sp_RegistrarCategorias(
	@CodigoCategoria varchar(40),
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
	begin
		SET @Resultado = 0
		IF NOT EXISTS (SELECT * FROM Categorias WHERE CodigoCategoria = @CodigoCategoria)
		begin
			INSERT INTO Categorias (CodigoCategoria,Descripcion) VALUES (@CodigoCategoria,@Descripcion)
			SET @Resultado = scope_identity()
		end
		else
			SET @Mensaje = 'Lo sentimos, esta categoria ya existe'
	end
GO
create procedure sp_ModificarCategorias(
	@IdCategorias int,
	@CodigoCategoria varchar(40),
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
	)
	as
	begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Categorias WHERE CodigoCategoria = @CodigoCategoria AND IdCategorias != @IdCategorias)
	begin
		UPDATE TOP(1) Categorias SET CodigoCategoria = @CodigoCategoria, Descripcion = @Descripcion, Activo = @Activo WHERE IdCategorias = @IdCategorias
		SET @Resultado = 1
	end
	else
		SET @Mensaje = 'Lo sentimos, esta categoria ya existe'
	end

GO
create procedure sp_VerificarCodigoUnicoCategorias(
	@CodigoCategoria varchar(40),
	@Resultado bit output
)
as
begin
	IF EXISTS (SELECT * FROM Categorias WHERE CodigoCategoria = @CodigoCategoria)
		SET @Resultado = 1
	ELSE
		SET @Resultado = 0
end

GO
create procedure sp_RegistrarMarcas(
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
	begin
		SET @Resultado = 0
		IF NOT EXISTS (SELECT * FROM Marcas WHERE Descripcion = @Descripcion)
		begin
			INSERT INTO Marcas(Descripcion, Activo) VALUES (@Descripcion,@Activo)
			SET @Resultado = scope_identity()
		end
		else
			SET @Mensaje = 'Lo sentimos, esta marca ya existe'
	end

GO
create procedure sp_ModificarMarcas(
	@IdMarcas int,
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
	)
	as
	begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Marcas WHERE Descripcion = @Descripcion AND IdMarcas != @IdMarcas)
	begin
		UPDATE TOP(1) Marcas SET Descripcion = @Descripcion, Activo = @Activo WHERE IdMarcas = @IdMarcas
		SET @Resultado = 1
	end
	else
		SET @Mensaje = 'Lo sentimos, esta marca ya existe'
	end


GO
create procedure sp_VerificarNombreMarca(
	@Descripcion varchar(100),
	@Resultado bit output
)
as
begin
	IF EXISTS (SELECT * FROM Marcas WHERE Descripcion = @Descripcion)
		SET @Resultado = 1
	ELSE
		SET @Resultado = 0
end

GO
create procedure sp_RegistrarGarantiasProductos(
	@IdMarcas int,
	@DuracionDiasGarantia int,
	@DescripcionFabricanteGarantia varchar(500),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
	begin
		SET @Resultado = 0
		IF NOT EXISTS (SELECT * FROM GarantiasProductos WHERE IdMarcas = @IdMarcas)
		begin
			INSERT INTO GarantiasProductos(IdMarcas, DuracionDiasGarantia, DescripcionFabricanteGarantia,Activo) VALUES (@IdMarcas,@DuracionDiasGarantia,@DescripcionFabricanteGarantia,@Activo)
			SET @Resultado = scope_identity()
		end
		else
			SET @Mensaje = 'Lo sentimos, ya existe una garantia asociada a este producto'
	end

GO
create procedure sp_ModificarGarantiasProductos(
	@IdControlGarantias int,
	@IdMarcas int,
	@DuracionDiasGarantia int,
	@DescripcionFabricanteGarantia varchar(500),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
	begin
		SET @Resultado = 0
		IF NOT EXISTS (SELECT * FROM GarantiasProductos WHERE IdMarcas = @IdMarcas AND IdControlGarantias != @IdControlGarantias)
		begin
			UPDATE TOP(1) GarantiasProductos SET IdMarcas = @IdMarcas, DuracionDiasGarantia = @DuracionDiasGarantia, DescripcionFabricanteGarantia = @DescripcionFabricanteGarantia, Activo = @Activo WHERE IdControlGarantias = @IdControlGarantias
			SET @Resultado = 1
		end
		else
			SET @Mensaje = 'Lo sentimos, ya existe una garantia asociada a este producto'
	end

GO
create procedure sp_EliminarGarantiasProductos(
	@IdControlGarantias int,
	@Mensaje varchar(500) output,
	@Resultado bit output
	)
	as
	begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM GarantiasProductos WHERE IdControlGarantias != IdControlGarantias)
	begin
		DELETE TOP (1) FROM GarantiasProductos WHERE IdControlGarantias = @IdControlGarantias
		SET @Resultado = 1
	end
	else
		SET @Mensaje = 'Lo sentimos, no fue posible eliminar esta garantia'
	end

create procedure sp_VerificarExistenciasMarcas_GarantiasProductos(
	@IdMarcas int,
	@Resultado bit output
)
as
begin
	IF EXISTS (SELECT * FROM GarantiasProductos WHERE IdMarcas = @IdMarcas)
		SET @Resultado = 1
	ELSE
		SET @Resultado = 0
end

	select * from GarantiasProductos

GO
create procedure sp_RegistrarProductos(
	@Nombre varchar(500),
	@Descripcion varchar(500),
	@IdMarcas int,
	@IdCategorias int,
	@IdControlGarantias int,
	@Precio money,
	@Stock int,
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Productos WHERE Nombre = @Nombre)
	begin
		INSERT INTO Productos(Nombre,Descripcion,IdMarcas,IdCategorias,IdControlGarantias,Precio,Stock,Activo) VALUES (@Nombre,@Descripcion,@IdMarcas,@IdCategorias,@IdControlGarantias,@Precio,@Stock,@Activo)
		SET @Resultado = scope_identity()
	end
	else
		SET @Mensaje = 'Lo sentimos, el producto ya existe'
end

GO
create procedure sp_ModificarProducto(
	@IdProducto int,
	@Nombre varchar(100),
	@Descripcion varchar(500),
	@IdMarcas int,
	@IdCategorias int,
	@IdControlGarantias int,
	@Precio money,
	@Stock int,
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Productos WHERE Nombre = @Nombre AND IdProducto != @IdProducto)
	begin
		UPDATE Productos SET Nombre = @Nombre, Descripcion = @Descripcion, IdMarcas = @IdMarcas, IdCategorias = @IdCategorias, IdControlGarantias = @IdControlGarantias, Precio = @Precio, Stock = @Stock, Activo = @Activo WHERE IdProducto = @IdProducto
		SET @Resultado = 1
	end
	else
		SET @Mensaje = 'Lo sentimos, el producto ya existe'
end

create procedure sp_GuardarImagenProductos(
	@IdProducto int,
	@NombreImagen varchar(255),
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
begin
	SET @Resultado = 0
	UPDATE TOP(1) Productos SET NombreImagen = @NombreImagen WHERE IdProducto = @IdProducto
	SET @Resultado = 1
end

GO
create procedure sp_RegistroProductos_CarritoCompras(
	@IdUsuario int,
	@IdProducto int,
	@Sumar bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
as 
begin
	SET @Resultado = 1
	SET @Mensaje = ''
	declare @existecarrito bit = iif(exists(SELECT * FROM CarritoCompras WHERE IdUsuario = @IdUsuario and Idproducto = @IdProducto),1,0)
	declare @stockproducto int = (SELECT Stock FROM Productos WHERE IdProducto = @IdProducto) 
	BEGIN TRY
		BEGIN TRANSACTION OPERACION
			if(@Sumar = 1)
				begin
				if(@stockproducto > 0)
					begin
					if(@existecarrito = 1)
						UPDATE CarritoCompras SET Cantidad = Cantidad + 1 WHERE IdUsuario = @IdUsuario and IdProducto = @IdProducto
					else
						INSERT INTO CarritoCompras (IdUsuario, IdProducto, Cantidad) VALUES (@IdUsuario, @IdProducto, 1)
						UPDATE Productos SET Stock = Stock - 1 WHERE IdProducto = @IdProducto
					end
				else
					begin
						SET @Resultado = 0
						SET @Mensaje = 'Lo sentimos, este producto ya no cuenta con stock disponible'
					end
				end
				else
					begin
						UPDATE CarritoCompras SET Cantidad = Cantidad - 1 WHERE IdUsuario = @IdUsuario and IdProducto = @IdProducto
						UPDATE Productos SET Stock = Stock + 1 WHERE IdProducto = @IdProducto
					end
				COMMIT TRANSACTION OPERACION
			END TRY
		BEGIN CATCH
			SET @Resultado = 0
			SET @Mensaje = ERROR_MESSAGE()
			ROLLBACK TRANSACTION OPERACION
		END CATCH
end

GO
create procedure sp_EliminarProductos_CarritoCompras(
	@IdUsuario int,
	@IdProducto int,
	@Resultado bit output
)
as
begin
	SET @Resultado = 1
	declare @cantidadproducto int = (SELECT cantidad FROM CarritoCompras WHERE IdUsuario = @IdUsuario and IdProducto = @IdProducto)
	BEGIN TRY
		BEGIN TRANSACTION OPERACION
			UPDATE Productos SET Stock = Stock + @cantidadproducto WHERE IdProducto = @IdProducto
			DELETE TOP(1) FROM CarritoCompras WHERE IdUsuario = @IdUsuario and IdProducto = @IdProducto
		COMMIT TRANSACTION OPERACION
	END TRY
	BEGIN CATCH 
		SET @Resultado = 0
		ROLLBACK TRANSACTION OPERACION
	END CATCH
end


GO
create procedure sp_ComprobarExistencias_CarritoCompras(
	@IdUsuario int,
	@IdProducto int,
	@Resultado bit output
)
as
begin
	IF EXISTS (SELECT * FROM CarritoCompras WHERE IdUsuario = @IdUsuario and IdProducto = @IdProducto)
		SET @Resultado = 1
	ELSE
		SET @Resultado = 0
end
GO
create procedure sp_ComprobarStockProductos_CarritoCompras(
	@IdProducto int,
	@Resultado bit output
)
as
begin
	IF EXISTS (SELECT * FROM Productos WHERE Stock <= 0 and IdProducto = @IdProducto)
		SET @Resultado = 1
	ELSE
		SET @Resultado = 0
end

GO
create procedure sp_ListadoProductosCarritoComprasClientes_CantidadArticulos(
	@IdUsuario int
)
as
begin
	select SUM(CarritoCompras.Cantidad) AS CantidadArticulos from CarritoCompras INNER JOIN Productos productos ON productos.IdProducto = CarritoCompras.IdProducto where IdUsuario = @IdUsuario
end

GO
create procedure sp_ListadoProductosCarritoComprasClientes_TotalCancelar(
	@IdUsuario int
)
as
begin
	select SUM(CarritoCompras.Cantidad * productos.Precio) AS TotalCancelar from CarritoCompras INNER JOIN Productos productos ON productos.IdProducto = CarritoCompras.IdProducto where IdUsuario = @IdUsuario
end

GO
create procedure sp_RegistrarVentasClientes(
	@IdUsuario int,
	@IdDepartamento varchar(10),
	@IdMunicipios int,
	@TotalProducto int,
	@MontoTotal money,
	@Nombres varchar(255),
	@Apellidos varchar(255),
	@Dui varchar(10),
	@Telefono varchar(10),
	@Direccion varchar(500),
	@IdTransaccion varchar(100),
	@DetalleVenta [EDetalle_Venta] READONLY,
	@Resultado bit output,
	@Mensaje varchar(500) output
)
as
begin
	begin try
		declare @idventa int = 0 
		set @Resultado = 1
		set @Mensaje = ''
		begin transaction registro
			insert into Ventas(IdUsuario,IdDepartamento,IdMunicipios,TotalProducto,MontoTotal,Nombres,Apellidos,Dui,Telefono,Direccion,IdTransaccion) 
			VALUES (@IdUsuario,@IdDepartamento,@IdMunicipios,@TotalProducto,@MontoTotal,@Nombres,@Apellidos,@Dui,@Telefono,@Direccion,@IdTransaccion)
			set @idventa = SCOPE_IDENTITY()
			insert into Detalle_Ventas(IdVenta, IdProducto, Cantidad, Total) SELECT @idventa,IdProducto,Cantidad,Total FROM @DetalleVenta
			delete from CarritoCompras where IdUsuario = @IdUsuario
		commit transaction registro
	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
	end catch
end


GO
create procedure sp_ModificarEstadoPedidosClientes(
	@IdVenta int,
	@EstadoPedido varchar(20),
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
begin
	SET @Resultado = 0
	UPDATE TOP(1) Ventas SET EstadoPedido = @EstadoPedido WHERE IdVenta = @IdVenta
	SET @Resultado = 1
end


GO
create procedure sp_RegistrarProblemasComprasClientes(
	@IdUsuario int,
	@IdVenta int,
	@DescripcionProblema varchar(500),
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
	begin
		SET @Resultado = 0
		IF NOT EXISTS (SELECT * FROM ReporteProblemasComprasClientes WHERE IdVenta = @IdVenta)
		begin
			INSERT INTO ReporteProblemasComprasClientes(IdUsuario,IdVenta,DescripcionProblema) VALUES (@IdUsuario,@IdVenta,@DescripcionProblema)
			SET @Resultado = scope_identity()
		end
		else
			SET @Mensaje = 'Lo sentimos, ya existe un reporte asociado a esta venta'
	end

GO
create procedure sp_ModificarProblemasComprasClientes(
	@IdReporteProblema int,
	@ComentarioRetroalimentacion varchar(500),
	@Estado bit,
	@Mensaje varchar(500) output,
	@Resultado int output
	)
	as
	begin
		SET @Resultado = 0
		IF EXISTS (SELECT * FROM ReporteProblemasComprasClientes WHERE IdReporteProblema = IdReporteProblema)
		begin
			UPDATE TOP(1) ReporteProblemasComprasClientes SET ComentarioRetroalimentacion = @ComentarioRetroalimentacion, Estado = @Estado, FechaActualizacion = GETDATE() WHERE IdReporteProblema = @IdReporteProblema
			SET @Resultado = 1
		end
		else
			SET @Mensaje = 'Lo sentimos, ya existe un reporte asociado a esta venta'
	end

GO
create procedure sp_ComprobarExistencias_ReportesProblemasVentas(
	@IdVenta int,
	@Resultado bit output
)
as
begin
	IF EXISTS (SELECT * FROM ReporteProblemasComprasClientes WHERE IdVenta = @IdVenta)
		SET @Resultado = 1
	ELSE
		SET @Resultado = 0
end

GO
create procedure sp_ReportePanelAdministracion_Administradores
as
begin
SELECT
(SELECT COUNT(*) FROM Usuarios) [TotalUsuarios],
(SELECT COUNT(*) FROM Productos) [TotalProductos],
(SELECT COUNT(*) FROM GarantiasProductos) [TotalGarantias],
(SELECT COUNT(*) FROM vista_consultarclientesregistrados) [TotalClientes]
end

GO
create procedure sp_ReportePanelAdministracion_Presidencia
as
begin
SELECT
(SELECT COUNT(*) FROM Usuarios WHERE IdRolUsuario >=2 AND IdRolUsuario <=4) [TotalEmpleados],
(SELECT COUNT(*) FROM Usuarios WHERE IdRolUsuario = 5) [TotalClientes],
(SELECT COUNT(*) FROM Productos) [TotalProductos],
(SELECT COUNT(*) FROM Ventas) [TotalVentas]
end

GO
create procedure sp_ReportePanelAdministracion_Gerencia
as
begin
SELECT
(SELECT COUNT(*) FROM Usuarios WHERE IdRolUsuario >=2 AND IdRolUsuario <=4) [TotalEmpleados],
(SELECT COUNT(*) FROM Usuarios WHERE IdRolUsuario = 5) [TotalClientes],
(SELECT COUNT(*) FROM Productos) [TotalProductos],
(SELECT SUM(Cantidad) FROM Detalle_Ventas) [TotalProductosVendidos]
end

GO
create procedure sp_ReporteCantidadProductosPorCategoria
as
begin
SELECT
(SELECT COUNT(*) FROM Productos WHERE IdCategorias=2) [TotalJoyeria],
(SELECT COUNT(*) FROM Productos WHERE IdCategorias=3) [TotalRelojeria],
(SELECT COUNT(*) FROM Productos WHERE IdCategorias=4) [TotalCalzado],
(SELECT COUNT(*) FROM Productos WHERE IdCategorias=5) [TotalTecnologia],
(SELECT COUNT(*) FROM Productos WHERE IdCategorias=6) [TotalLineaBlanca],
(SELECT COUNT(*) FROM Productos WHERE IdCategorias=7) [TotalElectrodomesticos],
(SELECT COUNT(*) FROM Productos WHERE IdCategorias=8) [TotalBelleza],
(SELECT COUNT(*) FROM Productos WHERE IdCategorias=9) [TotalJuguetes]
end

exec sp_ReporteCantidadProductosPorCategoria

GO
create procedure sp_ReporteVentasPorMes(
	@AnioActual int
)
AS
BEGIN 
	-- CAMBIAR POR A�O EN CURSO
	SET @AnioActual = 2022
	-- IGUALMENTE CONDICION DE MESES yyyymm='202201' -> YYYY?? <- CAMBIAR POR A�O EN CURSO
	SELECT
	-- ENERO
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202201' AND @AnioActual = @AnioActual),0)) [TotalEnero],
	-- FEBRERO
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202202' AND @AnioActual = @AnioActual),0)) [TotalFebrero],
	-- MARZO
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202203' AND @AnioActual = @AnioActual),0)) [TotalMarzo],
	-- ABRIL
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202204' AND @AnioActual = @AnioActual),0)) [TotalAbril],
	-- MAYO
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202205' AND @AnioActual = @AnioActual),0)) [TotalMayo],
	-- JUNIO
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202206' AND @AnioActual = @AnioActual),0)) [TotalJunio],
	-- JULIO
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202207' AND @AnioActual = @AnioActual),0)) [TotalJulio],
	-- AGOSTO
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202208' AND @AnioActual = @AnioActual),0)) [TotalAgosto],
	-- SEPTIEMBRE
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202209' AND @AnioActual = @AnioActual),0)) [TotalSeptiembre],
	-- OCTUBRE
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202210' AND @AnioActual = @AnioActual),0)) [TotalOctubre],
	-- NOVIEMBRE
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202211' AND @AnioActual = @AnioActual),0)) [TotalNoviembre],
	-- DICIEMBRE
	(ISNULL((SELECT sum(isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyymm='202212' AND @AnioActual = @AnioActual),0)) [TotalDiciembre],
	-- TOTAL DE INGRESOS PERCIBIDOS
	(ISNULL((SELECT (isnull(MontoTotalVenta ,0)) FROM vista_listadototalesventaspormes_ingresos WHERE yyyy=@AnioActual AND @AnioActual = @AnioActual),0)) [TotalIngresos]
END

GO
create procedure sp_ReporteVentasProcesadas(
	@fechainicio varchar(10),
	@fechafin varchar(10),
	@idtransaccion varchar(50)
)
as 
begin
	SET DATEFORMAT mdy;
	SELECT CONVERT (char(10),venta.FechaVenta,103)[FechaVenta],CONCAT(usuario.Nombres,' ',usuario.Apellidos)[Cliente], producto.Nombre[Producto], producto.Precio,
	detalleventa.Cantidad, detalleventa.Total, venta.IdTransaccion FROM Detalle_Ventas detalleventa
	INNER JOIN Productos producto ON producto.IdProducto = detalleventa.IdProducto
	INNER JOIN Ventas venta ON venta.IdVenta = detalleventa.IdVenta
	INNER JOIN Usuarios usuario ON usuario.IdUsuario = venta.IdUsuario
	WHERE CONVERT(date,venta.FechaVenta) between @fechainicio and @fechafin and venta.IdTransaccion = iif(@idtransaccion = '',venta.IdTransaccion,@idtransaccion)
end

-- BUSCADOR DE PRODUCTOS
CREATE PROCEDURE sp_buscadorproductos(
	@ValorBusqueda varchar(255)
)
AS
BEGIN
	-- BUSQUEDA POR NOMBRES, DESCRIPCION Y MARCAS DE PRODUCTOS
	 IF (LEN(@ValorBusqueda) > 0)
	 BEGIN
		SELECT * FROM vista_consultalistadoproductosregistrados WHERE Nombre Like '%' + @ValorBusqueda + '%' 
		OR DescripcionProducto Like '%' + @ValorBusqueda + '%'
		OR Descripcion Like '%' + @ValorBusqueda + '%'
		AND Activo=1
	 END
END
-- CREACION DE TABLAS [ARQUITECTO ALMACENES]
-- ROLES DE USUARIOS
--DBCC CHECKIDENT (ReporteProblemasComprasClientes, RESEED, 0);
GO
CREATE TABLE Roles(
	IdRolUsuario int IDENTITY PRIMARY KEY,
	NombreRolUsuario varchar(50) UNIQUE NOT NULL,
	DescripcionCortaRolUsuario varchar(100) NOT NULL
);
--Update usuarios set IdRolUsuario = 1 WHERE IdUsuario = 1
--Update usuarios set activo = 1 WHERE IdUsuario = 1

-- USUARIOS Y CLIENTES
GO
CREATE TABLE Usuarios(
	IdUsuario int primary key identity,
	Nombres varchar(255) NOT NULL,
	Apellidos varchar(255) NOT NULL,
	Correo varchar(255) UNIQUE NOT NULL,
	Usuario varchar(255) UNIQUE NOT NULL,
	Contrasenia varbinary(500) NOT NULL,
	Reestablecer bit default 1,
	Activo bit default 1,
	FechaRegistro datetime default getdate(),
	IdRolUsuario int REFERENCES Roles(IdRolUsuario),
	Fotoperfil varchar(255) default 'avatar-defecto.gif'
);

-- REPORTE PROBLEMAS PLATAFORMA
GO
CREATE TABLE ProblemasPlataforma(
	IdReportePlataforma int PRIMARY KEY IDENTITY,
	NombreReporte varchar(40) NOT NULL,
	DescripcionReporte varchar(255) NOT NULL,
	NombreImagenReporte varchar(255) NULL,
	FechaRegistro datetime default getdate(),
	EstadoReporte varchar(10) default 'activo' NOT NULL,
	IdUsuario int references Usuarios(IdUsuario),
);
-- CATEGORIAS DE PRODUCTOS
GO
CREATE TABLE Categorias(
	IdCategorias int primary key identity,
	CodigoCategoria varchar(40) UNIQUE NOT NULL,
	Descripcion varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
);

-- MARCAS DE PRODUCTOS
GO
CREATE TABLE Marcas(
	IdMarcas int primary key identity,
	Descripcion varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
);
-- CONTROL DE GARANTIAS PRODUCTOS ARQUITECTOS ALMACENES S.A DE C.V
GO
CREATE TABLE GarantiasProductos(
	IdControlGarantias int PRIMARY KEY IDENTITY,
	IdMarcas int references Marcas(IdMarcas),
	DuracionDiasGarantia int NOT NULL,
	DescripcionFabricanteGarantia varchar(500) NOT NULL,
	Activo bit default 1,
);
-- PRODUCTOS DE ARQUITECTOS ALMACENES S.A DE C.V
GO
CREATE TABLE Productos(
	IdProducto int PRIMARY KEY IDENTITY,
	Nombre varchar(500) NOT NULL,
	Descripcion varchar(500) NOT NULL,
	IdMarcas int references Marcas(IdMarcas),
	IdCategorias int references Categorias(IdCategorias),
	IdControlGarantias int references GarantiasProductos(IdControlGarantias),
	Precio money default 0 NOT NULL,
	Stock int NOT NULL,
	NombreImagen varchar(100) NULL,
	Activo bit default 1,
	FechaRegistro datetime default getdate()
);
-- CARRITO DE COMPRAS CLIENTES
CREATE TABLE CarritoCompras(
	IdCarrito int primary key identity,
	Nombres varchar(100) NULL,
	IdUsuario int references Usuarios(IdUsuario),
	IdProducto int references Productos(IdProducto),
	Cantidad int NOT NULL,
	FechaRegistro datetime default getdate()
);

-- VENTAS PROCESADAS [FORMULARIO CARRITO DE COMPRAS FINALIZADO]
CREATE TABLE Ventas(
	IdVenta int primary key identity,
	IdUsuario int references Usuarios(IdUsuario),
	IdDepartamento varchar(10) references Departamentos(IdDepartamento),
	IdMunicipios int references Municipios(IdMunicipios),
	TotalProducto int NOT NULL,
	MontoTotal money NOT NULL,
	Nombres varchar(255) NOT NULL,
	Apellidos varchar(255) NOT NULL,
	Dui varchar(10) NOT NULL,
	Telefono varchar(50) NOT NULL,
	Direccion varchar(500) NOT NULL,
	IdTransaccion varchar(100) NOT NULL,
	FechaVenta datetime default getdate(),
	EstadoPedido varchar(20) default 'en proceso'
);
-- DETALLE DE TODAS LAS VENTAS PROCESADAS
CREATE TABLE Detalle_Ventas(
	IdDetalleVenta int primary key identity,
	IdVenta int references Ventas(IdVenta),
	IdProducto int references Productos(IdProducto),
	Cantidad int NOT NULL,
	Total money NOT NULL
);
-- REPORTES PROBLEMAS COMPRAS CLIENTES
GO
CREATE TABLE ReporteProblemasComprasClientes(
	IdReporteProblema int PRIMARY KEY IDENTITY,
	IdUsuario int references Usuarios(IdUsuario),
	IdVenta int references Ventas(IdVenta),
	DescripcionProblema varchar(500) NOT NULL,
	FechaRegistro datetime default getdate(),
	FechaActualizacion datetime default getdate(),
	ComentarioRetroalimentacion varchar(500) NULL,
	Estado bit default 1
);
-- DEPARTAMENTOS DEL PAIS [VALIDO PARA EL SALVADOR [SV]]
GO
CREATE TABLE Departamentos(
	IdDepartamento varchar(10) primary key,
	Descripcion varchar(45) NOT NULL
);
-- MUNICIPIOS DEL PAIS [VALIDO PARA EL SALVADOR [SV]]
GO
CREATE TABLE Municipios(
	IdMunicipios int PRIMARY KEY NOT NULL,
	Descripcion varchar(45) NOT NULL,
	IdDepartamento varchar(10) references Departamentos(IdDepartamento)
);


-- TABLA VIRTUAL -> PROCESAMIENTO DE PRODUCTOS VENTAS PROCESADAS
CREATE TYPE EDetalle_Venta AS TABLE(
	IdProducto int NULL,
	Cantidad int NULL,
	Total money NULL
)


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
	-- CAMBIAR POR AÑO EN CURSO
	SET @AnioActual = 2022
	-- IGUALMENTE CONDICION DE MESES yyyymm='202201' -> YYYY?? <- CAMBIAR POR AÑO EN CURSO
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


-- INSERCION DE DATOS POR DEFECTO

GO
insert into Departamentos VALUES ('SV-AH','Ahuachapan')
insert into Departamentos VALUES ('SV-CA','Cabañas')
insert into Departamentos VALUES ('SV-CH','Chalatenango')
insert into Departamentos VALUES ('SV-CU','Cuscatlán')
insert into Departamentos VALUES ('SV-LI','La Libertad')
insert into Departamentos VALUES ('SV-MO','Morazán')
insert into Departamentos VALUES ('SV-PA','La Paz')
insert into Departamentos VALUES ('SV-SA','Santa Ana')
insert into Departamentos VALUES ('SV-SM','San Miguel')
insert into Departamentos VALUES ('SV-SO','Sonsonate')
insert into Departamentos VALUES ('SV-SS','San Salvador')
insert into Departamentos VALUES ('SV-SV','San Vicente')
insert into Departamentos VALUES ('SV-UN','La Unión')
insert into Departamentos VALUES ('SV-US','Usulutan')
GO
insert into Municipios values (1,'Ahuachapan','SV-AH')
insert into Municipios values (2,'Apaneca','SV-AH')
insert into Municipios values (3,'Atiquizaya','SV-AH')
insert into Municipios values (4,'Concepción de Ataco','SV-AH')
insert into Municipios values (5,'El Refugio','SV-AH')
insert into Municipios values (6,'Guaymango','SV-AH')
insert into Municipios values (7,'Jujutla','SV-AH')
insert into Municipios values (8,'San Francisco Menéndez','SV-AH')
insert into Municipios values (9,'San Lorenzo','SV-AH')
insert into Municipios values (10,'San Pedro Puxtla','SV-AH')
insert into Municipios values (11,'Tacuba','SV-AH')
insert into Municipios values (12,'Turín','SV-AH')
GO
insert into Municipios values (13,'Sensuntepeque','SV-CA')
insert into Municipios values (14,'Cinquera','SV-CA')
insert into Municipios values (15,'Dolores','SV-CA')
insert into Municipios values (16,'Guacotecti','SV-CA')
insert into Municipios values (17,'Ilobasco ','SV-CA')
insert into Municipios values (18,'Jutiapa','SV-CA')
insert into Municipios values (19,'San Isidro','SV-CA')
insert into Municipios values (20,'Tejutepeque','SV-CA')
insert into Municipios values (21,'Victoria','SV-CA')
GO
insert into Municipios values (22,'Chalatenango','SV-CH')
insert into Municipios values (23,'Agua Caliente','SV-CH')
insert into Municipios values (24,'Arcatao','SV-CH')
insert into Municipios values (25,'Azacualpa','SV-CH')
insert into Municipios values (26,'Cancasque','SV-CH')
insert into Municipios values (27,'Citalá','SV-CH')
insert into Municipios values (28,'Comalapa','SV-CH')
insert into Municipios values (29,'Concepción Quezaltepeque','SV-CH')
insert into Municipios values (30,'Dulce Nombre de María','SV-CH')
insert into Municipios values (31,'El Carrizal','SV-CH')
insert into Municipios values (32,'El Paraíso','SV-CH')
insert into Municipios values (33,'La Laguna','SV-CH')
insert into Municipios values (34,'La Palma','SV-CH')
insert into Municipios values (35,'La Reina','SV-CH')
insert into Municipios values (36,'Las Flores','SV-CH')
insert into Municipios values (37,'Las Vueltas','SV-CH')
insert into Municipios values (38,'Nombre de Jesús','SV-CH')
insert into Municipios values (39,'Nueva Concepción ','SV-CH')
insert into Municipios values (40,'Nueva Trinidad','SV-CH')
insert into Municipios values (41,'Ojos de Agua','SV-CH')
insert into Municipios values (42,'Potonico','SV-CH')
insert into Municipios values (43,'San Antonio de la Cruz','SV-CH')
insert into Municipios values (44,'San Antonio Los Ranchos','SV-CH')
insert into Municipios values (45,'San Fernando','SV-CH')
insert into Municipios values (46,'San Francisco Lempa','SV-CH')
insert into Municipios values (47,'San Francisco Morazán','SV-CH')
insert into Municipios values (48,'San Ignacio','SV-CH')
insert into Municipios values (49,'San Isidro Labrador','SV-CH')
insert into Municipios values (50,'San Luis del Carmen','SV-CH')
insert into Municipios values (51,'San Miguel de Mercedes','SV-CH')
insert into Municipios values (52,'Santa Rita','SV-CH')
insert into Municipios values (53,'San Rafael','SV-CH')
insert into Municipios values (54,'Tejutla','SV-CH')

GO
insert into Municipios values (55,'Cojutepeque','SV-CU')
insert into Municipios values (56,'Candelaria','SV-CU')
insert into Municipios values (57,'El Carmen','SV-CU')
insert into Municipios values (58,'El Rosario','SV-CU')
insert into Municipios values (59,'Monte San Juan','SV-CU')
insert into Municipios values (60,'Oratorio de Concepción','SV-CU')
insert into Municipios values (61,'San Bartolomé Perulapía','SV-CU')
insert into Municipios values (62,'San Cristóbal','SV-CU')
insert into Municipios values (63,'San José Guayabal','SV-CU')
insert into Municipios values (64,'San Pedro Perulapán','SV-CU')
insert into Municipios values (65,'San Rafael Cedros','SV-CU')
insert into Municipios values (66,'San Ramón','SV-CU')
insert into Municipios values (67,'Santa Cruz Analquito','SV-CU')
insert into Municipios values (68,'Santa Cruz Michapa','SV-CU')
insert into Municipios values (69,'Suchitoto','SV-CU')
insert into Municipios values (70,'Tenancingo','SV-CU')


GO
insert into Municipios values (71,'Santa Tecla','SV-LI')
insert into Municipios values (72,'Antiguo Cuscatlán','SV-LI')
insert into Municipios values (73,'Chiltiupán','SV-LI')
insert into Municipios values (74,'Ciudad Arce','SV-LI')
insert into Municipios values (75,'Colon','SV-LI')
insert into Municipios values (76,'Comasagua','SV-LI')
insert into Municipios values (77,'Huizúcar','SV-LI')
insert into Municipios values (78,'Jayaque','SV-LI')
insert into Municipios values (79,'Jicalapa','SV-LI')
insert into Municipios values (80,'La Libertad','SV-LI')
insert into Municipios values (81,'Nuevo Cuscatlan','SV-LI')
insert into Municipios values (82,'Quezaltepeque','SV-LI')
insert into Municipios values (83,'San Juan Opico','SV-LI')
insert into Municipios values (84,'Sacacoyo','SV-LI')
insert into Municipios values (85,'San José Villanueva','SV-LI')
insert into Municipios values (86,'San Matías','SV-LI')
insert into Municipios values (87,'San Pablo Tacachico','SV-LI')
insert into Municipios values (88,'Talnique','SV-LI')
insert into Municipios values (89,'Tamanique','SV-LI')
insert into Municipios values (90,'Teotepeque','SV-LI')
insert into Municipios values (91,'Tepecoyo','SV-LI')
insert into Municipios values (92,'Zaragoza','SV-LI')

GO
insert into Municipios values (93,'Zacatecoluca','SV-PA')
insert into Municipios values (94,'Cuyultitán','SV-PA')
insert into Municipios values (95,'El Rosario','SV-PA')
insert into Municipios values (96,'Jerusalén','SV-PA')
insert into Municipios values (97,'Mercedes La Ceiba','SV-PA')
insert into Municipios values (98,'Olocuilta','SV-PA')
insert into Municipios values (99,'Paraíso de Osorio','SV-PA')
insert into Municipios values (100,'San Antonio Masahuat','SV-PA')
insert into Municipios values (101,'San Emigdio','SV-PA')
insert into Municipios values (102,'San Francisco Chinameca','SV-PA')
insert into Municipios values (103,'San Pedro Masahuat','SV-PA')
insert into Municipios values (104,'San Juan Nonualco','SV-PA')
insert into Municipios values (105,'San Juan Talpa','SV-PA')
insert into Municipios values (106,'San Juan Tepezontes','SV-PA')
insert into Municipios values (107,'San Luis La Herradura','SV-PA')
insert into Municipios values (108,'San Luis Talpa','SV-PA')
insert into Municipios values (109,'San Miguel Tepezontes','SV-PA')
insert into Municipios values (110,'San Pedro Nonualco','SV-PA')
insert into Municipios values (111,'San Rafael Obrajuelo','SV-PA')
insert into Municipios values (112,'Santa María Ostuma','SV-PA')
insert into Municipios values (113,'Santiago Nonualco','SV-PA')
insert into Municipios values (114,'Tapalhuaca','SV-PA')
GO
insert into Municipios values (115,'La Unión','SV-UN')
insert into Municipios values (116,'Anamorós','SV-UN')
insert into Municipios values (117,'Bolívar','SV-UN')
insert into Municipios values (118,'Concepción de Oriente','SV-UN')
insert into Municipios values (119,'Conchagua','SV-UN')
insert into Municipios values (120,'El Carmen','SV-UN')
insert into Municipios values (121,'El Sauce','SV-UN')
insert into Municipios values (122,'El Sauce','SV-UN')
insert into Municipios values (123,'Lislique','SV-UN')
insert into Municipios values (124,'Meanguera del Golfo','SV-UN')
insert into Municipios values (125,'Nueva Esparta','SV-UN')
insert into Municipios values (126,'Pasaquina','SV-UN')
insert into Municipios values (127,'Polorós','SV-UN')
insert into Municipios values (128,'San Alejo','SV-UN')
insert into Municipios values (129,'San José','SV-UN')
insert into Municipios values (130,'Santa Rosa de Lima','SV-UN')
insert into Municipios values (131,'Yayantique','SV-UN')
insert into Municipios values (132,'Yucuaiquín','SV-UN')

GO
insert into Municipios values (133,'San Francisco Gotera','SV-MO')
insert into Municipios values (134,'Arambala','SV-MO')
insert into Municipios values (135,'Cacaopera','SV-MO')
insert into Municipios values (136,'Chilanga','SV-MO')
insert into Municipios values (137,'Corinto','SV-MO')
insert into Municipios values (138,'Delicias de Concepción','SV-MO')
insert into Municipios values (139,'El Divisadero','SV-MO')
insert into Municipios values (140,'El Rosario','SV-MO')
insert into Municipios values (141,'Gualococti','SV-MO')
insert into Municipios values (142,'Gualococti','SV-MO')
insert into Municipios values (143,'Joateca','SV-MO')
insert into Municipios values (144,'Jocoaitique','SV-MO')
insert into Municipios values (145,'Jocoro','SV-MO')
insert into Municipios values (146,'Lolotiquillo','SV-MO')
insert into Municipios values (147,'Meanguera','SV-MO')
insert into Municipios values (148,'Osicala','SV-MO')
insert into Municipios values (149,'Perquín','SV-MO')
insert into Municipios values (150,'San Carlos','SV-MO')
insert into Municipios values (151,'San Fernando','SV-MO')
insert into Municipios values (152,'San Isidro','SV-MO')
insert into Municipios values (153,'San Simón','SV-MO')
insert into Municipios values (154,'Sensembra','SV-MO')
insert into Municipios values (155,'Sociedad','SV-MO')
insert into Municipios values (156,'Torola','SV-MO')
insert into Municipios values (157,'Yamabal','SV-MO')
insert into Municipios values (158,'Yoloaiquín','SV-MO')

GO
insert into Municipios values (159,'San Miguel','SV-SM')
insert into Municipios values (160,'Carolina','SV-SM')
insert into Municipios values (161,'Chapeltique','SV-SM')
insert into Municipios values (162,'Chinameca','SV-SM')
insert into Municipios values (163,'Chirilagua','SV-SM')
insert into Municipios values (164,'Ciudad Barrios','SV-SM')
insert into Municipios values (165,'Comacarán','SV-SM')
insert into Municipios values (166,'El Tránsito','SV-SM')
insert into Municipios values (167,'Lolotique','SV-SM')
insert into Municipios values (168,'Moncagua','SV-SM')
insert into Municipios values (169,'Nueva Guadalupe','SV-SM')
insert into Municipios values (170,'Nuevo Edén de San Juan','SV-SM')
insert into Municipios values (171,'Quelepa','SV-SM')
insert into Municipios values (172,'San Antonio','SV-SM')
insert into Municipios values (173,'San Gerardo','SV-SM')
insert into Municipios values (174,'San Jorge','SV-SM')
insert into Municipios values (175,'San Luis de la Reina','SV-SM')
insert into Municipios values (176,'San Rafael Oriente','SV-SM')
insert into Municipios values (177,'Sesori','SV-SM')
insert into Municipios values (178,'Uluazapa','SV-SM')

GO
insert into Municipios values (179,'San Salvador','SV-SS')
insert into Municipios values (180,'Aguilares','SV-SS')
insert into Municipios values (181,'Apopa','SV-SS')
insert into Municipios values (182,'Ayutuxtepeque','SV-SS')
insert into Municipios values (183,'Ciudad Delgado','SV-SS')
insert into Municipios values (184,'Cuscatancingo','SV-SS')
insert into Municipios values (185,'El Paisnal','SV-SS')
insert into Municipios values (186,'Guazapa','SV-SS')
insert into Municipios values (187,'Ilopango','SV-SS')
insert into Municipios values (188,'Mejicanos','SV-SS')
insert into Municipios values (189,'Nejapa','SV-SS')
insert into Municipios values (190,'Panchimalco','SV-SS')
insert into Municipios values (191,'Panchimalco','SV-SS')
insert into Municipios values (192,'San Marcos','SV-SS')
insert into Municipios values (193,'San Martín','SV-SS')
insert into Municipios values (194,'Santiago Texacuangos','SV-SS')
insert into Municipios values (195,'Santo Tomás','SV-SS')
insert into Municipios values (196,'Soyapango','SV-SS')
insert into Municipios values (197,'Tonacatepeque','SV-SS')

GO
insert into Municipios values (198,'San Vicente','SV-SV')
insert into Municipios values (199,'Apastepeque','SV-SV')
insert into Municipios values (200,'Guadalupe','SV-SV')
insert into Municipios values (201,'San Cayetano Istepeque','SV-SV')
insert into Municipios values (202,'San Esteban Catarina','SV-SV')
insert into Municipios values (203,'San Ildefonso','SV-SV')
insert into Municipios values (204,'San Lorenzo','SV-SV')
insert into Municipios values (205,'San Sebastián','SV-SV')
insert into Municipios values (206,'Santa Clara','SV-SV')
insert into Municipios values (207,'Santo Domingo','SV-SV')
insert into Municipios values (208,'Tecoluca','SV-SV')
insert into Municipios values (209,'Tepetitán','SV-SV')
insert into Municipios values (210,'Verapaz','SV-SV')

GO
insert into Municipios values (211,'Santa Ana','SV-SA')
insert into Municipios values (212,'Candelaria de la Frontera','SV-SA')
insert into Municipios values (213,'Chalchuapa','SV-SA')
insert into Municipios values (214,'Coatepeque','SV-SA')
insert into Municipios values (215,'El Congo','SV-SA')
insert into Municipios values (216,'El Porvenir','SV-SA')
insert into Municipios values (217,'Masahuat','SV-SA')
insert into Municipios values (218,'Metapán','SV-SA')
insert into Municipios values (219,'San Antonio Pajonal','SV-SA')
insert into Municipios values (220,'San Sebastián Salitrillo','SV-SA')
insert into Municipios values (221,'Santa Rosa Guachipilín','SV-SA')
insert into Municipios values (222,'Santiago de la Frontera','SV-SA')
insert into Municipios values (223,'Texistepeque','SV-SA')

GO
insert into Municipios values (224,'Sonsonate','SV-SO')
insert into Municipios values (225,'Acajutla','SV-SO')
insert into Municipios values (226,'Armenia','SV-SO')
insert into Municipios values (227,'Caluco','SV-SO')
insert into Municipios values (228,'Cuisnahuat','SV-SO')
insert into Municipios values (229,'Izalco','SV-SO')
insert into Municipios values (230,'Juayúa','SV-SO')
insert into Municipios values (231,'Nahuizalco','SV-SO')
insert into Municipios values (232,'Nahulingo','SV-SO')
insert into Municipios values (233,'Salcoatitán','SV-SO')
insert into Municipios values (234,'San Antonio del Monte','SV-SO')
insert into Municipios values (235,'San Julián','SV-SO')
insert into Municipios values (236,'Santa Catarina Masahuat','SV-SO')
insert into Municipios values (237,'Santa Isabel Ishuatán','SV-SO')
insert into Municipios values (238,'Santo Domingo de Guzmán','SV-SO')
insert into Municipios values (239,'Sonzacate','SV-SO')

GO
insert into Municipios values (240,'Usulutan','SV-US')
insert into Municipios values (241,'Alegría','SV-US')
insert into Municipios values (242,'Berlín','SV-US')
insert into Municipios values (243,'California','SV-US')
insert into Municipios values (244,'Concepción Batres','SV-US')
insert into Municipios values (245,'El Triunfo','SV-US')
insert into Municipios values (246,'Ereguayquín','SV-US')
insert into Municipios values (247,'Estanzuelas','SV-US')
insert into Municipios values (248,'Jiquilisco','SV-US')
insert into Municipios values (249,'Jucuapa','SV-US')
insert into Municipios values (250,'Jucuarán','SV-US')
insert into Municipios values (251,'Mercedes Umaña','SV-US')
insert into Municipios values (252,'Nueva Granada','SV-US')
insert into Municipios values (253,'Ozatlán','SV-US')
insert into Municipios values (254,'Puerto El Triunfo','SV-US')
insert into Municipios values (255,'San Agustín','SV-US')
insert into Municipios values (256,'San Buenaventura','SV-US')
insert into Municipios values (257,'San Dionisio','SV-US')
insert into Municipios values (258,'San Francisco Javier','SV-US')
insert into Municipios values (259,'Santa Elena','SV-US')
insert into Municipios values (260,'Santa María','SV-US')
insert into Municipios values (261,'Santiago de María','SV-US')
insert into Municipios values (262,'Tecapán','SV-US')


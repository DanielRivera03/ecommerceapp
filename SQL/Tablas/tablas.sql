CREATE TABLE Roles(
	IdRolUsuario int IDENTITY PRIMARY KEY,
	NombreRolUsuario varchar(50) UNIQUE NOT NULL,
	DescripcionCortaRolUsuario varchar(100) NOT NULL
);
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
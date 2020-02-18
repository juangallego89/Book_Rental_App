USE master
GO
DROP DATABASE IF EXISTS BookRental
GO
CREATE DATABASE BookRental
GO
USE BookRental
GO
DROP TABLE IF EXISTS Usuarios
GO
CREATE TABLE Usuarios(
	ID_Usuario bigint IDENTITY(1,1) PRIMARY KEY NOT NULL,
	PrimerNombre nchar(30) NOT NULL,
	SegundoNombre nchar(30) NULL,
	PrimerApellido nchar(30) NOT NULL,
	SegundoApellido nchar(30) NULL,
	Correo nchar(30) NOT NULL,
	Contrasena nchar(10) NOT NULL,
	Telefono nchar(10) NOT NULL,
	Direccion nchar(20) NOT NULL,
	Documento nchar(20) NOT NULL,
	TipoDocumento nchar(5) NOT NULL,
	Rol nchar(20) DEFAULT N'Cliente' NOT NULL,
	Estado nchar(10) DEFAULT N'Activo' NOT NULL,
	EstadoRegistro int DEFAULT 1 NOT NULL,
)
GO
DROP TABLE IF EXISTS Reservas
GO
CREATE TABLE Reservas(
	ID_Reserva bigint IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ID_Usuario bigint NOT NULL,
	ID_Libro bigint NOT NULL,
	ValorReserva money NOT NULL,
	FechaReserva datetime DEFAULT (getdate()) NOT NULL,
	FechaEntrega date NOT NULL,
	Estado nchar(10) DEFAULT N'Activo' NOT NULL,
	EstadoRegistro int DEFAULT 1 NOT NULL,
	CONSTRAINT FK_Usuarios_Reservas FOREIGN KEY (ID_Usuario) REFERENCES Usuarios (ID_Usuario),
)
GO
DROP TABLE IF EXISTS Tarifas
GO
CREATE TABLE Tarifas(
	ID_Tarifa bigint IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ID_Libro bigint NULL,
	Nombre nchar(50) NOT NULL,
	Tarifa money NOT NULL,
	Estado nchar(10) DEFAULT N'Activo' NOT NULL,
	EstadoRegistro int DEFAULT 1 NOT NULL
)
GO
DROP TABLE IF EXISTS Libros
GO
CREATE TABLE Libros(
	ID_Libro bigint IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ID_Reserva bigint NULL,
	ID_Tarifa bigint NOT NULL,
	Titulo nchar(50) NOT NULL,
	Autor nchar(50) NOT NULL,
	Categoria nchar(30) NOT NULL,
	Ejemplares int NOT NULL,
	Imagen nchar(50) NULL,
	Estado nchar(10) DEFAULT N'Activo' NOT NULL,
	EstadoRegistro int DEFAULT 1 NOT NULL,
	CONSTRAINT FK_Libros_Tarifas FOREIGN KEY (ID_Tarifa) REFERENCES Tarifas (ID_Tarifa),
	CONSTRAINT FK_Libros_Reservas FOREIGN KEY (ID_Reserva) REFERENCES Reservas (ID_Reserva)
)
GO
INSERT INTO Usuarios(PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Correo,Contrasena,Telefono,Direccion,Documento,TipoDocumento,Rol)
VALUES('JUAN','DAVID','GALLEGO','GIRALDO','JUANGALLEGO98@GMAIL.COM','1234','3134595990','CALLE 6D SUR 39-100','1120451214','CC','Administrador');
INSERT INTO Usuarios(PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Correo,Contrasena,Telefono,Direccion,Documento,TipoDocumento,Rol)
VALUES('MARIA','DE LOS ÁNGELES','GALLEGO','GIRALDO','MARIA2020@GMAIL.COM','1234','3103102030','CALLE 6D SUR 50-100','11214587412','CC','Cliente');
INSERT INTO Tarifas(Nombre,Tarifa)
VALUES('Tarifa test', 2500);
INSERT INTO Tarifas(Nombre,Tarifa)
VALUES('Tarifa test 2', 5000);
INSERT INTO Tarifas(Nombre,Tarifa)
VALUES('Tarifa test 3', 3000);
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,1,'Libro de prueba 1','Developer','Software',10,'/images/book-1.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,2,'Libro de prueba 2','Developer','Software',2,'/images/book-2.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,2,'Libro de prueba 3','Developer','Software',8,'/images/book-3.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,2,'Libro de prueba 4','Developer','Software',4,'/images/book-4.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,2,'Libro de prueba 5','Developer','Software',7,'/images/book-5.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,2,'Libro de prueba 6','Developer','Software',3,'/images/book-6.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,2,'Libro de prueba 7','Developer','Software',1,'/images/book-7.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,1,'Libro de prueba 8','Developer','Software',5000,'/images/book-8.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,3,'Libro de prueba 9','Developer','Software',48,'/images/book-9.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,1,'Libro de prueba 10','Developer','Software',85,'/images/book-icon.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,3,'Libro de prueba 11','Developer','Software',34,'/images/book-icon.jpg');
INSERT INTO Libros(ID_Reserva,ID_Tarifa,Titulo,Autor,Categoria,Ejemplares,Imagen)
VALUES(NULL,2,'Libro de prueba 12','Developer','Software',12,'/images/book-icon.jpg');
GO
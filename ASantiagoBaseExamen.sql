
CREATE DATABASE ASantiagoExamenAARCO;
GO

USE ASantiagoExamenAARCO;
GO

CREATE TABLE Marca(
IdMarca INT PRIMARY KEY IDENTITY(1,1),
NombreMarca VARCHAR(40)
);

CREATE TABLE SubMarca(
IdSubMarca INT PRIMARY KEY IDENTITY(1,1),
NombreSubMarca VARCHAR(20),
IdMarca INT FOREIGN KEY REFERENCES Marca(IdMarca) 
);

CREATE TABLE ModeloSubMarca(
IdModeloSubMarca TINYINT PRIMARY KEY IDENTITY(1,1),
YearModeloSubMarca INT
);

CREATE TABLE CatalogoDescripcion(
IdCatalogoDescripcion INT PRIMARY KEY IDENTITY(1,1),
NombreDescripcion VARCHAR(100)
);

CREATE TABLE Descripcion(
IdDescripcion VARCHAR(36) PRIMARY KEY,
IdModeloSubMarca TINYINT FOREIGN KEY REFERENCES ModeloSubMarca(IdModeloSubMarca),
IdCatalogoDescripcion INT FOREIGN KEY REFERENCES CatalogoDescripcion(IdCatalogoDescripcion),
IdSubMarca INT FOREIGN KEY REFERENCES SubMarca(IdSubMarca)
)


---------------------------------------------MARCA
GO
CREATE PROCEDURE MarcaAdd --'ACURA'
@NombreMarca VARCHAR(40)
AS
	INSERT INTO Marca (NombreMarca)
	VALUES (@NombreMarca)

GO
CREATE PROCEDURE MarcaGetAll
AS
	SELECT IdMarca, NombreMarca FROM Marca
GO
CREATE PROCEDURE MarcaGetNombre --'ACURA'
@NombreMarca VARCHAR(40)
AS
	SELECT IdMarca, NombreMarca FROM Marca WHERE NombreMarca = @NombreMarca
GO

----------------------------------------------SUBMARCA
CREATE PROCEDURE SubMarcaAdd
@NombreSubMarca VARCHAR(20),
@IdMarca INT
AS
	INSERT INTO SubMarca (NombreSubMarca,IdMarca)
	VALUES (@NombreSubMarca,@IdMarca)
GO
CREATE PROCEDURE SubMarcaGetByIdMarca
@IdMarca INT
AS
	SELECT 
	IdSubMarca,
	NombreSubMarca,
	Marca.IdMarca,
	Marca.NombreMarca 
	FROM SubMarca 
	INNER JOIN Marca ON SubMarca.IdMarca = Marca.IdMarca
	WHERE Marca.IdMarca = @IdMarca
GO
CREATE PROCEDURE SubMarcaGetNombre
@NombreSubMarca VARCHAR(20)
AS
	SELECT IdSubMarca, NombreSubMarca,SubMarca.IdMarca,Marca.NombreMarca FROM SubMarca 
	INNER JOIN Marca ON SubMarca.IdMarca = Marca.IdMarca
	WHERE NombreSubMarca = @NombreSubMarca
GO

-----------------------------------------------------------------MODELOSUBMARCA

CREATE PROCEDURE ModeloSubMarcaAdd
@YearModeloSubMarca INT
AS
	INSERT INTO ModeloSubMarca (YearModeloSubMarca)
	VALUES (@YearModeloSubMarca)

GO
CREATE PROCEDURE ModeloSubMarcaGetById
@IdModeloSubMarca TINYINT
AS
	SELECT IdModeloSubMarca,YearModeloSubMarca FROM ModeloSubMarca WHERE IdModeloSubMarca = @IdModeloSubMarca
GO
CREATE PROCEDURE ModeloSubMarcaGetYear
@YearModeloSubMarca INT
AS
	SELECT IdModeloSubMarca,YearModeloSubMarca FROM ModeloSubMarca WHERE YearModeloSubMarca = @YearModeloSubMarca

GO
CREATE PROCEDURE YearGetByIdSubMarca 
@IdSubMarca INT
AS
SELECT 
	ModeloSubMarca.YearModeloSubMarca,
	Descripcion.IdModeloSubMarca,
	IdDescripcion,
	CatalogoDescripcion.IdCatalogoDescripcion,
	CatalogoDescripcion.NombreDescripcion,
	IdSubMarca
FROM Descripcion
INNER JOIN ModeloSubMarca ON Descripcion.IdModeloSubMarca = ModeloSubMarca.IdModeloSubMarca
INNER JOIN CatalogoDescripcion ON Descripcion.IdCatalogoDescripcion = CatalogoDescripcion.IdCatalogoDescripcion
WHERE IdSubMarca = @IdSubMarca
GROUP BY ModeloSubMarca.YearModeloSubMarca
ORDER BY YearModeloSubMarca ASC

GO

CREATE PROCEDURE GetBySubModeloMarca
@IdModeloSubMarca TINYINT,
@IdSubMarca INT
AS
SELECT 
	Descripcion.IdDescripcion,
	CatalogoDescripcion.IdCatalogoDescripcion,
	CatalogoDescripcion.NombreDescripcion,
	ModeloSubMarca.IdModeloSubMarca,
	ModeloSubMarca.YearModeloSubMarca,
	Descripcion.IdSubMarca
FROM Descripcion
INNER JOIN CatalogoDescripcion ON Descripcion.IdCatalogoDescripcion = CatalogoDescripcion.IdCatalogoDescripcion
INNER JOIN ModeloSubMarca ON Descripcion.IdModeloSubMarca = ModeloSubMarca.IdModeloSubMarca
WHERE ModeloSubMarca.IdModeloSubMarca = @IdModeloSubMarca AND IdSubMarca = @IdSubMarca
ORDER BY NombreDescripcion ASC

GO
----------------------------------------------------------------CATALOGDESCRIPCION

CREATE PROCEDURE CatalogoDescripcionAdd
@NombreDescripcion VARCHAR(100)
AS
	INSERT INTO CatalogoDescripcion (NombreDescripcion)
	VALUES (@NombreDescripcion)
GO
CREATE PROCEDURE CatalogoDescripcionGetById
@IdCatalogoDescripcion INT
AS
	SELECT IdCatalogoDescripcion,NombreDescripcion FROM CatalogoDescripcion WHERE IdCatalogoDescripcion = @IdCatalogoDescripcion
GO
CREATE PROCEDURE CatalogoDescripcionGetNombre
@NombreDescripcion VARCHAR(100)
AS
	SELECT IdCatalogoDescripcion,NombreDescripcion FROM CatalogoDescripcion WHERE NombreDescripcion = @NombreDescripcion

GO
------------------------------------------------------------------DESCRIPCION
GO
CREATE PROCEDURE DescripcionAdd
@IdDescripcion VARCHAR(36),
@IdModeloSubMarca TINYINT,
@IdCatalogoDescripcion INT,
@IdSubMarca INT
AS
	INSERT INTO Descripcion (IdDescripcion,IdModeloSubMarca,IdCatalogoDescripcion,IdSubMarca)
	VALUES
	(@IdDescripcion,@IdModeloSubMarca,@IdCatalogoDescripcion,@IdSubMarca)
GO
CREATE PROCEDURE DescripcionGetById
@IdMarca INT,
@IdSubMarca INT,
@IdModeloSubMarca TINYINT,
@IdCatalogoDescripcion INT
AS
	SELECT
		IdDescripcion,
		Descripcion.IdModeloSubMarca,
		ModeloSubMarca.YearModeloSubMarca,
		Descripcion.IdCatalogoDescripcion,
		CatalogoDescripcion.NombreDescripcion,
		Descripcion.IdSubMarca,
		SubMarca.NombreSubMarca,
		SubMarca.IdMarca,
		Marca.NombreMarca
	FROM
		Descripcion
	INNER JOIN ModeloSubMarca ON Descripcion.IdModeloSubMarca = ModeloSubMarca.IdModeloSubMarca
	INNER JOIN CatalogoDescripcion ON Descripcion.IdCatalogoDescripcion = CatalogoDescripcion.IdCatalogoDescripcion
	INNER JOIN SubMarca ON Descripcion.IdSubMarca = SubMarca.IdSubMarca
	INNER JOIN Marca ON SubMarca.IdMarca = Marca.IdMarca
	WHERE (SubMarca.IdMarca = @IdMarca) AND (SubMarca.IdSubMarca = @IdSubMarca) AND (Descripcion.IdModeloSubMarca= @IdModeloSubMarca)
	AND (Descripcion.IdCatalogoDescripcion = @IdCatalogoDescripcion)

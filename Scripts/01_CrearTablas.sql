-- Script para crear las tablas con eliminación en cascada
-- Ejecutar en el orden especificado para respetar las dependencias

-- 1. Tabla de Empresas
CREATE TABLE Empresa_tbl (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

-- 2. Tabla de Personal (con CASCADE DELETE)
CREATE TABLE Personal_tbl (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    Id_Empresa INT NOT NULL,
    
    -- Clave foránea con CASCADE DELETE
    CONSTRAINT FK_Personal_Empresa 
        FOREIGN KEY (Id_Empresa) 
        REFERENCES Empresa_tbl(Id) 
        ON DELETE CASCADE  -- Esto permite la eliminación en cascada
);

-- 3. Tabla de Usuarios
CREATE TABLE Usuario_tbl (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

-- 4. Tabla de Perfiles
CREATE TABLE Perfil_tbl (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

-- 5. Tabla de Ingresos (con CASCADE DELETE hacia Personal)
CREATE TABLE Ingreso_tbl (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Personal INT NOT NULL,
    FechaHoraIngreso DATETIME2 NOT NULL DEFAULT GETDATE(),
    TipoMotivo INT NOT NULL, -- Enum
    
    -- Clave foránea con CASCADE DELETE
    CONSTRAINT FK_Ingreso_Personal 
        FOREIGN KEY (Id_Personal) 
        REFERENCES Personal_tbl(Id) 
        ON DELETE CASCADE  -- Cuando se elimine personal, se eliminan sus ingresos
);

-- Crear índices para mejorar rendimiento
CREATE INDEX IX_Personal_Id_Empresa ON Personal_tbl(Id_Empresa);
CREATE INDEX IX_Ingreso_Id_Personal ON Ingreso_tbl(Id_Personal);
CREATE INDEX IX_Personal_Estado ON Personal_tbl(Estado);
CREATE INDEX IX_Empresa_Estado ON Empresa_tbl(Estado);

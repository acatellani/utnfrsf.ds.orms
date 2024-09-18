-- Crear la tabla Roles
CREATE TABLE Roles (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- Crear la tabla Usuarios
CREATE TABLE Usuarios (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    RolId INT REFERENCES Roles(Id) ON DELETE SET NULL
);

-- Crear la tabla Domicilios
CREATE TABLE Domicilios (
    Id SERIAL PRIMARY KEY,
    Calle VARCHAR(100) NOT NULL,
    Numero INT NOT NULL,
    UsuarioId INT REFERENCES Usuarios(Id) ON DELETE CASCADE
);
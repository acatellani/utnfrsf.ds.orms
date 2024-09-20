BEGIN TRANSACTION;
GO

CREATE TABLE [Roles] (
    [Id] INTEGER IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Usuarios] (
    [Id] INTEGER IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50) NOT NULL,
    [RolId] INTEGER NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Usuarios_Roles_RolId] FOREIGN KEY ([RolId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Domicilios] (
    [Id] INTEGER IDENTITY(1,1) NOT NULL,
    [Calle] nvarchar(100) NOT NULL,
    [Numero] INTEGER NOT NULL,
    [UsuarioId] INTEGER NULL,
    CONSTRAINT [PK_Domicilios] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Domicilios_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id])
);
GO

CREATE INDEX [IX_Domicilios_UsuarioId] ON [Domicilios] ([UsuarioId]);
GO

CREATE INDEX [IX_Usuarios_RolId] ON [Usuarios] ([RolId]);
GO

COMMIT;
GO

USE ejemploorms
GO

-- Insertar Roles
INSERT INTO [Roles] ([Id], [Nombre]) VALUES (1, 'Administrador');
INSERT INTO [Roles] ([Id], [Nombre]) VALUES (2, 'Usuario');
INSERT INTO [Roles] ([Id], [Nombre]) VALUES (3, 'Moderador');
INSERT INTO [Roles] ([Id], [Nombre]) VALUES (4, 'Invitado');
INSERT INTO [Roles] ([Id], [Nombre]) VALUES (5, 'SuperUsuario');
GO

-- Insertar Usuarios
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (1, 'Juan Perez', 1);
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (2, 'Maria Gomez', 2);
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (3, 'Pedro Ramirez', 3);
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (4, 'Laura Lopez', 4);
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (5, 'Carlos Fernandez', 5);
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (6, 'Ana Martinez', 2);
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (7, 'Roberto Diaz', 1);
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (8, 'Lucia Sanchez', 4);
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (9, 'Francisco Ortiz', 3);
INSERT INTO [Usuarios] ([Id], [Nombre], [RolId]) VALUES (10, 'Sofia Castillo', 5);
GO

-- Insertar Domicilios para los Usuarios
-- Juan Perez (Usuario 1)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (1, 'Calle Falsa', 123, 1);
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (2, 'Avenida Siempre Viva', 742, 1);

-- Maria Gomez (Usuario 2)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (3, 'Calle del Sol', 45, 2);

-- Pedro Ramirez (Usuario 3)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (4, 'Calle Luna', 78, 3);
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (5, 'Calle Estrella', 101, 3);

-- Laura Lopez (Usuario 4)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (6, 'Calle Amanecer', 12, 4);

-- Carlos Fernandez (Usuario 5)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (7, 'Calle Bosque', 9, 5);
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (8, 'Calle Rio', 15, 5);
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (9, 'Calle Lago', 22, 5);

-- Ana Martinez (Usuario 6)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (10, 'Calle Verde', 35, 6);

-- Roberto Diaz (Usuario 7)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (11, 'Calle Azul', 27, 7);
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (12, 'Calle Amarilla', 89, 7);

-- Lucia Sanchez (Usuario 8)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (13, 'Calle Blanca', 56, 8);

-- Francisco Ortiz (Usuario 9)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (14, 'Calle Roja', 67, 9);
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (15, 'Calle Negra', 90, 9);

-- Sofia Castillo (Usuario 10)
INSERT INTO [Domicilios] ([Id], [Calle], [Numero], [UsuarioId]) VALUES (16, 'Calle Morada', 105, 10);
GO


-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/14/2024 15:32:21
-- Generated from EDMX file: C:\Users\USER\source\repos\JADE\CAPADATOS\Modelo.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_JAADE];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_USUARIOESTADO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[USUARIO] DROP CONSTRAINT [FK_USUARIOESTADO];
GO
IF OBJECT_ID(N'[dbo].[FK_USUARIOPERSONA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[USUARIO] DROP CONSTRAINT [FK_USUARIOPERSONA];
GO
IF OBJECT_ID(N'[dbo].[FK_ROLUSUARIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[USUARIO] DROP CONSTRAINT [FK_ROLUSUARIO];
GO
IF OBJECT_ID(N'[dbo].[FK_AGENDAPERSONA_AGENDA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PERSONA_AGENDA] DROP CONSTRAINT [FK_AGENDAPERSONA_AGENDA];
GO
IF OBJECT_ID(N'[dbo].[FK_MODULO_PERMISOMODULO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MODULO_PERMISO] DROP CONSTRAINT [FK_MODULO_PERMISOMODULO];
GO
IF OBJECT_ID(N'[dbo].[FK_MODULO_PERMISOUSUARIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MODULO_PERMISO] DROP CONSTRAINT [FK_MODULO_PERMISOUSUARIO];
GO
IF OBJECT_ID(N'[dbo].[FK_MODULO_PERMISOUSUARIO1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MODULO_PERMISO] DROP CONSTRAINT [FK_MODULO_PERMISOUSUARIO1];
GO
IF OBJECT_ID(N'[dbo].[FK_CLIENTEPERSONA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CLIENTE] DROP CONSTRAINT [FK_CLIENTEPERSONA];
GO
IF OBJECT_ID(N'[dbo].[FK_PERSONA_AGENDAPERSONA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PERSONA_AGENDA] DROP CONSTRAINT [FK_PERSONA_AGENDAPERSONA];
GO
IF OBJECT_ID(N'[dbo].[FK_LOTEZONA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LOTE] DROP CONSTRAINT [FK_LOTEZONA];
GO
IF OBJECT_ID(N'[dbo].[FK_MODULOMODULO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MODULO] DROP CONSTRAINT [FK_MODULOMODULO];
GO
IF OBJECT_ID(N'[dbo].[FK_CLIENTEESTADO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CLIENTE] DROP CONSTRAINT [FK_CLIENTEESTADO];
GO
IF OBJECT_ID(N'[dbo].[FK_ExcepcionUSUARIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EXCEPCION] DROP CONSTRAINT [FK_ExcepcionUSUARIO];
GO
IF OBJECT_ID(N'[dbo].[FK_LOTEESTADO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LOTE] DROP CONSTRAINT [FK_LOTEESTADO];
GO
IF OBJECT_ID(N'[dbo].[FK_CONTROLMODULO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CONTROL] DROP CONSTRAINT [FK_CONTROLMODULO];
GO
IF OBJECT_ID(N'[dbo].[FK_CONTROL_PERMISOCONTROL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CONTROL_PERMISO] DROP CONSTRAINT [FK_CONTROL_PERMISOCONTROL];
GO
IF OBJECT_ID(N'[dbo].[FK_CONTROL_PERMISOUSUARIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CONTROL_PERMISO] DROP CONSTRAINT [FK_CONTROL_PERMISOUSUARIO];
GO
IF OBJECT_ID(N'[dbo].[FK_CONTROL_PERMISOUSUARIO1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CONTROL_PERMISO] DROP CONSTRAINT [FK_CONTROL_PERMISOUSUARIO1];
GO
IF OBJECT_ID(N'[dbo].[FK_CLIENTES_SOCIOSCLIENTE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CLIENTES_SOCIOS] DROP CONSTRAINT [FK_CLIENTES_SOCIOSCLIENTE];
GO
IF OBJECT_ID(N'[dbo].[FK_SOCIOSCLIENTES_SOCIOS]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CLIENTES_SOCIOS] DROP CONSTRAINT [FK_SOCIOSCLIENTES_SOCIOS];
GO
IF OBJECT_ID(N'[dbo].[FK_CLIENTELOTECLIENTE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CLIENTELOTE] DROP CONSTRAINT [FK_CLIENTELOTECLIENTE];
GO
IF OBJECT_ID(N'[dbo].[FK_CLIENTELOTELOTE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CLIENTELOTE] DROP CONSTRAINT [FK_CLIENTELOTELOTE];
GO
IF OBJECT_ID(N'[dbo].[FK_CLIENTELOTEUSUARIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CLIENTELOTE] DROP CONSTRAINT [FK_CLIENTELOTEUSUARIO];
GO
IF OBJECT_ID(N'[dbo].[FK_PAGOUSUARIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PAGO] DROP CONSTRAINT [FK_PAGOUSUARIO];
GO
IF OBJECT_ID(N'[dbo].[FK_PAGOCLIENTELOTE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PAGO] DROP CONSTRAINT [FK_PAGOCLIENTELOTE];
GO
IF OBJECT_ID(N'[dbo].[FK_CLIENTELOTESOCIOS]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CLIENTELOTE] DROP CONSTRAINT [FK_CLIENTELOTESOCIOS];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[USUARIO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[USUARIO];
GO
IF OBJECT_ID(N'[dbo].[PERSONA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PERSONA];
GO
IF OBJECT_ID(N'[dbo].[MODULO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MODULO];
GO
IF OBJECT_ID(N'[dbo].[ROL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ROL];
GO
IF OBJECT_ID(N'[dbo].[MODULO_PERMISO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MODULO_PERMISO];
GO
IF OBJECT_ID(N'[dbo].[CLIENTE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CLIENTE];
GO
IF OBJECT_ID(N'[dbo].[AGENDA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AGENDA];
GO
IF OBJECT_ID(N'[dbo].[ESTADO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ESTADO];
GO
IF OBJECT_ID(N'[dbo].[ZONA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZONA];
GO
IF OBJECT_ID(N'[dbo].[LOTE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LOTE];
GO
IF OBJECT_ID(N'[dbo].[PERSONA_AGENDA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PERSONA_AGENDA];
GO
IF OBJECT_ID(N'[dbo].[VARIABLEGLOBAL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VARIABLEGLOBAL];
GO
IF OBJECT_ID(N'[dbo].[EXCEPCION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EXCEPCION];
GO
IF OBJECT_ID(N'[dbo].[CONTROL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CONTROL];
GO
IF OBJECT_ID(N'[dbo].[CONTROL_PERMISO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CONTROL_PERMISO];
GO
IF OBJECT_ID(N'[dbo].[SOCIOS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SOCIOS];
GO
IF OBJECT_ID(N'[dbo].[CLIENTES_SOCIOS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CLIENTES_SOCIOS];
GO
IF OBJECT_ID(N'[dbo].[CLIENTELOTE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CLIENTELOTE];
GO
IF OBJECT_ID(N'[dbo].[PAGO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PAGO];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'USUARIO'
CREATE TABLE [dbo].[USUARIO] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Alias] nvarchar(45)  NOT NULL,
    [Password] nvarchar(255)  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [ESTADOId] int  NOT NULL,
    [PERSONAId] int  NOT NULL,
    [ROLId] int  NOT NULL
);
GO

-- Creating table 'PERSONA'
CREATE TABLE [dbo].[PERSONA] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombres] nvarchar(120)  NOT NULL,
    [Apellidos] nvarchar(120)  NOT NULL,
    [FechaNacimiento] datetime  NULL,
    [Curp] nchar(18)  NULL,
    [Calle] nvarchar(120)  NULL,
    [NoExt] nvarchar(15)  NULL,
    [NoInt] nvarchar(15)  NULL,
    [Colonia] nvarchar(100)  NULL,
    [Localidad] nvarchar(100)  NULL,
    [Municipio] nvarchar(65)  NULL,
    [CodigoPostal] nvarchar(6)  NULL,
    [EntidadFederativa] nvarchar(65)  NULL
);
GO

-- Creating table 'MODULO'
CREATE TABLE [dbo].[MODULO] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(120)  NOT NULL,
    [Ruta] nvarchar(450)  NULL,
    [Icono] nvarchar(250)  NULL,
    [MODULOId] int  NULL
);
GO

-- Creating table 'ROL'
CREATE TABLE [dbo].[ROL] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(35)  NOT NULL
);
GO

-- Creating table 'MODULO_PERMISO'
CREATE TABLE [dbo].[MODULO_PERMISO] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [MODULOId] int  NOT NULL,
    [USUARIOId] int  NOT NULL,
    [Motivo] nvarchar(max)  NOT NULL,
    [USUARIOAUTORIZOId] int  NOT NULL
);
GO

-- Creating table 'CLIENTE'
CREATE TABLE [dbo].[CLIENTE] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Clave] nchar(5)  NOT NULL,
    [PERSONAId] int  NOT NULL,
    [ESTADOId] int  NOT NULL
);
GO

-- Creating table 'AGENDA'
CREATE TABLE [dbo].[AGENDA] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Tipo] int  NOT NULL,
    [Valor] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'ESTADO'
CREATE TABLE [dbo].[ESTADO] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(35)  NOT NULL,
    [Proceso] nvarchar(100)  NOT NULL,
    [Baja] bit  NOT NULL
);
GO

-- Creating table 'ZONA'
CREATE TABLE [dbo].[ZONA] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [NoManzanas] int  NULL,
    [NoLotes] int  NOT NULL,
    [Direccion] nvarchar(450)  NOT NULL
);
GO

-- Creating table 'LOTE'
CREATE TABLE [dbo].[LOTE] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Identificador] nvarchar(10)  NOT NULL,
    [ZONAId] int  NOT NULL,
    [MNorte] decimal(18,7)  NOT NULL,
    [MSur] decimal(18,7)  NOT NULL,
    [MOeste] decimal(18,7)  NOT NULL,
    [MEste] decimal(18,7)  NOT NULL,
    [CNorte] nvarchar(350)  NULL,
    [CSur] nvarchar(350)  NULL,
    [COeste] nvarchar(350)  NULL,
    [CEste] nvarchar(350)  NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Precio] decimal(18,7)  NOT NULL,
    [Manzana] int  NULL,
    [ESTADOId] int  NOT NULL
);
GO

-- Creating table 'PERSONA_AGENDA'
CREATE TABLE [dbo].[PERSONA_AGENDA] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AGENDAId] int  NOT NULL,
    [PERSONAId] int  NOT NULL
);
GO

-- Creating table 'VARIABLEGLOBAL'
CREATE TABLE [dbo].[VARIABLEGLOBAL] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(65)  NOT NULL,
    [Tipo] nvarchar(10)  NOT NULL,
    [Valor] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EXCEPCION'
CREATE TABLE [dbo].[EXCEPCION] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Formulario] nvarchar(100)  NOT NULL,
    [Resumen] nvarchar(max)  NOT NULL,
    [Detalle] nvarchar(max)  NOT NULL,
    [USUARIOId] int  NOT NULL
);
GO

-- Creating table 'CONTROL'
CREATE TABLE [dbo].[CONTROL] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(65)  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Baja] bit  NOT NULL,
    [MODULOId] int  NOT NULL
);
GO

-- Creating table 'CONTROL_PERMISO'
CREATE TABLE [dbo].[CONTROL_PERMISO] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CONTROLId] int  NOT NULL,
    [USUARIOSOLICITAId] int  NOT NULL,
    [USUARIOASIGNOId] int  NOT NULL,
    [Visible] nvarchar(max)  NOT NULL,
    [Enabled] nvarchar(max)  NOT NULL,
    [Readonly] bit  NOT NULL,
    [FechaRegistro] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SOCIOS'
CREATE TABLE [dbo].[SOCIOS] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CLIENTES_SOCIOS'
CREATE TABLE [dbo].[CLIENTES_SOCIOS] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CLIENTEId] int  NOT NULL,
    [SOCIOSId] int  NOT NULL
);
GO

-- Creating table 'CLIENTELOTE'
CREATE TABLE [dbo].[CLIENTELOTE] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Folio] nchar(9)  NOT NULL,
    [FechaArrendamiento] datetime  NOT NULL,
    [CLIENTEId] int  NOT NULL,
    [LOTEId] int  NOT NULL,
    [USUARIOOperacionId] int  NOT NULL,
    [NoPagos] int  NOT NULL,
    [PrecioInicial] decimal(18,7)  NOT NULL,
    [DiaPago] int  NOT NULL,
    [FechaReimpresion] datetime  NULL,
    [SOCIOSId] int  NULL
);
GO

-- Creating table 'PAGO'
CREATE TABLE [dbo].[PAGO] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Folio] nchar(11)  NOT NULL,
    [FechaEmision] datetime  NOT NULL,
    [ContratoId] int  NOT NULL,
    [USUARIORecibeId] int  NOT NULL,
    [Monto] decimal(18,7)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'USUARIO'
ALTER TABLE [dbo].[USUARIO]
ADD CONSTRAINT [PK_USUARIO]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PERSONA'
ALTER TABLE [dbo].[PERSONA]
ADD CONSTRAINT [PK_PERSONA]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MODULO'
ALTER TABLE [dbo].[MODULO]
ADD CONSTRAINT [PK_MODULO]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ROL'
ALTER TABLE [dbo].[ROL]
ADD CONSTRAINT [PK_ROL]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MODULO_PERMISO'
ALTER TABLE [dbo].[MODULO_PERMISO]
ADD CONSTRAINT [PK_MODULO_PERMISO]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CLIENTE'
ALTER TABLE [dbo].[CLIENTE]
ADD CONSTRAINT [PK_CLIENTE]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AGENDA'
ALTER TABLE [dbo].[AGENDA]
ADD CONSTRAINT [PK_AGENDA]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ESTADO'
ALTER TABLE [dbo].[ESTADO]
ADD CONSTRAINT [PK_ESTADO]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ZONA'
ALTER TABLE [dbo].[ZONA]
ADD CONSTRAINT [PK_ZONA]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LOTE'
ALTER TABLE [dbo].[LOTE]
ADD CONSTRAINT [PK_LOTE]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PERSONA_AGENDA'
ALTER TABLE [dbo].[PERSONA_AGENDA]
ADD CONSTRAINT [PK_PERSONA_AGENDA]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VARIABLEGLOBAL'
ALTER TABLE [dbo].[VARIABLEGLOBAL]
ADD CONSTRAINT [PK_VARIABLEGLOBAL]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EXCEPCION'
ALTER TABLE [dbo].[EXCEPCION]
ADD CONSTRAINT [PK_EXCEPCION]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CONTROL'
ALTER TABLE [dbo].[CONTROL]
ADD CONSTRAINT [PK_CONTROL]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CONTROL_PERMISO'
ALTER TABLE [dbo].[CONTROL_PERMISO]
ADD CONSTRAINT [PK_CONTROL_PERMISO]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SOCIOS'
ALTER TABLE [dbo].[SOCIOS]
ADD CONSTRAINT [PK_SOCIOS]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CLIENTES_SOCIOS'
ALTER TABLE [dbo].[CLIENTES_SOCIOS]
ADD CONSTRAINT [PK_CLIENTES_SOCIOS]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CLIENTELOTE'
ALTER TABLE [dbo].[CLIENTELOTE]
ADD CONSTRAINT [PK_CLIENTELOTE]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PAGO'
ALTER TABLE [dbo].[PAGO]
ADD CONSTRAINT [PK_PAGO]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ESTADOId] in table 'USUARIO'
ALTER TABLE [dbo].[USUARIO]
ADD CONSTRAINT [FK_USUARIOESTADO]
    FOREIGN KEY ([ESTADOId])
    REFERENCES [dbo].[ESTADO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USUARIOESTADO'
CREATE INDEX [IX_FK_USUARIOESTADO]
ON [dbo].[USUARIO]
    ([ESTADOId]);
GO

-- Creating foreign key on [PERSONAId] in table 'USUARIO'
ALTER TABLE [dbo].[USUARIO]
ADD CONSTRAINT [FK_USUARIOPERSONA]
    FOREIGN KEY ([PERSONAId])
    REFERENCES [dbo].[PERSONA]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USUARIOPERSONA'
CREATE INDEX [IX_FK_USUARIOPERSONA]
ON [dbo].[USUARIO]
    ([PERSONAId]);
GO

-- Creating foreign key on [ROLId] in table 'USUARIO'
ALTER TABLE [dbo].[USUARIO]
ADD CONSTRAINT [FK_ROLUSUARIO]
    FOREIGN KEY ([ROLId])
    REFERENCES [dbo].[ROL]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ROLUSUARIO'
CREATE INDEX [IX_FK_ROLUSUARIO]
ON [dbo].[USUARIO]
    ([ROLId]);
GO

-- Creating foreign key on [AGENDAId] in table 'PERSONA_AGENDA'
ALTER TABLE [dbo].[PERSONA_AGENDA]
ADD CONSTRAINT [FK_AGENDAPERSONA_AGENDA]
    FOREIGN KEY ([AGENDAId])
    REFERENCES [dbo].[AGENDA]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AGENDAPERSONA_AGENDA'
CREATE INDEX [IX_FK_AGENDAPERSONA_AGENDA]
ON [dbo].[PERSONA_AGENDA]
    ([AGENDAId]);
GO

-- Creating foreign key on [MODULOId] in table 'MODULO_PERMISO'
ALTER TABLE [dbo].[MODULO_PERMISO]
ADD CONSTRAINT [FK_MODULO_PERMISOMODULO]
    FOREIGN KEY ([MODULOId])
    REFERENCES [dbo].[MODULO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MODULO_PERMISOMODULO'
CREATE INDEX [IX_FK_MODULO_PERMISOMODULO]
ON [dbo].[MODULO_PERMISO]
    ([MODULOId]);
GO

-- Creating foreign key on [USUARIOId] in table 'MODULO_PERMISO'
ALTER TABLE [dbo].[MODULO_PERMISO]
ADD CONSTRAINT [FK_MODULO_PERMISOUSUARIO]
    FOREIGN KEY ([USUARIOId])
    REFERENCES [dbo].[USUARIO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MODULO_PERMISOUSUARIO'
CREATE INDEX [IX_FK_MODULO_PERMISOUSUARIO]
ON [dbo].[MODULO_PERMISO]
    ([USUARIOId]);
GO

-- Creating foreign key on [USUARIOAUTORIZOId] in table 'MODULO_PERMISO'
ALTER TABLE [dbo].[MODULO_PERMISO]
ADD CONSTRAINT [FK_MODULO_PERMISOUSUARIO1]
    FOREIGN KEY ([USUARIOAUTORIZOId])
    REFERENCES [dbo].[USUARIO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MODULO_PERMISOUSUARIO1'
CREATE INDEX [IX_FK_MODULO_PERMISOUSUARIO1]
ON [dbo].[MODULO_PERMISO]
    ([USUARIOAUTORIZOId]);
GO

-- Creating foreign key on [PERSONAId] in table 'CLIENTE'
ALTER TABLE [dbo].[CLIENTE]
ADD CONSTRAINT [FK_CLIENTEPERSONA]
    FOREIGN KEY ([PERSONAId])
    REFERENCES [dbo].[PERSONA]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CLIENTEPERSONA'
CREATE INDEX [IX_FK_CLIENTEPERSONA]
ON [dbo].[CLIENTE]
    ([PERSONAId]);
GO

-- Creating foreign key on [PERSONAId] in table 'PERSONA_AGENDA'
ALTER TABLE [dbo].[PERSONA_AGENDA]
ADD CONSTRAINT [FK_PERSONA_AGENDAPERSONA]
    FOREIGN KEY ([PERSONAId])
    REFERENCES [dbo].[PERSONA]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PERSONA_AGENDAPERSONA'
CREATE INDEX [IX_FK_PERSONA_AGENDAPERSONA]
ON [dbo].[PERSONA_AGENDA]
    ([PERSONAId]);
GO

-- Creating foreign key on [ZONAId] in table 'LOTE'
ALTER TABLE [dbo].[LOTE]
ADD CONSTRAINT [FK_LOTEZONA]
    FOREIGN KEY ([ZONAId])
    REFERENCES [dbo].[ZONA]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LOTEZONA'
CREATE INDEX [IX_FK_LOTEZONA]
ON [dbo].[LOTE]
    ([ZONAId]);
GO

-- Creating foreign key on [MODULOId] in table 'MODULO'
ALTER TABLE [dbo].[MODULO]
ADD CONSTRAINT [FK_MODULOMODULO]
    FOREIGN KEY ([MODULOId])
    REFERENCES [dbo].[MODULO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MODULOMODULO'
CREATE INDEX [IX_FK_MODULOMODULO]
ON [dbo].[MODULO]
    ([MODULOId]);
GO

-- Creating foreign key on [ESTADOId] in table 'CLIENTE'
ALTER TABLE [dbo].[CLIENTE]
ADD CONSTRAINT [FK_CLIENTEESTADO]
    FOREIGN KEY ([ESTADOId])
    REFERENCES [dbo].[ESTADO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CLIENTEESTADO'
CREATE INDEX [IX_FK_CLIENTEESTADO]
ON [dbo].[CLIENTE]
    ([ESTADOId]);
GO

-- Creating foreign key on [USUARIOId] in table 'EXCEPCION'
ALTER TABLE [dbo].[EXCEPCION]
ADD CONSTRAINT [FK_ExcepcionUSUARIO]
    FOREIGN KEY ([USUARIOId])
    REFERENCES [dbo].[USUARIO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExcepcionUSUARIO'
CREATE INDEX [IX_FK_ExcepcionUSUARIO]
ON [dbo].[EXCEPCION]
    ([USUARIOId]);
GO

-- Creating foreign key on [ESTADOId] in table 'LOTE'
ALTER TABLE [dbo].[LOTE]
ADD CONSTRAINT [FK_LOTEESTADO]
    FOREIGN KEY ([ESTADOId])
    REFERENCES [dbo].[ESTADO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LOTEESTADO'
CREATE INDEX [IX_FK_LOTEESTADO]
ON [dbo].[LOTE]
    ([ESTADOId]);
GO

-- Creating foreign key on [MODULOId] in table 'CONTROL'
ALTER TABLE [dbo].[CONTROL]
ADD CONSTRAINT [FK_CONTROLMODULO]
    FOREIGN KEY ([MODULOId])
    REFERENCES [dbo].[MODULO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CONTROLMODULO'
CREATE INDEX [IX_FK_CONTROLMODULO]
ON [dbo].[CONTROL]
    ([MODULOId]);
GO

-- Creating foreign key on [CONTROLId] in table 'CONTROL_PERMISO'
ALTER TABLE [dbo].[CONTROL_PERMISO]
ADD CONSTRAINT [FK_CONTROL_PERMISOCONTROL]
    FOREIGN KEY ([CONTROLId])
    REFERENCES [dbo].[CONTROL]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CONTROL_PERMISOCONTROL'
CREATE INDEX [IX_FK_CONTROL_PERMISOCONTROL]
ON [dbo].[CONTROL_PERMISO]
    ([CONTROLId]);
GO

-- Creating foreign key on [USUARIOSOLICITAId] in table 'CONTROL_PERMISO'
ALTER TABLE [dbo].[CONTROL_PERMISO]
ADD CONSTRAINT [FK_CONTROL_PERMISOUSUARIO]
    FOREIGN KEY ([USUARIOSOLICITAId])
    REFERENCES [dbo].[USUARIO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CONTROL_PERMISOUSUARIO'
CREATE INDEX [IX_FK_CONTROL_PERMISOUSUARIO]
ON [dbo].[CONTROL_PERMISO]
    ([USUARIOSOLICITAId]);
GO

-- Creating foreign key on [USUARIOASIGNOId] in table 'CONTROL_PERMISO'
ALTER TABLE [dbo].[CONTROL_PERMISO]
ADD CONSTRAINT [FK_CONTROL_PERMISOUSUARIO1]
    FOREIGN KEY ([USUARIOASIGNOId])
    REFERENCES [dbo].[USUARIO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CONTROL_PERMISOUSUARIO1'
CREATE INDEX [IX_FK_CONTROL_PERMISOUSUARIO1]
ON [dbo].[CONTROL_PERMISO]
    ([USUARIOASIGNOId]);
GO

-- Creating foreign key on [CLIENTEId] in table 'CLIENTES_SOCIOS'
ALTER TABLE [dbo].[CLIENTES_SOCIOS]
ADD CONSTRAINT [FK_CLIENTES_SOCIOSCLIENTE]
    FOREIGN KEY ([CLIENTEId])
    REFERENCES [dbo].[CLIENTE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CLIENTES_SOCIOSCLIENTE'
CREATE INDEX [IX_FK_CLIENTES_SOCIOSCLIENTE]
ON [dbo].[CLIENTES_SOCIOS]
    ([CLIENTEId]);
GO

-- Creating foreign key on [SOCIOSId] in table 'CLIENTES_SOCIOS'
ALTER TABLE [dbo].[CLIENTES_SOCIOS]
ADD CONSTRAINT [FK_SOCIOSCLIENTES_SOCIOS]
    FOREIGN KEY ([SOCIOSId])
    REFERENCES [dbo].[SOCIOS]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SOCIOSCLIENTES_SOCIOS'
CREATE INDEX [IX_FK_SOCIOSCLIENTES_SOCIOS]
ON [dbo].[CLIENTES_SOCIOS]
    ([SOCIOSId]);
GO

-- Creating foreign key on [CLIENTEId] in table 'CLIENTELOTE'
ALTER TABLE [dbo].[CLIENTELOTE]
ADD CONSTRAINT [FK_CLIENTELOTECLIENTE]
    FOREIGN KEY ([CLIENTEId])
    REFERENCES [dbo].[CLIENTE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CLIENTELOTECLIENTE'
CREATE INDEX [IX_FK_CLIENTELOTECLIENTE]
ON [dbo].[CLIENTELOTE]
    ([CLIENTEId]);
GO

-- Creating foreign key on [LOTEId] in table 'CLIENTELOTE'
ALTER TABLE [dbo].[CLIENTELOTE]
ADD CONSTRAINT [FK_CLIENTELOTELOTE]
    FOREIGN KEY ([LOTEId])
    REFERENCES [dbo].[LOTE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CLIENTELOTELOTE'
CREATE INDEX [IX_FK_CLIENTELOTELOTE]
ON [dbo].[CLIENTELOTE]
    ([LOTEId]);
GO

-- Creating foreign key on [USUARIOOperacionId] in table 'CLIENTELOTE'
ALTER TABLE [dbo].[CLIENTELOTE]
ADD CONSTRAINT [FK_CLIENTELOTEUSUARIO]
    FOREIGN KEY ([USUARIOOperacionId])
    REFERENCES [dbo].[USUARIO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CLIENTELOTEUSUARIO'
CREATE INDEX [IX_FK_CLIENTELOTEUSUARIO]
ON [dbo].[CLIENTELOTE]
    ([USUARIOOperacionId]);
GO

-- Creating foreign key on [USUARIORecibeId] in table 'PAGO'
ALTER TABLE [dbo].[PAGO]
ADD CONSTRAINT [FK_PAGOUSUARIO]
    FOREIGN KEY ([USUARIORecibeId])
    REFERENCES [dbo].[USUARIO]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PAGOUSUARIO'
CREATE INDEX [IX_FK_PAGOUSUARIO]
ON [dbo].[PAGO]
    ([USUARIORecibeId]);
GO

-- Creating foreign key on [ContratoId] in table 'PAGO'
ALTER TABLE [dbo].[PAGO]
ADD CONSTRAINT [FK_PAGOCLIENTELOTE]
    FOREIGN KEY ([ContratoId])
    REFERENCES [dbo].[CLIENTELOTE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PAGOCLIENTELOTE'
CREATE INDEX [IX_FK_PAGOCLIENTELOTE]
ON [dbo].[PAGO]
    ([ContratoId]);
GO

-- Creating foreign key on [SOCIOSId] in table 'CLIENTELOTE'
ALTER TABLE [dbo].[CLIENTELOTE]
ADD CONSTRAINT [FK_CLIENTELOTESOCIOS]
    FOREIGN KEY ([SOCIOSId])
    REFERENCES [dbo].[SOCIOS]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CLIENTELOTESOCIOS'
CREATE INDEX [IX_FK_CLIENTELOTESOCIOS]
ON [dbo].[CLIENTELOTE]
    ([SOCIOSId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
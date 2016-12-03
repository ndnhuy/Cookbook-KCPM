
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/16/2014 11:55:36
-- Generated from EDMX file: C:\Users\Nguyen\Documents\lap-trinh-hien-dai\Web Service\Web Service\Models\FoodLover.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db81560d64cbf14b329a89a2d400507c54];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_NguoiDungBinhLuan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BinhLuan] DROP CONSTRAINT [FK_NguoiDungBinhLuan];
GO
IF OBJECT_ID(N'[dbo].[FK_MonAnBinhLuan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BinhLuan] DROP CONSTRAINT [FK_MonAnBinhLuan];
GO
IF OBJECT_ID(N'[dbo].[FK_NguoiDungMonAn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MonAn] DROP CONSTRAINT [FK_NguoiDungMonAn];
GO
IF OBJECT_ID(N'[dbo].[FK_MucDoMonAn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MonAn] DROP CONSTRAINT [FK_MucDoMonAn];
GO
IF OBJECT_ID(N'[dbo].[FK_MonAnLoaiMon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MonAn] DROP CONSTRAINT [FK_MonAnLoaiMon];
GO
IF OBJECT_ID(N'[dbo].[FK_NguoiDungThich]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Thich] DROP CONSTRAINT [FK_NguoiDungThich];
GO
IF OBJECT_ID(N'[dbo].[FK_MonAnThich]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Thich] DROP CONSTRAINT [FK_MonAnThich];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[NguoiDung]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NguoiDung];
GO
IF OBJECT_ID(N'[dbo].[MonAn]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MonAn];
GO
IF OBJECT_ID(N'[dbo].[BinhLuan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BinhLuan];
GO
IF OBJECT_ID(N'[dbo].[MucDo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MucDo];
GO
IF OBJECT_ID(N'[dbo].[LoaiMon]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoaiMon];
GO
IF OBJECT_ID(N'[dbo].[Thich]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Thich];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'NguoiDung'
CREATE TABLE [dbo].[NguoiDung] (
    [MaNguoiDung] nvarchar(255)  NOT NULL,
    [Ho] nvarchar(max)  NULL,
    [Ten] nvarchar(max)  NULL,
    [Hinh] nvarchar(max)  NULL,
    [DiaChi] nvarchar(max)  NULL
);
GO

-- Creating table 'MonAn'
CREATE TABLE [dbo].[MonAn] (
    [MaMonAn] int IDENTITY(1,1) NOT NULL,
    [TenMon] nvarchar(max)  NULL,
    [GioiThieu] nvarchar(max)  NULL,
    [Hinh] nvarchar(max)  NULL,
    [ThoiGianChuanBi] int  NULL,
    [ThoiGianNau] int  NULL,
    [NgayDang] datetime  NULL,
    [NguyenLieu] nvarchar(max)  NULL,
    [CachLam] nvarchar(max)  NULL,
    [MaNguoiDung] nvarchar(255)  NOT NULL,
    [MaMucDo] int  NOT NULL,
    [MaLoaiMon] int  NOT NULL
);
GO

-- Creating table 'BinhLuan'
CREATE TABLE [dbo].[BinhLuan] (
    [MaBinhLuan] int IDENTITY(1,1) NOT NULL,
    [MaNguoiDung] nvarchar(255)  NOT NULL,
    [MaMonAn] int  NOT NULL,
    [NgayDang] datetime  NULL,
    [NoiDung] nvarchar(255)  NULL
);
GO

-- Creating table 'MucDo'
CREATE TABLE [dbo].[MucDo] (
    [MaMucDo] int IDENTITY(1,1) NOT NULL,
    [TenMucDo] nvarchar(max)  NULL
);
GO

-- Creating table 'LoaiMon'
CREATE TABLE [dbo].[LoaiMon] (
    [MaLoaiMon] int IDENTITY(1,1) NOT NULL,
    [TenLoaiMon] nvarchar(max)  NULL
);
GO

-- Creating table 'Thich'
CREATE TABLE [dbo].[Thich] (
    [MaNguoiDung] nvarchar(255)  NOT NULL,
    [MaMonAn] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MaNguoiDung] in table 'NguoiDung'
ALTER TABLE [dbo].[NguoiDung]
ADD CONSTRAINT [PK_NguoiDung]
    PRIMARY KEY CLUSTERED ([MaNguoiDung] ASC);
GO

-- Creating primary key on [MaMonAn] in table 'MonAn'
ALTER TABLE [dbo].[MonAn]
ADD CONSTRAINT [PK_MonAn]
    PRIMARY KEY CLUSTERED ([MaMonAn] ASC);
GO

-- Creating primary key on [MaBinhLuan] in table 'BinhLuan'
ALTER TABLE [dbo].[BinhLuan]
ADD CONSTRAINT [PK_BinhLuan]
    PRIMARY KEY CLUSTERED ([MaBinhLuan] ASC);
GO

-- Creating primary key on [MaMucDo] in table 'MucDo'
ALTER TABLE [dbo].[MucDo]
ADD CONSTRAINT [PK_MucDo]
    PRIMARY KEY CLUSTERED ([MaMucDo] ASC);
GO

-- Creating primary key on [MaLoaiMon] in table 'LoaiMon'
ALTER TABLE [dbo].[LoaiMon]
ADD CONSTRAINT [PK_LoaiMon]
    PRIMARY KEY CLUSTERED ([MaLoaiMon] ASC);
GO

-- Creating primary key on [MaNguoiDung], [MaMonAn] in table 'Thich'
ALTER TABLE [dbo].[Thich]
ADD CONSTRAINT [PK_Thich]
    PRIMARY KEY CLUSTERED ([MaNguoiDung], [MaMonAn] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MaNguoiDung] in table 'BinhLuan'
ALTER TABLE [dbo].[BinhLuan]
ADD CONSTRAINT [FK_NguoiDungBinhLuan]
    FOREIGN KEY ([MaNguoiDung])
    REFERENCES [dbo].[NguoiDung]
        ([MaNguoiDung])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NguoiDungBinhLuan'
CREATE INDEX [IX_FK_NguoiDungBinhLuan]
ON [dbo].[BinhLuan]
    ([MaNguoiDung]);
GO

-- Creating foreign key on [MaMonAn] in table 'BinhLuan'
ALTER TABLE [dbo].[BinhLuan]
ADD CONSTRAINT [FK_MonAnBinhLuan]
    FOREIGN KEY ([MaMonAn])
    REFERENCES [dbo].[MonAn]
        ([MaMonAn])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MonAnBinhLuan'
CREATE INDEX [IX_FK_MonAnBinhLuan]
ON [dbo].[BinhLuan]
    ([MaMonAn]);
GO

-- Creating foreign key on [MaNguoiDung] in table 'MonAn'
ALTER TABLE [dbo].[MonAn]
ADD CONSTRAINT [FK_NguoiDungMonAn]
    FOREIGN KEY ([MaNguoiDung])
    REFERENCES [dbo].[NguoiDung]
        ([MaNguoiDung])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NguoiDungMonAn'
CREATE INDEX [IX_FK_NguoiDungMonAn]
ON [dbo].[MonAn]
    ([MaNguoiDung]);
GO

-- Creating foreign key on [MaMucDo] in table 'MonAn'
ALTER TABLE [dbo].[MonAn]
ADD CONSTRAINT [FK_MucDoMonAn]
    FOREIGN KEY ([MaMucDo])
    REFERENCES [dbo].[MucDo]
        ([MaMucDo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MucDoMonAn'
CREATE INDEX [IX_FK_MucDoMonAn]
ON [dbo].[MonAn]
    ([MaMucDo]);
GO

-- Creating foreign key on [MaLoaiMon] in table 'MonAn'
ALTER TABLE [dbo].[MonAn]
ADD CONSTRAINT [FK_MonAnLoaiMon]
    FOREIGN KEY ([MaLoaiMon])
    REFERENCES [dbo].[LoaiMon]
        ([MaLoaiMon])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MonAnLoaiMon'
CREATE INDEX [IX_FK_MonAnLoaiMon]
ON [dbo].[MonAn]
    ([MaLoaiMon]);
GO

-- Creating foreign key on [MaNguoiDung] in table 'Thich'
ALTER TABLE [dbo].[Thich]
ADD CONSTRAINT [FK_NguoiDungThich]
    FOREIGN KEY ([MaNguoiDung])
    REFERENCES [dbo].[NguoiDung]
        ([MaNguoiDung])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MaMonAn] in table 'Thich'
ALTER TABLE [dbo].[Thich]
ADD CONSTRAINT [FK_MonAnThich]
    FOREIGN KEY ([MaMonAn])
    REFERENCES [dbo].[MonAn]
        ([MaMonAn])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MonAnThich'
CREATE INDEX [IX_FK_MonAnThich]
ON [dbo].[Thich]
    ([MaMonAn]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
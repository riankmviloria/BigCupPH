
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/25/2022 19:37:54
-- Generated from EDMX file: C:\Users\rianm\source\Workspaces\BigCup\BigCupPayroll\BigCupPayroll.DB\BigCupPayrollDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BigCupPayroll];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserRole];
GO
IF OBJECT_ID(N'[dbo].[FK_UserSalary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Salaries] DROP CONSTRAINT [FK_UserSalary];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAttendance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attendances] DROP CONSTRAINT [FK_UserAttendance];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollAllowance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Allowances] DROP CONSTRAINT [FK_PayrollAllowance];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollDeduction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Deductions] DROP CONSTRAINT [FK_PayrollDeduction];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPayroll]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Payrolls] DROP CONSTRAINT [FK_UserPayroll];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Salaries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Salaries];
GO
IF OBJECT_ID(N'[dbo].[Attendances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attendances];
GO
IF OBJECT_ID(N'[dbo].[Payrolls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payrolls];
GO
IF OBJECT_ID(N'[dbo].[Allowances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Allowances];
GO
IF OBJECT_ID(N'[dbo].[Deductions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Deductions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmployeeNo] nvarchar(max)  NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [HiredDate] datetime  NULL,
    [BirthDate] datetime  NULL,
    [Role_Id] int  NOT NULL,
    [Position] nvarchar(max)  NULL,
    [BranchId] int  NOT NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'Salaries'
CREATE TABLE [dbo].[Salaries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BasicPay] decimal(18,0)  NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Attendances'
CREATE TABLE [dbo].[Attendances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TimeIn] datetime  NOT NULL,
    [TimeOut] datetime  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [WorkDate] datetime  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Payrolls'
CREATE TABLE [dbo].[Payrolls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CutOffDate] datetime  NOT NULL,
    [GrossPay] decimal(18,0)  NOT NULL,
    [NetPay] decimal(18,0)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Allowances'
CREATE TABLE [dbo].[Allowances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [PayrollId] int  NOT NULL
);
GO

-- Creating table 'Deductions'
CREATE TABLE [dbo].[Deductions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [PayrollId] int  NOT NULL
);
GO

-- Creating table 'Branches'
CREATE TABLE [dbo].[Branches] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Location] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Salaries'
ALTER TABLE [dbo].[Salaries]
ADD CONSTRAINT [PK_Salaries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [PK_Attendances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Payrolls'
ALTER TABLE [dbo].[Payrolls]
ADD CONSTRAINT [PK_Payrolls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Allowances'
ALTER TABLE [dbo].[Allowances]
ADD CONSTRAINT [PK_Allowances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Deductions'
ALTER TABLE [dbo].[Deductions]
ADD CONSTRAINT [PK_Deductions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Branches'
ALTER TABLE [dbo].[Branches]
ADD CONSTRAINT [PK_Branches]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Role_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserRole]
    FOREIGN KEY ([Role_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole'
CREATE INDEX [IX_FK_UserRole]
ON [dbo].[Users]
    ([Role_Id]);
GO

-- Creating foreign key on [UserId] in table 'Salaries'
ALTER TABLE [dbo].[Salaries]
ADD CONSTRAINT [FK_UserSalary]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSalary'
CREATE INDEX [IX_FK_UserSalary]
ON [dbo].[Salaries]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [FK_UserAttendance]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAttendance'
CREATE INDEX [IX_FK_UserAttendance]
ON [dbo].[Attendances]
    ([UserId]);
GO

-- Creating foreign key on [PayrollId] in table 'Allowances'
ALTER TABLE [dbo].[Allowances]
ADD CONSTRAINT [FK_PayrollAllowance]
    FOREIGN KEY ([PayrollId])
    REFERENCES [dbo].[Payrolls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollAllowance'
CREATE INDEX [IX_FK_PayrollAllowance]
ON [dbo].[Allowances]
    ([PayrollId]);
GO

-- Creating foreign key on [PayrollId] in table 'Deductions'
ALTER TABLE [dbo].[Deductions]
ADD CONSTRAINT [FK_PayrollDeduction]
    FOREIGN KEY ([PayrollId])
    REFERENCES [dbo].[Payrolls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollDeduction'
CREATE INDEX [IX_FK_PayrollDeduction]
ON [dbo].[Deductions]
    ([PayrollId]);
GO

-- Creating foreign key on [UserId] in table 'Payrolls'
ALTER TABLE [dbo].[Payrolls]
ADD CONSTRAINT [FK_UserPayroll]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPayroll'
CREATE INDEX [IX_FK_UserPayroll]
ON [dbo].[Payrolls]
    ([UserId]);
GO

-- Creating foreign key on [BranchId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_BranchUser]
    FOREIGN KEY ([BranchId])
    REFERENCES [dbo].[Branches]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BranchUser'
CREATE INDEX [IX_FK_BranchUser]
ON [dbo].[Users]
    ([BranchId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
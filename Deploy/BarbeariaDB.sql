IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260824144149_Inicial'
)
BEGIN
    CREATE TABLE [Barbeiros] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Barbeiros] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260824144149_Inicial'
)
BEGIN
    CREATE TABLE [Clientes] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [CPF] nvarchar(max) NOT NULL,
        [Telefone] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260824144149_Inicial'
)
BEGIN
    CREATE TABLE [Servicos] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [Descricao] nvarchar(max) NOT NULL,
        [Preco] decimal(18,2) NOT NULL,
        [DuracaoMinutos] int NOT NULL,
        CONSTRAINT [PK_Servicos] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260824144149_Inicial'
)
BEGIN
    CREATE TABLE [Agendamentos] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NOT NULL,
        [BarbeiroId] int NOT NULL,
        [ServicoId] int NOT NULL,
        [Data] datetime2 NOT NULL,
        [Horario] time NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Agendamentos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Agendamentos_Barbeiros_BarbeiroId] FOREIGN KEY ([BarbeiroId]) REFERENCES [Barbeiros] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Agendamentos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Agendamentos_Servicos_ServicoId] FOREIGN KEY ([ServicoId]) REFERENCES [Servicos] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260824144149_Inicial'
)
BEGIN
    CREATE INDEX [IX_Agendamentos_BarbeiroId] ON [Agendamentos] ([BarbeiroId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260824144149_Inicial'
)
BEGIN
    CREATE INDEX [IX_Agendamentos_ClienteId] ON [Agendamentos] ([ClienteId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260824144149_Inicial'
)
BEGIN
    CREATE INDEX [IX_Agendamentos_ServicoId] ON [Agendamentos] ([ServicoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260824144149_Inicial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260824144149_Inicial', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260829203903_AdicionarSenhaHashCliente'
)
BEGIN
    ALTER TABLE [Clientes] ADD [Admin] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260829203903_AdicionarSenhaHashCliente'
)
BEGIN
    ALTER TABLE [Clientes] ADD [SenhaHash] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260829203903_AdicionarSenhaHashCliente'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260829203903_AdicionarSenhaHashCliente', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830035021_AdicionarDadosIniciais'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Servicos]') AND [c].[name] = N'Preco');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Servicos] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Servicos] ALTER COLUMN [Preco] decimal(10,2) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830035021_AdicionarDadosIniciais'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nome') AND [object_id] = OBJECT_ID(N'[Barbeiros]'))
        SET IDENTITY_INSERT [Barbeiros] ON;
    EXEC(N'INSERT INTO [Barbeiros] ([Id], [Nome])
    VALUES (1, N''Vinicius Silva Lima''),
    (2, N''Miguel Miyaki da Cruz'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nome') AND [object_id] = OBJECT_ID(N'[Barbeiros]'))
        SET IDENTITY_INSERT [Barbeiros] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830035021_AdicionarDadosIniciais'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao', N'DuracaoMinutos', N'Nome', N'Preco') AND [object_id] = OBJECT_ID(N'[Servicos]'))
        SET IDENTITY_INSERT [Servicos] ON;
    EXEC(N'INSERT INTO [Servicos] ([Id], [Descricao], [DuracaoMinutos], [Nome], [Preco])
    VALUES (1, N''Corte de cabelo completo'', 60, N''Cabelo completo'', 0.0),
    (2, N''Serviço completo de barba'', 30, N''Barba completa'', 0.0),
    (3, N''Design de sobrancelha'', 15, N''Sobrancelha'', 0.0),
    (4, N''Corte feito com máquina'', 30, N''Máquina'', 0.0),
    (5, N''Corte completo com hidratação'', 90, N''Cabelo completo + Hidratação'', 0.0),
    (6, N''Combo de cabelo, barba e sobrancelha'', 90, N''Cabelo completo + Barba + Sobrancelha'', 0.0),
    (7, N''Depilação nasal com cera'', 15, N''Depilação a cera do nariz'', 0.0),
    (8, N''Depilação da sobrancelha com cera'', 15, N''Depilação a cera da sobrancelha'', 0.0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao', N'DuracaoMinutos', N'Nome', N'Preco') AND [object_id] = OBJECT_ID(N'[Servicos]'))
        SET IDENTITY_INSERT [Servicos] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830035021_AdicionarDadosIniciais'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260830035021_AdicionarDadosIniciais', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830213442_AdicionarIndicesUnicosCliente'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Email');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Clientes] ALTER COLUMN [Email] nvarchar(450) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830213442_AdicionarIndicesUnicosCliente'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'CPF');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Clientes] ALTER COLUMN [CPF] nvarchar(450) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830213442_AdicionarIndicesUnicosCliente'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Clientes_CPF] ON [Clientes] ([CPF]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830213442_AdicionarIndicesUnicosCliente'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Clientes_Email] ON [Clientes] ([Email]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830213442_AdicionarIndicesUnicosCliente'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260830213442_AdicionarIndicesUnicosCliente', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830215121_AtualizarPrecosServicos'
)
BEGIN
    EXEC(N'UPDATE [Servicos] SET [DuracaoMinutos] = 30, [Preco] = 70.0
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830215121_AtualizarPrecosServicos'
)
BEGIN
    EXEC(N'UPDATE [Servicos] SET [Preco] = 70.0
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830215121_AtualizarPrecosServicos'
)
BEGIN
    EXEC(N'UPDATE [Servicos] SET [Preco] = 20.0
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830215121_AtualizarPrecosServicos'
)
BEGIN
    EXEC(N'UPDATE [Servicos] SET [DuracaoMinutos] = 20, [Preco] = 50.0
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830215121_AtualizarPrecosServicos'
)
BEGIN
    EXEC(N'UPDATE [Servicos] SET [DuracaoMinutos] = 50, [Preco] = 120.0
    WHERE [Id] = 5;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830215121_AtualizarPrecosServicos'
)
BEGIN
    EXEC(N'UPDATE [Servicos] SET [DuracaoMinutos] = 80, [Preco] = 160.0
    WHERE [Id] = 6;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830215121_AtualizarPrecosServicos'
)
BEGIN
    EXEC(N'UPDATE [Servicos] SET [DuracaoMinutos] = 20, [Preco] = 35.0
    WHERE [Id] = 7;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830215121_AtualizarPrecosServicos'
)
BEGIN
    EXEC(N'UPDATE [Servicos] SET [DuracaoMinutos] = 20, [Preco] = 35.0
    WHERE [Id] = 8;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260830215121_AtualizarPrecosServicos'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260830215121_AtualizarPrecosServicos', N'9.0.0');
END;

COMMIT;
GO


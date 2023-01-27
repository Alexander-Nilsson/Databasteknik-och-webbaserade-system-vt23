CREATE TABLE [dbo].[Plats] (
    [Id]  INT           IDENTITY (1, 1) NOT NULL,
    [Lan] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Tbl_Plats] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Person] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Fornamn]   VARCHAR (30) NOT NULL,
    [Efternamn] VARCHAR (30) NOT NULL,
    [Fodelsear] INT          NOT NULL,
    [Epost]     VARCHAR (50) NULL,
    [Bor]       INT          NOT NULL,
    CONSTRAINT [PK_Tbl_Person] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Person_Tbl_Plats] FOREIGN KEY ([Bor]) REFERENCES [dbo].[Plats] ([Id]),
);

CREATE TABLE [dbo].[Hobby] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Aktivitet] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tbl_Hobby] PRIMARY KEY CLUSTERED ([Id] ASC)
);
 
CREATE TABLE [dbo].[HarHobby] (
	[Id]  INT IDENTITY (1, 1) NOT NULL,
    [Hobby]  INT NOT NULL,
    [Person] INT NOT NULL,
    CONSTRAINT [PK_Tbl_HarHobby] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK1_Tbl_HarHobby] FOREIGN KEY ([Hobby]) REFERENCES [dbo].[Hobby] ([Id]),
    CONSTRAINT [FK2_Tbl_HarHobby] FOREIGN KEY ([Person]) REFERENCES [dbo].[Person] ([Id])
);

INSERT INTO [Hobby] 
([Aktivitet])
VALUES ('Souting'), ('Stickning'),('Cykling');

INSERT INTO [dbo].[Plats] ([Lan])
VALUES 
('Västerbotten'),
('Halland'),
('Södermanland'),
('Skåne'),
('Gotland'),
('Uppsala');

INSERT INTO [dbo].[Person] 
([Fornamn], [Efternamn], [Fodelsear], [Epost], [Bor]) 
VALUES 
('Tina', 'Olofsson', 1950, 'tina@epost.se', 1),
('Siv', 'Kvist', 1965, 'siv@epost.se', 3),
('Olle', 'Svensson', 1980, 'olle@epost.se', 2),
('Karin', 'Ödling', 1965, 'karin@epost', 1);

INSERT INTO [dbo].[HarHobby] 
([Hobby], [Person]) 
VALUES 
(1, 1),
(1, 3),
(2, 1),
(2, 3),
(3, 2);

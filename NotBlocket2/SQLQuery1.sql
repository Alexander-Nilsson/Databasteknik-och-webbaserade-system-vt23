CREATE TABLE [dbo].[Locations] (
    [Location_Id]  INT IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (30) NOT NULL,

    CONSTRAINT [PK_Tbl_Locations] PRIMARY KEY CLUSTERED ([Location_Id] ASC)
);

CREATE TABLE [dbo].[Profiles] (
    [Id]  INT           IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (30) NOT NULL,
    [Location_Id] INT NOT NULL,
    [Email]     VARCHAR (50) NOT NULL,
    [Password]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tbl_Profiles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Profiles_Tbl_Locations] FOREIGN KEY ([Location_Id]) REFERENCES [dbo].[Locations] ([Location_Id]),
);


CREATE TABLE [dbo].[Ads] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (30) NOT NULL,
    [Description] NVARCHAR (50) NULL,
    [Category] NVARCHAR (30) NULL,
    [Price] Int NULL,
    CONSTRAINT [PK_Tbl_Ads] PRIMARY KEY CLUSTERED ([Id] ASC)
);
 

INSERT INTO [dbo].[Locations] ([Name])
VALUES ('Halmstad'), ('Stockholm'),('Gothemburg'), ('Linkoping');

INSERT INTO [dbo].[Profiles] ([Name],[Location_Id],[Email],[Password])
VALUES 
('Axel', 1, 'Axel@gmail.com', 'Axel1234'),
('Bjorn', 2, 'bjorn4@gmail.com', '1234'),
('Hasse', 3, 'Hasse123@gmail.com', '54321'),
('Axel', 4, 'Axel2@gmail.com', 'Axel1234'),
('Simon', 1, 'Simon1234f@gmail.com', 'Simon1234f');


INSERT INTO [dbo].[Ads] 
([Name], [Price]) 
VALUES 
('Samsung Tv 56inch', 5600),
('Samsung Galaxy s6', 500),
('Ipad 4', 5600),
('Volvo v70', 500000),
('Tesla model Y', 1000000);
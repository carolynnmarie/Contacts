CREATE TABLE [dbo].[Address] (
    [AddressId]      INT           IDENTITY (1, 1) NOT NULL,
    [Street]         VARCHAR (50)  NULL,
    [StreetLineTwo]  VARCHAR (50)  NULL,
    [City]           VARCHAR (50)  NULL,
    [State]          VARCHAR (2)   NULL,
    [ZipCode]        VARCHAR (5)   NULL,
    [Country]        VARCHAR (50)  DEFAULT ('USA') NULL,
    [NonUSAAddress]  VARCHAR (MAX) NULL,
    [PrimaryAddress] BIT           NULL DEFAULT ((0)),
    [ContactId]      INT           NULL,
    PRIMARY KEY CLUSTERED ([AddressId] ASC),
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contact] ([ContactId]),
    CHECK ([ZipCode] like '[0-9][0-9][0-9][0-9][0-9]' OR [ZipCode] IS NULL OR [ZipCode]='')
);


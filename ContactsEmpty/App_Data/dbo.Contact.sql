CREATE TABLE [dbo].[Contact] (
    [ContactId]     INT          IDENTITY (1, 1) NOT NULL,
    [LastName]      VARCHAR (50) NULL,
    [FirstName]     VARCHAR (50) NULL,
    [MiddleInitial] VARCHAR (5)  NULL,
    PRIMARY KEY CLUSTERED ([ContactId] ASC)
);


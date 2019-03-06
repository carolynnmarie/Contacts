CREATE TABLE [dbo].[Email] (
    [EmailId]      INT           IDENTITY (1, 1) NOT NULL,
    [UserName]     VARCHAR (64)  NULL,
    [Domain]       VARCHAR (250) NULL,
    [PrimaryEmail] BIT           NULL,
    [ContactId]    INT           NULL,
    PRIMARY KEY CLUSTERED ([EmailId] ASC),
    CONSTRAINT [FK_Contact] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contact] ([ContactId])
);


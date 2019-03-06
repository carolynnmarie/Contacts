CREATE TABLE [dbo].[Phone] (
    [PhoneId]         INT          IDENTITY (1, 1) NOT NULL,
    [Type]            VARCHAR (50) NULL,
    [CountryCode]     VARCHAR (6)  DEFAULT ('1') NULL,
    [AreaCode]        VARCHAR (3)  NULL,
    [PhoneNumberPOne] VARCHAR (3)  NULL,
    [PhoneNumberPTwo] VARCHAR (4)  NULL,
    [Extension]       VARCHAR (10) NULL,
    [PrimaryNumber]   BIT          DEFAULT ((0)) NULL,
    [ContactId]       INT          NULL,
    PRIMARY KEY CLUSTERED ([PhoneId] ASC),
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contact] ([ContactId]),
    CHECK ([PhoneNumberPOne] like '[0-9][0-9][0-9]' OR [PhoneNumberPOne]='' OR [PhoneNumberPOne] IS NULL),
    CHECK ([PhoneNumberPTwo] like '[0-9][0-9][0-9][0-9]' OR [PhoneNumberPTwo]='' OR [PhoneNumberPTwo] IS NULL),
    CHECK ([Extension] like '%[0-9]%' OR [Extension]='' OR [Extension] IS NULL),
    CHECK ([AreaCode] like '[0-9][0-9][0-9]' OR [AreaCode]='' OR [AreaCode] IS NULL),
    CHECK ([CountryCode] like '%[0-9]%' OR [CountryCode]='' OR [CountryCode] IS NULL)
);


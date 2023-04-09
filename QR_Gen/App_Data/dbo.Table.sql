CREATE TABLE [dbo].[Table] (
    [Id]           INT            NOT NULL,
    [QRCode]	   VARCHAR (MAX)  NULL,
	[timeStamp]    TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	[url]          VARCHAR (2048) NULL,
    [phoneNum]     VARCHAR (15)   NULL,
    [SMSMessage]   VARCHAR (160)  NULL,
    [emailAddress] VARCHAR (320)  NULL,
    [emailSubject] VARCHAR (255)  NULL,
    [emailBody]    VARCHAR (5000) NULL,
    [firstName]    VARCHAR (50)   NULL,
    [lastName]     VARCHAR (50)   NULL,
    [workNum]      VARCHAR (15)   NULL,
    [organization] VARCHAR (100)  NULL,
    [houseNumber]  VARCHAR (10)   NULL,
    [street]       VARCHAR (50)   NULL,
    [city]         VARCHAR (50)   NULL,
    [state]        VARCHAR (50)   NULL,
    [zipCode]      VARCHAR (10)   NULL,
    [note]         VARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


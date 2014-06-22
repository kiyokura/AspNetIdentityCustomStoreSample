CREATE TABLE [dbo].[UserInfo] (
    [UserID]    UNIQUEIDENTIFIER NOT NULL,
    [LoginCode] NVARCHAR (50)    NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [EMail]     NVARCHAR (MAX)   NOT NULL,
    [Password]  NVARCHAR (MAX)   NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserInfo_U1]
    ON [dbo].[UserInfo]([LoginCode] ASC);


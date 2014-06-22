INSERT INTO [dbo].[UserInfo]
	 (
	 [UserID], 
	 [LoginCode], 
	 [FirstName], 
	 [LastName], 
	 [EMail], 
	 [Password]
	 )
VALUES
	 (
	 NEWID(), 
	 N'narami', 
	 N'‚È‚ç‚Ý', 
	 N'‚«‚æ‚­‚ç', 
	 N'narami@example.com', 
	 dbo.HashPassword('hogepassword')
	 );

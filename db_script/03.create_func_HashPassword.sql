/*パスワードのハッシュ化アルゴリズムのサンプル*/
CREATE FUNCTION [dbo].[HashPassword]
(
	@raw_password NVARCHAR
)
RETURNS NVARCHAR(128)
AS
BEGIN
	/* SQL Serverの標準HASH関数を利用する例。あくまで『独自の方式のパスワードハッシュ化ロジック』のサンプルです。 */
	RETURN(SUBSTRING(dbo.BinToHexString(HASHBYTES('SHA2_512', @raw_password)),3,128))
END

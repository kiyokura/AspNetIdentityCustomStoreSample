/* バイナリを16進の文字列に変換 */
CREATE FUNCTION [dbo].[BinToHexString]
(
     @binvalue VARBINARY(255)
)
RETURNS VARCHAR(255)
AS
BEGIN
   DECLARE @charvalue VARCHAR(255)
   DECLARE @i INT
   DECLARE @length INT
   DECLARE @hexstring CHAR(16)

   SET @charvalue = '0x'
   SET @i = 1
   SET @length = datalength(@binvalue)
   SET @hexstring = '0123456789abcdef'

   WHILE (@i <= @length)
   BEGIN

     DECLARE @tempint INT
     DECLARE @firstint INT
     DECLARE @secondint INT

     SET @tempint = convert(INT, substring(@binvalue,@i,1))
     SET @firstint = floor(@tempint/16)
     SET @secondint = @tempint - (@firstint*16)

     SET @charvalue = @charvalue +
       substring(@hexstring, @firstint+1, 1) +
       substring(@hexstring, @secondint+1, 1)
     SET @i = @i + 1
   END

   RETURN(@charvalue)
END

USE [AspNetCoreHero.Boilerplate]
GO

/****** Object: Table [dbo].[Brand] Script Date: 25.05.2021 9:09:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
Declare @UserId nvarchar(max)
Set @UserId=(select Id from dbo.Users where Email='basicuser@gmail.com')
Print @UserId

declare @id int
set @id=1

While @id<1000
Begin
	Insert Into dbo.Brand ("Name","Description","Tax","CreatedBy","CreatedOn")
	values ('Brand-'+CAST(@id as nvarchar),
								   'Auto fill data: Brand-'+CAST(@id as nvarchar),
								   1,
								   @UserId,
								   GETDATE())
   Print @id
   Set @id=@id+1
End
GO

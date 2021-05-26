USE [AspNetCoreHero.Boilerplate]
GO

/****** Object: Table [dbo].[Products] Script Date: 25.05.2021 9:16:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Declare @UserId nvarchar(max)
Set @UserId=(select Id from dbo.Users where Email='basicuser@gmail.com')
Print @UserId

Declare @id int
Set @id=1
Declare @brandId int 
Declare @BrandName nvarchar(200)
Set @brandId=1

While @brandId<1000
Begin
Set @id=1
Set @BrandName=(Select Name from dbo.Brand  where Id=@brandId)
Print @BrandName
While @id<500
Begin
    
	Insert Into dbo.Products ("Name","Barcode","Description","Rate","BrandId","CreatedBy","CreatedOn")
	values ('Product-('+@BrandName +') ' + CAST(@id as nvarchar),
			'Barcode'+ CAST(@id as nvarchar),
			'This auto fill record: '+'Product-'+@BrandName + CAST(@id as nvarchar),5,
			@brandId,
			@UserId,
			GETDATE())
	Print @id
	Set @id=@id+1
End --Product
Print @brandId
Set @brandId=@brandId+1
End --brand





--use master
--go

--create database [CocCanDB]
--go

use [CocCanDB]
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

drop table [MenuDetail]
drop table [OrderDetail]
drop table [Order]
drop table [Batch]
drop table [Session]
drop table [Menu]
drop table [Product]
drop table [TimeSlot]
drop table [Category]
drop table [PickUpSpot]
drop table [LocationStore]
drop table [Store]
drop table [Location]
drop table [Customer]
drop table [Staff]
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Staff](
	[Id] [uniqueidentifier] not null
		constraint [PK_Staff] primary key clustered ([Id])
		constraint [DF_Staff_ID] default(newid()),
	[Username] [nvarchar](100) not null unique,
	[Password] [nvarchar](100) not null,
	[Fullname] [nvarchar](100) not null,
	[Email] [nvarchar](100) not null unique
		constraint [CK_STAFF_EMAIL] check ([Email] like '_%@_%._%'),
	[Image] [nvarchar](200) not null,
	[Phone] [nchar](10) not null
		constraint [CK_STAFF_PHONE] check ([Phone] not like '%[^0-9]%'),
	[Role] [int] not null
		constraint [CK_STAFF_ROLE] check ([Role] like '[01]'),
	[Status] [int] not null
		constraint [CK_STAFF_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Customer](
	[Id] [uniqueidentifier] not null
		constraint [PK_CUSTOMER] primary key clustered ([Id])
		constraint [DF_CUSTOMER_ID] default(newid()),
	[Fullname] [nvarchar](100) not null,
	[Image] [nvarchar](100) not null,
	[Email] [nvarchar](100) not null unique
		constraint [CK_CUSTOMER_EMAIL] check ([Email] like '_%@fpt.edu.vn'),
	[Phone] [nchar](10) not null
		constraint [CK_CUSTOMER_PHONE] check ([Phone] not like '%[^0-9]%'),
	[Status] [int] not null
		constraint [CK_CUSTOMER_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Location](
	[Id] [uniqueidentifier] not null
		constraint [PK_LOCATION] primary key clustered ([Id])
		constraint [DF_LOCATION_ID] default(newid()),
	[Name] nvarchar(100),
	[Address] nvarchar(100),
	[Status] [int] not null
		constraint [CK_LOCATION_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Store](
	[Id] [uniqueidentifier] not null
		constraint [PK_STORE] primary key clustered ([Id])
		constraint [DF_STORE_ID] default(newid()),
	[Name] [nvarchar](100) not null,
	[Image] [nvarchar](200) not null,
	[Status] [int] not null
		constraint [CK_STORE_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [LocationStore](
	[LocationId] [uniqueidentifier] not null
		constraint [FK_LOCATIONSTORE_LOCATIONID] foreign key ([Locationid]) references [Location]([Id]),
	[StoreId] [uniqueidentifier] not null
		constraint [FK_LOCATIONSTORE_STOREID] foreign key ([Storeid]) references [Store]([Id])
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [PickUpSpot](
	[Id] [uniqueidentifier] not null
		constraint [PK_PICKUPSPOT] primary key clustered ([Id])
		constraint [DF_PICKUPSPOT_ID] default(newid()),
	[Fullname] [nvarchar](100) not null,
	[Address] [nvarchar](100) not null,
	[LocationId] [uniqueidentifier] not null
		constraint [FK_PICKUPSPOT_LOCATIONID] foreign key ([LocationId]) references [Location]([Id]),
	[Status] [int] not null
		constraint [CK_PICKUPSPOT_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Category](
	[Id] [uniqueidentifier] not null
		constraint [PK_CATEGORY] primary key clustered ([Id])
		constraint [DF_CATEGORY_ID] default(newid()),
	[Name] [nvarchar](100) not null,
	[Image] [nvarchar](200) not null,
	[Status] [int] not null
		constraint [CK_CATEGORY_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Product](
	[Id] [uniqueidentifier] not null
		constraint [PK_PRODUCT] primary key clustered ([Id])
		constraint [DF_PRODUCT_ID] default(newid()),
	[Name] [nvarchar](100) not null,
	[Image] [nvarchar](200) not null,
	[CategoryId] [uniqueidentifier]
		constraint [FK_PRODUCT_CATEGORYID] foreign key ([CategoryId]) references [Category]([Id]),
	[StoreId] [uniqueidentifier]
		constraint [FK_PRODUCT_STOREID] foreign key ([StoreId]) references [Store]([Id]),
	[Status] [int] not null
		constraint [CK_PRODUCT_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [TimeSlot](
	[Id] [uniqueidentifier] not null
		constraint [PK_TIMESLOT] primary key clustered ([Id])
		constraint [DF_TIMESLOT_ID] default(newid()),
	[StarTtime] time,
	[EndTime] time,
	[Status] [int] not null
		constraint [CK_TIMESLOT_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Menu](
	[Id] [uniqueidentifier] not null
		constraint [PK_MENU] primary key clustered ([Id])
		constraint [DF_MENU_ID] default(newid()),
	[Status] [int] not null
		constraint [CK_MENU_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [MenuDetail](
	[Id] [uniqueidentifier] not null
		constraint [PK_MENUDETAIL] primary key clustered ([id])
		constraint [DF_MENUDETAIL_ID] default(newid()),
	[Price] [money] not null,
	[MenuId] [uniqueidentifier] not null
		constraint [FK_MENUDETAIL_MENUID] foreign key ([MenuId]) references [Menu]([Id]),
	[ProductId] [uniqueidentifier] not null
		constraint [FK_MENUDETAIL_PRODUCTID] foreign key ([ProductId]) references [Product]([Id])
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Session](
	[Id] [uniqueidentifier] not null
		constraint [PK_SESSION] primary key clustered ([Id])
		constraint [DF_SESSION_ID] default(newid()),
	[Date] date,
	[TimeSlotId] [uniqueidentifier] not null
		constraint [FK_SESSION_TIMESLOTID] foreign key ([TimeSlotId]) references [TimeSlot]([Id]),
	[LocationId] [uniqueidentifier] not null
		constraint [FK_SESSION_LOCATIONID] foreign key ([LocationId]) references [Location]([Id]),
	[MenuId] [uniqueidentifier] not null
		constraint [FK_SESSION_MENUID] foreign key ([MenuId]) references [Menu]([Id]),
	[Status] [int] not null
		constraint [CK_SESSION_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Batch](
	[Id] [uniqueidentifier] not null
		constraint [PK_BATCH] primary key clustered ([Id])
		constraint [DF_BATCH_ID] default(newid()),
	[StaffId] [uniqueidentifier] not null
		constraint [FK_BATCH_STAFFID] foreign key ([StaffId]) references [Staff]([Id]),
	[SessionId] [uniqueidentifier] not null
		constraint [FK_BATCH_SESSIONID] foreign key ([SessionId]) references [Session]([Id]),
	[StoreId] [uniqueidentifier] not null
		constraint [FK_BATCH_STOREID] foreign key ([StoreId]) references [Store]([Id]),
	[Status] [int] not null
		constraint [CK_BATCH_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [Order](
	[Id] [uniqueidentifier] not null
		constraint [PK_ORDER] primary key clustered ([Id])
		constraint [DF_ORDER_ID] default(newid()),
	[OrderTime] [datetime] not null,
	[ServiceFee] [money] not null,
	[TotalPrice] [money] not null,
	[CustomerId] [uniqueidentifier] not null
		constraint [FK_ORDER_CUSTOMERID] foreign key ([CustomerId]) references [Customer]([Id]),
	[SessionId] [uniqueidentifier] not null
		constraint [FK_ORDER_SESSIONID] foreign key ([SessionId]) references [Session]([Id]),
	[PickUpSpotId] [uniqueidentifier] not null
		constraint [FK_ORDER_PICKUPSPOTID] foreign key ([PickUpSpotId]) references [PickUpSpot]([Id]),
	[Status] [int] not null
		constraint [CK_ORDER_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/

create table [OrderDetail](
	[Id] [uniqueidentifier] not null
		constraint [PK_ORDERDETAIL] primary key clustered ([Id])
		constraint [DF_ORDERDETAIL_ID] default(newid()),
	[Quantity] [int] not null,
	[ProductId] [uniqueidentifier] not null
		constraint [FK_ORDERDETAIL_PRODUCTID] foreign key ([ProductId]) references [Product]([Id]),
	[OrderId] [uniqueidentifier] not null
		constraint [FK_ORDERDETAIL_ORDERID] foreign key ([OrderId]) references [Order]([Id]),
	[Status] [int] not null
		constraint [CK_ORDERDETAIL_STATUS] check ([Status] like '[01]')
)
go

/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*/





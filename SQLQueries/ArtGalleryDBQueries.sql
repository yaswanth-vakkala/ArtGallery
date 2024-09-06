create database ArtGalleryDb;

use ArtGalleryDb;

create table Address(AddressId UNIQUEIDENTIFIER primary key default newid(), AppUserId NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(Id), 
AddressLine nvarchar(500) not null, PinCode nvarchar(12) not null, City nvarchar(100) not null, Landmark nvarchar(100), Country nvarchar(100) not null,
CountryCode nvarchar(6) not null, PhoneNumber nvarchar(15) not null);

create table Category(CategoryId UNIQUEIDENTIFIER primary key default newsequentialid(), Name NVARCHAR(50) not null UNIQUE, Description NVARCHAR(500),
CreatedAt datetime2 not null, ModifiedAt datetime2, ModifiedBy nvarchar(100));

create table Product(ProductId UNIQUEIDENTIFIER primary key DEFAULT newsequentialid(), Name nvarchar(100) not null, Description nvarchar(max) not null,
ImageUrl nvarchar(max), Price decimal(13, 3) not null, Status nvarchar(30) not null,CategoryId UNIQUEIDENTIFIER foreign key references Category(CategoryId), 
CreatedAt datetime2 not null, ModifiedAt datetime2, ModifiedBy nvarchar(100));

create table Cart(CartId uniqueidentifier primary key default newid(), AppUserId NVARCHAR(450) FOREIGN key REFERENCES AspNetUsers(Id),
ProductId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Product(ProductId), CreatedAt datetime2 not null);

create table WishList(WishListId uniqueidentifier PRIMARY KEY default newid(), AppUserId NVARCHAR(450) FOREIGN key REFERENCES AspNetUsers(Id),
ProductId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Product(ProductId), CreatedAt datetime2 not null);

create table Payment(PaymentId uniqueidentifier primary key default newid(), Amount decimal(13,3) not null, PaymentDate datetime2 not null, 
Status nvarchar(30) not null, CardNumber nvarchar(100) not null, CardHolderName nvarchar(100) not null, ExpiryDate datetime2 not null);

create table AppOrder(OrderId uniqueidentifier primary key default newid(), AppUserId NVARCHAR(450) foreign key references AspNetUsers(Id), 
PaymentId uniqueidentifier foreign key references Payment(PaymentId), AddressId uniqueidentifier foreign key references Address(AddressId), 
CreatedAt datetime2 not null, ModifiedAt datetime2, ModifiedBy nvarchar(100));

create table OrderItem(OrderItemId uniqueidentifier primary key default newid(),OrderId uniqueidentifier foreign key references AppOrder(OrderId), 
ProductId uniqueidentifier foreign key references Product(ProductId), Status nvarchar(30) not null,
ProductCost decimal(13,3) not null, TaxCost decimal(13,3) not null, ShippingCost decimal(13,3) not null);


/*create table AppUser(AppUserId UNIQUEIDENTIFIER primary key DEFAULT newid(), FirstName nvarchar(50) not null, LastName nvarchar(50) not null,
Email nvarchar(100) not null unique, Role nvarchar(30) not null, Status nvarchar(30) not null, CountryCode nvarchar(6), PhoneNumber nvarchar(15), CreatedAt datetime2 not null,
ModifiedAt datetime2, ModifiedBy nvarchar(100));*/

/*create table Inventory(InventoryId uniqueidentifier primary key default newid(), Quantity int not null, CreatedAt datetime2 not null, ModifiedAt datetime2, 
ModifiedBy nvarchar(100));*/
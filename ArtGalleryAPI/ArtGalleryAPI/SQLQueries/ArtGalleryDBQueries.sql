create database ArtGallery;

use ArtGallery;

create table AppUser(AppUserId UNIQUEIDENTIFIER primary key DEFAULT newid(), FirstName nvarchar(30) not null check(len(FirstName) >= 2), LastName nvarchar(30) not null,
Password nvarchar(100) not null, Email nvarchar(100) not null unique, Role nvarchar(30) not null, AppUserPhoneCode nvarchar(6), AppUserPhone nvarchar(15), CreatedAt datetime2 not null,
ModifiedAt datetime2, ModifiedBy nvarchar(100) , AccountStatus nvarchar(30) not null);

create table UserAddress(UserAddressId UNIQUEIDENTIFIER primary key default newid(), AppUserId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES AppUser(AppUserId), 
AddressLine nvarchar(300) not null, PinCode nvarchar(12) not null, City nvarchar(100) not null, Landmark nvarchar(100), Country nvarchar(100) not null,
AddressPhoneCode nvarchar(6) not null, AddressPhone nvarchar(15) not null);

create table Product(ProductId UNIQUEIDENTIFIER primary key DEFAULT newsequentialid(), ProductName nvarchar(100) not null, Description nvarchar(300) not null,
Price money not null, CreatedAt datetime2 not null, ModifiedAt datetime2, ModifiedBy nvarchar(100), ProductStatus nvarchar(30) not null,
CategoryId UNIQUEIDENTIFIER foreign key references Category(CategoryId), InventoryId uniqueidentifier foreign key references Inventory(InventoryId));

create table Category(CategoryId UNIQUEIDENTIFIER primary key default newsequentialid(), CategoryName NVARCHAR(30) not null UNIQUE, Description NVARCHAR(300),
CreatedAt datetime2 not null, ModifiedAt datetime2, ModifiedBy nvarchar(100));

create table Inventory(InventoryId uniqueidentifier primary key default newid(), Quantity int not null, CreatedAt datetime2 not null, ModifiedAt datetime2, 
ModifiedBy nvarchar(100));

create table AppOrder(OrderId uniqueidentifier primary key default newid(), AppUserId uniqueidentifier foreign key references AppUser(AppUserId), 
PaymentId uniqueidentifier foreign key references Payment(PaymentId), CreatedAt datetime2 not null,
UserAddressId uniqueidentifier foreign key references UserAddress(UserAddressId), ModifiedAt datetime2, ModifiedBy nvarchar(100));

create table OrderItem(OrderItemId uniqueidentifier primary key default newid(),OrderId uniqueidentifier foreign key references AppOrder(OrderId), 
ProductId uniqueidentifier foreign key references Product(ProductId),Quantity int not null, OrderItemStatus nvarchar(30) not null,
 ProductCost money not null, TaxCost money not null, ShippingCost money not null);

create table Cart(CartId uniqueidentifier primary key default newid(), AppUserId UNIQUEIDENTIFIER FOREIGN key REFERENCES AppUser(AppUserId),
ProductId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Product(ProductId), Quantity int not null);

create table WishList(WishListId uniqueidentifier primary key default newid(), AppUserId UNIQUEIDENTIFIER FOREIGN key REFERENCES AppUser(AppUserId),
ProductId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Product(ProductId));

create table Payment(PaymentId uniqueidentifier primary key default newid(), PaymentAmount money not null, PaymentDate datetime2 not null,
PaymentMethod nvarchar(100) not null, );

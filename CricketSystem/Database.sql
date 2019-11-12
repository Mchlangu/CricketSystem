CREATE TABLE user_table
(
	User_id int primary key IDENTITY(1,1) NOT NULL,
	User_type varchar(20) NOT NULL,
	Firstname varchar(30) NOT NULL,
	Lastname varchar(30) NOT NULL,
	IDNumber varchar(13) NOT NULL,
	Gender varchar(10) NOT NULL,
	Cellno varchar(15) NOT NULL,
	Email varchar(50) NOT NULL,
	Username varchar(20) NOT NULL,
	Password varchar(20) NOT NULL,
	Date_created datetime NOT NULL,
	Status varchar(15) NOT NULL
)

INSERT INTO user_table( User_type, Firstname, Lastname,IDNumber, Gender, Cellno,Email,Username, Password,Date_created,Status)
VALUES ('Customer', 'Customer', 'Customer','9999999999999','Male','0835586866','Customer@gmail.com', 'Customer', '123456','2019/04/15','Active'),
	   ('Admin', 'Admin', 'Admin','9999999999999','Male','0835586866','Admin@gmail.com', 'Admin', '123456','2019/04/15','Active'),
	   ('Staff', 'Staff', 'Store','9999999999999','Male','0835586866','Staff@gmail.com', 'Staff', '123456','2019/04/15','Active'),
	   ('Supplier', 'Supplier', 'Supplier','9999999999999','Male','0835586866','Supplier@gmail.com', 'Supplier', '123456','2019/04/15','Active')

CREATE TABLE login_history_table
(
	id int primary key IDENTITY(1,1) NOT NULL,
	User_id int NOT NULL,
	Username varchar(30) NOT NULL,
	DateAndTime varchar(100) NOT NULL,
)


CREATE TABLE product_table
(
	Product_id int primary key IDENTITY(1,1) NOT NULL,
	User_id int NOT NULL,
	ProductNo int NOT NULL,
	product_purpose varchar(20) NOT NULL,
	Product_type varchar(20) NOT NULL,
	Name varchar(30) NOT NULL,
	Price varchar(30) NOT NULL,
	Image_url text NOT  NULL,
	Username varchar(30) NOT NULL,
	UserType varchar(30) NOT NULL
)

CREATE TABLE supplier_product_table
(
	Product_id int primary key IDENTITY(1,1) NOT NULL,
	User_id int NOT NULL,
	ProductNo int NOT NULL,
	Product_type varchar(20) NOT NULL,
	Name varchar(30) NOT NULL,
	Price varchar(30) NOT NULL,
	Image_url text NOT  NULL,
	Username varchar(30) NOT NULL
)



CREATE TABLE order_table
(
	Order_id int primary key IDENTITY(1,1) NOT NULL,
    Order_no int NOT NULL,
	Product_id int NOT NULL,
	User_id int NOT NULL,
	Username varchar(30) NOT NULL,
	Name varchar(30) NOT NULL,
	Price decimal(30) NOT NULL,
	Quantity int NOT NULL,
	Date  varchar(30) NOT NULL,
	Time  varchar(30) NOT NULL,
	OrderBy varchar(30) NOT NULL,
	Status varchar(30) NOT NULL
)
CREATE TABLE hired_table
(
	Hire_id int primary key IDENTITY(1,1) NOT NULL,
    Hire_No int NOT NULL,
	User_id int NOT NULL,
	Username varchar(30) NOT NULL,
	Name varchar(30) NOT NULL,
	Price decimal(30) NOT NULL,
	Quantity int NOT NULL,
	NoDays int NOT NULL,
	Date  varchar(30) NOT NULL,
	Time  varchar(30) NOT NULL,
	Status varchar(30) NOT NULL,
	ReturnDate varchar(30) NOT NULL
)


CREATE TABLE pawn_table
(
	Pawn_id int primary key IDENTITY(1,1) NOT NULL,
	Pawn_Product_id int NOT NULL,
	User_id int NOT NULL,
	Quantity int NOT NULL,
	Condition varchar(30) NOT NULL,
	LoanAmount decimal(7,2) NOT NULL,
	AmountDue decimal(7,2) NOT NULL,
	Date varchar(30) NOT NULL,
	Time varchar(30) NOT NULL,
	collectionDate varchar(30) NOT NULL,
	Status varchar(30) NOT NULL
)

CREATE TABLE pawn_product_table
(
	Pawn_Product_id int primary key IDENTITY(1,1) NOT NULL,	
	Product_type varchar(20) NOT NULL,
	Name varchar(30) NOT NULL,
	Amount decimal(7,2) NOT NULL,
	Image_url text NOT  NULL,
)

CREATE TABLE payment_table
(
	Payment_id int primary key IDENTITY(1,1) NOT NULL,
	Purchase_id int NOT NULL,
	Purchase_Type varchar(30) NOT NULL,
	User_id int NOT NULL,
	Bank varchar(30) NOT NULL,
	Card_holder varchar(50) NOT NULL,
	Card_number varchar(50) NOT NULL,
	Cvv varchar(4) NOT NULL,
	ExpMM int NOT NULL,
	ExpYY int NOT NULL,
	totatAmount decimal(7,2) NOT NULL,
	Date  varchar(30) NOT NULL,
	Time  varchar(30) NOT NULL,
	Status varchar(30) NOT NULL
)


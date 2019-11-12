CREATE TABLE Customer_table
(
	Customer_id int primary key IDENTITY(1,1) NOT NULL,
	Customer_type varchar(20) NOT NULL,
	Staff varchar(30)  NULL,
	Firstname varchar(30) NOT NULL,
	Lastname varchar(30) NOT NULL,
	IDNumber varchar(15) NOT NULL,
	Gender varchar(10) NOT NULL,
	Age int NOT NULL,
	Cellno varchar(15) NOT NULL,
	Email varchar(50) NOT NULL,
	Customername varchar(20) NOT NULL,
	Password varchar(20) NOT NULL,
	Date_created datetime NOT NULL,
	Status varchar(15) NOT NULL
)


INSERT INTO Customer_table( Customer_type,Staff, Firstname, Lastname,IDNumber, Gender,Age, Cellno,Email,Customername, Password,Date_created,Status)
VALUES ('Customer','', 'Customer', 'Customer','9999999999999','Male',35,'0835586866','Customer@gmail.com', 'Customer', '123456','2019/04/15','Active'),
	   ('Admin','', 'Admin', 'Admin','9999999999999','Male',35,'0835586866','Admin@gmail.com', 'Admin', '123456','2019/04/15','Active'),
	   ('Staff','KFC', 'Staff Manager', 'Staff','9999999999999','Male',35,'0835586866','Staff@gmail.com', 'Staff', '123456','2019/04/15','Active'),
	   ('Supplier','', 'Supplier', 'Supplier','9999999999999','Male',35,'0835586866','Supplier@gmail.com', 'Supplier', '123456','2019/04/15','Active')
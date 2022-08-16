CREATE TABLE logintable(
id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
username varchar(150) NOT NULL,
pass varchar(150) NOT NULL,
)

INSERT INTO logintable (username, pass) VALUES ('admin', 'password');

SELECT * FROM Newbook

create table Newbook(
bid int NOT NULL IDENTITY(1,1) primary key,
bName varchar(250) not null,
bAuthor varchar(250) not null,
bPubl varchar(250) not null,
bDate varchar(250) not null,
bPrice bigint not null,
bQuan bigint not null,
)


create table NewStudent(
stuid int NOT NULL IDENTITY(1,1) primary key,
sname varchar(250) not null,
enroll varchar(250) not null,
dep varchar(250) not null,
sem varchar(250) not null,
contact bigint not null,
email varchar(250) not null,
)

SELECT * FROM NewStudent

create table ISBook(
id int NOT NULL IDENTITY(1,1) primary key,
std_enroll varchar(250) not null,
std_name varchar(250) not null,
std_dep varchar(250) not null,
std_sem varchar(250) not null,
std_contact bigint not null,
std_email varchar(250) not null,
book_name  varchar(1250) not null, 
book_issue_date  varchar(250) not null,
book_return_date  varchar(250) ,
);
select * from ISBook



SELECT COUNT(std_name) FROM ISBook WHERE book_return_date IS NULL

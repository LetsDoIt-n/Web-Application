
create database expense_record_db;

use expense_record_db;

create table expenseCategory(
category_id int primary key auto_increment,
category_name varchar(100) not null
)Engine=InnoDB;

create table expense(
  expense_id int primary key auto_increment,
  expense_date datetime not null,
  expense_category_id int,
  expense_description text,
  expense_amount decimal(15, 2) not null,
  expense_paidby varchar(100),
  expense_attachement text,
  foreign key (expense_category_id) references expenseCategory(category_id)
) Engine = InnoDB;






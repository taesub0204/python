create database if not exists sample; -- 만약 이 DB가 있다면 만들지 마 

use sample;

create table doit_increment(
	col_1 int auto_increment primary key,
    col_2 varchar (50),
    col_3 int


); 

show tables;

insert into doit_increment(col_2,col_3) 
values('1 자동입력', 1),('2 자동입력', 2),('3 자동입력', 3),('4 자동입력', 4),('5 자동입력', 5);

insert into doit_increment(col_2,col_3) 
values('11 자동입력', 11),('12 자동입력', 12),('13 자동입력', 13),('4 자동입력', 14),('5 자동입력', 15);


select * from doit_increment;

insert into doit_increment values(10, '강제로 입력', 10);
insert into doit_increment values(6, '강제로 입력', 6);


select last_insert_id();

alter table doit_increment auto_increment = 100;


set @@auto_increment_increment = 5;



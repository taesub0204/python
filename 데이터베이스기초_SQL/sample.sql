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




create table doit(
	col_1 int auto_increment primary key,
    col_2 varchar (50),
    col_3 int

); 

-- 테이블 복제 

insert into doit select * from doit_increment; -- 기존 테이블 넣는 거 
select * from doit;



create table doit_new as (select * from doit_increment); -- 새로 만들어서 넣는 거 
select * from doit_new; 


create table parent
(col_1 int primary key,
 col_2 varchar(20),
 col_3 varchar(20)
);

desc parent;

create table child
(
	item_1 int primary key,
    item_2 int not null,
    item_3 varchar(20) unique,
    item_4 int 
);
alter table child add foreign key (item_2) references parent(col_1);
alter table child add foreign key (item_2) references parent(col_1) on delete cascade; -- on cascade 옵션 사용해주면 연결되어 있는 곳 같이 삭제됨
alter table child add foreign key (item_2) references parent(col_1) on delete set null;  -- 자식 테이블 fk를 null처리 
desc child;


insert into parent values(19900204,'홍길동','kim1@naver.com'),(19900205,'김강우','kim2@naver.com'),(19900206,'김강동','kim3@naver.com'),(19910207,'김수진','kim4@naver.com'),(19910208,'원빈','kim5@naver.com');
select * from parent;
delete from parent;

insert into child values(10,11111111,'한국폴리텍',2);   --  col_1 부모 테이블 참조 기본키 확인 해야됨  
insert into child values(10,19900204,'한국폴리텍',2); 
insert into child values(20,19900205,'한경대학교',2); 
insert into child values(21,1990207,'한경대학교',2);
select * from child;

delete from parent where col_1 = 19900204;
delete from parent where col_1 = 19910207;
delete from parent where col_1 = 19900206; -- 참조 되지 않은 거 삭제 가능
delete from parent where col_1 = 19900205; -- 외래키 제약 조건 때문에 삭제 불가능 지우면 차일드가 고아 상태가 됨...... 외래키 제약 조건
delete from child;

drop table parent; -- 차일드가 고아가 됨 그래서 자식 테이블 drop 되어 야 부모테이블도 지울 수 있음 
select constraint_name from child;
show create table child;

alter table child drop constraint child_ibfk_1; -- 제약조건 삭제 이후 부터 삭제자유롭게 가능해짐

alter table child drop constraint child_ibfk_2;


create table my1(col_1 decimal(100,2)); -- 정수 10자리 쓰고 소수점 2자리 써라

insert into my1 values(0.7);
select * from my1;
select * from my1 where col_1 = 0.7; -- float는 부동소수로 사용해야 할 듯 일치해야 하는 값을 찾을 수 없음
drop table my1;

















CREATE DATABASE library;


create table books(
    id int auto_increment  primary key,
    title varchar(30) not null,
    author varchar(20) not null,
    published_year int,
    available boolean default true
    );

show tables;
desc books;

insert into books(title, author, published_year, available) values('데미안', '헤르만헤세', 1919, true);
insert into books(title, author, published_year, available) values('어린왕자', '생택쥐베리', 1943, true);
insert into books(title, author, published_year, available) values('데이터베이스입문', '홍길동', 2026, true);
insert into books(title, author, published_year, available) values('백설공주', '이솝', 2020, true);
insert into books(title, author, published_year, available) values('빅데이터처리', '김길동', 2026, true);


select count(*) as total_books, sum(available) as 대출가능권수, avg(published_year) as 최근책 from books ;
select * from books;
select available, count(*) as 책수 from books group by available; 
select * from books order by published_year desc; -- 정렬 order by, ASC(오름차순, 디폴트값), DESC(내림차순)
select * from books order by available desc; 
select title, author from books; 
select * from books where published_year > 2000;
select * from books where  title like '데%';
delete from books where id=9;
delete from books where  author = '홍길동';
update books set available=false where author ='헤르만헤세';
update books set title='빅데이터활용' where id = 6;
update books set title='데미안1' where id =1;
update books set title='데미안2' where id =2;



create table students(
    id int auto_increment primary key, 
    name varchar(20) not null, 
    age int,
    major varchar(20)
);

create table borrow(
    id int auto_increment, 
   student_id int not null,
    book_id int not null,
    borrow_date Date not null default (CURRENT_DATE),
    return_date Date, 
    primary key(id),
    foreign key(student_id) references students(id),
   foreign key(book_id) references books(id)
);

select * from students;
select * from books;
desc borrow;

update students set major = '컴퓨터공학' where id = 1;
update students set major = '반도체공학' where id = 2;
update books set available = true;


select * from students;
select * from students order by age asc limit 2 offset 5;
select * from students where major is not null;
select * from students where name like '%이%';
select * from students where age between 20 and 25;
select * from students where major in ('컴퓨터공학', '전자공학', '전기공학');
insert into students(name, age, major) values('김철수', 20, '컴퓨터공학');
insert into students(name, age, major) values('이영희', 21, '반도체공학');
insert into students(name, age, major) values('박정훈', 22, '전자공학');
insert into students(name, age, major) values('아이유', 25, '대중음악'); 
insert into students(name, age, major) values('성시경', 30, '수학');
insert into students(name, age, major) values('박신혜', 35, '연극영화');
update students set major='컴퓨터공학' where id = 9;

select major, count(*) as  학생수, avg(age) as 평균나이, max(age) as old, min(age) as young  from students group by major ;   
select major, count(*) as  학생수, avg(age) as 평균나이 from students group by major having count(*) >= 2;
select major, count(*) as  학생수, avg(age) as 평균나이 from students group by major order by 학생수 desc ;
select major, count(*) as  학생수, avg(age) as 평균나이 from students group by major order by 평균나이 desc ;

select * from students;
select * from borrow;
select * from books;
select * from students where major like '%!%%' escape '!' ; -- escape 문자 설정 !%, #% ==> % 원래의 기능을 제거
 
update students set major = '컴퓨&공학' where id =1;
update students set major = '반도&공학' where id =2;

use library;
desc books;
desc students;



SELECT s.name, b.title 
FROM students as s
JOIN borrow AS br ON s.id = br.student_id
JOIN books AS b ON br.book_id = b.id;

select s.name, b.title 
from students as s
cross join books as b;

SELECT s.name, b.title
FROM students AS s
LEFT JOIN borrow AS br ON s.id = br.student_id
LEFT JOIN books AS b ON br.book_id = b.id;

SELECT s.name, b.title
FROM students AS s
right JOIN borrow AS br ON s.id = br.student_id
right JOIN books AS b ON br.book_id = b.id;


SELECT s.name, b.title
FROM students AS s
INNER JOIN borrow AS br ON s.id = br.student_id
INNER JOIN books AS b ON br.book_id = b.id;

-- INNER JOIN: 교집합 (양쪽에 모두 있는 데이터)
-- LEFT JOIN: 왼쪽 기준 전체 + 오른쪽 매칭
-- RIGHT JOIN: 오른쪽 기준 전체 + 왼쪽 매칭
-- FULL OUTER JOIN: 합집합 (MySQL에서는 직접 구현)
-- CROSS JOIN: 모든 조합

SELECT s.name, b.title, b.published_year, br.borrow_date
FROM students AS s
INNER JOIN borrow AS br ON s.id = br.student_id -- 두테이블 조인 할 떄 
INNER JOIN books AS b ON br.book_id = b.id
where s.name like '김%';


SELECT *
FROM students
WHERE age = (SELECT MAX(age) FROM students); -- 서브쿼리 예제

SELECT MAX(age) FROM students;



SELECT s.name, b.title, br.borrow_date
FROM students AS s
LEFT JOIN borrow AS br ON s.id = br.student_id
LEFT JOIN books AS b ON br.book_id = b.id
where b.published_year > 2020;

SELECT name
FROM students
WHERE id IN (
    SELECT student_id
    FROM borrow
    WHERE book_id IN (
        SELECT id
        FROM books
        WHERE published_year >= 2020
    )
);

 SELECT id
        FROM books
        WHERE published_year >= 2020;

select major, avg(age)
from students 
group by major
having avg(age) >= 22; 


SELECT sub.major, sub.avg_age
FROM (
    SELECT major, AVG(age) AS avg_age
    FROM students
    GROUP BY major
) AS sub
WHERE sub.avg_age >= 22;

SELECT major, AVG(age) AS avg_age
    FROM students
    GROUP BY major;

create view avgage as
SELECT major, AVG(age) AS avg_age
    FROM students
    GROUP BY major;
    
select * from avgage;

select major, avgage
from  avgage
where avg_age >= 22;

show tables;

show full tables where table_type = 'VIEW';
show full tables;
drop view avgager;




create	view borrowed_books as
select s.name, b.title, br.borrow_date
from students as s
inner join borrow as br on s.id = br.student_id
inner join books as b on br.book_id = b.id;



CREATE INDEX idx_title -- 인덱스 생성
ON books(title);

show index from books; -- 북테이블 인덱스 보기 

explain select * from books where title = '데미안';
explain select * from books where author = '헤르만헤세';

-- WHERE 조건으로 자주 검색하는 컬럼
-- JOIN 조건으로 자주 쓰이는 컬럼
-- ORDER BY, GROUP BY에 자주 쓰이는 컬럼
    
--     CREATE TABLE students (
--     id INT AUTO_INCREMENT PRIMARY KEY,
--     name VARCHAR(50),
--     email VARCHAR(100) UNIQUE
-- );
-- 👉 email은 중복 저장 불가 + 인덱스 자동 생성 성능최적화를 위해 알고 있어야 함 


    
    
    
    
    



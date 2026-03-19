
DROP DATABASE IF EXISTS library; -- 1. 기존에 잘못 만들었을지 모를 DB 삭제 (선택사항)
create database library;  -- library라는 데이터베이스 생성
show databases;

use library;


create table books(
id int auto_increment primary key, -- 고유번호
title varchar(100) not null, -- 책 제목
author varchar(50), -- 저자
published_year int, -- 출판년도
available boolean default true
);

show tables;

DESCRIBE books;

INSERT INTO books (title, author, published_year, available)
VALUES ('데미안', '헤르만 헤세', 1919, TRUE);

INSERT INTO books (title, author, published_year, available)
VALUES ('어린왕자', '생텍쥐페리', 1943, TRUE);

INSERT INTO books (title, author, published_year, available)
VALUES ('데이터베이스 입문', '홍길동', 2025, FALSE);

INSERT INTO books (title, author, published_year, available)
VALUES ('C언어', '장문수', 2000, FALSE);

INSERT INTO books (title, author, published_year, available)
VALUES ('개념원리', '조태섭', 1800, FALSE);

INSERT INTO books (title, author, published_year, available) 
VALUES ('미래의 반도체', '미상', NULL, TRUE);


select * from books;

-- CREATE DATABASE → 새로운 데이터베이스 만들기
-- CREATE TABLE → 데이터를 담을 표 구조 정의
-- INSERT INTO → 실제 데이터 저장
-- SELECT → 데이터 확인

SET SQL_SAFE_UPDATES = 0; -- 안전모드 해제 실수로 모든데이터 수정 삭제 해버리는 사고 방지 title은 기본키가 아님

update books
set published_year = 2024
where title = '데이터베이스 입문';

delete from books 
where title = '어린왕자'; -- 안지워짐 아래와 같이 id로 날려줌

-- select * from books;
-- DELETE FROM books WHERE id = 2;

select *  from books
where published_year >=2000;

select * from books
where author = '홍길동' AND available = FALSE;

select * from books
where author = '홍길동' or published_year < 1900;

select * from books
order by published_year asc; -- 정렬 하기 오름차순 1, 2, 3,...

select * from books
order by published_year desc; -- 내림차순 100 99 98

select * from books 
order by published_year desc LIMIT 2; -- 많은 데이터 중 일부 확인하기

select * from books;

select * from books
where author = '홍길동' AND published_year >= 2020; -- 저자가 홍길동이고 출판연도가 2020년 이후인 책

select * from books
where author = '홍길동' or published_year < 1950;

select * from books
where author in ('홍길동','호르만헤세',"조태섭");

select * from books
where title like '%데이터%';

select * from books
where title like '어린%';

select * from books
where title like '___';

select * from books   -- 저자가 입력되지 않은 책
where author is null;

select * from books   -- 저자가 입력되지 않은 책
where published_year is null;

select * from books   -- 저자가 입력된 책
where author is not null;

-- 집계 함수란 
-- 집계 함수 (Aggregate Function)은 여러 행의 값을 모아 하나의 결과로 보여주는 함수 입니다.
-- 자주 쓰이는 집계 함수는 다음과 같습니다.

-- books 테이블에 등록된 책의 수 
-- as 쓰면 별칭 붙여줌
select count(*) as total_books from books;


CREATE TABLE students (
    student_id INT PRIMARY KEY,       -- 학번 (중복 안됨, 고유 번호)
    name VARCHAR(50) NOT NULL,        -- 이름 (비어있으면 안됨)
    age INT,                          -- 나이 (NULL 허용 가능)
    major VARCHAR(100),               -- 학과 (반도체장비소프트웨어과 등)
    enrollment_date DATE              -- 입학일
);
INSERT INTO students (student_id, name, age, major, enrollment_date) VALUES 
(2026001, '김철수', 20, '반도체장비소프트웨어과', '2026-03-02'),
(2026002, '이영희', 21, '시각디자인과', '2025-03-02'),
(2026003, '박민준', 23, '컴퓨터공학과', '2023-03-02'),
(2026004, '최수지', NULL, '반도체장비소프트웨어과', '2026-03-02'), -- 나이 모름(NULL)
(2026005, '정태양', 22, NULL, '2024-03-02'); -- 학과 미정(NULL)

commit;

select sum(age) as total_age, 
		avg(age) as avg_age from students;
        
select min(age) as youngest, max(age) as oldman from students;

select major, count(*) as students_count from students group by major; -- 그룹별로 요약 해줌

select major, count(*) as student_count from students group by major having count(*) >= 3;
select major, count(*) as student_count from students group by major having count(*) >= 2; -- 그룹화된  통계치에 조건을 거는 거 having은 조건을 줄 수 있음

select major, count(*) as student_count from students group by major order by student_count desc;


select major, count(*) as studnet_count,
avg(age) as avg_age,
max(age) as oldest
from students
group by major having major = '반도체장비소프트웨어과';

/* 
SELECT 컬럼들
FROM 테이블1
JOIN 테이블2
  ON 테이블1.컬럼 = 테이블2.컬럼;
*/
use library;

DROP TABLE IF EXISTS borrow;
CREATE TABLE borrow (
    borrow_id INT PRIMARY KEY AUTO_INCREMENT, -- 대출 번호 (자동 증가)
    student_id INT,                           -- 빌린 학생 (students 테이블의 학번)
    book_id INT,                              -- 빌린 책 (books 테이블의 ID)
    borrow_date DATE,                         -- 빌린 날짜
    return_date DATE                          -- 반납 예정일 (혹은 반납일)
);

INSERT INTO borrow (student_id, book_id, borrow_date, return_date) VALUES 
(2026001, 1, '2026-03-10', '2026-03-24'), -- 김철수가 1번 책 대출
(2026001, 6, '2026-03-12', '2026-03-26'), -- 김철수가 2번 책 또 대출   2번 delete해서 날린거는 복구가 안됨 6으로 수정
(2026002, 3, '2026-03-15', '2026-03-29'), -- 이영희가 3번 책 대출
(2026003, 1, '2026-02-01', '2026-02-15'), -- 박민준이 예전에 1번 빌렸던 기록
(2026004, 5, '2026-03-18', '2026-04-01'); -- 최수지가 5번 책 대출

COMMIT; -- 번개 잊지 마세요!

-- 학생이 빌린 책 정보 확인하기
select title from books;

select s.name, b.title from students as s 
inner join borrow as br on s.student_id = br.student_id -- 같은 student_id가 있다면 합쳐
inner join books as b on br.book_id = b.id; -- book id가 있다면 합쳐 그리고 교집합을 조인해 


--  모든 학생과 그들이 빌린 책 보기
select s.name, b.title from students as s     -- 먼저 쓴놈이 대장 기준 그제 왼쪽이라네 studens로 이름 다 붙여 
left join borrow as br on s.student_id = br.student_id
left join books as b on br.book_id = b.id;

SELECT id, title FROM books;

select s.name, b.title from students as s     -- 먼저 쓴놈이 대장 기준 그제 왼쪽이라네 studens로 이름 다 붙여 
right join borrow as br on s.student_id = br.student_id
right join books as b on br.book_id = b.id; -- 좀 어렵네  책이 있는데 아무도 대출하지 않은 책 null로 노출됨
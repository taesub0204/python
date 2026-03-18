
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
VALUES ('어린 왕자', '생텍쥐페리', 1943, TRUE);

INSERT INTO books (title, author, published_year, available)
VALUES ('데이터베이스 입문', '홍길동', 2025, FALSE);


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

select * from books;
DELETE FROM books WHERE id = 2;



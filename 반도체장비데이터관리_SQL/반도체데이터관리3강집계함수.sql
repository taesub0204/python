-- 3강 집계 함수 
-- 연습 문제 마당 DB

select bookname, price as '도서가격' -- as 별칭 1번 문제
from book;

select name as '고객명', address as '거주지' -- 2번 문제
from customer;

select  orderid, saleprice as '판매금액' -- 3번 문제
from orders;

-- 집계함수:기본문제 

select count(*) as '도서의 총 권수' -- 1번 문제
from book;

select count(*) as '전체 주문 건수' -- 2번 문제
from orders;

select count(*) as '전체 고객의 수' -- 3번 문제
from customer;

select max(price) as '가장비싼 도서의 가격' -- 4번 문제
from book;

select min(price) as '가장저렴한 도서의 가격' -- 5번 문제
from book;

select avg(price) as '도서 가격 평균' -- 6번 문제
from book;

select sum(saleprice) as 'orders전체판매금액' -- 7번 문제
from orders;

select avg(saleprice) as 'orders 평균 판매가격' -- 8번 문제
from orders;

select min(orderdate) as '가장 빠른 주문일' -- 9번 문제
from orders;

select max(orderdate) as '가장 최근 주문일' -- 10번 문제
from orders;



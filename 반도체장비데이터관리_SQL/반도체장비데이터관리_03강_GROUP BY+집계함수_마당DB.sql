-- 반도체장비데이터관리 03강 
-- GROUP BY + 집계함수 연습문제


-- 기본 문제
-- 1. 고객별 주문 횟수를 구하시오.
select custid as 고객별, count(custid) as 주문횟수
from orders 
group by custid;

-- 2. 도서별 판매 횟수를 구하시오.
select bookid as 도서별, count(bookid) as 횟수
from orders
group by bookid;

-- 3. 고객별 총 구매 금액을 구하시오.
select custid as 고객별, sum(saleprice) as '총 구매 금액'
from orders
group by custid;

-- 4. 고객별 평균 구매 금액을 구하시오.
select custid as 고객별, avg(saleprice) as 평균금액
from orders
group by custid;

-- 5. 고객별 최고 구매 금액을 구하시오.
select custid, max(saleprice)
from orders 
group by custid;






-- 조건 포함 문제
-- 1. 판매가격이 10,000원 이상인 주문만 대상으로 고객별 주문 횟수를 구하시오.
select custid as 고객별, count(custid) as 주문수
from orders
where saleprice >= 10000
group by custid;


-- 2. 2024년 이후 주문만 대상으로 고객별 총 구매 금액을 구하시오.
select custid, sum(saleprice)
from orders
where orderdate >= '2024-01-01'
group by custid;

-- 3. 도서별 총 판매 금액을 구하시오.
select bookid as 도서별, sum(saleprice) as '총 판매 금액'
from orders
group by bookid;

-- 4. 도서별 평균 판매 가격을 구하시오.
select bookid as 도서별, avg(saleprice) as '평균가격'
from orders
group by bookid;

-- 5. 고객별 최소 구매 금액을 구하시오.
select custid as 고객별, min(saleprice) as 최소금액
from orders
group by custid;

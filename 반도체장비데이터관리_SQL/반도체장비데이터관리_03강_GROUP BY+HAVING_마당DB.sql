-- 반도체장비데이터관리 03강 
-- GROUP BY + HAVING 연습문제

-- 기본 문제
-- 1. 주문 횟수가 2회 이상인 고객을 조회하시오.
-- 주문/횟수/가 /2회 이상인 / 고객
select custid, count(*)
from orders
group by custid
having count(*) >= 2;

-- 2. /총 구매 금액/이 /30,000원 이상/인 고객/을 조회하시오.
select custid, sum(saleprice)
from orders
group by custid
having sum(saleprice) >= 30000;

-- 3. /평균/ 구매 금액이 /15,000원 이상/인 /고객/을 조회하시오.
select custid, avg(saleprice)
from orders
group by custid
having avg(saleprice) >= 15000;


-- 4. 판매/ 횟수가 /2회 이상/인 도서를 조회하시오.
select bookid, count(*)
from orders
group by bookid
having count(*) >= 2;

-- 5. 최소/ 구매 금액/이 10,000원 이상/인 고객/을 조회하시오.
select custid, min(saleprice)
from orders
group by custid
having min(saleprice) >= 10000;



-- 2026-03-30

-- 집계 함수
select count(*) as 총주문건수
from orders;

-- 그룹

select distinct bookid
from orders;

select bookid, salseprice
from orders
group by bookid;  -- 실행되지 않음

-- 그룹 + 집계 함수 

select bookid, count(*)
from orders
group by bookid;    -- bookid를  그룹화 해서 각각 카운트 해줌

-- 1권이라도 책을 구매한 고객의 명단(ID)
-- 2024년 7월 7일 이후에 (7/7)포함
select custid
from orders
where orderdate >= '2024-07-07'
group by custid;




select distinct custid
from orders;

select custid
from orders
group by custid;

-- 각 고객별로 구매한 도서의 갯수
-- + 2024 7월 7일 이후 구매한 주문을 대상
-- + 2권 이상을 구매한 고객의 명단
select custid, count(*) as 구매한_도서수량
from orders
where orderdate >= '2024-07-07'
group by custid
having	구매한_도서수량 >= 2;

-- where 없이 having만 쓰는 경우도 있음
-- 3권 이상 구매한 고객의 명단 
select custid, count(*)
from orders
group by custid
having count(*) >= 3;














-- 조건 결합 문제
-- 1. 판매가격이 10,000원 이상인 주문만 고려하여, 주문 횟수가 2회 이상인 고객을 조회하시오.
-- 2. 2024년 이후 주문만 고려하여, 총 구매 금액이 20,000원 이상인 고객을 조회하시오.
-- 3. 도서별 총 판매 금액이 30,000원 이상인 도서를 조회하시오.
-- 4. 고객별 최고 구매 금액이 20,000원 이상인 고객을 조회하시오.
-- 5. 고객별 평균 구매 금액이 12,000원 이상인 고객을 조회하시오.



-- 1번 문제 
-- 1. 판매가격이/ 10,000원 이상/인 주문만 고려하여, /주문 횟수/가 /2회 이상/인 고객/을 조회하시오.
select custid, count(*)
from orders
where saleprice >= 10000
group by custid
having count(*) >= 2;

select custid, count(*)
from orders
where saleprice >= 10000
group by custid
having count(*);


-- 2. 2024년 이후 /주문/만 고려하여, 총/ 구매 금액/이 20,000원 이상/인 고객/을 조회하시오.
select custid, sum(saleprice)
from orders
where orderdate >= '2024-07-04'
group by custid
having sum(saleprice) >= 20000;



-- 3. 도서별/ 총 판매 금액이 30,000/원 이상/인/ 도서/를 조회하시오.
select bookid, sum(saleprice), count(*)
from orders
group by bookid -- 그룹 하고 난 다움에 하는게 having
having sum(saleprice) >= 20000;

-- 4. 고객별/ 최고/ 구매 금액이 20,000원 이상/인 /고객/을 조회하시오.
select custid, max(saleprice)
from orders
group by custid
having max(saleprice) >= 20000;

-- 5. 고객별/ 평균 /구매 금액이 12,000원 이상/인/ 고객/을 조회하시오.
select custid, avg(saleprice)
from orders
group by custid
having avg(saleprice) >= 12000;

-- 3,4,5 where 가 안걸린 거 잘 기억하기










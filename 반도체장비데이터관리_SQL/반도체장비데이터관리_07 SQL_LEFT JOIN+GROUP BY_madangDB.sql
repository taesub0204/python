-- =========================================
-- 마당서점 DB LEFT JOIN + GROUP BY 실습문제
-- 테이블
-- Book(bookid, bookname, publisher, price)
-- Orders(orderid, custid, bookid, saleprice, orderdate)
-- Customer(custid, name, address, phone)
-- =========================================


-- 문제 1
-- 모든 고객의 고객번호, 이름, 주문 건수를 조회하세요.
-- 주문이 없는 고객도 포함하세요.
select c.custid, name,  count(orderid) -- 행의 갯수 그래서 count 함수 안에 order id를 넣어줘야 한다.
from customer as c
left join orders as o on c.custid = o.custid
group by c.custid, name; -- 하나의 쌍으로 봐서 두개가 동일 하게 묶임

-- 문제 2
-- 모든 도서의 도서번호, 도서명, 판매 건수를 조회하세요.
-- 판매된 적 없는 도서도 포함하세요.

select b.bookid, bookname,  count(o.bookid)
from book as b
left join orders as o on b.bookid = o.bookid
group by b.bookid, bookname;




-- 문제 3
-- 고객별 총 주문금액을 조회하세요.
-- 주문이 없는 고객도 포함하세요.

select c.custid, c.name, sum(saleprice)
from customer as c
left join orders as o on c.custid = o.custid
group by c.custid, c.name;


-- 문제 4
-- 판매된 도서 수를 고객별로 조회하세요.
-- 주문이 없는 고객도 포함하세요.

select name, count(o.bookid)
from customer as c
left join orders as o on c.custid = o.custid
group by name;


-- 문제 5
-- 출판사별 판매 건수를 조회하세요.
-- 단, 판매되지 않은 도서도 결과에 반영되도록 작성하세요.
select publisher, count(o.bookid) as 판매건수
from  book as b
left join orders as o on b.bookid = o.bookid
group by publisher;
 
 
-- 문제 6
-- 주문이 없는 고객만 조회하세요.
-- 고객번호, 이름, 주문 건수를 출력하세요.
select c.custid, c.name, count(o.orderid)
from customer as c
left join orders as o on c.custid = o.custid
group by c.custid, c.name
having count(o.orderid) =0;



-- 문제 7
-- 한 번도 판매되지 않은 도서만 조회하세요.
-- 도서번호, 도서명, 판매 건수를 출력하세요.

select *
from book as b
left join orders as o on b.bookid = o.bookid
where o.orderid is null;



-- 문제 8
-- 고객별 주문 건수를 조회하되,
-- 주문 건수가 많은 순서대로 정렬하세요.
select * -- 행의 갯수 그래서 count 함수 안에 order id를 넣어줘야 한다.
from customer as c
left join orders as o on c.custid = o.custid
group by c.custid, name
order by 주문건수 desc; -- 하나의 쌍으로 봐서 두개가 동일 하게 묶임



-- 문제 9
-- 도서별 판매 건수를 조회하되,
-- 판매 건수가 적은 순서대로 정렬하세요.
select b.bookid, bookname,  count(o.bookid) as 판매건수
from book as b
left join orders as o on b.bookid = o.bookid
group by b.bookid, bookname
order by 판매건수 asc;


-- 문제 10
-- 주문이 1건 이상인 고객만 조회하세요.
-- 고객번호, 이름, 주문 건수를 출력하세요.
select c.custid, c.name, count(o.orderid) as 주문건수
from customer as c
left join orders o 
on c.custid = o.custid 
group by c.custid, c.name
having count(o.orderid) >=1;








-- =========================================
-- 마당서점 DB LEFT JOIN + GROUP BY 실습문제 정답
-- 테이블
-- Book(bookid, bookname, publisher, price)
-- Orders(orderid, custid, bookid, saleprice, orderdate)
-- Customer(custid, name, address, phone)
-- =========================================

-- 실습 전 확인
-- 아래 데이터가 존재한다고 가정한다.
-- Customer: 6번 고객(주문 있음), 7번 고객(주문 없음)
-- Book: 11번 도서(주문 있음), 12번 도서(주문 없음)


-- 문제 1
-- 모든 고객의 고객번호, 이름, 주문 건수를 조회하세요.
-- 주문이 없는 고객도 포함하세요.
SELECT c.custid, c.name, COUNT(o.orderid) AS 주문건수
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid
GROUP BY c.custid, c.name;


-- 문제 2
-- 모든 도서의 도서번호, 도서명, 판매 건수를 조회하세요.
-- 판매된 적 없는 도서도 포함하세요.
SELECT b.bookid, b.bookname, COUNT(o.orderid) AS 판매건수
FROM Book b
LEFT JOIN Orders o
ON b.bookid = o.bookid
GROUP BY b.bookid, b.bookname;


-- 문제 3
-- 고객별 총 주문금액을 조회하세요.
-- 주문이 없는 고객도 포함하세요.
SELECT c.custid, c.name, SUM(o.saleprice) AS 총주문금액
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid
GROUP BY c.custid, c.name;


-- 문제 4
-- 판매된 도서 수를 고객별로 조회하세요.
-- 주문이 없는 고객도 포함하세요.
SELECT c.custid, c.name, COUNT(o.bookid) AS 주문도서수
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid
GROUP BY c.custid, c.name;


-- 문제 5
-- 출판사별 판매 건수를 조회하세요.
-- 단, 판매되지 않은 도서도 결과에 반영되도록 작성하세요.
SELECT b.publisher, COUNT(o.orderid) AS 판매건수
FROM Book b
LEFT JOIN Orders o
ON b.bookid = o.bookid
GROUP BY b.publisher;


-- 문제 6
-- 주문이 없는 고객만 조회하세요.
-- 고객번호, 이름, 주문 건수를 출력하세요.
SELECT c.custid, c.name, COUNT(o.orderid) AS 주문건수
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid
GROUP BY c.custid, c.name
HAVING COUNT(o.orderid) = 0;


-- 문제 7
-- 한 번도 판매되지 않은 도서만 조회하세요.
-- 도서번호, 도서명, 판매 건수를 출력하세요.
SELECT b.bookid, b.bookname, COUNT(o.orderid) AS 판매건수
FROM Book b
LEFT JOIN Orders o
ON b.bookid = o.bookid
GROUP BY b.bookid, b.bookname
HAVING COUNT(o.orderid) = 0;


-- 문제 8
-- 고객별 주문 건수를 조회하되,
-- 주문 건수가 많은 순서대로 정렬하세요.
SELECT c.custid, c.name, COUNT(o.orderid) AS 주문건수
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid
GROUP BY c.custid, c.name
ORDER BY 주문건수 DESC;


-- 문제 9
-- 도서별 판매 건수를 조회하되,
-- 판매 건수가 적은 순서대로 정렬하세요.
SELECT b.bookid, b.bookname, COUNT(o.orderid) AS 판매건수
FROM Book b
LEFT JOIN Orders o
ON b.bookid = o.bookid
GROUP BY b.bookid, b.bookname
ORDER BY 판매건수 ASC;


-- 문제 10
-- 주문이 1건 이상인 고객만 조회하세요.
-- 고객번호, 이름, 주문 건수를 출력하세요.
SELECT c.custid, c.name, COUNT(o.orderid) AS 주문건수
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid
GROUP BY c.custid, c.name
HAVING COUNT(o.orderid) >= 1;
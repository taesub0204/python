-- =========================================
-- 마당서점 DB  [INNER JOIN과 LEFT JOIN 비교] 실습문제 정답
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
-- 고객 이름과 주문번호를 조회하세요.
-- 주문이 있는 고객만 조회되도록 작성하세요.
SELECT c.name, o.orderid
FROM Customer c
INNER JOIN Orders o
ON c.custid = o.custid;


-- 문제 2
-- 모든 고객의 이름과 주문번호를 조회하세요.
-- 주문 이력이 없는 고객도 함께 나타나도록 작성하세요.
SELECT c.name, o.orderid
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid;


-- 문제 3
-- 문제 1과 문제 2의 결과를 비교하여,
-- 어떤 고객이 LEFT JOIN에서만 나타나는지 확인하세요.
-- 정답 해설:
-- 주문 이력이 없는 고객(예: 7번 고객 이수민)이 LEFT JOIN 결과에만 나타난다.
-- 이 경우 주문번호(orderid)는 NULL로 표시된다.



-- [고객 기준 LEFT JOIN]

-- 문제 4
-- 모든 고객의 고객번호, 이름, 주문번호, 주문일자를 조회하세요.
-- 주문이 없는 고객도 포함하세요.
SELECT c.custid, c.name, o.orderid, o.orderdate
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid;


-- 문제 5
-- 주문 이력이 없는 고객의 고객번호와 이름만 조회하세요.
SELECT c.custid, c.name
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid
WHERE o.orderid IS NULL;



-- [도서 기준 INNER JOIN / LEFT JOIN 비교]

-- 문제 6
-- 도서명과 주문번호를 조회하세요.
-- 주문된 도서만 조회되도록 작성하세요.
SELECT b.bookname, o.orderid
FROM Book b
INNER JOIN Orders o
ON b.bookid = o.bookid;


-- 문제 7
-- 모든 도서의 도서번호, 도서명, 주문번호를 조회하세요.
-- 판매된 적 없는 도서도 함께 나타나도록 작성하세요.
SELECT b.bookid, b.bookname, o.orderid
FROM Book b
LEFT JOIN Orders o
ON b.bookid = o.bookid;


-- 문제 8
-- 한 번도 판매되지 않은 도서의 도서번호와 도서명만 조회하세요.
SELECT b.bookid, b.bookname
FROM Book b
LEFT JOIN Orders o
ON b.bookid = o.bookid
WHERE o.orderid IS NULL;



-- [LEFT JOIN + 정렬]

-- 문제 9
-- 모든 고객의 이름과 주문번호를 조회하되,
-- 고객번호 오름차순으로 정렬하세요.
SELECT c.name, o.orderid
FROM Customer c
LEFT JOIN Orders o
ON c.custid = o.custid
ORDER BY c.custid ASC;


-- 문제 10
-- 모든 도서의 도서명과 주문번호를 조회하되,
-- 도서번호 오름차순으로 정렬하세요.
SELECT b.bookname, o.orderid
FROM Book b
LEFT JOIN Orders o
ON b.bookid = o.bookid
ORDER BY b.bookid ASC;
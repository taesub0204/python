-- 가격이 8000원 이상인 도서를 구매한 고객
-- 고객별 주문 도서의 총 수량
-- 단, 2권 이상 구매한 고객만 출력

select custid
from orders
where saleprice >= 8000;

select custid, count(*)
from orders
where saleprice >= 8000
group by custid;

select custid, count(*)
from orders
where saleprice >= 8000
group by custid
having count(*) >= 2;
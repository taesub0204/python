use sakila;

show tables;
select first_name from customer;

select first_name, last_name from customer;

show columns from customer;

describe customer;

select * from payment; 

select * from customer
where address_id
between 5 and 10;

select * from customer
where create_date
between '2005-06-17' and '2005-06-30';

select * 
from payment
where payment_date
between '2005-06-17' and '2005-06-30';

select *
from customer;

-- 이름 검색도 between으로 검색 가능함

select * 
from customer
where last_name
not between 'M' and 'o';

select *
from city;

select * 
from city 
where city = 'IFe' and country_id = 69; 

select * 
from city 
where city = 'IFe' and last_update < '2006-03-01';

select * 
from city 
where city = 'IFe' or last_update < '2006-03-01';

select *
from customer
where last_name in ('smith','brown','hall'); -- or를 세개로 연결해야 하는 데 in 연산을 사용하면 편리하게 씀...

select * 
from city 
where country_id = 86 and  city in ('Cheju','Sunnyvale','Dallas');

select * 
from city 
where country_id = 103 or country_id = 86 and city in ('Cheju','Sunnyvale','Dallas'); -- 연산자 우선순위 때문에 and를 먼저 처리됨

select * 
from city 
where (country_id = 103 or country_id = 86) and city in ('Cheju','Sunnyvale','Dallas'); -- 괄호로 쳐주면 우선순위가 괄호로 바뀜

select *
from address
where address2 = null;

select *
from address
where address2 is null;   -- null 과 공백 차이 유의 null 아무값도 안드러간 거 

select *
from address
where address2 = null;   -- 이렇게 x

select *
from address
where address2 = ''; -- 공백 검색됨 공백과 null은 같지 않음

select *
from customer
order by first_name desc;

select *
from customer
order by store_id, first_name, last_name;

select *
from customer
order by store_id asc, first_name desc, last_name limit 100, 10; -- 10인덱스 위치 부터니까 11째 부터 20개  101 부터 10 개

select *
from customer
limit 10 offset 100; -- 100 건너 뛰고 101 부터 10개 
-- pandas 문법은 파이썬 제공 아님

select *
from customer
where first_name like 'A%';

select *
from customer
where first_name like 'B%';

select *
from customer
where first_name like 'A%' and last_name like 'B%';

select *
from customer
where first_name like 'AA%';

select *
from customer
where first_name like '%RA';

select *
from customer
where first_name like '%A%';

select *
from customer
where first_name not like '%A%';

 select *
from customer
where first_name like '%A__'; -- A뒤에 최소한 두개 A__

 select *
from customer
where first_name like '%_%'; 

select * from sakila.staff;

with Temp(결과1) as ( 
	select 'a%cdv' union all 
    select 'A_BC' union all 
    select 'ABC' union all 
	select 'bb%%cdv' union all 
    select 'CA_%BC' union all 
    select 'A_B_C' 
)
select * from temp where 결과1 like '%#%%' escape '#';




select *
from customer
where first_name like 'A___A';

select *
from customer
where first_name like 'A%R_';  -- A R뒤에 문자 한개로 끝나는...

-- 정규 표현 식


select * 
from customer
where first_name 
REGEXP '^K\n$'; -- k로 시작하거나  N으로 끝나는 거

select * 
from customer
where first_name 
REGEXP 'K[L-N]'; -- k를 포함하고 L N 포함하는 문자

select * 
from customer
where first_name 
REGEXP 'A[A-C]'; -- A를 포함하고 L N 포함하는 문자


select * 
from customer
where first_name 
REGEXP 'A[^A-C]'; -- A를 포함하고 L N 포함하는 문자

select * 
from customer
where first_name 
like 'S%' and first_name
regexp 'A[L-N]'; -- 정규표현 식 like로 함께 서서 뽑아 낸다 S 로 시작 A 포함  [L-N] 중에 하나

select * 
from customer
where first_name 
like 'S%' and first_name
regexp 'A[L-N]';

select * 
from customer
where first_name regexp 'S{2}';

select * 
from customer
where first_name regexp 'S{1,2}'; -- s 하나 이상 s 2개 이하 


select * 
from customer
where first_name regexp '[abc]d';  -- ad bd cd 조합 문자 보여줌

select * 
from customer
where first_name regexp 'a*'; -- a 가 들어 있어도 되고 안들어도 되고

select * 
from customer
where first_name regexp '....';-- 4글자 이상 

-- 정규표현식 활용하면 여러가지 표현 가능




select * from film;
select special_features 
from film 
group by special_features;

select rating 
from film 
group by rating;

select rating, special_features 
from film
group by special_features, rating; -- 2개 까지 그룹하면 더 튜플이 더 추가됨   앞껄 기준으로 그룹핑 됨/ 그룹화 할 조건이 여러개면 앞쪽에 있는 항목으로 그룹화가 진행된다.

select count(rating), special_features 
from film
group by special_features, rating; 


select count(*), special_features 
from film
group by special_features; 

select special_features, rating
from film
group by special_features, rating
having rating = 'G';


select * from film;

select special_features, count(*) as cnt
from film
group by special_features
having cnt > 70;

select special_features, rating ,count(*) as cnt
from film
group by special_features, rating
having rating ='G' and cnt > 10 ;

select special_features, rating ,count(*) as cnt
from film
group by special_features, rating
having rating ='PG' and cnt > 30;

select distinct special_features, rating cnt
from film; -- distinct 유일한 값 2개로 조합 값 2개를 합친 거 찾아라alter


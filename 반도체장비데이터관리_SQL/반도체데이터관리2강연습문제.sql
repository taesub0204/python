use semicon_equipDB;

-- select
-- from 테이블 
-- where 필터링 조건;






-- select 기초

select * 
from equipmentuser; -- 1번문제

select model_name, status -- 2번문제
from equipment;

select user_id, equipment_id -- 3번문제
from usageLog;

select name, department -- 4번문제
from equipmentuser;


-- 비교연산자(where)

select *
from equipment where status = 'active'; -- 5번문제 '문자열'

select *
from equipmentuser where department = '품질팀'; -- 6번문제

select * 
from usagelog  where issue_report is not null; -- 7번문제

select * 
from equipment where status <> 'active'; -- 8번문제

-- BETWEEN 
select * 
from equipment where install_date
between '2021-01-01' and '2023-12-31'; -- 9번문제

select * 
from usagelog where use_date 
between '2024-03-05' and '2024-03-12'; -- 10문제

-- IN/NOT IN 

select * 
from equipmentuser where department in ('개발팀','품질팀'); -- 11문제

select * 
from equipmentuser where department = '개발팀'or'품질팀'; -- 11문제

select * 
from equipment where status in ('active','maintenance'); -- 12문제

select * 
from equipmentuser where department not in ('품질팀','개발팀'); -- 13문제 

-- LIKE
select * 
from equipmentuser where name like '김%'; -- 14문제

select *
from equipment where model_name like '%cvd%'; -- 15문제alter

select * 
from equipmentuser where name like '%호%'; -- 16문제

-- 복합조건 and/ or

select *
from equipment where status = 'active' 
and install_date >= '2022-01-01'; -- 17문제 2021 05 10 날짜가 들어옴 ;; 

select *
from	equipmentuser where  department = '연구팀' or department ='품질팀';  -- 18문제 

select *
from usagelog where issue_report is not null 
and use_date > '2024-03-08'; -- 19문제

-- order by

select * 
from equipment order by install_date desc; -- 20문제

select *
from equipmentuser order by name asc; -- 21문제

select * 
from usagelog order by use_date desc; -- 22문제

select * 
from equipment order by status, install_date asc; -- 23

select distinct department
from equipmentuser; -- 24

select distinct equipment_id
from usagelog; -- 25

select distinct user_id
from usagelog; -- 26

-- 복합 연습문제 

select *
from equipment where status = 'active' and install_date > '2022-01-01' order by install_date desc; -- 1번 문제

select *
from equipmentuser where department = '품질팀' or department = '개발팀' order by name asc; -- 2번 문제

select *
from usagelog where use_date between '2024-03-05' and '2024-03-12' 
and issue_report is not null order by use_date; -- 3번 문제

select * 
from equipment 
where model_name like '%A%' and status  <> 'retired'; -- 4번 문제 !!가능

select *
from equipmentuser 
where name like '김%' or name like'정%'  
order by name; -- 5번 문제

select * 
from usagelog 
where (equipment_id = '101' or equipment_id = '103') -- 괄호 필수 그래야 and 를 다 받을 수 있음
and use_date >= '2024-03-02' order by use_date desc;-- 6번 문제

select *
from equipment 
where (status = 'active' or status = 'maintenance') -- status라 지목 해줘야 정확히 됨 in (,)
and install_date between '2021-01-01' and '2023-12-31';  -- 7번 문제alter

select *
from usagelog
where issue_report like '%이상%' order by use_date desc; -- 8번 문제 

select *
from equipmentuser 
where department <> '연구팀' -- 9번 문제
and name like '%아%' or name like '%훈%';  -- %사용땐 like를 사용해야댐

select *
from equipment
where model_name like 'ETCH%' or model_name like 'CVD%' order by install_date desc; -- 10번 문제  시작하는 키워드 주의 

select * 
from equipment
where equipment_id = '101';

select * 
from equipment
where model_name = 'ETCH'; -- 똑깥이 떨어지지 않는지 확인필요 = , like(비슷한)

select *
from usagelog
where issue_report is null 
and equipment_id in('102','103','105') 
order by equipment_id asc; -- 문제 11번 단 표현은 and로 봐야댐 

select distinct department 
from equipmentuser
order by department; -- 문제 12번

select *
from equipment
where status = 'active' 
and model_name like '%-%' 
order by install_date desc; -- 13번 문제

select *
from usagelog 
where use_date >= '2024-03-08' 
and issue_report is not null 
order by use_date asc; -- 14번 문제 

select  distinct equipment_id
from usagelog
where use_date >= '2024-03-05'; -- 15번 문제

-- 통계가 주된 핵심 구해야댐 
-- 집계함수 
-- group by 3주 후에 알려줌






 









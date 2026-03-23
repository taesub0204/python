
-- 반도체장비데이터 관리 03강 집계 함수
-- 연습문제 2. semicon_equipDB

-- 1번 문제
select model_name as '장비모델', status '장비상태'
from equipment;

-- 2번 문제
select name as '사용자명', department '부서'
from equipmentuser;

-- 3번 문제
select equipment_id as 이름, use_date 사용일자 
from usagelog;


-- 집계함수 기본 문제 

-- 1번 문제 
select count(*)
from equipment;

-- 2번 문제
select count(*)
from usagelog;

-- 3번 문제
select max(use_date)
from usagelog;

-- 4번 문제
select min(use_date)
from usagelog;

-- 5번 문제
select max(install_date) as '설치일 최근'
from equipment;

-- 짐계함수 : 조건 포함 문제 

-- 1번 문제 
select count(*)
from equipment
where status = 'active';

-- 2번 문제 
select count(*) as '문제가 보고되지 않음'
from usagelog 
where issue_report is null;

-- 3번 문제 
select count(*) as '3월 10일 이후'
from usagelog
where use_date >= '2024-03-10';

-- 4번 문제 
select count(*) as '기록중 문제 발생 횟수'
from usagelog
where use_date >= '2024-03-10' and issue_report is not null;

-- 5번 문제 
select max(use_date)
from usagelog
where equipment_id = '105';

-- 6번 문제 
select count(*)
from equipment
where install_date >= '2022-01-01';

-- 7번 문제 
select count(distinct equipment_id) as '사용기록 존재하는 장비 종류수'
from usagelog;

select distinct

-- 생각해볼문제 















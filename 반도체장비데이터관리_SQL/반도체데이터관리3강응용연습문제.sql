

select equipment_id, use_date -- 1번 문제
from UsageLog
order by use_date DESC
limit 1;


select * -- 2번 문제
from equipment
order by install_date asc
limit 1;
-- 장비, 사용자, 사용기록 테이블 

select use_date, equipment_id  -- 3번 문제
from usagelog
where issue_report is not null
order by use_date desc 
limit 1;

select *  -- 4번 문제
from usagelog
where issue_report is not null 
order by use_date desc;

select equipment_id, install_date -- 5번 문제
from equipment
where status <> 'active';

select * -- 6번 문제
from equipment
where model_name like '%CVD%';

select equipment_id -- 7번 문제
from equipment
where install_date >= '2022-01-01' and status = 'active';

select equipment_id -- 8번 문제
from usagelog 
where use_date between '2024-03-10' and '2024-03-15';

select model_name -- 9번 문제
from equipment
where status = 'active'
order by install_date desc
limit 1;

select *-- 10번 문제
from usagelog 
where equipment_id = '103'
order by use_date desc
limit 1;
-- where 앞에 order by가 나올 수 없음









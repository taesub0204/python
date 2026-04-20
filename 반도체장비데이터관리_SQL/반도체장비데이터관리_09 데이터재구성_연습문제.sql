-- =========================================================
-- SemiconDB 실습 문제
-- 주제: 데이터를 읽고, 연결하고, 해석하기
-- 순서:
-- 1. 기초 문제
-- 2. 중간 난이도 문제
-- 3. 시간 기반 문제
-- =========================================================


-- =========================================================
-- [1] 기초 문제
-- 목적:
-- - 각 테이블의 역할 이해
-- - 장비별 사용/유지보수 기록 확인
-- - 데이터가 "이벤트 기록"이라는 점에 익숙해지기
-- =========================================================

-- 문제 1
-- 각 장비는 언제 사용되었는가?
-- 장비 ID와 사용 날짜를 함께 조회하세요.

select equipment_id, use_date
from usagelog
order by equipment_id;



-- select e.equipment_id, u.use_date
-- from equipment as e 
-- join usagelog as u  on e.equipment_id = u.equipment_id;





select * from equipment;
select * from usagelog;
select * from maintenancelog;


-- 문제 2
-- 각 장비는 언제 유지보수를 받았는가?
-- 장비 ID와 유지보수 날짜를 함께 조회하세요.
select equipment_id, maintenance_date 
from maintenancelog
order by equipment_id, maintenance_date;



-- 문제 3
-- 같은 장비에 대해 사용 기록과 유지보수 기록을 함께 비교해보세요.
-- 장비 ID 기준으로 두 기록을 함께 조회하세요.
select u.equipment_id, use_date, maintenance_date
from usagelog as u 
join maintenancelog as m on u.equipment_id = m.equipment_id
order by u.equipment_id, use_date, maintenance_date;

select * from usagelog;
select * from maintenancelog;





-- 문제 4
-- 장비 102번의 사용 기록만 조회하세요.
select * 
from usagelog
where equipment_id = 102
order by equipment_id, use_date;




-- 문제 5
-- 장비 102번의 유지보수 기록만 조회하세요.
select * 
from maintenancelog
where equipment_id = 102
order by equipment_id, maintenance_date;


-- =========================================================
-- [2] 중간 난이도 문제
-- 목적:
-- - JOIN, DISTINCT, GROUP BY, LEFT JOIN을 통해
--   데이터의 패턴을 파악하기
-- - 해석 문제로 넘어가기 전 연결 감각 익히기
-- =========================================================

-- 문제 6
-- 이상(issue_report)이 한 번이라도 보고된 장비를 찾아보세요.
-- 장비 ID와 모델명을 함께 조회하고,
-- 같은 장비가 중복되지 않도록 하세요.

select distinct e.equipment_id, model_name
from usagelog as u
join equipment as e on u.equipment_id = e.equipment_id
where issue_report is not null;





-- 문제 7
-- 각 장비가 얼마나 자주 사용되었는지 확인해보세요.
-- 장비 모델명과 사용 횟수를 조회하고,
-- 사용 횟수가 많은 순으로 정렬하세요.

select e.model_name, count(*) as 사용횟수
from usagelog as u
join equipment as e on u.equipment_id = e.equipment_id
group by e.model_name
order by count(*) desc;


select * from usagelog;


-- 문제 8
-- 유지보수 이력이 있는 장비와 없는 장비를 구분해보세요.
-- 모든 장비를 조회하되,
-- 유지보수 이력이 있는지 여부를 함께 확인할 수 있도록 조회하세요.
-- 8번도 제외

select * from maintenancelog;
select * from equipment;

select e.equipment_id, model_name, count(m.maintenance_id)
from equipment as e
left join maintenancelog as m on e.equipment_id = m.equipment_id
group by e.equipment_id, model_name;





-- 문제 9
-- 이상이 보고된 기록과 유지보수 기록을 함께 조회해보세요.
-- 같은 장비 기준으로
-- 사용 날짜와 유지보수 날짜를 함께 출력하세요.
select u.equipment_id, u.use_date, m.maintenance_date
from maintenancelog as m
join usagelog as u on m.equipment_id = u.equipment_id
where u.issue_report is not null
order by u.equipment_id;




-- x
-- 문제 10
-- 최근 상태가 변경된 장비를 확인해보세요.
-- 장비 모델명과 status_update_date를 조회하고,
-- 최근 날짜 순으로 정렬하세요.
-- 나중에 status_update_date 추가를 하고... 진행 예정


-- =========================================================
-- [3] 시간 기반 문제
-- 목적:
-- - 데이터를 시간 흐름으로 해석하기
-- - 이상 발생 → 유지보수 → 상태 변화의 관계 이해하기
-- =========================================================

-- 문제 11
-- 공정 중 이상이 보고된 장비가 이후 실제로 유지보수로 이어졌는지 확인해보세요.
-- 이상이 보고된 시점과 유지보수 시점을 함께 조회하세요.
select e.model_name,u.equipment_id ,use_date, maintenance_date, issue_report
from usagelog as u
join maintenancelog as m on u.equipment_id = m.equipment_id
join equipment as e on u.equipment_id = e.equipment_id
where u.issue_report is not null and m.maintenance_date >= use_date
order by u.equipment_id, use_date;



-- x
-- 문제 12
-- 이상 보고 이후, 장비가 현재 상태로 갱신되기까지의 흐름을 확인해보세요.
-- 이상 발생 시점(use_date)과
-- 현재 상태 갱신 시점(status_update_date)을 함께 조회하세요.
-- 나중에 status_update_date 추가를 하고... 진행 예정









-- =========================================
-- JOIN + GROUP BY 연습문제 (핵심 8문제)
-- =========================================

-- =========================================
-- ① 기초 문제
-- =========================================

-- 1. 사용자별 장비 사용 횟수를 조회하시오.
-- (사용자 이름, 사용 횟수)
-- (EquipmentUser, UsageLog)
select name, count(*)
from usagelog as u
join equipmentuser as eu on u.user_id = eu.user_id
group by name;


-- 2. 장비별 사용 횟수를 조회하시오.
-- (장비 모델명, 사용 횟수)
-- (Equipment, UsageLog)
select model_name, count(*) -- model name 11개  count 1개 씩 새어줌.
from usagelog as u
join equipment as e on u.equipment_id = e.equipment_id
group by model_name;

select * -- 11개 * 다른애들다 ... 
from usagelog as u
join equipment as e on u.equipment_id = e.equipment_id
group by model_name;-- 5개 


-- 3. 부서별 장비 사용 횟수를 조회하시오.
-- (부서명, 사용 횟수)
-- (EquipmentUser, UsageLog)
select department, count(*)
from usagelog
join equipmentuser on usagelog.user_id = equipmentuser.user_id
group by department;

select department, count(*) -- 행갯수가 안맞아서 에러 발생됨
from usagelog
join equipmentuser on usagelog.user_id = equipmentuser.user_id;






-- 4. 문제가 보고된 장비별 문제 발생 건수를 조회하시오.
-- (장비 모델명, 문제 발생 건수)
-- (Equipment, UsageLog)
select model_name, count(*)
from usagelog
join equipment on usagelog.equipment_id = equipment.equipment_id
where issue_report is not null
group by model_name;




-- =========================================
-- ② 요구사항 해석형 문제
-- =========================================

-- 5. “누가 장비를 가장 자주 쓰는지 먼저 보고 싶어요.”
-- 사용자별 사용 횟수를 조회하되,
-- 사용 횟수가 많은 순으로 정렬하시오.

select name, count(*)
from usagelog
join equipmentuser on usagelog.user_id = equipmentuser.user_id
group by name;






-- 6. “어떤 장비가 가장 많이 쓰였는지 알고 싶어요.”
-- 장비별 사용 횟수를 조회하되,
-- 사용 횟수가 많은 순으로 정렬하시오.
select model_name, count(*)
from usagelog
join equipment on usagelog.equipment_id = equipment.equipment_id
group by model_name
order by count(*) desc;




-- 7. “문제가 자주 발생한 장비를 찾고 싶어요.”
-- 문제 보고가 있는 기록만 대상으로 하여,
-- 장비별 문제 발생 건수를 조회하시오.
select model_name, count(*)
from usagelog
join equipment on usagelog.equipment_id = equipment.equipment_id
where issue_report is not null
group by model_name;


-- 8. “어느 부서가 장비를 많이 사용하는지 파악하고 싶어요.”
-- 부서별 장비 사용 횟수를 조회하시오.
select department, count(*)
from usagelog
join equipmentuser on usagelog.user_id = equipmentuser.user_id
group by department;





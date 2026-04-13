-- =========================================
-- MaintenanceLog + Equipment 실습문제
-- =========================================


-- 문제 1
-- 유지보수일자, 장비 모델명, 담당 엔지니어 이름을 함께 조회하세요.
select *
from maintenancelog;

select maintenance_date, e.model_name,  engineer_name
from maintenancelog as m 
join equipment as e on m.equipment_id = e.equipment_id;



-- 문제 2
-- maintenance_type이 '정기점검'인 기록에 대해
-- 유지보수일자, 장비 모델명, 담당 엔지니어 이름을 조회하세요.

select e.model_name, maintenance_date, engineer_name
from maintenancelog as m 
join equipment as e on m.equipment_id = e.equipment_id
where  maintenance_type = '정기점검';




-- 문제 3
-- 각 /엔지니어/별 유지보수 건/수/를 조회하세요.
-- 엔지니어 이름과 유지보수 건수를 출력하세요.

-- 그룹 기준
-- 행의 개수를 세는 방법 집계 합수 count() 

select  engineer_name, count(*)
from maintenancelog as m 
-- join equipment as e on m.equipment_id = e.equipment_id
group by engineer_name;


-- 문제 4
-- 각/ 장비별 유지보수 건수를 조회하세요.
-- 장비 모델명과 유지보수 건수를 출력하세요.

select e.model_name, count(*) as '유지보수 건수'
from maintenancelog as m 
join equipment as e on m.equipment_id = e.equipment_id
group by e.model_name;


-- 문제 5
-- 유지보수 기록이 //2//건 이상/인 엔지니어의 이름과 유지보수 건수를 조회하세요.
-- 횟수 == 집계함수, 트정 대상별 건수 == 그룹alter
-- '건수'에 조건을 걸려면, 집계함수로 한 후에, 그룹을 하고 나서 집계해야

select engineer_name , count(*) as '유지보수 건수'
from maintenancelog as m 
-- left join equipment as e on m.equipment_id = e.equipment_id
group by engineer_name
having count(*) >= 2;
-- left join 남발하지 않기...


-- 문제 6
-- /각 장비별 /가장 최근 /유지보수 날짜/를 조회하세요.
-- 장비 모델명과 가장 최근 유지보수 날짜를 출력하세요.
select e.model_name, max(maintenance_date)
from maintenancelog as m 
join equipment as e on m.equipment_id = e.equipment_id
group by e.model_name, maintenance_date;

-- 에러 메세지 non-aggregate 집계하지 않은 컬럼이 포함되어 있음 그리고 그룹이 되지 않은 녀석이 있어...


-- 문제 7
-- 유지보수/가 한 번도 수행되지 않은// 장비/의 모델명을 조회하세요.

-- maintenancelog에 한 번도 수행되지 않은 장비가 
-- 매칭 해서 봐야 할 게 있을 때(없는 정보도 끌어올 때)
select e.model_name
from equipment as e 
left join maintenancelog as m on m.equipment_id = e.equipment_id
where m.equipment_id is null;





-- 문제 8
-- //각 //장비/별/ 유지보수 건수//를 조회하세요.
-- 유지보수가 없는 장비도 포함하여 출력하세요.
select e.model_name, count(m.equipment_id)
from equipment as e 
left join maintenancelog as m on m.equipment_id = e.equipment_id
group by e.model_name;





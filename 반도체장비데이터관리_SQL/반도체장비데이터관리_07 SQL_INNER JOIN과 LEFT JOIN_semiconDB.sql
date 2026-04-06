-- =========================================
-- semiconDB JOIN 실습문제
-- =========================================
select * from equipment;
select * from equipmentuser;
select * from usagelog;

-- [INNER JOIN vs LEFT JOIN 비교 - 사용자 기준]

-- 문제 1
-- 사용자/ 이름/과 사용 /로그 /번호를 조회하세요.
-- 사용 기록이 있는 사용자만 조회되도록 작성하세요.

select eu.name, us.log_id 
from usagelog as us
inner join equipmentuser as eu on us.user_id = eu.user_id;

-- 문제 2
-- 모든 사용자의 이름과 사용 로그 번호를 조회하세요.
-- 사용 기록이 없는 사용자도 포함되도록 작성하세요.
select name, us.user_id
from equipmentuser as eu
left join usagelog as us on us.user_id = eu.user_id;


-- 문제 3
-- 문제 1과 문제 2의 결과를 비교하여,
-- 어떤 사용자가 LEFT JOIN에서만 나타나는지 확인하세요.
-- 둘다 사용자 확인됨 



-- [사용자 기준 LEFT JOIN 활용]

-- 문제 4
-- 모든 사용자의 사용자번호, 이름, 사용일자를 조회하세요.
-- 사용 기록이 없는 사용자도 포함하세요.
select eu.user_id, eu.name, us.use_date
from equipmentuser as eu
left join usagelog as us on eu.user_id = us.user_id;


-- 문제 5
-- 사용 기록이 없는 사용자만 조회하세요.
-- (사용자번호, 이름)
select eu.user_id, eu.name, us.use_date
from equipmentuser as eu
left join usagelog as us on eu.user_id = us.user_id
where use_date is null;







-- [장비 기준 INNER JOIN vs LEFT JOIN]

-- 문제 6
-- 장비 모델명과 사용 로그 번호를 조회하세요.
-- 사용된 장비만 조회되도록 작성하세요.

select model_name, log_id
from equipment as e
inner join usagelog as u 
on e.equipment_id = u.equipment_id;
 



-- 문제 7
-- 모든 장비의 장비번호, 모델명, 사용 로그 번호를 조회하세요.
-- 사용된 적 없는 장비도 포함하세요.

select e.equipment_id, e.model_name, u.log_id
from equipment as e
left join usagelog as u 
on e.equipment_id = u.equipment_id;
 



-- 문제 8
-- 한 번도 사용되지 않은 장비만 조회하세요.
-- (장비번호, 모델명)
select *
from equipment as e
left join usagelog as u 
on e.equipment_id = u.equipment_id
where use_date is null;


-- [JOIN 확장 - 사용자 + 장비]

-- 문제 9
-- 사용자 이름, 장비 모델명, 사용일자를 조회하세요.

select name, model_name, use_date
from usagelog as u
join equipment as e on u.equipment_id = e.equipment_id
join equipmentuser as eu on u.user_id = eu.user_id;





-- 문제 10
-- 모든 사용자의 이름과 장비 모델명을 조회하세요.
-- 사용 기록이 없는 사용자도 포함하세요.

select *
from equipmentuser as eu
left join usagelog as u on eu.user_id = eu.user_id;








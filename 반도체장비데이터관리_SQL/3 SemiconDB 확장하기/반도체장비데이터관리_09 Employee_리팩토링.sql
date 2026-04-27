-- =========================================================
-- SemiconDB 구조 리팩터링 실습
-- 내용:
-- 1) EquipmentUser -> Employee 테이블명 변경
-- 2) UsageLog.user_id -> employee_id 컬럼명 변경
-- 3) MaintenanceLog.engineer_name -> engineer_id로 변경
-- 4) Department.manager_name -> manager_id로 변경
-- =========================================================


-- =========================================================
-- 0. 현재 상태 확인
-- =========================================================
SELECT * FROM Department;
SELECT * FROM EquipmentUser;
SELECT * FROM Equipment;
SELECT * FROM UsageLog;
SELECT * FROM MaintenanceLog;
select * from employee;

-- =========================================================
-- 1. EquipmentUser -> Employee 테이블명 변경
-- =========================================================


rename table equipmentuser to employee; -- 데이터 성격이 바꿈 더 큰 개념으로 바꿈
select * from equipmentuser; -- 조회 했을 때 존재하지 않을 것임





-- =========================================================
-- 2. Employee의 user_id -> employee_id로 컬럼명 변경
-- =========================================================
alter table employee 
change user_id employee_id int; -- 컬럼 이름 바꿈 



-- =========================================================
-- 3. UsageLog의 user_id -> employee_id로 컬럼명 변경
-- =========================================================

alter table usagelog 
change user_id employee_id int;
select * from usagelog;

-- =========================================================
-- 4. MaintenanceLog: engineer_name -> engineer_id
-- =========================================================
-- engineer_id 컬럼 추가 
-- -> 기존 데이터의 engineer_id가 null 값인 데 , 값을 채워줘야 




-- 4-1. 새 컬럼 추가
alter table maintenancelog
add engineer_id int;

-- 티입이 잘 모르겠다 
desc employee;




-- 4-2. 이름 기준으로 engineer_id 매핑
-- 변경한 의미 맞게 값을 채워줘야 함
update maintenancelog as m 
join employee as e 
on m.engineer_name = e.name
set m.engineer_id = e.employee_id;

-- 4-3. 매핑 결과 확인
select engineer_id
from maintenancelog;

select * from maintenancelog;

-- 4-4. 매핑되지 않은 데이터 확인
select *
from maintenancelog
where engineer_id is null; -- 잘 채워 졌는 지 null 로 확인 안전제일



-- 4-5. 모든 행이 정상적으로 매핑된 뒤 NOT NULL 설정
-- 컬럼 속성을 바꾸는 경우 modify 
alter table maintenancelog
modify engineer_id int not null;

desc maintenancelog; -- 

-- 4-6. 외래키 추가
alter table maintenancelog
add constraint fk_maintenance_engineer
foreign key (engineer_id)
references employee(employee_id);

select * from maintenancelog;
desc maintenancelog;


-- 4-7. 기존 engineer_name 컬럼 삭제
alter table maintenancelog
drop column engineer_name;

-- 4-8. MaintenanceLog 에서 engineer_name 컴럼을 삭제하세요.alter

desc maintenancelog;




-- =========================================================
-- 5. Department: manager_name -> manager_id
-- =========================================================

-- 5-1. 새 컬럼 추가
desc department;
alter table department add manager_id int; -- int 자료형 빼먹음 주의alter



-- 5-2. 이름 기준으로 manager_id 매핑(manager_name 기준으로 manager_id를 채우세요. 결과를 확인하세요.)
select * from employee;
select * from department;

update department as d 
join employee as e 
on d.manager_name = e.name
set d.manager_id = e.employee_id;


-- 5-3. 매핑 결과 확인
select manager_id
from department;

-- 5-4. 매핑되지 않은 데이터 확인
select *
from department
where manager_id is null;

-- 5-5. manager_id가 모두 채워졌다면 외래키 추가
select * from employee;

alter table department
add constraint fk_manager_emp
foreign key (manager_id)
references employee(employee_id);

select * from department;




-- 5-6. 기존 manager_name 컬럼 삭제
alter table department drop column manager_name;
desc department;



show create table department;

CREATE TABLE `department` (
   `dept_id` int NOT NULL,
   `dept_name` varchar(50) NOT NULL,
   `office_location` varchar(50) DEFAULT NULL,
   `manager_id` int DEFAULT NULL,
   PRIMARY KEY (`dept_id`),
   UNIQUE KEY `dept_name` (`dept_name`),
   KEY `fk_manager_emp` (`manager_id`),
   CONSTRAINT `fk_manager_emp` FOREIGN KEY (`manager_id`) REFERENCES `employee` (`employee_id`)
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- =========================================================
-- 6. 최종 구조 확인
-- =========================================================
DESC Department;
DESC Employee;
DESC Equipment;
DESC UsageLog;
DESC MaintenanceLog;

SHOW CREATE TABLE Department;
SHOW CREATE TABLE Employee;
SHOW CREATE TABLE UsageLog;
SHOW CREATE TABLE MaintenanceLog;


-- =========================================================
-- 7. 최종 데이터 확인
-- =========================================================
SELECT * FROM Department;
SELECT * FROM Employee;
SELECT * FROM UsageLog;
SELECT * FROM MaintenanceLog;
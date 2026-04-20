-- =========================================
-- Department 생성 + EquipmentUser 구조 변경
-- =========================================


-- 1. Department 테이블 생성
create table Department(
	dept_id int primary key,
    dept_name varchar(50) not null unique,
    manager_name varchar(50),
    office_location varchar(50)
);

-- 2. Department 데이터 입력
insert into Department values (1, '제조팀', '김부장', 'A동 2층');
insert into Department values (2, '품질팀', '이과장', 'B동 1층');
insert into Department values (3, '연구팀', '박차장', 'A동 1층');
insert into Department values (4, '개발팀', '최팀장', '연구동 3층');
insert into Department values (5, '생산기술팀', '송팀장', '연구동 3층');



set SQL_SAFE_UPDATES = 0;
-- 3. EquipmentUser에 dept_id 컬럼 추가
alter table equipmentuser add dept_id int;





select * from equipmentuser;


select * from equipmentuser e
join department d on e.department = d.dept_name;


-- 4. 기존 department 값을 기반으로 EquipmentUser의 dept_id 채우기
update EquipmentUser eu
join Department d 
on eu.department = d.dept_name
set eu.dept_id = d.dept_id;


desc equipmentuser;

-- 5. EquipmentUser의 dept_id를 NOT NULL로 변경
alter table equipmentuser
modify detp_id int not null;

select * from equipmentuser;


-- 6. EquipmentUser에 외래키 추가
alter table equipmentuser
add constraint fk_employee_department
foreign key(dept_id)
references department(dept_id);

desc equipmentuser;
 



-- 7. EquipmentUser의 기존 department 컬럼 삭제
alter table equipmentuser -- equipmentuser 테이블에 수정할 건데 column 
drop column department;





-- 8. 최종 구조 확인
SELECT * FROM Department;

SELECT * FROM EquipmentUser;
DESC EquipmentUser;


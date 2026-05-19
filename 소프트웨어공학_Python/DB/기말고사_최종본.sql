USE processsensordb;

-- 1-1.
CREATE table Alarmlog(
	alarm_id int primary key,
	equipment_id int not null,
	run_id int not  null,
    alarm_time datetime not null,
    alarm_level varchar(20) not null,
    alarm_message varchar(255) not null,
	FOREIGN KEY (run_id) REFERENCES Runhistory(run_id),
    FOREIGN KEY (equipment_id) REFERENCES equipment(equipment_id)
);


SELECT * FROM Alarmlog;

-- 1-2

insert into Alarmlog values
(1,101,7,'2024-03-15 14:20:00','WARNING','챔버 온도 정상 범위 초과'),
(2,101,7,'2024-03-15 14:30:00','CRITICAL','챔버 온도 지속 상승'),
(3,102,8,'2024-03-15 15:05:00','WARNING','RF 출력 정상 범위 초과'),
(4,102,8,'2024-03-15 15:05:00','WARNING','가스 유량 정상 범위 이하'),
(5,102,8,'2024-03-15 15:20:00','CRITICAL','RF 출력 및 가스 유량 이상 상태 지속');

SELECT * FROM Alarmlog;

-- 2. 장비별 알람 발생 횟수 조회
SELECT equipment_id, count(*) as 장비별_알람
FROM Alarmlog
group by equipment_id
;



-- 2-1
select e.equipment_id, e.model_name, count(*) as alarm_count
from equipment as e
join Alarmlog as a
on a.equipment_id = e.equipment_id
group by e.equipment_id, e.model_name;

-- 2-2
-- 102	ETCH-A100	3(회) 에서 알람이 가장 많이 발생하였으며, RF 출력 및 가스 유량 이상 상태 지속됨alter

-- 3. 알람 등급별 발생 횟수 조회
-- 3-1 
select Alarm_level, count(*) as alarm_count
from equipment as e
join Alarmlog as a
on a.equipment_id = e.equipment_id
group by alarm_level;

-- 3-2
-- waring 3, critical 2  위험에서 크리티컬로 넘어가는 양상으로 보입니다.

-- 4
-- 4-1. 






-- 5. 
-- 5-1
select r.run_id, r.equipment_id, run_status, count(*) as alarm_count
from runhistory as r
join Alarmlog as a
on a.run_id = r.run_id
group by r.run_id
order by r.run_id desc
;

-- 5-2
-- step_id 1번(산화막증착)과 2번(식각공정) 
-- 식각 공정에서 많은 3회 warning 발생하였습니다.
select r.run_id, r.equipment_id, run_status, count(*) as alarm_count, step_id
from runhistory as r
join Alarmlog as a
on a.run_id = r.run_id
group by r.run_id
order by r.run_id desc
;

-- 7번 8번 공정에서 

SELECT * FROM Alarmlog;
select * from runhistory;




-- 6. 








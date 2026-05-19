USE processsensordb;

CREATE table Alarmlog(
	alarm_id int primary key,
    equipment_id int foreign key not null,
    run_id int foreign key not null,
    alarm_time datetime not null,
    alarm_level varchar(20) not null,
    alarm_message varchar(255) not null
    
);


select * from processsensordb;
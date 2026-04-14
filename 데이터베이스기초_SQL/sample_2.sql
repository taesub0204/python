use sample;
select * from parent;
select * from child;


insert into child values(21,1991208,'한경대학교',2),(40, 19910207,'반도체융합캠퍼스', 3);

select p.col_1, p.col_2, p.col_3,c.item_3, c.item_4 
from parent as p 
inner join child as c on p.col_1 = c.item_2
where c.item_4 = 2;

-- 2026-04-14 join 연산 다음주 이어서 할 예정 
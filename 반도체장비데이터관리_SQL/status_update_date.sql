ALTER TABLE Equipment
ADD status_update_date DATE;

UPDATE Equipment SET status_update_date = '2024-03-07' WHERE equipment_id = 101;
UPDATE Equipment SET status_update_date = '2024-03-06' WHERE equipment_id = 102;
UPDATE Equipment SET status_update_date = '2024-03-12' WHERE equipment_id = 103;
UPDATE Equipment SET status_update_date = '2024-03-09' WHERE equipment_id = 104;
UPDATE Equipment SET status_update_date = '2024-03-08' WHERE equipment_id = 105;
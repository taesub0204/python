import pymysql
import pandas as pd


# # 1. DB 연걸



conn = pymysql.connect(
    host='localhost', 
    user='taesub', 
    password='0204',
    database='processsensordb',
    charset='utf8mb4' # 문자열을 UTF-8로 인코딩하여 저장하기 위해 설정
    )



# 2. SQL 쿼리 작성

equipment_id = 101

query ="""
SELECT 
s.equipment_id,
sm.measured_at,
s.sensor_name,
sm.measured_value
 FROM SensorMeasurement sm
 join Sensor s
 on sm.sensor_id = s.sensor_id
 where s. equipment_id = %s
 order by sm.measured_at, s.sensor_name;
"""




#. SQL 실행 결과를 DataFrame으로 가져오기
df = pd.read_sql(query, conn, params=(equipment_id,))


# 4. 결과 확인
print("=======원시 센서 데이터=======")
print(df.head(10))   

print("\n 데이터 개수:", len(df))
print("\n 센서 목록:")
print(df['sensor_name'].unique())

#. DB 연결 종료
conn.close()




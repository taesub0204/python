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



query ="""
SELECT 
* FROM SensorMeasurement;


"""

query2 ="""
SELECT 
* FROM processstep;


"""


#. SQL 실행 결과를 DataFrame으로 가져오기
df = pd.read_sql(query, conn)
df1 = pd.read_sql(query2, conn)

# 4. 결과 확인
print(df.head())   
print(df1.head())

#. DB 연결 종료
conn.close()




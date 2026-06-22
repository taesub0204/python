import sqlite3
import pandas as pd

#DB 연결
conn = sqlite3.connect('d:/data/empdb.db')

#커서 생성

cur = conn.cursor()

# 테이블 목록 조회

cur. execute("SELECT name FROM sqlite_master WHERE type='table';")
result = cur.fetchall()
result

#dept 테이블의 전체 데이터 조회
cur.execute("SELECT * FROM dept;")
result = cur.fetchall()
result
cur.description #description은 컬럼명 0번 인덱스에 컬럼명이 들어있음
columns = [column[0] for column in cur.description] 
df_result = pd.DataFrame(result, columns=columns)
df_result

cur.close() #커서 종료
conn.close() # DB 연결 종료


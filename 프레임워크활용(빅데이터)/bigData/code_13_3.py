# import sqlite3
# import pandas as pd

# #DB 연결
# conn = sqlite3.connect('d:/data/empdb.db')

# #커서 생성
# df = pd.read_csv('d:/data/dept.csv')
# df.to_sql(name='dept_df', con=conn, if_exists='replace', index=False, dtype={'DEPTNO': 'integer', 'DNAME': 'text', 'LOC': 'text'})

# # emp 테이블 생성
# df = pd.read_csv('d:/data/emp.csv')
# df.to_sql(name = 'emp_df', con = conn, if_exists ='replace', index = False)

# conn.close()



import sqlite3
import pandas as pd

# SQLite 데이터베이스 파일에 연결
conn = sqlite3.connect('d:/data/empdb.db')

# CSV 파일을 읽어와 seoul 테이블에 추가(append) 저장
df = pd.read_csv('d:/data/seoul_201712.csv')
df.to_sql(name='seoul', con=conn, if_exists='append', index=False)

# SQL 실행을 위한 커서 생성
cur = conn.cursor()

# seoul 테이블의 전체 데이터 조회
cur.execute('SELECT * FROM seoul')
# fetchall(): 조회된 모든 행을 한 번에 가져와 (튜플의 리스트) 형태로 반환
rows = cur.fetchall()

# 조회 결과 컬럼명(cur.description)과 데이터(rows)로 DataFrame 생성
columns = [column[0] for column in cur.description]
df_result = pd.DataFrame(rows, columns=columns)
df_result

# 조회 결과를 CSV 파일로 저장
df_result.to_csv('d:/data/seoul123.csv', index=False)






conn.close()
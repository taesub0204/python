


import os


# SQLite.py 모듈을 먼저 찾은 뒤 작업 폴더를 변경합니다.
os.chdir('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/bigData/')

from SQLite import *

# SQLite 클래스(기능 모음)를 사용할 객체를 만듭니다.
SL = SQLite()

# 실행할 SQL 문장을 문자열로 저장합니다.
sql = "select * from dept"

# dept 테이블 전체 조회
SL.run_sql(sql)

# 급여(sal)가 2500보다 큰 사원만 조회하는 SQL
sql = "select * from emp where sal > 2500"
# 조회 결과를 result 변수(데이터프레임 형태)로 받습니다.
result = SL.run_sql(sql)

# 조회된 결과를 화면에 표시
result

# SAL 컬럼(급여)의 총합 계산
result['sal'].sum()

# 작업이 끝나면 DB 연결을 닫아 줍니다.
SL.close_db()

    
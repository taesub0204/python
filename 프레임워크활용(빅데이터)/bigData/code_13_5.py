import sys
import os
os.chdir('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/bigData/')
sys.path.insert(0, os.getcwd())

from SQLite import *
    
SL = SQLite()

sql = "select * from subway limit 10;"
result = SL.run_sql(sql)
result
result.head()

result['07-08+08-09시간대'] = result['07-08시간대'] + result['08-09시간대']



df_pivot = result.pivot_table(index='호선명', columns='승객유형', values='07-08+08-09시간대', aggfunc='sum')   
df_pivot

df_pivot.to_csv('d:/data/subway_pivot.csv', encoding = 'EUC-KR')
SL.close_db()
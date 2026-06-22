import sys
import os
os.chdir('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/bigData/')
sys.path.insert(0, os.getcwd())

from SQLite import *
import pandas as pd

SL = SQLite()
 
df = pd.read_csv('d:/data/서울교통공사.csv', encoding='cp949')
df.to_sql('subway', con = SL.conn, if_exists='replace', index=False)

SL.close_db()


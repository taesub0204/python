import pandas as pd
import matplotlib.pyplot as plt

emp = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/emp.csv')
dept = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/dept.csv')
emp.head()
dept.head()

df = emp.merge(dept, on = 'DEPTNO')
df.head()

df.pivot_table(index = 'LOC',columns='JOB', values = 'SAL', aggfunc='sum') # LOC를 행으로, JOB을 열로, SAL을 값으로 하는 피벗 테이블 생성
df.pivot_table(index='DNAME', columns='JOB', values='ENAME', aggfunc='count') # DNAME을 행으로, JOB을 열로, ENAME을 값으로 하는 피벗 테이블 생성
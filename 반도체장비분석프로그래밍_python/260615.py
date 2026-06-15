import os

from matplotlib.pylab import float32 

#현재 작업 폴더 (어디서 실행 중인지)
print(os.getcwd())

# 폴더 안 파일 목록
print(os.listdir('.'))

# data 폴더 만들기 (이미 있어도 에러 없음)
os.makedirs('data', exist_ok=True)


from pathlib import Path
base = Path('data')
file_path = base / 'equip_log.csv'
print(file_path) #data/equip_log.csv

#유영한 속성들
print(file_path.exists()) #파일 존재 여부
# 존재 여부 그냥 실행보고, data폴더 안에 equip_log 파일 옮기고 실행
print(file_path.suffix)
# .csv(확장자)
print(file_path.stem) 
# equip_log(이름)

import pandas as pd
# 읽기
df = pd.read_csv('data/equip_log.csv')
print(df.head())

# 쓰기 (index =False : 행번호 저장안함)
df.to_csv('data/output.csv', index=False) 

# 일부 컬럼만 저장
df.to_csv('out.csv', columns=['name', 'temp'], index=False) 



# df = pd.read_csv('kor', sep='utf')
# df = pd.read_csv('kor.csv', sep= 'cp949')

# df.to_csv('out.csv', index = False, encoding='utf-8-sig') #utf-8-sig : 엑셀에서 한글 깨짐 방지


# 헤더가 없는 파일  > 컬럼명 직접 지정 
df  = pd.read_csv('out.csv', header = None, names = ['name', 'temp'])

# 필요한 컬럼만 읽기 (대용량에서 메모리 절약)
df =pd.read_csv('out.csv', usecols = ['name', 'temp']) 

# 위쪽 설명 줄 건너뛰기 / 일부 행만 읽기
df = pd.read_csv('out.csv',skiprows = 3, nrows = 1000) 

# 특정 값을 결측치(Nan)로 인식
df = pd.read_csv('out.csv', na_values = ['ERROR', 'N/A', -999]) 
print(df)


df.head()
df.tail(3)
df.shape
df.columns
df.dtypes
df.info()
df.describe()


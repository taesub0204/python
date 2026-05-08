import pandas as pd
import numpy as np


#결측값을 포함하는 데이터 프레임 생성
df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')
df.head()
df.iloc[0,1] = pd.NA
df.iloc[0,2] = pd.NA
df.iloc[1,2] = np.nan
df.iloc[2,3] = None
df.iloc[100,3] = None
df.head()


#결측값 호가인
df.isnull().sum() # 각 열의 결측값 개수 확인 (열별로)
df.isnull().sum(axis=1) # 각 행의 결측값 개수 확인(행별로)
df.loc[df.isnull().sum(axis=1) > 0,:] # 결측값이 있는 행 선택

#결측값 제거 

df = df.dropna() # 결측값이 있는 행 제거
df.reset_index(drop = True) # 인덱스 재설정
df.head()
import pandas as pd
import numpy as np


# 자료의 입력
temp = pd.Series([-0.8, -0.1, 7.7, 13.8, 18.0, 22.4,
25.9, 25.3, 21.0, 14.0, 9.6, -1.4]) ##시리즈의 특징 시리즈의 번호가 출력됨 세로로 출력 시리즈 특징
temp



temp.index =  ['1월','2월','3월','4월',
               '5월','6월','7월','8월',
               '9월','10월','11월','12월'
               ]

temp 


temp.size # 판다스에서 제공하는 size
len(temp) # 길이 함수 파이썬 자체 내장함수
temp.shape # ,발생
temp.dtype
temp.shape[0] # 열에 갯수 1  ,  행의 갯수 값은 마지막 새로 추가될 번호

temp # 레이블은 있지만, 고유한 인덱스 있음
temp.iloc[2] #7.7  "인데스로 접근할꺼면 i"
temp.loc['3월'] #7.7
temp.iloc[[3,5,7]] # 4월, 6월, 8월  여러개 줄 떄는 리스트 구조로 
temp.loc[['4월','6월', '8월']] # 4월 6월 8월 
temp.iloc[5:8] # 5월 포함안됨 8까지 나옴
temp.loc['6월':'9월']#포함됨
temp.iloc[:4] #4월까지 나옴 
temp.iloc[9:] #9포함안됨 10
temp.iloc[:]# 전체 출력


temp.loc[temp >= 15]
temp.loc[temp >= 15].index#월만 조회함 위치만
temp.loc[(temp >= 15) & (temp < 25)]
temp.loc[(temp < 5) | (temp >= 25)]
march = temp.loc['3월']
march
temp.loc[temp < march]
temp.loc[temp < temp.loc['3월']]
temp.where(temp >= 15) # 다가져 오는데 조건에 맞지 않은 애덜 다 가져옴 nan
temp.where(temp >= 15).dropna()

temp.reset_index(drop=True, inplace=True) #원래 있던 레이블 없애고 새롭게 출력  새로운 변화를 바로 반영 하고 싶을 때
temp

# t1 = temp.reset_index(drop=True)
# t1

temp 
temp + 1 #실제에 더해지지 않는다. temp = 필요
temp * 2 + 0.1 
temp + temp
temp.loc[temp >= 15] + 1
temp # 다시 원래 값으로 돌아 온 것을 확인 할 수 있음


temp.sum() #합계
temp.mean() # 값들의 평균
temp.median() #값들의 중앙값
temp.max()
temp.min()
temp.std()#표준편차
temp.var()#값들의 분산
temp.abs()#값들의 절대값
temp.describe()#기초 통계 정보
import pandas as pd
import numpy as np


# 자료의 입력
temp = pd.Series([-0.8, -0.1, 7.7, 13.8, 18.0, 22.4,
25.9, 25.3, 21.0, 14.0, 9.6, -1.4])

temp.index =  ['1월','2월','3월','4월',
               '5월','6월','7월','8월',
               '9월','10월','11월','12월'
            
               ]
temp 


temp.iloc[2] #7.7
temp.loc['3월'] #7.7
temp.iloc[[3,5,7]] # 4월, 6월, 8월 
temp.loc[['4월','6월', '8월']] # 4월 6월 8월 
temp.iloc[5:8] # 5월 포함안됨 8까지 나옴
temp.loc['6월':'9월']#포함됨
temp.iloc[:4] #4월까지 나옴 
temp.iloc[9:] #9포함안됨 10
temp.iloc[:]# 전체 출력

temp.loc[temp >= 15]
temp.loc[(temp >= 15) & (temp < 25)]
temp.loc[(temp < 5) | (temp >= 25)]

march = temp.loc['3월']
temp.loc[temp < march]

temp.where(temp >= 15)
temp.where(temp >= 15).dropna()

temp.where(temp >= 15)
temp.where(temp >= 15).index
temp
temp + 1
2*temp + 0.1
temp 
temp +temp
temp.loc[temp >= 15] + 1

temp
temp.sum()
temp.median() # 짝수 일 때는 34 중앙값 미디엄값 설정함.
temp.mean() # 평균
temp.max()
temp.min()
temp.std() #표준편차
temp.var()
temp.abs() #절대값 
temp.describe() # 앞에서 진행 했던 내용 묘사
temp.value_counts()# 빈도수 도수분포를 확인하는 거


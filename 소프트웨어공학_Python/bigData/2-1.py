import pandas as pd   #판다스 쓰기 위해서 improt pandas 넣고 짧게 pd 별칭
#직접리스트입력
import numpy as np

age = pd.Series([25,34,19,45,60])


print(age)#age의내용출력
print(type(age))#age 데이터형 확인

#이미 생성된 리스트 객체 입력
data = ['string', 'summer', 'fall','winter']
data

np1=np.array(data)
print(data)
print(np1)
season = pd.Series(data)
print(season)
print(type(data))
print(type(np1))
print(type(season))



# #print(season)
# print(data)
# print(season.iloc[2]) #인덱스2



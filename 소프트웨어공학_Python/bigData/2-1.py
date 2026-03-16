import pandas as pd   #판다스 쓰기 위해서 improt pandas 넣고 짧게 pd 별칭
#직접리스트입력

age = pd.Series([25,34,19,45,60])


print(age)#age의내용출력
print(type(age))#age 데이터형 확인

#이미 생성된 리스트 객체 입력
data = ['string', 'summer', 'fall','winter']
season = pd.Series(data)

#print(season)
print(data)
print(season.iloc[2]) #인덱스2
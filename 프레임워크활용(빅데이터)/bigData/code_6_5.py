import pandas as pd

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')
df2 =df.loc[:,~df.columns.isin(['Species'])] # 종을 제외한 나머지 열 선택
df2
df2.corr() # 상관계수 계산, 종을 제외한 나머지 열의 상관계수 계산
df['Petal_Length'].corr(df['Petal_Width']) # 두 열의 상관계수 계산
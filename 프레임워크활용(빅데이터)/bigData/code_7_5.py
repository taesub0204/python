import pandas as pd 

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')

#데이터프레임의 정렬
#오름차순 정렬
df_sorted = df.sort_values('Sepal_Length') # Sepal_Length 열을 기준으로 오름차순 정렬
df_sorted.head(10) # 정렬된 데이터 프레임의 상위 10개 행 확인

#내림차순
df_sorted = df.sort_values('Sepal_Length', ascending=False) # Sepal_Length 열을 기준으로 내림차순 정렬
df_sorted.head(10) # 정렬된 데이터 프레임의 상위 10개 행 확인

#여러 개의 기준 컬럼 적용
df_sorted = df.sort_values(['Species', 'Sepal_Width'])
df_sorted.head(10)

df['Petal_Length'].rank() # 실수로 표시됨 기본은 오름 차수 석차를 구해줌
df['Petal_Length'].rank().astype(int) # 오름차순 순위
df['Petal_Length'].rank(ascending=False).astype(int) # 내림차순 순위
import pandas as pd  

df = pd.read_csv('iris.csv')  # csv 파일 읽기
# df = pd.read_csv('iris.csv', header=None) 
df # 내용 확인


# 코드4-1에 이어서 실행
# 데이터프레임 객체의 정보 확인
df.info() # 배열의 정보   결측치가 없음 null이 없음 
df.shape # 배열의 형태
df.shape[0] # 행의 개수
df.shape[1] # 컬럼의 개수

df.dtypes # 각 컬럼의 자료형
df['Species'] = df['Species'].astype('category') # 카테고리는 범주형으로 설정해라 
df.dtypes

df.index #인덱스 정보
df.columns # 컬럼들의 이름 보기



df.head(20) # 데이터 앞부분 일부 보기  맨앞 5개
df.tail(20) # 데이터 뒷부분 일부 보기  맨뒤 5개

df['Species']
df['Species'].unique() # 품종 정보 확인
df



# 코드4-1에 이어서 실행
# 인덱싱과 슬라이싱
df.iloc[2,3] # 2행 3열의 값
df.loc[3,'Petal_Width'] # 'Petal_Width' 컬럼의 3행 값
df.loc[[0,2,4],['Petal_Length', 'Petal_Width']]

df.loc[5:8,'Petal_Length'] # 'Petal_Length' 컬럼의 5~7행 값 8포함
df.iloc[:5,:4] # 0~4행, 0~3열의 값

df.loc[:, 'Petal_Length'] # 'Petal_Length' 컬럼의 모든 행의 값
df['Petal_Length'] # 'Petal_Length' 컬럼의 모든 값
df.Petal_Length # 'Petal_Length' 컬럼의 모든 값

df.iloc[:5,:] # 0~4행의 모든 컬럼의 값
df.iloc[:5] # 0~4행의 모든 컬럼의 값





# 꽃잎의 길이가 6.5 이상인 행들의 모든 컬럼을 보이시오
df
df.loc[df.Petal_Length >= 6.5, :] # 모든 열에 대해서 컬럼 지정
df.loc[df.Petal_Length >= 6.5] # 모든 컬럼의 경우 컬럼 인덱스 생략
# 꽃잎의 길이가 6.5 이상인 행들의 인덱스 번호를 보이시오
df.loc[df.Petal_Length >= 6.5].index

# 꽃잎의 길이가 3.5~3.8 사이인 행들의 모든 컬럼을 보이시오
df.loc[(df.Petal_Length >= 3.5) &(df.Petal_Length <= 3.8)]
# 꽃잎의 길이가 1.3 미만이거나 6.5 를 초과하는 행들의 꽃잎의 길이와 폭을 보이시오
df.loc[(df.Petal_Length < 1.3) | (df.Petal_Length > 6.5), ['Petal_Length','Petal_Width']]
# where() 를 이용한 조건 검색
df.where(df.Petal_Length >= 6.5).dropna()
df.where(df.Petal_Length >= 5)





#4-5
# 코드4-1에 이어서 실행
# 데이터프레임 객체에 대한 산술 연산
df.loc[:, df.columns != 'Species'] # 'Species' 컬럼을 제외
df.loc[:, ~df.columns.isin(['Species'])] # 'Species' 컬럼을 제외
df.loc[:, df.columns != 'Species'] + 10 # 모든 값들에 10을 더함
df['Sepal_Length'] + df['Petal_Length'] # 두 컬럼의 같은 행 값들끼리 연산
df
tmp = df['Petal_Length'].apply(lambda x: -1 if x >= 5 else 1) # 람다 함수 적용시켜   람다 함수는 petal legth =x로 집어녛어  -1이면 x >=5  면 -1 
tmp
df['Petal_Length'] * tmp




df2 = df.loc[:, df.columns != 'Species'] # 숫자타입 컬럼만 추출
df2.sum() # 값들의 합계
df2.mean() # 값들의 평균
df2.median() # 값들의 중앙값
df2.max() # 값들의 최대값
df2.min() # 값들의 최소값
df2.std() # 값들의 표준편차
df2.var() # 값들의 분산
df2.abs() # 값들의 절대값
df2.describe() # 기초통계정
# 프레임워크 여기까지 4-6
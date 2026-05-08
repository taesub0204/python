import pandas as pd
import itertools # 조합을 생성하기 위한 라이브러리
df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')


#임의 샘플링 
# 옵션 1. n : 샘플링할 개수 지정 frac : 샘플링할 비율 지정
# random_state : 난수 시드값 지정, 동일한 시드값을 사용하면 동일한 샘플링 결과를 얻을 수 있음
df20 = df.sample(n=20, random_state=123) # 데이터 프레임에서 20개의 샘플을 무작위로 추출, random_state는 재현성을 위해 설정
df20

df20 = df.sample(n=20, random_state=100) 
df20

df20 = df.sample(n=20, random_state=40) 
df20

df20 = df.sample(n=20, random_state=123) 
df20

df20 = df.sample(n=20, random_state=123) 
df20

df20 = df.sample(n=20, random_state=123) 
df20

# 층화 샘플링
# 그룹별로 샘플링, 각 그룹에서 20%의 샘플을 무작위로 추출, random_state는 재현성을 위해 설정
stratified = df.groupby('Species').apply(lambda x: x.sample(frac=0.2, random_state=123)) 
stratified

#조합
species = df.Species.unique() # 고유한 종의 이름을 배열로 반환
species

comb = list(itertools.combinations(species, 2)) # 고유한 종의 이름을 조합하여 2개씩 묶은 리스트 생성
comb

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')

df_agg = df.groupby('Species').mean() # 종별로 그룹화하여 각 그룹의 평균 계산
df_agg

df_agg = df.groupby('Species').std() # 종별로 그룹화하여 각 그룹의 표준편차 계산
df_agg

df_agg = df.groupby('Species').max() # 종별로 그룹화하여 각 그룹의 평균 계산
df_agg

df_agg = df.groupby('Species').min() # 종별로 그룹화하여 각 그룹의 최소값 계산
df_agg
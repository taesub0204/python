import pandas as pd
from scipy import stats
import matplotlib.pyplot as plt

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/ind_ttest.csv')
df

# 데이터 탐색
df.head()
df.groupby('group').count() #그룹별 표본 크기
df.groupby('group').mean()# 그룹별 평균
df.groupby('group').boxplot(grid = False)
plt.show()

group_1 = df.loc[df.group == 'A', 'height'] #(행 조건): group이라는 열의 값이 'A'인 행들만 필터링
group_2 = df.loc[df.group == 'B', 'height'] #(행 조건): group이라는 열의 값이 'B'인 행들만 필터링
group_1
group_2

#정규성 검정
stats.shapiro(group_1) #0.05보다 크면 정규성 만족
stats.shapiro(group_2) #0.05보다 크면 정규성 만족

#등분산성 검정
stats.levene(group_1, group_2)

#독립표본 t-검정
result = stats.ttest_ind(group_1, group_2, equal_var = True)
result
import pandas as pd
from scipy import stats
import matplotlib.pyplot as plt

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/mw_test.csv')
df

# 데이터 탐색
df.head()
df.groupby('group').count() #그룹별 표본 크기
df.groupby('group').mean()# 그룹별 평균
df.groupby('group').boxplot(grid = False)
plt.show()

group_1 = df.loc[df.group == 'A', 'score'] #(행 조건): group이라는 열의 값이 'A'인 행들만 필터링
group_2 = df.loc[df.group == 'B', 'score'] #(행 조건): group이라는 열의 값이 'B'인 행들만 필터링
group_1
group_2

#맨 휘트니 검정
stats.mannwhitneyu(group_1, group_2)
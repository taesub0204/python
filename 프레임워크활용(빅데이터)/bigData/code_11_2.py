import pandas as pd
from scipy import stats
import matplotlib.pyplot as plt

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/paired_ttest.csv')

df
# 데이터 탐색 

df.head()
df[['before', 'after']].mean() # 그룹별 평균
(df['after'] - df['before']).mean() # before와 after의 차이의 평균

fig, axes = plt.subplots(nrows = 1, ncols = 2)
df[['before', 'after']].boxplot(grid = False, ax = axes[0])
plt.ylim([60, 100])
df['after'].plot.box(grid = False, ax = axes[1])
plt.show()
# 대응 표본 t-검증일때는 각각의 정규성 검증이 아닌, 두 그룹의 차잇값에 대한 정규성 검정을 수행함(주의)
stats.shapiro(df['after'] - df['before']) # 정규성 검정
result = stats.ttest_rel(df['before'], df['after'])# 대응표본 t-검정
result

import pandas as pd
from scipy import stats
import matplotlib.pyplot as plt

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/wilcoxon_test.csv')

# 데이터 탐색
df.mean() # 그룹별 평균
(df['post'] - df['pre']).mean() # pre와 post의 차이의 평균

fig, axes = plt.subplots(nrows = 1, ncols = 2)
df[['pre', 'post']].boxplot(grid = False, ax = axes[0])
plt.ylim([60, 100])
df['post'].plot.box(grid = False, ax = axes[1])
plt.show()

#윌콕슨 부호 순위 검정
stats.wilcoxon(df['pre'], df['post'])
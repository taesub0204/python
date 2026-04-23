import pandas as pd
import matplotlib.pyplot as plt
plt.rcParams['font.family'] = 'Malgun Gothic' # 한글 폰트 설정
plt.rcParams['axes.unicode_minus'] = False# 음수 부호 깨짐 방지

hobby = pd.Series(['등산', '독서', '영화감상', '음악감상', '운동', '등산', '독서', '영화감상', '음악감상', '운동'])

hobby

fd = hobby.value_counts()
fd

fig, axes = plt.subplots(nrows =1, ncols =2) # nrows : 행의 개수, ncols : 열의 개수

fd.plot.bar(ax = axes[0])# bar 그래프
fd.plot.pie(ax=axes[1])# pie 그래프

fig.suptitle('선호 취미 분포', fontsize = 14)
plt.show()
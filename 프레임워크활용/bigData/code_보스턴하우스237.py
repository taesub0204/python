# (0단계) 라이브러리 및 그래프 환경 설정
import pandas as pd
import matplotlib.pyplot as plt
from pandas.api.types import CategoricalDtype

# 그래프에 한글 표시 준비
plt.rcParams['font.family'] = 'Malgun Gothic'
plt.rcParams['axes.unicode_minus'] = False

# (1단계) 데이터 준비
df = pd.read_csv('c:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/BostonHousing.csv')
df = df[['crim', 'rm', 'dis', 'tax', 'medv']]
df
titles = ['1인당 범죄율', '방의 개수', '직업센터까지의 거리',
          '재산세', '주택 가격']

# (2단계) 그룹 컬럼 추가
grp = pd.Series(['M' for i in range(len(df))])
grp


grp.loc[df.medv >= 25.0] = 'H'
grp.loc[df.medv <= 17.0] = 'L'
df['grp'] = grp
df

grp


# 그룹 컬럼의 자료형과 레이블 순서 변경
new_odr = ['H', 'M', 'L']
new_dtype = CategoricalDtype(categories=new_odr, ordered=True)
df.grp = df.grp.astype(new_dtype)# 그룹 컬럼의 자료형과 레이블 순서 변경
df.grp.dtype # 그룹 컬럼의 자료형 확인

# (3단계) 데이터셋의 형태와 기본적인 내용 파악
df.shape
df.head()
df.dtypes  # 컬럼별 자료형
df.grp.value_counts(sort=False)  # 주택 가격 그룹별 분포

# (4단계) 히스토그램으로 관측값의 분포 확인
# 화면 분할 정의
fig, axes = plt.subplots(nrows=2, ncols=3)
fig.subplots_adjust(hspace=0.5, wspace=0.3)  # 그래프 여백

# 각 분할 영역에 그래프 작성하기
for i in range(5):
    df[df.columns[i]].plot.hist(ax=axes[i//3, i % 3],# 각 분할 영역에 그래프 작성하기 i//3은 행 번호, i%3은 열 번호 i//3 값은 0,0,0,1,1 반복 i%3 값은 0,1,2,0,1 반복
                                ylabel='', xlabel=titles[i])

# 통합 그래프에 제목 지정
fig.suptitle('Histogram', fontsize=14)

# 분할 그래프 화면에 나타내기
fig.show()

# (5단계) 상자그림으로 관측값의 분포 확인
fig, axes = plt.subplots(nrows=2, ncols=3)
fig.subplots_adjust(hspace=0.5, wspace=0.3)  # 그래프 여백

# 각 분할 영역에 그래프 작성하기
for i in range(5):
    df[df.columns[i]].plot.box(ax=axes[i//3, i % 3],
                               label=titles[i])

fig.suptitle('Boxplot', fontsize=14)
fig.show()

# (6단계) 그룹별 관측값의 분포 확인
fig, axes = plt.subplots(nrows=2, ncols=3)
fig.subplots_adjust(hspace=0.5, wspace=0.3)  # 그래프 여백

# 각 분할 영역에 그래프 작성하기
for i in range(5):
    df.boxplot(column=df.columns[i], by='grp', grid=False,
               ax=axes[i//3, i % 3], xlabel=titles[i])

fig.suptitle('Boxplot by group', fontsize=14)
fig.show()

# (7단계) 다중 산점도를 통한 변수 간 상관관계의 확인
pd.plotting.scatter_matrix(df.iloc[:, :5])
plt.show()

# (8단계) 그룹 정보를 포함한 변수 간 상관관계의 확인
dict = {'H': 'red', 'M': 'green', 'L': 'gray'}
colors = list(dict[key] for key in df.grp)  # 각 점의 색을 지정
pd.plotting.scatter_matrix(df.iloc[:, :5], c=colors)
plt.show()

# (9단계) 변수 간 상관계수의 확인
df.iloc[:, :5].corr()

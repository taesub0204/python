import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt

df = pd.read_csv('C:/Users/user/Desktop/taesub/data/seoul_temp.csv')
df.head()
df
df['month'] = (df.날짜 - 20230000) // 100 #월 컬럼 생성
df.head()

# 그래프 테마 설정
sns.set_theme(style='whitegrid', rc={"figure.figsize" : (7,4)})
# 월별 평균 기온에 의한 순위 계산
tmp = df.groupby('month').mean()
tmp
rank = tmp['평균기온'].rank() -1
rank = rank.astype(int).to_list()
rank

mycolor = sns.color_palette('bwr', 12) # bwr : blue white red, 12 : 12가지 색상
mycolor = pd.Series(mycolor)[rank].to_list() # rank에 있는 값을 mycolor에서 뽑아서 리스트로 만들어라

plt.rcParams['font.family'] = 'Malgun Gothic'
plt.rcParams['axes.unicode_minus'] = False

sns.boxplot(data = df, 
            x ='month',
            y = '평균기온',
            hue = 'month',
            palette = mycolor
            ).set_title('월별 평균기온 분포')

plt.ylabel('기온')
plt.subplots_adjust(bottom=0.2)
plt.show()
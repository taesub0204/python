import seaborn as sns
import matplotlib.pyplot as plt
import pandas as pd
df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/SeoulBikeData_new.csv')

df.info()


# 막대 그래프로 평일, 주말/공휴일 색깔 다르게 구성 7,8,18,19 시 x값으로 설정, Rented Bike Count y값으로 설정


# Rented Bike Count 이상치 제거
# 이상치 확인할 수 있는 그래프 그리기(세로로 표현)
sns.boxplot(data=df, y='Rented Bike Count')
plt.title('Rented Bike Count 이상치 제거 후 확인')
plt.ylabel('Rented Bike Count')
plt.show()


df = df[df['Rented Bike Count'] < 20000] 

sns.boxplot(data=df, y='Rented Bike Count')
plt.title('Rented Bike Count 이상치 제거 후 확인')
plt.ylabel('Rented Bike Count')
plt.show()



sns.barplot(
    data=df,
    x='Hour',
    y='Rented Bike Count',
    hue='Holiday',
    ci=None,
    # x축 시간대 구간 설정
    order=[7, 8,9,10,11,12,13,14,15,16,17,18,19,20]



)




fig, axes = plt.subplots(1, 2, figsize=(12, 6))
sns.barplot(
    data=df,
    x='Hour',
    y='Rented Bike Count',
    hue='Holiday',
    ci=None,
    order=[7, 8,9,10,11,12,13,14,15,16,17,18,19,20],
    ax=axes[0]
)   
sns.barplot(
    data=df,
    x='Hour',
    y='Rented Bike Count',
    hue='Holiday',
    ci=None,
    order=[7,8,18,19],
    ax=axes[1]
)
# 한국어 깨짐 방지
plt.rc('font', family='Malgun Gothic')
# x y 라벨 한국어 깨짐 방지
plt.rcParams['axes.unicode_minus'] = False
axes[0].set_title('시간대별 대여량 (전체)')
axes[0].set_xlabel('시간대')
axes[0].set_ylabel('대여량')
axes[0].legend(title='휴일 여부', labels=['평일', '주말/공휴일'])
axes[1].set_title('시간대별 대여량 (7,8,18,19시)')
axes[1].set_xlabel('시간대')
axes[1].set_ylabel('대여량')
axes[1].legend(title='휴일 여부', labels=['평일', '주말/공휴일'])
plt.tight_layout()
plt.show()
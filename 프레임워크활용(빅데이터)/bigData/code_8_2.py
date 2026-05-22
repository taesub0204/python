import seaborn as sns
import matplotlib.pyplot as plt

df = sns.load_dataset('flights')
df.head()


# 그래프 테마 설정
sns.set_theme(style="whitegrid", rc={"figure.figsize" : (8,5)})
sns.set_palette('hls',12)

#월별, 연도별 항공기 탑승객 수
sns.lineplot(
        data = df,
        x = 'year',
        y = 'passengers',
        hue = 'month'
)
# 월별 그래프에 이름 넣어주기

for month in df['month'].unique():
    temp = df[df['month'] == month]
    plt.text(x=temp['year'].max(), 
             y=temp['passengers'].iloc[-1], 
             s=month)

plt.show()
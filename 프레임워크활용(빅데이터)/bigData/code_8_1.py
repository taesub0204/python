import seaborn as sns
import matplotlib.pyplot as plt

df = sns.load_dataset('tips')
df.head()
df['day'].unique()

# 그래프 테마 설정
sns.set_theme(style="whitegrid", rc={"figure.figsize" : (5,5)})
sns.set_palette(["red","green","blue","yellow"])

# 요일별 평균 지불 금액
sns.barplot(data=df,
            x = 'day',
            y = 'total_bill',
            estimator ='mean',
            hue= 'day',
            legend= True
            
            )
plt.legend(loc="center")
plt.show()

#요일별 성별 평균 지불 금액
sns.barplot(data=df,
            x = 'day',
            y = 'total_bill',
            estimator ='mean',
            hue= 'sex',
            ci = None
            
            )
plt.show()

#누적 막대그래프
df2 = df.pivot_table(index='day',
                     columns='sex',
                     values='total_bill',
                     aggfunc='mean'
                     
                     )

df2

df2.plot.bar(stacked=True) # 위로 해서 누적해서 그려라
plt.subplots_adjust(bottom=0.2) # 그래프 아래 쪽 여백 
plt.show()
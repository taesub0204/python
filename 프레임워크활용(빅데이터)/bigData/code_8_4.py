import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt

df = pd.read_csv('C:/Users/user/Desktop/taesub/data/crimeRatesByState2005.csv')
df.head()
df

# 버블차트
sns.set_theme(rc={'figure.figsize':(7,7)}) 

sns.scatterplot(
                    data = df,
                    x="murder", # x축
                    y="burglary", # y축
                    size="population",
                    sizes=(20,4000),
                    hue= "state", #  주
                    alpha=0.5, # 반투명
                    legend=False # 범주 없앰


                )

plt.xlim(0,12) # x축 값의 범위

for i in range(0, df.shape[0]):
    plt.text(x=df.murder[i], y = df.burglary[i], s=df.state[i],
             horizontalalignment = 'center', size ='small', color = 'dimgray'
             )
    
plt.show()
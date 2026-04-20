# pylint: disable=missing-module-docstring
# pylint: disable=unused-import

import matplotlib.pyplot as plt
import pandas as pd 
import seaborn as sns 

df = sns.load_dataset('tips')
df.head(10)

day = df.day

day

day.unique()
type(day)
fd = day.value_counts()
fd/len(day)

fd.plot.barh(xlabel = 'Day', ylabel = 'Frequency', rot = 0, title = '요일별 손님수', grid =True, figsize = (6,4))
plt.subplots_adjust(bottom=0.2)
plt.show()

exp =[0, 0.10, 0, 0.10]

wedge = {'edgecolor':'g','linewidth':3, 'width': 0.3}

fd.plot.pie(ylabel = '', title = '요일별 손님수', autopct = '%.1f%%', colors=['lightblue','lightgreen','lightpink','lightyellow'], figsize = (6,6), explode = exp, shadow = True, counterclock = False, wedgeprops = wedge)

#lbl = ['목요일','금요일','토요일','일요일']
plt.show()



df = sns.load_dataset('tips')
smoker = df.smoker
fd1 = smoker.value_counts()
col = ['lightblue', 'lightgreen']

fd1.plot.bar(xlabel = '흡연 여부', ylabel = 'Frequency', rot = 0, title = '흡연여부별 손님수', grid= True, figsize = (6,4), color = col)
plt.subplots_adjust(bottom=0.2)
plt.show()




df.time.unique()

fd2 = df.time.value_counts()
fd2
col = ['lightblue', 'lightgreen']
#lbl = ['점심', '저녁']
fd1.plot.pie(ylabel = '', title = '식사 시간별 손님수 ', autopct = '%.1f%%', color= col , figsize = (6,6) )
plt.show()

plt.subplots_adjust(bottom=0.2)
plt.show()



amount = df['total_bill']
amount.describe()
amount.quantile([0.25, 0.5, 0.75])
amount.plot.hist(bins = 6)
plt.show()
amount.value_counts(bins=6, ax=axes[0,0])
#20260410
#집에서 연습




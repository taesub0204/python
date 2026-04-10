# pylint: disable=missing-module-docstring
# pylint: disable=unused-import

import matplotlib.pyplot as plt
import pandas as pd   


hobby = pd.Series(['등산','낚시','골프','수영','등산','등산','낚시','수영','등산','낚시','수영','골프'])

hobby

fd = hobby.value_counts()
fd


fig, axes = plt.subplots(nrows=2, ncols=1)
fd.plot.bar(ax=axes[0])
fd.plot.pie(ax=axes[1])
fd.set_title('선호 취미 분포', fontsize=14)
plt.subplots_adjust(hspace=0.5)
plt.show()


df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/BostonHousing.csv')
house_price = df['medv']
house_price

house_price.mean()
house_price.median()
house_price.quantile([0.25, 0.5, 0.75])

quan = house_price.quantile([0.25,0.5,0.75])
bins = [house_price.min(), quan[0.25], quan[0.5], quan[0.75], house_price.max()]
bins

labels = ['Q1', 'Q2', 'Q3', 'Q4']
grp = pd.cut(house_price, bins = bins, labels=labels)
avg = house_price.groupby(grp).mean()
avg.plot.bar()
plt.xlabel('Quartiles')
plt.ylabel('House Price')
plt.title('Average House Price by Quartiles')
plt.show()


house_price.plot.box()
plt.show()

house_price.plot.hist(bins = 8)
plt.show()
# pylint: disable=missing-module-docstring
# pylint: disable=unused-import

import matplotlib.pyplot as plt
import pandas as pd   


df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')

fig, axes = plt.subplots(nrows = 2, ncols = 2)
df['Petal_Length'].plot.hist(ax=axes[0,0])
df['Petal_Length'].plot.box(ax=axes[0,1])
fd = df['Species'].value_counts()
fd.plot.pie(ax=axes[1,0])
fd.plot.barh(ax=axes[1,1])
fig.suptitle('Multiple Graph Example', fontsize = 14)
plt.show()
# pylint: disable=missing-module-docstring
import pandas as pd
import matplotlib.pyplot as plt
favorite = pd.Series(['WINTER', 'SUMMER', 'SPRING', 'SUMMER', 'SUMMER', 'FALL','FALL','SUMMER', 'SPRING', 'SPRING'])

favorite.value_counts
favorite.value_counts()


fd = favorite.value_counts()
type(fd)
fd['SUMMER']
fd.iloc[2]




fig, axes = plt.subplots(nrows=2, ncols=2, figsize=(20,10))

fd.plot.bar(xlabel = 'Season',
            ylabel = 'Frequency',
            rot = 0,
            title = 'Favorite Season',
            ax = axes[0,0])


fd.plot.barh(
        xlabel = 'Frequency',
        ylabel = 'season',
        rot = 0,
        title = 'Favorite Season',
        ax = axes[0,1])

plt.subplots_adjust(left = 0.2)
fig.suptitle('Favorite Season Graph')



# 5-3

fd.plot.pie(
        ylabel = '',
        autopct = '%1.2f%%', #퍼센트로 표현하겠다
        title = 'Favorite Season',
        ax = axes[1,0])


plt.subplots_adjust(wspace=0.3, hspace=0.3)
fig.suptitle('Favorite Season Graph')
plt.show()


#판다승서 append 안됨
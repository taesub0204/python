import pandas as pd
import numpy as np
from scipy import stats

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')
sw = df.Sepal_Width

df.Sepal_Width.head()

#Z-score
z = np.abs(stats.zscore(sw))
outliers = sw[z > 2]
print(outliers)

#IQR
Q1 = sw.quantile(0.25)
Q3 = sw.quantile(0.75)
IQR = Q3 - Q1


outliers = sw[(sw < Q1 - 1.5 * IQR) | (sw > Q3 + 1.5 * IQR)]
print(outliers)

#특이값제거
clean = sw.loc[~sw.isin(outliers)]
len(clean)
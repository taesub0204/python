# pylint: disable=missing-module-docstring
# pylint: disable=unused-import


import pandas as pd  
#df = pd.read_csv('iris.csv')
#df = pd.read_csv('C:\Users\user\Desktop\taesub\python\프레임워크활용\bigData) 슬러시 반대로 해서 결로 넣어줘야함
df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')


df.describe()
df.info()
df
df.shape
df.shape[0]
df.shape[1]
df.dtypes
df['Species'] = df['Species'].astype('category') # str 타입을 3종류로 범주형 데이터로 바꿈 ex 남, 여 같은거
df.dtypes
df.columns
df.head()
df.tail()
df['Species'].unique()
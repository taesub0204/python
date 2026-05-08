
import pandas as pd


df1 = pd.DataFrame([['a', 90], ['b', 80], ['c', 40]],
                   columns=['name', 'kor'])
df2 = pd.DataFrame([['a', 75], ['b', 60], ['d', 90]],
                   columns=['name', 'math'])

df1
df2

# df12 = df1.merge(df2, left_on = 'name', right_on = '이름')
# df12

#inner
df12 = df1.merge(df2, on ='name')
df12

#left
df12 = df1.merge(df2, how='left', on='name')
df12

#right
df12 = df1.merge(df2, how='right', on='name')
df12

#outer
df12 = df1.merge(df2, how='outer', on='name')
df12

df12 = df1.merge(df2, left_on = 'name', right_on = '이름')
df12
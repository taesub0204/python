import pandas as pd

df = pd.read_csv('iris.csv')
df


df.iloc[2,3]
df.loc[3,'Petal_Width' ]
df.loc[[0,2,4],['Petal_Length','Petal_Width']]
df.loc[5:8, 'Petal_Length']
df.iloc[:5, :4]
df.loc[:,'Petal_Length']
df['Petal_Length']
df.Petal_Length
df.iloc[:5,:]
df.iloc[:5]




#4-4 z
df.loc[df.Petal_Length >= 6.5, :]
df.loc[df.Petal_Length >= 6.5]
df.loc[df.Petal_Length >= 6.5].index
df.loc[(df.Petal_Length >=3.5) & (df.Petal_Length <= 3.8)]
df.loc[(df.Petal_Length < 1.3 ) | (df.Petal_Length > 6.0),['Petal_Length','Petal_Width']]
df.where(df.Petal_Length >= 6.5 ).dropna()
df.where(df.Petal_Length >= 5) 


#코드 4-5
df.loc[:,df.columns != 'Species']
df.loc[:,df.columns.isin(['Species'])]
df.loc[:,df.columns != 'Species'] + 10
df['Sepal_Length'] + df['Petal_Length']
tmp = df['Petal_Length'].apply(lambda x: -1 if x >= 5 else 1) #람다식을 적용해라   x에 -1 줘라, x에 1 줘라   x >= 5 이면 -1, x < 5 이면 1
tmp
df['Petal_Length'] * tmp

# 코드 4-6
df2 = df.loc[:,df.columns != 'Species']
df2
df2.sum()
df2.mean()
df2.median()
df2.max()
df2.min()
df2.std()
df2.var()
df2.abs()
df2.describe()

# 코드 4-7
df3 = df.copy()
df3
df3.iloc[1,2] = 5.5
df3

df3.loc[1,'Petal_Length'] = 1.1
df3

df3.Petal_Length.to_list()
df3.loc[df.Petal_Length > 6.5 , 'Petal_Length'] *= 100
df3.Petal_Length.to_list()





# 코드 4-8
new_idx = df3.shape[0] # 150 행 
new_idx
df3.loc[new_idx] = [1.1, 4.5, 3.4, 2.2, 'setosa']
df3.tail()

new_row = pd.DataFrame([[1.1, 2.2, 3.3, 4.4, 'virginica']], columns=df3.columns)
new_row

df3  =pd.concat([df3.iloc[:10], new_row, df3.iloc[10:]], ignore_index=True)
df3.iloc[8:13,:]

# ext = pd.DataFrame([[1.2, 3.5, 4]])
# ##df3 =df3._append(ext, ignore_index=True) ## 어팬드는  입력 필요 없음
# df3

new_col = df3.Petal_Length * 2
df3['new_col'] = new_col
df3


df4 = df.copy()
df4
df4.insert(loc =2, column='new_col2', value=new_col)
df4



# 코드 4-9

df5 = df.copy()
df5 = df5.drop(index=[1,3])
df5.head(5)
df5 = df5.reset_index()
df5

df5 = df5.drop(columns=['Petal_Length'])
df5
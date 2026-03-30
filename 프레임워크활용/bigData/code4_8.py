import seaborn as sns
import pandas as pd

df = sns.load_dataset('titanic')
df.columns # 배정보

df.head(10)

df['pclass'].unique()

df.pclass.unique()

df.iloc[2,4]

df.loc[:4,['pclass','sex','age','alive']] 

df.loc[df['age'] >= 60, ['pclass','age','alive']]

df.loc[(df.age < 10) & (df.sex == 'female'), ['age','alive']]

#df.loc[(df.alive =='yes') & (df.sex='male'),'age'].mean()

df.loc[df.alive == 'yes', 'alive'].count() /df.alive.count()

df.loc[df.alive == 'yes','alive'].count() /len(df)

df.loc[(df.alive == 'yes') & (df.age == df.age.min()),['sex','age']] # 11번 문제

df.loc[(df.alive == 'yes') & (df.pclass == 1), 'alive'].count() # 12번 문제

df.loc[(df.alive == 1 ), 'alive'].count()

df.loc[(df.alive == 'yes') & (df.pclass == 1), 'alive'].count() /df.loc[(df.pclass == 1),'alive'].count() # 12번 문제/

df.loc[(df.alive == 1 ), 'pclass'].count() - df.loc[(df.pclass == 2),'pclass'].count()

df1 = df.loc[(df.pclass == 3 )&(df.alive =='yes'), ['sex','age']]
df1.loc[df1.age == df1.age.max()]

df2 = df[['pclass','sex', 'age', 'alive']].copy()

df2

df2['age_calss'] = 'NOT'

df2.loc[df2.age < 40, 'age_class' ] = 'LOW'
df2
df2.loc[df2.age >= 40, 'age_class' ] = 'HIGH'
df2.head(20)
df.age + 10

df2.dropna(inplace=True);
df2
(df2.age + 10 ).mean()
df2.loc[:,~df2.columns.isin(['pclass'])]
df2
df2.to_csv('tit2.csv')
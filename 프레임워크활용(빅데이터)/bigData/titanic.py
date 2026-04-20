import seaborn as sns
import pandas as pd

df = sns.load_dataset('titanic')

# 행과 컬럼 몇개 인지 출력하시오
df.info()
df


# 컬럼이름 출력하시오
df.columns

# 처음 10행 출력하시오
df.head(10)

# 선실은 몇 개의 등급으로 구성되어 있는지 확인하시오.
df['pclass'].unique()
df.pclass.unique()
df['pclass'].value_counts()

# 인덱스 기준 2행 4열 출력하시오
df.iloc[2,4]

# 앞쪽 5명의 탑승객에 대해 선실, 성별, 나이, 생존 여부를 출력하시오.
df.loc[:4,['pclass','sex','age','alive']]

# 나이가 60세 이상인 탑승객에 대해 선실, 나이, 생존 여부를 출력하시오.
df.loc[df['age'] >= 60, ['pclass', 'age', 'alive']]

# 나이가 10세 미만인 여성 승객의 나이, 생존 여부를 출력하시오.
df.loc[(df['age']< 10) & (df['sex']=='female'),['age','survived']]

# 생존한 남성의 평균 연령을 구하시오.
df.loc[(df['survived'] == 1) & (df['sex'] == 'male'), ['age']].mean()

# 전체 승객 수 대비 생존자 비율
df
df.loc[df['alive'] == 'yes', 'alive'].count() / df['alive'].count() * 100

# 생존한 승객 중 가장 나이가 어린 승객의 성별과 나이를 출력하시오.
df.loc[df['alive'] == 'yes', ['sex', 'age']].min()

# 1등급 선실에 탑승한 승객 중 생존자 수를 구하시오.
df.loc[(df['alive'] == 'yes') & (df['pclass'] == 1), 'alive'].count()

# 1등급 선실에 탑승한 승객 중 생존자 비율을 구하시오.
df.loc[(df['alive']== 'yes') & (df['pclass'] ==1),'alive' ].count() / df.loc[df['pclass']==1, 'alive'].count()

# 1등실 승객  수와 2등실 승객 수 의 차이 
df.loc[df['pclass'] == 1, 'pclass'].count() - df.loc[df['pclass'] == 2, 'pclass'].count()

# 생존한 3등 승객 중 가장나이가 많은 승객의 의 성별과 나이를 확인하시오
df.loc[(df.alive == 'yes') & (df.pclass == 3), ['sex', 'age']].max()


# df에서 선실, 성별 , 나이 생조 여부 컬럼만 선택하여 df2에 복사한 후 내용을 출력하시오.
df2 = df.loc[:,['pclass','sex','age','alive']].copy()
df2

#16
df2['age_class'] = None
df2.loc[df2.age > 40, 'age_class'] = 'low'
df2.loc[df2.age >= 40, 'age_class'] = 'high'
df2

df2 = df.loc[:,['pclass','sex','age','alive']].copy()
df2
df2['age_class']= df2['age'].apply(lambda x: 'low' if x < 40 else 'high' )
df2




#17
df2['age'] + 10
df3 = df2['age'] + 10
df3.mean()

#19
df2
df2.loc[:, df2.columns != 'pclass']

#20
df2.to_csv("C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/titanic.csv", index=False)



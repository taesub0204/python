import seaborn as sns
df = sns.load_dataset('planets')


df

df.isnull().sum()
df.isna().sum()
df.isna().sum(axis =1).sum()

# mass, distance 컬럼을 제외 후, 결측값이 없는 행들만 추출하여 df_clean에 저장
df.drop(['mass', 'distance'], axis = 1, inplace = True)
df_clean = df.reset_index(drop = True) #인덱스 초기화 새로 날리고 초기화
df_clean

# orbital_period 컬럼에서 결측값을 추정하여 채운 후, 결측값 추정 전과 추정 후의 평균값을 출력하시오
mean_before = df_clean['orbital_period'].mean()
mean_before
df_clean['orbital_period'].fillna(mean_before, inplace = True)#결측값을 평균으로 채우기
mean_after = df_clean['orbital_period'].mean()
mean_after


# orbital_period 컬럼에서 Z-score를 특잇값을 찾아 출력하시오.
z_scores = (df_clean['orbital_period'] - df_clean['orbital_period'].mean()) / df_clean['orbital_period'].std()
outliers = df_clean[abs(z_scores) > 3]
print(outliers)


#distance 컬럼에서 IQR을 이용하여 특잇값을 찾아 출력하시오
df_clean['distance'] = df['distance'] #distance 컬럼을 다시 추가
Q1 = df_clean['distance'].quantile(0.25)
Q3 = df_clean['distance'].quantile(0.75)
IQR = Q3 - Q1
outliers_iqr = df_clean[(df_clean['distance'] < Q1 - 1.5 * IQR) | (df_clean['distance'] > Q3 + 1.5 * IQR)]
print(outliers_iqr)


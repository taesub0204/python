import pandas as pd 
import matplotlib.pyplot as plt

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/airquality.csv')
df.head()

df.isnull().sum() # 결측값 개수 확인
df.isna().sum(axis=1).sum() # 행별 결측값 개수 확인
df

df_cleaned = df.reset_index(drop=True) # 인덱스 재설정

ozone = df_cleaned['Ozone'] # Ozone 열 선택s
Q1 = ozone.quantile(0.25) # 1사분위수 계산
Q3 = ozone.quantile(0.75) # 3사분위수 계산
IQR = Q3 - Q1 # IQR 계산
IQR

outliers = ozone[(ozone < Q1 - 1.5 * IQR) | (ozone > Q3 + 1.5 * IQR)] # 이상치 선택
print(outliers) # 이상치 출력

df_cleaned = df_cleaned[~df_cleaned['Ozone'].isin(outliers)] # 이상치 제거
df_cleaned

result = df_cleaned.groupby('Month')[['Ozone', 'Solar.R', 'Wind', 'Temp']].mean() # 월별 Ozone 평균 계산

result


result.plot(marker='o') # 월별 Ozone 평균 그래프
plt.show()
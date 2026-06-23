import pandas as pd
import numpy as np

np.random.seed(42) #결과 재현용
n = 200
df = pd.DataFrame({
    'time':pd.date_range('2026-03-01',periods=n, freq ='s'),
    'temp':np.random.normal(450,5,n), #온도 ~450도
    'pressure':np.random.normal(1.0,0.05,n), # 압력 ~1.0Pa
    'vibration':np.random.normal(0.3,0.1,n) #진동 ~0.3mm/s

})
df
print(df.shape)
df

# 1) 결측치 : 30개를 무작위로 NaN 처리
idx = np.random.choice(df.index, 30, replace =False)
df.loc[idx,'temp'] = np.nan
df

# 2)이상치 : 5개를 비정상 고온으로
df.loc[df.index[:5], 'temp'] = 9999
df

# 3) 중복 : 앞 3행을 통째로 복제해 추가
df = pd.concat([df, df.iloc[:3]], ignore_index=True)
df

# 자료형 오류 : 압력을 문자열로
df['pressure'] = df['pressure'].astype(str)
df.to_csv('messy.csv',index = False) #저장

# 더러운 데이터 다시 읽기
df = pd.read_csv('messy.csv')

print(df.info()) #자료형 결측 개수 한눈에
print(df.isnull().sum())# 컬럼별 결측치 개수
print(df.duplicated().sum()) # 중복행 개수 
print(df.describe()) #통계요약 (이상치힌트)

# 결측이 있는 행 저전체 삭제 
df_drop = df.dropna()
print(len(df)), '→',len(df_drop) #203 > 173

#특정컬럼기준으로 만 삭제
df2 = df.dropna(subset=['temp'])

#결측이 너무 많은 '열' 삭제(예 50이상)
df3 = df.dropna(axis=1, thresh=len(df) * 0.5)

# 평균값으로 채우기 
df_mean = df.copy()
df_mean['temp'] = df_mean['temp'].fillna(df_mean['temp'].mean())

# 중앙값으로 채우기
df_median = df.copy()
df_median['temp'] = df_median['temp'].fillna(df_median['temp'].median())


# 직전 값으로 채우기
df_ffill = df.copy()
df_ffill['temp'] = df_ffill['temp'].ffill()

print("원본 결측치 개수")
print(df['temp'].isnull().sum())

print("평균 대체 후 결측치 개수")
print(df_mean['temp'].isnull().sum())

print("중앙값 대체 후 결측치 개수")
print(df_median['temp'].isnull().sum())

print("ffill 대체 후 결측치 개수")
print(df_ffill['temp'].isnull().sum())

# temp 컬럼에서 결측치는 잠시 제외하고 IQR 계산

temp_data = df['temp'].dropna() #결측치 제외한 temp 데이터
Q1 = temp_data.quantile(0.25)
Q3 = temp_data.quantile(0.75)
IQR = Q3 - Q1
low = Q1 - 1.5 * IQR
high = Q3 + 1.5 * IQR
print("IQR:", IQR)
print("정상범위:", low, "~", high)
#정상 범위 안의 값만 남기기
mask = (df['temp'] >= low) & (df['temp'] <= high)
df_clean = df[mask]
print("원본 행 개수: ", len(df))
print("IQR 이상치 제거 후 행 개수: ", len(df_clean))
print("이상치 제거 후 temp 최대값:", df_clean['temp'].max())

#실습 4-2 Z score 이상치 탐지
# Z-score= (값 - 평균) / 표준편차 → 평균에서 몇 표준편차 떨어져 있는지

# [실습 4-2] Z score 이상치 탐지
temp_data = df['temp'].dropna() #결측치 제외한 temp 데이터
mean = temp_data.mean()
std = temp_data.std()
df_z = df.copy()
df_z['z'] = (df_z['temp'] - mean) / std
df_z_clean = df_z[df_z['z'].abs() <= 3]
df_z_clean = df_z_clean.drop(columns='z')
print("Z-score 기준 이상치 제거 후 행 개수: ", len(df_z_clean))
print("Z-score 기준 제거 후 temp 최대값:", df_z_clean['temp'].max())


# [실습 4-3] 시각화로 눈으로 확인하기
# 숫자만으로 감이 안옵니다. 그래프로 그리면 이상치가 한누에 보입니다.





import matplotlib.pyplot as plt
#[실습 4-3] 시각화로 이상치 확인
import matplotlib.pyplot as plt
#박스플롯: 이상치가 점으로 튀어나옴

df.boxplot(column='temp')
plt.title('Temperature Boxplot')
plt.show()

#시계열 선그래프 : 9999 지점이 위로 솟구침
df['temp'].plot(title = 'Temperature over time')
plt.xlabel('Index')
plt.ylabel('Temperature')
plt.show()

# 이상치 제거 후 다시 확인
df_clean['temp'].plot(title = 'Temperature after IQR Claeaning')
plt.xlabel('Index')
plt.ylabel('Temperature')
plt.show



#실습 5-1 중복제게 & 자료형 변환
# 1) 중복 행  제거

print(df.drop_duplicates().sum()) #3
df =df.drop_duplicates()

# 2) 문자열 > 숫자변환
df['pressure'] = pd.to_numeric(df['pressure'], errors='coerce')

# 3) 문자열 > 날짜 시간 변환
df['time'] = pd.to_datetime(df['time'])
print(df.dtypes) #타입 확인
import pandas as pd
from sklearn.impute import KNNImputer
from sklearn.preprocessing import MinMaxScaler


#결측값을 포함하는 데이터 프레임 생성
df_org = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')
df_miss = df_org.copy()


#결측값 생성 
df_miss.iloc[0,3] = pd.NA ; df_miss.iloc[0,2] = pd.NA
df_miss.iloc[1,2] = None ; df_miss.iloc[2,3] = None 
df_miss.head(4)

# (1) 데이터 표준화
scaler = MinMaxScaler() # MinMaxScaler 객체 생성, 데이터 범위를 0과 1 사이로 설정
df_scaled = scaler.fit_transform(df_miss.iloc[:,0:4]) # fit transform으로 데이터 표준화, 결측값은 그대로 유지  0부터 1 사이로 스케일링된 데이터 반환, 결측값은 NaN으로 유지
df_scaled [0:5, :]


#(2) 결측값 추정
imputer = KNNImputer(n_neighbors=5) # KNNImputer 객체 생성, 이웃 수 설정 해서 결측값을 대체할 때 사용할 이웃의 수를 5로 설정
df_scaled = imputer.fit_transform(df_scaled) # KNNImputer로 결측값 대체
df_scaled [0:5, :]# array 형태로 반환된 데이터 확인 결측값이 대체된 데이터 확인, 5개 평균 값으로 대체된 것을 확인할 수 있음

#(3) 표준화 이전으로 변환
df_filled = scaler.inverse_transform(df_scaled) # 스케일링된 데이터를 원래대로 되돌림
df_filled [0:5, :]# 넘파이 상태

df_miss.iloc[:,0:4] = df_filled # 원래 데이터 프레임에 대체된 값 할당

# 추정값의 정확도 확인
df_miss.head(4)
df_org.head(4)
# 최근접 이웃 분류 과정
import pandas as pd
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split
from sklearn.neighbors import KNeighborsClassifier as KNC



# 데이터 준비
df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/PimaIndiansDiabetes.csv')
df.head() # 데이터 확인
df.groupby('diabetes')['diabetes'].count() # 범주형 변수의 빈도 확인

x = df.drop('diabetes', axis = 1) # 독립 변수
y = df['diabetes'] # 종속 변수

# 데이터 표준화
scaler = StandardScaler() # 평균이 0, 분산이 1이 되도록 표준화하는 클래스
scaler.fit(x) # 표준화 모델 학습
result = scaler.transform(x) # 표준화된 데이터 반환
x_scaled = pd.DataFrame(result, columns = x.columns) # 표준화된 데이터를 데이터프레임으로 변환
x_scaled.head() # 표준화된 데이터 확인

# 훈련용, 검증용 데이터 분리 
X_train, X_test, y_train, y_test = train_test_split(x_scaled, y, test_size = 0.3, random_state = 123, stratify = y) # 훈련용 데이터와 검증용 데이터로 분리 

# 모델 생성
knn = KNC(n_neighbors = 7) # 최근접 이웃 개수 7개로 설정
knn.fit(X_train, y_train) # 모델 학습

# 모델 평가
knn.score(X_test, y_test) # 모델 평가 점수 반환

# 모델 활용
# 1명의 환자 예측
data = pd.DataFrame([[8,182,64,0,0,23,0.67,32]], columns = X_test.columns) # 예측할 데이터 생성
data = scaler.transform(data) # 예측할 데이터 표준화
patient = pd.DataFrame(data, columns = X_test.columns) # 예측할 데이터를 데이터프레임으로 변환

patient

pred = knn.predict(patient) # 예측 결과 반환
pred

pred = knn.predict(X_test) # 여러명의 환자 예측
pred
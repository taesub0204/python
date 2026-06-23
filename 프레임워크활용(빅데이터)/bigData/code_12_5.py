# 코드 12-5

# 회귀분석 과정
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression as LR
from sklearn.metrics import mean_absolute_error as MAE
import matplotlib.pyplot as plt

# 데이터 준비
df = pd.read_csv('d:/data/Prestige.csv')
df.head()

X = df[['education', 'women', 'prestige']]
y = df['income']

# 훈련용, 검증용 데이터 분리
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.3, random_state=123)

# 모델 생성
model = LR()
model.fit(X_train, y_train)

# 회귀식 확인
model.coef_
model.intercept_

#모델 평가 : MAE
pred = model.predict(X_test) # 예측 결과 반환
MAE(y_test, pred) # MMean Absolute Error 계산

plt.scatter(y_test, pred, color='red',) # 실제값과 예측값의 산점도
plt.xlim([5000,16000])
plt.ylim([5000,16000])
plt.xlabel('y_test') # x축 레이블
plt.ylabel('pred') # y축 레이블
plt.plot(y_test, y_test, color='blue') # 실제값과 예측값의 선 그래프
plt.show()


#모델활용
job = pd.DataFrame([[12, 0, 80]], columns=X.columns) # 예측할 데이터 생성

job
pred = model.predict(job) # 예측 결과 반환
pred
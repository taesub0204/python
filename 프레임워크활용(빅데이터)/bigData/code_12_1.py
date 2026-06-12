import pandas as pd
from sklearn.preprocessing import StandardScaler    
from sklearn.decomposition import PCA
import matplotlib.pyplot as plt

# 데이터 준비 
df = pd .read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/iris.csv')
df = df.drop('Species', axis = 1) # 범주형 변수 제거
df.head()

# 데이터 표준화
scaler = StandardScaler()   # 평균이 0, 분산이 1이 되도록 표준화하는 클래스
result = scaler.fit_transform(df) # fit_transform() : 표준화된 데이터 반환
df_scaled = pd.DataFrame(result, columns = df.columns) # 표준화된 데이터를 데이터프레임으로 변환
df_scaled.head() # 표준화된 데이터 확인



#차원 축소 
pca = PCA(n_components = 2) # 2차원으로 축소
transform = pca.fit_transform(df_scaled)# 차원 축소된 데이터 반환
transform = pd.DataFrame(transform) # 차원 축소된 데이터를 데이터프레임으로 변환
transform.head() # 차원 축소된 데이터 확인

#시각화
transform.plot.scatter(x = 0, y = 1, title = 'PCA plot') # 산점도

plt.show()
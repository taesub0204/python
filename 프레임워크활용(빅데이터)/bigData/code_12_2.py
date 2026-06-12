import pandas as pd 
from sklearn.decomposition import PCA
from sklearn.cluster import KMeans
from sklearn.preprocessing import StandardScaler
import matplotlib.pyplot as plt
import seaborn as sns

# 데이터 준비
df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/iris.csv')
df = df.drop('Species', axis = 1) # 범주형 변수 제거



# 데이터 표준화
scaler = StandardScaler()   # 평균이 0, 분산이 1이 되도록 표준화하는 클래스
result = scaler.fit_transform(df) # fit_transform() : 표준화된 데이터 반환
df_scaled = pd.DataFrame(result, columns = df.columns) # 표준화된 데이터를 데이터프레임으로 변환
df_scaled.head() # 표준화된 데이터 확인
df_scaled

# 군집화 
model = KMeans(n_clusters = 3, n_init = 10, random_state = 123) # 군집 개수 3개로 설정
model.fit(df_scaled)


# 군집화 결과 확인
model.cluster_centers_ # 군집 중심 좌표
model.labels_ # 각 행의 군집 번호
model.inertia_ # 군집 평가 점수

# 차원 축소 
pca = PCA(n_components = 2) # 2차원으로 축소
transform = pca.fit_transform(df_scaled)# 차원 축소된 데이터 반환
transform = pd.DataFrame(transform) # 차원 축소된 데이터를 데이터프레임으로 변환
transform['cluster'] = model.labels_ # 군집 번호 열 추가
transform.tail(50) # 차원 축소된 데이터 확인

# 시각화
sns.scatterplot(
    data = transform,
    x = 0, 
    y = 1,
    hue = 'cluster', # 군집 번호에 따라 색상 구분
    palette = 'Set2', # 색상 팔레트 설정
    legend = False # 범례 표시
)
plt.show()
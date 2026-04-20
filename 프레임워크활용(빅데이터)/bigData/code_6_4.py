import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

beers = [5,2,9,8,3,7,3,5,3,5]
bal = [0.1, 0.03, 0.19, 0.12, 0.04, 0.0095, 0.07, 0.06,0.02,0.05]

dict ={ 'beers': beers, 'bal': bal}

df = pd.DataFrame(dict)
df


df.plot.scatter(x='beers', y='bal',title = ' Beers ~ Blood ALcohol Level')
plt.show()


df.plot.scatter(x='beers', y='bal',title = ' Beers ~ Blood ALcohol Level')

m, b = np.polyfit(beers, bal, 1) # beers와 bal의 데이터를 이용해서 1차원 선형 회귀 모델을 학습, m은 기울기, b는 절편
plt.plot(beers, m*np.array(beers)+b)# beers에 대한 선형 회귀선을 그리기 위해서 beers의 값에 m을 곱하고 b를 더한 값을 계산해서 선으로 그리기
plt.show()

df['beers'].corr(df['bal']) # beers와 bal의 상관계수 계산, 1에 가까울수록 양의 상관관계, -1에 가까울수록 음의 상관관계, 0에 가까울수록 상관관계가 없음
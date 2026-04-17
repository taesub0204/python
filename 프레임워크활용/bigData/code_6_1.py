import pandas as pd
import matplotlib.pyplot as plt

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/mtcars.csv')
df

df.plot.scatter(x = 'wt', y ='mpg') # x축과 y축 데이터 지정
plt.show() # 기본적으로 점의 크기와 색깔이 지정되어 있음


df.plot.scatter(
                x = 'wt', y ='mpg', # x축과 y축 데이터 지정
                s = 50, # 점의 크기
                c = 'red', # 점의 색깔
                marker = '+' # 점의 모양

)
plt.show()


df.plot.scatter(
                x = 'wt', y ='mpg', # x축과 y축 데이터 지정
                s = 50, # 점의 크기
                c = 'orange', # 점의 색깔
                marker = '*' # 점의 모양

)
plt.show()
# 직선기준으로 점 이 몰릴 수록 양의 상관관계, 멀어질 수록 음의 상관관계, 직선에서 멀어질 수록 상관관계가 낮음

#6-2
vars = ['mpg', 'disp','drat', 'wt']
pd.plotting.scatter_matrix(df[vars])
plt.show()
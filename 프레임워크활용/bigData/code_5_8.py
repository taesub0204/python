# pylint: disable=missing-module-docstring
import pandas as pd   
import matplotlib.pyplot as plt


#데이터 준비
df = pd. read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/cars.csv')
dist = df['dist']
dist.describe()


#r구간 개수를 지정하여 히스토그램 그리기
dist.plot.hist() #기본 그래프
plt.show()

dist.plot.hist(bins = 6)# 막대 개수 지정
plt.show()

# 구간별 빈도수 계산
dist.value_counts(bins = 6, sort = False) # 구간별 빈도수 계산, sort = False는 구간 순서대로 정렬하겠다

dist.plot.hist(
                bins = 6,  # 막대 개수
                title = 'Braking distance',# 그래프 제목
                xlabel = 'distance',# x축 제목
                ylabel ='frequency',# y축 제목 횟수
                color = 'white',# 막대 색깔
                edgecolor = 'black' # 막대 테두리 색깔
                )

plt.show()

dist.describe()

dist.plot.box(title = 'Braking distance')
plt.show() # 이상치 확인 박스데이터
# 전처리 과정에서 버리던지 정상으로 바꿔주던지 해야할듯



df1 =pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')
df1.boxplot(column = 'Petal_Length', by = 'Species', grid=False) # 종별로 박스플롯 그리기, 판다스에서 제공하는 boxplot 메서드 사용
plt.suptitle('asdf') # r기본 표시 제목 제거 
plt.show()

df1.describe()

#5-11 s눈으로
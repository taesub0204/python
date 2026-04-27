import pandas as pd
import matplotlib.pyplot as plt


plt.rcParams['font.family'] = 'Malgun Gothic'
plt.rcParams['axes.unicode_minus'] = False  # 마이너스 기호 깨짐 방지

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/Test/data/user_behavior_dataset.csv')
df

#시리즈로 
models = pd.Series(df['Device_Model'])
usage_time = pd.Series(df['App_Usage_Time'])


#1-1
#도수분표표 
md = models.value_counts()
md

#1-2
rate = md/len(models)
print("모델별 사용비율\n", rate)


#1-3
plt.figure(figsize=(10,6)) #그래프 사이즈 조절
plt.subplot(1,2,1)#1행 2열 중 첫번째 그래프
md.plot(kind='bar', title = '모델별 사용자수') #
plt.subplot(1,2,2)# 1행 2열 중 두번째 그래프
md.plot.pie(autopct = '%.1f%%', title = '모델별 사용자 비율') #
plt.show()

#1-4
usage_time.mean()#평균
usage_time.median()#중앙값
usage_time.std()#표준편차
usage_time.quantile([0.25, 0.5, 0.75])#사분위수

#1-5
plt.figure(figsize=(12,5)) #그래프 사이즈 조절
plt.subplot(1,2,1)
plt.hist(usage_time, bins=6) #히스토그램
plt.subplot(1,2,2)
plt.boxplot(usage_time) #상자그림
plt.show()


#1-7
df.boxplot(column='App_Usage_Time', by='Operating_System', grid = False) #운영체제별 앱 사용시간 상자그림
plt.title('운영체제별 앱 사용시간') #
plt.show()

#2-1
df1 = df[['App_Usage_Time', 'Screen On Time (hours/day)', 'Battery Drain (mAh/day)', 'Number of Apps Installed', 'Data Usage (MB/day)', 'Age', 'Gender']]#
df1.columns = ['App_Usage_Time', 'Screen_On_Time', 'Battery_Drain', 'No_of_Apps', 'Data_Usage', 'Age', 'Gender']#

plt.scatter(df1['App_Usage_Time'], df1['No_of_Apps']) #
plt.xlabel('App Usage Time') #
plt.ylabel('Number of Apps Installed') #
plt.title('App Usage Time vs Number of Apps Installed')
plt.show()



#2-2
dict = {'Male':'blue', 'Female':'red'}
colors = df['Gender'].map(dict)
colors 
plt.scatter(df1['App_Usage_Time'], df1['No_of_Apps'], c=colors)#
plt.xlabel('App Usage Time')
plt.ylabel('Number of Apps Installed')
plt.title('App Usage Time vs Number of Apps Installed')
plt.show()



#2-3
pd.plotting.scatter_matrix(df1[['App_Usage_Time', 'Screen_On_Time', 'Battery_Drain', 'No_of_Apps']])
plt.suptitle('Scatter Matrix of User Behavior Dataset',fontsize = 16)
plt.show()

#2-4
corr = df1['App_Usage_Time'].corr(df1['No_of_Apps'])
print("상관계수:", corr)

#2-5
corrAll = df1.drop(columns=['Gender']).corr() 
print("상관계수 행렬:\n", corrAll) 

#3
df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/Test/data/students.csv')

df['초등학교'] = df['초등학교'].str.replace(',', '').astype(int) # 문자열에서 쉼표 제거 후 정수형으로 변환
df['중학교'] = df['중학교'].str.replace(',', '').astype(int) # 문자열에서 쉼표 제거 후 정수형으로 변환
df['고등학교'] = df['고등학교'].str.replace(',', '').astype(int) # 문자열에서 쉼표 제거 후 정수형으로 변환

plt.plot(df['연도'], df['초등학교'], label='초등', color='blue') # 연도별 초등학교 학생 수 그래프
plt.plot(df['연도'], df['중학교'], label='중학교', color='orange') # 연도별 중학교 학생 수 그래프
plt.plot(df['연도'], df['고등학교'], label='고등학교', color='green') # 연도별 고등학교 학생 수 그래프
plt.legend() # 범례 표시
plt.title('연도별 학생 수') #   그래프 제목
plt.xlabel('연도')
plt.ylabel('학생 수')
plt.show()


#4

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/Test/data/airquality.csv')
print(df.isnull().sum()) # 결측치 확인

#4-2
df2 = df.dropna() # 결측치 제거
print(df2.isnull().sum()) # 결측치 제거 후 확인 


#4-3
df4 = df.sort_values(by = 'Temp', ascending = False) # 온도 기준으로 내림차순 정렬
print(df4.head(10)) # 상위 10개 행 출력


#4-4
print(df2.groupby('Month')[['Wind', 'Temp']].max()) # 월별 최고 온도 계산

#5
df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/Test/data/mtcars.csv')

result = df.groupby(['gear','carb'])['mpg'].mean() # 기어 수와 카뷰레터 수에 따른 평균 마일리지 계산    
print(result)

#6
df1 = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/Test/data/iris.csv')

mean_petal_length = df1.groupby('Species')['Petal_Length'].mean() # 종별로 꽃잎의 평균 길이 계산
print(mean_petal_length)

#7

import matplotlib.pyplot as plt
# 리스트로 바로 그린 것 임
# 데이터
month = [1,2,3,4,5,6,7,8,9,10,11,12]#
late1 = [5,8,7,9,4,6,12,13,8,6,6,4]
late2 = [4,6,5,8,7,8,10,11,6,5,7,3]

# 그래프
plt.plot(month, late1, label='late1', color='blue') #
plt.plot(month, late2, label='late2', color='red') #

# 꾸미기
plt.xlabel('month')
plt.ylabel('late count') #
plt.title('Monthly Late Student Statistics')
plt.legend()
plt.grid()

plt.show()




# 데이터프레임으로 작성
df = pd.DataFrame({
    'month': [1,2,3,4,5,6,7,8,9,10,11,12],
    'late1': [5,8,7,9,4,6,12,13,8,6,6,4],
    'late2': [4,6,5,8,7,8,10,11,6,5,7,3]
})

plt.plot(df['month'], df['late1'], label='late1', color='blue')
plt.plot(df['month'], df['late2'], label='late2', color='red')
plt.xlabel('month')
plt.ylabel('late count')
plt.title('Monthly Late Student Statistics')
plt.legend()
plt.grid()
plt.show()

#8
df1 = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/Test/data/iris.csv')
## iris 데이터셋에서 품종(Species)별 꽃잎(Petal)의 길이를 상자 그림으로 작성하시오.
import matplotlib.pyplot as plt
df1.boxplot(column='Petal_Length', by='Species', grid=False) # 품종별 꽃잎 길이 상자 그림
plt.title('품종별 꽃잎 길이') # 그래프 제목
plt.suptitle('') # 기본 제목 제거
plt.xlabel('Species') # x축 레이블
plt.ylabel('Petal Length') # y축 레이블
plt.show()









#---------------------------------------------------------------------------------------------------------

# 아래 시험 작업 물 입니다.












#점유율 계산
md/models.size*100

#막대그래프 새로 출력
md.plot.bar(
    xlabel = 'model',
    ylabel = '점유율',
    rot = 0,
    title = '디바이스 별 점유율'

)

plt.show()
#원그래프 출력
md.plot.pie(
    ylabel = '',
    autopct = '1.0f%%',
    title ='디바이스별 점유율'
)

plt.show()

#usage_time 대해 평균, 중앙값 표준편차 사분위수를 출력하시오.
usage_time = pd.Series(df['App_Usage_Time'])
usage_time

usage_time.mean()
usage_time.median()
usage_time.std()
usage_time.quantile()


#usage_time 히스토 그램
usage_time.plot.hist(bins=6)
plt.show()

#상자그림
usage_time.plot.box(title = '사용자시간')
plt.show()


#상자그림 2개 넣기  잘모르것음
usage_time.plot.box(title = '사용자시간')
plt.show()

df
df.loc[0]
df.loc['App_Usage_Time']

#

df.plot.scatter(x='App_Usage_Time', y='Number of Apps Installed')
plt.show()











import matplotlib.pyplot as plt
students = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/Test/data/students.csv')


students

students = students[['연도','초등학교','중학교','고등학교']]
students['연도']

students['초등학교']

초등 = pd.Series(students['초등학교'])
초등

초등.plot(title ='초중고 연도별로 학생 수 그래프',
        xlabel= '연도',
        ylabel= '초등학교',
        linestyle = 'solid'
        
        
        )

plt.show()




# 지각생 통계를 선 그래프로 작성하시오.

plt.rcParams['font.family'] = 'Malgun Gothic'
plt.rcParams['axes.unicode_minus'] = False  # 마이너스 기호 깨짐 방지


late = pd.Series([5, 8, 7, 9, 4, 6, 12, 13, 8, 6, 6 ,4], index = list(range(1,13)))
late.plot(title = '월별 지각횟수', xlabel = '월', ylabel = '지각횟수', linestyle = 'solid')
plt.show()

late.plot(title = '월별 지각횟수', xlabel = '월', ylabel = '지각횟수', linestyle = 'dashdot', marker = '+')
plt.show()


#6-7

late1 = [5,8,7,9,4,6,12,13,8,6,6,4]
late2 = [4,6,5,8,7,8,10,11,6,5,7,3]

dict = {'class_1': late1, 'class_2': late2 }

late = pd.DataFrame(dict, index = list(range(1,13)) )
late

late.plot(title = 'Late student per month', xlabel = 'month', ylabel = 'frequency', marker = 'o')
plt.legend(loc = 'upper right')
plt.show()
# 8월에 지각이 왜 많을지 분석
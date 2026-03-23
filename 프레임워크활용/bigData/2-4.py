import pandas as pd

age = pd.Series([25,34,19,45])
print(age)
age.index = ['John','Jane','Tom', 'Luka']
print(age)

print(age.iloc[2])   # iloc 아로케이션 인덱스 번호 찾는다 0,1,2 값  19임
print(age.loc['Tom'])


print(age.iloc[3])   # iloc 아로케이션 인덱스 번호 찾는다 0,1,2 값  19임
print(age.loc['Tom'])

score = pd.DataFrame([
[85, 96, 40, 95],
[73, 69, 45, 80],
[78, 50, 60, 90]


])

print(score)
score.index = ['John','Jane','Tom'] #행방향인덱스
score.columns = ['kor','ENG','MATH','SCI']# 열 방향 인덱스
print(score)

print(score.iloc[2,1])# 인덱스 1행 2열의 값
print(score.iloc[1,2])
print(score.loc['Tom','ENG']) # 레이블에 의한 인덱싱





# 시리즈에 레이블  부여
age = pd.Series([25,34,19,45,60])
print(age)
age
age.index = ['John','Jane','Tom', 'Luka','Tom'] # 행에 레이블 부여
print(age)

age.iloc[2]
age.loc['Tom']




population= pd.Series([523,675,690,720,800])
population
population.index = [2021, 2022, 2023, 2024, 2025]
population

population.iloc[1]
# population.iloc[20]
population.loc[2023]
#population.iloc[2023] 아이로케이션은 레이블 참조가 아님



import pandas as pd

list_data =([
[-0.1, 0.0, -0.1, -0.2],
[1.8, 2.0, 1.6, 1.6],
[6.4, 6.8, 5.8, 5.9],
[12.3, 12.9, 11.5, 11.5],
[17.9, 18.5, 17.1, 17.1],
[22.2, 22.8, 21.6, 21.5]
])

# list_data ={
# '전북': [-0.1, 0.0, -0.1, -0.2],
# '전주':[1.8, 2.0, 1.6, 1.6],
# '군산':[6.4, 6.8, 5.8, 5.9],
# '부안'[12.3, 12.9, 11.5, 11.5],
# [17.9, 18.5, 17.1, 17.1],
# [22.2, 22.8, 21.6, 21.5]
# }

data = pd.DataFrame(list_data)
data.index = ['1월','2월','3월','4월','5월','6월']
data.columns = ['전북','전주','군산','부안']
data

data.iloc[2,1]
data.loc['3월','전주']
data.iloc[3,3]
data.loc['4월', '부안']
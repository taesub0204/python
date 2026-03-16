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
score.index = ['John','Jane','Tom']
score.columns = ['kor','ENG','MATH','SCI']
print(score)

print(score.iloc[2,1])
print(score.loc['Tom','ENG'])






age = pd.Series([25,34,19,45])
print(age)
age.index = ['John','Jane','Tom', 'Luka']




population= pd.Series([523,675,690,720,800])
population
population.index = [2021, 2022, 2023, 2024, 2025]
population
population.iloc[2]
population.loc[2023]
#population.iloc[2023] 아이로케이션은 레이블 참조가 아님
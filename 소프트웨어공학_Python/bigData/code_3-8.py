import pandas as pd



salary = pd.Series([20, 15, 18, 30]) # 레이블 인덱스가 없는 시리즈 객체
score = pd.Series([75, 80, 90, 60], index=['KOR', 'ENG', 'MATH', 'SOC'])

salary
score

# 값의 변경
score.iloc[0] = 85 # 인덱스 0 의 값을 변경
score
score.loc['SOC'] = 65 # 인덱스 'SOC' 의 값을 변경
score
score.loc[['ENG','MATH']] = [70,80] # 인덱스 'ENG','MATH' 의 값을 변경
score


# 3-9

score.loc['PHY'] = 50 # 없는 인덱스 추가 맨뒤에 추가됨
score #맨뒤에 추가됨
score.iloc[5] = 90 # 에러 발생 레이블이 없음 절대적 위치로 참조 안됨
# 값의 추가 (레이블 인덱스가 없는 경우)
salary
next_idx = salary.size #4개
next_idx

salary.iloc[next_idx] = 33 # 에러 발생
salary.loc[next_idx] = 33 # 정상 수행
salary.loc[10] = 39
salary.loc[12] = 49
salary
salary1 = salary.reset_index(drop=True) # 인덱스 초기화
salary1

# pandas에서는 append 메서드 없어짐 리스트에서 쓸 수 있음
# _append() 메서드를 이용한 값의 추가

salary1 = pd.concat([salary,pd.Series([55])], ignore_index = True) # 시리즈 객체 연결하여 값추가 55 맨뒤로 감
salary1

salary1 = pd.concat([salary[:4], pd.Series([100]), salary[4:]], ignore_index = True) # 시리즈 객체 연결하여 값추가
salary1



# append 책 설명 버려야 함 
# New = pd.Series({'MUS': 95})
# score._append(new) # score 변경 없음
# score
# score = score._append(new) # score 변경됨
# score
# salary._append(pd.Series([66]), ignore_index=True)
# salary = salary._append(pd.Series([66]), ignore_index=True)
# salary


# 코드3-8에 이어서 실행
# 값의 삭제
# salary = pd.Series([20, 15, 18, 30]) # 레이블 인덱스가 없는 시리즈 객체
# score = pd.Series([75, 80, 90, 60],
# index=['KOR', 'ENG', 'MATH', 'SOC'])





score
score.drop('PHY', inplace=True) # 레이블 인덱스가 있는 경우 바로 즉각 반영
score # score의 내용 변동 없음
score = score.drop('PHY') # score 밑에 까지 실행야 변화가 일어남
score # score의 내용 변경



salary = salary.drop(1) # 레이블 인덱스가 없는 경우 1번 인덱스 삭제 됨
salary

salary = salary.drop([3,5]) # 레이블 인덱스가 없는 경우 1,3,5번 인덱스 삭제 됨
salary

# print(salary)  shift + enter   한줄 씩 가능, print(salary) 함수가 들어가면 전체 실행해야 되서 shift + enter      
# print(salary) shift+ enter 이후 전체 실행하면 안됨 그럴 땐 아까 쓰레기통 지우고 다시 터미널 생성하면댐


## 여기서 시리즈 끝남

## 판다스 2차원 datafram해야댐
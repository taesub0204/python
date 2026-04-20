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

################################################################

# append()메서드 사용 불가 

new = pd.Series({'MUS':95})
score = pd.concat([score, new])# 리스트 화 해서 붙여라  inplace 안되는 상황이다 새로 ㄷ입해주면댐
score

new1 = pd.Series({'ART':100})
score = pd.concat([score[:2], new1, score[2:]])
score

new0 = pd.Series({'SCI':88})
score = pd.concat([new0,score[:]]) # concat 집어 넣기 맨두 중간 맨앞 
score



score.iloc[8] = 90 # 없는 걸 넣을 수 없고 concat을 써서 집어 넣을 수 있음 해당 공간이 없어 에러 발생

salary

next_idx = salary.size
next_idx
salary.iloc[4] = 33 # 에러발생
salary.loc[next_idx] = 33 # 인덱스가 있는 상황에서도 레이블 값으로 판단해서 값을 넣어줌
salary.loc[100] = 33
salary

salary.reset_index(drop=True, inplace=True)# 인덱스 초기화
salary


score.drop('PHY', inplace=True)
score
score = score.drop('PHY')
score

salary = salary.drop(1)
salary

salary.drop(2, inplace=True) #자체 변화를 위해서 inplace True
salary



score1 = score  # score1 과 score와 같고 같이 반영됨 같은집
score1.loc['KOR'] = 95
score1
score

score3 = score1.copy() # 이렇게 쓰면 독립된 게체 다른 집
score3.loc['KOR'] = 70 
score3
score1

## 연습문제 P119
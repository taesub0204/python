import pandas as pd   #판다스 쓰기 위해서 improt pandas 넣고 짧게 pd 별칭
#직접리스트입력

#2차월 리스트로부터 데이터 프레임 생성
score = pd.DataFrame([
[85,96,40,95],
[73,96,45,80],
[78,80,60,90]


])


print(score)#score 내용 출력
print(type(score))#score 자료형 출력

print(score.index)# 행 방향 인덱스
print(score.columns)# 열 방향 인덱스 

print(score.iloc[1,2])
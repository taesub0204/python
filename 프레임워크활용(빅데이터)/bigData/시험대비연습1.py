# blood 변수에 혈액형 시리즈형태로
# A, B, O, AB, A, B, O, AB

import pandas as pd
blood = pd.Series(['A','B','AB','O'])
blood

# print 문을 이용해서 혈액형이 B를 출력
print(blood[1])
#아래 제시된 2차월 배열을 데이터프레임 df 저장하시오.
data =[[85,96],[14,12]]

df = pd.DataFrame(data)
df
# df의 2행 2열에 있는 값을 출력하시오.
print(df.iloc[1,1])

score = pd.DataFrame([[85, 96, 40, 95],
                      [72,69,45,80],
                      [88,90,50,92],
                      [78,82,60,85]],
                    index = ['영희','철수','민수'],
                    columns = ['국어','영어','수학','과학']

                  )
# 학생 이름 출력
print(score.index)
print(score.columns)
# 절대 위치 인덱스 기준 0행 2열
print(score.iloc[0,2])
score
print(score.iloc[1,1])
# 철수의 영어성적
print(score.loc['철수','영어'])

print(score.iloc[1,'영어']) # 오류 발생
print(score.loc['민수',2]) # 오류 발생

sales =pd.Series([781,650,705,406,580,450,550,640],
                 index = ['A','B','C','D','E','F','G','H']
                 
                 )
sales
#sales 배열의 길이
print(len(sales))
print(sales.iloc[1]) # A의 매출액

# f팀의 매출액 출력
print(sales.loc['F'])
#인덱스 기준 3~5까지의 매출액 출력
print(sales.iloc[3:6])
# a,b, D팀의 매출액 출력
print(sales.loc[['A','B','D']])

# 매출액이 600 이상인 팀의 매출액 출력
print(sales.loc[sales >= 600])
print(sales.iloc[sales >= 600]) #iloc는 위치기반이므로 오류 발생
#a매출액이 500 미만이거나 700 초과하는 팀들의 이름 매출액 출력
print(sales.loc[(sales < 500) | (sales > 700)]) #괄호 주의
#B팀 보다 매출액이 많은 팀 들의 이름과 매출액을 출력하시오
print(sales.loc[sales > sales.loc['B']])

print(sales.loc[sales < 600 ])

#각 팀의 매출액에 20% 증가한 매출액을 출력하시오
print(sales * 1.2)
print(sales.loc[sales < 600 ]* 1.2) #600 미만인 팀의 매출액에 20% 증가한 매출액을 출력하시오
# 8개팀의 매출평균, 합계, 표준편차
print(sales.mean()) #평균
print(sales.sum()) #합계
print(sales.std()) #표준편차
#평균
print(sales.median()) # 중앙값

# H팀의 매출액이 800으로 변경
sales.loc['H'] = 800
print(sales)

sales.loc['A'] = 810
sales.loc['C'] = 820
print(sales)
#신생팀 j의 매출액이 500으로 추가
sales.loc['J'] = 500
print(sales)

#j팀의 매출 정보 삭제   
sales.drop('J', inplace = True)
print(sales)

# sales의 내용을 sales2로 복사한 후 sales2의 매출액을 500씩 변경하시오 내용비교
sales2 = sales.copy()
sales2 += 500
print(sales2)

sales
sales2
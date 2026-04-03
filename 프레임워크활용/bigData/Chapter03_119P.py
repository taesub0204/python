import pandas as pd
import numpy as np

##  1번 매출 실적 데이터를 판다스 시리즈 salses에 저장하고,  팀 이름을 인덱스로 설정한 후 내용을 출력하시오.
sales = pd.Series([781, 650, 705, 406, 580, 450, 550, 640]) ##시리즈의 특징 시리즈의 번호가 출력됨 세로로 출력 시리즈 특징
sales


sales.index =  ['A','B','C','D','E','F','G','H']
sales

##  2번 sales 의 배열 길이 (값의 개수)를 출력하시오.
sales_length = len(sales) 
sales_length

## 3번 인덱스 기준 2번째 팀의 매출액을 출력하시오
sales.iloc[1]


## 4번 F팀의 매출액을 출력하시오.
sales.loc['F']

## 5번 인덱스 기준 3~5번째 팀의 매출액을 출력하시오.
sales.iloc[2:5]

## 6번 A, B, D 팀의 매출액을 출력하시오.
sales.loc[['A','B','D']]

## 7번 매출이 600 이상인 팀의 이름과 매출을 출력하시오
sales.loc[sales >= 600]

## 8번 매출이 500 미만 이거나 700 을 초과하는 팀들의 이름과 매출액을 출력하시오
sales.loc[(sales < 500) | (sales > 700)]

## 9번 B팀보다 매출액이 많은 팀들의 이름과 매출액을 출력하시오.
sales.loc[sales > sales.loc['B']]

## 10번 매출액이 600미만인 팀들의 이름을 출력하시오. 
sales.loc[sales < 600].index

## 11번 각 팀의 매출액이 20% 증가한 매출액을 출력하시오.
sales * 1.2

sales

## 12번 매출이 600 미만인 팀들의 매출액만 20% 올린 결과를 출력하시오
sales.loc[sales < 600] * 1.2

sales

## 13번 8개 팀의 매출액 평균, 합계, 표준편차를 출력하시오.
sales.mean() # 평균
sales.sum() # 합계
sales.std() # 표준편차

## 14번 H 팀의 매출액 800으로 변경하고, 변경여부 확인하시오
sales.loc['H'] = 800
sales

## 15번 A팀과 C임의 매출액을 각각 810, 820으로 변경하고, 변경여부 확인하시오
sales.loc[['A','C']] = [810, 820]
sales

## 16번 신생팀 J의 매출액 400을 추가하고, 추가여부 확인하시오
sales.loc['J'] = 400
sales

## 17 J팀의 매출액을 삭제하고, 삭제여부 확인하시오  
sales.drop('J', inplace=True)
sales

## 18번 sales의 내용을 slase2에 복사한 후 , sales2의 매출액을 500씩 올리고 sales와 sales2내용을 비교하시오
sales2 = sales.copy()
sales2 + 500
sales

import pandas as pd
score = pd.Series([30,20,40, pd.NA, 30, pd.NA])
score
score.sum() # na가 0으로 간주되어 계산
score.mean() # 결측값을 제외한 평균 계산
score +5 # 전체 데이터에 5를 더하되, NA는 NA로 유지

score

pd.isna(score) # na이냐??  is na  결측일 경우 True, 결측이 아닐 경우 False
pd.isna(score).sum() # NA 개수 확인

score.size # 결측 포함 데이터 개수
score.count() # 결측 제외 데이터 개수

pd.notna(score) # is na 반대 not na 결측이 아닐 경우 True, 결측일 경우 False
pd.notna(score).sum() # 결측값 아닌거 개수 확인

score = score.dropna() # 결측값 제거    
score
score = score.reset_index(drop = True) # 인덱스 재설정
score
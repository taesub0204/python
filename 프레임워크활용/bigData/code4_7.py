import pandas as pd

df = pd.read_csv('iris.csv')

# 코드 4-1에 이어서 실행 #################
# code_4

# 데이터프레임 객체에 있는 값의 수정 
df3 = df.copy() #df를 df3로 복사 깊은복사, 서로 영향을 주지 않는 복사 
#df3 = df  얕은 복사,, 서로 영향을 주는 복사
 
df3
df3.iloc[1, 2] = 100 #1행 2열의 값 수정 
df3
df

df3.loc[1, 'petal_Length'] = 500
df3

df3.Petal_Length.to_list()     #시리즈를 리스트 형태로 출력 
df3.loc[df.Petal_Length > 6.5, 'Petal_Length'] *= 100
df3.petal_Length.to_list()     #시리즈를 리스트 형태로 출력 

#code 4_8
#ep
df3
new_idx = df3.shape[0]  # 끝번호 150 
df3
df3.loc[new_idx] = [1.1, 4.5, 3.4, 2.2, 'setosa'] # 150 튜플 입력 됨
df3
df3.tail()


new_row = pd.DataFrame([[100, 2.2, 3.3, 4.4, 'setosa']],columns=df3.columns)
new_row
df3 = pd.concat([df3.iloc[:150], new_row, df3.iloc[150:]], ignore_index=False)
df3.reset_index(inplace=True)
df3

new_col = df3.Petal_Length * 10
df3['new_col'] = new_col
df3

df4 = df.copy()
df4.insert(loc = 2, column='new_col12', value = new_col)
df4


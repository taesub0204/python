import pandas as pd  

df = pd.read_csv('iris.csv', header=None)  # csv 파일 읽기
# df = pd.read_csv('iris.csv', header=None) 
df # 내용 확인

# df1 = df.drop('Species',axis=1)
# df1
# df1.to_csv('iris2.csv') # Species 열 삭제됨  csv로 파일로 저장

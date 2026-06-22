# glob으로 파일 목록 찾기 

import glob

# 1) data 폴더의 모든 csv 파일 경로 찾기
files = glob.glob('data/*.csv')


print("===== data 폴더의 모든 csv 파일 =====")
print(files)
print("파일 개수", len(files))

# 2) 패턴으로 파일 찾기 : 2026년 3월 로그 파일
files = glob.glob('data/log_2026-03-*.csv')
print("\n===== data 폴더의 2026년 3월 로그 csv 파일 =====")
print(files)
print("3월 로그 파일 개수", len(files))

# 3) 하위 폴더까지 모든 CSV 파일 찾기
files = glob.glob('data/**/*.csv', recursive=True)

print("\n===== data 폴더와 하위 폴더의 모든 csv 파일 =====")
print(files)
print("전체 CSV 파일 개수", len(files))


import glob, pandas as pd

files = glob.glob('data/log_2026-03-*.csv')
# 각 파일을 읽어 리스트 담고 한번에 합치기 
df_list=[] 
for f in files:
    temp = pd.read_csv(f)
    temp['source'] = f # 오느 파일에서 왔는지 기록
    df_list.append(temp)

merged = pd.concat(df_list, ignore_index=True)
print(merged.shape)

print("\n 앞 5행 확인")
print(merged.head())

print("\n source 컬럼 값 확인")
print(merged[['time', 'temp', 'source']].head(10))

print("\n 어떤 파일들이 합쳐졌는지 확인")
print(merged['source'].unique())

print("\n 각 파일별 행 개수")
print(merged['source'].value_counts().sort_index())
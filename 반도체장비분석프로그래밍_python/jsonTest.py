import glob
import json

# 1) JSON 파일 읽기 > 파이썬 딕셔너리
with open('device.json', encoding='utf-8') as f:
    data = json.load(f)

print(data['sensors']['temp'])

# 2) 딕셔너리 > JSON 파일쓰기
with open('out.json','w', encoding='utf-8') as f:
    json.dump(data, f, ensure_ascii=False, indent=2)

print("out.json 저장 완료")

#장비 여러 대의 기록이 리스트 담긴 JSON

records = [
    {"device": "ETCH-01", "temp": 450.2},
    {"device": "ETCH-02", "temp": 448.9},
  

]

import pandas as pd

# 리스트 of 딕셔너리 > DataFrame (가장 쉬운 경우)
df = pd.DataFrame(records)

# 중첩이 있으면 json_normalize()로 평탄화
df = pd.json_normalize(records)

print(df)


import json
import pandas as pd



# 1) json 파일 읽기
with open('out.json', encoding='utf-8') as f:
    data = json.load(f)

# 2)Json 데이터를 리스트에 담기
#pd.DataFrame은 리스트 안에 딕셔너리가 있는 구조를 가장 쉽게 처리함
records = [data]
# 3) 리스트 of 딕셔너리 > DataFrame
df = pd.DataFrame(records)
print("========= pd.DataFrame결과 =========")
print(df)
# 4) 중첩 구조 평탄화
df = pd.json_normalize(records)
print("\n========= pd.json_normalize결과 =========")
print(df)


# glob으로 파일 목록 찾기 

import glob

# data 폴더의 모든 csv 파일 경로 찾기
files = glob.glob('data/*.csv')
print(files)

# 패턴으로 특정 파일만 (2026년 3월 로그)
files = glob.glob('data/equip_log_2026-03*.csv')

# 하위 폴더까지 (** 와 recursive=True)
files = glob.glob('data/**/*.csv', recursive=True)

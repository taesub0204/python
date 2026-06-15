import pandas as pd

# 1) 특정 시트 읽기
df_temp = pd.read_excel("equip.xlsx", sheet_name = "온도")
print("===== 온도 데이터 =====")
print(df_temp.head())

# 2) 시트 이름 목록 확인
xls = pd.ExcelFile("equip.xlsx")

print("\n===== 시트 이름 =====")
print(xls.sheet_names)

# 3) 특정 시트 하나 더 읽기
df_pressure = pd.read_excel("equip.xlsx", sheet_name = "압력")

print("\n===== 압력 시트 데이터 =====")
print(df_pressure.head())




# 4) 모든 시트를 한 번에 읽기
sheets = pd.read_excel("equip.xlsx", sheet_name = None)

print("\n===== 모든 시트 데이터 =====")
print(sheets.keys()) # 시트 이름 목록

print("\n===== 압력 시트 데이터 =====")
print(sheets['압력'].head())

print("\n===== 요약 시트 데이터 =====")
print(sheets['요약'].head())


import json

with open('device.json', encoding='utf-8') as f:
    data = json.load(f)
print(data['sensors']['temp'])

with open('out.json','w', encoding='utf-8') as f:
    json.dump(data, f, ensure_ascii=False, indent=2)
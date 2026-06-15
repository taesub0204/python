# 260615huge.py
# 대용량 CSV 생성 + 나눠 읽기 실습
from pathlib import Path
import pandas as pd
import numpy as np

# 1. 저장 위치 설정: 현재 파이썬 파일이 있는 폴더 기준
base = Path(".")
csv_path = base / "huge.csv"

# 2. 10만 행 장비 데이터 생성
np.random.seed(42)
n = 100_000

df = pd.DataFrame({
    "time": pd.date_range("2026-03-01", periods=n, freq="s"),
    "temp": np.random.normal(450, 5, n),
    "pressure": np.random.normal(1.0, 0.05, n),
    "vibration": np.random.normal(0.3, 0.1, n)
})

# 3. huge.csv 저장
df.to_csv(csv_path, index=False, encoding="utf-8-sig")

print("huge.csv 생성 완료")
print("저장 경로:", csv_path.resolve())
print("데이터 크기:", df.shape)
print(df.head())

print("\n" + "=" * 50)
print("1. huge.csv를 1만 행씩 나눠 읽기")
print("=" * 50)

# 4. chunksize로 나눠 읽기
total_temp = 0
row_count = 0
chunk_count = 0

for chunk in pd.read_csv(csv_path, chunksize=10000):
    chunk_count += 1
    row_count += len(chunk)
    total_temp += chunk["temp"].sum()

    print(f"{chunk_count}번째 chunk 처리 완료 / 행 개수: {len(chunk)}")

print("전체 chunk 개수:", chunk_count)
print("전체 행 개수:", row_count)
print("전체 온도 합:", total_temp)
print("전체 온도 평균:", total_temp / row_count)

print("\n" + "=" * 50)
print("2. dtype을 지정해서 CSV 읽기")
print("=" * 50)

# 5. 자료형을 미리 지정해서 메모리 절약
df_small = pd.read_csv(
    csv_path,
    dtype={
        "temp": "float32",
        "pressure": "float32",
        "vibration": "float32"
    }
)

print(df_small.info())
print(df_small.head())
print("전체 행 개수:", len(df_small))
print("온도 평균:", df_small["temp"].mean())
print("압력 평균:", df_small["pressure"].mean())
print("진동 평균:", df_small["vibration"].mean())

print("\n실습 완료")
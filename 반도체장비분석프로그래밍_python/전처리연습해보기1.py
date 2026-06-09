import pandas as pd
import numpy as np

# 데이터 10개만 생성
data = {
    'order_id': [1, 2, 2, 4, 5, 6, 7, 8, 9, 10],            # 2번 중복
    'customer_id': ['C1', 'C2', 'C2', np.nan, 'C5', 'C6', 'C7', 'C8', 'C9', 'C10'], # 결측치 포함
    'price': [1000, 2000, 2000, 1500, 99999, 3000, 4000, 5000, 6000, 7000] # 99999는 이상치
}

df = pd.DataFrame(data)
print("--- 원본 데이터 ---")
print(df)
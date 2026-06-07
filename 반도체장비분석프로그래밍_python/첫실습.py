import pandas as pd
import numpy as np

np.random.seed(42) #결과 재현용
n = 100
df = pd.DataFrame({
    'time':pd.date_range('2026-03-01',periods=n, freq ='s'),
    'temp':np.random.normal(450,5,n), #온도 ~450도
    'pressure':np.random.normal(1.0,0.05,n) # 압력 ~1.0Pa

})
df
print(df.head())

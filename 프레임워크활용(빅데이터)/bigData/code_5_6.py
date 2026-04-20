import pandas as pd
import numpy as np  

# np1 = np.arange(0, 10,2)
# np1



# 사분위수 계산

mydata = [60,62,64,65,68,69,120]
mydata = pd.Series(mydata)
mydata.quantile(0.25) # 25% 지점의 값
mydata.quantile(0.5) # 50% 지점의 값, 중앙값
mydata.quantile(0.75)# 75% 지점의 값
mydata.quantile([0.25, 0.5, 0.75])

mydata.quantile(np.arange(0,1.1,0.1))
mydata.describe()

mydata.var() # 분산
mydata.std() # 표준편차 표준편차가 크면 평균으로 부터 멀리 떨어져 있따....
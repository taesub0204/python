# pylint: disable=missing-module-docstring
# pylint: disable=unused-import

import pandas as pd
import numpy as np  

# np1 = np.arange(0, 10,2)
# np1



# 사분위수 계산


mydata = pd.Series([60,62,64,65,68,69,72])
mydata1 = pd.Series([20,30,64,65,98,90,100])

mydata.var() # 분산
mydata1.var() # 분산
mydata.std() # 표준편차 표준편차가 크면 평균으로 부터 멀리 떨어져 있따....
mydata1.std() # 표준편차 표준편차가 크면 평균으로
mydata.mean() # 평균
mydata1.mean() # 평균


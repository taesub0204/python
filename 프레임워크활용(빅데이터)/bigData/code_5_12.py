# pylint: disable=missing-module-docstring
# pylint: disable=unused-import

# 5-13

import matplotlib.pyplot as plt
import numpy as np

plt.rc('font', family='Malgun Gothic')
#plt.rc('axes', unicode_minus=False)

np.linspace(0, 100, 100) # 0부터 100까지 100개의 점을 생성, 0과 100도 포함됨, 간격은 1

# 0~2π 범위 생성

x = np.linspace(0, 2 * np.pi, 100) # 파이란 원주율 원에 둘레를 지름으로 나눈값, 원의 둘레는 2πr, 지름은 2r, 둘레/지름 = 2πr/2r = π, 0부터 2π까지 100개의 점을 생성
x
plt.figure(figsize=(8, 5))

# 동일 x 범위에서 두 함수를 그리면 직접 비교 가능

plt.plot(x, np.sin(x), label="sin(x)", linewidth=2)
plt.plot(x, np.cos(x), label="cos(x)", linewidth=2)

plt.title("sin(x)와 cos(x) 비교 그래프")
plt.xlabel("x (라디안)")
plt.ylabel("함수 값")
plt.legend() # 그래프 구분을 위한 범례
plt.grid(True)
plt.show()





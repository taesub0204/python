


import numpy as np

data = np.array([
    [80, 85, 90, 75],
    [95, 100, 88, 92],
    [70, 60, 80, 65]


])

print("-- 전체 성적표 --")

print("1. 과목별 평균(세로)", data.mean(axis = 0))
print("2. 학생별 최고점(가로)", data.max(axis = 1))
print("3. 전체 점수 표준편차:", data.std())  # 표준편차 

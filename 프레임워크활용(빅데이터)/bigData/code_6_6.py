import pandas as pd
import matplotlib.pyplot as plt

plt.rcParams['font.family'] = 'Malgun Gothic'
plt.rcParams['axes.unicode_minus'] = False  # 마이너스 기호 깨짐 방지


late = pd.Series([5, 8, 7, 9, 4, 6, 12, 13, 8, 6, 6 ,4], index = list(range(1,13)))
late.plot(title = '월별 지각횟수', xlabel = '월', ylabel = '지각횟수', linestyle = 'solid')
plt.show()

late.plot(title = '월별 지각횟수', xlabel = '월', ylabel = '지각횟수', linestyle = 'dashdot', marker = '+')
plt.show()


#6-7

late1 = [5,8,7,9,4,6,12,13,8,6,6,4]
late2 = [4,6,5,8,7,8,10,11,6,5,7,3]

dict = {'class_1': late1, 'class_2': late2 }

late = pd.DataFrame(dict, index = list(range(1,13)) )
late

late.plot(title = 'Late student per month', xlabel = 'month', ylabel = 'frequency', marker = 'o')
plt.legend(loc = 'upper right')
plt.show()
# 8월에 지각이 왜 많을지 분석
import pandas as pd
import matplotlib.pyplot as plt
plt.rcParams['font.sans-serif'] = 'Malgun Gothic'  #한글 폰트 설정
plt.rcParams[plt.axes.unicode_minus] = False  #마이너스 폰트 설정

hobby = pd.Series(['등산', '낚시', '독서', '영화 감상', '요리', '낚시', '등산', '독서', '영화 감상', '요리'])
import pandas as pd
import matplotlib.pyplot as plt

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용/bigData/data/iris.csv')
df


dict = {'setosa':'red', 'versicolor':'green', 'virginica':'blue'}
# colors = list(dict[key] for key in df.Species)
# colors


# df.plot.scatter(x = 'Petal_Length', y = 'Petal_Width', s=50, c = colors, marker = 'o')
# plt.show()
# df.plot.scatter(x = 'Petal_Length', y = 'Petal_Width', s=50,  marker = 'o')
# plt.show()

for name, group in df.groupby('Species'):
    plt.scatter(group['Petal_Length'], group['Petal_Width'],c = dict[name], s=30, marker='o', label=name) #plt.scatter은 산점도 그리는 함수, groupby로 종별로 그룹화해서 각 그룹마다 산점도 그리기, c는 색깔, s는 점의 크기, marker는 점의 모양, label은 범례에 표시될 이름


plt.xlabel('Petal Length')
plt.ylabel('Petal Width')
plt.title('Petal Length vs Petal Width')
plt.legend()
plt.show()
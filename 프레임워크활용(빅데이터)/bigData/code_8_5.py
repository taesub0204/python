import matplotlib.pyplot as plt
import seaborn as sns
from statsmodels.graphics.mosaicplot import mosaic

plt.rcParams['font.family'] = 'malgun Gothic' #한글 폰트 설정 
plt.rcParams['axes.unicode_minus'] = False # 마이너스 부호 깨짐 방지

#데이터 준비
df = sns.load_dataset('titanic')
df.head()
dict1 = {0:'사망', 1: '생존'}
dict2 = {'male' : '남성', 'female': '여성'}
df = df.replace({'survived': dict1})
df = df.replace({'sex': dict2})
df.head()

def props(key):
    return {'color' :  'lightgreen' if '생존' in key else 'yellow'}

mosaic( data = df.sort_values('sex'),
        index = ['sex', 'survived'],
        properties=props,
        axes_label=True,
        title='타이타닉 남녀 생존비율'
        )
plt.show()

# 모자익 범주형 
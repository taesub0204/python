import matplotlib.image as mpimg
import plotly.graph_objects as go
import pandas as pd
import matplotlib.pyplot as plt

def radar(df, fills, min_max, title=''):
    fig = go.Figure()
    categories = df.columns.to_list()
    categories.append(categories[0])
    i = 0
    while (i < len(df)):
        scores = df.iloc[i, :].to_list()
        scores.append(scores[0])
        fig.add_trace(go.Scatterpolar(
            r=scores,  # 축의 값
            theta=categories,  # 축의 레이블
            fill=fills[i],  # 다각형 채우기 색
            name=df.index[i]  # 다각형 레이블
        ))
        i += 1
        
        fig.update_layout(
            polar_radialaxis_visible=True, # 숫자 눈끔 보이게
            polar_radialaxis_range=min_max,  # 축의 값 범위
            showlegend=True,
            margin_t=50,  # 상단 여백
            margin_l=100,  # 좌측 여백
            margin_r=100,  # 우측 여백
            margin_b=25,  # 하단 여백
            width=700,  # 그래프의 폭(pixel)
            height=700,  # 그래프의 높이(pixel)
            title_text=title,  # 그래프 제목
            title_font_size=30,  # 제목 폰트 사이즈
            font_size=20  # 폰트 사이즈
            )
        
 
    plt.axis('off') 
    fig.write_image('rader.png')
    plt.imshow(mpimg.imread('rader.png'))
    plt.show()






#####################################################################################
    #데이터 입력

df = pd.DataFrame({
        'Kor': [72, 70, 90, 60, 66],
        'Eng': [84,85,95,70,85],
        'Math': [71,40, 88, 80, 75],
        'Sci': [83,80, 91, 90, 70],
        'Phy': [60,60,60,70,50]



})

df.index = ['AVG', 'John', 'Tom', 'Smith', 'Grace']
print(df)

fills = [None, 'toself']
radar(df =df.iloc[[0,3],:],
        fills=fills,
          min_max=[0,100],
          title='Scores of Smith')
    

fills = [None, 'toself', 'toself', 'toself', 'toself']
radar(df = df.iloc[1:,:],
         fills=fills,
         min_max=[0,100],
         title='Scores of Students'
          
          )

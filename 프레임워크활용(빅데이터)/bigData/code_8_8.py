import plotly.express as px
import webbrowser
import matplotlib.pyplot as plt
import pandas as pd

df = pd.read_csv('C:/Users/user/Desktop/taesub/data/GNI2014.csv')
df.head()

fig = px.treemap(data_frame=df,
                 path=['continent', 'iso3'],  # 데이터의 계층 구조
                 values='population',  # 타일 면적 기준 컬럼
                 color='GNI',  # 색 온도 기준 컬럼
                 color_continuous_scale='Bluyl'  # 컬러 팔레트
                 )

plt.axis('off')  # 축 눈금 제거
fig.update_layout(margin_t=50, margin_l=25,  # 여백 설정
                  margin_r=25, margin_b=25,
                  width=800,  # 그래프의 폭(pixel)
                  height=600,  # 그래프의 높이(pixel)
                  title_text='GNI 2014',  # 그래프 제목
                  title_font_size=20  # 제목 폰트 크기
                  )

# 그래프 저장 & 화면에 표시하기
fig.write_html('d:/treemap.html')  # html 파일로 결과 저장
webbrowser.open('d:/treemap.html')  # 웹 브라우저에서 그래프 확인

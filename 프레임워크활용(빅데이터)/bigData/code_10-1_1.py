import pandas as pd
import matplotlib.pyplot as plt
from wordcloud import WordCloud
import konlpy #자바 문제가 있다면 업데이트 해야댐

# (1) 데이터 준비 

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/movie1_review.csv')
df

# (2) 형태소 분석기 정의
kkma = konlpy.tag.Kkma()

# (3) 단어 데이터프레임 만들기 
nouns = df['Review'].apply(kkma.nouns)
nouns

nouns = nouns.explode() # 리스트 형태의 단어들을 행으로 분리하기
nouns

# (4) 전처리 실시 
# 모시  티모시 , imax > 아이맥스,....
nouns[nouns =='모시'] = '티모시'
nouns[nouns =='IMAX'] = '아이맥스'
nouns[nouns =='파트3'] = '3편'


# 글자 수 2개 이상이 단어만 추출
df_word = pd.DataFrame({'word':nouns})
df_word['count'] = df_word['word'].str.len()
df_word = df_word.query('count >= 2')
df_word

# 단어 빈도수 집계 및 정렬
df_word = df_word.groupby('word', as_index = False)
df_word = df_word.count().sort_values(by = 'count', ascending = False)
df_word


# 불필요한 단어 제거
del_idx = df_word.loc[df_word.word.isin(['영화','편이','영화관','파트','년전'])].index
df_word = df_word.drop(index = del_idx)
df_word


# (5) 워드클라우드 작성
# 빈도수 상위 10개 단어
plt.rcParams['font.family'] = 'Malgun Gothic'
plt.rcParams['axes.unicode_minus'] = False

df_top10 = df_word.iloc[:10, :].sort_values(by = 'count', ascending = True)
df_top10.plot.barh(x= 'word', y = 'count')
plt.show()

# 워크 클라우드
dic_word = df_word.set_index('word').to_dict()['count']  # 딕셔너리 구조로 바꿈
dic_word

# 워드 클라우드 만드려면 딕셔너리 형태로 단어와 빈도수를 입력해야함
wc = WordCloud(random_state = 123,# 단어 위치 무작위로 배치하기 위한 시드값
               font_path = 'C:/Windows/Fonts/malgun.ttf',
               width = 800,
               height = 400,
               background_color = 'white'
               )

img_wordcloud = wc.generate_from_frequencies(dic_word) #워드 클라우드 > 이미지

plt.figure(figsize = (10, 10)) #크기 지정하기
plt.axis('off') # 축 없애기
plt.imshow(img_wordcloud) # 결과 보여주기
plt.show() #결과를 화면에 출력

# (6)클라우드의 모양과 글씨 색상 변경 
import PIL # 이미지 처리 라이브러리
import numpy as np# 수치 계산 라이브러리

icon = PIL.Image.open('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/circle.png').convert('RGBA') # 아이콘  구름 이미지 불러오기
img = PIL.Image.new(mode = 'RGBA', size = icon.size, color = (255, 255, 255))
img.paste(icon,icon) # 붙일 이미지, 마스크(투명한 부분을 어떻게 처리할지를 설정)
img = np.array(img)

img

wc = WordCloud(random_state = 123,
               font_path = 'C:/Windows/Fonts/malgun.ttf',
                width = 400,
                height = 400,
                background_color = 'white',
                mask = img,
                colormap ='inferno' #plasma, virides magma, cividis,blues
                )
img_wordcloud = wc.generate_from_frequencies(dic_word) #워드 클라우드 > 이미지

plt.figure(figsize = (10, 10)) # 크기 지정하기
plt.axis('off')# 축 없애기
plt.imshow(img_wordcloud)# 결과 보여주기
plt.show()#결과를 화면에 출력하기
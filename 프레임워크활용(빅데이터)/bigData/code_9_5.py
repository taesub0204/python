import folium
import geokakao as gk

import os

#C:\Users\user\Desktop\taesub\python\프레임워크활용(빅데이터)\bigData\Map
os.chdir('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/bigData/Map/')    # displayMap.py 가 있는 폴더지정
import displayMap as dm
import pandas as pd

# 관광지 정보를 데이터프레임으로 저장ins
names = ['용두암', '성산일출봉', '정방폭포',
         '중문관광단지', '한라산1100고지', '차귀도']
addr = ['제주시 용두암길 15',
        '서귀포시 성산읍 성산리',
        '서귀포시 동홍동 299-3',
        '서귀포시 중문동 2624-1',
        '서귀포시 색달동 산1-2',
        '제주시 한경면 고산리 산 117']

dict = {"names": names, "addr": addr}
df = pd.DataFrame(dict)
df

# 관광지의 좌표를 df에 추가
gk.add_coordinates_to_dataframe(df, 'addr')
df
df.dtypes

# 문자열 좌푯값을 숫자로 변환
df.decimalLatitude = pd.to_numeric(df.decimalLatitude)
df.decimalLongitude = pd.to_numeric(df.decimalLongitude)
df.dtypes
df

# 지도의 중심점 계산
center = [df.decimalLatitude.mean(), df.decimalLongitude.mean()]
center

# 지도 객체 생성
map = folium.Map(location=center, zoom_start=12)

# 지도에 마커 추가
for i in range(len(df)):
    folium.Marker(location=[df.iloc[i, 2], df.iloc[i, 3]],
                  icon=folium.Icon(color='red', icon='star')).add_to(map)
dm.showMap(map)

# 지도에 텍스트(관광지명) 추가
html_start = html = '<div \
style="\
font-size: 12px;\
color: blue;\
background-color:rgba(255, 255, 255, 0.2);\
width:85px;\
text-align:left;\
margin:0px;\
"><b>'
html_end = '</b></div>'

for i in range(len(df)):
    folium.Marker(location=[df.iloc[i, 2], df.iloc[i, 3]],
              icon=folium.DivIcon(
                  icon_anchor=(0, 0),  # 텍스트 위치 설정
                  html=html_start+df.names[i]+html_end
                  )).add_to(map)

# 웹 브라우저에 지도 출력
dm.showMap(map)
import folium
import geokakao as gk

import os
os.chdir('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/bigData/Map/')    # displayMap.py 가 있는 폴더지정
import displayMap as dm
import pandas as pd

df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/wind.csv')

df.head()


df = df.sample(50,random_state=123)

#지도의 중심점 구하기
center = [df.lat.mean(), df.lon.mean()]

#지도 가져오기
map = folium.Map(location = center, zoom_start = 5)
dm.showMap(map)

#측정 위치에 마커표시하기
for i in range(len(df)):
    folium.Marker(location = [df.lat.iloc[i], df.lon.iloc[i]],
                   icon =folium.Icon(color = 'blue', icon= 'flag')
                   ).add_to(map)
    
dm.showMap(map)


#풍속을 원의 크기로 표시하기
map = folium.Map(location = center, zoom_start = 5)


# 측정 위치에 마커 표시하기
for i in range(len(df)):
    folium.CircleMarker(location = [df.lat.iloc[i], df.lon.iloc[i]],
                        icon =folium.Icon(color = 'blue', icon= 'flag'),
                       ).add_to(map)
dm.showMap(map)
import folium
import geokakao as gk
import os
os.chdir('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/bigData/Map') # displayMap.py 가 있는 폴더 지정
import displayMap as dm

# loc = [37.54,127.05]
# map = folium.Map(location = loc)
# dm.showMap(map)

html_start = html = '<div style="font-size: 10px; color: blue; background-color: pink; width:40px; text-align:center; margin:0px"><b>'
html_end = '</b></div>'

loc = gk.convert_address_to_coordinates('경기도 안성시 공도읍 송원길 41-12')
map = folium.Map(location =loc, zoom_start=18, tiles='CartoDB Positron')
folium.Marker(loc, popup='우리집', icon = folium.Icon(color='green', icon='heart')).add_to(map)
folium.Marker(loc, icon = folium.DivIcon(icon_anchor=(0,60), html=html_start + '우리집' + html_end)).add_to(map)
dm.showMap(map)
import webbrowser
import os 

#지도를 웹 브라우저에 표시
def showMap(map):
    map.save('map.html')  # 지도 객체를 HTML 파일로 저장
    filepath = os.getcwd() #getcwd() 함수를 사용하여 현재 작업 디렉토리의 절대 경로를 가져옴
    file_uri ='file:///'+filepath + '/map.html'  # HTML 파일의 절대 경로 생성
    webbrowser.open_new_tab(file_uri)  # 웹 브라우저에서 HTML 파일 열기
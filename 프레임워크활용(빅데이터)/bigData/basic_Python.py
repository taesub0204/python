print("Hello world")
print('Hellw world')#작은 따옴표도 문자열로 표현가능
print("'Mary 's comsmetics")
print('신씨가 소리질렀다 "도둑이야"')
print("C:\Windows")
print("안녕하세요.\n만나서\t\t반갑습니다.")
print ("오늘은", "일요일")#쉽표로 공백 확인가능
print("naver","kakao","sk","samsung", sep=";") # seq 인자  인자는 전달해주는 값  seq 무슨 약자인지는 확인필요
print("first",end=""); print("second")#end =""작성해주면 붙일 수 있음
print(5/3)

삼성전자 = 50000   #50,000로 쓰면 안되넹;; 
주식수 = 10
samsung = 삼성전자 * 주식수
print(samsung)



시가총액 = 298000000000000
현재가 = 50000
PER = 15.79
print(시가총액, type(시가총액))# typed을 넣어주면 int float  자료형 확인가능
print(현재가, type(현재가))
print(PER, type(PER))






phone_number = "010-111-2222"

str = phone_number.replace("_","")
print(str,type(str))

str1 = phone_number.split("_")
print(str1, type(str1))



#lang = 'python'
#lang[0] = 'P'
#print(lang)




string = 'abcdfe2a354a32a'
string = string.replace('a', 'A')
print(string)


print("Hi" * 3)
print("-" * 80)



t1 = "python"
t2 = "java"
t3 = t1 + ' ' + t2 + ' '
print(t3 * 4)



name1 = "김민수" 
age1 = 10
name2 = "이철희"
age2 = 13


print("이름: %s 나이: %d" % (name1, age1))
print("이름: %s 나이: %d" % (name2, age2))

print(f"이름:{name1} 나이 : {age1+1}") #제일 많이 씀 f 포맷 문법 



상장주식수 = "5,969,782,550"

comma = 상장주식수.replace(",","")
타입변환 = int(comma)
print(타입변환, type(타입변환))



분기 = "2020/03(E) (IFRS연결)"


data = "   삼성전자    "
data1 = data.strip()
print(data1)


data = "   삼성전자    "
data2 = data.lstrip()
print(data2)

data = "   삼성전자    "
data3 = data.rstrip()
print(data3)

ticker = "btc_krw"

print(ticker.upper())


ticker = "btc_krw"

print(ticker.lower())


a = "hello"
a = a.capitalize()
print(a)


file_name = "보고서.xlsx"
file_name.endswith(("xlsx", "xls"))
print(file_name)



file_name = "2020_보고서.xlsx"
file_name = "2020_보고서.xlsx"
file_name.startswith("2020")
print(file_name)



a = "hello world"
b=a.split()
print(b) #리스트로 출력됨


#파이선 리스트는 순서가 있고 수정 가능한 자료구조입니다.
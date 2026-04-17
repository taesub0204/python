# 함수로 다형성
# add 메서드가 오버로드

# def add(a, b, c=0):
#     return a+b+c

# print(add(1,2))
# print(add(1,2,3)) # c의 기본값이 0이므로, c에 3이 전달되어 a+b+c가 계산됨
# print(add(1,2,c=4)) # c의 기본값이 0이지만, c에 4가 전달되어 a+b+c가 계산됨
# print(add(a =1, b=2, c=3))
# print(add(c=3, a=1, b=2))
# print(add(1, c=2, b=3))
# print(add(1, c=3, b=2))

# 다른방식

def add(a, b):
    if isinstance(a, (float, int)) and isinstance(b, (float, int)): # is로 시작하는 놈은 true false로 반환하는 놈
        return a + b
    elif isinstance(a, str) and isinstance(b, str):
        return a + b
    else:
        return "지원되지 않는 타입입니다."

print(add(1,2))  
print(add(5.6, 7.3))
print(add("Hello, ", "world!"))  
print(add([1,2,3], [4,5,6])) 

## 오버로딩을 사용한 다형성
## 파이썬의 오버로딩 다형성 
# 소프트웨어공학（개발） 입문


# number = int(input("숫자를 입력하세요: "))
# sum = 0
# for i in  range(1, number+1):
#     sum += i 
# print(f"1부터 {number}까지의 합은 {sum}입니다")


# 프린트 함수 (매개변수) 
# 함수단위로 디버깅하는 게 더 찾기 쉬움




def Hello(name1,name2):
    return f"Hello {name1} 잘지냈니??, 나는 {name2}야."

def Add(a,b):
    print(f"{a}+{b} = {a+b}")

def Sub(a,b):
    print(f"{a}-{b} = {a-b}")


Add(10, 20)
Sub(10, 20)



n1 = input("첫번째 이름")
n2 = input("두번째 이름")
print(Hello(n1,n2))

print(Hello("Alice","Bob"))
print(Hello("길동","영희"))

# 주요 스코프 유형:

# 전역 스코프(Global Scope): 프로그램 전체에서 접근 가능한 범위이다.
# 함수 스코프(Function Scope): 특정 함수 내에서만 접근 가능한 범위이다.
# 블록 스코프(Block Scope): 중괄호 {} 블록 내에서만 접근 가능한 범위이다.
# 다음주에 스코프에 내용까지 했음  2026 03 20


# 숙제
# 변수 스코프와 가시성 한번 집에서 해보기

# global 키워드: 전역 변수 수정
# nonlocal 키워드: 상위 함수의 변수 수정
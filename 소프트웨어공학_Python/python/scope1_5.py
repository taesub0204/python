
#소프트웨어공학 소프트웨어 개발 입문
# 이

# 전역 변수
global_var = "전역 변수" # 전역변수

def outer_function():
    # 함수 스코프 변수
    outer_var = "외부 함수 변수"

    def inner_function():
        # 지역 변수
        local_var = "지역 변수"

        # global 키워드: 전역 변수 수정
        global global_var
        global_var = "수정된 전역 변수"

        # nonlocal 키워드: 상위 함수의 변수 수정
        # nonlocal outer_var
        outer_var = "수정된 외부 함수 변수"

        print(f"지역: {local_var}")
        print(f"외부 함수: {outer_var}") # 수정된 외부 함수
        print(f"전역: {global_var}") # 수정된 전역 변수

    inner_function() # 함수 호출됨
    print(f"outer_function에서 outer_var: {outer_var}") ## 수정된 외부 함수 변수


def scope_example(): # 함수가 정의 됨, 여기 넣으면 메인이 깔끔해짐
    x = 20  # 지역 (전역 변수를 가림)
    print(f"함수 내부 x: {x}")




# 여기서 부터 실행됨 메인
# 실행 예제
print(f"수정 전 전역 변수: {global_var}")  # 여기서 부터 실행됨 메인
outer_function()
print(f"수정 후 전역 변수: {global_var}") # 수정된 전역변수

# 스코프 체인 예제
x = 10  # 전역
scope_example() # 다시 메인 실행됨
print(f"전역 x: {x}") ## 10 

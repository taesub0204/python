

# 전역 변수 
global_var = "전역 변수"

def outer_function():
    #함수 스코프 변수 
    outer_var = "외부 함수 변수"

    def inner_function():
        # 지역변수 
        local_var = "지역 변수"

        #global 키워드 : 전역 변수 수정
        global global_var
        global_var = "수정된 전역 변수123"

        #nonlocal 키워드 : 상위 함수의 변수 수정
        nonlocal outer_var
        outer_var = "수정된 외부 함수 변수123"

        print(f"지역 : {local_var}")
        print(f"외부함수 : {outer_var}")
        print(f"전역: {global_var}")

    inner_function()
    print(f"outer_function에서 outer_var {outer_var}")

    #실행 예제
print(f"수정 전 전역 변수 : {global_var}")
outer_function()
print(f"수정 후 전역 변수 : {global_var}")

    #스코프 체인 예제
x = 10 #전역

def scope_example():
        x = 20 # 지역 (전역 변수를 가림)
        print(f"함수 내부 x: {x}")

scope_example()
print(f"전역 x: {x}")


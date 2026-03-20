# 숫자를 입력받아 양수, 음수, 또는 0을 판별하는 프로그램
number = int(input("숫자를 입력하세요: ")) # 동적 타이핑 변수 실행시 값이 변환하는 

if number > 0:
    print(number, "는 양수입니다.")
elif number < 0:
    print(number, "는 음수입니다.")
else:
    print("입력한 숫자는 0입니다.")


number = int(input("숫자"))


if number > 0:
    
    print(number, "양수입니다")
    print(f"{number}는(은) 양수입니다.")
elif number < 0:
    print(number, "음수입니다")
else:
    print("입력한 숫자는 0 입니다")
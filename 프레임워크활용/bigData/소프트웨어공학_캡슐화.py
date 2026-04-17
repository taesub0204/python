class Person:
    def __init__(self, name, age):
        self.__name = name  # __를 붙여 비공개 속성으로 선언
        self.__age = age

    # getter: name 값을 반환
    def get_name(self):
        return self.__name

    # setter: name 값을 수정
    def set_name(self, name):
        self.__name = name

    # getter: age 값을 반환
    def get_age(self):
        return self.__age

    # setter: age 값을 수정 (유효성 검사 포함)
    def set_age(self, age):
        if age > 0:
            self.__age = age
        else:
            raise ValueError("나이는 양수여야 한다.")

# 사용 예제
def main():
    person = Person("Bob", 25)
    print("이름:", person.get_name())
    print("나이:", person.get_age())

    # 값 변경
    person.age = 30
    person.set_age(28)
    print("변경된 나이:", person.get_age())

if __name__ == "__main__":
    main()
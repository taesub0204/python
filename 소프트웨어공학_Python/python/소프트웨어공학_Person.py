# 클래스 정의: Person
class Person:
    def __init__(self, name, age):
        self.name = name  # 객체의 상태
        self.age = age

    # 객체의 행동을 정의하는 메서드
    def introduce(self):
        print(f"안녕하세요, 저는 {self.name}이고, {self.age}살입니다.")

    def have_birthday(self):
        self.age += 1  # 나이를 한 살 더 먹음
        print(f"생일 축하합니다! {self.name}은 이제 {self.age}살입니다.")

    def change_name(self, new_name):
        self.name = new_name  # 이름 변경
        print(f"이름이 {self.name}으로 변경되었습니다.")

    def chage_age(self, new_age):
        self.age = new_age  # 나이 변경
        print(f"나이가 {self.age}로 변경되었습니다.")

class Worker(Person):
    def __init__(self, name, age, job):
        super().__init__(name, age)  # 부모 클래스의 초기화 메서드 호출
        self.job = job  # 추가된 속성

    def introduce(self):
        # super().introduce()  # 부모 클래스의 introduce 메서드 호출
        print(f"저는 {self.job}로 일하고 있습니다.") # 오버라이딩  부모꺼 안써도 됨... 위에 주석처리
    

    def change_job(self, new_job):
        self.job = new_job  # 직업 변경
        print(f"직업이 {self.job}로 변경되었습니다.")
    
    def work(self):
        print(f"{self.name}이(가) 오늘 {self.job} 일하고 있습니다.")




# 객체 생성

worker = Worker("Charlie", 35, "소프트웨어 엔지니어")
worker.introduce()
worker.work()

worker.have_birthday()
worker.change_job("데이터 과학자")
worker.work()
worker.introduce()




# 메서드 호출





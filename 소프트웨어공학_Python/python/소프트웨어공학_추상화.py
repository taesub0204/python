from abc import ABC, abstractmethod   # 추상클래스

# 추상 클래스: Animal
class Animal(ABC): # abc.ABC는 추상 클래스를 정의하기 위한 베이스 클래스입니다.
    def __init__(self, name):
        self._name = name  # protected attribute

    @abstractmethod
    def make_sound(self):
        """각 동물이 고유의 소리를 내도록 구현"""
        pass # 구현 안함

    def get_name(self):
        return self._name

# 자식 클래스: Dog
class Dog(Animal):
    def make_sound(self):
        print("Woof")

# 자식 클래스: Cat
class Cat(Animal):
    def make_sound(self):
        print("Meow")

def main():
    dog = Dog("Buddy")
    cat = Cat("Kitty")

    print(f"{dog.get_name()} makes sound:")
    dog.make_sound()

    print(f"{cat.get_name()} makes sound:")
    cat.make_sound()

if __name__ == "__main__":
    main()

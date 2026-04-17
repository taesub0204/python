

class Animal:
    def eat(self):
        return "냠냠냠."
    def sound(self):
        raise NotImplementedError("서브클래스에서 구현해야 한다.")

class Dog(Animal):
    def sound(self):
        return "Woof"
    

class Cat(Animal):
    def sound(self):
        return "Meow"
    
    def eat(self):
        print("오물오물")


class Bird(Animal):

    def sound(self):
        return "쨲쨱"
    def play(self):
        print("축구를 합니다.")



def main():
    animals = [Dog(), Cat(), Bird()]
    for animal in animals:
        print(animal.sound())

    animals[0].eat()
    animals[1].eat()
    animals[2].eat()
    animals[2].play()

if __name__ == "__main__":
    main()
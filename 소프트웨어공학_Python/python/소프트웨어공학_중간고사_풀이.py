# x = 1
# def f():
#  global x
#  x = 5
# f()
# print(x)  # 출력: 5



# x = 10
# def func():
#     x =20
#     print(x) #20
# func()
# print(x) #10

# for i in range(3):
#     if i == 1:
#         continue
#     print(i)

# i = 0
# while i < 3:
#     print(i)
#     i += 1

# for i in range(3):
#     print(i)
#     break

# for i in range(3):
#     if i == 2:
#         break
#     print(i)

# def f(x):
#     x = x + 1
#     return x
# a = 5
# f(a)
# print(a) #5

# x = 10
# def f():
#     x = 20  
#     print(x) #20
# f()
# print(x) #10

# x = 5
# def f():
#     global x
#     x = x + 2
# f()
# print(x) #7


# for i in range(3):
#     for j in range(3):
#         if j == 1:
#             break
#         print(i,j)

# i = 0
# while i < 3:
#     i += 1
#     if i == 2:
#         continue
#     print(i)


# def f(x, y = 2):
#     return x + y
# print(f(3)) #5

# for i in range(2):
#     print(i)
# else:
#     print("반복문이 정상적으로 종료되었습니다.")


# def f():
#     return
# print(f()) #None

# x = 1 
# def f():
#     x = x + 1
#     return x
# f()


# for i in range(3):  
#     if i == 2:
#         break
#     print("end")

# def f():
#     print("hello")
# print(f()) #hello None


# for i in range(5):
#     if i % 2 == 1:  # i가 홀수인 경우
#         continue  # 다음 반복으로 건너뜁니다.   
#     #짝수만 출력    
     
#     print(i, end=" ")


# def func(a, b=2):
#     return a * b
# print(func(3))  # 6

# def f(a):
#     return a * 2    

# def g(b):
#     return f(b) + 3

# print(g(3))  # 11


# class A:
#     def func(self):
#         print("A 클래스의 func 메서드")
    
# class B(A):
#     def func(self):
#         print("B 클래스의 func 메서드")


# obj = B()
# obj.print()  # B 클래스의 func 메서드



# class Parent:
#     def show(self):
#         print("Parent 클래스의 show 메서드")

# class Child(Parent):
#     def show(self):
#         print("Child 클래스의 show 메서드")

# obj = Child()
# obj.show()  # Child 클래스의 show 메서드


# def f(x):
#     return x + 1
# x =1
# x = f(x)
# x = f(x)
# print(x) #3



# def f(a):
#     return a * 2 #

# def g(b):
#     return f(b) + 2

# print(g(5))  # 12


# i = 0
# while i < 3:
#     i += 1
#     if i == 2:
#         continue
#     print(i, end=" ")


# NONE 출력되도록
# def f():
#     print()
# X = f()
# print(X) #None

#출력이 5가 되도록 빈칸 채우기 
# x = 2

# def f():
#     global x
#     x = x + 2

# def g():
#     global x
#     x = x + 1 

# f()
# g()
# print(x) #5


# B가 출력되도록 빈칸 채우기
class A:
    def func(self):
        print("A 클래스의 func 메서드")

class B(A):
    def func(self):
        print("B 클래스의 func 메서드")

obj = B()
obj.func()  # B 클래스의 func 메서드


#1.5.5. 실습 과제




x = "전역" #1

def outer():
    x = "outer" #3
    def inner():
        x = "inner" #5
        print(f"inner에서 x: {x}") ##6
    inner() # 4
    print(f"outer에서 x: {x}") #7

outer() #2
print(f"전역에서 x: {x}") #8
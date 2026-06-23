def fibonacci(n):
    seq = []
    a, b = 1,1
    for _ in range(n):
        seq.append(a)
        a, b = b, a + b
    return seq
terms = 6
print(fibonacci(terms)[-1]) # 출력 # 802


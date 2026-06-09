---
layout: post
title: "IT 자격증 대비 프로그래밍 연습문제 종합 정리 (Python & Java)"
date: 2026-05-30 17:00:00 +0900
categories: [License, Programming]
tags: [python, java, 정보처리기능사, 프로그래밍기능사]
---

# 📚 IT 자격증 대비 프로그래밍 연습문제 종합 정리

이 포스트는 정보처리기능사, 프로그래밍기능사 등 IT 자격증 실기 시험을 완벽하게 대비하기 위해 실습 코드를 정리한 문서입니다. 
VS Code 스타일의 코드 블록을 제공하며, **코드 내부의 주석(Python: `#`, Java: `//`)으로 왕초보 수준의 코드 해석과 정답(실행 결과)을 모두 포함**하여 복사해서 바로 실행할 수 있도록 만들었습니다.

---

## 🐍 Python 연습 문제 상세 정리

### 1. [python_test1.py](python_test1.py) - 리스트 슬라이싱
```python
# ==========================================
# [최종 실행 결과(정답)]
# [0, 20, 40, 60]
# ==========================================

# 1. 0부터 90까지 10 단위로 증가하는 10개의 정수가 담긴 리스트 a를 생성합니다.
a = [0, 10, 20, 30, 40, 50, 60, 70, 80, 90]

# 2. 리스트 슬라이싱을 수행합니다. 구조는 [시작인덱스 : 끝인덱스 : 증감값] 입니다.
# - 시작인덱스 생략: 0번 인덱스부터 작동합니다.
# - 끝인덱스 7: 7번 직전인 6번 인덱스까지 범위가 한정됩니다. -> [0, 10, 20, 30, 40, 50, 60]
# - 증감값 2: 2씩 건너뛰며 요소를 가져옵니다. (0번, 2번, 4번, 6번 인덱스)
a[:7:2] 

# 3. 슬라이싱된 결과인 [0, 20, 40, 60]을 출력합니다.
print(a[:7:2]) 
```

### 2. [python_test2.py](python_test2.py) - 비교 연산자
```python
# ==========================================
# [최종 실행 결과(정답)]
# True
# ==========================================

# 1. 정수형 변수 a와 b를 선언하고 값을 대입합니다.
a = 100
b = 200

# 2. 비교 연산자 !=(같지 않다)를 사용하여 두 값이 다른지 비교합니다.
# a(100)와 b(200)는 서로 다르기 때문에 연산 결과는 참(True)이 됩니다.
print(a != b)
```

### 3. [python_test3.py](python_test3.py) - 입력값 슬라이싱 및 조작
```python
# ==========================================
# [최종 실행 결과(정답)]
# (키보드 입력이 'xyz789-klm123' 이라고 가정할 시)
# 789,xyz
# 3 4 5 
# ==========================================

# 1. 사용자 입력을 받아 하이픈(-) 기준으로 두 문자열을 나누어 x와 y에 대입합니다.
# 예시 입력: xyz789-klm123 -> x = 'xyz789', y = 'klm123'
x, y = input('입력: ').split('-')

# 2. 기초 리스트 a를 생성합니다.
a = ['abc123', 'def456', 'ghi789']

# 3. 리스트 맨 뒤에 x('xyz789')를 추가합니다. -> ['abc123', 'def456', 'ghi789', 'xyz789']
a.append(x)

# 4. 리스트 맨 뒤에 y('klm123')를 추가합니다. -> ['abc123', 'def456', 'ghi789', 'xyz789', 'klm123']
a.append(y)

# 5. 리스트에서 'def456' 값을 제거합니다.
# 리스트 상태: a[0]='abc123', a[1]='ghi789', a[2]='xyz789', a[3]='klm123'
a.remove('def456')

# 6. 슬라이싱으로 특정 문자열을 추출하여 쉼표로 구분 출력합니다.
# - a[1][-3:] : a[1]인 'ghi789'의 뒤에서 3글자 -> '789'
# - a[2][:-3] : a[2]인 'xyz789'의 뒤에서 3글자를 제외한 앞부분 -> 'xyz'
print(a[1][-3:], a[2][:-3], sep=',') 

# 7. range(3, 6)은 3, 4, 5를 뜻하며, end=' '에 의해 띄어쓰기로 이어 붙여 출력됩니다.
for i in range(3, 6):
    print(i, end=' ')
```

### 4. [python_test4.py](python_test4.py) - 세트(Set)의 중복 제거
```python
# ==========================================
# [최종 실행 결과(정답)]
# (세트는 순서가 없으므로 출력 순서는 무관하게 아래 3줄이 출력됩니다.)
# 과일명 : apple
# 과일명 : banana
# 과일명 : kiwi
# ==========================================

# 1. 중복을 허용하지 않고 순서가 없는 세트(Set) a를 생성합니다.
a = {'apple', 'lemon', 'banana'}

# 2. update()를 통해 여러 요소를 일괄 추가합니다. 'banana'는 중복되므로 1개만 남습니다.
# 세트 상태: {'apple', 'lemon', 'banana', 'kiwi'}
a.update({'kiwi', 'banana'})

# 3. 'lemon' 원소를 삭제합니다.
# 세트 상태: {'apple', 'banana', 'kiwi'}
a.remove('lemon')

# 4. 'apple'을 추가하려 하지만 이미 존재하므로 변화가 없습니다.
# 최종 세트 상태: {'apple', 'banana', 'kiwi'}
a.add('apple')

# 5. 루프를 돌려 과일명을 순서 없이 출력합니다.
for i in a:
    print("과일명 : %s" % i)
```

### 5. [python_test5.py](python_test5.py) - 중첩 리스트 순회
```python
# ==========================================
# [최종 실행 결과(정답)]
# [1, 2, 3]
# 7
# 1 2 3 
# 4 5 
# 6 7 8 9 
# ==========================================

# 1. 2차원 중첩 리스트 lol을 선언합니다.
lol = [[1, 2, 3], [4, 5], [6, 7, 8, 9]]

# 2. 0번 인덱스 행 전체를 출력합니다.
print(lol[0]) # 결과: [1, 2, 3]

# 3. 2번 인덱스 행([6, 7, 8, 9])의 1번 인덱스 열에 위치한 원소를 출력합니다.
print(lol[2][1]) # 결과: 7

# 4. 이중 반복문으로 행과 열을 순차적으로 탐색하여 출력합니다.
for sub in lol:
    for i in sub:
        print(i, end=' ') # 원소마다 한 칸씩 띄어서 출력
    print() # 행이 끝날 때마다 줄바꿈
```

### 6. [python_test6.py](python_test6.py) - 비트 시프트 연산
```python
# ==========================================
# [최종 실행 결과(정답)]
# 26
# ==========================================

a = 100
result = 0

# range(1, 3)은 i가 1, 2일 때 반복 실행됩니다.
for i in range(1, 3):
    # a >> i 는 a를 2의 i제곱으로 나눈 정수 몫과 동일합니다.
    # - i = 1 일 때: result = 100 >> 1 (50이 됨) -> result = 50 + 1 = 51
    # - i = 2 일 때: result = 100 >> 2 (25가 됨) -> result = 25 + 1 = 26
    result = a >> i
    result = result + 1

# 최종 계산된 result인 26을 출력합니다. (기존 주석의 25 오류 수정)
print(result) 
```

### 7. [python_test7.py](python_test7.py) - 클래스 변수와 반복문 결합
```python
# ==========================================
# [최종 실행 결과(정답)]
# sKIDDGP
# ==========================================

class CharClass:
    # 7개의 단어가 포함된 클래스 리스트 변수 a를 선언합니다.
    a = ['seoul', 'Kyeongi', 'Incheon', 'Daejeon', 'Daegu', 'Gwangju', 'Pusan']

# 1. 클래스 인스턴스 객체를 생성합니다.
myVar = CharClass()
str01 = ''

# 2. myVar.a 리스트의 단어를 하나씩 가져와서 첫 글자(i[0])만 str01 변수에 차례로 누적 결합합니다.
# - 'seoul'[0] = 's'
# - 'Kyeongi'[0] = 'K'
# - 'Incheon'[0] = 'I'
# - 'Daejeon'[0] = 'D'
# - 'Daegu'[0] = 'D'
# - 'Gwangju'[0] = 'G'
# - 'Pusan'[0] = 'P'
for i in myVar.a:
    str01 = str01 + i[0]

# 3. 결합된 최종 문자열 'sKIDDGP'를 인쇄합니다. (기존 주석의 sKIDDGPP 오류 수정)
print(str01) 
```

### 8. [python_test8.py](python_test8.py) - 문자열 슬라이싱 및 포맷팅
```python
# ==========================================
# [최종 실행 결과(정답)]
# REMVEMBR AND STR
# ==========================================

a = "REMEMBVER NOVEMBER0" 

# 1. 문자열 슬라이싱을 수행하여 b에 대입합니다.
# - a[0:3] : 0번 인덱스부터 2번 인덱스까지 -> "REM"
# - a[12:16] : 12번 인덱스부터 15번 인덱스까지 -> "VEMB" (공백=9, N=10, O=11, V=12, E=13, M=14, B=15)
# - b = "REM" + "VEMB" = "REMVEMB"
b = a[0:3] + a[12:16]

# 2. %s 자리 표시자에 문자열 "STR"을 채워 넣습니다.
c = "R AND %s" % "STR" # c는 "R AND STR"이 됩니다.

# 3. "REMVEMB"와 "R AND STR"을 이어 붙여 출력합니다. (기존 주석의 REMEMBER 오류 수정)
print(b+c) 
```

### 9. [python_test9.py](python_test9.py) - map과 lambda식
```python
# ==========================================
# [최종 실행 결과(정답)]
# [101, 102, 103, 104, 105]
# ==========================================

a = [1, 2, 3, 4, 5]
x = 100

# x가 100이므로 if, elif는 거짓이며, else 구문만 실행됩니다.
if x == 10:
    a = list(map(lambda num: num+10, a ))
elif x == 50:
    a = list(map(lambda num: num + 50, a ))
else:
    # lambda num: num + 100 은 각 수에 100을 더하는 익명 함수입니다.
    # map 함수를 사용하여 리스트 a의 모든 원소에 100을 합산한 뒤 새 리스트를 만듭니다.
    a = list(map(lambda num: num + 100, a ))

print(a)
```

### 10. [python_test10.py](python_test10.py) - 클래스와 값 교환 (Swap)
```python
# ==========================================
# [최종 실행 결과(정답)]
# 10 20
# 20 10
# ==========================================

class Cls:
    # 클래스 변수 x와 y를 선언합니다.
    x, y = 10, 20
    
    # 두 변수 x와 y의 값을 맞바꾸는(Swap) 메서드입니다.
    def chg(self):
        temp = self.x      # temp에 원래 x(10) 보관
        self.x = self.y    # x의 자리에 y(20)를 덮어씀
        self.y = temp      # y의 자리에 보관해둔 temp(10)를 대입

# 1. 인스턴스를 생성합니다.
a = Cls()

# 2. 교환 전 상태인 10과 20을 출력합니다.
print(a.x, a.y)

# 3. chg 메서드를 실행하여 값을 교환합니다.
a.chg()

# 4. 교환 완료된 상태인 20과 10을 출력합니다.
print(a.x, a.y)
```

### 11. [python_test11.py](python_test11.py) - 리스트 역순 정렬 및 부분합
```python
# ==========================================
# [최종 실행 결과(정답)]
# 3
# ==========================================

# 리스트 좌우 반전 함수입니다.
def func(lst):
    # 리스트의 절반 크기만큼 순회합니다. (6 // 2 = 3번 반복)
    for i in range(len(lst) // 2):
        # i번째 요소와 그에 상응하는 뒷부분 대칭 요소를 교환합니다.
        lst[i], lst[-i - 1] = lst[-i - 1], lst[i]

lst = [1, 2, 3, 4, 5, 6]
func(lst) # 리스트가 뒤집어집니다 -> [6, 5, 4, 3, 2, 1]

# lst[::2]   : 처음부터 끝까지 2 간격으로 원소를 구합니다 -> [6, 4, 2], 합은 12
# lst[1::2]  : 1번 방부터 끝까지 2 간격으로 원소를 구합니다 -> [5, 3, 1], 합은 9
# sum(lst[::2]) - sum(lst[1::2]) = 12 - 9 = 3
print(sum(lst[::2]) - sum(lst[1::2]))
```

### 12. [python_test12.py](python_test12.py) - 데이터 타입 체크
```python
# ==========================================
# [최종 실행 결과(정답)]
# 45
# ==========================================

def func(value):
    # 정수 형식(type(100))인지 대조합니다.
    if type(value) == type(100):
        return 100
    # 문자열 형식(type(""))인지 대조합니다.
    elif type(value) == type(""):
        return len(value) # 글자 개수를 돌려줌
    # 그 외의 형식일 때
    else:
        return 20

a = "100.0"     # 문자열 -> len("100.0")인 5 반환
b = 100.0       # 실수형(float) -> type(100)인 정수형과 달라 else 구문 적용하여 20 반환
c = (100, 200)  # 튜플형(tuple) -> else 구문 적용하여 20 반환

# 5 + 20 + 20 = 45 가 출력됩니다. (기존 주석의 120 오류 수정)
print(func(a) + func(b) + func(c))
```

### 13. [python_test13.py](python_test13.py) / [파이썬_시험출제문제5.py](파이썬_시험출제문제5.py) - 피보나치 수열
```python
# ==========================================
# [최종 실행 결과(정답)]
# 8
# ==========================================

# 피보나치 리스트를 n개 크기만큼 만드는 함수
def fibonacci(n):
    seq = []
    a, b = 1, 1
    for _ in range(n):
        seq.append(a)      # 리스트에 값 추가
        a, b = b, a + b    # 한 칸씩 수열 전진
    return seq

terms = 6
# fibonacci(6)은 [1, 1, 2, 3, 5, 8] 리스트를 만들어냅니다.
# [-1]은 뒤에서 첫 번째 즉, 마지막 요소를 가리킵니다.
print(fibonacci(terms)[-1]) # 최종 8을 출력합니다. (기존 주석의 802 오류 수정)
```

### 14. [python_test14.py](python_test14.py) - 트리 중위 순회
```python
# ==========================================
# [최종 실행 결과(정답)]
# 2/5/7/1/6/3/4/
# ==========================================

class Node:
    def __init__(self, key):
        self.left = None
        self.right = None
        self.key = key

# 중위 순회(Inorder Traversal: Left -> Root -> Right) 알고리즘
def testFunction(root):
    if root:
        testFunction(root.left)       # 왼쪽 자식 노드 방문
        print(root.key, end="/")      # 현재 노드의 키값 출력 후 슬래시(/)
        testFunction(root.right)      # 오른쪽 자식 노드 방문

# 이진 트리 생성
#        1
#      /   \
#     5     3
#    / \   / \
#   2   7 6   4
root = Node(1)
root.left = Node(5)
root.right = Node(3)
root.left.left = Node(2)
root.left.right = Node(7)
root.right.left = Node(6)
root.right.right = Node(4)

# LNR 순서에 따라 2/5/7/1/6/3/4/ 가 순차적으로 출력됩니다.
testFunction(root)
```

### 15. [python_test15.py](python_test15.py) - 딕셔너리와 세트 연산
```python
# ==========================================
# [최종 실행 결과(정답)]
# 2
# ==========================================

lst = [1, 2, 3]

# 1. 딕셔너리 컴프리헨션 생성: dst = {1: 2, 2: 4, 3: 6}
dst = {i: i*2 for i in lst}

# 2. 딕셔너리의 value 리스트 [2, 4, 6]로 세트 s를 생성합니다: s = {2, 4, 6}
s = set(dst.values())

# 3. 리스트를 조작하지만 이미 만들어진 dst와 s에는 영향이 가지 않습니다.
lst[0] = 99

# 4. dst의 키 2에 해당하는 값을 7로 고칩니다 -> dst = {1: 2, 2: 7, 3: 6}
dst[2] = 7

# 5. s 세트에 99를 추가합니다 -> s = {2, 4, 6, 99}

# 6. dst.values()는 [2, 7, 6]이 되며, 세트화 시키면 {2, 6, 7}이 됩니다.
# - s & set(dst.values())는 {2, 4, 6, 99}와 {2, 6, 7}의 교집합 연산입니다.
# - 공통으로 존재하는 원소는 2와 6 두 가지이므로 {2, 6}이 만들어지고, len()에 의해 길이 2를 출력합니다.
print(len(s & set(dst.values())))
```

### 16. [python_test16.py](python_test16.py) - 리스트 슬라이싱과 split
```python
# ==========================================
# [최종 실행 결과(정답)]
# ck
# ==========================================

def process_data(data_list):
    # data_list[1:4] 는 인덱스 1, 2, 3의 부분 리스트를 자릅니다 -> ['b', 'c', 'd']
    sliced_data = data_list[1:4]
    processed_list = []
    for item in sliced_data:
        # 각 문자에 '-Checked' 를 덧붙입니다.
        processed_list.append(item + '-Checked')
    return processed_list # ['b-Checked', 'c-Checked', 'd-Checked'] 반환

data = ['a', 'b', 'c', 'd', 'e']
result1 = process_data(data)

# 1. result1[1]은 'c-Checked' 입니다.
# 2. 'c-Checked'.split('e') 는 문자열에서 소문자 'e'를 지우고 쪼개는 경계선으로 사용합니다.
#    결과는 ['c-Ch', 'ck', 'd'] 가 됩니다.
result2 = result1[1].split('e')

# 3. result2 리스트의 1번 요소인 'ck'가 출력됩니다.
print(result2[1]) 
```

### 17. [python_test17.py](python_test17.py) - 다양한 리스트 함수
```python
# ==========================================
# [최종 실행 결과(정답)]
# ['ruler', 'stapler', 'pencil', 'eraser', 'pen']
# ==========================================

a = ['pen', 'eraser', 'notebook', 'pencil', 'pen']

# 1. a.count('pen')은 리스트 내 'pen'의 개수인 2를 리턴합니다.
# 2. a.pop(2)에 의해 2번 인덱스 원소인 'notebook'이 빠지고 탈락합니다.
#    a 상태: ['pen', 'eraser', 'pencil', 'pen']
a.pop(a.count('pen'))

# 3. a.pop()은 리스트 최하단의 마지막 원소인 'pen'을 제거하고 b에 대입합니다.
#    a 상태: ['pen', 'eraser', 'pencil']
b = a.pop()

c = ['stapler', 'ruler']
# 4. 리스트 a 뒤편에 c의 요소를 병합 결합합니다.
#    a 상태: ['pen', 'eraser', 'pencil', 'stapler', 'ruler']
a.extend(c)

# 5. 리스트 내부 배치 순서를 앞뒤 역순으로 반전 시킵니다.
a.reverse()

# 6. 반전 완료된 최종 리스트 a를 출력합니다.
print(a)
```

---

## ☕ Java 연습 문제 상세 정리

### 1. [JAVA/Main.java](JAVA/Main.java) - 다형성과 인터페이스
```java
// ==========================================
// [최종 실행 결과(정답)]
// 15,5,50
// ==========================================

// 다형성 구현을 위한 기본 인터페이스 Op를 선언합니다.
interface Op {
    int calc(int a, int b);
}

// Op 인터페이스의 덧셈 연산 구현체
class Add implements Op {
    public int calc(int a, int b) {
        return a + b;
    }
}

// Op 인터페이스의 뺄셈 연산 구현체
class Subtract implements Op {
    public int calc(int a, int b) {
        return a - b;
    }
}

// Op 인터페이스의 곱셈 연산 구현체
class Multiply implements Op {
    public int calc(int a, int b) {
        return a * b;
    }
}

public class Main {
    public static void main(String[] args) {
        int a = 10, b = 5;
        // 상위 인터페이스 타입 참조 변수에 서브 객체를 매칭하여 다형성을 보장합니다.
        Op add = new Add();
        Op sub = new Subtract();
        Op mul = new Multiply();

        // 10 + 5 = 15 출력 후 쉼표(,)
        System.out.print(add.calc(a, b) + ",");
        // 10 - 5 = 5 출력 후 쉼표(,)
        System.out.print(sub.calc(a, b) + ",");
        // 10 * 5 = 50 출력
        System.out.print(mul.calc(a, b));
    }
}
```

### 2. [JAVA/Main1.java](JAVA/Main1.java) - 기본 환영 출력문
```java
// ==========================================
// [최종 실행 결과(정답)]
// 드디어 Java 첫 실행 성공!
// 2026년형 JDK 25 버전이 아주 잘 돌아가네요.
// ==========================================

public class Main1 {
    public static void main(String[] args) {
        // System.out.println()은 출력 후 자동으로 줄바꿈을 해주는 메서드입니다.
        System.out.println("드디어 Java 첫 실행 성공!");
        System.out.println("2026년형 JDK 25 버전이 아주 잘 돌아가네요.");
    }
}
```

### 3. [JAVA/Main2.java](JAVA/Main2.java) - 문자열 주소 비교
```java
// ==========================================
// [최종 실행 결과(정답)]
// ASDFASDF
// ==========================================

public class Main2 {
    public static void main(String[] args) {
        // new 키워드를 쓰면 문자열 글자 내용이 같더라도 힙 영역의 별도 주소 공간에 인스턴스가 쪼개져 생성됩니다.
        String t1 = new String("ASDF");
        String t2 = new String("ASDF");
        
        // == 연산자는 변수에 담겨진 메모리 참조 주소값을 대조합니다.
        // t1과 t2의 내용물은 "ASDF"로 같지만 가리키는 실제 주소는 서로 다릅니다 (false 판정).
        if (t1 == t2)
            System.out.print(t1);
        else
            // 조건이 거짓(false)이므로 else문이 발동되어 두 문자를 연결한 ASDFASDF가 표시됩니다.
            System.out.print(t1 + t2);
    }
}
```

### 4. [JAVA/Main3.java](JAVA/Main3.java) - static 메서드 하이딩
```java
// ==========================================
// [최종 실행 결과(정답)]
// Ab
// ==========================================

public class Main3 {
    static class A {
        // static 메서드는 오버라이딩(재정의) 대상이 아니라 컴파일 타임에 형식이 묶이는 하이딩(Hiding) 대상입니다.
        static String nu() { return "A"; }
        // 인스턴스 메서드는 런타임 다형성에 의해 실체 인스턴스의 구현체가 동작(오버라이딩)합니다.
        String mo() { return "a"; }
    }
    
    static class B extends A {
        static String nu() { return "B"; } // 하이딩 발생
        String mo() { return "b"; }        // 오버라이딩 발생
    }

    public static void main(String[] args) {
        // 참조 형식은 A 클래스이며 생성된 실체는 B 인스턴스입니다.
        A tester = new B();
        
        // - tester.nu()는 static 메서드이므로 선언 타입인 클래스 A의 nu()가 매칭되어 "A"를 반환합니다.
        // - tester.mo()는 인스턴스 메서드이므로 실제 인스턴스인 B 클래스의 mo()가 매칭되어 "b"를 반환합니다.
        System.out.println(tester.nu() + tester.mo());
    }
}
```

### 5. [JAVA/Test.java](JAVA/Test.java) - 스캐너 표준 입력
```java
// ==========================================
// [최종 실행 결과(정답)]
// (스캐너 입력값으로 '3 5'를 대입했을 경우)
// 8
// ==========================================

import java.util.Scanner;

public class Test {
    public static void main(String[] args) {
        // 사용자로부터 키보드 입력을 받아오기 위해 Scanner 객체를 구합니다.
        Scanner scan = new Scanner(System.in);
        
        // 띄어쓰기나 줄바꿈 단위로 정수 2개를 차례대로 입력받아 적재합니다.
        int a = scan.nextInt();
        int b = scan.nextInt();
        
        // 두 입력 정수를 더한 결과값을 서식에 맞춰 인쇄합니다.
        System.out.printf("%d", a + b);
        
        // Scanner 작업 종결을 위해 리소스를 회수 닫아 줍니다.
        scan.close();
    }
}
```

### 6. [JAVA/Test1.java](JAVA/Test1.java) - 삼항 연산자와 증감
```java
// ==========================================
// [최종 실행 결과(정답)]
// 201 201 300
// ==========================================

public class Test1 {
    public static void main(String[] args) {
       int result;
       int a = 100;
       int b = 200;
       int c = 300;
       
       // 삼항 연산자 구조: 조건식 ? 참일때실행값 : 거짓일때실행값
       // - 조건식: a < b (100 < 200) -> 참(true)
       // - 참 구문인 ++b 가 실행되고 거짓 구문인 c-- 는 아예 동작하지 않고 건너뜁니다.
       // - ++b는 전위 연산자이므로 b에 1을 더해 201로 바꾸고 이 값을 result에 넘깁니다.
       // - c는 변경되지 않고 300으로 방치됩니다.
       result = a < b ? ++b : c--;
       
       // result=201, b=201, c=300 값을 포맷팅에 맞추어 표시합니다.
       System.out.printf("%d %d %d \n", result, b, c);
    }
}
```

### 7. [JAVA/Test2.java](JAVA/Test2.java) - 10진수의 2진수 변환
```java
// ==========================================
// [최종 실행 결과(정답)]
// 00001010
// ==========================================

public class Test2 {
    public static void main(String[] args) {
        int a[] = new int[8]; // 8비트를 담기 위한 정수 배열 생성
        int i = 0;
        int n = 10; // 변환할 대상 값
        
        // 루프 조건에 부합할 때까지 2로 나눈 나머지를 배열 하단부터 밀어 넣습니다.
        // - a[0] = 10 % 2 = 0, n = 5
        // - a[1] = 5 % 2 = 1, n = 2
        // - a[2] = 2 % 2 = 0, n = 1
        // - a[3] = 1 % 2 = 1, n = 0
        // - 나머지 a[4] ~ a[7]은 전부 0으로 채워짐
        while(i < a.length) {
            a[i++] = n % 2;
            n /= 2;
        }
        
        // 이진수 형태 출력을 위해 뒤쪽인 7번 방부터 역순 하강하며 하나씩 프린트합니다.
        for (i = 7; i >= 0; i--) {
            System.out.print(a[i]);
        }
    }
}
```

### 8. [JAVA/Test3.java](JAVA/Test3.java) - 다양한 진법 표현식
```java
// ==========================================
// [최종 실행 결과(정답)]
// 20, 24, 36 , 80
// ==========================================

public class Test3 {
    public static void main(String[] args) {
        // 숫자 0으로 시작하면 8진수입니다.
        int j = 024;  // 8진수 24 -> 2 * 8^1 + 4 * 8^0 = 16 + 4 = 20 (10진수 기준)
        
        // 일반 숫자는 10진수입니다.
        int k = 24;   // 10진수 24
        
        // 0x로 시작하면 16진수입니다.
        int l = 0x24; // 16진수 24 -> 2 * 16^1 + 4 * 16^0 = 32 + 4 = 36 (10진수 기준)
        
        // 20 + 24 + 36 = 80
        int hap = j + k + l; 
        
        System.out.printf("%d, %d, %d , %d", j, k, l, hap);
    }
}
```

### 9. [JAVA/Test4.java](JAVA/Test4.java) - OR 및 AND 논리 연산
```java
// ==========================================
// [최종 실행 결과(정답)]
// true, false
// ==========================================

public class Test4 {
    public static void main(String[] args) {
        int i = 5, j = 4, k = 1;
        boolean L, m;
        
        // L = (5 > 5) || (4 != 0) -> false || true -> 최종 true
        L = i > 5 || j != 0;
        
        // m = (4 <= 4) && (1 < 1) -> true && false -> 최종 false
        m = j <= 4 && k < 1;
        
        System.out.printf("%b, %b \n", L, m);
    }
}
```

### 10. [JAVA/Test5.java](JAVA/Test5.java) - 연산 순위 우선권 판독
```java
// ==========================================
// [최종 실행 결과(정답)]
// true
// ==========================================

public class Test5 {
    public static void main(String[] args) {
        int a = 5, b = 10, c = 15, d = 30;
        boolean result;
        
        // 연산 순서: 산술 연산(*, +, /, -) -> 관계 연산(>, <=) -> 논리 연산(&&, ||)
        // 1. 왼쪽 연산: a * 3 + b > d -> 5 * 3 + 10 > 30 -> 15 + 10 > 30 -> 25 > 30 -> false
        // 2. 오른쪽 연산: c - b/a <= d && true -> 15 - (10/5) <= 30 && true -> 15 - 2 <= 30 && true -> 13 <= 30 && true -> true && true -> true
        // 3. 병합 연산: false || true -> 최종 true
        result = a * 3 + b > d || c - b/a <= d && true;
        
        System.out.printf("%b\n", result);
    }
}
```

### 11. [JAVA/Test6.java](JAVA/Test6.java) - switch문과 break 누락
```java
// ==========================================
// [최종 실행 결과(정답)]
// -8
// ==========================================

public class Test6 {
    public static void main(String[] args) {
        int c = 1;
        // switch 식의 결과가 3이므로 직접 case 3으로 점프합니다.
        switch(3) {
            case 1: c += 3;
            case 2: c++;
            // case 3부터 시작하여 break를 만날 때까지 하단 구문을 모두 차례대로 강제 수동 실행시킵니다.
            case 3: c = 0;    // c에 0이 대입됩니다.
            case 4: c += 3;   // c는 0 + 3 = 3이 됩니다.
            case 5: c -= 10;  // c는 3 - 10 = -7이 됩니다.
            default: c--;     // c는 -7 - 1 = -8이 됩니다.
        }
        System.out.printf("%d", c);
    }
}
```

### 12. [JAVA/Test7.java](JAVA/Test7.java) - continue와 반복
```java
// ==========================================
// [최종 실행 결과(정답)]
// 30
// ==========================================

public class Test7 {
    public static void main(String[] args) {
        int a = 0, sum = 0;
        for (; a < 10; ) {
            a++; // a를 반복 직전 마다 1씩 먼저 상승
            // 만약 홀수이면 아래 합산식 코드를 패스하고 곧장 루프 상단 판단문으로 돌아갑니다.
            if (a % 2 == 1)
                continue;
            // 짝수인 2, 4, 6, 8, 10만 하단에 도달하여 sum에 계속 누적됩니다.
            sum += a;
        }
        System.out.printf("%d", sum);
    }
}
```

### 13. [JAVA/Test8.java](JAVA/Test8.java) - 2진수의 10진수 복원
```java
// ==========================================
// [최종 실행 결과(정답)]
// 46
// ==========================================

public class Test8 {
    public static void main(String[] args) {
        int input = 101110;
        int di = 1; // 2진수 한 자리 비트당 할당 크기 (1, 2, 4, 8, 16...)
        int sum = 0;
        
        while(true) {
            if (input == 0)
                break;
            // 가장 끝자리 값(0 또는 1)을 추출하여 그에 부합하는 di를 곱한 뒤 누계합니다.
            sum = sum + (input % 10) * di;
            di = di * 2;       // 배수 2배 증가
            input = input / 10; // 처리 완료된 끝 자릿수 절단
        }
        System.out.printf("%d", sum);
    }
}
```

### 14. [JAVA/Test9.java](JAVA/Test9.java) - 완전수(Perfect Number) 찾기
```java
// ==========================================
// [최종 실행 결과(정답)]
// 2
// ==========================================

public class Test9 {
    public static void main(String[] args) {
        int s, el = 0;
        // 6부터 30 범위 내 자연수들을 대입 순회합니다.
        for (int i = 6; i <= 30; i++) {
            s = 0;
            // 약수를 구하기 위해 해당 수의 절반값까지만 반복 확인합니다.
            for (int j = 1; j <= i / 2; j++) {
                if (i % j == 0) { // i가 j로 나누어 떨어지면 j는 i의 약수입니다.
                    s = s + j; // 구한 약수를 누적 합산합니다.
                }
            }
            // 자신을 제외한 약수 합(s)이 최초의 수(i)와 서로 완벽히 일치하면 완전수입니다.
            // 6부터 30까지 중 완전수는 6(1+2+3)과 28(1+2+4+7+14) 두 개입니다.
            if (s == i) {
                el++;
            }
        }
        System.out.printf("%d", el);
    }
}
```

### 15. [JAVA/Test10.java](JAVA/Test10.java) - 배열 롤링 인덱싱
```java
// ==========================================
// [최종 실행 결과(정답)]
// 4 3 2 1 5 
// ==========================================

public class Test10 {
    public static void main(String[] args) {
        int [] n = {5, 4, 3, 2, 1};
        // i가 0부터 4까지 5번 진행됩니다.
        // i+1을 원소 크기인 5로 나눈 나머지값으로 인덱싱을 타서 한 칸씩 좌측 시프트 롤링한 결과를 뽑아냅니다.
        // - i = 0: n[1 % 5] -> n[1] -> 4
        // - i = 1: n[2 % 5] -> n[2] -> 3
        // - i = 2: n[3 % 5] -> n[3] -> 2
        // - i = 3: n[4 % 5] -> n[4] -> 1
        // - i = 4: n[5 % 5] -> n[0] -> 5
        for (int i = 0; i < 5; i++)
            System.out.printf("%d ", n[(i + 1) % 5]);
    }
}
```

### 16. [JAVA/Test11.java](JAVA/Test11.java) - 가변 다차원 배열
```java
// ==========================================
// [최종 실행 결과(정답)]
// 3
// 1
// 45
// 50
// 75
// 89
// ==========================================

public class Test11 {
    public static void main(String[] args) {
        // 자바 2차원 배열 선언 (행마다 속한 열의 길이가 상이한 가변 구조)
        int aa[][] = {{45, 50, 75}, {89}};
        
        System.out.println(aa[0].length); // 0번째 행의 크기 -> 3 출력
        System.out.println(aa[1].length); // 1번째 행의 크기 -> 1 출력
        System.out.println(aa[0][0]); // 45 출력
        System.out.println(aa[0][1]); // 50 출력
        System.out.println(aa[0][2]); // 75 출력
        System.out.println(aa[1][0]); // 89 출력
    }
}
```

### 17. [JAVA/Test12.java](JAVA/Test12.java) - 정수 숫자 거꾸로 결합
```java
// ==========================================
// [최종 실행 결과(정답)]
// 4321
// ==========================================

public class Test12 {
    public static void main(String[] args) {
        int number = 1234;
        int div = 10, result = 0;

        // number가 0이 될 때까지 각 자릿수를 하나씩 뒤로 당겨 결합합니다.
        // - 1회전: result = 0*10 + (4) = 4, number = 123
        // - 2회전: result = 4*10 + (3) = 43, number = 12
        // - 3회전: result = 43*10 + (2) = 432, number = 1
        // - 4회전: result = 432*10 + (1) = 4321, number = 0 (종료)
        while(number > 0) {
            result = result * div;
            result = result + (number % div);
            number = number / div;
        }
        System.out.printf("%d", result);
    }
}
```

### 18. [JAVA/Test13.java](JAVA/Test13.java) - 클래스 메서드 호출
```java
// ==========================================
// [최종 실행 결과(정답)]
// 19
// ==========================================

class ClassA {
    int a = 10;
    // 파라미터 값에 멤버 필드(a=10)를 덧셈 연산하여 돌려주는 메서드
    int funcAdd(int x, int y) {
        return x + y + a;
    }
}

public class Test13 {
    public static void main(String[] args) {
        int x = 3, y = 6, r;
        ClassA cal = new ClassA();
        // 3 + 6 + 10 = 19가 반환되어 r에 장착됩니다.
        r = cal.funcAdd(x, y);
        System.out.printf("%d", r);
    }
}
```

### 19. [JAVA/Test14.java](JAVA/Test14.java) - 생성자 체이닝과 다형성 바인딩
```java
// ==========================================
// [최종 실행 결과(정답)]
// AED7
// ==========================================

class ClassA {
    ClassA() {
        System.out.print('A'); // 1단계: 'A' 출력
        // 다형성에 의해 실제 인스턴스인 ClassB의 오버라이딩 된 prn()이 강제 연계 호출됩니다.
        this.prn();            // 2단계: 'E' 출력
    }

    void prn() {
        System.out.print('B');
    }
}

class ClassB extends ClassA {
    ClassB() {
       super();                // 부모의 생성자 ClassA()를 직접 불러옵니다.
       System.out.print('D');  // 3단계: 'D' 출력
    }

    void prn() { // 부모 메서드 재정의(오버라이딩)
        System.out.print('E');
    }
    void prn(int x) { // 파라미터가 추가된 메서드 정의(오버로딩)
        System.out.print(x);
    }
}

public class Test14 {
    public static void main(String[] args) {
        int x = 7;
        // 생성자 실행 순서에 맞춰 AED 순서대로 화면에 인쇄됩니다.
        ClassB cal = new ClassB();
        // 객체 구성 완료 후, prn(7)이 정상 실행되어 끝에 '7'이 노출됩니다.
        cal.prn(x); 
    }
}
```

### 20. [JAVA/Test15.java](JAVA/Test15.java) - 추상 클래스 구현
```java
// ==========================================
// [최종 실행 결과(정답)]
// Chickenis animal
// Zoo
// ==========================================

// 추상 상위 뼈대 클래스 Animal 선언
abstract class Animal {
    String a = "is animal";
    abstract void look(); // 상속받아 자식이 의무 구현할 추상 메서드
    void show() {
        System.out.println("Zoo");
    }
}

class Chicken extends Animal {
    Chicken() {
        look(); // 인스턴스 생성 과정 중 본인 내부의 look() 호출
    }
    void look() {
        // 부모의 필드 문자열 a("is animal")를 결합하여 출력합니다.
        System.out.println("Chicken" + a); 
    }

    void display() {
        System.out.println("two wings");
    }
}

public class Test15 {
    public static void main(String[] args) {
        // 1. B 객체를 생성하면서 look() 메서드가 자동 실행됩니다 -> Chickenis animal 출력
        Animal a = new Chicken();
        // 2. 부모의 공용 일반 메서드인 show()를 호출합니다 -> Zoo 출력
        a.show();
    }
}
```

### 21. [JAVA/Test17.java](JAVA/Test17.java) - 다형성과 재귀 함수
```java
// ==========================================
// [최종 실행 결과(정답)]
// 1
// ==========================================

class Parent {
    int compute(int num) {
        if(num <= 1) return num;
        return compute(num - 1) + compute(num - 2);
    }
}

class Child extends Parent {
    // 부모의 compute()를 재정의 오버라이딩합니다.
    int compute(int num) {
        if(num <= 1) return num;
        // 피보나치 변형 재귀 공식을 수행합니다.
        return compute(num - 1) + compute(num - 3);
    }
}

public class Test17 {
    public static void main(String[] args) {
        Parent obj = new Child(); // 업캐스팅 참조
        
        // 동적 바인딩에 의해 Child 클래스의 compute(4)가 작동됩니다.
        // compute(4) = compute(3) + compute(1)
        //            = (compute(2) + compute(0)) + 1
        //            = ((compute(1) + compute(-1)) + 0) + 1
        //            = ((1 + -1) + 0) + 1
        //            = (0 + 0) + 1
        //            = 1
        System.out.print(obj.compute(4));
    }
}
```

### 22. [JAVA/Test18.java](JAVA/Test18.java) - 필드 섀도잉 및 상속
```java
// ==========================================
// [최종 실행 결과(정답)]
// Vehicle name : spark
// ==========================================

abstract class Vehicle {
    String name;
    abstract public String getName(String val);
    public String getName() {
        return "Vehicle name : " + name;
    }
}

class Car extends Vehicle {
    private String name; // 부모와 명칭이 동일한 본인만의 name 변수를 별도로 은닉 선언
    
    public Car(String val) {
        // super.name(부모 객체의 필드)와 name(본인 필드)을 일괄적으로 매칭 세팅합니다.
        name = super.name = val;
    }
    public String getName(String val) {
        return "Car name: " + val;
    }
    public String getName(byte[] val) {
        return "Car name: " + val;
    }
}

public class Test18 {
    public static void main(String[] args) {
        Vehicle obj = new Car("spark");
        
        // 인자가 공백 상태인 getName() 메서드를 호출합니다.
        // Car 에는 대응 인자가 없는 getName()이 설계되어 있지 않으므로 부모 Vehicle 의 기본 getName()이 사용됩니다.
        // 부모의 getName()은 super.name 필드를 찾으므로 "Vehicle name : spark"를 돌려주게 됩니다.
        System.out.print(obj.getName());
    }
}
```

### 23. [JAVA/Test19.java](JAVA/Test19.java) - try-catch-finally 흐름 제어
```java
// ==========================================
// [최종 실행 결과(정답)]
// 101
// ==========================================

public class Test19 {
    public static void main(String[] args) {
      int sum = 0;
      try {
        func(); // 에러 발생 함수 작동
      }
      catch(NullPointerException e) {
        // func()에서 던진 NullPointerException을 Catch하여 sum에 1을 더합니다.
        sum += 1;
      }
      catch(Exception e) {
        sum += 10;
      }
      finally {
        // catch 블록이 정상 처리된 이후에도 finally는 항상 강제 실행됩니다.
        sum += 100;
      }
      
      // 최종 sum(101) 값을 화면에 띄웁니다.
      System.out.println(sum);
    }

    static void func() throws Exception {
        // 강제로 NullPointerException 예외를 인스턴스화하여 던집니다.
        throw new NullPointerException();
    }
}
```

### 24. [JAVA/Test20.java](JAVA/Test20.java) - 생성자 체이닝 (this() 및 super())
```java
// ==========================================
// [최종 실행 결과(정답)]
// x
// y
// a
// ==========================================

class Test1 {
    Test1() {
        System.out.println("x"); // 1단계: 부모 기본 생성자에서 x 출력
    }
    Test1(char a) {
        this();
        System.out.println("a");
    }
}

class Test2 extends Test1 {
    Test2() {
        super(); // 부모 클래스의 기본 생성자 Test1()을 먼저 작동시킵니다.
        System.out.println("y"); // 2단계: 자식 기본 생성자에서 y 출력
    }
    Test2(char a) {
        this();  // 본인 클래스의 빈 파라미터 생성자 Test2()를 먼저 가동시킵니다.
        System.out.println("a"); // 3단계: 자식 문자 생성자에서 a 출력
    }
}

public class Test20 {
    public static void main(String[] args) {
        // 1. Test2('z') 호출
        // 2. this()에 의해 Test2()로 우회
        // 3. super()에 의해 부모 Test1()이 최종 첫머리로 지목되어 수행
        // 순서에 맞춰 x ➡️ y ➡️ a 가 차례로 한 줄씩 출력됩니다.
        Test1 t1 = new Test2('z');
    }
}
```

### 25. [JAVA/Test21.java](JAVA/Test21.java) - Math 내장 클래스 API
```java
// ==========================================
// [최종 실행 결과(정답)]
// 16
// ==========================================

public class Test21 {
    public static void main(String[] args) {
        // - Math.PI: 약 3.141592...
        // - Math.ceil(Math.PI): 올림 계산에 의해 실수형인 4.0이 됩니다.
        // - Math.pow(2, 4.0): 거듭제곱 계산에 의해 2^4 = 16.0이 됩니다.
        // - (int): 소수점을 떨어뜨리는 강제 명시적 형변환을 가해 정수형 16으로 추출합니다.
        int ans = (int)Math.pow(2, Math.ceil(Math.PI));
        
        System.out.println(ans);
    }
}
```

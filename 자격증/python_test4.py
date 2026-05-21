a = {'apple', 'lemon', 'banana'}
a.update({'kiwi', 'banana'})
a.remove('lemon')
a.add('apple')

for i in a:
    print("과일명 : %s" % i) # 출력 # 과일명 : apple, 과일명 : kiwi, 과일명 : banana
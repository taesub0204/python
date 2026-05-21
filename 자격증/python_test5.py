lol = [[1,2,3],[4,5],[6,7,8,9]]
print(lol[0])
print(lol[2][1])
for sub in lol: # 리스트 lol의 각 요소인 sub에 대해 반복
    for i in sub:# sub 리스트의 각 요소인 i에 대해 반복
        print(i, end=' ') # 출력 # 1 2 3 4 5 6 7 8 9
    print() # 줄바꿈
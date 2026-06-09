ary = [1,9,4,8,2]
for i in range(4):
    for j in range(i +1,5):
        if ary[i] > ary[j] :
           ary[i],ary[j]  = ary[j], ary[i]
result =[]
for i in range(5):
    result.append(ary[i])
result.reverse()
print(result[1]+result[3])

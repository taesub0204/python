def func(lst):
    for i in range(len(lst) //2):
        lst[i], lst[- i - 1] = lst[- i - 1], lst[i] #오른데이타 끄집에내서 왠쪽으로
lst = [1,2,3,4,5,6]
func(lst)
print(sum(lst[::2]) - sum(lst[1::2]))
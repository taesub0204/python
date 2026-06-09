a = ['pen', 'eraser', 'notebook', 'pencil', 'pen']
a.pop(a.count('pen'))
a
b = a.pop()
c = ['stapler', 'ruler']
a.extend(c)
a.reverse()
print(a) 
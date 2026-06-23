grades = [[85, 90, 78],
          [92, 88, 95],
          [76, 82, 80],
          [90, 85, 87]
          
          ]
totals = {}
number = 1
for i in grades:
    score = 0
    for j in i:
        score += j
    totals[number] =score
    number += 1
out_value = totals.get(3)
print(out_value)
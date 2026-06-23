from scipy import stats

Group_A =[7, 3]
Group_B = [2, 9]


# 기대 빈도
stats.chi2_contingency([Group_A, Group_B]) [3]# p-value

# 피셔의 정확 검정
stats.fisher_exact([Group_A, Group_B])
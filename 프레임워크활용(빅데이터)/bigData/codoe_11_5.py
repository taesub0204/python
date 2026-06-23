from scipy import stats

men = [10, 10]
women = [15, 65]

# 카이제곱 검정
stats.chi2_contingency([men, women])
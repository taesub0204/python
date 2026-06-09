scores = {'국어': 85, '영어': 92, '수학': 78}

# 일반적인 접근 방식 (비추천)
for key in scores:
    print(key, scores[key]) # 키로 다시 값을 찾아야 해서 번거로움

# items()를 사용한 방식 (시험에 나오는 깔끔한 방식)
for subject, score in scores.items():
    print(f"과목: {subject}, 점수: {score}")
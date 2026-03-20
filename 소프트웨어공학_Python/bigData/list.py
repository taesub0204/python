movie_rank = ["닥터 스트레인지", "스플릿", "럭키"]
movie_rank = ['닥터 스트레인지', '스플릿', '럭키', '배트맨']
movie_rank.insert(1, "슈퍼맨")
print(movie_rank)
movie_rank = ["닥터 스트레인지", "스플릿", "럭키"]
movie_rank.append("배트맨")
print(movie_rank)

del movie_rank[3]
print(movie_rank)




num_list = [1,2,3,4,5]
print(num_list)
num_list.reverse()
print(num_list)
print(max(num_list), min(num_list), sum(num_list)/ len(num_list))




lang1 = ["C", "C++", "JAVA"]
lang2 = ["Python", "Go", "C#"]
lang3 = lang1+lang2
print(lang3)



price = ['20180728', 100, 130, 140, 150, 160, 170]

print(price[1:])



nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
print(nums[::2])

nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
print(nums[::-1])
print(nums[2])
print(nums)


interest = ['삼성전자', 'LG전자', 'Naver', 'SK하이닉스', '미래에셋대우']

print(" ".join(interest))
print("/".join(interest))
print("\n".join(interest))


string = "삼성전자/LG전자/Naver"
interest = string.split("/")
print(interest)


nums = [11, 2, 3, 4, 5, 6, 7, 8, 9, 10]
#nums.sort()
sorted(nums)  #차이 확인필요
print(nums)


nums = [11, 2, 3, 4, 5, 6, 7, 8, 9, 10]
nums.sort(reverse=True)
print(nums) # 내림차순

nums = [11, 2, 3, 4, 5, 6, 7, 8, 9, 10]
nums.sort()
print(nums)   #오름 차순




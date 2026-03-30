# 좋은 예 - 사용자 입력은 항상 검증이 필요한 예상 가능한 상황
def search_books(keyword):
    if keyword is None or keyword.strip() == "": # 빈칸이나 값이 없이 날라 왔는지?
        print("오류: 검색 키워드가 필요합니다.")
        return []

    # 실제 검색 로직
    return perform_search(keyword)

def perform_search(keyword):
    return[f"책제목:{keyword}"]



print(search_books("파이썬 프로그래밍")) # 입력해줬다고 가정
print(search_books("")) # 오류 : 빈 문자열
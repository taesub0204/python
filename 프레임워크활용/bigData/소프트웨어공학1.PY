# 1.6.3. 언제 예외 처리(try-except)를 사용할까?
# 좋은 예 - 네트워크 오류는 예측할 수 없는 예외 상황

# pylint: disable=missing-module-docstring
# pylint: disable=unused-import

import requests

def fetch_user_data(user_id):
    try:
        response = requests.get(f"https://api.example.com/users/{user_id}")
        return response.json()
    except requests.RequestException as e:
        print(f"네트워크 오류가 발생했습니다: {e}")
        return None


# user_data = fetch_user_data(123)
# if user_data:
#     print("사용자 데이터:", user_data)
# else:
#     print("사용자 데이터를 가져올 수 없습니다.")



# 좋은 예 - 파일 읽기 중 발생할 수 있는 다양한 예외들
def read_file(filename):
    try:
        with open(filename, 'r') as f:
            return f.read()
    except FileNotFoundError:
        print(f"파일을 찾을 수 없습니다: {filename}")
    except PermissionError:
        print(f"파일에 접근할 권한이 없습니다: {filename}")
    except Exception as e:
        print(f"파일 읽기 중 오류가 발생했습니다: {e}")
    return None


content = read_file('data.txt')
if content:
    print("파일 내용:", content)
else:
    print("파일을 읽을 수 없습니다.")
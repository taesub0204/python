# 1주차 · 장비 데이터 탐색
데이터: UCI SECOM (실제 웨이퍼 팹 계측 데이터)

---

## 오늘의 상황

여러분은 반도체 팹의 장비 데이터 분석 담당자입니다. 생산 라인 장비들이 웨이퍼 한 장을
처리할 때마다 590개의 센서 신호를 남기고, 그 웨이퍼는 최종 검사에서 양품(PASS) 또는
불량(FAIL) 판정을 받습니다.

공정팀에서 이런 요청이 왔습니다.

> 불량이 나는 웨이퍼는 장비 신호가 뭔가 다를 겁니다. 어떤 신호가 문제인지 찾아주세요.

오늘은 이 요청에 답하기 위해 데이터를 열어보고, 쓸 수 있는 상태로 정리하고,
불량과 관련이 있어 보이는 신호를 추려내는 것까지 합니다.
모델을 만드는 건 다음 주에 합니다.

## 규칙

`# TODO` 가 붙은 셀만 작성하면 됩니다. 각 미션 뒤에 자가진단 셀이 있으니
`[통과]` 가 뜰 때까지 스스로 고쳐보세요. 막히면 물어보되 먼저 15분은 혼자 붙들어 보세요.
정답을 베끼는 것보다 틀리고 고치는 과정이 점수에 반영됩니다.

---

## 준비


```python
# 이 셀은 그대로 실행하세요.
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.font_manager as fm
from pathlib import Path

_have = {f.name for f in fm.fontManager.ttflist}
for _f in ['Malgun Gothic', 'AppleGothic', 'NanumGothic', 'DejaVu Sans']:
    if _f in _have:
        plt.rcParams['font.family'] = _f
        break
plt.rcParams['axes.unicode_minus'] = False
pd.set_option('display.max_columns', 30)

DATA = Path('../../data/secom')   # 폴더를 옮겼다면 이 줄만 고치세요

def check(name, cond, hint=''):
    if cond:
        print('[통과] ' + name)
    else:
        print('[실패] ' + name + (('  ->  ' + str(hint)) if hint else ''))

print('폰트:', plt.rcParams['font.family'][0], '| 데이터 폴더:', DATA.exists())
```

    폰트: Malgun Gothic | 데이터 폴더: False
    

---

## 미션 1 · 데이터 열어보기

파일이 두 개 있습니다.

- `secom_equipment.csv` — 웨이퍼 1,567장과 신호 590개, 그리고 양불 판정
- `signal_metadata.csv` — 각 신호가 어느 장비 모듈의 무슨 센서인지

두 파일을 `df`, `meta` 로 불러오고, 크기와 불량 비율을 확인하세요.
`label` 컬럼은 1이 불량, 0이 양품입니다.

`timestamp` 는 날짜로 읽어야 합니다. 여기서 `parse_dates` 를 빠뜨리면
미션 7에서 반드시 막히니 지금 챙기세요.


```python
# TODO 1-1: 두 파일 불러오기
#   힌트: pd.read_csv(DATA / '파일명', encoding='utf-8-sig', parse_dates=['timestamp'])
df = pd.read_csv('./secom/secom_equipment.csv',encoding='utf-8-sig', parse_dates=['timestamp'])
meta = pd.read_csv('./secom/signal_metadata.csv',encoding='utf-8-sig', parse_dates=['signal_id'])


# TODO 1-2: 두 데이터의 크기 출력
print(df.shape)
print(meta.shape)


# TODO 1-3: 불량 건수와 비율(%)
# pass_fail "FAIL"인 경우를 세기
n_fail = (df['pass_fail'] == 'FAIL').sum()
fail_rate = n_fail / len(df) * 100
print(n_fail)
print(fail_rate)

```

    (1567, 594)
    (590, 13)
    104
    6.636885768985322
    

    C:\Users\user\AppData\Local\Temp\ipykernel_48152\451003692.py:4: UserWarning: Could not infer format, so each element will be parsed individually, falling back to `dateutil`. To ensure parsing is consistent and as-expected, please specify a format.
      meta = pd.read_csv('./secom/signal_metadata.csv',encoding='utf-8-sig', parse_dates=['signal_id'])
    

자가진단 1 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('데이터 로드', df is not None and meta is not None, '두 파일을 읽어오세요')
check('df 크기 1567 x 594', df is not None and df.shape == (1567, 594), None if df is None else df.shape)
check('meta 590행', meta is not None and len(meta) == 590)
check('불량 104건', n_fail == 104, n_fail)
check('불량률 6.64%', fail_rate is not None and abs(fail_rate - 6.64) < 0.05, fail_rate)
```

    [통과] 데이터 로드
    [통과] df 크기 1567 x 594
    [통과] meta 590행
    [통과] 불량 104건
    [통과] 불량률 6.64%
    



---

## 미션 2 · 결측 진단

실제 장비 데이터에는 비어 있는 값이 많습니다. 센서가 고장 났거나, 그 레시피에서는
아예 측정하지 않는 항목이거나, 통신이 끊긴 경우입니다.

신호별로 얼마나 비어 있는지 재고, 절반 넘게 비어 있는 신호를 추려내세요.
절반 이상 비어 있으면 채워 넣어도 대부분이 지어낸 값이 되기 때문에 버리는 편이 낫습니다.


```python
# TODO 2-1: SIG_ 로 시작하는 컬럼 이름만 모으기
sig_cols = [col for col in df.columns if col.startswith('SIG_')]

print(df.columns)

# TODO 2-2: 신호별 결측 비율 (힌트: .isna().mean())
miss = df[sig_cols].isna().mean()

# TODO 2-3: 결측이 많은 상위 10개 출력
print(miss.sort_values(ascending=False).head(10))


# TODO 2-4: 결측 50%를 넘는 신호 이름 리스트
high_missing = miss[miss > 0.5].index.tolist()
print("결측 50%를 넘는 신호:", len(high_missing),'개')
```

    Index(['wafer_id', 'timestamp', 'pass_fail', 'label', 'SIG_001', 'SIG_002',
           'SIG_003', 'SIG_004', 'SIG_005', 'SIG_006',
           ...
           'SIG_581', 'SIG_582', 'SIG_583', 'SIG_584', 'SIG_585', 'SIG_586',
           'SIG_587', 'SIG_588', 'SIG_589', 'SIG_590'],
          dtype='str', length=594)
    SIG_293    0.911934
    SIG_294    0.911934
    SIG_159    0.911934
    SIG_158    0.911934
    SIG_493    0.855775
    SIG_086    0.855775
    SIG_359    0.855775
    SIG_221    0.855775
    SIG_245    0.649649
    SIG_518    0.649649
    dtype: float64
    결측 50%를 넘는 신호: 28 개
    

자가진단 2 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('신호 590개', sig_cols is not None and len(sig_cols) == 590, None if sig_cols is None else len(sig_cols))
check('결측 비율 계산', miss is not None and abs(miss.mean() - 0.0454) < 0.001)
check('50% 초과 28개', high_missing is not None and len(high_missing) == 28, None if high_missing is None else len(high_missing))
```

    [통과] 신호 590개
    [통과] 결측 비율 계산
    [통과] 50% 초과 28개
    

---

## 미션 3 · 쓸모없는 신호 걸러내기

값이 처음부터 끝까지 똑같은 신호가 있습니다. 1,567장 내내 항상 100.0 인 식입니다.
이런 신호는 양품이든 불량이든 구별해 주지 못하니 빼고 갑니다.

미션 2에서 추린 것과 합쳐 제거 목록을 만드세요.
두 리스트를 그냥 이어 붙이지 말고 집합으로 합쳐야 합니다. 겹치는 신호가 있을 수 있으니까요.


```python
# TODO 3-1: 값이 항상 같은 신호 찾기 (힌트: .nunique())
const_cols = [c for c in sig_cols if df[c].nunique(dropna=True) <= 1]
print('값이 항상 같은 신호:', len(const_cols), '개')


# TODO 3-2: 제거 목록 합치기 (중복 없이)
drop_cols = sorted(set(high_missing) | set(const_cols))

# TODO 3-3: 남는 신호
keep_cols = [c for c in sig_cols if c not in drop_cols]
print('590개 중 {}개 제거, {}개 사용'.format(len(drop_cols), len(keep_cols)))
```

    값이 항상 같은 신호: 116 개
    590개 중 144개 제거, 446개 사용
    

자가진단 3 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('상수 신호 116개', const_cols is not None and len(const_cols) == 116, None if const_cols is None else len(const_cols))
check('제거 144개', drop_cols is not None and len(drop_cols) == 144, '결측 28 + 상수 116')
check('사용 446개', keep_cols is not None and len(keep_cols) == 446, None if keep_cols is None else len(keep_cols))
```

    [통과] 상수 신호 116개
    [통과] 제거 144개
    [통과] 사용 446개
    

---

## 미션 4 · 남은 결측 채우기

남은 446개 신호에도 결측이 조금씩 있습니다. 이건 버리기 아까우니 채웁니다.

무난한 방법은 그 신호의 중앙값으로 채우는 것입니다. 평균이 아니라 중앙값을 쓰는 이유는
센서 값에 크게 튀는 값이 섞여 있어도 덜 흔들리기 때문입니다.


```python
# TODO 4-1: 중앙값으로 채우기
#   힌트: X = df[keep_cols].fillna( ... .median())
X = df[keep_cols].fillna(df[keep_cols].median())

# TODO 4-2: 남은 결측 개수와 X 크기 출력
print('남은 결측:', X.isna().sum().sum())
print('X 크기:', X.shape)

```

    남은 결측: 0
    X 크기: (1567, 446)
    

자가진단 4 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('X 생성', X is not None)
check('X 크기 1567 x 446', X is not None and X.shape == (1567, 446), None if X is None else X.shape)
check('결측 0개', X is not None and int(X.isna().sum().sum()) == 0)
```

    [통과] X 생성
    [통과] X 크기 1567 x 446
    [통과] 결측 0개
    

---

## 미션 5 · 장비 모듈별로 나눠보기

`meta` 에는 각 신호가 어느 장비 모듈의 무슨 센서인지 적혀 있습니다.
모듈은 챔버, RF전원, 진공, 가스공급, 정전척, 온도제어, 이송, 계측 여덟 가지입니다.

전처리 후 살아남은 신호가 모듈별로 몇 개씩인지 세고 막대그래프로 그리세요.

한 가지 알아둘 것이 있습니다. 이 모듈·센서 이름은 수업용으로 붙인 가상의 이름입니다.
SECOM 원본은 신호가 익명화되어 있어서 실제로 어떤 센서인지는 공개되지 않았습니다.
미션 8에서 이 점을 다시 다룹니다.


```python
# TODO 5-1: 남은 신호의 메타데이터만 추출 (힌트: .isin())
meta_keep = meta[meta['signal_id'].isin(keep_cols)]


# TODO 5-2: 모듈별 개수 세기 (힌트: value_counts)
by_module = meta_keep['module_kr'].value_counts()
print(by_module.to_string())

# TODO 5-3: 막대그래프. 제목과 축 이름을 꼭 넣으세요
fig, ax = plt.subplots(figsize=(8, 4))
by_module.plot(kind='bar', ax=ax, color='#4C78A8')
ax.set_title('장비 모듈별 분석 대상 신호 수 (전처리 후 446개)')
ax.set_xlabel('장비 모듈')
ax.set_ylabel('신호 개수')
plt.xticks(rotation=0)
plt.tight_layout()
plt.show()
```

    module_kr
    계측      62
    챔버      60
    가스공급    60
    정전척     56
    이송      56
    RF전원    54
    온도제어    50
    진공      48
    


    
![png](data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAxYAAAGGCAYAAADmRxfNAAAAOnRFWHRTb2Z0d2FyZQBNYXRwbG90bGliIHZlcnNpb24zLjEwLjksIGh0dHBzOi8vbWF0cGxvdGxpYi5vcmcvJkbTWQAAAAlwSFlzAAAPYQAAD2EBqD+naQAAPupJREFUeJzt3QeYFFX29/EzgIAgUXdAARHFxJoRgUUBFcWEuouIAgbMsqIEdWVREMOCYcWM6xoAXQETIIggkgygLgoqCqsYWQUkCAMShGHe53f3rf5393Sumen0/TxPPTMdp6a6uuqee869VVBSUlJiAAAAAOBDJT8vBgAAAAACCwAAAABlgowFAAAAAN8ILAAAAAD4RmABAAAAwDcCCwAAAAC+EVgAAAAA8I3AAgAAAIBvBBZAjrnkkkvs3Xff9fUeZ511lt1+++2Wq1544QV7//33LRMtWrTIfv7553J570MOOcRGjx6d0mu//vprGzx4sJWHtWvX2lNPPZXw83/77Tfbvn275aK3337bFixYEPM52ncLCgqiPt6xY0cbMWJEyuswd+5cq169uvn16KOP2scff2y5SMdHHScBhCKwALKAGoNqSERbgk9w8+bNs1WrVlk2Ouqoo2L+n1rUOI6mbt26rlEUbr/99rOXX345cPvJJ5+M+LxYtI1vvfXWqI8rmEukMTZ9+nRr3Lhx1McvuugimzZtmpWFDRs2uG323XffJfR8rZu2VSQrVqywhx56KOV1ueKKK+y6666L+JjW78orr0z4vQYMGGBXX321lbWVK1e67fXtt9+WeSM01j7dr1+/wHOfeeYZ+9e//mW5INcCi3PPPTenO1yAslClTN4FQLnq3r27nXbaaYHbalyrh/f44493txNp0KqXs23btlZSUlLqMb1ejUr1dKZKPaSDBg2K+7x33nknsN6RjBw50nr16hX18SpVUjtsqfG6ePFi9/vmzZtTeg9tu507d0Z8rLi42NJBgcD3339f6n41jhVoJfv/Rdo/0mndunWl1mnbtm0uY6FMR7i99tor5b/1xRdfuJ/Lli2zZs2aWVlR4HDppZe637Xe+v6+9dZbdsABB7j7ateuHTdDc9JJJwVub9q0yf0M/h79/ve/t3/84x8x30ffb3U8RHLffffZjTfeaKn44Ycf7NRTT424D9522212//33h9zfuXNnX0FqedH/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+SXhwGLy5Ml20EEHWdOmTct3jQAAAADkZinUvHnz7JZbbrG5c+eW/xoBAAAAyL3AolatWrZlyxZ7/fXXrUGDBoH7dbXs+vXrB25rzIXKo6ZPn27HHXdc+a0xAAAAgOwLLJYvX24TJ060G2+80U444YTAheoqV67srogdrm7duuWzpgAAAACyN7BQluKaa66xXbt22a233mojR4509ys7seeee1bEOgIAAADIlelmFVwceeSR9ssvv1i9evXKd60AAAAA5OasUJUqVbIePXrYK6+8Ur5rBAAAACC3r2PRtm1bW79+vfudC+QBAAAASOnK2x06dLCOHTu637t3757MSwEAAADksKQyFsFX237wwQfLY30AAAAA5HpgAQAAAABJl0INGTLEknXddddZYWFh0q8DAAAAkKMZC10EL3z59ttvbdKkSREf0wIAAAAg/8TMWAwdOrTUfTNmzLDi4uKIjwEAAADIT3FnhWrWrFnIoO0tW7bY5s2bbf/994/4/G+++aZs1xAAAABA9gcWc+fOrZg1AQAAAJC7gcXq1autcePGts8++1TMGgEAAADIvelmjz/+eLfsvffe1rt3b3vvvfcqZs0AAAAA5E5gseeee7pxE0uWLLETTjjBrr76ajv99NPthx9+qJg1BAAAAJD9gYU3cFsBxmWXXWaffvqpnXrqqda2bVubN29eRawjAAAAgGwPLEpKSkJfUKmS9e/f31555RXr1asXpVEAAAAA4gcWXbp0iXh/mzZtbNy4cdajRw/bsWMHmxIAAADIY3EDiyeffDLqYxrU/eabb9puu+1W1usFAAAAIJcCi3gOPvjgslkTAAAAAPkbWAAAAAAAgQUAAAAA3wgsAAAAAPhGYAEAAADANwILAAAAAJkRWLRu3bos3gYAAABArgYWs2bNCrmWRfiVuOWHH34o+zUDAAAAkDuBxYYNG2zVqlXu94svvtiqVatmZ555pm3evDnwnIKCgvJdSwAAAAC5UQr10ksv2S+//GIrVqywQw45xO666y7ff/zee++1gw46yPbdd187/PDD7bXXXgs8tmjRImvTpo01bdrUWrRoYTNnzvT99wAAAACkMbBQ+ZMCi9tuu80aNGhgQ4YMsUmTJpXJ2IzPP//clVI99thj1r17d1u3bp1t2rTJunTp4oKX77//3kaNGmXdunULZE4AAAAAZFFgMW/ePFu4cKH7XY1/ZQ6kTp06tmPHDpdhmDx5sm3fvj2lP96hQwfbbbfd3O/t27e3GjVq2Jo1a2zcuHHWqlUr69SpU+B5enzChAkp/R0AAAAAaQwsFDTMmTPH/b5161Y3vsJTpUoVe+aZZ2z06NHuMT+2bdtmDz74oAsmVGa1YMECa9euXansxuLFi339HQAAAABpCCweeOABu+mmm9zvDRs2tJ9++ilQGvXbb7+5cqiJEye6DEYqvv76a2vSpInLVIwfP94ef/xxd//KlStdyVWwwsJCVyYViTImRUVFIQsAAACADBy8HVyK9Oabb7rB1n5nhTrggAPcYPAtW7bY9ddfb23btrWvvvrKdu7cWWpa2+Li4qh/Z/jw4S648RYFKwAAAAAqTpVEnqQG/ZVXXmktW7a0uXPn2ieffOLKpMpK9erVrUePHu6aGWPGjLH69evb2rVrQ56jsRfKmkQyaNAgGzBgQOC2MhYEFwAAAEAGZixUivT+++9b165d3biLY489tsxXRmM4dt99dxfAzJ8/P+Qx3VZGI9rrateuHbIAAAAAyKCMxSmnnGLHH3+8+71Ro0Z2+eWXl3pOpKtxx/Pjjz/a22+/7aaR1UBw/a7xGpqJSmMuRowYYbNnz7aTTjrJpk2bZkuXLnXPBQAAAJCFgUUiGYBXX3016T+sLMPTTz9tN9xwg9WqVcv2228/F1jognmiwdx9+vSx9evXW/PmzW3KlClWs2bNpP8OAAAAgAwZYxFPtBKlWPbaay976623oj7euXNnW7Zsmc81AwAAAJBRYywAAAAAIKWMxZAhQyweDeY+8sgjA1fI1hgJAAAAAPklZmBRuXLluG8QfG2Jb775pmzWCgAAAEDuBBZDhw5N6s1SvVAeAAAAgDwcY/Hzzz+7i+QBAAAAQEqBha5ZoWlgdY0JAAAAAEg6sNi6datdfPHF7roT/fv3ZwsCAAAASOw6Frr4XXFxsbvy9ZgxY6xnz542bNiweC8DAAAAkEfiBhYvvfSSCyyWL19u27Zts6OOOiowSHv16tX22GOPBUqkioqKyn+NAQAAAGRfYDFu3LjA70uWLLG+ffvaRx99ZHfffXepKWkpjwIAAADyU9zAIthhhx1mM2bMsAsvvNAFFoMHD056SloAAAAAuSfpWaGqVq1qo0ePtk8//bR81ggAAABAflzHQrNCTZgwoezXBgAAAED+BBYAAAAAEIzAAgAAAIBvBBYAAAAAfCOwAAAAAOAbgQUAAAAA3wgsAAAAAPhGYAEAAADANwILAAAAAL4RWAAAAADwjcACAAAAgG8EFgAAAAB8I7AAAAAAQGABAAAAIP3IWAAAAADwjcACAAAAgG8EFgAAAAB8I7AAAAAA4BuBBQAAAADfCCwAAAAA+EZgAQAAAMA3AgsAAAAAvhFYAAAAAPCNwAIAAABAdgcWs2fPtnbt2lnz5s3tgAMOsEceeSTw2HfffWennHKKNW3a1D3+/PPPp3NVAQAAAMRQxdJo8uTJ9swzz9jBBx9s33zzjbVv394OPPBAF1B06dLFBg4caJdeeql98cUXdvzxx9thhx1mRx11VDpXGQAAAECmBRYPPfRQ4Pf999/fzj//fJfFqFSpklWpUsUFFdKiRQvr1auXjRkzhsACAAAAyEAZNcZizZo1VqdOHVuwYIErkQrWunVrW7x4cdrWDQAAAEAWBBYffvihTZ061Xr06GErV660Bg0ahDxeWFho69ati/ja7du3W1FRUcgCAAAAIM8Ci/Hjx9vZZ5/tSp2aNWtmO3futJKSkpDnFBcXW0FBQcTXDx8+3GU6vKVJkyYVtOYAAAAA0j7GQsFC3759bc6cOTZjxgw78sgj3f3169e3tWvXliqTatiwYcT3GTRokA0YMCBwWxkLggsAAAAgTwKLfv36udmgFi5caDVr1gzc37JlS7vvvvtCnjt//nxr27ZtxPepVq2aWwAAAADkWSnUtm3bbNSoUfbss8+GBBWiqWZ/+umnwLUrFHhoatorrrgiTWsLAAAAICMzFspU7Nq1q1QWQte0UFnUlClT7Morr3QlTiqBeuGFF6xx48bpWl0AAAAAmRhY6NoUCiyiUTnUxx9/XKHrBAAAACCLZ4UCAAAAkN0ILAAAAAD4RmABAAAAwDcCCwAAAAC+EVgAAAAA8I3AAgAAAIBvBBYAAAAAfCOwAAAAAOAbgQUAAAAA3wgsAAAAAPhGYAEAAADANwILAAAAAL4RWAAAAADwjcACAAAAgG8EFgAAAAB8I7AAAAAA4BuBBQAAAADfCCwAAAAA+EZgAQAAAMA3AgsAAAAAvhFYAAAAAPCNwAIAAACAbwQWAAAAAHwjsAAAAADgG4EFAAAAAN8ILAAAAAD4RmABAAAAwDcCCwAAAAC+EVgAAAAA8I3AAgAAAIBvBBYAAAAAfCOwAAAAAOAbgQUAAAAA3wgsAAAAAPhGYAEAAADANwILAAAAANkdWJSUlNjYsWOtbdu2IfcvWrTI2rRpY02bNrUWLVrYzJkz07aOAAAAAOKrYmkyffp0u+mmm2zr1q1Wpcr/rcamTZusS5cuNnr0aOvUqZPNmzfPzjnnHFu2bJk1bNgwXasLAAAAIBMzFr/++qvdc8899tRTT4XcP27cOGvVqpULKqRDhw7Wvn17mzBhQprWFAAAAEDGZiy6du3qfs6dOzfk/gULFli7du1C7mvdurUtXry4QtcPAAAAQBYP3l65cqU1aNAg5L7CwkJbt25d1Nds377dioqKQhYAAAAAeRxY7Ny50w3qDlZcXGwFBQVRXzN8+HCrU6dOYGnSpEkFrCkAAACAjA0s6tevb2vXrg25b82aNTEHbg8aNMg2btwYWFasWFEBawoAAAAgYwOLli1b2vz580Pu0+3wKWmDVatWzWrXrh2yAAAAAMjjwKJnz542a9Ysmz17trs9bdo0W7p0qXXr1i3dqwYAAAAg02aFiqZx48Y2fvx469Onj61fv96aN29uU6ZMsZo1a6Z71QAAAABkamDRsWNHd/G7YJ07dy51HwAAAIDMlXGlUAAAAACyD4EFAAAAAN8ILAAAAAD4RmABAAAAwDcCCwAAAAC+EVgAAAAA8I3AAgAAAIBvBBYAAAAAfCOwAAAAAOAbgQUAAAAA3wgsAAAAAPhGYAEAAADANwILAAAAAL4RWAAAAAAgsAAAAACQfmQsAAAAAPhGYAEAAADANwILAAAAAL4RWAAAAADwjcACAAAAgG8EFgAAAAB8I7AAAAAA4BuBBQAAAADfCCwAAAAA+EZgAQAAAMA3AgsAAAAAvhFYAAAAAPCNwAIAAACAbwQWAAAAAHwjsAAAAADgG4EFAAAAAN8ILAAAAAD4RmABAAAAwDcCCwAAAAC+EVgAAAAA8I3AAgAAAEDuBhZbt261q666ypo2bWqNGze2m2++2UpKStK9WgAAAACyKbAYOHCg7dq1y77++mv7/PPPbc6cOfboo4+me7UAAAAAZEtgsXnzZhszZozde++9VqVKFatTp44NGjTInnnmmXSvGgAAAIBsCSw++ugja9asmdWvXz9wX+vWrW3JkiVWXFyc1nUDAAAAUFoVy0ArV660Bg0ahNxXWFhoO3futI0bN4YEHLJ9+3a3ePQcKSoqKpf127F9i2Wb8toW5Skbt7OwrdnOubZfZ+M+LWxrtnOu7dfZuE8L2zq7t7P3vomMdS4oycAR0c8//7wre5o9e3bgvm3bttnuu+9u69evt3r16oU8//bbb7dhw4alYU0BAACA3LdixQo3oVLWZSyUkVi7dm3IfWvWrLHq1au78RbhNP5iwIABgdsa9K0AZM8997SCggLLBooGmzRp4j602rVrp3t1chrbmm2di9iv2c65hn2abZ2LirKwvaccxKZNm2yfffaJ+9yMDCyOOeYY+89//mO//PJLIDsxf/58N86iUqXSw0KqVavmlmB169a1bKSdLFt2tGzHtmZb5yL2a7ZzrmGfZlvnotpZ1t6L1LGfNYO3GzZsaKeddpr99a9/deMqlL24++67rV+/fuleNQAAAADZEljI008/bT/99JPtvffeduyxx7qL5Z177rnpXi0AAAAA2VIKJXvttZdNnjzZ8oVKuYYOHVqqpAts62zGfs22zjXs02zrXMR+zbYuKxk5KxQAAACA7JKxpVAAgPLx5JNP2vLly0vdP2fOHDdxBpCpNNXld999l/Tr2rRpY3Pnzi2XdQLiGT16tPXq1SsvNhSBRRZ499137Ycffkj3amSFH3/8MeL9Ssy99dZbSb2Xro+iJV9pqmZNnoD0UkP/oYceSuo1t956a8x994UXXojYOBszZowtWLAgpfXMdTp+dOzYMd2rkdM0m6MWzT6jGSC924mMr/zyyy/tj3/8ozVr1swtxx9/fMi1sJD+RrK+P8meh7Oljebtq3vssYfttttugdt9+vSJ+jodgzVZUbSlcuXKbqxxtiGwSDPtWPEuNnL//fe76XYRX9OmTUMaw5deeqk7CBYXF9spp5wS88CoHtz99tsvLzazeqbDD2K6fox69ZLRuXNne+mll+I+75133ol5AK1atWrE1z388MNuvFWkRdP0/elPf7Jsdu+99wb+H12np1atWoHbH3zwgXuO5jqfOHFi4DU6UXknLZ3AdCLzbsc6CWlbedtbx5Pzzz8/cPvDDz+0fLd582Z3vND3oLCw0P7yl7+4ayIlo3379u7irvlM21DTxOtYqnNbhw4dbPHixYHznTos9Ji3tG3bNvDaDRs2uOXTTz91n4N3e9KkSTH/pj6nTp06uQDkm2++sW+//dbuu+8+d4z/+uuvLVd8//33dt1119khhxxi+++/v1sOO+wwN4Omrt0VbuHChValSpWox9ATTjih1GtatWrljgk6vurY4h0jtm/f7gKDWFkfdUposh0di37/+9/bjBkzLB8oiPX21X/961928sknB24//vjjUV+n/X/VqlVRF30H9PllGwKLCmy86Yt62WWXxe0N15c3+HWvv/66mxUr+L5HH320PFc9q0yYMCGwXRRA6GSm39Uri8hOPPHEUgexBx54wAVmiVIWSL3py5Yti/tcncCiHTx14o928Lz++uvddNORFs0cpx6dbHbzzTcH/p8zzjjD7rnnnsBtXbcnEp2ovJOWTmjjx48P3I518aJXX301sM2POuoo9zrv9nHHHWf5Tg02HT+UHf78889dYKfPIxE7duxwWaKaNWu6Y/Pbb79t+UxBmYKI//73vy7Q6NKli2uYeoOE9Zi3RMqQffHFF7Zu3Tr3eq8j6KmnnnLLli1bSj1/48aNblEg4V0UVwGLGt2RSv6y0datW933XY3Rf//73y6A0qKGvratOnki0TaIdgxVh084vbeOCUOGDHHfCe8YEW9iGV137JxzzrHBgwe735944gm75JJLUipby2ZffPGFLVmyJOlOCR2/r7766pD7fvvtN9fhlG0ILCqw8aargyfScNOBwnvNY4895qJWpXY//vjjwP36wuN/unfv7raJ1xumxq5u9+jRg02UZDr3D3/4Q8LP/+c//+l6vVSvv3r16oRf99VXX9ltt93m++Cpxlw2HnQjUZbto48+spkzZ7rb06dPDwTLyixEs3Tp0kBv8HvvvRdofHn3RbJt2zbXcF60aFE5/CfZSceLqVOn2qhRo1wv7e9+9zu3HUeOHBm1HFD7vDp91FuswKxBgwbuPXSfvhM6/v/jH/9w2SHt4/mqd+/ebhtqX02UGqUKfkeMGBHoqfcCkUifhzIkF110kWvY6jNQuc0NN9zgGuPKmOQCBbz67t54440us+nRMVidlGof6PGyou2sQDtR+r6cffbZrhxN52F1Jl177bVJl3FmM52Tnn76addmU1lpMvTZ6dgR3HGn40bwZ50tCCwqkE72w4cPdyeuv/3tbxGf8+uvv7oDsL6kSu3qAKueMx04VMqjL6oaHTrQJvOlzwdK++rLqAZWsL59+7oGWqSaXPUuesvzzz/vtqkXvKk0Il8UFRW56Z27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)
    


자가진단 5 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('meta_keep 446행', meta_keep is not None and len(meta_keep) == 446, None if meta_keep is None else len(meta_keep))
check('모듈 8종', by_module is not None and len(by_module) == 8)
```

    [통과] meta_keep 446행
    [통과] 모듈 8종
    

---

## 미션 6 · 불량 관련 신호 Top 10 찾기

오늘의 본론입니다.

불량 웨이퍼와 양품 웨이퍼의 신호 값을 비교해서 차이가 큰 신호를 찾습니다.
그런데 신호마다 단위가 다릅니다. 어떤 건 3000 언저리이고 어떤 건 0.1 언저리입니다.
그냥 빼서 비교하면 값이 큰 신호가 무조건 이깁니다.

그래서 표준편차로 나눠줍니다. 이렇게 하면 단위가 사라져서 신호끼리 공정하게 비교됩니다.
이 값을 효과크기라고 부릅니다.

```
효과크기 = | (불량 평균 - 양품 평균) / 전체 표준편차 |
```

표준편차가 0인 신호는 0으로 나누게 되니 `np.nan` 으로 바꿔두고 계산하세요.




```python
# TODO 6-1: 불량 마스크
is_fail = df['label'] == 1

# TODO 6-2: 그룹별 평균
mean_fail = X[is_fail].mean()
mean_pass = X[~is_fail].mean()

# TODO 6-3: 효과크기 (절댓값, 큰 순서로 정렬)
effect = ((mean_fail - mean_pass)/X.std()).sort_values(ascending=False)

# TODO 6-4: Top 10 을 meta 와 합쳐 표로 출력
top10 = effect.head(10)
tbl = (top10.rename('효과크기').reset_index().rename(columns={'index': 'signal_id'})
       .merge(meta[['signal_id', 'module_kr', 'sensor_type', 'unit']], on='signal_id', how='left'))
tbl['효과크기'] = tbl['효과크기'].round(4)
print(tbl.to_string(index=False))


# TODO 6-5: 1등 신호의 양품 vs 불량 박스플롯
# 1등 신호의 양품 vs 불량 박스플롯
best = top10.index[0]
fig, ax = plt.subplots(figsize=(6, 4.5))
ax.boxplot([X.loc[~is_fail, best], X.loc[is_fail, best]], tick_labels=['양품', '불량'])
ax.set_title('{} 값 분포 (효과크기 {:.3f})'.format(best, top10.iloc[0]))
ax.set_ylabel('센서 값')
plt.tight_layout()
plt.show()


```

    signal_id   효과크기 module_kr sensor_type unit
      SIG_060 0.6265      가스공급        라인압력  psi
      SIG_104 0.6073        계측        표면조도   nm
      SIG_511 0.5288        이송       진공척흡착  kPa
      SIG_349 0.5253       정전척         척전압    V
      SIG_432 0.4817        계측        표면조도   nm
      SIG_435 0.4470        진공     게이트밸브개도    %
      SIG_431 0.4382        이송       진공척흡착  kPa
      SIG_022 0.4351      온도제어       쿨런트온도 degC
      SIG_436 0.4348      가스공급        라인압력  psi
      SIG_437 0.4262       정전척         척전압    V
    


    
![png](data:image/png;base64,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)
    


자가진단 6 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('불량 104건', is_fail is not None and int(is_fail.sum()) == 104)
check('효과크기 계산', effect is not None and len(effect.dropna()) > 400)
check('1위 SIG_060', top10 is not None and top10.index[0] == 'SIG_060', None if top10 is None else top10.index[0])
check('2위 SIG_104', top10 is not None and top10.index[1] == 'SIG_104')
check('1위 효과크기 0.627', top10 is not None and abs(top10.iloc[0] - 0.6265) < 0.01)
```

    [통과] 불량 104건
    [통과] 효과크기 계산
    [통과] 1위 SIG_060
    [통과] 2위 SIG_104
    [통과] 1위 효과크기 0.627
    

---

## 미션 7 · 시간에 따른 불량률 변화

장비는 시간이 지나면서 상태가 변합니다. 소모품이 닳고, 챔버가 오염되고, 정기점검을 받습니다.
예지보전의 출발점은 불량이 언제 몰렸는지 보는 것입니다.

월별로 처리량과 불량률을 집계하고 선그래프로 그리세요.
그래프를 보면서 미션 8에서 뭐라고 쓸지 생각해 두세요.


```python
# TODO 7-1: 월 단위 컬럼 만들기
#   힌트: df['timestamp'].dt.to_period('M')
#   (아래에 직접 작성하세요)
month = df['timestamp'].dt.to_period('M').rename('month')
df = pd.concat([df, month], axis=1)


# TODO 7-2: 월별 처리량 / 불량수 / 불량률
# 월별 처리량 4개월 집계 
monthly = df.groupby('month')['label'].agg(처리량='size', 불량수='sum', 불량률='mean')
monthly['불량률'] = (monthly['불량률'] * 100).round(2)
print(monthly.to_string())

# 데이터프레임으로 묶기
# monthly_total과 monthly_fail 변수를 더 이상 사용하지 않으므로 제거
# 이미 monthly 데이터프레임에 처리량, 불량수, 불량률이 포함되어 있음


# 월별 불량률 컬럼 추가
monthly['불량률'] = monthly['불량수'] / monthly['처리량']

# 💡 4개월 집계가 맞는지 확인하는 코드 (True면 4개월 데이터가 정확히 있는 것)
print("월별 데이터가 4개월치인지 확인:", len(monthly) == 4)

print("\n[월별 집계 데이터프레임]")
print(monthly)




# TODO 7-3: 월별 불량률 선그래프
# Period 타입 인덱스 때문에 생기는 오류를 방지하기 위해 인덱스를 문자열로 변환
fig, ax = plt.subplots(figsize=(7, 4))
ax.plot(monthly.index.astype(str), monthly['불량률'], marker='o', color='#E45756', linewidth=2)
ax.set_title('월별 불량률 추이')
ax.set_xlabel('월')
ax.set_ylabel('불량률 (%)')
ax.grid(alpha=.3)
plt.tight_layout()
plt.show()
```

             처리량  불량수    불량률
    month                   
    2008-07   63   14  22.22
    2008-08  555   51   9.19
    2008-09  590   17   2.88
    2008-10  359   22   6.13
    월별 데이터가 4개월치인지 확인: True
    
    [월별 집계 데이터프레임]
             처리량  불량수       불량률
    month                      
    2008-07   63   14  0.222222
    2008-08  555   51  0.091892
    2008-09  590   17  0.028814
    2008-10  359   22  0.061281
    


    
![png](data:image/png;base64,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oX7ASEUR0hyekJBQ7Fz5bbl+/foVHpfRg9I8L5VEmu8bNGiARYsWYf78+eq8VatWFZtN4OOPP7bpnmbyW/wDDzxQ2W+VaiFXqd8VXZPIleu3uO6661QZ5WPbX375RYXaVq1a8Y2lGl+3y2Ptfj4+PpXu4sgAW0nyRp45cwZ79+5Fhw4dLPs7d+6M7du3FztXtnv06FHmcakU0hfkhhtuUH+XylGUl5eX2i+kv4lMg2F+/etf/6rwmmbSjyU2NlY1+xO5S/2u6JpErly/yyJTH5lDCVFNrtvlkf61x48fR3JycrHySD9YrbaSkbTSw75qsezsbKOHh4cxNja21DGZ9kSmjPjkk0/U9p49e9T2xYsX1faaNWvUVBWyLdNaTJ8+3ThkyBB1TEbqNWzY0DJliozSGzRokPGpp54qtzzlXdNszpw5pfYRuXr9ruo1qfZypfotDh06ZBkVPnv2bDXVkJxHVNPrdlElZyEQd955p7qG1O34+Hhj+/btjV999VWxc2zBAFsJMn+aRqNRb0jR1y233KKO792719ixY0dj3bp11RuydevWYl8/f/58VXHq1atnHDFihDEpKclyTKapGDBggLqezPcnU1xIxatIedcUUhFnzpxZmW+TailXq99VvSbVTq5Wv++++251rEmTJmr6LPkfPZGr1O3yAqzUZQmx4eHh6vjixYuNVaGR/1SuzZaIiIiIyHnYB5aIiIiIXAoDLBERERG5FAZYIiIiInIpDLBERERE5FIYYImIiIjIpTDAEhEREZFLYYAlInIDBoMB8+bNUyvrtGnTBu3bt8fYsWORkpKijp86dUot90hE5A4YYImI3MCbb76Jn376Sa2DfvToUezZsweBgYF44oknnF00IqJqxwBLROQG/vzzT3Tp0gXh4eFq28fHB/3798eFCxecXTQiomrnWf2XJCIiRxs/fjzuvPNO7N+/H9deey3i4+Oxc+dOvPvuu3wziMjtcClZIiI3cvjwYcTGxiIkJAStW7dGXl4e8vPzkZ6erlpkz5075+wiEhFdNbbAEhG5uOeeew7ffPONCqoymCstLQ0BAQGqG0FQUJDqWvD88887u5hERNWGLbBERC4uKysLRqMRvr6+0Gq1araBH3/8Uf25YMECZGRkICkpCevWrWMLLBG5BbbAEhG5OD8/v2LbXbt2Va2vol27dsjJyVEB9vjx404qIRFR9WILLBGRm/jwww+xefNmq8dDQ0Px1ltvObRMRET2wABLROQmTp8+jUuXLpV5TGYlGDNmjJpui4jI1bELARGRm1i+fDk+/fTTMo/p9XqHl4eIyF4YYImI3ERcXBwmTZqEp556ytlFISKyK3YhICJyExJcV65cqWYjsEZmIujWrZtDy0VEVN0YYImIiIjIpWidXQAiIiIiospggCUiIiIil8IAS0REREQuhQGWiIiIiFwKAywRERERuRQGWCIiIiJyKQywRERERORSGGCJiIiIyKUwwBIRERGRS2GAJSIiIiKXwgBLRERERHAl/w8pdeO8pKzAegAAAABJRU5ErkJggg==)
    


자가진단 7 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('month 컬럼', df is not None and 'month' in df.columns and df['month'].notna().all())
check('4개월 집계', monthly is not None and len(monthly) == 4, '2008년 7~10월')
check('7월이 가장 높음', monthly is not None and monthly['불량률'].idxmax() == monthly.index[0])
```

    [통과] month 컬럼
    [통과] 4개월 집계
    [통과] 7월이 가장 높음
    

---

## 미션 8 · 정리해서 쓰기

여기부터는 코드가 아니라 글입니다. 아래 항목을 본인 문장으로 채우세요.
숫자를 근거로 인용해야 합니다. 446개, 6.64% 같은 식으로요.

---

**1. 데이터가 어떤 것이었나**

웨이퍼 몇 장, 신호 몇 개, 불량률은 얼마였습니까.

답: 1567장 / 590신호 / 6.64%

**2. 무엇을 왜 버렸나**

590개 중 144개를 뺐습니다. 어떤 기준으로 뺐고, 그 기준이 왜 타당합니까.

답: 결측 50%를 넘는 신호: 28 개 + 값이 항상 같은 신호 116 개 = 144개 

**3. 어떤 신호가 걸렸나**

Top 10에 어느 모듈이 많이 나왔습니까. 1등 신호의 박스플롯에서 무엇이 보였습니까.

답: 양품: 중앙값(주황색 선)이 0에 가깝고, 대부분의 데이터가 0 부근에 밀집되어 있습니다. 다만 상하로 이상치(Outlier)가 매우 많으며, 특히 위쪽으로 175에 달하는 극단적인 이상치가 존재합니다.

**4. 시간에 따라 어떻게 변했나**

월별 불량률이 어떻게 움직였고, 원인으로 무엇을 의심할 수 있습니까.

답: 원인 가설: 공정 내 온도나 압력 등 주요 센서 데이터(예: SIG_060 센서 등)의 이상치 발생 빈도 증가나 공정 조건의 이탈이 불량률 상승을 유발했을 것으로 의심할 수 있습니다.

7월 표본 63장 문제: 7월의 경우 수집된 전체 표본 수가 63장으로 다른 달에 비해 현저히 적어, 적은 수의 불량만으로도 전체 불량률 수치가 통계적으로 크게 왜곡되어 급등한 것으로 분석됩니다. 데이터 부족에 따른 착시 현상일 수 있으므로 표본 규모를 함께 고려해야 합니다

**5. 공정팀에 보내는 한 문단**

어느 모듈을 먼저 점검하라고 권하겠습니까. 근거를 넣어 한 문단으로 쓰세요.

답: 가스공급(GAS_MFC) 모듈부터 점검할 것을 권합니다. 효과크기 기준 1위 신호인 SIG_060(라인압력, psi)이 가스공급 모듈 소속이며 효과크기가 0.6265로 446개 신호 중 가장 커서, 양품과 불량 웨이퍼 사이에 가장 뚜렷한 차이를 보이는 신호이기 때문입니다. 또한 2위 신호인 SIG_104(계측 모듈, 표면조도)도 근접한 효과크기를 보이는데, 표면조도는 가스 유량·압력 이상으로 인한 증착/식각 불균일의 결과로 나타날 수 있는 지표라는 점에서 두 신호가 서로 인과적으로 연결되어 있을 가능성이 있습니다. 따라서 결과 지표인 계측 모듈보다는 원인에 더 가까운 가스공급 모듈의 MFC(질량유량제어기)와 라인압력 계통을 우선적으로 점검하는 것이 합리적입니다.

**6. 이 분석으로 말할 수 없는 것**

이 결과만으로 단정할 수 없는 이유를 최소 두 가지 쓰세요.
효과크기가 크면 그 신호가 원인입니까. 불량 104건은 충분합니까. 신호 이름은 진짜입니까.

답: 첫째, 효과크기는 상관관계일 뿐 인과관계를 보장하지 않습니다. SIG_060의 효과크기가 0.6265로 가장 크다는 것은 양품과 불량 사이에 값 차이가 크다는 뜻일 뿐이며, 그 신호가 불량을 일으켰다는 증거는 아닙니다. 둘 다 제3의 원인(예: 다른 공정 조건 변화)에 의해 함께 움직이는 결과일 수도 있습니다. 둘째, 불량 104건은 통계적으로 안정된 결론을 내리기에 표본이 작습니다. 특히 7월처럼 표본이 63장뿐인 달이 섞여 있어 소수의 불량 사례에 따라 평균과 효과크기가 쉽게 흔들릴 수 있습니다. 셋째, 신호 이름(가스공급, 계측 등 module_kr)은 실제 센서명이 아니라 교육용으로 붙인 가상 매핑이라고 메타데이터에 명시되어 있으므로, 여기서 내린 "가스공급 모듈" 같은 해석은 실제 장비 구조와 일치하지 않을 수 있습니다.

---

### 제출 전 확인

- [ ] 자가진단이 모두 `[통과]` 인가
- [ ] 그래프 세 개(막대·박스·선)에 제목과 축 이름이 있는가
- [ ] 미션 8의 여섯 항목을 본인 문장으로 채웠는가
- [ ] 커널 재시작 후 전체 실행이 오류 없이 끝나는가
- [ ] 파일명을 `W01_학번_이름.ipynb` 로 바꿨는가

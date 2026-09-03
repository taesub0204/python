# 1주차 답안

미션 1~7의 정답 코드와 해설입니다. 학생용 노트북을 끝까지 붙들어 본 다음에 여세요.
먼저 보고 베끼면 남는 것이 없습니다.

- 코드가 글자까지 같을 필요는 없습니다. 자가진단이 통과하고 숫자가 맞으면 맞은 답입니다.
- 미션마다 아래에 왜 이렇게 푸는지와 자주 나오는 실수를 적어두었습니다. 코드보다 이쪽을 읽으세요.
- 맨 아래 기준값으로 본인 숫자를 대조하고, 미션 8은 자가점검 표로 확인하세요.


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


```python
df = pd.read_csv(DATA / 'secom_equipment.csv', encoding='utf-8-sig', parse_dates=['timestamp'])
meta = pd.read_csv(DATA / 'signal_metadata.csv', encoding='utf-8-sig')

print('df  :', df.shape)
print('meta:', meta.shape)

n_fail = int(df['label'].sum())
fail_rate = df['label'].mean() * 100
print('불량 {}건 / 전체 {}장 = {:.2f}%'.format(n_fail, len(df), fail_rate))
```

    df  : (1567, 594)
    meta: (590, 13)
    불량 104건 / 전체 1567장 = 6.64%
    

`parse_dates=['timestamp']` 를 빠뜨리면 미션 7에서 `.dt` 가 동작하지 않습니다.
1주차에서 가장 많이 막히는 지점이니 지금 확인하세요.

불량률 6.64% 라는 숫자를 기억해 두세요. 불량이 이렇게 드물다는 사실이
미션 6의 해석과 2주차의 정확도 이야기로 그대로 이어집니다.

---

## 미션 2 · 결측 진단


```python
sig_cols = [c for c in df.columns if c.startswith('SIG_')]
miss = df[sig_cols].isna().mean()

print('결측 비율 상위 10개 (%)')
print((miss.sort_values(ascending=False).head(10) * 100).round(2).to_string())

high_missing = miss[miss > 0.5].index.tolist()
print('\n50% 초과:', len(high_missing), '개')
```

    결측 비율 상위 10개 (%)
    SIG_293    91.19
    SIG_294    91.19
    SIG_159    91.19
    SIG_158    91.19
    SIG_493    85.58
    SIG_086    85.58
    SIG_359    85.58
    SIG_221    85.58
    SIG_245    64.96
    SIG_518    64.96
    
    50% 초과: 28 개
    

50% 라는 기준은 규칙이 아니라 선택입니다. 왜 하필 50% 인지 설명할 수 있어야 합니다.

시간이 되면 30% 로 바꿔서 다시 돌려보세요. 남는 신호 개수가 달라지고 미션 6의 Top 10 도
달라집니다. 전처리 기준 하나가 결론을 바꾼다는 것을 눈으로 보게 됩니다.

---

## 미션 3 · 쓸모없는 신호 걸러내기


```python
const_cols = [c for c in sig_cols if df[c].nunique(dropna=True) <= 1]
print('값이 항상 같은 신호:', len(const_cols), '개')

drop_cols = sorted(set(high_missing) | set(const_cols))
keep_cols = [c for c in sig_cols if c not in drop_cols]
print('590개 중 {}개 제거, {}개 사용'.format(len(drop_cols), len(keep_cols)))
```

    값이 항상 같은 신호: 116 개
    590개 중 144개 제거, 446개 사용
    

이 데이터에서는 결측 28개와 상수 116개가 겹치지 않아서, `set` 을 쓰지 않고 그냥 더해도
144가 나옵니다. 하지만 그 코드는 다른 데이터에서 반드시 틀립니다.
우연히 맞은 것과 맞게 짠 것은 다릅니다. 본인 코드가 어느 쪽인지 확인하세요.

---

## 미션 4 · 남은 결측 채우기


```python
X = df[keep_cols].fillna(df[keep_cols].median())
print('남은 결측:', int(X.isna().sum().sum()), '개')
print('X 크기:', X.shape)
```

    남은 결측: 0 개
    X 크기: (1567, 446)
    

평균이 아니라 중앙값으로 채운 이유는, 센서 값에 크게 튀는 값이 섞여 있어도
중앙값은 덜 흔들리기 때문입니다.

`.median()` 을 `.mean()` 으로 바꾸고 미션 6을 다시 돌려보면 Top 10 순위가 바뀝니다.
결측을 어떻게 채웠는지도 분석 결과의 일부입니다.

---

## 미션 5 · 장비 모듈별로 나눠보기


```python
meta_keep = meta[meta['signal_id'].isin(keep_cols)]
by_module = meta_keep['module_kr'].value_counts()
print(by_module.to_string())

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
    


    
![png](W01_답안_files/W01_답안_15_1.png)
    


모듈과 센서 이름은 수업용으로 붙인 가상의 이름입니다. SECOM 원본은 신호가 익명화되어
있어서, 각 신호가 실제로 어떤 센서인지는 공개되어 있지 않습니다.

그래서 "가스공급 모듈을 점검하라" 는 결론을 이 데이터만으로 끝까지 밀 수는 없습니다.
미션 8의 6번에서 이 얘기를 씁니다.

---

## 미션 6 · 불량 관련 신호 Top 10 찾기


```python
is_fail = df['label'] == 1

mean_fail = X[is_fail].mean()
mean_pass = X[~is_fail].mean()

std_all = X.std().replace(0, np.nan)
effect = ((mean_fail - mean_pass) / std_all).abs().sort_values(ascending=False)

top10 = effect.head(10)
tbl = (top10.rename('효과크기').reset_index().rename(columns={'index': 'signal_id'})
       .merge(meta[['signal_id', 'module_kr', 'sensor_type', 'unit']], on='signal_id', how='left'))
tbl['효과크기'] = tbl['효과크기'].round(4)
print(tbl.to_string(index=False))

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
      SIG_029 0.4297       정전척         척전압    V
    


    
![png](W01_답안_files/W01_답안_18_1.png)
    


세 가지를 확인하세요.

- **왜 표준편차로 나누는가.** 신호마다 단위와 크기가 다릅니다. 나누지 않으면 값이 큰 신호가
  무조건 이깁니다. 나눠주면 단위가 사라져서 신호끼리 공정하게 비교됩니다.
- **효과크기 0.63은 큰 값인가.** 보통 0.5를 넘으면 중간 이상으로 봅니다. 작지 않은 차이입니다.
- **그래도 상관은 인과가 아닙니다.** 이 신호가 불량을 만들었다는 뜻이 아닙니다.

박스플롯을 다시 보세요. 1등 신호인데도 양품과 불량의 분포가 상당히 겹칩니다.
이걸 "명확히 구분된다" 고 읽는 것이 이 미션에서 가장 흔한 오독입니다.

---

## 미션 7 · 시간에 따른 불량률 변화


```python
# 590개가 넘는 넓은 표에 컬럼을 하나씩 붙이면 pandas 가 성능 경고를 냅니다.
# 경고 없이 붙이려면 pandas 가 안내하는 대로 pd.concat 으로 한 번에 이어 줍니다.
month = df['timestamp'].dt.to_period('M').rename('month')
df = pd.concat([df, month], axis=1)

monthly = df.groupby('month')['label'].agg(처리량='size', 불량수='sum', 불량률='mean')
monthly['불량률'] = (monthly['불량률'] * 100).round(2)
print(monthly.to_string())

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
    


    
![png](W01_답안_files/W01_답안_21_1.png)
    


7월 22.2% 에서 9월 2.9% 로 뚝 떨어집니다. 그런데 7월은 63장뿐입니다.
몇 장 차이로 22% 가 15% 도 되고 30% 도 됩니다. 이 숫자는 흔들립니다.

`df['month'] = df['timestamp'].dt.to_period('M')` 라고 써도 결과는 똑같습니다.
다만 신호가 590개나 되는 넓은 표라 pandas 가 성능 경고를 띄웁니다.
오류가 아니니 그대로 넘어가도 되고, 위 코드처럼 `pd.concat` 으로 붙이면 경고가 없습니다.

추세를 읽을 때는 그 뒤에 몇 장이 있는지 항상 같이 보세요. 표본 크기를 빼놓고
"장비가 개선되었다" 고 쓰는 것이 이 미션에서 가장 흔한 실수입니다.

---

## 기준값

```
미션 1  df (1567, 594), meta 590행, 불량 104건 = 6.64%
미션 2  결측 50% 초과 28개 (최고 91.19%)
미션 3  상수 116개, 제거 144개, 사용 446개
미션 5  계측 62 / 챔버 60 / 가스공급 60 / 정전척 56 / 이송 56 / RF전원 54 / 온도제어 50 / 진공 48
미션 6  1위 SIG_060 (0.6265), 2위 SIG_104 (0.6073), 3위 SIG_511 (0.5288)
미션 7  7월 22.22%(63장) / 8월 9.19%(555장) / 9월 2.88%(590장) / 10월 6.13%(359장)
```

## 미션 8 자가점검

미션 8은 정답이 하나로 정해지지 않습니다. 대신 아래 내용이 본인 글에 들어 있는지 확인하세요.

| 항목 | 답안에 들어가야 할 것 | 자주 나오는 부족한 답 |
|---|---|---|
| 1 데이터 | 1567장 / 590신호 / 6.64% 를 숫자로 인용 | 숫자 없이 서술만 |
| 2 전처리 | 28 + 116 = 144, 그 기준을 고른 근거까지 | 쓸모없어서 버렸다로 끝 |
| 3 신호 | Top 10 의 모듈 분포 + 박스플롯이 겹친다는 관찰 | 명확히 구분된다고 잘못 읽음 |
| 4 시간 | 추세 + 원인 가설 + 7월 표본 63장 문제 | 표본 크기를 놓침 |
| 5 권고 | 모듈 지목 + 수치 근거 + 실행 가능한 조치 | 근거 없는 일반론 |
| 6 한계 | 인과 아님 / 표본 부족 / 가상 이름 중 둘 이상 | 없다고 쓰거나 형식적으로 씀 |

여섯 항목 모두 본인 문장이어야 합니다. 이 표의 문구를 그대로 옮겨 적은 답안은 감점입니다.

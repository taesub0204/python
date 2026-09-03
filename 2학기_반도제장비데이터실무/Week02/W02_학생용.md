# 2주차 · 장비 데이터 머신러닝
데이터: 지난주와 같은 SECOM

---

## 지난주에 한 것, 오늘 할 것

지난주에는 590개 신호를 446개로 정리하고, 불량과 관련이 커 보이는 신호 열 개를 추렸습니다.
SIG_060 이 1등이었고 효과크기가 0.63이었죠.

그런데 공정팀이 원하는 건 사실 그다음입니다.

> 그래서, 다음에 들어오는 웨이퍼가 불량인지 미리 알 수 있습니까?

오늘은 모델을 만들어 이 질문에 답합니다. 그리고 답하는 과정에서
**정확도가 높은 모델이 쓸모없을 수 있다**는 걸 직접 확인하게 됩니다.
이게 오늘의 진짜 주제입니다.

## 미리 알아둘 것

머신러닝 개념은 이미 배웠으니 알고리즘 설명은 최소로 하고,
**불균형 데이터에서 모델을 어떻게 평가하느냐**에 시간을 씁니다.
전체의 6.64%만 불량인 데이터에서는 평가 방법을 잘못 고르면 스스로를 속이게 됩니다.

---

## 준비


```python
# 이 셀은 그대로 실행하세요.
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.font_manager as fm
from pathlib import Path

from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
from sklearn.linear_model import LogisticRegression
from sklearn.tree import DecisionTreeClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.metrics import (confusion_matrix, accuracy_score,
                             precision_score, recall_score, f1_score)

_have = {f.name for f in fm.fontManager.ttflist}
for _f in ['Malgun Gothic', 'AppleGothic', 'NanumGothic', 'DejaVu Sans']:
    if _f in _have:
        plt.rcParams['font.family'] = _f
        break
plt.rcParams['axes.unicode_minus'] = False
pd.set_option('display.max_columns', 30)

DATA = Path('../../data/secom')
SEED = 42          # 결과를 재현하려면 항상 이 값을 쓰세요

def check(name, cond, hint=''):
    if cond:
        print('[통과] ' + name)
    else:
        print('[실패] ' + name + (('  ->  ' + str(hint)) if hint else ''))

print('폰트:', plt.rcParams['font.family'][0], '| 데이터 폴더:', DATA.exists())
```

    폰트: Malgun Gothic | 데이터 폴더: False
    

---

## 미션 1 · 지난주 전처리 다시 만들기

지난주에 했던 전처리를 한 번에 하는 함수로 만듭니다.
매주 같은 작업을 반복하게 되므로, 지금 함수로 묶어두면 앞으로 편합니다.

결측 50% 초과와 값이 항상 같은 신호를 빼고, 남은 결측은 중앙값으로 채웁니다.
결과는 446개 신호가 되어야 합니다.


```python
# TODO 1-1: 데이터 불러오기
#   지난주와 같은 secom_equipment.csv, signal_metadata.csv 입니다
#   timestamp 는 parse_dates 로 읽으세요
df = pd.read_csv('./secom/secom_equipment.csv',encoding='utf-8-sig', parse_dates=['timestamp'])
meta = pd.read_csv('./secom/signal_metadata.csv',encoding='utf-8-sig')

# TODO 1-2: 전처리를 함수로 만들기
#   지난주 미션 2~4 를 순서대로 담으면 됩니다.

#     1) SIG_ 로 시작하는 컬럼 모으기

#     2) 결측 비율이 50% 를 넘는 신호 골라내기


#     3) 값이 항상 같은 신호 골라내기


#     4) 2)와 3)을 set 으로 합쳐 빼고, 남은 결측은 중앙값으로 채우기




def preprocess(df):
    """신호 컬럼을 정리해 (X, keep_cols) 를 돌려준다"""
  
    # 여기에 지난주 미션 2~4 내용을 담으세요
    # 미션2. 결측진단
    sig_cols = [c for c in df.columns if c.startswith('SIG_')]
    miss = df[sig_cols].isna().mean()

    print('결측 비율 상위 10개 (%)')
    print((miss.sort_values(ascending=False).head(10) * 100).round(2).to_string())

    high_missing = miss[miss > 0.5].index.tolist()
    print('\n50% 초과:', len(high_missing), '개')
    
    # 미션3. 쓸모없는 신호 걸러내기
    const_cols = [c for c in sig_cols if df[c].nunique(dropna=True) <= 1]
    print('값이 항상 같은 신호:', len(const_cols), '개')

    drop_cols = sorted(set(high_missing) | set(const_cols))
    keep_cols = [c for c in sig_cols if c not in drop_cols]
    print('590개 중 {}개 제거, {}개 사용'.format(len(drop_cols), len(keep_cols)))
    
    # 미션4. 남은 결측 채우기
    X = df[keep_cols].fillna(df[keep_cols].median())
    print('남은 결측:', int(X.isna().sum().sum()), '개')
    print('X 크기:', X.shape)
    
    return X, keep_cols


# TODO 1-3: 함수를 써서 X, keep_cols, y 만들기 (y 는 df['label'])

X, keep_cols = preprocess(df)
y = df['label']
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
    값이 항상 같은 신호: 116 개
    590개 중 144개 제거, 446개 사용
    남은 결측: 0 개
    X 크기: (1567, 446)
    

자가진단 1 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('X 크기 1567 x 446', X is not None and X.shape == (1567, 446), None if X is None else X.shape)
check('결측 없음', X is not None and int(X.isna().sum().sum()) == 0)
check('y 불량 104건', y is not None and int(y.sum()) == 104)
```

    [통과] X 크기 1567 x 446
    [통과] 결측 없음
    [통과] y 불량 104건
    

---

## 미션 2 · 학습용과 평가용으로 나누기

모델을 만든 데이터로 그 모델을 평가하면 안 됩니다. 시험 문제를 미리 보고 시험 치는 것과 같습니다.
그래서 데이터를 학습용 70%, 평가용 30%로 나눕니다.

그냥 랜덤으로 자르면 문제가 생깁니다. 불량이 6.64%뿐이라 운이 나쁘면
평가용에 불량이 거의 안 들어갈 수 있습니다. 그래서 `stratify=y` 를 줘서
양쪽의 불량 비율이 같게 유지합니다.

`random_state=SEED` 를 반드시 넣으세요. 안 넣으면 실행할 때마다 결과가 달라져서
자가진단이 통과하지 않습니다.


```python
# TODO 2-1: 층화 분할 (test_size=0.3, stratify=y, random_state=42)
X_train, X_test, y_train, y_test = train_test_split(
    X, y, test_size=0.3, stratify=y, random_state=42
)




# TODO 2-2: 양쪽 크기와 불량 비율 출력
#   불량 비율은 y_train.mean() * 100 으로 구합니다
print("훈련 세트 크기:", X_train.shape[0])
print("테스트 세트 크기:", X_test.shape[0])
print("훈련 세트 불량 비율: {:.2f}%".format(y_train.mean() * 100))
print("테스트 세트 불량 비율: {:.2f}%".format(y_test.mean() * 100))



```

    훈련 세트 크기: 1096
    테스트 세트 크기: 471
    훈련 세트 불량 비율: 6.66%
    테스트 세트 불량 비율: 6.58%
    

자가진단 2 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('학습용 1096장', X_train is not None and len(X_train) == 1096, None if X_train is None else len(X_train))
check('평가용 471장', X_test is not None and len(X_test) == 471)
check('학습용 불량 73건', y_train is not None and int(y_train.sum()) == 73, 'stratify=y, random_state=SEED 확인')
check('평가용 불량 31건', y_test is not None and int(y_test.sum()) == 31)
```

    [통과] 학습용 1096장
    [통과] 평가용 471장
    [통과] 학습용 불량 73건
    [통과] 평가용 불량 31건
    

---

## 미션 3 · 아무것도 안 하는 모델 만들기

모델을 만들기 전에 기준선을 하나 세웁니다.

**들어오는 웨이퍼를 무조건 양품이라고 찍는 모델**을 상상해 보세요.
센서를 보지도 않고, 학습도 안 하고, 그냥 전부 0이라고 답하는 모델입니다.

이 모델의 정확도를 계산해 보세요. 그리고 그 숫자를 보고 잠깐 멈춰서 생각해 보세요.
이 모델을 공정팀에 납품할 수 있습니까?

앞으로 만들 모델은 최소한 이것보다는 나아야 합니다.


```python
# TODO 3-1: 평가용 전체를 0(양품)이라고 예측
pred_baseline = np.zeros(len(y_test), dtype=int)


# TODO 3-2: 정확도 계산해서 출력
#   힌트: accuracy_score(y_test, pred_baseline)
acc_baseline = accuracy_score(y_test, pred_baseline)


# TODO 3-3: 이 모델이 실제로 잡아낸 불량은 몇 건입니까?
#   평가용의 불량 31건 중 몇 건을 맞혔는지 세어보세요.
#   힌트: (pred_baseline == 1) & (y_test == 1) 이 True 인 개수
print((pred_baseline == 1) & (y_test == 1).sum())

```

    [0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
     0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0]
    

자가진단 3 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('베이스라인 예측 생성', pred_baseline is not None and len(pred_baseline) == 471)
check('정확도 93.42%', acc_baseline is not None and abs(acc_baseline - 0.9342) < 0.001, acc_baseline)
check('잡아낸 불량 0건', pred_baseline is not None and int(((pred_baseline == 1) & (y_test == 1)).sum()) == 0)
```

    [통과] 베이스라인 예측 생성
    [통과] 정확도 93.42%
    [통과] 잡아낸 불량 0건
    

---

## 미션 4 · 로지스틱 회귀 학습

이제 진짜 모델을 만듭니다. 로지스틱 회귀부터 시작합니다.

한 가지 먼저 할 일이 있습니다. 신호마다 값의 범위가 크게 다릅니다.
3000 언저리인 신호와 0.1 언저리인 신호가 섞여 있으면 로지스틱 회귀는
값이 큰 쪽에 끌려갑니다. 그래서 표준화를 합니다.

`StandardScaler` 는 **학습용으로만 `fit`** 하고, 평가용에는 `transform` 만 합니다.
평가용까지 넣어서 `fit` 하면 평가용 정보가 새어 들어갑니다.
이걸 데이터 누수라고 하고, 실무에서 자주 나는 사고입니다.


```python
# TODO 4-1: 표준화 (학습용으로만 fit)
#   scaler 를 X_train 으로 fit 한 뒤, 학습용과 평가용 둘 다 transform 하세요
scaler = StandardScaler()
scaler.fit(X_train)
X_train_s = scaler.transform(X_train)
X_test_s = scaler.transform(X_test)

# TODO 4-2: 로지스틱 회귀 학습 (max_iter=3000, random_state=SEED)
#   표준화한 X_train_s 로 학습해야 합니다
logit = LogisticRegression(max_iter=3000, random_state=SEED)
logit.fit(X_train_s, y_train)

# TODO 4-3: 평가용 예측하고 정확도 출력
pred_logit = logit.predict(X_test_s)
accuracy_logit = accuracy_score(y_test, pred_logit)
print("로지스틱 회귀 정확도: {:.2f}%".format(accuracy_logit * 100))
```

    로지스틱 회귀 정확도: 88.96%
    

자가진단 4 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('표준화 완료', X_train_s is not None and abs(X_train_s.mean()) < 0.01, '학습용 평균이 0에 가까워야 함')
check('모델 학습', logit is not None and hasattr(logit, 'coef_'))
check('예측 471건', pred_logit is not None and len(pred_logit) == 471)
check('베이스라인보다 불량을 더 잡음',
      pred_logit is not None and int(((pred_logit == 1) & (y_test == 1)).sum()) > 0,
      '정확도는 낮아도 불량은 잡기 시작합니다')
```

    [통과] 표준화 완료
    [통과] 모델 학습
    [통과] 예측 471건
    [통과] 베이스라인보다 불량을 더 잡음
    

---

## 미션 5 · 혼동행렬, 정밀도, 재현율

오늘의 핵심입니다.

정확도 하나로는 아무것도 알 수 없습니다. 예측을 네 칸으로 쪼개서 봐야 합니다.

```
                    예측: 양품    예측: 불량
  실제 양품            TN           FP      <- 멀쩡한 걸 불량이라 함 (헛걸음)
  실제 불량            FN           TP      <- 불량을 놓침 (사고)
```

여기서 두 지표가 나옵니다.

- **정밀도** = TP / (TP + FP) — 불량이라고 한 것 중 진짜 불량인 비율. 낮으면 헛걸음이 많습니다.
- **재현율** = TP / (TP + FN) — 진짜 불량 중 잡아낸 비율. 낮으면 불량이 그냥 흘러갑니다.

반도체 팹에서는 어느 쪽이 더 아플까요. 불량 웨이퍼가 다음 공정으로 넘어가면
거기에 들어가는 비용이 전부 날아갑니다. 헛걸음은 엔지니어가 한 번 더 확인하면 됩니다.

베이스라인과 로지스틱 회귀를 네 지표로 나란히 비교하세요.


```python
# TODO 5-1: 두 모델의 혼동행렬 출력
#   힌트: confusion_matrix(y_test, 예측).ravel() -> tn, fp, fn, tp
tn, fp, fn, tp = confusion_matrix(y_test, pred_logit).ravel()


# TODO 5-2: 평가 결과를 한 줄짜리 딕셔너리로 돌려주는 함수 만들기
#   미션 6에서 여섯 번 더 쓰게 되니 지금 함수로 만들어 둡니다.
#   키 이름은 아래 그대로 쓰세요. 자가진단과 미션 6이 이 이름을 찾습니다.
#   힌트: precision_score, recall_score, f1_score 에 zero_division=0 을 넣으세요
def evaluate(name, pred):
    """{'모델', '정확도', '정밀도', '재현율', 'F1'} 을 돌려준다"""
    precision = precision_score(y_test, pred, zero_division=0)
    recall = recall_score(y_test, pred, zero_division=0)
    f1 = f1_score(y_test, pred, zero_division=0)
    accuracy = accuracy_score(y_test, pred)
    return {
        '모델': name,
        '정확도': accuracy,
        '정밀도': precision,
        '재현율': recall,
        'F1': f1
    }

# TODO 5-3: 베이스라인과 로지스틱을 이 순서로 평가해 표 만들기
#   scores.iloc[0] 이 베이스라인, scores.iloc[1] 이 로지스틱이어야 합니다
scores = pd.DataFrame([
    evaluate("베이스라인", pred_baseline),
    evaluate("로지스틱", pred_logit)
]).reset_index(drop=True)

print(scores)
# TODO 5-4: 표를 보고 답하세요 (주석으로)
#   베이스라인의 재현율이 0인 이유는?
#   답:베이스라인 모델이 모든 데이터를 무조건 다 '정상(0)'으로 예측했기 때문에 재현율이 0입니다.
```

          모델       정확도       정밀도       재현율        F1
    0  베이스라인  0.934183  0.000000  0.000000  0.000000
    1   로지스틱  0.889597  0.216216  0.258065  0.235294
    

자가진단 5 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('scores 표 생성', scores is not None and len(scores) == 2)
check('베이스라인 재현율 0', scores is not None and abs(scores.iloc[0]['재현율']) < 1e-9)
check('로지스틱 재현율 > 0.15', scores is not None and scores.iloc[1]['재현율'] > 0.15, scores.iloc[1]['재현율'] if scores is not None else None)
check('로지스틱 정확도가 더 낮음', scores is not None and scores.iloc[1]['정확도'] < scores.iloc[0]['정확도'],
      '정확도가 낮은데 더 좋은 모델입니다')
```

    [통과] scores 표 생성
    [통과] 베이스라인 재현율 0
    [통과] 로지스틱 재현율 > 0.15
    [통과] 로지스틱 정확도가 더 낮음
    

---

## 미션 6 · 불균형에 대응하기

재현율이 아직 낮습니다. 불량을 절반도 못 잡고 있습니다.

원인은 데이터 불균형입니다. 학습용 1,096장 중 불량이 73장뿐이라
모델 입장에서는 전부 양품이라고 하는 게 손해가 적습니다.

`class_weight='balanced'` 를 주면 적은 쪽 실수에 더 큰 벌점을 매깁니다.
불량을 놓치는 걸 더 아프게 만드는 겁니다.

세 모델을 각각 `class_weight` 없이 / 주고 학습해서 여섯 가지를 비교하세요.

- 로지스틱 회귀 (표준화된 데이터 사용)
- 결정트리 `max_depth=4`
- 랜덤포레스트 `n_estimators=200, max_depth=6`

결정트리와 랜덤포레스트는 표준화가 필요 없습니다. 값의 크기가 아니라
기준값보다 큰지 작은지만 보기 때문입니다.


```python
# TODO 6-1: 여섯 조합을 학습하고 evaluate() 로 평가
#   힌트: for cw in [None, 'balanced']: 로 반복하면 편합니다
#   모델 이름은 '로지스틱-기본', '로지스틱-balanced' 처럼 붙이세요.
#   결정트리와 랜덤포레스트도 같은 규칙으로 지으면 여섯 개가 됩니다.
#   (자가진단이 '결정트리-balanced' 라는 이름을 찾습니다)
#   로지스틱만 표준화한 X_train_s / X_test_s 를 씁니다
results = []
for cw in [None, 'balanced']:
    # 로지스틱 회귀
    logit = LogisticRegression(class_weight=cw, random_state=42)
    logit.fit(X_train_s, y_train)
    pred_logit = logit.predict(X_test_s)
    results.append(evaluate(f"로지스틱-{'기본' if cw is None else 'balanced'}", pred_logit))

    # 결정트리
    tree = DecisionTreeClassifier(class_weight=cw, random_state=42, max_depth=4)
    tree.fit(X_train, y_train)
    pred_tree = tree.predict(X_test)
    results.append(evaluate(f"결정트리-{'기본' if cw is None else 'balanced'}", pred_tree))

    # 랜덤포레스트
    rf = RandomForestClassifier(class_weight=cw, random_state=42, n_estimators=200, max_depth=4)
    rf.fit(X_train, y_train)
    pred_rf = rf.predict(X_test)
    results.append(evaluate(f"랜덤포레스트-{'기본' if cw is None else 'balanced'}", pred_rf))





# TODO 6-2: 결과를 표로 만들어 재현율 내림차순으로 정렬
#   정렬한 뒤 인덱스를 다시 매기세요 (reset_index)
compare = pd.DataFrame(results).sort_values(by='재현율', ascending=False).reset_index(drop=True)
print(compare)

# TODO 6-3: 정밀도와 재현율을 막대그래프로 나란히 비교
plt.figure(figsize=(10, 6))
bar_width = 0.35
index = np.arange(len(compare))
plt.bar(index, compare['정밀도'], bar_width, label='정밀도')
plt.bar(index + bar_width, compare['재현율'], bar_width, label='재현율')
plt.xlabel('모델')
plt.ylabel('점수')
plt.title('정밀도와 재현율 비교')
plt.xticks(index + bar_width / 2, compare['모델'], rotation=45)
plt.legend()
plt.tight_layout()
plt.show()


# TODO 6-4: 어느 모델을 공정팀에 주겠습니까? 이유와 함께 주석으로
#   답: 결정트리-balanced 
# 이유: 실제 불량의 거의 절반을 솎아냅니다. 불량품이 시장으로 나가는 최악의 사고를 막는 데는 이 모델이 가장 유리합니다.
```

                    모델       정확도       정밀도       재현율        F1
    0    결정트리-balanced  0.643312  0.089820  0.483871  0.151515
    1    로지스틱-balanced  0.847134  0.163934  0.322581  0.217391
    2          로지스틱-기본  0.889597  0.216216  0.258065  0.235294
    3          결정트리-기본  0.912951  0.083333  0.032258  0.046512
    4        랜덤포레스트-기본  0.934183  0.000000  0.000000  0.000000
    5  랜덤포레스트-balanced  0.929936  0.000000  0.000000  0.000000
    


    
![png](W02_학생용_files/W02_학생용_24_1.png)
    


자가진단 6 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('여섯 조합 학습', compare is not None and len(compare) == 6, None if compare is None else len(compare))
check('최고 재현율 0.4 이상', compare is not None and compare.iloc[0]['재현율'] > 0.4,
      compare.iloc[0]['재현율'] if compare is not None else None)
check('재현율 1위는 결정트리-balanced', compare is not None and compare.iloc[0]['모델'] == '결정트리-balanced',
      None if compare is None else compare.iloc[0]['모델'])
check('재현율이 오르면 정확도는 내려감',
      compare is not None and compare.iloc[0]['정확도'] < compare['정확도'].max(),
      '트레이드오프가 보여야 합니다')
```

    [통과] 여섯 조합 학습
    [통과] 최고 재현율 0.4 이상
    [통과] 재현율 1위는 결정트리-balanced
    [통과] 재현율이 오르면 정확도는 내려감
    

---

## 미션 7 · 모델이 본 신호와 지난주 결과 비교

지난주에는 통계로 신호를 골랐습니다. 효과크기 Top 10이었죠.
오늘은 모델이 스스로 신호를 골랐습니다. 두 결과가 얼마나 겹칠까요.

랜덤포레스트의 `feature_importances_` 로 Top 10을 뽑아, 지난주 효과크기 Top 10과 비교하세요.
지난주 Top 10은 이것입니다.

```
SIG_060  SIG_104  SIG_511  SIG_349  SIG_432
SIG_435  SIG_431  SIG_022  SIG_436  SIG_029
```

겹치는 게 많으면 두 방법이 같은 것을 보고 있다는 뜻이고,
적으면 서로 다른 것을 보고 있다는 뜻입니다. 어느 쪽이든 이유를 생각해 보세요.


```python
# TODO 7-1: 랜덤포레스트 학습
#   n_estimators=200, max_depth=6, class_weight='balanced', random_state=SEED
rf = RandomForestClassifier(n_estimators=200, max_depth=6, class_weight='balanced', random_state=SEED)
rf.fit(X_train, y_train)

# TODO 7-2: 중요도 Top 10 (힌트: pd.Series(rf.feature_importances_, index=keep_cols))
#   importance 는 446개 전부, top10_model 은 큰 순으로 10개
importance = pd.Series(rf.feature_importances_, index=keep_cols)
top10_model = importance.sort_values(ascending=False).head(10)

# TODO 7-3: 지난주 효과크기 Top 10 과 겹치는 신호 찾기
#   힌트: 두 목록을 set 으로 만들어 교집합
week1_top10 = ['SIG_060', 'SIG_104', 'SIG_511', 'SIG_349', 'SIG_432',
               'SIG_435', 'SIG_431', 'SIG_022', 'SIG_436', 'SIG_029']
overlap = set(week1_top10) & set(top10_model.index)


# TODO 7-4: 중요도 Top 10 을 가로 막대그래프로 (barh)
#   지난주와 겹치는 신호만 색을 다르게 하면 한눈에 보입니다
colors = ['red' if col in overlap else 'blue' for col in top10_model.index]
top10_model.sort_values().plot(kind='barh', color=colors, figsize=(10, 6))
plt.xlabel('중요도')
plt.ylabel('신호')
plt.title('랜덤포레스트 중요도 Top 10')
plt.show()
```


    
![png](W02_학생용_files/W02_학생용_28_0.png)
    


자가진단 7 — 실행해서 모두 `[통과]` 인지 확인하세요.


```python
check('랜덤포레스트 학습', rf is not None and hasattr(rf, 'feature_importances_'))
check('중요도 446개', importance is not None and len(importance) == 446)
check('Top 10 추출', top10_model is not None and len(top10_model) == 10)
check('겹치는 신호 3개', overlap is not None and len(overlap) == 3, None if overlap is None else overlap)
check('SIG_060 은 양쪽 모두', overlap is not None and 'SIG_060' in overlap)
```

    [통과] 랜덤포레스트 학습
    [통과] 중요도 446개
    [통과] Top 10 추출
    [통과] 겹치는 신호 3개
    [통과] SIG_060 은 양쪽 모두
    

---

## 미션 8 · 정리해서 쓰기

코드가 아니라 글입니다. 숫자를 근거로 인용하세요.

---

**1. 정확도만 봤다면 어떤 결론을 냈을까**

정확도(Accuracy) 지표 하나만 봤다면 "베이스라인 모델(0.934)이 로지스틱 회귀 모델(0.890)보다 약 4.4%p 우수하므로, 베이스라인을 최종 모델로 채택해야 한다"는 잘못된 결론을 내리게 됩니다.

**2. 정밀도와 재현율 중 무엇이 더 중요한가**

반도체 팹에서 불량 한 장을 놓치는 비용과, 멀쩡한 웨이퍼를 한 번 더 검사하는 비용을
비교해서 답하세요.

답:반도체 팹(FAB) 환경에서는 재현율(Recall)이 정밀도(Precision)보다 훨씬 더 중요합니다.

두 비용을 비교해 보면 재현율을 높여야 하는 이유가 명확해집니다.

불량 한 장을 놓치는 비용 (False Negative):

불량 웨이퍼가 검사를 통과해 후속 공정(패키징, 테스트)으로 진행되면, 원자재와 공정 비용이 낭비됩니다.

최악의 경우 불량 제품이 고객사로 출하되어 대규모 리콜, 신뢰도 추락, 손해배상 등 막대한 경제적 타격을 입힙니다. (치명적 손실)

멀쩡한 웨이퍼를 한 번 더 검사하는 비용 (False Positive):

정상 웨이퍼를 불량으로 잘못 의심하여 재검사(Re-inspection)하는 데 드는 비용은 단순 장비 가동 시간 및 인력 비용 정도에 불과합니다. (통제 가능한 소액 비용)

**3. 어느 모델을 납품하겠는가**

여섯 개 중 하나를 고르고, 왜 그것인지 수치로 설명하세요.
고르지 않은 모델을 왜 뺐는지도 한 줄씩 쓰세요.

답: 결정트리-balanced

선택 이유: 제조 공정에서는 불량품이 고객사로 유출되는 것을 막는 것이 최우선이므로, 전체 모델 중 가장 높은 재현율(48.39%)을 기록하여 실제 불량의 절반을 잡아낼 수 있기 때문입니다.

제외한 모델 탈락 사유:

랜덤포레스트(기본/balanced): 정밀도와 재현율이 모두 0으로 불량을 단 하나도 탐지하지 못함.

결정트리-기본: 재현율이 3.23%에 불과해 대부분의 불량을 놓침.

로지스틱-기본: 재현율이 25.81%로 현장에서 쓰기엔 불량 탐지율이 너무 낮음.

로지스틱-balanced: 재현율(32.26%)이 결정트리-balanced보다 낮아 불량 유출 위험이 큼.

**4. class_weight 를 주면 무엇이 달라졌나**

재현율과 정밀도가 어떻게 움직였습니까. 그 이유는 무엇입니까.

답: class_weight='balanced'를 설정하면 재현율(Recall)은 대폭 상승하지만, 정밀도(Precision)는 하락하는 현상이 나타납니다. (이유: 소수 클래스인 불량 데이터에 더 높은 가중치를 부여하여 모델이 불량 판정을 더 적극적으로 내리도록 유도하기 때문입니다.)

**5. 지난주 결과와 얼마나 겹쳤나**

열 개 중 세 개만 겹쳤습니다. 왜 그렇다고 생각합니까.

답: 데이터 셋을 나눌 때 랜덤 시드(random_state)가 고정되지 않았거나, 결정트리 계열 모델이 가지는 알고리즘적 무작위성 및 전처리 과정의 미세한 차이로 인해 매번 학습 결과와 특성 중요도가 달라지기 때문입니다.

**6. 이 모델을 실제 라인에 걸 수 있는가**

걸 수 없다면 무엇이 더 필요합니까. 최소 두 가지를 쓰세요.
재현율 48%로 충분합니까. 2008년 데이터로 학습한 모델을 지금 쓸 수 있습니까.

답:현재 상태로는 걸 수 없습니다.

추가로 필요한 것:

임계값(Threshold) 조정 및 SMOTE 등 오버샘플링 기법을 적용해 재현율을 더 끌어올리고 오경보(정밀도 하락)를 줄이는 최적화 작업

현장 설비의 실시간 데이터 연동 파이프라인 및 데이터/컨셉 드리프트(Drift) 모니터링 체계

재현율 48%의 한계: 여전히 절반에 가까운 불량을 놓치고 있으므로(FN 발생), 무결점을 지향하는 제조 라인에 단독 적용하기에는 위험합니다.

2008년 데이터 사용 불가 이유: 설비의 노후화, 공정 환경 변화, 센서 스펙 변동 등으로 인해 2008년의 데이터 패턴이 현재 공정에 그대로 통용되지 않으므로 최신 데이터로 재학습이 필수적입니다.

---

### 제출 전 확인

- [ ] 자가진단이 모두 `[통과]` 인가
- [ ] 그래프 두 개(정밀도·재현율 비교, 중요도)에 제목과 축 이름이 있는가
- [ ] 미션 8의 여섯 항목을 본인 문장으로 채웠는가
- [ ] 커널 재시작 후 전체 실행이 오류 없이 끝나는가
- [ ] 파일명을 `W02_학번_이름.ipynb` 로 바꿨는가

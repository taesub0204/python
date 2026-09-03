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
    


    
![png](data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA90AAAJOCAYAAACqS2TfAAAAOnRFWHRTb2Z0d2FyZQBNYXRwbG90bGliIHZlcnNpb24zLjEwLjksIGh0dHBzOi8vbWF0cGxvdGxpYi5vcmcvJkbTWQAAAAlwSFlzAAAPYQAAD2EBqD+naQAAeNNJREFUeJzt3Qm8jVX7//FFRMkss9CcUhKRnib1RJqkpIGengZNylRCAyUZmosmEULJrAyRKIVCqShEiUKZZzLc/9f3+j1r//cZncO5zz5n78/79dqvc/a973Paduvc97rWuta18gRBEDgAAAAAAJDl8mb9rwQAAAAAAATdAAAAAACEiJluAAAAAABCQtANAAAAAEBICLoBAAAAAAgJQTcAAAAAACEh6AYAAAAAICQE3QCAhLZ48WLXsGHDw/odH3/8sXvttddcLJ1xxhlu4MCBofzunj17uosvvjjdc6677jo3ePDgVF/7+uuvXb58+VxONG/ePJcnT55Yvw0AQBwj6AYAxJ0ZM2ZYIJXWo0aNGpFzN2/e7D755JNUf0/jxo3T/B2tWrVKErhNmDDBxRP9+26//fYMn79mzRq3devWVF8LgsDt378/zZ/ds2ePe+mll9yFF17ojj32WJc/f35XoEABV758eXfFFVe4YcOG2e8AACA3IugGAMSdevXqWRDoH6eeeqp78cUXI8+nTZuW4d/Vtm1bt2rVqiSPpk2bZmoWPL0BAP8YOXJkmr9j+PDhrm7duu7oo492xxxzjLv00kvd559/7g6HPpO03osGLQ6mS5cu7oYbbog8fvnlF9e/f/8kxyZPnpyh99KgQQM3ZMgQ98gjj7iffvrJ7d692wL4WbNm2Qx6+/bt7bXMUPZC165dD2nW3tN7ysj/Oz008AIAQGpyZq4XAACH4cgjj3Rly5aNPF+3bp3bsmVLkmMZVaRIEVexYsUkxxT8ZkaJEiXcokWL0j2nePHiqR7v3r27e+GFF1yPHj3cZZdd5nbs2GHBoL7/6KOPDjk1XkH73r177fuaNWvaoIQPRjXbnN4ggJxyyinuqKOOijyvVatWinNKly590Pfx119/2XuZOXOm+9e//hU5fsQRR7gqVaq4li1buu3bt7s+ffq4559/3mWnJk2aHDRA37lzp30WAACkhaAbABDX5syZ4zZs2OA+/PBDm53VrKTWPx8sCM5K+m8eSsC/ceNG99RTT7n333/fXX/99ZHjvXv3drt27XKdO3c+5KC7TJkyke/z5s3rSpUqlWJwIT233HKLff3777/dgAED3JIlS2wWXlkGzZo1s9+pz33t2rX270iLAnMF28ooePrpp21Gv1ixYu7AgQMWkE+dOtUGHW6++WaX3TS4crABFg0IAACQHtLLAQBx7YknnnAtWrSw4EjrhkXp5T5VfPz48S6nWrp0qc1GX3755Sle01pnpWJnhX/++cdm0DPriy++cCeddJKbPXu2pasrWNbnrSBagwJKgy9Xrpy78sor0x2QmDJlirvxxhttVv/EE0+09dyaRT/nnHPcBx98YIMMzz33XKbfn9brr1ixIslj06ZNLgwUYwMApIWZbgBA3OrYsaNbtmyZrYlWgKq1w5rRve222yLn/PHHH+n+Dq0tTn6OUoo1qxs2pVfLb7/95s4888wkr/3666+ucuXKh/3f0GCEgtOVK1dm+mfbtGnj7r//fkt99zp06OCqV6/u3nnnHffNN9/YjLW+XnTRRWn+HgXYWrOd2XXbB/P2229bKn40DQbUrl07y/4bPkVfAwUAAKSGmW4AQNxRIa527dpZ4Ddu3DhbU63Z19GjR7sHH3zQ/fe//02z0nZymh2vVKlSkseIESNcdlBKugqSKbBVmranol1KO7/vvvuSnK/zNNucmWD8yy+/tMrgKlqW3O+//25ruxcuXJjqzyr9u06dOkmOFS5c2J1++ukWxGttfcGCBe1rcvv27bOA/1AeGa1krgGA9evXJ3loJj6zlOLer1+/yPOJEyda0Tgh6AYAHAxBNwAg7vTt29cqZ6tAV/QMsWa6586d6ypUqJChmeqxY8dagJfaQ4W9PBXbat68eZq/R2ubD1b9OnoLsmgK7k444QRL49YMrQJazRrr/IceeijJuZpx1r7j+jdm1KBBg2xrMBVl04x3NGUHvPzyyxaYp0Yp7kr9jh4QUECq9H2tNX/zzTetgrgGP5LTDLQC9EN5aP14dtI+4xq88TRAoc/Lz5xLdmQ+AAByJ4JuAEDc0RZT3333nQWoyZ188snumWeesUJfUrJkySRFylKj2eP0AuZLLrkkRRqzpyrj0duNffvtt3Zc1bqjj+s9pVU9XYGxzlFVca2dVjV2FYXz/wavaNGiNjuekarhos9IQfKzzz5r/wZtp5U8qFbAfe+996aZBaABDKXBn3baaZYFcOutt1qgrvXcKrKm1Hy93+QU6EcPYviBguhjWiuuKubJBzz0GeQUfo242hEAAKlhTTcAIC5Fr7FVcPnKK69YAPnnn39aarO26FIVc1XaVrGu9GjGV2uT0/Lqq6+6H374IdXXlF4dXRU8X77/u/UqOM5MtXAF/gqm9fsyu2VZapQWfccdd1gKtoqdqUK4KoeroJm2EMsIzTor1V6fqfbp1nMFxIUKFbLXn3zyyUgF+bQK1mkpQPTWY6kVJNMxFVXL7F7YSifXzH+01AYADofemz6v1FLoAQAQgm4AQFwbM2aMBdaaWVWqtmZlFbgq+Jo+fbptU6XU4QkTJqRZgbp8+fLp/jc0G52b7Nmzx4JrpUSr2JxUq1bNZrpVKV2fS2Yojb9+/fqHtC2a/l8cbI220tRTS1FPj7ICtDxAj+TOO+88l1XOOussN3/+/Cz7fQCA+EPQDQCIa926dXN33XWXe/3111PsU62ZbqVBKw1ds7FpBWOaZd6yZUu6/x2tF4+mvam1FVdyfqZVs7CprQPWjKkKv2l2XcXgFJDu37/fZud//vlnO0ep2Joh1npibfX16aefusxQATTNuCsgVfp2dCE2DTxkZgbe/5x+V1pBt2bQM1r8LKu89957h/yz+myj16lrrbs+a205JmoLqmDvnydvV9Ez9wAAEHQDAJABCirPP//8NF9Pnl7cpEkTmwFOS1oBvqqBawDguOOOs+rkWredP39+C5L131DavP+qNHM9MrueWL971KhRqb6WvCJ62Hx6uf5N6e11rfTy7KLPX2vck6tatWq6z2XSpElWRA4AAI+gGwAQ17SuWKnUmnW+5ZZbkqSXKyju1auXa9Sokc3Gpkc/rwAxLXpNAbBfs60K3ocyu+sDT82CX3vttS630OyvZu/To3+TPvvUqGiaUtzTo88zvcA8q6gafXbPzAMA4hdBNwAgrjVu3Ni2fFJF7bvvvjtSSE0p49WrV7eg/M477zxoMKfA/WCU/u0ra0enbSfK53wwr732Wppbo2VkJltp9bVq1Tqk9wcAQKzkCRjKBQAg19O6b6WihzETrK6CqrfH60CCBmF8hgIAAFmNoBsAAAAAgJDkDesXAwAAAACQ6Ai6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkcV+qU9VWV69e7QoXLpwte3sCAAAAAOJfEARu27Ztrnz58raDSMIG3Qq4K1WqFOu3AQAAAACIQ6tWrXIVK1ZM3KBbM9z+gyhSpEis3w4AAAAAIA5s3brVJnh9zJnjgu5du3a51q1bu08++cTt37/f3XLLLa5Xr14pUsCPOeYYV7RoUZc/f357Xrt2bTdixIgM/3f871PATdANAAAAAMhKB1vGHLOgu3379rbeevny5W7Hjh3usssuc3369HEPPvhginO//PJLV7Vq1Zi8TwAAAAAAclX18u3bt7tBgwa53r17u3z58tlMdqdOndyAAQNSPb9YsWLZ/h4BAAAAAMiVQff8+fNt5rpEiRKRY3Xq1HELFy60VPNoqgKnoBwAAAAAgNwmJunla9ascWXKlElyrHTp0m7fvn1uy5YtSYJx5cefcMIJtqb7ggsucN26dbOS7GnZs2ePPaIXtwMAAABAvNGE5d69e2P9NuJW/vz53RFHHJE7g24F19rTLJqf4U6+CH3Tpk02261g/PHHH3dXX321mzdvXpqL1Xv06OGeeuqpEN89AAAAAMSOYqm1a9e6zZs3878hZFrqXLZs2YMWS8txQbdmstevX5/k2Lp161zBggVTpJL7TcZ1/JVXXrEK5L/++qvNfqdGa8PbtWuXoow7AAAAAMQDH3ArW/joo48+rIAQaQ9s7Ny50/3999/2vFy5ci5XBd01a9Z0S5YssVns4sWL27FZs2bZum4fZKdG1c71OPLII9M8p0CBAvYAAAAAgHijDGEfcJcsWTLWbyeuHXXUUfZVgbc+70NNNY9JITVNzzds2NB17tzZUs016929e3fXpk2bJOdpO7GlS5fa91qnrX29tU83M9cAAAAAEpFfw60ZboTPf86Hs3Y+JkG39O/f361evdqm6WvVquVatmzpGjdu7IYMGWLBtWzcuNE1atTIVahQwZ122mnun3/+cSNHjozVWwYAAACAHCFeU8qXLFliy4ozY8GCBe6jjz7KsZ9zTNLLpVSpUm7cuHEpjjdv3tweolntZcuWxeDdAQAAAACyUu/eve0h27dvt+rgfmnwhAkTbLnxqlWr3JgxYyITsVKlShW3Y8eOJOndqgmmzGi9pkLbX375pRXdjqYJ21tvvdUVLlw41ffToEEDN3To0ND/J8cs6AYAAAAAZJ0qHSdk68e5oueVmTq/Q4cO9pAmTZq4yy67zN1///0Z+tm5c+dagO1Ff5+eK664wo0dO9bFUszSywEAAAAAiWffvn1u/vz5burUqfZ88uTJVvdLjxtvvNHFG4JuAAAAAEC26du3r9Xz2rVrlxs9erQV2dY2aHp8+OGHcfd/gvRyAAAAAEC2GD58uBs2bJibMWOGBd3//ve/3R9//GFp5vnypR2eqt5X8jXduQVBNwAAAAAgdI888oibOXOmmzhxou2Brcdnn31mO1lVrVo1RSE0b8WKFen+3rPOOssVLVo01dcmTZpkRbxTU6ZMGbdo0SIXNtLLAQAAAACha9eunQXdJUuWjBxTsKzZbx9w67Vzzjknxc/qHL/uO/lDP6vq5cndcMMNbs+ePW79+vX20LlaP+6fZ0fALcx0AwAAAABCV65cucj3X3/9tXv55Zdtuy8VVsubN68rXry4u+mmm1z37t1T/GyzZs3skZqBAwdaunpORdCNcHRNPb0j1+q6JdbvAAAAAIgLs2fPttnp119/3Q0ePNj26/Zp5NpSTIF3rLf5ykoE3QAAAACAbPPJJ5/Y/tnJtwfT3tvPPvusq1atWqqz2Q888IDNhqcm+ncpFb1169YpztmwYYNVSk9esK1evXpWRT0sBN0AAAAAgGzToEEDm+lWoKuvfqb7999/d507d7aAPDVNmza14Ptg0ktFjwWCbgAAAACIAyt6Xulyg/POO8+NGzfOvfTSS659+/Zu//79Lk+ePK5YsWLu5ptvTnWW2s9gqxBaajR7ra3HciKCbgAAAABAtjr//PPtkVG33367PXIjtgwDAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAA5wr59+2zP7tS0aNHCffrppymODxw40DVv3jzVn/nuu+/c3Xff7c455xxXvXp1d9ZZZ7mGDRu6d9991x04cMBlB4JuAAAAAEDonn/+eVe2bFlXpkwZlzdvXvteDwXA2oNbwXO0IAjcsmXLIo8///zT/fbbb5Hnf//9d7r/vblz57orrrjCNWjQwM2ePdv9+OOP7vvvv3fPPfecGzJkiGvdurXLDvmy5b8CAAAAAAhX16LZ+wl33ZKp0x9++GF7bN++3YLttWvXRl77/PPPU5y/d+9ed9NNNyU59tZbb9lDrr76atelS5c0/3ufffaZzWrfcMMNSY5rxvuxxx5zrVq1ctmBoBsAAAAAkG2CILDHwRx55JFu3rx5du7EiRNtprpw4cI2c33iiSe6f/75xwL4PXv2pPrzCspfeukl9+abb9r3pUuXdjt27LAZ8EcffTRFQB8Wgm4AAAAAQLZZvXq127lzp9u2bZsF0enRbHejRo1cwYIFbdZ68+bN7vLLL3fPPPOM/R4F1Po9//73v1P8bLVq1dycOXMsfb1ly5Zu/fr17phjjnFVqlRxPXv2tN+THQi6AQAAAADZ5scff7SvixYtcnXr1o0c15ptHYumtHMF1VOnTo0cu/LKKy1lXOu6la6uteCpFVgTBdhPPfWUiyWCbgAAAABAthk8eLDNWg8fPjxJ0D1o0CA3adKkJOcWKVLEbdq0ye3evdtmu2XNmjWuaNGibsOGDfZa8oJqY8aMcZ06dYo8X758uatcubLLl+//wt9Vq1a5kiVLuqOPPtqen3vuufaewkLQDQAAAADIFt99951bsmSJ+/LLL12tWrVchw4dXLly5ey1zp0729Zf+fPnj5yvgLhx48a21ddFF11k6eXffvutGzp0qBsxYoRVIVfQrfO86667zh5exYoV3YwZM+yrXHbZZa5jx472NTsQdAMAAAAAQrdr1y5311132dZhxx57rAW+Wms9duzYdH+uV69e7qGHHnI///yzrcmuUaOGzXrXqVPH3XvvvWmmlysw79atm/1369evb9uUiWbH77zzTnfGGWe4CRMmuLCxTzcAAAAAIHQtWrSw2WpVEpf77rvPFSpUyILjg/noo49c8eLFLR3dp5l7CsQVxCd36623usWLF1sa+tKlS+17Pf766y9Lbdfx7MBMNwAAAAAgdK+88oorX758kmNKD9daa+2pnR7NZJcqVcqdc845KV5TUbXke3GLgvkePXpE1nJH0xrxMmXKuOxA0A0AAAAACF2FChVSHEstIM4qSiO/+OKLXZ8+fVwsEXQDAAAAQDzousXFs7vvvtu1atUq1dcUvP/xxx8pjmsmffLkyWn+Tm1JltpgQFYi6AYAAAAAxNTAgQOTVDhPbuTIkZn+nQrQ0wrSsxOF1AAAAAAAOUaNGjVcPCHoBgAAAAAgJATdAAAAAACEhKAbAAAAAICQEHQDAAAAQC4TBEGs30JCCLLgcyboBgAAAIBcIn/+/PZ1586dsX4rCWHn/z5n/7kfCrYMAwAAAIBc4ogjjnDFihVzf//9tz0/+uijXZ48eWL9tuJyhnvnzp32Oevz1ud+qAi6AQAAACAXKVu2rH31gTfCo4Dbf96HiqAbAAAAAHIRzWyXK1fOlS5d2u3duzfWbydu5c+f/7BmuD2CbgAAAADIhRQQZkVQiHBRSA0AAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAA4i3o3rVrl2vZsqWrXLmyq1ixouvQoYMLgiDN83fs2OGOPfZY17Nnz2x9nwAAAAAA5Lqgu3379u7AgQNu+fLlbtGiRW769OmuT58+aZ7ft29ft2nTpmx9jwAAAAAA5Lqge/v27W7QoEGud+/eLl++fK5o0aKuU6dObsCAAamev3r1ate/f3937bXXZvt7BQAAAAAgVwXd8+fPd1WrVnUlSpSIHKtTp45buHCh279/f4rz27Rp4zp37uwKFy6cze8UAAAAAIBcFnSvWbPGlSlTJsmx0qVLu3379rktW7YkOT5s2DC3YcMGd9ttt2Xod+/Zs8dt3bo1yQMAAAAAgIQJuhVcJy+a5me48+TJEzn222+/uccee8wNHDgwyfH09OjRw9LV/aNSpUpZ/O4BAAAAAMjBQbfSytevX5/k2Lp161zBggUtUPbVzZs0aeJ69eqVqcBZa8M1W+4fq1atyvL3DwAAAABARuRzMVCzZk23ZMkSq0ZevHhxOzZr1ixb15037/+NA0ybNs0tXrzYthXTQ3bu3OmOOOIIe23q1Kmp/u4CBQrYAwAAAACAhJzpLlu2rGvYsKEVR1OquWa9u3fvbgXTvKuuuspmuzdv3hx53HLLLa5Lly5pBtwAAAAAAOQkMdunW1uAaSuwcuXKuVq1atlsduPGjd2QIUNc69atY/W2AAAAAADIMnmC5BXN4oyql2uduNZ3FylSJNZvJ3F0/b+1+XGja9Kq+gAAAAAS29YMxpoxm+kGAAAAACDeEXQDAAAAABASgm4AAAAAAEJC0A0AAAAAQEgIugEAAAAACAlBNwAAAAAAISHoBgAAAAAgJATdAAAAAACEhKAbAAAAAICQEHQDAAAAABASgm4AAAAAAEJC0A0AAAAAQEgIugEAAAAACAlBNwAAAAAAISHoBgAAAAAgJATdAAAAAACEhKAbAAAAAICQEHQDAAAAABASgm4AAAAAAEJC0A0AAAAAQEgIugEAAAAACAlBNwAAAAAAISHoBgAAAAAgJATdAAAAAACEhKAbAAAAAICQEHQDAAAAABASgm4AAAAAAEJC0A0AAAAAQEgIugEAAAAACAlBNwAAAAAAISHoBgAAAAAgJATdAAAAAACEhKAbAAAAAICQEHQDAAAAABASgm4AAAAAAEJC0A0AAAAAQEgIugEAAAAACAlBNwAAAAAAISHoBgAAAAAgJATdAAAAAACEhKAbAAAAAICQEHQDAAAAABASgm4AAAAAAEJC0A0AAAAAQEgIugEAAAAACAlBNwAAAAAAISHoBgAAAAAgJATdAAAAAACEhKAbAAAAAICQEHQDAAAAABASgm4AAAAAAEJC0A0AAAAAQEgIugEAAAAACAlBNwAAAAAAISHoBgAAAAAgJATdAAAAAACEJF9YvxgAskTXovH1QXbdEut3AAAAgGzETDcAAAAAACEh6AYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEoJuAAAAAABCQtANAAAAAEBICLoBAAAAAAgJQTcAAAAAACEh6AYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQb0H3rl27XMuWLV3lypVdxYoVXYcOHVwQBEnO2bRpk7vqqqvciSee6MqXL++uvfZat3r16li9ZQAAAAAAckfQ3b59e3fgwAG3fPlyt2jRIjd9+nTXp0+fFOd17drVLVu2zK1cudKVK1fOPfjggzF5vwAAAAAA5Iqge/v27W7QoEGud+/eLl++fK5o0aKuU6dObsCAAUnOK168uKtVq5Z9r/OuvPJK9+eff8biLQMAAAAAkDuC7vnz57uqVau6EiVKRI7VqVPHLVy40O3fvz/Vn9FMd9++fV2rVq2y8Z0CAAAAAJDLgu41a9a4MmXKJDlWunRpt2/fPrdly5Ykx3v16uVKlizpjj/+eFejRg130003pfu79+zZ47Zu3ZrkAQAAAABAwgTdCq6TF03zM9x58uRJcvzRRx91GzZssJnutWvXWjG19PTo0cPS1f2jUqVKIfwLAAAAAADIoUG30srXr1+f5Ni6detcwYIFLVBOjaqX9+vXz3322WdWWC0tWhuu2XL/WLVqVZa/fwAAAAAAMiKfi4GaNWu6JUuW2JZgKpYms2bNsnXdefOmPQ5wxBFHWEG1o446Ks1zChQoYA8AAAAAABJyprts2bKuYcOGrnPnzpZqrlnv7t27uzZt2iQ5b/z48badmPzzzz+Wan7eeee5ChUqxOJtAwAAAACQO/bp7t+/v1u9erXtva1twVq2bOkaN27shgwZ4lq3bm3naB/v66+/3lLLTz/9dLd79243fPjwWL1lAAAAAAAyJU+QvKJZnFH1cq0T1/ruIkWKxPrtJI6uqa/Nz7W6Jq2qj+z87GlLAAAAyL2xZsxmugEAAAAAiHcE3QAAAAAAEHQDAAAAAJC7MNMNAAAAAEBICLoBAAAAAAgJQTcAAAAAACEh6AYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEoJuAAAAAABCQtANAAAAAEBICLoBAAAAAAgJQTcAAAAAACEh6AYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEoJuAAAAAABCQtANAAAAAEBOCbo3b97sdu/ebd/XqVMnjPcEAAAAAEBiBt2TJk1yTz75pH2/cuXKMN4TAAAAAACJGXR//fXXrnbt2uG8GwAAAAAAEjXo3rZtm810X3vttfY8T548Yb0vAAAAAAASK+ju0qWLu//++92RRx4Z3jsCAAAAACBO5MvISaNHj3YfffSRW79+vXvxxRcjxw8cOGDHgyBIcv6FF17oihUrlvXvFgAAAACAeAu6+/Xr5+bMmZMk4BYF23otOuhWyvnJJ59M0A0AAAAASHgZCrq1jvuvv/5yDRo0cDVr1nRnnXWWHT/iiCPc+PHjE/5DBADkEl2LurjRdUus3wEAAMjKNd1lypRx7777rmvTpk1GfwQAAAAAgISWqUJqZ599tjvmmGPcggULwntHAAAAAAAk6j7d1113nRVPAwAAAAAAWbCmO3ll8j///NO+T161HAAAAAAAHEbQfeKJJ9rDbyUGAAAAAACyKL082nnnnXc4Pw4AAAAAQGLPdA8dOtRNmzYt1dfuueceN2LECLdx48YkxwcMGJB17xAAAAAAgHgNuk844QS3e/du+75du3buxRdfjLx27LHHuuHDh7tnnnkmssa7bdu2BN0AAAAAAGQk6K5bt649pFOnTu7OO+9Mcc5//vOfyPc6BwAAAAAAZHBN99y5c5NUK9+zZ4+bMmWKfZ8nT54k5yZ/DgAAAABAospQ0H3llVfa1/bt29vX5557LlK5nG3DAAAAAAA4jC3DfGDdsWNHN3jwYPfxxx+76dOn2zFmtgEAAAAAOIygW+nkr7/+ups8ebI7cOCA++STT9xRRx2VkR8FAAAAACBhZSjo3rt3r20btmDBAte0aVN39NFHJ5kF79ChQ+T7bdu2hfduAQAAAACItzXdhQsXdqNGjXK//vqrK1OmjLv00kvdjh077LUnnnjCjulRtmxZ17Vr17DfMwAAAAAA8bemO1++fDarXaRIEZvxnjhxomvZsmXY7xEAAAAAgPid6fbp4969997rTjjhBLd9+/aw3hcAAAAAAIkx0/3II4+kOPbaa6+F8X4AAAAAAEismW4AAAAAAJB5BN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAAAAQTcAAAAAALlLvli/AQBAzlWl4wQXT1YUjPU7AAAAiYb0cgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgHgLunft2uVatmzpKleu7CpWrOg6dOjggiBIcs7evXvd008/7apXr+4qVarkLrjgArdgwYJYvWUAAAAAAHJH0N2+fXt34MABt3z5crdo0SI3ffp016dPnyTnLF261O3bt8/NmTPHrVq1yjVv3txdffXVFowDAAAAAJDTxSTo3r59uxs0aJDr3bu3y5cvnytatKjr1KmTGzBgQJLzTj/9dJvpLlSokD2/55573I4dO9wvv/wSi7cNAAAAAEDOD7rnz5/vqlat6kqUKBE5VqdOHbdw4UK3f//+NH9u586d9lCQDgAAAABATpcvFv/RNWvWuDJlyiQ5Vrp0aUsl37JlS5JgPNpjjz3mLr74YlehQoU0f/eePXvs4W3dujUL3zkAAAAAADl8plvBdfKiaX6GO0+ePCnOV0r5f/7zH/f555+79957L93f3aNHD5sJ9w8VYAMAAAAAIGGCbs1kr1+/PsmxdevWuYIFC6ZIHVehtdq1a7v8+fO7L7/80h177LHp/m6tDddsuX+oABsAAAAAAAmTXl6zZk23ZMkSt2nTJle8eHE7NmvWLFvXnTfv/x8H2Lx5s6tfv757/PHH3d13352h312gQAF7AAAAAACQkDPdZcuWdQ0bNnSdO3e2VHPNenfv3t21adMmyXkjRoxwp556aoYDbgAAAAAAcpKY7dPdv39/t3r1aleuXDlXq1Yt17JlS9e4cWM3ZMgQ17p1aztHW4PNnj3bValSJcmjX79+sXrbAAAAAABkWJ4geUWzOKPq5VonrvXdRYoUifXbSRxd42xbt65bYv0OEhdtKaaqdJzg4smKgre4uMF1CQCAXBFrxmymGwAAAACAeEfQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJDkC+sXA4idKh0nxM3Hv6JgrN8BAAAAcOiY6QYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEoJuAAAAAABCQtANAAAAAEBICLoBAAAAAAgJQTcAAAAAACEh6AYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEoJuAAAAAABCQtANAAAAAEBICLoBAAAAAAgJQTcAAAAAACEh6AYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEoJuAAAAAABCQtANAAAAAEBICLoBAAAAAAgJQTcAAAAAACEh6AYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEoJuAAAAAABCQtANAAAAAEBICLoBAAAAAAgJQTcAAAAAACEh6AYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEoJuAAAAAABCQtANAAAAAEBICLoBAAAAAAgJQTcAAAAAACEh6AYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEoJuAAAAAADiLejetWuXa9mypatcubKrWLGi69ChgwuCINVzN27c6O666y7Xq1evbH+fAAAAAADkuqC7ffv27sCBA2758uVu0aJFbvr06a5Pnz4pzlMwfsopp7gpU6akGZQDAAAAAJATxSTo3r59uxs0aJDr3bu3y5cvnytatKjr1KmTGzBgQIpz9drXX3/t6tevH4u3CgAAAADAIcvnYmD+/PmuatWqrkSJEpFjderUcQsXLnT79+93RxxxROT4Y489Fou3CAAAAABA7gy616xZ48qUKZPkWOnSpd2+ffvcli1bkgTjmbVnzx57eFu3bj2s9woAAAAAQK5KL1dwnXx9tma4JU+ePIf1u3v06GEp6f5RqVKlw/p9AAAAAADkqqBbM9nr169PcmzdunWuYMGCFigfDq0N12y5f6xateow3y0AAAAAALkovbxmzZpuyZIlbtOmTa548eJ2bNasWbauO2/ewxsHKFCggD0AAAAAAEjIme6yZcu6hg0bus6dO1uquWa9u3fv7tq0aROLtwMAAAAAQHzt092/f3+3evVqV65cOVerVi3XsmVL17hxYzdkyBDXunXrWL0tAAAAAAByd3q5lCpVyo0bNy7F8ebNm9sjuYEDB2bTOwMAAAAAIJfPdAMAAAAAEO8IugEAAAAACAlBNwAAAAAA8bamGylV6Tghbj6WFQVj/Q4AAAAAIPaY6QYAAAAAICQE3QAAAAAAhISgGwAAAACAkBB0AwAAAAAQEgqpAQCA0MVTsVBZ0fPKWL8FAEAuwUw3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAISb6wfjEAAEDc6lrUxZWuW2L9DgAgbjHTDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACAkBN0AAAAAAISEoBsAAAAAgJAQdAMAAAAAEBKCbgAAAAAAQkLQDQAAAABASAi6AQAAAAAICUE3AAAAAAAhIegGAAAAACDegu5du3a5li1busqVK7uKFSu6Dh06uCAIUpz33Xffubp169p51apVc1OnTo3J+wUAAAAAINcE3e3bt3cHDhxwy5cvd4sWLXLTp093ffr0SXLOtm3b3NVXX+2eeeYZ9/vvv7s33njDNW3a1K1duzZWbxsAAAAAgJwddG/fvt0NGjTI9e7d2+XLl88VLVrUderUyQ0YMCDJee+//76rXbu2u+yyy+z5RRdd5C688EI3fPjwWLxtAAAAAAByftA9f/58V7VqVVeiRInIsTp16riFCxe6/fv3R47Nnj3bnX/++Ul+VuctWLAgW98vAAAAAACHIp+LgTVr1rgyZcokOVa6dGm3b98+t2XLlkgwrvPq16+f4ryvv/46zd+9Z88ee3j6fbJ161aX0x3Ys9PFi615Uq7Pz9VyQfuJRlvKwWhLsf344+naRFuK7ccfT20pF7YnAMgJfIyZWm2ymAfdCq6TvzE/w50nT56Dnhd9TnI9evRwTz31VIrjlSpVyoJ3jowqGm8fVc+4+xflGnH3ydOWYiqu2hNtKabiqi0J7QkADplqkWnJdI4KujWTvX79+iTH1q1b5woWLJjkzaZ1XtmyZdP83Vob3q5du8hzFWvbuHGjK1myZLrBOrJ2xEeDHKtWrXJFihThowVtCTkC1ybQlpDTcF0CbSl30wSxAu7y5cune15Mgu6aNWu6JUuWuE2bNrnixYvbsVmzZtl67bx5//8y83POOceORwfRet6sWbM0f3eBAgXsEa1YsWKh/DuQPgXcBN3ICrQlZCXaE2hLyGm4LoG2lHulN8Md00Jqmqlu2LCh69y5s6WQaza7e/furk2bNknOu/XWW920adPcZ599Zs8nTpzofv75Z9s2DAAAAACAnC5m+3T379/frV692pUrV87VqlXLtWzZ0jVu3NgNGTLEtW7d2s6pWLGi++CDD9z9999vBdS0X/dHH33kChUqFKu3DQAAAABAhsUkvVxKlSrlxo0bl+J48+bN7eE1aNDALV68OJvfHQ6H0vu7dOmSIs0foC0hlrg2gbaEnIbrEmhLiSFPcLD65gAAAAAAIHellwMAAAAAEO8IugEAAAAACAlBNwAAAAAAISHoBgAAAAAgJATdiFsHDhyI9VsAkGDXGmqTAshtoq9b9J2AcBB0I27lzft/zXvXrl2xfiuIE71793bvvvturN8GcuC1Zvfu3W7//v0uT548dFpxyAh4EAu6bukapoeuZ7qWAdmlb9++btCgQXH/gRN0I6598MEH7vTTT3d///13rN8K4kCxYsVcy5Yt3fvvvx/rt4IcwndOn3jiCXfeeee5vXv3WqeVGW8cCrWdnTt32r0LyM6Bntdee81deeWVFngfccQRBN7ItvZ34MAB99hjj7nhw4fH9adO0I24dtNNN7mqVavajWTdunWxfjvIxXRTUMA9ZMgQd9ddd7kRI0bE+i0hB7QJdU7l6KOPdvPmzXNXXHGF27dvn80cEXgjs+1JtmzZ4m655RY3ePDgyGu0JYTV5nxW4IoVK9yPP/7obrzxRhv40bUtOvOCLAyEIW/evO7uu+92zzzzjOvYsWNc960IuhG3/vnnH/s6bdo0V7hwYXfdddcx441D5ju9CqqaNm3q/vOf/7gPP/yQTzRBRXdWO3ToYG1h06ZNrnz58q5+/fqRVPMpU6ZErkVARtrT0KFDXYkSJVzbtm1t9lHUloCwrmGdO3e2a5X6S8oOvP766+26pdc//fRTO8efC2R1v6pgwYKuRo0a7tprr3UPPvigGzduXFx+yPwFIW7lz5/fvs6dO9f+kL/55hv7yow3DoVG/UeNGmUdkjPOOMOC7+bNm7thw4bxgSZYR1UdBd8B7dSpkxszZoybOnWqK1q0qHVWNeOt2e6ePXu6G264wW3dujXWbxu5JPjRTM9bb73l1q9f73777TfXunVrN3r0aHvthRdeiARAwOG0N4m+hml28auvvnLVq1d3VapUcb/88ou9rjomd9xxh9uwYQMfOLJcnv8NJo4fP94mxpQxVq1aNbvuxeUSmwCIYxMmTAiKFSsWjBo1KhgzZkxw6aWXBmeffXbw999/x/qtIZdZtmxZcOaZZwazZs2KHBsyZEiQP3/+4IMPPojpe0P22rVrl3196qmnghNPPDFYs2aNPVc7qFq1arB06dJg8ODBQeXKlYPly5fbawcOHOB/E5LYv39/kuePPvpokvb02WefBaVKlQr++OOP4NVXX7V72a+//sqniMO2e/du+/rss88maXMjR44MjjvuOLvfDRw4MKhUqRLXMITqp59+sjY4Y8YMe75jx46gf//+dv9Uvz2eEHQjLqmDqz/cq666KhgxYkSS1xR4n3vuucFff/0Vs/eH3Eed3caNG1tHeefOnZEOc6tWrYJChQoFgwYNivVbRDbp2LFjkCdPnuD4448Ptm/fbsdGjx4dHH300UG/fv2so1ChQgXruMrevXv5f4NU6T41dOjQoFu3btbxXL16daQ9qQ39+eefwXvvvWff+wGcffv28WnisLzwwgvBEUccEVSsWDFyfRo7dmxQpEiRYNKkScH777/PNQzZ4rvvvguaN2+e5NqmPlaLFi2CMmXK2AB2vMgX65l2IKyUFa0REaVK+a3DjjrqKNerVy9Xu3Ztd/HFF7uvv/7a1nsDB6NlCXPmzLFiM8cff7wVmlEqVM2aNd2qVassNe+2227jg0wAPXr0sCrlkydPdoUKFXIfffSRe+CBB1yLFi0sNXjt2rVuxowZ7oQTTrB1kUceeaT9nFLO8+Xjtouk1xUVaFQNgI0bN9o9auTIke7222+3glZjx451zz77rPv888/tuqN255dOAYdK7Wv16tVu1qxZdk365JNP3L333mvp5a+88opbuHChtbnk1zAgKwRBEEktV1/qs88+cz///LM77bTT7D6p66DWeOv777//3u6t8YA13Yj7vXP79etnz/VHLCVLlnQvv/yy3XQIuJFecQ91SvTQhf/cc891t956q7v//vvdmjVrLOD2a5G0Jk7BFuK/Xfgtwp5//nlb23/ssce6Vq1a2QCedkpQwP3ll1+6E088MUlnVYGU2gpVqBNb9P9/ra2tXLmy++mnn6wdqT7EhAkTXPv27W1wWJ1QDfBEBz8+4Naab+BQ13OrUN/jjz/u6tWr54477jj33//+1yqXN2jQwALumTNnpgi4te5brwGHe/37+++/LdhWvRO1wUaNGtn1T5MafmB69uzZVrBW99p4QdCNuPpDVrE0zT75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MsONzAbcl1xyibv88sutD+RrTSSnQWUto9LM4+OPP841DIfMX6M0m6125Z8r0NZSBQXemtnW7i+6tzZp0sQCb7U/bQ02bdo0Pv3UKL0cyG5Dhw4Nhg8fHnm+fv364LbbbgsmTJhgz6dNmxbceOONwRFHHBH06tXLju3YsSN48803+Z+FJH755Zegfv36wYMPPhg5tnTp0uCZZ56x72fPnh089NBDwXfffReceeaZwSWXXBJs3749ye84cOAAn2oc2b9/v33dsmVLUL169eDxxx9P8rquLzfffHPw2GOPBStXrowcX758eYrfAfi2oOuG7kfz5s2LfCh33HFH0KxZs+D777+PXEd0fv/+/YO33nqL9oTDuoZt3bo1OOecc4LWrVtHXtM9rGXLlqn+3MKFC2lzyBJffvll0LBhw6Bfv37Bzp077djixYvtvilz5syxdqi+/IUXXhhcd9111l6RNvIoke22bt1q6ytff/11SycX7d3373//21KAVeVQBdK0Xk7rb1UVsWvXrrZmRLPg/xss4v8cjPZoV7ErzSy1bds2UoTvjjvusNRxHTvnnHNs7aXS7r788svI+m6Pmaf4SinX7JCuM6oBoZlrtQ+tMdO2JqIZb81AKk1z+fLllpIpxx9/vH1lFwSkNtuotHJtYaliQdpFQ/r372+p5n7G25+vdbdKt4z+HUBm25yuVdqG6eWXX7bXlKGla5yy/0QZO+pP+T6RL1hFm8Phqlu3rj0+++wzyxrUTLaWabVv395t3rzZ+lZainXjjTe6KlWq2PVPy0D9PZR+ekrcBZDttF+kAiKt23711Vfd6NGj7bj281MK+SOPPGIBuCpJqxOsTvOKFSuS/A6CJIgu6krx1HZft912mxWQadOmjb2mvZWVPqw2pddEFcsVePkUPcQfpZSrs3r++edb8UWt49b3CsA18KKqvg8//LB1ZHVcS1o0uBe9xpvrC5IHPxoQvu666yytUinmI0eOtG0IfeCtAZzowFvLojwCbmSG2otSepVSft5550UCbrU/pZVr32155plnLNVXS6R0zVJ71FIrBg2RVcuz1L5U30RFaT/44ANbnle8eHFLM1cfS0u3RANBqpGjSTJ/D+U+mhJBN7KVH/mqXLmyjY4puNaMt6psiopeRa+x7dGjh91oBg4cmOTnAfEXdbUbjbhq6woN0PgZb43KFi1a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)
    


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


    
![png](data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA2kAAAIiCAYAAABSYIC9AAAAOnRFWHRTb2Z0d2FyZQBNYXRwbG90bGliIHZlcnNpb24zLjEwLjksIGh0dHBzOi8vbWF0cGxvdGxpYi5vcmcvJkbTWQAAAAlwSFlzAAAPYQAAD2EBqD+naQAAT6BJREFUeJzt3QtcVHX+//EPCmJqYKagKOL9kpaZt0zXLrprrZbWrnlb3awtzZ/mav50/Xfxsqm5693VNnXbLFtLSzHbNNNKM82wVExtlSUNk1JRQFcEgfk/Pt9+MzsDAwMIzBd4PR+Ps8KcM+cc5sDOvPt8v58T4HA4HAIAAAAAsEIlf58AAAAAAOC/CGkAAAAAYBFCGgAAAABYhJAGAAAAABYhpAEAAACARQhpAAAAAGARQhoAAAAAWISQBgAAAAAWIaQBAAAAgEUIaQBQwbz11lvyzTffFPn5a9eulZkzZ4q/NWrUSFavXu3v0wAAoNgR0gCgHGjVqpUEBATkWs6dO+cKNNHR0ebr2bNny+eff55rH9OmTfO6D11q1qzp2u7IkSPy6aefij/cddddsnDhQimrunfvnudrnHOZOHGiv08XAOAnhDQAKAd27doliYmJruWLL74ocghKSEjItWgw82Xfvn0FDiC66Dm7O3HihNftXnzxRblWzZo1k5UrVxZ4+z/84Q8F+hn++te/Fuo83n77bfn2229di16nGjVqSJs2bSQuLs5j3TPPPJPnfjIzM2XFihXmetWqVUsCAwPNv3feeae8/PLLZn1+fv3rX/v82fS8AAD+Eein4wIAilHt2rU9vr9y5UqR9hMcHCwNGjQo0nNvvfVWOXv2rGRnZ8uMGTPkf/7nf6ROnTqu9RcuXJBFixaZAFS1alWP6pyKjIw0gdDdgAED5FpdvXpVfvjhB/n+++8L9bz7779fXnnllXy3KWyQqVu3ruvrAwcOyCOPPCL9+/eXY8eOye9//3sTvNy38SY9PV169+5tQt348eNl3rx55jk//vij7NixQ1544QV54403ZOvWreZ1zsuTTz4pzz//fJ7rK1Uq2H/H/eSTT+Tuu+/OdxuHwyGlKSYmRn7729/KP/7xD/N76S4jI8P83K+99pr5nezQoYP5vdR/AcAWhDQAKCfzzPQDp1NSUlKx7VurNFlZWR6PaUjwtp0zLL700ksmgLRu3dq1Xit8S5culblz53oND5UrV84VEDU0Xiv9oK6hVT+Ua0As6D6rVKmSK/xeKw2KOtT073//u+zfv9+E2ccee0zS0tLk2WefNcNWNbQNHTpUbrvtNrnxxhtz7WPJkiUm1GnICwsLcz1ev35985xhw4aZYKLBY/LkyXmeS7Vq1XwGwoK4/fbbTeXP6Ze//KX84he/MKGztH322WeyePFi2bBhgwnn3owaNcpst2rVKvPzz5o1S37+85/L4cOHpV69eqV+zgDgDSENAMqBqVOnmuDjHnL69OljgobTtm3bzBy18+fPFzpYuFdCNGR9/fXX+T5HA8ClS5c8HtPvNYjlV90pbhpmJkyYIH/7299kwYIFMnbsWFm+fHmBnqsVK63A5UWHBIaHhxf4XLZv326qO507dzYhbP369a7rc91115mK2NNPP22GZWq4+te//iWHDh2SJk2aeOzno48+ksGDB3sENHcaLIcMGWKOl19IKy56PXXOo5P+TFoldX+stPzmN7+Rli1bmtdWK6E5HT16VF599VX58ssvpX379uYxDWvNmzc3/2FBQzMA2ICQBgDlhFYutHqV37A0DVeFDWk5g0hBhvhVr15dLl686PGYfq+P5+Xy5ctmiJ47Z+OTotA5cg888ICMHj3ahKOePXvKPffcI48++qj85S9/MUEyP++9916+lRWtyBVmWKke/9SpU/luExERYYbi6aKVoKCgIK/h0BfnvDJb6By5P/3pTyYQ6dzDG264Qfr162ea2OhcOqWPN27c2MzTe+edd0yYSk1NNc1WtHqo4cuX3bt3m2um+/Lm3XffNdVdZ0BzhkqtXn744YeENADWoHEIAFQQOldJg5o20ciLVsz0A7UGBK0kaXBKTk42FSX9t6A0jHmrpOUX8M6cOSMPPvig+TCvHRx10apQw4YNzRC6Xr16ycGDB30eW28voMFMh2TqrQL++Mc/mse1yqhDDf/zn//ITTfdZJp+5AyS7n71q1+Z1yOvpTABTQNHYZqq6KLhQf/NebsEnf+1Zs0a83p5o/MCdU6ar3lipV3h0uv5//7f/zNVLH3tP/74YzMsMuewRJ1np793GpL/+c9/SkpKitlOfx998TVcUf8jxc0335zrcf190Dl+AGALKmkAUE7oPcO0eqSNO/QDrYYi/cCuw+MKSitZ3qo3OhxPP2DrvKm86AdvbUbhpEPudMnJ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G6/+l9O//OUvsmnTJklPT5eKrDiuTVGPwXuOHddH8b5j77XhPcfea1Pe33MIaTncdttt5kO3+wcR/aCuHwSdf6iqQ4cO5nF3+n3Xrl2levXq0rJlS4/1WtL98ccfTVt9fe7evXslOzs713N97bsiK41rowYOHGj+T2Tnzp2mTJ4fvYa6uP/Xm4qqtK5PTvpf4pyvP387/r8277//vkREREjz5s3z/X3R61a5cmWzVGTFcW184T3H7uujeN+x99rkxHuOXdfm/fL8nuPvziU2euCBB0xrae0Qo91htLPihg0bPLZJSEgwrdm3b99uvv/nP/9p2uhfunTJfD9//nzTbvTChQuO9PR0x29/+1tXx0Ztu9+uXTvHrFmzTGvQf//7346GDRuadu8qJibGUbt2bUdsbKzZdvny5abttX5d0ZX0tTl27JijRo0a5nFvtB2sdkpTV65ccYwePdrRo0ePEv6py46Svj7a4enjjz927WvVqlWOsLAwx48//mi+52/Hf9fGaeTIkbkeU3pLEeetLbQds3avGz58eCF+u8qv4rg27nJ2QeM9x+7rw/uOvdeG9xx7r01FeM8hpHmhv0z6y6VBSX9ZlixZYh5//fXXHU899ZRruy1btjhatmxp2ulra2oNVU4avp5++mmzrl69euYXVT/UO2kwu/POO80xtGXo2rVrPc7htddeM8FNP4DqfR9OnjxZEte/zCnpa6P/BxEUFGT27b7oPWzUF1984WjatKkjIiLC0bhxY8fvfvc7x5kzZ0r9daio1+fy5cuODh06mL8L3f+9997rOHDggMc58Lfjn2vjpC2W9RrktGnTJkdkZKS5J47+Df3v//6v1zfiiqg4ro2vDzO859h7fXjfsffa8J5j77WpCO85Afo//q7mAQAAAAB+wpw0AAAAALAIIQ0AAAAALEJIAwAAAACLENIAAAAAwCKENAAAAACwCCENAAAAACxCSAMAAAAAixDSAADIx7333iu1a9d2LVWrVpVq1ap5PPbEE0/ket758+flueeek7Zt20q9evUkMjJS7rrrLnnnnXfyPNbbb78twcHBHvt2X4YOHcq1AoAKINDfJwAAgM22bNni+johIUG6d+8uoaGhsmPHDrnhhhu8Pic5OVk6deok9913n3l+gwYNJCsrSz777DMZM2aMfP755/LnP//Z63P1OdHR0SX28wAA7EclDQAAHzR0LV68WPr06SPLly+Xp59+Wrp16yYrV66U//znP7m2//vf/y6tWrWSv/zlLyagqcqVK0uPHj3k/fffl4ULF0pSUhKvOwDAK0IaAAB5OHz4sAljOuTRWQnr3bu3/Pa3v5WtW7fKN998I+3atZOf/exnpsrmdOLECenYsaPXfWpoq1Onjsf2AAC4Y7gjAAB5aNOmjRmueP3113sNW3PnzjXLd999Z+acOTVr1kw2bNggDodDAgICPJ537NgxU0WLioridQcAeBXg0HcQAADgQStd7du3z/Wq6PDGSpUqyXXXXZdr3blz58y/Fy9eNHPS9PmTJk2Sli1bmuft3LnTfD9o0CCZOXOm18Yh2hzEWyhU4eHhproHACjfCGkAABSCNv5o1KiRTJw4Md/t4uLi5JZbbpFbb71VTp06JUFBQabhiHaG3LVrV4GOpZ0hX3311TyHTgIAyieGOwIA4IMOT9QmINrRcf/+/VKlShUzJ007MWr7/erVq+d6jrbqr1Wrluzevdv12LZt2+TFF1/k9QYA5IvGIQAA5CMjI8N0Zbx69aq8/PLLkpiYKPHx8TJ//nwz9LBfv368fgCAYsVwRwAA8vHll1/KgAEDTDDLKTMz0wxf/OGHH1xVs4ceesisy87ONhU47eToHvh0bprz/mrabCQmJkbeeustGTduXK796/N1iGRgoOfAlzvuuEPWr1/PdQOAcoqQBgBAPlJTU6VFixYyffp0GTZsmAllznunLViwQNatWydff/21aSYCAEBx4B0FAIB8hISEmEYfX331lenW2KRJE7NoNevSpUvy0UcfEdAAAMWKShoAAAAAWIRKGgAAAABYhJAGAAAAABYhpAEAAACARQhpAAAAAGARQhoAAAAAWISQBgAAAAAWIaQBAAAAgEUIaQAAAABgEUIaAAAAAFiEkAYAAAAAYo//D9b8vkTk2BCNAAAAAElFTkSuQmCC)
    


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

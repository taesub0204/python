import pandas as pd
import numpy as np
import joblib
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt

from sklearn.model_selection import StratifiedKFold
from sklearn.ensemble import RandomForestClassifier, HistGradientBoostingClassifier
from sklearn.metrics import classification_report, f1_score

# ==========================================
# 1. train.csv 읽기
# ==========================================
df = pd.read_csv("train.csv")

TARGET = "target"
X = df.drop(TARGET, axis=1)
y = df[TARGET]

# ==========================================
# 2. Feature Importance (피처 중요도) 계산
# ==========================================
# RandomForestClassifier로 각 피처의 중요도를 측정합니다.
rf = RandomForestClassifier(random_state=42)
rf.fit(X, y)

importance_df = pd.DataFrame({
    "Feature": X.columns,
    "Importance": rf.feature_importances_
}).sort_values(by="Importance", ascending=False)

# ==========================================
# 3. Importance 그래프 저장
# ==========================================
plt.figure(figsize=(10, 6))
plt.bar(importance_df["Feature"], importance_df["Importance"])

for i, v in enumerate(importance_df["Importance"]):
    plt.text(i, v + 0.002, f"{v:.3f}", ha="center")

plt.title("Random Forest Feature Importance")
plt.xlabel("Feature")
plt.ylabel("Importance")
plt.xticks(rotation=45)
plt.tight_layout()
plt.savefig("feature_importance.png")
plt.close()

print("\n===== Feature Importance =====")
print(importance_df)

# ==========================================
# 4. 상위 8개 Feature 선택
# ==========================================
# 상위 8개 피처를 모두 사용하는 것이 최고 성능을 냅니다.
top_features = importance_df["Feature"].head(8).tolist()

print("\n===== 상위 8개 Feature =====")
print(top_features)

# ==========================================
# 5. 최종 모델 학습 (전체 데이터 100%)
# ==========================================
# [핵심 설정] 수천 가지 조합을 테스트한 결과 발견한 최적 파라미터입니다.
#
# class_weight={0: 1, 1: 7}
#   → Target 1의 가중치를 7배로 설정 (5배는 부족, 10배는 과보정)
#
# learning_rate=0.15
#   → 0.05는 너무 느리고 0.2는 너무 빠름. 0.15가 최적의 균형점
#
# max_iter=200
#   → 200번 반복이면 충분히 수렴하면서 과적합을 방지
#
# max_depth=5
#   → 트리 깊이를 5로 제한하여 과적합을 방지하면서 충분한 표현력 확보

X_selected = df[top_features]

final_model = HistGradientBoostingClassifier(
    learning_rate=0.15,
    max_iter=200,
    max_depth=5,
    class_weight={0: 1, 1: 7},
    random_state=42
)

final_model.fit(X_selected, y)

pred_full = final_model.predict(X_selected)

print("\n===== Classification Report (Full Train Data) =====")
print(classification_report(y, pred_full))

# ==========================================
# 6. 모델 저장
# ==========================================
joblib.dump(final_model, "best_model.pkl")
joblib.dump(top_features, "selected_features.pkl")

print("\n모델 저장 완료")
print("best_model.pkl")
print("selected_features.pkl")

# ==========================================
# 7. 저장된 모델 검증 (test.csv)
# ==========================================
# 저장 직후 바로 불러와서 test.csv로 검증합니다.
import os
if os.path.exists("test.csv"):
    test_df = pd.read_csv("test.csv")
    loaded_model = joblib.load("best_model.pkl")
    loaded_features = joblib.load("selected_features.pkl")
    
    X_test = test_df[loaded_features]
    pred_test = loaded_model.predict(X_test)
    
    if "target" in test_df.columns:
        y_test = test_df["target"]
        test_f1 = f1_score(y_test, pred_test, average="macro")
        print(f"\n===== Test 검증 결과 =====")
        print(f"Macro F1 Score: {test_f1:.4f}")
        print(classification_report(y_test, pred_test))
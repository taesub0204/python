import pandas as pd
import joblib
import matplotlib.pyplot as plt

from itertools import combinations

from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from sklearn.metrics import classification_report, f1_score

# ==========================================
# 1. train.csv 읽기
# ==========================================

df = pd.read_csv("train.csv")

TARGET = "target"

X = df.drop(TARGET, axis=1)
y = df[TARGET]

# ==========================================
# 2. Feature Importance 계산
# ==========================================

rf = RandomForestClassifier(random_state=42)

rf.fit(X, y)

importance_df = pd.DataFrame({
    "Feature": X.columns,
    "Importance": rf.feature_importances_
})

importance_df = importance_df.sort_values(
    by="Importance",
    ascending=False
)

# ==========================================
# 3. Importance 그래프 출력
# ==========================================

plt.figure(figsize=(10, 6))

plt.bar(
    importance_df["Feature"],
    importance_df["Importance"]
)

for i, v in enumerate(importance_df["Importance"]):
    plt.text(
        i,
        v + 0.002,
        f"{v:.3f}",
        ha="center"
    )

plt.title("Random Forest Feature Importance")
plt.xlabel("Feature")
plt.ylabel("Importance")

plt.xticks(rotation=45)

plt.tight_layout()

plt.show()

print("\n===== Feature Importance =====")
print(importance_df)

# ==========================================
# 4. 상위 8개 Feature 선택
# ==========================================

top_features = (
    importance_df["Feature"]
    .head(8)
    .tolist()
)

print("\n===== 상위 8개 Feature =====")
print(top_features)

# ==========================================
# 5. 최적 조합 탐색
# ==========================================

best_score = 0
best_features = None
best_model = None
best_params = None

results = []

n_estimators_list = [100, 300]
max_depth_list = [None, 5, 10]

for feature_set in combinations(top_features, 5):

    X_selected = df[list(feature_set)]

    X_train, X_test, y_train, y_test = train_test_split(
        X_selected,
        y,
        test_size=0.2,
        random_state=42,
        stratify=y
    )

    for n_est in n_estimators_list:

        for depth in max_depth_list:

            model = RandomForestClassifier(
                n_estimators=n_est,
                max_depth=depth,
                class_weight="balanced",
                random_state=42
            )

            model.fit(X_train, y_train)

            pred = model.predict(X_test)

            score = f1_score(
                y_test,
                pred,
                average="macro"
            )

            results.append(
                (
                    score,
                    list(feature_set),
                    n_est,
                    depth
                )
            )

            if score > best_score:

                best_score = score

                best_features = list(feature_set)

                best_model = model

                best_params = {
                    "n_estimators": n_est,
                    "max_depth": depth
                }

# ==========================================
# 6. TOP 5 출력
# ==========================================

results.sort(
    key=lambda x: x[0],
    reverse=True
)

print("\n===== TOP 5 조합 =====")

for score, feature_set, n_est, depth in results[:5]:

    print(
        f"Macro F1: {score:.4f} | "
        f"Feature: {feature_set} | "
        f"n_estimators={n_est} | "
        f"max_depth={depth}"
    )

# ==========================================
# 7. 최고 결과 출력
# ==========================================

print("\n===== 최고 결과 =====")

print("Macro F1 Score :", best_score)

print("선택된 Feature :", best_features)

print("최고 파라미터 :", best_params)

# ==========================================
# 8. 최종 성능 출력
# ==========================================

X_selected = df[best_features]

X_train, X_test, y_train, y_test = train_test_split(
    X_selected,
    y,
    test_size=0.2,
    random_state=42,
    stratify=y
)

pred = best_model.predict(X_test)

print("\n===== Classification Report =====")

print(
    classification_report(
        y_test,
        pred
    )
)

# ==========================================
# 9. 모델 저장
# ==========================================

joblib.dump(
    best_model,
    "best_model.pkl"
)

joblib.dump(
    best_features,
    "selected_features.pkl"
)

print("\n모델 저장 완료")
print("best_model.pkl")
print("selected_features.pkl")
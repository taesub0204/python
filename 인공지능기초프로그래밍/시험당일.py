import pandas as pd
import joblib

from sklearn.metrics import classification_report

# 저장된 모델 불러오기
model = joblib.load(
    "best_model.pkl"
)

# 저장된 Feature 불러오기
features = joblib.load(
    "selected_features.pkl"
)

# test.csv 읽기
test_df = pd.read_csv(
    "test.csv"
)

# Feature 추출
X_test = test_df[features]

# 예측
pred = model.predict(
    X_test
)

print("\n===== 예측 결과 =====")
print(pred)

# target이 있는 경우 평가
if "target" in test_df.columns:

    y_test = test_df["target"]

    print("\n===== Classification Report =====")

    print(
        classification_report(
            y_test,
            pred
        )
    )
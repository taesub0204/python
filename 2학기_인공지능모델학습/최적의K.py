import os
import numpy as np
import matplotlib.pyplot as plt
from PIL import Image
from sklearn.cluster import KMeans


images = []
for file in os.listdir("./my"):
    if file.lower().endswith(".jpg"):
        with Image.open(os.path.join("./my", file)) as img:
            img = img.convert("L").resize((50, 50))
            images.append(np.array(img))

# 교재의 fruits_2d와 같은 역할: 이미지 한 장을 한 줄의 데이터로 만든다.
fruits_2d = np.array(images).reshape(len(images), -1)


def draw_fruits(arr, max_images=30):
    """이미지 배열에서 최대 max_images장을 10개씩 출력한다."""
    arr = arr[:max_images]
    n = len(arr)
    rows = int(np.ceil(n / 10))

    fig, axs = plt.subplots(rows, 10, figsize=(12, rows * 1.3))
    axs = np.ravel(axs)
    for i in range(n):
        axs[i].imshow(arr[i], cmap="gray")
        axs[i].axis("off")
    for i in range(n, len(axs)):
        axs[i].axis("off")
    plt.tight_layout()
    plt.show()


inertia = []
# K=2~12까지 비교해야 엘보우 지점(K=6)을 충분히 확인할 수 있다.
K_VALUES = range(2, 13)
for k in K_VALUES:
    km = KMeans(n_clusters=k, random_state=42, n_init=10)
    km.fit(fruits_2d)
    inertia.append(km.inertia_)

# K=2~12 범위의 엘보우 그래프를 확인한 결과, 최종 K를 6으로 결정했다.
best_k = 6
print("선택한 최적 K:", best_k)

plt.plot(K_VALUES, inertia, marker="o")
plt.axvline(best_k, color="red", linestyle="--", label=f"best K = {best_k}")
plt.xlabel("k")
plt.ylabel("inertia")
plt.legend()
plt.show()

# 최적 K로 이미지들을 분류한다.
km = KMeans(n_clusters=best_k, random_state=42, n_init=10)
labels = km.fit_predict(fruits_2d)

for label in range(best_k):
    cluster_images = np.array(images)[labels == label]
    print(f"군집 {label}: {len(cluster_images)}장")
    draw_fruits(cluster_images)

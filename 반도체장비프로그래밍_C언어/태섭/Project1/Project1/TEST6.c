#include <stdio.h>

int main() {
    int money = 127670; // 계산할 금액
    int units[] = { 50000, 10000, 5000, 1000, 500, 100, 50, 10 }; // 화폐 단위 배열
    int count = 0; // count 0 초기값
    int total_count = 0; // total_count = 0 초기값

    printf("금액: %d원\n", money);
    printf("--------------------------\n");

    for (int i = 0; i < 8; i++) {
        count = money / units[i];      // 해당 화폐의 개수 (몫) 처음  127670 / 50000
        money = money % units[i];      // 남은 금액 (나머지)   50000으로 나눈뒤 나머지 금액
        total_count += count;          // 총 화폐 개수 누적

        if (count > 0) {
            printf("%d원: %d개\n", units[i], count);
        }
    }

    printf("--------------------------\n");
    printf("총 화폐 개수: %d개\n", total_count);

    return 0;
}
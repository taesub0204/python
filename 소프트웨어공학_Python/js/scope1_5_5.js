





for (var i = 0; i < 3; i++) {
    console.log(i)
    setTimeout(function() {
        console.log(i);  // 예상: 0, 1, 2 → 실제: 3, 3, 3
    }, 1000);
}
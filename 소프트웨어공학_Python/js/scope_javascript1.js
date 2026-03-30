console.log(hoistedVar);
console.log(hoistedVar);  // undefined
var hoistedVar = "값";
console.log(hoistedVar);
// 실제로는 다음과 같이 동작:
// var hoistedVar;
// console.log(hoistedVar);  // undefined
// hoistedVar = "값";

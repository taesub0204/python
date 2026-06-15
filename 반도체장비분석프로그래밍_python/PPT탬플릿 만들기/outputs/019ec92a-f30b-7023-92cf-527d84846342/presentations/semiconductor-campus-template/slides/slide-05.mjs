import { C, frame, title, footer } from './_template.mjs';
export async function slide05(presentation, ctx) {
  const slide = presentation.slides.add(); frame(slide, ctx); title(slide, ctx, 'COMPARISON', '두 가지 관점을 나란히 비교합니다.');
  [['Before', C.blue], ['After', C.orange]].forEach(([label, color], i) => {
    const x = 92 + i*574;
    ctx.addShape(slide, { x, y: 194, width: 500, height: 360, fill: C.white, line: ctx.line('#D5E2EA', 1) });
    ctx.addShape(slide, { x, y: 194, width: 500, height: 6, fill: color });
    ctx.addText(slide, { text: label, x: x+28, y: 226, width: 170, height: 32, fontSize: 21, bold: true, color, typeface: 'Aptos' });
    ctx.addText(slide, { text: '내용 제목', x: x+28, y: 290, width: 320, height: 34, fontSize: 23, bold: true, color: C.ink, typeface: 'Malgun Gothic' });
    ctx.addText(slide, { text: '비교 항목, 코드 실행 결과, 개선 전후 지표 등을 입력하세요.', x: x+28, y: 346, width: 410, height: 72, fontSize: 15, color: C.muted, typeface: 'Malgun Gothic' });
  }); footer(slide, ctx, 5); return slide;
}

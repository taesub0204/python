import { C, frame, title, footer } from './_template.mjs';
export async function slide02(presentation, ctx) {
  const slide = presentation.slides.add(); frame(slide, ctx); title(slide, ctx, 'OVERVIEW', '오늘의 흐름을 한눈에 정리합니다.');
  const items = ['학습 목표', '분석 데이터', '핵심 코드', '실습 결과'];
  items.forEach((t, i) => {
    const x = 92 + i*278;
    ctx.addShape(slide, { x, y: 230, width: 220, height: 220, fill: C.white, line: ctx.line('#D5E2EA', 1) });
    ctx.addText(slide, { text: `0${i+1}`, x: x+24, y: 252, width: 62, height: 40, fontSize: 28, bold: true, color: i%2 ? C.orange : C.blue, typeface: 'Aptos' });
    ctx.addShape(slide, { x: x+24, y: 312, width: 48, height: 4, fill: i%2 ? C.gold : C.blue2 });
    ctx.addText(slide, { text: t, x: x+24, y: 344, width: 160, height: 34, fontSize: 21, bold: true, color: C.ink, typeface: 'Malgun Gothic' });
    ctx.addText(slide, { text: '주요 항목을 입력하세요', x: x+24, y: 392, width: 160, height: 42, fontSize: 13, color: C.muted, typeface: 'Malgun Gothic' });
  }); footer(slide, ctx, 2); return slide;
}

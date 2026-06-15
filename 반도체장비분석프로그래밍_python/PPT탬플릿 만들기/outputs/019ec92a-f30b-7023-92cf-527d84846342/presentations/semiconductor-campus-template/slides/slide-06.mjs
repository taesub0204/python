import { C, frame, footer } from './_template.mjs';
export async function slide06(presentation, ctx) {
  const slide = presentation.slides.add(); frame(slide, ctx);
  ctx.addText(slide, { text: 'SUMMARY', x: 72, y: 84, width: 220, height: 28, fontSize: 15, bold: true, color: C.blue, typeface: 'Aptos' });
  ctx.addText(slide, { text: '핵심 내용을 정리합니다.', x: 72, y: 142, width: 620, height: 58, fontSize: 36, bold: true, color: C.ink, typeface: 'Malgun Gothic' });
  [0,1,2].forEach(i => {
    const y = 250 + i*96;
    ctx.addShape(slide, { x: 92, y, width: 32, height: 32, fill: i===1 ? C.orange : C.blue });
    ctx.addText(slide, { text: '요약 문장 입력', x: 154, y: y-2, width: 520, height: 30, fontSize: 22, bold: true, color: C.ink, typeface: 'Malgun Gothic' });
    ctx.addText(slide, { text: '근거 또는 다음 액션을 간단히 입력하세요.', x: 154, y: y+34, width: 660, height: 28, fontSize: 14, color: C.muted, typeface: 'Malgun Gothic' });
  });
  ctx.addShape(slide, { x: 890, y: 242, width: 230, height: 230, fill: '#EAF4F9', line: ctx.line('#C8DBE7', 1) });
  ctx.addText(slide, { text: 'Q&A', x: 926, y: 318, width: 158, height: 70, fontSize: 42, bold: true, color: C.blue, typeface: 'Aptos', align: 'center', valign: 'middle' });
  footer(slide, ctx, 6); return slide;
}

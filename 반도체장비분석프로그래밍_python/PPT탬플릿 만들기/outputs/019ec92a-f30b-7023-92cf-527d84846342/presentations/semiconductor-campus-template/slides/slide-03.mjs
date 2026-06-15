import { C, frame, footer } from './_template.mjs';
export async function slide03(presentation, ctx) {
  const slide = presentation.slides.add(); frame(slide, ctx);
  ctx.addShape(slide, { x: 72, y: 136, width: 92, height: 6, fill: C.gold });
  ctx.addText(slide, { text: 'SECTION 01', x: 72, y: 172, width: 260, height: 30, fontSize: 15, bold: true, color: C.blue, typeface: 'Aptos' });
  ctx.addText(slide, { text: '섹션 제목을\n입력하세요', x: 72, y: 224, width: 700, height: 132, fontSize: 50, bold: true, color: C.ink, typeface: 'Malgun Gothic' });
  ctx.addText(slide, { text: '해당 장에서 다룰 핵심 메시지를 한 문장으로 정리합니다.', x: 76, y: 396, width: 660, height: 34, fontSize: 20, color: C.muted, typeface: 'Malgun Gothic' });
  ctx.addShape(slide, { x: 854, y: 166, width: 260, height: 260, fill: '#EAF4F9', line: ctx.line('#C8DBE7', 1) });
  ctx.addShape(slide, { x: 914, y: 226, width: 140, height: 140, fill: '#00000000', line: ctx.line(C.blue, 4) });
  ctx.addShape(slide, { x: 974, y: 166, width: 6, height: 260, fill: C.orange });
  footer(slide, ctx, 3); return slide;
}

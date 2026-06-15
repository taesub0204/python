import { C, IMG } from './_template.mjs';
export async function slide01(presentation, ctx) {
  const slide = presentation.slides.add();
  ctx.addShape(slide, { x: 0, y: 0, width: ctx.W, height: ctx.H, fill: C.paper });
  ctx.addShape(slide, { x: 0, y: 0, width: 470, height: ctx.H, fill: C.white });
  ctx.addShape(slide, { x: 470, y: 0, width: 10, height: ctx.H, fill: C.blue });
  ctx.addShape(slide, { x: 480, y: 0, width: 4, height: ctx.H, fill: C.gold });
  ctx.addShape(slide, { x: 0, y: 0, width: 18, height: ctx.H, fill: C.blue });
  await ctx.addImage(slide, { path: IMG, x: 56, y: 280, width: 356, height: 126, fit: 'contain', alt: '반도체융합캠퍼스 로고' });
  ctx.addShape(slide, { x: 548, y: 88, width: 88, height: 5, fill: C.gold });
  ctx.addText(slide, { text: 'PRESENTATION TEMPLATE', x: 548, y: 118, width: 420, height: 30, fontSize: 14, bold: true, color: C.blue, typeface: 'Aptos' });
  ctx.addText(slide, { text: '반도체 장비 분석\n프로그래밍', x: 548, y: 178, width: 620, height: 145, fontSize: 48, bold: true, color: C.ink, typeface: 'Malgun Gothic', valign: 'middle' });
  ctx.addText(slide, { text: 'Python 기반 데이터 분석 및 실습 자료', x: 552, y: 340, width: 560, height: 34, fontSize: 21, color: C.muted, typeface: 'Malgun Gothic' });
  ctx.addShape(slide, { x: 548, y: 446, width: 520, height: 1, fill: '#C9D8E2' });
  ctx.addText(slide, { text: '작성자 / 소속 / 날짜', x: 552, y: 472, width: 350, height: 30, fontSize: 17, color: C.ink, typeface: 'Malgun Gothic' });
  ctx.addShape(slide, { x: 1088, y: 516, width: 110, height: 110, fill: '#E7F1F7', line: ctx.line('#C8DBE7', 1) });
  ctx.addShape(slide, { x: 1110, y: 538, width: 66, height: 66, fill: '#00000000', line: ctx.line(C.blue2, 3) });
  return slide;
}

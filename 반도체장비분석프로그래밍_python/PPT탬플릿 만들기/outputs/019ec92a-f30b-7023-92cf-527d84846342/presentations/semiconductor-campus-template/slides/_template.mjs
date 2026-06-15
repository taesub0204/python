export const C = {
  ink: '#172033',
  muted: '#5D6B7A',
  paper: '#F8FAFC',
  soft: '#EEF4F8',
  blue: '#0060B0',
  blue2: '#0090D0',
  gold: '#F0B010',
  orange: '#F08010',
  olive: '#C0C000',
  white: '#FFFFFF',
  rule: '#D9E3EA',
};
export const IMG = String.raw`C:\\Users\\user\\Desktop\\taesub\\python\\반도체장비분석프로그래밍_python\\PPT탬플릿 만들기\\(국문좌우) 반도체융합캠퍼스.png`;

export function bg(slide, ctx) {
  ctx.addShape(slide, { x: 0, y: 0, width: ctx.W, height: ctx.H, fill: C.paper });
  ctx.addShape(slide, { x: 0, y: 0, width: ctx.W, height: 10, fill: C.blue });
  ctx.addShape(slide, { x: 0, y: 10, width: ctx.W, height: 3, fill: C.gold });
}

export function frame(slide, ctx, opts = {}) {
  bg(slide, ctx);
  const m = opts.margin ?? 34;
  ctx.addShape(slide, { x: m, y: 34, width: ctx.W - 2*m, height: ctx.H - 68, fill: '#00000000', line: ctx.line(C.blue, 1.5) });
  ctx.addShape(slide, { x: m+8, y: 42, width: ctx.W - 2*m - 16, height: ctx.H - 84, fill: '#00000000', line: ctx.line('#B8CBD8', 0.6) });
  ctx.addShape(slide, { x: m, y: 34, width: 112, height: 4, fill: C.gold });
  ctx.addShape(slide, { x: ctx.W - m - 112, y: ctx.H - 38, width: 112, height: 4, fill: C.orange });
}

export function title(slide, ctx, kicker, claim) {
  ctx.addShape(slide, { name: 'kicker-marker', x: 72, y: 70, width: 9, height: 9, fill: C.gold });
  ctx.addText(slide, { name: 'kicker-label', text: kicker, x: 92, y: 63, width: 310, height: 24, fontSize: 12, bold: true, color: C.blue, typeface: 'Malgun Gothic', valign: 'middle' });
  ctx.addText(slide, { text: claim, x: 72, y: 94, width: 760, height: 56, fontSize: 30, bold: true, color: C.ink, typeface: 'Malgun Gothic', valign: 'middle' });
}

export function footer(slide, ctx, page) {
  ctx.addText(slide, { text: 'SEMICONDUCTOR CONVERGENCE CAMPUS', x: 72, y: 662, width: 360, height: 18, fontSize: 10, color: C.muted, typeface: 'Aptos', valign: 'middle' });
  ctx.addText(slide, { text: String(page).padStart(2, '0'), x: 1164, y: 656, width: 44, height: 26, fontSize: 13, bold: true, color: C.blue, typeface: 'Aptos', align: 'right', valign: 'middle' });
}

export function bodyPlaceholder(slide, ctx, x, y, w, h, label='내용 입력') {
  ctx.addShape(slide, { x, y, width: w, height: h, fill: C.white, line: ctx.line(C.rule, 1) });
  ctx.addShape(slide, { x, y, width: 5, height: h, fill: C.blue });
  ctx.addText(slide, { text: label, x: x+26, y: y+22, width: w-52, height: 32, fontSize: 20, bold: true, color: C.ink, typeface: 'Malgun Gothic' });
  ctx.addText(slide, { text: '핵심 메시지, 그래프, 표 또는 이미지가 들어갈 영역입니다.', x: x+26, y: y+66, width: w-52, height: 52, fontSize: 14, color: C.muted, typeface: 'Malgun Gothic' });
}

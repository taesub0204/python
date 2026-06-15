import { frame, title, footer, bodyPlaceholder } from './_template.mjs';
export async function slide04(presentation, ctx) {
  const slide = presentation.slides.add(); frame(slide, ctx); title(slide, ctx, 'ANALYSIS', '핵심 분석 결과를 명확하게 보여줍니다.');
  bodyPlaceholder(slide, ctx, 86, 190, 760, 402, '그래프 / 표 / 이미지 영역');
  bodyPlaceholder(slide, ctx, 884, 190, 280, 402, '주요 해석');
  footer(slide, ctx, 4); return slide;
}

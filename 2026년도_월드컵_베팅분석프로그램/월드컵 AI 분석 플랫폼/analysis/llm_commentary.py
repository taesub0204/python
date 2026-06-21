"""LLM 코멘터리 모듈 (Gemini)."""

from typing import Dict, Any
from config.settings import get_config
from models.data_models import Match, TeamStats

try:
    import google.generativeai as genai
    _HAS_GENAI = True
except ImportError:
    _HAS_GENAI = False

class LLMCommentary:
    def __init__(self):
        self.config = get_config()
        self.api_key = self.config.llm.api_key
        self.is_available = False
        
        if _HAS_GENAI and self.api_key:
            try:
                genai.configure(api_key=self.api_key)
                self.model = genai.GenerativeModel(self.config.llm.model_name)
                self.is_available = True
            except:
                pass
                
    def generate_commentary(self, match: Match, h_team: TeamStats, a_team: TeamStats, bet_analysis: Dict[str, Any]) -> str:
        if not self.is_available:
            return self._generate_fallback(match, h_team, a_team, bet_analysis)
            
        prompt = f"""당신은 월드컵 전문 베팅 분석가입니다. 아래 팀의 현재 상황을 바탕으로 스포츠 기사 스타일의 생생한 해설 코멘트를 작성해주세요.
        
        [경기 정보]
        - {match.home_team} (현재 {h_team.points}점, {h_team.matches_played}경기 진행)
        - {match.away_team} (현재 {a_team.points}점, {a_team.matches_played}경기 진행)
        
        [AI 분석 데이터]
        - 홈팀 심리적 압박감: {bet_analysis['home_pressure']}/100
        - 원정팀 심리적 압박감: {bet_analysis['away_pressure']}/100
        - 예상 경기 페이스: {bet_analysis['expected_pace']}
        - AI 추천 베팅 (2.5골 기준): {bet_analysis['uo_recommendation']} (신뢰도: {bet_analysis['uo_confidence']}%)
        - AI 추천 베팅 (3.5골 기준): {bet_analysis.get('uo_3_5_recommendation', 'PASS')} (신뢰도: {bet_analysis.get('uo_3_5_confidence', 0)}%)
        
        [작성 조건]
        1. 왜 언더/오버가 추천되는지 각 팀의 '승점 상황'과 '심리적 압박감(벼랑 끝이냐 여유냐)'을 중심으로 풀어주세요.
        2. 2.5골 기준뿐만 아니라 3.5골 기준의 추천 결과도 함께 언급하며 분석의 깊이를 더해주세요.
        3. 분량은 300자 내외로, 너무 길지 않게 핵심만 작성하세요.
        4. 스포츠 아나운서처럼 흥미롭고 분석적인 어조를 사용하세요.
        """
        
        try:
            response = self.model.generate_content(prompt)
            if response and response.text:
                return response.text.strip()
        except:
            pass
            
        return self._generate_fallback(match, h_team, a_team, bet_analysis)
        
    def _generate_fallback(self, match: Match, h_team: TeamStats, a_team: TeamStats, bet_analysis: Dict[str, Any]) -> str:
        """API 실패 시 템플릿 기반 코멘트 생성"""
        rec = bet_analysis['uo_recommendation']
        
        if rec == "OVER":
            return f"현재 {match.home_team}({h_team.points}점)과 {match.away_team}({a_team.points}점)의 맞대결입니다. 양 팀의 평균 압박감이 상당히 높습니다. 승점이 절실한 팀이 라인을 올리고 공격적으로 나설 수밖에 없는 상황이므로, 수비 뒷공간이 열리며 다득점(오버) 양상이 예상됩니다."
        elif rec == "UNDER":
            return f"현재 {match.home_team}({h_team.points}점)과 {match.away_team}({a_team.points}점)의 경기입니다. 양 팀 모두 크게 무리할 필요가 없거나, 1차전 탐색전 성격이 강해 수비적인 운영(언더)이 예상됩니다."
        else:
            return f"{match.home_team}과 {match.away_team}의 경기입니다. 양 팀의 동기부여가 엇갈려 변수가 많습니다. 베팅을 패스(PASS)하는 것을 추천합니다."

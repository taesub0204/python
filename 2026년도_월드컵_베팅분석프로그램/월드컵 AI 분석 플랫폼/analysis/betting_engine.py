"""심리/상황 기반 언더오버 분석 엔진."""

from typing import Dict, Any
from models.data_models import Match, TeamStats

class BettingEngine:
    def __init__(self):
        pass
        
    def analyze_match_context(self, match: Match, h_team: TeamStats, a_team: TeamStats) -> Dict[str, Any]:
        """두 팀의 현재 승점과 조별리그 통과 확률을 바탕으로 언더/오버 성향을 분석한다.
        
        월드컵 48개국 체제:
        - 조 1, 2위 진출 (승점 4점 이상이면 매우 안정권)
        - 조 3위 중 상위 8팀 진출 (승점 3점이라도 골득실 중요)
        """
        
        analysis = {
            "match_id": match.id,
            "home_pressure": 0.0, # 0.0 (여유) ~ 100.0 (절박)
            "away_pressure": 0.0,
            "expected_pace": "NORMAL", # SLOW, NORMAL, FAST(OVER)
            "uo_recommendation": "PASS",
            "uo_confidence": 0.0
        }
        
        # 경기 수가 0이면(1차전) 보통 탐색전 -> 언더 성향
        if h_team.matches_played == 0 and a_team.matches_played == 0:
            analysis["home_pressure"] = 30.0
            analysis["away_pressure"] = 30.0
            analysis["expected_pace"] = "SLOW"
            analysis["uo_recommendation"] = "UNDER"
            analysis["uo_confidence"] = 70.0
            analysis["uo_3_5_recommendation"] = "UNDER"
            analysis["uo_3_5_confidence"] = 90.0
        else:
            # 2차전 or 3차전 분석
            # 승점 3점 이상이면 1차전 승리팀 -> 약간의 여유
            # 승점 0점이면 1차전 패배팀 -> 무조건 이겨야 함 (압박감 MAX)
            def calc_pressure(t: TeamStats) -> float:
                if t.matches_played == 1:
                    if t.points >= 3: return 20.0
                    if t.points == 1: return 60.0
                    return 85.0 # 패배팀
                elif t.matches_played == 2:
                    if t.points >= 4: return 10.0 # 사실상 확정, 로테이션 가능
                    if t.points == 3: return 70.0
                    if t.points == 1: return 90.0 # 무조건 이겨야함
                    return 95.0 # 기적을 바라며 닥공
                return 50.0

            analysis["home_pressure"] = calc_pressure(h_team)
            analysis["away_pressure"] = calc_pressure(a_team)
            
            avg_pressure = (analysis["home_pressure"] + analysis["away_pressure"]) / 2
            diff_pressure = abs(analysis["home_pressure"] - analysis["away_pressure"])
            
            if avg_pressure > 75.0:
                # 둘 다 이겨야 하는 단두대 매치 -> 난타전 오버 성향
                analysis["expected_pace"] = "FAST"
                analysis["uo_recommendation"] = "OVER"
                analysis["uo_confidence"] = 80.0
                
                # 3.5 기준 로직
                if avg_pressure > 85.0:
                    analysis["uo_3_5_recommendation"] = "OVER"
                    analysis["uo_3_5_confidence"] = 60.0
                else:
                    analysis["uo_3_5_recommendation"] = "UNDER"
                    analysis["uo_3_5_confidence"] = 55.0
                    
            elif avg_pressure < 30.0:
                # 둘 다 이미 16강 확정 -> 무리 안함 (로테이션)
                analysis["expected_pace"] = "SLOW"
                analysis["uo_recommendation"] = "UNDER"
                analysis["uo_confidence"] = 85.0
                
                analysis["uo_3_5_recommendation"] = "UNDER"
                analysis["uo_3_5_confidence"] = 95.0
            else:
                if diff_pressure > 50.0:
                    # 한쪽은 절박하고 한쪽은 여유로움
                    analysis["expected_pace"] = "NORMAL"
                    analysis["uo_recommendation"] = "OVER" 
                    analysis["uo_confidence"] = 65.0
                    
                    analysis["uo_3_5_recommendation"] = "UNDER"
                    analysis["uo_3_5_confidence"] = 70.0
                else:
                    analysis["expected_pace"] = "NORMAL"
                    analysis["uo_recommendation"] = "PASS"
                    analysis["uo_confidence"] = 40.0
                    
                    analysis["uo_3_5_recommendation"] = "UNDER"
                    analysis["uo_3_5_confidence"] = 80.0
                
        # 1X2 승무패 예측 및 가치 분석 (ELO & 심리 기반)
        team_strengths = {
            "프랑스": 1720, "아르헨티나": 1730, "브라질": 1700, "잉글랜드": 1690, "스페인": 1680, "포르투갈": 1670, "네덜란드": 1650, "벨기에": 1640, "독일": 1630,
            "우루과이": 1560, "크로아티아": 1540, "모로코": 1530, "콜롬비아": 1520, "미국": 1510, "대한민국": 1500, "일본": 1510, "스위스": 1490, "스웨덴": 1480, 
            "에콰도르": 1470, "세네갈": 1460, "오스트리아": 1470, "멕시코": 1460, "튀르키예": 1450, "체코": 1440,
            "호주": 1410, "파라과이": 1390, "캐나다": 1420, "이란": 1400, "가나": 1380, "이집트": 1370, "알제리": 1360, "우즈베키스탄": 1350, 
            "남아프리카공화국": 1330, "보스니아 헤르체고비나": 1320, "노르웨이": 1400, "카타르": 1310,
            "뉴질랜드": 1280, "카보베르데": 1270, "콩고민주공화국": 1260, "파나마": 1250, "요르단": 1240, "이라크": 1230, "아이티": 1180, 
            "퀴라소": 1190, "튀니지": 1220
        }
        
        h_str = team_strengths.get(match.home_team, 1350)
        a_str = team_strengths.get(match.away_team, 1350)
        
        h_adv = 50
        if match.home_team in ["미국", "멕시코", "캐나다"]:
            h_adv = 100
        diff = (h_str + h_adv) - a_str
        
        win_prob = 1.0 / (1.0 + 10**(-diff / 400.0))
        lose_prob = 1.0 / (1.0 + 10**(diff / 400.0))
        draw_prob = 0.24 * (1.0 - abs(win_prob - lose_prob))
        
        total_prob = win_prob + draw_prob + lose_prob
        p_win = win_prob / total_prob
        p_draw = draw_prob / total_prob
        p_lose = lose_prob / total_prob
        
        # 동기부여/심리적 상황에 따른 승률 미세조정
        pressure_factor = (analysis["home_pressure"] - analysis["away_pressure"]) * 0.0015
        p_win = max(0.05, min(0.95, p_win + pressure_factor))
        p_lose = max(0.05, min(0.95, p_lose - pressure_factor))
        
        # 재정규화
        sum_p = p_win + p_draw + p_lose
        p_win /= sum_p
        p_draw /= sum_p
        p_lose /= sum_p
        
        analysis["prob_win"] = round(p_win * 100, 1)
        analysis["prob_draw"] = round(p_draw * 100, 1)
        analysis["prob_lose"] = round(p_lose * 100, 1)
        
        # 추천 결과 산정: 가장 확률이 높은 선택지 추천
        probs = {"HOME_WIN": p_win, "DRAW": p_draw, "AWAY_WIN": p_lose}
        recommend = max(probs, key=probs.get)
        analysis["1x2_recommendation"] = recommend
        analysis["1x2_confidence"] = round(probs[recommend] * 100, 1)
        
        # Value bet 판단 (AI 확률 * 배당률 - 1.0)
        val_home = (p_win * match.odds_home) - 1.0
        val_draw = (p_draw * match.odds_draw) - 1.0
        val_away = (p_lose * match.odds_away) - 1.0
        
        analysis["val_home"] = round(val_home, 3)
        analysis["val_draw"] = round(val_draw, 3)
        analysis["val_away"] = round(val_away, 3)
        
        # 가장 가치가 높은 선택지
        values = {"HOME_WIN": val_home, "DRAW": val_draw, "AWAY_WIN": val_away}
        val_recommend = max(values, key=values.get)
        analysis["value_recommendation"] = val_recommend
        analysis["value_score"] = round(values[val_recommend], 3)
        
        return analysis

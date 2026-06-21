"""해외식 토토 베팅 조합 엔진.

골든 티켓(Golden Ticket) 및 하이퍼큐어(Hyper Cure) 기법을 기반으로
승점 상황과 전력 대비 해외 배당률의 가치(Value)를 분석하여 최적의 포트폴리오 조합을 제공합니다.
승무패(1X2) 및 2.5골/3.5골 언더오버를 결합하여 안정성과 수익성을 최적화합니다.
"""

from typing import List, Dict, Any, Tuple
from models.data_models import Match, TeamStats
from analysis.betting_engine import BettingEngine

class CombinationEngine:
    def __init__(self):
        self.engine = BettingEngine()

    def _get_uo_3_5_odds(self, match: Match, pick: str) -> float:
        """2.5골 오버 배당률로부터 3.5골 언더/오버 배당률을 수학적으로 역산한다."""
        payout = 0.94 # 환급률 94%
        
        # 2.5 오버 확률 계산
        p_over_2_5 = payout / match.odds_over
        
        # 3.5 오버 확률은 보통 2.5 오버 확률의 45% 수준으로 수렴 (포아송 분포 모사)
        p_over_3_5 = p_over_2_5 * 0.45
        p_over_3_5 = max(0.05, min(0.45, p_over_3_5))
        p_under_3_5 = 1.0 - p_over_3_5
        
        if pick == "OVER":
            return round(payout / p_over_3_5, 2)
        else:
            return round(payout / p_under_3_5, 2)

    def generate_betting_portfolios(self, matches: List[Match], teams: Dict[str, TeamStats]) -> Dict[str, Any]:
        """예정된 경기를 분석하여 골든 티켓과 하이퍼큐어 조합을 생성한다."""
        if len(matches) < 3:
            return {
                "status": "INSUFFICIENT_DATA",
                "message": "조합을 생성하기 위해서는 최소 3경기 이상의 예정된 경기가 필요합니다.",
                "golden_tickets": [],
                "hyper_cures": []
            }

        # 1. 모든 경기 AI 분석 수행 및 분석 결과 패키징 (W/D/L 및 U/O 2.5/3.5 세 가지 옵션 추출)
        analyzed_matches = []
        for m in matches:
            h_team = teams.get(m.home_team)
            a_team = teams.get(m.away_team)
            if not h_team or not a_team:
                continue
            
            analysis = self.engine.analyze_match_context(m, h_team, a_team)
            
            # W/D/L 옵션 정보 추출
            rec_1x2 = analysis["1x2_recommendation"]
            rec_korean_1x2 = {"HOME_WIN": "홈 승", "DRAW": "무승부", "AWAY_WIN": "원정 승"}[rec_1x2]
            odds_1x2 = m.odds_home if rec_1x2 == "HOME_WIN" else m.odds_draw if rec_1x2 == "DRAW" else m.odds_away
            opt_1x2 = {
                "pick": rec_1x2,
                "pick_desc": rec_korean_1x2,
                "odds": odds_1x2,
                "prob": analysis["1x2_confidence"],
                "category": "W/D/L"
            }
            
            # U/O 2.5 옵션 정보 추출
            rec_uo = analysis["uo_recommendation"]
            if rec_uo == "PASS":
                rec_uo = "UNDER" # 기본값 언더로 우회
            rec_korean_uo = "2.5 언더" if rec_uo == "UNDER" else "2.5 오버"
            odds_uo = m.odds_under if rec_uo == "UNDER" else m.odds_over
            opt_uo = {
                "pick": rec_uo,
                "pick_desc": rec_korean_uo,
                "odds": odds_uo,
                "prob": analysis["uo_confidence"],
                "category": "U/O 2.5"
            }
            
            # U/O 3.5 옵션 정보 추출
            rec_uo35 = analysis.get("uo_3_5_recommendation", "UNDER")
            rec_korean_uo35 = "3.5 언더" if rec_uo35 == "UNDER" else "3.5 오버"
            odds_uo35 = self._get_uo_3_5_odds(m, rec_uo35)
            opt_uo35 = {
                "pick": rec_uo35,
                "pick_desc": rec_korean_uo35,
                "odds": odds_uo35,
                "prob": analysis.get("uo_3_5_confidence", 80.0),
                "category": "U/O 3.5"
            }
            
            # 이 중 가장 신뢰도(Confidence)가 높은 것을 이 경기의 최적 피킹(best_option)으로 설정
            options = [opt_1x2, opt_uo, opt_uo35]
            best_opt = max(options, key=lambda x: x["prob"])
            
            analyzed_matches.append({
                "match": m,
                "analysis": analysis,
                "best_option": best_opt,
                "options": {
                    "W/D/L": opt_1x2,
                    "U/O 2.5": opt_uo,
                    "U/O 3.5": opt_uo35
                },
                "confidence": best_opt["prob"],
                "value_score": analysis["value_score"]
            })

        # 2. 신뢰도 최상위 3경기 선정 (골든 티켓용 축 경기)
        banker_candidates = sorted(analyzed_matches, key=lambda x: x["confidence"], reverse=True)
        bankers = banker_candidates[:3]
        
        # 3. 축 경기를 제외한 나머지 중에서 변수 경기(Hedge 1) 선정
        banker_ids = {b["match"].id for b in bankers}
        hedge_candidates = [h for h in banker_candidates if h["match"].id not in banker_ids]
        
        if not hedge_candidates:
            # 경기가 부족한 경우 마지막 축 경기를 변수 경기로 중복 지정
            h1 = bankers[-1]
        else:
            h1 = hedge_candidates[0]

        if len(bankers) < 2:
            return {
                "status": "INSUFFICIENT_DATA",
                "message": "분석 가능한 경기 데이터가 부족합니다.",
                "golden_tickets": [],
                "hyper_cures": []
            }

        # 3. 골든 티켓 (Golden Ticket) 조합 생성
        # 골든 티켓 구성:
        # - 축 3경기의 최적 픽(승무패 또는 언더/오버 조합)을 엮어 승률 극대화 단일 조합
        golden_main_selections = []
        for b in bankers:
            best_opt = b["best_option"]
            golden_main_selections.append({
                "match": b["match"],
                "pick": best_opt["pick"],
                "pick_desc": best_opt["pick_desc"],
                "odds": best_opt["odds"],
                "prob": best_opt["prob"]
            })
            
        main_odds = 1.0; main_prob = 1.0
        for sel in golden_main_selections:
            main_odds *= sel["odds"]
            main_prob *= (sel["prob"] / 100.0)

        # 투자금 분배 추천: 단일 주력 조합이므로 100% 집중 투자로 설계
        stake_main = 100.0

        golden_tickets = [
            {
                "type": "MAIN",
                "name": "🎫 골든 티켓 - 메인 조합 (단일 주력)",
                "desc": "확률이 가장 높은 강팀 승리 및 고신뢰도 선택지만 모은 100% 무보험 주력 단일 조합",
                "selections": golden_main_selections,
                "total_odds": round(main_odds, 2),
                "expected_prob": round(main_prob * 100, 1),
                "stake_ratio": stake_main,
                "target_return": round(stake_main * main_odds, 1)
            }
        ]

        # 4. 하이퍼큐어 (Hyper Cure) 조합 생성
        # 하이퍼큐어 구성:
        # - 고정 축 2경기: Banker 1 + Banker 2 (매우 안정적인 최고의 픽 2개)
        # - 여기에 변수 경기 1개(Hedge 1)의 카테고리별(승무패, 2.5골, 3.5골) 추천을 분할 결합
        # - 조합 A: 고정 축 2개 + 변수 경기 W/D/L 승무패 추천
        # - 조합 B: 고정 축 2개 + 변수 경기 2.5골 언더오버 추천
        # - 조합 C: 고정 축 2개 + 변수 경기 3.5골 언더오버 추천
        # - 조합 D (축 보험): 고정 축 중 1개 무승부/반대 마킹 + 변수 경기 2.5골 언더오버 추천
        
        fixed_selections = []
        for b in bankers[:2]:
            best_opt = b["best_option"]
            fixed_selections.append({
                "match": b["match"],
                "pick": best_opt["pick"],
                "pick_desc": best_opt["pick_desc"],
                "odds": best_opt["odds"],
                "prob": best_opt["prob"]
            })
            
        h1_match = h1["match"]
        h1_opts = h1["options"]
        
        opt_wdl = h1_opts["W/D/L"]
        opt_uo25 = h1_opts["U/O 2.5"]
        opt_uo35 = h1_opts["U/O 3.5"]
        
        # 조합 A (W/D/L 주력): Fixed Bankers + 변수 경기 승무패 추천
        sel_a = fixed_selections + [{
            "match": h1_match,
            "pick": opt_wdl["pick"],
            "pick_desc": opt_wdl["pick_desc"],
            "odds": opt_wdl["odds"],
            "prob": opt_wdl["prob"]
        }]
        odds_a = 1.0; prob_a = 1.0
        for s in sel_a:
            odds_a *= s["odds"]
            prob_a *= (s["prob"] / 100.0)
            
        # 조합 B (2.5골 헤징): Fixed Bankers + 변수 경기 2.5골 추천
        sel_b = fixed_selections + [{
            "match": h1_match,
            "pick": opt_uo25["pick"],
            "pick_desc": opt_uo25["pick_desc"],
            "odds": opt_uo25["odds"],
            "prob": opt_uo25["prob"]
        }]
        odds_b = 1.0; prob_b = 1.0
        for s in sel_b:
            odds_b *= s["odds"]
            prob_b *= (s["prob"] / 100.0)
            
        # 조합 C (3.5골 헤징): Fixed Bankers + 변수 경기 3.5골 추천 (최고 안정형)
        sel_c = fixed_selections + [{
            "match": h1_match,
            "pick": opt_uo35["pick"],
            "pick_desc": opt_uo35["pick_desc"],
            "odds": opt_uo35["odds"],
            "prob": opt_uo35["prob"]
        }]
        odds_c = 1.0; prob_c = 1.0
        for s in sel_c:
            odds_c *= s["odds"]
            prob_c *= (s["prob"] / 100.0)

        # 조합 D (축 보험): Banker 1 + Banker 2 보험 마킹 + 변수 경기 2.5골 추천
        # Banker 2에 대한 보험 마킹 생성 (W/D/L은 DRAW로, U/O는 반대 픽으로)
        b2 = bankers[1]
        b2_best = b2["best_option"]
        
        if b2_best["category"] == "W/D/L":
            ins_pick = "DRAW"
            ins_desc = "무승부"
            ins_odds = b2["match"].odds_draw
            ins_prob = b2["analysis"]["prob_draw"]
        else:
            opp_pick = "OVER" if b2_best["pick"] == "UNDER" else "UNDER"
            ins_pick = opp_pick
            ins_desc = "2.5 오버" if opp_pick == "OVER" else "2.5 언더"
            ins_odds = b2["match"].odds_over if opp_pick == "OVER" else b2["match"].odds_under
            ins_prob = 100.0 - b2_best["prob"]

        sel_d = [
            fixed_selections[0],
            {
                "match": b2["match"],
                "pick": ins_pick,
                "pick_desc": ins_desc,
                "odds": ins_odds,
                "prob": ins_prob
            },
            {
                "match": h1_match,
                "pick": opt_uo25["pick"],
                "pick_desc": opt_uo25["pick_desc"],
                "odds": opt_uo25["odds"],
                "prob": opt_uo25["prob"]
            }
        ]
        odds_d = 1.0; prob_d = 1.0
        for s in sel_d:
            odds_d *= s["odds"]
            prob_d *= (s["prob"] / 100.0)

        # 투자 비율 설정: 조합 A(35%), 조합 B(30%), 조합 C(20%), 조합 D(15%)
        hyper_cures = [
            {
                "type": "HYPER_A",
                "name": "🔥 하이퍼큐어 - 변수 경기 승무패 조합",
                "desc": f"고정 축 2경기와 변수 경기({h1_match.home_team} vs {h1_match.away_team})의 AI 추천 승무패({opt_wdl['pick_desc']})를 결합한 주력 조합",
                "selections": sel_a,
                "total_odds": round(odds_a, 2),
                "expected_prob": round(prob_a * 100, 1),
                "stake_ratio": 35.0,
                "target_return": round(35.0 * odds_a, 1)
            },
            {
                "type": "HYPER_B",
                "name": "🔥 하이퍼큐어 - 변수 경기 2.5골 언더오버",
                "desc": f"고정 축 2경기와 변수 경기({h1_match.home_team} vs {h1_match.away_team})의 2.5골 {opt_uo25['pick_desc']} 베팅을 결합한 안정형 헤징 조합",
                "selections": sel_b,
                "total_odds": round(odds_b, 2),
                "expected_prob": round(prob_b * 100, 1),
                "stake_ratio": 30.0,
                "target_return": round(30.0 * odds_b, 1)
            },
            {
                "type": "HYPER_C",
                "name": "🔥 하이퍼큐어 - 변수 경기 3.5골 언더오버",
                "desc": f"고정 축 2경기와 변수 경기({h1_match.home_team} vs {h1_match.away_team})의 3.5골 {opt_uo35['pick_desc']} 베팅을 결합한 초고안정형 헤징 조합",
                "selections": sel_c,
                "total_odds": round(odds_c, 2),
                "expected_prob": round(prob_c * 100, 1),
                "stake_ratio": 20.0,
                "target_return": round(20.0 * odds_c, 1)
            },
            {
                "type": "HYPER_INSURANCE",
                "name": "🛡️ 하이퍼큐어 - 축 무승부 보험 조합",
                "desc": f"고정 축 중 하나인 {b2['match'].home_team}의 이변({ins_desc})을 커버하고 변수 경기는 2.5골 언더오버로 헤징한 축 보험 조합",
                "selections": sel_d,
                "total_odds": round(odds_d, 2),
                "expected_prob": round(prob_d * 100, 1),
                "stake_ratio": 15.0,
                "target_return": round(15.0 * odds_d, 1)
            }
        ]

        return {
            "status": "SUCCESS",
            "bankers": bankers,
            "hedges": [h1],
            "golden_tickets": golden_tickets,
            "hyper_cures": hyper_cures
        }

    def _get_pick_info(self, match: Match, recommendation: str) -> Tuple[str, float, str]:
        """추천 문자열에 해당하는 배당 및 한글 설명 정보를 반환한다."""
        if recommendation == "HOME_WIN":
            return "HOME_WIN", match.odds_home, "홈 승"
        elif recommendation == "DRAW":
            return "DRAW", match.odds_draw, "무승부"
        elif recommendation == "AWAY_WIN":
            return "AWAY_WIN", match.odds_away, "원정 승"
        elif recommendation == "OVER":
            return "OVER", match.odds_over, "2.5 오버"
        elif recommendation == "UNDER":
            return "UNDER", match.odds_under, "2.5 언더"
        else:
            return "HOME_WIN", match.odds_home, "홈 승"

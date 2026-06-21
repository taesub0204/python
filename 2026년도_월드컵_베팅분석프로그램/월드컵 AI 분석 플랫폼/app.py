"""2026 북중미 월드컵 AI 분석 플랫폼 메인 앱."""

from typing import Dict, Any, List
import streamlit as st
import pandas as pd
from datetime import datetime
import time

import importlib
from config.settings import get_config
from data.fetcher import DataFetcher, WC_GROUPS

import analysis.betting_engine
importlib.reload(analysis.betting_engine)
from analysis.betting_engine import BettingEngine

import analysis.llm_commentary
importlib.reload(analysis.llm_commentary)
from analysis.llm_commentary import LLMCommentary

import analysis.combination_engine
importlib.reload(analysis.combination_engine)
from analysis.combination_engine import CombinationEngine

st.set_page_config(page_title="2026 북중미 월드컵 AI 분석", page_icon="🏆", layout="wide")

# 세션 상태 초기화 및 강제 업데이트 (본선 확정 조편성 반영)
APP_VERSION = "4.6" # 일자별 조합 분석 및 가이드 기능 추가로 캐시 리셋

if "app_version" not in st.session_state or st.session_state.app_version != APP_VERSION:
    st.session_state.app_version = APP_VERSION
    for key in ["fetcher", "betting_engine", "llm", "combination_engine"]:
        if key in st.session_state:
            del st.session_state[key]

if "fetcher" not in st.session_state:
    st.session_state.fetcher = DataFetcher()
    st.session_state.betting_engine = BettingEngine()
    st.session_state.llm = LLMCommentary()
    st.session_state.combination_engine = CombinationEngine()

fetcher: DataFetcher = st.session_state.fetcher
engine: BettingEngine = st.session_state.betting_engine
llm: LLMCommentary = st.session_state.llm
comb_engine: CombinationEngine = st.session_state.combination_engine

# 최신 데이터 동기화
fetcher.sync_live_data()

# 사이드바 데이터 새로고침
with st.sidebar:
    st.markdown("### ⚙️ 시스템 도구")
    if st.button("🔄 실시간 데이터 강제 새로고침"):
        st.session_state.fetcher = DataFetcher()
        st.session_state.fetcher.sync_live_data()
        st.rerun()

st.title("🏆 2026 FIFA 북중미 월드컵 AI 분석 플랫폼")
st.markdown("**현재 월드컵 본선 진행 중!** 48개국 조별리그 순위표와 경기 상황에 따른 심리 기반 **언더/오버** 추천 결과를 확인하세요.")

# 실시간 라이브 경기 배너 (현재 시간 기준 진행 중인 경기가 있을 경우 노출)
live_matches = [m for m in fetcher.matches if m.status == "LIVE"]
if live_matches:
    with st.container():
        st.markdown("<h4 style='color: #ff4b4b; margin-top: 0;'>🔴 실시간 라이브 경기 진행 상황</h4>", unsafe_allow_html=True)
        cols = st.columns(len(live_matches))
        for idx, m in enumerate(live_matches):
            elapsed_mins = int((datetime.now() - m.match_time).total_seconds() / 60)
            elapsed_display = f"{elapsed_mins}'" if elapsed_mins <= 90 else "90+'"
            with cols[idx]:
                live_item_html = f"""
                <div style='background: linear-gradient(135deg, #1e1e1e 0%, #111 100%); padding: 15px; border-radius: 10px; border: 1px solid #ff4b4b; text-align: center; margin-bottom: 15px;'>
                    <span style='background-color: #ff4b4b; color: white; padding: 2px 6px; border-radius: 4px; font-size: 0.8em; font-weight: bold;'>LIVE {elapsed_display}</span>
                    <div style='margin-top: 8px; font-size: 1.15em; font-weight: bold; color: white;'>{m.home_team} <span style='color: #ff4b4b;'>{m.home_score}</span> : <span style='color: #ff4b4b;'>{m.away_score}</span> {m.away_team}</div>
                    <div style='font-size: 0.8em; color: gray; margin-top: 4px;'>그룹 {m.group} | 실시간 스코어 자동 반영 중</div>
                </div>
                """
                clean_live_html = " ".join([line.strip() for line in live_item_html.splitlines() if line.strip()])
                st.markdown(clean_live_html, unsafe_allow_html=True)

tab1, tab2, tab3 = st.tabs(["📊 조별 순위표", "🎯 AI 오늘의 분석", "🎯 해외식 토토 조합기"])

with tab1:
    st.header("🌍 실시간 48개국 본선 조별 순위표")
    standings = fetcher.get_standings()
    
    # 3개 열로 나누어 12개 조 배치
    cols = st.columns(3)
    
    group_letters = list(WC_GROUPS.keys())
    for i, g in enumerate(group_letters):
        col_idx = i % 3
        with cols[col_idx]:
            st.subheader(f"Group {g}")
            group_teams = standings[g]
            
            data = []
            for rank, t in enumerate(group_teams):
                data.append({
                    "순위": rank + 1,
                    "국가": t.name,
                    "승점": t.points,
                    "경기수": t.matches_played,
                    "승": t.wins,
                    "무": t.draws,
                    "패": t.losses,
                    "득실차": t.goal_difference
                })
            df = pd.DataFrame(data)
            st.dataframe(df, hide_index=True, use_container_width=True)

@st.cache_data(show_spinner=False)
def get_commentary_cached(match_id: str, home_team: str, away_team: str, h_points: int, h_played: int, a_points: int, a_played: int, uo_rec: str, uo_conf: float, uo_3_5_rec: str, uo_3_5_conf: float) -> str:
    from analysis.llm_commentary import LLMCommentary
    from models.data_models import Match, TeamStats
    from analysis.betting_engine import BettingEngine
    
    dummy_match = Match(id=match_id, group="", home_team=home_team, away_team=away_team, match_time=datetime.now())
    dummy_h = TeamStats(name=home_team, group="", matches_played=h_played)
    dummy_h.wins = h_points // 3
    dummy_h.draws = h_points % 3
    
    dummy_a = TeamStats(name=away_team, group="", matches_played=a_played)
    dummy_a.wins = a_points // 3
    dummy_a.draws = a_points % 3
    
    engine = BettingEngine()
    analysis = engine.analyze_match_context(dummy_match, dummy_h, dummy_a)
    analysis["uo_recommendation"] = uo_rec
    analysis["uo_confidence"] = uo_conf
    analysis["uo_3_5_recommendation"] = uo_3_5_rec
    analysis["uo_3_5_confidence"] = uo_3_5_conf
    
    llm_comp = LLMCommentary()
    return llm_comp.generate_commentary(dummy_match, dummy_h, dummy_a, analysis)

with tab2:
    st.header("🎯 AI 언더/오버 체리피커 분석")
    st.markdown("팀의 상황(승점, 진출 확률)이 만들어내는 심리적 압박감을 분석하여 베팅을 추천합니다.")
    
    upcoming = fetcher.get_upcoming_matches(limit=5)
    
    if not upcoming:
        st.info("예정된 경기가 없습니다. (조별리그 종료)")
    else:
        for match in upcoming:
            with st.container(border=True):
                h_team = fetcher.teams[match.home_team]
                a_team = fetcher.teams[match.away_team]
                
                col1, col2 = st.columns([1, 2])
                with col1:
                    st.subheader(f"🏟 {match.home_team} vs {match.away_team}")
                    st.caption(f"그룹 {match.group} | 경기 시간: {match.match_time.strftime('%Y-%m-%d %H:%M')}")
                    
                    st.write(f"**{match.home_team}**: 승점 {h_team.points}점 ({h_team.matches_played}경기)")
                    st.write(f"**{match.away_team}**: 승점 {a_team.points}점 ({a_team.matches_played}경기)")
                    
                with col2:
                    with st.spinner("AI가 각 팀의 심리를 분석 중입니다..."):
                        analysis = engine.analyze_match_context(match, h_team, a_team)
                        
                        rec_25 = analysis["uo_recommendation"]
                        color_25 = "red" if rec_25 == "OVER" else "blue" if rec_25 == "UNDER" else "gray"
                        
                        rec_35 = analysis.get("uo_3_5_recommendation", "PASS")
                        color_35 = "red" if rec_35 == "OVER" else "blue" if rec_35 == "UNDER" else "gray"
                        
                        # 2.5 골 기준
                        st.markdown(f"**💡 2.5골 기준:** <span style='color:{color_25}; font-size:1.1em; font-weight:bold;'>{rec_25}</span> ({analysis['uo_confidence']}%)", unsafe_allow_html=True)
                        st.progress(analysis["uo_confidence"] / 100.0)
                        
                        # 3.5 골 기준
                        st.markdown(f"**💡 3.5골 기준:** <span style='color:{color_35}; font-size:1.1em; font-weight:bold;'>{rec_35}</span> ({analysis.get('uo_3_5_confidence', 0)}%)", unsafe_allow_html=True)
                        st.progress(analysis.get("uo_3_5_confidence", 0) / 100.0)
                        
                        # 캐싱된 코멘터리 호출
                        commentary = get_commentary_cached(
                            match.id, match.home_team, match.away_team, 
                            h_team.points, h_team.matches_played, 
                            a_team.points, a_team.matches_played, 
                            rec_25, analysis["uo_confidence"], 
                            rec_35, analysis.get("uo_3_5_confidence", 0)
                        )
                        st.info(f"🎙️ **AI 해설위원 코멘트**\n\n{commentary}")

def render_ticket_slip(ticket: Dict[str, Any], budget: float, strategy_type: str):
    # Calculate stake and returns
    stake_ratio = ticket["stake_ratio"]
    stake_krw = int(budget * (stake_ratio / 100.0))
    expected_return_krw = int(stake_krw * ticket["total_odds"])
    
    # Format numbers with commas
    stake_str = f"{stake_krw:,}원"
    return_str = f"{expected_return_krw:,}원"
    
    # Gradient backgrounds and borders based on type
    if strategy_type == "GOLDEN":
        if ticket["type"] == "MAIN":
            border_color = "#ffd700"  # Gold
            bg_gradient = "linear-gradient(135deg, #2d2613 0%, #15120a 100%)"
            badge_color = "#ffd700"
            badge_text_color = "#000000"
        elif ticket["type"] == "HEDGE":
            border_color = "#ff8c00"  # Dark Orange
            bg_gradient = "linear-gradient(135deg, #2e1d0f 0%, #180f08 100%)"
            badge_color = "#ff8c00"
            badge_text_color = "#ffffff"
        else: # BANKER_INSURANCE
            border_color = "#00ced1"  # Dark Turquoise (Blue insurance)
            bg_gradient = "linear-gradient(135deg, #0e2b30 0%, #071518 100%)"
            badge_color = "#00ced1"
            badge_text_color = "#000000"
    else: # HYPER
        if "INSURANCE" in ticket["type"]:
            border_color = "#00ced1"
            bg_gradient = "linear-gradient(135deg, #0e2b30 0%, #071518 100%)"
            badge_color = "#00ced1"
            badge_text_color = "#000000"
        elif "C" in ticket["type"]: # JACKPOT
            border_color = "#9370db"  # Medium Purple
            bg_gradient = "linear-gradient(135deg, #24163d 0%, #120b1f 100%)"
            badge_color = "#9370db"
            badge_text_color = "#ffffff"
        else: # HYPER_A, HYPER_B
            border_color = "#1e90ff"  # Dodger Blue
            bg_gradient = "linear-gradient(135deg, #102542 0%, #081221 100%)"
            badge_color = "#1e90ff"
            badge_text_color = "#ffffff"
            
    # Build selections HTML (clean and join lines to prevent markdown separation)
    selections_html_list = []
    for sel in ticket["selections"]:
        match_time_str = sel["match"].match_time.strftime("%m/%d %H:%M")
        item_html = f"""
        <div style='display: flex; justify-content: space-between; align-items: center; border-bottom: 1px dashed rgba(255,255,255,0.1); padding: 8px 0;'>
            <div style='text-align: left;'>
                <div style='font-size: 0.9em; font-weight: bold; color: #fff;'>{sel['match'].home_team} vs {sel['match'].away_team}</div>
                <div style='font-size: 0.75em; color: #aaa;'>{match_time_str} | 그룹 {sel['match'].group}</div>
            </div>
            <div style='text-align: right;'>
                <span style='background-color: rgba(255,255,255,0.1); color: #fff; padding: 2px 6px; border-radius: 4px; font-size: 0.85em; font-weight: bold; margin-right: 5px;'>{sel['pick_desc']}</span>
                <span style='color: #4da6ff; font-weight: bold; font-size: 0.9em;'>{sel['odds']}배</span>
            </div>
        </div>
        """
        selections_html_list.append(" ".join([line.strip() for line in item_html.splitlines() if line.strip()]))
        
    selections_html = " ".join(selections_html_list)
        
    card_html = f"""
    <div style='background: {bg_gradient}; border: 1px solid {border_color}; padding: 20px; border-radius: 12px; margin-bottom: 20px; box-shadow: 0 4px 15px rgba(0,0,0,0.5);'>
        <div style='display: flex; justify-content: space-between; align-items: center; margin-bottom: 12px;'>
            <span style='font-size: 1.1em; font-weight: bold; color: #fff;'>{ticket['name']}</span>
            <span style='background-color: {badge_color}; color: {badge_text_color}; padding: 3px 8px; border-radius: 20px; font-size: 0.8em; font-weight: bold;'>비중 {stake_ratio}%</span>
        </div>
        <p style='font-size: 0.85em; color: #ccc; margin-top: 0; margin-bottom: 15px;'>{ticket['desc']}</p>
        <div style='margin-bottom: 15px;'>
            {selections_html}
        </div>
        <div style='background-color: rgba(0,0,0,0.3); padding: 12px; border-radius: 8px; font-size: 0.9em;'>
            <div style='display: flex; justify-content: space-between; margin-bottom: 6px;'>
                <span style='color: #aaa;'>조합 총 배당률</span>
                <span style='font-weight: bold; color: #4da6ff;'>{ticket['total_odds']} 배</span>
            </div>
            <div style='display: flex; justify-content: space-between; margin-bottom: 6px;'>
                <span style='color: #aaa;'>추천 베팅 금액</span>
                <span style='font-weight: bold; color: #fff;'>{stake_str}</span>
            </div>
            <div style='display: flex; justify-content: space-between; margin-bottom: 6px;'>
                <span style='color: #aaa;'>적중 시 예상 당첨금</span>
                <span style='font-weight: bold; color: #32cd32; font-size: 1.05em;'>{return_str}</span>
            </div>
            <div style='display: flex; justify-content: space-between;'>
                <span style='color: #aaa;'>AI 분석 예상 확률</span>
                <span style='font-weight: bold; color: #e6e6e6;'>{ticket['expected_prob']}%</span>
            </div>
        </div>
    </div>
    """
    # Clean HTML to prevent any markdown-based formatting issues
    clean_card_html = " ".join([line.strip() for line in card_html.splitlines() if line.strip()])
    st.markdown(clean_card_html, unsafe_allow_html=True)

with tab3:
    st.header("🎯 해외식 토토 조합 및 분석 가이드")
    st.markdown("해외 사이트(Bet365, Pinnacle 등)의 실시간 소수점 배당률 정보를 모니터링하고, 이를 활용해 **골든 티켓(Golden Ticket)** 및 **하이퍼큐어(Hyper Cure)** 베팅 기법을 각 경기에 적용할 수 있는 전략 가이드를 제공합니다.")
    
    # 1. 모든 경기 가져와 일자별(KST)로 그룹핑
    all_upcoming = fetcher.get_upcoming_matches(limit=30)
    
    if not all_upcoming:
        st.info("예정된 경기가 없습니다. (조별리그 종료)")
    else:
        # 일자별 그룹핑
        from collections import defaultdict
        matches_by_date = defaultdict(list)
        for match in all_upcoming:
            date_str = match.match_time.strftime("%Y-%m-%d")
            matches_by_date[date_str].append(match)
            
        sorted_dates = sorted(matches_by_date.keys())
        
        # 일자 선택 UI
        st.subheader("📅 일자별 분석 일정 선택")
        selected_date = st.selectbox("조회할 일자를 선택해 주세요:", options=sorted_dates)
        
        # 해당 날짜의 경기
        date_matches = list(matches_by_date[selected_date])
        
        # 경기 수 보완 (Padding 로직)
        final_matches = list(date_matches)
        is_padded = False
        padded_from_dates = []
        
        if len(final_matches) < 4:
            for match in all_upcoming:
                if match.id not in [m.id for m in final_matches]:
                    final_matches.append(match)
                    m_date = match.match_time.strftime("%Y-%m-%d")
                    if m_date not in padded_from_dates:
                        padded_from_dates.append(m_date)
                    if len(final_matches) >= 4:
                        is_padded = True
                        break
                        
        if is_padded:
            st.info(f"💡 **경기 일정 보완 알림**: 선택하신 날짜({selected_date})의 예정 경기가 {len(date_matches)}개로 조합 구성을 위해 부족합니다. 최적의 베팅 포트폴리오(골든 티켓 & 하이퍼큐어)를 완성하기 위해 다음 인접 일정({', '.join(padded_from_dates)})의 경기를 자동으로 추가하여 베팅 설계를 완료했습니다.")
            
        st.markdown(f"### 📊 {selected_date} 경기 해외 배당률 및 AI 분석 예측 가이드")
        st.markdown("선택하신 날짜의 해외 배당률과 전력 분석(ELO) 및 동기부여를 기반으로 한 승무패 및 언더오버 추천 가이드입니다.")
        
        for match in final_matches:
            is_padded_match = match.id not in [m.id for m in date_matches]
            
            with st.container(border=True):
                h_team_stats = fetcher.teams.get(match.home_team)
                a_team_stats = fetcher.teams.get(match.away_team)
                
                # Betting Engine을 이용해 승무패 분석 획득
                analysis = engine.analyze_match_context(match, h_team_stats, a_team_stats)
                
                col1, col2, col3 = st.columns([2.5, 3.0, 2.5])
                with col1:
                    padding_label = " ⚠️ [인접일 추가 경기]" if is_padded_match else ""
                    st.markdown(f"##### 🏟️ **{match.home_team} vs {match.away_team}**{padding_label}")
                    st.caption(f"그룹 {match.group} | {match.match_time.strftime('%m-%d %H:%M')}")
                    
                with col2:
                    st.markdown("**🎲 해외 사이트 실시간 배당률**")
                    st.markdown(f"홈 승: :green[**{match.odds_home}**] | 무승부: :green[**{match.odds_draw}**] | 원정 승: :green[**{match.odds_away}**]")
                    
                    # 3.5골 배당률 역산
                    odds_uo35_under = comb_engine._get_uo_3_5_odds(match, "UNDER")
                    odds_uo35_over = comb_engine._get_uo_3_5_odds(match, "OVER")
                    st.markdown(f"Under 2.5: ` {match.odds_under} ` | Over 2.5: ` {match.odds_over} `")
                    st.markdown(f"Under 3.5: ` {odds_uo35_under} ` | Over 3.5: ` {odds_uo35_over} `")
                    
                with col3:
                    # AI 추천 픽 및 신뢰도
                    rec = analysis["1x2_recommendation"]
                    rec_korean = {"HOME_WIN": "홈 승", "DRAW": "무승부", "AWAY_WIN": "원정 승"}[rec]
                    conf = analysis["1x2_confidence"]
                    
                    rec_uo = analysis["uo_recommendation"]
                    rec_uo_k = "2.5 언더" if rec_uo == "UNDER" else "2.5 오버" if rec_uo == "OVER" else "패스"
                    conf_uo = analysis["uo_confidence"]
                    
                    rec_uo35 = analysis.get("uo_3_5_recommendation", "UNDER")
                    rec_uo35_k = "3.5 언더" if rec_uo35 == "UNDER" else "3.5 오버"
                    conf_uo35 = analysis.get("uo_3_5_confidence", 80.0)
                    
                    st.markdown("**💡 AI 분석 추천 가이드**")
                    st.markdown(f"**승무패:** {rec_korean} ({conf}%)")
                    st.markdown(f"**2.5골:** {rec_uo_k} ({conf_uo}%)")
                    st.markdown(f"**3.5골:** {rec_uo35_k} ({conf_uo35}%)")
                    
        st.markdown("---")
        
        # 모의 베팅 설계기
        st.subheader("💵 모의 베팅 예산 설계기")
        st.markdown("베팅 예산을 변경하면 추천 가이드에 따른 티켓별 베팅금과 예상 적중 당첨금이 실시간으로 자동 설계됩니다.")
        budget_input = st.slider("💵 총 투자 금액 설정 (원)", min_value=10000, max_value=1000000, value=100000, step=10000, format="%d원")
        
        # 2. 하이퍼큐어 및 골든 티켓 가이드 가이드라인 분석 제공
        st.subheader(f"🏆 {selected_date} 하이퍼큐어 X 골든 티켓 베팅 설계 분석")
        st.markdown(f"선택하신 날짜({selected_date})의 경기 일정(추가 매치 포함)을 활용한 하이퍼큐어 및 골든 티켓 베팅 기법 포트폴리오 분석 가이드입니다.")
        
        # 해당 날짜의 경기로만 포트폴리오 구성
        portfolio = comb_engine.generate_betting_portfolios(final_matches, fetcher.teams)
        
        if portfolio["status"] == "SUCCESS":
            col_gt, col_hc = st.columns(2)
            
            with col_gt:
                st.markdown("### 🎫 골든 티켓(Golden Ticket) 설계 포트폴리오")
                st.markdown("""
                **골든 티켓 기법이란?**
                - 가장 확률이 높은 확실한 **축(Bankers) 경기 3개**를 단일 조합으로 엮어 **100% 집중 투자**하는 메인 주력 전략입니다.
                - 별도의 무승부나 이변 보험을 섞지 않고 강팀들의 정배당 승리에 집중하여 **승률과 배당 메리트를 균형 있게 가져가는 단순 명료한 베팅 방식**입니다.
                """)
                
                # 티켓 슬립 렌더링
                for ticket in portfolio["golden_tickets"]:
                    render_ticket_slip(ticket, budget_input, "GOLDEN")
            
            with col_hc:
                st.markdown("### 🔥 하이퍼큐어(Hyper Cure) 설계 포트폴리오")
                st.markdown("""
                **하이퍼큐어 기법이란?**
                - 절대 부러지지 않을 확실한 **고정 축 경기 2개**를 고정합니다.
                - 여기에 변수가 크고 무승부/역배 가능성이 큰 이변 경기 2개(Hedge 1, Hedge 2)를 다중 분할하여 엮습니다.
                - 고배당 복합 조합(조합 A, 조합 B, 초고배당 조합 C)을 여러 개로 분산 투자하여, 리스크는 치료(Cure)하고 배당 메리트를 극대화하는 기법입니다.
                """)
                
                # 티켓 슬립 렌더링
                for ticket in portfolio["hyper_cures"]:
                    render_ticket_slip(ticket, budget_input, "HYPER")
        else:
            st.warning(portfolio["message"])

st.sidebar.markdown("---")
st.sidebar.caption(f"마지막 업데이트: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}")
if st.sidebar.button("🔄 실시간 결과 수동 갱신", use_container_width=True):
    st.rerun()

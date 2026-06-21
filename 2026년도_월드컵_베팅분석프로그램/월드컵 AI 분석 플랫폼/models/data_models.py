"""데이터 모델."""
from pydantic import BaseModel
from typing import List, Optional
from datetime import datetime

class TeamStats(BaseModel):
    name: str
    group: str
    matches_played: int = 0
    wins: int = 0
    draws: int = 0
    losses: int = 0
    goals_for: int = 0
    goals_against: int = 0
    
    @property
    def points(self) -> int:
        return (self.wins * 3) + (self.draws * 1)
        
    @property
    def goal_difference(self) -> int:
        return self.goals_for - self.goals_against

class Match(BaseModel):
    id: str
    group: str
    home_team: str
    away_team: str
    match_time: datetime
    status: str = "SCHEDULED" # SCHEDULED, LIVE, FINISHED
    home_score: int = 0
    away_score: int = 0
    odds_home: float = 1.0
    odds_draw: float = 1.0
    odds_away: float = 1.0
    odds_under: float = 1.0
    odds_over: float = 1.0
    
    @property
    def total_goals(self) -> int:
        return self.home_score + self.away_score
    
    @property
    def is_over_2_5(self) -> bool:
        return self.total_goals > 2.5

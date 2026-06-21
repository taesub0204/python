"""설정 모듈."""
from pydantic import BaseModel
import os
from pathlib import Path

PROJECT_ROOT = Path(__file__).parent.parent.resolve()

class LLMConfig(BaseModel):
    # 이 부분에 사용자가 발급받을 API 키를 입력하게 됩니다.
    api_key: str = "AQ.Ab8RN6KpGDNNbwO_BqYM76Kh72WqYgk1TxyCm_gYtHB-47lCPA"
    model_name: str = "gemini-2.0-flash"

class AppConfig(BaseModel):
    app_name: str = "2026 북중미 월드컵 AI 분석 플랫폼"
    llm: LLMConfig = LLMConfig()

_config: AppConfig | None = None

def get_config() -> AppConfig:
    global _config
    if _config is None:
        _config = AppConfig()
    return _config

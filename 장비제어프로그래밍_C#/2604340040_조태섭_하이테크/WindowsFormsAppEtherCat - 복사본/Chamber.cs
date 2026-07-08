using System;
using System.Threading.Tasks;

namespace WindowsFormsAppEtherCat
{
    // 챔버의 현재 상태를 나타내는 열거형(Enum)입니다.
    public enum ChamberState { Idle, Processing, Finished }

    /// <summary>
    /// 웨이퍼를 가공하는 챔버(Chamber) 역할을 하는 클래스입니다.
    /// 챔버 내 웨이퍼 유무와 현재 공정 상태를 관리합니다.
    /// </summary>
    public class Chamber
    {
        // 외부(UI 또는 Scheduler)로 로그 메시지를 전달하기 위한 이벤트
        public event Action<string> OnLog;
        
        // 챔버의 현재 상태 (대기, 공정중, 완료)
        public ChamberState State { get; private set; }
        
        // 챔버 안에 웨이퍼가 들어있는지 여부
        public bool HasWafer { get; private set; }

        public Chamber()
        {
            State = ChamberState.Idle; // 처음엔 대기(Idle) 상태
            HasWafer = false;          // 처음엔 비어있음
        }

        /// <summary>
        /// 챔버가 웨이퍼를 받을 준비가 되었는지(대기 상태이면서 비어있는지) 확인합니다.
        /// </summary>
        public bool IsReady()
        {
            return State == ChamberState.Idle && !HasWafer;
        }

        /// <summary>
        /// 로봇이 챔버에 웨이퍼를 투입할 때 호출합니다.
        /// </summary>
        public void PlaceWafer()
        {
            HasWafer = true;
            Log("챔버에 웨이퍼 투입됨.");
        }

        /// <summary>
        /// 공정이 끝난 웨이퍼를 로봇이 꺼내갈 때 호출합니다.
        /// </summary>
        public void PickWafer()
        {
            HasWafer = false;
            State = ChamberState.Idle; // 웨이퍼를 꺼내면 챔버는 다시 대기(Idle) 상태로 전환
            Log("챔버에서 완료된 웨이퍼 꺼냄.");
        }

        /// <summary>
        /// 챔버 공정을 시작합니다. (비동기 Task로 가상의 시간을 시뮬레이션 합니다.)
        /// </summary>
        public async Task StartProcessAsync()
        {
            if (!HasWafer) return; // 웨이퍼가 없으면 공정 불가

            State = ChamberState.Processing;
            Log("챔버 공정 시작...");
            
            // 실제 장비였다면 여기서 하드웨어 제어 코드가 들어갑니다.
            // 시뮬레이션을 위해 1초(1000ms) 대기합니다.
            await Task.Delay(1000); 
            
            State = ChamberState.Finished;
            Log("챔버 공정 완료!");
        }

        // 내부 로그 발생
        private void Log(string message)
        {
            if (OnLog != null)
            {
                OnLog("[Chamber] " + message);
            }
        }
    }
}

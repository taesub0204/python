using System;
using System.Threading.Tasks;

namespace WindowsFormsAppEtherCat
{
    /// <summary>
    /// 웨이퍼를 이송하는 트랜스퍼 로봇(TR_Robot) 클래스입니다.
    /// 로봇의 현재 위치와 웨이퍼 집게(Arm) 상태를 관리합니다.
    /// </summary>
    public class TR_Robot
    {
        // 외부(UI 또는 Scheduler)로 로그 메시지를 전달하기 위한 이벤트
        public event Action<string> OnLog;
        
        // 로봇의 현재 위치 ("Home", "Foup", "Chamber" 등)
        public string CurrentPosition { get; private set; }
        
        // 로봇이 현재 웨이퍼를 쥐고 있는지 여부
        public bool HasWafer { get; private set; }

        public TR_Robot()
        {
            CurrentPosition = "Home"; // 초기 위치
            HasWafer = false;         // 처음엔 빈 손
        }

        /// <summary>
        /// 지정된 위치(목적지)로 로봇을 이동시킵니다.
        /// </summary>
        /// <param name="target">목적지 이름</param>
        public async Task MoveToAsync(string target)
        {
            CurrentPosition = target;
            Log(string.Format("로봇 이동 중 -> {0} 위치로 이동 완료", target));
            
            // 실제 로봇이 이동하는 물리적 시간을 가상(0.5초)으로 구현
            await Task.Delay(500); 
        }

        /// <summary>
        /// Foup에서 웨이퍼를 픽업(Pick) 합니다.
        /// </summary>
        public void PickFromFoup(Foup foup)
        {
            foup.PickWafer(); // Foup의 웨이퍼 개수 차감
            HasWafer = true;  // 로봇은 웨이퍼를 쥔 상태로 변경
            Log("Foup에서 웨이퍼 Pick 완료 (로봇 HasWafer: O)");
        }

        /// <summary>
        /// 챔버에 웨이퍼를 내려놓습니다(Place).
        /// </summary>
        public void PlaceToChamber(Chamber chamber)
        {
            chamber.PlaceWafer(); // 챔버에 웨이퍼 투입
            HasWafer = false;     // 로봇은 빈 손으로 변경
            Log("Chamber에 웨이퍼 Place 완료 (로봇 HasWafer: X)");
        }

        /// <summary>
        /// 챔버에서 작업이 끝난 웨이퍼를 픽업(Pick) 합니다.
        /// </summary>
        public void PickFromChamber(Chamber chamber)
        {
            chamber.PickWafer(); // 챔버를 비움
            HasWafer = true;     // 로봇이 웨이퍼를 쥠
            Log("Chamber에서 웨이퍼 Pick 완료 (로봇 HasWafer: O)");
        }

        /// <summary>
        /// 최종 완료된 웨이퍼를 Foup으로 되돌려 놓습니다(Place).
        /// </summary>
        public void PlaceToFoup(Foup foup)
        {
            foup.PlaceWafer(); // Foup 반환 로직 실행
            HasWafer = false;  // 로봇은 빈 손
            Log("Foup에 웨이퍼 반환(Place) 완료 (로봇 HasWafer: X)");
        }

        // 내부 로그 발생
        private void Log(string message)
        {
            if (OnLog != null)
            {
                OnLog("[TR_Robot] " + message);
            }
        }
    }
}

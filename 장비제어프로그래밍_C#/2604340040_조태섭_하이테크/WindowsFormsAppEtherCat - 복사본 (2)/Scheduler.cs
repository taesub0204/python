using System;
using System.Threading.Tasks;

namespace WindowsFormsAppEtherCat
{
    /// <summary>
    /// Foup, Chamber, TR_Robot을 모두 생성하여 소유하고, 
    /// 이 객체들에게 순서대로 명령을 내리는 두뇌(제어) 역할을 하는 클래스입니다.
    /// </summary>
    public class Scheduler
    {
        // 폼(Form1) 쪽으로 모든 통합 로그를 쏴주기 위한 마스터 이벤트입니다.
        public event Action<string> OnLog;
        
        // 제어 대상인 하위 객체들
        private Foup _foup;
        private Chamber _chamber;
        private TR_Robot _robot;

        public Scheduler()
        {
            // 하위 객체 초기화 (Foup 웨이퍼 5개 세팅)
            _foup = new Foup(5);
            _chamber = new Chamber();
            _robot = new TR_Robot();

            // 3개 객체에서 발생하는 각자의 OnLog 이벤트를 구독(+=)하여 
            // Scheduler의 Log 메서드로 모아줍니다. (로그 통합)
            _foup.OnLog += Log;
            _chamber.OnLog += Log;
            _robot.OnLog += Log;
        }

        /// <summary>
        /// Foup에 웨이퍼가 0개가 될 때까지 1사이클 공정을 무한 반복(자동 모드) 합니다.
        /// </summary>
        public async Task RunAutoAsync()
        {
            Log("=== 스케줄러 자동 모드 시작 ===");
            while (_foup.HasWafer())
            {
                await RunOneCycleAsync();
            }
            Log("=== 모든 웨이퍼 공정 완료 ===");
        }

        /// <summary>
        /// 웨이퍼 1장에 대해 [Foup -> 로봇 이동 -> 챔버 가공 -> 로봇 반환 -> Foup] 
        /// 의 요청하신 1~8단계 시나리오를 1회 실행합니다.
        /// </summary>
        public async Task RunOneCycleAsync()
        {
            if (!_foup.HasWafer())
            {
                Log("Foup에 더 이상 남은 웨이퍼가 없습니다.");
                return;
            }

            Log("--- 1사이클 시작 ---");

            Log("1. 조건 확인: Foup 웨이퍼 유무 및 챔버 Ready 상태 확인");
            // 실제 환경에선 IsReady가 true가 될때까지 Task.Delay로 기다려야 하지만, 여기선 1:1 시나리오이므로 바로 통과
            if (!_chamber.IsReady())
            {
                Log("챔버가 Ready 상태가 아닙니다. 대기합니다.");
                return; 
            }

            Log("2. Scheduler -> TR: Foup 위치로 이동 명령");
            await _robot.MoveToAsync("Foup");

            Log("3. TR: Foup에서 웨이퍼 꺼내기");
            _robot.PickFromFoup(_foup);

            Log("4. Scheduler -> TR: Chamber 위치로 이동 명령");
            await _robot.MoveToAsync("Chamber");

            Log("5. TR: Chamber에 웨이퍼 내려놓기");
            _robot.PlaceToChamber(_chamber);

            Log("6. Scheduler -> Chamber: 공정 시작 명령");
            await _chamber.StartProcessAsync();

            Log("7. TR: Chamber에서 완료된 웨이퍼 꺼내기");
            _robot.PickFromChamber(_chamber);

            Log("8. TR: Foup으로 이동 후 반환");
            await _robot.MoveToAsync("Foup");
            _robot.PlaceToFoup(_foup);

            Log("--- 1사이클 완료 ---");
        }

        // 로그 메시지 처리: 하위 객체들의 로그와 스케줄러의 로그를 모두 받아옵니다.
        private void Log(string message)
        {
            // UI에 로그넷을 연결할 수 있도록 이벤트 발생
            if (OnLog != null)
            {
                OnLog("[Scheduler] " + message);
            }
            // 개발자 확인을 위한 비주얼 스튜디오 디버그 출력
            System.Diagnostics.Debug.WriteLine("[Scheduler] " + message);
        }
    }
}

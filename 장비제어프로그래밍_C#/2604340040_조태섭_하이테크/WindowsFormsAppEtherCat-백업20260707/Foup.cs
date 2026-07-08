using System;

namespace WindowsFormsAppEtherCat
{
    /// <summary>
    /// 웨이퍼를 보관하는 Foup(보관함) 역할을 하는 클래스입니다.
    /// 웨이퍼의 개수를 관리하고, 로봇이 웨이퍼를 꺼내거나 반환할 때 사용됩니다.
    /// </summary>
    public class Foup
    {
        // 외부(UI 또는 Scheduler)로 로그 메시지를 전달하기 위한 이벤트입니다.
        public event Action<string> OnLog;
        
        // 현재 Foup에 남아있는 웨이퍼 개수입니다.
        public int WaferCount { get; private set; }

        /// <summary>
        /// Foup 생성자. 초기 웨이퍼 개수를 설정합니다.
        /// </summary>
        /// <param name="initialCount">초기 웨이퍼 개수</param>
        public Foup(int initialCount)
        {
            WaferCount = initialCount;
            Log(string.Format("Foup 초기 세팅: 웨이퍼 {0}개 설정", WaferCount));
        }

        /// <summary>
        /// Foup 안에 꺼낼 웨이퍼가 남아있는지 확인합니다.
        /// </summary>
        public bool HasWafer()
        {
            return WaferCount > 0;
        }

        /// <summary>
        /// 로봇이 Foup에서 웨이퍼를 하나 꺼낼 때 호출합니다.
        /// 남은 웨이퍼 개수가 1 감소합니다.
        /// </summary>
        public void PickWafer()
        {
            if (WaferCount > 0)
            {
                WaferCount--;
                Log(string.Format("Foup에서 웨이퍼 꺼냄. (남은 개수: {0})", WaferCount));
            }
        }

        /// <summary>
        /// 모든 공정을 마친 웨이퍼를 로봇이 Foup으로 다시 반환할 때 호출합니다.
        /// </summary>
        public void PlaceWafer()
        {
            // 필요에 따라 완료된 웨이퍼 개수를 따로 카운트할 수 있습니다.
            Log("Foup으로 완료된 웨이퍼 반환됨.");
        }

        // 내부에서 발생한 로그를 OnLog 이벤트를 통해 외부로 쏘아줍니다.
        private void Log(string message)
        {
            if (OnLog != null)
            {
                OnLog("[Foup] " + message);
            }
        }
    }
}

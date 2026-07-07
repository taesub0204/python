using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppEtherCat
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 로그(log4net) 초기화 - Logs\semi_e95.log 에 기록됩니다.
            LoggerConfig.Initialize();

            // 공용 EtherCAT 객체 생성
            IEG3268_Dll.IEG3268 sharedEtherCat = new IEG3268_Dll.IEG3268();

            // 메인 윈도우(빈 폼) 생성 및 전체화면 설정
            Form mainForm = new Form();
            mainForm.Text = "SEMI E95 UI - Main";
            mainForm.WindowState = FormWindowState.Maximized;

            // SemiE95View를 메인 윈도우에 꽉 차게 부착
            SemiE95View semiView = new SemiE95View();
            semiView.EtherCAT_M = sharedEtherCat;
            semiView.Dock = DockStyle.Fill;
            mainForm.Controls.Add(semiView);

            // 애플리케이션 시작
            Application.Run(mainForm);
        }
    }
}

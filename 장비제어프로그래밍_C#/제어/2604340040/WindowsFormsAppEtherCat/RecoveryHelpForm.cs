using System;
using System.Windows.Forms;

namespace WindowsFormsAppEtherCat
{
    // 시연 중 알람(오버슈트/드라이브 무응답) 발생 시 복구 순서를 보여주는 모덜리스(비모달) 도움말 창.
    // Show()로 띄워서 조작 화면을 계속 사용하면서 옆에 띄워둘 수 있음.
    public partial class RecoveryHelpForm : Form
    {
        public RecoveryHelpForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

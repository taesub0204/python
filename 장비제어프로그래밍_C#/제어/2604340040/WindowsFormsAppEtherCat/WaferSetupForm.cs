using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsAppEtherCat
{
    public partial class WaferSetupForm : Form
    {
        // 체크된 슬롯 번호(1~5)만 담김 (예: 3번만 체크하면 {3}) — 실제 작업은 이 슬롯들만 수행함
        public List<int> SelectedSlots { get; private set; } = new List<int>();
        public int SelectedWaferCount => SelectedSlots.Count;

        private bool suppressSlotEvents = false;

        public WaferSetupForm()
        {
            InitializeComponent();
        }

        // 전체 선택 체크박스: 켜면 슬롯 5개 모두 체크, 끄면 모두 해제
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressSlotEvents) return;
            suppressSlotEvents = true;
            bool check = chkSelectAll.Checked;
            chkSlot1.Checked = check;
            chkSlot2.Checked = check;
            chkSlot3.Checked = check;
            chkSlot4.Checked = check;
            chkSlot5.Checked = check;
            suppressSlotEvents = false;
        }

        // 슬롯을 개별적으로 체크/해제할 때 5개가 전부 체크됐으면 전체 선택도 같이 체크되도록 동기화
        private void chkSlot_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressSlotEvents) return;
            suppressSlotEvents = true;
            chkSelectAll.Checked = chkSlot1.Checked && chkSlot2.Checked && chkSlot3.Checked && chkSlot4.Checked && chkSlot5.Checked;
            suppressSlotEvents = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedSlots = new List<int>();
            if (chkSlot1.Checked) SelectedSlots.Add(1);
            if (chkSlot2.Checked) SelectedSlots.Add(2);
            if (chkSlot3.Checked) SelectedSlots.Add(3);
            if (chkSlot4.Checked) SelectedSlots.Add(4);
            if (chkSlot5.Checked) SelectedSlots.Add(5);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

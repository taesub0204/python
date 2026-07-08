using System;
using System.Windows.Forms;

namespace WindowsFormsAppEtherCat
{
    public partial class WaferSetupForm : Form
    {
        public int SelectedWaferCount { get; private set; } = 0;

        public WaferSetupForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedWaferCount = 0;
            if (chkSlot1.Checked) SelectedWaferCount++;
            if (chkSlot2.Checked) SelectedWaferCount++;
            if (chkSlot3.Checked) SelectedWaferCount++;
            if (chkSlot4.Checked) SelectedWaferCount++;
            if (chkSlot5.Checked) SelectedWaferCount++;

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

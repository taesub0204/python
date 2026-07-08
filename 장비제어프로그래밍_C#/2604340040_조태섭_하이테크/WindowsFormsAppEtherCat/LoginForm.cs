using System;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsAppEtherCat
{
    public partial class LoginForm : Form
    {
        public bool IsAuthenticated { get; private set; } = false;
        private string saveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "saved_id.txt");

        public LoginForm()
        {
            InitializeComponent();
            LoadSavedId();
        }

        private void LoadSavedId()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    string savedId = File.ReadAllText(saveFilePath).Trim();
                    if (!string.IsNullOrEmpty(savedId))
                    {
                        txtId.Text = savedId;
                        chkSaveId.Checked = true;
                        
                        // Form이 로드될 때 TextBox 포커스 제어
                        this.Load += (s, e) => { txtPw.Focus(); };
                    }
                }
            }
            catch { }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "admin" && txtPw.Text == "admin")
            {
                IsAuthenticated = true;

                // 아이디 저장 처리
                try
                {
                    if (chkSaveId.Checked)
                    {
                        File.WriteAllText(saveFilePath, txtId.Text.Trim());
                    }
                    else
                    {
                        if (File.Exists(saveFilePath))
                        {
                            File.Delete(saveFilePath);
                        }
                    }
                }
                catch { }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호가 틀렸습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

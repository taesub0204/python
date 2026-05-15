using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Addressbook
{
    public partial class Form1 : Form
    {



        int selected = -1; // 선택이 됬는지 
        string connStr = "Server=localhost;Port=3306;Database=library;Uid=root;Pwd=root;"; // 설명 : MySQL 데이터베이스에 연결하기 위한 연결 문자열입니다. "Server"는 데이터베이스 서버의 주소를 나타내며, "Database"는 연결하려는 데이터베이스의 이름을 나타냅니다. "Uid"는 데이터베이스 사용자 이름을 나타내며, "Pwd"는 해당 사용자의 비밀번호를 나타냅니다. 이 문자열은 MySQL 데이터베이스에 연결할 때 사용됩니다.
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }


        private void LoadData()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr)) // using 블럭의 역활 : using 블럭은 IDisposable 인터페이스를 구현하는 객체를 자동으로 해제하는 데 사용됩니다. MySqlConnection 객체는 데이터베이스 연결을 나타내며, using 블럭 내에서 생성되고 사용된 후 자동으로 Dispose() 메서드가 호출되어 리소스가 해제됩니다. 이렇게 하면 데이터베이스 연결이 적절하게 닫히고 리소스 누수가 방지됩니다.
            {
                // MySQL 객체 생성 및 연결 
                conn.Open(); // 연결
                string sql = "select * from contacts";
                MySqlDataAdapter result = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                result.Fill(dt);
                dataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataView.DataSource = dt;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

            }

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr)) // using 블럭의 역활 : using 블럭은 IDisposable 인터페이스를 구현하는 객체를 자동으로 해제하는 데 사용됩니다. MySqlConnection 객체는 데이터베이스 연결을 나타내며, using 블럭 내에서 생성되고 사용된 후 자동으로 Dispose() 메서드가 호출되어 리소스가 해제됩니다. 이렇게 하면 데이터베이스 연결이 적절하게 닫히고 리소스 누수가 방지됩니다.
            {
                // MySQL 객체 생성 및 연결 
                conn.Open(); // 연결

                string sql = @"insert into contacts(name, phone, email, address) values(@name, @phone, @email, @address)"; // 매개변수 @
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@phone", textBox2.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                cmd.Parameters.AddWithValue("@address", textBox4.Text);
                cmd.ExecuteNonQuery(); // SQL 명령 실행
                MessageBox.Show("추가완료!!");

                LoadData(); // 데이터 새로고침
               


            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
          // 삭제를 하는 데 필요한 것은 id값이 필요함

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "delete from contacts where id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", selected);
                cmd.ExecuteNonQuery();
                MessageBox.Show("삭제완료!!");
                LoadData();
            } // 설명 : using 블럭은 IDisposable 인터페이스를 구현하는 객체를 자동으로 해제하는 데 사용됩니다.
              // MySqlConnection 객체는 데이터베이스 연결을 나타내며,
              // using 블럭 내에서 생성되고 사용된 후 자동으로 Dispose() 메서드가 호출되어 리소스가 해제됩니다.
              // 이렇게 하면 데이터베이스 연결이 적절하게 닫히고 리소스 누수가 방지됩니다.






        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void dataView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (!dataView.Rows[e.RowIndex].IsNewRow)
            {


                DataGridViewRow row = dataView.Rows[e.RowIndex];
                selected = Convert.ToInt32(row.Cells["id"].Value);
                textBox1.Text = row.Cells["name"].Value.ToString();
                textBox2.Text = row.Cells["phone"].Value.ToString();
                textBox3.Text = row.Cells["email"].Value.ToString();
                textBox4.Text = row.Cells["address"].Value.ToString();

                //parse 함수는 정수로 바꿔줌
                //convert.toInt32 객체 형태도 바꿤줌 범위가 더 넓음
            }

        }
    }
}

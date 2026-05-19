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
        string connStr = "Server=localhost;Port=3306;Database=library;Uid=root;Pwd=root;"; // 설명 : MySQL 데이터베이스에 연결하기 위한 연결 문자열입니다.
                                                                                           // "Server"는 데이터베이스 서버의 주소를 나타내며,
                                                                                           // "Database"는 연결하려는 데이터베이스의 이름을 나타냅니다.
                                                                                           // "Uid"는 데이터베이스 사용자 이름을 나타내며, "Pwd"는 해당 사용자의 비밀번호를 나타냅니다.
                                                                                           // 이 문자열은 MySQL 데이터베이스에 연결할 때 사용됩니다.
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }


        private void LoadData() // 데이터 조회 및 새로고침
        {
            using (MySqlConnection conn = new MySqlConnection(connStr)) // using 블럭의 역활 : using 블럭은 IDisposable 인터페이스를 구현하는 객체를 자동으로 해제하는 데 사용됩니다.
                                                                        // MySqlConnection 객체는 데이터베이스 연결을 나타내며, using 블럭 내에서 생성되고 사용된 후 자동으로 Dispose() 메서드가 호출되어 리소스가 해제됩니다.
                                                                        // 이렇게 하면 데이터베이스 연결이 적절하게 닫히고 리소스 누수가 방지됩니다.
            {
                // MySQL 객체 생성 및 연결 
                conn.Open(); // 연결
                string sql = "select * from contacts"; // SQL 명령어 작성
                MySqlDataAdapter result = new MySqlDataAdapter(sql, conn); // 인스턴스화 result라는 붕어빵 담을자리 
                DataTable dt = new DataTable(); // DataTable 객체 생성
                result.Fill(dt); // DataTable에 데이터 채우기
                dataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // 설명 : DataGridView 컨트롤의 열 크기를 자동으로 조정하는 모드를 설정하는 코드입니다. DataGridViewAutoSizeColumnsMode.Fill은 열이 DataGridView의 전체 너비를 채우도록 설정하는 모드입니다. 이렇게 하면 열이 DataGridView의 가로 공간을 균등하게 나누어 채우게 됩니다. 이 설정은 열의 크기를 자동으로 조정하여
                                                                                     // DataGridView의 가로 공간을 효율적으로 활용할 수 있도록 도와줍니다.
                dataView.DataSource = dt; // DataGridView의 데이터 소스를
                                          // DataTable로 설정하여 데이터를 표시합니다.
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                selected = -1; // 선택 초기

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

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
       
        }





       


        private void btnInsert_Click(object sender, EventArgs e)

        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("모든 필드를 입력하세요!!");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connStr)) // using 블럭의 역활 : using 블럭은 IDisposable 인터페이스를 구현하는 객체를 자동으로 해제하는 데 사용됩니다.
                                                                        // MySqlConnection 객체는 데이터베이스 연결을 나타내며, using 블럭 내에서 생성되고 사용된 후 자동으로 Dispose() 메서드가 호출되어 리소스가 해제됩니다.
                                                                        // 이렇게 하면 데이터베이스 연결이 적절하게 닫히고 리소스 누수가 방지됩니다.

                // 추가 선택 했을 때 빈셀 인데 추가됨



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
                // 클리어
                clear();
               
               


            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            //if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            //{
            //    MessageBox.Show("모든 필드를 입력하세요!!");
            //    return;
            //}

            // 선택
            if (selected == -1)      
            {
                MessageBox.Show("수정할 데이터를 선택하세요!!");
                return;
            }



            // 업데이트  
            using (MySqlConnection conn = new MySqlConnection(connStr))    // 데이터 베이스 필요함 using 블록 MySqlConnection 객체는 데이터베이스 연결을 나타내며,
                                                                           // using 블록 내에서 생성되고 사용된 후 자동으로 Dispose() 메서드가 호출되어 리소스가 해제됩니다.
                                                                           // 이렇게 하면 데이터베이스 연결이 적절하게 닫히고 리소스 누수가 방지됩니다.
            {
                conn.Open();
                string sql = @"update contacts set name = @name, phone= @phone, email = @email, address = @address where id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@phone", textBox2.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                cmd.Parameters.AddWithValue("@address", textBox4.Text);
                cmd.Parameters.AddWithValue("@id", selected); // 전역변수로 선언
                cmd.ExecuteNonQuery(); // SQL 명령 실행
                MessageBox.Show("수정완료!!");
                LoadData();// 데이터 새로고침
                clear();


            }





        }








        private void btnDelete_Click(object sender, EventArgs e)
        {
          // 삭제를 하는 데 필요한 것은 id값이 필요함

            using (MySqlConnection conn = new MySqlConnection(connStr))    // using 블록 사용
            {
                conn.Open(); 
                string sql = "delete from contacts where id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@phone", textBox2.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                cmd.Parameters.AddWithValue("@address", textBox4.Text);
                cmd.Parameters.AddWithValue("@id", selected); 
                cmd.ExecuteNonQuery();
                LoadData(); 
                clear();


            } 





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
            // ID 영역 누르면 에러 
            if (e.RowIndex < 0) return;
            


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

        private void 추가ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnInsert_Click(sender, e);
        }

        private void 수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(sender, e);
        }

        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDelete_Click(sender, e);
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnLoad_Click(sender, e);
        }

        private void 계산기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cal cal = new Cal();
            cal.ShowDialog(); // 모달 창
         
        }

        private void 숫자맞추기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindNumber fn = new FindNumber();
            fn.Show(); // 모달 창이 아님
        }
    }
}

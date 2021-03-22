using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5951071031_HaNhatHuy
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DemoCrud2;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }

        private void GetStudentRecord()
        {
           

            SqlCommand com = new SqlCommand("SELECT * FROM StudentTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = com.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgvStudent.DataSource = dt;

        }

        private bool IsValiData()
        {
            if(TxtFatherName.Text == string.Empty
                || TxtName.Text == string.Empty
                || txtAddress.Text == string.Empty
                || string.IsNullOrEmpty(TxtPhoneNumber.Text)
                || string.IsNullOrEmpty(TxtRollNumber.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!!","Lỗi dữ liệu"
                    ,MessageBoxButtons.OK
                    ,MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValiData())
            {
                SqlCommand com = new SqlCommand("INSERT INTO " +
                    "StudentTb Values (@Name, @FatherName, @RollNumber" +
                    ", @Address, @Mobile)",con);
                com.Parameters.AddWithValue("@Name", TxtName.Text);
                com.Parameters.AddWithValue("@FatherName", TxtFatherName.Text);
                com.Parameters.AddWithValue("@RollNumber", TxtRollNumber.Text);
                com.Parameters.AddWithValue("@Address", txtAddress.Text);
                com.Parameters.AddWithValue("@Mobile", TxtPhoneNumber.Text);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }
        public int StudentID;
        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if(index >= 0)
            {
                StudentID = Convert.ToInt32(dgvStudent.Rows[index].Cells["StudentID"].Value.ToString());
                TxtName.Text = dgvStudent.Rows[index].Cells["Name"].Value.ToString();
                TxtFatherName.Text = dgvStudent.Rows[index].Cells["FatherName"].Value.ToString();
                txtAddress.Text = dgvStudent.Rows[index].Cells["Address"].Value.ToString();
                TxtPhoneNumber.Text = dgvStudent.Rows[index].Cells["Mobile"].Value.ToString();
                TxtRollNumber.Text = dgvStudent.Rows[index].Cells["RollNumber"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                string query = "update StudentTb set Name = @Name" +
                    ", FatherName = @FatherName, Address = @Address, " +
                    "RollNumber = @RollNumber, " +
                    "Mobile = @Mobile where StudentID=@ID";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@ID", this.StudentID);
                com.Parameters.AddWithValue("@Name", TxtName.Text);
                com.Parameters.AddWithValue("@FatherName", TxtFatherName.Text);
                com.Parameters.AddWithValue("@Address", txtAddress.Text);
                com.Parameters.AddWithValue("@RollNumber", TxtRollNumber.Text);
                com.Parameters.AddWithValue("@Mobile", TxtPhoneNumber.Text);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                GetStudentRecord();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                string query = "delete StudentTb where StudentID = @ID";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                GetStudentRecord();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}

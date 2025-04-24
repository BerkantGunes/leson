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

namespace hospitalmanagement
{
    public partial class Medicine: Form
    {
        public Medicine()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\gunes\Desktop\DEV\FULL-STACK\My Projects\Form Projects\5) Hospital Management Project\Hospital Management System\Hospital Management System\HMSDatabase.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False");
        private void btnDoctors_Click(object sender, EventArgs e)
        {
            Doctors doc = new Doctors();
            doc.Show();
            this.Hide();
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            Patients ptn = new Patients();
            ptn.Show();
            this.Hide();
        }

        private void btnDiagnosis_Click(object sender, EventArgs e)
        {
            Diagnosis dgn = new Diagnosis();
            dgn.Show();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            Admin_Login adm = new Admin_Login();
            adm.Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MedicineDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMedId.Text = MedicineDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtMedicineName.Text = MedicineDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtMedicinetype.Text = MedicineDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtbydoctor.Text = MedicineDGV.SelectedRows[0].Cells[3].Value.ToString();
        }
        void populate()
        {
            Con.Open();
            string query = "select * from MedicineTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            MedicineDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Reset()
        {
            txtMedId.Text = "";
            txtMedicineName.Text = "";
            txtMedicinetype.Text = "";
            txtbydoctor.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMedId.Text == "" && txtMedicineName.Text == "" && txtMedicinetype.Text == "" && txtbydoctor.Text == "")
            {
                MessageBox.Show("Missing Information! Fill all details");
            }
            else
            {
                Con.Open();
                string query = "insert into MedicineTbl values(" + txtMedId.Text + ",'" + txtMedicineName.Text + "','" + txtMedicinetype.Text + "','" + txtbydoctor.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Medicine Added Successfully!");
                Con.Close();
                populate();
                Reset();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "update MedicineTbl set MedicineName = '" + txtMedicineName.Text + "', MedicineType = '" + txtMedicinetype.Text + "', ByDoctor = '" + txtbydoctor.Text + "' where MedicineId = " + txtMedId + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Medicine Informations Updated Successfully!");
            Con.Close();
            populate();
            Reset();
        }

        private void Medicine_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtMedId.Text == "")
            {
                MessageBox.Show("Enter the Medicine Id");
            }
            else
            {
                Con.Open();
                string query = "delete from MedicineTbl where MedicineId = " + txtMedId.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Medicine Deleted Successfully!");
                Con.Close();
                populate();
                Reset();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}

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
    public partial class Diagnosis: Form
    {
        public Diagnosis()
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

        private void btnMedicine_Click(object sender, EventArgs e)
        {
            Medicine mdc = new Medicine();
            mdc.Show();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            Admin_Login admn = new Admin_Login();
            admn.Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void populateId()
        {
            string patientSql = "select * from PatientTbl";
            SqlCommand cmd = new SqlCommand(patientSql, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("PatientId", typeof(int));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                cbPatientId.ValueMember = "PatientId";
                cbPatientId.DataSource = dt;
                Con.Close();
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            {

            }
        }
        string PatientName;
        void FetchPatientName()
        {
            Con.Open();
            string mysql = "select * from PatientTbl where PatientId = " + cbPatientId.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(mysql, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                PatientName = dr["PatientName"].ToString();
                txtPatientName.Text = PatientName;
            }
            Con.Close();
        }
        void populate()
        {
            Con.Open();
            string query = "select * from DiagnosisTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            DiagnosisDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDiagnosisId.Text == "" && txtPatientName.Text == "" && txtSymptoms.Text == "" && txtDiagnosis.Text == "" && txtMedicine.Text =="")
            {
                MessageBox.Show("Missing Information! Fill all details");
            } 
            else
            {
                Con.Open();
                string query = "insert into DiagnosisTbl values(" + txtDiagnosisId.Text + ",'" + txtPatientName.Text + "'," + txtSymptoms.Text + ",'" + txtDiagnosis.Text + "', '"+txtMedicine.Text+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Diagnosis Added Successfully!");
                Con.Close();
                populate();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "update DiagnosisTbl set PatientName = '" + txtPatientName.Text + "', Symptoms = '" + txtSymptoms.Text + "', Diagnosis = '" + txtDiagnosis.Text + "', Medicine = '" + txtMedicine.Text + "' where DiagnosisId = " + txtDiagnosisId.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Diagnosis Informations Updated Successfully!");
            Con.Close();
            populate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtDiagnosisId.Text == "")
            {
                MessageBox.Show("Enter the Diagnosis Id");
            }
            else
            {
                Con.Open();
                string query = "delete from DiagnosisTbl where DiagnosisId = " + txtDiagnosisId.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Diagnosis Deleted Successfully!");
                Con.Close();
                populate();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}

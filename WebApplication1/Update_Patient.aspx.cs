using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class UpdatePatient : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPatients();

            }
        }

        private void LoadPatients()
        {

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Patients WHERE PatientID = @PatientID", con);
                cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    gvPatients.DataSource = dt;
                    gvPatients.DataBind();


                    string gender = dt.Rows[0]["Gender"].ToString();
                    if (gender == "Male")
                        rbtnMale.Checked = true;
                    else if (gender == "Female")
                        rbtnFemale.Checked = true;



                    if (dt.Rows[0]["DOB"] != DBNull.Value)
                        txtDOB.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("yyyy-MM-dd");


                    btnUpdate.Enabled = true;
                    lblMessage.Text = "";
                }
                else
                {
                    lblMessage.Text = "No patient found!";
                    btnUpdate.Enabled = false;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Patients SET ";

                List<string> updates = new List<string>();

                if (!string.IsNullOrWhiteSpace(txtFName.Text))
                {
                    updates.Add("FName = @FName");
                }

                if (!string.IsNullOrWhiteSpace(txtMI.Text))
                {
                    updates.Add("MI = @MI");
                }

                if (!string.IsNullOrWhiteSpace(txtLName.Text))
                {
                    updates.Add("LName = @LName");
                }

                string gender = rbtnMale.Checked ? "Male" : rbtnFemale.Checked ? "Female" : null;
                if (gender != null)
                {
                    updates.Add("Gender = @Gender");
                }

                if (!string.IsNullOrWhiteSpace(txtDOB.Text))
                {
                    DateTime dob = DateTime.Parse(txtDOB.Text);  // Make sure to parse the DOB input
                    updates.Add("DOB = @DOB");
                }

                if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    updates.Add("Phone = @Phone");
                }

                if (!string.IsNullOrWhiteSpace(txtStreet.Text))
                {
                    updates.Add("Street = @Street");
                }

                if (!string.IsNullOrWhiteSpace(txtCity.Text))
                {
                    updates.Add("City = @City");
                }

                if (!string.IsNullOrWhiteSpace(txtZip.Text))
                {
                    updates.Add("Zip = @Zip");
                }

                if (updates.Count > 0)
                {
                    query += string.Join(", ", updates);
                    query += " WHERE PatientID = @PatientID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    if (!string.IsNullOrWhiteSpace(txtFName.Text))
                        cmd.Parameters.AddWithValue("@FName", txtFName.Text);
                    if (!string.IsNullOrWhiteSpace(txtMI.Text))
                        cmd.Parameters.AddWithValue("@MI", txtMI.Text);
                    if (!string.IsNullOrWhiteSpace(txtLName.Text))
                        cmd.Parameters.AddWithValue("@LName", txtLName.Text);
                    if (!string.IsNullOrWhiteSpace(gender))
                        cmd.Parameters.AddWithValue("@Gender", gender);
                    if (!string.IsNullOrWhiteSpace(txtDOB.Text))
                        cmd.Parameters.AddWithValue("@DOB", DateTime.Parse(txtDOB.Text));  // Ensure the date is parsed correctly
                    if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    if (!string.IsNullOrWhiteSpace(txtStreet.Text))
                        cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                    if (!string.IsNullOrWhiteSpace(txtCity.Text))
                        cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    if (!string.IsNullOrWhiteSpace(txtZip.Text))
                        cmd.Parameters.AddWithValue("@Zip", txtZip.Text);

                    cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Patient updated successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Update failed.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "No fields were updated.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

            LoadPatients();
        }

    }
}

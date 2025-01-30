using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class View_Patients_Perscriptions : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvPrescriptions.DataSource = null;
                gvPrescriptions.DataBind();
                gvRefillInfo.DataSource = null;
                gvRefillInfo.DataBind();
            }
        }

        private void LoadPrescriptions()
        {

        }

        private void LoadRefillInfo(string prescriptionId)
        {

        }

        protected void gvPrescriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvPrescriptions.SelectedRow;
            txtPrescriptionID.Text = row.Cells[0].Text;
            txtMedicine.Text = row.Cells[1].Text;
            txtDosage.Text = row.Cells[2].Text;
            txtIntMethod.Text = row.Cells[3].Text;

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            // Load refill info
            LoadRefillInfo(txtPrescriptionID.Text);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = "UPDATE Prescriptions SET ";
                List<string> updates = new List<string>();
                SqlCommand cmd = new SqlCommand();

               
                if (!string.IsNullOrWhiteSpace(txtMedicine.Text))
                {
                    updates.Add("MedName = @MedName");
                    cmd.Parameters.AddWithValue("@MedName", txtMedicine.Text);
                }

                if (!string.IsNullOrWhiteSpace(txtDosage.Text))
                {
                    updates.Add("Dosage = @Dosage");
                    cmd.Parameters.AddWithValue("@Dosage", txtDosage.Text);
                }

                if (!string.IsNullOrWhiteSpace(txtIntMethod.Text))
                {
                    updates.Add("IntMethod = @IntMethod");
                    cmd.Parameters.AddWithValue("@IntMethod", txtIntMethod.Text);
                }

                
                if (updates.Count > 0)
                {
                    query += string.Join(", ", updates);
                    query += " WHERE PrescriptionID = @PrescriptionID";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@PrescriptionID", txtPrescriptionID.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lblMessage.Text = "Prescription updated successfully.";
                }
                else
                {
                    lblMessage.Text = "No changes to update.";
                }
            }

            
            LoadPrescriptions();

            
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }


        protected void btnUpdateRefill_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrescriptionID.Text))
            {
                lblMessage2.Text = "Error: No prescription selected!";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                return; 
            }

            
            if (string.IsNullOrWhiteSpace(txtRefillDate.Text))
            {
                lblMessage2.Text = "Error: Please enter the refill date.";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Prescriptions SET Refillsleft = @RefillsLeft WHERE PrescriptionID = @PrescriptionID", con);
                cmd.Parameters.AddWithValue("@RefillsLeft", txtRefillCount.Text);
                cmd.Parameters.AddWithValue("@PrescriptionID", txtPrescriptionID.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Refills SET RefillDate = @RefillDate, Status = @Status WHERE PrescriptionID = @PrescriptionID", con);
                cmd.Parameters.AddWithValue("@RefillDate", DateTime.Parse(txtRefillDate.Text));
                cmd.Parameters.AddWithValue("@PrescriptionID", txtPrescriptionID.Text);

                 
                if (chkPending.Checked)
                {
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                }
                else if (chkCompleted.Checked)
                {
                    cmd.Parameters.AddWithValue("@Status", "Completed");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Status", DBNull.Value); // no checkbox is checked
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            lblMessage2.Text = "Refill updated successfully.";
            lblMessage2.ForeColor = System.Drawing.Color.Green;

            
            LoadRefillInfo(txtPrescriptionID.Text);
        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Prescriptions WHERE PrescriptionID = @PrescriptionID", con);
                cmd.Parameters.AddWithValue("@PrescriptionID", txtPrescriptionID.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            
            LoadPrescriptions();

            
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prescriptions WHERE PrescriptionID = @PrescriptionID OR @PrescriptionID = ''", con);
                cmd.Parameters.AddWithValue("@PrescriptionID", txtPatSearch.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lblMessage.Text = string.Empty;

                if (dt.Rows.Count > 0)
                {
                    gvPrescriptions.DataSource = dt;
                    gvPrescriptions.DataBind();

                    
                    if (dt.Rows.Count == 1)
                    {
                        txtPrescriptionID.Text = dt.Rows[0]["PrescriptionID"].ToString();
                    }
                }
                else
                {
                    gvPrescriptions.DataSource = null;
                    gvPrescriptions.DataBind();
                    lblMessage.Text = "No Prescription Info was found.";
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Refills WHERE PrescriptionID = @PrescriptionID OR @PrescriptionID = ''", con);
                cmd.Parameters.AddWithValue("@PrescriptionID", txtPatSearch.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lblMessage2.Text = string.Empty;

                if (dt.Rows.Count > 0)
                {
                    gvRefillInfo.DataSource = dt;
                    gvRefillInfo.DataBind();
                }
                else
                {
                    gvRefillInfo.DataSource = null;
                    gvRefillInfo.DataBind();
                    lblMessage2.Text = "No Refill Info was found.";
                }
            }
        }




        protected void txtPatientSearch_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("");
        }
        protected void btnSearch_Click2(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Refills";
                SqlCommand cmd = new SqlCommand();

                if (!string.IsNullOrWhiteSpace(txtRXsearch.Text.Trim()))
                {
                    query += " WHERE RX_NO = @RX_NO";
                    cmd.Parameters.AddWithValue("@RX_NO", txtRXsearch.Text.Trim());
                }

                cmd.CommandText = query;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lblMessage2.Text = string.Empty;

                
                gvRefillInfo.DataSource = dt;
                gvRefillInfo.DataBind();

               
                if (dt.Rows.Count == 0)
                {
                    lblMessage2.Text = "No Refill Info was found.";
                }
            }
        }


        protected void chkPending_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPending.Checked)
            {
                chkCompleted.Checked = false;
                chkCompleted.Enabled = false;
            }
            else
            {
                chkCompleted.Enabled = true;
            }
        }

        protected void chkCompleted_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompleted.Checked)
            {
                chkPending.Checked = false;
                chkPending.Enabled = false;
            }
            else
            {
                chkPending.Enabled = true;
            }
        }

    }
}

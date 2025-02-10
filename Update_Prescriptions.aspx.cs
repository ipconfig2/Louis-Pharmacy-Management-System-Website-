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
<<<<<<< HEAD
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

            
=======
                LoadPrescriptions();
                LoadRefillInfo();
            }
        }


        private void LoadPrescriptions()
        {
            try
            {
                Class obj = new Class();
                DataTable dt = obj.GetAllPrescriptions(); 

                if (dt.Rows.Count > 0)
                {
                    gvPrescriptions.DataSource = dt;
                    gvPrescriptions.DataBind();
                }
                else
                {
                    gvPrescriptions.DataSource = null;
                    gvPrescriptions.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading prescriptions: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }




        private void LoadRefillInfo()
        {
            try
            {
                Class obj = new Class();
                DataTable dt = obj.GetAllRefills(); // Fetch all refills

                if (dt.Rows.Count > 0)
                {
                    gvRefillInfo.DataSource = dt;
                    gvRefillInfo.DataBind();
                }
                else
                {
                    gvRefillInfo.DataSource = null;
                    gvRefillInfo.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage2.Text = "Error loading refills: " + ex.Message;
                lblMessage2.ForeColor = System.Drawing.Color.Red;
            }
        }




        protected void gvPrescriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvPrescriptions.SelectedRow != null)
            {
                GridViewRow row = gvPrescriptions.SelectedRow;
                txtPrescriptionID.Text = row.Cells[1].Text; // Adjust index based on your columns
                txtMedicine.Text = row.Cells[2].Text;
                txtDosage.Text = row.Cells[3].Text;
                txtIntMethod.Text = row.Cells[4].Text;

                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrescriptionID.Text))
            {
                lblMessage.Text = "Error: No prescription selected!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                string prescriptionID = txtPrescriptionID.Text.Trim();
                string medName = txtMedicine.Text.Trim();
                string dosage = txtDosage.Text.Trim();
                string intMethod = txtIntMethod.Text.Trim();
                int? refillsLeft = string.IsNullOrWhiteSpace(txtRefillCount.Text) ? (int?)null : int.Parse(txtRefillCount.Text.Trim());

                Class obj = new Class();
                bool isUpdated = obj.UpdatePrescription(prescriptionID, medName, dosage, intMethod, refillsLeft);

                if (isUpdated)
                {
                    lblMessage.Text = "Prescription updated successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "No changes were made or prescription not found.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                LoadPrescriptions(); // Refresh the prescription list
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating prescription: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }



        protected void btnUpdateRefill_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRXsearch.Text))
            {
                lblMessage2.Text = "Error: No RX number provided!";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrescriptionID.Text))
            {
                lblMessage2.Text = "Error: No Prescription ID provided!";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                return;
            }

>>>>>>> update message
            if (string.IsNullOrWhiteSpace(txtRefillDate.Text))
            {
                lblMessage2.Text = "Error: Please enter the refill date.";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                return;
            }

<<<<<<< HEAD
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
=======
            if (string.IsNullOrWhiteSpace(txtRefillCount.Text))
            {
                lblMessage2.Text = "Error: Please enter the number of refills left.";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string status = "";
            if (chkPending.Checked)
                status = "Pending";
            else if (chkCompleted.Checked)
                status = "Completed";

            try
            {
                Class obj = new Class();
                bool isUpdated = obj.UpdateRefill(
                    txtRXsearch.Text.Trim(),
                    txtPrescriptionID.Text.Trim(),
                    txtRefillDate.Text.Trim(),
                    status,
                    int.Parse(txtRefillCount.Text.Trim()) // Convert to int
                );

                if (isUpdated)
                {

                }
                else
                {
                    lblMessage2.Text = "Refill updated successfully.";
                    lblMessage2.ForeColor = System.Drawing.Color.Green;
                }

                LoadRefillInfo();   // Refresh refill grid
                LoadPrescriptions(); // Refresh prescription grid (since RefillsLeft is updated)
            }
            catch (Exception ex)
            {
                lblMessage2.Text = "Error updating refill: " + ex.Message;
                lblMessage2.ForeColor = System.Drawing.Color.Red;
            }
        }



>>>>>>> update message



        protected void btnDelete_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
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
=======
            if (string.IsNullOrWhiteSpace(txtPrescriptionID.Text))
            {
                lblMessage.Text = "Error: No prescription selected!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string prescriptionID = txtPrescriptionID.Text.Trim();

            try
            {
                Class obj = new Class();
                bool isDeleted = obj.DeletePresc(prescriptionID);

                if (isDeleted)
                {
                    lblMessage.Text = "Prescription deleted successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Prescription not found.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                LoadPrescriptions(); // Refresh prescriptions grid
                LoadRefillInfo();    // Refresh refills grid
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting prescription: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }





        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtPatSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                lblMessage.Text = "Please enter a search term.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                Class obj = new Class();
                DataTable dt = obj.SearchPrescriptions(searchTerm);
>>>>>>> update message

                if (dt.Rows.Count > 0)
                {
                    gvPrescriptions.DataSource = dt;
                    gvPrescriptions.DataBind();
<<<<<<< HEAD

                    
                    if (dt.Rows.Count == 1)
                    {
                        txtPrescriptionID.Text = dt.Rows[0]["PrescriptionID"].ToString();
                    }
=======
                    lblMessage.Text = ""; // Clear error message
>>>>>>> update message
                }
                else
                {
                    gvPrescriptions.DataSource = null;
                    gvPrescriptions.DataBind();
<<<<<<< HEAD
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
=======
                    lblMessage.Text = "No matching prescriptions found.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error searching for prescriptions: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
>>>>>>> update message
            }
        }


<<<<<<< HEAD


=======
>>>>>>> update message
        protected void txtPatientSearch_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("");
        }
        protected void btnSearch_Click2(object sender, EventArgs e)
        {
<<<<<<< HEAD
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


=======
            string searchTerm = txtRXsearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                lblMessage2.Text = "Please enter an RX number or Prescription ID.";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                Class obj = new Class();
                DataTable dt = obj.SearchRefills(searchTerm);

                if (dt.Rows.Count > 0)
                {
                    gvRefillInfo.DataSource = dt;
                    gvRefillInfo.DataBind();
                    lblMessage2.Text = ""; // Clear error message
                }
                else
                {
                    gvRefillInfo.DataSource = null;
                    gvRefillInfo.DataBind();
                    lblMessage2.Text = "No matching refills found.";
                    lblMessage2.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage2.Text = "Error searching for refills: " + ex.Message;
                lblMessage2.ForeColor = System.Drawing.Color.Red;
            }
        }



>>>>>>> update message
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

<<<<<<< HEAD
    }
}
=======
        protected void btnAddPrescription_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text) || string.IsNullOrWhiteSpace(txtPhysicianID.Text) ||
                string.IsNullOrWhiteSpace(txtMedicine.Text) || string.IsNullOrWhiteSpace(txtDosage.Text) ||
                string.IsNullOrWhiteSpace(txtIntMethod.Text) || string.IsNullOrWhiteSpace(txtRefillCount.Text) ||
                string.IsNullOrWhiteSpace(txtRefillDate.Text))
            {
                lblMessage.Text = "Error: Please fill in all fields.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                string patientID = txtPatientID.Text.Trim();
                string physicianID = txtPhysicianID.Text.Trim();
                string medName = txtMedicine.Text.Trim();
                string dosage = txtDosage.Text.Trim();
                string intMethod = txtIntMethod.Text.Trim();
                int refillsLeft = int.Parse(txtRefillCount.Text.Trim());
                DateTime initialRefillDate = DateTime.Parse(txtRefillDate.Text.Trim());

                Class obj = new Class();
                bool isAdded = obj.AddPrescription(patientID, physicianID, medName, dosage, intMethod, refillsLeft, initialRefillDate);

                if (isAdded)
                {
                    lblMessage.Text = "Prescription and first refill added successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Error adding prescription.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                LoadPrescriptions(); // Refresh prescription grid
                LoadRefillInfo();    // Refresh refill grid
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error adding prescription: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

}

>>>>>>> update message

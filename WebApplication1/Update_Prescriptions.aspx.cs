using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;

namespace WebApplication1
{
    public partial class Update_Perscriptions : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                gvPrescriptions.DataSource = null;
                gvPrescriptions.DataBind();
                gvRefillInfo.DataSource = null;
                gvRefillInfo.DataBind();
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

                // Load refill info
                Class obj = new Class();
obj.LoadRefills(txtPrescriptionID.Text);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedicine.Text) || txtMedicine.Text.Length > 50)
            {
                ShowMessage(lblMessage, "Error: Medicine Name is required and must be less than 50 characters!", Color.Red);
                return;
            }

            Class obj = new Class();

            // Check if the prescription already exists
            if (obj.DoesPrescriptionExist(txtMedicine.Text.Trim(), txtPatientID.Text.Trim()))
            {
                ShowMessage(lblMessage, "Error: Prescription already exists for this patient!", Color.Red);
                return;
            }

            try
            {
                bool isUpdated = obj.UpdatePrescription(
                    txtPrescriptionID.Text.Trim(),
                    txtMedicine.Text.Trim(),
                    txtDosage.Text.Trim(),
                    txtIntMethod.Text.Trim(),
                    int.Parse(txtRefillCount.Text.Trim())
                );

                if (isUpdated)
                {
                    ShowMessage(lblMessage, "Prescription updated successfully!", Color.Green);
                    LoadPrescriptions();
                }
                else
                {
                    ShowMessage(lblMessage, "Error updating prescription.", Color.Red);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(lblMessage, "Unexpected error: " + ex.Message, Color.Red);
            }
        }


        // Helper Function to Show Messages
        private void ShowMessage(Label lbl, string message, Color color)
        {
            lbl.Text = message;
            lbl.ForeColor = color;
        }

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtPrescriptionID.Text))
        //    {
        //        ShowMessage(lblMessage, "Error: No prescription selected!", Color.Red);
        //        return;
        //    }

        //try
        //    {
        //        string prescriptionID = txtPrescriptionID.Text.Trim();
        //        string medName = txtMedicine.Text.Trim();
        //        string dosage = txtDosage.Text.Trim();
        //        string intMethod = txtIntMethod.Text.Trim();
        //        int? refillsLeft = string.IsNullOrWhiteSpace(txtRefillCount.Text) ? (int?)null : int.Parse(txtRefillCount.Text.Trim());

        //        Class obj = new Class();
        //        bool isUpdated = obj.UpdatePrescription(prescriptionID, medName, dosage, intMethod, refillsLeft);

        //        if (isUpdated)
        //        {
        //            ShowMessage(lblMessage, "Prescription updated successfully!", Color.Green);
        //            LoadPrescriptions(); // Refresh prescription list
        //        }
        //        else
        //        {
        //            ShowMessage(lblMessage, "No changes made or prescription not found.", Color.Red);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessage(lblMessage, "Error updating prescription: " + ex.Message, Color.Red);
        //    }
        //}


        //private void ShowMessage(Label lbl, string message, Color color)
        //{
        //    lbl.Text = message;
        //    lbl.ForeColor = color;
        //}



        protected void btnUpdateRefill_Click(object sender, EventArgs e)
        {
            lblMessage2.Text = ""; // Clear previous messages
            lblMessage2.Visible = false; // Hide the label unless an error occurs

            if (string.IsNullOrWhiteSpace(txtRXsearch.Text) || string.IsNullOrWhiteSpace(txtPrescriptionID.Text))
            {
                lblMessage2.Text = "❌ Error: RX number and Prescription ID must be provided!";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                lblMessage2.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRefillDate.Text) || string.IsNullOrWhiteSpace(txtRefillCount.Text))
            {
                lblMessage2.Text = "❌ Error: Please enter refill date and refill count!";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                lblMessage2.Visible = true;
                return;
            }

            string status = chkPending.Checked ? "Pending" : chkCompleted.Checked ? "Completed" : "";

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

                if (!isUpdated) // Show error message only if update fails
                {
                    lblMessage2.Text = "❌ Error: RX Number and Prescription ID do not match or update failed.";
                    lblMessage2.ForeColor = System.Drawing.Color.Red;
                    lblMessage2.Visible = true;
                }
                else
                {
                    lblMessage2.Text = " Success.";
                    lblMessage2.ForeColor = System.Drawing.Color.Green;
                    lblMessage2.Visible = true;
                }

                LoadRefillInfo();   // Refresh refill grid
                LoadPrescriptions(); // Refresh prescription grid
            }
            catch (Exception ex)
            {
                lblMessage2.Text = $"⚠️ Error updating refill: {ex.Message}";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                lblMessage2.Visible = true;
            }

            lblMessage2.Text = "Refill updated successfully.";
            lblMessage2.ForeColor = System.Drawing.Color.Green;

            Class obj2 = new Class();
            obj2.LoadRefills(txtPrescriptionID.Text);
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
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


        protected void ValidateRefillCount(object source, ServerValidateEventArgs args)
        {
            if (int.TryParse(txtRefillCount.Text, out int value))
            {
                args.IsValid = value > 0;
            }

            else
            {
                args.IsValid = false;
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtPatSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                ShowMessage(lblMessage, "Please enter a search term.", Color.Red);
                return;
            }

            try
            {
                Class obj = new Class();
                DataTable dt = obj.SearchPrescriptions(searchTerm);

                if (dt.Rows.Count > 0)
                {
                    gvPrescriptions.DataSource = dt;
                    gvPrescriptions.DataBind();
                    lblMessage.Text = ""; // Clear error message
                }
                else
                {
                    gvPrescriptions.DataSource = null;
                    gvPrescriptions.DataBind();
                    ShowMessage(lblMessage, "No matching prescriptions found.", Color.Red);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(lblMessage, "Error searching prescriptions: " + ex.Message, Color.Red);
            }
        }
        //old search
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    string searchTerm = txtPatSearch.Text.Trim();

        //    if (string.IsNullOrWhiteSpace(searchTerm))
        //    {
        //        lblMessage.Text = "Please enter a search term.";
        //        lblMessage.ForeColor = System.Drawing.Color.Red;
        //        return;
        //    }

        //    try
        //    {
        //        Class obj = new Class();
        //        DataTable dt = obj.SearchPrescriptions(searchTerm);

        //        if (dt.Rows.Count > 0)
        //        {
        //            gvPrescriptions.DataSource = dt;
        //            gvPrescriptions.DataBind();
        //            lblMessage.Text = ""; // Clear error message
        //        }
        //        else
        //        {
        //            gvPrescriptions.DataSource = null;
        //            gvPrescriptions.DataBind();
        //            lblMessage.Text = "No matching prescriptions found.";
        //            lblMessage.ForeColor = System.Drawing.Color.Red;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Error searching for prescriptions: " + ex.Message;
        //        lblMessage.ForeColor = System.Drawing.Color.Red;
        //    }
        //}




        protected void txtPatientSearch_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("");
        }
        protected void btnSearch_Click2(object sender, EventArgs e)
        {
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

        protected void btnAddPrescription_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text) || string.IsNullOrWhiteSpace(txtPhysicianID.Text) ||
                string.IsNullOrWhiteSpace(txtMedicine.Text) || string.IsNullOrWhiteSpace(txtDosage.Text) ||
                string.IsNullOrWhiteSpace(txtIntMethod.Text) || string.IsNullOrWhiteSpace(txtRefillCount.Text) ||
                string.IsNullOrWhiteSpace(txtRefillDate.Text))
            {
                lblMessage.Text = "Error: Please fill in all fields; Medicine, Dose, Intake, PhyID, PatID, and Refill count and date";
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


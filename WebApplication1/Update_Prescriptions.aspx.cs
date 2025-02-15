using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

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
                DataTable dt = obj.GetAllRefills(); 

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
                txtPrescriptionID.Text = row.Cells[1].Text;
                txtMedicine.Text = row.Cells[4].Text;
                txtDosage.Text = row.Cells[5].Text;
                txtIntMethod.Text = row.Cells[6].Text;
                txtRefillCount.Text = row.Cells[7].Text;
                txtPatientID.Text = row.Cells[2].Text;
                txtPhysicianID.Text = row.Cells[3].Text;


               
                txtPatientID.Enabled = false;
                txtPhysicianID.Enabled = false;
                txtPrescriptionID.Enabled = false;


                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;

                // Load refill info
                Class obj = new Class();
                obj.LoadRefills(txtRXID.Text);
                obj.LoadRefills(txtPrescriptionID.Text);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAndEnableTextBoxes(this);
        }

        private void ClearAndEnableTextBoxes(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox txt)
                {
                    txt.Text = ""; 
                    txt.Enabled = true;  
                }
                else if (c.HasControls())
                {
                    ClearAndEnableTextBoxes(c);
                }
            }
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (string.IsNullOrWhiteSpace(txtPrescriptionID.Text)) 
            {
                lblMessage.Text = "❌ Error: Prescription ID is required.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                string prescriptionID = txtPrescriptionID.Text.Trim();
                string patientID = txtPatientID.Text.Trim();  
                string physicianID = txtPhysicianID.Text.Trim();
                string medName = txtMedicine.Text.Trim();
                string dosage = txtDosage.Text.Trim();
                string intMethod = txtIntMethod.Text.Trim();
                DateTime refillDate = DateTime.Parse(txtRefillDate.Text.Trim());
                string status = chkPending.Checked ? "Pending" : chkCompleted.Checked ? "Completed" : "";

                Class obj = new Class();
                string result = obj.UpdatePrescription(prescriptionID, patientID, physicianID, medName, dosage, intMethod, refillDate, status);

                lblMessage.Text = result;
                lblMessage.ForeColor = result.StartsWith("✅") ? System.Drawing.Color.Green : System.Drawing.Color.Red;

    
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Error updating prescription: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }





     
        private void ShowMessage(Label lbl, string message, Color color)
        {
            lbl.Text = message;
            lbl.ForeColor = color;
        }



        protected void btnUpdateRefill_Click(object sender, EventArgs e)
        {
            lblMessage2.Text = "";
            lblMessage2.Visible = false;

            if (string.IsNullOrWhiteSpace(txtRXsearch.Text) || string.IsNullOrWhiteSpace(txtRXID.Text))
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
                    txtRXID.Text.Trim(),
                    txtRefillDate.Text.Trim(),
                    status,
                    int.Parse(txtRefillCount.Text.Trim())
                );

                if (isUpdated)
                {
                    lblMessage2.Text = "✅ Refill updated successfully.";
                    lblMessage2.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage2.Text = "❌ Error: Refills left cannot be increased or prescription does not exist.";
                    lblMessage2.ForeColor = System.Drawing.Color.Red;
                }

                lblMessage2.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage2.Text = $"⚠️ Error updating refill: {ex.Message}";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                lblMessage2.Visible = true;
            }

            Class obj2 = new Class();
            obj2.LoadRefills(txtRXID.Text);
  
        }






        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrescriptionID.Text))
            {
                lblMessage.Text = "❌ Error: No prescription selected!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string prescriptionID = txtPrescriptionID.Text.Trim();

            try
            {
                Class obj = new Class();
                string result = obj.DeletePrescription(prescriptionID);

                lblMessage.Text = result;
                lblMessage.ForeColor = result.StartsWith("✅") ? System.Drawing.Color.Green : System.Drawing.Color.Red;

  
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Error deleting prescription: " + ex.Message;
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
                    lblMessage.Text = ""; 
                }
                else
                {
                    gvPrescriptions.DataSource = null;
                    gvPrescriptions.DataBind();
                    lblMessage.Text = "No matching prescriptions found.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                ShowMessage(lblMessage, "Error searching prescriptions: " + ex.Message, Color.Red);
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
            string searchTerm = txtRXsearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                lblMessage2.Text = "Please enter an RX number.";
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
            lblMessage.Text = "";
            lblMessage.ForeColor = System.Drawing.Color.Black;

            if (string.IsNullOrWhiteSpace(txtPatientID.Text) || string.IsNullOrWhiteSpace(txtPhysicianID.Text) ||
                string.IsNullOrWhiteSpace(txtMedicine.Text) || string.IsNullOrWhiteSpace(txtDosage.Text) ||
                string.IsNullOrWhiteSpace(txtIntMethod.Text) || string.IsNullOrWhiteSpace(txtRefillCount.Text) ||
                string.IsNullOrWhiteSpace(txtRefillDate.Text))
            {
                lblMessage.Text = "❌ Error: Please fill in all fields.";
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
                string result = obj.AddPrescription(patientID, physicianID, medName, dosage, intMethod, refillsLeft, initialRefillDate);

                if (result.StartsWith("✅"))
                {
                    lblMessage.Text = result;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = result; 
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

  
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Error adding prescription: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnDeleteRefill_Click(object sender, EventArgs e)
        {
            lblMessage2.Text = "";

            if (string.IsNullOrWhiteSpace(txtRXsearch.Text))
            {
                lblMessage2.Text = "❌ Error: RX number is required.";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                
                if (!int.TryParse(txtRXsearch.Text.Trim(), out int rxNo))
                {
                    lblMessage2.Text = "❌ Error: RX number must be a valid number.";
                    lblMessage2.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                
                Class obj = new Class();
                string result = obj.DeleteRefill(rxNo);

                lblMessage2.Text = result;
                lblMessage2.ForeColor = result.StartsWith("✅") ? System.Drawing.Color.Green : System.Drawing.Color.Red;

              
            }
            catch (Exception ex)
            {
                lblMessage2.Text = "❌ Error deleting refill.";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
            }
        }



        protected void btnAddRefill_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (string.IsNullOrWhiteSpace(txtRXID.Text))
            {
                lblMessage.Text = "❌ Error: Prescription ID is required.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                string prescriptionID = txtRXID.Text.Trim();
                DateTime refillDate = string.IsNullOrWhiteSpace(txtRefillDate.Text) ? DateTime.Now : DateTime.Parse(txtRefillDate.Text.Trim());

                Class obj = new Class();
                string result = obj.AddRefill(prescriptionID, refillDate);

                lblMessage.Text = result;
                lblMessage.ForeColor = result.StartsWith("✅") ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            ; 
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Error adding refill: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}




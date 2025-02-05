using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class FrmPatientPrescriptionSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string patientID = txtPatientSearch.Text.Trim();
            if (string.IsNullOrEmpty(patientID))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a Patient ID.');", true);
                return;
            }

            Class loader = new Class();
            loader.LoadPatientInfo(patientID, gvPrescriptions, lblPatientName, lblDOB, lblAddress, lblPhone);

            if (gvPrescriptions.Rows.Count > 0)
            {
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No prescriptions found.');", true);
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }

        protected void gvPrescriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvPrescriptions.SelectedRow != null)
            {
                string prescriptionID = gvPrescriptions.SelectedRow.Cells[0].Text;

                string confirmMessage = "Are you sure you want to delete this prescription?";
                string script = $"if (confirm('{confirmMessage}')) {{ __doPostBack('{btnDelete.ClientID}', ''); }}";
                ClientScript.RegisterStartupScript(this.GetType(), "confirmDelete", script, true);
            }
            else
            {
                string message = "Please select a prescription to delete.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gvPrescriptions.SelectedRow != null)
            {
                string prescID = gvPrescriptions.SelectedRow.Cells[0].Text;
                Response.Redirect($"UpdatePrescription.aspx?prescID={prescID}");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a prescription to update.');", true);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            // Redirect to a home page or close the current page
            Response.Redirect("Homepage.aspx");
        }
    }
}
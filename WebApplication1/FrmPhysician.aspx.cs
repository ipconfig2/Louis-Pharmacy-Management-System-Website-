using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class FrmPhysician : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvPhysicianData.DataSource = null;
            gvPhysicianData.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Class dataService = new Class();
            DataSet physicianData = dataService.SearchPhysician(txtSearch.Text.Trim());

            gvPhysicianData.DataSource = physicianData.Tables[0];
            gvPhysicianData.DataBind();


            if (physicianData.Tables[0].Rows.Count == 0)
            {
                lblSearch.Text = "No records found.";
            }
        }

        string physicianId, Fname, LName, Phone, Email;

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string selectedMode = Mode.SelectedValue;

            if (selectedMode == "Add")
            {
                Fname = null;
                LName = null;
                Phone = null;
                Email = null;
                Fname = txtFname.Text.Trim();
                LName = txtLname.Text.Trim();
                Phone = txtPhone.Text.Trim();
                Email = txtEmail.Text.Trim();
                Class dataService2 = new Class();
                string message = dataService2.AddPhysician(txtFname.Text, txtLname.Text, txtEmail.Text, txtPhone.Text);
                lblMessage.Text = message;

                if (message == "Physician added successfully!")
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (selectedMode == "Update")
            {
                try
                {
                    physicianId = null;
                    physicianId = txtphysicianId.Text.Trim();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Enter Valid Patient ID");
                }

                Fname = null;
                LName = null;
                Phone = null;
                Email = null;
                Fname = txtFname.Text.Trim();
                LName = txtLname.Text.Trim();
                Phone = txtPhone.Text.Trim();
                Email = txtEmail.Text.Trim();
                Class dataService2 = new Class();
                string message = dataService2.AddPhysician(txtFname.Text, txtLname.Text, txtEmail.Text, txtPhone.Text);
                lblMessage.Text = message;

                if (message == "Physician details updated successfully.")
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string physicianId = txtphysicianId.Text.Trim();

                if (string.IsNullOrEmpty(physicianId))
                {
                    lblMessage.Text = "Please enter a valid Physician ID.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                Class dataService = new Class();
                string message = dataService.DeletePhysician(physicianId);

                lblMessage.Text = message;
                lblMessage.ForeColor = message == "Physician deleted successfully." ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

protected void Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel.Visible = (Mode.SelectedValue == "Update");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }
    }
}

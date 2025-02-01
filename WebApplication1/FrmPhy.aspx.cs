using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication1
{
    public partial class FrmPhy : System.Web.UI.Page
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

            lblErrorMessage.Visible = false;

            string selectedMode = Mode.SelectedValue;
            string resultMessage = string.Empty;


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
                dataService2.AddPhysician(Fname, LName, Email, Phone);
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
                dataService2.UpdatePhysician(physicianId, Fname, LName, Email, Phone);
            }

        }

        protected void Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel.Visible = (Mode.SelectedValue == "Update");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            // Redirect to homepage (Kevin)
            Response.Redirect("landingpage.aspx");
        }

    }
}
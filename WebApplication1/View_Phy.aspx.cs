using System;
using System.Data;

namespace WebApplication1
{
    public partial class View_Phy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            // Redirect to homepage (Kevin)
            Response.Redirect("landingpage.aspx");
        }
    }
}
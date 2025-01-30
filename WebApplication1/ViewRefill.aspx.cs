using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ViewRefill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static string myID = "";

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string prescriptionId = txtViewRefill.Text.Trim();

            if (!string.IsNullOrEmpty(prescriptionId))
            {
                Class refillsload = new Class();
                DataSet result = refillsload.LoadRefills(prescriptionId);

                if (result != null && result.Tables.Count > 0)
                {
                    RefillsGridView.DataSource = result.Tables[0];
                    RefillsGridView.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No data found.');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a Prescription ID.');", true);
            }
        }

        protected void btnGetRefill_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtViewRefill.Text.Trim(), out int prescriptionId))
                {
                    string status = "COMPLETED";

                    Class refilltrigger = new Class();
                    refilltrigger.TriggerRefillProcedure(prescriptionId, status);

                    btnSearch_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('An error occurred: {ex.Message}');", true);
            }
        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("landingpage.aspx");
        }
    }
}
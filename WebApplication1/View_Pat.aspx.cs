using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class View_Pat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dgvPatientData.DataSource = null;
                dgvPatientData.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchKey = txtPatientSearch.Text.Trim();
            Class dataService = new Class(); // Using your existing class name
            DataSet patientData = dataService.SearchPatient(searchKey);

            if (patientData.Tables[0].Rows.Count > 0)
            {
                dgvPatientData.DataSource = patientData.Tables[0];
                dgvPatientData.DataBind();
            }
            else
            {
                dgvPatientData.DataSource = null;
                dgvPatientData.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No records found.');", true);
            }
        }

        protected void dgvPatientData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeletePatient")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string patientId = dgvPatientData.DataKeys[rowIndex]?.Value.ToString();

                if (!string.IsNullOrEmpty(patientId))
                {
                    Class dataService = new Class(); // Using your existing class name
                    dataService.DeletePatient(patientId);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Patient deleted successfully.');", true);
                    btnSearch_Click(sender, e); // Refresh the grid
                }
            }
        }
    }
}
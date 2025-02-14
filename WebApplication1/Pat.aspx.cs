using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Pat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dgvPatientData.DataSource = null;
                dgvPatientData.DataBind();
            }
        }



        string patientId, Fname, M_I, LName, DOB, Gender, Phone, Address, CITY, STATE, ZIP, Country, Insurance;

       

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string selectedMode = Mode.SelectedValue;

            if (selectedMode == "Add")
            {
                Fname = null;
                M_I = null;
                LName = null;
                DOB = null;
                Gender = null;
                Phone = null;
                Address = null;
                CITY = null;
                STATE = null;
                ZIP = null;
                Country = null;
                Insurance = null;
                Fname = txtFname.Text.Trim();
                M_I = txtMi.Text.Trim();
                LName = txtLname.Text.Trim();
                DOB = ddob.Text.Trim();
                Gender = txtGender.Text.Trim();
                Phone = txtPhone.Text.Trim();
                Address = txtAddress.Text.Trim();
                CITY = txtCity.Text.Trim();
                STATE = DDLState.SelectedValue;
                ZIP = txtZip.Text.Trim();
                Country = txtCity.Text.Trim();
                Insurance = txtInsurance.Text.Trim();
                Class dataService2 = new Class();
                string message = dataService2.AddPatient(Fname, M_I, LName, DOB, Gender, Phone, Address, CITY, STATE, ZIP, Country, Insurance);
                lblMessage.Text = message;

                if (message == "Patient added successfully!")
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
                    patientId = null;
                    patientId = txtpatientId.Text.Trim();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Enter Valid Patient ID");
                }

                Fname = null;
                M_I = null;
                LName = null;
                DOB = null;
                Gender = null;
                Phone = null;
                Address = null;
                CITY = null;
                STATE = null;
                ZIP = null;
                Country = null;
                Insurance = null;
                Fname = txtFname.Text.Trim();
                M_I = txtMi.Text.Trim();
                LName = txtLname.Text.Trim();
                DOB = ddob.Text.Trim();
                Gender = txtGender.Text.Trim();
                Phone = txtPhone.Text.Trim();
                Address = txtAddress.Text.Trim();
                CITY = txtCity.Text.Trim();
                STATE = DDLState.SelectedValue;
                ZIP = txtZip.Text.Trim();
                Country = txtCity.Text.Trim();
                Insurance = txtInsurance.Text.Trim();
                Class dataService2 = new Class();
                string message = dataService2.UpdatePatient(patientId, Fname, M_I, LName, DOB, Gender, Phone, Address, CITY, STATE, ZIP, Country, Insurance);
                lblMessage.Text = message;

                if (message == "Patient updated successfully!")
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
                string patientId = txtpatientId.Text.Trim();

                if (string.IsNullOrEmpty(patientId))
                {
                    lblMessage.Text = "Please enter a valid patient ID.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                Class dataService = new Class();
                string message = dataService.DeletePatient(patientId);

                lblMessage.Text = message;
                lblMessage.ForeColor = message == "patient deleted successfully." ? System.Drawing.Color.Green : System.Drawing.Color.Red;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchKey = txtPatientSearch.Text.Trim();
            Class dataService = new Class();
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

       



    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication1
{
    public partial class Add_Pat : System.Web.UI.Page
    {
        string Fname, M_I, LName, DOB, Gender, Phone, Address, CITY, STATE, ZIP, Country, Insurance;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Fname = txtFname.Text.Trim();
            M_I = txtMi.Text.Trim();
            LName = txtLname.Text.Trim();
            DOB = ddob.ToString();
            Gender = txtGender.Text.Trim();
            Phone = txtPhone.Text.Trim();
            Address = txtAddress.Text.Trim();
            CITY = txtCity.Text.Trim();
            STATE = txtState.Text.Trim();
            ZIP = txtZip.Text.Trim();
            Country = txtCity.Text.Trim();
            Insurance = txtInsurance.Text.Trim();

            Class dataService = new Class();
            dataService.AddPatient(Fname, M_I, LName, DOB, Gender, Phone, Address, CITY, STATE, ZIP, Country, Insurance);

        }
    }
    
}
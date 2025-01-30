using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication1
{
    public partial class AddPhysician : System.Web.UI.Page
    {
        string Fname, LName, Phone, Email;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Fname = txtFname.Text.Trim();
            LName = txtLname.Text.Trim();
            Phone = txtPhone.Text.Trim();
            Email = txtEmail.Text.Trim();

            

            Class dataService = new Class();
            dataService.AddPhysician(Fname, LName, Email, Phone);
        }
    }
}
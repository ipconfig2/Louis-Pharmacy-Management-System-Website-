using System;

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
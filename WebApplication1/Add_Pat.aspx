<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Pat.aspx.cs" Inherits="WebApplication1.Add_Pat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Patient</title>
</head>
<body>
    <form id="form1" runat="server" onreset="return confirm('Do you really want to clear this form?');">
        <div>
            <p>First Name:</p><asp:TextBox ID="txtFname" runat="server"></asp:TextBox> <br />
            <p>MI:</p><asp:TextBox ID="txtMi" runat="server"></asp:TextBox> <br />
            <p>Last Name:</p><asp:TextBox ID="txtLname" runat="server"></asp:TextBox><br />
            <p>Date of Birth</p><asp:TextBox ID="ddob" TextMode="Date" runat="server"></asp:TextBox><br />
            <p>Gender</p><asp:TextBox ID="txtGender" runat="server"></asp:TextBox><br />
            <p>Phone</p><asp:TextBox ID="txtPhone" TextMode="Phone" runat="server"></asp:TextBox><br />
            <p>Address:</p><asp:TextBox ID="txtAddress" runat="server"> </asp:TextBox> <br />
            <p>City:</p><asp:TextBox ID="txtCity" runat="server"> </asp:TextBox> <br />
            <p>Zip:</p><asp:TextBox ID="txtZip" runat="server"> </asp:TextBox> <br />
            <p>Country:</p><asp:TextBox ID="txtCountry" runat="server"> </asp:TextBox> <br />
            <p>State:</p><asp:TextBox ID="txtState" runat="server"> </asp:TextBox> <br />
            <p>Insurance:</p><asp:TextBox ID="txtInsurance" runat="server"> </asp:TextBox> <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
            <input type="reset" />
        </div>
    </form>
</body>
</html>

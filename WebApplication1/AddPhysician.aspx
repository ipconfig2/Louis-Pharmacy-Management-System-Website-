<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPhysician.aspx.cs" Inherits="WebApplication1.AddPhysician" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Add Physician</title>
</head>
<body>
    <form id="form1" runat="server" onreset="return confirm('Do you really want to clear this form?');">
        <div>
            <p>First Name:</p><asp:TextBox ID="txtFname" runat="server"></asp:TextBox> <br />
            <p>Last Name:</p><asp:TextBox ID="txtLname" runat="server"></asp:TextBox><br />
            <p>Phone</p><asp:TextBox ID="txtPhone" TextMode="Phone" runat="server"></asp:TextBox><br />
            <p>Email:</p><asp:TextBox ID="txtEmail" runat="server" TextMode="Email"> </asp:TextBox> <br />
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
            <input type="reset" />
        </div>
    </form>
</body>
</html>

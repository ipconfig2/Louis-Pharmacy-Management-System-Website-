<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmPhysician.aspx.cs" Inherits="WebApplication1.FrmPhysician" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
     <div>
         <asp:Label ID="lblSearch" runat="server" Text="Search:"></asp:Label>
         <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
         <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
         <br /><br />
         <asp:GridView ID="gvPhysicianData" runat="server" AutoGenerateColumns="true">
         </asp:GridView>
         <br />
     </div>

 <div>
     <asp:RadioButtonList ID="Mode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Mode_SelectedIndexChanged">
    <asp:ListItem Value="Add" Selected="True">Add</asp:ListItem>
    <asp:ListItem Value="Update">Update</asp:ListItem>
</asp:RadioButtonList>
 <asp:Panel ID="UpdatePanel" runat="server" Visible="False">
    <p>Physician ID:</p><asp:TextBox ID="txtphysicianId" runat="server"></asp:TextBox> <br />
     <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
</asp:Panel>
    <p>First Name:</p><asp:TextBox ID="txtFname" runat="server"></asp:TextBox> <br />
    <p>Last Name:</p><asp:TextBox ID="txtLname" runat="server"></asp:TextBox><br />
    <p>Phone</p><asp:TextBox ID="txtPhone" TextMode="Phone" runat="server"></asp:TextBox><br />
    <p>Email:</p><asp:TextBox ID="txtEmail" runat="server" TextMode="Email"> </asp:TextBox> <br />
    <br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
    <input type="reset" />
              <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
     <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

</div>
 </form>
</body>
</html>

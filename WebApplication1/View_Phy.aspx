<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Phy.aspx.cs" Inherits="WebApplication1.View_Phy" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Physician</title>
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
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
        </div>
    </form>
</body>
</html>

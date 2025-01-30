<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Pat.aspx.cs" Inherits="WebApplication1.View_Pat" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Patient</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblSearch" runat="server" Text="Search Patient:"></asp:Label>
            <asp:TextBox ID="txtPatientSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />

            <asp:GridView ID="dgvPatientData" runat="server" AutoGenerateColumns="true" 
                OnRowCommand="dgvPatientData_RowCommand" EmptyDataText="No records found">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
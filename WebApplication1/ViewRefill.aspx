<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewRefill.aspx.cs" Inherits="WebApplication1.ViewRefill" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Refill</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>View Refill</h1>
            <asp:Label ID="lblPrescriptionId" runat="server" Text="Prescription ID:"></asp:Label>
            <asp:TextBox ID="txtViewRefill" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <asp:Button ID="btnGetRefill" runat="server" Text="Get Refill" OnClick="btnGetRefill_Click" />

            <asp:GridView ID="RefillsGridView" runat="server" AutoGenerateColumns="True">
                <Columns>
                    <asp:ButtonField ButtonType="Button" CommandName="Update" Text="Update" />
                    <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete" />
                </Columns>
            </asp:GridView>

            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
        </div>
    </form>
</body>
</html>
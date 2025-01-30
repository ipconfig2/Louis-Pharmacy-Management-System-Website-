<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Patients_Perscriptions.aspx.cs" Inherits="WebApplication1.View_Patients_Perscriptions" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>View Patient Prescriptions</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>View Patients' Prescriptions</h1>

            <asp:Label ID="Label1" runat="server" Text="Patient ID: "></asp:Label>
            <asp:TextBox ID="txtPatientSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />

            <br /><br />

            <asp:Label ID="lblPatientName" runat="server" Text="Name: "></asp:Label><br />
            <asp:Label ID="lblDOB" runat="server" Text="DOB: "></asp:Label><br />
            <asp:Label ID="lblAddress" runat="server" Text="Address: "></asp:Label><br />
            <asp:Label ID="lblPhone" runat="server" Text="Phone: "></asp:Label>

            <br /><br />

            <asp:GridView ID="gvPrescriptions" runat="server" AutoGenerateColumns="true" OnSelectedIndexChanged="gvPrescriptions_SelectedIndexChanged">
            </asp:GridView>

            <br />

            <asp:Button ID="btnDelete" runat="server" Text="Delete Prescription" OnClick="btnDelete_Click" Enabled="false" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update Prescription" OnClick="btnUpdate_Click" Enabled="false" />
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
        </div>
    </form>
</body>
</html>

<%@ Page Title="Patient Prescriptions Search" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PatientPrescriptionSearch.aspx.cs" Inherits="WebApplication1.View_Patients_Perscriptions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container p-5 my-5 border">
            <h3><asp:Label ID="lblPrescriptions" runat="server" Text="Patient Prescriptions Search:"></asp:Label> </h3><br />
            <asp:Label ID="Label1" runat="server" Text="Patient ID: "></asp:Label>
            <asp:TextBox ID="txtPatientSearch" runat="server"></asp:TextBox>
            <asp:Button class="btn btn-primary rounded-pill px-3" ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />

            <br /><br />

            <asp:Label ID="lblPatientName" runat="server" Text="Name: "></asp:Label><br />
            <asp:Label ID="lblDOB" runat="server" Text="DOB: "></asp:Label><br />
            <asp:Label ID="lblAddress" runat="server" Text="Address: "></asp:Label><br />
            <asp:Label ID="lblPhone" runat="server" Text="Phone: "></asp:Label>

            <br /><br />

            <asp:GridView ID="gvPrescriptions" runat="server" AutoGenerateColumns="true" OnSelectedIndexChanged="gvPrescriptions_SelectedIndexChanged">
            </asp:GridView>

            <br />

            <asp:Button class="btn btn-warning rounded-pill px-3" ID="btnDelete" runat="server" Text="Delete Prescription" OnClick="btnDelete_Click" Enabled="false" />
            <asp:Button class="btn btn-primary rounded-pill px-3" ID="btnUpdate" runat="server" Text="Update Prescription" OnClick="btnUpdate_Click" Enabled="false" />

        </div>
  </asp:Content>
    
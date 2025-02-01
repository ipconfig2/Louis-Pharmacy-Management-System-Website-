<%@ Page Title="Search Patient" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchPatient.aspx.cs" Inherits="WebApplication1.View_Pat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container p-5 my-5 border">
            <h3><asp:Label ID="lblSearch" runat="server" Text="Search Patient:"></asp:Label> </h3><br />
            <asp:Label ID="Label1" runat="server" Text="Patient ID: "></asp:Label>
            <asp:TextBox ID="txtPatientSearch" runat="server"></asp:TextBox>
            <asp:Button class="btn btn-primary rounded-pill px-3" ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <br/>
            <asp:GridView ID="dgvPatientData" runat="server" AutoGenerateColumns="true" OnRowCommand="dgvPatientData_RowCommand" EmptyDataText="No Records Found">
            </asp:GridView>
        </div>
    </asp:Content>
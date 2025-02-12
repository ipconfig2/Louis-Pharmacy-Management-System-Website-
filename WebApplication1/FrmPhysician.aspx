<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FrmPhysician.aspx.cs" Inherits="WebApplication1.FrmPhysician" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Physician Management</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container p-5 my-5 border">
        <asp:Label ID="lblSearch" runat="server" Text="Search:"></asp:Label>
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" class="btn btn-primary rounded-pill px-3" runat="server" Text="Search" OnClick="btnSearch_Click" />
        <br /><br />
        <asp:GridView ID="gvPhysicianData" CssClass="gridview" runat="server" AutoGenerateColumns="true">
        </asp:GridView>
        <br />
    </div>

    <div class="container p-5 my-5 border">
        <asp:RadioButtonList ID="Mode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Mode_SelectedIndexChanged">
            <asp:ListItem Value="Add" Selected="True">Add</asp:ListItem>
            <asp:ListItem Value="Update">Update</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Panel ID="UpdatePanel" runat="server" Visible="False">
            <p>Physician ID:</p>
            <asp:TextBox ID="txtphysicianId" runat="server"></asp:TextBox> <br />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
        </asp:Panel>

        <p>First Name:</p><asp:TextBox ID="txtFname" runat="server"></asp:TextBox> <br />
        <p>Last Name:</p><asp:TextBox ID="txtLname" runat="server"></asp:TextBox><br />
        <p>Phone:</p><asp:TextBox ID="txtPhone"  runat="server"></asp:TextBox><br />
        <p>Email:</p><asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox><br />

        <br />
        <asp:Button ID="btnSubmit" class="btn btn-primary rounded-pill px-3" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <input type="reset"  class="btn btn-danger rounded-pill px-3" />
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>

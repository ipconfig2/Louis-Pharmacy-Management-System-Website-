<%@ Page Title="New Physician" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="NewPhysician.aspx.cs" Inherits="WebApplication1.AddPhysician" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2"  onreset="return confirm('Do you really want to clear this form?');" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container p-5 my-5 border">
            <h3><asp:Label ID="lblDetails" runat="server" Text="Enter Physician Details:"></asp:Label> </h3><br />
            <p>First Name:</p><asp:TextBox ID="txtFname" runat="server"></asp:TextBox> <br />
            <p>Last Name:</p><asp:TextBox ID="txtLname" runat="server"></asp:TextBox><br />
            <p>Phone</p><asp:TextBox ID="txtPhone" TextMode="Phone" runat="server"></asp:TextBox><br />
            <p>Email:</p><asp:TextBox ID="txtEmail" runat="server" TextMode="Email"> </asp:TextBox> <br />
            <br />
            <br />
            <asp:Button class="btn btn-primary rounded-pill px-3" ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
            <input type="reset"class="btn btn-danger rounded-pill px-3" />
            </div>
</asp:Content>
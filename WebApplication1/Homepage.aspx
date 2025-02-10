<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="WebApplication1.Homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container p-5 my-5 border">
        <h1><asp:Label ID="lblWelcome" runat="server" Text="Welcome"></asp:Label> </h1><br />
        <h1><asp:Label ID="Label1" runat="server" Text="Beyond the counter there's a heart dedicated to healing"></asp:Label> </h1><br />
        <asp:Image ID="imgHome" runat="server" ImageUrl="~/home.jpg" AlternateText="Home Image" CssClass="img-fluid" />
        </div>

     <div class="container p-5 my-5 border text-center">
        <h2>Employee of the Month</h2>
        <asp:Image ID="imgEmployee" runat="server" ImageUrl="~/employee.jpg" AlternateText="Employee of the Month" CssClass="img-fluid rounded-circle" Width="200px" Height="200px"/>
        <h3><asp:Label ID="lblEmployeeName" runat="server" Text="Dr. Mcfarlane"></asp:Label></h3>
    </div>
</asp:Content>

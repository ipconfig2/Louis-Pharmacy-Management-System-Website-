<%@ Page Title="New Patient" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="NewPatient.aspx.cs" Inherits="WebApplication1.Add_Pat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container p-5 my-5 border">
        <h3><asp:Label ID="lblDetails" runat="server" Text="Enter Patient Details:"></asp:Label> </h3><br />
        <p>First Name:</p><asp:TextBox ID="txtFname" runat="server"></asp:TextBox> <br />
        <p>MI:</p><asp:TextBox ID="txtMi" runat="server"></asp:TextBox> <br />
        <p>Last Name:</p><asp:TextBox ID="txtLname" runat="server"></asp:TextBox><br />
        <p>Date of Birth</p><asp:TextBox ID="ddob" TextMode="Date" runat="server"></asp:TextBox><br />
        <p>Gender</p><asp:TextBox ID="txtGender" runat="server"></asp:TextBox><br />
        <p>Phone</p><asp:TextBox ID="txtPhone" TextMode="Phone" runat="server"></asp:TextBox><br />
        <p>Address:</p><asp:TextBox ID="txtAddress" runat="server"> </asp:TextBox> <br />
        <p>City:</p><asp:TextBox ID="txtCity" runat="server"> </asp:TextBox> <br />
        <p>Zip:</p><asp:TextBox ID="txtZip" runat="server"> </asp:TextBox> <br />
        <p>Country:</p><asp:TextBox ID="txtCountry" runat="server"> </asp:TextBox> <br />
        <p>State:</p><asp:TextBox ID="txtState" runat="server"> </asp:TextBox> <br />
        <p>Insurance:</p><asp:TextBox ID="txtInsurance" runat="server"> </asp:TextBox> <br />
        <br />
        <asp:Button class="btn btn-primary rounded-pill px-3" ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
        <input type="reset" class="btn btn-danger rounded-pill px-3" />
    </div>
</asp:Content>

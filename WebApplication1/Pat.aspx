﻿<%@ Page Title="Patient" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Pat.aspx.cs" Inherits="WebApplication1.Pat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        
        <div class="container p-5 my-5 border">
    <asp:Label ID="Label1" runat="server" Text="Search Patient:"></asp:Label>
    <asp:TextBox ID="txtPatientSearch" runat="server"></asp:TextBox>
    <asp:Button class="btn btn-primary rounded-pill px-3" ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" />

    <asp:GridView ID="dgvPatientData" runat="server" AutoGenerateColumns="true" EmptyDataText="No records found"> </asp:GridView>
    </div>
       
        <div class="container p-5 my-5 border">
        <asp:RadioButtonList ID="Mode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Mode_SelectedIndexChanged">
           <asp:ListItem Value="Add" Selected="True">Add</asp:ListItem>
            <asp:ListItem Value="Update">Update</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Panel ID="UpdatePanel" runat="server" Visible="False">
            <p>Patient ID:</p><asp:TextBox ID="txtpatientId" runat="server"></asp:TextBox> <br />
            </asp:Panel>
            <p>First Name:</p><asp:TextBox ID="txtFname" runat="server"></asp:TextBox> <br />
            <p>MI:</p><asp:TextBox ID="txtMi" runat="server"></asp:TextBox> <br />
            <p>Last Name:</p><asp:TextBox ID="txtLname" runat="server"></asp:TextBox><br />
            <p>Date of Birth:</p><asp:TextBox ID="ddob" TextMode="Date" runat="server"></asp:TextBox><br />
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
            <input class="btn btn-danger rounded-pill px-3" type="reset" />
             <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

        </div>
</asp:Content>
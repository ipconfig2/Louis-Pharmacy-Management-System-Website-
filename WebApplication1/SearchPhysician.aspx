<%@ Page Title="Search Physician" Language="C#"  MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchPhysician.aspx.cs" Inherits="WebApplication1.View_Phy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container p-5 my-5 border" >
            <h3><asp:Label ID="lblSearch" runat="server" Text="Search Physician:"></asp:Label></h3> <br />
            <asp:Label ID="Label1" runat="server" Text="Physician ID: "></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button class="btn btn-primary rounded-pill px-3" ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <br /><br />
            <asp:GridView ID="gvPhysicianData" runat="server" AutoGenerateColumns="true" EmptyDataText="No Records Found">
            </asp:GridView>
            

        </div>
   </asp:Content>
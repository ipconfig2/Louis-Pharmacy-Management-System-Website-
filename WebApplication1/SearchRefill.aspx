<%@ Page Title="Search Refill" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="SearchRefill.aspx.cs" Inherits="WebApplication1.ViewRefill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container p-5 my-5 border">
            <h3><asp:Label ID="lblSearch" runat="server" Text="Search Refill:"></asp:Label></h3> <br />
            <asp:Label ID="lblPrescriptionId" runat="server" Text="Prescription ID:"></asp:Label>
            <asp:TextBox ID="txtViewRefill" runat="server"></asp:TextBox>
            <asp:Button class="btn btn-primary rounded-pill px-3" ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <asp:Button class="btn btn-secondary rounded-pill px-3" ID="btnGetRefill" runat="server" Text="Get Refill" OnClick="btnGetRefill_Click" />

            <asp:GridView ID="RefillsGridView" runat="server" AutoGenerateColumns="True">
                <Columns>
                    <asp:ButtonField ButtonType="Button" CommandName="Update" Text="Update" />
                    <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete" />
                </Columns>
            </asp:GridView>
        </div>
  </asp:Content>
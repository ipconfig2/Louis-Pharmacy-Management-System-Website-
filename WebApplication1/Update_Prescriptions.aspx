<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Prescriptions.aspx.cs" Inherits="WebApplication1.View_Patients_Perscriptions" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>View Patient Prescriptions</title>
</head>
    <script type="text/javascript">
    function setCurrentDateTime(input) {
        if (!input.value) {  
            var now = new Date();
            var year = now.getFullYear();
            var month = ('0' + (now.getMonth() + 1)).slice(-2); 
            var day = ('0' + now.getDate()).slice(-2);
            var hours = ('0' + now.getHours()).slice(-2);
            var minutes = ('0' + now.getMinutes()).slice(-2);

           
            input.value = `${year}-${month}-${day}T${hours}:${minutes}`;
        }
    }
    </script>

<body>
    <form id="form1" runat="server">
        <div>
            <h1>Update Prescriptions</h1>


            <asp:Label ID="Label1" runat="server" Text="Prescription ID: "></asp:Label>
            <asp:TextBox ID="txtPatSearch" runat="server" OnTextChanged="txtPatientSearch_TextChanged"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />


            <br /><br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <br /><br />
            <asp:GridView ID="gvPrescriptions" runat="server" AutoGenerateColumns="true" OnSelectedIndexChanged="gvPrescriptions_SelectedIndexChanged">
            </asp:GridView>

            <br />

            <h3>Update Information</h3>

            <asp:Label ID="lblPrescriptionID" runat="server" Text="Prescription ID:" Visible="true"></asp:Label>
            <asp:TextBox ID="txtPrescriptionID" runat="server" Enabled="true" Visible="true"></asp:TextBox>

            <asp:Label ID="lblMedicine" runat="server" Text="Medicine: "></asp:Label>
            <asp:TextBox ID="txtMedicine" runat="server"></asp:TextBox>

            <asp:Label ID="lblDosage" runat="server" Text="Dosage: "></asp:Label>
            <asp:TextBox ID="txtDosage" runat="server"></asp:TextBox>

            <asp:Label ID="lblIntMethod" runat="server" Text="Intake: "></asp:Label>
            <asp:TextBox ID="txtIntMethod" runat="server"></asp:TextBox>

            <br /><br />

            <asp:Button ID="btnDelete" runat="server" Text="Delete Prescription" OnClick="btnDelete_Click" Enabled="true" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update Prescription" OnClick="btnUpdate_Click" Enabled="true" />
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />




            <br /><br />
            <br /><br />

            <h2>Refill Information</h2>

            <asp:Label ID="lblRXNO" runat="server" Text="RX_NO: "></asp:Label>
            <asp:TextBox ID="txtRXsearch" runat="server" OnTextChanged="txtPatientSearch_TextChanged"></asp:TextBox>
            <asp:Button ID="RXsearch" runat="server" Text="Search" OnClick="btnSearch_Click2" />

            <br /><br />
            <asp:Label ID="lblMessage2" runat="server" ForeColor="Red"></asp:Label>
            <br /><br />

            <asp:GridView ID="gvRefillInfo" runat="server" AutoGenerateColumns="true">
            </asp:GridView>

            <br />

            <asp:Label ID="lblRefillCount" runat="server" Text="Refill Count: "></asp:Label>
            <asp:TextBox ID="txtRefillCount" runat="server"></asp:TextBox>

            <asp:Label ID="lblRefillDate" runat="server" Text="Refill Date: "></asp:Label>
            <asp:TextBox ID="txtRefillDate" runat="server" TextMode="DateTimeLocal" onfocus="setCurrentDateTime(this)"></asp:TextBox>

            <asp:Label ID="lblStatus" runat="server" Text="Status: "></asp:Label>
            <asp:CheckBox ID="chkPending" runat="server" Text="Pending" AutoPostBack="true" OnCheckedChanged="chkPending_CheckedChanged" />
            <asp:CheckBox ID="chkCompleted" runat="server" Text="Completed" AutoPostBack="true" OnCheckedChanged="chkCompleted_CheckedChanged" />

            <br /><br />

            <asp:Button ID="btnUpdateRefill" runat="server" Text="Update Refill Information" OnClick="btnUpdateRefill_Click" />



        </div>
    </form>
</body>
</html>


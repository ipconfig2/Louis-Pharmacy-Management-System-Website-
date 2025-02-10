<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frm_Prescription.aspx.cs" Inherits="WebApplication1.Frm_Prescription" %>

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

    <script type="text/javascript">
        window.onload = function () {
            var grid = document.getElementById('<%= gvPrescriptions.ClientID %>');
            if (grid) {
                var rows = grid.getElementsByTagName("tr");
                for (var i = 1; i < rows.length; i++) {
                    rows[i].onclick = function () {
                        this.cells[0].getElementsByTagName("input")[0].click();
                    };
                }
            }
        };
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

            

            <asp:GridView ID="gvPrescriptions" runat="server" AutoGenerateColumns="False" 
    AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvPrescriptions_SelectedIndexChanged"
    CssClass="table table-striped" >
    <Columns>
        <asp:BoundField DataField="PrescriptionID" HeaderText="Prescription ID" />
        <asp:BoundField DataField="MedName" HeaderText="Medicine Name" />
        <asp:BoundField DataField="Dosage" HeaderText="Dosage" />
        <asp:BoundField DataField="IntMethod" HeaderText="Intake Method" />
        <asp:BoundField DataField="RefillsLeft" HeaderText="Refills Left" />
        
    </Columns>
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

                        <asp:Label ID="lblPatientID" runat="server" Text="PatientID: "></asp:Label>
            <asp:TextBox ID="txtPatientID" runat="server"></asp:TextBox>

                        <asp:Label ID="lblPhysicianID" runat="server" Text="PhysicianID: "></asp:Label>
            <asp:TextBox ID="txtPhysicianID" runat="server"></asp:TextBox>

            <br /><br />
            <asp:Button ID="btnAddPrescription" runat="server" Text="Add Prescription" OnClick="btnAddPrescription_Click" />
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



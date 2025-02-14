<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Prescriptions.aspx.cs" Inherits="WebApplication1.Update_Perscriptions" MasterPageFile="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View Patient Prescriptions</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/mvc/5.0/jquery.validate.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/mvc/5.0/jquery.validate.unobtrusive.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Update Prescriptions</h1>

    <asp:Label ID="Label1" runat="server" Text="Prescription ID: "></asp:Label>
    <asp:TextBox ID="txtPatSearch" runat="server" OnTextChanged="txtPatientSearch_TextChanged"></asp:TextBox>
    <asp:Button ID="btnSearch" class="btn btn-primary rounded-pill px-3" runat="server" Text="Search" OnClick="btnSearch_Click" />

    <br /><br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <br /><br />

<asp:GridView ID="gvPrescriptions" runat="server" AutoGenerateColumns="False" 
    AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvPrescriptions_SelectedIndexChanged"
    CssClass="table table-striped">
    <Columns>
        <asp:BoundField DataField="PrescriptionID" HeaderText="Prescription ID" />
        <asp:BoundField DataField="PatientID" HeaderText="Patient ID" /> 
        <asp:BoundField DataField="PhysicianID" HeaderText="Physician ID" /> 
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

    <asp:Label ID="lblPatientID" runat="server" Text="PatientID: "></asp:Label>
    <asp:TextBox ID="txtPatientID" runat="server"></asp:TextBox>

    <asp:Label ID="lblPhysicianID" runat="server" Text="PhysicianID: "></asp:Label>
    <asp:TextBox ID="txtPhysicianID" runat="server"></asp:TextBox>

    <br /><br />
    <asp:Button ID="btnAddPrescription" class="btn btn-primary rounded-pill px-3" runat="server" Text="Add Prescription" OnClick="btnAddPrescription_Click" />
    <asp:Button ID="btnDelete" class="btn btn-danger rounded-pill px-3" runat="server" Text="Delete Prescription" OnClick="btnDelete_Click" Enabled="true" />
    <asp:Button ID="btnUpdate" class="btn btn-primary rounded-pill px-3" runat="server" Text="Update Prescription"
        OnClick="btnUpdate_Click" OnClientClick="return validateForm();" />
    <asp:Button ID="btnClear" class="btn btn-primary rounded-pill px-3" runat="server" Text="Clear" OnClick="btnClear_Click" />
    <br /><br />
    <h2>Refill Information</h2>

    <asp:Label ID="lblRXNO" runat="server" Text="RX_NO: "></asp:Label>
    <asp:TextBox ID="txtRXsearch" runat="server" OnTextChanged="txtPatientSearch_TextChanged"></asp:TextBox>

    <asp:Label ID="lblRXID" runat="server" Text="Prescription ID: "></asp:Label>
       <asp:TextBox ID="txtRXID" runat="server" Enabled="true"></asp:TextBox>

    <asp:Button ID="RXsearch" class="btn btn-primary rounded-pill px-3" runat="server" Text="Search" OnClick="btnSearch_Click2" />

    <br /><br />
    <asp:Label ID="lblMessage2" runat="server" ForeColor="Red"></asp:Label>
    <br /><br />

    <asp:GridView ID="gvRefillInfo" CssClass="gridview" runat="server" AutoGenerateColumns="true"></asp:GridView>

    <br />
    <asp:Label ID="RefilCount" runat="server" Text="Refill Count: "></asp:Label>
    <asp:TextBox ID="txtRefillCount" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
    <asp:CustomValidator ID="cvRefillCount" runat="server"
        ControlToValidate="txtRefillCount"
        OnServerValidate="ValidateRefillCount"
        ClientValidationFunction="validateRefillCount"
        ErrorMessage="Refill Count must be at least 1!"
        ForeColor="Red" Display="Dynamic" />

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode < 48 || charCode > 57) {
                alert("Only numbers are allowed!");
                return false;
            }
            return true;
        }
    </script>

    <asp:Label ID="lblRefillDate" runat="server" Text="Refill Date: "></asp:Label>
    <asp:TextBox ID="txtRefillDate" runat="server" TextMode="DateTimeLocal" onfocus="setCurrentDateTime(this)"></asp:TextBox>

    <asp:Label ID="lblStatus" runat="server" Text="Status: "></asp:Label>
    <asp:CheckBox ID="chkPending" runat="server" Text="Pending" AutoPostBack="true" OnCheckedChanged="chkPending_CheckedChanged" />
    <asp:CheckBox ID="chkCompleted" runat="server" Text="Completed" AutoPostBack="true" OnCheckedChanged="chkCompleted_CheckedChanged" />

    <br /><br />
    <asp:Button ID="btnUpdateRefill" class="btn btn-primary rounded-pill px-3" runat="server" Text="Update Refill Information" OnClick="btnUpdateRefill_Click" />
    <asp:Button ID="btnAddfill" class="btn btn-primary rounded-pill px-3" runat="server" Text="Add Refill Information" OnClick="btnAddRefill_Click" />
    <asp:Button ID="btnDeleteRefill" class="btn btn-danger rounded-pill px-3" runat="server" Text="Delete Refill Information" OnClick="btnDeleteRefill_Click" />

</asp:Content>
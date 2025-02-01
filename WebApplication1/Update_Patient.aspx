<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Patient.aspx.cs" Inherits="WebApplication1.UpdatePatient" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Update Patient</title>
    <script type="text/javascript">

        function openCalendar() {
            var inputField = document.getElementById('<%= txtDOB.ClientID %>');
            var currentDate = inputField.value ? new Date(inputField.value) : new Date();
            var dateString = currentDate.getFullYear() + '-' + (currentDate.getMonth() + 1).toString().padStart(2, '0') + '-' + currentDate.getDate().toString().padStart(2, '0');

            var datePicker = document.createElement('input');
            datePicker.type = 'date';
            datePicker.value = dateString;

            document.body.appendChild(datePicker);
            datePicker.click();

            datePicker.onchange = function () {
                inputField.value = datePicker.value;
                document.body.removeChild(datePicker);
            };
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <h2>Update Patient Information</h2>

        <div>
            <label for="txtPatientID">Patient ID:</label>
            <asp:TextBox ID="txtPatientID" runat="server" aria-label="Enter Patient ID"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /><br /><br />
        </div>

        <div>
            <label for="txtFName">First Name:</label>
            <asp:TextBox ID="txtFName" runat="server" aria-label="Enter First Name"></asp:TextBox><br />

            <label for="txtMI">Middle Initial:</label>
            <asp:TextBox ID="txtMI" runat="server" aria-label="Enter Middle Initial"></asp:TextBox><br />

            <label for="txtLName">Last Name:</label>
            <asp:TextBox ID="txtLName" runat="server" aria-label="Enter Last Name"></asp:TextBox><br />
        </div>

        <div>
            <label for="txtDOB">Date of Birth:</label>
            <asp:TextBox ID="txtDOB" runat="server" placeholder="Select Date of Birth" OnClick="openCalendar()" aria-label="Select Date of Birth"></asp:TextBox><br />
        </div>

        <div>

        </div>

        <div>
            <label>Gender:</label>
            <asp:RadioButton ID="rbtnMale" runat="server" Text="Male" GroupName="Gender" aria-label="Select Male" />
            <asp:RadioButton ID="rbtnFemale" runat="server" Text="Female" GroupName="Gender" aria-label="Select Female" /><br />
        </div>

        <div>
            <label for="txtPhone">Phone:</label>
            <asp:TextBox ID="txtPhone" runat="server" aria-label="Enter Phone Number"></asp:TextBox><br />

            <label for="txtStreet">Street:</label>
            <asp:TextBox ID="txtStreet" runat="server" aria-label="Enter Street Address"></asp:TextBox><br />

            <label for="txtCity">City:</label>
            <asp:TextBox ID="txtCity" runat="server" aria-label="Enter City"></asp:TextBox><br />

            <label for="txtState">State:</label>
            <asp:TextBox ID="txtState" runat="server" aria-label="Enter State"></asp:TextBox><br />

            <label for="txtZip">Zip Code:</label>
            <asp:TextBox ID="txtZip" runat="server" aria-label="Enter Zip Code"></asp:TextBox><br /><br />
        </div>

        <div>
            <asp:Button ID="btnUpdate" runat="server" Text="Update Patient" OnClick="btnUpdate_Click" Enabled="false" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>

        <asp:GridView ID="gvPatients" runat="server" AutoGenerateColumns="true" BorderWidth="1px" CellPadding="4" GridLines="Both"></asp:GridView>

    </form>
</body>
</html>
 
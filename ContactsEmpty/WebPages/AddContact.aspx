

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContact.aspx.cs" Inherits="ContactsEmpty.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../StyleSheet1.css" />
    <style>
        body {
            background-color: #fffcef;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="ContactName" runat="server" Text="Contact Name" Font-Underline="true" Font-Size="Larger" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="FirstName" runat="server" Text="First Name:" Width="90px"></asp:Label>
        <asp:TextBox ID="FirstNameTextBox" runat="server" Width="250px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
        <asp:Label ID="MiddleInitial" runat="server" Text="MI:" Width="35px" CssClass="leftpdg"></asp:Label>
        <asp:TextBox ID="MiddleInitialTextBox" runat="server" Width="25px" BorderStyle="Inset" BorderWidth="1" MaxLength="3"></asp:TextBox>
        <asp:Label ID="LastName" runat="server" Text="Last Name:" Width="90px" CssClass="leftpdg"></asp:Label>
        <asp:TextBox ID="LastNameTextBox" runat="server" Width="300px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
        <br />
        <asp:Button ID="SaveNameBtn" runat="server" Text="Next" OnClick="SaveName" Font-Size="Medium" CssClass="addbtn" />
        <br />
        <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="CancelContact" Font-Size="Medium" CssClass="addbtn" />

        <%--
        <div>
            <asp:Label ID="ContactName" runat="server" Text="Contact Name" Font-Underline="true" Font-Size="Larger" Font-Bold="true"></asp:Label>
            <br />
            <br />
            <asp:Label ID="FirstName" runat="server" Text="First Name:" Width="90px" ></asp:Label>
            <asp:TextBox ID="FirstNameTextBox" runat="server" Width="250px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <asp:Label ID="MiddleInitial" runat="server" Text="MI:" Width="35px" CssClass="leftpdg"></asp:Label>
            <asp:TextBox ID="MiddleInitialTextBox" runat="server" Width="25px" BorderStyle="Inset" BorderWidth="1" MaxLength="3"></asp:TextBox>
            <asp:Label ID="LastName" runat="server" Text="Last Name:" Width="90px" CssClass="leftpdg"></asp:Label>
            <asp:TextBox ID="LastNameTextBox" runat="server" Width="300px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <br />
        </div>
        <br />
        <asp:Label ID="Address" runat="server" Text="Address" Font-Underline="true" Font-Size="Larger" Font-Bold="true"></asp:Label>
        <p>
        <asp:Label ID="Street" runat="server" Text="Street:" Width="75px" CssClass="linemrgn"></asp:Label>
            <asp:TextBox ID="StreetTextBox" runat="server" Width="500px" CssClass="linemrgn" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <br />
            <asp:Label ID="StreetLine2" runat="server" Text="Line Two:" Width="75px" CssClass="linemrgn"></asp:Label>
            <asp:TextBox ID="StreetLine2TextBox" runat="server" Width="500px" CssClass="linemrgn" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <br />
            <asp:Label ID="CityLabel" runat="server" Text="City:" Width="75px" ></asp:Label>
            <asp:TextBox ID="CityTextBox" runat="server" Width="270px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <asp:Label ID="StateLabel" runat="server" Text="State:" Width="50px" CssClass="leftpdg"></asp:Label>
            <asp:TextBox ID="StateTextBox" runat="server" Width="30px" BorderStyle="Inset" BorderWidth="1" ></asp:TextBox>
            <asp:Label ID="ZipCodeLabel" runat="server" Text="Zip Code:" Width="70px" CssClass="leftpdg"></asp:Label>
            <asp:TextBox ID="ZipCodeTextBox" runat="server" Width="50px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="PhoneNumber" runat="server" Text="Phone Number" Font-Underline="true" Font-Size="Larger" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label ID="TypeLabel" runat="server" Text="Type: " Width="55px"></asp:Label>
            <asp:DropDownList ID="PhoneTypeList" AutoPostBack="true" runat="server">
                <asp:ListItem Selected="True" Value="">Select</asp:ListItem>
                <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
                <asp:ListItem Value="Home">Home</asp:ListItem>
                <asp:ListItem Value="Work">Work</asp:ListItem>
                <asp:ListItem Value="Fax">Fax</asp:ListItem>
                <asp:ListItem Value="Google">Google</asp:ListItem>
                <asp:ListItem Value="Other">Other</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="PhoneNumberLabel" runat="server" Text="Number: " Width="75px"></asp:Label>
            <asp:Label ID="FrontParenthesis" runat="server" Text="("></asp:Label>
            <asp:TextBox ID="AreaCodeTextBox" runat="server" Width="25px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <asp:Label ID="EndParenthesis" runat="server" Text=")"></asp:Label>
            <asp:TextBox ID="NumbPt1TxtBx" runat="server" Width="25px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <asp:Label ID="Dash" runat="server" Text="-"></asp:Label>
            <asp:TextBox ID="NumberPart2TextBox" runat="server" Width="40px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <asp:Label ID="Ext" runat="server" Text="Ext: " Width="40px" ></asp:Label>
            <asp:TextBox ID="ExtTextBox" runat="server" Width="75px" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <br />
        </p>
        <p>
            <asp:Label ID="EMail" runat="server" Text="E-Mail Address" Font-Underline="true" Font-Size="Larger" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label ID="EmailLabel" runat="server" Text="E-Mail Address: " Width="120px"></asp:Label>
            <asp:TextBox ID="UserNameTextBox" runat="server" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <asp:Label ID="At" runat="server" Text="@"></asp:Label>
            <asp:TextBox ID="DomainTextBox" runat="server" BorderStyle="Inset" BorderWidth="1"></asp:TextBox>
            <br />
        </p>
        <asp:Button ID="Save" runat="server" Text="Save Contact" OnClick="Save_Click" Font-Size="Medium" CssClass="addbtn" />
        <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="CancelContact" Font-Size="Medium" CssClass="addbtn" />
        --%>
    </form>
</body>
</html>

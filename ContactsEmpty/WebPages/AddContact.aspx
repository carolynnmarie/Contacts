

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContact.aspx.cs" Inherits="ContactsEmpty.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" >
        <div>
            <asp:Label ID="ContactName" runat="server" Text="Contact Name" Font-Underline="true"></asp:Label>
            <br />
            <br />
            <asp:Label ID="FirstName" runat="server" Text="First Name: "></asp:Label>
            <asp:TextBox ID="FirstNameTextBox" runat="server" Width="250px"></asp:TextBox>
            <asp:Label ID="MiddleInitial" runat="server" Text="MiddleInitial: "></asp:Label>
            <asp:TextBox ID="MiddleInitialTextBox" runat="server" Width="25px"></asp:TextBox>
            <asp:Label ID="LastName" runat="server" Text="Last Name: "></asp:Label>
            <asp:TextBox ID="LastNameTextBox" runat="server" Width="300px"></asp:TextBox>
            <br />
        </div>

        <br />
        <asp:Label ID="Address" runat="server" Text="Address" Font-Underline="true"></asp:Label>

        <p>
        <asp:Label ID="Street" runat="server" Text="Street: " Width="100px"></asp:Label>
            <asp:TextBox ID="StreetTextBox" runat="server" Width="500px"></asp:TextBox>
        </p>

        <p>
            <asp:Label ID="StreetLine2" runat="server" Text="Line Two: " Width="100px"></asp:Label>
            <asp:TextBox ID="StreetLine2TextBox" runat="server" Width="500px"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="CityLabel" runat="server" Text="City: " Width="100px"></asp:Label>
            <asp:TextBox ID="CityTextBox" runat="server" Width="350px"></asp:TextBox>
            <asp:Label ID="StateLabel" runat="server" Text="State: "></asp:Label>
            <asp:TextBox ID="StateTextBox" runat="server"></asp:TextBox>
            <asp:Label ID="ZipCodeLabel" runat="server" Text="Zip Code: "></asp:Label>
            <asp:TextBox ID="ZipCodeTextBox" runat="server" Width="45px" MaxLength="5"></asp:TextBox>
        </p>
        <p>
            <asp:CheckBox ID="PrimaryAddress" runat="server" />
            <asp:Label ID="PrimaryAddressLabel" runat="server" Text="Primary Address"></asp:Label>
        </p>
        <p>
            <asp:Label ID="PhoneNumber" runat="server" Text="Phone Number" Font-Underline="true"></asp:Label>
        </p>
        <asp:Label ID="TypeLabel" runat="server" Text="Phone Number Type: "></asp:Label>
        <asp:DropDownList ID="PhoneTypeList" AutoPostBack="true" runat="server">
            <asp:ListItem Selected="True" Value="">Select</asp:ListItem>
            <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
            <asp:ListItem Value="Home">Home</asp:ListItem>
            <asp:ListItem Value="Work">Work</asp:ListItem>
            <asp:ListItem Value="Fax">Fax</asp:ListItem>
            <asp:ListItem Value="Google">Google</asp:ListItem>
            <asp:ListItem Value="Other">Other</asp:ListItem>
        </asp:DropDownList>        
        <br />        
        <br />
        <asp:Label ID="PhoneNumberLabel" runat="server" Text="Phone Number: " Width="130px"></asp:Label>
        <asp:Label ID="FrontParenthesis" runat="server" Text="("></asp:Label>
        <asp:TextBox ID="AreaCodeTextBox" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
        <asp:Label ID="EndParenthesis" runat="server" Text=")"></asp:Label>
        <asp:TextBox ID="NumberPart1TextBox" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
        <asp:Label ID="Dash" runat="server" Text="-"></asp:Label>
        <asp:TextBox ID="NumberPart2TextBox" runat="server" MaxLength="4" Width="35px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Ext" runat="server" Text="Ext: " Width="130px"></asp:Label>
        <asp:TextBox ID="ExtTextBox" runat="server" Width="75px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="EMail" runat="server" Text="E-Mail Address" Font-Underline="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="EmailLabel" runat="server" Text="E-Mail Address: "></asp:Label>
        <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="At" runat="server" Text="@"></asp:Label>
        <asp:TextBox ID="DomainTextBox" runat="server"></asp:TextBox>

        <asp:Button ID="Save" runat="server" Text="Save Contact" OnClick="Save_Click"/>
    </form>
</body>
</html>

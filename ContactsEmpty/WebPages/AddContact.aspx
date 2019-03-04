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
        <asp:Button ID="SaveNameBtn" runat="server" Text="Save and Proceed to Add Information" OnClick="SaveName" Font-Size="Medium" CssClass="addbtn" />
        <br />
        <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="CancelContact" Font-Size="Medium" CssClass="addbtn" />
    </form>
</body>
</html>

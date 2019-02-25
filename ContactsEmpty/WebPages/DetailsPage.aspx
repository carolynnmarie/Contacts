<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailsPage.aspx.cs" Inherits="ContactsEmpty.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            CONTACT DETAILS<br />
            <br />
            Name<br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            Addresses<br />
            <asp:Label ID="Label2" runat="server" Text="Label" CssClass="white-space: pre-wrap"></asp:Label>
            <br />
        </div>
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
        <br />
        PhoneNumbers<asp:GridView ID="GridView3" runat="server">
        </asp:GridView>
        <br />
        <br />
        Email<asp:GridView ID="GridView4" runat="server">
        </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Edit Contact" OnClick="Edit" />
        <p>
        <asp:Button ID="Button2" runat="server" Text="Back To Contacts" OnClick="BackToContacts" />
        </p>
    </form>
</body>
</html>

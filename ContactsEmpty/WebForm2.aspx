<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="ContactsEmpty.WebForm2" %>

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
            Name<asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
            Addresses</div>
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
        <br />
        PhoneNumbers<asp:GridView ID="GridView3" runat="server">
        </asp:GridView>
        <br />
        <br />
        Email<asp:GridView ID="GridView4" runat="server">
        </asp:GridView>
    </form>
</body>
</html>

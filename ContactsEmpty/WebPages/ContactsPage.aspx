<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactsPage.aspx.cs" Inherits="ContactsEmpty.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Contacts" Font-Size="X-Large" Font-Underline="true" Font-Bold="true"></asp:Label>
            <br />
        </div>
        <asp:GridView ID="GridView1" DataKeyNames="ContactId" AutoGenerateColumns="False"   OnRowCommand="Details"  OnRowDeleting="DeleteContact"
            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ContactId" Visible="false" />
                <asp:ButtonField ButtonType="Button" CommandName="Details" Text="View/Edit Details"  />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                        <asp:Label ID="MiddleInitial" runat="server" Text='<%# Bind("MiddleInitial") %>'></asp:Label>
                        <asp:Label ID="LastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Button" ShowDeleteButton="true" DeleteText="Delete Contact" />
<%--
                <asp:BoundField DataField="FirstName" />
                <asp:BoundField DataField="MiddleInitial" Visible ="false" />
                <asp:BoundField DataField="LastName" />
     --%>                             
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
        <br />
        <asp:Button ID="AddContact" runat="server" Text="Add New Contact" OnClick="Add" />
    </form>
</body>
</html>

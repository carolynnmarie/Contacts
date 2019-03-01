<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="ContactsEmpty.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="../StyleSheet1.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="Contacts">
            <div class="row">
                <div class="leftColumn">
                    <asp:UpdatePanel ID="ContactName" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="Label1" runat="server" Text="Contacts" Font-Size="X-Large" Font-Underline="true" Font-Bold="true"></asp:Label>
                                <br />
                            </div>
                            <asp:GridView ID="GridView1" DataKeyNames="ContactId" AutoGenerateColumns="False" OnRowCommand="EditDelete"
                                runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" GridLines="None" RowStyle-CssClass="cbtn">
                                <Columns>
                                    <asp:BoundField DataField="ContactId" Visible="false" />
                                    <asp:ButtonField ButtonType="Button" CommandName="DeleteContact" Text="Delete" ControlStyle-CssClass="cntbtn" />
                                    <asp:TemplateField ItemStyle-CssClass="lfpdg">
                                        <ItemTemplate>
                                            <asp:Label ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                            <asp:Label ID="MiddleInitial" runat="server" Text='<%# Bind("MiddleInitial") %>'></asp:Label>
                                            <asp:Label ID="LastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Button" CommandName="Details" Text="Edit" ControlStyle-CssClass="cntbtn" />
                                    <asp:CommandField ButtonType="Button" ShowSelectButton="true" SelectText="View Details"/>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Button ID="AddContact" runat="server" Text="Add New Contact" OnClick="Add" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                    <div class="rightColumn">
                        <asp:UpdatePanel ID="ContactDetails" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewName" DataKeyNames="ContactId" AutoGenerateColumns="False" runat="server" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="ContactId" Visible="false" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>' Font-Size="X-Large" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="MiddleInitial" runat="server" Text='<%# Bind("MiddleInitial") %>' Font-Size="X-Large" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="LastName" runat="server" Text='<%# Bind("LastName") %>' Font-Size="X-Large" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <br />
                                <asp:GridView ID="PhoneGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="PhoneId" GridLines="None" RowStyle-CssClass="itempdg">
                                    <Columns>
                                        <asp:BoundField DataField="PhoneId" Visible="false" />
                                        <asp:BoundField DataField="ContactId" Visible="false" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone Numbers" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Underline="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACFrontParen" runat="server" Text="("></asp:Label>
                                                <asp:Label ID="AreaCodeLabel" runat="server" Text='<%# Bind("AreaCode") %>'></asp:Label>
                                                <asp:Label ID="ACEndParen" runat="server" Text=")"></asp:Label>
                                                <asp:Label ID="PhoneNumberP1Label" runat="server" Text='<%# Bind("PhoneNumberPOne") %>'></asp:Label>
                                                <asp:Label ID="DashLbl" runat="server" Text="-"></asp:Label>
                                                <asp:Label ID="PhoneNumberP2Lbl" runat="server" Text='<%# Bind("PhoneNumberPTwo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="Ext" runat="server" Text="ext:" Width="25px"></asp:Label>
                                                <asp:Label ID="ExtLabel" runat="server" Text='<%# Bind("Extension") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <br />
                                <asp:GridView ID="EmailGridView" runat="server" DataKeyNames="EmailId" AutoGenerateColumns="false" GridLines="None" RowStyle-CssClass="itempdg">
                                    <Columns>
                                        <asp:BoundField DataField="EmailId" Visible="false" />
                                        <asp:BoundField DataField="ContactId" Visible="false" />
                                        <asp:TemplateField HeaderText="E-Mail Addresses" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Underline="true">
                                            <ItemTemplate>
                                                <asp:Label ID="UserNameLbl" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                                <asp:Label ID="atLbl" runat="server" Text="@" Width="15px"></asp:Label>
                                                <asp:Label ID="DomainLbl" runat="server" Text='<%# Bind("Domain") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <asp:GridView ID="AddressGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="AddressId" GridLines="None" RowStyle-CssClass="itempdg">
                                    <Columns>
                                        <asp:BoundField DataField="AddressId" Visible="false" />
                                        <asp:BoundField DataField="ContactId" Visible="false" />
                                        <asp:TemplateField HeaderText="Addresses" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Underline="true">
                                            <ItemTemplate>
                                                <asp:Label ID="StreetLbl" runat="server" Text='<%# Bind("Street")%>'></asp:Label>
                                                <asp:Label ID="StreetLine2Lbl" runat="server" Text='<%# Bind("StreetLineTwo") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="CityLbl" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                                <asp:Label ID="comma" runat="server" Text=", "></asp:Label>
                                                <asp:Label ID="StateLbl" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                <asp:Label ID="ZipCodeLbl" runat="server" Text='<%# Bind("ZipCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                               
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>                
            </div>
        </div>
    </form>
</body>
</html>

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
                <div class="Column left">
                    <asp:UpdatePanel ID="ContactName" runat="server">
                        <ContentTemplate>
                            <div>
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="Contacts" Font-Size="X-Large" Font-Underline="true" Font-Bold="true"></asp:Label>
                                <br />
                            </div>
                            <asp:GridView ID="GridView2" DataKeyNames="ContactId" AutoGenerateColumns="false" runat="server" OnRowCommand="Edit" OnSelectedIndexChanged="SelectContactDetails"
                                OnRowDeleting="DeleteContact" OnRowDataBound="ConfirmDeleteContact" GridLines="None" RowStyle-CssClass="contactName" CellPadding="4">
                                <Columns>
                                    <asp:BoundField DataField="ContactId" Visible="false" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>' Font-Size="Large"></asp:Label>
                                            <asp:Label ID="MiddleInitial" runat="server" Text='<%# Bind("MiddleInitial") %>' Font-Size="Large"></asp:Label>
                                            <asp:Label ID="LastName" runat="server" Text='<%# Bind("LastName") %>' Font-Size="Large"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Link" CommandName="Details" Text="Edit" ItemStyle-CssClass="SelectDeleteBtn" />
                                    <asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="Display" DeleteText="Delete" ShowDeleteButton="true"
                                        ControlStyle-CssClass="SelectDeleteBtn" />
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Button ID="AddContact" runat="server" Text="Add New Contact" OnClick="Add" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="Column right">
                    <asp:UpdatePanel ID="ContactDetails" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="GridViewName" DataKeyNames="ContactId" AutoGenerateColumns="False" runat="server" GridLines="None" RowStyle-HorizontalAlign="Left"
                                RowStyle-CssClass="bottombdr">
                                <Columns>
                                    <asp:BoundField DataField="ContactId" Visible="false" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
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
                            <asp:GridView ID="PhoneGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="PhoneId" RowStyle-HorizontalAlign="Left" GridLines="None"
                                RowStyle-CssClass="contactdisplay">
                                <Columns>
                                    <asp:BoundField DataField="PhoneId" Visible="false" />
                                    <asp:BoundField DataField="ContactId" Visible="false" />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Phone Numbers" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>' Width="50px"></asp:Label>
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
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="PrimaryPhoneChkBx" runat="server" Enabled="false" Text='<%# Bind("PrimaryNumber") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />
                            <asp:GridView ID="EmailGridView" runat="server" DataKeyNames="EmailId" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Left" GridLines="None"
                                RowStyle-CssClass="contactdisplay">
                                <Columns>
                                    <asp:BoundField DataField="EmailId" Visible="false" />
                                    <asp:BoundField DataField="ContactId" Visible="false" />
                                    <asp:TemplateField HeaderText="E-Mail Addresses" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Underline="true"
                                        HeaderStyle-CssClass="headeralign">
                                        <ItemTemplate>
                                            <asp:Label ID="UserNameLbl" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                            <asp:Label ID="atLbl" runat="server" Text="@" Width="15px"></asp:Label>
                                            <asp:Label ID="DomainLbl" runat="server" Text='<%# Bind("Domain") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:GridView ID="AddressGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="AddressId" GridLines="None" RowStyle-CssClass="contactdisplay">
                                <Columns>
                                    <asp:BoundField DataField="AddressId" Visible="false" />
                                    <asp:BoundField DataField="ContactId" Visible="false" />
                                    <asp:TemplateField HeaderText="Addresses" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Left">
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

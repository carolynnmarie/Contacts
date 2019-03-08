<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditContact.aspx.cs" Inherits="ContactsEmpty.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function ConfirmDelete() {
            return confirm("Delete this contact and all contact information?");
        }
    </script>
    <link rel="stylesheet" type="text/css" href="../StyleSheet1.css" />
    <style>
        body {
            background-color: #fffcef;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="DetailsGridDiv">
            <div class="row">
                <div class="Column leftInt">
            <asp:UpdatePanel ID="UpdateContactDetailPanel" runat="server">
                <ContentTemplate>
                    <asp:Button ID="BackToContactsButton" runat="server" Text="Back To Contacts" OnClick="BackToContacts" />
                    <br />
                    <asp:GridView ID="GridView1" DataKeyNames="ContactId" AutoGenerateColumns="False" OnRowEditing="EditName" OnRowUpdating="UpdateName"
                        runat="server" GridLines="None" OnRowCancelingEdit="OnRowCancelingEditName">
                        <Columns>
                            <asp:BoundField DataField="ContactId" Visible="false" />
                            <asp:TemplateField ItemStyle-CssClass="bottombdr" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>' Font-Size="X-Large" Font-Bold="true"></asp:Label>
                                    <asp:Label ID="MiddleInitial" runat="server" Text='<%# Bind("MiddleInitial") %>' Font-Size="X-Large" Font-Bold="true"></asp:Label>
                                    <asp:Label ID="LastName" runat="server" Text='<%# Bind("LastName") %>' Font-Size="X-Large" Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="FirstNameTxtBx" runat="server" Text='<%# Eval("FirstName") %>'></asp:TextBox>
                                    <asp:TextBox ID="MITxtBx" runat="server" Text='<%# Eval("MiddleInitial") %>' MaxLength="3" Width="15px"></asp:TextBox>
                                    <asp:TextBox ID="LastNameTxtBx" runat="server" Text='<%# Eval("LastName") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Link" ShowEditButton="true" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:LinkButton ID="IntInfoLink" runat="server"  OnClick="GoToIntInfo" Text="Click to add international contact information"></asp:LinkButton>
                    <br />
                    <asp:GridView ID="PhoneGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="PhoneId" GridLines="None" OnRowDataBound="ConfirmDeletePhone"
                        OnRowEditing="EditPhone" OnRowUpdating="UpdatePhone" OnRowDeleting="DeletePhone" OnRowCancelingEdit="OnRowCancelingEditPhone" CellPadding="4">
                        <Columns>
                            <asp:BoundField DataField="PhoneId" Visible="false" />
                            <asp:BoundField DataField="ContactId" Visible="false" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="TypeList" runat="server" AppendDataBoundItems="true">                                       
                                        <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
                                        <asp:ListItem Value="Home">Home</asp:ListItem>
                                        <asp:ListItem Value="Work">Work</asp:ListItem>
                                        <asp:ListItem Value="Fax">Fax</asp:ListItem>
                                        <asp:ListItem Value="Google">Google</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Phone Numbers" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:Label ID="ACFrontParen" runat="server" Text="("></asp:Label>
                                    <asp:Label ID="AreaCodeLabel" runat="server" Text='<%# Bind("AreaCode") %>'></asp:Label>
                                    <asp:Label ID="ACEndParen" runat="server" Text=")"></asp:Label>
                                    <asp:Label ID="PhoneNumberP1Label" runat="server" Text='<%# Bind("PhoneNumberPOne") %>'></asp:Label>
                                    <asp:Label ID="DashLbl" runat="server" Text="-"></asp:Label>
                                    <asp:Label ID="PhoneNumberP2Lbl" runat="server" Text='<%# Bind("PhoneNumberPTwo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="ACFrontParenEdit" runat="server" Text="("></asp:Label>
                                    <asp:TextBox ID="AreaCodeTextBox" runat="server" Text='<%# Eval("AreaCode") %>'
                                        MaxLength="3" Width="25px"></asp:TextBox>
                                    <asp:Label ID="ACEndParenEdit" runat="server" Text=")"></asp:Label>
                                    <asp:TextBox ID="PhoneNumberP1TextBox" runat="server" Text='<%# Eval("PhoneNumberPOne") %>'
                                        MaxLength="3" Width="25px"></asp:TextBox>
                                    <asp:Label ID="DashLblEdit" runat="server" Text="-"></asp:Label>
                                    <asp:TextBox ID="PhoneNumberP2TextBox" runat="server" Text='<%# Eval("PhoneNumberPTwo") %>'
                                        MaxLength="4" Width="35px"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Ext" runat="server" Text="ext:" Width="25px"></asp:Label>
                                    <asp:Label ID="ExtLabel" runat="server" Text='<%# Bind("Extension") %>' Width="45" Visible="true"></asp:Label>
                                    <asp:Label ID="PrimaryPhoneLbl" runat="server" Text="Primary" Width="50px"></asp:Label>
                                    <asp:CheckBox ID="PrimaryNumberChkBx" runat="server" Enabled="false" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("PrimaryNumber")) %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="Ext" runat="server" Text="ext:" Width="25px"></asp:Label>
                                    <asp:TextBox ID="ExtTextBox" runat="server" Text='<%# Eval("Extension") %>'
                                        Width="45px"></asp:TextBox>
                                    <asp:Label ID="PrimaryPhoneLblEdit" runat="server" Text="Primary" Width="50px"></asp:Label>
                                    <asp:CheckBox ID="PrimaryNumberChkBxEdit" runat="server" Enabled="true" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("PrimaryNumber")) %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ControlStyle-CssClass="editbtn" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="SqlPhoneUpdateError" runat="server"></asp:Label>
                    <asp:DropDownList ID="PhoneTypeList" AutoPostBack="true" runat="server">
                        <asp:ListItem Selected="True" Value="">Select</asp:ListItem>
                        <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
                        <asp:ListItem Value="Home">Home</asp:ListItem>
                        <asp:ListItem Value="Work">Work</asp:ListItem>
                        <asp:ListItem Value="Fax">Fax</asp:ListItem>
                        <asp:ListItem Value="Google">Google</asp:ListItem>
                        <asp:ListItem Value="Other">Other</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="FrontParenthesis" runat="server" Text="(" CssClass="linemrgn"></asp:Label>
                    <asp:TextBox ID="AreaCodeTextBox" runat="server" MaxLength="3" Width="25px" CssClass="linemrgn"></asp:TextBox>
                    <asp:Label ID="EndParenthesis" runat="server" Text=")" CssClass="linemrgn"></asp:Label>
                    <asp:TextBox ID="NumberPart1TextBox" runat="server" MaxLength="3" Width="25px" CssClass="linemrgn"></asp:TextBox>
                    <asp:Label ID="Dash" runat="server" Text="-" CssClass="linemrgn"></asp:Label>
                    <asp:TextBox ID="NumberPart2TextBox" runat="server" MaxLength="4" Width="35px" CssClass="linemrgn"></asp:TextBox>
                    <asp:Label ID="Ext" runat="server" Text="Ext: " Width="30px" CssClass="linemrgn"></asp:Label>
                    <asp:TextBox ID="ExtTextBox" runat="server" Width="50px" CssClass="linemrgn"></asp:TextBox>
                    <asp:Label ID="SqlPhoneInsertError" runat="server"></asp:Label>
                    <asp:Label ID="PrimaryPhoneLbl" runat="server" Text="Primary" Width="50px"></asp:Label>
                    <asp:CheckBox ID="PrimaryNumberChkBxAdd" runat="server" Enabled="true" AutoPostBack="true" /> 
                    <br />
                    <asp:Button ID="AddPhoneButton" runat="server" Text="Add Phone Number" OnClick="AddPhone" CssClass="addbtn" />
                    <br />
                    <br />
                    <asp:GridView ID="EmailGridView" runat="server" DataKeyNames="EmailId" AutoGenerateColumns="false" OnRowEditing="EditEmail" OnRowDataBound="ConfirmDeleteEmail"
                        OnRowDeleting="DeleteEmail" OnRowUpdating="UpdateEmail" OnRowCancelingEdit="OnRowCancelingEditAddress" GridLines="None" CellPadding="4">
                        <Columns>
                            <asp:BoundField DataField="EmailId" Visible="false" />
                            <asp:BoundField DataField="ContactId" Visible="false" />
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="E-Mail Addresses" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:Label ID="UserNameLbl" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                    <asp:Label ID="atLbl" runat="server" Text="@" Width="15px"></asp:Label>
                                    <asp:Label ID="DomainLbl" runat="server" Text='<%# Bind("Domain") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="UserNameTxtBx" runat="server" Text='<%# Eval("UserName") %>'></asp:TextBox>
                                    <asp:Label ID="atLblEdit" runat="server" Text="@" Width="15px"></asp:Label>
                                    <asp:TextBox ID="DomainTxtBx" runat="server" Text='<%# Eval("Domain") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="PrimaryEmailLbl" runat="server" Text="Primary" Width="50px"></asp:Label>
                                    <asp:CheckBox ID="PEmailChkBx" runat="server" Enabled="false" AutoPostBack="true" Checked='<%#Convert.ToBoolean(Eval("PrimaryEmail")) %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="PrimaryEmailLblEdit" runat="server" Text="Primary" Width="50px"></asp:Label>
                                    <asp:CheckBox ID="PEmailChkBxEdit" runat="server" Enabled="true" AutoPostBack="true" Checked='<%#Convert.ToBoolean(Eval("PrimaryEmail")) %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ControlStyle-CssClass="editbtn" />
                        </Columns>
                    </asp:GridView>
                    <asp:TextBox ID="AddUserNameTxtBx" runat="server" CssClass="linemrgn"></asp:TextBox>
                    <asp:Label ID="At" runat="server" Text="@" CssClass="linemrgn"></asp:Label>
                    <asp:TextBox ID="AddDomainTxtBx" runat="server" CssClass="linemrgn"></asp:TextBox>
                    <asp:Label ID="PrimaryEmailLbl" runat="server" Text="Primary" Width="50px"></asp:Label>
                    <asp:CheckBox ID="PEmailChkBxAdd" runat="server" Enabled="true" AutoPostBack="true" />
                    <br />
                    <asp:Button ID="AddEmailButton" runat="server" Text="Add E-Mail Address" OnClick="AddEmail" CssClass="addbtn" />
                    <br />
                    <br />
                    <asp:GridView ID="AddressGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="AddressId" OnRowEditing="EditAddress"
                        OnRowUpdating="UpdateAddress" OnRowDeleting="DeleteAddress" OnRowCancelingEdit="OnRowCancelingEditAddress" GridLines="None" CellPadding="4"
                        OnRowDataBound="ConfirmDeleteAddress">
                        <Columns>
                            <asp:BoundField DataField="AddressId" Visible="false" />
                            <asp:BoundField DataField="ContactId" Visible="false" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Addresses" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Underline="true" ItemStyle-CssClass="itempdg">
                                <ItemTemplate>
                                    <asp:Label ID="StreetLbl" runat="server" Text='<%# Bind("Street")%>'></asp:Label>
                                    <asp:Label ID="StreetLine2Lbl" runat="server" Text='<%# Bind("StreetLineTwo") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="CityLbl" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                    <asp:Label ID="StateLbl" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                    <asp:Label ID="ZipCodeLbl" runat="server" Text='<%# Bind("ZipCode") %>' Width="50px"></asp:Label>                              
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="StreetTextBox" runat="server" Text='<%# Eval("Street")%>'></asp:TextBox>
                                    <asp:TextBox ID="StreetLine2TextBox" runat="server" Text='<%# Eval("StreetLineTwo") %>'></asp:TextBox>
                                    <br />
                                    <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Eval("City") %>'></asp:TextBox>
                                    <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Eval("State") %>'></asp:TextBox>
                                    <asp:TextBox ID="ZipCodeTextBox" runat="server" Text='<%# Eval("ZipCode") %>'></asp:TextBox>                               
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="PrimaryAddrLbl" runat="server" Text="Primary" Width="50px"></asp:Label>
                                    <asp:CheckBox ID="PrimaryAddrChkBx" runat="server" Enabled="false" AutoPostBack="true" Checked='<%#Convert.ToBoolean(Eval("PrimaryAddress")) %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="PrimaryAddrLbl" runat="server" Text="Primary" Width="50px" ></asp:Label>
                                    <asp:CheckBox ID="PrimaryAddrChkBxEdit" runat="server" Enabled="true" AutoPostBack="true" Checked='<%#Convert.ToBoolean(Eval("PrimaryAddress")) %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ControlStyle-CssClass="editbtn" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="AddStreetLabel" runat="server" Text="Street: " Width="60px" CssClass="linemrgn"></asp:Label>
                    <asp:TextBox ID="AddStreetTextBox" runat="server" CssClass="linemrgn"></asp:TextBox>
                    <asp:Label ID="AddStrLnTwoLabel" runat="server" Text="Street Line Two: " Width="120px" CssClass="linemrgn"></asp:Label>
                    <asp:TextBox ID="AddStrLnTwoTextBox" runat="server" CssClass="linemrgn"></asp:TextBox>
                    <br />
                    <asp:Label ID="AddCityLabel" runat="server" Text="City: " Width="60px"></asp:Label>
                    <asp:TextBox ID="AddCityTextBox" runat="server"></asp:TextBox>
                    <asp:Label ID="AddStateLabel" runat="server" Text="State: "></asp:Label>
                    <asp:TextBox ID="AddStateTextBox" runat="server" MaxLength="2" Width="25px"></asp:TextBox>
                    <asp:Label ID="AddZipCodeLabel" runat="server" Text="ZipCode: "></asp:Label>
                    <asp:TextBox ID="AddZipCodeTextBox" runat="server" Width="45px" MaxLength="5"></asp:TextBox>
                    <asp:Label ID="PrimaryAddrLblAdd" runat="server" Text="Primary" Width="50px"></asp:Label>
                    <asp:CheckBox ID="PrimaryAddrChkBxAdd" runat="server" Enabled="true" AutoPostBack="true" />
                    <asp:Label ID="SqlZipCodeError" runat="server"></asp:Label>
                    <br />
                    <asp:Button ID="AddAddressButton" runat="server" Text="Add Address" OnClick="AddAddress" CssClass="addbtn" />
                    <br />
                    <br />
                    <asp:Button ID="FinishedEditButton" runat="server" Text="Finish" OnClick="Finish" Font-Size="Larger" Font-Bold="true" CssClass="addbtn" />
                    <br />
                    <br />
                    <asp:LinkButton ID="DeleteContactButton" runat="server" Font-Size="Large" Text="Delete Contact" OnClick="DeleteContact"
                        OnClientClick="return ConfirmDelete();" />
                </ContentTemplate>
            </asp:UpdatePanel>
           </div>
                <div class="Column rightInt">
                    <asp:UpdatePanel ID="InternationalPanel" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="InternationalPhoneNumber" runat="server"></asp:Label>
                            <br />
                            <asp:GridView ID="IntPhoneGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="PhoneId" OnDataBound="IntPhoneGridView_DataBound" OnRowDataBound="IntPhoneGridView_RowDataBound"
                                 OnRowEditing="IntPhoneGridView_RowEditing" OnRowCancelingEdit="IntPhoneGridView_RowCancelingEdit" OnRowDeleting="IntPhoneGridView_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="PhoneId" Visible="false" />
                                    <asp:BoundField DataField="ContactId" Visible="false" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="CodeLbl" runat="server" Text="Country Code: "></asp:Label>
                                            <asp:Label ID="CountryCode" runat="server" Text='<%#Bind("CountryCode") %>'></asp:Label>
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

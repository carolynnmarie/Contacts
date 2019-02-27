<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailsPage.aspx.cs" Inherits="ContactsEmpty.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="DetailsGridDiv">
            <asp:UpdatePanel ID="UpdateContactDetailPanel" runat="server">
                <ContentTemplate>
                    CONTACT DETAILS<br />
                    <br /> 
                    <asp:Label ID="ContactInformation" runat="server" Text="Name" Font-Underline="true"></asp:Label>
                    <br />
                    <asp:Label ID="NameLabel" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="AddressLabel" runat="server" Text="Addresses" Font-Underline="true"></asp:Label>
                    <br />
                    <asp:GridView ID="AddressGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="AddressId" OnRowEditing="EditAddress" 
                        OnRowUpdating="UpdateAddress" OnRowDeleting="DeleteAddress" OnRowCancelingEdit="OnRowCancelingEditAddress" OnRowDataBound="OnRowDataBoundAddress">
                        <Columns>
                            <asp:BoundField DataField="AddressId"  Visible="false" />                           
                            <asp:BoundField DataField="ContactId" Visible="false" />  
                            <asp:TemplateField HeaderText="Street">
                                <ItemTemplate>
                                    <asp:Label ID="StreetLbl" runat="server" Text='<%# Bind("Street")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="StreetTextBox" runat="server" Text='<%# Eval("Street")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Street Line Two">
                                <ItemTemplate>
                                    <asp:Label ID="StreetLine2Lbl" runat="server" Text='<%# Bind("StreetLineTwo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="StreetLine2TextBox" runat="server" Text='<%# Eval("StreetLineTwo") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <asp:Label ID="CityLbl" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Eval("City") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label ID="StateLbl" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Eval("State") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ZipCode">
                                <ItemTemplate>
                                    <asp:Label ID="ZipCodeLbl" runat="server" Text='<%# Bind("ZipCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="ZipCodeTextBox" runat="server" Text ='<%# Eval("ZipCode") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--
                            <asp:TemplateField HeaderText="Primary">
                                <ItemTemplate>
                                    <asp:CheckBox ID="PrimaryAddrChkBox" runat="server" Text='<%# Bind("PrimaryAddress") %>'/>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="PrimaryAddrChkBoxEdit" runat="server" Text='<%# Eval("PrimaryAddress") %>'/>
                                </EditItemTemplate>
                            </asp:TemplateField>
                                 --%>
                            <asp:CommandField ButtonType="Button" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" />
                        </Columns>                           
                    </asp:GridView>                            
                    <br />
                       <asp:Label ID="AddNewAddressLabel" runat="server" Text="Add New Address" Font-Underline="true"></asp:Label>
                    <br />
                    <asp:Label ID="AddStreetLabel" runat="server" Text="Street: " Width="150px"></asp:Label>
                    <asp:TextBox ID="AddStreetTextBox" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="AddStrLnTwoLabel" runat="server" Text="Street Line Two: " Width="150px"></asp:Label>
                    <asp:TextBox ID="AddStrLnTwoTextBox" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="AddCityLabel" runat="server" Text="City: " Width="150px"></asp:Label>
                    <asp:TextBox ID="AddCityTextBox" runat="server"></asp:TextBox>
                    <asp:Label ID="AddStateLabel" runat="server" Text="State: "></asp:Label>
                    <asp:TextBox ID="AddStateTextBox" runat="server" MaxLength="2" Width="25px"></asp:TextBox>
                    <asp:Label ID="AddZipCodeLabel" runat="server" Text="ZipCode: " ></asp:Label>
                    <asp:TextBox ID="AddZipCodeTextBox" runat="server" Width="45px" MaxLength="5"></asp:TextBox>
                    <asp:CheckBox ID="AddPrimaryAddrChk" runat="server" />
                    <asp:Label ID="AddPrimaryAddrLabel" runat="server" Text="Primary Address"></asp:Label>
                    <br />
                    <asp:Button ID="AddAddressButton" runat="server" Text="Add Address" OnClick="AddAddress" />
                    <br />       
                    <asp:Label ID="PhoneNumbers" runat="server" Text="Phone Numbers" Font-Underline="true"></asp:Label>
                    <br /> 
                    <asp:GridView ID= "PhoneGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="PhoneId" 
                        OnRowEditing="EditPhone" OnRowUpdating="UpdatePhone" OnRowDeleting="DeletePhone" OnRowCancelingEdit="OnRowCancelingEditPhone">
                        <Columns>
                            <asp:BoundField DataField="PhoneId" Visible="false" />
                            <asp:BoundField DataField="ContactId" Visible="false" />
                            <asp:TemplateField HeaderText="Number Type">
                                <ItemTemplate>
                                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TypeTextBox" runat="server" Width="40px" Text='<%# Eval("Type") %>'></asp:TextBox>
                                </EditItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Area Code">
                                <ItemTemplate>
                                    <asp:Label ID="ACFrontParen" runat="server" Text="("></asp:Label>
                                    <asp:Label ID="AreaCodeLabel" runat="server" Text='<%# Bind("AreaCode") %>'></asp:Label>
                                    <asp:Label ID="ACEndParen" runat="server" Text=")"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="ACFrontParenEdit" runat="server" Text="("></asp:Label>
                                    <asp:TextBox ID="AreaCodeTextBox" runat="server" Text='<%# Eval("AreaCode") %>' 
                                        MaxLength="3" Width="25px"></asp:TextBox>
                                    <asp:Label ID="ACEndParenEdit" runat="server" Text=")"></asp:Label>
                                </EditItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone Number">
                                <ItemTemplate>
                                    <asp:Label ID="PhoneNumberP1Label" runat="server" Text='<%# Bind("PhoneNumberPOne") %>'></asp:Label>
                                    <asp:Label ID="DashLbl" runat="server" Text="-"></asp:Label>
                                    <asp:Label ID="PhoneNumberP2Lbl" runat="server" Text='<%# Bind("PhoneNumberPTwo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="PhoneNumberP1TextBox" runat="server" Text='<%# Eval("PhoneNumberPOne") %>'
                                        MaxLength="3" Width="25px"></asp:TextBox>
                                    <asp:Label ID="DashLblEdit" runat="server" Text="-"></asp:Label>
                                    <asp:TextBox ID="PhoneNumberP2TextBox" runat="server" Text='<%# Eval("PhoneNumberPTwo") %>'
                                        MaxLength="4" Width="35px"></asp:TextBox>
                                </EditItemTemplate>
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ext">
                                <ItemTemplate>
                                    <asp:Label ID="ExtLabel" runat="server" Text='<%# Bind("Extension") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="ExtTextBox" runat="server" Text='<%# Eval("Extension") %>'
                                        Width="45px"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--
                            <asp:TemplateField HeaderText="Primary">                                
                                <ItemTemplate>
                                    <asp:CheckBox ID="PrimaryChkBox" runat="server" Text='<%# Bind("PrimaryNumber") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="PrimaryChkBoxEdit" runat="server" Text='<%# Eval("PrimaryNumber") %>' />
                                </EditItemTemplate>                                    
                            </asp:TemplateField>
                                 --%>
                            <asp:CommandField ButtonType="Button" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Label ID="TypeLabel" runat="server" Text="Number Type: "></asp:Label> 
                    <asp:DropDownList ID="PhoneTypeList" AutoPostBack="true" runat="server">
                        <asp:ListItem Selected="True" Value="">Select</asp:ListItem>
                        <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
                        <asp:ListItem Value="Home">Home</asp:ListItem>
                        <asp:ListItem Value="Work">Work</asp:ListItem>
                        <asp:ListItem Value="Fax">Fax</asp:ListItem>
                        <asp:ListItem Value="Google">Google</asp:ListItem>
                        <asp:ListItem Value="Other">Other</asp:ListItem>
                    </asp:DropDownList>                           
                    <asp:Label ID="PhoneNumberLabel" runat="server" Text="Phone Number: " Width="130px"></asp:Label>
                    <asp:Label ID="FrontParenthesis" runat="server" Text="("></asp:Label>
                    <asp:TextBox ID="AreaCodeTextBox" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
                    <asp:Label ID="EndParenthesis" runat="server" Text=")"></asp:Label>
                    <asp:TextBox ID="NumberPart1TextBox" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
                    <asp:Label ID="Dash" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="NumberPart2TextBox" runat="server" MaxLength="4" Width="35px"></asp:TextBox>
                    <asp:Label ID="Ext" runat="server" Text="Ext: " Width="30px"></asp:Label>
                    <asp:TextBox ID="ExtTextBox" runat="server" Width="75px"></asp:TextBox>
                    <br />
                    <asp:Button ID="AddPhoneButton" runat="server" Text="Add Phone Number" OnClick="AddPhone" />
                    <br />
                    <br />
                    <asp:Label ID="Email" runat="server" Text="Email Addresses" Font-Underline="true"></asp:Label>
                    <br />
                    <asp:GridView ID="EmailGridView" runat="server" DataKeyNames="EmailId" AutoGenerateColumns="false" OnRowEditing="EditEmail" 
                        OnRowDeleting="DeleteEmail" OnRowUpdating="UpdateEmail" OnRowCancelingEdit="OnRowCancelingEditEmail">
                        <Columns>
                            <asp:BoundField DataField="EmailId" Visible="false" />
                            <asp:BoundField DataField="ContactId" Visible="false" />
                            <asp:TemplateField HeaderText="E-Mail Address">
                                <ItemTemplate>
                                    <asp:Label ID="UserNameLbl" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                    <asp:Label ID="atLbl" runat="server" Text="@" Width="15px"></asp:Label>
                                    <asp:Label ID="DomainLbl" runat="server" Text='<%# Bind("Domain") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="UserNameTxtBx" runat="server" Text='<%# Eval("UserName") %>' ></asp:TextBox>
                                    <asp:Label ID="atLblEdit" runat="server" Text="@" Width="15px"></asp:Label>
                                    <asp:TextBox ID="DomainTxtBx" runat="server" Text='<%# Eval("Domain") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Button" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Label ID="EmailLabel" runat="server" Text="E-Mail Address: "></asp:Label>
                    <asp:TextBox ID="AddUserNameTxtBx" runat="server"></asp:TextBox>
                    <asp:Label ID="At" runat="server" Text="@"></asp:Label>
                    <asp:TextBox ID="AddDomainTxtBx" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="AddEmailButton" runat="server" Text="Add E-Mail Address"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <p>
        <asp:Button ID="BackToContactsButton" runat="server" Text="Back To Contacts" OnClick="BackToContacts" />
        </p>
    </form>
</body>
</html>

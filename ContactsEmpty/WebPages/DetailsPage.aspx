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
                    <asp:GridView ID="AddressGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="AddressId" 
                        OnRowEditing="EditAddress" OnRowUpdating="UpdateAddress" OnRowDeleting="DeleteAddress" >
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
                    <br />
                    <asp:Button ID="InsertButton" runat="server" Text="Add New Address" OnClick="AddAddress" />
                    <br />       
                    <asp:Label ID="PhoneNumbers" runat="server" Text="Phone Numbers" Font-Underline="true"></asp:Label>
                    <br />
                    <asp:GridView ID= "PhoneGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="PhoneId" 
                        OnRowEditing="EditPhone" OnRowUpdating="UpdatePhone" OnRowDeleting="DeletePhone" >
                        <Columns>
                            <asp:BoundField DataField="PhoneId" Visible="false" />
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TypeTextBox" runat="server" Text='<%# Eval("Type") %>'></asp:TextBox>
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:Label ID="AreaCodeLabel" runat="server" Text='<%# Bind("AreaCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="AreaCodeTextBox" runat="server" Text='<%# Eval("AreaCode") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="PhoneNumberLabel" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="PhoneNumberTextBox" runat="server" Text='<%# Eval("PhoneNumber") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="ExtLabel" runat="server" Text='<%# Bind("Extension") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="ExtTextBox" runat="server" Text='<%# Eval("Extension") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="PrimaryChkBox" runat="server" Text='<%# Bind("PrimaryNumber") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="PrimaryChkBoxEdit" runat="server" Text='<%# Eval("PrimaryNumber") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <%-- 
        PhoneNumbers<asp:GridView ID="GridView2" runat="server" OnRowCommand="DeletePhone" OnRowEditing="EditPhone">
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Edit" />
                <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="AddPhoneButton" runat="server" Text="Add Phone Number" />
        <br />
        <br />
--%>
        Email<asp:GridView ID="GridView3" runat="server" OnSelectedIndexChanged="GridView4_SelectedIndexChanged" OnRowDeleting="DeleteEmail" OnRowEditing="EditEmail">
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Edit" />
                <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="AddEmailButton" runat="server" Text="Add E-Mail Address"/>
        <br />
        <p>
        <asp:Button ID="BackToContactsButton" runat="server" Text="Back To Contacts" OnClick="BackToContacts" />
        </p>
    </form>
</body>
</html>

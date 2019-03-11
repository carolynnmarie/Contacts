using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ContactsEmpty {

    public partial class WebForm2 : System.Web.UI.Page {

        private string contactId { get; set; }
        private string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
 
        protected void Page_Load(object sender, EventArgs e) {
                contactId = Request.QueryString["id"];
            if (String.IsNullOrEmpty(contactId)) {
                Response.Redirect("Contacts.aspx");
            }            
            if (!this.IsPostBack) {
                DataSet dataSet = GetDetails(contactId);
                GridView1.DataSource = dataSet.Tables["Contact"];
                GridView1.DataBind();
                AddressGridView.DataSource = dataSet.Tables["Address"];
                AddressGridView.DataBind();
                PhoneGridView.DataSource = dataSet.Tables["Phone"];
                PhoneGridView.DataBind();
                EmailGridView.DataSource = dataSet.Tables["Email"];
                EmailGridView.DataBind();
            }            
        }

        protected void BackToContacts(object sender, EventArgs e) {
            Response.Redirect("Contacts.aspx");
        }

        private DataSet GetDetails(string contact) {
            string contactQueryString = "SELECT * FROM Contact WHERE ContactId =@ContactId;";
            string addressQueryString = "SELECT AddressId, Street, StreetLineTwo,City,State,ZipCode,PrimaryAddress, ContactId FROM Address " +
                "WHERE ContactId =@ContactId AND Country='USA';";
            string eMailQueryString = "SELECT EmailId, UserName,Domain,PrimaryEmail, ContactId FROM Email WHERE ContactId =@ContactId;";
            string phoneQueryString = "SELECT PhoneId, Type, AreaCode, PhoneNumberPOne,PhoneNumberPTwo,Extension,PrimaryNumber, ContactId FROM Phone " +
                "WHERE ContactId=@ContactId AND CountryCode='1';";            
            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlDataAdapter contactAdapter = new SqlDataAdapter();
                contactAdapter.TableMappings.Add("Table", "Contact");
                SqlCommand cCommand = new SqlCommand(contactQueryString, connection);
                cCommand.Parameters.AddWithValue("@ContactId", contact);
                contactAdapter.SelectCommand = cCommand;
                contactAdapter.Fill(dataSet);

                SqlCommand aCommand = new SqlCommand(addressQueryString, connection);
                aCommand.Parameters.AddWithValue("ContactId", contact);
                SqlDataAdapter addressAdapter = new SqlDataAdapter();
                addressAdapter.TableMappings.Add("Table", "Address");
                addressAdapter.SelectCommand = aCommand;
                addressAdapter.Fill(dataSet);

                SqlCommand eCommand = new SqlCommand(eMailQueryString, connection);
                eCommand.Parameters.AddWithValue("ContactId", contact);
                SqlDataAdapter eMailAdapter = new SqlDataAdapter();
                eMailAdapter.TableMappings.Add("Table", "Email");
                eMailAdapter.SelectCommand = eCommand;
                eMailAdapter.Fill(dataSet);

                SqlCommand pCommand = new SqlCommand(phoneQueryString, connection);
                pCommand.Parameters.AddWithValue("ContactId", contact);
                SqlDataAdapter phoneAdapter = new SqlDataAdapter();
                phoneAdapter.TableMappings.Add("Table", "Phone");
                phoneAdapter.SelectCommand = pCommand;
                phoneAdapter.Fill(dataSet);

                connection.Close();

                DataColumn parentColumn = dataSet.Tables["Contact"].Columns["ContactId"];
                DataColumn addressChild = dataSet.Tables["Address"].Columns["ContactId"];
                DataColumn emailChild = dataSet.Tables["Email"].Columns["ContactId"];
                DataColumn phoneChild = dataSet.Tables["Phone"].Columns["ContactId"];
                DataRelation cAddrRelation = new DataRelation("contactAddress", parentColumn, addressChild);
                dataSet.Tables["Address"].ParentRelations.Add(cAddrRelation);
                DataRelation cEmailRelation = new DataRelation("contactEmail", parentColumn, emailChild);
                dataSet.Tables["Email"].ParentRelations.Add(cEmailRelation);
                DataRelation cPhoneRelation = new DataRelation("contactPhone", parentColumn, phoneChild);
                dataSet.Tables["Phone"].ParentRelations.Add(cPhoneRelation);
                return dataSet;
            }
        }

        protected void BindGrid() {
            DataSet dataSet = GetDetails(contactId);
            GridView1.DataSource = dataSet.Tables["Contact"];
            GridView1.DataBind();
            AddressGridView.DataSource = dataSet.Tables["Address"];
            AddressGridView.DataBind();
            PhoneGridView.DataSource = dataSet.Tables["Phone"];
            PhoneGridView.DataBind();
            EmailGridView.DataSource = dataSet.Tables["Email"];
            EmailGridView.DataBind();
        }
        
        protected void EditName(object sender, GridViewEditEventArgs e) {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void OnRowCancelingEditName(object sender, GridViewCancelEditEventArgs e) {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void UpdateName(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string contact = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value).ToString();
            string query = "UPDATE Contact SET FirstName=@FirstName, LastName=@LastName, MiddleInitial=@MiddleInitial WHERE ContactId=@ContactId";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string firstName = (row.FindControl("FirstNameTxtBx") as TextBox).Text;
                string mI = (row.FindControl("MITxtBx") as TextBox).Text;
                string lastName = (row.FindControl("LastNameTxtBx") as TextBox).Text;
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("ContactId", contact);
                command.Parameters.AddWithValue("FirstName", firstName);
                command.Parameters.AddWithValue("MiddleInitial", mI);
                command.Parameters.AddWithValue("LastName", lastName);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void EditPhone(object sender, GridViewEditEventArgs e) {
            PhoneGridView.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void OnRowCancelingEditPhone(object sender, GridViewCancelEditEventArgs e) {
            PhoneGridView.EditIndex = -1;
            this.BindGrid();
        }

        protected void UpdatePhone(object sender, GridViewUpdateEventArgs e) {
            string updatePhoneQuery = "UPDATE Phone SET Type=@Type,AreaCode=@AreaCode,PhoneNumberPOne=@PhoneNumberPOne," +
                "PhoneNumberPTwo=@PhoneNumberPTwo,Extension=@Extension,PrimaryNumber=@PrimaryNumber WHERE PhoneId=@PhoneId AND PhoneId IS NOT NULL";
            string phoneId = Convert.ToInt32(PhoneGridView.DataKeys[e.RowIndex].Value).ToString();
            GridViewRow row = PhoneGridView.Rows[e.RowIndex];
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(updatePhoneQuery, connection);
                string type1 = (row.FindControl("TypeList") as DropDownList).Text;
                string areaCode = (row.FindControl("AreaCodeTextBox") as TextBox).Text;
                string phoneNumberP1 = (row.FindControl("PhoneNumberP1TextBox") as TextBox).Text;
                string phoneNumberP2 = (row.FindControl("PhoneNumberP2TextBox") as TextBox).Text;
                string ext = (row.FindControl("ExtTextBox") as TextBox).Text;
                bool primaryNumber = (row.FindControl("PrimaryNumberChkBxEdit") as CheckBox).Checked;
                string pNStr = primaryNumber.ToString();
                string phoneNumber = areaCode + phoneNumberP1 + phoneNumberP2 + ext;
                command.Parameters.AddWithValue("Type", type1);
                command.Parameters.AddWithValue("AreaCode", areaCode);
                command.Parameters.AddWithValue("PhoneNumberPOne", phoneNumberP1);
                command.Parameters.AddWithValue("PhoneNumberPTwo", phoneNumberP2);
                command.Parameters.AddWithValue("Extension", ext);
                command.Parameters.AddWithValue("PrimaryNumber", pNStr);
                command.Parameters.AddWithValue("PhoneId", phoneId);
                try {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    PhoneGridView.EditIndex = -1;
                    this.BindGrid();
                    SqlPhoneUpdateError.Text = "";
                } catch (SqlException) {                   
                    SqlPhoneUpdateError.Text = "*Invalid entry. Use numeric digits only. <br />";
                }
            }
        }
             
        protected void DeletePhone(object sender, GridViewDeleteEventArgs e) {
            string deletePhoneQuery = "DELETE FROM Phone WHERE PhoneId=@PhoneId AND PhoneId IS NOT NULL";
            string phoneId = Convert.ToInt32(PhoneGridView.DataKeys[e.RowIndex].Value).ToString();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand command = new SqlCommand(deletePhoneQuery, connection);
                command.Parameters.AddWithValue("PhoneId", phoneId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            this.BindGrid();
        }

        protected void ConfirmDeletePhone(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != PhoneGridView.EditIndex) {
                LinkButton deleteLink = (LinkButton)e.Row.Cells[5].Controls[2];
                deleteLink.OnClientClick = "return confirm('Delete this phone number?');";
                CheckBox checkBox = (CheckBox)e.Row.FindControl("PrimaryNumberChkBxEdit");
            }
        }

        protected void AddPhone(object sender, EventArgs e) {
            SavePhone();
        }

        private void SavePhone() {
            string insertQuery = "INSERT INTO Phone(Type,AreaCode,PhoneNumberPOne,PhoneNumberPTwo, Extension, PrimaryNumber, ContactId) VALUES (@Type,@AreaCode," +
                "@PhoneNumberPOne,@PhoneNumberPTwo, @Extension,@PrimaryNumber,@ContactId)";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(insertQuery, connection);                
                command.Parameters.AddWithValue("Type", PhoneTypeList.Text);
                command.Parameters.AddWithValue("AreaCode", AreaCodeTextBox.Text);
                command.Parameters.AddWithValue("PhoneNumberPOne", NumberPart1TextBox.Text);
                command.Parameters.AddWithValue("PhoneNumberPTwo", NumberPart2TextBox.Text);
                command.Parameters.AddWithValue("Extension", ExtTextBox.Text);
                command.Parameters.AddWithValue("PrimaryNumber", PrimaryNumberChkBxAdd.Checked.ToString());
                command.Parameters.AddWithValue("ContactId", contactId);
                try {
                    connection.Open();                   
                    command.ExecuteNonQuery();
                    connection.Close();
                    PhoneGridView.EditIndex = -1;
                    this.BindGrid();
                    PhoneTypeList.ClearSelection();
                    AreaCodeTextBox.Text = "";
                    NumberPart1TextBox.Text = "";
                    NumberPart2TextBox.Text = "";
                    ExtTextBox.Text = "";
                    SqlPhoneInsertError.Text = "";
                } catch (SqlException) {
                    SqlPhoneInsertError.Text = "<br />*Invalid entry. Use numeric digits only. ";
                }                
            }          
        }

        protected void EditEmail(object sender, GridViewEditEventArgs e) {
            EmailGridView.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void OnRowCancelingEditEmail(object sender, GridViewCancelEditEventArgs e) {
            EmailGridView.EditIndex = -1;
            this.BindGrid();
        }

        protected void UpdateEmail(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = EmailGridView.Rows[e.RowIndex];
            string emailId = Convert.ToInt32(EmailGridView.DataKeys[e.RowIndex].Value).ToString();
            string updateQuery = "UPDATE Email SET UserName=@UserName, Domain=@Domain, PrimaryEmail=@PrimaryEmail WHERE EmailId=@EmailId";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string userName = (row.FindControl("UserNameTxtBx") as TextBox).Text;
                string domain = (row.FindControl("DomainTxtBx") as TextBox).Text;
                string primaryEmail = (row.FindControl("PEmailChkBxEdit") as CheckBox).Checked.ToString();
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("UserName", userName);
                command.Parameters.AddWithValue("Domain", domain);
                command.Parameters.AddWithValue("PrimaryEmail", primaryEmail);
                command.Parameters.AddWithValue("EmailId", emailId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            EmailGridView.EditIndex = -1;
            this.BindGrid();           
        }
       
        protected void DeleteEmail(object sender, GridViewDeleteEventArgs e) {
            string emailId = Convert.ToInt32(EmailGridView.DataKeys[e.RowIndex].Value).ToString();
            string deleteQuery = "DELETE from Email WHERE EmailId=@EmailId";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("EmailId", emailId);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
            }
            this.BindGrid();
        }

        protected void ConfirmDeleteEmail(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != EmailGridView.EditIndex) {
                LinkButton dtlEmailLink = (LinkButton)e.Row.Cells[4].Controls[2];
                dtlEmailLink.OnClientClick = "return confirm('Delete this e-mail address?');";
            }
        }

        protected void AddEmail(object sender, EventArgs e) {
            SaveEmail();
        }

        private void SaveEmail() {
            string insertQuery = "INSERT INTO Email(UserName,Domain,PrimaryEmail,ContactId) VALUES (@UserName, @Domain,@PrimaryEmail, @ContactId)";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("UserName", AddUserNameTxtBx.Text);
                command.Parameters.AddWithValue("Domain", AddDomainTxtBx.Text);
                command.Parameters.AddWithValue("PrimaryEmail", PEmailChkBxAdd.Checked.ToString());
                command.Parameters.AddWithValue("ContactId", contactId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            this.BindGrid();
            AddUserNameTxtBx.Text = "";
            AddDomainTxtBx.Text = "";
            PEmailChkBxAdd.Checked = false;
        }

        protected void EditAddress(object sender, GridViewEditEventArgs e) {
            AddressGridView.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void OnRowCancelingEditAddress(object sender, GridViewCancelEditEventArgs e) {
            AddressGridView.EditIndex = -1;
            this.BindGrid();
        }

        protected void UpdateAddress(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = AddressGridView.Rows[e.RowIndex];
            string addressId = Convert.ToInt32(AddressGridView.DataKeys[e.RowIndex].Value).ToString();
            string updateAddressQuery = "UPDATE Address SET Street=@Street,StreetLineTwo=@StreetLineTwo,City=@City,State=@State,ZipCode=@ZipCode" +
                ",PrimaryAddress=@PrimaryAddress WHERE AddressId=@AddressId AND AddressId IS NOT NULL";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand updateCommand = new SqlCommand(updateAddressQuery, connection);
                string street = (row.FindControl("StreetTextBox") as TextBox).Text;
                string streetLineTwo = (row.FindControl("StreetLine2TextBox") as TextBox).Text;
                string city = (row.FindControl("CityTextBox") as TextBox).Text;
                string state = (row.FindControl("StateTextBox") as TextBox).Text;
                string zipCode = (row.FindControl("ZipCodeTextBox") as TextBox).Text;
                string primaryAddress = (row.FindControl("PrimaryAddrChkBxEdit") as CheckBox).Checked.ToString();
                updateCommand.Parameters.AddWithValue("AddressId", addressId);
                updateCommand.Parameters.AddWithValue("Street", street);
                updateCommand.Parameters.AddWithValue("StreetLineTwo", streetLineTwo);
                updateCommand.Parameters.AddWithValue("City", city);
                updateCommand.Parameters.AddWithValue("State", state);
                updateCommand.Parameters.AddWithValue("ZipCode", zipCode);
                updateCommand.Parameters.AddWithValue("PrimaryAddress", primaryAddress);
                try{
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                    AddressGridView.EditIndex = -1;
                    this.BindGrid();
                    SqlZipCodeError.Text = "";
                } catch(SqlException) {
                    SqlZipCodeError.Text = "Zip Code must be numeric digits";
                }               
            }
        }

        protected void DeleteAddress(object sender, GridViewDeleteEventArgs e) {
            string id = Convert.ToInt32(AddressGridView.DataKeys[e.RowIndex].Value).ToString();
            string deleteQueryString = "DELETE FROM Address WHERE AddressId=@AddressId AND AddressId IS NOT NULL";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand deleteCommand = new SqlCommand(deleteQueryString, connection)) {
                    connection.Open();
                    deleteCommand.Parameters.AddWithValue("AddressId", id);
                    try {
                        deleteCommand.ExecuteNonQuery();
                    } catch (SqlException) {
                        SqlZipCodeError.Text = "There was an error in attempting to delete this address";
                    }                                                     
                    connection.Close();
                }
            }
            this.BindGrid();
        }

        protected void ConfirmDeleteAddress(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != AddressGridView.EditIndex) {
                LinkButton addrDltLink = (LinkButton)e.Row.Cells[4].Controls[2];
                addrDltLink.OnClientClick = "return confirm('Delete this address?');";
            }
        }

        protected void AddAddress(object sender, EventArgs e) {
            SaveAddress();
        }

        private void SaveAddress() {
            string insertQuery = "INSERT INTO Address(Street,StreetLineTwo,City,State,ZipCode,PrimaryAddress,ContactId) VALUES " +
                "(@Street,@StreetLineTwo,@City,@State,@ZipCode,@PrimaryAddress,@ContactId)";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string street = AddStreetTextBox.Text;
                if (!String.IsNullOrEmpty(AddStrLnTwoTextBox.Text)){
                    street += ", ";
                }
                SqlCommand command = new SqlCommand(insertQuery, connection);                              
                command.Parameters.AddWithValue("Street", street);
                command.Parameters.AddWithValue("StreetLineTwo", AddStrLnTwoTextBox.Text);                
                command.Parameters.AddWithValue("City", AddCityTextBox.Text);
                command.Parameters.AddWithValue("State", AddStateTextBox.Text);
                command.Parameters.AddWithValue("ZipCode", AddZipCodeTextBox.Text);
                command.Parameters.AddWithValue("PrimaryAddress", PrimaryAddrChkBxAdd.Checked.ToString());
                command.Parameters.AddWithValue("ContactId", contactId);
                try{
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    this.BindGrid();
                    AddStreetTextBox.Text = "";
                    AddStrLnTwoTextBox.Text = "";
                    AddCityTextBox.Text = "";
                    AddStateTextBox.Text = "";
                    AddZipCodeTextBox.Text = "";
                    SqlZipCodeError.Text = "";
                    PrimaryAddrChkBxAdd.Checked = false;
                } catch (SqlException) {
                    SqlZipCodeError.Text = "Zip Code must be numeric digits";
                }
            }            
        }

        protected void DeleteContact(object sender, EventArgs e) {
            string deleteQuery = "DELETE FROM Address WHERE ContactId=@ContactId;" +
                " DELETE FROM Phone WHERE ContactId=@ContactId;" +
                " DELETE FROM Email WHERE ContactId=@ContactId;" +
                " DELETE FROM Contact WHERE ContactId=@ContactId";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("ContactId", contactId);
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
            }
            Response.Redirect("Contacts.aspx");
        }


        protected void Finish(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(AddStreetTextBox.Text) || !String.IsNullOrEmpty(AddStrLnTwoTextBox.Text) || !String.IsNullOrEmpty(AddCityTextBox.Text)
                || !String.IsNullOrEmpty(AddStateTextBox.Text) || !String.IsNullOrEmpty(AddZipCodeTextBox.Text)) {
                SaveAddress();
            }
            if (!String.IsNullOrEmpty(AreaCodeTextBox.Text) || !String.IsNullOrEmpty(NumberPart1TextBox.Text) ||
                !String.IsNullOrEmpty(NumberPart2TextBox.Text) || !String.IsNullOrEmpty(ExtTextBox.Text)) {
                SavePhone();
            }
            if(!String.IsNullOrEmpty(AddDomainTxtBx.Text)|| !String.IsNullOrEmpty(AddUserNameTxtBx.Text)) {
                SaveEmail();
            }
            if(!String.IsNullOrEmpty(AddCountryCdTxtBx.Text) || !String.IsNullOrEmpty(AddIntPhoneTxtBx.Text)) {
                SaveInternationalPhone();
            }
            if(String.IsNullOrEmpty(SqlZipCodeError.Text)&&String.IsNullOrEmpty(SqlPhoneInsertError.Text)){
            Response.Redirect("Contacts.aspx");
            }            
        }

        protected void GoToIntInfo(object sender, EventArgs e) {
            BindIntData();
            InternationalInformation.Text = "International Contact Information";
            AddIntPhoneTxtBx.Visible = true;
            AddIPhoneBtn.Visible = true;
            AddCountryCdTxtBx.Visible = true;
            CntryCd.Visible = true;
            IPhoneLbl.Visible = true;
            IAddPrimaryLbl.Visible = true;
            AddIPhoneChkBx.Visible = true;
            IPhnDropDownList.Visible = true;
        }

        private DataSet GetInternationalData() {
            DataSet set = new DataSet();
            string iPhoneQuery = "SELECT PhoneId,Type,CountryCode,PrimaryNumber,ContactId,International FROM Phone WHERE ContactId=@ContactId AND CountryCode!='1'";           
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand iPCommand = new SqlCommand(iPhoneQuery, connection);
                iPCommand.Parameters.AddWithValue("ContactId", contactId);
                SqlDataAdapter iPAdapter = new SqlDataAdapter();
                iPAdapter.TableMappings.Add("Table", "InternationalPhone");
                iPAdapter.SelectCommand = iPCommand;
                iPAdapter.Fill(set);
                connection.Close();
                return set;
            }
        }

        private void BindIntData() {
            DataSet set = GetInternationalData();
            IntPhoneGridView.DataSource = set.Tables["InternationalPhone"];
            IntPhoneGridView.DataBind();          
        }
        
        protected void IntPhoneGridView_RowEditing(object sender, GridViewEditEventArgs e) {
            IntPhoneGridView.EditIndex = e.NewEditIndex;
            BindIntData();
        }
        protected void IntPhoneGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            IntPhoneGridView.EditIndex = -1;
            BindIntData();
        }
     
        protected void IntPhoneGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != IntPhoneGridView.EditIndex) {
                LinkButton addrDltLink = (LinkButton)e.Row.Cells[5].Controls[2];
                addrDltLink.OnClientClick = "return confirm('Delete this address?');";
            }
        }

        protected void IntPhoneGridView_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = IntPhoneGridView.Rows[e.RowIndex];
            string phoneId = Convert.ToInt32(IntPhoneGridView.DataKeys[e.RowIndex].Value).ToString();
            string update = "UPDATE Phone SET CountryCode=@CountryCode, PrimaryNumber=@PrimaryNumber, International=@International WHERE PhoneId=@PhoneId AND PhoneId" +
                "IS NOT NULL";
            using(SqlConnection connection=new SqlConnection(connectionString)) {
                string countryCode = (row.FindControl("CountryCodeTxtBx") as TextBox).Text;
                string phone = (row.FindControl("IPhoneNumberTxtBx") as TextBox).Text;
                string primary = (row.FindControl("PrimaryNumberChkBxEdit") as CheckBox).Checked.ToString();
                SqlCommand command = new SqlCommand(update, connection);
                command.Parameters.AddWithValue("CountryCode", countryCode);
                command.Parameters.AddWithValue("International", phone);
                command.Parameters.AddWithValue("PrimaryNumber", primary);
                command.Parameters.AddWithValue("PhoneId", phoneId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }           
            IntPhoneGridView.EditIndex = -1;
            BindIntData();
        }

        protected void IntPhoneGridView_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            string deleteQuery = "DELETE FROM Phone WHERE PhoneId=@PhoneId";
            string id = Convert.ToInt32(IntPhoneGridView.DataKeys[e.RowIndex].Value).ToString();
            using (SqlConnection connection = new SqlConnection(connectionString)){
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("PhoneId", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            BindIntData();
        }

        protected void InsertInternationalPhone(object sender, EventArgs e) {
            SaveInternationalPhone();
        }

        private void SaveInternationalPhone() {
            string insert = "INSERT INTO Phone(Type,CountryCode,International,PrimaryNumber,ContactId) VALUES (@Type,@CountryCode,@International,@PrimaryNumber,@ContactId);";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(insert, connection);
                command.Parameters.AddWithValue("Type", IPhnDropDownList.Text);
                command.Parameters.AddWithValue("CountryCode", AddCountryCdTxtBx.Text);
                command.Parameters.AddWithValue("International", AddIntPhoneTxtBx.Text);
                command.Parameters.AddWithValue("PrimaryNumber", AddIPhoneChkBx.Checked.ToString());
                command.Parameters.AddWithValue("ContactId", contactId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            AddCountryCdTxtBx.Text = "";
            AddIntPhoneTxtBx.Text = "";
            AddIPhoneChkBx.Checked = false;
            IPhnDropDownList.ClearSelection();
            BindIntData();
        }

        protected void IntPhoneGridView_DataBound(object sender, EventArgs e) {
        }       

    }
}
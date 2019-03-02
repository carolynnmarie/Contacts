using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ContactsEmpty {

    public partial class WebForm2 : System.Web.UI.Page{

        private string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private string contactId { get; set; }

        protected void Page_Load(object sender, EventArgs e){
            contactId = "0";
            contactId = Request.QueryString["id"];
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
            if (contactId.Equals("0")) {
                Response.Redirect("Contacts.aspx");
            }           
        }       

        private DataSet GetDetails(string ContactId) {
            string contactQueryString = "SELECT ContactId, LastName, FirstName, MiddleInitial FROM Contact WHERE ContactId = @ContactId";
            string addressQueryString = "SELECT AddressId, Street, StreetLineTwo,City,State,ZipCode,PrimaryAddress, ContactId FROM Address WHERE ContactId = @ContactId";
            string eMailQueryString = "SELECT EmailId, UserName,Domain,PrimaryEmail, ContactId FROM Email WHERE ContactId = @ContactId";
            string phoneQueryString = "SELECT PhoneId, Type, AreaCode, PhoneNumberPOne,PhoneNumberPTwo,Extension,PrimaryNumber, ContactId FROM Phone WHERE ContactId = @ContactId";


            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlDataAdapter contactAdapter = new SqlDataAdapter();
                contactAdapter.TableMappings.Add("Table", "Contact");

                connection.Open();
                DataSet dataSet = new DataSet();
                SqlCommand cCommand = new SqlCommand(contactQueryString, connection);
                cCommand.Parameters.AddWithValue("ContactId", ContactId);
                contactAdapter.SelectCommand = cCommand;                
                contactAdapter.Fill(dataSet);

                SqlCommand aCommand = new SqlCommand(addressQueryString, connection);
                aCommand.Parameters.AddWithValue("ContactId", contactId);
                SqlDataAdapter addressAdapter = new SqlDataAdapter();
                addressAdapter.TableMappings.Add("Table", "Address");
                addressAdapter.SelectCommand = aCommand;
                addressAdapter.Fill(dataSet);

                SqlCommand eCommand = new SqlCommand(eMailQueryString, connection);
                eCommand.Parameters.AddWithValue("ContactId", contactId);
                SqlDataAdapter eMailAdapter = new SqlDataAdapter();
                eMailAdapter.TableMappings.Add("Table", "Email");
                eMailAdapter.SelectCommand = eCommand;
                eMailAdapter.Fill(dataSet);

                SqlCommand pCommand = new SqlCommand(phoneQueryString, connection);
                pCommand.Parameters.AddWithValue("ContactId", contactId);
                SqlDataAdapter phoneAdapter = new SqlDataAdapter();
                phoneAdapter.TableMappings.Add("Table","Phone");
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

        protected void BindGrid(string key) {
            DataSet dataSet = GetDetails(key);
            GridView1.DataSource = dataSet.Tables["Contact"];
            GridView1.DataBind();
            AddressGridView.DataSource = dataSet.Tables["Address"];
            AddressGridView.DataBind();
            PhoneGridView.DataSource = dataSet.Tables["Phone"];
            PhoneGridView.DataBind();
            EmailGridView.DataSource = dataSet.Tables["Email"];
            EmailGridView.DataBind();            
        }

        
        protected void BackToContacts(object sender, EventArgs e) {
            Response.Redirect("Contacts.aspx");
        }

        protected void EditName(object sender, GridViewEditEventArgs e) {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid(contactId);            
        }

        protected void UpdateName(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string query = "UPDATE Contact SET FirstName=@FirstName, LastName=@LastName, MiddleInitial=@MiddleInitial WHERE ContactId=@ContactId";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string firstName = (row.FindControl("FirstNameTxtBx") as TextBox).ToString();
                string mI = (row.FindControl("MITxtBx") as TextBox).ToString();
                string lastName = (row.FindControl("LastNameTxtBx") as TextBox).ToString();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("ContactId", contactId);
                command.Parameters.AddWithValue("FirstName", firstName);
                command.Parameters.AddWithValue("MiddleInitial", mI);
                command.Parameters.AddWithValue("LastName", lastName);
                command.ExecuteNonQuery();
                connection.Close();
            }
            GridView1.EditIndex = -1;
            this.BindGrid(contactId);
        }

        protected void EditAddress(object sender, GridViewEditEventArgs e) {
            AddressGridView.EditIndex = e.NewEditIndex;
            this.BindGrid(contactId);
        }

        protected void UpdateAddress(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = AddressGridView.Rows[e.RowIndex];
            string addressId = Convert.ToInt32(AddressGridView.DataKeys[e.RowIndex].Value).ToString();
            string updateAddressQuery = "UPDATE Address SET Street=@Street,StreetLineTwo=@StreetLineTwo,City=@City,State=@State,ZipCode=@ZipCode" +
                " WHERE AddressId=@AddressId AND AddressId IS NOT NULL";
            using(SqlConnection connection= new SqlConnection(connectionString)) {                
                SqlCommand updateCommand = new SqlCommand(updateAddressQuery, connection);
                connection.Open();
                updateCommand.Parameters.AddWithValue("@AddressId", addressId);
                string street = (row.FindControl("StreetTextBox") as TextBox).Text;
                updateCommand.Parameters.AddWithValue("Street", street);
                string streetLineTwo = (row.FindControl("StreetLine2TextBox") as TextBox).Text;
                updateCommand.Parameters.AddWithValue("StreetLineTwo", streetLineTwo);
                string city = (row.FindControl("CityTextBox") as TextBox).Text;
                updateCommand.Parameters.AddWithValue("City", city);
                string state = (row.FindControl("StateTextBox") as TextBox).Text;
                updateCommand.Parameters.AddWithValue("State", state);
                string zipCode = (row.FindControl("ZipCodeTextBox") as TextBox).Text;
                updateCommand.Parameters.AddWithValue("ZipCode", zipCode);
                updateCommand.ExecuteNonQuery();
                connection.Close();            
            }
            AddressGridView.EditIndex = -1;
            this.BindGrid(contactId);
        }

        protected void OnRowCancelingEditAddress(object sender, GridViewCancelEditEventArgs e) {
            AddressGridView.EditIndex = -1;
            this.BindGrid(contactId);
        }

        protected void DeleteAddress(object sender, GridViewDeleteEventArgs e) {
            string id = Convert.ToInt32(AddressGridView.DataKeys[e.RowIndex].Value).ToString();        
            string deleteQueryString = "DELETE FROM Address WHERE AddressId=@AddressId AND AddressId IS NOT NULL";
            using(SqlConnection connection= new SqlConnection(connectionString)) {
                using (SqlCommand deleteCommand = new SqlCommand(deleteQueryString,connection)) {
                    connection.Open();
                    deleteCommand.Parameters.AddWithValue("AddressId", id);
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            this.BindGrid(contactId);
        }

        protected void AddressGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != AddressGridView.EditIndex) {
                LinkButton addrDltLink = (LinkButton)e.Row.Cells[3].Controls[2];
                addrDltLink.OnClientClick = "return confirm('Delete this address?');";
            }
        }
       
        protected void AddAddress(object sender, EventArgs e) {
            string insertQuery = "INSERT INTO Address(Street,StreetLineTwo,City,State,ZipCode,ContactId) VALUES " +
                "(@Street,@StreetLineTwo,@City,@State,@ZipCode,@ContactId)";
            using(SqlConnection connection= new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(insertQuery,connection);
                connection.Open();
                string streetLineOne = AddStreetTextBox.Text;
                command.Parameters.AddWithValue("Street", streetLineOne);
                string streetLineTwo = AddStrLnTwoTextBox.Text;
                command.Parameters.AddWithValue("StreetLineTwo", streetLineTwo);
                string city = AddCityTextBox.Text;
                command.Parameters.AddWithValue("City", city);
                string state = AddStateTextBox.Text;
                command.Parameters.AddWithValue("State", state);
                string zipCode = AddZipCodeTextBox.Text;
                command.Parameters.AddWithValue("ZipCode", zipCode);
                command.Parameters.AddWithValue("ContactId", contactId);

                command.ExecuteNonQuery();
                connection.Close();
            }
            this.BindGrid(contactId);
            AddStreetTextBox.Text = "";
            AddStrLnTwoTextBox.Text = "";
            AddCityTextBox.Text = "";
            AddStateTextBox.Text = "";
            AddZipCodeTextBox.Text = "";
        }

        protected void EditPhone(object sender, GridViewEditEventArgs e) {
            PhoneGridView.EditIndex = e.NewEditIndex;
            this.BindGrid(contactId);
        }

        protected void UpdatePhone(object sender, GridViewUpdateEventArgs e) {
            string updatePhoneQuery = "UPDATE Phone SET Type=@Type,AreaCode=@AreaCode,PhoneNumberPOne=@PhoneNumberPOne," +
                "PhoneNumberPTwo=@PhoneNumberPTwo,Extension=@Extension WHERE PhoneId=@PhoneId AND PhoneId IS NOT NULL";
            string phoneId = Convert.ToInt32(PhoneGridView.DataKeys[e.RowIndex].Value).ToString();
            GridViewRow row = PhoneGridView.Rows[e.RowIndex];
            using(SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(updatePhoneQuery, connection);
                connection.Open();
                string type = (row.FindControl("TypeTextBox") as TextBox).Text;
                command.Parameters.AddWithValue("Type", type);
                string areaCode = (row.FindControl("AreaCodeTextBox") as TextBox).Text;
                command.Parameters.AddWithValue("AreaCode", areaCode);
                string phoneNumberP1 = (row.FindControl("PhoneNumberP1TextBox") as TextBox).Text;
                command.Parameters.AddWithValue("PhoneNumberPOne", phoneNumberP1);
                string phoneNumberP2 = (row.FindControl("PhoneNumberP2TextBox") as TextBox).Text;
                command.Parameters.AddWithValue("PhoneNumberPTwo", phoneNumberP2);
                string ext = (row.FindControl("ExtTextBox") as TextBox).Text;
                command.Parameters.AddWithValue("Extension", ext);
                command.Parameters.AddWithValue("PhoneId", phoneId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            PhoneGridView.EditIndex = -1;
            this.BindGrid(contactId);
        }

        protected void OnRowCancelingEditPhone(object sender, GridViewCancelEditEventArgs e) {
            PhoneGridView.EditIndex = -1;
            this.BindGrid(contactId);
        }
         
        protected void DeletePhone(object sender, GridViewDeleteEventArgs e) {
            string deletePhoneQuery = "DELETE FROM Phone WHERE PhoneId=@PhoneId AND PhoneId IS NOT NULL";
            string phoneId= Convert.ToInt32(PhoneGridView.DataKeys[e.RowIndex].Value).ToString();
            using(SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand command = new SqlCommand(deletePhoneQuery, connection);
                command.Parameters.AddWithValue("PhoneId", phoneId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            this.BindGrid(contactId);
        }

        protected void PhoneGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != PhoneGridView.EditIndex) {
                LinkButton deleteLink = (LinkButton)e.Row.Cells[5].Controls[2];
                deleteLink.OnClientClick = "return confirm('Delete this phone number?');";
            }
        }

        protected void AddPhone(object sender, EventArgs e){
            string insertQuery = "INSERT INTO Phone(Type,AreaCode,PhoneNumberPOne,PhoneNumberPTwo, Extension, ContactId) VALUES (@Type,@AreaCode," +
                "@PhoneNumberPOne,@PhoneNumberPTwo, @Extension,@ContactId)";
            using(SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                connection.Open();
                string type = PhoneTypeList.Text;
                command.Parameters.AddWithValue("Type", type);
                string areaCode = AreaCodeTextBox.Text;
                command.Parameters.AddWithValue("AreaCode", areaCode);
                string phoneP1 = NumberPart1TextBox.Text;
                command.Parameters.AddWithValue("PhoneNumberPOne", phoneP1);
                string phoneP2 = NumberPart2TextBox.Text;
                command.Parameters.AddWithValue("PhoneNumberPTwo", phoneP2);
                string ext = ExtTextBox.Text;
                command.Parameters.AddWithValue("Extension", ext);
                command.Parameters.AddWithValue("ContactId", contactId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            PhoneGridView.EditIndex = -1;
            this.BindGrid(contactId);
            PhoneTypeList.ClearSelection();
            AreaCodeTextBox.Text = "";            
            NumberPart1TextBox.Text = "";
            NumberPart2TextBox.Text = "";
            ExtTextBox.Text = "";
        }

//        protected void OnRowDataBoundEmail(object sender, GridViewCommandEventArgs e) {
//
//        }

        protected void EditEmail(object sender, GridViewEditEventArgs e) {
            EmailGridView.EditIndex = e.NewEditIndex;
            this.BindGrid(contactId);
        }

        protected void UpdateEmail(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = EmailGridView.Rows[e.RowIndex];
            string emailId = Convert.ToInt32(EmailGridView.DataKeys[e.RowIndex].Value).ToString();
            string updateQuery = "UPDATE Email SET UserName=@UserName, Domain=@Domain WHERE EmailId=@EmailId";
            using(SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                connection.Open();
                string userName = (row.FindControl("UserNameTxtBx") as TextBox).Text;
                command.Parameters.AddWithValue("UserName", userName);
                string domain = (row.FindControl("DomainTxtBx") as TextBox).Text;
                command.Parameters.AddWithValue("Domain", domain);
                command.Parameters.AddWithValue("EmailId", emailId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            EmailGridView.EditIndex = -1;
            this.BindGrid(contactId);
        }

        protected void OnRowCancelingEditEmail(object sender, GridViewCancelEditEventArgs e) {
            EmailGridView.EditIndex = -1;
            this.BindGrid(contactId);
        }

        protected void DeleteEmail(object sender, GridViewDeleteEventArgs e) {
            string emailId = Convert.ToInt32(EmailGridView.DataKeys[e.RowIndex].Value).ToString();
            string deleteQuery = "DELETE from Email WHERE EmailId=@EmailId";
            using(SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("EmailId", emailId);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
            }
            this.BindGrid(contactId);
        }

        protected void EmailGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != EmailGridView.EditIndex) {
                LinkButton dtlEmailLink = (LinkButton)e.Row.Cells[3].Controls[2];
                dtlEmailLink.OnClientClick = "return confirm('Delete this e-mail address?');";
            }
        }

        protected void AddEmail(object sender, EventArgs e) {
            string insertQuery = "INSERT INTO Email(UserName,Domain,ContactId) VALUES (@UserName, @Domain, @ContactId)";
            using(SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand command = new SqlCommand(insertQuery, connection);
                string userName = AddUserNameTxtBx.Text;
                command.Parameters.AddWithValue("UserName", userName);
                string domain = AddDomainTxtBx.Text;
                command.Parameters.AddWithValue("Domain", domain);
                command.Parameters.AddWithValue("ContactId", contactId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            this.BindGrid(contactId);
            AddUserNameTxtBx.Text = "";
            AddDomainTxtBx.Text = "";
        }      
       
        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e) {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        protected void DeleteContact(object sender, EventArgs e) {
            string deleteQuery = "DELETE FROM Contact WHERE ContactId=@ContactId" +
                " DELETE FROM Address WHERE ContactId=@ContactId" +
                " DELETE FROM Phone WHERE ContactId=@ContactId" +
                " DELETE FROM Email WHERE ContactId=@ContactId";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("ContactId", contactId);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
            }
            Response.Redirect("Contacts.aspx");
        }
/*
        protected void ConfirmDeleteContact(object sender, EventArgs e) {
            if(e.)
        }
*/


    }
}
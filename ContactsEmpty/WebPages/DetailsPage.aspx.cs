using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

namespace ContactsEmpty{

    public partial class WebForm2 : System.Web.UI.Page{

        private string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private int contactId { get; set; }

        protected void Page_Load(object sender, EventArgs e){
            string id = Request.QueryString["id"];
            int x=0;
            Int32.TryParse(id, out x);
            if (!this.IsPostBack) {                
                this.BindGrid(x);
            }
            contactId = x;
        }       

        private DataSet GetDetails(int ContactId) {
            string contactQueryString = "SELECT ContactId, LastName, FirstName, MiddleInitial FROM Contact WHERE ContactId = @ContactId";
            string addressQueryString = "SELECT AddressId, Street, StreetLineTwo,City,State,ZipCode,PrimaryAddress, ContactId FROM Address WHERE ContactId = @ContactId";
            string eMailQueryString = "SELECT EmailId, UserName,Domain,PrimaryEmail, ContactId FROM Email WHERE ContactId = @ContactId";
            string phoneQueryString = "SELECT PhoneId, PhoneNumber,Extension,PrimaryNumber ContactId FROM Phone WHERE ContactId = @ContactId";

            string contactId = ContactId.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlDataAdapter contactAdapter = new SqlDataAdapter();
                contactAdapter.TableMappings.Add("Table", "Contact");

                connection.Open();
                DataSet dataSet = new DataSet();
                SqlCommand cCommand = new SqlCommand(contactQueryString, connection);
                cCommand.Parameters.AddWithValue("ContactId", contactId);
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
                /*
                foreach (DataRow row in dataSet.Tables["Phone"].Rows) {
                    row["PhoneNumber"] = "(" + row["PhoneNumber"].ToString().Substring(0, 3) + ")" + row["PhoneNumber"].ToString().Substring(3, 3)
                        + "-" + row["PhoneNumber"].ToString().Substring(6);
                }
                */               
                DataColumn parentColumn = dataSet.Tables["Contact"].Columns["ContactId"];
                DataColumn addressChild = dataSet.Tables["Address"].Columns["ContactId"];
                DataColumn emailChild = dataSet.Tables["Email"].Columns["ContactId"];
                DataColumn phoneChild = dataSet.Tables["Phone"].Columns["ContactId"];
                DataRelation cAddrRelation = new DataRelation("contactAddress", parentColumn, addressChild);
                dataSet.Tables["Address"].ParentRelations.Add(cAddrRelation);
                DataRelation cEmailRelation = new DataRelation("contactEmail", parentColumn, emailChild);
                dataSet.Tables["Email"].ParentRelations.Add(cEmailRelation);              
                return dataSet;
            }            
        }
        /*
        protected void Edit(object sender, EventArgs e) {
            Response.Redirect("EditPage.aspx");
        }*/

        protected void BindGrid(int key) {
            DataSet dataSet = GetDetails(key);
//            NameLabel.Text = printName(dataSet.Tables["Contact"]);
            AddressGridView.DataSource = dataSet.Tables["Address"];
            AddressGridView.DataBind();
            PhoneGridView.DataSource = dataSet.Tables["Phone"];
            PhoneGridView.DataBind();
            GridView3.DataSource = dataSet.Tables["Email"];
            GridView3.DataBind();            
        }

        protected void EditAddress(object sender, GridViewEditEventArgs e) {
            AddressGridView.EditIndex = e.NewEditIndex;
            this.BindGrid(contactId);
        }

        protected void UpdateAddress(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = AddressGridView.Rows[e.RowIndex];
            string addressId = Convert.ToInt32(AddressGridView.DataKeys[e.RowIndex].Value).ToString();
            string updateAddressQuery = "UPDATE Address SET Street=@Street,StreetLineTwo=@StreetLineTwo,City=@City,State=@State,ZipCode=@ZipCode " +
                "WHERE AddressId=@AddressId AND AddressId IS NOT NULL";
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

        protected void OnRowCancelingEditAddress(object sender, EventArgs e) {
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
/*
        protected void OnRowDataBoundAddress(object sender, GridViewRowEventArgs e) {
            this.BindGrid(contactId);
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex) {
                if (e.Row.RowIndex >= 0 && e.Row.Cells.Count>0) {
                    (e.Row.Cells[6].Controls[2] as Button).Attributes["onclick"] = "return confirm('Delete This Address?');";
                }
                this.BindGrid(contactId);
            }
        }
 */       
        
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
        
//        protected void OnRowDataBoundPhone(object sender, GridViewRowEventArgs e) {
//        }

        protected void EditPhone(object sender, GridViewEditEventArgs e) {
            PhoneGridView.EditIndex = e.NewEditIndex;
        }

        protected void UpdatePhone(object sender, GridViewUpdateEventArgs e) {
            string updatePhoneQuery = "UPDATE Phone SET Type=@Type,AreaCode=@AreaCode,PhoneNumber=@PhoneNumber,PrimaryNumber=@PrimaryNumber" +
                "WHERE PhoneId=@PhoneId AND PhoneId IS NOT NULL";
            string phoneId = Convert.ToInt32(PhoneGridView.DataKeys[e.RowIndex].Value).ToString();
            GridViewRow row = PhoneGridView.Rows[e.RowIndex];
            using(SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(updatePhoneQuery, connection);


            }
        }

        protected void OnRowCancelingEditPhone(object sender, EventArgs e) {
            PhoneGridView.EditIndex = -1;
        }

        protected void DeletePhone(object sender, GridViewDeleteEventArgs e) {

        }

        protected void AddPhone(object sender, EventArgs e){

        }

//        protected void OnRowDataBoundEmail(object sender, GridViewCommandEventArgs e) {
//
//        }

        protected void EditEmail(object sender, GridViewEditEventArgs e) {
            
        }

        protected void UpdateEmail(object sender, GridViewUpdateEventArgs e) {

        }

        protected void OnRowCancelingEditEmail(object sender, EventArgs e) {

        }

        protected void DeleteEmail(object sender, GridViewDeleteEventArgs e) {

        }

        protected void AddEmail(object sender, EventArgs e) {

        }



        protected void BackToContacts(object sender, EventArgs e) {
            Response.Redirect("ContactsPage.aspx");
        }




        private static string printName(DataTable table){
            string name = "";
            if (table.Rows.Count>0) {
                name = table.Rows[0][1].ToString() + " " + table.Rows[0][2].ToString() + " " + table.Rows[0][0].ToString();
            }
            return name;
        }

        private string printAddresses(DataTable addressTable){
            StringBuilder builder = new StringBuilder();
            if(addressTable.Rows.Count>0){
            foreach(DataRow row in addressTable.Rows){
                    builder.Append(row[0]);
                    string x = row[1].ToString();
                if ( !String.IsNullOrEmpty(x)){
                        builder.Append("<br/>")
                        .Append(row[1]);                        
                };
                builder.Append("<br/>")
                        .Append(row[2])
                    .Append(", ")
                    .Append(row[3])
                    .Append(" ")
                    .Append(row[4])
                    .Append("<br/>");

               // Fix: still displays *primary address when row[5] value is 0
                if(row[5].ToString().Equals(1)){
                    builder.Append("* Primary Address <br/>");
                 };
                 
                 builder.Append("<br/>");
             }      
            }
            return builder.ToString();
        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
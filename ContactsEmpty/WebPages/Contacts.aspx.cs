using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace ContactsEmpty{

    public partial class WebForm1 : System.Web.UI.Page {

        private string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e) {
            
            GridView2.DataSource = getContacts();
            GridView2.DataBind();
        }
    
        private DataSet getContacts(){
            string contactQueryString = "SELECT * FROM dbo.Contact ORDER BY LastName";
            using (SqlConnection connection = new SqlConnection(connectionString)){
                SqlDataAdapter contactAdapter = new SqlDataAdapter();
                DataSet contactDS = new DataSet();
                contactAdapter.TableMappings.Add("Table", "Contact");
                contactAdapter.SelectCommand = new SqlCommand(contactQueryString, connection);
                contactAdapter.Fill(contactDS);
                return contactDS;
            } 
        }

        protected void Edit(Object sender, GridViewCommandEventArgs e) {            
            int index = Convert.ToInt32(e.CommandArgument);
            string x = GridView2.DataKeys[index].Value.ToString();
            if (e.CommandName == "Details") {
                Response.Redirect($"EditContact.aspx?id=" + x);
            }           
        }

        protected void DeleteContactButton_Command(object sender, CommandEventArgs e) {
            string contactId = Convert.ToInt32(e.CommandArgument).ToString();
            DisplayContactDetails(contactId);
            string commandQuery = "DELETE FROM Address WHERE ContactId=@ContactId;  DELETE FROM Phone WHERE ContactId=@ContactId; " +
                                    "DELETE FROM Email WHERE ContactId=@ContactId; DELETE FROM Contact WHERE ContactId=@ContactId;";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand deleteCommand = new SqlCommand(commandQuery, connection);
                deleteCommand.Parameters.AddWithValue("ContactId", contactId);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
            }
            GridView2.DataBind();
        }

        protected void DeleteContact(object sender, GridViewDeleteEventArgs e) {
            string contactId = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value).ToString();
            DisplayContactDetails(contactId);
            string commandQuery = "DELETE FROM Address WHERE ContactId=@ContactId;  DELETE FROM Phone WHERE ContactId=@ContactId; " + 
                "DELETE FROM Email WHERE ContactId=@ContactId; DELETE FROM Contact WHERE ContactId=@ContactId;";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand deleteCommand = new SqlCommand(commandQuery, connection);
                deleteCommand.Parameters.AddWithValue("ContactId", contactId);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
            }
            GridView2.DataBind();
            Response.Redirect(Request.RawUrl);
        }

        protected void ConfirmDeleteContact(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                LinkButton dltLink = (LinkButton)e.Row.Cells[3].Controls[0];
                dltLink.OnClientClick= "return confirm('Delete this contact?');";
            }
        }
        
        protected void Add(Object sender, EventArgs e) {
            Response.Redirect("AddContact.aspx");
        }

        protected void SelectContactDetails(object sender, EventArgs e) {
            string contactId = GridView2.SelectedDataKey.Value.ToString();
            DisplayContactDetails(contactId);
        }

        private void DisplayContactDetails(string contactId) {
            string contactQueryString = "SELECT ContactId, LastName, FirstName, MiddleInitial FROM Contact WHERE ContactId = @ContactId";
            string addressQueryString = "SELECT AddressId, Street, StreetLineTwo,City,State,ZipCode,PrimaryAddress, ContactId FROM Address WHERE ContactId = @ContactId";
            string eMailQueryString = "SELECT EmailId, UserName,Domain,PrimaryEmail, ContactId FROM Email WHERE ContactId = @ContactId";
            string phoneQueryString = "SELECT PhoneId, Type, AreaCode, PhoneNumberPOne,PhoneNumberPTwo,Extension,PrimaryNumber, ContactId FROM Phone WHERE ContactId = @ContactId";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                DataSet dataSet = new DataSet();

                SqlDataAdapter contactAdapter = new SqlDataAdapter();
                contactAdapter.TableMappings.Add("Table", "Contact");
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
                phoneAdapter.TableMappings.Add("Table", "Phone");
                phoneAdapter.SelectCommand = pCommand;
                phoneAdapter.Fill(dataSet);

                connection.Close();

                GridViewName.DataSource = dataSet.Tables["Contact"];
                GridViewName.DataBind();
                AddressGridView.DataSource = dataSet.Tables["Address"];
                AddressGridView.DataBind();
                EmailGridView.DataSource = dataSet.Tables["Email"];
                EmailGridView.DataBind();
                PhoneGridView.DataSource = dataSet.Tables["Phone"];
                PhoneGridView.DataBind();

                
            }           
        }


    }
}
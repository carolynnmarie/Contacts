using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsEmpty {

    public partial class WebForm3 : System.Web.UI.Page{

        private string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e){

        }
        
        protected void CancelContact(object sender, EventArgs e) {
            Response.Redirect("Contacts.aspx");
        }

        //Client enters and saves a name.  The ContactId is then retrieved and sent to the edit page as the user is directed there to
        //input the rest of the contact information.
        protected void SaveName(object sender, EventArgs e) {
            string insertContact = "INSERT INTO Contact(LastName,FirstName,MiddleInitial) VALUES (@LastName,@FirstName,@MiddleInitial)";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string middleInitial = MiddleInitialTextBox.Text;
                if (!String.IsNullOrEmpty(firstName) || !String.IsNullOrEmpty(lastName) || !String.IsNullOrEmpty(middleInitial)) {
                    connection.Open();
                    SqlCommand insertNameComm = new SqlCommand(insertContact, connection);
                    insertNameComm.Parameters.AddWithValue("FirstName", firstName);
                    insertNameComm.Parameters.AddWithValue("LastName", lastName);
                    insertNameComm.Parameters.AddWithValue("MiddleInitial", middleInitial);
                    insertNameComm.ExecuteNonQuery();
                    connection.Close();
                    string selectContactId = "SELECT ContactId FROM Contact WHERE LastName=@LastName AND FirstName=@FirstName AND" +
                        " MiddleInitial=@MiddleInitial";
                    connection.Open();
                    SqlCommand command = new SqlCommand(selectContactId, connection);
                    command.Parameters.AddWithValue("FirstName", firstName);
                    command.Parameters.AddWithValue("LastName", lastName);
                    command.Parameters.AddWithValue("MiddleInitial", middleInitial);
                    string contactId = "";
                    try {                      
                        contactId = command.ExecuteScalar().ToString();
                    } catch {
                        Response.Redirect("Contacts.aspx");
                    }
                    connection.Close();
                    Response.Redirect($"EditContact.aspx?id=" + contactId);
                }
            }
        }

    }
   
}
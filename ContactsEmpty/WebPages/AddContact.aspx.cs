using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsEmpty {

    public partial class WebForm3 : System.Web.UI.Page{

        private string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e){


        }
        //My old method page

        /*
                protected void Save_Click(object sender, EventArgs e) {
                    string insertContact = "INSERT INTO Contact(LastName,FirstName,MiddleInitial) VALUES  (@LastName,@FirstName,@MiddleInitial)";                     
                    string insertAddr = "INSERT INTO Address(Street,StreetLineTwo,City,State,ZipCode,ContactId) VALUES " +
                        "(@Street,@StreetLineTwo,@City,@State,@ZipCode,(SELECT ContactId FROM Contact WHERE LastName=@LastName AND FirstName=@FirstName AND" +
                        " MiddleInitial=@MiddleInitial));";
                    string insertPhone= "INSERT INTO Phone(Type,AreaCode,PhoneNumberPOne,PhoneNumberPTwo,Extension,ContactId) VALUES (@Type,@AreaCode," +
                        "@PhoneNumberPOne,@PhoneNumberPTwo,@Extension,(SELECT ContactId FROM Contact WHERE LastName=@LastName AND FirstName=@FirstName AND MiddleInitial=@MiddleInitial));";
                    string insertEmail = "INSERT INTO Email(UserName,Domain,ContactId) VALUES (@UserName,@Domain,(SELECT ContactId FROM Contact WHERE LastName=@LastName AND FirstName=@FirstName " +
                        "AND MiddleInitial=@MiddleInitial))"; 

                    using (SqlConnection connection = new SqlConnection(connectionString)) {

                        string firstName = FirstNameTextBox.Text;
                        string lastName = LastNameTextBox.Text;
                        string middleInitial = MiddleInitialTextBox.Text;
                        if(!String.IsNullOrEmpty(firstName)||!String.IsNullOrEmpty(lastName)||!String.IsNullOrEmpty(middleInitial)){
                            connection.Open();
                            SqlCommand insertNameComm = new SqlCommand(insertContact, connection);             
                            insertNameComm.Parameters.AddWithValue("FirstName", firstName);
                            insertNameComm.Parameters.AddWithValue("LastName", lastName);
                            insertNameComm.Parameters.AddWithValue("MiddleInitial", middleInitial);
                            insertNameComm.ExecuteNonQuery();
                            connection.Close();
                        }

                        string streetLineOne = StreetTextBox.Text;
                        string streetLineTwo = StreetLine2TextBox.Text;               
                        string city = CityTextBox.Text;
                        string state = StateTextBox.Text;
                        string zipCode = ZipCodeTextBox.Text;
                        if (!String.IsNullOrEmpty(StreetLine2TextBox.Text)) {
                            streetLineOne = StreetTextBox.Text + ", ";
                        }
                        if(!String.IsNullOrEmpty(streetLineOne)||!String.IsNullOrEmpty(streetLineTwo)||!String.IsNullOrEmpty(city)||!String.IsNullOrEmpty(state)||
                            !String.IsNullOrEmpty(zipCode)){                   
                                SqlCommand commandAddr = new SqlCommand(insertAddr, connection);
                                commandAddr.Parameters.AddWithValue("FirstName", firstName);
                                commandAddr.Parameters.AddWithValue("LastName", lastName);
                                commandAddr.Parameters.AddWithValue("MiddleInitial", middleInitial);
                                commandAddr.Parameters.AddWithValue("Street", streetLineOne);
                                commandAddr.Parameters.AddWithValue("StreetLineTwo", streetLineTwo);
                                commandAddr.Parameters.AddWithValue("City", city);
                                commandAddr.Parameters.AddWithValue("State", state);
                                commandAddr.Parameters.AddWithValue("ZipCode", zipCode);
                                connection.Open();
                                commandAddr.ExecuteNonQuery();
                                connection.Close();                    
                        }

                        string phoneNumberType = PhoneTypeList.Text;
                        string areaCode = AreaCodeTextBox.Text;
                        string phoneNumberP1 = NumbPt1TxtBx.Text;
                        string phoneNumberP2 = NumberPart2TextBox.Text;
                        string ext = ExtTextBox.Text;
                        if(!String.IsNullOrEmpty(phoneNumberType)|| !String.IsNullOrEmpty(phoneNumberP1) || !String.IsNullOrEmpty(phoneNumberP2) || 
                            !String.IsNullOrEmpty(areaCode)||!String.IsNullOrEmpty(ext)){
                            SqlCommand commandPhone = new SqlCommand(insertPhone, connection);
                            commandPhone.Parameters.AddWithValue("FirstName", firstName);
                            commandPhone.Parameters.AddWithValue("LastName", lastName);
                            commandPhone.Parameters.AddWithValue("MiddleInitial", middleInitial);
                            commandPhone.Parameters.AddWithValue("Type", phoneNumberType);
                            commandPhone.Parameters.AddWithValue("AreaCode", areaCode);
                            commandPhone.Parameters.AddWithValue("PhoneNumberPOne", phoneNumberP1);
                            commandPhone.Parameters.AddWithValue("PhoneNumberPTwo", phoneNumberP2);
                            commandPhone.Parameters.AddWithValue("Extension", ext);
                            connection.Open();
                            commandPhone.ExecuteNonQuery();
                            connection.Close();
                        }

                        string userName = UserNameTextBox.Text;
                        string domain = DomainTextBox.Text;
                        if(!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(domain)){
                            SqlCommand commandEmail = new SqlCommand(insertEmail, connection);
                            commandEmail.Parameters.AddWithValue("FirstName", firstName);
                            commandEmail.Parameters.AddWithValue("LastName", lastName);
                            commandEmail.Parameters.AddWithValue("MiddleInitial", middleInitial);
                            commandEmail.Parameters.AddWithValue("UserName", userName);
                            commandEmail.Parameters.AddWithValue("Domain", domain);
                            connection.Open();
                            commandEmail.ExecuteNonQuery();
                            connection.Close();
                        }              
                    }
                    Response.Redirect("Contacts.aspx");
                }


        */


        protected void CancelContact(object sender, EventArgs e) {
            Response.Redirect("Contacts.aspx");
        }

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
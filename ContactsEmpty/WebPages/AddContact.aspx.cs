using System;
using System.Data.SqlClient;

namespace ContactsEmpty {

    public partial class WebForm3 : System.Web.UI.Page{

        private string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e){


        }

        protected void Save_Click(object sender, EventArgs e) {
            string insertContact = "INSERT INTO Contact(LastName,FirstName,MiddleInitial) VALUES  (@LastName,@FirstName,@MiddleInitial)";
            string insertAddrPhoneEmail = "INSERT INTO Address(Street,StreetLineTwo,City,State,ZipCode,ContactId) VALUES " +
                "(@Street,@StreetLineTwo,@City,@State,@ZipCode,(SELECT ContactId FROM Contact WHERE LastName=@LastName AND FirstName=@FirstName AND MiddleInitial=@MiddleInitial)) " +
                "INSERT INTO Phone(Type,AreaCode,PhoneNumberPOne,PhoneNumberPTwo,Extension,ContactId) VALUES (@Type,@AreaCode," +
                "@PhoneNumberPTwo,@PhoneNumberPTwo,@Extension,(SELECT ContactId FROM Contact WHERE LastName=@LastName AND FirstName=@FirstName AND MiddleInitial=@MiddleInitial)) " +
                "INSERT INTO Email(UserName,Domain,ContactId) VALUES (@UserName,@Domain,(SELECT ContactId FROM Contact WHERE LastName=@LastName AND FirstName=@FirstName AND MiddleInitial=@MiddleInitial))";            
            using (SqlConnection connection = new SqlConnection(connectionString)) {

                connection.Open();
                SqlCommand insertNameComm = new SqlCommand(insertContact, connection);             
                string firstName = FirstNameTextBox.Text;
                insertNameComm.Parameters.AddWithValue("FirstName", firstName);
                string lastName = LastNameTextBox.Text;
                insertNameComm.Parameters.AddWithValue("LastName", lastName);
                string middleInitial = MiddleInitialTextBox.Text;
                insertNameComm.Parameters.AddWithValue("MiddleInitial", middleInitial);
                insertNameComm.ExecuteNonQuery();
                connection.Close();

                SqlCommand command = new SqlCommand(insertAddrPhoneEmail, connection);
                command.Parameters.AddWithValue("FirstName", firstName);
                command.Parameters.AddWithValue("LastName", lastName);
                command.Parameters.AddWithValue("MiddleInitial", middleInitial);
                string streetLineOne = StreetTextBox.Text;
                command.Parameters.AddWithValue("Street",streetLineOne);
                string streetLineTwo = StreetLine2TextBox.Text;
                command.Parameters.AddWithValue("StreetLineTwo", streetLineTwo);
                string city = CityTextBox.Text;
                command.Parameters.AddWithValue("City", city);
                string state = StateTextBox.Text;
                command.Parameters.AddWithValue("State", state);
                string zipCode = ZipCodeTextBox.Text;
                command.Parameters.AddWithValue("ZipCode", zipCode);

                string phoneNumberType = PhoneTypeList.Text;
                command.Parameters.AddWithValue("Type", phoneNumberType);
                string areaCode = AreaCodeTextBox.Text;
                command.Parameters.AddWithValue("AreaCode", areaCode);
                string phoneNumberP1 =  NumberPart1TextBox.Text;
                command.Parameters.AddWithValue("PhoneNumberPOne", phoneNumberP1);
                string phoneNumberP2 = NumberPart2TextBox.Text;
                command.Parameters.AddWithValue("PhoneNumberPTwo", phoneNumberP2);
                string ext = ExtTextBox.Text;
                command.Parameters.AddWithValue("Extension", ext);

                string userName = UserNameTextBox.Text;
                command.Parameters.AddWithValue("UserName", userName);
                string domain = DomainTextBox.Text;
                command.Parameters.AddWithValue("Domain", domain);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            Response.Redirect("Contacts.aspx");
        }
         
        protected void CancelContact(object sender, EventArgs e) {
            Response.Redirect("Contacts.aspx");
        }

    }
}

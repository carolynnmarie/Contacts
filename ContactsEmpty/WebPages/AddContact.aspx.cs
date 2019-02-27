using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ContactsEmpty {

    public partial class WebForm3 : System.Web.UI.Page{

        string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e){


        }

        protected void Save_Click(object sender, EventArgs e) {

            string insertContactQuery = "INSERT INTO Contact(LastName,FirstName,MiddleInitial) VALUES (@LastName,@FirstName,@MiddleInitial)"+
            "INSERT INTO Address(Street,StreetLineTwo,City,State,ZipCode,ContactId) VALUES " +
                "(@Street,@StreetLineTwo,@City,@State,@ZipCode,(SELECT ContactId FROM Contact WHERE LastName=@LastName AND FirstName=@FirstName" +
                " AND MiddleInitial=@MiddleInitial))" +
                "INSERT INTO Phone(Type,AreaCode,PhoneNumberPOne,PhoneNumberPTwo,Extension,ContactId) VALUES (@Type,@AreaCode," +
                "@PhoneNumberPTwo,@PhoneNumberPTwo,@Extension,(SELECT ContactId FROM Contact WHERE LastName=@LastName AND " +
                "FirstName=@FirstName AND MiddleInitial=@MiddleInitial))" +
                "INSERT INTO Email(UserName,Domain,ContactId) VALUES (@UserName,@Domain,(SELECT ContactId FROM Contact WHERE LastName=@LastName" +
                " AND FirstName=@FirstName AND MiddleInitial=@MiddleInitial))";
            
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
               // SqlDataAdapter contactAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(insertContactQuery, connection);             
                string firstName = FirstNameTextBox.Text;
                command.Parameters.AddWithValue("FirstName", firstName);
                string lastName = LastNameTextBox.Text;
                command.Parameters.AddWithValue("LastName", lastName);
                string middleInitial = MiddleInitialTextBox.Text;
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
                string domain = "@" + DomainTextBox.Text;
                command.Parameters.AddWithValue("Domain", domain);

                command.ExecuteNonQuery();
                connection.Close();
            }
            Response.Redirect("ContactsPage.aspx");
        }

        /*
        private DataSet GetDataSet() {
            string contactSelectQuery = "SELECT ContactId,LastName,FirstName,MiddleInitial FROM Contacts";
            string addressSelectQuery = "SELECT AddressId,ContactId FROM Address";
            string phoneSelectQuery = "SELECT PhoneId,ContactId FROM Phone";
            string emailSelectQuery = "SELECT EmailId,ContactId FROM Email";
            
            using(SqlConnection connection = new SqlConnection(connectionString)) {
                DataSet dataSet = new DataSet("Contact");

                SqlDataAdapter contactAdapter = new SqlDataAdapter();
                contactAdapter.TableMappings.Add("Table", "Contact");
                connection.Open();
                SqlCommand cCommand = new SqlCommand(contactSelectQuery, connection);
                cCommand.CommandType = CommandType.Text;
                contactAdapter.SelectCommand = cCommand;
                contactAdapter.Fill(dataSet);

                SqlDataAdapter addressAdapter = new SqlDataAdapter();
                addressAdapter.TableMappings.Add("Table", "Address");
                SqlCommand aCommand = new SqlCommand(addressSelectQuery, connection);
                aCommand.CommandType = CommandType.Text;
                addressAdapter.SelectCommand = aCommand;
                addressAdapter.Fill(dataSet);
            }
            */
        }
       
    }

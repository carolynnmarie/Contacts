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

        protected void Page_Load(object sender, EventArgs e){
            
            DataSet dataSet = getDetails(1);
            Label1.Text = printName(dataSet.Tables["Contact"]);
            Label2.Text = printAddresses(dataSet.Tables["Address"]);
            GridView2.DataSource = dataSet.Tables["Address"];
            GridView2.DataBind();
            GridView3.DataSource = dataSet.Tables["Phone"];
            GridView3.DataBind();
            GridView4.DataSource = dataSet.Tables["Email"];
            GridView4.DataBind();
        }

        private static DataSet getDetails(int ContactId){
            string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            string contactQueryString = $"SELECT LastName, FirstName, MiddleInitial FROM Contact WHERE ContactId = {ContactId}";
            string addressQueryString = $"SELECT Street, StreetLineTwo,City,State,ZipCode, PrimaryAddress FROM Address WHERE ContactId = {ContactId}";
            string eMailQueryString = $"SELECT UserName, Domain, PrimaryEmail FROM Email WHERE ContactId = {ContactId}";
            string phoneQueryString = $"SELECT PhoneNumber,Extension, PrimaryPhoneNumber FROM Phone WHERE ContactId = {ContactId}";

            using(SqlConnection connection = new SqlConnection(connectionString)){
                SqlDataAdapter contactAdapter = new SqlDataAdapter();
                contactAdapter.TableMappings.Add("Table", "Contact");

                connection.Open();

                DataSet dataSet = new DataSet();
                SqlCommand cCommand = new SqlCommand(contactQueryString, connection);
                cCommand.CommandType = CommandType.Text;
                contactAdapter.SelectCommand = cCommand;
                contactAdapter.Fill(dataSet);
           
                SqlCommand aCommand = new SqlCommand(addressQueryString, connection);
                aCommand.CommandType = CommandType.Text;
                SqlDataAdapter addressAdapter = new SqlDataAdapter();
                addressAdapter.TableMappings.Add("Table", "Address");
                addressAdapter.SelectCommand = aCommand;
                addressAdapter.Fill(dataSet);

                SqlCommand eCommand = new SqlCommand(eMailQueryString, connection);
                eCommand.CommandType = CommandType.Text;
                SqlDataAdapter eMailAdapter = new SqlDataAdapter();
                eMailAdapter.TableMappings.Add("Table", "Email");
                eMailAdapter.SelectCommand = eCommand;
                eMailAdapter.Fill(dataSet);

                SqlCommand pCommand = new SqlCommand(phoneQueryString, connection);
                pCommand.CommandType = CommandType.Text;
                SqlDataAdapter phoneAdapter = new SqlDataAdapter();
                phoneAdapter.TableMappings.Add("Table", "Phone");
                phoneAdapter.SelectCommand = pCommand;
                phoneAdapter.Fill(dataSet);

                connection.Close();
                /*
                DataColumn parentColumn = dataSet.Tables["Contact"].Columns["ContactId"];

                DataColumn addressChildColumn = dataSet.Tables["Address"].Columns["ContactId"];
                DataRelation addressRelation = new DataRelation("ContactAddress", parentColumn, addressChildColumn);
                dataSet.Relations.Add(addressRelation);

                DataColumn emailChildColumn = dataSet.Tables["Email"].Columns["ContactId"];
                DataRelation emailRelation = new DataRelation("ContactEmail", parentColumn, emailChildColumn);
                dataSet.Relations.Add(emailRelation);

                DataColumn phoneChildColumn = dataSet.Tables["Phone"].Columns["ContactId"];
                DataRelation phoneRelation = new DataRelation("ContactPhone", parentColumn, phoneChildColumn);
                dataSet.Relations.Add(phoneRelation);
                */
                return dataSet;
            }       

        }

        private static string printName(DataTable nameTable){
            string name = nameTable.Rows[0][1].ToString() + " " + nameTable.Rows[0][2].ToString() + " " + nameTable.Rows[0][0];
            return name;
        }

        private static string printAddresses(DataTable addressTable){
            StringBuilder builder = new StringBuilder();
            foreach(DataRow row in addressTable.Rows){
                builder.Append(row[0])
                    .Append("\n");
                if (row[1] != null){
                    builder.Append(row[1])
                        .Append("\n");
                };
                builder.Append(row[2])
                    .Append(", ")
                    .Append(row[3])
                    .Append(" ")
                    .Append(row[4])
                    .Append("\n\n");
            }
            return builder.ToString();
        }
    }
}
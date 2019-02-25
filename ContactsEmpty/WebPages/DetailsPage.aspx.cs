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
            string id = Request.QueryString["id"];
            int x = -1;
            Int32.TryParse(id, out x);
            this.bindData(x);
        }

        private void bindData(int contactId) {
            DataSet dataSet = getDetails(contactId);
            Label1.Text = printName(dataSet.Tables["Contact"]);
            Label2.Text = printAddresses(dataSet.Tables["Address"]);
            GridView3.DataSource = dataSet.Tables["Phone"];
            GridView3.DataBind();
            GridView4.DataSource = dataSet.Tables["Email"];
            GridView4.DataBind();
        }

        protected void Edit(object sender, EventArgs e) {
            Response.Redirect("EditPage.aspx");
        }

        protected void BackToContacts(object sender, EventArgs e) {
            Response.Redirect("ContactsPage.aspx");
        }

        private static DataSet getDetails(int ContactId){
            string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            string contactQueryString = $"SELECT LastName, FirstName, MiddleInitial FROM Contact WHERE ContactId = {ContactId}";
            string addressQueryString = $"SELECT Street, StreetLineTwo,City,State,ZipCode,PrimaryAddress FROM Address WHERE ContactId = {ContactId}";
            string eMailQueryString = $"SELECT UserName,Domain,PrimaryEmail FROM Email WHERE ContactId = {ContactId}";
            string phoneQueryString = $"SELECT PhoneNumber,Extension,PrimaryNumber FROM Phone WHERE ContactId = {ContactId}";

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
                foreach(DataRow row in dataSet.Tables["Phone"].Rows) {
                    row["PhoneNumber"] = "(" + row["PhoneNumber"].ToString().Substring(0,3) + ")" + row["PhoneNumber"].ToString().Substring(3, 3)
                        + "-" + row["PhoneNumber"].ToString().Substring(6);
                }
                return dataSet;
            }       
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

        

    }
}
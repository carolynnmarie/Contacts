using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ContactsEmpty{

    public partial class WebForm1 : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            
            GridView1.DataSource = getContacts();
            GridView1.DataBind();
                     
        }
    
        private static DataSet getContacts(){
            string contactQueryString = "SELECT * FROM dbo.Contact ORDER BY LastName";
            string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString)){
                SqlDataAdapter contactAdapter = new SqlDataAdapter();
                DataSet contactDS = new DataSet();
                contactAdapter.TableMappings.Add("Table", "Contact");
                contactAdapter.SelectCommand = new SqlCommand(contactQueryString, connection);
                contactAdapter.Fill(contactDS);
                return contactDS;
            } 
        }

        protected void DetailsOrDelete(Object sender, GridViewCommandEventArgs e) {            
            int index = Convert.ToInt32(e.CommandArgument);
            string x = GridView1.DataKeys[index].Value.ToString();
            if (e.CommandName == "Details") {
                Response.Redirect($"DetailsPage.aspx?id=" + x);
            }
            if(e.CommandName == "Delete") {
                DeleteContactSqlDB(x);
            }
            Response.Redirect(Request.RawUrl);
        }


        protected void Add(Object sender, EventArgs e) {
            Response.Redirect("AddContact.aspx");
        }

        protected void DeleteContactSqlDB(string key) {
            string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string commandQuery = "DELETE FROM Contact WHERE ContactId=@ContactId DELETE FROM Address WHERE ContactId=@ContactId AND ContactId IS NOT NULL" +
                " DELETE FROM Phone WHERE ContactId=@ContactId AND ContactId IS NOT NULL DELETE FROM Email WHERE ContactId=@ContactId AND ContactId IS NOT NULL";
            using(SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand deleteCommand = new SqlCommand(commandQuery, connection);
                string contactId = key;
                deleteCommand.Parameters.AddWithValue("ContactId", contactId);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
            }
            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
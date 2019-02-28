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

        private string connectionString = "Data Source=LAPTOP-8VAG7JTV\\SQLEXPRESS;Initial Catalog=ContactsDataBase;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e) {
            
            GridView1.DataSource = getContacts();
            GridView1.DataBind();
                     
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

        protected void Details(Object sender, GridViewCommandEventArgs e) {            
            int index = Convert.ToInt32(e.CommandArgument);
            string x = GridView1.DataKeys[index].Value.ToString();
            if (e.CommandName == "Details") {
                Response.Redirect($"DetailsPage.aspx?id=" + x);
            }           
        }

        protected void DeleteContact(Object sender, GridViewDeleteEventArgs e) {
            string contactId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex]).ToString();
            string commandQuery = "DELETE FROM Contact WHERE ContactId=@ContactId DELETE FROM Address WHERE ContactId=@ContactId AND ContactId IS NOT NULL" +
                " DELETE FROM Phone WHERE ContactId=@ContactId AND ContactId IS NOT NULL DELETE FROM Email WHERE ContactId=@ContactId AND ContactId IS NOT NULL";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand deleteCommand = new SqlCommand(commandQuery, connection);
                deleteCommand.Parameters.AddWithValue("ContactId", contactId);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
            }
            GridView1.DataBind();
        }


        protected void Add(Object sender, EventArgs e) {
            Response.Redirect("AddContact.aspx");
        }

        protected void DeleteContactSqlDB(string key) {
            
            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
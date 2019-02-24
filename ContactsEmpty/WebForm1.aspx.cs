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


    }
}
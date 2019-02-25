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

        protected void Details(Object sender, GridViewCommandEventArgs e) {
            int index = Convert.ToInt32(e.CommandArgument);
            string x = GridView1.DataKeys[index].Value.ToString();
            Response.Redirect($"DetailsPage.aspx?id=" + x);
        }

        protected void Add(Object sender, EventArgs e) {
            Response.Redirect("AddContact.aspx");
        }
    
        private static SortedList<string,int> printName(DataTable table){
            SortedList<string, int> nameList = new SortedList<string, int>();
            foreach(DataRow dataRow in table.Rows) {
                string name = dataRow[1].ToString() + " " + dataRow[2].ToString() + " " + dataRow[0].ToString();
                string idString = dataRow[3].ToString();
                int id = int.Parse(idString);
                nameList.Add(name, id);
            };
            return nameList;
        }

        private static List<ContactName> printContact(DataTable table) {
            List<ContactName> contacts = new List<ContactName>();
            foreach(DataRow row in table.Rows) {

                ContactName name = new ContactName(row[0].ToString(), row[1].ToString(), row[2].ToString(), int.Parse(row[3].ToString()));
                contacts.Add(name);
            }
            return contacts;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
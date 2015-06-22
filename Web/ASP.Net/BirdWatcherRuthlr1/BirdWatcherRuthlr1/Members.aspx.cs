using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BirdWatcherRuthlr1
{
    public partial class Members : System.Web.UI.Page
    {
        DBManager database = new DBManager();
        SqlConnection bitDev = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {            
             DisplayTables();            
        }

        // show all the members
        public void DisplayTables()
        {
            String allBirds = "SELECT LastName as [Last Name],FirstName as [First Name], suburb as [Suburb] FROM tblMember ORDER BY LastName";
            database.doSelectQuery(allBirds, GridView1);
        }

        protected void AddMember_Click(object sender, EventArgs e)
        {

            database.Connect(); // connect to the database

            // creating variables to hold the apprioate input data
            String newFirstNm = TextBoxFirstNm.Text;
            String newLastNm = TextBoxLastNm.Text;
            String newSuburb = TextBoxSuburb.Text;

            // if all the fields are not empty
            if ((newFirstNm!="")&&(newLastNm!="")&&(newSuburb!=""))
            {

                database.insertMemberRecord(newLastNm, newFirstNm, newSuburb);

                feedback.Text = newFirstNm + " " + newLastNm + " has been successfully added"; // feedback

                DisplayTables(); // show results       
            }
            else
            {
                // alert the user to fill out all the fields
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please fill out all the fields')", true);
            }
            database.Close(); // close connection
        }
    }
}
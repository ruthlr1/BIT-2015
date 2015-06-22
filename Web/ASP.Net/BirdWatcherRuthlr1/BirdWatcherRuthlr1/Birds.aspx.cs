using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BirdWatcherRuthlr1
{
    public partial class Birds : System.Web.UI.Page
    {
        DBManager database = new DBManager();
        
        protected void Page_Load(object sender, EventArgs e)
        {           
            DisplayTables();                  
        }
 
        // Shows all birds from the table
        public void DisplayTables()
        {
            String allBirds = "SELECT MaoriName as [Maori Name], EnglishName as [English Name], ScientificName as [Scientific Name] FROM tblBird";
            database.doSelectQuery(allBirds, GridView1);
        }
        
        // adding a bird
        protected void AddBird_Click(object sender, EventArgs e)
        {
            database.Connect();
            // creating variables to hold the apprioate input data
            String eng = TextBoxEnglish.Text;
            String mao = TextBoxMaori.Text;
            String sci = TextBoxSci.Text;
            // if all the fields are not empty
            if ((eng != "") && (mao != "") && (sci != ""))
            {
                //insert bird using database insertBirdRecord
                database.insertBirdRecord(mao, eng, sci);
                feedback.Text = eng + " Bird has been successfully added"; // feedback

                DisplayTables(); // show reults                             
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BirdWatcherRuthlr1
{
    public partial class Sighting : System.Web.UI.Page
    {
        DBManager database = new DBManager();

        // Page runs automatically
        protected void Page_Load(object sender, EventArgs e)
        {                                       
            DisplayTables();
            
           // DropDownList("tblBird", "BirdID", "EnglishName", BirdsList);
           // DropDownList("tblMember", "MemberID", "fullName", MembersList);

            PopulateDropDownMember("SELECT memberID, lastName as [Last Name], firstName as [First Name] FROM dbo.tblMember", MembersList);
            PopulateDropDownBird("SELECT birdID, englishName FROM dbo.tblBird", BirdsList);
            
        }

        public void PopulateDropDownMember(String query, DropDownList listView)
        {
            database.Connect();
            SqlCommand cmd = new SqlCommand(query, database.bitDev);
            SqlDataReader qr = cmd.ExecuteReader();
            while (qr.Read())
            {
                ListItem lItem = new ListItem(qr["First Name"] +" "+qr["Last Name"]  , qr["memberID"].ToString());

                listView.Items.Add(lItem);
            }
            database.Close();
        }

        public void PopulateDropDownBird(String query, DropDownList listView)
        {
            database.Connect();
            SqlCommand cmd = new SqlCommand(query, database.bitDev);
            SqlDataReader qr = cmd.ExecuteReader();
            while (qr.Read())
            {
                ListItem lItem = new ListItem(qr["englishName"].ToString(), qr["birdID"].ToString());

                listView.Items.Add(lItem);
            }
            database.Close();
        }

        // Show all the bird sightings
        public void DisplayTables()
        {
            String allSightings = "SELECT tblMember.LastName as [Last Name],tblMember.FirstName as [First Name],  tblBird.maoriName as [Maori Name], " +
                "tblBird.englishName as [English Name] " +
                 "FROM tblMember JOIN tblBirdMember ON tblMember.memberID = tblBirdMember.memberID JOIN tblBird ON tblBirdMember.birdID = tblBird.birdID ORDER BY tblMember.LastName";
            database.doSelectQuery(allSightings, ShowEntries);
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            database.Connect();// connect to the database

            int birdSelected = Convert.ToInt32(BirdsList.SelectedItem.Value);// int id of bird that was chosen
            int nameSelected = Convert.ToInt32(MembersList.SelectedItem.Value); // int id of member that was chosen

            database.insertBirdMemberRecord(birdSelected, nameSelected);

            feedback.Text = "Bird has been sighted"; // show feedback
            DisplayTables(); // show all results

            database.Close(); // close connection
        }
        

        

            
     }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BirdWatcherRuthlr1
{
    public class DBManager
    {
        public SqlConnection bitDev = new SqlConnection();

        public void Connect()
        {
            if (bitDev.State == System.Data.ConnectionState.Open)
            {
                bitDev.Close();
            }
            bitDev.ConnectionString = "Data Source = bitdev.ict.op.ac.nz;" +
                "Initial Catalog = IN712_201501_ruthlr1;" + "User ID = ruthlr1;" +
                "Password = LRu_FBA1;";

            bitDev.Open();
        }
        

        public void doSelectQuery(string queryString, GridView gvDisplay)
        {
            Connect();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = bitDev;
            cmd.CommandText = queryString;

            gvDisplay.DataSource = cmd.ExecuteReader();
            gvDisplay.DataBind();
            Close();
        }

        /// <summary> Reset and fill database </summary>
        public void populateDatabase()
        {
            Connect();

            DropTable("tblBirdMember");
            DropTable("tblBird");
            DropTable("tblMember");

            CreatetblBird();
            InsertSeedBirds();

            CreatetblMember();
            InsertSeedMembers();

            CreatetblBirdMember();
            InsertSeedBirdMembers();

            Close();
        }

        

        public void Close()
        {
            bitDev.Close();
        }

        public void ExecuteNonQuery(String queryToExecute)
        {           
            //to do a non-select
            SqlCommand SExecute = new SqlCommand(queryToExecute, bitDev);
            SExecute.ExecuteNonQuery();            
        }

        public void DropTable(String tableName)
        {
            String dropTable = "IF OBJECT_ID('dbo." + tableName + "', 'U') IS NOT NULL DROP TABLE dbo." + tableName + ";";
            ExecuteNonQuery(dropTable);
        }
        /****************************************************************************************************************************************************************************
         *                  CREATE TABLES
         * ****************************************************************************************************************************************************************************
         */

        public void CreatetblBird()
        {
            String CreatetblBird = "CREATE TABLE dbo.tblBird(BirdID INT IDENTITY, MaoriName VARCHAR(30), EnglishName VARCHAR(30), ScientificName VARCHAR(30), PRIMARY KEY (BirdID))";
            ExecuteNonQuery(CreatetblBird);
        }

        public void CreatetblMember()
        {
            String CreatetblMember = "CREATE TABLE dbo.tblMember(MemberID INT IDENTITY, LastName VARCHAR(30), FirstName VARCHAR(30), Suburb VARCHAR(30), PRIMARY KEY (MemberID))";
            ExecuteNonQuery(CreatetblMember);
        }

        public void CreatetblBirdMember()
        {
            String CreatetblBirdMember = "CREATE TABLE dbo.tblBirdMember(BirdID INT, MemberID INT, FOREIGN KEY (BirdID) REFERENCES tblBird(BirdID), FOREIGN KEY (MemberID) REFERENCES tblMember(MemberID))";
            ExecuteNonQuery(CreatetblBirdMember);
        }

        /****************************************************************************************************************************************************************************
         *                  INSERT METHODS
         * ****************************************************************************************************************************************************************************
         */

        public void insertBirdRecord(string MaoriName, string EngName, string SciName)
        {
            String InsertBirdRecord = "INSERT INTO dbo.tblBird VALUES('" + MaoriName + "', '" + EngName + "', '" + SciName + "');";
            ExecuteNonQuery(InsertBirdRecord);
        }

        public void insertMemberRecord(string LastName, string FirstName, string suburb)
        {
            String insertMemberRecord = "INSERT INTO dbo.tblMember VALUES('" + LastName + "', '" + FirstName + "', '" + suburb + "');";
            ExecuteNonQuery(insertMemberRecord);
        }

        public void insertBirdMemberRecord(int BirdID, int MemberID)
        {
            String insertBirdMemberRecord = "INSERT INTO dbo.tblBirdMember VALUES('" + BirdID + "', '" + MemberID + "');";
            ExecuteNonQuery(insertBirdMemberRecord);
        }

        /****************************************************************************************************************************************************************************
         *                  INSERT DATA
         * ****************************************************************************************************************************************************************************
         */
        
        
        /// <summary> insert the birds </summary>
        public void InsertSeedBirds()
        {
            insertBirdRecord("Kea", "Mountain Parrot", "Nestor notabilis");
            insertBirdRecord("Kereru", "New Zealand Wood Pigeon", "Hemiphaga novaeseelandiae");
            insertBirdRecord("Korimako", "Bell bird", "Anthornis melanura");
            insertBirdRecord("Piwakawaka", "Fantail", "Rhipidura fulginosa");
            insertBirdRecord("Tauhou", "Silvereye", "Zosterops lateralis");
            insertBirdRecord("Toroa", "Royal Albatross", "Diomedea epomophora");
            insertBirdRecord("Tui", "Parson Bird", "Prosthemadera novaeseelandiae");
            insertBirdRecord("Wani", "Black Swan", "Cygnus atratus");
        }

        /// <summary> insert the members </summary>
        public void InsertSeedMembers()
        {
            insertMemberRecord("McCormack", "Howard", "Pine Hill");
            insertMemberRecord("Kerford", "Claudia", "Dunedin North");
            insertMemberRecord("Curro", "Benita", "St. Kilda");
            insertMemberRecord("Felsch", "Eva", "Roslyn");
            insertMemberRecord("Vandine", "Erik", "Opoho");
            insertMemberRecord("Moroney", "Louisa", "Ravensbourne");
            insertMemberRecord("Loh", "Jessie", "Waverly");
            insertMemberRecord("Stanford", "Ngaio", "Opoho");
            insertMemberRecord("Mills", "Elva", "Roslyn");
            insertMemberRecord("Woodford", "Sacha", "St. Kilda");
        }

        /// <summary> insert the sightings </summary>
        public void InsertSeedBirdMembers()
        {
            insertBirdMemberRecord(1, 2);
            insertBirdMemberRecord(1, 3);
            insertBirdMemberRecord(1, 7);
            insertBirdMemberRecord(2, 5);
            insertBirdMemberRecord(4, 9);
            insertBirdMemberRecord(8, 5);
            insertBirdMemberRecord(5, 10);
            insertBirdMemberRecord(6, 9);
            insertBirdMemberRecord(4, 7);
            insertBirdMemberRecord(3, 2);
            insertBirdMemberRecord(5, 8);
            insertBirdMemberRecord(6, 7);
            insertBirdMemberRecord(4, 10);
            insertBirdMemberRecord(8, 1);
            insertBirdMemberRecord(2, 4);
            insertBirdMemberRecord(3, 6);
        }

        
    }
}
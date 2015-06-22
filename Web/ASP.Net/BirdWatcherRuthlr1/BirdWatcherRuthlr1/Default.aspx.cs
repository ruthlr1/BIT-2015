using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BirdWatcherRuthlr1
{   
    public partial class Default : System.Web.UI.Page
    {
        DBManager dbManager = new DBManager();
        SqlConnection bitDev = new SqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                dbManager.populateDatabase();
                
            }
        }
        
    }
}
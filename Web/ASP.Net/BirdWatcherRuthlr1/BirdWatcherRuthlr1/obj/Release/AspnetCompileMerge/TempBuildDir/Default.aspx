<!-- Lachie Rutherford
     15/03/15
     All images that are used are for personal use -->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BirdWatcherRuthlr1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width">

    <title> BIT Bird Watchers Club </title>

    <link rel="stylesheet" type="text/css" href="BirdStyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="page">
			<div class="header">
                
				<asp:Image ID="Banner" runat="server" ImageUrl="images/birds.jpg" Width="900px" />

			</div>	
			<div class="logo"></div>
			<div class="navigation">
				<ul>
                    <li class="link"><a href="Default.aspx">Home</a></li>
					<li class="link"><a href="Birds.aspx">Birds</a></li>
					<li class="link"><a href="Members.aspx">Members</a></li>
					<li class="link"><a href="Sighting.aspx">All Sightings</a></li>
				</ul>
			</div>

			<div class="title">
				<h2>  Welcome to the BIT Bird Watchers Club </h2>
			</div>	
            
			<div class="text">
				
		        <br />
				
		        
                <p>
                    Bird Watching is a form of wildlife observation as a recreational activity. 
                    Birds can be viewed with the naked eye or through a visual enhancement like binoculars and telescopes.
                                        					        
				</p>
                <asp:Image ID="DisplayImg" ImageUrl="images/birdwatching.jpg" runat="server"  Width="300px" />
                <p>
                    The bird watchers club is made up of unique students from the Bachelor of Information Technology program in Dunedin.
                    The students monitor the wildlife on regular basis. There are weekly field trips out to the Otago Peninsula where the students are watching birds all day.

                </p>
                <p>
                    After a student sights a bird, another student must comfirm the sighting to validate it.
                    The next step is to log it into the database. If the student is not in the database, they can be added at any time.
						        
				</p>
		    </div>
            
            <div class="footer">
                <br />
                <p>International <b>+64 3 477 3014</b></p>
                <p>New Zealand <b>0800 762 786</b></p>
                <p>Email <b>info@op.ac.nz</b></p>
            </div>	    
        </div>
    </form>
</body>
</html>

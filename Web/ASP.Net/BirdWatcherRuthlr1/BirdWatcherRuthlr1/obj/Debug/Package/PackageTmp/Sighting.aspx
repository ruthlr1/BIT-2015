<!-- Lachie Rutherford
     15/03/15
     All images that are used are for personal use -->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sighting.aspx.cs" Inherits="BirdWatcherRuthlr1.Sighting" %>


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
				<h2>  All Sightings</h2>
			</div>	

			<div class="text">
                <br />
                <asp:Label ID="BirdLabel" runat="server" Text="Choose a Bird"></asp:Label>
                <br />
                <asp:DropDownList ID="BirdsList" runat="server" AppendDataBoundItems="True"></asp:DropDownList>
				<br />
                <br />
                <asp:Label ID="MemberLabel" runat="server" Text="Choose a Member"></asp:Label>
                <br />
                <asp:DropDownList ID="MembersList" runat="server" AppendDataBoundItems="True">
                </asp:DropDownList>
				
                <asp:Button ID="Submit" runat="server" Text="Submit and Show Sightings" OnClick="Submit_Click" Style="left:500px; position:absolute"/>
                <br />
                <asp:Label ID="feedback" runat="server"></asp:Label>
                <br />
                <br />
                <div style="height:400px; overflow:scroll">
                <asp:GridView ID="ShowEntries" runat="server" >
                </asp:GridView>
                    </div>
                <br />
				
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

<!-- Lachie Rutherford
     15/03/15
     All images that are used are for personal use -->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Birds.aspx.cs" Inherits="BirdWatcherRuthlr1.Birds" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


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
				<h2>  All Birds that have been sighted </h2>
			</div>	

			<div class="text">
				
		        <br />
                 <br />
                <asp:Label ID="AddBirdLabel" runat="server" Font-Bold="True" Font-Size="Medium" Text="Add a Bird"></asp:Label>
                <br />
                <br />
                <asp:Label ID="englishNmLabel" runat="server" Text="English Name" Style="left:40px; position:absolute"></asp:Label>
                <asp:Label ID="MaoriNmLabel" runat="server" Text="Maori Name" Style="left:200px; position:absolute"></asp:Label>
                <asp:Label ID="SciNmLabel" runat="server" Text="Scientific Name" Style="left:350px; position:absolute"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxEnglish" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBoxMaori" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBoxSci" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="AddBird" runat="server" OnClick="AddBird_Click" Text="Add Bird" />
                <asp:Label ID="feedback" runat="server"></asp:Label>
                <br />
                <br />
                <br />
				
		        <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
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

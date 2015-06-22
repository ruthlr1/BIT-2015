<!-- Lachie Rutherford
     15/03/15
     All images that are used are for personal use -->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Members.aspx.cs" Inherits="BirdWatcherRuthlr1.Members" %>

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
				<h2>  Members </h2>
			</div>	

			<div class="text">
				
		        <br />
                <asp:Label ID="AddMeberLabel" runat="server" Font-Bold="True" Font-Size="Medium" Text="Add a Member"></asp:Label>
                <br />
                <br />
                <asp:Label ID="firstNmLabel" runat="server" Text="First Name" Style="left:40px; position:absolute"></asp:Label>
                <asp:Label ID="LastNmLabel" runat="server" Text="Last Name" Style="left:200px; position:absolute"></asp:Label>
                <asp:Label ID="SuburbLabel" runat="server" Text="Suburb" Style="left:360px; position:absolute"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxFirstNm" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBoxLastNm" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBoxSuburb" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="AddMember" runat="server" OnClick="AddMember_Click" Text="Add Member" />
                <asp:Label ID="feedback" runat="server"></asp:Label>
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

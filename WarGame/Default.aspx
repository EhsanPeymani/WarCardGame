<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WarGame.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="resultLabel" runat="server"></asp:Label>
    
        <br />
        <br />
    
        <asp:Button ID="deckButton" runat="server" OnClick="deckButton_Click" Text="Deck" ViewStateMode="Enabled" />
&nbsp;<asp:Button ID="shuffleDeckButton" runat="server" OnClick="shuffleDeckButton_Click" Text="Shuffle Deck" ViewStateMode="Enabled" />
&nbsp;<asp:Button ID="dealCardsButton" runat="server" OnClick="dealCardsButton_Click" Text="Deal Cards" ViewStateMode="Enabled" />
&nbsp;<asp:Button ID="shuffleDealButton" runat="server" OnClick="shuffleDeal_Click" Text="Shuffle &amp; Deal" />
&nbsp;<asp:Button ID="playerHandButton" runat="server" OnClick="playerHandButton_Click" Text="Show Players Hand" ViewStateMode="Enabled" />
        &nbsp;<asp:Button ID="playRoundButton" runat="server" OnClick="playRoundButton_Click" Text="Play One Round" />
&nbsp;<asp:Button ID="playGameButton" runat="server" OnClick="playGameButton_Click" Text="Play Game" />
        <br />
    
    </div>
    </form>
</body>
</html>

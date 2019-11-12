<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryPage.aspx.cs" Inherits="CricketSystem.QueryPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Add query here...
        <br />
    <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" ForeColor="Black"  Rows="10" Width="500px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br/>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
    </div>
    </form>
</body>
</html>

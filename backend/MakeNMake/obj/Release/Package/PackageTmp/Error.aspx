<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MakeNMake.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Static/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-bottom:20px;">
        <img src="Static/images/404error.png" style="max-width:100%;" />
    </div>
      <center><asp:Button ID="btnError" OnClick="btnError_Click" CssClass="btn-danger btn" runat="server" Text="Back To LoginPage" /></center>  
    </form>
</body>
</html>

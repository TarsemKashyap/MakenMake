<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncodeDecode.aspx.cs" Inherits="MakeNMake.EncodeDecode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="txtdecode" runat="server"></asp:TextBox>
        <asp:Button ID="btndecode" runat="server" Text="Decode" OnClick="btndecode_Click" />
        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
    
    </div>
    </form>
</body>
</html>

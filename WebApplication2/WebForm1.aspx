<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div style="position:fixed;left:50%;top:50%;z-index:100;">
            <asp:FileUpload ID="FileUpload1" runat="server" Width="220px" />
     
            <br /><br /><br />
            
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" Width="75px" />
        </div>
    </form>
</body>
</html>

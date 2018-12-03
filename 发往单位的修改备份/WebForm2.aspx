<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="PredicTable.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
    <script type="text/javascript" src="js/plugins/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.cookie.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.validate.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.tagsinput.min.js"></script>
       <script type="text/javascript">
           function test(obj) {
               $("#" + obj.id).attr("readonly", false);
               
            }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
       <asp:TextBox ID="TextBox1" name="TextBox"  runat ="server"  OnTextChanged="TextBox1_TextChanged"  ondblclick="test(this)" AutoPostBack="true"></asp:TextBox>
         <asp:TextBox ID="TextBox2" name="TextBox" runat ="server" ></asp:TextBox>
         </div>
    </form>
</body>
</html>

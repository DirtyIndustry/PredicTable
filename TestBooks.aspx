<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestBooks.aspx.cs" Inherits="PredicTable.TestBooks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>书签检索测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>测试书签检索</h3>
        <asp:Button ID="btnCheckBook" runat="server" OnClick="btnCheckBook_Click" text ="获取"/>
    </div>
    </form>
</body>
</html>

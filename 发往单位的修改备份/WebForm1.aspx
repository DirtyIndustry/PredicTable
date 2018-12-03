<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="PredicTable.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lbl1" runat="server" Text="输入"></asp:Label>:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </br>
       <asp:Button ID="Button2" runat="server" Text="选择表单" OnClick="Button2_Click" />
            填报日期：
            <asp:Panel ID="Panel1" runat="server" Height="281px" Width="612px">
                <asp:RadioButton ID="RadioButton1" runat="server" Text="3号黄河南海堤预报单" />
                <br>
                <asp:RadioButton ID="RadioButton2" runat="server" Text="4号预报单" />
                <br>
                <asp:RadioButton ID="RadioButton3" runat="server" Text="7号海洋水温海冰预报" />
                <br>
                <asp:RadioButton ID="RadioButton4" runat="server" Text="9号预报单" />
                <br>
                <asp:RadioButton ID="RadioButton5" runat="server" Text="10号预报单" />
                <br>
                <asp:RadioButton ID="RadioButton6" runat="server" Text="11号预报单" />
                <br>
                <asp:RadioButton ID="RadioButton7" runat="server" Text="12号预报单" />
                <br>
                <asp:RadioButton ID="RadioButton8" runat="server" Text="14号海上山东" />
                <br>
                <asp:RadioButton ID="RadioButton9" runat="server" Text="15号预报单" />
                <br>
                <asp:RadioButton ID="RadioButton10" runat="server" Text="20号潍坊市海洋预报台专项预报" />
                <br>
                    <asp:RadioButton ID="RadioButton11" runat="server" Text="24号东营近海" />
                    <br></br>
                    <br>
                        <br></br>
                        <br>
                            <br>
                                <br></br>
                                <br>
                                    <br>
                                        <br></br>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br></br>
                                        <br></br>
                                        <br></br>
                                        <br></br>
                                        <br></br>
                                        <br></br>
                                    </br>
                                </br>
                            </br>
                        </br>
                    </br>
                </br>
            </asp:Panel>
            </br></br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="确定" />
            &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="生成表单" OnClick="Button1_Click" />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>

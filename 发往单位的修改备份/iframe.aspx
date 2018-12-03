<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iframe.aspx.cs" Inherits="PredicTable.iframe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
</head>
<body style="margin: 0px; padding: 0px; text-align: center">
    <form id="form1" runat="server">
        <div style="float: left">


            <asp:FileUpload ID="FileUploadWord" runat="server"></asp:FileUpload>
            <asp:Button ID="BtnSave" runat="server" Text="上传" OnClick="BtnSave_Click" />
            <%-- <asp:Label runat="server" ID="StatusLabel" Text=" " />--%>
            <asp:TextBox ID="TextBox1" Width="300px" runat="server" BorderStyle="None"></asp:TextBox>
            <input runat="server" id="newname" name="newname" size="50" type="hidden" />
            <input runat="server" id="type" name="type" size="50" type="hidden" />
            <%--Visible="false"--%>
            <%-- <input type="button"  onclick="aa()"/>--%>
            <script>
                //function aa() {
                //    alert(document.getElementById("TextBox1").value);
                //}
                //function bb() {
                //    alert("bb");
                //}
                //$("#TextBox1").change(function () {
                //    alert(1);
                //});
                parent.$("#l1").change(function () {
                    change();
                });
                parent.$("#l8").change(function () {
                    change();
                });
                parent.$("#l3").change(function () {
                    change();
                });
                parent.$("#l4").change(function () {
                    change();
                });
                parent.$("#l5").change(function () {
                    change();
                });
                parent.$("#l7").change(function () {
                    change();
                });
                $(document).ready(function () {

                    change();
                    var type = parent.document.getElementById('type').value;
                    $("#type").val(type);
                });
                //function a() {
                //    alert(1);
                //}
                function change() {

                    var newname = "";
                    newname += parent.$("#l1").val() + "_";
                    newname += parent.$("#l8").val() + "_";
                    newname += parent.$("#l3").val();
                    newname += parent.$("#l4").val() + "_";
                    newname += parent.$("#l5").val() + "_";
                    newname += parent.$("#l7").val();
                    $("#newname").val(newname);

                }

            </script>
        </div>
    </form>
</body>
</html>


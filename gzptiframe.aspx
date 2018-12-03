<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gzptiframe.aspx.cs" Inherits="PredicTable.gzptiframe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="js/plugins/jquery-1.7.min.js"></script>
    <script type="text/javascript" language="javascript">

        //$(document).ready(function () {

        //    var DOCTYPE = parent.document.getElementById('DOCTYPEwb').value;
        //    $("#DOCTYPE").val(DOCTYPE);

        //});

        //parent.$("#DOCTYPEwb").change(function () {
        //    var DOCTYPE = parent.document.getElementById('DOCTYPEwb').value;
        //    $("#DOCTYPE").val(DOCTYPE);
        //});

        //parent.$("#DOCUMENTCONTENT").blur(function () {
        //    var DOCUMENTCONTENT = parent.document.getElementById('DOCUMENTCONTENT').value;

        //    $("#DOCUMENTCONTENT").val(DOCUMENTCONTENT);
        //});
        function addForm() {
            var strForm = "<input type='file' size='30' name='File' /> 文件类型:<select name='vtype' id='type'> <option value='下载附件'>下载附件</option> <option value='展示文件'>展示文件</option><option value='封面'>封面</option> </select>  图片排序:<input  name='SORTID' size='8' type='text' /><br/>";
            document.getElementById('MyFile').insertAdjacentHTML("beforeEnd", strForm)
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">

        <div id="aa" align="center" >
            <textarea runat="server" style="width: 90%; resize: none;" onkeyup="this.value = this.value.slice(0, 2000)" id="DOCUMENTCONTENT" name="DOCUMENTCONTENT" cols="20" maxlength="2000" rows="7"></textarea>

            <br />
            <br />
            文档类型:<select runat="server" id="DOCTYPE">
                <option value="预报">预报</option>
                <option value="预警">预警</option>
            </select>&nbsp;&nbsp;
                                         <br />
            <p id="MyFile">
                <input type="file" size="30" name="File" />
                文件类型:<select name="vtype" id="type">
                    <option value="下载附件">下载附件</option>
                    <option value="展示文件">展示文件</option>
                </select>
                图片排序:<input  name='SORTID' size='8' type='text' />
                <br />
            
            </p>
            <p align="center">
                <input type="button" value="增加一个" onclick="addForm()" />

                <%-- <input runat="server" id="DOCUMENTCONTENT" name="DOCUMENTCONTENT" type="hidden" />--%>

                <%-- <input runat="server" id="DOCTYPE" name="DOCTYPE" size="50" type="hidden" />--%>
                <asp:Button runat="server" Text="提交数据" ID="UploadButton"
                    OnClick="UploadButton_Click"></asp:Button>
                <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </p>
        </div>
        <div id="bb">
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>

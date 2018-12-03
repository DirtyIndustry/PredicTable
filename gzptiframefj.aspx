<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gzptiframefj.aspx.cs" Inherits="PredicTable.gzptiframefj" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
 <script type="text/javascript" src="js/plugins/jquery-1.7.min.js"></script>
    <script type="text/javascript" language="javascript">
        function addForm() {
            var strForm = "<input type='file' size='30' name='File' /> 文件类型:<select name='vtype' id='type'> <option value='下载附件'>下载附件</option> <option value='展示文件'>展示文件</option> </select> 图片排序:<input  name='SORTID' size='8' type='text' /><br/> <br/>";
            document.getElementById('MyFile').insertAdjacentHTML("beforeEnd", strForm)
        }
       
      
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div >
            <p id="MyFile">
                <input type="file" size="30" name="File" />
                
               文件类型:<select name="vtype" id="type" >
                                                    <option value="下载附件">下载附件</option>
                                                    <option value="展示文件">展示文件</option>
                                                </select>
                 图片排序:<input  name='SORTID' size='8' type='text' />
                <br/>
                 <br/>
            </p>
            <p align="center">
                  <input runat="server" id="waiid" name="waiid" size="50" type="hidden"/>
                <input type="button" value="增加一个" onclick="addForm()" />
              
                 <asp:Button runat="server" Text="提交数据" ID="UploadButton"
                    OnClick="UploadButton_Click"></asp:Button>
                <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </p>
        </div>
    </form>
</body>
</html>


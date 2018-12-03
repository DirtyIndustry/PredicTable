<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kindeditoriframe.aspx.cs" Inherits="PredicTable.kindeditoriframe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="js/plugins/jquery-1.7.min.js"></script>
    <link rel="stylesheet" href="kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="kindeditor/plugins/code/prettify.css" />
    <script charset="utf-8" src="kindeditor/kindeditor-all-min.js"></script>
    <script charset="utf-8" src="kindeditor/kindeditor.js"></script>
    <script charset="utf-8" src="kindeditor/lang/zh-CN.js"></script>
    <script charset="utf-8" src="kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript">
        var urllist = "";
        KindEditor.ready(function (K) {
            var editor = K.create('#DOCUMENTCONTENTwx', {
                //上传管理
                uploadJson: 'Ajax/upload_json.ashx',
                //文件管理
                fileManagerJson: 'Ajax/file_manager_json.ashx',
                allowFileManager: true,
                //设置编辑器创建后执行的回调函数
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                },
                //上传文件后执行的回调函数,获取上传图片名称（包括扩展名称）
                afterUpload: function (url) {
                    // alert(url);
                    urllist = urllist + url + ",";

                    $("#urllist").val(urllist);

                    // alert(urllist);
                },
                afterBlur: function () {
                    this.sync();
                    var html = document.getElementById('DOCUMENTCONTENTwx').value;//原生API 
                    $("#schtmlnr").val(html);//把KindEditor产生的html代码放到schtmlnr里面，用于提交
                },

                //编辑器宽度
                width: '800px',
                //编辑器高度
                height: '250px;',
                //配置编辑器的工具栏
                items: [
                'source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
                'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
                'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
                'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
                'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
                'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'image', 'multiimage',
                'flash', 'media', 'insertfile', 'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
                'anchor', 'link', 'unlink', '|', 'about'
                ]
            });
            prettyPrint();

        });
        function addForm() {
            var strForm = "<input type='file' size='50' name='File' /> 文件类型:<select name='vtype' id='type'> <option value='下载附件'>下载附件</option> <option value='展示文件'>展示文件</option><option value='封面'>封面</option> </select>  图片排序:<input  name='SORTID' size='8' type='text' /><br/>";
            document.getElementById('MyFile').insertAdjacentHTML("beforeEnd", strForm)
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div align="center">
            标题：<input runat="server" id="SUBJECT" name="SUBJECT" size="50" type="text" />
            <br />
            <br />
            摘要：<input runat="server" id="ABSTRACT" name="ABSTRACT" size="50" type="text" />
            <br />
            <asp:TextBox ID="DOCUMENTCONTENTwx" name="DOCUMENTCONTENTwx" TextMode="MultiLine" runat="server"></asp:TextBox>
            <input runat="server" id="urllist" name="urllist" type="hidden" />
            <textarea runat="server" rows="10" cols="200" name="schtmlnr" id="schtmlnr" style="display: none;"></textarea>
            <br />
            文档类型:<select runat="server" id="DOCTYPE">
                <option value="预报">预报</option>
                <option value="预警">预警</option>
            </select>&nbsp;&nbsp;
               类型:<select runat="server" id="TYPE">
                   <option value="文字">文字</option>
                   <option value="图文">图文</option>
                   <option value="视频">视频</option>
                   <option value="音频">音频</option>
               </select>&nbsp;&nbsp; 
              <br />

            <p id="MyFile">
                <input type="file" size="50" name="File" />
                文件类型:<select name="vtype" id="vtype">
                    <option value="下载附件">下载附件</option>
                    <option value="展示文件">展示文件</option>
                    <option value="封面">封面</option>
                </select>
                图片排序:<input name="SORTID" size="8" type="text" />
                <br />
            </p>
             
            <p align="center">
                <input type="button" value="增加一个" onclick="addForm()" />
                <asp:Button runat="server" Text="提交数据" ID="WXButton" OnClick="WXButton_Click" UseSubmitBehavior="false"></asp:Button>
            </p>

        </div>
    </form>


</body>
</html>

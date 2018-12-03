<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpTideData1.aspx.cs" Inherits="PredicTable.UpTideData1" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/Gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />

    <link rel="stylesheet" href="css/style.default.css" type="text/css" />

    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="js/bundle.js"></script>--%>

<%--    <script type="text/javascript">
        var SupportFileApis = false;
        // Check for the various File API support.
        if (window.File && window.FileReader && window.FileList && window.Blob) {
            // Great success! All the File APIs are supported.

            SupportFileApis = true;

            oFReader = new FileReader(), rFilter = /^(?:text\/plain)$/i;

            oFReader.onload = function (oFREvent) {
                document.getElementById("preview").innerText = oFREvent.target.result;
                $('#dialog').dialog('open');
            };

        } else {
            alert('The File APIs are not fully supported in this browser.');
        }

        function upLoad(ele) {

            if (document.getElementById("tideFiles").files.length == 0) { alert("请选择文件"); }
            else
            {
                //alert(ele.type);
                ele.disabled = true;
                //document.getElementById("upBtn").value = "正在上传...";
                ele.textContent = "正在上传...";
                //alert(document.getElementById("tideFiles").files.length);
                document.getElementById("form1").submit();
            }

        }
        function upTxtFile() {
            if (document.getElementById("tideFiles").files.length == 0) { return; }
            var files = document.getElementById("tideFiles").files;
            document.getElementById("filesDiv").innerHTML = "";
            for (var i = 0; i < files.length; i++)
            {
                var file = files[i];
                var img = document.createElement("img");
                img.width = 64;
                img.height = 64;
                img.src = "Images/txt.png";
                img.title = file.name;
                img.alt = file.name;
                var pre = document.createElement("a");
                pre.addEventListener("click",(function (i) {
                    return function (e) {
                        if (document.getElementById("tideFiles").files[i]) {
                            if (SupportFileApis) {
                                oFReader.readAsText(document.getElementById("tideFiles").files[i]);
                                $('#dialog').dialog('setTitle', document.getElementById("tideFiles").files[i].name);
                            }
                            else {
                                alert("浏览器不支持文件接口，请升级浏览器版本");
                            }
                        }
                        e.preventDefault();
                    }               
                })(i), 'false');

                pre.href = "#";
                pre.innerText = "预览";

                var div = document.createElement("div");
                div.style = "display:inline-block;margin-right:10px";
                div.appendChild(img);
                div.appendChild(pre);
                filesDiv.appendChild(div);
            }

        }


</script>--%>
<%--</head>
<body>
    <table>
         <tr style="line-height: 45px">
            <td>第一次高潮</td>
            <td>
                <input id="01GC_H" type="text" />h</td>
            <td>
                <input id="01GC_MIN" type="text" />min</td>
            <td>
                <input id="01GC_CM" type="text" />cm</td>
        </tr>
        <tr style="line-height: 45px">
            <td>第一次低潮</td>
            <td>
                <input id="01DC_H" type="text" />h</td>
            <td>
                <input id="01DC_MIN" type="text" />min</td>
            <td>
                <input id="01DC_CM" type="text" />cm</td>
        </tr>
        <tr style="line-height: 45px">
            <td>第二次高潮</td>
            <td>
                <input id="02GC_H" type="text" />h</td>
            <td>
                <input id="02GC_MIN" type="text" />min</td>
            <td>
                <input id="02GC_CM" type="text" />cm</td>
        </tr>
        <tr style="line-height: 45px">
            <td>第二次低潮</td>
            <td>
                <input id="02DC_H" type="text" />h</td>
            <td>
                <input id="02DC_MIN" type="text" />min</td>
            <td>
                <input id="02DC_CM" type="text" />cm</td>
        </tr>
    </table>--%>



<%--    <div id ="filesDiv"></div>
    <a href="#" id="fileSelect" type="button" >选择</a>
    <form id="form1" method="post" action="Ajax/UpTideData.ashx"
         enctype="multipart/form-data">
        <input type="file" id="tideFiles" name="tideFiles" style="display:none"
             accept="text/plain,.txt" multiple="multiple" onchange="upTxtFile()"/>
        <button  type="button" onclick="upLoad(this)" >上传
        </button>
    </form>
    <div id="dialog" class="easyui-dialog" title="数据" style="width:1000px;height:800px;padding:10px">
        <pre  id="preview" >
        </pre>
    </div>




    <script type="text/javascript">
        $(function () {
            $('#dialog').dialog('close');
        });
        var fileSelect = document.getElementById("fileSelect");
        var fileElem = document.getElementById("tideFiles");
        
        fileSelect.addEventListener("click", function (e) {
            if (fileElem) {
                fileElem.click();
            }

            e.preventDefault(); // prevent navigation to "#"
        }, false);
    </script>--%>
<%--</body>
</html>--%>


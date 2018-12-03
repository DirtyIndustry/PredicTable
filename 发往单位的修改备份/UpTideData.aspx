<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpTideData.aspx.cs" Inherits="PredicTable.UpTideData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
          <link rel="stylesheet" href="css/style.default.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>


    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/Gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />



<%--    <script src="js/crossdomain.js"></script>--%>



    <script type="text/javascript">
        var SupportFileApis = false;
        // Check for the various File API support.
        if (window.File && window.FileReader && window.FileList && window.Blob) {
            // Great success! All the File APIs are supported.

            SupportFileApis = true;

        } else {

            //alert('The File APIs are not fully supported in this browser.');
        }

        xmlHttp=null;
        if (window.XMLHttpRequest)
        {// code for IE7, Firefox, Opera, etc.
            xmlHttp=new XMLHttpRequest();
        }
        else if (window.ActiveXObject)
        {// code for IE6, IE5
            xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
        }
        

        if (SupportFileApis)
        {
            oFReader = new FileReader();

            oFReader.onload = function (oFREvent) {
                document.getElementById("preview").innerText = oFREvent.target.result;
                $('#dialog').dialog('open');
            };

        }


        function uploadAndSubmit() {
            if (SupportFileApis && xmlHttp != null) {
                ReadAndUpFile(0);
            }
            else {
                form.action = "Ajax/UpTideData.ashx";
                form.submit();
            }
        }
        function ReadAndUpFile(index) {

            var form = document.forms["demoForm"];
            if (index < form["file"].files.length) {

                var file = form["file"].files[index];
                form["submit"].disabled = true;
                form["submit"].value = "上传中...";
                document.getElementById("procFileName").textContent = file.name;
                document.getElementById("bytesTotal").textContent = file.size;

                readFile(file,index);
            }
            else
            {
                if (index == 0) {
                    alert("请选择文件");
                }
                else {
                    alert("数据导入成功");
                    form["submit"].disabled = false;
                    form["submit"].value = "上传";
                }
            }
        }




        var fileName = "";
        var FileIndex = 0;
        var arrBufferCallback = function (e) {
            var binary = "";
            var bytes = new Uint8Array(e.target.result);
            var length = bytes.byteLength;
            for (var i = 0; i < length; i++) {
                binary += String.fromCharCode(bytes[i]);
                document.getElementById("bytesRead").textContent = i+1;
            }
            sendFile(binary, fileName, FileIndex);
        };



        function readFile(file, index) {

            document.getElementById("proMsgDiv").style.display = "block";
            var reader = new FileReader();

            reader.onloadstart = function () {
                // 这个事件在读取开始时触发
                //console.log("onloadstart");

            }
            reader.onprogress = function (p) {
                // 这个事件在读取进行中定时触发
                //console.log("onprogress");
                document.getElementById("bytesRead").textContent = p.loaded;
            }

            reader.onload = function () {
                // 这个事件在读取成功结束后触发
                //console.log("load complete");
                //alert("load");
            }

            reader.onloadend = function () {
                // 这个事件在读取结束后，无论成功或者失败都会触发
                if (reader.error) {
                    alert(reader.error);
                    //console.log(reader.error);
                } else {
                    document.getElementById("bytesRead").textContent = file.size;

                    sendFile(reader.result,file.name,index);
                }
            }

            if (typeof reader.readAsBinaryString != "undefined") {
                reader.readAsBinaryString(file);
            }
            else
            {
                reader.onloadend = arrBufferCallback;
                fileName = file.name;
                FileIndex = index;
                reader.readAsArrayBuffer(file);
            }

        }
        function sendFile(result,fileName,index) {
            var form = document.forms["demoForm"];
            //if (xmlHttp != null) {

            document.getElementById("bytesUp").textContent = "0%";

            // 构造 XMLHttpRequest 对象，发送文件 Binary 数据
            var xhr = new XMLHttpRequest();
            var upload = xhr.upload;
            // 设置上传文件相关的事件处理函数
            upload.addEventListener("progress", uploadProgress, false);
            upload.addEventListener("load", uploadSucceed, false);
            upload.addEventListener("error", uploadError, false);
            
            xhr.open("POST", "Ajax/UpTideDataAsync.ashx?fileName=" + fileName);
            xhr.overrideMimeType("application/octet-stream");
            //xhr.sendAsBinary(reader.result);
            xhr.send(result);

            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4) {
                    if (xhr.status == 200) {
                        //alert(xhr.responseText);
                        index++;
                        ReadAndUpFile(index);
                        show(fileName + "导入成功");
                        //console.log("upload complete");
                        //console.log("response: " + xhr.responseText);
                    }
                    else {
                        show(fileName + "导入失败");
                        alert("网络出现故障，请稍后再操作");
                        form["submit"].disabled = false;
                        form["submit"].value = "上传";
                        document.getElementById("bytesUp").textContent = "0%";
                    }
                }
            }

            function uploadProgress(event) {
                if (event.lengthComputable) {
                    // 将进度换算成百分比
                    var percentage = Math.round((event.loaded * 100) / event.total);
                    //console.log("percentage:" + percentage);
                    if (percentage < 100) {
                        document.getElementById("bytesUp").textContent = percentage + "%";
                    }
                }
            }
            function uploadSucceed(event) {
                document.getElementById("bytesUp").textContent = "100%";

            }
            function uploadError(error) {
                //alert("错误: " + error);
                document.getElementById("bytesUp").textContent = "0%";
            }
        }


        function upTxtFile() {
            if (document.getElementById("file").files.length == 0) { return; }
            var files = document.getElementById("file").files;
            document.getElementById("filesDiv").innerHTML = "";
            for (var i = 0; i < files.length; i++) {
                var file = files[i];

                var title = document.createElement("h3");
                title.innerText = file.name;
                var img = document.createElement("img");
                img.width = 64;
                img.height = 64;
                img.src = "Images/txt.png";
                img.title = file.name;
                img.alt = file.name;
                var pre = document.createElement("a");
                pre.addEventListener("click", (function (i) {
                    return function (e) {
                        if (document.getElementById("file").files[i]) {
                            if (SupportFileApis) {
                                oFReader.readAsText(document.getElementById("file").files[i]);
                                $('#dialog').dialog('setTitle', document.getElementById("file").files[i].name);
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
                pre.style = "color:orangered";
                var div = document.createElement("div");
                div.style = "display:inline-block;margin-right:10px";
                div.appendChild(title);
                div.appendChild(img);
                div.appendChild(pre);
                filesDiv.appendChild(div);
            }
        }

        function show(fileUpMsg){
              $.messager.show({
                        title:'提示',
                        msg: fileUpMsg,
                        showType:'show'
                        });
        }

</script>

</head>
<body style="margin-left:20px">
<h3 style="margin-top:20px;margin-bottom:50px;">导入潮汐数据</h3> 

<!-- 用于文件上传的表单元素 --> 
<div id ="filesDiv"></div>
<a href="#" id="fileSelect" type="button" style="color:orange;font-size:medium" >选择文件</a>

<form name="demoForm" id="demoForm" method="post" enctype="multipart/form-data" 
      action="javascript: uploadAndSubmit();" style="margin-top:20px"> 
    <input type="file" id="file" name="file" style="display:none" multiple="multiple"
                accept="text/plain,.txt" onchange="upTxtFile()"/>
    <p><input name="submit" type="submit" value="上传" /></p> 
</form>

<div id="proMsgDiv" style="margin-top:20px;display:none">
        <div><h5>上传文件:</h5>
            <span id="procFileName"></span> 
        </div> 
        <div style="display:inline-block"><h5>文件读取(字节):</h5>
            <span id="bytesRead"></span> / <span id="bytesTotal"></span> 
        </div> 
        <div><h5>上传进度 : </h5>
            <span id="bytesUp"></span>
        </div> 
</div>
<div id="dialog" class="easyui-dialog" title="数据" 
    style="width:1000px;height:600px;padding:10px">
    <pre  id="preview" >
    </pre>
</div>


<script type="text/javascript">
    $(function () {
        $('#dialog').dialog('close');
    });
    var fileSelect = document.getElementById("fileSelect");
    var fileElem = document.getElementById("file");
        
    fileSelect.addEventListener("click", function (e) {
        if (fileElem) {
            fileElem.click();
        }
        e.preventDefault(); // prevent navigation to "#"
        }, false);
</script>
</body>
</html>

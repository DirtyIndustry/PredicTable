<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iframUpLoad.aspx.cs" Inherits="PredicTable.iframUpLoad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
</head>
<body style="margin: 0px; padding: 0px; text-align: center">
    <form id="form1" runat="server">
    <div style="float: left">
            <asp:FileUpload ID="FileUploadWord" runat="server"></asp:FileUpload>
            <asp:Button ID="BtnUpLoad" runat="server" Text="上传" OnClick="BtnUpLoad_Click"/>
            <asp:TextBox ID="txtMessage" Width="300px" runat="server" BorderStyle="None"></asp:TextBox>
            <input runat="server" id="newname" name="newname" size="50" type="hidden" />
            <input runat="server" id="type" name="type" size="50" type="hidden" value="CN" />
    </div>
        <script type="text/javascript">
            $(function () {
                var newName = "";
                //文档类型
                var wordType = "CN";
                //海区 （SD：山东近海；NCS:北海区）
                var oceanarea = "";

                //******************select选择事件******************
               
                parent.$("#12").change(function () {
                    nameChange();
                });
                parent.$("#11").change(function () {
                    nameChange();
                });
                
                parent.$("#13").change(function () {
                    var str_key, str_val;
                    switch (parent.$("#13").val()) {
                        case "HJ": str_key = "1旬,1月"; str_val = "10day,1mon";
                            break;
                        case "ZH": str_key = "1年"; str_val = "1yr";
                            break;
                        default:
                    }
                    var data = { key: str_key, value: str_val };
                    parent.$("#12").empty();//先清空再绑定
                    var text = data.key.split(',');
                    var value = data.value.split(',');
                    var optionstr = "";
                    parent.$("#uniform-12 span").html(text[0]);//更改选中值 ;
                    for (var i = 0; i < text.length; i++) {
                        optionstr += "<option value='" + value[i] + "'>" + text[i] + "</option>"
                    }
                    parent.$("#12").append(optionstr);
                    nameChange();
                });

                // 选择时间事件 onSelect
                parent.$('#l6').datebox({
                    onSelect: function () {
                        nameChange();
                    }
                });

               
                //为word生成新名称
                function nameChange() {
                    wordType = parent.$("#hid_type").val();
                    newName = "";
                    if (wordType == 'EN') {
                        newName = "YB";
                        oceanarea = parent.$("#11").val();
                        if (oceanarea == 'NMFC') {
                            newName += "_NCS";
                        } else if (oceanarea == 'SDMF') {
                            newName += "_SD";
                        }
                        newName = changeEN();
                        //alert(newName);
                    } else if (wordType == 'CN') {
                        newName = changeCN();
                        //alert(newName);
                    }
                    $("#newname").val(newName);
                    $("#type").val(wordType);
                }

                //**********************生成英文文件名*************************
                function changeEN() {
                    newName += "_" + parent.$("#13").val();
                    newName += "_" + parent.$("#12").val();
                    newName += getTime();
                    newName += "_" + oceanarea;
                    return newName;
                }

                //获取时间
                function getTime() {
                    var content;
                    if (parent.$("#l6").datebox("getValue") != "") {
                        content = "_" + parent.$("#l6").datebox("getValue");
                    } else {
                        content = "_" + formatterDate(new Date());
                    }
                    return content;
                }
                //格式化时间
                formatterDate = function (date) {
                    var day = date.getDate() > 9 ? date.getDate() : "0" + date.getDate();
                    var month = (date.getMonth() + 1) > 9 ? (date.getMonth() + 1) : "0"
                    + (date.getMonth() + 1);
                    return date.getFullYear() + month + day;
                };
                //*************************生成中文文件名*************************
                function changeCN() {
                    newName = getTile();
                    newName += "-" + parent.$("#11").val();
                    return newName;
                }
                

                function getTile() {
                    var time,year,month,day,content;
                    if (parent.$("#l6").datebox("getValue") != "") {
                        time = parent.$("#l6").datebox("getValue");
                    } else {
                        time = formatterDate(new Date());
                    }
                    year = time.substring(0, 4);
                    month = parseInt(time.substring(4, 6));
                    day = parseInt(time.substring(6));
                    if (day <= (10 - 0)) {
                        content = year + "年" + month + "月及上旬预报";
                    } else if ((10 - 0) < day && day <= (20 - 0)) {
                        content = year + "年" + month + "月中旬预报";
                    } else if(day > (20 - 0)) {
                        content = year + "年" + month + "月下旬预报";
                    }
                    return content;
                }

                
                //初始化时生成文件名
                nameChange();

                //var type = parent.document.getElementById('type').value;
                $("#type").val(type);
            });
        </script>
    </form>
</body>
</html>

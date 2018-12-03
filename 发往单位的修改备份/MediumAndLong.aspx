<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MediumAndLong.aspx.cs" Inherits="PredicTable.MediumAndLong" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228" Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>中长期预报表单</title>
    <link rel="stylesheet" href="css/style.default.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>

    <script type="text/javascript" src="js/plugins/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.cookie.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.validate.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.tagsinput.min.js"></script>
    <script type="text/javascript" src="js/plugins/charCount.js"></script>
    <script type="text/javascript" src="js/plugins/ui.spinner.min.js"></script>
    <script type="text/javascript" src="js/plugins/chosen.jquery.min.js"></script>

    <script type="text/javascript" src="js/custom/forms.js"></script>
    <script type="text/javascript" src="js/custom/widgets.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="js/custom/general.js"></script>
    <script type="text/javascript" src="js/custom/tables.js"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/Gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />
    
    <script type="text/javascript">
        //$(function () {
        //    getuserinfo("-1");
        //});
        $(function () {
            $("#l1").change(function () {
                //判断文件类型
                var str = $(this).find("option:selected").attr("title");
                $("#type").val(str);
                //判断时效类型
                getShiXiao();
                //生成新的文件名
                getNewName();
                //获取模板
                getWordModel(str);
            });
            function getWordModel(str) {
                $.ajax({
                    type: "GET",
                    url: "/Ajax/GetWordModel.ashx?method=getModel&type=" + str,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null) {
                            $("#list").empty();
                            var option = "";
                            var first_txt = "";
                            //var result = eval(data);
                            //console.log(result);
                            
                            for (var i = 0; i < data.list.length; i++) {
                                option += "<option value=" + data.list[i].oldname + ">" + data.list[i].newname + "</option>";
                                first_txt = data.list[0].newname;
                            }
                            $("#uniform-list span").html(first_txt);//更改选中值 ;
                            $("#list").append(option);
                            $("#hidd_model").val(first_txt);
                        } else {
                            alert("下拉框数据获取失败！");
                        }
                    }

                });
            }
            $("#list").change(function () {
                $("#hidd_model").val($("#list").find("option:selected").text());
            });
            
            $("#l2").change(function () {
                getNewName();
            });
            // 选择时间事件 onSelect
            parent.$('#l6').datebox({
                onSelect: function () {
                    getNewName();
                }
            });
            getNewName();
        });
    </script>

    <script type="text/javascript">
                    //生成新文件名
                    function getNewName() {
                        var newname = "";
                        var lan_type = $("#type").val();
                        if (lan_type == "CN") {
                            newname = getTile();
                            newname += "-" + $("#l1").val();
                        }
                        else if (lan_type == "EN") {
                            newname = "YB";
                            if ($("#l1").val() == "NMFC") {
                                newname += "_NCS";
                            } else if ($("#l1").val()=="SDMF") {
                                newname += "_SD";
                            }
                            newname += "_" + $("#l3").val();
                            newname += "_" + $("#l2").val();
                            newname += getTime();
                            newname += "_" + $("#l1").val();
                        }
                        $("#newname").val(newname);
                    }

                    //*********************获取中文文件名*********************
                    function getTile() {
                        var time, year, month, day, content;
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
                        } else if (day > (20 - 0)) {
                            content = year + "年" + month + "月下旬预报";
                        }
                        return content;
                    }
                    //通过预报类型的修改，修改时效类型
                    $(function () {
                        $("#l3").change(function () {
                            //var obj = $("#l1").find("option:selected").attr("title");
                            getShiXiao();
                            getNewName();
                        });
                    });

                    function getShiXiao() {
                        var value_type = $("#type").val();
                        if (value_type == "CN") {
                            str_key = "1旬,1月,1年";
                            str_val = "10day,1mon,1yr";
                        } else if (value_type == "EN") {
                            switch ($("#l3").val()) {
                                case "HJ": str_key = "1旬,1月"; str_val = "10day,1mon";
                                    break;
                                case "ZH": str_key = "1年"; str_val = "1yr";
                                    break;
                                default:
                                    break;
                            }
                        }
                        var data = { key: str_key, value: str_val };
                        parent.$("#l2").empty();//先清空再绑定
                        var text = data.key.split(',');
                        var value = data.value.split(',');
                        var optionstr = "";
                        parent.$("#uniform-l2 span").html(text[0]);//更改选中值 ;
                        for (var i = 0; i < text.length; i++) {
                            optionstr += "<option value='" + value[i] + "'>" + text[i] + "</option>"
                        }
                        parent.$("#l2").append(optionstr);
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
                </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodywrapper">
        <div id="contentwrapper" class="contentwrapper">
            <div class="contenttitle2">
                <h3 id="tx1">中长期预报单</h3>
            </div>
            <div class="selects" style="z-index: 9999;">
               预警报发布单位：
                <select id="l1" class="uniformselect">
                    <option value="东营环境预报" title="CN">东营环境预报</option>
                    <option value="南堡油田" title="CN">南堡油田</option>
                    <option value="胜利油田" title="CN">胜利油田</option>
                    <option value="NMFC" title="EN">国家海洋局北海预报中心</option>
                    <option value="SDMF" title="EN">山东省海洋预报台</option>
               </select>&nbsp;&nbsp;
                <input runat="server" id="hid" name="hidd_fbdw" size="33" type="hidden" />
                
                预警报内容：
                <select id="l3" class="uniformselect">
                        <option value="HJ">环境预报</option>
                        <option value="ZH">海洋灾害趋势</option>
                    </select>
                    &nbsp;&nbsp;
                预警报时效：
                <select id="l2" class="uniformselect">
                    <option value="10day">1旬</option>
                    <option value="1mon">1月</option>
                    <option value="1yr">1年</option>
               </select>&nbsp;&nbsp;
                
                发布时间：
                <input id="l6" name="16" style="z-index: 1000; position: absolute" class="easyui-datebox" editable="false" data-options="formatter:myformatter,parser:myparser"></input>
                <script type="text/javascript">
                        function myformatter(date) {
                            var y = date.getFullYear();
                            var m = date.getMonth() + 1;
                            var d = date.getDate();
                            return y + (m < 10 ? ('0' + m) : m) + (d < 10 ? ('0' + d) : d);
                        }

                        function myformatter1(date) {
                            var y = date.getFullYear();
                            var m = date.getMonth() + 1;
                            var d = date.getDate();
                            return y + '年' + (m < 10 ? ('0' + m) : m) + '月' + (d < 10 ? ('0' + d) : d) + '日';
                        }

                        function myparser(s) {
                            if (!s) return new Date();
                            //var ss = (s.split('-'));
                            //var y = parseInt(ss[0], 10);
                            //var m = parseInt(ss[1], 10);
                            //var d = parseInt(ss[2], 10);

                            var y = parseInt(s.substring(0, 4), 10);
                            var m = parseInt(s.substring(4, 6), 10);
                            var d = parseInt(s.substring(6, 8), 10);
                            if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                                return new Date(y, m - 1, d);
                            } else {
                                return new Date();
                            }
                        }
                    </script>
                <%--&nbsp;&nbsp;--%>
               <BR />
                <input runat="server" id="type" name="type" size="33" type="hidden" value="CN" />
                <input runat="server" id="newname" name="newname" size="33" type="hidden" />
                <input runat="server" id="uploadname" name="uploadname" size="33" type="hidden" />
                模板选择：<select runat="server" id="list" class="uniformselect"></select>
                <asp:HiddenField ID="hidd_model" runat="server" Value="" />
                <input runat="server" type="checkbox" id="chk_checkModel" name="chk_checkModel" value="checkbox" />
                使用模板 
                <asp:Button runat="server" ID="startoperation" Text="开始编写文档"  OnClick="startoperation_Click"/>
            </div>
            <div class="contenttitle2" style="z-index: 1000;">
                    <h3 id="mbcz">编写预报文档（ctrl+s保存）</h3>

                </div>
               <%--  <asp:Button ID="btnDq" runat="server" onclick="btnDq_Click" Text="读取" />
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
            <div runat="server" id="Div1" style="height: 900px">
                    <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" Menubar="False" Titlebar="False"></po:PageOfficeCtrl>
                </div>
        </div>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NineteenthTable.aspx.cs" Inherits="PredicTable.NineteenthTable" %>
<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228" Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>19号预报单</title>
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
    <script type="text/javascript"  src="js/My97DatePicker/WdatePicker.js"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/Gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />
<!--[if IE 9]>
    <link rel="stylesheet" media="screen" href="css/style.ie9.css"/>
<![endif]-->
    <!--[if IE 8]>
    <link rel="stylesheet" media="screen" href="css/style.ie8.css"/>
<![endif]-->
    <!--[if lt IE 9]>
	<script src="js/plugins/css3-mediaqueries.js"></script>
<![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="bodywrapper">
            <div id="contentwrapper" class="contentwrapper">
                <div class="contenttitle2">
                    <h3 id="tx1">19号预报单</h3>
                </div>
                <div class="selects">
                    <div>
                         发布日期：<input id="pubDate" name="pubDate" type="text"
                         onclick="WdatePicker()" runat="server" />
                    &nbsp;&nbsp;
                    模板选择：<select runat="server" id="list" class="uniformselect">
                                <option value="19号预报单--周.doc">19号预报单--周.doc</option>
                                <option value="19号预报单--旬.doc">19号预报单--旬.doc</option>
                                <option value="19号预报单--月.doc">19号预报单--月.doc</option>
                                <option value="19号预报单--年.doc">19号预报单--年.doc</option>
                         </select>
                        <asp:Button runat="server" ID="startoperation" Text="开始编写文档" OnClick="startoperation_Click" />
                    </div>
                    <div id="unit" style="margin-top:15px;  ">
                        
                        <div id="unitcontent">

                        </div>
                        <input type="hidden" id="hidUnitName" value="" runat="server" />
                        
                    </div>
                </div>
                <div class="contenttitle2">
                    <h3 id="mbcz">编写预报文档（ctrl+s保存）</h3>
                </div>
                
                <div style="height:900px; margin-top:10px;z-index:0 !important;">
                        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" Menubar="False" Titlebar="False"></po:PageOfficeCtrl>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function myformatter(date) {
                var y = date.getFullYear();
                var m = date.getMonth() + 1;
                var d = date.getDate();
                return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
            }

            $("#pubDate").val(myformatter(new Date()));
        </script>
        <%--获取发送单位分组及发送单位信息--%>
        <script>
            $(function () {
                $.ajax({
                    type: "GET",
                    url: '/Ajax/OprationSendUnit.ashx?method=getGroupAndUnit',

                    success: function (result) {
                        if (result != null && result.length>0) {
                            var unitHtml = "<div style='float:left;'>选择发送单位：</div><div style='float:left;'>";
                            for (var i = 0; i < result.length; i++) {
                                unitHtml += "<span style='margin-left:40px;'><input type='checkbox' value='" + result[i]['GROUPNAME'] + "' id='" + result[i]['ID'] + "' attrValue='" + result[i]['UNITNAME'] + "' onclick='setCheck(this)'><span style='margin-left:5px;'>" + result[i]['GROUPNAME'] + "</span></input></span>";
                            }
                            unitHtml += "</div><div style='clear:both;'></div>";
                            $("#unitcontent").html(unitHtml);
                        } else {
                            $("#unit").empty();
                        }
                    }
                });
            });
            //
            function setCheck(obj) {
                var unitName = "";
                $("#unitcontent input:checkbox").each(function () {
                    if ($(this).prop("checked")) {
                        unitName += $(this).attr("attrValue")+",";
                    }
                });
                unitName = unitName.substring(0, unitName.length-1);
                $("#hidUnitName").val(unitName);
            }
        </script>
    </form>
</body>
</html>

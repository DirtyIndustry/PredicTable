<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMediumAndLong.aspx.cs" Inherits="PredicTable.NewMediumAndLong" %>

<%@ Register Src="~/Ascx/NCSModel.ascx" TagPrefix="uc1" TagName="NCSModel" %>
<%@ Register Src="~/Ascx/SDModel.ascx" TagPrefix="uc1" TagName="SDModel" %>

<%-- 页面整合到一起的情况 --%>
<%--<%@ Register Src="~/Ascx/MonthAndXunModel.ascx" TagPrefix="uc1" TagName="MonthAndXunModel" %>--%>


<%@ Register Src="~/Ascx/MonthModel.ascx" TagPrefix="uc1" TagName="MonthModel" %>
<%@ Register Src="~/Ascx/XunModel.ascx" TagPrefix="uc1" TagName="XunModel" %>





<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>中长期预报单制作</title>
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
    
    <%--页面事件--%>
    <script type="text/javascript" src="/Scripts/NewMediumAndLong/AspxEvent.js"></script>
    <script src="Scripts/NewMediumAndLong/GetMediumAndLongData.js"></script>
    <script src="Scripts/NewMediumAndLong/SetMediumAndLongData.js"></script>
    <%--用户控件样式--%>
    <link href="Scripts/NewMediumAndLong/AscxStyle.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <div class="bodywrapper">
        <div id="contentwrapper" class="contentwrapper">
            <div class="contenttitle2">
                <h3 id="tx1">中长期预报单</h3>
            </div>
            <div class="selects" style="z-index: 9999;">
                <script type="text/javascript">
                        function myformatter(date) {
                            var y = date.getFullYear().toString();
                            var m = (date.getMonth() + 1).toString();
                            var d = date.getDate().toString();
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
                <input runat="server" id="type" name="type" size="33" type="hidden" value="CN" />
                <input runat="server" id="newname" name="newname" size="33" type="hidden" />
                <input runat="server" id="uploadname" name="uploadname" size="33" type="hidden" />
                模板选择：
                <select runat="server" id="list" class="uniformselect">
                    <option value="NCS">国家海洋环境年预报</option>
                    <option value="SD">山东海洋环境年预报</option>
                    <%--<option value="MonthAndXun">月、旬预报</option>--%>
                    <option value="Month">月预报</option>
                    <option value="Days">旬预报</option>
                </select>
                发布时间：
                <input id="publishTime" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser"></input>
                <%--使用模板--%> 
                <input type="button" id="startoperation" value="开始编写文档" />
            </div>
            <div id="div1" style="margin-top:20px;display:none;" >
                <uc1:NCSModel runat="server" ID="NCSModel" />
            </div>
            <%--国家海洋环境年预报--%>
            <div id="div2" style="margin-top:20px;display:none;">
                <uc1:SDModel runat="server" id="SDModel" />
            </div>
            <%--山东海洋环境年预报--%>
            <div id="div3" style="margin-top:20px;display:none;">
                <uc1:monthmodel runat="server" id="monthmodel" />
            </div>
            <%--月预报--%>
            <div id="div4" style="margin-top:20px;display:none;">
                <uc1:XunModel runat="server" ID="XunModel" />
            </div>
            <%--旬预报--%>


            <%-- 月、旬预报 新添加的 by Durriya start--%>
            <%--<div id="div5" style="margin-top:20px;display:none;">
                <uc1:MonthAndXunModel runat="server" id="MonthAndXunModel" />
            </div>--%>
            <%-- 月、旬预报 新添加的 by Durriya end--%>
        </div>
    </div>
    </form>
</body>
</html>

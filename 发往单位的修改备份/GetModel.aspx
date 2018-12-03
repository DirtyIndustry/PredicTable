<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetModel.aspx.cs" Inherits="PredicTable.GetModel" %>
<%@ Register Src="~/Ascx/CNModel.ascx" TagPrefix="uc2" TagName="CNModel" %>
<%@ Register Src="~/Ascx/ENModelDay.ascx" TagPrefix="uc2" TagName="ENModelDay" %>
<%@ Register Src="~/Ascx/ENModelYear.ascx" TagPrefix="uc2" TagName="ENModelYear" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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


   <%-- <script src="js/crossdomain.js"></script>--%>




    <style type="text/css">
        #SaveContent{
            background:#FB9337;
            border: 1px solid #F0882C; 
            color:#eee;
            padding:7px 10px;
        }
    </style>
    <script>

    



        $(function () {
            $("#list").change(function () {
                $("#hidd_model").val($("#list").find("option:selected").text());
            });
            // 选择时间事件 onSelect
            $('#l6').datebox({
                onSelect: function () {
                    $("#hid_field_time").val($("#l6").datebox("getValue"));
                }
            });
        });
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
                模板选择：<select runat="server" id="list" class="uniformselect"></select>
                <asp:HiddenField ID="hidd_model" runat="server" Value="" />
                发布时间：
                <input id="l6" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser"></input>
                <script>
                    $(function () {
                        var t = myformatter(new Date());
                        $("#l6").datebox("setValue", t);
                    });
                </script>
                <asp:HiddenField ID="hid_field_time" runat="server" Value="" />
                <%--使用模板--%> 
                <asp:Button runat="server" ID="startoperation" Text="开始编写文档" OnClick="startoperation_Click"/>
            </div>

            <div class="contenttitle2" style="z-index: 1000;">
                    <h3 id="mbcz">编写预报文档</h3>
            </div>
                
            <div runat="server" id="Div1" style="height: 1000px">

            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $(function () {
            var pbtime;
            if ($("#l6").datebox("getValue") != "") {
                pbtime = $("#l6").datebox("getValue");
            } else {
                pbtime = formatterDate(new Date());
            }
            $("#hid_field_time").val(pbtime);
        });
    </script>
</body>
</html>

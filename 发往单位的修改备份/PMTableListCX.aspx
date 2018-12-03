<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PMTableListCX.aspx.cs" Inherits="PredicTable.PMTableListCX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预报表单列表</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
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

    <!--[if IE 9]>
    <link rel="stylesheet" media="screen" href="css/style.ie9.css"/>
<![endif]-->
    <!--[if IE 8]>
    <link rel="stylesheet" media="screen" href="css/style.ie8.css"/>
<![endif]-->
    <!--[if lt IE 9]>
	<script src="js/plugins/css3-mediaqueries.js"></script>
<![endif]-->
    <style>
        #leixing1 {
            float: left;
            clear: both;
        }

            #leixing1 li {
                list-style-type: none;
                float: left;
                border: 5px solid #ccc7c7;
                padding: 3px;
                margin: 3px;
                cursor: pointer;
                /*background-color: #ff6a00;*/
                background-color: #ffffff;
            }

                #leixing1 li:hover {
                    border: 5px solid #FB9337;
                }

                #leixing1 li:visited {
                    border: 5px solid #FB9338;
                }

        .dlgs {
            margin: 10px 0px 10px 0px;
            clear: both;
        }

            .dlgs input[type=text] {
                width: 80%;
            }

        input[type='text'] {
            font-size: 18px;
            font-weight: 500;
        }

        #addtable td {
            text-align: center;
        }


        /*#dlg_23 input {
        font-size:18px;
        }*/
        #uniform-Hailang {
            font-size: 18px;
        }

        #uniform-Chaoxi {
            font-size: 18px;
        }

        #uniform-Shuiwen {
            font-size: 18px;
        }
    </style>
</head>
<body>
    <script>
        var cx_arry = new Array(75, 18, 69, 76, 82, 63, 83, 84, 77, 48, 78, 79, 10, 80, 42, 81, 12);
        var sw_arry = new Array(1, 3, 6, 11, 17, 19, 21, 24, 49, 56, 59, 62, 65, 68, 71, 74, 53);
        var fl_arry = new Array(1, 3, 5, 6, 8, 9, 11, 13, 14, 15, 17, 19, 21, 26, 35, 41, 43, 47, 55, 58, 61, 64, 67, 70, 73, 53);
        var type = "<%=Session["type"]%>";
        // var type = "<%=type%>";
        var makewordtime = "pm";
        //var type = "cx";
        function quanxian(type, date) {
            if (getdatenow() == date) {
                switch (type) { //all_hide(); show_bytype(cx_arry);
                    case "cx": all_disabled(); cx_isabled(); tb_isabled(); $("#yby_type").val("潮汐"); break;//潮汐能填写
                    case "fl": all_isabled(); cx_disabled(); sw_disabled(); tb_isabled(); $("#yby_type").val("风、海浪"); break;//风浪能填写
                    case "sw": all_disabled(); sw_isabled(); tb_isabled(); $("#yby_type").val("水温"); break;//水温能填写

                    default: all_disabled(); $("#yby_type").val("无"); break;// 都不能填写
                }
            } else {//不是当天不能编辑
                switch (type) {
                    case "cx": all_disabled(); $("#yby_type").val("潮汐"); break;//都不能填写
                    case "fl": all_disabled(); tb_isabled(); $("#yby_type").val("风、海浪"); break;//风浪能填写
                    case "sw": all_disabled(); $("#yby_type").val("水温"); break;//都不能填写
                    default: all_disabled(); $("#yby_type").val("无"); break;// 都不能填写
                }
            }
        }

        //获取当前日期 格式 yyyy-mm-dd
        function getdatenow() {
            var myDate = new Date();
            var date = myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate();
            return date;
        }
        $(function () {

            //getuserinfo(type);
            quanxian(type, getdatenow());
            setVisiable();
            //$("#btn_show").click(function () {

            //    if ($("#btn_show").val() == "显示所有") {
            //        all_show();
            //        $("#btn_show").val("显示可编辑");
            //    } else if ($("#btn_show").val() == "显示可编辑") {
            //        switch (type) {
            //            case "cx": all_hide(); show_bytype(cx_arry); break;//潮汐能填写
            //            case "fl": all_hide(); show_bytype(fl_arry); break;//风浪能填写
            //            case "sw": all_hide(); show_bytype(sw_arry); break;//水温能填写
            //            default: break;// 都不能填写 
            //        }
            //        $("#btn_show").val("显示所有");
            //    }
            //});

            function setVisiable() {
                switch (type) {
                    case "cx": all_hide(); show_bytype(cx_arry); break;//潮汐能填写
                    case "fl": all_hide(); show_bytype(fl_arry); break;//风浪能填写
                    case "sw": all_hide(); show_bytype(sw_arry); break;//水温能填写
                    default: break;// 都不能填写
                }
            }
            ////  var topWin = window.top.document.getElementById("username").contentWindow;
        });


        //显示所有
        function all_show() {
            var str = "";
            for (var i = 1; i <= 100; i++) {
                if (i < 10) {
                    str = "#ddlg_0" + i;
                } else {
                    str = "#ddlg_" + i;
                }
                $(str).css("display", "");
                $("#lx_" + i).css("display", "");
            }
            if (type == "sw") {
                var fltable = document.getElementById("fltable");
                $(fltable).css("display", "none");

            }
            else {
                var swtable = document.getElementById("swtable");
                $(swtable).css("display", "none");
                var fltable = document.getElementById("fltable");
                $(fltable).css("display", "");

            }
        }
        //隐藏所有
        function all_hide() {
            var str = "";
            for (var i = 1; i <= 100; i++) {
                if (i < 10) {
                    str = "#ddlg_0" + i;
                } else {
                    str = "#ddlg_" + i;
                }
                $(str).css("display", "none");
                $("#lx_" + i).css("display", "none");
            }
            if (type == "sw") {
                var fltable = document.getElementById("fltable");
                $(fltable).css("display", "none");

            }
            else if (type == "fl") {
                var swtable = document.getElementById("swtable");
                $(swtable).css("display", "none");

            }
            else {
                var swtable = document.getElementById("swtable");
                $(swtable).css("display", "none");
                var fltable = document.getElementById("fltable");
                $(fltable).css("display", "none");
            }

        }
        //根据id显示表单 
        function show_bytype(array) {
            // $("#yby_type").val("潮汐");
            //$('#dlg').dialog('close'); //dlg关闭
            //  var cx_arry = new Array(2, 4, 7, 10, 12, 16, 18, 20, 22);
            var str = "";
            for (var i = 0; i < array.length; i++) {
                if (array[i] < 10) {
                    str = "#ddlg_0" + array[i];
                } else {
                    str = "#ddlg_" + array[i];
                }
                $(str).css("display", "");
                $("#lx_" + array[i]).css("display", "");
            }
        }

        //滑动到指定位置
        function click_scroll(id) {
            var scroll_offset = $("#" + id).offset();  //得到pos这个div层的offset，包含两个值，top和left
            $("body,html").animate({
                scrollTop: (scroll_offset.top - 200)  //让body的scrollTop等于pos的top，就实现了滚动
            }, 1000);
        }

    </script>

    <form id="form2" runat="server">
        <div>
            <div id="contentwrapper" class="contentwrapper">
                <%-- 表单类型 start--%>
                <div>
                    <div style="position: fixed; top: 0px; left: 20px; z-index: 2; display: none">
                        <ul id="leixing1">
                            <li id="xwbd" style="clear: both; border-color: #fb9337; background-color: #fb9337; color: white; font-weight: bold">下午预报表单潮汐预报</li>
                            <li id="lx_75" onclick="click_scroll('ddlg_75')">一、青岛（五号码头）</li>
                            <li id="lx_18" onclick="click_scroll('ddlg_18')">二、青岛（小麦岛）</li>
                            <li id="lx_69" onclick="click_scroll('ddlg_69')">三、青岛（董家口）</li>
                            <li id="lx_76" onclick="click_scroll('ddlg_76')">四、日照</li>
                            <li id="lx_82" onclick="click_scroll('ddlg_82')">五、乳山</li>
                            <li id="lx_63" onclick="click_scroll('ddlg_63')">六、文登</li>
                            <li id="lx_83" onclick="click_scroll('ddlg_83')">七、石岛</li>
                            <li id="lx_84" onclick="click_scroll('ddlg_84')">八、成山头</li>
                            <li id="lx_77" onclick="click_scroll('ddlg_77')">九、威海</li>
                            <li id="lx_48" onclick="click_scroll('ddlg_48')">十、海洋牧场</li>
                            <li id="lx_78" onclick="click_scroll('ddlg_78')">十一、烟台</li>
                            <li id="lx_79" onclick="click_scroll('ddlg_79')">十二、潍坊</li>
                            <li id="lx_10" onclick="click_scroll('ddlg_10')">十三、小岛河</li>
                            <li id="lx_80" onclick="click_scroll('ddlg_80')">十四、黄河海港</li>
                            <li id="lx_42" onclick="click_scroll('ddlg_42')">十五、东营港</li>
                            <li id="lx_81" onclick="click_scroll('ddlg_81')">十六、滨州港</li>
                            <li id="lx_12" onclick="click_scroll('ddlg_12')">十七、曹妃甸</li>
                        </ul>
                    </div>
                </div>
                <%-- 表单类型 end--%>

                <%-- 表单信息 start --%>
                <div style="margin-top: 0px">
                    <div class="contenttitle2">
                        <h3 id="tx1">表单信息</h3>
                    </div>
                    <br />
                    <div style="font-size: 15px; font-weight: bold">
                        预报员类型：<select id="yby_type" name="select" disabled="disabled" class="uniformselect">
                            <option>潮汐</option>
                            <option>风、海浪</option>
                            <option>水温</option>
                            <option>无</option>
                        </select>&nbsp; 填报日期：<input id="tianbaoriqi" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser"></input>
                        <script type="text/javascript">
                            function myformatter(date) {
                                var y = date.getFullYear();
                                var m = date.getMonth() + 1;
                                var d = date.getDate();
                                return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
                            }

                            function myformatter1(date) {
                                var y = date.getFullYear();
                                var m = date.getMonth() + 1;
                                var d = date.getDate();
                                return y + '年' + (m < 10 ? ('0' + m) : m) + '月' + (d < 10 ? ('0' + d) : d) + '日';
                            }

                            function myparser(s) {
                                if (!s) return new Date();
                                var ss = (s.split('-'));
                                var y = parseInt(ss[0], 10);
                                var m = parseInt(ss[1], 10);
                                var d = parseInt(ss[2], 10);
                                if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                                    return new Date(y, m - 1, d);
                                } else {
                                    return new Date();
                                }
                            }
                        </script>
                        &nbsp; 时间：                           
                        <select id="select_hour" name="select" class="uniformselect" style="height: 21px;">
                        </select>
                        时&nbsp; 
                        <input type="button" id="btn_select" class="stdbtn" value="查询" />
                        <input type="button" id="btn_czrz" onclick="$('#dlg_czrz').dialog('open')" class="stdbtn" value="操作日志" />
                        <%--<input type="button" id="btn_show" class="stdbtn" value="显示所有" />--%>
                        <%--<input type="button" id="checkmodel" onclick="$('#dlg_xzmb').dialog('open'); click_scroll('dlg_xzmb') " class="stdbtn" value="选择模版并发布" />--%>
                        <input type="button" id="setall" onclick="alldlg_Submit()" class="stdbtn" value="保存所有" />
                        <input type="button" id="btnrole" class="stdbtn" value="验证所有" />
                        <%--<input type="button" id="ReleasetableAll" onclick="All_Releasetable()" class="stdbtn" value="发布下午表单" />--%>
                        <br />
                    </div>
                </div>
                <%-- 表单信息 end --%>

                <%-- 表单信息 start --%>

                <!--表单01. 下午一  青岛（五号码头）  start-->
                <div class="dlgs" id="ddlg_75" style="width: auto; height: 350px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="16">下午一、青岛（五号码头）</th>
                            </tr>
                            <tr style="text-align: center">
                                <%--<th class="head0" rowspan="3">地市</th>--%>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="QD_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="QD_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="QD_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="QD_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="QD_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="QD_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="QD_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="QD_02D_CG_001" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="QD_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="QD_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="QD_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="QD_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="QD_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="QD_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="QD_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="QD_02D_CG_002" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="QD_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="QD_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="QD_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="QD_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="QD_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="QD_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="QD_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="QD_02D_CG_003" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(75)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(75)" value="提交" />
                </div>
                <!--表单01. 下午一  青岛（五号码头）  end-->

                <!--表单02. 下午二  青岛（小麦岛）下午12的不发生变化 start-->
                <div class="dlgs" id="ddlg_18" style="width: auto; height: 350px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="16">下午二、青岛（小麦岛）</th>
                            </tr>
                            <tr style="text-align: center">
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <%--<td style="width: 4%;" rowspan="3">青岛市区海水浴场</td>--%>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="XMD_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="XMD_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_CG_001" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="XMD_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="XMD_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_CG_002" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="XMD_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="XMD_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_CG_003" type="text" /></td>
                            </tr>

                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(18)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(18)" value="提交" />
                </div>

                <!--表单02. 下午二  青岛（小麦岛）  end-->

                <%-- 表单03 下午三青岛（董家口）start --%>
                <div class="dlgs" id="ddlg_69" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="9">下午三、青岛（董家口）</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>
                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="DJKP_01G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_01G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CG_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_02D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_02D_CG_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="DJKP_01G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_01G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CG_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_02D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_02D_CG_02" type="text" /></td>
                            </tr>

                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="DJKP_01G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_01G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CG_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_02D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_02D_CG_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(69)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(69)" value="提交" />
                </div>
                <%-- 表单03 下午三青岛（董家口）end --%>

                <%-- 表单04 下午四 日照 start --%>
                <div class="dlgs" id="ddlg_76" style="width: auto; height: 350px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="16">下午四、日照</th>
                            </tr>
                            <tr style="text-align: center">
                                <%--<th class="head0" rowspan="3">地市</th>--%>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="RZ_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="RZ_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_CG_001" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="RZ_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="RZ_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_CG_002" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="RZ_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="RZ_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_CG_003" type="text" /></td>
                            </tr>

                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(76)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(76)" value="提交" />
                </div>
                <%-- 表单04 下午四 日照end --%>

                <%-- 表单05 下午五  乳山 start--%>
                <div class="dlgs" id="ddlg_82" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head0" colspan="10">下午五、乳山</th>
                            </tr>
                            <tr>
                                <%--<th class="head0" rowspan="3">港口</th>--%>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>
                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <%--<td rowspan="2">乳山</td>--%>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="RS_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="RS_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="RS_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="RS_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="RS_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="RS_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="RS_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="RS_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="RS_01G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="RS_01G_CW_02" type="text" /></td>
                                <td>
                                    <input id="RS_01D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="RS_01D_CW_02" type="text" /></td>
                                <td>
                                    <input id="RS_02G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="RS_02G_CW_02" type="text" /></td>
                                <td>
                                    <input id="RS_02D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="RS_02D_CW_02" type="text" /></td>
                            </tr>

                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(82)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(82)" value="提交" />
                </div>
                <%-- 表单05 下午五  乳山end --%>

                <%-- 表单06 下午六  文登 不变start--%>
                <div class="dlgs" id="ddlg_63" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="9">下午六、文登</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>
                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WHXQ_01G_CS_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01G_CG_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CG_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02D_CS_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02D_CG_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="WHXQ_01G_CS_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01G_CG_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CG_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02D_CS_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02D_CG_02" type="text" /></td>
                            </tr>

                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="WHXQ_01G_CS_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01G_CG_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CG_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02D_CS_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02D_CG_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(63)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(63)" value="提交" />
                </div>
                <%-- 表单06 下午六  文登end --%>

                <%-- 表单07 下午七  石岛start --%>
                <div class="dlgs" id="ddlg_83" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head0" colspan="10">下午七、石岛</th>
                            </tr>
                            <tr>
                                <%--<th class="head0" rowspan="3">港口</th>--%>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <%--<td rowspan="2">石岛</td>--%>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="SD_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="SD_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="SD_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="SD_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="SD_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="SD_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="SD_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="SD_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="SD_01G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="SD_01G_CW_02" type="text" /></td>
                                <td>
                                    <input id="SD_01D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="SD_01D_CW_02" type="text" /></td>
                                <td>
                                    <input id="SD_02G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="SD_02G_CW_02" type="text" /></td>
                                <td>
                                    <input id="SD_02D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="SD_02D_CW_02" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(83)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(83)" value="提交" />
                </div>

                <%-- 表单07 下午七  石岛end --%>

                <%-- 表单08 下午八  成山头 start --%>
                <div class="dlgs" id="ddlg_84" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head0" colspan="10">下午八、成山头</th>
                            </tr>
                            <tr>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="CST_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="CST_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="CST_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="CST_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="CST_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="CST_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="CST_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="CST_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="CST_01G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="CST_01G_CW_02" type="text" /></td>
                                <td>
                                    <input id="CST_01D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="CST_01D_CW_02" type="text" /></td>
                                <td>
                                    <input id="CST_02G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="CST_02G_CW_02" type="text" /></td>
                                <td>
                                    <input id="CST_02D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="CST_02D_CW_02" type="text" /></td>
                            </tr>

                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(84)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(84)" value="提交" />
                </div>
                <%-- 表单08 下午八  成山头end --%>

                <%-- 表单09 下午九  威海 start--%>
                <div class="dlgs" id="ddlg_77" style="width: auto; height: 350px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="16">下午九、威海</th>
                            </tr>
                            <tr style="text-align: center">
                                <%--<th class="head0" rowspan="3">地市</th>--%>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WH_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="WH_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WH_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="WH_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="WH_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="WH_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WH_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="WH_02D_CG_001" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="WH_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="WH_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="WH_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="WH_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="WH_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="WH_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="WH_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="WH_02D_CG_002" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="WH_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="WH_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="WH_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="WH_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="WH_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="WH_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="WH_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="WH_02D_CG_003" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(77)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(77)" value="提交" />
                </div>
                <%-- 表单09 下午九  威海end --%>

                <%-- 表单09 下午十海洋牧场（威海）start --%>
                <div class="dlgs" id="ddlg_48" style="height: 390px; padding: 10px;">
                    <div style="height: 10px"></div>
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="line-height: 45px;">
                                <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="34">下午十、海洋牧场</td>
                            </tr>
                            <tr>
                                <th class="head0" rowspan="2">海洋牧场长名称</th>
                                <th class="head0" rowspan="2">预报日期</th>
                                <th class="head0" colspan="24">24小时潮位</th>
                                <th class="head0" colspan="2">第一高潮</th>
                                <th class="head0" colspan="2">第一低潮</th>
                                <th class="head0" colspan="2">第二高潮</th>
                                <th class="head0" colspan="2">第二低潮</th>
                            </tr>
                            <tr>
                                <td>00时</td>
                                <td>01时</td>
                                <td>02时</td>
                                <td>03时</td>
                                <td>04时</td>
                                <td>05时</td>
                                <td>06时</td>
                                <td>07时</td>
                                <td>08时</td>
                                <td>09时</td>
                                <td>10时</td>
                                <td>11时</td>
                                <td>12时</td>
                                <td>13时</td>
                                <td>14时</td>
                                <td>15时</td>
                                <td>16时</td>
                                <td>17时</td>
                                <td>18时</td>
                                <td>19时</td>
                                <td>20时</td>
                                <td>21时</td>
                                <td>22时</td>
                                <td>23时</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>
                        </thead>

                        <tbody class="textStyle" style="text-align: center">
                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">寻山海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_CQ_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_CQ_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_H23" type="text" maxlength="20" /></td>

                                <td>
                                    <input id="TIDE_CQ_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_CQ_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_CQ_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">荣成烟墩角游钓型海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_RC_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_RC_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_RC_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_RC_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_RC_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">西霞口集团国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_XXK_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_XXK_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_XXK_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_XXK_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <%-- 下午20添加7个海洋牧场预报  Lian start --%>
                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">滨州正海底播型海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_BZZH_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_H23" type="text" maxlength="20" /></td>

                                <td>
                                    <input id="TIDE_BZZH_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_BZZH_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_BZZH_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_BZZH_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东通和底播型海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_SDTH_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_SDTH_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_SDTH_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_SDTH_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东莱州太平湾明波国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_TPW_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_TPW_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_TPW_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_TPW_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东琵琶口富瀚国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_PPK_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_PPK_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_PPK_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_H23" type="text" maxlength="20" /></td>

                                <td>
                                    <input id="TIDE_PPK_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_PPK_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东庙岛群岛东部佳益国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_MDQD_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_MDQD_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_MDQD_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_MDQD_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东海州湾顺风国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_HZW_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_HZW_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_HZW_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_HZW_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东岚山东部万泽丰国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H00" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H04" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H05" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H06" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H07" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H08" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H09" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H10" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H11" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H12" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H13" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H14" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H15" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H16" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H17" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H18" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H19" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H20" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H21" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H22" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_H23" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_FIRSTHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_FIRSTLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>

                                <td>
                                    <input id="TIDE_LSDBWZF_03_SECONDHTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_SECONDLTIME" type="text" maxlength="20" style="width: 45px;" /></td>
                                <td>
                                    <input id="TIDE_LSDBWZF_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <%-- 下午20添加7个海洋牧场预报  Lian end --%>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(48)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(48)" value="提交" />
                </div>
                <%-- 表单10 下午十 海洋牧场（威海）end --%>

                <%-- 表单11 下午十一  烟台start --%>
                <div class="dlgs" id="ddlg_78" style="width: auto; height: 350px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="16">下午十一、烟台</th>
                            </tr>
                            <tr style="text-align: center">
                                <%--<th class="head0" rowspan="3">地市</th>--%>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="YT_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="YT_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="YT_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="YT_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="YT_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="YT_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="YT_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="YT_02D_CG_001" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="YT_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="YT_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="YT_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="YT_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="YT_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="YT_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="YT_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="YT_02D_CG_002" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="YT_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="YT_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="YT_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="YT_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="YT_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="YT_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="YT_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="YT_02D_CG_003" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(78)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(78)" value="提交" />
                </div>
                <%-- 表单11 下午十一  烟台end --%>

                <%-- 表单12 下午十二  潍坊start --%>
                <div class="dlgs" id="ddlg_79" style="width: auto; height: 350px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="16">下午十二、潍坊</th>
                            </tr>
                            <tr style="text-align: center">
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WF_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="WF_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WF_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="WF_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="WF_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="WF_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WF_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="WF_02D_CG_001" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="WF_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="WF_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="WF_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="WF_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="WF_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="WF_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="WF_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="WF_02D_CG_002" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="WF_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="WF_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="WF_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="WF_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="WF_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="WF_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="WF_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="WF_02D_CG_003" type="text" /></td>
                            </tr>


                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(79)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(79)" value="提交" />
                </div>
                <%-- 表单12 下午十二  潍坊end --%>

                <%-- 表单13 下午十三  小岛河start --%>
                <div class="dlgs" id="ddlg_10" style="width: auto; height: 315px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head0" colspan="10">下午十三、小岛河</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="MZZ_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="MZZ_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="MZZ_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="MZZ_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="MZZ_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="MZZ_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="MZZ_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="MZZ_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="MZZ_01G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="MZZ_01G_CW_02" type="text" /></td>
                                <td>
                                    <input id="MZZ_01D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="MZZ_01D_CW_02" type="text" /></td>
                                <td>
                                    <input id="MZZ_02G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="MZZ_02G_CW_02" type="text" /></td>
                                <td>
                                    <input id="MZZ_02D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="MZZ_02D_CW_02" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="MZZ_01G_SJ_03" type="text" /></td>
                                <td>
                                    <input id="MZZ_01G_CW_03" type="text" /></td>
                                <td>
                                    <input id="MZZ_01D_SJ_03" type="text" /></td>
                                <td>
                                    <input id="MZZ_01D_CW_03" type="text" /></td>
                                <td>
                                    <input id="MZZ_02G_SJ_03" type="text" /></td>
                                <td>
                                    <input id="MZZ_02G_CW_03" type="text" /></td>
                                <td>
                                    <input id="MZZ_02D_SJ_03" type="text" /></td>
                                <td>
                                    <input id="MZZ_02D_CW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(10)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(10)" value="提交" />
                </div>

                <%-- 表单13 下午十三  小岛河end --%>

                <%-- 表单14 下午十四  黄河海港start --%>
                <div class="dlgs" id="ddlg_80" style="width: auto; height: 350px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="16">下午十四、黄河海港</th>
                            </tr>
                            <tr style="text-align: center">
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="DY_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="DY_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="DY_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="DY_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="DY_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="DY_02D_CG_001" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="DY_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="DY_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="DY_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="DY_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="DY_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="DY_02D_CG_002" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="DY_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="DY_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="DY_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="DY_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="DY_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="DY_02D_CG_003" type="text" /></td>
                            </tr>

                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(80)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(80)" value="提交" />
                </div>
                <%-- 表单14 下午十四  黄河海港end --%>

                <!--表单43.东营埕岛预报编号-->
                <%--<div class="dlgs" id="ddlg_43" style="height: auto; padding: 10px;">
                     <div style="border: 1px solid #ddd; background-color: #d2ffe7; height: auto; padding: 10px;" >
                          东营埕岛预报编号：<input type="text" id="ProYear" style="width: 50px" />-<input type="text" id="ProNo" style="width: 50px"/>
                     </div>
                </div>--%>

                <%-- 表单15 下午十五  东营港 start --%>
                <div class="dlgs" id="ddlg_42" style="height: 320px; padding: 10px;">

                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head0" colspan="10">下午十五、东营港</th>
                            </tr>
                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>
                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="DY_01G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DY_01G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CG_01" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DY_02D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DY_02D_CG_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="DY_01G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DY_01G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CG_02" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DY_02D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DY_02D_CG_02" type="text" /></td>
                            </tr>

                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="DY_01G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DY_01G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CG_03" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DY_02D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DY_02D_CG_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(42)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(42)" value="提交" />
                </div>

                <%-- 表单15 下午十五  东营港 end --%>

                <%-- 表单16 下午十六  滨州港 start --%>
                <div class="dlgs" id="ddlg_81" style="width: auto; height: 350px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="16">下午十六、滨州港</th>
                            </tr>
                            <tr style="text-align: center">
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="BZ_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="BZ_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_CG_001" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="BZ_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="BZ_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_CG_002" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="BZ_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="BZ_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_CG_003" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(81)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(81)" value="提交" />
                </div>
                <%-- 表单16 下午十六  滨州港 end --%>

                <%-- 表单17 下午十七  曹妃甸 start --%>
                <div class="dlgs" id="ddlg_12" style="width: auto; height: 315px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr>

                                <th class="head0" colspan="10">下午十七、曹妃甸</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                                <td>潮时</td>
                                <td>潮高</td>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="NBYT_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="NBYT_01G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_01G_CW_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_01D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_01D_CW_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_02G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_02G_CW_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_02D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_02D_CW_02" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="NBYT_01G_SJ_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_01G_CW_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_01D_SJ_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_01D_CW_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_02G_SJ_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_02G_CW_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_02D_SJ_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_02D_CW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(12)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(12)" value="提交" />
                </div>

                <%-- 表单17 下午十七  曹妃甸 end --%>

                <!--填报信息 start-->
                <div id="dlg_23" class="dlgs ">
                    <div class="contenttitle2">
                        <h3 id="hj2">填报信息</h3>
                    </div>
                    <div id="tbxx" style="font-size: 12px; font-weight: 300">
                        <div style="margin-bottom: 5px" />
                        &nbsp;&nbsp;&nbsp;发布单位： 
                        <input id="Fabudanwei" type="text" style="width: 200px" />
                        &nbsp;&nbsp;
                            <%--电话：--%><input id="Tel" type="hidden" style="width: 200px" />
                        预报值班：<input id="ZhibanTel" type="text" style="width: 200px" value="0532-58750688" />
                        &nbsp;&nbsp;
                    </div>
                    <div style="margin-bottom: 5px">
                        &nbsp;&nbsp;&nbsp;发布时间：
                        <input id="Fabushijian" type="text" style="width: 200px" />
                        &nbsp;&nbsp;<%--传真：--%>
                        <input id="Chuanzhen" type="hidden" style="width: 200px" />
                        预报发送：<input id="SendTel" type="text" style="width: 200px" value="0532-58750626" />
                        &nbsp;&nbsp;
                    </div>
                    海浪预报员：<%--<input id="Hailang" type="text" style="width: 200px" />--%>
                    <select id="Hailang" class="uniformselect" style="width: 100%; height: 25px;">
                        <option value="">请选择</option>
                    </select>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        潮汐预报员：<%--<input id="Chaoxi" type="text" style="width: 200px" />--%>
                    <select id="Chaoxi" class="uniformselect" style="width: 100%; height: 25px;">
                        <option value="">请选择</option>
                    </select>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        水温预报员：<%--<input id="Shuiwen" type="text" style="width: 200px" />--%>
                    <select id="Shuiwen" class="uniformselect" style="width: 100%; height: 25px;">
                        <option value="">请选择</option>
                    </select>
                    <br />
                    海浪预报员电话：<input id="HailangTel" type="text" style="width: 150px" />&nbsp;&nbsp;
                    潮汐预报员电话：<input id="ChaoxiTel" type="text" style="width: 150px" />&nbsp;&nbsp;
                    水温预报员电话：<input id="ShuiwenTel" type="text" style="width: 150px" />

                    &nbsp;&nbsp;
                    <input style="margin: 3px 5px 0px 40px" type="button" class="stdbtn" onclick="dlg_Submit(23)" value="填报信息提交" />
                </div>

                <script type="text/javascript">
                    $(function () {
                        var datas = { page: 1, rows: 100 };
                        $.ajax({
                            type: "POST",
                            url: '/Ajax/Reporter.ashx?method=GetReporter',
                            data: datas,
                            success: function (result) {
                                $("#Chaoxi").html("");
                                $("#Hailang").html("");
                                $("#Shuiwen").html("");
                                var strs1 = "";
                                var strs2 = "";
                                var first_txt_hl = "";
                                var first_txt_fbchb = "";
                                var resjson = JSON.parse(result);
                                for (var j = 0; j < resjson["total"]; j++) {

                                    var yubaoid = resjson["rows"][j]["reportertypeid"];
                                    var reportercode = resjson["rows"][j]["reportercode"];
                                    var reportername = resjson["rows"][j]["reportername"];
                                    var reportertel = resjson["rows"][j]["reportertel"];
                                    if (yubaoid == "FBHBYB") {
                                        strs1 += "<option value='" + reportercode + "' bs='" + reportertel + "'>" + reportercode + "</option>";
                                        $("#ChaoxiTel").val(reportertel);
                                        $("#ShuiwenTel").val(reportertel);

                                        $("#Chaoxi").html(strs1);
                                        $("#Shuiwen").html(strs1);
                                        $("#uniform-Chaoxi span").text(reportercode);
                                        $("#uniform-Shuiwen span").text(reportercode);
                                        $("#uniform-Chaoxi span").attr("code", reportercode);
                                        $("#uniform-Shuiwen span").attr("code", reportercode);
                                    } else if (yubaoid == "HLYB") {
                                        strs2 += "<option value='" + reportercode + "' bs='" + reportertel + "'>" + reportercode + "</option>";
                                        $("#HailangTel").val(reportertel);
                                        $("#Hailang").html(strs2);
                                        $("#uniform-Hailang span").text(reportercode);
                                        $("#uniform-Hailang span").attr("code", reportercode);
                                    }

                                }
                            }
                        });
                    });
                    $("#Hailang").change(function () {

                        var code = $("#Hailang option:selected").val();
                        var tel = $("#Hailang option:selected").attr("bs");
                        $("#HailangTel").val(tel);
                        $("#uniform-Hailang span").attr("code", code);
                    });
                    $("#Chaoxi").change(function () {
                        var code = $("#Chaoxi option:selected").val();
                        var tel = $("#Chaoxi option:selected").attr("bs");
                        $("#ChaoxiTel").val(tel);
                        $("#uniform-Chaoxi span").attr("code", code);
                    });
                    $("#Shuiwen").change(function () {
                        var code = $("#Shuiwen option:selected").val();
                        var tel = $("#Shuiwen option:selected").attr("bs");
                        $("#ShuiwenTel").val(tel);
                        $("#uniform-Shuiwen span").attr("code", code);
                    });
                </script>

                <style>
                    #Hailang option {
                        font-family: "Helvetica Neue", Helvetica, Arial, sans-serif !important;
                        font-size: 14px;
                    }

                    #Chaoxi option {
                        font-family: "Helvetica Neue", Helvetica, Arial, sans-serif !important;
                        font-size: 14px;
                    }

                    #Shuiwen option {
                        font-family: "Helvetica Neue", Helvetica, Arial, sans-serif !important;
                        font-size: 14px;
                    }
                </style>
                <!--填报信息 end-->

                <!--操作日志-->
                <div id="dlg_czrz" class="easyui-dialog" title="操作日志" data-options="iconCls:'icon-save'" style="width: 800px; height: 530px; padding: 10px;">
                    <iframe width="100%" id="win" height="435" name="czrz" frameborder="0" src="Logbyuser.aspx"></iframe>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_czrz').dialog('close'); " value="取消" />
                </div>

                <div id="basicform" style="position: fixed; bottom: 0px; left: 20px; z-index: 2"></div>
                <br />
                <%-- 表单信息 end --%>
            </div>
        </div>
    </form>



    <script>
        //表单验证
        var tb_cw, tb_cw2;
        var tb_sw, tb_sw2;
        var tb_bg, tb_bg2;
        var tb_bx;
        var tb_fx;
        var tb_yx;
        var tb_tq;
        var tb_fl, tb_fl2;
        var tb_lg, tb_lg2;

        //01获取验证规则
        $.ajax({
            type: "POST",
            url: "/Ajax/GetRole.ashx?method=getall&s=" + new Date().toTimeString() + "",
            success: function (data) {
                var resjson = JSON.parse(data);
                tb_cw = resjson.tb_cw;
                tb_cw2 = resjson.tb_cw2;

                //        //海浪
                tb_bg = resjson.tb_bg;
                tb_bx = resjson.tb_bx;
                tb_yx = resjson.tb_yx;
                tb_fx = resjson.tb_fx;
                tb_fl = resjson.tb_fl;
                tb_lg = resjson.tb_lg;
                tb_tq = resjson.tb_tq;
                tb_bg2 = resjson.tb_bg2;
                tb_fl2 = resjson.tb_fl2;
                tb_lg2 = resjson.tb_lg2

                //      //水温
                tb_sw = resjson.tb_sw;
                tb_sw2 = resjson.tb_sw2;
            }
        });
        //鼠标离开文本框验证事件
        {
            //02当潮位焦点离开时触发验证
            $("input[id*='CW']").blur(function () {
                valisection(this, tb_cw, tb_cw2);
            });
            $("input[id*='CG']").blur(function () {
                valisection(this, tb_cw, tb_cw2);
            });
            $("input[id*='C_C']").blur(function () {
                valisection(this, tb_cw, tb_cw2);
            });
            //潮汐时分
            $("input[id*='SJ']").blur(function () {
                hmsection(this);
            });
            $("input[id*='CS']").blur(function () {
                hmsection(this);
            });
            //潮汐时
            $("input[id*='G_H']").blur(function () {
                hsection(this);
            });
            $("input[id*='D_H']").blur(function () {
                hsection(this);
            });
            $("input[id*='C_H']").blur(function () {
                hsection(this);
            });
            //潮汐分
            $("input[id*='_M']").blur(function () {
                msection(this);
            });
            //潮汐时
            //$("input[id*='G_H']").blur(function () {
            //    hsection(this);
            //});
            //    //潮汐分
            //$("input[id*='G_M']").blur(function () {
            //    msection(this);
            //});

            ////波高
            //$("input[id*='BG']").blur(function () {
            //    valisection(this, tb_bg, tb_bg2);
            //});
            ////波向
            //$("input[id*='BX']").blur(function () {
            //    strsection(this, tb_bx);
            //});
            ////风向
            //$("input[id*='FX']").blur(function () {
            //    strsection(this, tb_fx);
            //});
            ////涌向
            //$("input[id*='YX']").blur(function () {
            //    strsection(this, tb_yx);
            //});
            ////天气
            //$("input[id*='TQ']").blur(function () {
            //    strsection(this, tb_tq);
            //});
            ////风力
            //$("input[id*='FL']").blur(function () {
            //    valisection(this, tb_fl, tb_fl2);
            //});
            ////浪高
            //$("input[id*='LG']").blur(function () {
            //    valisection(this, tb_lg, tb_lg2);
            //});
            //$("input[id*='_GD']").blur(function () {
            //    valisection(this, tb_lg, tb_lg2);
            //});

            ////水温
            //$("input[id*='SW']").blur(function () {
            //    valisection(this, tb_sw, tb_sw2);
            //});
        }

        //验证区间方法
        function valisection(obj, num1, num2) {
            var thisval = $(obj).val();
            if (thisval != "-") {
                if (!isNaN(thisval)) {//如果是数字
                    if (parseFloat(thisval) >= num1 && parseFloat(thisval) <= num2) {//在验证规则区间
                        $(obj).css("border", "1px solid gray");
                    } else {
                        $(obj).css("border", "1px solid red");
                    }
                } else {
                    //不是数字
                    if (thisval.indexOf("-") > 0 && thisval.split('-').length == 2) {
                        if (parseFloat(thisval.split('-')[0]) >= num1 && parseFloat(thisval.split('-')[0]) <= num2 && parseFloat(thisval.split('-')[1]) >= num1 && parseFloat(thisval.split('-')[1]) <= num2) {
                            $(obj).css("border", "1px solid gray");
                        } else {
                            $(obj).css("border", "1px solid red");
                        }
                    } else {
                        $(obj).css("border", "1px solid red");
                    }
                }
            } else {
                $(obj).css("border", "1px solid gray");
            }
        }

        //时、分验证方法
        function hmsection(obj) {
            var thisval = $(obj).val();
            if (thisval != "-") {
                if (!isNaN(thisval)) {//如果是数字
                    if (parseFloat(thisval.substring(2, 0)) < 24 && parseFloat(thisval.substring(2, 4)) < 60) {//在验证规则区间
                        $(obj).css("border", "1px solid gray");
                    } else {
                        $(obj).css("border", "1px solid red");
                    }
                } else {
                    $(obj).css("border", "1px solid red");
                }
            } else {
                $(obj).css("border", "1px solid gray");
            }
        }

        //时 验证方法
        function hsection(obj) {
            var thisval = $(obj).val();
            if (thisval != "-") {
                if (!isNaN(thisval)) {//如果是数字 && thisval.length == 2
                    if (parseFloat(thisval) < 24 && parseFloat(thisval) > 0) {//在验证规则区间
                        $(obj).css("border", "1px solid gray");
                    } else {
                        $(obj).css("border", "1px solid red");
                    }
                } else {
                    $(obj).css("border", "1px solid red");
                }
            } else {
                $(obj).css("border", "1px solid gray");
            }
        }

        //分 验证方法
        function msection(obj) {
            var thisval = $(obj).val();
            if (thisval != "-") {
                if (!isNaN(thisval)) {//如果是数字 && thisval.length == 2
                    if (parseFloat(thisval) < 60 && parseFloat(thisval) > 0) {//在验证规则区间
                        $(obj).css("border", "1px solid gray");
                    } else {
                        $(obj).css("border", "1px solid red");
                    }
                } else {
                    $(obj).css("border", "1px solid red");
                }
            } else {
                $(obj).css("border", "1px solid gray");
            }
        }

        //字符验证事件
        function strsection(obj, strs) {
            var thisval = $(obj).val();
            var valistr = strs.split('-');
            var istrue = false;
            var istrues = 0;
            if (thisval.indexOf("转") > 0) {
                for (var i = 0; i < valistr.length; i++) {
                    if (valistr[i] == thisval.split('转')[0]) {
                        for (var j = 0; j < valistr.length; j++) {
                            if (valistr[j] == thisval.split('转')[1]) {
                                istrue = true;
                            }
                        }
                    }
                }
            }
            else {
                for (var i = 0; i < valistr.length; i++) {
                    if (valistr[i] == thisval) { istrue = true; }
                }
            }
            if (istrue) { $(obj).css("border", "1px solid gray"); }
            else
            { $(obj).css("border", "1px solid red"); }
        }

        //验证所有点击按钮
        $("#btnrole").click(function () {
            roleall();
        });

        //验证所有
        function roleall() {
            //潮位
            $("input[id*='CW']").each(function () {
                valisection(this, tb_cw, tb_cw2);
            });
            $("input[id*='CG']").each(function () {
                valisection(this, tb_cw, tb_cw2);
            });
            $("input[id*='C_C']").each(function () {
                valisection(this, tb_cw, tb_cw2);
            });
            //潮汐时分
            $("input[id*='SJ']").each(function () {
                hmsection(this);
            });
            $("input[id*='CS']").each(function () {
                hmsection(this);
            });

            //潮汐时
            $("input[id*='G_H']").each(function () {
                hsection(this);
            });
            $("input[id*='D_H']").each(function () {
                hsection(this);
            });
            $("input[id*='C_H']").each(function () {
                hsection(this);
            });
            //潮汐分
            $("input[id*='_M']").each(function () {
                msection(this);
            });
            ////潮汐时
            //$("input[id*='G_H']").each(function () {
            //    hsection(this);
            //});
            ////潮汐分
            //$("input[id*='G_M']").each(function () {
            //    msection(this);
            //});

            //海浪
            //波高
            //$("input[id*='BG']").each(function () {
            //    valisection(this, tb_bg, tb_bg2);
            //});
            ////波向
            //$("input[id*='BX']").each(function () {
            //    strsection(this, tb_bx);
            //});
            ////风向
            //$("input[id*='FX']").each(function () {
            //    strsection(this, tb_bx);
            //});
            ////涌向
            //$("input[id*='YX']").each(function () {
            //    strsection(this, tb_yx);
            //});
            ////风力
            //$("input[id*='FL']").each(function () {
            //    valisection(this, tb_fl, tb_fl2);
            //});
            ////浪高
            //$("input[id*='LG']").each(function () {
            //    valisection(this, tb_lg, tb_lg2);
            //});
            //$("input[id*='_GD']").each(function () {
            //    valisection(this, tb_lg, tb_lg2);
            //});
            ////天气
            //$("input[id*='TQ']").each(function () {
            //    strsection(this, tb_tq);
            //});

            ////水温
            //$("input[id*='SW']").each(function () {
            //    valisection(this, tb_sw, tb_sw2);
            //});
        }

        var date = new Date();
        var num = 0;
        var nqyname;
        //↓表单提交(表单编号)
        function dlg_Submit(id) {//（无当天数据保存、有当天数据修改）↓
            switch (id) {
                case 75: submit_75(id); break;//下午一、青岛（五号码头） 原下午三青岛潮汐数据
                case 18: submit_18(id); break;//下午二、青岛（小麦岛）   原下午十二 小麦岛72小时潮汐预报 不变
                case 69: submit_69(id); break;//下午三、青岛（董家口）   原下午三十七 董家口72小时潮汐预报 不变
                case 76: submit_76(id); break;//下午四 日照  原下午三青岛三天潮汐预报。
                case 82: submit_82(id); break;//下午五 乳山  原下午十六乳山两天潮汐预报
                case 63: submit_63(id); break;//下午六 文登   原下午三十一 不变
                case 83: submit_83(id); break;//下午七 石岛   原下午下午十六石岛两天潮汐预报
                case 84: submit_84(id); break;//下午八  成山头  原下午下午十六成山头两天潮汐预报
                case 77: submit_77(id); break;//下午九  威海  原下午三威海三天潮汐预报
                case 48: submit_48(id); break;//下午十  海洋牧场（威海） 原下午二十 不变
                case 78: submit_78(id); break;//下午十一  烟台  原下午三烟台三天潮汐预报
                case 79: submit_79(id); break;//下午十二  潍坊  原下午三潍坊三天潮汐预报
                case 10: submit_10(id); break;//下午十三  小岛河  原下午五明泽闸三天潮汐预报 不变
                case 80: submit_80(id); break;//下午十四  黄河海港 原下午三 东营
                case 42: submit_42(id); break;//下午十五 东营港   原下午十八 不变
                case 81: submit_81(id); break;//下午十六  滨州港  原下午三滨州三天潮汐预报
                case 12: submit_12(id); break;//下午十七  曹妃甸   原下午七南堡油田三天潮汐预报 不变
                case 23: submit_23(id); break;//填报信息提交表单
                default:
            }
        }

        //保存所有
        function alldlg_Submit() {//（无当天数据保存、有当天数据修改）↓
            submit_75(75);//下午一、青岛（五号码头） 原下午三青岛潮汐数据
            submit_18(18);//下午二、青岛（小麦岛）   原下午十二 小麦岛72小时潮汐预报 不变
            submit_69(69);//下午三、青岛（董家口）   原下午三十七 董家口72小时潮汐预报 不变
            submit_76(76);//下午四 日照  原下午三青岛三天潮汐预报。
            submit_82(82);//下午五 乳山  原下午十六乳山两天潮汐预报
            submit_63(63);//下午六 文登   原下午三十一 不变
            submit_83(83);//下午七 石岛   原下午下午十六石岛两天潮汐预报
            submit_84(84);//下午八  成山头  原下午下午十六成山头两天潮汐预报
            submit_77(77);//下午九  威海  原下午三威海三天潮汐预报
            submit_48(48);//下午十  海洋牧场（威海） 原下午二十 不变
            submit_78(78);//下午十一  烟台  原下午三烟台三天潮汐预报
            submit_79(79);//下午十二  潍坊  原下午三潍坊三天潮汐预报
            submit_10(10);//下午十三  小岛河  原下午五明泽闸三天潮汐预报 不变
            submit_80(80);//下午十四  黄河海港 原下午三 东营
            submit_42(42);//下午十五 东营港   原下午十八 不变
            submit_81(81);//下午十六  滨州港  原下午三滨州三天潮汐预报
            submit_12(12);//下午十七  曹妃甸   原下午七南堡油田三天潮汐预报 不变
            submit_23(23);//填报信息提交表单   
        }

        //表单数据拼接提交 从左至右 从上至下
        //↓表单提交（表单编号）↓ start 
        {
            //以下是将原先的下午三拆分的

            //下午一、青岛（五号码头） 原下午三青岛潮汐数据
            function submit_75(id) {
                var str_data = "";//总的str
                var str_data_CH = "";//潮汐数据潮时
                var str_data_CG = "";//潮汐数据潮高
                var nqyname = "QD";
                for (var j = 1; j < 4; j++) {

                    //潮汐数据潮时提交
                    str_data_CH += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02D_H_00" + j).val() + ",";

                    //潮汐数据潮高提交
                    str_data_CG += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";

                    //潮汐数据潮时提交
                    str_data += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";
                }

                str_data_CH = str_data_CH.substring(0, str_data_CH.length - 1);
                str_data_CG = str_data_CG.substring(0, str_data_CG.length - 1);
                str_data = str_data.substring(0, str_data.length - 1);

                var data_CH = { datas: str_data_CH, areaname: nqyname };
                var data_CG = { datas: str_data_CG, areaname: nqyname };
                var data = { datas: str_data, areaname: nqyname }

                SubmitSevenPMCH(data_CH);
                SubmitSevenPMCG(data_CG);
                submitSevenPMZong(data);
            }

            //下午四 日照  原下午三青岛三天潮汐预报。
            function submit_76(id) {
                var str_data = "";//总的str
                var str_data_CH = "";//潮汐数据潮时
                var str_data_CG = "";//潮汐数据潮高
                var nqyname = "RZ";
                for (var j = 1; j < 4; j++) {

                    //潮汐数据潮时提交
                    str_data_CH += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02D_H_00" + j).val() + ",";

                    //潮汐数据潮高提交
                    str_data_CG += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";

                    //潮汐数据潮时提交
                    str_data += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";
                }

                str_data_CH = str_data_CH.substring(0, str_data_CH.length - 1);
                str_data_CG = str_data_CG.substring(0, str_data_CG.length - 1);
                str_data = str_data.substring(0, str_data.length - 1);


                var data_CH = { datas: str_data_CH, areaname: nqyname };
                var data_CG = { datas: str_data_CG, areaname: nqyname };
                var data = { datas: str_data, areaname: nqyname }

                SubmitSevenPMCH(data_CH);
                SubmitSevenPMCG(data_CG);
                submitSevenPMZong(data);
            }

            //下午九  威海  原下午三威海三天潮汐预报
            function submit_77(id) {
                var str_data_CH = "";//潮汐数据潮时
                var str_data_CG = "";//潮汐数据潮高
                var nqyname = "WH";
                for (var j = 1; j < 4; j++) {

                    //潮汐数据潮时提交
                    str_data_CH += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02D_H_00" + j).val() + ",";

                    //潮汐数据潮高提交
                    str_data_CG += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";
                }

                str_data_CH = str_data_CH.substring(0, str_data_CH.length - 1);
                str_data_CG = str_data_CG.substring(0, str_data_CG.length - 1);

                var data_CH = { datas: str_data_CH, areaname: nqyname };
                var data_CG = { datas: str_data_CG, areaname: nqyname };

                SubmitSevenPMCH(data_CH);
                SubmitSevenPMCG(data_CG);
            }

            //下午十一  烟台  原下午三烟台三天潮汐预报
            function submit_78(id) {
                var str_data = "";
                var str_data_CH = "";//潮汐数据潮时
                var str_data_CG = "";//潮汐数据潮高
                var nqyname = "YT";
                for (var j = 1; j < 4; j++) {

                    //潮汐数据潮时提交
                    str_data_CH += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02D_H_00" + j).val() + ",";

                    //潮汐数据潮高提交
                    str_data_CG += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";

                    //潮汐数据潮时提交
                    str_data += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";
                }

                str_data_CH = str_data_CH.substring(0, str_data_CH.length - 1);
                str_data_CG = str_data_CG.substring(0, str_data_CG.length - 1);
                str_data = str_data.substring(0, str_data.length - 1);

                var data_CH = { datas: str_data_CH, areaname: nqyname };
                var data_CG = { datas: str_data_CG, areaname: nqyname };
                var data = { datas: str_data, areaname: nqyname }

                SubmitSevenPMCH(data_CH);
                SubmitSevenPMCG(data_CG);
                submitSevenPMZong(data);
            }

            //下午十二  潍坊  原下午三潍坊三天潮汐预报
            function submit_79(id) {
                var str_data = "";
                var str_data_CH = "";//潮汐数据潮时
                var str_data_CG = "";//潮汐数据潮高
                var nqyname = "WF";
                for (var j = 1; j < 4; j++) {

                    //潮汐数据潮时提交
                    str_data_CH += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02D_H_00" + j).val() + ",";

                    //潮汐数据潮高提交
                    str_data_CG += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";

                    //潮汐数据潮时提交
                    str_data += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_H_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";
                }

                str_data_CH = str_data_CH.substring(0, str_data_CH.length - 1);
                str_data_CG = str_data_CG.substring(0, str_data_CG.length - 1);
                str_data = str_data.substring(0, str_data.length - 1);

                var data_CH = { datas: str_data_CH, areaname: nqyname };
                var data_CG = { datas: str_data_CG, areaname: nqyname };
                var data = { datas: str_data, areaname: nqyname }

                SubmitSevenPMCH(data_CH);
                SubmitSevenPMCG(data_CG);
                submitSevenPMZong(data);
            }

            //下午十四  黄河海港 原下午三 东营
            function submit_80(id) {
                var str_data_CH = "";//潮汐数据潮时
                var str_data_CG = "";//潮汐数据潮高
                var nqyname = "DY";
                for (var j = 1; j < 4; j++) {

                    //潮汐数据潮时提交
                    str_data_CH += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02D_H_00" + j).val() + ",";

                    //潮汐数据潮高提交
                    str_data_CG += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";
                }

                str_data_CH = str_data_CH.substring(0, str_data_CH.length - 1);
                str_data_CG = str_data_CG.substring(0, str_data_CG.length - 1);

                var data_CH = { datas: str_data_CH, areaname: nqyname };
                var data_CG = { datas: str_data_CG, areaname: nqyname };

                SubmitSevenPMCH(data_CH);
                SubmitSevenPMCG(data_CG);
            }

            //下午十六  滨州港  原下午三滨州三天潮汐预报
            function submit_81(id) {
                var str_data_CH = "";//潮汐数据潮时
                var str_data_CG = "";//潮汐数据潮高
                var nqyname = "BZ";
                for (var j = 1; j < 4; j++) {

                    //潮汐数据潮时提交
                    str_data_CH += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                    str_data_CH += $("#" + nqyname + "_02D_H_00" + j).val() + ",";

                    //潮汐数据潮高提交
                    str_data_CG += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                    str_data_CG += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";
                }

                str_data_CH = str_data_CH.substring(0, str_data_CH.length - 1);
                str_data_CG = str_data_CG.substring(0, str_data_CG.length - 1);

                var data_CH = { datas: str_data_CH, areaname: nqyname };
                var data_CG = { datas: str_data_CG, areaname: nqyname };

                SubmitSevenPMCH(data_CH);
                SubmitSevenPMCG(data_CG);
            }

            //潮时
            function SubmitSevenPMCH(data) {
                submit_ajax('75', data);
            }
            //潮高
            function SubmitSevenPMCG(data) {
                submit_ajax('76', data);
            }
            //综合的提交
            function submitSevenPMZong(data) {
                submit_ajax('77', data);
            }

            //以上是将原先的下午三拆分的


            //以下三个可以共用一个 start
            //下午五 乳山  原下午十六乳山两天潮汐预报(22)
            function submit_82(id) {
                var str_data = "";
                var nqyname = "RS";

                for (var j = 1; j <= 2; j++) {

                    str_data += $("#" + nqyname + "_01G_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01G_CW_0" + j).val() + ",";

                    str_data += $("#" + nqyname + "_01D_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_CW_0" + j).val() + ",";

                    str_data += $("#" + nqyname + "_02G_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_CW_0" + j).val() + ",";

                    str_data += $("#" + nqyname + "_02D_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_CW_0" + j).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data, areaname: nqyname };
                SubmitSixteenPM(data)
                //submit_ajax(id, data);
            }
            //下午七 石岛   原下午下午十六石岛两天潮汐预报(22)
            function submit_83(id) {
                var str_data = "";
                var nqyname = "SD";

                for (var j = 1; j <= 2; j++) {

                    str_data += $("#" + nqyname + "_01G_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01G_CW_0" + j).val() + ",";

                    str_data += $("#" + nqyname + "_01D_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_CW_0" + j).val() + ",";

                    str_data += $("#" + nqyname + "_02G_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_CW_0" + j).val() + ",";

                    str_data += $("#" + nqyname + "_02D_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_CW_0" + j).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data, areaname: nqyname };
                SubmitSixteenPM(data)
            }

            //下午八  成山头  原下午下午十六成山头两天潮汐预报(22)
            function submit_84(id) {
                var str_data = "";
                var nqyname = "CST";

                for (var j = 1; j <= 2; j++) {

                    str_data += $("#" + nqyname + "_01G_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01G_CW_0" + j).val() + ",";

                    str_data += $("#" + nqyname + "_01D_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_01D_CW_0" + j).val() + ",";

                    str_data += $("#" + nqyname + "_02G_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02G_CW_0" + j).val() + ",";

                    str_data += $("#" + nqyname + "_02D_SJ_0" + j).val() + ",";
                    str_data += $("#" + nqyname + "_02D_CW_0" + j).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data, areaname: nqyname };
                SubmitSixteenPM(data)
            }
            //共用同一个提交的方法
            function SubmitSixteenPM(data) {
                submit_ajax('82', data);
            }
            //以上三个可以共用一个 end



            //不变的 start
            //下午二、青岛（小麦岛）   原下午十二 小麦岛72小时潮汐预报 不变（18）
            function submit_18(id) {
                var str_data = "";
                for (var i = 0; i < 2; i++) {
                    switch (i) {
                        case 0: nqyname = "XMD"; break;
                        default:
                    }

                    for (var j = 1; j <= 3; j++) {

                        str_data += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                        str_data += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";

                        str_data += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                        str_data += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";

                        str_data += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                        str_data += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";

                        str_data += $("#" + nqyname + "_02D_H_00" + j).val() + ",";
                        str_data += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";
                    }
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //下午三、青岛（董家口）   原下午三十七 董家口72小时潮汐预报 不变（69）
            function submit_69(id) {
                var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#DJKP_01G_CS_0" + i).val() + ",";
                    str_data += $("#DJKP_01G_CG_0" + i).val() + ",";
                    str_data += $("#DJKP_02G_CS_0" + i).val() + ",";
                    str_data += $("#DJKP_02G_CG_0" + i).val() + ",";
                    str_data += $("#DJKP_01D_CS_0" + i).val() + ",";
                    str_data += $("#DJKP_01D_CG_0" + i).val() + ",";
                    str_data += $("#DJKP_02D_CS_0" + i).val() + ",";
                    str_data += $("#DJKP_02D_CG_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午六 文登   原下午三十一 不变(63)
            function submit_63(id) {
                var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#WHXQ_01G_CS_0" + i).val() + ",";
                    str_data += $("#WHXQ_01G_CG_0" + i).val() + ",";
                    str_data += $("#WHXQ_02G_CS_0" + i).val() + ",";
                    str_data += $("#WHXQ_02G_CG_0" + i).val() + ",";
                    str_data += $("#WHXQ_01D_CS_0" + i).val() + ",";
                    str_data += $("#WHXQ_01D_CG_0" + i).val() + ",";
                    str_data += $("#WHXQ_02D_CS_0" + i).val() + ",";
                    str_data += $("#WHXQ_02D_CG_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //下午十  海洋牧场（威海） 原下午二十 不变(48)
            function submit_48(id) {

                var str_data = "";
                var d = new Date();
                var nqyname = "";
                var countH = 24;

                for (var j = 0; j < 10; j++) {
                    switch (j) {
                        case 0: nqyname = "CQ"; break;
                        case 1: nqyname = "RC"; break;
                        case 2: nqyname = "XXK"; break;

                        case 3: nqyname = "BZZH"; break;
                        case 4: nqyname = "SDTH"; break;
                        case 5: nqyname = "TPW"; break;
                        case 6: nqyname = "PPK"; break;
                        case 7: nqyname = "MDQD"; break;
                        case 8: nqyname = "HZW"; break;
                        case 9: nqyname = "LSDBWZF"; break;
                        default:
                    }

                    for (var k = 1; k < 4; k++) {
                        for (var i = 0; i < countH; i++) {
                            if (i < 10) {
                                str_data += $("#TIDE_" + nqyname + "_0" + k + "_H0" + i).val() + ",";
                            }
                            else {
                                str_data += $("#TIDE_" + nqyname + "_0" + k + "_H" + i).val() + ",";
                            }
                        }
                        str_data += $("#TIDE_" + nqyname + "_0" + k + "_FIRSTHTIME").val() + ",";
                        str_data += $("#TIDE_" + nqyname + "_0" + k + "_FIRSTHHEIGHT").val() + ",";

                        str_data += $("#TIDE_" + nqyname + "_0" + k + "_SECONDHTIME").val() + ",";
                        str_data += $("#TIDE_" + nqyname + "_0" + k + "_SECONDHHEIGHT").val() + ",";

                        str_data += $("#TIDE_" + nqyname + "_0" + k + "_FIRSTLTIME").val() + ",";
                        str_data += $("#TIDE_" + nqyname + "_0" + k + "_FIRSTLHEIGHT").val() + ",";

                        str_data += $("#TIDE_" + nqyname + "_0" + k + "_SECONDLTIME").val() + ",";
                        str_data += $("#TIDE_" + nqyname + "_0" + k + "_SECONDLHEIGHT").val() + ",";
                    }
                }

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //下午十三  小岛河  原下午五明泽闸三天潮汐预报 不变
            function submit_10(id) {
                var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#MZZ_01G_SJ_0" + i).val() + ",";
                    str_data += $("#MZZ_01G_CW_0" + i).val() + ",";
                    str_data += $("#MZZ_01D_SJ_0" + i).val() + ",";
                    str_data += $("#MZZ_01D_CW_0" + i).val() + ",";
                    str_data += $("#MZZ_02G_SJ_0" + i).val() + ",";
                    str_data += $("#MZZ_02G_CW_0" + i).val() + ",";
                    str_data += $("#MZZ_02D_SJ_0" + i).val() + ",";
                    str_data += $("#MZZ_02D_CW_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //下午十五 东营港   原下午十八 不变(42)
            function submit_42(id) {
                var str_data = "";
                for (var j = 1; j < 4; j++) {


                    str_data += $("#DY_01G_CS_0" + j).val() + ",";
                    str_data += $("#DY_01G_CG_0" + j).val() + ",";

                    str_data += $("#DY_01D_CS_0" + j).val() + ",";
                    str_data += $("#DY_01D_CG_0" + j).val() + ",";

                    str_data += $("#DY_02G_CS_0" + j).val() + ",";
                    str_data += $("#DY_02G_CG_0" + j).val() + ",";

                    str_data += $("#DY_02D_CS_0" + j).val() + ",";
                    str_data += $("#DY_02D_CG_0" + j).val() + ",";
                }

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
                //submitDYForeastNo();
            }
            //下午十五 东营港  东营埕岛预报编号
            //function submitDYForeastNo() {
            //    var str_data = "";
            //    var proyear = $('#ProYear').val() + ",";
            //    var prono = $('#ProNo').val() + ",";;

            //    str_data = proyear + prono;
            //    str_data = str_data.substring(0, str_data.length - 1);
            //    var data = { datas: str_data };
            //    //alert(str_data);
            //    submit_ajax(43, data);
            //}
            //下午十七  曹妃甸   原下午七南堡油田三天潮汐预报 不变(12)
            function submit_12(id) {
                var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#NBYT_01G_SJ_0" + i).val() + ",";
                    str_data += $("#NBYT_01G_CW_0" + i).val() + ",";
                    str_data += $("#NBYT_01D_SJ_0" + i).val() + ",";
                    str_data += $("#NBYT_01D_CW_0" + i).val() + ",";
                    str_data += $("#NBYT_02G_SJ_0" + i).val() + ",";
                    str_data += $("#NBYT_02G_CW_0" + i).val() + ",";
                    str_data += $("#NBYT_02D_SJ_0" + i).val() + ",";
                    str_data += $("#NBYT_02D_CW_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //不变的

            //信息提交
            function submit_23(id) {
                var str_data = "";
                str_data += $("#select_hour").val() + ",";
                str_data += $("#Fabudanwei").val() + ",";
                str_data += $("#Tel").val() + ",";
                //str_data += $("#Fabushijian").val() + ",";
                str_data += $("#Chuanzhen").val() + ",";
                //str_data += $("#Hailang").val() + ",";
                //str_data += $("#Chaoxi").val() + ",";
                //str_data += $("#Shuiwen").val() + ",";
                str_data += $('#uniform-Hailang span').attr("code") + ",";
                str_data += $('#uniform-Chaoxi span').attr("code") + ",";
                str_data += $('#uniform-Shuiwen span').attr("code") + ",";
                str_data += $("#HailangTel").val() + ",";
                str_data += $("#ChaoxiTel").val() + ",";
                str_data += $("#ShuiwenTel").val() + ",";
                //新添加的预报值班，预报发送
                str_data += $("#ZhibanTel").val() + ",";
                str_data += $("#SendTel").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(id, data);
            }

            //ajax公共方法(表单类型，post数组)
            function submit_ajax(types, datas1) {
                var text_color = "";
                var area_color = "";
                var ys_color = "";
                var divid = "ddlg_" + types;

                $("#" + divid + " input[type=text]").each(function () {
                    var x = $(this).css("border-color");
                    text_color == "rgb(255, 0, 0)" ? text_color = "rgb(255, 0, 0)" : text_color = $(this).css("border-color");
                });
                if (text_color == "rgb(255, 0, 0)") {
                    $.messager.confirm("操作提示", "存在非正常格式数据，是否确认提交？", function (data) {
                        if (data) {
                            submit_rtn(types, datas1);
                        }
                        else {
                            return false;
                        }
                    });
                } else {
                    submit_rtn(types, datas1);//23的提交
                }
            }

            function submit_rtn(types, datas1) {
                var date = myformatter(new Date($("#tianbaoriqi").datebox("getValue")));//控件时间 格式：2015-04-28
                $.ajax({
                    type: "POST",
                    url: "/Ajax/gettablelist.ashx?method=submit&type=" + types + "&date=" + date,
                    data: datas1,
                    beforeSend: function () {
                        $('#w').window('open');
                    },
                    success: function (result) {
                        // alert(result);
                        if (result == "editsuccess") {
                            $.messager.show({
                                title: '操作消息',
                                msg: '表单' + types + ' 修改成功',
                                showType: 'show'
                            });
                        } else if (result == "editerror") {
                            $.messager.show({
                                title: '操作消息',
                                msg: '表单' + types + ' 修改失败',
                                showType: 'fade',
                                style: {
                                    left: '',
                                    right: 0,
                                    bottom: ''
                                }
                            });
                        }
                        else if (result == "addsuccess") {
                            $.messager.show({
                                title: '操作消息',
                                msg: '表单' + types + ' 添加成功',
                                showType: 'show'
                            });
                        } else if (result == "adderror") {
                            $.messager.show({
                                title: '操作消息',
                                msg: '表单' + types + ' 添加失败',
                                showType: 'fade',
                                style: {
                                    left: '',
                                    right: 0,
                                    bottom: ''
                                }
                            });
                        } else if (result == "-1") {
                            top_url_login();//session为null，父窗体跳转到登陆页
                        }

                    },
                    complete: function () {
                        $('#w').window('close');
                    }
                });
            }
        }
        //↑表单提交（表单编号）↑ end

        //↓根据潮汐 风浪 水温 操作性↓
        {
            //潮汐dlg_02
            $('#yby_type').change(function () {
                // type_change();
            });
            //类型选择事件
            function type_change() {
                if ($("#yby_type").val() == "潮汐") {
                    all_disabled();
                    cx_isabled();//潮汐可编辑
                    tb_isabled();//填报信息可编辑
                }
                else if ($("#yby_type").val() == "风、海浪") {
                    all_isabled();
                    cx_disabled(); //潮汐不可编辑
                    sw_disabled();//水温不可编辑
                    tb_isabled();//填报信息可编辑
                }
                else if ($("#yby_type").val() == "水温") {
                    all_disabled();
                    sw_isabled();//水温可编辑
                    tb_isabled();//填报信息可编辑
                }
            }
            //填报信息可编辑
            function tb_isabled() {
                $("#dlg_23 :text").removeAttr("disabled");
                $("#ddlg_43 :text").removeAttr("disabled");
            }
            //潮汐可编辑
            function cx_isabled() {
                $("input[id*='SJ']").removeAttr("disabled");
                $("input[id*='CW']").removeAttr("disabled");
                $("input[id*='1D_']").removeAttr("disabled");
                $("input[id*='1G_']").removeAttr("disabled");
                $("input[id*='2D_']").removeAttr("disabled");
                $("input[id*='2G_']").removeAttr("disabled");
                $("input[id*='DC']").removeAttr("disabled");
                $("input[id*='GC']").removeAttr("disabled");
                $("input[id*='TIDE']").removeAttr("disabled");
                $("#ProSDYear").removeAttr("disabled");
                $("#ProSDNo").removeAttr("disabled");
            }
            //潮汐不可编辑
            function cx_disabled() {
                $("input[id*='SJ']").attr("disabled", "disabled");
                $("input[id*='CW']").attr("disabled", "disabled");
                $("input[id*='1D_']").attr("disabled", "disabled");
                $("input[id*='1G_']").attr("disabled", "disabled");
                $("input[id*='2D_']").attr("disabled", "disabled");
                $("input[id*='2G_']").attr("disabled", "disabled");
                $("input[id*='DC']").attr("disabled", "disabled");
                $("input[id*='GC']").attr("disabled", "disabled");
                $("input[id*='TIDE']").attr("disabled", "disabled");
            }
            //水温可编辑
            function sw_isabled() {
                $("input[id*='SW']").removeAttr("disabled");
                $("input[id*='ICE']").removeAttr("disabled");
                $("input[id*='TEMP']").removeAttr("disabled");
                $("#ProSDYear").removeAttr("disabled");
                $("#ProSDNo").removeAttr("disabled");
            }
            //水温不可编辑
            function sw_disabled() {
                $("input[id*='SW']").attr("disabled", "disabled");
                $("input[id*='TEMP']").attr("disabled", "disabled");
            }
            //都不可编辑
            function all_disabled() {
                $(":text").each(function () {//默认加载时textbox都不可编辑
                    this.disabled = true;
                });
                $("textarea").each(function () {//textarea都不可编辑
                    this.disabled = true;
                });
            }
            //都可编辑
            function all_isabled() {
                $(":text").each(function () {//默认加载时textbox都可编辑
                    //  this.removeAttr("disabled");
                    this.disabled = false;
                });
                $("textarea").each(function () {//textarea都可编辑
                    //  this.removeAttr("disabled");
                    this.disabled = false;
                });
            }
        }
        //↑根据潮汐 风浪 水温 操作性↑

        //小时选择事件
        $('#select_hour').change(function () {
            var sj = $("#Fabushijian").val();
            if (sj.length < 3) {

            } else {
                $("#Fabushijian").val(sj.split('日')[0] + "日" + $('#select_hour').val() + "时");
            }
        });

        //初始化加载
        $(function () {
            var curr_time = new Date();
            gettabledata(curr_time, "f");

            //默认基本信息
            $.ajax({//综合
                type: "POST",
                url: "/Ajax/gettablelist.ashx?method=getbaseinfo",
                success: function (data) {
                    var spstr = data.split(',');
                    $("#Fabudanwei").val(spstr[0]);
                    $("#Tel").val(spstr[1]);
                    $("#Chuanzhen").val(spstr[2]);
                }
            });

            $(".datebox :text").attr("readonly", "readonly");
            $("#tianbaoriqi").datebox("setValue", myformatter(curr_time));  //设置默认时间 当前时间

            $('#dlg_xzmb').dialog('close');//新建模板关闭
            $('#dlg_czrz').dialog('close');//操作日志关闭

            //  all_disabled();
            // type_change();
            //添加字数限制↓
            {
                $('#dlg_23 :text').attr("maxlength", "30");
                $('#Fabudanwei').attr("maxlength", "100");
            }
            //添加字数限制↑

            $(".textbox").css("width", "131px");
            $("#uniform-select_hour").css("height", "21px");
            $("#tbxx .textbox").css("width", "231px").css("height", "23px").css("margin-right", "6px");

            //加载日期 ‘时’ 
            var hour_str = "";
            for (var i = 0; i < 24; i++) {
                hour_str += "<option>" + i + "</option>";
            }
            $("#select_hour").append(hour_str);
            $("#select_hour").val(15);
            var today = new Date(); // 获取今天时间
            GetDate(today); //根据当前年月日 推算未来年月日
            var date = $("#tianbaoriqi").datebox("getValue");
            t = date.split('-')[0] + "年" + date.split('-')[1] + "月" + date.split('-')[2] + "日" + $('#select_hour').val() + "时";
            $("#Fabushijian").val(t);
        });//初始化加载       

        //设置类型的边框 有值则变橙色 无值灰色
        function setlxborder(id) {
            $("#lx_" + id).css("border", "5px solid #FB9338");
        }

        //初始化类型边框为灰色
        function Initlxborder() {
            for (var i = 0; i <= 22; i++) {
                $("#lx_" + i).css("border", "5px solid #ccc7c7").hover(function () {
                    //  
                    if ($(this).css('border') == "5px solid rgb(204, 199, 199)") {
                        $(this).css("border", "5px solid #FB9337");
                    }
                }, function () {

                    if (isnull(this.id.split('_')[1])) {

                    }
                    else {
                        $(this).css("border", "5px solid #ccc7c7");
                    }
                });
            }
        }

        //判断textbox是否有值
        function isnull(id) {

            var str;
            var str1 = false;

            if (id < 10) {
                str = "#ddlg_0" + id;
            } else {
                str = "#ddlg_" + id;
            }

            $(str + " input[type=text]").each(function () {
                if ($(this).val() != "") {
                    str1 = true;
                }
            });
            return str1;
        }

        //ajax 加载各表数据
        function gettabledata(date1, searchType) { //searchType 按填报日期还是预报日期查询 p:填报日期 f:预报日期

            var dates = myformatter(date1);
            $.ajax({//综合
                type: "POST",
                url: "/Ajax/gettablelist.ashx?method=getbydataPM&date=" + dates + "&searchtype=" + searchType,
                cache: false,
                beforeSend: function () {
                    $('#w').window('open');
                    $("#btn_select").attr({ disabled: "disabled" });
                    getPredicTideData(dates);
                },
                success: function (result) {
                    var resjson = JSON.parse(result);
                    for (var j = 0; j < resjson.length; j++) {
                        switch (resjson[j].type) {
                            //不需要修改的
                            case "t18": gettable18(resjson[j].children, date1); dlgclose("18"); break;//下午二，原先的下午12
                            case "t69": gettable69(resjson[j].children, date1); dlgclose("69"); break;//下午三,原先的下午37
                            case "t63": gettable63(resjson[j].children, date1); dlgclose("63"); break;//下午六,原先的下午31
                            case "t48": gettable48(resjson[j].children, date1); dlgclose("48"); break;//下午十,原先的下午20
                            case "t10": gettable10(resjson[j].children, date1); dlgclose("10"); break;//下午13,原先的下午5
                            case "t42": gettable42(resjson[j].children, date1); dlgclose("42"); break;//下午15,原先的下午18
                                break;
                            case "t43": gettable43(resjson[j], date1); dlgclose("43"); break;//东营埕岛的编号
                            case "t12": gettable12(resjson[j].children, date1); dlgclose("12"); break;//下午17,原先的下午7
                            case "t23": gettable23(resjson[j].children, date1); break;//信息提交

                                //下午三数据拆分的，潮时
                            case "t75": gettable75(resjson[j].children, date1); dlgclose("75"); break;
                            case "t76": gettable76(resjson[j].children, date1); dlgclose("76"); break;

                                //下午十六数据拆分的，潮时
                            case "t82": gettable82(resjson[j].children, date1); dlgclose("82"); break;

                            default:
                        }
                    }
                },
                complete: function () {
                    $('#w').window('close');
                    $("#btn_select").removeAttr("disabled");
                }
            });
        }
        //ajax 加载各表数据
        {
            //这里是读取数据库的填报数据
            //方法维持原样的有如下 start

            //下午二、青岛（小麦岛）   原下午十二 小麦岛72小时(18)
            function gettable18(resjson, date1) {

                for (var i = 0; i < resjson.length; i++) {
                    var stationName = "";
                    if (resjson[i].SEABEACH == "青岛市区")
                        stationName = "XMD";
                    else if (resjson[i].SEABEACH == "金沙滩")
                        stationName = "WMT";

                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#" + stationName + "_01G_H_00" + num).val(resjson[i].FIRSTHIGHTIME);
                    $("#" + stationName + "_01G_CG_00" + num).val(resjson[i].FIRSTHIGHLEVEL);
                    $("#" + stationName + "_02G_H_00" + num).val(resjson[i].SECONDHIGHTIME);
                    $("#" + stationName + "_02G_CG_00" + num).val(resjson[i].SECONDHEIGHTLEVEL);
                    $("#" + stationName + "_01D_H_00" + num).val(resjson[i].FIRSTLOWTIME);
                    $("#" + stationName + "_01D_CG_00" + num).val(resjson[i].FIRSTLOWLEVEL);
                    $("#" + stationName + "_02D_H_00" + num).val(resjson[i].SECONDLOWTIME);
                    $("#" + stationName + "_02D_CG_00" + num).val(resjson[i].SECONDLOWLEVEL);
                }
            }

            //下午三、青岛（董家口）   原下午三十七 董家口72小时潮汐预报 不变(69)
            function gettable69(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#DJKP_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
                    $("#DJKP_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
                    $("#DJKP_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
                    $("#DJKP_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
                    $("#DJKP_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
                    $("#DJKP_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
                    $("#DJKP_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
                    $("#DJKP_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
                }
            }
            //下午六 文登   原下午三十一 不变(63)
            function gettable63(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#WHXQ_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
                    $("#WHXQ_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
                    $("#WHXQ_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
                    $("#WHXQ_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
                    $("#WHXQ_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
                    $("#WHXQ_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
                    $("#WHXQ_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
                    $("#WHXQ_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
                }
            }
            //下午十  海洋牧场（威海） 原下午二十 不变(48)
            function gettable48(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    var nqyname = "";
                    var date = new Date(resjson[i].FORECASTDATE);
                    switch (resjson[i].OCEANRANCHNAME) {
                        case "寻山海洋牧场": nqyname = "CQ"; break;
                        case "荣成烟墩角游钓型海洋牧场": nqyname = "RC"; break;
                        case "西霞口集团国家级海洋牧场": nqyname = "XXK"; break;

                            //新增加7个
                        case "滨州正海底播型海洋牧场": nqyname = "BZZH"; break;
                        case "山东通和底播型海洋牧场": nqyname = "SDTH"; break;
                        case "山东莱州太平湾明波国家级海洋牧场": nqyname = "TPW"; break;
                        case "山东琵琶口富瀚国家级海洋牧场": nqyname = "PPK"; break;
                        case "山东庙岛群岛东部佳益国家级海洋牧场": nqyname = "MDQD"; break;
                        case "山东海州湾顺风国家级海洋牧场": nqyname = "HZW"; break;
                        case "山东岚山东部万泽丰国家级海洋牧场": nqyname = "LSDBWZF"; break;

                        default:
                    }

                    var value = "";
                    var num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;

                    for (var j = 0; j < 24; j++) {
                        if (j < 10) {
                            value = "TIDE24H0" + j;
                            $("#TIDE_" + nqyname + "_0" + num + "_H0" + j).val(resjson[i][value]);
                        }
                        else {
                            value = "TIDE24H" + j;
                            $("#TIDE_" + nqyname + "_0" + num + "_H" + j).val(resjson[i][value]);
                        }
                    }
                    $("#TIDE_" + nqyname + "_0" + num + "_FIRSTHTIME").val(resjson[i].TIDEFIRSTHTIME);
                    $("#TIDE_" + nqyname + "_0" + num + "_FIRSTHHEIGHT").val(resjson[i].TIDEFIRSTHHEIGHT);

                    $("#TIDE_" + nqyname + "_0" + num + "_SECONDHTIME").val(resjson[i].TIDESECONDHTIME);
                    $("#TIDE_" + nqyname + "_0" + num + "_SECONDHHEIGHT").val(resjson[i].TIDESECONDHHEIGHT);

                    $("#TIDE_" + nqyname + "_0" + num + "_FIRSTLTIME").val(resjson[i].TIDEFIRSTLTIME);
                    $("#TIDE_" + nqyname + "_0" + num + "_FIRSTLHEIGHT").val(resjson[i].TIDEFIRSTLHEIGHT);

                    $("#TIDE_" + nqyname + "_0" + num + "_SECONDLTIME").val(resjson[i].TIDESECONDLTIME);
                    $("#TIDE_" + nqyname + "_0" + num + "_SECONDLHEIGHT").val(resjson[i].TIDESECONDLHEIGHT);
                }
            }
            //下午十三  小岛河  原下午五明泽闸三天潮汐预报 不变(10)
            function gettable10(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].yb);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#MZZ_01G_CW_0" + num).val(resjson[i].g1c);
                    $("#MZZ_01G_SJ_0" + num).val(resjson[i].g1s);
                    $("#MZZ_01D_CW_0" + num).val(resjson[i].d1c);
                    $("#MZZ_01D_SJ_0" + num).val(resjson[i].d1s);
                    $("#MZZ_02G_CW_0" + num).val(resjson[i].g2c);
                    $("#MZZ_02G_SJ_0" + num).val(resjson[i].g2s);
                    $("#MZZ_02D_CW_0" + num).val(resjson[i].d2c);
                    $("#MZZ_02D_SJ_0" + num).val(resjson[i].d2s);
                }
            }
            //下午十五 东营港   原下午十八 不变 (42)
            function gettable42(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#DY_01G_CS_0" + num).val(resjson[i].NOTFFIRSTHIGHWAVETIME);
                    $("#DY_01G_CG_0" + num).val(resjson[i].NOTFFIRSTHIGHWAVEHEIGHT);
                    $("#DY_01D_CS_0" + num).val(resjson[i].NOTFFIRSTLOWWAVETIME);
                    $("#DY_01D_CG_0" + num).val(resjson[i].NOTFFIRSTLOWWAVEHEIGHT);
                    $("#DY_02G_CS_0" + num).val(resjson[i].NOTFSECONDHIGHWAVETIME);
                    $("#DY_02G_CG_0" + num).val(resjson[i].NOTFSECONDHIGHWAVEHEIGHT);
                    $("#DY_02D_CS_0" + num).val(resjson[i].NOTFSECONDLOWWAVETIME);
                    $("#DY_02D_CG_0" + num).val(resjson[i].NOTFSECONDLOWWAVEHEIGHT);
                }
            }
            //东营埕岛（新的下午15）的编号
            function gettable43(result, date1) {
                var effect = result.pbtime;
                var resjson = result.children;
                var proyear = resjson[0].PROYEAR;
                var prono = resjson[0].PRONO;
                if (effect == "today") {
                    $('#ProYear').val(proyear);
                    if (prono * 1 < 10) {
                        prono = "00" + prono * 1;
                    } else if (prono * 1 >= 10 && prono * 1 < 100) {
                        prono = "0" + prono * 1;
                    } else {
                        prono = prono;
                    }
                    $('#ProNo').val(prono);
                }
                else {
                    $('#ProYear').val(proyear);
                    prono = prono * 1 + 1;
                    if (prono * 1 < 10) {
                        prono = "00" + prono * 1;
                    } else if (prono * 1 > 10 && prono * 1 < 100) {
                        prono = "0" + prono * 1;
                    } else {
                        prono = prono;
                    }
                    $('#ProNo').val(prono);
                }
            }
            //下午十七  曹妃甸   原下午七南堡油田三天潮汐预报 不变(12)
            function gettable12(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#NBYT_01G_CW_0" + num).val(resjson[i].NOTFFIRSTHIGHWAVEHEIGHT);
                    $("#NBYT_01G_SJ_0" + num).val(resjson[i].NOTFFIRSTHIGHWAVETIME);
                    $("#NBYT_01D_CW_0" + num).val(resjson[i].NOTFFIRSTLOWWAVEHEIGHT);
                    $("#NBYT_01D_SJ_0" + num).val(resjson[i].NOTFFIRSTLOWWAVETIME);
                    $("#NBYT_02G_CW_0" + num).val(resjson[i].NOTFSECONDHIGHWAVEHEIGHT);
                    $("#NBYT_02G_SJ_0" + num).val(resjson[i].NOTFSECONDHIGHWAVETIME);
                    $("#NBYT_02D_CW_0" + num).val(resjson[i].NOTFSECONDLOWWAVEHEIGHT);
                    $("#NBYT_02D_SJ_0" + num).val(resjson[i].NOTFSECONDLOWWAVETIME);
                }
            }
            //填报信息提交表单   
            function gettable23(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#Fabudanwei").val(resjson[i].fb);
                    $("#Tel").val(resjson[i].dh);
                    $("#Chuanzhen").val(resjson[i].cz);
                    $("#ZhibanTel").val(resjson[i].zhibantel);
                    $("#SendTel").val(resjson[i].sendtel);
                    if (resjson[i].cx != "" && resjson[i].cx != null) {
                        $("#uniform-Chaoxi span").text(resjson[i].cx);
                        $("#uniform-Chaoxi span").attr("code", resjson[i].cx);
                        $("#ChaoxiTel").val(resjson[i].cxtel);
                    }
                    if (resjson[i].sw != "" && resjson[i].sw != null) {
                        $("#uniform-Shuiwen span").text(resjson[i].sw);
                        $("#uniform-Shuiwen span").attr("code", resjson[i].sw);
                        $("#ShuiwenTel").val(resjson[i].swtel);
                    }
                    if (resjson[i].hl != "" && resjson[i].hl != null) {
                        $("#uniform-Hailang span").text(resjson[i].hl);
                        $("#uniform-Hailang span").attr("code", resjson[i].hl);
                        $("#HailangTel").val(resjson[i].hltel);
                    }
                }
            }
            //方法维持原样的有如上 end

            //表单75,76,77,78,79,80,81,潮汐数据查询到同一个方法里面并按地区显示
            //潮时数据
            function gettable75(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    switch (resjson[i].SDOSCTCITY) {
                        case "日照": nqyname = "RZ"; break;
                        case "青岛": nqyname = "QD"; break;
                        case "威海": nqyname = "WH"; break;
                        case "烟台": nqyname = "YT"; break;
                        case "潍坊": nqyname = "WF"; break;
                        case "东营": nqyname = "DY"; break;
                        case "滨州": nqyname = "BZ"; break;
                        default:
                    }
                    $("#" + nqyname + "_01G_H_00" + num).val(resjson[i].SDOSCTFIRSTHIGHWAVEHOUR + resjson[i].SDOSCTFIRSTHIGHWAVEMINUTE);
                    //$("#" + nqyname + "_01G_MIN").val(resjson[i].SDOSCTFIRSTHIGHWAVEMINUTE);
                    $("#" + nqyname + "_01D_H_00" + num).val(resjson[i].SDOSCTFIRSTLOWWAVEHOUR + resjson[i].SDOSCTFIRSTLOWWAVEMINUTE);
                    //$("#" + nqyname + "_01D_MIN").val(resjson[i].SDOSCTFIRSTLOWWAVEMINUTE);
                    $("#" + nqyname + "_02G_H_00" + num).val(resjson[i].SDOSCTSECONDHIGHWAVEHOUR + resjson[i].SDOSCTSECONDHIGHWAVEMINUTE);
                    //$("#" + nqyname + "_02G_MIN").val(resjson[i].SDOSCTSECONDHIGHWAVEMINUTE);
                    $("#" + nqyname + "_02D_H_00" + num).val(resjson[i].SDOSCTSECONDLOWWAVEHOUR + resjson[i].SDOSCTSECONDLOWWAVEMINUTE);
                    //$("#" + nqyname + "_02D_MIN").val(resjson[i].SDOSCTSECONDLOWWAVEMINUTE);
                }
            }

            //表单75,76,77,78,79,80,81,潮汐数据查询到同一个方法里面并按地区显示
            //潮高数据
            function gettable76(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    switch (resjson[i].SDOSCTCITY) {
                        case "日照": nqyname = "RZ"; break;
                        case "青岛": nqyname = "QD"; break;
                        case "威海": nqyname = "WH"; break;
                        case "烟台": nqyname = "YT"; break;
                        case "潍坊": nqyname = "WF"; break;
                        case "东营": nqyname = "DY"; break;
                        case "滨州": nqyname = "BZ"; break;
                        default:
                    }
                    $("#" + nqyname + "_01G_CG_00" + num).val(resjson[i].FIRSTHIGHWAVETIDEDATA);
                    $("#" + nqyname + "_01D_CG_00" + num).val(resjson[i].FIRSTLOWWAVETIDEDATA);
                    $("#" + nqyname + "_02G_CG_00" + num).val(resjson[i].SECONDHIGHWAVETIDEDATA);
                    $("#" + nqyname + "_02D_CG_00" + num).val(resjson[i].SECONDLOWWAVETIDEDATA);
                }
            }

            //下午5,7,8 都是从原先下午16（表单22）的取数据
            function gettable82(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].qy) {
                        case "石岛": nqyname = "SD"; break;
                        case "成山头": nqyname = "CST"; break;
                        case "乳山": nqyname = "RS"; break;
                        default:
                            nqyname = ""; break;
                    }
                    date = new Date(resjson[i].yb);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#" + nqyname + "_01G_CW_0" + num).val(resjson[i].g1c);
                    $("#" + nqyname + "_01G_SJ_0" + num).val(resjson[i].g1s);
                    $("#" + nqyname + "_01D_CW_0" + num).val(resjson[i].d1c);
                    $("#" + nqyname + "_01D_SJ_0" + num).val(resjson[i].d1s);
                    $("#" + nqyname + "_02G_CW_0" + num).val(resjson[i].g2c);
                    $("#" + nqyname + "_02G_SJ_0" + num).val(resjson[i].g2s);
                    $("#" + nqyname + "_02D_CW_0" + num).val(resjson[i].d2c);
                    $("#" + nqyname + "_02D_SJ_0" + num).val(resjson[i].d2s);
                }
            }


        }


        <%------------------------------------  获取潮汐数据 这部分内容没有改动维持原有-----------------------------------------%>

        {
            function procTideData(stationCode, i, tide) {
                var index, s_01G_SJ, s_01G_CW, s_01D_SJ, s_01D_CW, s_02G_SJ, s_02G_CW, s_02D_SJ, s_02D_CW;
                index = i + 1;
                s_01G_SJ = "#" + stationCode + "_01G_SJ_0" + index;
                s_01G_CW = "#" + stationCode + "_01G_CW_0" + index;
                s_01D_SJ = "#" + stationCode + "_01D_SJ_0" + index;
                s_01D_CW = "#" + stationCode + "_01D_CW_0" + index;
                s_02G_SJ = "#" + stationCode + "_02G_SJ_0" + index;
                s_02G_CW = "#" + stationCode + "_02G_CW_0" + index;
                s_02D_SJ = "#" + stationCode + "_02D_SJ_0" + index;
                s_02D_CW = "#" + stationCode + "_02D_CW_0" + index;
                $(s_01G_SJ).val(tide.FSTHIGHWIDETIME.replace(":", ""));
                $(s_01G_CW).val(tide.FSTHIGHWIDEHEIGHT);
                $(s_01D_SJ).val(tide.FSTLOWWIDETIME.replace(":", ""));
                $(s_01D_CW).val(tide.FSTLOWWIDEHEIGHT);
                $(s_02G_SJ).val(tide.SCDHIGHWIDETIME.replace(":", ""));
                $(s_02G_CW).val(tide.SCDHIGHWIDEHEIGHT);
                $(s_02D_SJ).val(tide.SCDLOWWIDETIME.replace(":", ""));
                $(s_02D_CW).val(tide.SCDLOWWIDEHEIGHT);
            }
            //下午四，青岛24小时天文潮数据
            function procQD24TideData(tide) {
                var fstHTime, fstLTime, scdHTime, scdLTime;
                fstHTime = tide.FSTHIGHWIDETIME;
                fstLTime = tide.FSTLOWWIDETIME;
                scdHTime = tide.SCDHIGHWIDETIME;
                scdLTime = tide.SCDLOWWIDETIME;
                if (fstHTime.indexOf("-") == -1) {
                    var arr = fstHTime.split(":");
                    $("#01GC_H").val(arr[0]);
                    $("#01GC_MIN").val(arr[1]);
                    $("#01GC_CM").val(tide.FSTHIGHWIDEHEIGHT);
                }
                else {
                    $("#01GC_H").val("-");
                    $("#01GC_MIN").val("-");
                    $("#01GC_CM").val(tide.FSTHIGHWIDEHEIGHT);
                }
                if (fstLTime.indexOf("-") == -1) {
                    var arr = fstLTime.split(":");
                    $("#01DC_H").val(arr[0]);
                    $("#01DC_MIN").val(arr[1]);
                    $("#01DC_CM").val(tide.FSTLOWWIDEHEIGHT);
                }
                else {
                    $("#01DC_H").val("-");
                    $("#01DC_MIN").val("-");
                    $("#01DC_CM").val(tide.FSTLOWWIDEHEIGHT);
                }
                if (scdHTime.indexOf("-") == -1) {
                    var arr = scdHTime.split(":");
                    $("#02GC_H").val(arr[0]);
                    $("#02GC_MIN").val(arr[1]);
                    $("#02GC_CM").val(tide.SCDHIGHWIDEHEIGHT);
                }
                else {
                    $("#02GC_H").val("-");
                    $("#02GC_MIN").val("-");
                    $("#02GC_CM").val(tide.SCDHIGHWIDEHEIGHT);
                }
                if (scdLTime.indexOf("-") == -1) {
                    var arr = scdLTime.split(":");
                    $("#02DC_H").val(arr[0]);
                    $("#02DC_MIN").val(arr[1]);
                    $("#02DC_CM").val(tide.SCDLOWWIDEHEIGHT);
                }
                else {
                    $("#02DC_H").val("-");
                    $("#02DC_MIN").val("-");
                    $("#02DC_CM").val(tide.SCDLOWWIDEHEIGHT);
                }
            }

            function proc7City24TideData(i, tide) {
                if (i % 3 == 0) {
                    i = 3;
                }
                else if (i % 3 == 1) {
                    i = 1;
                }
                else if (i % 3 == 2) {
                    i = 2;
                }
                //for (var k = 1; k < 4; k++) {
                var matchValue, stationName, s_01G_H, s_01G_MIN, s_01D_H, s_01D_MIN, s_02G_H, s_02G_MIN, s_02D_H, s_02D_MIN, fstHTime, fstLTime, scdHTime, scdLTime, fstHValue, fstLValue, scdHValue, scdLValue, s_01G_CG, s_01D_CG, s_02G_CG, s_02D_CG;
                matchValue = tide.STATION;
                stationName = matchValue === "101wmt" ? "QD" : matchValue === "104rzh" ? "RZ" : matchValue === "109wh" ? "WH" : matchValue === "111zfd" ? "YT" : matchValue === "114wfg" ? "WF" : matchValue === "119hhg" ? "DY" : matchValue === "125bzg" ? "BZ" : "";


                s_01G_H = "#" + stationName + "_01G_H_00" + i;
                s_01D_H = "#" + stationName + "_01D_H_00" + i;
                s_02G_H = "#" + stationName + "_02G_H_00" + i;
                s_02D_H = "#" + stationName + "_02D_H_00" + i;

                s_01G_CG = "#" + stationName + "_01G_CG_00" + i;
                s_01D_CG = "#" + stationName + "_01D_CG_00" + i;
                s_02G_CG = "#" + stationName + "_02G_CG_00" + i;
                s_02D_CG = "#" + stationName + "_02D_CG_00" + i;

                fstHTime = tide.FSTHIGHWIDETIME;
                fstLTime = tide.FSTLOWWIDETIME;
                scdHTime = tide.SCDHIGHWIDETIME;
                scdLTime = tide.SCDLOWWIDETIME;

                fstHValue = tide.FSTHIGHWIDEHEIGHT;
                fstLValue = tide.FSTLOWWIDEHEIGHT;
                scdHValue = tide.SCDHIGHWIDEHEIGHT;
                scdLValue = tide.SCDLOWWIDEHEIGHT;

                $(s_01G_CG).val(fstHValue);
                $(s_01D_CG).val(fstLValue);
                $(s_02G_CG).val(scdHValue);
                $(s_02D_CG).val(scdLValue);

                if (fstHTime.indexOf("-") == -1) {
                    var arr = fstHTime.split(":");
                    $(s_01G_H).val(arr[0] + arr[1]);
                    //$(s_01G_MIN).val(arr[1]);
                }
                else {
                    $(s_01G_H).val("--");
                    //$(s_01G_MIN).val("-");
                }
                if (fstLTime.indexOf("-") == -1) {
                    var arr = fstLTime.split(":");
                    $(s_01D_H).val(arr[0] + arr[1]);
                    //$(s_01D_MIN).val(arr[1]);
                }
                else {
                    $(s_01D_H).val("--");
                    //$(s_01D_MIN).val("-");
                }
                if (scdHTime.indexOf("-") == -1) {
                    var arr = scdHTime.split(":");
                    $(s_02G_H).val(arr[0] + arr[1]);
                    //$(s_02G_MIN).val(arr[1]);
                }
                else {
                    $(s_02G_H).val("--");
                    //$(s_02G_MIN).val("-");
                }
                if (scdLTime.indexOf("-") == -1) {
                    var arr = scdLTime.split(":");
                    $(s_02D_H).val(arr[0] + arr[1]);
                    //$(s_02D_MIN).val(arr[1]);
                }
                else {
                    $(s_02D_H).val("--");
                    //$(s_02D_MIN).val("-");
                }
                //}
            }


            function getTideDataWithParams(stationsStr, dayCountInt, procTideDataCallBack, date1) {
                $.ajax({
                    type: "POST",
                    url: "Ajax/GetTideData.ashx",
                    data: { stations: stationsStr, dayCount: dayCountInt, date: date1 },
                    dataType: "json",
                    success: function (resp) {
                        procTideDataCallBack(resp);
                    },
                    error: function () {
                        //alert("潮汐数据加载失败");
                    }
                });

            }

            function getSD7City24HTideData(date1) {
                var stationsStr = "'101wmt','104rzh','109wh','111zfd','114wfg','119hhg','125bzg'";
                var procCallBack = function (data) {
                    $(data).each(function (i, value) {
                        proc7City24TideData(i + 1, value);
                    });
                }
                // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }

            function getQD24HTideData(date1) {
                var stationsStr = "'101wmt'";
                var procCallBack = function (data) {
                    $(data).each(function (i, value) {
                        procQD24TideData(value);
                    });
                }
                //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 1, procCallBack, date1);
            }

            function getMZZ72HTideData(date1) {
                var stationsStr = "'117xdh'";
                var procCallBack = function (data) {
                    $(data).each(function (i, value) {
                        procTideData("MZZ", i, value);
                    });
                }
                //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }

            function getNBYT72HTideData(date1) {
                var stationsStr = "'128cfd'";
                var procCallBack = function (data) {
                    $(data).each(function (i, value) {
                        procTideData("NBYT", i, value);
                    });
                }
                //  var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }

            function getWFG24HTideData(date1) {
                var stationsStr = "'114wfg'";
                var procCallBack = function (data) {
                    $(data).each(function (i, value) {
                        $('#WFG_GCCS_01').val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $('#WFG_GCCS_02').val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $('#WFG_GCCG_01').val(value.FSTHIGHWIDEHEIGHT);
                        $('#WFG_GCCG_02').val(value.SCDHIGHWIDEHEIGHT);
                        $('#WFG_DCCS_01').val(value.FSTLOWWIDETIME.replace(":", ""));
                        $('#WFG_DCCS_02').val(value.SCDLOWWIDETIME.replace(":", ""));
                        $('#WFG_DCCG_01').val(value.FSTLOWWIDEHEIGHT);
                        $('#WFG_DCCG_02').val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 1, procCallBack, date1);
            }

            function getQDBathing24HTideData(date1) {
                var stationsStr = "'101wmt','102xmd'";
                var num = 0;
                var procCallBack = function (data) {
                    $(data).each(function (i, value) {
                        var StationName = "";
                        //if (value.STATION == "101wmt")
                        //    StationName = "WMT";
                        //else
                        if (value.STATION == "102xmd")
                            StationName = "XMD";
                        if (i % 3 == 0) {
                            num = 1;
                        }
                        else if (i % 3 == 1) {
                            num = 2;
                        }
                        else if (i % 3 == 2) {
                            num = 3;
                        }
                        var FSTHIGHWIDETIME = getHourMinute(value.FSTHIGHWIDETIME);
                        var SCDHIGHWIDETIME = getHourMinute(value.SCDHIGHWIDETIME);
                        var FSTLOWWIDETIME = getHourMinute(value.FSTLOWWIDETIME);
                        var SCDLOWWIDETIME = getHourMinute(value.SCDLOWWIDETIME);
                        $("#" + StationName + "_01G_H_00" + num).val(FSTHIGHWIDETIME);
                        $("#" + StationName + "_01G_CG_00" + num).val(value.FSTHIGHWIDEHEIGHT);
                        $("#" + StationName + "_02G_H_00" + num).val(SCDHIGHWIDETIME);
                        $("#" + StationName + "_02G_CG_00" + num).val(value.SCDHIGHWIDEHEIGHT);
                        $("#" + StationName + "_01D_H_00" + num).val(FSTLOWWIDETIME);
                        $("#" + StationName + "_01D_CG_00" + num).val(value.FSTLOWWIDEHEIGHT);
                        $("#" + StationName + "_02D_H_00" + num).val(SCDLOWWIDETIME);
                        $("#" + StationName + "_02D_CG_00" + num).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                // var  = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }

            function getHourMinute(str) {
                var time = "";
                if (str.indexOf("-") == -1) {
                    var arr = str.split(":");
                    time = arr[0] + arr[1];
                }
                else {
                    time = "--";
                }
                return time;
            }

            function getQDCoast48HTideData(date1) {
                var stationsStr = "'101wmt'";
                var procCallBack = function (data) {
                    $(data).each(function (i, value) {
                        var fstHTime = value.FSTHIGHWIDETIME;
                        var fstLTime = value.FSTLOWWIDETIME;
                        var scdHTime = value.SCDHIGHWIDETIME;
                        var scdLTime = value.SCDLOWWIDETIME;
                        if (fstHTime.indexOf("-") == -1) {
                            var arr = fstHTime.split(":");
                            $("#01GC_H20").val(arr[0]);
                            $("#01GC_MIN20").val(arr[1]);
                            $("#01GC_CM20").val(value.FSTHIGHWIDEHEIGHT);
                        }
                        else {
                            $("#01GC_H20").val("-");
                            $("#01GC_MIN20").val("-");
                            $("#01GC_CM20").val(value.FSTHIGHWIDEHEIGHT);
                        }
                        if (fstLTime.indexOf("-") == -1) {
                            var arr = fstLTime.split(":");
                            $("#01DC_H20").val(arr[0]);
                            $("#01DC_MIN20").val(arr[1]);
                            $("#01DC_CM20").val(value.FSTLOWWIDEHEIGHT);
                        }
                        else {
                            $("#01DC_H20").val("-");
                            $("#01DC_MIN20").val("-");
                            $("#01DC_CM20").val(value.FSTLOWWIDEHEIGHT);
                        }
                        if (scdHTime.indexOf("-") == -1) {
                            var arr = scdHTime.split(":");
                            $("#02GC_H20").val(arr[0]);
                            $("#02GC_MIN20").val(arr[1]);
                            $("#02GC_CM20").val(value.SCDHIGHWIDEHEIGHT);
                        }
                        else {
                            $("#02GC_H20").val("-");
                            $("#02GC_MIN20").val("-");
                            $("#02GC_CM20").val(value.SCDHIGHWIDEHEIGHT);
                        }
                        if (scdLTime.indexOf("-") == -1) {
                            var arr = scdLTime.split(":");
                            $("#02DC_H20").val(arr[0]);
                            $("#02DC_MIN20").val(arr[1]);
                            $("#02DC_CM20").val(value.SCDLOWWIDEHEIGHT);
                        }
                        else {
                            $("#02DC_H20").val("-");
                            $("#02DC_MIN20").val("-");
                            $("#02DC_CM20").val(value.SCDLOWWIDEHEIGHT);
                        }


                    });
                }
                //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 2, procCallBack, date1);
            }

            function getWH48HTideData(date1) {
                var stationsStr = "'109wh','107sdo','106wdn','108cst','105nhd'";
                var procCallBack = function (data) {
                    $(data).each(function (i, value) {
                        var StationName = "";
                        if (value.STATION == "109wh")
                            StationName = "WH";
                        else if (value.STATION == "107sdo")
                            StationName = "SD";
                        else if (value.STATION == "106wdn")
                            StationName = "WD";
                        else if (value.STATION == "108cst")
                            StationName = "CST";
                        else if (value.STATION == "105nhd")
                            StationName = "RS";
                        if (i % 2 == 0) i = 1;
                        else i = 2;
                        if (StationName == "RS" && i == 1) {
                            return true;
                        }
                        var S_01G_SJ = "#" + StationName + "_01G_SJ_0" + i;
                        var S_01G_CW = "#" + StationName + "_01G_CW_0" + i;
                        var S_01D_SJ = "#" + StationName + "_01D_SJ_0" + i;
                        var S_01D_CW = "#" + StationName + "_01D_CW_0" + i;
                        var S_02G_SJ = "#" + StationName + "_02G_SJ_0" + i;
                        var S_02G_CW = "#" + StationName + "_02G_CW_0" + i;
                        var S_02D_SJ = "#" + StationName + "_02D_SJ_0" + i;
                        var S_02D_CW = "#" + StationName + "_02D_CW_0" + i;
                        $(S_01G_SJ).val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $(S_01G_CW).val(value.FSTHIGHWIDEHEIGHT);
                        $(S_01D_SJ).val(value.FSTLOWWIDETIME.replace(":", ""));
                        $(S_01D_CW).val(value.FSTLOWWIDEHEIGHT);
                        $(S_02G_SJ).val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $(S_02G_CW).val(value.SCDHIGHWIDEHEIGHT);
                        $(S_02D_SJ).val(value.SCDLOWWIDETIME.replace(":", ""));
                        $(S_02D_CW).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 2, procCallBack, date1);
            }


            //东营埕岛-未来三天高/低潮预报
            function getDYTide(date1) {
                var stationsStr = "'136dyg'";
                var procCallBack = function (data) {
                    $(data).each(function (j, value) {
                        i = j + 1;
                        $('#DY_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $('#DY_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
                        $('#DY_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
                        $('#DY_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

                        $('#DY_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $('#DY_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
                        $('#DY_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
                        $('#DY_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);

            }

            function getPredicTideData(date1) {

                //      <!--表单07.山东省近海七市24小时潮汐预报-->
                getSD7City24HTideData(date1);
                //      <!--表单08.青岛24小时潮位预报-->
                getQD24HTideData(date1);
                //      <!--表单10.明泽闸潮位预报-->
                getMZZ72HTideData(date1);
                //      <!--表单12.南堡油田海域潮汐预报-->

                getNBYT72HTideData(date1);

                //      <!--表单16.潍坊港24小时潮汐预报-->
                //getWFG24HTideData(date1);
                //      <!--表单18.青岛海水浴场24小时潮汐预报-->
                getQDBathing24HTideData(date1);
                //      <!--表单20.青岛沿岸48小时潮汐预报-->
                getQDCoast48HTideData(date1);
                //      <!--表单22.潮汐预报-->
                getWH48HTideData(date1);

                //下午二十二、 东营埕岛-未来三天高/低潮预报
                getDYTide(date1);


                SDdyguangli(date1);
                SDrztaohuadao(date1);
                SDwfdujiaqu(date1);
                SDwhxinqu(date1);
                SDytqingquan(date1);
                SDdjk(date1);
                SDdyyugang(date1);
            }
            //$(function () {
            //    getPredicTideData();
            //}); 

            //山东七地市
            //下午二十四、东营广利渔港-未来三天高/低潮预报
            function SDdyguangli(date1) {
                var stationsStr = "'117xdh'";
                var procCallBack = function (data) {
                    $(data).each(function (j, value) {
                        i = j + 1;
                        $('#DYGL_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $('#DYGL_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
                        $('#DYGL_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
                        $('#DYGL_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

                        $('#DYGL_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $('#DYGL_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
                        $('#DYGL_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
                        $('#DYGL_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }
            //下午二十七、日照桃花岛-未来三天高/低潮预报
            function SDrztaohuadao(date1) {
                var stationsStr = "'104rzh'";
                var procCallBack = function (data) {
                    $(data).each(function (j, value) {
                        i = j + 1;
                        $('#RZTHD_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $('#RZTHD_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
                        $('#RZTHD_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
                        $('#RZTHD_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

                        $('#RZTHD_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $('#RZTHD_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
                        $('#RZTHD_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
                        $('#RZTHD_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }
            //下午三十、潍坊度假区-未来三天高/低潮预报
            function SDwfdujiaqu(date1) {
                var stationsStr = "'114wfg'";
                var procCallBack = function (data) {
                    $(data).each(function (j, value) {
                        i = j + 1;
                        $('#WFDJQ_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $('#WFDJQ_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
                        $('#WFDJQ_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
                        $('#WFDJQ_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

                        $('#WFDJQ_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $('#WFDJQ_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
                        $('#WFDJQ_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
                        $('#WFDJQ_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }
            //下午三十三、威海新区-未来三天高/低潮预报
            function SDwhxinqu(date1) {
                var stationsStr = "'106wdn'";
                var procCallBack = function (data) {
                    $(data).each(function (j, value) {
                        i = j + 1;
                        $('#WHXQ_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $('#WHXQ_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
                        $('#WHXQ_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
                        $('#WHXQ_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

                        $('#WHXQ_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $('#WHXQ_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
                        $('#WHXQ_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
                        $('#WHXQ_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }
            //下午三十六、烟台清泉-未来三天高/低潮预报
            function SDytqingquan(date1) {
                var stationsStr = "'111zfd'";
                var procCallBack = function (data) {
                    $(data).each(function (j, value) {
                        i = j + 1;
                        $('#YTQQ_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $('#YTQQ_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
                        $('#YTQQ_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
                        $('#YTQQ_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

                        $('#YTQQ_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $('#YTQQ_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
                        $('#YTQQ_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
                        $('#YTQQ_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }
            //下午三十九、董家口-未来三天高/低潮预报
            function SDdjk(date1) {
                var stationsStr = "'139djk'";
                var procCallBack = function (data) {
                    $(data).each(function (j, value) {
                        i = j + 1;
                        $('#DJKP_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $('#DJKP_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
                        $('#DJKP_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
                        $('#DJKP_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

                        $('#DJKP_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $('#DJKP_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
                        $('#DJKP_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
                        $('#DJKP_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }
            //下午四十二、东营渔港-未来三天高/低潮预报
            function SDdyyugang(date1) {
                var stationsStr = "'101wmt'";
                var procCallBack = function (data) {
                    $(data).each(function (j, value) {
                        i = j + 1;
                        $('#DYFP_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
                        $('#DYFP_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
                        $('#DYFP_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
                        $('#DYFP_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

                        $('#DYFP_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
                        $('#DYFP_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
                        $('#DYFP_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
                        $('#DYFP_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
                    });
                }
                // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
                getTideDataWithParams(stationsStr, 3, procCallBack, date1);
            }
        }
<%--======================================================================================================--%>



        //查询按钮点击事件
        $("#btn_select").click(function () {

            // $(".panel window :text").val("");ddlg_01
            $('#dlg :text').val("");
            var str = "";
            for (var i = 1; i <= 23; i++) {
                if (i < 10) {
                    str = "#ddlg_0" + i;
                } else {
                    str = "#ddlg_" + i;
                }
                $(str + " :text").val("");
                $(str + " textarea").val("");

            }
            Initlxborder();//初始化类型边框为灰色

            var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
            var d = new Date(date1);
            var date = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();
            var dates1 = myformatter(date1);
            getPredicTideData(dates1);
            gettabledata(date1, "p");




            //var date = new Date($("#tianbaoriqi").datebox("getValue"));
            //gettabledata(date, "p");
            //只能修改当前天的数据
            quanxian(type, date);

        }); //查询按钮点击事件

        //表单类型点击事件
        $("#leixing1 li").click(function () {
            //  $("#leixing1 li").eq($(this).index()).css("border", "5px solid #FB9337");
            //$("#swbd").css("border", "5px solid #999999");
            //$("#xwbd").css("border", "5px solid #999999");
        })//表单类型点击事件

        //↓表单类型关闭事件↓并判断是否有数据
        function dlgclose(id) {
            var str;
            if (id == "1") {
                str = "#ddlg_01";
            }
            else if (id < 10) {
                str = "#ddlg_0" + id;
            } else {
                str = "#ddlg_" + id;
            }
            //  $(str).dialog('close');
            if (isnull(id)) {

                //  $("#lx_" + id).css("border", "5px solid #FB9337");

                for (var i in cx_arry) {
                    if (id.toString() == cx_arry[i].toString()) {
                        $("#lx_" + id).css("border", "5px solid #FB9337");
                    }
                }

                for (var i in fl_arry) {
                    if (id.toString() == fl_arry[i].toString()) {
                        $("#lx_" + id).css("border", "5px solid #ffff00");
                    }
                }
                for (var i in sw_arry) {
                    if (id.toString() == sw_arry[i].toString()) {
                        $("#lx_" + id).css("border", "5px solid #CCFF33");
                    }
                }

            }
            else {
                $("#lx_" + id).css("border", "5px solid #ccc7c7");
            }
        }


        //边框颜色
        function bordercolor(id) {
            if (isnull("" + id)) {
                $("#lx_" + id).css("border", "5px solid #FB9337");
            }
            else {
                $("#lx_" + id).css("border", "5px solid #ccc7c7");
            }
        }


        $('#dlg_xzmb').dialog({
            onClose: function () {
                //  $('#checkmodel').val('选择模版(' + $('#modellist .checked').length + ')');
            }
        });//↑表单类型关闭事件↑

        //根据当前年月日 推算未来年月日
        function GetDate(today) {
            var ds0 = (today.getMonth() + 1) + "月" + today.getDate() + "日";
            today.setDate(today.getDate() + 1);
            $('.SJ_01').text("" + (today.getMonth() + 1) + "月" + today.getDate() + "日");
            var ds1 = "" + (today.getMonth() + 1) + "月" + today.getDate() + "日";
            $('.SJ_0001MO').text(ds0 + "08时至" + ds1 + "08时");
            $('.SJ_0001XW').text(ds0 + "20时至" + ds1 + "20时");

            //*月*号08时至*月*号08时
            today.setDate(today.getDate() + 1);
            $('.SJ_02').text("" + (today.getMonth() + 1) + "月" + today.getDate() + "日");
            var ds2 = (today.getMonth() + 1) + "月" + today.getDate() + "日";
            $('.SJ_0102MO').text(ds1 + "08时至" + ds2 + "08时");
            $('.SJ_0102XW').text(ds1 + "20时至" + ds2 + "20时");

            today.setDate(today.getDate() + 1);
            $('.SJ_03').text("" + (today.getMonth() + 1) + "月" + today.getDate() + "日");
            var ds3 = (today.getMonth() + 1) + "月" + today.getDate() + "日";
            $('.SJ_0203MO').text(ds2 + "08时至" + ds3 + "08时");
            $('.SJ_0203XW').text(ds2 + "20时至" + ds3 + "20时");

            today.setDate(today.getDate() + 1);
            $('.SJ_04').text("" + (today.getMonth() + 1) + "月" + today.getDate() + "日");
            today.setDate(today.getDate() + 1);
            $('.SJ_05').text("" + (today.getMonth() + 1) + "月" + today.getDate() + "日");
            today.setDate(today.getDate() + 1);
            $('.SJ_06').text("" + (today.getMonth() + 1) + "月" + today.getDate() + "日");
            today.setDate(today.getDate() + 1);
            $('.SJ_07').text("" + (today.getMonth() + 1) + "月" + today.getDate() + "日");


            // today.setDate(today.getDate() + 7); // 系统会自动转换
            //下面是date类提供的三个你可能生成字符串用到的函数：
            //getDate() 从 Date 对象返回一个月中的某一天 (1 ~ 31).
            //getMonth() 从 Date 对象返回月份 (0 ~ 11).
            // getFullYear() 从 Date 对象以四位数字返回年份.
        }//根据当前年月日 推算未来年月日

        //填报日期修改事件
        $('#tianbaoriqi').datebox({
            onSelect: function (date) {
                //  $("#receive_date").datebox('setValue', dateAdd(date, 0));
                // $("#expected_report_date").datebox('setValue', dateAdd(date, 28));
                var Time = $("#tianbaoriqi").datebox("getValue");
                var selectDate = new Date(Time);
                //show72hOr7d(selectDate);
                //show72hOr7dForPortTide(selectDate);
                GetDate(selectDate);
            }
        }); //填报日期修改事件

        //判断时间是否符合要求
        function isDate(dateString) {
            if (dateString.trim() == "") return true;
            var r = dateString.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
            if (r == null) {
                // alert("请输入格式正确的日期\n\r日期格式：yyyy-mm-dd\n\r例    如：2008-08-08\n\r");
                return false;
            }
            var d = new Date(r[1], r[3] - 1, r[4]);
            var num = (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4]);
            if (num == 0) {
                return false;
                // alert("请输入格式正确的日期\n\r日期格式：yyyy-mm-dd\n\r例    如：2008-08-08\n\r");
            }
            return (num != 0);
        }//判断时间是否符合要求

    </script>



    <div id="w" class="easyui-window" title="正在加载" data-options="modal:true,closed:true,iconCls:'icon-save'" style="width: 200px; height: 100px; padding: 10px; font-size: 12px">
        <img src="images/loaders/loader2.gif" alt="">&nbsp;&nbsp; 正在加载...
    </div>

    <div id="w1" class="easyui-window" title="信息提示" data-options="modal:true,closed:true,iconCls:'icon-save'" style="width: 400px; height: 500px; padding: 10px; font-size: 12px; overflow-y: scroll; overflow-x: hidden">
    </div>
    <div style="position: fixed; bottom: 5px; right: 5px; z-index: 2">
        <img width="30" height="30" onclick="click_scroll('contentwrapper')" src="Images/top.png" />
    </div>
</body>
</html>

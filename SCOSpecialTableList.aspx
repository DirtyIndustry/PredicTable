<!--黄海绿潮专项海洋环境预报  add by yy on 2018-04-23-->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SCOSpecialTableList.aspx.cs" Inherits="PredicTable.SCOSpecialTableList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>黄海绿潮专项海洋环境预报</title>
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

    <script type="text/javascript" src="js/MsgBox.js"></script>

    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/Gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />
    <style>
        body {
            color: #333333 !important;
        }

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
            width: auto;
        }

            .dlgs input[type=text] {
                width: 80%;
            }

        .textStyle input {
            width: 85%;
        }
        /*.fontStyle {
           font-size:18px;
           font-weight:500
        }*/
        input[type='text'] {
            font-size: 18px;
            font-weight: 500;
        }

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
    <script type="text/javascript">
        var cx_array = new Array("1");
        var sw_array = new Array("1","5","6");
        var fl_array = new Array("1", "2", "3", "4");

        var type = "<%=Session["type"]%>";
        var makewordtime = "pm";
       // var type = "fl";
        function stringToDate(str) {
            var dateStrs = str.split("-");
            var year = parseInt(dateStrs[0], 10);
            var month = parseInt(dateStrs[1], 10) - 1;
            var day = parseInt(dateStrs[2], 10);
            var date = new Date(year, month, day);
            return date;
        }
       
        //根据权限设置下拉框默认内容
        function quanxian(type, date)
        {
            if (getdatenow() == date) {
                switch (type) {
                    //case "cx": all_disabled(); cx_isabled(); tb_isabled(); $("#yby_type").val("潮汐"); break;//潮汐能填写
                    case "fl": all_isabled();  sw_disabled();  $("#yby_type").val("风、海浪"); /*fl_isabled();*/ break;//风浪能填写
                    case "sw": all_disabled(); sw_isabled();  $("#yby_type").val("水温"); break;//水温能填写
                    default: all_disabled(); $("#yby_type").val("无"); break;// 都不能填写
                }
            }
                //不是当天不能编辑
            else {
                switch (type) {
                    case "cx": all_disabled(); $("#yby_type").val("潮汐"); break;//都不能填写
                    case "fl": all_disabled();  $("#yby_type").val("风、海浪"); break;//风浪能填写
                    case "sw": all_disabled(); $("#yby_type").val("水温"); break;//都不能填写
                    default: all_disabled(); $("#yby_type").val("无"); break;// 都不能填写
                }
            }
        }

        $(function () {
            quanxian(type, getdatenow());
            setVisiable();
            $("#btn_show").click(function () {
                if ($("#btn_show").val() == "显示所有") {
                    all_show();
                    $("#btn_show").val("显示可编辑");
                } else if ($("#btn_show").val() == "显示可编辑") {
                    switch (type) {
                        case "cx": all_hide(); show_bytype(cx_array); break;//潮汐能编辑的
                        case "fl": all_hide(); show_bytype(fl_array); break;//风浪可以编辑的
                        case "sw": all_hide(); show_bytype(sw_array); break;//显示水温能编辑的
                        default: break;//不能填写
                    }
                    $("#btn_show").val("显示所有");
                }
            });
            //设置页面初始化时的可见性
            function setVisiable() {
                switch (type) {
                    case "cx": all_hide(); show_bytype(cx_array); break;//潮汐能填写
                    case "fl": all_hide(); show_bytype(fl_array); break;//风浪能填写
                    case "sw": all_hide(); show_bytype(sw_array); break;//水温能填写
                    default: break;// 都不能填写
                }
            }
        });
        //获取当前日期 格式 yyyy-mm-dd
        function getdatenow() {
            var myDate = new Date();
            var date = myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate();

            return date;
        }
        //显示所有
        function all_show() {
            var str = "";
            for (var i = 1; i <= 100; i++)
            {
                if (i < 10) {
                    str = "#ddlg_0" + i;
                } else {
                    str = "#ddlg_" + i;
                }
                $(str).css("display", "");
                //$("#lx_" + i).css("dispaly", "");
            }
        }
        //隐藏所有
        function all_hide() {
            var str = "";
            for (var i = 1; i <= 10; i++) {
                if (i < 10) {
                    str = "#ddlg_0" + i;
                } else {
                    str = "#ddlg_" + i;
                }
                $(str).css("display", "none");
                //$("#lx_" + i).css("display", "none");
            }
        }
        //根据id显示表单 
        function show_bytype(array) {
            var str = "";
            for (var i = 0; i < array.length; i++) {
                if (array[i] < 10) {
                    str = "#ddlg_0" + array[i];
                } else {
                    str = "#ddlg_" + array[i];
                }

                $(str).css("display", "");
            }

        }
    </script>
</head>
    
<body>
    <iframe width="0" height="0" src="SessionKeeper.asp"></iframe>
    <form id="form1" runat="server">
        <div>
            <div id="contentwrapper" class="contentwrapper">
                <div>
                    <!--表单信息-->
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
                                <option>海冰</option>
                                <option>无</option>
                            </select>&nbsp; 填报日期：<input id="tianbaoriqi" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser" />
                            <script type="text/javascript">
                            function myformatter(date1) {
                                var date = new Date(date1);
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
                            &nbsp;时间：
                            <select id="select_hour" name="select" class="uniformselect" style="height: 21px">
                            </select>时&nbsp;
                            <input type="button" id="btn_select" class="stdbtn" value="查询" />
                            <input type="button" id="btn_show" class="stdbtn" value="显示所有" />
                            <input type="button" id="setall" onclick="alldlg_Submit()" class="stdbtn" value="保存所有" />
                            <input type="button"id="preview" onclick="PreviewWord()" class="stdbtn" value="预览"/>
                            <input type="button" id="ReleasetableAll" onclick="All_Releasetable()" class="stdbtn" value="发布表单" />
                         <br />
                        </div>
                    </div>
                    <!--期数-->
                    <div class="dlgs" id="ddlg_01" style="height: auto; padding: 10px;">
                        <div style="border: 1px solid #ddd; background-color: #d2ffe7; height: auto; padding: 10px;">
                            期数：<input type="text" id="ProNo" style="width: 50px" />
                        </div>
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Close(1)" value="取消" />
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(1)" value="提交" />
                    </div>
                    <!--一、黄海绿潮专项海洋环境预报 综述-->
                    <div class="dlgs" id="ddlg_02" style="height: auto; padding: 10px">
                        <div style="border: 1px solid #ddd; width: auto; line-height: 36px; font-weight: bold; text-align: center;">黄海绿潮专项海洋环境预报综述</div>
                        <div style="border: 1px solid #ddd; background-color: #d2ffe7;">
                            <div style="float: left; margin-top: 18px;">综述：</div>
                            <div style="float: left; width: 95%">
                                <textarea name="txt_SWQX_ZS_3HOURS" style="margin: 10px 10px 10px 10px; width: 95%; height: 40px; font-size: 18px; font-weight: 500; overflow-x: hidden;" id="Summarize"> </textarea>
                            </div>
                            <div style="clear: both"></div>
                        </div>

                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Close(2)" value="取消" />
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(2)" value="提交" />

                    </div>
                    <!-- 二、青岛近海环境预报 FL_01_00 内海风力 第一天 00时 /tianqi_02_01 外海 天气 第二天 第一次-->
                    <div class="dlgs" id="ddlg_03" style="height: auto; padding: 10px;">
                        <div style="height: 10px"></div>
                        <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                            <colgroup></colgroup>
                            <thead>
                                <tr>
                                    <th id="ddlg_01title" class="head0" colspan="7">一、青岛近海环境预报</th>
                                </tr>
                                <tr>
                                    <th class="head0">日期</th>
                                    <%--<th class="head1">时间</th>--%>
                                    <th class="head0">天气</th>
                                    <th class="head1">风力（级）</th>
                                    <th class="head0">风向</th>
                                    <th class="head1">波高（m）</th>
                                    <th class="head0">波向</th>
                                </tr>
                            </thead>
                            <tbody id ="ddlg_01_tbody" class="textStyle" style="text-align: center">
                                <tr>
                                    <td class="SJ_01">*日</td>
                                    <td >
                                        <input id="JH_Weather_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="JH_FL_01" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="JH_FX_01" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="JH_BG_01" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="JH_BX_01"type="text" maxlength="20"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SJ_02">*日</td>
                                    <td >
                                        <input id="JH_Weather_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="JH_FL_02" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="JH_FX_02" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="JH_BG_02" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="JH_BX_02"type="text" maxlength="20"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SJ_03">*日</td>
                                    <td >
                                        <input id="JH_Weather_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="JH_FL_03" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="JH_FX_03" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="JH_BG_03" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="JH_BX_03"type="text" maxlength="20"/>
                                    </td>
                                </tr>
                            </tbody>
                            <%-- 上合峰会使用，注掉不用 --%>
                            <%--<tbody id="ddlg_01_tbody1" style="text-align: center">
                                <!--24小时预报-->
                                <tr>
                                    <td rowspan="24" class="SJ_01">*日</td>
                                    <td class="time24" style="width: 120px;">0:00</td>
                                    <td class="tianqi" rowspan="13" style="width: 180px;">
                                        <input id="tianqi_01_01" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="FL_01_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_00" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">1:00</td>
                                    <td>
                                        <input id="FL_01_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_01" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">2:00</td>
                                    <td>
                                        <input id="FL_01_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_02" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">3:00</td>
                                    <td>
                                        <input id="FL_01_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_03" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">4:00</td>
                                    <td>
                                        <input id="FL_01_04" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_04" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_04" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_04" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">5:00</td>
                                    <td>
                                        <input id="FL_01_05" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_05" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_05" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_05" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">6:00</td>
                                    <td>
                                        <input id="FL_01_06" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_06" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_06" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_06" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">7:00</td>
                                    <td>
                                        <input id="FL_01_07" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_07" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_07" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_07" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">8:00</td>
                                    <td>
                                        <input id="FL_01_08" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_08" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_08" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_08" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">9:00</td>
                                    <td>
                                        <input id="FL_01_09" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_09" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_09" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_09" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">10:00</td>
                                    <td>
                                        <input id="FL_01_10" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_10" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_10" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_10" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">11:00</td>
                                    <td>
                                        <input id="FL_01_11" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_11" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_11" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_11" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">12:00</td>
                                    <td>
                                        <input id="FL_01_12" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_12" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_12" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_12" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">13:00</td>
                                    <td class="tianqi" rowspan="11">
                                        <input id="tianqi_01_02" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="FL_01_13" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_13" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_13" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_13" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">14:00</td>
                                    <td>
                                        <input id="FL_01_14" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_14" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_14" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_14" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">15:00</td>
                                    <td>
                                        <input id="FL_01_15" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_15" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_15" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_15" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">16:00</td>
                                    <td>
                                        <input id="FL_01_16" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_16" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_16" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_16" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">17:00</td>
                                    <td>
                                        <input id="FL_01_17" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_17" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_17" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_17" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">18:00</td>
                                    <td>
                                        <input id="FL_01_18" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_18" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_18" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_18" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">19:00</td>
                                    <td>
                                        <input id="FL_01_19" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_19" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_19" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_19" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">20:00</td>
                                    <td>
                                        <input id="FL_01_20" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_20" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_20" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_20" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">21:00</td>
                                    <td>
                                        <input id="FL_01_21" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_21" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_21" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_21" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">22:00</td>
                                    <td>
                                        <input id="FL_01_22" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_22" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_22" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_22" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">23:00</td>
                                    <td>
                                        <input id="FL_01_23" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_01_23" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_01_23" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_01_23" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <!--48小时预报-->
                                <tr>
                                    <td rowspan="4" class="SJ_02">*日</td>
                                    <td class="time24" style="width: 60px;">0:00-6:00</td>
                                    <td class="tianqi" rowspan="2">
                                        <input id="tianqi_02_01" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="FL_02_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_02_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_02_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_02_00" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">6:00-12:00</td>
                                    <td>
                                        <input id="FL_02_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_02_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_02_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_02_01" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">12:00-18:00</td>
                                    <td class="tianqi" rowspan="2">
                                        <input id="tianqi_02_02" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="FL_02_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_02_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_02_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_02_02" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">18:00-24:00</td>
                                    <td>
                                        <input id="FL_02_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_02_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_02_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_02_03" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <!--72小时预报-->
                                <tr>
                                    <td rowspan="4" class="SJ_03">*日</td>
                                    <td class="time24" style="width: 60px;">0:00-6:00</td>
                                    <td class="tianqi" rowspan="2">
                                        <input id="tianqi_03_01" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="FL_03_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_03_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_03_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_03_00" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">6:00-12:00</td>
                                    <td>
                                        <input id="FL_03_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_03_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_03_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_03_01" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">12:00-18:00</td>
                                    <td class="tianqi" rowspan="2">
                                        <input id="tianqi_03_02" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="FL_03_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_03_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_03_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_03_02" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">18:00-24:00</td>
                                    <td>
                                        <input id="FL_03_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="FX_03_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BG_03_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="BX_03_03" type="text" maxlength="20" />
                                    </td>
                                </tr>
                            </tbody>--%>
                        </table>
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Close(3)" value="取消" />
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(3)" value="提交" />
                    </div>
                    <!--青岛近海水温-->
                    <div class="dlgs" id="ddlg_06" style="height: auto; padding: 0px;">
                        <div style="height: 10px"></div>
                        <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                            <colgroup></colgroup>
                            <thead>
                                <tr>
                                    <th colspan="4">二、青岛近海水温预报</th>
                                </tr>
                                <tr>
                                    <th class="head0">日期</th>
                                    <th class="head0">最高(℃)</th>
                                    <th class="head0">最低(℃)</th>
                                    <th class="head0">平均(℃)</th>
                                </tr>
                            </thead>
                            <tbody class="textStyle" style="text-align: center">
                                <tr>
                                    <td class="SJ_01">*日</td>
                                    <td>
                                        <input id="Max_SW_01_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Min_SW_01_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Avg_SW_01_01" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SJ_02">*日</td>
                                    <td>
                                        <input id="Max_SW_01_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Min_SW_01_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Avg_SW_01_02" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SJ_03">*日</td>
                                    <td>
                                        <input id="Max_SW_01_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Min_SW_01_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Avg_SW_01_03" type="text" maxlength="20" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Close(6)" value="取消" />
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(6)" value="提交" />

                    </div>
                    <!--三、青岛外海环境预报 WH_FL_01_00 外海风力 第一天 00时  //WH_tianqi_02_01 外海 天气 第二天 第一次-->
                    <div class="dlgs" id="ddlg_04" style="height: auto; padding: 0px;">
                        <div style="height: 10px"></div>
                        <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                            <colgroup></colgroup>
                            <thead>
                                <tr>
                                    <th id="ddlg_04title" class="head0" colspan="7">三、青岛外海环境预报</th>
                                </tr>
                                <tr>
                                    <th class="head0">日期</th>
                                    <%--<th class="head1">时间</th>--%>
                                    <th class="head0">天气</th>
                                    <th class="head1">风力（级）</th>
                                    <th class="head0">风向</th>
                                    <th class="head1">波高（m）</th>
                                    <th class="head0">波向</th>
                                </tr>
                            </thead>
                            <tbody id="ddlg_04_tbody" style="text-align: center">
                                <tr>
                                    <td class="SJ_01">*日</td>
                                    <td >
                                        <input id="WH_Weather_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FL_01" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="WH_FX_01" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="WH_BG_01" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="WH_BX_01"type="text" maxlength="20"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SJ_02">*日</td>
                                    <td >
                                        <input id="WH_Weather_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FL_02" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="WH_FX_02" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="WH_BG_02" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="WH_BX_02"type="text" maxlength="20"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SJ_03">*日</td>
                                    <td >
                                        <input id="WH_Weather_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FL_03" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="WH_FX_03" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="WH_BG_03" type="text" maxlength="20"/>
                                    </td>
                                    <td>
                                        <input id="WH_BX_03"type="text" maxlength="20"/>
                                    </td>
                                </tr>
                            </tbody>
                             <%-- 上合峰会使用，注掉不用 --%>
                            <%--<tbody id="ddlg_04_tbody1" style="text-align: center">
                                <!--24小时预报-->
                                <tr>
                                    <td rowspan="24" class="SJ_01">*日</td>
                                    <td class="time24" style="width: 120px;">0:00</td>
                                    <td class="tianqi" rowspan="13" style="width: 180px;">
                                        <input id="WH_tianqi_01_01" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="WH_FL_01_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_00" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">1:00</td>
                                    <td>
                                        <input id="WH_FL_01_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_01" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">2:00</td>
                                    <td>
                                        <input id="WH_FL_01_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_02" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">3:00</td>
                                    <td>
                                        <input id="WH_FL_01_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_03" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">4:00</td>
                                    <td>
                                        <input id="WH_FL_01_04" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_04" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_04" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_04" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">5:00</td>
                                    <td>
                                        <input id="WH_FL_01_05" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_05" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_05" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_05" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">6:00</td>
                                    <td>
                                        <input id="WH_FL_01_06" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_06" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_06" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_06" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">7:00</td>
                                    <td>
                                        <input id="WH_FL_01_07" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_07" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_07" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_07" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">8:00</td>
                                    <td>
                                        <input id="WH_FL_01_08" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_08" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_08" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_08" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">9:00</td>
                                    <td>
                                        <input id="WH_FL_01_09" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_09" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_09" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_09" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">10:00</td>
                                    <td>
                                        <input id="WH_FL_01_10" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_10" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_10" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_10" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">11:00</td>
                                    <td>
                                        <input id="WH_FL_01_11" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_11" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_11" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_11" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">12:00</td>
                                    <td>
                                        <input id="WH_FL_01_12" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_12" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_12" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_12" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">13:00</td>
                                    <td class="tianqi" rowspan="11">
                                        <input id="WH_tianqi_01_02" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="WH_FL_01_13" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_13" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_13" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_13" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">14:00</td>
                                    <td>
                                        <input id="WH_FL_01_14" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_14" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_14" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_14" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">15:00</td>
                                    <td>
                                        <input id="WH_FL_01_15" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_15" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_15" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_15" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">16:00</td>
                                    <td>
                                        <input id="WH_FL_01_16" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_16" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_16" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_16" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">17:00</td>
                                    <td>
                                        <input id="WH_FL_01_17" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_17" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_17" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_17" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">18:00</td>
                                    <td>
                                        <input id="WH_FL_01_18" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_18" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_18" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_18" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">19:00</td>
                                    <td>
                                        <input id="WH_FL_01_19" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_19" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_19" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_19" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">20:00</td>
                                    <td>
                                        <input id="WH_FL_01_20" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_20" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_20" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_20" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">21:00</td>
                                    <td>
                                        <input id="WH_FL_01_21" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_21" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_21" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_21" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">22:00</td>
                                    <td>
                                        <input id="WH_FL_01_22" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_22" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_22" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_22" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">23:00</td>
                                    <td>
                                        <input id="WH_FL_01_23" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_01_23" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_01_23" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_01_23" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <!--48小时预报-->
                                <tr>
                                    <td rowspan="4" class="SJ_02">*日</td>
                                    <td class="time24" style="width: 60px;">0:00-6:00</td>
                                    <td class="tianqi" rowspan="2">
                                        <input id="WH_tianqi_02_01" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="WH_FL_02_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_02_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_02_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_02_00" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">6:00-12:00</td>
                                    <td>
                                        <input id="WH_FL_02_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_02_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_02_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_02_01" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">12:00-18:00</td>
                                    <td class="tianqi" rowspan="2">
                                        <input id="WH_tianqi_02_02" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="WH_FL_02_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_02_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_02_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_02_02" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">18:00-24:00</td>
                                    <td>
                                        <input id="WH_FL_02_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_02_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_02_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_02_03" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <!--72小时预报-->
                                <tr>
                                    <td rowspan="4" class="SJ_03">*日</td>
                                    <td class="time24" style="width: 60px;">0:00-6:00</td>
                                    <td class="tianqi" rowspan="2">
                                        <input id="WH_tianqi_03_01" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="WH_FL_03_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_03_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_03_00" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_03_00" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">6:00-12:00</td>
                                    <td>
                                        <input id="WH_FL_03_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_03_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_03_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_03_01" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">12:00-18:00</td>
                                    <td class="tianqi" rowspan="2">
                                        <input id="WH_tianqi_03_02" type="text" maxlength="20" value="" />
                                    </td>
                                    <td>
                                        <input id="WH_FL_03_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_03_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_03_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_03_02" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="time24" style="width: 60px;">18:00-24:00</td>
                                    <td>
                                        <input id="WH_FL_03_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_FX_03_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BG_03_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="WH_BX_03_03" type="text" maxlength="20" />
                                    </td>
                                </tr>
                            </tbody>--%>
                        </table>
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Close(4)" value="取消" />
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(4)" value="提交" />
                    </div>
                    <!--四、青岛外海水温预报  Max_SW_02_01 最大值 水温 外海 第一天-->
                    <div class="dlgs" id="ddlg_05" style="height: auto; padding: 0px;">
                        <div style="height: 10px"></div>
                        <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                            <colgroup></colgroup>
                            <thead>
                                <tr>
                                    <th colspan="4">四、青岛外海水温预报</th>
                                </tr>
                                <tr>
                                    <th class="head0">日期</th>
                                    <th class="head0">最高(℃)</th>
                                    <th class="head0">最低(℃)</th>
                                    <th class="head0">平均(℃)</th>
                                </tr>
                            </thead>
                            <tbody class="textStyle" style="text-align: center">
                                <tr>
                                    <td class="SJ_01">*日</td>
                                    <td>
                                        <input id="Max_SW_02_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Min_SW_02_01" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Avg_SW_02_01" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SJ_02">*日</td>
                                    <td>
                                        <input id="Max_SW_02_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Min_SW_02_02" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Avg_SW_02_02" type="text" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SJ_03">*日</td>
                                    <td>
                                        <input id="Max_SW_02_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Min_SW_02_03" type="text" maxlength="20" />
                                    </td>
                                    <td>
                                        <input id="Avg_SW_02_03" type="text" maxlength="20" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Close(5)" value="取消" />
                        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(5)" value="提交" />

                    </div>

                </div>
            </div>
        </div>
        
        
    </form>
   
    <div id="w" class="easyui-window" title="正在加载" data-options="modal:true,closed:true,iconCls:'icon-save'" style="width: 200px; height: 100px; padding: 10px; font-size: 12px">
        <img src="images/loaders/loader2.gif" alt="" />&nbsp;&nbsp; 正在加载...
    </div>
    <div id="w1" class="easyui-window" title="信息提示" data-options="modal:true,closed:true,iconCls:'icon-save'" style="width: 400px; height: 500px; padding: 10px; font-size: 12px; overflow-y: scroll; overflow-x: hidden">
    </div>
    <div id="dlg_preview" class="easyui-window" title="预览表单" data-options="modal:true,closed:true,iconCls:'icon-save'" style="width: 320px; height: 350px; padding: 10px;overflow-y: scroll; overflow-x: hidden"">
            <a href="#" class="easyui-linkbutton"
            onclick="PreviewTable(1)"  data-options="iconCls:'icon-save',plain:'true'" id="a_pingtai">黄海绿潮专项海洋环境预报</a>
            <%--<a href="#" class="easyui-linkbutton"
            onclick="PreviewTable(2)"  data-options="iconCls:'icon-save',plain:'true'" id="a_zhuanxiang">上合峰会专项海洋环境预报单</a>--%>
     </div> 
    <script type="text/javascript"> 
         //文件预览JS
        var PriviewType = "";
        var Priviewdate = "";
       //打开预览文件选项窗口
        function PreviewWord() {
            $('#dlg_preview').window('open');
        }
        //预览上合峰会平台海洋环境预报单
            function PreviewTable(id) {
                PriviewType = id;
                var d = new Date();
                var strlist = "上合峰会海洋环境预报单预览,";
                var date = myformatter(new Date($("#tianbaoriqi").datebox("getValue")));
                Priviewdate = date;
                // if (date)
                var phour = $("#select_hour").val();
                var datas = { datas: strlist, dates: date, hours: phour, makewordtime: makewordtime ,Id:id};
                $.ajax({
                    type: "POST",
                    url: "/Ajax/releasetable.ashx",
                    data: datas,
                    beforeSend: function () {
                        $('#w').window('open');
                    },
                    success: function (result) {
                        if (result != "PDFSuccess") {
                            $('#w_preview').html(result);
                        } else {
                            //$("#w_preview").html("<iframe name='MyIframe' src='PriViewSCOTable.aspx?PriviewType='" + PriviewType + "&date=" + date + "; style='width:100%;height:100%;'></iframe>");
                            window.frames["MyIframe"].location.href = "PriViewSCOTable.aspx?PriviewType=" + PriviewType + "&date=" + Priviewdate;
                        }

                        $('#w_preview').window({
                            width: 1200,
                            height: 800,
                            left: 450,
                            top:70,
                            closed:false
                        });
                    },
                    complete: function () {
                        $('#w').window('close');
                        $('#dlg_preview').window('close');
                        
                    }
                });   
            }
        </script>
    <div id="w_preview" class="easyui-window" title="预览" data-options="modal:true,closed:true,iconCls:'icon-save'"style="overflow-y:hidden!important;" "overflow-y: scroll; overflow-x: hidden">
    <%--<div id="w_preview" class="easyui-window" title="预览" data-options="modal:true,closed:true,iconCls:'icon-save'"overflow-y: scroll; overflow-x: hidden" style="position:absolute;top:50px;width:1200px; height:900px;left:50%;margin-left:-600px;">--%>
        <%--<div style="position:absolute;right:20px;top:10px; color:#fff;cursor:pointer; width:30px;height:30px;border-radius:6px;text-align:center;line-height:30px;background-color:#49657b;">x</div>--%>
        <iframe name='MyIframe' src="/"; style='width:100%;height:100%;'></iframe>
    </div>
    <script type="text/javascript">
        //权限操作JS
        //小时选择事件
        $('#select_hour').change(function () {
            var sj = $("#Fabushijian").val();
            if (sj.length < 3) {

            } else {
                $("#Fabushijian").val(sj.split('日')[0] + "日" + $('#select_hour').val() + "时");
            }
        });
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
                    //cx_isabled();//潮汐可编辑
                    //tb_isabled();//填报信息可编辑
                }
                else if ($("#yby_type").val() == "风、海浪") {
                    all_isabled();
                   // cx_disabled(); //潮汐不可编辑
                    sw_disabled();//水温不可编辑
                    //tb_isabled();//填报信息可编辑
                }
                else if ($("#yby_type").val() == "水温") {
                    all_disabled();
                    sw_isabled();//水温可编辑
                    //tb_isabled();//填报信息可编辑
                }
                else if ($("#yby_type").val() == "海冰") {
                    all_disabled();
                    hb_isabled();//海冰可编辑
                    //tb_isabled();//填报信息可编辑
                }
            }
           
            //水温可编辑
            function sw_isabled() {
                $("input[id*='SW']").removeAttr("disabled");
                //$("input[id*='ICE']").removeAttr("disabled");
                //$("#SWQX_ZS_3HOURS").removeAttr("disabled");
                //$("#SWQX_ZS_24HOURS").removeAttr("disabled");
                //$("#SWQX_ZS_7DAYS").removeAttr("disabled");
            }
            //水温不可编辑
            function sw_disabled() {
                $("input[id*='SW']").attr("disabled", "disabled");
                //$("#SWQX_ZS_3HOURS").attr("disabled", "disabled");
                //$("#SWQX_ZS_24HOURS").attr("disabled", "disabled");
                //$("#SWQX_ZS_7DAYS").attr("disabled", "disabled");

                //$("#CXQX_ZS_3DAYS").attr("disabled", "disabled");
                //$("#CXQX_ZS_24HOURS").attr("disabled", "disabled");
                //$("#CXQX_ZS_7DAYS").attr("disabled", "disabled");
            }
            

            //都不可编辑
            function all_disabled() {
                $(":text").each(function () {//默认加载时textbox都不可编辑
                    this.disabled = true;
                });
                $("textarea").each(function () {//textarea都不可编辑
                    $(this).attr("disabled", "disabled");
                });
            }
            //都可编辑
            function all_isabled() {
                $(":text").each(function () {//默认加载时textbox都可编辑
                    //  this.removeAttr("disabled");
                    this.disabled = false;
                });
                $("textarea").each(function () {//textarea都可编辑
                    $(this).removeAttr("disabled");
                    //this.disabled = false;
                });
            }
        }
        //↑根据潮汐 风浪 水温 操作性↑

    </script>
</body>
    <script type="text/javascript">
        var AuthorityType = "<%=Session["type"]%>"; //获取用户权限
        //var AuthorityType = "fl";
        function GetDate(today) {
            var ds0 = (today.getMonth() + 1) + "月" + today.getDate() + "日";
            today.setDate(today.getDate() + 1);
            $('.SJ_01').text(""  + today.getDate() + "日");
           
            today.setDate(today.getDate() + 1);
            $('.SJ_02').text(""  + today.getDate() + "日");

            today.setDate(today.getDate() + 1);
            $('.SJ_03').text("" + today.getDate() + "日");
            // today.setDate(today.getDate() + 7); // 系统会自动转换
            //下面是date类提供的三个你可能生成字符串用到的函数：
            //getDate() 从 Date 对象返回一个月中的某一天 (1 ~ 31).
            //getMonth() 从 Date 对象返回月份 (0 ~ 11).
            // getFullYear() 从 Date 对象以四位数字返回年份.
        }//根据当前年月日 推算未来年月日
        $('#tianbaoriqi').datebox({
            onSelect: function (date) {
                //  $("#receive_date").datebox('setValue', dateAdd(date, 0));
                // $("#expected_report_date").datebox('setValue', dateAdd(date, 28));
                var Time = $("#tianbaoriqi").datebox("getValue");
                var selectDate = new Date(Time);
   
                GetDate(selectDate);
               // setWeekReportTime();
            }
        }); //填报日期修改事件

        $(function () {
            //加载日期 ‘时’ 
            var hour_str = "";
            for (var i = 0; i < 24; i++) {
                hour_str += "<option>" + i + "</option>";
            }
            $("#select_hour").append(hour_str);
            $("#select_hour").val(15);
            var today = new Date(); // 获取今天时间
            GetDate(today); //根据当前年月日 推算未来年月日
            //setWeekReportTime();
            var date = $("#tianbaoriqi").datebox("getValue");

            //获取黄海绿潮专项海洋环境预报初始数据
            var curr_time = new Date();
            getSCOTableData(curr_time);


            $("#tianbaoriqi").datebox("setValue", myformatter(curr_time));  //设置默认时间 当前时间
        });
        //获取黄海绿潮专项海洋环境预报初始数据
        function getSCOTableData(date1) {
            var dates = myformatter(date1);
            $.ajax({
                type: "POST",
                url: "/Ajax/SCOSpecialTableList.ashx?method=getSCOTableList&date=" + dates + "&t=" + Math.random(),
                cache: false,
                beforeSend: function () {
                    $("#w").window('open');
                    $("#btn_select").attr({ disabled: "disabled" });
                },
                success: function (res) {
                    var resJson = JSON.parse(res);
                    //解析json
                    for (var i = 0; i < resJson.length; i++)
                    {
                        switch (resJson[i].type) {
                            case "t0": gettable00(resJson[i]); break; //期数和综述
                                //case "t1": gettable01(resJson[i]); break; //近海风
                                //case "t2": gettable02(resJson[i]); break; //外海风
                                //case "t3": gettable03(resJson[i]); break; //近海浪
                                //case "t4": gettable04(resJson[i]); break; //外海浪
                            case "t1": gettable01(resJson[i]); break;       //近海风浪
                            case "t2": gettable02(resJson[i]); break;       //外海风浪
                            case "t5": gettable05(resJson[i]); break; //外海水温
                            case "t6": gettable06(resJson[i]); break; //近海水温、
                            default: break;
                        }
                    }
                },
                complete: function () {
                    $('#w').window('close');
                    $("#btn_select").removeAttr("disabled");
                }
            })
        }
        //表单数据加载
        {
            //表单o  期数和综述
            function gettable00(resJson) {
                var resjsonType = resJson.pbtype;
                var resjson = resJson.SummarizeAndPeriod[0];
                var prono = resjson.PERIODS;
                if (resjsonType == "today")  //取出的是当天数据
                {
                    $("#ProNo").val(prono);
                    $("#Summarize").val(resjson.USUMMARIZE);
                } else if (resjsonType == "yestoday") //取出之前一天的数据
                {
                    prono = prono * 1 + 1;
                    //if (prono * 1 < 10) {
                    //    prono = "00" + prono * 1;
                    //} else if (prono * 1 > 10 && prono * 1 < 100) {
                    //    prono = "0" + prono * 1;
                    //} else {
                    //    prono = prono;
                    //}
                    $('#ProNo').val(prono);
                }

            }
            //表单一  绿潮近海海海洋环境（风力、风向）
            function gettable01(resJson) {
                for (var i = 0; i < 3; i++)
                {
                    var resjson = resJson.OffWaveAndWind[i];
                    $("#JH_Weather_0" + (i + 1)).val(resjson["WEATHER"]); 
                    $("#JH_FL_0" + (i + 1)).val(resjson["WINDFORCE"]);
                    $("#JH_FX_0" + (i + 1)).val(resjson["WINDDIRECTION"]);
                    $("#JH_BG_0" + (i + 1)).val(resjson["WAVEHIGHT"]);
                    $("#JH_BX_0" + (i + 1)).val(resjson["WAVEDIRECTION"]);
                }
            }
            //表单二 绿潮外海海洋环境（风力、风向）
            function gettable02(resJson) {
                for (var i = 0; i < 3; i++) {
                    var resjson = resJson.OpenWaveAndWind[i];
                    $("#WH_Weather_0" + (i + 1)).val(resjson["WEATHER"]);
                    $("#WH_FL_0" + (i + 1)).val(resjson["WINDFORCE"]);
                    $("#WH_FX_0" + (i + 1)).val(resjson["WINDDIRECTION"]);
                    $("#WH_BG_0" + (i + 1)).val(resjson["WAVEHIGHT"]);
                    $("#WH_BX_0" + (i + 1)).val(resjson["WAVEDIRECTION"]);
                }
                    
            }
            //上合峰会预报单注掉不用
            {
                //表单一 内海海洋环境（浪高、浪向）
                //function gettable03(resJson) {
                //    var strnum = "";
                //    var resjson = resJson.onShoreWave[0];
                //    for (var i = 0; i <= 23; i++) {
                //        if (i < 10) {
                //            strnum = "0" + i
                //        }
                //        else {
                //            strnum = i;
                //        }

                //        $("#BG_01_" + strnum).val(resjson["WAVEFORCE" + strnum + "H"]);
                //        $("#BX_01_" + strnum).val(resjson["WAVEDIRECTION" + strnum + "H"]);
                //    }
                //    $("#BG_02_00").val(resjson.WAVEFORCE24H);
                //    $("#BG_02_01").val(resjson.WAVEFORCE25H);
                //    $("#BG_02_02").val(resjson.WAVEFORCE26H);
                //    $("#BG_02_03").val(resjson.WAVEFORCE27H);

                //    $("#BG_03_00").val(resjson.WAVEFORCE28H);
                //    $("#BG_03_01").val(resjson.WAVEFORCE29H);
                //    $("#BG_03_02").val(resjson.WAVEFORCE30H);
                //    $("#BG_03_03").val(resjson.WAVEFORCE31H);

                //    $("#BX_02_00").val(resjson.WAVEDIRECTION24H);
                //    $("#BX_02_01").val(resjson.WAVEDIRECTION25H);
                //    $("#BX_02_02").val(resjson.WAVEDIRECTION26H);
                //    $("#BX_02_03").val(resjson.WAVEDIRECTION27H);

                //    $("#BX_03_00").val(resjson.WAVEDIRECTION28H);
                //    $("#BX_03_01").val(resjson.WAVEDIRECTION29H);
                //    $("#BX_03_02").val(resjson.WAVEDIRECTION30H);
                //    $("#BX_03_03").val(resjson.WAVEDIRECTION31H);
                //}
                //表单二 外海海洋环境（浪高、浪向）
            
            //function gettable04(resJson) {
            //    var strnum = "";
            //    var resjson = resJson.onOpenWave[0]; 
            //    for (var i = 0; i <= 23; i++) {
            //        if (i < 10) {
            //            strnum = "0" + i
            //        }
            //        else {
            //            strnum = i; 
            //        }

            //        $("#WH_BG_01_" + strnum).val(resjson["WAVEFORCE" + strnum + "H"]);
            //        $("#WH_BX_01_" + strnum).val(resjson["WAVEDIRECTION" + strnum + "H"]);
            //    }
            //    $("#WH_BG_02_00").val(resjson.WAVEFORCE24H);
            //    $("#WH_BG_02_01").val(resjson.WAVEFORCE25H);
            //    $("#WH_BG_02_02").val(resjson.WAVEFORCE26H);
            //    $("#WH_BG_02_03").val(resjson.WAVEFORCE27H);

            //    $("#WH_BG_03_00").val(resjson.WAVEFORCE28H);
            //    $("#WH_BG_03_01").val(resjson.WAVEFORCE29H);
            //    $("#WH_BG_03_02").val(resjson.WAVEFORCE30H);
            //    $("#WH_BG_03_03").val(resjson.WAVEFORCE31H);

            //    $("#WH_BX_02_00").val(resjson.WAVEDIRECTION24H);
            //    $("#WH_BX_02_01").val(resjson.WAVEDIRECTION25H);
            //    $("#WH_BX_02_02").val(resjson.WAVEDIRECTION26H);
            //    $("#WH_BX_02_03").val(resjson.WAVEDIRECTION27H);

            //    $("#WH_BX_03_00").val(resjson.WAVEDIRECTION28H);
            //    $("#WH_BX_03_01").val(resjson.WAVEDIRECTION29H);
            //    $("#WH_BX_03_02").val(resjson.WAVEDIRECTION30H);
            //    $("#WH_BX_03_03").val(resjson.WAVEDIRECTION31H);
                //}
            }
            //表单三 外海海洋环境（水温）
            function gettable05(resJson) {
                var strnum = "";
                var resjson = resJson.onShoreSW[0];
                $("#Max_SW_02_01").val(resjson.MAXVALUE_24H);
                $("#Min_SW_02_01").val(resjson.MINVALUE_24H);
                $("#Avg_SW_02_01").val(resjson.AVERAGE_24H);

                $("#Max_SW_02_02").val(resjson.MAXVALUE_48H);
                $("#Min_SW_02_02").val(resjson.MINVALUE_48H);
                $("#Avg_SW_02_02").val(resjson.AVERAGE_48H);

                $("#Max_SW_02_03").val(resjson.MAXVALUE_72H);
                $("#Min_SW_02_03").val(resjson.MINVALUE_72H);
                $("#Avg_SW_02_03").val(resjson.AVERAGE_72H);
            }
            //表单   近海水温
            function gettable06(resJson) {
                var strnum = "";
                var resjson = resJson.offShoreSW[0];
                $("#Max_SW_01_01").val(resjson.MAXVALUE_24H);
                $("#Min_SW_01_01").val(resjson.MINVALUE_24H);
                $("#Avg_SW_01_01").val(resjson.AVERAGE_24H);

                $("#Max_SW_01_02").val(resjson.MAXVALUE_48H);
                $("#Min_SW_01_02").val(resjson.MINVALUE_48H);
                $("#Avg_SW_01_02").val(resjson.AVERAGE_48H);

                $("#Max_SW_01_03").val(resjson.MAXVALUE_72H);
                $("#Min_SW_01_03").val(resjson.MINVALUE_72H);
                $("#Avg_SW_01_03").val(resjson.AVERAGE_72H);
            }
        }
        //查询按钮点击事件
        $("#btn_select").click(function () {
            //alert("查询");//5213 
            $('#dlgs :text').val("");

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
            var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
            var d = new Date(date1);
            var date = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();
            getSCOTableData(date1);
            //只能修改当前天的数据
            quanxian(type, date);
        });
        //表单提交（表单编号）↓
        function dlg_Submit(id) {
            //alert("提交表单" + id);
            switch (id)
            {
                case 1: submit_1(id); break;//表单一提交
                case 2: submit_2(id); break;//表单二提交
                case 3: submit_3(id); break;//表单三提交
                case 4: submit_3(id); break;//表单四提交
                case 5: submit_5(id); break;//表单五提交
                case 6: submit_5(id); break;//表单五提交
            }
        }
        //表单数据拼接   
        {
            //表单01 期数
            function submit_1(id) {   
               // var str_data = "";
                
                var periods = $("#ProNo").val()//期数编号
                //str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: periods };
                submit_Ajax(id, data)
            }
            //表单02 综述
            function submit_2(id) {
                if (AuthorityType != "fl")
                {
                    $.messager.show({
                        title: '操作消息',
                        msg: '当前用户没有不能提交综述',
                        showType: 'show'
                    });
                    return;
                }
                var summarize = $("#Summarize").val();
                var data = { datas: summarize };
                submit_Ajax(id, data);
            }
            //绿潮近/外海预报
            function submit_3(id) {
                //判断权限
                if (AuthorityType != "fl") {
                    $.messager.show({
                        title: '操作消息',
                        msg: '当前用户没有不能提交风浪数据',
                        showType: 'show'
                    });
                    return;
                }
                var WindAndWave_strData = "";
                if (id == 3)//近海
                {
                    for (var i = 1; i <= 3; i++) {
                        WindAndWave_strData += $("#JH_Weather_0" + i).val() + ",";
                        WindAndWave_strData += $("#JH_FL_0" + i).val() + ",";
                        WindAndWave_strData += $("#JH_FX_0" + i).val() + ",";
                        WindAndWave_strData += $("#JH_BG_0" + i).val() + ",";
                        WindAndWave_strData += $("#JH_BX_0" + i).val() + ",";
                    }
                    WindAndWave_strData = WindAndWave_strData.substring(0, WindAndWave_strData.length - 1);

                } else if (id == 4) {

                    for (var i = 1; i <= 3; i++) {
                        WindAndWave_strData += $("#WH_Weather_0" + i).val() + ",";
                        WindAndWave_strData += $("#WH_FL_0" + i).val() + ",";
                        WindAndWave_strData += $("#WH_FX_0" + i).val() + ",";
                        WindAndWave_strData += $("#WH_BG_0" + i).val() + ",";
                        WindAndWave_strData += $("#WH_BX_0" + i).val() + ",";
                    }
                    WindAndWave_strData = WindAndWave_strData.substring(0, WindAndWave_strData.length - 1);
                }
                if (WindAndWave_strData == "") {
                    $.messager.show({
                        title: '操作消息',
                        msg: '数据获取异常',
                        showType: 'show'
                    });
                }
                var data = { datas: WindAndWave_strData };
                submit_Ajax(id, data);
            }
            //上合峰会预报单  注掉不用
            {    
            //表单03 近海风、浪
            //function submit_3(id) {
            //    if (AuthorityType != "fl")
            //    {
            //        $.messager.show({
            //            title: '操作消息',
            //            msg: '当前用户没有不能提交风浪数据',
            //            showType: 'show'
            //        });
            //        return;
            //    }
            //    var str_data = ""; var num;
            //    var FL_strData = "", //风向
            //        FX_strData = "", //风力
            //        BG_strData = "", //波高
            //        BX_strData = "", //波向
            //        Weather_strData="";//天气
            //    for (var i = 0; i <= 23; i++)
            //    {
            //        if (i < 10) {
            //            num = "0" + i;
            //        } else {
            //            num = i;
            //        }
            //        FL_strData += $("#FL_01_" + num).val() + ",";
            //        FX_strData += $("#FX_01_" + num).val() + ",";
            //        BG_strData += $("#BG_01_" + num).val() + ",";
            //        BX_strData += $("#BX_01_" + num).val() + ",";
            //    }
            //    for (var i = 0; i <= 3; i++) {
            //        if (i < 10) {
            //            num = "0" + i;
            //        } else {
            //            num = i;
            //        }
            //        FL_strData += $("#FL_02_" + num).val() + ",";
            //        FX_strData += $("#FX_02_" + num).val() + ",";
            //        BG_strData += $("#BG_02_" + num).val() + ",";
            //        BX_strData += $("#BX_02_" + num).val() + ",";
            //    }
            //    for (var i = 0; i <= 3; i++) {
            //        if (i < 10) {
            //            num = "0" + i;
            //        } else {
            //            num = i;
            //        }
            //        FL_strData += $("#FL_03_" + num).val() + ",";
            //        FX_strData += $("#FX_03_" + num).val() + ",";
            //        BG_strData += $("#BG_03_" + num).val() + ",";
            //        BX_strData += $("#BX_03_" + num).val() + ",";
            //    }
            //    FL_strData = FL_strData.substring(0, FL_strData.length - 1);
            //    FX_strData = FX_strData.substring(0, FX_strData.length - 1);
            //    BG_strData = BG_strData.substring(0, BG_strData.length - 1);
            //    BX_strData = BX_strData.substring(0, BX_strData.length - 1);
            //    for (var i = 1; i < 4; i++)
            //    {
            //        for (var j = 1; j < 3; j++)
            //        {
            //            Weather_strData += $("#tianqi_0" + i + "_0" + j).val()+",";
            //        }
            //    }
            //    Weather_strData = Weather_strData.substring(0, Weather_strData.length - 1);
            //    var data = { FL_strData: FL_strData, FX_strData: FX_strData, BG_strData: BG_strData, BX_strData: BX_strData, Weather_strData: Weather_strData };
            //    submit_Ajax(id, data);
            //}
            //表单04 外海风、浪
            //function submit_4(id) {
            //    if (AuthorityType != "fl")
            //    {
            //        $.messager.show({
            //            title: '操作消息',
            //            msg: '当前用户没有不能提交风浪数据',
            //            showType: 'show'
            //        });
            //        return;
            //    }
            //    var str_data = ""; var num;
            //    var FL_strData = "", //风向
            //        FX_strData = "", //风力
            //        BG_strData = "", //波高
            //        BX_strData = "", //波向
            //        Weather_strData = "";//天气
            //    for (var i = 0; i <= 23; i++) {
            //        if (i < 10) {      
            //            num = "0" + i;
            //        } else {
            //            num = i;
            //        }
            //        FL_strData += $("#WH_FL_01_" + num).val() + ",";
            //        FX_strData += $("#WH_FX_01_" + num).val() + ",";
            //        BG_strData += $("#WH_BG_01_" + num).val() + ",";
            //        BX_strData += $("#WH_BX_01_" + num).val() + ",";
            //    }
            //    for (var i = 0; i <= 3; i++) {
            //        if (i < 10) {
            //            num = "0" + i;
            //        } else {
            //            num = i;
            //        }
            //        FL_strData += $("#WH_FL_02_" + num).val() + ",";
            //        FX_strData += $("#WH_FX_02_" + num).val() + ",";
            //        BG_strData += $("#WH_BG_02_" + num).val() + ",";
            //        BX_strData += $("#WH_BX_02_" + num).val() + ",";
            //    }
            //    for (var i = 0; i <= 3; i++) {
            //        if (i < 10) {
            //            num = "0" + i;
            //        } else {
            //            num = i;
            //        }
            //        FL_strData += $("#WH_FL_03_" + num).val() + ",";
            //        FX_strData += $("#WH_FX_03_" + num).val() + ",";
            //        BG_strData += $("#WH_BG_03_" + num).val() + ",";
            //        BX_strData += $("#WH_BX_03_" + num).val() + ",";
            //    }
            //    FL_strData = FL_strData.substring(0, FL_strData.length - 1);
            //    FX_strData = FX_strData.substring(0, FX_strData.length - 1);
            //    BG_strData = BG_strData.substring(0, BG_strData.length - 1);
            //    BX_strData = BX_strData.substring(0, BX_strData.length - 1);
            //    for (var i = 1; i < 4; i++) {
            //        for (var j = 1; j < 3; j++) {
            //            Weather_strData += $("#WH_tianqi_0" + i + "_0" + j).val() + ",";
            //        }
            //    }
            //    Weather_strData = Weather_strData.substring(0, Weather_strData.length - 1);
            //    var data = { FL_strData: FL_strData, FX_strData: FX_strData, BG_strData: BG_strData, BX_strData: BX_strData, Weather_strData: Weather_strData };
            //    submit_Ajax(id, data);
            //}
            }
            //表单05/06 近海/外海水温
            function submit_5(id) {
                if(AuthorityType!="sw")
                {
                    $.messager.show({
                        title: '操作消息',
                        msg: '当前用户没有不能提交风浪数据',
                        showType: 'show'
                    });
                    return;
                }
                Temp_strData = "";
                if (id == 5) {
                    for (var i = 1; i <= 3; i++) {
                        Temp_strData += $("#Max_SW_02_0" + i).val() + ",";
                        Temp_strData += $("#Min_SW_02_0" + i).val() + ",";
                        Temp_strData += $("#Avg_SW_02_0" + i).val() + ",";
                    }
                    Temp_strData = Temp_strData.substring(0, Temp_strData.length - 1);
                } else {
                    for (var i = 1; i <= 3; i++) {
                        Temp_strData += $("#Max_SW_01_0" + i).val() + ",";
                        Temp_strData += $("#Min_SW_01_0" + i).val() + ",";
                        Temp_strData += $("#Avg_SW_01_0" + i).val() + ",";
                    }
                    Temp_strData = Temp_strData.substring(0, Temp_strData.length - 1);
                }
                if (Temp_strData == "") {
                    $.messager.show({
                        title: '操作消息',
                        msg: '数据获取异常',
                        showType: 'show'
                    });
                    return;
                }
                var data = { datas: Temp_strData };
                submit_Ajax(id, data);
            }
            //表单提交公共方法
            function submit_Ajax(types, datas)   {
                var date = myformatter(new Date($("#tianbaoriqi").datebox("getValue")));//控件时间 格式：2015-04-28
                var tabletype="";
                if (types == 1) {
                    tabletype = "期数";
                } else if (types == 2) {
                    tabletype = "综述";
                } else if (types == 3) {
                    tabletype = "近海环境预报";
                } else if (types == 4) {
                    tabletype = "外海环境预报";
                } else if (types == 5) {
                    tabletype = "外海水温";
                } else if (types == 6) {
                    tabletype = "近海水温";
                }

                $.ajax({
                    type:"POST",
                    url: "/Ajax/SCOSpecialTableList.ashx?method=submit&type=" + types + "&date=" + date,
                    data: datas,
                    beforeSend: function () {
                        $('#w').window('open');
                    },
                    success: function (result) {
                        //alert(result);
                        if (result == "editsuccess") {
                            $.messager.show({
                                title: '操作消息',
                                msg: '表单' + tabletype + ' 修改成功',
                                showType: 'show'
                            });
                        } else if (result == "editerror") {
                            $.messager.show({
                                title: '操作消息',
                                msg: '表单' + tabletype + ' 修改失败',
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
                                msg: '表单' + tabletype + ' 添加成功',
                                showType: 'show'
                            });
                        } else if (result == "adderror") {
                            $.messager.show({
                                title: '操作消息',
                                msg: '表单' + tabletype + ' 添加失败',
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

                })
            }
        }
        //表单提交（表单编号）↑

        //保存所有   //--提交全部表单
        function alldlg_Submit() {
            //alert("保存所有");
            submit_1(1);
            submit_2(2);
            submit_3(3);
            submit_3(4);
            submit_5(5);
            submit_5(6);
        } 
        //发布全部表单
        function All_Releasetable() {
            //alert("发布全部表单");
            var d = new Date();

            var strlist = "上合峰会海洋环境预报单,";
            var date = myformatter(new Date($("#tianbaoriqi").datebox("getValue")));
           // if (date)
            var phour = $("#select_hour").val();
            var datas = { datas: strlist, dates: date, hours: phour, makewordtime: makewordtime };
            $.ajax({
                type: "POST",
                url: "/Ajax/releasetable.ashx",
                data: datas,
                beforeSend: function () {
                    $('#w').window('open');
                },
                success: function (result) {


                    $('#w1').html(result);
                    $('#w1').window('open');

                },
                complete: function () {
                    $('#w').window('close');
                }
            });
        }

    </script>
</html>

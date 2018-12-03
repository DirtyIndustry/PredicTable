<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AMTableListNew.aspx.cs" Inherits="PredicTable.AMTableListNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上午指挥处、渔政局预报列表</title>
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
    <script>
        var cx_arry = new Array("2", "27", "4", "28", "33", "36", "38");
        var sw_arry = new Array("1", "3", "37");
        var fl_arry = new Array("1", "27", "3", "28", "33", "37", "39", "44", "45");


        var type = "<%=Session["type"]%>";
       var makewordtime = "am";
       function stringToDate(str) {
           var dateStrs = str.split("-");
           var year = parseInt(dateStrs[0], 10);
           var month = parseInt(dateStrs[1], 10) - 1;
           var day = parseInt(dateStrs[2], 10);
           var date = new Date(year, month, day);
           return date;
       }



       function quanxian(type, date) {
           // alert(getdatenow()+date);
           if (getdatenow() <= date) {
               switch (type) { //all_hide(); show_bytype(cx_arry);
                   case "cx": all_disabled(); cx_isabled(); tb_isabled(); $("#yby_type").val("潮汐"); break;//潮汐能填写
                   case "fl": all_isabled(); cx_disabled(); sw_disabled(); tb_isabled(); $("#yby_type").val("风、海浪"); fl_isabled(); break;//风浪能填写
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


       $(function () {
           //getuserinfo(type);
           quanxian(type, getdatenow());
           setVisiable();
           $("#btn_show").click(function () {
               if ($("#btn_show").val() == "显示所有") {
                   all_show();
                   $("#btn_show").val("显示可编辑");
               } else if ($("#btn_show").val() == "显示可编辑") {
                   switch (type) {
                       case "cx": all_hide(); show_bytype(cx_arry); break;//潮汐能填写
                       case "fl": all_hide(); show_bytype(fl_arry); break;//风浪能填写
                       case "sw": all_hide(); show_bytype(sw_arry); break;//水温能填写
                       case "hb": all_hide(); show_bytype(hb_arry); break;//海冰能填写
                       default: break;// 都不能填写
                   }
                   $("#btn_show").val("显示所有");
               }
           });
           //设置页面初始化时的可见性
           function setVisiable() {
               switch (type) {
                   case "cx": all_hide(); show_bytype(cx_arry); break;//潮汐能填写
                   case "fl": all_hide(); show_bytype(fl_arry); break;//风浪能填写
                   case "sw": all_hide(); show_bytype(sw_arry); break;//水温能填写
                   case "hb": all_hide(); show_bytype(hb_arry); break;//海冰能填写
                   default: break;// 都不能填写
               }
           }
           ////  var topWin = window.top.document.getElementById("username").contentWindow;
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
           for (var i = 1; i <= 40; i++) {
               if (i < 10) {
                   str = "#ddlg_0" + i;
               } else {
                   str = "#ddlg_" + i;
               }
               $(str).css("display", "");
               $("#lx_" + i).css("display", "");
           }
       }
       //隐藏所有
       function all_hide() {
           var str = "";
           for (var i = 1; i <= 40; i++) {
               if (i < 10) {
                   str = "#ddlg_0" + i;
               } else {
                   str = "#ddlg_" + i;
               }
               $(str).css("display", "none");
               $("#lx_" + i).css("display", "none");
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



       //根据浪高判断海浪级别
       function setlevel(num) {
           //num = parseInt(num);
           num = num * 1;
           var level;
           if (num == 0) { level = "无浪"; }
           else if (num < 0.1) { level = "微浪"; }
           else if (num >= 0.1 && num < 0.5) { level = "小浪"; }
           else if (num >= 0.5 && num < 1.25) { level = "轻浪"; }
           else if (num >= 1.25 && num < 2.5) { level = "中浪"; }
           else if (num >= 2.5 && num < 4) { level = "大浪"; }
           else if (num >= 4 && num < 6) { level = "巨浪"; }
           else if (num >= 6 && num < 9) { level = "狂浪"; }
           else if (num >= 9 && num < 14) { level = "狂涛"; }
           else if (num >= 14) { level = "怒涛"; }
           return level;
       }

       //滑动到指定位置
       function click_scroll(id) {
           var scroll_offset = $("#" + id).offset(); //得到pos这个div层的offset，包含两个值，top和left
           $("body,html").animate({
               scrollTop: (scroll_offset.top - 200)  //让body的scrollTop等于pos的top，就实现了滚动
           }, 1000);
       }
       function createTr(stationCode, i) {

           return "<td class='SJ_0" + i + "'>*月*号</td>"
                   + "<td><input id='" + stationCode + "_BG_0" + i + "' type='text' maxlength='15' /></td>"
                   + "<td><input id='" + stationCode + "_BX_0" + i + "' type='text' maxlength='15' /></td>"
                   + "<td><input id='" + stationCode + "_FX_0" + i + "' type='text' maxlength='15' /></td>"
                   + "<td><input id='" + stationCode + "_FL_0" + i + "' type='text' maxlength='15' /></td>"
                   + "<td><input id='" + stationCode + "_SW_0" + i + "' type='text' maxlength='15' /></td>"
                   + "</tr>";
       };


       function show72hOr7d(d) { //渤海海区及黄河海港风、浪预报 :周一显示7天预报单，其它显示72h预报单
           //var d=new Date()
           var trCount = 3;
           if (d.getDay() == 1) {
               trCount = 7;
           }
           var bhHtml = "";
           var hhHtml = "";

           for (var i = 1; i <= trCount ; i++) {
               var bhTr = "";
               var hhhgTr = "";
               if (i == 1) {
                   bhTr = "<tr><td rowspan='" + trCount + "'>渤海</td>" + createTr("BH", i);
                   hhhgTr = "<tr><td rowspan='" + trCount + "'>黄河海港</td>" + createTr("HHHG", i);
               }
               else {
                   bhTr = "<tr>" + createTr("BH", i);
                   hhhgTr = "<tr>" + createTr("HHHG", i);

               }
               bhHtml = bhHtml + bhTr;
               hhHtml = hhHtml + hhhgTr;
           }

           //alert(bhHtml+hhHtml);
           $("#ddlg_01 tbody").html(bhHtml + hhHtml);
           //$("#ddlg_01 tbody").html("<tr><td>1</td><td>2</td><td></td><td></td><td></td><td></td><td></td></tr>");
           if (trCount == 3) {
               $("#lx_1").text("一、72小时渤海海区及黄河海港风、浪预报"); $("#ddlg_01title").text("一、72小时渤海海区及黄河海港风、浪预报");

           }
           else if (trCount == 7) {
               $("#lx_1").text("一、7天渤海海区及黄河海港风、浪预报");
               $("#ddlg_01title").text("一、7天渤海海区及黄河海港风、浪预报");
           }

           //波高
           $("input[id*='BG']").blur(function () {
               valisection(this, tb_bg, tb_bg2);
           });
           //波向
           $("input[id*='BX']").blur(function () {
               strsection(this, tb_bx);
           });
           //风向
           $("input[id*='FX']").blur(function () {
               strsection(this, tb_fx);
           });
           //风力
           $("input[id*='FL']").blur(function () {
               valisection(this, tb_fl, tb_fl2);
           });
           //水温
           $("input[id*='SW']").blur(function () {
               valisection(this, tb_sw, tb_sw2);
           });
           //}
       }

       function createTrForPortTide(stationCode, i) { //港口潮位预报

           return "<td class='SJ_0" + i + "'>*月*号</td>"
                   + "<td><input id='" + stationCode + "_01G_SJ_0" + i + "' type='text' maxlength='20' /></td>"
                   + "<td><input id='" + stationCode + "_01G_CW_0" + i + "' type='text' maxlength='20' /></td>"
                   + "<td><input id='" + stationCode + "_01D_SJ_0" + i + "' type='text' maxlength='20' /></td>"
                   + "<td><input id='" + stationCode + "_01D_CW_0" + i + "' type='text' maxlength='20' /></td>"
                   + "<td><input id='" + stationCode + "_02G_SJ_0" + i + "' type='text' maxlength='20' /></td>"
                   + "<td><input id='" + stationCode + "_02G_CW_0" + i + "' type='text' maxlength='20' /></td>"
                   + "<td><input id='" + stationCode + "_02D_SJ_0" + i + "' type='text' maxlength='20' /></td>"
                   + "<td><input id='" + stationCode + "_02D_CW_0" + i + "' type='text' maxlength='20' /></td>"
                   + "</tr>";
       };


       function show72hOr7dForPortTide(d) { //港口潮位预报 :周一显示7天预报单，其它显示72h预报单
           //var d=new Date()
           var trCount = 3;
           if (d.getDay() == 1) {
               trCount = 7;
           }

           var bhHtml = "";
           var hhHtml = "";

           for (var i = 1; i <= trCount; i++) {
               var bhTr = "";
               var hhhgTr = "";
               if (i == 1) {
                   bhTr = "<tr><td rowspan='" + trCount + "'>龙口港</td>" + createTrForPortTide("LKG", i);
                   hhhgTr = "<tr><td rowspan='" + trCount + "'>黄河海港</td>" + createTrForPortTide("HHHG", i);
               }
               else {
                   bhTr = "<tr>" + createTrForPortTide("LKG", i);
                   hhhgTr = "<tr>" + createTrForPortTide("HHHG", i);

               }
               bhHtml = bhHtml + bhTr;
               hhHtml = hhHtml + hhhgTr;
           }

           //alert(bhHtml+hhHtml);
           $("#ddlg_02 tbody").html(bhHtml + hhHtml);

           $("input[id*='CW']").blur(function () {
               valisection(this, tb_cw, tb_cw2);
           });
           //潮汐时分
           $("input[id*='SJ']").blur(function () {
               hmsection(this);
           });
       }


    </script>
    <script>
        //表单5自动验证
        $(function () {
            $("#BH_HL_01").focus(function () { //dgl_03

                var numf = $("#BH_GDF_01").val();
                var nume = $("#BH_GDE_01").val();
                if (numf != "" && nume != "") {
                    if (setlevel(numf) != setlevel(nume)) {
                        $("#BH_HL_01").val(setlevel(numf) + "-" + setlevel(nume));
                    }
                    else {
                        $("#BH_HL_01").val(setlevel(numf));
                    }
                }
            });
            $("#HHBB_HL_01").focus(function () { //dgl_03
                var numf = $("#HHBB_GDF_01").val();
                var nume = $("#HHBB_GDE_01").val();
                if (numf != "" && nume != "") {
                    if (setlevel(numf) != setlevel(nume)) {
                        $("#HHBB_HL_01").val(setlevel(numf) + "-" + setlevel(nume));
                    }
                    else {
                        $("#HHBB_HL_01").val(setlevel(numf));
                    }
                }
            });

            $("#BH_HL_401").focus(function () {//dgl_05
                var numf = $("#BH_GDF_401").val();
                var nume = $("#BH_GDE_401").val();
                if (numf != "" && nume != "") {
                    if (setlevel(numf) != setlevel(nume)) {
                        $("#BH_HL_401").val(setlevel(numf) + "-" + setlevel(nume));
                    }
                    else {
                        $("#BH_HL_401").val(setlevel(numf));
                    }
                }
            });
            $("#HHBB_HL_401").focus(function () {//dgl_05
                var numf = $("#HHBB_GDF_401").val();
                var nume = $("#HHBB_GDE_401").val();
                if (numf != "" && nume != "") {
                    if (setlevel(numf) != setlevel(nume)) {
                        $("#HHBB_HL_401").val(setlevel(numf) + "-" + setlevel(nume));
                    }
                    else {
                        $("#HHBB_HL_401").val(setlevel(numf));
                    }
                }
            });
            $("#HHZB_HL_401").focus(function () {//dgl_05
                var numf = $("#HHZB_GDF_401").val();
                var nume = $("#HHZB_GDE_401").val();
                if (numf != "" && nume != "") {
                    if (setlevel(numf) != setlevel(nume)) {
                        $("#HHZB_HL_401").val(setlevel(numf) + "-" + setlevel(nume));
                    }
                    else {
                        $("#HHZB_HL_401").val(setlevel(numf));
                    }
                }
            });

            $("#HHNB_HL_401").focus(function () {//dgl_05
                var numf = $("#HHNB_GDF_401").val();
                var nume = $("#HHNB_GDE_401").val();
                if (numf != "" && nume != "") {
                    if (setlevel(numf) != setlevel(nume)) {
                        $("#HHNB_HL_401").val(setlevel(numf) + "-" + setlevel(nume));
                    }
                    else {
                        $("#HHNB_HL_401").val(setlevel(numf));
                    }
                }
            });

        });
    </script>
</head>
<body>
    <iframe width="0" height="0" src="SessionKeeper.asp"></iframe>



    <form id="form2" runat="server">

        <%--<div class="bodywrapper">--%>

        <div <%--class="centercontent"--%>>
            <div id="contentwrapper" class="contentwrapper">
                <div>
                    <div style="position: fixed; top: 0px; left: 20px; z-index: 2; display: none">
                        <ul id="leixing1">
                            <li id="swbd" style="border-color: #fb9337; background-color: #fb9337; color: white; font-weight: bold">上午预报表单</li>
                            <li id="lx_1" onclick="click_scroll('ddlg_01')">一、72小时渤海海区及黄河海港风、浪预报</li>
                            <li id="lx_2" onclick="click_scroll('ddlg_02')">二、72小时港口潮位预报</li>
                            <li id="lx_27" onclick="click_scroll('ddlg_27')">三、3天海洋水文气象预报综述</li>
                            <li id="lx_3" onclick="click_scroll('ddlg_03')">四、预计未来24小时海浪、水温预报</li>
                            <li id="lx_4" onclick="click_scroll('ddlg_04')">五、24小时潮位预报</li>
                            <li id="lx_28" onclick="click_scroll('ddlg_28')">六、24小时水文气象预报综述</li>
                            <li id="lx_39" onclick="click_scroll('ddlg_39')">七、海上丝绸之路三天海浪、气象预报</li>
                            <li id="lx_38" onclick="click_scroll('ddlg_38')">八、海上丝绸之路三天潮汐预报</li>
                            <li id="lx_29" onclick="click_scroll('ddlg_29')">九、7天渤海海区及黄河海港风、浪预报</li>
                            <li id="lx_30" onclick="click_scroll('ddlg_30')">十、7天海洋水文气象预报综述</li>
                            <li id="lx_31" onclick="click_scroll('ddlg_31')">十一、7天港口潮位预报</li>
                            <li id="lx_25" onclick="click_scroll('ddlg_25')">十二、指挥处上午预报</li>
                            <li id="lx_23" onclick="click_scroll('ddlg_24')">十三、东营胜利油田专项海冰周报</li>
                            <li id="lx_06" onclick="click_scroll('ddlg_32')">十四、东营胜利油田专项海温周报</li>
                            <%--<li id="lx_37" onclick="click_scroll('ddlg_33')">十三、72小时东营神仙沟挡潮闸专项预报</li>--%>
                        </ul>
                    </div>
                </div>
                <!--表单类型-->
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
                        &nbsp; 时间：
                            
                           
                        <select id="select_hour" name="select" class="uniformselect" style="height: 21px;">
                        </select>
                        时&nbsp; 
                        <input type="button" id="btn_select" class="stdbtn" value="查询" />
                        <input type="button" id="btn_czrz" onclick="$('#dlg_czrz').dialog('open')" class="stdbtn" value="操作日志" />
                        <input type="button" id="btn_show" class="stdbtn" value="显示所有" />
                        <%--<input type="button" id="checkmodel" onclick="$('#dlg_xzmb').dialog('open'); click_scroll('dlg_xzmb') " class="stdbtn" value="选择模版并发布" />--%>
                        <input type="button" id="setall" onclick="alldlg_Submit()" class="stdbtn" value="保存所有" />
                        <input type="button" id="btnrole" class="stdbtn" value="验证所有" />
                        <input type="button" id="ReleasetableAll" onclick="All_Releasetable()" class="stdbtn" value="发布表单" />
                        <%-- <input type="button" id ="btn_check " onclick=" check()" class="stdbtn" value="查看预报单" />--%>
                        <br />
                    </div>
                </div>
                <!--表单信息-->

                <div class="dlgs" id="ddlg_44" style="width: auto; height: 950px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head0" colspan="7">上午一、上午指挥处、渔政局预报</th>
                            </tr>
                            <tr>
                                <th class="head0">时效</th>
                                <th class="head1">海区</th>
                                <th class="head0">天气现象</th>
                                <th class="head1">风向(方位)</th>
                                <th class="head0">风力(级)</th>
                                <th class="head1">波高(米)</th>
                                <th class="head0">波向(方位)</th>
                            </tr>
                        </thead>

                        <tbody style="text-align: center">
                            <tr>
                                <td rowspan="7" class="SJ_0001XW" style="border-bottom: 2px #d8d8d8 solid;">*月*号08时至*月*号08时</td>
                                <td style="border-left: 1px #d8d8d8 solid;">青岛市</td>
                                <td>
                                    <input id="ZHC_XW_QD_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QD_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QD_FL_01" type="text" maxlength="15" /></td>
                                <td colspan="2">气温：
                                        <input id="ZHC_XW_QD_BX_01" type="text" maxlength="15" />℃</td>
                                <%--                                    <td>
                                        <input id="ZHC_XW_QD_BX_01" type="text" maxlength="15" /></td>--%>
                            </tr>
                            <tr>
                                <td>青岛近海</td>
                                <td>
                                    <input id="ZHC_XW_QDJH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>渤海</td>
                                <td>
                                    <input id="ZHC_XW_BH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>渤海海峡</td>
                                <td>
                                    <input id="ZHC_XW_BHHX_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BHHX_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BHHX_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BHHX_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BHHX_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="ZHC_XW_NHH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="ZHC_XW_MHH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="table-bottom">黄海南部</td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_TX_01" type="text" maxlength="15" /></td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_FX_01" type="text" maxlength="15" /></td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_FL_01" type="text" maxlength="15" /></td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_BG_01" type="text" maxlength="15" /></td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_BX_01" type="text" maxlength="15" /></td>
                            </tr>


                            <tr>
                                <td rowspan="5" class="SJ_0102XW" style="border-bottom: 2px #d8d8d8 solid;">*月*号08时至*月*号08时</td>
                                <td style="border-left: 1px #d8d8d8 solid;">青岛近海</td>
                                <td>
                                    <input id="ZHC_XW_QDJH_TX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_BX_02" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td>渤海</td>
                                <%--<td rowspan="4" class="SJ_0102XW">*月*号08时至*月*号08时</td>--%>
                                <td>
                                    <input id="ZHC_XW_BH_TX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_BX_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="ZHC_XW_NHH_TX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_BX_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="ZHC_XW_MHH_TX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_BX_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="table-bottom">黄海南部</td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_TX_02" type="text" maxlength="15" /></td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_FX_02" type="text" maxlength="15" /></td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_FL_02" type="text" maxlength="15" /></td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_BG_02" type="text" maxlength="15" /></td>
                                <td class="table-bottom">
                                    <input id="ZHC_XW_SHH_BX_02" type="text" maxlength="15" /></td>
                            </tr>


                            <tr>
                                <td rowspan="5" class="SJ_0203XW">*月*号08时至*月*号08时</td>
                                <td style="border-left: 1px #d8d8d8 solid;">青岛近海</td>
                                <td>
                                    <input id="ZHC_XW_QDJH_TX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QDJH_BX_03" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <%--<td rowspan="4" class="SJ_0203XW">*月*号08时至*月*号08时</td>--%>
                                <td>渤海</td>
                                <td>
                                    <input id="ZHC_XW_BH_TX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_BH_BX_03" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="ZHC_XW_NHH_TX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_NHH_BX_03" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="ZHC_XW_MHH_TX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_MHH_BX_03" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海南部</td>
                                <td>
                                    <input id="ZHC_XW_SHH_TX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_BX_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>


                    <%--<table id="addtable" cellpadding="0" cellspacing="0" border="0" class="stdtable" style="margin-top: 20px">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col />
                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head1">海区</th>
                                <th class="head0">时效</th>
                                <th class="head1">风向(方位)</th>
                                <th class="head0">风力(级)</th>
                                <th class="head1">波高(米)</th>
                                <th>
                                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="addtr()" value="增加海区" />
                                    预报天数：
                                    <input id="day" type="text" style="width: 30px" maxlength="2" onkeyup="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" onafterpaste="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" /></th>
                            </tr>
                        </thead>
                    </table>--%>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(44)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(44)" value="提交" />
                </div>
                <!--表单40.上午一、指挥处上午预报-->
                <%--<div class="dlgs" id="ddlg_45" style="height: 390px; padding: 10px;">

                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th id="ddlg_01title" class="head0" colspan="7">上午二、渔政局风向风力浪高预报</th>
                            </tr>
                            <tr>
                                <th class="head0">海区</th>
                                <th class="head1">日期</th>
                                <th class="head0">风向（方位）</th>
                                <th class="head1">风力（级）</th>
                                <th class="head0">浪高（h）</th>
                            </tr>
                        </thead>
                        <tbody id="ddlg_01_tbody" style="text-align: center">
                            <tr>
                                <td rowspan="2">旅顺</td>
                                <td id="LS_YBTIME_01" class="SJ_01">*月*号</td>
                                <td>
                                    <input id="LS_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="LS_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="LS_BG_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td id="LS_YBTIME_02" class="SJ_02">*月*号</td>
                                <td>
                                    <input id="LS_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="LS_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="LS_BG_02" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td rowspan="2">烟台</td>
                                <td id="YT_YBTIME_01" class="SJ_01">*月*号</td>
                                <td>
                                    <input id="YT_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YT_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YT_BG_01" type="text" maxlength="15" /></td>

                            </tr>
                            <tr>
                                <td id="YT_YBTIME_02" class="SJ_02">*月*号</td>
                                <td>
                                    <input id="YT_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YT_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YT_BG_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="2">威海</td>
                                <td id="WH_YBTIME_01" class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WH_BG_01" type="text" maxlength="15" /></td>

                            </tr>
                            <tr>
                                <td id="WH_YBTIME_02" class="SJ_02">*月*号</td>
                                <td>
                                    <input id="WH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WH_BG_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="2">石岛</td>
                                <td id="SD_YBTIME_01" class="SJ_01">*月*号</td>
                                <td>
                                    <input id="SD_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="SD_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="SD_BG_01" type="text" maxlength="15" /></td>

                            </tr>
                            <tr>
                                <td id="SD_YBTIME_02" class="SJ_02">*月*号</td>
                                <td>
                                    <input id="SD_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="SD_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="SD_BG_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="2">责任海区1</td>
                                <td id="ZRHQ_YBTIME_01" class="SJ_01">*月*号</td>
                                <td>
                                    <input id="ZRHQ_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZRHQ_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZRHQ_BG_01" type="text" maxlength="15" /></td>

                            </tr>
                            <tr>
                                <td id="ZRHQ_YBTIME_02" class="SJ_02">*月*号</td>
                                <td>
                                    <input id="ZRHQ_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZRHQ_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZRHQ_BG_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="2">责任海区2</td>
                                <td id="ZRHQ_YBTIME_03" class="SJ_01">*月*号</td>
                                <td>
                                    <input id="ZRHQ_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZRHQ_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZRHQ_BG_03" type="text" maxlength="15" /></td>

                            </tr>
                            <tr>
                                <td id="ZRHQ_YBTIME_04" class="SJ_02">*月*号</td>
                                <td>
                                    <input id="ZRHQ_FX_04" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZRHQ_FL_04" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZRHQ_BG_04" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="1">青岛</td>
                                <td id="QD_YBTIME_02" class="SJ_02">*月*号</td>
                                <td>
                                    <input id="QD_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="QD_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="QD_BG_01" type="text" maxlength="15" /></td>

                            </tr>
                        </tbody>
                    </table>

                    <table id="addtable2" cellpadding="0" cellspacing="0" border="0" class="stdtable" style="margin-top: 20px">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col />
                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head1">海区</th>
                                <th class="head0">时效</th>
                                <th class="head1">风向(方位)</th>
                                <th class="head0">风力(级)</th>
                                <th class="head1">波高(米)</th>
                                <th>
                                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="addtr2()" value="增加海区" />
                                    预报天数：
                                    <input id="day2" type="text" style="width: 30px" maxlength="2" onkeyup="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" onafterpaste="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" /></th>
                            </tr>
                        </thead>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(45)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(45)" value="提交" />

                </div>--%>
                <!-- 表单41. 上午二、渔政局风向风力浪高预报-->

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
                        预报值班：<input id="ZhibanTel" type="text" style="width: 200px" value="0532-58750688"/>
                        &nbsp;&nbsp;
                    </div>
                    <div style="margin-bottom: 5px">
                        &nbsp;&nbsp;&nbsp;发布时间：
                        <input id="Fabushijian" type="text" style="width: 200px" />
                        &nbsp;&nbsp;<%--传真：--%>
                        <input id="Chuanzhen" type="hidden" style="width: 200px" />
                        预报发送：<input id="SendTel" type="text" style="width: 200px" value="0532-58750626"/>
                        &nbsp;&nbsp;
                    </div>
                    海浪预报员：<%--<input id="Hailang" type="text" style="width: 200px" />--%>
                    <select id="Hailang" class="uniformselect" style="width: 100%; height: 19px;">
                        <option value="">请选择</option>
                    </select>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        潮汐预报员：<%--<input id="Chaoxi" type="text" style="width: 200px" />--%>
                    <select id="Chaoxi" class="uniformselect" style="width: 100%; height: 25px;">
                        <option value="" style="font-size: 18px; font-weight: 500;">请选择</option>
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
                    <input type="hidden" id="iHailang" value="" />
                    <input type="hidden" id="iChaoxi" value="" />
                    <input type="hidden" id="iShuiwen" value="" />
                </div>
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
                <!--填报信息-->

                <div id="dlg_czrz" class="easyui-dialog" title="操作日志" data-options="iconCls:'icon-save'" style="width: 800px; height: 530px; padding: 10px;">
                    <iframe width="100%" id="win" height="435" name="czrz" frameborder="0" src="Logbyuser.aspx"></iframe>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_czrz').dialog('close'); " value="取消" />
                </div>
                <!--操作日志-->
                <!--<div id="basicform" style="clear: both" class="subcontent">-->
                <div id="basicform" style="position: fixed; bottom: 0px; left: 20px; z-index: 2">
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
                                getValue();
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

                    ////由于加载顺序，此处为覆盖预报员信息
                    function getValue() {
                        var chaoxi = $("#iChaoxi").val();
                        var hailang = $("#iHailang").val();
                        var shuiwen = $("#iShuiwen").val();
                        if (chaoxi != "" && chaoxi != null) {
                            $("#uniform-Chaoxi span").text(chaoxi);
                            $("#uniform-Chaoxi span").attr("code", chaoxi);
                            //$("#ChaoxiTel").val(tel);
                        }
                        if (shuiwen != "" && shuiwen != null) {
                            $("#uniform-Shuiwen span").text(shuiwen);
                            $("#uniform-Shuiwen span").attr("code", shuiwen);
                            //$("#ShuiwenTel").val(tel);
                        }
                        if (hailang != "" && hailang != null) {
                            $("#uniform-Hailang span").text(hailang);
                            $("#uniform-Hailang span").attr("code", hailang);
                            //$("#HailangTel").val(tel);
                        }
                    }
                </script>

                <script type="text/javascript">
                    //模板禁用
                    $('#checkmodel').click(function () {
                        $.ajax({
                            type: "POST",
                            url: '/Ajax/getPublishedTemplates.ashx',

                            success: function (result) {
                                if (result != "notFound") {
                                    modelAbled(result);
                                }
                            }
                        });
                    });
                    //解析模板名称并控制可用（暂时为改成红色）
                    function modelAbled(result) {
                        result = result.substring(0, result.length - 1);
                        var names = result.split(',');
                        for (var i = 0; i < names.length; i++) {
                            var name = names[i];
                            $("#modellist span").each(function () {
                                if ($(this).attr("name") == name) {
                                    $(this).css("color", "red");
                                }
                            });

                        }
                    }
                </script>
                <!--选择模版-->
                <script type="text/javascript" language="javascript">
                    Array.prototype.unique3 = function () {
                        var res = [];
                        var json = {};
                        for (var i = 0; i < this.length; i++) {
                            if (!json[this[i]]) {
                                res.push(this[i]);
                                json[this[i]] = 1;
                            }
                        }
                        return res;
                    }
                    function addtr() {
                        var i = 0;
                        var list = [];
                        $("tr[name='trname']").each(function () {
                            list.push($(this).attr("id"));
                        })
                        if (list == "") {
                            i = 1;
                        }
                        else {
                            // alert(list.unique3());
                            i = list.unique3().length + 1;

                        }
                        var tr = "";
                        var num = $("#day").val();
                        if (num == "" || num == null) {
                            //  alert("请填写预报天数");
                        }
                        else {
                            var tab = document.getElementById("addtable");
                            //表格行数
                            var rows = tab.rows.length;


                            for (j = 1; j <= num; j++) {
                                //i = (rows - 1) / 2
                                if (j == 1) {
                                    tr += "<tr  id =" + i + " name='trname'><td rowspan='" + num + "'> <input id='ZHC_XW_SA0" + i + "' type='text' maxlength='15' /></td>" +
                                    "<td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td rowspan='" + num + "'><input class='button' id =" + i + " type='button' value='删除' onclick='deletr(this)'></td></tr>";

                                }
                                else {
                                    tr += "<tr id=" + i + " name='trname'><td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>  </tr>";
                                }

                            }
                            $("#addtable").append(tr);
                            var today = new Date(); // 获取今天时间
                            GetDate(today); //根据当前年月日 推算未来年月日

                        }
                    }
                    function deletr(obj) {
                        var list = [];
                        var num = 0;
                        $("tr[name='trname']").each(function () {
                            list.push($(this).attr("id"));
                        })

                        if (list == "") {

                        }
                        else {
                            for (i = 0; i < list.length ; i++) {
                                // alert(list[i]);
                                if (list[i] == (obj.id)) {
                                    num++;
                                }
                            }
                            for (j = num; j > 0; j--) {
                                $("#" + obj.id).remove();
                            }
                        }
                    }


                    function addtr2() {
                        var i = 0;
                        var list = [];
                        $("tr[name='trname2']").each(function () {
                            list.push($(this).attr("id"));
                        })
                        if (list == "") {
                            i = 1;
                        }
                        else {
                            // alert(list.unique3());
                            i = list.unique3().length + 1;

                        }
                        var tr = "";
                        var num = $("#day2").val();
                        if (num == "" || num == null) {
                            //  alert("请填写预报天数");
                        }
                        else {
                            var tab = document.getElementById("addtable2");
                            //表格行数
                            var rows = tab.rows.length;


                            for (j = 1; j <= num; j++) {
                                //i = (rows - 1) / 2
                                if (j == 1) {
                                    tr += "<tr  id =" + i + " name='trname2'><td rowspan='" + num + "'> <input id='YZJ_XW_SA0" + i + "' type='text' maxlength='15' /></td>" +
                                    "<td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td rowspan='" + num + "'><input class='button' id =" + i + " type='button' value='删除' onclick='deletr2(this)'></td></tr>";

                                }
                                else {
                                    tr += "<tr id=" + i + " name='trname2'><td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>  </tr>";
                                }

                            }
                            $("#addtable2").append(tr);
                            var today = new Date(); // 获取今天时间
                            GetDate(today); //根据当前年月日 推算未来年月日

                        }
                    }
                    function deletr2(obj) {
                        var list = [];
                        var num = 0;
                        $("tr[name='trname2']").each(function () {
                            list.push($(this).attr("id"));
                        })

                        if (list == "") {

                        }
                        else {
                            for (i = 0; i < list.length ; i++) {
                                // alert(list[i]);
                                if (list[i] == (obj.id)) {
                                    num++;
                                }
                            }
                            for (j = num; j > 0; j--) {
                                $("#" + obj.id).remove();
                            }
                        }
                    }

                    var check = document.getElementsByName("check");

                    for (var i = 1; i <= check.length; i++) {

                        if (check[i].checked) {

                            document.getElementById('table1').deleteRow(i);

                        }

                    }

                </script>

                <br />
            </div>

            <!--subcontent-->
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

            //波高
            $("input[id*='BG']").blur(function () {
                valisection(this, tb_bg, tb_bg2);
            });
            //波向
            $("input[id*='BX']").blur(function () {
                strsection(this, tb_bx);
            });
            //风向
            $("input[id*='FX']").blur(function () {
                strsection(this, tb_fx);
            });
            //涌向
            $("input[id*='YX']").blur(function () {
                strsection(this, tb_yx);
            });
            //天气
            $("input[id*='TQ']").blur(function () {
                strsection(this, tb_tq);
            });
            //风力
            $("input[id*='FL']").blur(function () {
                valisection(this, tb_fl, tb_fl2);
            });
            //浪高
            $("input[id*='LG']").blur(function () {
                valisection(this, tb_lg, tb_lg2);
            });
            $("input[id*='_GD']").blur(function () {
                valisection(this, tb_lg, tb_lg2);
            });

            //水温
            $("input[id*='SW']").blur(function () {
                valisection(this, tb_sw, tb_sw2);
            });
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
            $("input[id*='BG']").each(function () {
                valisection(this, tb_bg, tb_bg2);
            });
            //波向
            $("input[id*='BX']").each(function () {
                strsection(this, tb_bx);
            });
            //风向
            $("input[id*='FX']").each(function () {
                strsection(this, tb_bx);
            });
            //涌向
            $("input[id*='YX']").each(function () {
                strsection(this, tb_yx);
            });
            //风力
            $("input[id*='FL']").each(function () {
                valisection(this, tb_fl, tb_fl2);
            });
            //浪高
            $("input[id*='LG']").each(function () {
                valisection(this, tb_lg, tb_lg2);
            });
            $("input[id*='_GD']").each(function () {
                valisection(this, tb_lg, tb_lg2);
            });
            //天气
            $("input[id*='TQ']").each(function () {
                strsection(this, tb_tq);
            });

            //水温
            $("input[id*='SW']").each(function () {
                valisection(this, tb_sw, tb_sw2);
            });
        }
        //直接点击发布按钮 （根据用户提供的信息，将预报单分类。点击此按钮根据分类直接生成上午预报单，特殊情况只有周一生成周报，其他时候不生成）
        //function check()
        //{
        // window.open("ftp://用户名:密码@ip地址:端口号");
        //}

        function All_Releasetable() {
            var d = new Date();
            var week = d.getDay();
            var strlist = "";

            strlist = "上午的指挥处预报";//生成文件中不含周报

            var date = myformatter(new Date($("#tianbaoriqi").datebox("getValue")));//控件时间 格式：2015-04-28
            var phour = $('#select_hour').val();
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
        //发布表单
        $("#Releasetable").click(function () {
            var strlist = "";
            if ($('#modellist .checked').length <= 0) {
                $.messager.show({
                    title: '发布表单操作',
                    msg: '请先选择模板',
                    showType: 'show'
                });
            }
            else {

                $("#modellist .checked input").each(function () {
                    strlist += $("#modellist [for='" + $(this).attr("id") + "']").html() + ",";
                    // alert(strlist);
                });
                //ajax请求 模板生成
                strlist = strlist.substring(0, strlist.length - 1);//去掉最后一个，
                var date = myformatter(new Date($("#tianbaoriqi").datebox("getValue")));//控件时间 格式：2015-04-28
                var phour = $('#select_hour').val();
                var datas = { datas: strlist, dates: date, hours: phour, makewordtime: makewordtime };
                $.ajax({
                    type: "POST",
                    url: "/Ajax/releasetable.ashx",
                    data: datas,
                    beforeSend: function () {
                        $('#w').window('open');
                    },
                    success: function (result) {
                        //$.messager.show({
                        //    title: '操作完成',
                        //    msg: '操作完成',
                        //    showType: 'fade',
                        //    style: {
                        //        right: '',
                        //        bottom: ''
                        //    }
                        //});

                        $('#w1').html(result);
                        $('#w1').window('open');

                    },
                    complete: function () {
                        $('#w').window('close');
                    }
                });



            }
        });

        //发布表单
        $("#Releasetable1").click(function () {
            var strlist = "";
            if ($('#modellist .checked').length <= 0) {
                $.messager.show({
                    title: '发布表单操作',
                    msg: '请先选择模板',
                    showType: 'show'
                });
            }
            else {

                $("#modellist .checked input").each(function () {
                    strlist += $("#modellist [for='" + $(this).attr("id") + "']").html() + ",";
                    // alert(strlist);
                });
                //ajax请求 模板生成
                strlist = strlist.substring(0, strlist.length - 1);//去掉最后一个，
                var date = myformatter(new Date($("#tianbaoriqi").datebox("getValue")));//控件时间 格式：2015-04-28
                var phour = $('#select_hour').val();
                var datas = {
                    datas: strlist,
                    dates: date,
                    hours: phour,
                    makewordtime: makewordtime

                };
                $.ajax({
                    type: "POST",
                    url: "/Ajax/releasetable.ashx",
                    data: datas,
                    beforeSend: function () {
                        $('#w').window('open');
                    },
                    success: function (result) {
                        //$.messager.show({
                        //    title: '操作完成',
                        //    msg: '操作完成',
                        //    showType: 'fade',
                        //    style: {
                        //        right: '',
                        //        bottom: ''
                        //    }
                        //});

                        $('#w1').html(result);
                        $('#w1').window('open');

                    },
                    complete: function () {
                        $('#w').window('close');
                    }
                });



            }
        });

        var date = new Date();
        var num = 0;
        var nqyname;
        //↓表单提交(表单编号)
        function dlg_Submit(id) {//（无当天数据保存、有当天数据修改）↓
            switch (id) {
                case 23: submit_23(id); break;//填报信息
                case 44: submit_44(id); break;//上午指挥处
                    //case 45: submit_45(id); break;//上午渔政局
                default:
            }
        }

        //保存所有
        function alldlg_Submit() {//（无当天数据保存、有当天数据修改）↓
            submit_23(23); //填报信息
            submit_44(44); //上午指挥处
            //submit_45(45); //上午渔政局
        }

        //表单数据拼接 从左至右 从上至下
        {
            //表单23
            function submit_23(id) {
                var str_data = "";
                str_data += $("#select_hour").val() + ",";
                str_data += $("#Fabudanwei").val() + ",";
                str_data += $("#Tel").val() + ",";
                // str_data += $("#Fabushijian").val() + ",";
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

            //上午指挥处
            function submit_44(id) {
                var str_data = "";

                str_data += $("#ZHC_XW_QD_TX_01").val() + ",";
                str_data += $("#ZHC_XW_QD_FX_01").val() + ",";
                str_data += $("#ZHC_XW_QD_FL_01").val() + ",";
                str_data += ",";
                //str_data += $("#ZHC_XW_QD_SW_01").val() + ",";//青岛WAVEDIRECTION字段存水温
                //str_data += $("#ZHC_XW_QDJH_BG_01").val() + ",";
                str_data += $("#ZHC_XW_QD_BX_01").val() + ",";

                str_data += $("#ZHC_XW_QDJH_TX_01").val() + ",";
                str_data += $("#ZHC_XW_QDJH_FX_01").val() + ",";
                str_data += $("#ZHC_XW_QDJH_FL_01").val() + ",";
                str_data += $("#ZHC_XW_QDJH_BG_01").val() + ",";
                str_data += $("#ZHC_XW_QDJH_BX_01").val() + ",";

                str_data += $("#ZHC_XW_BH_TX_01").val() + ",";
                str_data += $("#ZHC_XW_BH_FX_01").val() + ",";
                str_data += $("#ZHC_XW_BH_FL_01").val() + ",";
                str_data += $("#ZHC_XW_BH_BG_01").val() + ",";
                str_data += $("#ZHC_XW_BH_BX_01").val() + ",";

                str_data += $("#ZHC_XW_BHHX_TX_01").val() + ",";
                str_data += $("#ZHC_XW_BHHX_FX_01").val() + ",";
                str_data += $("#ZHC_XW_BHHX_FL_01").val() + ",";
                str_data += $("#ZHC_XW_BHHX_BG_01").val() + ",";
                str_data += $("#ZHC_XW_BHHX_BX_01").val() + ",";

                str_data += $("#ZHC_XW_NHH_TX_01").val() + ",";
                str_data += $("#ZHC_XW_NHH_FX_01").val() + ",";
                str_data += $("#ZHC_XW_NHH_FL_01").val() + ",";
                str_data += $("#ZHC_XW_NHH_BG_01").val() + ",";
                str_data += $("#ZHC_XW_NHH_BX_01").val() + ",";

                str_data += $("#ZHC_XW_MHH_TX_01").val() + ",";
                str_data += $("#ZHC_XW_MHH_FX_01").val() + ",";
                str_data += $("#ZHC_XW_MHH_FL_01").val() + ",";
                str_data += $("#ZHC_XW_MHH_BG_01").val() + ",";
                str_data += $("#ZHC_XW_MHH_BX_01").val() + ",";

                str_data += $("#ZHC_XW_SHH_TX_01").val() + ",";
                str_data += $("#ZHC_XW_SHH_FX_01").val() + ",";
                str_data += $("#ZHC_XW_SHH_FL_01").val() + ",";
                str_data += $("#ZHC_XW_SHH_BG_01").val() + ",";
                str_data += $("#ZHC_XW_SHH_BX_01").val() + ",";
                //=============================================
                //第一组是新添加的
                str_data += $("#ZHC_XW_QDJH_TX_02").val() + ",";
                str_data += $("#ZHC_XW_QDJH_FX_02").val() + ",";
                str_data += $("#ZHC_XW_QDJH_FL_02").val() + ",";
                str_data += $("#ZHC_XW_QDJH_BG_02").val() + ",";
                str_data += $("#ZHC_XW_QDJH_BX_02").val() + ",";

                str_data += $("#ZHC_XW_BH_TX_02").val() + ",";
                str_data += $("#ZHC_XW_BH_FX_02").val() + ",";
                str_data += $("#ZHC_XW_BH_FL_02").val() + ",";
                str_data += $("#ZHC_XW_BH_BG_02").val() + ",";
                str_data += $("#ZHC_XW_BH_BX_02").val() + ",";

                str_data += $("#ZHC_XW_NHH_TX_02").val() + ",";
                str_data += $("#ZHC_XW_NHH_FX_02").val() + ",";
                str_data += $("#ZHC_XW_NHH_FL_02").val() + ",";
                str_data += $("#ZHC_XW_NHH_BG_02").val() + ",";
                str_data += $("#ZHC_XW_NHH_BX_02").val() + ",";

                str_data += $("#ZHC_XW_MHH_TX_02").val() + ",";
                str_data += $("#ZHC_XW_MHH_FX_02").val() + ",";
                str_data += $("#ZHC_XW_MHH_FL_02").val() + ",";
                str_data += $("#ZHC_XW_MHH_BG_02").val() + ",";
                str_data += $("#ZHC_XW_MHH_BX_02").val() + ",";

                str_data += $("#ZHC_XW_SHH_TX_02").val() + ",";
                str_data += $("#ZHC_XW_SHH_FX_02").val() + ",";
                str_data += $("#ZHC_XW_SHH_FL_02").val() + ",";
                str_data += $("#ZHC_XW_SHH_BG_02").val() + ",";
                str_data += $("#ZHC_XW_SHH_BX_02").val() + ",";
                //==============================================

                //第一组是新添加的青岛近海
                str_data += $("#ZHC_XW_QDJH_TX_03").val() + ",";
                str_data += $("#ZHC_XW_QDJH_FX_03").val() + ",";
                str_data += $("#ZHC_XW_QDJH_FL_03").val() + ",";
                str_data += $("#ZHC_XW_QDJH_BG_03").val() + ",";
                str_data += $("#ZHC_XW_QDJH_BX_03").val() + ",";

                str_data += $("#ZHC_XW_BH_TX_03").val() + ",";
                str_data += $("#ZHC_XW_BH_FX_03").val() + ",";
                str_data += $("#ZHC_XW_BH_FL_03").val() + ",";
                str_data += $("#ZHC_XW_BH_BG_03").val() + ",";
                str_data += $("#ZHC_XW_BH_BX_03").val() + ",";

                str_data += $("#ZHC_XW_NHH_TX_03").val() + ",";
                str_data += $("#ZHC_XW_NHH_FX_03").val() + ",";
                str_data += $("#ZHC_XW_NHH_FL_03").val() + ",";
                str_data += $("#ZHC_XW_NHH_BG_03").val() + ",";
                str_data += $("#ZHC_XW_NHH_BX_03").val() + ",";

                str_data += $("#ZHC_XW_MHH_TX_03").val() + ",";
                str_data += $("#ZHC_XW_MHH_FX_03").val() + ",";
                str_data += $("#ZHC_XW_MHH_FL_03").val() + ",";
                str_data += $("#ZHC_XW_MHH_BG_03").val() + ",";
                str_data += $("#ZHC_XW_MHH_BX_03").val() + ",";

                str_data += $("#ZHC_XW_SHH_TX_03").val() + ",";
                str_data += $("#ZHC_XW_SHH_FX_03").val() + ",";
                str_data += $("#ZHC_XW_SHH_FL_03").val() + ",";
                str_data += $("#ZHC_XW_SHH_BG_03").val() + ",";
                str_data += $("#ZHC_XW_SHH_BX_03").val() + ",";

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(id, data);
            }
            //上午渔政局表
            function submit_45(id) {
                var str_data = "";
                str_data += "旅顺,";

                str_data += $("#LS_FX_01").val() + ",";
                str_data += $("#LS_FL_01").val() + ",";
                str_data += $("#LS_BG_01").val() + ",";
                str_data += "旅顺,";
                str_data += $("#LS_FX_02").val() + ",";
                str_data += $("#LS_FL_02").val() + ",";
                str_data += $("#LS_BG_02").val() + ",";
                str_data += "烟台,";
                str_data += $("#YT_FX_01").val() + ",";
                str_data += $("#YT_FL_01").val() + ",";
                str_data += $("#YT_BG_01").val() + ",";
                str_data += "烟台,";
                str_data += $("#YT_FX_02").val() + ",";
                str_data += $("#YT_FL_02").val() + ",";
                str_data += $("#YT_BG_02").val() + ",";
                str_data += "威海,";
                str_data += $("#WH_FX_01").val() + ",";
                str_data += $("#WH_FL_01").val() + ",";
                str_data += $("#WH_BG_01").val() + ",";
                str_data += "威海,";
                str_data += $("#WH_FX_02").val() + ",";
                str_data += $("#WH_FL_02").val() + ",";
                str_data += $("#WH_BG_02").val() + ",";
                str_data += "石岛,";
                str_data += $("#SD_FX_01").val() + ",";
                str_data += $("#SD_FL_01").val() + ",";
                str_data += $("#SD_BG_01").val() + ",";
                str_data += "石岛,";
                str_data += $("#SD_FX_02").val() + ",";
                str_data += $("#SD_FL_02").val() + ",";
                str_data += $("#SD_BG_02").val() + ",";
                str_data += "责任海区1,";
                str_data += $("#ZRHQ_FX_01").val() + ",";
                str_data += $("#ZRHQ_FL_01").val() + ",";
                str_data += $("#ZRHQ_BG_01").val() + ",";
                str_data += "责任海区1,";
                str_data += $("#ZRHQ_FX_02").val() + ",";
                str_data += $("#ZRHQ_FL_02").val() + ",";
                str_data += $("#ZRHQ_BG_02").val() + ",";
                str_data += "责任海区2,";
                str_data += $("#ZRHQ_FX_03").val() + ",";
                str_data += $("#ZRHQ_FL_03").val() + ",";
                str_data += $("#ZRHQ_BG_03").val() + ",";
                str_data += "责任海区2,";
                str_data += $("#ZRHQ_FX_04").val() + ",";
                str_data += $("#ZRHQ_FL_04").val() + ",";
                str_data += $("#ZRHQ_BG_04").val() + ",";
                str_data += "青岛,";
                str_data += $("#QD_FX_01").val() + ",";
                str_data += $("#QD_FL_01").val() + ",";
                str_data += $("#QD_BG_01").val() + ",";

                //================动态责任海区====================
                var list2 = [];
                $("tr[name='trname2']").each(function () {
                    list2.push($(this).attr("id"));
                })
                if (list2 == "") {

                }
                else {
                    // alert(list2.unique3());

                    //将相同的数据分组
                    var obj2 = {};

                    for (var i = 0; i < list2.length; i++) {

                        var item = list2[i];

                        if (!obj2[item]) {

                            obj2[item] = [item];

                        } else {

                            obj2[item].push(item);

                        }

                    }
                    for (var n in obj2) {

                        // alert(obj2[i]); 
                        // alert(list2);
                        // list2.unique3().length + 1;
                        var i = obj2[n].unique3()[0];

                        for (var j = 1; j <= obj2[n].length; j++) {
                            str_data += $("#YZJ_XW_SA0" + i).val() + ",";
                            str_data += document.getElementById("SJ_0" + i + "_" + j).innerHTML + ",";
                            str_data += $("#YZJ_XW_SA0" + i + "_FX_0" + j).val() + ",";
                            str_data += $("#YZJ_XW_SA0" + i + "_FL_0" + j).val() + ",";
                            str_data += $("#YZJ_XW_SA0" + i + "_BG_0" + j).val() + ",";
                        }
                    }
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(id, data);
            }


            //判断数据格式，进行友好提示
            function submit_ajax(types, datas1) {
                var text_color = ""; var area_color = ""; var divid = "";
                var ys_color = "";
                if (types * 1 < 10) {
                    divid = "ddlg_0" + types;
                } else {
                    divid = "ddlg_" + types;
                }
                //判断是文本框还是富文本框
                //if (types == 27 || types == 28 || types == 30) {
                //$("#" + divid + " :textarea").each(function () {
                //    area_color == "rgb(102, 102, 102)" ? "rgb(102, 102, 102)" : $(this).css("color");
                //});  
                //}
                //else {
                //    $("#" + divid + " input[type=text]").each(function () {
                //        text_color == "rgb(102, 102, 102)" ? "rgb(102, 102, 102)" : $(this).css("color");
                //    });
                //}
                var div = "#" + divid + " input[type=text]"
                $(div).each(function () {
                    //var x = $(this).css("border-color");
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
                    submit_rtn(types, datas1);
                }
            }
            //ajax公共方法(表单类型，post数组)
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
            //↑表单提交（表单编号）↑
        }
        //↑表单提交（表单编号）↑

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
                else if ($("#yby_type").val() == "海冰") {
                    all_disabled();
                    hb_isabled();//海冰可编辑
                    tb_isabled();//填报信息可编辑
                }
            }
            //填报信息可编辑
            function tb_isabled() {
                $("#dlg_23 :text").removeAttr("disabled");
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
                $("#CXQX_ZS_3DAYS").removeAttr("disabled");
                $("#CXQX_ZS_24HOURS").removeAttr("disabled");
                $("#CXQX_ZS_7DAYS").removeAttr("disabled");
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
                $("#SWQX_ZS_3DAYS").attr("disabled", "disabled");
                $("#SWQX_ZS_24HOURS").attr("disabled", "disabled");
                $("#SWQX_ZS_7DAYS").attr("disabled", "disabled");
            }
            //风浪可编辑
            function fl_isabled() {
                $("#SWQX_ZS_3DAYS").removeAttr("disabled");
                $("#SWQX_ZS_24HOURS").removeAttr("disabled");
                $("#SWQX_ZS_7DAYS").removeAttr("disabled");
            }
            //水温可编辑
            function sw_isabled() {
                $("input[id*='SW']").removeAttr("disabled");
                $("input[id*='ICE']").removeAttr("disabled");
                //$("#SWQX_ZS_3HOURS").removeAttr("disabled");
                //$("#SWQX_ZS_24HOURS").removeAttr("disabled");
                //$("#SWQX_ZS_7DAYS").removeAttr("disabled");
            }
            //水温不可编辑
            function sw_disabled() {
                $("input[id*='SW']").attr("disabled", "disabled");
                $("#SWQX_ZS_3HOURS").attr("disabled", "disabled");
                $("#SWQX_ZS_24HOURS").attr("disabled", "disabled");
                $("#SWQX_ZS_7DAYS").attr("disabled", "disabled");

                $("#CXQX_ZS_3DAYS").attr("disabled", "disabled");
                $("#CXQX_ZS_24HOURS").attr("disabled", "disabled");
                $("#CXQX_ZS_7DAYS").attr("disabled", "disabled");
            }
            //海冰可编辑
            function hb_disabled() {
                $("input[id*='ICE']").attr("disabled", "disabled");
            }
            //海冰不可编辑
            function hb_isabled() {
                $("input[id*='ICE']").removeAttr("disabled");
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
            //$("#dlg").dialog({ onClose: function () { bordercolor(1); } });//dlg关闭事件
            //$("#dlg_02").dialog({ onClose: function () { bordercolor(2); } });
            //$("#dlg_03").dialog({ onClose: function () { bordercolor(3); } });
            //$("#dlg_04").dialog({ onClose: function () { bordercolor(4); } });
            //$("#dlg_05").dialog({ onClose: function () { bordercolor(5); } });
            //$("#dlg_06").dialog({ onClose: function () { bordercolor(6); } });
            //$("#dlg_07").dialog({ onClose: function () { bordercolor(7); } });
            //$("#dlg_08").dialog({ onClose: function () { bordercolor(8); } });
            //$("#dlg_09").dialog({ onClose: function () { bordercolor(9); } });
            //$("#dlg_10").dialog({ onClose: function () { bordercolor(10); } });
            //$("#dlg_11").dialog({ onClose: function () { bordercolor(11); } });
            //$("#dlg_12").dialog({ onClose: function () { bordercolor(12); } });
            //$("#dlg_13").dialog({ onClose: function () { bordercolor(13); } });
            //$("#dlg_14").dialog({ onClose: function () { bordercolor(14); } });
            //$("#dlg_15").dialog({ onClose: function () { bordercolor(15); } });
            //$("#dlg_16").dialog({ onClose: function () { bordercolor(16); } });
            //$("#dlg_17").dialog({ onClose: function () { bordercolor(17); } });
            //$("#dlg_18").dialog({ onClose: function () { bordercolor(18); } });
            //$("#dlg_19").dialog({ onClose: function () { bordercolor(19); } });
            //$("#dlg_20").dialog({ onClose: function () { bordercolor(20); } });
            //$("#dlg_21").dialog({ onClose: function () { bordercolor(21); } });
            //$("#dlg_22").dialog({ onClose: function () { bordercolor(22); } });

            //  $('#dlg').dialog('close'); //dlg关闭
            //var str = "";
            //for (var i = 2; i <= 22; i++) {
            //    if (i <10) {
            //        str = "#dlg_0" + i;
            //    } else {
            //        str = "#dlg_" + i;
            //    }
            //    $(str).dialog("close");
            //}
            $('#dlg_xzmb').dialog('close');//新建模板关闭
            $('#dlg_czrz').dialog('close');//操作日志关闭





            //  all_disabled();
            // type_change();
            //添加字数限制↓
            $('#dlg_04 :text').attr("maxlength", "20");
            $('#dlg_05 :text').attr("maxlength", "20");
            $('#dlg_07 :text').attr("maxlength", "10");
            $('#dlg_08 :text').attr("maxlength", "10");
            $('#MRHBLG').attr("maxlength", "80");
            $('#dlg_09 :text').attr("maxlength", "10");
            $('#dlg_10 :text').attr("maxlength", "20");
            $('#dlg_11 :text').attr("maxlength", "10");
            $('#dlg_11 :text[id*="FL"]').attr("maxlength", "20");
            $('#dlg_12 :text').attr("maxlength", "10");
            $('#dlg_13 :text[id*="BG"]').attr("maxlength", "80");
            $('#dlg_13 :text[id*="BX"]').attr("maxlength", "10");
            $('#dlg_13 :text[id*="YX"]').attr("maxlength", "10");
            $('#dlg_14 :text[id*="BG"]').attr("maxlength", "80");
            $('#dlg_14 :text[id*="BX"]').attr("maxlength", "10");
            $('#dlg_14 :text[id*="YX"]').attr("maxlength", "10");
            $('#dlg_15 :text[id*="BG"]').attr("maxlength", "80");
            $('#dlg_15 :text[id*="BX"]').attr("maxlength", "10");
            $('#dlg_15 :text[id*="YX"]').attr("maxlength", "10");
            $('#HHBB14_BG').attr("maxlength", "200");
            $('#HHBB15_BG').attr("maxlength", "200");
            $('#dlg_16 :text').attr("maxlength", "10");
            $('#dlg_17 :text[id*="LG"]').attr("maxlength", "80");
            $('#dlg_17 :text[id*="SW"]').attr("maxlength", "10");
            $('#dlg_17 :text[id*="YY"]').attr("maxlength", "30");
            $('#dlg_18 :text').attr("maxlength", "10");
            $('#dlg_19 :text[id*="LG"]').attr("maxlength", "80");
            $('#dlg_19 :text[id*="SW"]').attr("maxlength", "10");
            $('#dlg_20 :text').attr("maxlength", "10");
            $('#dlg_21 :text[id*="LG"]').attr("maxlength", "80");
            $('#dlg_21 :text[id*="SW"]').attr("maxlength", "20");
            $('#dlg_22 :text').attr("maxlength", "20");
            $('#dlg_23 :text').attr("maxlength", "30");
            $('#Fabudanwei').attr("maxlength", "100");


            //添加字数限制↑


            //$("input[id*='SJ']").removeAttr("disabled");
            //$("input[id*='CW']").removeAttr("disabled");
            //$("input[id*='1D_']").removeAttr("disabled");
            //$("input[id*='1G_']").removeAttr("disabled");
            //$("input[id*='2D_']").removeAttr("disabled");
            //$("input[id*='2G_']").removeAttr("disabled");
            //$("input[id*='DC']").removeAttr("disabled");
            //$("input[id*='GC']").removeAttr("disabled");


            //$("#swbd").css("border", "5px solid #999999");
            //$("#xwbd").css("border", "5px solid #999999");
            $(".textbox").css("width", "131px");
            $("#uniform-select_hour").css("height", "21px");
            $("#tbxx .textbox").css("width", "231px").css("height", "23px").css("margin-right", "6px");

            //加载日期 ‘时’ 
            var hour_str = "";
            for (var i = 0; i < 24; i++) {
                hour_str += "<option>" + i + "</option>";
            }
            $("#select_hour").append(hour_str);
            $("#select_hour").val(7);
            var today = new Date(); // 获取今天时间
            GetDate(today); //根据当前年月日 推算未来年月日
            setWeekReportTime();
            var date = $("#tianbaoriqi").datebox("getValue");
            t = date.split('-')[0] + "年" + date.split('-')[1] + "月" + date.split('-')[2] + "日" + $('#select_hour').val() + "时";
            $("#Fabushijian").val(t);
        });//初始化加载

        //设置周报显示时间
        function setWeekReportTime() {
            var MondayOfWeek = getMonday(0);
            var Month = MondayOfWeek.getMonth() + 1;
            for (var i = 1; i < 8; i++) {
                var d = MondayOfWeek.setDate(MondayOfWeek.getDate() + 1);
                var Day = MondayOfWeek.getDate();
                Month = MondayOfWeek.getMonth() + 1;
                var CurrentDate = "" + Month + "月" + Day + "日";
                $(".week_report .SJ_0" + i).text(CurrentDate);
            }
        }

        function getMonday(offset) {
            var d = new Date();
            return new Date(d - ((d.getDay() || 7) - 1 - (offset || 0) * 7) * 864E5);
        }

        //模板选择全选、反选、取消
        $(function () {
            $("#selectAll").click(function () {//全选  
                if ($("#uniform-selectAll span").attr("class") != "checked") {
                    $("#modellist div div span").attr("class", "checked");
                    $("#uniform-unselect span").removeClass("checked");
                    $("#uniform-reverse span").removeClass("checked");
                }
            });

            $("#unselect").click(function () {//全不选  
                if ($("#uniform-unselect span").attr("class") != "checked") {
                    $("#modellist div div span").removeClass("checked");
                    $("#uniform-reverse span").removeClass("checked");
                    $("#uniform-selectAll span").removeClass("checked");
                }
            });

            $("#reverse").click(function () {//反选  
                if ($("#uniform-reverse span").attr("class") != "checked") {
                    $("#modellist div div span").each(function () {
                        if ($(this).attr("class") == "checked") {
                            $(this).removeClass("checked");
                        } else {
                            $(this).attr("class", "checked");
                        }
                        $("#uniform-unselect span").removeClass("checked");
                        $("#uniform-selectAll span").removeClass("checked");
                    });
                }
            });
        });//模板选择全选、反选、取消

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
                        //  $(this).css("border", "5px solid #FB9337");



                    }
                    else {
                        $(this).css("border", "5px solid #ccc7c7");
                    }
                });
            }

            //$("#swbd").css("border", "5px solid #999999");
            //$("#xwbd").css("border", "5px solid #999999");
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
            // $(str).dialog("close");
            //if($(".input[type=text]").val().length==0){
            // $("#lx_" + id).css("border", "5px solid #FB9338");
            //}


            $(str + " input[type=text]").each(function () {
                if ($(this).val() != "") {
                    // alert($(this).val());
                    str1 = true;
                }

                //var value = $(this).val();//获取到了datatype值
            });
            return str1;



        }

        //ajax 加载各表数据
        function gettabledata(date1, searchType) { //searchType 按填报日期还是预报日期查询 p:填报日期 f:预报日期

            var dates = myformatter(date1);
            $.ajax({//综合
                type: "POST",
                url: "/Ajax/gettablelist.ashx?method=getAMdataNew&date=" + dates + "&searchtype=" + searchType + "&t=" + Math.random(),
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
                            case "t23": gettable23(resjson[j].children, date1); break;
                            case "t44": gettable44(resjson[j], date1); dlgclose("44"); break;
                                //case "t45": gettable45(resjson[j], date1); dlgclose("45"); break;
                            default:
                        }
                    }
                },
                complete: function () {
                    $('#w').window('close');
                    $("#btn_select").removeAttr("disabled");
                }
            });
        }//ajax 加载各表数据

        {

            function getaddtr(bgdhqlist) {
                var list = bgdhqlist;

                if (list == "") {

                }
                else {

                    //将相同的数据分组
                    var obj = {};

                    for (var n = 0; n < list.length; n++) {

                        var item = list[n];

                        if (!obj[item]) {

                            obj[item] = [item];

                        } else {

                            obj[item].push(item);

                        }

                    }
                    var tr = "";
                    var m = "";
                    var vlist = [];
                    for (var n in obj) {
                        // m = obj[n].length;//预报海区个数
                        vlist.push(n);
                    }





                    if (vlist != "") {

                        for (i = 1; i <= vlist.length ; i++) {
                            m = obj[vlist[i - 1]].length;//预报海区个数
                            for (j = 1; j <= m; j++) {
                                if (j == 1) {
                                    tr += "<tr  id =" + i + " name='trname'><td rowspan='" + m + "'><input id='ZHC_XW_SA0" + i + "' type='text' maxlength='15' /></td>" +
                                    "<td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td rowspan='" + m + "'><input class='button' id =" + i + " type='button' value='删除' onclick='deletr(this)'></td></tr>";

                                }
                                else {
                                    tr += "<tr id=" + i + " name='trname'><td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>  </tr>";
                                }




                            }



                        }
                        $("#addtable").append(tr);
                        var today = new Date(); // 获取今天时间
                        GetDate(today); //根据当前年月日 推算未来年月日


                    }


                }
            }

            function getaddtr2(bgdhqlist) {
                var list3 = bgdhqlist;

                if (list3 == "") {

                }
                else {

                    //将相同的数据分组
                    var obj3 = {};

                    for (var n = 0; n < list3.length; n++) {

                        var item = list3[n];

                        if (!obj3[item]) {

                            obj3[item] = [item];

                        } else {

                            obj3[item].push(item);

                        }

                    }
                    var tr2 = "";
                    var m = "";
                    var vlist3 = [];
                    for (var n in obj3) {
                        // m = obj3[n].length;//预报海区个数
                        vlist3.push(n);
                    }





                    if (vlist3 != "") {

                        for (i = 1; i <= vlist3.length ; i++) {
                            m = obj3[vlist3[i - 1]].length;//预报海区个数
                            for (j = 1; j <= m; j++) {
                                if (j == 1) {
                                    tr2 += "<tr  id =" + i + " name='trname2'><td rowspan='" + m + "'><input id='YZJ_XW_SA0" + i + "' type='text' maxlength='15' /></td>" +
                                    "<td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td rowspan='" + m + "'><input class='button' id =" + i + " type='button' value='删除' onclick='deletr(this)'></td></tr>";

                                }
                                else {
                                    tr2 += "<tr id=" + i + " name='trname2'><td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>  </tr>";
                                }




                            }



                        }
                        $("#addtable2").append(tr2);
                        var today = new Date(); // 获取今天时间
                        GetDate(today); //根据当前年月日 推算未来年月日


                    }


                }
            }
            //上午指挥处
            function gettable44(result, date1) {
                var effect = result.pbtime;
                var resjson = result.children;
                for (var i = 0; i < resjson.length; i++) {
                    var seaArea = resjson[i].SEAAREA;
                    var publishDate = resjson[i].PUBLISHTIME;
                    var forecastDate = resjson[i].FORECASTTIME;

                    var fdate = new Date(Date.parse(forecastDate.replace(/-/g, "/"))).getTime();
                    var pdate = new Date(Date.parse(publishDate.replace(/-/g, "/"))).getTime();
                    var num = 1;

                    var areaCode = "";
                    switch (seaArea) {
                        case "青岛市":
                            areaCode = "QD";
                            break;
                        case "青岛近海":
                            areaCode = "QDJH";
                            num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                            break;
                        case "渤海海峡":
                            areaCode = "BHHX";
                            break;
                        case "渤海":
                            areaCode = "BH";
                            num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                            break;
                        case "黄海北部":
                            areaCode = "NHH";

                            num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                            break;
                        case "黄海中部":
                            areaCode = "MHH";
                            num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                            break;
                        case "黄海南部":
                            areaCode = "SHH";
                            num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                            break;
                        default:
                            break;
                    }
                    var TXInputId = "#ZHC_XW_" + areaCode + "_TX_0" + num;
                    var FXInputId = "#ZHC_XW_" + areaCode + "_FX_0" + num;
                    var FLInputId = "#ZHC_XW_" + areaCode + "_FL_0" + num;
                    if (areaCode == "QD") {
                        var SWInputId = "#ZHC_XW_QD_BX_01";
                        $(SWInputId).val(resjson[i].WAVEDIRECTION);
                    }
                    else {
                        var BGInputId = "#ZHC_XW_" + areaCode + "_BG_0" + num;
                        var BXInputId = "#ZHC_XW_" + areaCode + "_BX_0" + num;
                        $(BGInputId).val(resjson[i].WAVEHEIGHT);
                        $(BXInputId).val(resjson[i].WAVEDIRECTION);
                    }
                    $(TXInputId).val(resjson[i].WEATHERAPPEARANCE);
                    $(FXInputId).val(resjson[i].WINDDIRECTION);
                    $(FLInputId).val(resjson[i].WINDFORCE);
                }
            }

            //上午渔政局
            function gettable45(result, date1) {
                var effect = result.pbtime;
                var resjson = result.children;
                $("#addtable2 tbody").empty();
                var bgdhqlist2 = [];
                if (resjson.length > 1) {
                    //var addedSeaAreaCount = (resjson.length -15)/2;

                    for (var k = 0; k < resjson.length; k++) {
                        if (resjson[k].SEAAREA != "旅顺" && resjson[k].SEAAREA != "青岛" && resjson[k].SEAAREA != "石岛" && resjson[k].SEAAREA != "威海" && resjson[k].SEAAREA != "烟台" && resjson[k].SEAAREA != "责任海区1" && resjson[k].SEAAREA != "责任海区2")
                            // bgdhqlist += resjson[k].SEAAREA + ",";
                            bgdhqlist2.push(resjson[k].SEAAREA);
                    }
                    getaddtr2(bgdhqlist2);
                    for (var i = 0; i < resjson.length; i++) {
                        var seaArea = resjson[i].SEAAREA;
                        // var seaArea = resjson[i].SEAAREA;
                        var publishDate = resjson[i].PUBLISHDATE;
                        var forecastDate = resjson[i].FORECASTDATE;
                        var pdate = new Date();
                        var fdate = new Date(Date.parse(forecastDate.replace(/-/g, "/"))).getTime();
                        //var pdate = new Date(Date.parse(publishDate.replace(/-/g, "/"))).getTime();
                        var num = 1;
                        switch (seaArea) {
                            case "旅顺":
                                num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24) + 1;
                                $("#LS_FX_0" + num).val(resjson[i].WINDDIRECTION);
                                $("#LS_FL_0" + num).val(resjson[i].WINDFORCE);
                                $("#LS_BG_0" + num).val(resjson[i].WAVEHEIGHT);
                                break;
                            case "青岛":
                                num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                                $("#QD_FX_0" + num).val(resjson[i].WINDDIRECTION);
                                $("#QD_FL_0" + num).val(resjson[i].WINDFORCE);
                                $("#QD_BG_0" + num).val(resjson[i].WAVEHEIGHT);
                                break;
                            case "石岛":
                                num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24) + 1;
                                $("#SD_FX_0" + num).val(resjson[i].WINDDIRECTION);
                                $("#SD_FL_0" + num).val(resjson[i].WINDFORCE);
                                $("#SD_BG_0" + num).val(resjson[i].WAVEHEIGHT);
                                break;
                            case "威海":
                                num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24) + 1;
                                $("#WH_FX_0" + num).val(resjson[i].WINDDIRECTION);
                                $("#WH_FL_0" + num).val(resjson[i].WINDFORCE);
                                $("#WH_BG_0" + num).val(resjson[i].WAVEHEIGHT);
                                break;
                            case "烟台":
                                num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24) + 1;
                                $("#YT_FX_0" + num).val(resjson[i].WINDDIRECTION);
                                $("#YT_FL_0" + num).val(resjson[i].WINDFORCE);
                                $("#YT_BG_0" + num).val(resjson[i].WAVEHEIGHT);
                                break;
                            case "责任海区1":
                                num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24) + 1;
                                $("#ZRHQ_FX_0" + num).val(resjson[i].WINDDIRECTION);
                                $("#ZRHQ_FL_0" + num).val(resjson[i].WINDFORCE);
                                $("#ZRHQ_BG_0" + num).val(resjson[i].WAVEHEIGHT);
                                break;
                            case "责任海区2":
                                num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24) + 3;
                                $("#ZRHQ_FX_0" + num).val(resjson[i].WINDDIRECTION);
                                $("#ZRHQ_FL_0" + num).val(resjson[i].WINDFORCE);
                                $("#ZRHQ_BG_0" + num).val(resjson[i].WAVEHEIGHT);
                                break;
                            default:
                                if (bgdhqlist2 == "") {
                                }
                                else {
                                    //将相同的数据分组
                                    var obj = {};
                                    for (var n = 0; n < bgdhqlist2.length; n++) {
                                        var item = bgdhqlist2[n];
                                        if (!obj[item]) {
                                            obj[item] = [item];
                                        } else {
                                            obj[item].push(item);
                                        }
                                    }
                                    var tr = "";

                                    var vlist3 = [];
                                    for (var n in obj) {
                                        vlist3.push(n);



                                    }
                                    var addedSeaAreaCount = vlist3.length;
                                    num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                                    if (effect == "today") {

                                        num = num + 1;
                                    }
                                    for (var m = 1; m <= addedSeaAreaCount; m++) {
                                        if ($("#YZJ_XW_SA0" + m).val() == "" || $("#YZJ_XW_SA0" + m).val() == seaArea) {
                                            $("#YZJ_XW_SA0" + m).val(seaArea);
                                            $("#YZJ_XW_SA0" + m + "_FX_0" + num).val(resjson[i].WINDDIRECTION);
                                            $("#YZJ_XW_SA0" + m + "_FL_0" + num).val(resjson[i].WINDFORCE);
                                            $("#YZJ_XW_SA0" + m + "_BG_0" + num).val(resjson[i].WAVEHEIGHT);
                                            break;
                                        }
                                    }
                                    break;
                                }
                                break;
                        }
                        //var TXInputId = "#ZHC_XW_" + areaCode + "_TX_0" + num;
                        //var FXInputId = "#ZHC_XW_" + areaCode + "_FX_0" + num;
                        //var FLInputId = "#ZHC_XW_" + areaCode + "_FL_0" + num;
                    }
                    //if (resjson.length > 1) {
                    //    var ta= resjson.split(",");
                    //    //旅顺
                    //    $("#LS_FX_01").val(ta[0]);
                    //    $("#LS_FL_01").val(ta[1]);
                    //    $("#LS_BG_01").val(ta[2]);
                    //    $("#LS_FX_02").val(ta[3]);
                    //    $("#LS_FL_02").val(ta[4]);
                    //    $("#LS_BG_02").val(ta[5]);
                    //    //青岛
                    //    $("#QD_FX_01").val(ta[6]);
                    //    $("#QD_FL_01").val(ta[7]);
                    //    $("#QD_BG_01").val(ta[8]);
                    //    //石岛
                    //    $("#SD_FX_01").val(ta[9]);
                    //    $("#SD_FL_01").val(ta[10]);
                    //    $("#SD_BG_01").val(ta[11]);
                    //    $("#SD_FX_02").val(ta[12]);
                    //    $("#SD_FL_02").val(ta[13]);
                    //    $("#SD_BG_02").val(ta[14]);
                    //    //威海
                    //    $("#WH_FX_01").val(ta[15]);
                    //    $("#WH_FL_01").val(ta[16]);
                    //    $("#WH_BG_01").val(ta[17]);
                    //    $("#WH_FX_02").val(ta[18]);
                    //    $("#WH_FL_02").val(ta[19]);
                    //    $("#WH_BG_02").val(ta[20]);
                    //    //烟台
                    //    $("#YT_FX_01").val(ta[21]);
                    //    $("#YT_FL_01").val(ta[22]);
                    //    $("#YT_BG_01").val(ta[23]);
                    //    $("#YT_FX_02").val(ta[24]);
                    //    $("#YT_FL_02").val(ta[25]);
                    //    $("#YT_BG_02").val(ta[26]);
                    //    //责任海区1
                    //    $("#ZRHQ_FX_01").val(ta[27]);
                    //    $("#ZRHQ_FL_01").val(ta[28]);
                    //    $("#ZRHQ_BG_01").val(ta[29]);
                    //    $("#ZRHQ_FX_02").val(ta[30]);
                    //    $("#ZRHQ_FL_02").val(ta[31]);
                    //    $("#ZRHQ_BG_02").val(ta[32]);
                    //     //责任海区2
                    //    $("#ZRHQ_FX_03").val(ta[27]);
                    //    $("#ZRHQ_FL_03").val(ta[28]);
                    //    $("#ZRHQ_BG_03").val(ta[29]);
                    //    $("#ZRHQ_FX_04").val(ta[30]);
                    //    $("#ZRHQ_FL_04").val(ta[31]);
                    //    $("#ZRHQ_BG_04").val(ta[32]);
                    //}
                    //else {

                    //}
                }
            }

            //填报信息数据
            function gettable23(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#Fabudanwei").val(resjson[i].fb);
                    $("#Tel").val(resjson[i].dh);
                    //var ptime = myformatter1(new Date(resjson[i].rq)) + resjson[i].xs + "时";
                    //var ptime = myformatter1(new Date(resjson[i].rq)) + "10时";
                    // $("#Fabushijian").val(ptime);
                    $("#Chuanzhen").val(resjson[i].cz);
                    //$("#Hailang").val(resjson[i].hl);
                    //$("#Chaoxi").val(resjson[i].cx);
                    //$("#Shuiwen").val(resjson[i].sw);
                    $("#ZhibanTel").val(resjson[i].zhibantel);
                    $("#SendTel").val(resjson[i].sendtel);
                    if (resjson[i].cx != "" && resjson[i].cx != null) {
                        $("#uniform-Chaoxi span").text(resjson[i].cx);
                        $("#uniform-Chaoxi span").attr("code", resjson[i].cx);
                        $("#iChaoxi").val(resjson[i].cx);
                        $("#ChaoxiTel").val(resjson[i].cxtel);
                    }
                    if (resjson[i].sw != "" && resjson[i].sw != null) {
                        $("#uniform-Shuiwen span").text(resjson[i].sw);
                        $("#uniform-Shuiwen span").attr("code", resjson[i].sw);
                        $("#iShuiwen").val(resjson[i].sw);
                        $("#ShuiwenTel").val(resjson[i].swtel);
                    }
                    if (resjson[i].hl != "" && resjson[i].hl != null) {
                        $("#uniform-Hailang span").text(resjson[i].hl);
                        $("#uniform-Hailang span").attr("code", resjson[i].hl);
                        $("#iHailang").val(resjson[i].hl);
                        $("#HailangTel").val(resjson[i].hltel);
                    }
                }
            }
        }



        //查询按钮点击事件
        $("#btn_select").click(function () {

            // $(".panel window :text").val("");ddlg_01
            $('#dlg :text').val("");
            $("#SWQX_ZS_24HOURS").val("");
            $("#CXQX_ZS_24HOURS").val("");
            $("#SWQX_ZS_7DAYS").val("");
            $("#CXQX_ZS_7DAYS").val("");
            $("#SWQX_ZS_3DAYS").val("");
            $("#CXQX_ZS_3DAYS").val("");

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
            // getPredicTideData(date1);
            var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
            var d = new Date(date1);
            var date = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();

            gettabledata(date1, "p");
            //只能修改当前天的数据
            quanxian(type, date);
            //  $('<script src="js/PreTideData.min.js" type="text/javascript"><"/script>').appendTo($("body"));
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
                // for (var i in hb_arry) {
                //    if (id.toString() == hb_arry[i].toString()) {
                //        $("#lx_" + id).css("border", "5px solid #CCFF33");
                //    }
                //}
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
            $('.SJ_0001XW').text(ds0 + "08时至" + ds1 + "08时");

            //*月*号08时至*月*号08时
            today.setDate(today.getDate() + 1);
            $('.SJ_02').text("" + (today.getMonth() + 1) + "月" + today.getDate() + "日");
            var ds2 = (today.getMonth() + 1) + "月" + today.getDate() + "日";
            $('.SJ_0102MO').text(ds1 + "08时至" + ds2 + "08时");
            $('.SJ_0102XW').text(ds1 + "08时至" + ds2 + "08时");

            today.setDate(today.getDate() + 1);
            $('.SJ_03').text("" + (today.getMonth() + 1) + "月" + today.getDate() + "日");
            var ds3 = (today.getMonth() + 1) + "月" + today.getDate() + "日";
            $('.SJ_0203MO').text(ds2 + "08时至" + ds3 + "08时");
            $('.SJ_0203XW').text(ds2 + "08时至" + ds3 + "08时");

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
                setWeekReportTime();
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
    <%--<script type="text/javascript">
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
                    getValue();
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

        ////由于加载顺序，此处为覆盖预报员信息
        function getValue() {
            var chaoxi = $("#iChaoxi").val();
            var hailang = $("#iHailang").val();
            var shuiwen = $("#iShuiwen").val();
            if (chaoxi != "" && chaoxi != null) {
                $("#uniform-Chaoxi span").text(chaoxi);
                $("#uniform-Chaoxi span").attr("code", chaoxi);
                //$("#ChaoxiTel").val(tel);
            }
            if (shuiwen != "" && shuiwen != null) {
                $("#uniform-Shuiwen span").text(shuiwen);
                $("#uniform-Shuiwen span").attr("code", shuiwen);
                //$("#ShuiwenTel").val(tel);
            }
            if (hailang != "" && hailang != null) {
                $("#uniform-Hailang span").text(hailang);
                $("#uniform-Hailang span").attr("code", hailang);
                //$("#HailangTel").val(tel);
            }
        }
    </script>--%>
    <%-----------------  获取潮汐数据------------------%>
    <script type="text/javascript" src="js/PreTideData.min.js"></script>
    <%--//====================================================================--%>

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

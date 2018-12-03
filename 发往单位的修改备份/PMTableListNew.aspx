<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PMTableListNew.aspx.cs" Inherits="PredicTable.PMTableListNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>下午指挥处、渔政局预报列表</title>
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
         font-size:18px;
        }
        #uniform-Chaoxi {
        font-size:18px;
        }
        #uniform-Shuiwen {
        font-size:18px;
        }
    </style>
</head>
<body>
    <iframe width="0" height="0" src="SessionKeeper.asp"></iframe>

    <script>
        var cx_arry = new Array(2, 4, 7, 8,10, 12, 16, 18, 20, 22,42,43);
        var sw_arry = new Array(1, 3, 6, 11, 17, 19, 21,24);
        var fl_arry = new Array(1,3, 5, 6, 8, 9, 11, 13, 14,15,17,19,21,26,35,41,43);
         var type = "<%=Session["type"]%>";
       // var type = "<%=type%>";
           var makewordtime = "pm";
        //var type = "cx";
        function quanxian(type, date) {
            if (getdatenow() <= date) {
                  switch (type) { //all_hide(); show_bytype(cx_arry);
                      case "cx": all_disabled(); cx_isabled(); tb_isabled(); $("#yby_type").val("潮汐");  break;//潮汐能填写
                      case "fl": all_isabled(); cx_disabled(); sw_disabled(); tb_isabled(); $("#yby_type").val("风、海浪");  break;//风浪能填写
                      case "sw": all_disabled(); sw_isabled(); tb_isabled(); $("#yby_type").val("水温"); break;//水温能填写
                      
                      default: all_disabled(); $("#yby_type").val("无"); break;// 都不能填写
                  }
              } else {//不是当天不能编辑
                  switch (type) {
                      case "cx": all_disabled();  $("#yby_type").val("潮汐");  break;//都不能填写
                      case "fl": all_disabled();   tb_isabled(); $("#yby_type").val("风、海浪");  break;//风浪能填写
                      case "sw": all_disabled();  $("#yby_type").val("水温");  break;//都不能填写
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
                  quanxian(type,getdatenow());
                  setVisiable();
              $("#btn_show").click( function (){

                  if( $("#btn_show").val()=="显示所有"){
                      all_show();
                      $("#btn_show").val("显示可编辑");
                  }else if($("#btn_show").val()=="显示可编辑"){
                        switch (type) {
                  case "cx":  all_hide(); show_bytype(cx_arry); break;//潮汐能填写
                  case "fl":  all_hide();show_bytype(fl_arry);  break;//风浪能填写
                  case "sw":  all_hide();show_bytype(sw_arry);    break;//水温能填写
                  default:   break;// 都不能填写
              }
                       $("#btn_show").val("显示所有");
                  }
              });

              function setVisiable() {
                 switch (type) {
                  case "cx":  all_hide(); show_bytype(cx_arry); break;//潮汐能填写
                  case "fl":  all_hide();show_bytype(fl_arry);  break;//风浪能填写
                  case "sw":  all_hide();show_bytype(sw_arry);    break;//水温能填写
                  default: break;// 都不能填写
                  }
               }
            ////  var topWin = window.top.document.getElementById("username").contentWindow;
          }); 

      
           //显示所有
          function all_show() {
              var str = "";
              for (var i = 1; i <= 50; i++) {
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
                for (var i = 1; i <= 50; i++) {
                    if (i <10) {
                        str = "#ddlg_0" + i;
                    } else {
                        str = "#ddlg_" + i;
                    }
                    $(str).css("display","none");
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
              else{
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
                            
         //根据浪高判断海浪级别
         function setlevel(num) {
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
  var scroll_offset = $("#"+id).offset();  //得到pos这个div层的offset，包含两个值，top和left
  $("body,html").animate({
   scrollTop:(scroll_offset.top-200)  //让body的scrollTop等于pos的top，就实现了滚动
   },1000);
 }

    </script>
    <form id="form2" runat="server">

        <%--<div class="bodywrapper">--%>
        <div <%--class="centercontent"--%>>
            <div id="contentwrapper" class="contentwrapper">
                <div>
                    <%--  <div class="contenttitle2">
                            <h3 id="tx">表单类型</h3>
                        </div>--%>
                    <div style="position: fixed; top: 0px; left: 20px; z-index: 2; display: none">
                        <ul id="leixing1">
                            <li id="xwbd" style="clear: both; border-color: #fb9337; background-color: #fb9337; color: white; font-weight: bold">下午预报表单</li>
                            <li id="lx_5" onclick="click_scroll('ddlg_05')">一、各海区24小时海浪预报</li>
                            <li id="lx_6" onclick="click_scroll('ddlg_06')">二、山东近海七市24小时海浪、水温预报</li>
                            <li id="lx_7" onclick="click_scroll('ddlg_07')">三、山东近海七市24小时潮汐预报</li>
                            <li id="lx_8" onclick="click_scroll('ddlg_08')">四、青岛24小时潮位预报</li>
                            <li id="lx_9" onclick="click_scroll('ddlg_09')">五、黄河南海堤附近海域72小时风、浪预报</li>
                            <li id="lx_10" onclick="click_scroll('ddlg_10')">六、明泽闸潮位预报</li>
                            <li id="lx_11" onclick="click_scroll('ddlg_11')">七、南堡油田海域波浪、风、水温预报</li>
                            <li id="lx_12" onclick="click_scroll('ddlg_12')">八、南堡油田海域潮汐预报</li>
                            <li id="lx_13" onclick="click_scroll('ddlg_13')">九、海区24小时海浪、水温预报</li>
                            <li id="lx_14" onclick="click_scroll('ddlg_14')">十、海区48小时海浪预报</li>
                            <li id="lx_15" onclick="click_scroll('ddlg_15')">十一、海区72小时海浪预报</li>
                            <li id="lx_16" onclick="click_scroll('ddlg_16')">十二、潍坊港24小时潮汐预报</li>
                            <li id="lx_17" onclick="click_scroll('ddlg_17')">十三、青岛市各海水浴场海浪、水温预报</li>
                            <li id="lx_18" onclick="click_scroll('ddlg_18')">十四、小麦岛24小时潮汐预报</li>
                            <li id="lx_19" onclick="click_scroll('ddlg_19')">十五、青岛周边海域24小时海浪、水温预报</li>
                            <li id="lx_20" onclick="click_scroll('ddlg_20')">十六、青岛沿岸48小时潮汐预报</li>
                            <li id="lx_21" onclick="click_scroll('ddlg_21')">十七、威海电视台未来24小时预报</li>
                            <li id="lx_22" onclick="click_scroll('ddlg_22')">十八、威海24小时潮汐预报</li>
                            <li id="lx_35" onclick="click_scroll('ddlg_35')">十九、渔政局风向风力浪高预报</li>
                            <li id="lx_26" onclick="click_scroll('ddlg_26')">二十、指挥处下午预报</li>
                            <li id="lx_41" onclick="click_scroll('ddlg_41')">二十、东营埕岛油田-未来三天的海面风及海浪有效波高预报（20时起报）</li>
                            <li id="lx_42" onclick="click_scroll('ddlg_42')">二十、东营埕岛油田-未来三天高/低潮预报</li>
                            <li id="lx_43" onclick="click_scroll('ddlg_43')">二十、东营埕岛油田-未来一周的海冰厚度预报</li>
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
                        <input type="button" id="btn_show" class="stdbtn" value="显示所有" />
                        <input type="button" id="checkmodel" onclick="$('#dlg_xzmb').dialog('open'); click_scroll('dlg_xzmb') " class="stdbtn" value="选择模版并发布" />
                        <input type="button" id="setall" onclick="alldlg_Submit()" class="stdbtn" value="保存所有" />
                        <input type="button" id="btnrole" class="stdbtn" value="验证所有" />
                        <input type="button" id="ReleasetableAll" onclick="All_Releasetable()" class="stdbtn" value="发布表单" />
                        <br />
                    </div>
                </div>
                <!--表单信息-->

                <%--<a href="javascript:void(0)" class="easyui-linkbutton" onclick="$('#dlg').dialog('open')">Open</a>
		<a href="javascript:void(0)" class="easyui-linkbutton" onclick="$('#dlg').dialog('close')">Close</a>--%>

                <div class="dlgs" id="ddlg_26" style="width: auto; height: 950px; padding: 10px;">
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
                                <th class="head0" colspan="7">下午一、指挥处下午预报</th>
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
                                <td rowspan="7" class="SJ_0001XW" style="border-bottom: 2px #d8d8d8 solid;">*月*号20时至*月*号20时</td>
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
                                <td rowspan="5" class="SJ_0102XW" style="border-bottom: 2px #d8d8d8 solid;">*月*号20时至*月*号20时</td>
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
                                <%--<td rowspan="4" class="SJ_0102XW">*月*号20时至*月*号20时</td>--%>
                                <td>渤海</td>
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
                                <td rowspan="5" class="SJ_0203XW">*月*号20时至*月*号20时</td>
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
                                <%--<td rowspan="4" class="SJ_0203XW">*月*号20时至*月*号20时</td>--%>
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


                    <table id="addtable" cellpadding="0" cellspacing="0" border="0" class="stdtable" style="margin-top: 20px">
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
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(26)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(26)" value="提交" />
                </div>
                <!--表单26.下午二十、指挥处下午预报-->
                <div class="dlgs" id="ddlg_35" style="height: 390px; padding: 10px;">

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
                                <th id="ddlg_01title" class="head0" colspan="7">下午二、渔政局风向风力浪高预报</th>
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
                             <%--<tr>
                                 <td colspan="5">
                                     <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="addtr2()" value="增加海区" />
                                 </td>
                            </tr>
                            <tr id="">

                            </tr>--%>
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
                                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="addtr2()" value="增加责任海区" />
                                   <%-- 预报天数：
                                    <input id="day2" type="text" style="width: 30px" maxlength="2" onkeyup="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" onafterpaste="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" />--%>

                                </th>
                            </tr>
                        </thead>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(35)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(35)" value="提交" />

                </div>
                <!-- 表单33. 渔政局表二-->

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
                        &nbsp;&nbsp;
                        <%--传真：--%><input id="Chuanzhen" type="hidden"  style="width: 200px" />
                        预报发送：<input id="SendTel" type="text" style="width: 200px" value="0532-58750626"/>
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
                                         $("#uniform-Chaoxi span").attr("code",reportercode);
                                          $("#uniform-Shuiwen span").attr("code",reportercode);
                                     }else  if (yubaoid == "HLYB") {
                                         strs2 += "<option value='" + reportercode + "' bs='" + reportertel + "'>" + reportercode + "</option>";
                                         $("#HailangTel").val(reportertel);
                                         $("#Hailang").html(strs2);
                                         $("#uniform-Hailang span").text(reportercode);
                                          $("#uniform-Hailang span").attr("code",reportercode);
                                     }

                                }
                            }
                        });
                    });
                    $("#Hailang").change(function () {
                      
                        var code = $("#Hailang option:selected").val();
                        var tel = $("#Hailang option:selected").attr("bs");
                        $("#HailangTel").val(tel);
                        $("#uniform-Hailang span").attr("code",code);
                    });
                    $("#Chaoxi").change(function () {
                        var code = $("#Chaoxi option:selected").val();
                        var tel = $("#Chaoxi option:selected").attr("bs");
                        $("#ChaoxiTel").val(tel);
                        $("#uniform-Chaoxi span").attr("code",code);
                    });
                    $("#Shuiwen").change(function () {
                        var code = $("#Shuiwen option:selected").val();
                        var tel = $("#Shuiwen option:selected").attr("bs");
                        $("#ShuiwenTel").val(tel);
                        $("#uniform-Shuiwen span").attr("code",code);
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
                <!--填报信息-->

                <div id="dlg_czrz" class="easyui-dialog" title="操作日志" data-options="iconCls:'icon-save'" style="width: 800px; height: 530px; padding: 10px;">
                    <iframe width="100%" id="win" height="435" name="czrz" frameborder="0" src="Logbyuser.aspx"></iframe>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_czrz').dialog('close'); " value="取消" />
                </div>
                <!--操作日志-->
                <!--<div id="basicform" style="clear: both" class="subcontent">-->
                <div id="basicform" style="position: fixed; bottom: 0px; left: 20px; z-index: 2">
                </div>
                <div id="dlg_xzmb" class="easyui-dialog" title="选择模版" data-options="iconCls:'icon-save'" style="width: 320px; height: 500px; padding: 10px;">
                    <div>
                        <asp:CheckBox ID="selectAll" runat="server" Text="全选" />
                        <asp:CheckBox ID="reverse" runat="server" Text="反选" />
                        <asp:CheckBox ID="unselect" runat="server" Text="取消" />
                    </div>
                    <hr />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_xzmb').dialog('close'); $('#modellist div div span').removeClass('checked');" value="取消" />
                    <input id="Releasetable1" style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" value="发布表单" />
                    <br />
                    <br />
                    <div id="modellist">
                        <div style="margin-top:10px;    ">
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="下午的指挥处预报" name="下午的指挥处预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox28" runat="server" Text="农业部黄渤海区渔政局专项预报" name="农业部黄渤海区渔政局专项预报" />
                        </div>
                    </div>

                   <%-- <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_xzmb').dialog('close'); $('#modellist div div span').removeClass('checked');" value="取消" />
                    <input id="Releasetable" style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" value="发布表单" />--%>
                </div>
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
                            i=list.unique3().length+1;
                   
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
                                    "<td id = 'SJ_0" +i + "_"+ j +"' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td rowspan='" + num + "'><input class='button' id =" + i + " type='button' value='删除' onclick='deletr(this)'></td></tr>";

                                }
                                else {
                                    tr += "<tr id=" + i + " name='trname'><td id = 'SJ_0" + i + "_"+ j +"' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='ZHC_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>  </tr>";
                                }

                            }
                            $("#addtable").append(tr);
                            var today = new Date($("#tianbaoriqi").datebox("getValue"));//控件时间 格式：2015-04-28
                            var today2 = new Date(); // 获取今天时间
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
                                if (list[i]==(obj.id)) {
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
                            i=list.unique3().length+1;
                   
                        }
                        var tr = "";
                        var num = 2;//$("#day2").val();
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
                                    tr += "<tr  id =yzj" + i + " name='trname2'><td rowspan='" + num + "'> <input id='YZJ_XW_SA0" + i + "' type='text' maxlength='15' /></td>" +
                                    "<td id = 'SJ_0" +i + "_"+ j +"' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td rowspan='" + num + "'><input class='button' id =yzj" + i + " type='button' value='删除' onclick='deletr2(this)'></td></tr>";

                                }
                                else {
                                    tr += "<tr id=yzj" + i + " name='trname2'><td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>  </tr>";
                                }

                            }
                            $("#addtable2").append(tr);
                            var today = new Date($("#tianbaoriqi").datebox("getValue"));//控件时间 格式：2015-04-28
                            //var today = new Date(); // 获取今天时间
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
                                if (list[i]==(obj.id)) {
                                    num++;
                                }
                            }
                            for (j = num; j > 0; j--) {
                                $("#" + obj.id).remove();
                            }
                        }
                    }
  var check = document.getElementsByName("check"); 

        for(var i=1;i<=check.length;i++){ 

            if(check[i].checked){ 

                 document.getElementById('table1').deleteRow(i); 

            } 

        }
                </script>
                <script>
                    function onchlist() {
                        alert($("#selectlist1").text());
                    }
                    //$(function () {
                    //    $.ajax({
                    //        type: "Post",
                    //        url: 'ajax/gettablelist.ashx',
                    //        data: {},
                    //        success: function (data) {
                    //         // var tablelist = JSON.parse("[" + data + "]");
                    //        //  alert(tablelist[1].id);
                    //            var resjson = JSON.parse(data);
                    //            var list="";
                    //            //  alert(resjson[0].id);
                    //            for (var i = 0; i < resjson.length; i++) {
                    //                $("#selectlist1").append('<option value="' + resjson[i].id + '">' + resjson[i].text + '</option>')
                    //                 $(".chzn-results").append('<li id="selectlist_chzn_o_' + i + '" class="active-result" style="">' + resjson[i].text + '</li>');
                    //              list+='<option value="' + resjson[i].id + '">' + resjson[i].text + '</option>';
                    //            }
                    //          alert(list);
                    //          //  chzn - results
                    //           // <li id="selectlist_chzn_o_2" class="active-result" style="">United Kingdom</li>
                    //        }
                    //    });
                    //})
                    //$(function () { 
                    //    $("#selectlist1").change(function () {
                    //        alert("gg");
                    //    });


                    //});
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



                //for (var i = 0; i < resjson.length; i++) {
                //    //潮汐
                //    if (resjson[i].tb_cw) {tb_cw=resjson[i].tb_cw; }
                //    else if (resjson[i].tb_cw2) { tb_cw2=resjson[i].tb_cw2; }
                //        //海浪
                //    else if (resjson[i].tb_bg) { tb_bg=resjson[i].tb_bg; }
                //    else if (resjson[i].tb_bx) { tb_bx=resjson[i].tb_bx; }
                //    else if (resjson[i].tb_yx) { tb_yx=resjson[i].tb_yx; }
                //    else if (resjson[i].tb_fx) {tb_fx=resjson[i].tb_fx; }
                //    else if (resjson[i].tb_fl) { tb_fl=resjson[i].tb_fl; }
                //    else if (resjson[i].tb_lg) { tb_lg=resjson[i].tb_lg; }
                //    else if (resjson[i].tb_tq) { tb_tq=resjson[i].tb_tq; }
                //    else if (resjson[i].tb_bg2) { tb_bg2=resjson[i].tb_bg2; }

                //    else if (resjson[i].tb_fl2) { tb_fl2=resjson[i].tb_fl2; }
                //    else if (resjson[i].tb_lg2) { tb_lg2=resjson[i].tb_lg2; }

                //      //水温
                //    else if (resjson[i].tb_sw) { tb_sw=resjson[i].tb_sw; }
                //    else if (resjson[i].tb_sw2) { tb_sw2=resjson[i].tb_sw2; }

                //}
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



        //发布下午表单按钮
            function All_Releasetable() {
          var d = new Date();
          var week= d.getDay();
          var strlist = "下午的指挥处预报,农业部黄渤海区渔政局专项预报";
        

             var date = myformatter(new Date($("#tianbaoriqi").datebox("getValue")));//控件时间 格式：2015-04-28
                var phour = $('#select_hour').val();
                var datas = { datas: strlist, dates: date, hours: phour,  makewordtime :makewordtime };
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
                //alert(strlist);
                var datas = { datas: strlist, dates: date,hours: phour,  makewordtime :makewordtime };
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
                //alert(strlist);
                var datas = { datas: strlist, dates: date,hours: phour,  makewordtime :makewordtime };
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
                case 26: submit_26(id); break;//表单26提交
                case 35: submit_35(id); break;//表单35提交 渔政局表2
                default:
            }
        }

        //保存所有
        function alldlg_Submit() {//（无当天数据保存、有当天数据修改）↓
            submit_23(23); //填报信息
            submit_26(26); //表单26提交
            submit_35(35);//表单35
        }

        //表单数据拼接 从左至右 从上至下
        { 

            //表单23
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


            //表单26
            function submit_26(id) {
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

                //var tab = document.getElementById("addtable") ;
                //var rows = tab.rows.length;    
                //var count = (rows - 1) / 2;
                var list = [];
                $("tr[name='trname']").each(function () {
                    list.push($(this).attr("id"));
                })
                if (list == "") {

                }
                else {
                    // alert(list.unique3());

                    //将相同的数据分组
                    var obj = {};

                    for (var i = 0; i < list.length; i++) {

                        var item = list[i];

                        if (!obj[item]) {

                            obj[item] = [item];

                        } else {

                            obj[item].push(item);

                        }

                    }




                    for (var n in obj) {

                        // alert(obj[i]); 
                        // alert(list);
                        // list.unique3().length + 1;
                        var i = obj[n].unique3()[0];
                         
                        for (var j = 1; j <= obj[n].length; j++) {
                             str_data += $("#ZHC_XW_SA0" + i).val() + ",";
                           str_data +=document.getElementById("SJ_0" + i + "_" + j).innerHTML + ",";
                            str_data += $("#ZHC_XW_SA0" + i + "_FX_0" + j).val() + ",";
                            str_data += $("#ZHC_XW_SA0" + i + "_FL_0" + j).val() + ",";
                            str_data += $("#ZHC_XW_SA0" + i + "_BG_0" + j).val() + ",";
                        }
                    }
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(id, data,list);
            }
            //表单35 渔政局表2
           function submit_35(id) {
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
                            if (i.length > 2) {
                                i = i.substring(3, i.length);
                            }
                            
                             str_data += $("#YZJ_XW_SA0" + i).val() + ",";
                           str_data +=document.getElementById("SJ_0" + i + "_" + j).innerHTML + ",";
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

            //表单41 下午二十一、东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
            function submit_41(id) {
              var str_data = "";
                for (var i = 1; i < 4; i++) {
                    str_data += $("#DY_12H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#DY_24H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#DY_48H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                   str_data += $("#DY_72H_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
                submitDYForeastNo();
            }

            //ajax公共方法(表单类型，post数组)
            function submit_ajax(types, datas1) {
                //   alert(datas[0].datas);
                 var text_color = ""; var area_color = ""; var divid = "";
                var ys_color = "";
                if (types * 1 < 10) {
                    divid = "ddlg_0" + types;
                } else {
                    divid = "ddlg_" + types;
                }
               
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
                }else{
                    submit_rtn(types, datas1);
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

            }
            //水温可编辑
            function sw_isabled() {
                $("input[id*='SW']").removeAttr("disabled");
                $("input[id*='ICE']").removeAttr("disabled");
            }
            //水温不可编辑
            function sw_disabled() {
                $("input[id*='SW']").attr("disabled", "disabled");
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
            $("#select_hour").val(16);
            var today = new Date(); // 获取今天时间
            GetDate(today); //根据当前年月日 推算未来年月日
            var date = $("#tianbaoriqi").datebox("getValue");
            t = date.split('-')[0] + "年" + date.split('-')[1] + "月" + date.split('-')[2] + "日" + $('#select_hour').val() + "时";
            $("#Fabushijian").val(t);
        });//初始化加载
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

            //$(":input").each(function (a, b) {
            //    var id = $(b).attr("id");//获取id属性
            //    var name = $(b).attr("name");//获取name属性
            //    if (id == "name" || id == "pwd" || id == "theme") {
            //        $(b).val("");//清空
            //    }
            //});

        }

        //ajax 加载各表数据
        function gettabledata(date1, searchType) { //searchType 按填报日期还是预报日期查询 p:填报日期 f:预报日期

            var dates = myformatter(date1);
            $.ajax({//综合
                type: "POST",
                url: "/Ajax/gettablelist.ashx?method=getPMdataNew&date=" + dates + "&searchtype=" + searchType,
                cache:false,
                beforeSend: function () {
                    $('#w').window('open');
                    $("#btn_select").attr({ disabled: "disabled" });
                    //getPredicTideData(dates);
                },
                success: function (result) {
                    var resjson = JSON.parse(result);
                    for (var j = 0; j < resjson.length; j++) {
                        switch (resjson[j].type) {
                            case "t23": gettable23(resjson[j].children, date1); break;
                            case "t26": gettable26(resjson[j], date1); dlgclose("26"); break;
                            case "t35": gettable35(resjson[j], date1); dlgclose("35"); break;
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

            //表单05数据
            function gettable05(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].ESASWAREA) {
                        case "渤海": nqyname = "BH"; break;
                        case "黄海北部": nqyname = "HHBB"; break;
                        case "黄海中部": nqyname = "HHZB"; break;
                        case "黄海南部": nqyname = "HHNB"; break;
                        default:
                    }
                    $("#" + nqyname + "_GDF_401").val(resjson[i].ESASWLOWESTWAVEHEIGHT);
                    $("#" + nqyname + "_GDE_401").val(resjson[i].ESASWHIGHTESTWAVEHEIGHT);
                    $("#" + nqyname + "_HL_401").val(resjson[i].ESASWWAVETYPE);
                }
            }

            //表单06数据
            function gettable06(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].qy) {
                        case "日照近海": nqyname = "RZJH"; break;
                        case "青岛近海": nqyname = "QDJH"; break;
                        case "威海近海": nqyname = "WHJH"; break;
                        case "烟台近海": nqyname = "YTJH"; break;
                        case "潍坊近海": nqyname = "WFJH"; break;
                        case "东营近海": nqyname = "DYJH"; break;
                        case "滨州近海": nqyname = "BZJH"; break;
                        default:
                    }
                    $("#" + nqyname + "_LG").val(resjson[i].dl);
                    $("#" + nqyname + "_BCSW").val(resjson[i].sw);
                }
            }

            //表单07数据
            function gettable07(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
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
                    $("#" + nqyname + "_01G_H").val(resjson[i].SDOSCTFIRSTHIGHWAVEHOUR);
                    $("#" + nqyname + "_01G_MIN").val(resjson[i].SDOSCTFIRSTHIGHWAVEMINUTE);
                    $("#" + nqyname + "_01D_H").val(resjson[i].SDOSCTFIRSTLOWWAVEHOUR);
                    $("#" + nqyname + "_01D_MIN").val(resjson[i].SDOSCTFIRSTLOWWAVEMINUTE);
                    $("#" + nqyname + "_02G_H").val(resjson[i].SDOSCTSECONDHIGHWAVEHOUR);
                    $("#" + nqyname + "_02G_MIN").val(resjson[i].SDOSCTSECONDHIGHWAVEMINUTE);
                    $("#" + nqyname + "_02D_H").val(resjson[i].SDOSCTSECONDLOWWAVEHOUR);
                    $("#" + nqyname + "_02D_MIN").val(resjson[i].SDOSCTSECONDLOWWAVEMINUTE);
                }
            }

            //表单08数据
            function gettable08(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#01GC_H").val(resjson[i].QDTLFIRSTHIGHWAVEHOUR);
                    $("#01GC_MIN").val(resjson[i].QDTLFIRSTHIGHWAVEMINUTE);
                    $("#01GC_CM").val(resjson[i].QDTLFIRSTHIGHWAVEHEIGHT);
                    $("#01DC_H").val(resjson[i].QDTLFIRSTLOWWAVEHOUR);
                    $("#01DC_MIN").val(resjson[i].QDTLFIRSTLOWWAVEMINUTE);
                    $("#01DC_CM").val(resjson[i].QDTLFIRSTLOWWAVEHEIGHT);
                    $("#02GC_H").val(resjson[i].QDTLSECONDHIGHWAVEHOUR);
                    $("#02GC_MIN").val(resjson[i].QDTLSECONDHIGHWAVEMINUTE);
                    $("#02GC_CM").val(resjson[i].QDTLSECONDHIGHWAVEHEIGHT);
                    $("#02DC_H").val(resjson[i].QDTLSECONDLOWWAVEHOUR);
                    $("#02DC_MIN").val(resjson[i].QDTLSECONDLOWWAVEMINUTE);
                    $("#02DC_CM").val(resjson[i].QDTLSECONDLOWWAVEHEIGHT);
                    $("#MRHBLG").val(resjson[i].QDTLTOMORROWWAVEHEIGHT);
                    $("#MRHBLX").val(resjson[i].QDTLTOMORROWWAVEDIR);
                }
            }

            //表单09数据
            function gettable09(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {

                    date = new Date(resjson[i].yb);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#BG_0" + num).val(resjson[i].bg);
                    $("#BX_0" + num).val(resjson[i].bx);
                    $("#FX_0" + num).val(resjson[i].fx);
                    $("#FL_0" + num).val(resjson[i].fl);

                }
            }

            //表单10数据
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

            //表单11数据
            function gettable11(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#NBYT_BG_0" + num).val(resjson[i].NWFWTFWAVEHEIGHT);
                    $("#NBYT_BX_0" + num).val(resjson[i].NWFWTFWAVEDIR);
                    $("#NBYT_FX_0" + num).val(resjson[i].NWFWTFFLOWDIR);
                    $("#NBYT_FL_0" + num).val(resjson[i].NWFWTFFLOWLEVEL);
                    $("#NBYT_SW_0" + num).val(resjson[i].NWFWTFWATERTEMPERATURE);
                    $("#NBYT_TQ_0" + num).val(resjson[i].NWFWTFWEATHER);
                }
            }

            //表单12数据
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

            //表单13数据
            function gettable13(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#BH13_BG").val(resjson[i].bg);
                    $("#BH13_BX").val(resjson[i].bb);
                    $("#BH13_YX").val(resjson[i].by);
                    $("#BH13_BZ").val(resjson[i].bbz);
                    $("#HHBB_BG").val(resjson[i].hbg);
                    $("#HHBB_BX").val(resjson[i].hbbx);
                    $("#HHBB_YX").val(resjson[i].hby);
                    $("#HHBB_BZ").val(resjson[i].hbb);
                    $("#HHZB_BG").val(resjson[i].hzg);
                    $("#HHZB_BX").val(resjson[i].hzbx);
                    $("#HHZB_YX").val(resjson[i].hzy);
                    $("#HHZB_BZ").val(resjson[i].hzb);
                    $("#HHNB_BG").val(resjson[i].hng);
                    $("#HHNB_BX").val(resjson[i].hnb);
                    $("#HHNB_YX").val(resjson[i].hny);
                    $("#HHNB_BZ").val(resjson[i].hnbz);
                    $("#QDJA_BG").val(resjson[i].qg);
                    $("#QDJA_BX").val(resjson[i].qb);
                    $("#QDJA_YX").val(resjson[i].qy);
                    $("#QDJA_BZ").val(resjson[i].hjb);
                    //$("#BH13_HL").val(resjson[i].bj);
                    //$("#HHBB_HL").val(resjson[i].hbbj);
                    //$("#HHZB_HL").val(resjson[i].hzbj);
                }
            }

            //表单14数据
            function gettable14(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#BH14_BG").val(resjson[i].bg);
                    $("#BH14_BX").val(resjson[i].bb);
                    $("#BH14_YX").val(resjson[i].by);
                    $("#BH14_BZ").val(resjson[i].bbz);
                    $("#HHBB14_BG").val(resjson[i].hbg);
                    $("#HHBB14_BX").val(resjson[i].hbbx);
                    $("#HHBB14_YX").val(resjson[i].hby);
                    $("#HHBB14_BZ").val(resjson[i].hbb);
                    $("#HHZB14_BG").val(resjson[i].hzg);
                    $("#HHZB14_BX").val(resjson[i].hzbx);
                    $("#HHZB14_YX").val(resjson[i].hzy);
                    $("#HHZB14_BZ").val(resjson[i].hzb);
                    $("#HHNB14_BG").val(resjson[i].hng);
                    $("#HHNB14_BX").val(resjson[i].hnb);
                    $("#HHNB14_YX").val(resjson[i].hny);
                    $("#HHNB14_BZ").val(resjson[i].hnbz);
                    //$("#QDJA14_BG").val(resjson[i].qg);
                    //$("#QDJA14_BX").val(resjson[i].qb);
                    //$("#QDJA14_YX").val(resjson[i].qy);
                    //$("#QDJA14_BZ").val(resjson[i].hjb);
                }
            }

            //表单15数据
            function gettable15(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#BH15_BG").val(resjson[i].bg);
                    $("#BH15_BX").val(resjson[i].bb);
                    $("#BH15_YX").val(resjson[i].by);
                    $("#BH15_BZ").val(resjson[i].bbz);
                    $("#HHBB15_BG").val(resjson[i].hbg);
                    $("#HHBB15_BX").val(resjson[i].hbbx);
                    $("#HHBB15_YX").val(resjson[i].hby);
                    $("#HHBB15_BZ").val(resjson[i].hbb);
                    $("#HHZB15_BG").val(resjson[i].hzg);
                    $("#HHZB15_BX").val(resjson[i].hzbx);
                    $("#HHZB15_YX").val(resjson[i].hzy);
                    $("#HHZB15_BZ").val(resjson[i].hzb);
                    $("#HHNB15_BG").val(resjson[i].hng);
                    $("#HHNB15_BX").val(resjson[i].hnb);
                    $("#HHNB15_YX").val(resjson[i].hny);
                    $("#HHNB15_BZ").val(resjson[i].hnbz);
                    //$("#QDJA15_BG").val(resjson[i].qg);
                    //$("#QDJA15_BX").val(resjson[i].qb);
                    //$("#QDJA15_YX").val(resjson[i].qy);
                    //$("#QDJA15_BZ").val(resjson[i].hjb);
                }
            }

            //表单16数据
            function gettable16(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#WFG_GCCS_01").val(resjson[i].s1);
                    $("#WFG_GCCS_02").val(resjson[i].s2);
                    $("#WFG_GCCG_01").val(resjson[i].g1);
                    $("#WFG_GCCG_02").val(resjson[i].g2);
                    $("#WFG_DCCS_01").val(resjson[i].ds1);
                    $("#WFG_DCCS_02").val(resjson[i].ds2);
                    $("#WFG_DCCG_01").val(resjson[i].dg1);
                    $("#WFG_DCCG_02").val(resjson[i].dg2);

                }
            }

            //表单17数据
            function gettable17(resjson, date1) {

                for (var i = 0; i < resjson.length; i++) {

                    $("#DYHSYC_LG").val(resjson[i].yl);
                    $("#DYHSYC_SW").val(resjson[i].ys);
                    $("#DYHSYC_YY").val(resjson[i].yy);
                    $("#DLHSYC_LG").val(resjson[i].ll);
                    $("#DLHSYC_SW").val(resjson[i].ls);
                    $("#DLHSYC_YY").val(resjson[i].ly);
                    $("#SLRHSYC_LG").val(resjson[i].sl);
                    $("#SLRHSYC_SW").val(resjson[i].ss);
                    $("#SLRHSYC_YY").val(resjson[i].sy);
                    $("#JSTHSYC_LG").val(resjson[i].jl);
                    $("#JSTHSYC_SW").val(resjson[i].js);
                    $("#JSTHSYC_YY").val(resjson[i].jy);

                }
            }

            //表单18数据
            function gettable18(resjson, date1) {

                for (var i = 0; i < resjson.length; i++) {
                    var stationName = "";
                    if (resjson[i].SEABEACH == "青岛市区") 
                        stationName = "XMD";
                    else if (resjson[i].SEABEACH == "金沙滩") 
                        stationName = "WMT";
                    $("#"+stationName+"_01GC_H").val(resjson[i].GB24HTFFIRSTHIGHWAVEHOUR);
                    $("#"+stationName+"_01GC_MIN").val(resjson[i].GB24HTFFIRSTHIGHWAVEMINUTE);
                    $("#"+stationName+"_01DC_H").val(resjson[i].GB24HTFFIRSTLOWWAVEHOUR);
                    $("#"+stationName+"_01DC_MIN").val(resjson[i].GB24HTFFIRSTLOWWAVEMINUTE);
                    $("#"+stationName+"_02GC_H").val(resjson[i].GB24HTFSECONDHIGHWAVEHOUR);
                    $("#"+stationName+"_02GC_MIN").val(resjson[i].GB24HTFSECONDHIGHWAVEMINUTE);
                    $("#"+stationName+"_02DC_H").val(resjson[i].GB24HTFSECONDLOWWAVEHOUR);
                    $("#"+stationName+"_02DC_MIN").val(resjson[i].GB24HTFSECONDLOWWAVEMINUTE);
                }
            }

            //表单19数据
            function gettable19(resjson, date1) {

                for (var i = 0; i < resjson.length; i++) {
                    $("#QDJH_LG19").val(resjson[i].ql);
                    $("#QDJH_SW19").val(resjson[i].qs);
                    $("#JMJH_LG19").val(resjson[i].jl);
                    $("#JMJH_SW19").val(resjson[i].js);
                    $("#JZJH_LG19").val(resjson[i].jzl);
                    $("#JZJH_SW19").val(resjson[i].jzs);
                    $("#JNJH_LG19").val(resjson[i].jnl);
                    $("#JNJH_SW19").val(resjson[i].jns);
                }
            }

            //表单20数据
            function gettable20(resjson, date1) {

                for (var i = 0; i < resjson.length; i++) {
                    $("#01GC_H20").val(resjson[i].g1s);
                    $("#01GC_MIN20").val(resjson[i].g1m);
                    $("#01GC_CM20").val(resjson[i].g1g);
                    $("#01DC_H20").val(resjson[i].d1s);
                    $("#01DC_MIN20").val(resjson[i].d1m);
                    $("#01DC_CM20").val(resjson[i].d1g);
                    $("#02GC_H20").val(resjson[i].g2s);
                    $("#02GC_MIN20").val(resjson[i].g2m);
                    $("#02GC_CM20").val(resjson[i].g2g);
                    $("#02DC_H20").val(resjson[i].d2s);
                    $("#02DC_MIN20").val(resjson[i].d2m);
                    $("#02DC_CM20").val(resjson[i].d2g);

                }
            }

            //表单21数据
            function gettable21(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#SDJH_LG_24").val(resjson[i].s2l);
                    $("#SDJH_SW_24").val(resjson[i].s2s);
                    $("#WDQ_LG_24").val(resjson[i].w2l);
                    $("#WDQ_SW_24").val(resjson[i].w2s);
                    $("#CST_LG_24").val(resjson[i].c2l);
                    $("#CST_SW_24").val(resjson[i].c2s);
                    $("#RSS_LG_24").val(resjson[i].r2l);
                    $("#RSS_SW_24").val(resjson[i].r2s);
                    $("#WHJH_LG_48").val(resjson[i].wh4l);
                    $("#WHJH_SW_48").val(resjson[i].wh4s);
                    $("#SDJH_LG_48").val(resjson[i].s4l);
                    $("#SDJH_SW_48").val(resjson[i].s4s);
                    $("#WDQ_LG_48").val(resjson[i].w4l);
                    $("#WDQ_SW_48").val(resjson[i].w4s);
                    $("#CST_LG_48").val(resjson[i].c4l);
                    $("#CST_SW_48").val(resjson[i].c4s);
                    $("#RSS_LG_48").val(resjson[i].r4l);
                    $("#RSS_SW_48").val(resjson[i].r4s);




                    $("#SDJH_LG_24_1").val(resjson[i].s2l);
                    $("#SDJH_SW_24_1").val(resjson[i].s2s);
                    $("#WDQ_LG_24_1").val(resjson[i].w2l);
                    $("#WDQ_SW_24_1").val(resjson[i].w2s);
                    $("#CST_LG_24_1").val(resjson[i].c2l);
                    $("#CST_SW_24_1").val(resjson[i].c2s);
                    $("#RSS_LG_24_1").val(resjson[i].r2l);
                    $("#RSS_SW_24_1").val(resjson[i].r2s);
                    $("#WHJH_LG_48_1").val(resjson[i].wh4l);
                    $("#WHJH_SW_48_1").val(resjson[i].wh4s);
                    $("#SDJH_LG_48_1").val(resjson[i].s4l);
                    $("#SDJH_SW_48_1").val(resjson[i].s4s);
                    $("#WDQ_LG_48_1").val(resjson[i].w4l);
                    $("#WDQ_SW_48_1").val(resjson[i].w4s);
                    $("#CST_LG_48_1").val(resjson[i].c4l);
                    $("#CST_SW_48_1").val(resjson[i].c4s);
                    $("#RSS_LG_48_1").val(resjson[i].r4l);
                    $("#RSS_SW_48_1").val(resjson[i].r4s);
                }
            }

            //表单22数据
            function gettable22(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].qy) {
                        case "石岛": nqyname = "SD"; break;
                        case "文登": nqyname = "WD"; break;
                        case "威海": nqyname = "WH"; break;
                        case "成山头": nqyname = "CST"; break;
                        case "乳山": nqyname = "RS"; break;
                        default:
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

            //表单23填报数据 默认值
            function gettable23(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#Fabudanwei").val(resjson[i].fb);
                    $("#Tel").val(resjson[i].dh);
                    //$("#Fabushijian").val(myformatter1(new Date(resjson[i].rq)) + resjson[i].xs + "时");
                    //$("#Fabushijian").val(myformatter1(new Date(resjson[i].rq)) + "15时");
                    $("#Chuanzhen").val(resjson[i].cz);
                    //$("#Hailang").val(resjson[i].hl);
                    //$("#Chaoxi").val(resjson[i].cx);
                    //$("#Shuiwen").val(resjson[i].sw);
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

         function count(o){

                var t = typeof o;
                if(t == 'string'){

                        return o.length;

                }else if(t == 'object'){

                        var n = 0;

                       for(var i in o){
                                n++;
                        }
                        return n;
                }
                return false;
        }

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
                          m =  obj[vlist[i-1]].length;//预报海区个数
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
                     //var today = new Date(); // 获取今天时间
                        var today = new Date($("#tianbaoriqi").datebox("getValue"));//控件时间 格式：2015-04-28
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
                          m =  obj3[vlist3[i-1]].length;//预报海区个数
                         for (j = 1; j <= m; j++) {
                             if (j == 1) {
                                 tr2 += "<tr  id =yzj" + i + " name='trname2'><td rowspan='" + m + "'><input id='YZJ_XW_SA0" + i + "' type='text' maxlength='15' /></td>" +
                                 "<td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                 "<td> <input id='YZJ_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                 "<td> <input id='YZJ_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                 "<td> <input id='YZJ_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>" +
                                 "<td rowspan='" + m + "'><input class='button' id =yzj" + i + " type='button' value='删除' onclick='deletr2(this)'></td></tr>";

                             }
                             else {
                                 tr2 += "<tr id=yzj" + i + " name='trname2'><td id = 'SJ_0" + i + "_" + j + "' class='SJ_0" + j + "'>*日</td>" +
                                 "<td> <input id='YZJ_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                 "<td> <input id='YZJ_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                 "<td> <input id='YZJ_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>  </tr>";
                             }

                            
                          

                         }
                        
                         

                     }
                        $("#addtable2").append(tr2);
                     //var today = new Date(); // 获取今天时间
                        var today = new Date($("#tianbaoriqi").datebox("getValue"));//控件时间 格式：2015-04-28
                         GetDate(today); //根据当前年月日 推算未来年月日

                            
                 }
                   

             }
         }
                   
        //表单指挥处16点填报数据
        function gettable26(result, date1) {

            var pbtypes = result.pbtype;
            if (pbtypes == "bys") {
                var waveForecast = result.wave;
                for (var j = 0; j < waveForecast.length; j++) {
                    if (waveForecast[j].FORECASTAREA == "青岛近海") {
                        $("#ZHC_XW_QDJH_BG_01").val(waveForecast[j].WAVE24FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "渤海海峡") {
                        $("#ZHC_XW_BHHX_BG_01").val(waveForecast[j].WAVE24FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "渤海") {
                        $("#ZHC_XW_BH_BG_01").val(waveForecast[j].WAVE24FORECAST);

                        $("#ZHC_XW_BH_BG_02").val(waveForecast[j].WAVE48FORECAST);

                        $("#ZHC_XW_BH_BG_03").val(waveForecast[j].WAVE72FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "黄海北部") {
                        $("#ZHC_XW_NHH_BG_01").val(waveForecast[j].WAVE24FORECAST);

                        $("#ZHC_XW_NHH_BG_02").val(waveForecast[j].WAVE48FORECAST);

                        $("#ZHC_XW_NHH_BG_03").val(waveForecast[j].WAVE72FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "黄海中部") {
                        $("#ZHC_XW_MHH_BG_01").val(waveForecast[j].WAVE24FORECAST);
                        $("#ZHC_XW_MHH_BG_02").val(waveForecast[j].WAVE48FORECAST);
                        $("#ZHC_XW_MHH_BG_03").val(waveForecast[j].WAVE72FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "黄海南部") {
                        $("#ZHC_XW_SHH_BG_01").val(waveForecast[j].WAVE24FORECAST);
                        $("#ZHC_XW_SHH_BG_02").val(waveForecast[j].WAVE48FORECAST);
                        $("#ZHC_XW_SHH_BG_03").val(waveForecast[j].WAVE72FORECAST);
                    }
                }
                //add by Lian start
                //青岛近海未来两天的风力、风向取建模数据，
                //波向取风向的值
                var windForecast = result.wind;
                for (var j = 0; j < windForecast.length; j++) {
                    if (windForecast[j].FORECASTAREA == "青岛近海") {
                        $("#ZHC_XW_QDJH_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                        $("#ZHC_XW_QDJH_FX_03").val(windForecast[j].WINDDIRECTION72FORECAST);

                        $("#ZHC_XW_QDJH_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                        $("#ZHC_XW_QDJH_FL_03").val(windForecast[j].WINDFORCE72FORECAST);

                        $("#ZHC_XW_QDJH_BX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                        $("#ZHC_XW_QDJH_BX_03").val(windForecast[j].WINDDIRECTION72FORECAST);
                    }
                }
                //波高去下午二的填报数据
                var bogaoQDJH = result.qdjhfor2;
                for (var k = 0; k < bogaoQDJH.length; k++) {
                    if (bogaoQDJH[k].SDOSCWAREA == "青岛近海") {
                        $("#ZHC_XW_QDJH_BG_02").val(bogaoQDJH[k].SDOSCWESTWAVEHEIGHT48H);
                        $("#ZHC_XW_QDJH_BG_03").val(bogaoQDJH[k].SDOSCWESTWAVEHEIGHT72H);
                    }
                }

                //add by Lian end




                //var windForecast = result.wind;
                //for (var j = 0; j < windForecast.length; j++) {

                //    if (windForecast[j].FORECASTAREA == "青岛近海") {
                //        $("#ZHC_XW_QD_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                //        $("#ZHC_XW_QD_FL_01").val(windForecast[j].WINDFORCE24FORECAST);

                //        $("#ZHC_XW_QDJH_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                //        $("#ZHC_XW_QDJH_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                //        $("#ZHC_XW_QDJH_BX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                //    }
                //    else if (windForecast[j].FORECASTAREA == "渤海海峡") {
                //        $("#ZHC_XW_BHHX_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                //        $("#ZHC_XW_BHHX_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                //        $("#ZHC_XW_BHHX_BX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                //    }
                //    else if (windForecast[j].FORECASTAREA == "渤海") {
                //        $("#ZHC_XW_BH_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                //        $("#ZHC_XW_BH_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                //        $("#ZHC_XW_BH_BX_01").val(windForecast[j].WINDDIRECTION24FORECAST);

                //        $("#ZHC_XW_BH_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                //        $("#ZHC_XW_BH_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                //        $("#ZHC_XW_BH_BX_02").val(windForecast[j].WINDDIRECTION48FORECAST);

                //        $("#ZHC_XW_BH_FX_03").val(windForecast[j].WINDDIRECTION72FORECAST);
                //        $("#ZHC_XW_BH_FL_03").val(windForecast[j].WINDFORCE72FORECAST);
                //        $("#ZHC_XW_BH_BX_03").val(windForecast[j].WINDDIRECTION72FORECAST);
                //    }
                //    else if (windForecast[j].FORECASTAREA == "黄海北部") {
                //        $("#ZHC_XW_NHH_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                //        $("#ZHC_XW_NHH_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                //        $("#ZHC_XW_NHH_BX_01").val(windForecast[j].WINDDIRECTION24FORECAST);

                //        $("#ZHC_XW_NHH_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                //        $("#ZHC_XW_NHH_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                //        $("#ZHC_XW_NHH_BX_02").val(windForecast[j].WINDDIRECTION48FORECAST);

                //        $("#ZHC_XW_NHH_FX_03").val(windForecast[j].WINDDIRECTION72FORECAST);
                //        $("#ZHC_XW_NHH_FL_03").val(windForecast[j].WINDFORCE72FORECAST);
                //        $("#ZHC_XW_NHH_BX_03").val(windForecast[j].WINDDIRECTION72FORECAST);
                //    }
                //    else if (windForecast[j].FORECASTAREA == "黄海中部") {
                //        $("#ZHC_XW_MHH_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                //        $("#ZHC_XW_MHH_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                //        $("#ZHC_XW_MHH_BX_01").val(windForecast[j].WINDDIRECTION24FORECAST);

                //        $("#ZHC_XW_MHH_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                //        $("#ZHC_XW_MHH_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                //        $("#ZHC_XW_MHH_BX_02").val(windForecast[j].WINDDIRECTION48FORECAST);

                //        $("#ZHC_XW_MHH_FX_03").val(windForecast[j].WINDDIRECTION72FORECAST);
                //        $("#ZHC_XW_MHH_FL_03").val(windForecast[j].WINDFORCE72FORECAST);
                //        $("#ZHC_XW_MHH_BX_03").val(windForecast[j].WINDDIRECTION72FORECAST);
                //    }
                //    else if (windForecast[j].FORECASTAREA == "黄海南部") {
                //        $("#ZHC_XW_SHH_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                //        $("#ZHC_XW_SHH_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                //        $("#ZHC_XW_SHH_BX_01").val(windForecast[j].WINDDIRECTION24FORECAST);

                //        $("#ZHC_XW_SHH_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                //        $("#ZHC_XW_SHH_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                //        $("#ZHC_XW_SHH_BX_02").val(windForecast[j].WINDDIRECTION48FORECAST);

                //        $("#ZHC_XW_SHH_FX_03").val(windForecast[j].WINDDIRECTION72FORECAST);
                //        $("#ZHC_XW_SHH_FL_03").val(windForecast[j].WINDFORCE72FORECAST);
                //        $("#ZHC_XW_SHH_BX_03").val(windForecast[j].WINDDIRECTION72FORECAST);
                //    }
                //}

                var dt24 = result.dt24;
                for (var q = 0; q < dt24.length; q++) {
                    $("#ZHC_XW_QDJH_BG_01").val(dt24[q].SA24HWFQDOFFSHOREWAVEHEIGHT);
                    $("#ZHC_XW_QDJH_BX_01").val(dt24[q].SA24HWFQDOFFSHOREWAVEDIR);
                    $("#ZHC_XW_QDJH_FX_01").val(dt24[q].SA24HWFQDOFFSHOREWAVEDIR);
                    $("#ZHC_XW_QD_FX_01").val(dt24[q].SA24HWFQDOFFSHOREWAVEDIR);

                    $("#ZHC_XW_BH_BG_01").val(dt24[q].SA24HWFBOHAIWAVEHEIGHT);
                    $("#ZHC_XW_BH_BX_01").val(dt24[q].SA24HWFBOHAIWAVEDIR);
                    $("#ZHC_XW_BH_FX_01").val(dt24[q].SA24HWFBOHAIWAVEDIR);

                    $("#ZHC_XW_NHH_BG_01").val(dt24[q].SA24HWFNORTHOFYSWAVEHEIGHT);
                    $("#ZHC_XW_NHH_BX_01").val(dt24[q].SA24HWFNORTHOFYSWAVEDIR);
                    $("#ZHC_XW_NHH_FX_01").val(dt24[q].SA24HWFNORTHOFYSWAVEDIR);

                    $("#ZHC_XW_MHH_BG_01").val(dt24[q].SA24HWFMIDDLEOFYSWAVEHEIGHT);
                    $("#ZHC_XW_MHH_BX_01").val(dt24[q].SA24HWFMIDDLEOFYSWAVEDIR);
                    $("#ZHC_XW_MHH_FX_01").val(dt24[q].SA24HWFMIDDLEOFYSWAVEDIR);

                    $("#ZHC_XW_SHH_BG_01").val(dt24[q].SA24HWFSOUTHOFYSWAVEHEIGHT);
                    $("#ZHC_XW_SHH_BX_01").val(dt24[q].SA24HWFSOUTHOFYSWAVEDIR);
                    $("#ZHC_XW_SHH_FX_01").val(dt24[q].SA24HWFSOUTHOFYSWAVEDIR);
                }

                var dt48 = result.dt48;
                for (var w = 0; w < dt48.length; w++) {
                    $("#ZHC_XW_BH_BG_02").val(dt48[w].SA48HWFBOHAIWAVEHEIGHT);
                    $("#ZHC_XW_BH_BX_02").val(dt48[w].SA48HWFBOHAIWAVEDIR);
                    $("#ZHC_XW_BH_FX_02").val(dt48[w].SA48HWFBOHAIWAVEDIR);

                    $("#ZHC_XW_NHH_BG_02").val(dt48[w].SA48HWFNORTHOFYSWAVEHEIGHT);
                    $("#ZHC_XW_NHH_BX_02").val(dt48[w].SA48HWFNORTHOFYSWAVEDIR);
                    $("#ZHC_XW_NHH_FX_02").val(dt48[w].SA48HWFNORTHOFYSWAVEDIR);

                    $("#ZHC_XW_MHH_BG_02").val(dt48[w].SA48HWFMIDDLEOFYSWAVEHEIGHT);
                    $("#ZHC_XW_MHH_BX_02").val(dt48[w].SA48HWFMIDDLEOFYSWAVEDIR);
                    $("#ZHC_XW_MHH_FX_02").val(dt48[w].SA48HWFMIDDLEOFYSWAVEDIR);

                    $("#ZHC_XW_SHH_BG_02").val(dt48[w].SA48HWFSOUTHOFYSWAVEHEIGHT);
                    $("#ZHC_XW_SHH_BX_02").val(dt48[w].SA48HWFSOUTHOFYSWAVEDIR);
                    $("#ZHC_XW_SHH_FX_02").val(dt48[w].SA48HWFSOUTHOFYSWAVEDIR);
                }

                var dt72 = result.dt72;
                for (var e = 0; e < dt72.length; e++) {
                    $("#ZHC_XW_BH_BG_03").val(dt72[e].SA72HWFBOHAIWAVEHEIGHT);
                    $("#ZHC_XW_BH_BX_03").val(dt72[e].SA72HWFBOHAIWAVEDIR);
                    $("#ZHC_XW_BH_FX_03").val(dt72[e].SA72HWFBOHAIWAVEDIR);

                    $("#ZHC_XW_NHH_BG_03").val(dt72[e].SA72HWFNORTHOFYSWAVEHEIGHT);
                    $("#ZHC_XW_NHH_BX_03").val(dt72[e].SA72HWFNORTHOFYSWAVEDIR);
                    $("#ZHC_XW_NHH_FX_03").val(dt72[e].SA72HWFNORTHOFYSWAVEDIR);

                    $("#ZHC_XW_MHH_BG_03").val(dt72[e].SA72HWFMIDDLEOFYSWAVEHEIGHT);
                    $("#ZHC_XW_MHH_BX_03").val(dt72[e].SA72HWFMIDDLEOFYSWAVEDIR);
                    $("#ZHC_XW_MHH_FX_03").val(dt72[e].SA72HWFMIDDLEOFYSWAVEDIR);

                    $("#ZHC_XW_SHH_BG_03").val(dt72[e].SA72HWFSOUTHOFYSWAVEHEIGHT);
                    $("#ZHC_XW_SHH_BX_03").val(dt72[e].SA72HWFSOUTHOFYSWAVEDIR);
                    $("#ZHC_XW_SHH_FX_03").val(dt72[e].SA72HWFSOUTHOFYSWAVEDIR);
                }

                var dtZRArea = result.dtZRArea;
                if (dtZRArea != null) {
                    $("#addtable tbody").empty();
                    var bgdhqlist = [];
                    if (dtZRArea.length > 1) {
                        for (var k = 0; k < dtZRArea.length; k++) {

                            bgdhqlist.push(dtZRArea[k].SEAAREA);

                        }

                        getaddtr(bgdhqlist);
                        for (var i = 0; i < dtZRArea.length; i++) {
                            var seaArea = dtZRArea[i].SEAAREA;
                            var publishDate = dtZRArea[i].PUBLISHTIME;
                            var forecastDate = dtZRArea[i].FORECASTTIME;

                            var fdate = new Date(Date.parse(forecastDate.replace(/-/g, "/"))).getTime();
                            var pdate = new Date(Date.parse(publishDate.replace(/-/g, "/"))).getTime();
                            var num = 1;
                            if (bgdhqlist == "") {
                            }
                            else {
                                //将相同的数据分组
                                var obj = {};
                                for (var n = 0; n < bgdhqlist.length; n++) {
                                    var item = bgdhqlist[n];
                                    if (!obj[item]) {
                                        obj[item] = [item];
                                    } else {
                                        obj[item].push(item);
                                    }
                                }
                                var tr = "";

                                var vlist = [];
                                for (var n in obj) {
                                    vlist.push(n);
                                }
                                var addedSeaAreaCount = vlist.length;
                                num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24) - 1;
                                for (var m = 1; m <= addedSeaAreaCount; m++) {
                                    if ($("#ZHC_XW_SA0" + m).val() == "" || $("#ZHC_XW_SA0" + m).val() == seaArea) {
                                        $("#ZHC_XW_SA0" + m).val(seaArea);
                                        $("#ZHC_XW_SA0" + m + "_FX_0" + num).val(dtZRArea[i].WINDDIRECTION);
                                        $("#ZHC_XW_SA0" + m + "_FL_0" + num).val(dtZRArea[i].WINDFORCE);
                                        $("#ZHC_XW_SA0" + m + "_BG_0" + num).val(dtZRArea[i].WAVEHEIGHT);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (pbtypes == "bydb") {
                var resjson = result.children;
                $("#addtable tbody").empty();
                var bgdhqlist = [];
                if (resjson.length > 1) {
                    //var addedSeaAreaCount = (resjson.length -15)/2;

                    for (var k = 0; k < resjson.length; k++) {
                        if (resjson[k].SEAAREA != "青岛市" && resjson[k].SEAAREA != "青岛近海" && resjson[k].SEAAREA != "渤海海峡" && resjson[k].SEAAREA != "渤海" && resjson[k].SEAAREA != "黄海北部" && resjson[k].SEAAREA != "黄海中部" && resjson[k].SEAAREA != "黄海南部")
                            // bgdhqlist += resjson[k].SEAAREA + ",";
                            bgdhqlist.push(resjson[k].SEAAREA);

                    }

                    getaddtr(bgdhqlist);
                    for (var i = 0; i < resjson.length; i++) {
                        var seaArea = resjson[i].SEAAREA;
                        // var seaArea = resjson[i].SEAAREA;
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
                                if (bgdhqlist == "") {
                                }
                                else {
                                    //将相同的数据分组
                                    var obj = {};
                                    for (var n = 0; n < bgdhqlist.length; n++) {
                                        var item = bgdhqlist[n];
                                        if (!obj[item]) {
                                            obj[item] = [item];
                                        } else {
                                            obj[item].push(item);
                                        }
                                    }
                                    var tr = "";

                                    var vlist = [];
                                    for (var n in obj) {
                                        vlist.push(n);



                                    }
                                    var addedSeaAreaCount = vlist.length;
                                    num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                                    for (var m = 1; m <= addedSeaAreaCount; m++) {
                                        if ($("#ZHC_XW_SA0" + m).val() == "" || $("#ZHC_XW_SA0" + m).val() == seaArea) {
                                            $("#ZHC_XW_SA0" + m).val(seaArea);
                                            $("#ZHC_XW_SA0" + m + "_FX_0" + num).val(resjson[i].WINDDIRECTION);
                                            $("#ZHC_XW_SA0" + m + "_FL_0" + num).val(resjson[i].WINDFORCE);
                                            $("#ZHC_XW_SA0" + m + "_BG_0" + num).val(resjson[i].WAVEHEIGHT);
                                            break;
                                        }
                                    }





                                    //    //if (vlist != "") {
                                    //    var addedSeaAreaCount = vlist.length;
                                    //     num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                                    //    for (m = 1; m <= addedSeaAreaCount ; m++) {
                                    //        if(vlist[m-1]==seaArea){
                                    //                 var  x =  obj[seaArea].length;
                                    //        for (j = 1; j <= x; j++) {
                                    //            if ($("#ZHC_XW_SA0" + m).val() == "" || $("#ZHC_XW_SA0" + m).val() == seaArea) {
                                    //                $("#ZHC_XW_SA0" + m).val(seaArea);
                                    //                $("#ZHC_XW_SA0" + m + "_FX_0" + j).val(resjson[i].WINDDIRECTION);
                                    //                $("#ZHC_XW_SA0" + m + "_FL_0" + j).val(resjson[i].WINDFORCE);
                                    //                $("#ZHC_XW_SA0" + m + "_BG_0" + j).val(resjson[i].WAVEHEIGHT);

                                    //            }
                                    //        }
                                    //    } 

                                    //}
                                    // }
                                    break;
                                }





                                //num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                                //for (var m = 1; m <= addedSeaAreaCount;m++)
                                //{
                                //    if($("#ZHC_XW_SA0"+m).val()==""||$("#ZHC_XW_SA0"+m).val()==seaArea)
                                //    {
                                //        $("#ZHC_XW_SA0"+m).val(seaArea);
                                //        $("#ZHC_XW_SA0"+m+"_FX_0"+num).val(resjson[i].WINDDIRECTION);
                                //        $("#ZHC_XW_SA0"+m+"_FL_0"+num).val(resjson[i].WINDFORCE);
                                //        $("#ZHC_XW_SA0"+m+"_BG_0"+num).val(resjson[i].WAVEHEIGHT);
                                //        break;
                                //    }
                                //}
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
                else {
                    for (var i = 0; i < resjson.length; i++) {

                        $("#ZHC_XW_QDJH_BG_01").val(resjson[i].sa24qdoswh);
                        $("#ZHC_XW_QDJH_BX_01").val(resjson[i].sa24qdoswd);

                        //$("#ZHC_XW_BHHX_TX_01").val(resjson[i].fb);
                        //$("#ZHC_XW_BHHX_FX_01").val(resjson[i].dh);
                        //$("#ZHC_XW_BHHX_FL_01").val(myformatter1(new Date(resjson[i].rq)) + resjson[i].xs + "时");
                        //$("#ZHC_XW_BHHX_BG_01").val(resjson[i].cz);
                        //$("#ZHC_XW_BHHX_BX_01").val(resjson[i].hl);

                        $("#ZHC_XW_BH_BG_01").val(resjson[i].sa24bhwh);
                        $("#ZHC_XW_BH_BX_01").val(resjson[i].sa24bhwd);

                        $("#ZHC_XW_NHH_BG_01").val(resjson[i].sa24nhwh);
                        $("#ZHC_XW_NHH_BX_01").val(resjson[i].sa24nhwd);

                        $("#ZHC_XW_MHH_BG_01").val(resjson[i].sa24mhwh);
                        $("#ZHC_XW_MHH_BX_01").val(resjson[i].sa24mhwd);

                        $("#ZHC_XW_SHH_BG_01").val(resjson[i].sa24shwh);
                        $("#ZHC_XW_SHH_BX_01").val(resjson[i].sa24shwd);
                        //================================================
                        $("#ZHC_XW_BH_BG_02").val(resjson[i].sa48bhwh);
                        $("#ZHC_XW_BH_BX_02").val(resjson[i].sa48bhwd);

                        $("#ZHC_XW_NHH_BG_02").val(resjson[i].sa48nhwh);
                        $("#ZHC_XW_NHH_BX_02").val(resjson[i].sa48nhwd);

                        $("#ZHC_XW_MHH_BG_02").val(resjson[i].sa48mhwh);
                        $("#ZHC_XW_MHH_BX_02").val(resjson[i].sa48mhwd);

                        $("#ZHC_XW_SHH_BG_02").val(resjson[i].sa48shwh);
                        $("#ZHC_XW_SHH_BX_02").val(resjson[i].sa48shwd);
                        //================================================
                        $("#ZHC_XW_BH_BG_03").val(resjson[i].sa72bhwh);
                        $("#ZHC_XW_BH_BX_03").val(resjson[i].sa72bhwd);

                        $("#ZHC_XW_NHH_BG_03").val(resjson[i].sa72nhwh);
                        $("#ZHC_XW_NHH_BX_03").val(resjson[i].sa72nhwd);

                        $("#ZHC_XW_MHH_BG_03").val(resjson[i].sa72mhwh);
                        $("#ZHC_XW_MHH_BX_03").val(resjson[i].sa72mhwd);

                        $("#ZHC_XW_SHH_BG_03").val(resjson[i].sa72shwh);
                        $("#ZHC_XW_SHH_BX_03").val(resjson[i].sa72shwd);
                        //================================================

                    }
                }
            }
        }
        //下午渔政局
        function gettable35(result, date1) {
            var effect = result.pbtime;
            if (effect == "yesterday") {
                var waveForecast = result.wave;
                for (var j = 0; j < waveForecast.length; j++) {
                    if (waveForecast[j].FORECASTAREA == "旅顺") {
                        $("#LS_BG_01").val(waveForecast[j].WAVE24FORECAST);
                        $("#LS_BG_02").val(waveForecast[j].WAVE48FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "烟台近海") {
                        $("#YT_BG_01").val(waveForecast[j].WAVE24FORECAST);
                        $("#YT_BG_02").val(waveForecast[j].WAVE48FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "威海近海") {
                        $("#WH_BG_01").val(waveForecast[j].WAVE24FORECAST);
                        $("#WH_BG_02").val(waveForecast[j].WAVE48FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "石岛近海") {
                        $("#SD_BG_01").val(waveForecast[j].WAVE24FORECAST);
                        $("#SD_BG_02").val(waveForecast[j].WAVE48FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "黄海北部") {
                        $("#ZRHQ_BG_01").val(waveForecast[j].WAVE24FORECAST);
                        $("#ZRHQ_BG_02").val(waveForecast[j].WAVE48FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "黄海北部") {
                        $("#ZRHQ_BG_03").val(waveForecast[j].WAVE24FORECAST);
                        $("#ZRHQ_BG_04").val(waveForecast[j].WAVE48FORECAST);
                    }
                    else if (waveForecast[j].FORECASTAREA == "青岛近海") {
                        $("#QD_BG_01").val(waveForecast[j].WAVE24FORECAST);
                    }
                }

                var windForecast = result.wind;
                for (var j = 0; j < windForecast.length; j++) {
                    if (windForecast[j].FORECASTAREA == "旅顺") {
                        $("#LS_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                        $("#LS_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                        $("#LS_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                        $("#LS_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                    }
                    else if (windForecast[j].FORECASTAREA == "烟台近海") {
                        $("#YT_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                        $("#YT_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                        $("#YT_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                        $("#YT_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                    }
                    else if (windForecast[j].FORECASTAREA == "威海近海") {
                        $("#WH_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                        $("#WH_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                        $("#WH_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                        $("#WH_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                    }
                    else if (windForecast[j].FORECASTAREA == "石岛近海") {
                        $("#SD_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                        $("#SD_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                        $("#SD_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                        $("#SD_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                    }
                    else if (windForecast[j].FORECASTAREA == "黄海北部") {
                        $("#ZRHQ_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                        $("#ZRHQ_FX_02").val(windForecast[j].WINDDIRECTION48FORECAST);
                        $("#ZRHQ_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                        $("#ZRHQ_FL_02").val(windForecast[j].WINDFORCE48FORECAST);
                    }
                    else if (windForecast[j].FORECASTAREA == "黄海北部") {
                        $("#ZRHQ_FX_03").val(windForecast[j].WINDDIRECTION24FORECAST);
                        $("#ZRHQ_FX_04").val(windForecast[j].WINDDIRECTION48FORECAST);
                        $("#ZRHQ_FL_03").val(windForecast[j].WINDFORCE24FORECAST);
                        $("#ZRHQ_FL_04").val(windForecast[j].WINDFORCE48FORECAST);
                    }
                    else if (windForecast[j].FORECASTAREA == "青岛近海") {
                        $("#QD_FX_01").val(windForecast[j].WINDDIRECTION24FORECAST);
                        $("#QD_FL_01").val(windForecast[j].WINDFORCE24FORECAST);
                    }
                }
                var dtZRArea2 = result.dtZRArea;
                if (dtZRArea2 != null) {
                    $("#addtable2 tbody").empty();
                    var bgdhqlist2 = [];
                    if (dtZRArea2.length > 1) {
                        for (var k = 0; k < dtZRArea2.length; k++) {
                            bgdhqlist2.push(dtZRArea2[k].SEAAREA);
                        }
                        getaddtr2(bgdhqlist2);
                        for (var i = 0; i < dtZRArea2.length; i++) {
                            var seaArea = dtZRArea2[i].SEAAREA;
                            //var publishDate = dtZRArea2[i].PUBLISHDATE;
                            var forecastDate = dtZRArea2[i].FORECASTDATE;
                            var pdate = new Date($("#tianbaoriqi").datebox("getValue"));
                            //var pdate = new Date(Date.parse(publishDate.replace(/-/g, "/"))).getTime();
                            var fdate = new Date(Date.parse(forecastDate.replace(/-/g, "/"))).getTime();
                            var num = 1;
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
                                //if (effect == "today") {
                                // num = num + 1;
                                //}
                                for (var m = 0; m <= addedSeaAreaCount; m++) {
                                    if ($("#YZJ_XW_SA0" + m).val() == "" || $("#YZJ_XW_SA0" + m).val() == seaArea) {
                                        $("#YZJ_XW_SA0" + m).val(seaArea);
                                        $("#YZJ_XW_SA0" + m + "_FX_0" + num).val(dtZRArea2[i].WINDDIRECTION);
                                        $("#YZJ_XW_SA0" + m + "_FL_0" + num).val(dtZRArea2[i].WINDFORCE);
                                        $("#YZJ_XW_SA0" + m + "_BG_0" + num).val(dtZRArea2[i].WAVEHEIGHT);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (effect == "today") {
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
                        //var today = new Date($("#tianbaoriqi").datebox("getValue"));//控件时间 格式：2015-04-28
                        //var pdate = new Date();
                        var pdate = new Date(Date.parse(publishDate.replace(/-/g, "/"))).getTime();
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
                                    //if (effect == "today") {
                                    // num = num + 1;
                                    //}
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
                    }
                }
            }
        }


        //下午二十一、东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
         function gettable41(result, date1) {
             var effect = result.pbtime;
             var resjson=result.children;
             if(effect == "today"){
                 for (var i = 0; i < resjson.length; i++) {
                     if (resjson[i].TIMEEFFECTIVE == "12") {
                         $("#DY_12H_01").val(resjson[i].WINDDIRECTION);
                         $("#DY_12H_02").val(resjson[i].WINDFORCE);
                         $("#DY_12H_03").val(resjson[i].WAVEHEIGHT);
                     }
                     else if (resjson[i].TIMEEFFECTIVE == "24") {
                         $("#DY_24H_01").val(resjson[i].WINDDIRECTION);
                         $("#DY_24H_02").val(resjson[i].WINDFORCE);
                         $("#DY_24H_03").val(resjson[i].WAVEHEIGHT);
                     } else if (resjson[i].TIMEEFFECTIVE == "48") {
                         $("#DY_48H_01").val(resjson[i].WINDDIRECTION);
                         $("#DY_48H_02").val(resjson[i].WINDFORCE);
                         $("#DY_48H_03").val(resjson[i].WAVEHEIGHT);
                     } else if (resjson[i].TIMEEFFECTIVE == "72") {
                         $("#DY_72H_01").val(resjson[i].WINDDIRECTION);
                         $("#DY_72H_02").val(resjson[i].WINDFORCE);
                         $("#DY_72H_03").val(resjson[i].WAVEHEIGHT);
                     }
                 }
             }
             else if (effect == "yesterday") {
                  for (var i = 0; i < resjson.length; i++) {
                     if (resjson[i].TIMEEFFECTIVE == "24") {
                          $("#DY_12H_01").val(resjson[i].WINDDIRECTION);
                         $("#DY_12H_02").val(resjson[i].WINDFORCE);
                         $("#DY_12H_03").val(resjson[i].WAVEHEIGHT);
                         
                     } else if (resjson[i].TIMEEFFECTIVE == "48") {
                         $("#DY_24H_01").val(resjson[i].WINDDIRECTION);
                         $("#DY_24H_02").val(resjson[i].WINDFORCE);
                         $("#DY_24H_03").val(resjson[i].WAVEHEIGHT);
                        
                     } else if (resjson[i].TIMEEFFECTIVE == "72") {
                         $("#DY_48H_01").val(resjson[i].WINDDIRECTION);
                         $("#DY_48H_02").val(resjson[i].WINDFORCE);
                         $("#DY_48H_03").val(resjson[i].WAVEHEIGHT);
                     }
                 }
             }
         }

        //下午二十二、东营埕岛-未来三天高/低潮预报
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

         function gettable43(result, date1) {
             var effect = result.pbtime;
             var resjson=result.children;
             var proyear =  resjson[0].PROYEAR;
             var prono = resjson[0].PRONO;
              if(effect == "today"){
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






<%------------------------------------  获取潮汐数据-----------------------------------------%>

      function procTideData(stationCode,i,tide){
         var index,s_01G_SJ,s_01G_CW,s_01D_SJ,s_01D_CW,s_02G_SJ,s_02G_CW,s_02D_SJ,s_02D_CW;
         index=i+1;
         s_01G_SJ="#"+stationCode+"_01G_SJ_0"+index;
         s_01G_CW="#"+stationCode+"_01G_CW_0"+index;
         s_01D_SJ="#"+stationCode+"_01D_SJ_0"+index;
         s_01D_CW="#"+stationCode+"_01D_CW_0"+index;
         s_02G_SJ="#"+stationCode+"_02G_SJ_0"+index;
         s_02G_CW="#"+stationCode+"_02G_CW_0"+index;
         s_02D_SJ="#"+stationCode+"_02D_SJ_0"+index;
         s_02D_CW="#"+stationCode+"_02D_CW_0"+index;
         $(s_01G_SJ).val(tide.FSTHIGHWIDETIME.replace(":",""));
         $(s_01G_CW).val(tide.FSTHIGHWIDEHEIGHT);
         $(s_01D_SJ).val(tide.FSTLOWWIDETIME.replace(":",""));
         $(s_01D_CW).val(tide.FSTLOWWIDEHEIGHT);
         $(s_02G_SJ).val(tide.SCDHIGHWIDETIME.replace(":",""));
         $(s_02G_CW).val(tide.SCDHIGHWIDEHEIGHT);
         $(s_02D_SJ).val(tide.SCDLOWWIDETIME.replace(":",""));
         $(s_02D_CW).val(tide.SCDLOWWIDEHEIGHT);
        }

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
    function  proc7City24TideData(tide)
        {

         var matchValue,stationName,s_01G_H,s_01G_MIN,s_01D_H,s_01D_MIN,s_02G_H,s_02G_MIN,s_02D_H,s_02D_MIN,fstHTime,fstLTime,scdHTime,scdLTime;
         matchValue=tide.STATION;
         stationName=matchValue==="101wmt"?"QD":matchValue==="104rzh"?"RZ":matchValue==="109wh"?"WH":matchValue==="111zfd"?"YT":matchValue==="114wfg"?"WF":matchValue==="119hhg"?"DY":matchValue==="125bzg"?"BZ":"";
         s_01G_H="#"+stationName+"_01G_H";
         s_01G_MIN="#"+stationName+"_01G_MIN";
         s_01D_H="#"+stationName+"_01D_H";
         s_01D_MIN="#"+stationName+"_01D_MIN";
         s_02G_H="#"+stationName+"_02G_H";
         s_02G_MIN="#"+stationName+"_02G_MIN";
         s_02D_H="#"+stationName+"_02D_H";
         s_02D_MIN="#"+stationName+"_02D_MIN";
         fstHTime=tide.FSTHIGHWIDETIME;
         fstLTime=tide.FSTLOWWIDETIME;
         scdHTime=tide.SCDHIGHWIDETIME;
         scdLTime=tide.SCDLOWWIDETIME;
         if(fstHTime.indexOf("-")==-1)
          {
           var arr = fstHTime.split(":");
           $(s_01G_H).val(arr[0]);
           $(s_01G_MIN).val(arr[1]);
          }
        else
         {
               $(s_01G_H).val("-");
           $(s_01G_MIN).val("-");
         }
         if(fstLTime.indexOf("-")==-1)
          {
           var arr = fstLTime.split(":");
           $(s_01D_H).val(arr[0]);
           $(s_01D_MIN).val(arr[1]);
          }
         else
         {
              $(s_01D_H).val("-");
            $(s_01D_MIN).val("-");
         }
         if(scdHTime.indexOf("-")==-1)
          {
           var arr = scdHTime.split(":");
           $(s_02G_H).val(arr[0]);
           $(s_02G_MIN).val(arr[1]);
          }
          else
         {
              $(s_02G_H).val("-");
            $(s_02G_MIN).val("-");
         }
         if(scdLTime.indexOf("-")==-1)
          {
           var arr = scdLTime.split(":");
           $(s_02D_H).val(arr[0]);
           $(s_02D_MIN).val(arr[1]);
          }
          else
         {
              $(s_02D_H).val("-");
            $(s_02D_MIN).val("-");
         }
        }


       function getTideDataWithParams(stationsStr,dayCountInt,procTideDataCallBack,date1){
           $.ajax({
                    type: "POST",
                    url: "Ajax/GetTideData.ashx",
                    data: { stations: stationsStr,dayCount:dayCountInt,date:date1 },
                    dataType: "json",
                    success: function (resp) {
                        procTideDataCallBack(resp);
                    },
                    error: function () {
                        //alert("潮汐数据加载失败");
                    }
                });

        }

      function getSD7City24HTideData(date1)
       {
        var stationsStr = "'101wmt','104rzh','109wh','111zfd','114wfg','119hhg','125bzg'";
        var procCallBack = function(data){
              $(data).each(function (i, value) {
                  proc7City24TideData(value);
              });
          }
          // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
          getTideDataWithParams(stationsStr, 1, procCallBack,date1);
       }

      function getQD24HTideData(date1)
       {
          var stationsStr = "'101wmt'";
          var procCallBack = function(data){
              $(data).each(function (i, value) {
                  procQD24TideData(value);
              });
          }
           //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
          getTideDataWithParams(stationsStr, 1, procCallBack,date1);
       }

      function getMZZ72HTideData(date1)
       {
          var stationsStr = "'117xdh'";
          var procCallBack = function(data){
              $(data).each(function (i, value) {
                  procTideData("MZZ", i, value);
              });
          }
            //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
          getTideDataWithParams(stationsStr, 3, procCallBack,date1);
       }

      function getNBYT72HTideData(date1)
       {
          var stationsStr = "'128cfd'";
          var procCallBack = function(data){
              $(data).each(function (i, value) {
                  procTideData("NBYT", i, value);
              });
          }
          //  var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
          getTideDataWithParams(stationsStr, 3, procCallBack,date1);
       }

     function getWFG24HTideData(date1)
       {
          var stationsStr = "'114wfg'";
          var procCallBack = function(data){
                $(data).each(function (i, value) {
                        $('#WFG_GCCS_01').val(value.FSTHIGHWIDETIME.replace(":",""));
                        $('#WFG_GCCS_02').val(value.SCDHIGHWIDETIME.replace(":",""));
                        $('#WFG_GCCG_01').val(value.FSTHIGHWIDEHEIGHT);
                        $('#WFG_GCCG_02').val(value.SCDHIGHWIDEHEIGHT);
                        $('#WFG_DCCS_01').val(value.FSTLOWWIDETIME.replace(":",""));
                        $('#WFG_DCCS_02').val(value.SCDLOWWIDETIME.replace(":",""));
                        $('#WFG_DCCG_01').val(value.FSTLOWWIDEHEIGHT);
                        $('#WFG_DCCG_02').val(value.SCDLOWWIDEHEIGHT);
                });
          }
          // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
          getTideDataWithParams(stationsStr, 1, procCallBack,date1);
       }

      function getQDBathing24HTideData(date1)
       {
          var stationsStr = "'101wmt','102xmd'";
          var procCallBack = function(data){
            $(data).each(function (i, value) {
                var StationName = "";
                if (value.STATION == "101wmt")
                    StationName = "WMT";
                else if(value.STATION == "102xmd")
                    StationName = "XMD";
                if (i%2 == 0) {
                    var Station_01GCTime = value.SCDHIGHWIDETIME;
                    if (Station_01GCTime.indexOf("-") == -1) {
                        var arr = Station_01GCTime.split(":");

                        $("#" + StationName + "_01GC_H").val(arr[0]);
                        $("#" + StationName + "_01GC_MIN").val(arr[1]);
                    }
                    else {
                        $("#" + StationName + "_01GC_H").val("-");
                        $("#" + StationName + "_01GC_MIN").val("-");
                    }
                    var Station_01DCTime = value.SCDLOWWIDETIME;
                    if(Station_01DCTime.indexOf("-") == -1)
                    {
                        var arr = Station_01DCTime.split(":");
                        $("#"+StationName+"_01DC_H").val(arr[0]);
                        $("#"+StationName+"_01DC_MIN").val(arr[1]);
                    }
                     else {
                        $("#"+StationName+"_01DC_H").val("-");
                        $("#"+StationName+"_01DC_MIN").val("-");
                    }

                }
                else if (i%2 == 1) {
                    var Station_02GCTime = value.FSTHIGHWIDETIME;
                    if(Station_02GCTime.indexOf("-") == -1)
                    {
                        var arr = Station_02GCTime.split(":");
                        $("#"+StationName+"_02GC_H").val(arr[0]);
                        $("#"+StationName+"_02GC_MIN").val(arr[1]);
                    }
                     else {
                       $("#"+StationName+"_02GC_H").val("-");
                        $("#"+StationName+"_02GC_MIN").val("-");
                    }
                    var Station_02DCTime = value.FSTLOWWIDETIME;
                    if(Station_02DCTime.indexOf("-") == -1)
                    {
                        var arr = Station_02DCTime.split(":");
                        $("#"+StationName+"_02DC_H").val(arr[0]);
                        $("#"+StationName+"_02DC_MIN").val(arr[1]);
                    }else {
                       $("#"+StationName+"_02DC_H").val("-");
                        $("#"+StationName+"_02DC_MIN").val("-");
                    }

                }
            });
          }
           // var  = new Date($("#tianbaoriqi").datebox("getValue"));
          getTideDataWithParams(stationsStr, 2, procCallBack,date1);
       }

    function getQDCoast48HTideData(date1)
       {
          var stationsStr = "'101wmt'";
          var procCallBack = function(data){
            $(data).each(function (i, value) {
                var fstHTime=value.FSTHIGHWIDETIME;
                var fstLTime=value.FSTLOWWIDETIME;
                var scdHTime=value.SCDHIGHWIDETIME;
                var scdLTime=value.SCDLOWWIDETIME;
                if(fstHTime.indexOf("-")==-1)
                {
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
                if(scdHTime.indexOf("-")==-1)
                {
                    var arr = scdHTime.split(":");
                    $("#02GC_H20").val(arr[0]);
                    $("#02GC_MIN20").val(arr[1]);
                    $("#02GC_CM20").val(value.SCDHIGHWIDEHEIGHT);
                }
                else
                {
                  $("#02GC_H20").val("-");
                    $("#02GC_MIN20").val("-");
                    $("#02GC_CM20").val(value.SCDHIGHWIDEHEIGHT);
}
                if(scdLTime.indexOf("-")==-1)
                {
                    var arr = scdLTime.split(":");
                    $("#02DC_H20").val(arr[0]);
                    $("#02DC_MIN20").val(arr[1]);
                    $("#02DC_CM20").val(value.SCDLOWWIDEHEIGHT);
                }
                else
                {  $("#02DC_H20").val("-");
                    $("#02DC_MIN20").val("-");
                    $("#02DC_CM20").val(value.SCDLOWWIDEHEIGHT);
                    }


            });
          }
         //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
          getTideDataWithParams(stationsStr, 2, procCallBack,date1);
       }

    function getWH48HTideData(date1)
       {
          var stationsStr = "'109wh','107sdo','106wdn','108cst','105nhd'";
          var procCallBack = function(data){
            $(data).each(function (i, value) {
                var StationName = "";
                if (value.STATION == "109wh")
                    StationName = "WH";
                else if(value.STATION == "107sdo")
                    StationName = "SD";
                else if(value.STATION == "106wdn")
                    StationName = "WD";
                else if(value.STATION == "108cst")
                    StationName = "CST";
                else if(value.STATION == "105nhd")
                    StationName = "RS";
                if (i % 2 == 0) i = 1;
                else i = 2;
                if (StationName == "RS" && i == 1) {
                    return true;
                }
                var S_01G_SJ = "#"+StationName+"_01G_SJ_0"+i;
                var S_01G_CW = "#"+StationName+"_01G_CW_0"+i;
                var S_01D_SJ = "#"+StationName+"_01D_SJ_0"+i;
                var S_01D_CW = "#"+StationName+"_01D_CW_0"+i;
                var S_02G_SJ = "#"+StationName+"_02G_SJ_0"+i;
                var S_02G_CW = "#"+StationName+"_02G_CW_0"+i;
                var S_02D_SJ = "#"+StationName+"_02D_SJ_0"+i;
                var S_02D_CW = "#"+StationName+"_02D_CW_0"+i;
                $(S_01G_SJ).val(value.FSTHIGHWIDETIME.replace(":",""));
                $(S_01G_CW).val(value.FSTHIGHWIDEHEIGHT);
                $(S_01D_SJ).val(value.FSTLOWWIDETIME.replace(":",""));
                $(S_01D_CW).val(value.FSTLOWWIDEHEIGHT);
                $(S_02G_SJ).val(value.SCDHIGHWIDETIME.replace(":",""));
                $(S_02G_CW).val(value.SCDHIGHWIDEHEIGHT);
                $(S_02D_SJ).val(value.SCDLOWWIDETIME.replace(":",""));
                $(S_02D_CW).val(value.SCDLOWWIDEHEIGHT);
            });
          }
         //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
          getTideDataWithParams(stationsStr, 2, procCallBack,date1);
       }


      //东营埕岛-未来三天高/低潮预报
    function getDYTide(date1) {
        var stationsStr = "'136dyg'";
        var procCallBack = function (data) {
            $(data).each(function (j, value) {
                i = j + 1;
                $('#DY_01G_CS_0'+i).val(value.FSTHIGHWIDETIME.replace(":", ""));
                $('#DY_01D_CS_0'+i).val(value.FSTLOWWIDETIME.replace(":", ""));
                $('#DY_01G_CG_0'+i).val(value.FSTHIGHWIDEHEIGHT);
                $('#DY_01D_CG_0'+i).val(value.FSTLOWWIDEHEIGHT);

                $('#DY_02G_CS_0'+i).val(value.SCDHIGHWIDETIME.replace(":", ""));
                $('#DY_02D_CS_0'+i).val(value.SCDLOWWIDETIME.replace(":", ""));
                $('#DY_02G_CG_0'+i).val(value.SCDHIGHWIDEHEIGHT);
                $('#DY_02D_CG_0'+i).val(value.SCDLOWWIDEHEIGHT);
            });
        }
        // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
          getTideDataWithParams(stationsStr, 3, procCallBack,date1);
     
    }

    function getPredicTideData(date1)
    {
//      <!--表单07.山东省近海七市24小时潮汐预报-->
        getSD7City24HTideData(date1);
//      <!--表单08.青岛24小时潮位预报-->
        getQD24HTideData(date1);
//      <!--表单10.明泽闸潮位预报-->
        getMZZ72HTideData(date1);
//      <!--表单12.南堡油田海域潮汐预报-->
    
        getNBYT72HTideData(date1);

//      <!--表单16.潍坊港24小时潮汐预报-->
        getWFG24HTideData(date1);
//      <!--表单18.青岛海水浴场24小时潮汐预报-->
       getQDBathing24HTideData(date1);
//      <!--表单20.青岛沿岸48小时潮汐预报-->
        getQDCoast48HTideData(date1);
//      <!--表单22.潮汐预报-->
        getWH48HTideData(date1);

        //下午二十二、 东营埕岛-未来三天高/低潮预报
        getDYTide(date1);
    }
    //$(function () {
    //    getPredicTideData();
    //}); 

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
            //var d = new Date(date1);
            var d = new Date($("#tianbaoriqi").datebox("getValue"));//控件时间 格式：2015-04-28
            var date=d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();  
            //getPredicTideData(date1);
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
                                    $(this).css("color","red");
                                }
                            });
                            
                        }
                    }
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
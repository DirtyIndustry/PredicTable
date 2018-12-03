<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OceanRanchList.aspx.cs" Inherits="PredicTable.OceanRanchList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>海洋牧场</title>
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
    <style type ="text/css">
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
        .stdtable tbody tr td:first-child {
            border-left: 1px solid #ddd;
            width: 8%;
        }
    </style>
</head>
<body>
    <iframe width="0" height="0" src="SessionKeeper.asp"></iframe>

    <script>
        var cx_arry = new Array([2]);
        var sw_arry = new Array([3]);
        var fl_arry = new Array([1]);
         var type = "<%=Session["type"]%>";
       // var type = "<%=type%>";
           var makewordtime = "pm";
        //var type = "cx";
        function quanxian(type, date) {
            if (getdatenow() == date) {
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
    </script>
    <form id="form2" runat="server">
        <div>
            <div id="contentwrapper" class="contentwrapper">
               
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
                        <input type="button" id="setall" onclick="alldlg_Submit()" class="stdbtn" value="保存所有" />
                       
                        <br />
                    </div>
                </div>
                <!--表单信息-->

                <div class="dlgs" id="ddlg_01"  style="height: 390px; padding: 10px;">
                    <div style="height: 10px"></div>
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="line-height: 45px;">
                                <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="7">一、海洋牧场-海浪预报</td>
                            </tr>
                            <tr>
                                <th class="head0" rowspan="3">海洋牧场长名称</th>
                                <th class="head0" colspan="6">最大有效波高</th>
                            </tr>
                            <tr>
                                <td colspan="2">24小时</td>
                                <td colspan="2">48小时</td>
                                <td colspan="2">72小时</td>
                            </tr>
                            <tr>
                                <td>白天</td>
                                <td>夜晚</td>
                                <td>白天</td>
                                <td>夜晚</td>
                                <td>白天</td>
                                <td>夜晚</td>
                            </tr>
                        </thead>
                        
                        <tbody  class="textStyle" style="text-align: center">
                            <tr>
                                <td>长青海洋牧场<input type="hidden" id="WAVE_CQ_FORCASTDATE"/></td>
                                <td> <input id="WAVE_CQ_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>荣成烟墩角游钓型海洋牧场<input type="hidden" id="WAVE_RC_FORCASTDATE"/></td>
                                <td> <input id="WAVE_RC_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>西霞口集团国家级海洋牧场<input type="hidden" id="WAVE_XXK_FORCASTDATE"/></td>
                                <td> <input id="WAVE_XXK_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_72_N" type="text" maxlength="20" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(1)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(1)" value="提交" />

                </div>
                <!--表单01.一、海洋牧场-海浪预报-->

                <div class="dlgs" id="ddlg_02" style="height: 390px; padding: 10px;">
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
                                <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="33">二、海洋牧场-潮汐预报</td>
                            </tr>
                            <tr>
                                <th class="head0" rowspan="2">海洋牧场长名称</th>
                                <th class="head0" colspan="24">24小时潮位</th>
                                <th class="head0" colspan="2">第一高潮</th>
                                <th class="head0" colspan="2">第二高潮</th>
                                <th class="head0" colspan="2">第一低潮</th>
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
                                <td>威海长青国家级海洋牧场<input type="hidden" id="TIDE_CQ_FORCASTDATE"/></td>
                                <td> <input id="TIDE_CQ_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>荣成烟墩角游钓型海洋牧场<input type="hidden" id="TIDE_RC_FORCASTDATE"/></td>
                                <td> <input id="TIDE_RC_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>西霞口集团国家级海洋牧场<input type="hidden" id="TIDE_XXK_FORCASTDATE"/></td>
                                <td> <input id="TIDE_XXK_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(2)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(2)" value="提交" />

                </div>
                <!--表单02.二、海洋牧场-潮汐预报-->

                <div class="dlgs" id="ddlg_03" style="height: 390px; padding: 10px;">
                    <div style="height: 10px"></div>
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="line-height: 45px;">
                                <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="4">三、海洋牧场-海温预报</td>
                            </tr>
                            <tr>
                                <th class="head0" rowspan="2">海洋牧场长名称</th>
                                <th class="head0" colspan="3">海温平均值</th>
                            </tr>
                            <tr>
                                <td>24小时</td>
                                <td>48小时</td>
                                <td>72小时</td>
                            </tr>
                        </thead>
                        
                        <tbody class="textStyle" style="text-align: center">
                            <tr>
                                <td>长青海洋牧场<input type="hidden" id="TEMP_CQ_FORCASTDATE"/></td>
                                <td> <input id="TEMP_CQ_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_CQ_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_CQ_72" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>荣成烟墩角游钓型海洋牧场<input type="hidden" id="TEMP_RC_FORCASTDATE"/></td>
                                <td> <input id="TEMP_RC_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_RC_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_RC_72" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>西霞口集团国家级海洋牧场<input type="hidden" id="TEMP_XXK_FORCASTDATE"/></td>
                                <td> <input id="TEMP_XXK_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_XXK_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_XXK_72" type="text" maxlength="20" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(3)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(3)" value="提交" />

                </div>
                <!--表单03.三、海洋牧场-海温预报-->

                <div id="dlg_23" class="dlgs "  style="padding: 10px;">
                    <div class="contenttitle2">
                        <h3 id="hj2">填报信息</h3>
                    </div>
                    <div id="tbxx" style="font-size: 12px; font-weight: 300">
                        <div style="margin-bottom: 5px" />
                        &nbsp;&nbsp;&nbsp;发布单位： 
                        <input id="Fabudanwei" type="text" style="width: 200px" />
                        &nbsp;&nbsp;
                            电话：<input id="Tel" type="text" style="width: 200px" />
                        &nbsp;&nbsp;
                    </div>
                    <div style="margin-bottom: 5px">
                        &nbsp;&nbsp;&nbsp;发布时间：
                        <input id="Fabushijian" type="text" style="width: 200px" />
                        &nbsp;&nbsp;传真：
                        <input id="Chuanzhen" type="text" style="width: 200px" />
                        &nbsp;&nbsp;
                    </div>
                    海浪预报员：<%--<input id="Hailang" type="text" style="width: 200px" />--%>
                    <select id="Hailang" class="uniformselect" style="width: 100%; height: 25px;">
                        <option value="">请选择</option>
                    </select>
                    &nbsp;&nbsp;
                        潮汐预报员：<%--<input id="Chaoxi" type="text" style="width: 200px" />--%>
                    <select id="Chaoxi" class="uniformselect" style="width: 100%; height: 25px;">
                        <option value="">请选择</option>
                    </select>
                    &nbsp;&nbsp;
                        水温预报员：<%--<input id="Shuiwen" type="text" style="width: 200px" />--%>
                    <select id="Shuiwen" class="uniformselect" style="width: 100%; height: 25px;">
                        <option value="">请选择</option>
                    </select>
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
                                     var reportername =  resjson["rows"][j]["reportername"];
                                     if (yubaoid == "FBHBYB") {
                                         strs1 += "<option value='" + reportercode + "'>" + reportercode + "</option>";
                                         
                                         $("#Chaoxi").html(strs1);
                                         $("#Shuiwen").html(strs1);
                                         $("#uniform-Chaoxi span").text(reportercode);
                                         $("#uniform-Shuiwen span").text(reportercode);
                                         $("#uniform-Chaoxi span").attr("code",reportercode);
                                          $("#uniform-Shuiwen span").attr("code",reportercode);
                                     }else  if (yubaoid == "HLYB") {
                                         strs2 += "<option value='" + reportercode + "'>" + reportercode + "</option>";
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
                        $("#uniform-Hailang span").attr("code",code);
                    });
                    $("#Chaoxi").change(function () {
                        var code = $("#Chaoxi option:selected").val();
                        $("#uniform-Chaoxi span").attr("code",code);
                    });
                    $("#Shuiwen").change(function () {
                        var code = $("#Shuiwen option:selected").val();
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
                <br />
            </div>
        </div>

    </form>
    <script>
        
        var date = new Date();
        var num = 0;
        var nqyname;
        //↓表单提交(表单编号)
        function dlg_Submit(id) {
            switch (id) {
                case 1: submit_1(id); break;//表单1提交 一、海洋牧场-海浪预报
                case 2: submit_2(id); break;//表单2提交 二、海洋牧场-潮汐预报
                case 3: submit_3(id); break;//表单3提交 三、海洋牧场-海温预报
                case 23: submit_23(id); break; //填报信息提交
                default:
            }
        }

        //保存所有
        function alldlg_Submit() {
            submit_1(1); //表单1提交 一、海洋牧场-海浪预报
            submit_2(2); //表单2提交 二、海洋牧场-潮汐预报
            submit_3(3); //表单3提交 三、海洋牧场-海温预报
            submit_23(23); //填报信息提交
        }

        //表单数据拼接 从左至右 从上至下
        {
            //一、海洋牧场-海浪预报
            function submit_1(id) {

                var str_data = "";
                var d = new Date();

                str_data += $("#WAVE_CQ_FORCASTDATE").val() + ",";
                str_data += $("#WAVE_CQ_24_D").val() + ",";
                str_data += $("#WAVE_CQ_24_N").val() + ",";
                str_data += $("#WAVE_CQ_48_D").val() + ",";
                str_data += $("#WAVE_CQ_48_N").val() + ",";
                str_data += $("#WAVE_CQ_72_D").val() + ",";
                str_data += $("#WAVE_CQ_72_N").val() + ",";

                str_data += $("#WAVE_RC_FORCASTDATE").val() + ",";
                str_data += $("#WAVE_RC_24_D").val() + ",";
                str_data += $("#WAVE_RC_24_N").val() + ",";
                str_data += $("#WAVE_RC_48_D").val() + ",";
                str_data += $("#WAVE_RC_48_N").val() + ",";
                str_data += $("#WAVE_RC_72_D").val() + ",";
                str_data += $("#WAVE_RC_72_N").val() + ",";

                str_data += $("#WAVE_XXK_FORCASTDATE").val() + ",";
                str_data += $("#WAVE_XXK_24_D").val() + ",";
                str_data += $("#WAVE_XXK_24_N").val() + ",";
                str_data += $("#WAVE_XXK_48_D").val() + ",";
                str_data += $("#WAVE_XXK_48_N").val() + ",";
                str_data += $("#WAVE_XXK_72_D").val() + ",";
                str_data += $("#WAVE_XXK_72_N").val() + ",";
               
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //二、海洋牧场-潮汐预报
            function submit_2(id) {

                var str_data = "";
                var d = new Date();
                var nqyname = "";
                var countH = 24;

                for (var j = 0; j < 3; j++) {

                    switch (j) {
                        case 0: nqyname = "CQ"; break;
                        case 1: nqyname = "RC"; break;
                        case 2: nqyname = "XXK"; break;
                        default:
                    }

                    for (var i = 0; i < countH; i++) {
                        if (i < 10) {
                            str_data += $("#TIDE_" + nqyname + "_H0" + i).val() + ",";
                        }
                        else {
                            str_data += $("#TIDE_" + nqyname + "_H" + i).val() + ",";
                        }
                    }
                    str_data += $("#TIDE_" + nqyname + "_FIRSTHTIME").val() + ",";
                    str_data += $("#TIDE_" + nqyname + "_FIRSTHHEIGHT").val() + ",";

                    str_data += $("#TIDE_" + nqyname + "_SECONDHTIME").val() + ",";
                    str_data += $("#TIDE_" + nqyname + "_SECONDHHEIGHT").val() + ",";

                    str_data += $("#TIDE_" + nqyname + "_FIRSTLTIME").val() + ",";
                    str_data += $("#TIDE_" + nqyname + "_FIRSTLHEIGHT").val() + ",";

                    str_data += $("#TIDE_" + nqyname + "_SECONDLTIME").val() + ",";
                    str_data += $("#TIDE_" + nqyname + "_SECONDLHEIGHT").val() + ",";

                    str_data += $("#TIDE_" + nqyname + "_FORCASTDATE").val() + ",";

                }

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //三、海洋牧场-海温预报
            function submit_3(id) {
                var str_data = "";
                var d = new Date();
               
                str_data += $("#TEMP_CQ_FORCASTDATE").val() + ",";
                str_data += $("#TEMP_CQ_24").val() + ",";
                str_data += $("#TEMP_CQ_48").val() + ",";
                str_data += $("#TEMP_CQ_72").val() + ",";

                str_data += $("#TEMP_RC_FORCASTDATE").val() + ",";
                str_data += $("#TEMP_RC_24").val() + ",";
                str_data += $("#TEMP_RC_48").val() + ",";
                str_data += $("#TEMP_RC_72").val() + ",";

                str_data += $("#TEMP_XXK_FORCASTDATE").val() + ",";
                str_data += $("#TEMP_XXK_24").val() + ",";
                str_data += $("#TEMP_XXK_48").val() + ",";
                str_data += $("#TEMP_XXK_72").val() + ",";

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //填报信息
            function submit_23(id) {
                var str_data = "";
                str_data += $("#select_hour").val() + ",";
                str_data += $("#Fabudanwei").val() + ",";
                str_data += $("#Tel").val() + ",";
                str_data += $("#Chuanzhen").val() + ",";
                str_data += $('#uniform-Hailang span').attr("code") + ",";
                str_data += $('#uniform-Chaoxi span').attr("code") + ",";
                str_data += $('#uniform-Shuiwen span').attr("code") + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(id, data);
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
                    url: "/Ajax/OceanRanchList.ashx?method=SubmitOceanListData&type=" + types + "&date=" + date,
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
                $("input[id*='TIDE']").removeAttr("disabled");
            }
            //潮汐不可编辑
            function cx_disabled() {
                $("input[id*='TIDE']").attr("disabled", "disabled");

            }
            //水温可编辑
            function sw_isabled() {
                $("input[id*='TEMP']").removeAttr("disabled");
            }
            //水温不可编辑
            function sw_disabled() {
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
            $("#select_hour").val(15);
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
                url: "/Ajax/OceanRanchList.ashx?method=GetOceanListData&date=" + dates,
                cache:false,
                beforeSend: function () {
                    $('#w').window('open');
                    $("#btn_select").attr({ disabled: "disabled" });
                },
                success: function (result) {
                    var resjson = JSON.parse(result);
                    for (var j = 0; j < resjson.length; j++) {
                        switch (resjson[j].type) {
                            case "t1": gettable01(resjson[j].children, date1); dlgclose("1"); break;
                            case "t2": gettable02(resjson[j].children, date1); dlgclose("2"); break;
                            case "t3": gettable03(resjson[j].children, date1); dlgclose("3"); break;
                            case "t23": gettable23(resjson[j].children, date1); dlgclose("23"); break;
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

        //表单01数据 一、洋牧场-海浪预报
            function gettable01(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].OCEANRANCHNAME) {
                        case "长青海洋牧场": nqyname = "CQ"; break;
                        case "荣成烟墩角游钓型海洋牧场": nqyname = "RC"; break;
                        case "西霞口集团国家级海洋牧场": nqyname = "XXK"; break;
                        default:
                    }
                    $("#WAVE_" + nqyname + "_24_D").val(resjson[i].WAVE24HDAY);
                    $("#WAVE_" + nqyname + "_24_N").val(resjson[i].WAVE24HNEIGHT);
                    $("#WAVE_" + nqyname + "_48_D").val(resjson[i].WAVE48HDAY);
                    $("#WAVE_" + nqyname + "_48_N").val(resjson[i].WAVE48HNEIGHT);
                    $("#WAVE_" + nqyname + "_72_D").val(resjson[i].WAVE72HDAY);
                    $("#WAVE_" + nqyname + "_72_N").val(resjson[i].WAVE72HNEIGHT);

                    $("#WAVE_" + nqyname + "_FORCASTDATE").val(resjson[i].FORECASTDATE);
                    //$("#WAVE_RC_FORCASTDATE").val(resjson[i].FORECASTDATE);
                    //$("#WAVE_XXK_FORCASTDATE").val(resjson[i].FORECASTDATE);
                }
            }
        //表单02数据 二、海洋牧场-潮汐预报
            function gettable02(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    var nqyname = "";
                    switch (resjson[i].OCEANRANCHNAME) {
                        case "威海长青国家级海洋牧场": nqyname = "CQ"; break;
                        case "荣成烟墩角游钓型海洋牧场": nqyname = "RC"; break;
                        case "西霞口集团国家级海洋牧场": nqyname = "XXK"; break;
                        default:
                    }

                    var value="";
                    for (var j = 0; j < 24; j++) {
                        if (j < 10) {
                            value="TIDE24H0"+j;
                            $("#TIDE_" + nqyname + "_H0" + j).val(resjson[i][value]);
                        }
                        else {
                            value="TIDE24H"+j;
                            $("#TIDE_" + nqyname + "_H" + j).val(resjson[i][value]);
                        }
                    }
                    $("#TIDE_" + nqyname + "_FIRSTHTIME").val(resjson[i].TIDEFIRSTHTIME);
                    $("#TIDE_" + nqyname + "_FIRSTHHEIGHT").val(resjson[i].TIDEFIRSTHHEIGHT);

                    $("#TIDE_" + nqyname + "_SECONDHTIME").val(resjson[i].TIDESECONDHTIME);
                    $("#TIDE_" + nqyname + "_SECONDHHEIGHT").val(resjson[i].TIDESECONDHHEIGHT);
                    
                    $("#TIDE_" + nqyname + "_FIRSTLTIME").val(resjson[i].TIDEFIRSTLTIME);
                    $("#TIDE_" + nqyname + "_FIRSTLHEIGHT").val(resjson[i].TIDEFIRSTLHEIGHT);

                    $("#TIDE_" + nqyname + "_SECONDLTIME").val(resjson[i].TIDESECONDLTIME);
                    $("#TIDE_" + nqyname + "_SECONDLHEIGHT").val(resjson[i].TIDESECONDLHEIGHT);

                    $("#TIDE_" + nqyname + "_FORCASTDATE").val(resjson[i].FORECASTDATE);
                }
            }
        //表单03数据 三、海洋牧场-海温预报
            function gettable03(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].OCEANRANCHNAME) {
                        case "长青海洋牧场": nqyname = "CQ"; break;
                        case "荣成烟墩角游钓型海洋牧场": nqyname = "RC"; break;
                        case "西霞口集团国家级海洋牧场": nqyname = "XXK"; break;
                        default:
                    }   
                    $("#TEMP_" + nqyname + "_24").val(resjson[i].TEMP24H);
                    $("#TEMP_" + nqyname + "_48").val(resjson[i].TEMP48H);
                    $("#TEMP_" + nqyname + "_72").val(resjson[i].TEMP72H);

                    $("#TEMP_" + nqyname + "_FORCASTDATE").val(resjson[i].FORECASTDATE);
                    //$("#TEMP_RC_FORCASTDATE").val(resjson[i].FORECASTDATE);
                    //$("#TEMP_XXK_FORCASTDATE").val(resjson[i].FORECASTDATE);
                }
            }

            //表单23 填报数据
            function gettable23(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#Fabudanwei").val(resjson[i].fb);
                    $("#Tel").val(resjson[i].dh);
                    $("#Chuanzhen").val(resjson[i].cz);
                    if (resjson[i].cx != "" && resjson[i].cx != null) {
                        $("#uniform-Chaoxi span").text(resjson[i].cx);
                        $("#uniform-Chaoxi span").attr("code", resjson[i].cx);
                        $("#iChaoxi").val(resjson[i].cx);
                    }
                    if (resjson[i].sw != "" && resjson[i].sw != null) {
                        $("#uniform-Shuiwen span").text(resjson[i].sw);
                        $("#uniform-Shuiwen span").attr("code", resjson[i].sw);
                        $("#iShuiwen").val(resjson[i].sw);
                    }
                    if (resjson[i].hl != "" && resjson[i].hl != null) {
                        $("#uniform-Hailang span").text(resjson[i].hl);
                        $("#uniform-Hailang span").attr("code", resjson[i].hl);
                        $("#iHailang").val(resjson[i].hl);
                    }
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
            var date=d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();  
            getPredicTideData(date1);
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


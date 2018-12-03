<%-- 
    变更记录1/**/
变更时间：10180710    
变更内容：下午四潮汐取消，取数从下午三青岛24小时预报取
          下午五取消，取数从下午三青岛48小时预报取
          下午十二潮汐预报取消，从下午三潍坊24小时预报取数
          下午十四的金沙滩预报取消，从下午三三天预报中取数
          下午十八威海取消，从下午三威海24小时预报取数
变更人员：Yuy   
      
    变更记录2/**/
变更时间：20180801    
变更内容：下午三、下午五、下午七、下午十二、下午十六、下午十八、下午二十、下午二十二、下午二十五、下午二十八、下午三十一、下午三十四、下午三十七、下午四十
            取数规则
变更人员：Yuy    
    变更记录3:
变更时间：2018.09.04
变更内容：海洋牧场 下午19 20 21 新增加7个预报海域     
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PMTableList.aspx.cs" Inherits="PredicTable.PMTableList" %>

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
    <%--<iframe width="0" height="0" src="SessionKeeper.asp"></iframe>--%>

    <script>
        //var cx_arry = new Array(2,4,  7, 10, 12, 16, 18, 20, 22,42,43,48,54,57,60,63,66,69,72,53);
        var cx_arry = new Array(2,4);
        var sw_arry = new Array(1, 3, 6,11, 17, 19, 21,24,49,56,59,62,65,68,71,74,53);
        var fl_arry = new Array(1,3, 5, 6, 8, 9, 11, 13, 14,15,17,19,21,26,35,41,43,47,55,58,61,64,67,70,73,53);
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
                  quanxian(type, getdatenow());
                  setVisiable();
                  var date = new Date();
                  setVisiable_21(date);//add by xp 2018-10-9 冬季的时候隐藏21号预报单


                  $("#btn_show").click(function () {

                      if ($("#btn_show").val() == "显示所有") {
                          all_show();
                          $("#btn_show").val("显示可编辑");
                      } else if ($("#btn_show").val() == "显示可编辑") {
                          switch (type) {
                              case "cx": all_hide(); show_bytype(cx_arry); break;//潮汐能填写
                              case "fl": all_hide(); show_bytype(fl_arry); break;//风浪能填写
                              case "sw": all_hide(); show_bytype(sw_arry); break;//水温能填写
                              default: break;// 都不能填写 
                          }
                          $("#btn_show").val("显示所有");
                      }
                      var date = new Date($("#tianbaoriqi").datebox("getValue"));//add by xp 2018-10-9 冬季的时候隐藏21号预报单
                      if (!isVisiable_21(date)) {
                          setVisiable_21(date);//add by xp 2018-10-9 冬季的时候隐藏21号预报单
                      }
                  });

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

      
          //edit by lian 2018-10-9 冬季的时候隐藏21号预报单
             
              function setVisiable_21(date) {
                  if (isVisiable_21(date)) {
                      $(moban_21).css("display", "");//显示
                      if (type != "cx") {
                          $(ddlg_17).css("display", "");
                      } else {
                          $(ddlg_17).css("display", "none");
                          $(ddlg_18).css("display", "none");
                      }

                  } else {
                      $(moban_21).css("display", "none");
                      $(ddlg_17).css("display", "none");
                      $(ddlg_18).css("display", "none");
                  }
              }
          //add by xp 2018-10-9 是否隐藏21号预报单
          function isVisiable_21(date) {
              
              var startDate = new Date(new Date(date).getFullYear() + "-06-30 00:00:00");
              var endDate = new Date(new Date(date).getFullYear() + "-09-29 23:59:59");

              if (new Date(date) >= startDate && new Date(date) <= endDate) {
                  return true;
               }
              return false;
          }
        //显示所有要分权限
          function all_show() {
              var str = "";
              if (type == "fl") {
                  for (var i = 1; i <= 100; i++) {
                      if (i < 10) {
                          str = "#ddlg_0" + i;
                      } else {
                          str = "#ddlg_" + i;
                      }
                      $(str).css("display", "");
                      $("#lx_" + i).css("display", "");
                  }
              } else {
                  var array = new Array(1,3, 5, 6, 8, 9, 11, 13, 14,15,17,19,21,24,26,35,41,43,47,49,53,55,56,58,59,61,62,64,65,67,68,70,71,73,74);
                  for (var j = 0 ; j < array.length; j++) {
                      if (array[j] < 10) {
                          str = "#ddlg_0" + array[j];
                      } else {
                          str = "#ddlg_" + array[j];
                      }
                      $(str).css("display", "");
                      $("#lx_" + array[j]).css("display", "");
                  }
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
                <!--表单类型-->
                <div>
                    <%--  <div class="contenttitle2">
                            <h3 id="tx">表单类型</h3>
                        </div>--%>
                    <div style="position: fixed; top: 0px; left: 20px; z-index: 2; display: none">
                        <ul id="leixing1">
                            <li id="xwbd" style="clear: both; border-color: #fb9337; background-color: #fb9337; color: white; font-weight: bold">下午预报表单</li>
                            <li id="lx_5" onclick="click_scroll('ddlg_05')">一、各海区24小时海浪预报</li>
                            <li id="lx_6" onclick="click_scroll('ddlg_06')">二、山东近海七市3天海浪、水温预报</li>
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
                        <input type="button" id="ReleasetableAll" onclick="All_Releasetable()" class="stdbtn" value="发布下午表单" />
                        <br />
                    </div>
                </div>
                <%--<a href="javascript:void(0)" class="easyui-linkbutton" onclick="$('#dlg').dialog('open')">Open</a>
		        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="$('#dlg').dialog('close')">Close</a>--%>

                <!--表单05. 下午一、各海区24小时海浪预报-->
                <div class="dlgs" id="ddlg_05" <%--id="dlg_05" class="easyui-dialog" title="下午一、各海区24小时海浪预报" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 285px; padding: 10px">

                    <table style="border: 1px solid #ddd; width: 100%">

                        <tr style="line-height: 45px;">
                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="4">下午一、各海区24小时海浪预报</td>
                        </tr>
                        <tbody>
                            <tr style="line-height: 45px">
                                <td>渤海</td>
                                <td>有<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="BH_GDF_401" type="text" />米</td>
                                <td>到<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="BH_GDE_401" type="text" />米</td>
                                <td>的<input id="BH_HL_401" type="text" /></td>
                            </tr>
                            <tr style="line-height: 45px">
                                <td>黄海北部</td>
                                <td>有<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHBB_GDF_401" type="text" />米</td>
                                <td>到<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHBB_GDE_401" type="text" />米</td>
                                <td>的<input id="HHBB_HL_401" type="text" /></td>
                            </tr>
                            <tr style="line-height: 45px">
                                <td>黄海中部</td>
                                <td>有<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHZB_GDF_401" type="text" />米</td>
                                <td>到<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHZB_GDE_401" type="text" />米</td>
                                <td>的<input id="HHZB_HL_401" type="text" /></td>
                            </tr>
                            <tr style="line-height: 45px">
                                <td>黄海南部</td>
                                <td>有<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHNB_GDF_401" type="text" />米</td>
                                <td>到<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHNB_GDE_401" type="text" />米</td>
                                <td>的<input id="HHNB_HL_401" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(5)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(5)" value="提交" />

                </div>

                <!--表单06. 下午二、山东省近海七市3天海浪、水温预报-->
                <div class="dlgs" id="ddlg_06" <%--id="dlg_06" class="easyui-dialog" title="下午二、山东省近海七市3天海浪、水温预报" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 435px; padding: 10px;">

                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="10">下午二、山东省近海七市3天海浪、水温预报</th>
                            </tr>
                            <tr style="text-align: center">
                                <th class="head0" rowspan="2">地市</th>
                                <th class="head0" colspan="3">浪高（m）</th>
                                <th class="head1" colspan="3">表层水温（℃）</th>
                            </tr>
                            <tr>
                                <td>24h</td>
                                <td>48h</td>
                                <td>72h</td>
                                <td>24h</td>
                                <td>48h</td>
                                <td>72h</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr style="line-height: 45px">
                                <td style="width: 80px;">日照近海</td>
                                <td>
                                    <input id="RZJH_LG_24h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="RZJH_LG_48h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="RZJH_LG_72h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="RZJH_BCSW_24h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="RZJH_BCSW_48h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="RZJH_BCSW_72h" type="text" maxlength="20" style="width: 90%;" /></td>
                            </tr>
                            <tr style="line-height: 45px">
                                <td>青岛近海</td>
                                <td>
                                    <input id="QDJH_LG_24h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="QDJH_LG_48h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="QDJH_LG_72h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="QDJH_BCSW_24h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="QDJH_BCSW_48h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="QDJH_BCSW_72h" type="text" maxlength="20" style="width: 90%;" /></td>
                            </tr>
                            <tr style="line-height: 45px">
                                <td>威海近海</td>
                                <td>
                                    <input id="WHJH_LG_24h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="WHJH_LG_48h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="WHJH_LG_72h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="WHJH_BCSW_24h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="WHJH_BCSW_48h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="WHJH_BCSW_72h" type="text" maxlength="20" style="width: 90%;" /></td>
                            </tr>
                            <tr style="line-height: 45px">
                                <td>烟台近海</td>
                                <td>
                                    <input id="YTJH_LG_24h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="YTJH_LG_48h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="YTJH_LG_72h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="YTJH_BCSW_24h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="YTJH_BCSW_48h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="YTJH_BCSW_72h" type="text" maxlength="20" style="width: 90%;" /></td>
                            </tr>
                            <tr style="line-height: 45px">
                                <td>潍坊近海</td>
                                <td>
                                    <input id="WFJH_LG_24h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="WFJH_LG_48h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="WFJH_LG_72h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="WFJH_BCSW_24h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="WFJH_BCSW_48h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="WFJH_BCSW_72h" type="text" maxlength="20" style="width: 90%;" /></td>
                            </tr>
                            <tr style="line-height: 45px">
                                <td>东营近海</td>
                                <td>
                                    <input id="DYJH_LG_24h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="DYJH_LG_48h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="DYJH_LG_72h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="DYJH_BCSW_24h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="DYJH_BCSW_48h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="DYJH_BCSW_72h" type="text" maxlength="20" style="width: 90%;" /></td>
                            </tr>
                            <tr style="line-height: 45px">
                                <td>滨州近海</td>
                                <td>
                                    <input id="BZJH_LG_24h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="BZJH_LG_48h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="BZJH_LG_72h" type="text" maxlength="80" style="width: 90%;" /></td>
                                <td>
                                    <input id="BZJH_BCSW_24h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="BZJH_BCSW_48h" type="text" maxlength="20" style="width: 90%;" /></td>
                                <td>
                                    <input id="BZJH_BCSW_72h" type="text" maxlength="20" style="width: 90%;" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(6)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(6)" value="提交" />
                </div>

                <!--表单07. 下午三、山东省近海七市72小时潮汐预报-->
                <div class="dlgs" id="ddlg_07" style="width: auto; height: 430px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0"/>
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
                                <th class="head0" colspan="16">下午三、山东省近海七市72小时潮汐预报</th>
                            </tr>
                            <tr style="text-align: center">
                                <th class="head0" rowspan="3">地市</th>
                                <th class="head0" rowspan="3">时间</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                            </tr>
                            <tr>
                                <td>（h、min）</td>
                                <td>cm</td>
                                <td>（h、min）</td>
                                <td>cm</td>
                                <td>（h、min）</td>
                                <td>cm</td>
                                <td>（h、min）</td>
                                <td>cm</td>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td style="width:4%;" rowspan ="3">日照</td>
                                <td class="SJ_01" style="width:6%;">*月*号</td>
                                <td>
                                    <input id="RZ_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="RZ_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_CG_001" type="text" /></td>
                            </tr>
                             <tr>
                                <td class="SJ_02" style="width:6%;">*月*号</td>
                                <td>
                                    <input id="RZ_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="RZ_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_CG_002" type="text" /></td>
                            </tr>
                             <tr>
                                <td class="SJ_03" style="width:6%;">*月*号</td>
                                <td>
                                    <input id="RZ_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="RZ_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_CG_003" type="text" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">青岛</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="QD_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="QD_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="QD_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="QD_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="QD_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="QD_01D_CG_001" type="text" /></td>
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
                                    <input id="QD_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="QD_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="QD_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="QD_01D_CG_002" type="text" /></td>
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
                                    <input id="QD_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="QD_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="QD_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="QD_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="QD_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="QD_02D_CG_003" type="text" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">威海</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WH_01G_H_001" type="text" /></td>
                                 <td>
                                    <input id="WH_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WH_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="WH_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WH_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="WH_01D_CG_001" type="text" /></td>
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
                                    <input id="WH_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="WH_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="WH_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="WH_01D_CG_002" type="text" /></td>
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
                                    <input id="WH_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="WH_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="WH_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="WH_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="WH_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="WH_02D_CG_003" type="text" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">烟台</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="YT_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="YT_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="YT_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="YT_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="YT_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="YT_01D_CG_001" type="text" /></td>
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
                                    <input id="YT_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="YT_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="YT_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="YT_01D_CG_002" type="text" /></td>
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
                                    <input id="YT_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="YT_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="YT_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="YT_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="YT_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="YT_02D_CG_003" type="text" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">潍坊</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WF_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="WF_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WF_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="WF_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WF_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="WF_01D_CG_001" type="text" /></td>
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
                                    <input id="WF_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="WF_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="WF_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="WF_01D_CG_002" type="text" /></td>
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
                                    <input id="WF_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="WF_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="WF_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="WF_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="WF_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="WF_02D_CG_003" type="text" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">东营</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="DY_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="DY_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="DY_02G_H_001" type="text" /></td>
                                 <td>
                                    <input id="DY_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="DY_01D_H_001" type="text" /></td>
                                 <td>
                                    <input id="DY_01D_CG_001" type="text" /></td>
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
                                    <input id="DY_02G_H_002" type="text" /></td>
                                 <td>
                                    <input id="DY_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="DY_01D_H_002" type="text" /></td>
                                 <td>
                                    <input id="DY_01D_CG_002" type="text" /></td>
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
                                    <input id="DY_02G_H_003" type="text" /></td>
                                 <td>
                                    <input id="DY_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="DY_01D_H_003" type="text" /></td>
                                 <td>
                                    <input id="DY_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="DY_02D_H_003" type="text" /></td>
                                 <td>
                                    <input id="DY_02D_CG_003" type="text" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">滨州</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="BZ_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="BZ_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_CG_001" type="text" /></td>
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
                                    <input id="BZ_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_CG_002" type="text" /></td>
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
                                    <input id="BZ_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_CG_003" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(7)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(7)" value="提交" />
                </div>
                
                 <!--表单08. 原下午四、青岛24小时潮位预报中潮汐 取消-->
                <div class="dlgs" id="ddlg_08" <%--id="dlg_08" class="easyui-dialog" title="" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 108px; padding: 10px;">

                    <!--table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">
                            <td colspan="4" style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center">下午四、青岛24小时潮位预报</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第一次高潮</td>
                            <td>
                                <input id="01GC_H" type="text" />h</td>
                            <td>
                                <input id="01GC_MIN" type="text" />min</td>
                            <td>
                                <input id="01GC_CM" type="text" />cm</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第一次低潮</td>
                            <td>
                                <input id="01DC_H" type="text" />h</td>
                            <td>
                                <input id="01DC_MIN" type="text" />min</td>
                            <td>
                                <input id="01DC_CM" type="text" />cm</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第二次高潮</td>
                            <td>
                                <input id="02GC_H" type="text" />h</td>
                            <td>
                                <input id="02GC_MIN" type="text" />min</td>
                            <td>
                                <input id="02GC_CM" type="text" />cm</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第二次低潮</td>
                            <td>
                                <input id="02DC_H" type="text" />h</td>
                            <td>
                                <input id="02DC_MIN" type="text" />min</td>
                            <td>
                                <input id="02DC_CM" type="text" />cm</td>
                        </tr>
                    </table-->
                    <table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">
                            <td colspan="4" style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center">下午四、青岛24小时海浪预报</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td> 明日海滨浪高：</td>
                            <td>
                               <input id="MRHBLG" type="text" style="margin-right: 25px; width: 30%" />
                            </td>
                            <td>
                                浪向：
                            </td>
                            <td id=" ">
                                <input id="MRHBLX" type="text" style="width: 30%" />
                           </td>
                        </tr>
                    </table>

                   <%-- <br />
                    明日海滨浪高：<input id="MRHBLG" type="text" style="margin-right: 25px; width: 30%" />
                    &nbsp; &nbsp; 浪向：<input id="MRHBLX" type="text" style="width: 30%" />
                    <br />--%>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(8)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(8)" value="提交" />

                </div>
               
                 <!--表单19.青岛周边海域48小时海浪、水温预报-->
                <!--原下午五、青岛沿岸48小时潮汐预报 取消-->
                <!--div class="dlgs" id="ddlg_20" <%-- id="dlg_20" class="easyui-dialog" title="下午十六、青岛沿岸48小时潮汐预报" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 285px; padding: 10px;">
                    <table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">
                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="4">下午五、青岛沿岸48小时潮汐预报</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第一次高潮</td>      
                            <td>
                                <input id="01GC_H20" type="text" />h</td>
                            <td>
                                <input id="01GC_MIN20" type="text" />min</td>
                            <td>
                                <input id="01GC_CM20" type="text" />cm</td>
                        </tr>
                         <tr style="line-height: 45px">
                            <td>第二次高潮</td>
                            <td>
                                <input id="02GC_H20" type="text" />h</td>
                            <td>
                                <input id="02GC_MIN20" type="text" />min</td>
                            <td> 
                                <input id="02GC_CM20" type="text" />cm</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第一次低潮</td>
                            <td>
                                <input id="01DC_H20" type="text" />h</td>
                            <td>
                                <input id="01DC_MIN20" type="text" />min</td>
                            <td>
                                <input id="01DC_CM20" type="text" />cm</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第二次低潮</td>
                            <td>
                                <input id="02DC_H20" type="text" />h</td>
                            <td>
                                <input id="02DC_MIN20" type="text" />min</td>
                            <td>
                                <input id="02DC_CM20" type="text" />cm</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(20)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(20)" value="提交" />

                </div-->
             
                <!--表单10. 下午四、明泽闸潮位预报-->
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
                                <th class="head0" colspan="10">下午五、明泽闸潮位预报</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                            </tr>
                            <tr>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                                <td>（h、min）</td>
                                <td>(cm)</td>
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

                <!--表单11. 下午六、南堡油田海域波浪、风、水温预报 -->
                <div class="dlgs" id="ddlg_11" style="width: auto; height: 245px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="7">下午六、南堡油田海域波浪、风、水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">日期</th>
                                <th class="head1">波高(m)</th>
                                <th class="head0">波向(方位)</th>
                                <th class="head1">风向(方位)</th>
                                <th class="head0">风力（级）</th>
                                <%--<th class="head0">水温（℃）</th>
                                <th class="head0">天气</th>--%>
                                <th class="head0">天气</th>
                                <th class="head0">水温（℃）</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="NBYT_BG_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_BX_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_FX_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_FL_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_TQ_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_SW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="NBYT_BG_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_BX_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_FX_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_FL_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_TQ_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_SW_02" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="NBYT_BG_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_BX_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_FX_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_FL_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_TQ_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_SW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(11)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(11)" value="提交" />
                </div>

                 <!--表单12. 下午七、南堡油田海域潮汐预报-->
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

                                <th class="head0" colspan="10">下午七、南堡油田海域潮汐预报</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="3">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                            </tr>
                            <tr>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                                <td>（h、min）</td>
                                <td>(cm)</td>
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
               
                <!--表单13. 下午八、海区24小时海浪、水温预报 原下午九、-->
                <div class="dlgs" id="ddlg_13" <%-- id="dlg_13" class="easyui-dialog" title="下午八、海区24小时海浪、水温预报" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 425px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="6">下午八、海区24小时海浪、水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0"></th>
                                <th class="head1">波高(m)</th>
                                <th class="head0">波向(方位)</th>
                                <%--                                <th class="head1">波级(浪)</th>--%>
                                <th class="head0">涌向(方位)</th>
                                <th class="head1">备注</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>渤海</td>
                                <td>
                                    <input id="BH13_BG" type="text" /></td>
                                <td>
                                    <input id="BH13_BX" type="text" /></td>
                                <%--                                <td>
                                    <input id="BH13_HL" type="text" /></td>--%>
                                <td>
                                    <input id="BH13_YX" type="text" /></td>
                                <td>
                                    <textarea id="BH13_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="HHBB_BG" type="text" /></td>
                                <td>
                                    <input id="HHBB_BX" type="text" /></td>
                                <%--                                <td>
                                    <input id="HHBB_HL" type="text" /></td>--%>
                                <td>
                                    <input id="HHBB_YX" type="text" /></td>
                                <td>
                                    <textarea id="HHBB_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="HHZB_BG" type="text" /></td>
                                <td>
                                    <input id="HHZB_BX" type="text" /></td>
                                <%--                                <td>
                                    <input id="HHZB_HL" type="text" /></td>--%>
                                <td>
                                    <input id="HHZB_YX" type="text" /></td>
                                <td>
                                    <textarea id="HHZB_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>黄海南部</td>
                                <td>
                                    <input id="HHNB_BG" type="text" /></td>
                                <td>
                                    <input id="HHNB_BX" type="text" /></td>
                                <%--                                <td></td>--%>
                                <td>
                                    <input id="HHNB_YX" type="text" /></td>
                                <td>
                                    <textarea id="HHNB_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>青岛近岸</td>
                                <td>
                                    <input id="QDJA_BG" type="text" /></td>
                                <td>
                                    <input id="QDJA_BX" type="text" /></td>
                                <%--                                <td></td>--%>
                                <td>
                                    <input id="QDJA_YX" type="text" /></td>
                                <td>
                                    <textarea id="QDJA_BZ"> </textarea></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(13)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(13)" value="提交" />
                </div>
                
                <!--表单14. 下午九、海区48小时海浪预报 元下午十-->
                <div class="dlgs" id="ddlg_14" <%--id="dlg_14" class="easyui-dialog" title="下午九、海区48小时海浪预报" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 425px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午九、海区48小时海浪预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0"></th>
                                <th class="head1">波高(m)</th>
                                <th class="head0">波向(方位)</th>
                                <th class="head1">涌向(方位)</th>
                                <th class="head0">备注</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>渤海</td>
                                <td>
                                    <input id="BH14_BG" type="text" /></td>
                                <td>
                                    <input id="BH14_BX" type="text" /></td>
                                <td>
                                    <input id="BH14_YX" type="text" /></td>
                                <td>
                                    <textarea id="BH14_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="HHBB14_BG" type="text" /></td>
                                <td>
                                    <input id="HHBB14_BX" type="text" /></td>
                                <td>
                                    <input id="HHBB14_YX" type="text" /></td>
                                <td>
                                    <textarea id="HHBB14_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="HHZB14_BG" type="text" /></td>
                                <td>
                                    <input id="HHZB14_BX" type="text" /></td>
                                <td>
                                    <input id="HHZB14_YX" type="text" /></td>
                                <td>
                                    <textarea id="HHZB14_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>黄海南部</td>
                                <td>
                                    <input id="HHNB14_BG" type="text" /></td>
                                <td>
                                    <input id="HHNB14_BX" type="text" /></td>
                                <td>
                                    <input id="HHNB14_YX" type="text" /></td>
                                <td>
                                    <textarea id="HHNB14_BZ"> </textarea></td>
                            </tr>
                            <%--  <tr>
                                    <td>青岛近岸</td>
                                    <td>
                                        <input id="QDJA14_BG" type="text" /></td>
                                    <td>
                                        <input id="QDJA14_BX" type="text" /></td>
                                    <td>
                                        <input id="QDJA14_YX" type="text" /></td>
                                    <td>
                                        <textarea id="QDJA14_BZ"  > </textarea></td>
                                </tr>--%>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(14)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(14)" value="提交" />
                </div>
                
                <!--表单15. 下午十、海区72小时海浪预报 -->
                <div class="dlgs" id="ddlg_15" <%-- id="dlg_15" class="easyui-dialog" title="下午十、海区72小时海浪预报" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 425px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午十、海区72小时海浪预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0"></th>
                                <th class="head1">波高(m)</th>
                                <th class="head0">波向(方位)</th>
                                <th class="head1">涌向(方位)</th>
                                <th class="head0">备注</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>渤海</td>
                                <td>
                                    <input id="BH15_BG" type="text" /></td>
                                <td>
                                    <input id="BH15_BX" type="text" /></td>
                                <td>
                                    <input id="BH15_YX" type="text" /></td>
                                <td>
                                    <textarea id="BH15_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="HHBB15_BG" type="text" /></td>
                                <td>
                                    <input id="HHBB15_BX" type="text" /></td>
                                <td>
                                    <input id="HHBB15_YX" type="text" /></td>
                                <td>
                                    <textarea id="HHBB15_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="HHZB15_BG" type="text" /></td>
                                <td>
                                    <input id="HHZB15_BX" type="text" /></td>
                                <td>
                                    <input id="HHZB15_YX" type="text" /></td>
                                <td>
                                    <textarea id="HHZB15_BZ"> </textarea></td>
                            </tr>
                            <tr>
                                <td>黄海南部</td>
                                <td>
                                    <input id="HHNB15_BG" type="text" /></td>
                                <td>
                                    <input id="HHNB15_BX" type="text" /></td>
                                <td>
                                    <input id="HHNB15_YX" type="text" /></td>
                                <td>
                                    <textarea id="HHNB15_BZ"> </textarea></td>
                            </tr>
                            <%--  <tr>
                                    <td>青岛近岸</td>
                                    <td>
                                        <input id="QDJA15_BG" type="text" /></td>
                                    <td>
                                        <input id="QDJA15_BX" type="text" /></td>
                                    <td>
                                        <input id="QDJA15_YX" type="text" /></td>
                                    <td>
                                        <textarea id="QDJA15_BZ"  > </textarea></td>
                                </tr>--%>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(15)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(15)" value="提交" />
                </div>
               
                <!--表单16.原下午十二、潍坊港24小时潮汐预报 取消不用-->
                <!--div class="dlgs" id="ddlg_16" <%-- id="dlg_16" class="easyui-dialog" title="下午十二、潍坊港24小时潮汐预报" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 285px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="4">下午十二、潍坊港24小时潮汐预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0" colspan="2">&nbsp;</th>
                                <th class="head0">第一次</th>
                                <th class="head1">第二次</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td rowspan="2">高潮</td>
                                <td>潮时（h、min）</td>
                                <td>
                                    <input id="WFG_GCCS_01" type="text" /></td>
                                <td>
                                    <input id="WFG_GCCS_02" type="text" /></td>
                            </tr>
                            <tr>
                                <td>潮高（cm）</td>
                                <td>
                                    <input id="WFG_GCCG_01" type="text" /></td>
                                <td>
                                    <input id="WFG_GCCG_02" type="text" /></td>
                            </tr>
                            <tr>
                                <td rowspan="2">低潮</td>
                                <td>潮时（h、min）</td>
                                <td>
                                    <input id="WFG_DCCS_01" type="text" /></td>
                                <td>
                                    <input id="WFG_DCCS_02" type="text" /></td>
                            </tr>
                            <tr>
                                <td>潮高（cm）</td>
                                <td>
                                    <input id="WFG_DCCG_01" type="text" /></td>
                                <td>
                                    <input id="WFG_DCCG_02" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(16)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(16)" value="提交" />
                </div-->
                
                <!--表单17.下午十一、青岛市各海水浴场海浪、水温预报 -->
                <div class="dlgs" id="ddlg_17" style="width: auto; height: 285px; padding: 10px;">
                    <table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">
                            <td colspan="9" style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center">下午十一、青岛市各海水浴场海浪、水温预报</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="width:150px;">第一海水浴场</td>
                            <td style="width:70px; text-align:right;">浪高</td><td><input id="DYHSYC_LG" type="text" style="width:97%;"/></td><td style="width:30px; text-align:left;">米</td>                         
                            <td>
                                <input id="DYHSYC_YY" type="text" style="width:97%;"/></td><td style="width:50px; text-align:left;">游泳</td>
                            <td style="width:70px; text-align:right;">水温</td><td><input id="DYHSYC_SW" type="text" style="width:97%;"/></td><td style="width:30px; text-align:left;">℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第六海水浴场</td>
                            <td style="text-align:right;">浪高</td><td><input id="DLHSYC_LG" type="text" style="width:97%;"/></td><td style="text-align:left;">米</td>                 
                            <td>
                                <input id="DLHSYC_YY" type="text" style="width:97%;"/></td><td style="text-align:left;">游泳</td>
                            <td style="text-align:right;">水温</td><td><input id="DLHSYC_SW" type="text" style="width:97%;"/></td><td style="text-align:left;">℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>石老人海水浴场</td>
                            <td style="text-align:right;">浪高</td><td><input id="SLRHSYC_LG" type="text" style="width:97%;"/></td><td style="text-align:left;">米</td>
                            
                            <td>
                                <input id="SLRHSYC_YY" type="text" style="width:97%;"/></td><td style="text-align:left;">游泳</td>
                            <td style="text-align:right;">水温</td><td><input id="SLRHSYC_SW" type="text" style="width:97%;"/></td><td style="text-align:left;">℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>金沙滩海水浴场</td>
                            <td style="text-align:right;">浪高</td><td><input id="JSTHSYC_LG" type="text" style="width:97%;"/></td><td style="text-align:left;">米</td>
                            
                            <td>
                                <input id="JSTHSYC_YY" type="text" style="width:97%;"/></td><td style="text-align:left;">游泳</td>
                            <td style="text-align:right;">水温</td><td><input id="JSTHSYC_SW" type="text" style="width:97%;"/></td><td style="text-align:left;">℃</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(17)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(17)" value="提交" />
                </div>
                
                <!--表单18.下午十二、小麦岛72小时潮汐预报 原下午十四 金沙滩预报取消，从下午三青岛取-->
                <div class="dlgs" id="ddlg_18" style="width: auto; height: 430px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0"/>
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
                                <th class="head0" colspan="16">下午十二、小麦岛72小时潮汐预报</th>
                            </tr>
                            <tr style="text-align: center">
                                <th class="head0" rowspan="3">地市</th>
                                <th class="head0" rowspan="3">时间</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第二次高潮</th>
                                <th class="head1" colspan="2">第一次低潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>
                            <tr>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                            </tr>
                            <tr>
                                <td>（h、min）</td>
                                <td>cm</td>
                                <td>（h、min）</td>
                                <td>cm</td>
                                <td>（h、min）</td>
                                <td>cm</td>
                                <td>（h、min）</td>
                                <td>cm</td>
                            </tr>

                        </thead>
                        <tbody>
                             <tr>
                                <td style="width:4%;" rowspan ="3">青岛市区海水浴场</td>
                                <td class="SJ_01" style="width:6%;">*月*号</td>
                                <td>
                                    <input id="XMD_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="XMD_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_CG_001" type="text" /></td>
                            </tr>
                             <tr>
                                <td class="SJ_02" style="width:6%;">*月*号</td>
                                <td>
                                    <input id="XMD_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="XMD_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_CG_002" type="text" /></td>
                            </tr>
                             <tr>
                                <td class="SJ_03" style="width:6%;">*月*号</td>
                                <td>
                                    <input id="XMD_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="XMD_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="XMD_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="XMD_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="XMD_02D_CG_003" type="text" /></td>
                            </tr>
                             <%--<tr>
                                <td rowspan="3">金沙滩海水浴场</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WMT_01G_H_001" type="text" /></td>
                                <td>
                                    <input id="WMT_01G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WMT_02G_H_001" type="text" /></td>
                                <td>
                                    <input id="WMT_02G_CG_001" type="text" /></td>
                                <td>
                                    <input id="WMT_01D_H_001" type="text" /></td>
                                <td>
                                    <input id="WMT_01D_CG_001" type="text" /></td>
                                <td>
                                    <input id="WMT_02D_H_001" type="text" /></td>
                                <td>
                                    <input id="WMT_02D_CG_001" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="WMT_01G_H_002" type="text" /></td>
                                <td>
                                    <input id="WMT_01G_CG_002" type="text" /></td>
                                <td>
                                    <input id="WMT_02G_H_002" type="text" /></td>
                                <td>
                                    <input id="WMT_02G_CG_002" type="text" /></td>
                                <td>
                                    <input id="WMT_01D_H_002" type="text" /></td>
                                <td>
                                    <input id="WMT_01D_CG_002" type="text" /></td>
                                <td>
                                    <input id="WMT_02D_H_002" type="text" /></td>
                                <td>
                                    <input id="WMT_02D_CG_002" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="WMT_01G_H_003" type="text" /></td>
                                <td>
                                    <input id="WMT_01G_CG_003" type="text" /></td>
                                <td>
                                    <input id="WMT_02G_H_003" type="text" /></td>
                                <td>
                                    <input id="WMT_02G_CG_003" type="text" /></td>
                                <td>
                                    <input id="WMT_01D_H_003" type="text" /></td>
                                <td>
                                    <input id="WMT_01D_CG_003" type="text" /></td>
                                <td>
                                    <input id="WMT_02D_H_003" type="text" /></td>
                                <td>
                                    <input id="WMT_02D_CG_003" type="text" /></td>
                            </tr>--%>
                            
                        </tbody>
                    </table>
                   <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(18)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(18)" value="提交" />
                </div>
                
                <!--下午十三、青岛周边海域24小时海浪、水温预报-->
                <div class="dlgs" id="ddlg_19" style="width: auto; height: 285px; padding: 10px;">
                    <table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">
                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="3">下午十三、青岛周边海域24小时海浪、水温预报</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>青岛近海</td>
                            <td>浪高<input id="QDJH_LG19" type="text" />米</td>
                            <td>水温<input id="QDJH_SW19" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>即墨近海</td>
                            <td>浪高<input id="JMJH_LG19" type="text" />米</td>
                            <td>水温<input id="JMJH_SW19" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>胶州湾</td>
                            <td>浪高<input id="JZJH_LG19" type="text" />米</td>
                            <td>水温<input id="JZJH_SW19" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>黄岛近海</td>
                            <td>浪高<input id="JNJH_LG19" type="text" />米</td>
                            <td>水温<input id="JNJH_SW19" type="text" />℃</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(19)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(19)" value="提交" />
                </div>
               
                <!--表单09.下午十四、黄河南海堤附近海域72小时风、浪预报 原下午十六-->
                <div class="dlgs" id="ddlg_09" <%-- id="dlg_09" class="easyui-dialog" title="下午十三、黄河南海堤附近海域72小时风、浪预报" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午十四、黄河南海堤附近海域72小时风、浪预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">日期</th>
                                <th class="head1">波高(m)</th>
                                <th class="head0">波向(方位)</th>
                                <th class="head1">风向(方位)</th>
                                <th class="head0">风力（级）</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="BG_01" type="text" /></td>
                                <td>
                                    <input id="BX_01" type="text" /></td>
                                <td>
                                    <input id="FX_01" type="text" /></td>
                                <td>
                                    <input id="FL_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="BG_02" type="text" /></td>
                                <td>
                                    <input id="BX_02" type="text" /></td>
                                <td>
                                    <input id="FX_02" type="text" /></td>
                                <td>
                                    <input id="FL_02" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="BG_03" type="text" /></td>
                                <td>
                                    <input id="BX_03" type="text" /></td>
                                <td>
                                    <input id="FX_03" type="text" /></td>
                                <td>
                                    <input id="FL_03" type="text" /></td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold">备注</td>
                                <td style="font-weight: bold" colspan="4">波高系指10m等深线处波高</td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(9)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(9)" value="提交" />
                </div>
                <!--表单21.下午十五 、威海电视台未来24小时预报-->
                <div class="dlgs" id="ddlg_21" style="width: auto; height: 425px; padding: 10px;">
                   
                     <table id="fltable" cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                         <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                         </colgroup>
                         <thead>
                             <tr style="text-align: center">
                                 <th class="head0" colspan="5">下午十五、威海电视台未来24小时预报</th>
                            </tr>
                            <tr style="text-align: center">
                                <th class="head0"></th>
                                <th class="head1" colspan="2">浪高(m)</th>
                                <th class="head0" colspan="2">水温(℃)</th>
                            </tr>
                             <tr>
                                <th class="head0"></th>
                                <th class="head1">未来24小时</th>
                                <th class="head0">未来48小时</th>
                                <th class="head1">未来24小时</th>
                                <th class="head0">未来48小时</th>
                             </tr>
                         </thead>
                        <tbody>
                         <tr>
                             <td>乳山市</td>
                            <td><input name ="RSS_LG_24" id="RSS_LG_24" type="text" /></td>
                            <td><input name="RSS_LG_48" id="RSS_LG_48" type="text" /></td>
                            <td><input name ="RSS_SW_24" id="RSS_SW_24" type="text" /></td>
                            <td><input name="RSS_SW_48" id="RSS_SW_48" type="text" /></td>
                        </tr>
                         <tr>
                            <td>文登区</td>
                            <td><input name="WDQ_LG_24" id="WDQ_LG_24" type="text" /></td>
                            <td><input name="WDQ_LG_48" id="WDQ_LG_48" type="text" /></td>
                            <td><input name="WDQ_SW_24" id="WDQ_SW_24" type="text" /></td>
                            <td><input name="WDQ_SW_48" id="WDQ_SW_48" type="text" /></td>
                        </tr>
                         <tr >
                            <td>石岛</td>
                            <td><input name="SDJH_LG_24" id="SDJH_LG_24" type="text" /></td>
                            <td><input name="SDJH_LG_48" id="SDJH_LG_48" type="text" /></td>
                            <td><input name="SDJH_SW_24" id="SDJH_SW_24" type="text" /></td>
                            <td><input name="SDJH_SW_48" id="SDJH_SW_48" type="text" /></td>
                        </tr>
                         <tr>
                            <td>成山头</td>
                            <td><input name="CST_LG_24" id="CST_LG_24" type="text" /></td>
                            <td><input name="CST_LG_48" id="CST_LG_48" type="text" /></td>
                            <td><input name="CST_SW_24" id="CST_SW_24" type="text" /></td>
                            <td><input name="CST_SW_48" id="CST_SW_48" type="text" /></td>
                        </tr>
                         <tr>
                            <td>威海市区</td>
                            <td></td>
                            <td><input name="WHJH_LG_48" id="WHJH_LG_48" type="text" /></td>
                            <td></td>
                            <td><input name="WHJH_SW_48" id="WHJH_SW_48" type="text" style="visibility:hidden;" /></td>
                        </tr>
                        </tbody>
                        
                    </table>

                     <table id ="swtable" cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                           <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                         </colgroup>
                         <thead>
                             <tr style="text-align: center">
                                 <th class="head0" colspan="5">下午十四、威海电视台未来24小时预报</th>
                            </tr>
                              <tr style="text-align: center">
                                <th class="head0"></th>
                                <th class="head1" colspan="2">浪高(m)</th>
                                <th class="head0" colspan="2">水温(℃)</th>
                            </tr>
                             <tr>
                                <th class="head0"></th>
                                <th class="head1">未来24小时</th>
                                <th class="head0">未来48小时</th>
                                <th class="head1">未来24小时</th>
                                <th class="head0">未来48小时</th>
                             </tr>
                         </thead>
                           <tbody>
                            <tr>
                            <td> 威海市区</td>
                            <td></td>
                            <td><input  id="WHJH_LG_48_1" type="text" /></td>
                            <td></td>
                            <td><input  id="WHJH_SW_48_1" type="text"  style="visibility:hidden;" /></td>
                            </tr>
                            <tr>
                            <td>石岛</td>
                            <td><input  id="SDJH_LG_24_1" type="text" /></td>
                            <td><input  id="SDJH_LG_48_1" type="text" /></td>
                            <td><input  id="SDJH_SW_24_1" type="text" /></td>
                            <td><input  id="SDJH_SW_48_1" type="text" /></td>
                        </tr>
                         <tr>
                            <td>文登区</td>
                            <td><input  id="WDQ_LG_24_1" type="text" /></td>
                            <td><input id="WDQ_LG_48_1" type="text" /></td>
                            <td><input  id="WDQ_SW_24_1" type="text" /></td>
                            <td><input id="WDQ_SW_48_1" type="text" /></td>
                        </tr>
                         <tr>
                           <td>成山头</td>
                            <td><input  id="CST_LG_24_1" type="text" /></td>
                            <td><input  id="CST_LG_48_1" type="text" /></td>
                            <td><input  id="CST_SW_24_1" type="text" /></td>
                            <td><input  id="CST_SW_48_1" type="text" /></td>
                        </tr>
                        <tr>
                             <td>乳山市</td>
                            <td><input  id="RSS_LG_24_1" type="text" /></td>
                            <td><input  id="RSS_LG_48_1" type="text" /></td>
                            <td><input  id="RSS_SW_24_1" type="text" /></td>
                            <td><input  id="RSS_SW_48_1" type="text" /></td>
                        </tr>
                       </tbody>
                       
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(21)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(21)" value="提交" />
                </div>
               
                <!--表单22.下午十六、威海48小时潮汐预报 其中威海取消不用去下午三威海24和48小时 原下午十八-->
                <div class="dlgs" id="ddlg_22" <%--  id="dlg_22" class="easyui-dialog" title="下午十六、潮汐预报" data-options="iconCls:'icon-save'"--%> style="width: auto; height: 640px; padding: 10px;">
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
                                <th class="head0" colspan="10">下午十六、威海48小时潮汐预报</th>
                            </tr>
                            <tr>
                                <th class="head0" rowspan="3">港口</th>
                                <th class="head0" rowspan="3">时间</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第二次低潮</th>
                            </tr>

                            <tr>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                                <td>时间</td>
                                <td>潮位</td>
                            </tr>
                            <tr>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                                <td>（h、min）</td>
                                <td>(cm)</td>
                            </tr>
                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td rowspan="2">乳山</td>
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

                            <tr>
                                <td rowspan="2">文登</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WD_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="WD_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="WD_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="WD_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="WD_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="WD_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="WD_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="WD_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="WD_01G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="WD_01G_CW_02" type="text" /></td>
                                <td>
                                    <input id="WD_01D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="WD_01D_CW_02" type="text" /></td>
                                <td>
                                    <input id="WD_02G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="WD_02G_CW_02" type="text" /></td>
                                <td>
                                    <input id="WD_02D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="WD_02D_CW_02" type="text" /></td>
                            </tr>
                            <tr>
                                <td rowspan="2">石岛</td>
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
                            <tr>
                                <td rowspan="2">成山头</td>
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

                            <%--<tr>
                                <td rowspan="2">威海</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WH_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="WH_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="WH_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="WH_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="WH_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="WH_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="WH_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="WH_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="WH_01G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="WH_01G_CW_02" type="text" /></td>
                                <td>
                                    <input id="WH_01D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="WH_01D_CW_02" type="text" /></td>
                                <td>
                                    <input id="WH_02G_SJ_02" type="text" /></td>
                                <td>
                                    <input id="WH_02G_CW_02" type="text" /></td>
                                <td>
                                    <input id="WH_02D_SJ_02" type="text" /></td>
                                <td>
                                    <input id="WH_02D_CW_02" type="text" /></td>
                            </tr>--%>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(22)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(22)" value="提交" />
                </div>
               
                <!--表单43.东营埕岛预报编号-->
                <div class="dlgs" id="ddlg_43" style="height: auto; padding: 10px;">
                     <div style="border: 1px solid #ddd; background-color: #d2ffe7; height: auto; padding: 10px;" >
                          东营埕岛预报编号：<input type="text" id="ProYear" style="width: 50px" />-<input type="text" id="ProNo" style="width: 50px"/>
                     </div>
                </div>

                <!-- 表单41.下午十七、东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报） 原下午十九-->
                <div class="dlgs" id="ddlg_41" style="height: 290px; padding: 10px;">

                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                           <%-- <col class="con1" />
                            <col class="con0" />--%>
                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head0" colspan="7">下午十七、东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）</th>
                            </tr>
                            <tr>
                                <th class="head0">时间（h）</th>
                                <th class="head1">12</th>
                                <th class="head0">24</th>
                                <th class="head1">48</th>
                                <th class="head0">72</th>
                            </tr>
                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td>风向</td>
                                <td>
                                    <input id="DY_12H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DY_24H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DY_48H_01" type="text" maxlength="15" /></td>
                                 <td>
                                    <input id="DY_72H_01" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td>风力（级）</td>
                                <td>
                                    <input id="DY_12H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DY_24H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DY_48H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DY_72H_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>浪高（m）</td>
                                <td>
                                    <input id="DY_12H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DY_24H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DY_48H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DY_72H_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(41)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(41)" value="提交" />

                </div>
                
                <!-- 表单42. 下午十八、 东营埕岛-未来三天高/低潮预报 下午二十 -->
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
                                <th class="head0" colspan="10">下午十八、 东营埕岛-未来三天高/低潮预报</th>
                            </tr>
                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
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
                                    <input id="DY_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CG_01" type="text" /></td>
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
                                    <input id="DY_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CG_02" type="text" /></td>
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
                                    <input id="DY_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DY_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DY_01D_CG_03" type="text" /></td>
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
               
                <!--表单01.下午十九、海洋牧场-海浪预报 原下午二十一-->
                <div class="dlgs" id="ddlg_47"  style="height: 390px; padding: 10px;">
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
                                <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="7">下午十九、海洋牧场-海浪预报</td>
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
                                <td>寻山海洋牧场</td>
                                <td> <input id="WAVE_CQ_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_CQ_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>荣成烟墩角游钓型海洋牧场</td>
                                <td> <input id="WAVE_RC_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_RC_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>西霞口集团国家级海洋牧场</td>
                                <td> <input id="WAVE_XXK_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_XXK_72_N" type="text" maxlength="20" /></td>
                            </tr>

                            <%-- 新增7个海洋牧场的预报制作 add by Lian Start --%>
                            <tr>
                                <td>滨州正海底播型海洋牧场</td>
                                <td> <input id="WAVE_BZZH_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_BZZH_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_BZZH_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_BZZH_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_BZZH_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_BZZH_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>山东通和底播型海洋牧场</td>
                                <td> <input id="WAVE_SDTH_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_SDTH_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_SDTH_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_SDTH_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_SDTH_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_SDTH_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>山东莱州太平湾明波国家级海洋牧场</td>
                                <td> <input id="WAVE_TPW_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_TPW_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_TPW_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_TPW_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_TPW_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_TPW_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>山东琵琶口富瀚国家级海洋牧场</td>
                                <td> <input id="WAVE_PPK_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_PPK_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_PPK_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_PPK_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_PPK_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_PPK_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>山东庙岛群岛东部佳益国家级海洋牧场</td>
                                <td> <input id="WAVE_MDQD_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_MDQD_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_MDQD_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_MDQD_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_MDQD_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_MDQD_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>山东海州湾顺风国家级海洋牧场</td>
                                <td> <input id="WAVE_HZW_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_HZW_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_HZW_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_HZW_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_HZW_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_HZW_72_N" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>山东岚山东部万泽丰国家级海洋牧场</td>
                                <td> <input id="WAVE_LSDBWZF_24_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_LSDBWZF_24_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_LSDBWZF_48_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_LSDBWZF_48_N" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_LSDBWZF_72_D" type="text" maxlength="20" /></td>
                                <td> <input id="WAVE_LSDBWZF_72_N" type="text" maxlength="20" /></td>
                            </tr>

                            <%-- 新增7个海洋牧场的预报制作 add by Lian End --%>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(47)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(47)" value="提交" />

                </div>

                <!--表单02.下午二十、海洋牧场-潮汐预报 原下午二十二-->
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
                                <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="33">下午二十、海洋牧场-潮汐预报</td>
                            </tr>
                            <tr>
                                <th class="head0" rowspan="2">海洋牧场长名称</th>
                                <th class="head0" rowspan="2">预报日期</th>
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
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">寻山海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_CQ_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_CQ_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_CQ_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_CQ_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_CQ_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                                
                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">荣成烟墩角游钓型海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_RC_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_RC_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_RC_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_RC_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_RC_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">西霞口集团国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_XXK_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_XXK_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_XXK_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_XXK_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_XXK_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <%-- 下午20添加7个海洋牧场预报  Lian start --%>
                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">滨州正海底播型海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_BZZH_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_BZZH_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_BZZH_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_BZZH_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_BZZH_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东通和底播型海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_SDTH_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_SDTH_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_SDTH_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_SDTH_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_SDTH_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                             <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东莱州太平湾明波国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_TPW_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_TPW_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_TPW_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_TPW_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_TPW_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东琵琶口富瀚国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_PPK_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_PPK_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_PPK_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_PPK_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_PPK_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东庙岛群岛东部佳益国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_MDQD_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_MDQD_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_MDQD_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_MDQD_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_MDQD_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                             <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东海州湾顺风国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_HZW_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_HZW_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_HZW_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_HZW_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_HZW_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="3" style="border-bottom: 2px #d8d8d8 solid;">山东岚山东部万泽丰国家级海洋牧场</td>
                                <td class="SJ_01" style="border-left: 1px #d8d8d8 solid;">*月*号</td>
                                <td> <input id="TIDE_LSDBWZF_01_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_01_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td> <input id="TIDE_LSDBWZF_02_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_02_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>
                            <tr class="tr-bottom">
                                <td class="SJ_03">*月*号</td>
                                <td> <input id="TIDE_LSDBWZF_03_H00" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H01" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H02" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H03" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H04" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H05" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H06" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H07" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H08" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H09" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H10" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H11" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H12" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H13" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H14" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H15" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H16" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H17" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H18" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H19" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H20" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H21" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H22" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_H23" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_FIRSTHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_FIRSTHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_SECONDHTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_SECONDHHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_FIRSTLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_FIRSTLHEIGHT" type="text" maxlength="20" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_SECONDLTIME" type="text" maxlength="20" style="width:45px;" /></td>
                                <td> <input id="TIDE_LSDBWZF_03_SECONDLHEIGHT" type="text" maxlength="20" /></td>
                            </tr>

                        <%-- 下午20添加7个海洋牧场预报  Lian end --%>

                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(48)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(48)" value="提交" />

                </div>
                
                <!--表单03.下午二十一、海洋牧场-海温预报 原下午二十三-->
                <div class="dlgs" id="ddlg_49" style="height: 390px; padding: 10px;">
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
                                <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="4">下午二十一、海洋牧场-海温预报</td>
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
                                <td>寻山海洋牧场</td>
                                <td> <input id="TEMP_CQ_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_CQ_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_CQ_72" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>荣成烟墩角游钓型海洋牧场</td>
                                <td> <input id="TEMP_RC_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_RC_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_RC_72" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td>西霞口集团国家级海洋牧场</td>
                                <td> <input id="TEMP_XXK_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_XXK_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_XXK_72" type="text" maxlength="20" /></td>
                            </tr>
                            <%-- 新添加7个预报海区 Lian start --%>
                            <tr>
                                <td>滨州正海底播型海洋牧场</td>
                                <td> <input id="TEMP_BZZH_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_BZZH_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_BZZH_72" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td>山东通和底播型海洋牧场</td>
                                <td> <input id="TEMP_SDTH_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_SDTH_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_SDTH_72" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td>山东莱州太平湾明波国家级海洋牧场</td>
                                <td> <input id="TEMP_TPW_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_TPW_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_TPW_72" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td>山东琵琶口富瀚国家级海洋牧场</td>
                                <td> <input id="TEMP_PPK_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_PPK_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_PPK_72" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td>山东庙岛群岛东部佳益国家级海洋牧场</td>
                                <td> <input id="TEMP_MDQD_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_MDQD_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_MDQD_72" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td>山东海州湾顺风国家级海洋牧场</td>
                                <td> <input id="TEMP_HZW_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_HZW_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_HZW_72" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td>山东岚山东部万泽丰国家级海洋牧场</td>
                                <td> <input id="TEMP_LSDBWZF_24" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_LSDBWZF_48" type="text" maxlength="20" /></td>
                                <td> <input id="TEMP_LSDBWZF_72" type="text" maxlength="20" /></td>
                            </tr>
                            <%-- 新添加7个预报海区 Lian end --%>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(49)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(49)" value="提交" />

                </div>
                
                <!--表单53.山东省7地市预报编号-->
                <div class="dlgs" id="ddlg_53" style="height: auto; padding: 10px;">
                     <div style="border: 1px solid #ddd; background-color: #d2ffe7; height: auto; padding: 10px;" >
                          山东省7地市预报编号：<input type="text" id="ProSDYear" style="width: 50px" />-<input type="text" id="ProSDNo" style="width: 50px"/>
                     </div>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(53)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(53)" value="提交" />
                </div>
               
                <!--表单54.下午二十二、东营广利渔港-未来三天高/低潮预报 原下午二十四-->
                <div class="dlgs" id="ddlg_54" style="width: auto; height: 280px; padding: 10px;">
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
                                <th class="head0" colspan="9">下午二十二、东营广利渔港-未来三天高/低潮预报</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
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
                                    <input id="DYGL_01G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DYGL_01G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DYGL_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DYGL_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DYGL_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DYGL_01D_CG_01" type="text" /></td>
                                <td>
                                    <input id="DYGL_02D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DYGL_02D_CG_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="DYGL_01G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DYGL_01G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DYGL_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DYGL_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DYGL_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DYGL_01D_CG_02" type="text" /></td>
                                <td>
                                    <input id="DYGL_02D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DYGL_02D_CG_02" type="text" /></td>
                            </tr>

                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="DYGL_01G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DYGL_01G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DYGL_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DYGL_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DYGL_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DYGL_01D_CG_03" type="text" /></td>
                                <td>
                                    <input id="DYGL_02D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DYGL_02D_CG_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(54)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(54)" value="提交" />
                </div>
               
                <!--表单55.下午二十三、东营广利渔港-未来三天的海面风及海浪有效波高预报（20时起报）原下午二十五-->
                <div class="dlgs" id="ddlg_55" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午二十三、东营广利渔港-未来三天的海面风及海浪有效波高预报（20时起报）</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间（h）</th>
                                <th class="head1">12</th>
                                <th class="head0">24</th>
                                <th class="head1">48</th>
                                <th class="head0">72</th>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td>风向</td>
                                <td>
                                    <input id="DYGL_12H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYGL_24H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYGL_48H_01" type="text" maxlength="15" /></td>
                                 <td>
                                    <input id="DYGL_72H_01" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td>风力（级）</td>
                                <td>
                                    <input id="DYGL_12H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYGL_24H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYGL_48H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYGL_72H_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>浪高（m）</td>
                                <td>
                                    <input id="DYGL_12H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYGL_24H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYGL_48H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYGL_72H_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(55)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(55)" value="提交" />
                </div>
               
                <!--表单56.下午二十四、东营广利渔港-未来三天的海面水温预报 原下午二十六-->
                <div class="dlgs" id="ddlg_56" style="width: auto; height: 200px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午二十四、东营广利渔港-未来三天的海面水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间</th>
                                <th class="SJ_01">*月*号</th>
                                <th class="SJ_02">*月*号</th>
                                <th class="SJ_03">*月*号</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>水温（℃）</td>
                                <td>
                                    <input id="DYGL_SW_01" type="text" /></td>
                                <td>
                                    <input id="DYGL_SW_02" type="text" /></td>
                                <td>
                                    <input id="DYGL_SW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(56)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(56)" value="提交" />
                </div>
                
                <!--表单57.下午二十五、日照桃花岛-未来三天高/低潮预报-->
                <div class="dlgs" id="ddlg_57" style="width: auto; height: 280px; padding: 10px;">
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
                                <th class="head0" colspan="9">下午二十五、日照桃花岛-未来三天高/低潮预报</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
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
                                    <input id="RZTHD_01G_CS_01" type="text" /></td>
                                <td>
                                    <input id="RZTHD_01G_CG_01" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="RZTHD_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="RZTHD_01D_CG_01" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02D_CS_01" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02D_CG_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="RZTHD_01G_CS_02" type="text" /></td>
                                <td>
                                    <input id="RZTHD_01G_CG_02" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="RZTHD_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="RZTHD_01D_CG_02" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02D_CS_02" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02D_CG_02" type="text" /></td>
                            </tr>

                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="RZTHD_01G_CS_03" type="text" /></td>
                                <td>
                                    <input id="RZTHD_01G_CG_03" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="RZTHD_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="RZTHD_01D_CG_03" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02D_CS_03" type="text" /></td>
                                <td>
                                    <input id="RZTHD_02D_CG_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(57)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(57)" value="提交" />
                </div>
                
                <!--表单58.下午二十六、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）-->
                <div class="dlgs" id="ddlg_58" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午二十六、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间（h）</th>
                                <th class="head1">12</th>
                                <th class="head0">24</th>
                                <th class="head1">48</th>
                                <th class="head0">72</th>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td>风向</td>
                                <td>
                                    <input id="RZTHD_12H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="RZTHD_24H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="RZTHD_48H_01" type="text" maxlength="15" /></td>
                                 <td>
                                    <input id="RZTHD_72H_01" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td>风力（级）</td>
                                <td>
                                    <input id="RZTHD_12H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="RZTHD_24H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="RZTHD_48H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="RZTHD_72H_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>浪高（m）</td>
                                <td>
                                    <input id="RZTHD_12H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="RZTHD_24H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="RZTHD_48H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="RZTHD_72H_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(58)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(58)" value="提交" />
                </div>
               
                <!--表单59.下午二十七、日照桃花岛-未来三天的海面水温预报-->
                <div class="dlgs" id="ddlg_59" style="width: auto; height: 200px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午二十七、日照桃花岛-未来三天的海面水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间</th>
                                <th class="SJ_01">*月*号</th>
                                <th class="SJ_02">*月*号</th>
                                <th class="SJ_03">*月*号</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>水温（℃）</td>
                                <td>
                                    <input id="RZTHD_SW_01" type="text" /></td>
                                <td>
                                    <input id="RZTHD_SW_02" type="text" /></td>
                                <td>
                                    <input id="RZTHD_SW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(59)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(59)" value="提交" />
                </div>
               
                <!--表单60.下午二十八、潍坊度假区-未来三天高/低潮预报-->
                <div class="dlgs" id="ddlg_60" style="width: auto; height: 280px; padding: 10px;">
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
                                <th class="head0" colspan="9">下午二十八、潍坊度假区-未来三天高/低潮预报</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
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
                                    <input id="WFDJQ_01G_CS_01" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_01G_CG_01" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_01D_CG_01" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02D_CS_01" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02D_CG_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="WFDJQ_01G_CS_02" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_01G_CG_02" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_01D_CG_02" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02D_CS_02" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02D_CG_02" type="text" /></td>
                            </tr>

                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="WFDJQ_01G_CS_03" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_01G_CG_03" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_01D_CG_03" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02D_CS_03" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_02D_CG_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(60)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(60)" value="提交" />
                </div>
               
                <!--表单61.下午二十九、潍坊度假区-未来三天的海面风及海浪有效波高预报（20时起报）-->
                <div class="dlgs" id="ddlg_61" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午二十九、潍坊度假区-未来三天的海面风及海浪有效波高预报（20时起报）</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间（h）</th>
                                <th class="head1">12</th>
                                <th class="head0">24</th>
                                <th class="head1">48</th>
                                <th class="head0">72</th>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td>风向</td>
                                <td>
                                    <input id="WFDJQ_12H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WFDJQ_24H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WFDJQ_48H_01" type="text" maxlength="15" /></td>
                                 <td>
                                    <input id="WFDJQ_72H_01" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td>风力（级）</td>
                                <td>
                                    <input id="WFDJQ_12H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WFDJQ_24H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WFDJQ_48H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WFDJQ_72H_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>浪高（m）</td>
                                <td>
                                    <input id="WFDJQ_12H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WFDJQ_24H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WFDJQ_48H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WFDJQ_72H_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(61)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(61)" value="提交" />
                </div>
               
                <!--表单62.下午三十、潍坊度假区-未来三天的海面水温预报-->
                <div class="dlgs" id="ddlg_62" style="width: auto; height: 200px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午三十、潍坊度假区-未来三天的海面水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间</th>
                                <th class="SJ_01">*月*号</th>
                                <th class="SJ_02">*月*号</th>
                                <th class="SJ_03">*月*号</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>水温（℃）</td>
                                <td>
                                    <input id="WFDJQ_SW_01" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_SW_02" type="text" /></td>
                                <td>
                                    <input id="WFDJQ_SW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(62)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(62)" value="提交" />
                </div>
                
                <!--表单63.下午三十一、威海新区-未来三天高/低潮预报-->
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
                                <th class="head0" colspan="9">下午三十一、威海新区-未来三天高/低潮预报</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
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
                                    <input id="WHXQ_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CG_01" type="text" /></td>
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
                                    <input id="WHXQ_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CG_02" type="text" /></td>
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
                                    <input id="WHXQ_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="WHXQ_01D_CG_03" type="text" /></td>
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
                
                <!--表单64.下午三十二、威海新区-未来三天的海面风及海浪有效波高预报（20时起报）-->
                <div class="dlgs" id="ddlg_64" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午三十二、威海新区-未来三天的海面风及海浪有效波高预报（20时起报）</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间（h）</th>
                                <th class="head1">12</th>
                                <th class="head0">24</th>
                                <th class="head1">48</th>
                                <th class="head0">72</th>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td>风向</td>
                                <td>
                                    <input id="WHXQ_12H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WHXQ_24H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WHXQ_48H_01" type="text" maxlength="15" /></td>
                                 <td>
                                    <input id="WHXQ_72H_01" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td>风力（级）</td>
                                <td>
                                    <input id="WHXQ_12H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WHXQ_24H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WHXQ_48H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WHXQ_72H_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>浪高（m）</td>
                                <td>
                                    <input id="WHXQ_12H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WHXQ_24H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WHXQ_48H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="WHXQ_72H_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(64)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(64)" value="提交" />
                </div>
                
                <!--表单65.下午三十三、威海新区-未来三天的海面水温预报-->
                <div class="dlgs" id="ddlg_65" style="width: auto; height: 200px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午三十三、威海新区-未来三天的海面水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间</th>
                                <th class="SJ_01">*月*号</th>
                                <th class="SJ_02">*月*号</th>
                                <th class="SJ_03">*月*号</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>水温（℃）</td>
                                <td>
                                    <input id="WHXQ_SW_01" type="text" /></td>
                                <td>
                                    <input id="WHXQ_SW_02" type="text" /></td>
                                <td>
                                    <input id="WHXQ_SW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(65)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(65)" value="提交" />
                </div>
                
                <!--表单66.下午三十四、烟台清泉-未来三天高/低潮预报-->
                <div class="dlgs" id="ddlg_66" style="width: auto; height: 280px; padding: 10px;">
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
                                <th class="head0" colspan="9">下午三十四、烟台清泉-未来三天高/低潮预报</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
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
                                    <input id="YTQQ_01G_CS_01" type="text" /></td>
                                <td>
                                    <input id="YTQQ_01G_CG_01" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="YTQQ_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="YTQQ_01D_CG_01" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02D_CS_01" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02D_CG_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="YTQQ_01G_CS_02" type="text" /></td>
                                <td>
                                    <input id="YTQQ_01G_CG_02" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="YTQQ_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="YTQQ_01D_CG_02" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02D_CS_02" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02D_CG_02" type="text" /></td>
                            </tr>

                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="YTQQ_01G_CS_03" type="text" /></td>
                                <td>
                                    <input id="YTQQ_01G_CG_03" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="YTQQ_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="YTQQ_01D_CG_03" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02D_CS_03" type="text" /></td>
                                <td>
                                    <input id="YTQQ_02D_CG_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(66)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(66)" value="提交" />
                </div>
                
                <!--表单67.下午三十五、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）-->
                <div class="dlgs" id="ddlg_67" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午三十五、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间（h）</th>
                                <th class="head1">12</th>
                                <th class="head0">24</th>
                                <th class="head1">48</th>
                                <th class="head0">72</th>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td>风向</td>
                                <td>
                                    <input id="YTQQ_12H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YTQQ_24H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YTQQ_48H_01" type="text" maxlength="15" /></td>
                                 <td>
                                    <input id="YTQQ_72H_01" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td>风力（级）</td>
                                <td>
                                    <input id="YTQQ_12H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YTQQ_24H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YTQQ_48H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YTQQ_72H_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>浪高（m）</td>
                                <td>
                                    <input id="YTQQ_12H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YTQQ_24H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YTQQ_48H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="YTQQ_72H_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(67)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(67)" value="提交" />
                </div>
                
                <!--表单68.下午三十六、烟台清泉-未来三天的海面水温预报-->
                <div class="dlgs" id="ddlg_68" style="width: auto; height: 200px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午三十六、烟台清泉-未来三天的海面水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间</th>
                                <th class="SJ_01">*月*号</th>
                                <th class="SJ_02">*月*号</th>
                                <th class="SJ_03">*月*号</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>水温（℃）</td>
                                <td>
                                    <input id="YTQQ_SW_01" type="text" /></td>
                                <td>
                                    <input id="YTQQ_SW_02" type="text" /></td>
                                <td>
                                    <input id="YTQQ_SW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(68)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(68)" value="提交" />
                </div>
               
                <!--表单69.下午三十七、董家口-未来三天高/低潮预报-->
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
                                <th class="head0" colspan="9">下午三十七、董家口-未来三天高/低潮预报</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
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
                                    <input id="DJKP_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CG_01" type="text" /></td>
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
                                    <input id="DJKP_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CG_02" type="text" /></td>
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
                                    <input id="DJKP_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DJKP_01D_CG_03" type="text" /></td>
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
               
                <!--表单70.下午三十八、董家口-未来三天的海面风及海浪有效波高预报（20时起报）-->
                <div class="dlgs" id="ddlg_70" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午三十八、董家口-未来三天的海面风及海浪有效波高预报（20时起报）</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间（h）</th>
                                <th class="head1">12</th>
                                <th class="head0">24</th>
                                <th class="head1">48</th>
                                <th class="head0">72</th>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td>风向</td>
                                <td>
                                    <input id="DJKP_12H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DJKP_24H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DJKP_48H_01" type="text" maxlength="15" /></td>
                                 <td>
                                    <input id="DJKP_72H_01" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td>风力（级）</td>
                                <td>
                                    <input id="DJKP_12H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DJKP_24H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DJKP_48H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DJKP_72H_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>浪高（m）</td>
                                <td>
                                    <input id="DJKP_12H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DJKP_24H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DJKP_48H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DJKP_72H_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(70)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(70)" value="提交" />
                </div>
                
                <!--表单71.下午三十九、董家口-未来三天的海面水温预报-->
                <div class="dlgs" id="ddlg_71" style="width: auto; height: 200px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午三十九、董家口-未来三天的海面水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间</th>
                                <th class="SJ_01">*月*号</th>
                                <th class="SJ_02">*月*号</th>
                                <th class="SJ_03">*月*号</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>水温（℃）</td>
                                <td>
                                    <input id="DJKP_SW_01" type="text" /></td>
                                <td>
                                    <input id="DJKP_SW_02" type="text" /></td>
                                <td>
                                    <input id="DJKP_SW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(71)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(71)" value="提交" />
                </div>
                
                <!--表单72.下午四十、东营渔港-未来三天高/低潮预报-->
                <div class="dlgs" id="ddlg_72" style="width: auto; height: 280px; padding: 10px;">
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
                                <th class="head0" colspan="9">下午四十、东营渔港-未来三天高/低潮预报</th>
                            </tr>

                            <tr>
                                <th class="head0" rowspan="2">日期</th>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
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
                                    <input id="DYFP_01G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DYFP_01G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DYFP_02G_CS_01" type="text" /></td>
                                <td>
                                    <input id="DYFP_02G_CG_01" type="text" /></td>
                                <td>
                                    <input id="DYFP_01D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DYFP_01D_CG_01" type="text" /></td>
                                <td>
                                    <input id="DYFP_02D_CS_01" type="text" /></td>
                                <td>
                                    <input id="DYFP_02D_CG_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="DYFP_01G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DYFP_01G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DYFP_02G_CS_02" type="text" /></td>
                                <td>
                                    <input id="DYFP_02G_CG_02" type="text" /></td>
                                <td>
                                    <input id="DYFP_01D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DYFP_01D_CG_02" type="text" /></td>
                                <td>
                                    <input id="DYFP_02D_CS_02" type="text" /></td>
                                <td>
                                    <input id="DYFP_02D_CG_02" type="text" /></td>
                            </tr>

                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="DYFP_01G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DYFP_01G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DYFP_02G_CS_03" type="text" /></td>
                                <td>
                                    <input id="DYFP_02G_CG_03" type="text" /></td>
                                <td>
                                    <input id="DYFP_01D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DYFP_01D_CG_03" type="text" /></td>
                                <td>
                                    <input id="DYFP_02D_CS_03" type="text" /></td>
                                <td>
                                    <input id="DYFP_02D_CG_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(72)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(72)" value="提交" />
                </div>
                
                <!--表单73.下午四十一、东营渔港-未来三天的海面风及海浪有效波高预报（20时起报）-->
                <div class="dlgs" id="ddlg_73" style="width: auto; height: 280px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午四十一、东营渔港-未来三天的海面风及海浪有效波高预报（20时起报）</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间（h）</th>
                                <th class="head1">12</th>
                                <th class="head0">24</th>
                                <th class="head1">48</th>
                                <th class="head0">72</th>
                            </tr>

                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td>风向</td>
                                <td>
                                    <input id="DYFP_12H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYFP_24H_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYFP_48H_01" type="text" maxlength="15" /></td>
                                 <td>
                                    <input id="DYFP_72H_01" type="text" maxlength="15" /></td>
                            </tr>

                            <tr>
                                <td>风力（级）</td>
                                <td>
                                    <input id="DYFP_12H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYFP_24H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYFP_48H_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYFP_72H_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>浪高（m）</td>
                                <td>
                                    <input id="DYFP_12H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYFP_24H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYFP_48H_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="DYFP_72H_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(73)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(73)" value="提交" />
                </div>
                
                <!--表单74.下午四十二、东营渔港-未来三天的海面水温预报-->
                <div class="dlgs" id="ddlg_74" style="width: auto; height: 200px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="5">下午四十二、东营渔港-未来三天的海面水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">时间</th>
                                <th class="SJ_01">*月*号</th>
                                <th class="SJ_02">*月*号</th>
                                <th class="SJ_03">*月*号</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>水温（℃）</td>
                                <td>
                                    <input id="DYFP_SW_01" type="text" /></td>
                                <td>
                                    <input id="DYFP_SW_02" type="text" /></td>
                                <td>
                                    <input id="DYFP_SW_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(74)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(74)" value="提交" />
                </div>
                
                <!--填报信息-->
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
                                     var reportername =  resjson["rows"][j]["reportername"];
                                     var reportertel = resjson["rows"][j]["reportertel"];
                                     if (yubaoid == "FBHBYB") {
                                         strs1 += "<option value='" + reportercode + "' bs='"+reportertel+"'>" + reportercode + "</option>";
                                         $("#ChaoxiTel").val(reportertel);
                                         $("#ShuiwenTel").val(reportertel);
                                         
                                         $("#Chaoxi").html(strs1);
                                         $("#Shuiwen").html(strs1);
                                         $("#uniform-Chaoxi span").text(reportercode);
                                         $("#uniform-Shuiwen span").text(reportercode);
                                         $("#uniform-Chaoxi span").attr("code",reportercode);
                                          $("#uniform-Shuiwen span").attr("code",reportercode);
                                     }else  if (yubaoid == "HLYB") {
                                         strs2 += "<option value='" + reportercode + "' bs='"+reportertel+"'>" + reportercode + "</option>";
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
                
                <!--操作日志-->
                <div id="dlg_czrz" class="easyui-dialog" title="操作日志" data-options="iconCls:'icon-save'" style="width: 800px; height: 530px; padding: 10px;">
                    <iframe width="100%" id="win" height="435" name="czrz" frameborder="0" src="Logbyuser.aspx"></iframe>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_czrz').dialog('close'); " value="取消" />
                </div>
                
                <!--<div id="basicform" style="clear: both" class="subcontent">-->
                <div id="basicform" style="position: fixed; bottom: 0px; left: 20px; z-index: 2">
                </div>
                 <!--选择模版-->
                <div id="dlg_xzmb" class="easyui-dialog" title="选择模版" data-options="iconCls:'icon-save'" style="width: 320px; height: 500px; padding: 10px;">
                    <div>
                        <asp:CheckBox ID="selectAll" runat="server" Text="全选" />
                        <asp:CheckBox ID="reverse" runat="server" Text="反选" />
                        <asp:CheckBox ID="unselect" runat="server" Text="取消" />
                    </div>
                    <hr />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_xzmb').dialog('close'); $('#modellist div div span').removeClass('checked'); <%--$('#checkmodel').val('选择模版(' + $('#modellist .checked').length + ')');--%>" value="取消" />
                    <input id="Releasetable1" style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" <%--onclick="$('#dlg_xzmb').dialog('close'); $('#checkmodel').val('选择模版(' + $('#modellist .checked').length + ')');"--%> value="发布表单" />
                    <br />
                    <br />
                    <div id="modellist">
                        <div>
                            <asp:CheckBox ID="CheckBox3" runat="server" Text="1号山东省电视台预报单" name="1号山东省电视台预报单" />
                        </div>
                         <div>
                            <asp:CheckBox ID="CheckBox6" runat="server" Text="1号72小时山东省电视台预报单" name="1号72小时山东省电视台预报单" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox5" runat="server" Text="3号黄河南海堤2006(a4)-3hao" name="3号黄河南海堤2006(a4)-3hao" />
                        </div>
                        <%--<div>
                            <asp:CheckBox ID="CheckBox6" runat="server" Text="4号预报单(a4)-2014" name="4号预报单(a4)-2014" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox7" runat="server" Text="5号预报单（a4）" name="5号预报单（a4）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox8" runat="server" Text="6号预报单（a4）" name="6号预报单（a4）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox9" runat="server" Text="7号海洋水温海冰预报" name="7号海洋水温海冰预报" />
                        </div>--%>

                        <div>
                            <asp:CheckBox ID="CheckBox11" runat="server" Text="9号预报单(a4)" name="9号预报单(a4)" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox12" runat="server" Text="10号预报单(A4)-南堡" name="10号预报单(A4)-南堡" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox13" runat="server" Text="11号预报单（a4）" name="11号预报单（a4）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox14" runat="server" Text="12号预报单(a4)" name="12号预报单(a4)" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox15" runat="server" Text="13号预报" name="13号预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox16" runat="server" Text="14号海上山东（18a4）-gai14" name="14号海上山东（18a4）-gai14" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="14号72小时预报单" name="14号72小时预报单" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox17" runat="server" Text="15号预报单（a4）" name="15号预报单（a4）" />
                        </div>

                        <div>
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="20号潍坊市海洋预报台专项预报" name="20号潍坊市海洋预报台专项预报" />
                        </div>
                        <div id="moban_21">
                            <asp:CheckBox ID="CheckBox23" runat="server" Text="21号青岛海水浴场预报-电视台播出" name="21号青岛海水浴场预报-电视台播出" />
                        </div>
                        <div  >
                            <asp:CheckBox ID="CheckBox24" runat="server" Text="22号海洋预报-电视台播出-非游泳季节" name="22号海洋预报-电视台播出-非游泳季节" />
                        </div>
                        <%--  <div>
                            <asp:CheckBox ID="CheckBox25" runat="server" Text="24号东营专项预报" name="24号东营专项预报" />
                        </div>--%>
                        <div>
                            <asp:CheckBox ID="CheckBox26" runat="server" Text="25号预报单" name="25号预报单" />
                        </div>
                        <%--     <div>
                            <asp:CheckBox ID="CheckBox27" runat="server" Text="26号预报单" name="26号预报单"/>
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="上午的指挥处预报" name="上午的指挥处预报" />
                        </div>--%>
                        <%--<div>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="下午的指挥处预报" name="下午的指挥处预报" />
                        </div>--%>
                        <%--<div>
                            <asp:CheckBox ID="CheckBox28" runat="server" Text="农业部黄渤海区渔政局专项预报" name="农业部黄渤海区渔政局专项预报" />
                        </div>--%>
                         <div>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="东营埕岛油田海洋环境预报" name="东营埕岛油田海洋环境预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox9" runat="server" Text="东营广利渔港预报" name="东营广利渔港预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox10" runat="server" Text="日照桃花岛预报" name="日照桃花岛预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox18" runat="server" Text="潍坊度假区预报" name="潍坊度假区预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox19" runat="server" Text="威海新区预报" name="威海新区预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox20" runat="server" Text="烟台清泉预报" name="烟台清泉预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox7" runat="server" Text="董家口港预报" name="董家口港预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox8" runat="server" Text="东营渔港预报" name="东营渔港预报" />
                        </div>
                    </div>

                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_xzmb').dialog('close'); $('#modellist div div span').removeClass('checked'); <%--$('#checkmodel').val('选择模版(' + $('#modellist .checked').length + ')');--%>" value="取消" />
                    <input id="Releasetable" style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" <%--onclick="$('#dlg_xzmb').dialog('close'); $('#checkmodel').val('选择模版(' + $('#modellist .checked').length + ')');"--%> value="发布表单" />
                </div>
               

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
                                    tr += "<tr  id =" + i + " name='trname2'><td rowspan='" + num + "'> <input id='YZJ_XW_SA0" + i + "' type='text' maxlength='15' /></td>" +
                                    "<td id = 'SJ_0" +i + "_"+ j +"' class='SJ_0" + j + "'>*日</td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FX_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_FL_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td> <input id='YZJ_XW_SA0" + i + "_BG_0" + j + "' type='text' maxlength='15' /></td>" +
                                    "<td rowspan='" + num + "'><input class='button' id =" + i + " type='button' value='删除' onclick='deletr2(this)'></td></tr>";

                                }
                                else {
                                    tr += "<tr id=" + i + " name='trname2'><td id = 'SJ_0" + i + "_"+ j +"' class='SJ_0" + j + "'>*日</td>" +
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
                //    else if (resjson[i].tb_sw2) { tb_sw2=resjson[i].tb_sw2; 
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
           //夏季：21号青岛海水浴场预报-电视台播出
          //非夏季：22号海洋预报-电视台播出-非游泳季节
          //var strlist = "1号山东省电视台预报单,3号黄河南海堤2006(a4)-3hao,9号预报单(a4),10号预报单(A4)-南堡,11号预报单（a4）,12号预报单(a4),13号预报,14号海上山东（18a4）-gai14,15号预报单（a4）,20号潍坊市海洋预报台专项预报,"
          //              +"22号海洋预报-电视台播出-非游泳季节,25号预报单,东营埕岛油田海洋环境预报,1号72小时山东省电视台预报单,14号72小时预报单,东营广利渔港预报,日照桃花岛预报,潍坊度假区预报,威海新区预报,烟台清泉预报,董家口港预报,东营渔港预报";
         //var strlist = "1号山东省电视台预报单,3号黄河南海堤2006(a4)-3hao,9号预报单(a4),10号预报单(A4)-南堡,11号预报单（a4）,12号预报单(a4),13号预报,14号海上山东（18a4）-gai14,15号预报单（a4）,20号潍坊市海洋预报台专项预报,"
         //               +"21号青岛海水浴场预报-电视台播出,25号预报单,东营埕岛油田海洋环境预报,1号72小时山东省电视台预报单,14号72小时预报单,东营广利渔港预报,日照桃花岛预报,潍坊度假区预报,威海新区预报,烟台清泉预报,董家口港预报,东营渔港预报";
          var strlist = "";
          var date = myformatter(new Date($("#tianbaoriqi").datebox("getValue")));
          if(isVisiable_21(date))//modify by xp 2018-10-9 冬季22号预报单启用期间，21号各海水浴场的预报不需要做了
          {
              strlist = "1号山东省电视台预报单,3号黄河南海堤2006(a4)-3hao,9号预报单(a4),10号预报单(A4)-南堡,11号预报单（a4）,12号预报单(a4),13号预报,14号海上山东（18a4）-gai14,15号预报单（a4）,20号潍坊市海洋预报台专项预报,"
                        +"21号青岛海水浴场预报-电视台播出,25号预报单,东营埕岛油田海洋环境预报,1号72小时山东省电视台预报单,14号72小时预报单,东营广利渔港预报,日照桃花岛预报,潍坊度假区预报,威海新区预报,烟台清泉预报,董家口港预报,东营渔港预报";
          }
          else
          {
              strlist = "1号山东省电视台预报单,3号黄河南海堤2006(a4)-3hao,9号预报单(a4),10号预报单(A4)-南堡,11号预报单（a4）,12号预报单(a4),13号预报,14号海上山东（18a4）-gai14,15号预报单（a4）,20号潍坊市海洋预报台专项预报,"
                        +"22号海洋预报-电视台播出-非游泳季节,25号预报单,东营埕岛油田海洋环境预报,1号72小时山东省电视台预报单,14号72小时预报单,东营广利渔港预报,日照桃花岛预报,潍坊度假区预报,威海新区预报,烟台清泉预报,董家口港预报,东营渔港预报";
          }           
             
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
                case 1: submit_1(id); break;//表单1提交
                case 2: submit_2(id); break;//表单2提交
                case 3: submit_3(id); break;//表单3提交
                case 4: submit_4(id); break;//表单4提交
                case 5: submit_5(id); break;//表单5提交
                case 6: submit_6(id); break;//表单6提交
                //case 7: submit_7(id); break;//表单7提交
                case 8: submit_8(id); break;//表单8提交 原下午四 青岛24小时取消不用
                case 9: submit_9(id); break;//表单9提交
                //case 10: submit_10(id); break;//表单10提交
                case 11: submit_11(id); break;//表单11提交
                //case 12: submit_12(id); break;//表单12提交
                case 13: submit_13(id); break;//表单13提交
                case 14: submit_14(id); break;//表单14提交
                case 15: submit_15(id); break;//表单15提交
                case 16: submit_16(id); break;//表单16提交
                case 17: submit_17(id); break;//表单17提交
                //case 18: submit_18(id); break;//表单18提交
                case 19: submit_19(id); break;//表单19提交
                case 20: submit_20(id); break;//表单20提交
                case 21: submit_21(id); break;//表单21提交
                //case 22: submit_22(id); break;//表单22提交
                case 23: submit_23(id); break;//表单23提交
                case 24: submit_24(id); break;//表单24提交
                case 25: submit_25(id); break;//表单25提交
                //case 26: submit_26(id); break;//表单26提交
                //case 35: submit_35(id); break;//表单35提交 渔政局表2
                case 41: submit_41(id); break;//表单41提交 下午二十一、东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
                //case 42: submit_42(id); break;//表单42提交 下午二十二、 东营埕岛-未来三天高/低潮预报
                case 47: submit_47(id); break;//表单1提交 一、海洋牧场-海浪预报
                //case 48: submit_48(id); break;//表单2提交 二、海洋牧场-潮汐预报
                case 49: submit_49(id); break;//表单3提交 三、海洋牧场-海温预报
                case 53: submit_53(id); break;
                //case 54: submit_54(id); break;//下午二十四、东营广利渔港-未来三天高/低潮预报
                case 55: submit_55(id); break;//下午二十五、东营广利渔港-未来三天的海面风及海浪有效波高预报（20时起报）
                case 56: submit_56(id); break;//下午二十六、东营广利渔港-未来三天的海面水温预报
                //case 57: submit_57(id); break;//下午二十七、日照桃花岛-未来三天高/低潮预报
                case 58: submit_58(id); break;//下午二十八、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）
                case 59: submit_59(id); break;//下午二十九、日照桃花岛-未来三天的海面水温预报
                //case 60: submit_60(id); break;//下午三十、潍坊度假区-未来三天高/低潮预报
                case 61: submit_61(id); break;//下午三十一、潍坊度假区-未来三天的海面风及海浪有效波高预报（20时起报）
                case 62: submit_62(id); break;//下午三十二、潍坊度假区-未来三天的海面水温预报
                //case 63: submit_63(id); break;//下午三十三、威海新区-未来三天高/低潮预报
                case 64: submit_64(id); break;//下午三十四、威海新区-未来三天的海面风及海浪有效波高预报（20时起报）
                case 65: submit_65(id); break;//下午三十五、威海新区-未来三天的海面水温预报
                //case 66: submit_66(id); break;//下午三十六、烟台清泉-未来三天高/低潮预报
                case 67: submit_67(id); break;//下午三十七、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）
                case 68: submit_68(id); break;//下午三十八、烟台清泉-未来三天的海面水温预报
                //case 69: submit_69(id); break;//下午三十九、董家口-未来三天高/低潮预报
                case 70: submit_70(id); break;//下午四十、董家口-未来三天的海面风及海浪有效波高预报（20时起报）
                case 71: submit_71(id); break;//下午四十一、董家口-未来三天的海面水温预报
                //case 72: submit_72(id); break;//下午四十二、东营渔港-未来三天高/低潮预报
                case 73: submit_73(id); break;//下午四十三、东营渔港 - 未来三天的海面风及海浪有效波高预报（20时起报）
                case 74: submit_74(id); break;//下午四十四、东营渔港-未来三天的海面水温预报
                default:
            }
        }

        //保存所有
        function alldlg_Submit() {//（无当天数据保存、有当天数据修改）↓
            //submit_1(1); //表单1提交
            //submit_2(2); //表单2提交
            //submit_3(3); //表单3提交
            //submit_4(4); //表单4提交
            submit_5(5); //表单5提交
            submit_6(6); //表单6提交
            //submit_7(7); //表单7提交
            submit_8(8); //表单8提交 下午四青岛潮汐24小时不用，取数下午三青岛24
            submit_9(9); //表单9提交
            //submit_10(10); //表单10提交
            submit_11(11); //表单11提交
            //submit_12(12); //表单12提交
            submit_13(13); //表单13提交
            submit_14(14); //表单14提交
            submit_15(15); //表单15提交
            //submit_16(16); //表单16提交 下午十二潍坊潮汐取消不用，取数从下午三潍坊
            var date = new Date($("#tianbaoriqi").datebox("getValue"));
            if(isVisiable_21(date))
            {
                submit_17(17); //表单17提交
                //submit_18(18); //表单18提交 下午十一、小麦岛72小时潮汐预报 其中金沙滩取消不用，从下午三青岛三天预报取数
            }                     
            submit_19(19); //表单19提交
           //submit_20(20); //表单20提交 下午五青岛潮汐48小时取消不用 取数下午三青岛48
            submit_21(21); //表单21提交
            //submit_22(22); //表单22提交 下午十五、威海48小时潮汐预报   其中威海两天预报取消不用，从下午三威海中取数
            submit_23(23); //表单23提交
            //submit_24(24); //表单24提交
            //submit_25(25); //表单25提交
            //submit_26(26); //表单26提交
            //submit_35(35);//表单35
            submit_41(41);//表单41下午二十一、东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
            //submit_42(42);//表单42下午二十二、 东营埕岛-未来三天高/低潮预报
            submit_47(47); //表单1提交 一、海洋牧场-海浪预报
            //submit_48(48); //表单2提交 二、海洋牧场-潮汐预报
            submit_49(49); //表单3提交 三、海洋牧场-海温预报
            submit_53(53);
            //submit_54(54);//下午二十四、东营广利渔港-未来三天高/低潮预报
            submit_55(55);//下午二十五、东营广利渔港-未来三天的海面风及海浪有效波高预报（20时起报）
            submit_56(56);//下午二十六、东营广利渔港-未来三天的海面水温预报
            //submit_57(57);//下午二十七、日照桃花岛-未来三天高/低潮预报
            submit_58(58);//下午二十八、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）
            submit_59(59);//下午二十九、日照桃花岛-未来三天的海面水温预报
            //submit_60(60);//下午三十、潍坊度假区-未来三天高/低潮预报
            submit_61(61);//下午三十一、潍坊度假区-未来三天的海面风及海浪有效波高预报（20时起报）
            submit_62(62);//下午三十二、潍坊度假区-未来三天的海面水温预报
            //submit_63(63);//下午三十三、威海新区-未来三天高/低潮预报
            submit_64(64);//下午三十四、威海新区-未来三天的海面风及海浪有效波高预报（20时起报）
            submit_65(65);//下午三十五、威海新区-未来三天的海面水温预报
            //submit_66(66);//下午三十六、烟台清泉-未来三天高/低潮预报
            submit_67(67);//下午三十七、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）
            submit_68(68);//下午三十八、烟台清泉-未来三天的海面水温预报
            //submit_69(69);//下午三十九、董家口-未来三天高/低潮预报
            submit_70(70);//下午四十、董家口-未来三天的海面风及海浪有效波高预报（20时起报）
            submit_71(71);//下午四十一、董家口-未来三天的海面水温预报
            //submit_72(72);//下午四十二、东营渔港-未来三天高/低潮预报
            submit_73(73);//下午四十三、东营渔港 - 未来三天的海面风及海浪有效波高预报（20时起报）
            submit_74(74);//下午四十四、东营渔港-未来三天的海面水温预报
        }

        //表单数据拼接提交 从左至右 从上至下
        { //表单01
            function submit_1(id) {
                var str_data = "";
                var d = new Date();
                var dayCount = 3;
                if (d.getDay() == 1)
                    dayCount = 7;
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#BH_BG_0" + i).val() + ",";
                    str_data += $("#BH_BX_0" + i).val() + ",";
                    str_data += $("#BH_FX_0" + i).val() + ",";
                    str_data += $("#BH_FL_0" + i).val() + ",";
                    str_data += $("#BH_SW_0" + i).val() + ",";
                }
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#HHHG_BG_0" + i).val() + ",";
                    str_data += $("#HHHG_BX_0" + i).val() + ",";
                    str_data += $("#HHHG_FX_0" + i).val() + ",";
                    str_data += $("#HHHG_FL_0" + i).val() + ",";
                    str_data += $("#HHHG_SW_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单02
            function submit_2(id) {
                var str_data = "";
                var d = new Date();
                var dayCount = 3;
                if (d.getDay() == 1)
                    dayCount = 7;
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#LKG_01G_SJ_0" + i).val() + ",";
                    str_data += $("#LKG_01G_CW_0" + i).val() + ",";
                    str_data += $("#LKG_01D_SJ_0" + i).val() + ",";
                    str_data += $("#LKG_01D_CW_0" + i).val() + ",";
                    str_data += $("#LKG_02G_SJ_0" + i).val() + ",";
                    str_data += $("#LKG_02G_CW_0" + i).val() + ",";
                    str_data += $("#LKG_02D_SJ_0" + i).val() + ",";
                    str_data += $("#LKG_02D_CW_0" + i).val() + ",";
                }
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#HHHG_01G_SJ_0" + i).val() + ",";
                    str_data += $("#HHHG_01G_CW_0" + i).val() + ",";
                    str_data += $("#HHHG_01D_SJ_0" + i).val() + ",";  
                    str_data += $("#HHHG_01D_CW_0" + i).val() + ",";
                    str_data += $("#HHHG_02G_SJ_0" + i).val() + ",";
                    str_data += $("#HHHG_02G_CW_0" + i).val() + ",";
                    str_data += $("#HHHG_02D_SJ_0" + i).val() + ",";
                    str_data += $("#HHHG_02D_CW_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单03
            function submit_3(id) {
                var str_data = "";
                str_data += $("#BH_GDF_01").val() + ",";
                str_data += $("#BH_GDE_01").val() + ",";
                str_data += $("#BH_HL_01").val() + ",";
                str_data += $("#HHBB_GDF_01").val() + ",";
                str_data += $("#HHBB_GDE_01").val() + ",";
                str_data += $("#HHBB_HL_01").val() + ",";
                str_data += $("#DKHY_LG_01").val() + ",";
                str_data += $("#DKHY_SW_01").val() + ",";
                str_data += $("#HHK_LG_01").val() + ",";
                str_data += $("#HHK_SW_01").val() + ",";
                str_data += $("#GDG_LG_01").val() + ",";
                str_data += $("#GDG_SW_01").val() + ",";
                str_data += $("#DYG_LG_01").val() + ",";
                str_data += $("#DYG_SW_01").val() + ",";
                str_data += $("#XH_LG_01").val() + ",";
                str_data += $("#XH_SW_01").val() + ",";
                str_data += $("#CK_LG_01").val() + ",";
                str_data += $("#CK_SW_01").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单04
            function submit_4(id) {
                var str_data = "";
                for (var i = 0; i < 4; i++) {
                    switch (i) {
                        case 0: nqyname = "FYT"; break;
                        case 1: nqyname = "GD"; break;
                        case 2: nqyname = "XDH"; break;
                        case 3: nqyname = "DYG"; break;
                        default:
                    }
                    str_data += $("#" + nqyname + "_01G_SJ_01").val() + ",";
                    str_data += $("#" + nqyname + "_01G_CW_01").val() + ",";
                    str_data += $("#" + nqyname + "_01D_SJ_01").val() + ",";
                    str_data += $("#" + nqyname + "_01D_CW_01").val() + ",";
                    str_data += $("#" + nqyname + "_02G_SJ_01").val() + ",";
                    str_data += $("#" + nqyname + "_02G_CW_01").val() + ",";
                    str_data += $("#" + nqyname + "_02D_SJ_01").val() + ",";
                    str_data += $("#" + nqyname + "_02D_CW_01").val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单05
            function submit_5(id) {
                var str_data = "";
                str_data += $("#BH_GDF_401").val() + ",";
                str_data += $("#BH_GDE_401").val() + ",";
                str_data += $("#BH_HL_401").val() + ",";
                str_data += $("#HHBB_GDF_401").val() + ",";
                str_data += $("#HHBB_GDE_401").val() + ",";
                str_data += $("#HHBB_HL_401").val() + ",";
                str_data += $("#HHZB_GDF_401").val() + ",";
                str_data += $("#HHZB_GDE_401").val() + ",";
                str_data += $("#HHZB_HL_401").val() + ",";
                str_data += $("#HHNB_GDF_401").val() + ",";
                str_data += $("#HHNB_GDE_401").val() + ",";
                str_data += $("#HHNB_HL_401").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单06-下午二、山东省近海七市3天海浪、水温预报
            function submit_6(id) {
                var str_data = "";
                for (var i = 0; i < 7; i++) {
                    switch (i) {
                        case 0: nqyname = "RZJH"; break;
                        case 1: nqyname = "QDJH"; break;
                        case 2: nqyname = "WHJH"; break;
                        case 3: nqyname = "YTJH"; break;
                        case 4: nqyname = "WFJH"; break;
                        case 5: nqyname = "DYJH"; break;
                        case 6: nqyname = "BZJH"; break;
                        default:
                    }
                    str_data += $("#" + nqyname + "_LG_24h").val() + ",";
                    str_data += $("#" + nqyname + "_LG_48h").val() + ",";
                    str_data += $("#" + nqyname + "_LG_72h").val() + ",";
                    str_data += $("#" + nqyname + "_BCSW_24h").val() + ",";
                    str_data += $("#" + nqyname + "_BCSW_48h").val() + ",";
                    str_data += $("#" + nqyname + "_BCSW_72h").val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //下午三、潮汐数据潮时提交
            function submit_7(id) {
                var str_data = "";
                for (var i = 0; i < 7; i++) {
                    for (var j = 1; j < 4; j++) {
                         switch (i) {
                            case 0: nqyname = "RZ"; break;
                            case 1: nqyname = "QD"; break;
                            case 2: nqyname = "WH"; break;
                            case 3: nqyname = "YT"; break;
                            case 4: nqyname = "WF"; break;
                            case 5: nqyname = "DY"; break;
                            case 6: nqyname = "BZ"; break;
                            default:
                        }
                        str_data += $("#" + nqyname + "_01G_H_00" + j).val() + ",";
                        //str_data += $("#" + nqyname + "_01G_MIN").val() + ",";
                        str_data += $("#" + nqyname + "_02G_H_00" + j).val() + ",";
                        //str_data += $("#" + nqyname + "_02G_MIN").val() + ",";
                        str_data += $("#" + nqyname + "_01D_H_00" + j).val() + ",";
                        //str_data += $("#" + nqyname + "_01D_MIN").val() + ",";
                        str_data += $("#" + nqyname + "_02D_H_00" + j).val() + ",";
                        //str_data += $("#" + nqyname + "_02D_MIN").val() + ",";
                    }
                }

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
                submit_46();//提交潮高数据
            }

            //下午三、潮汐数据潮高提交
            function submit_46() {
                 var str_data = "";
                for (var i = 0; i < 7; i++) {
                    for (var j = 1; j < 4; j++) {
                        switch (i) {
                            case 0: nqyname = "RZ"; break;
                            case 1: nqyname = "QD"; break;
                            case 2: nqyname = "WH"; break;
                            case 3: nqyname = "YT"; break;
                            case 4: nqyname = "WF"; break;
                            case 5: nqyname = "DY"; break;
                            case 6: nqyname = "BZ"; break;
                            default:
                        }
                        str_data += $("#" + nqyname + "_01G_CG_00" + j).val() + ",";
                        str_data += $("#" + nqyname + "_02G_CG_00" + j).val() + ",";
                        str_data += $("#" + nqyname + "_01D_CG_00" + j).val() + ",";
                        str_data += $("#" + nqyname + "_02D_CG_00" + j).val() + ",";
                    }
                }

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax("46", data);
            }

            //表单08
            function submit_8(id) {
                var str_data = "";
                //str_data += $("#01GC_H").val() + ",";
                //str_data += $("#01GC_MIN").val() + ",";
                //str_data += $("#01GC_CM").val() + ",";
                //str_data += $("#01DC_H").val() + ",";
                //str_data += $("#01DC_MIN").val() + ",";
                //str_data += $("#01DC_CM").val() + ",";
                //str_data += $("#02GC_H").val() + ",";
                //str_data += $("#02GC_MIN").val() + ",";
                //str_data += $("#02GC_CM").val() + ",";
                //str_data += $("#02DC_H").val() + ",";
                //str_data += $("#02DC_MIN").val() + ",";
                //str_data += $("#02DC_CM").val() + ",";
                str_data += $("#MRHBLG").val() + ",";
                str_data += $("#MRHBLX").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单09
            function submit_9(id) {
                var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#BG_0" + i).val() + ",";
                    str_data += $("#BX_0" + i).val() + ",";
                    str_data += $("#FX_0" + i).val() + ",";
                    str_data += $("#FL_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单10
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

            //表单11
            function submit_11(id) {
                var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#NBYT_BG_0" + i).val() + ",";
                    str_data += $("#NBYT_BX_0" + i).val() + ",";
                    str_data += $("#NBYT_FX_0" + i).val() + ",";
                    str_data += $("#NBYT_FL_0" + i).val() + ",";
                    str_data += $("#NBYT_SW_0" + i).val() + ",";
                    str_data += $("#NBYT_TQ_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单12
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

            //表单13
            function submit_13(id) {
                var str_data = "";
                str_data += $("#BH13_BG").val() + ",";
                str_data += $("#BH13_BX").val() + ",";
                str_data += $("#BH13_YX").val() + ",";
                str_data += $("#HHBB_BG").val() + ",";
                str_data += $("#HHBB_BX").val() + ",";
                str_data += $("#HHBB_YX").val() + ",";
                str_data += $("#HHZB_BG").val() + ",";
                str_data += $("#HHZB_BX").val() + ",";
                str_data += $("#HHZB_YX").val() + ",";
                str_data += $("#HHNB_BG").val() + ",";
                str_data += $("#HHNB_BX").val() + ",";
                str_data += $("#HHNB_YX").val() + ",";
                str_data += $("#QDJA_BG").val() + ",";
                str_data += $("#QDJA_BX").val() + ",";
                str_data += $("#QDJA_YX").val() + ",";
                str_data += $("#BH13_BZ").val() + ",";
                str_data += $("#HHBB_BZ").val() + ",";
                str_data += $("#HHZB_BZ").val() + ",";
                str_data += $("#HHNB_BZ").val() + ",";
                str_data += $("#QDJA_BZ").val() + ",";
                //str_data += $("#BH13_HL").val() + ",";
                //str_data += $("#HHBB_HL").val() + ",";
                //str_data += $("#HHZB_HL").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单14
            function submit_14(id) {
                var str_data = "";
                str_data += $("#BH14_BG").val() + ",";
                str_data += $("#BH14_BX").val() + ",";
                str_data += $("#BH14_YX").val() + ",";
                str_data += $("#HHBB14_BG").val() + ",";
                str_data += $("#HHBB14_BX").val() + ",";
                str_data += $("#HHBB14_YX").val() + ",";
                str_data += $("#HHZB14_BG").val() + ",";
                str_data += $("#HHZB14_BX").val() + ",";
                str_data += $("#HHZB14_YX").val() + ",";
                str_data += $("#HHNB14_BG").val() + ",";
                str_data += $("#HHNB14_BX").val() + ",";
                str_data += $("#HHNB14_YX").val() + ",";
                str_data += $("#BH14_BZ").val() + ",";
                str_data += $("#HHBB14_BZ").val() + ",";
                str_data += $("#HHZB14_BZ").val() + ",";
                str_data += $("#HHNB14_BZ").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单15
            function submit_15(id) {
                var str_data = "";
                str_data += $("#BH15_BG").val() + ",";
                str_data += $("#BH15_BX").val() + ",";
                str_data += $("#BH15_YX").val() + ",";
                str_data += $("#HHBB15_BG").val() + ",";
                str_data += $("#HHBB15_BX").val() + ",";
                str_data += $("#HHBB15_YX").val() + ",";
                str_data += $("#HHZB15_BG").val() + ",";
                str_data += $("#HHZB15_BX").val() + ",";
                str_data += $("#HHZB15_YX").val() + ",";
                str_data += $("#HHNB15_BG").val() + ",";
                str_data += $("#HHNB15_BX").val() + ",";
                str_data += $("#HHNB15_YX").val() + ",";
                str_data += $("#BH15_BZ").val() + ",";
                str_data += $("#HHBB15_BZ").val() + ",";
                str_data += $("#HHZB15_BZ").val() + ",";
                str_data += $("#HHNB15_BZ").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单16
            function submit_16(id) {
                var str_data = "";
                str_data += $("#WFG_GCCS_01").val() + ",";
                str_data += $("#WFG_GCCG_01").val() + ",";
                str_data += $("#WFG_GCCS_02").val() + ",";
                str_data += $("#WFG_GCCG_02").val() + ",";
                str_data += $("#WFG_DCCS_01").val() + ",";
                str_data += $("#WFG_DCCG_01").val() + ",";
                str_data += $("#WFG_DCCS_02").val() + ",";
                str_data += $("#WFG_DCCG_02").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单17
            function submit_17(id) {
                var str_data = "";
                str_data += $("#DYHSYC_LG").val() + ",";
                str_data += $("#DYHSYC_SW").val() + ",";
                str_data += $("#DYHSYC_YY").val() + ",";
                str_data += $("#DLHSYC_LG").val() + ",";
                str_data += $("#DLHSYC_SW").val() + ",";
                str_data += $("#DLHSYC_YY").val() + ",";
                str_data += $("#SLRHSYC_LG").val() + ",";
                str_data += $("#SLRHSYC_SW").val() + ",";
                str_data += $("#SLRHSYC_YY").val() + ",";
                str_data += $("#JSTHSYC_LG").val() + ",";
                str_data += $("#JSTHSYC_SW").val() + ",";
                str_data += $("#JSTHSYC_YY").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            function submit_18(id) {
                var str_data = "";
                for (var i = 0; i < 2; i++) {
                    switch (i) {
                        //case 1: nqyname = "WMT"; break;
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
                //alert(str_data);
                submit_ajax(id, data);
            }

            //表单19
            function submit_19(id) {
                var str_data = "";
                str_data += $("#QDJH_LG19").val() + ",";
                str_data += $("#QDJH_SW19").val() + ",";
                str_data += $("#JMJH_LG19").val() + ",";
                str_data += $("#JMJH_SW19").val() + ",";
                str_data += $("#JZJH_LG19").val() + ",";
                str_data += $("#JZJH_SW19").val() + ",";
                str_data += $("#JNJH_LG19").val() + ",";
                str_data += $("#JNJH_SW19").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单20
            function submit_20(id) {
                var str_data = "";
                str_data += $("#01GC_H20").val() + ",";
                str_data += $("#01GC_MIN20").val() + ",";
                str_data += $("#01GC_CM20").val() + ",";
                str_data += $("#02GC_H20").val() + ",";
                str_data += $("#02GC_MIN20").val() + ",";
                str_data += $("#02GC_CM20").val() + ",";
                str_data += $("#01DC_H20").val() + ",";
                str_data += $("#01DC_MIN20").val() + ",";
                str_data += $("#01DC_CM20").val() + ",";
                str_data += $("#02DC_H20").val() + ",";
                str_data += $("#02DC_MIN20").val() + ",";
                str_data += $("#02DC_CM20").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单21
            function submit_21(id) {
                var str_data = "";
                if (type == "fl") {
                    str_data += $("#SDJH_LG_24").val() + ",";
                    str_data += $("#SDJH_SW_24").val() + ",";
                    str_data += $("#WHJH_LG_48").val() + ",";
                    str_data += $("#WHJH_SW_48").val() + ",";
                    str_data += $("#SDJH_LG_48").val() + ",";
                    str_data += $("#SDJH_SW_48").val() + ",";
                    str_data += $("#WDQ_LG_24").val() + ",";
                    str_data += $("#WDQ_SW_24").val() + ",";
                    str_data += $("#CST_LG_24").val() + ",";
                    str_data += $("#CST_SW_24").val() + ",";
                    str_data += $("#RSS_LG_24").val() + ",";
                    str_data += $("#RSS_SW_24").val() + ",";
                    str_data += $("#WDQ_LG_48").val() + ",";
                    str_data += $("#WDQ_SW_48").val() + ",";
                    str_data += $("#CST_LG_48").val() + ",";
                    str_data += $("#CST_SW_48").val() + ",";
                    str_data += $("#RSS_LG_48").val() + ",";
                    str_data += $("#RSS_SW_48").val() + ",";

                }
                else {
                    str_data += $("#SDJH_LG_24_1").val() + ",";
                    str_data += $("#SDJH_SW_24_1").val() + ",";
                    str_data += $("#WHJH_LG_48_1").val() + ",";
                    str_data += $("#WHJH_SW_48_1").val() + ",";
                    str_data += $("#SDJH_LG_48_1").val() + ",";
                    str_data += $("#SDJH_SW_48_1").val() + ",";
                    str_data += $("#WDQ_LG_24_1").val() + ",";
                    str_data += $("#WDQ_SW_24_1").val() + ",";
                    str_data += $("#CST_LG_24_1").val() + ",";
                    str_data += $("#CST_SW_24_1").val() + ",";
                    str_data += $("#RSS_LG_24_1").val() + ",";
                    str_data += $("#RSS_SW_24_1").val() + ",";
                    str_data += $("#WDQ_LG_48_1").val() + ",";
                    str_data += $("#WDQ_SW_48_1").val() + ",";
                    str_data += $("#CST_LG_48_1").val() + ",";
                    str_data += $("#CST_SW_48_1").val() + ",";
                    str_data += $("#RSS_LG_48_1").val() + ",";
                    str_data += $("#RSS_SW_48_1").val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单22
            function submit_22(id) {
                var str_data = "";
                for (var i = 0; i < 5; i++) {
                    switch (i) {
                        //case 4: nqyname = "WH"; break;
                        case 2: nqyname = "SD"; break;
                        case 1: nqyname = "WD"; break;
                        case 3: nqyname = "CST"; break;
                        case 0: nqyname = "RS"; break;
                        default:
                    }

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
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(id, data);
            }

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

            //表单24
            function submit_24(id) {
                var str_data = "";
                str_data += $("#LDW_ICE_MAXAREA").val() + ",";
                str_data += $("#LDW_ICE_COMMONTHICKNESS").val() + ",";
                str_data += $("#LDW_ICE_MAXTHICKNESS").val() + ",";
                str_data += $("#BHW_ICE_MAXAREA").val() + ",";
                str_data += $("#BHW_ICE_COMMONTHICKNESS").val() + ",";
                str_data += $("#BHW_ICE_MAXTHICKNESS").val() + ",";
                str_data += $("#LZW_ICE_MAXAREA").val() + ",";
                str_data += $("#LZW_ICE_COMMONTHICKNESS").val() + ",";
                str_data += $("#LZW_ICE_MAXTHICKNESS").val() + ",";
                str_data += $("#NHH_ICE_MAXAREA").val() + ",";
                str_data += $("#NHH_ICE_COMMONTHICKNESS").val() + ",";
                str_data += $("#NHH_ICE_MAXTHICKNESS").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(id, data);
            }

            //表单25
            function submit_25(id) {
                var str_data = "";
                str_data += $("#ZHC_MO_QD_TX_01").val() + ",";
                str_data += $("#ZHC_MO_QD_FX_01").val() + ",";
                str_data += $("#ZHC_MO_QD_FL_01").val() + ",";
                str_data += ",";
               // str_data += $("#ZHC_MO_QD_SW_01").val() + ",";//青岛WAVEDIRECTION字段存水温
                //str_data += $("#ZHC_MO_QDJH_BG_01").val() + ",";
                str_data += $("#ZHC_MO_QD_BX_01").val() + ",";

                str_data += $("#ZHC_MO_QDJH_TX_01").val() + ",";
                str_data += $("#ZHC_MO_QDJH_FX_01").val() + ",";
                str_data += $("#ZHC_MO_QDJH_FL_01").val() + ",";
                str_data += $("#ZHC_MO_QDJH_BG_01").val() + ",";
                str_data += $("#ZHC_MO_QDJH_BX_01").val() + ",";

                str_data += $("#ZHC_MO_BH_TX_01").val() + ",";
                str_data += $("#ZHC_MO_BH_FX_01").val() + ",";
                str_data += $("#ZHC_MO_BH_FL_01").val() + ",";
                str_data += $("#ZHC_MO_BH_BG_01").val() + ",";
                str_data += $("#ZHC_MO_BH_BX_01").val() + ",";

                str_data += $("#ZHC_MO_BHHX_TX_01").val() + ",";
                str_data += $("#ZHC_MO_BHHX_FX_01").val() + ",";
                str_data += $("#ZHC_MO_BHHX_FL_01").val() + ",";
                str_data += $("#ZHC_MO_BHHX_BG_01").val() + ",";
                str_data += $("#ZHC_MO_BHHX_BX_01").val() + ",";

                str_data += $("#ZHC_MO_NHH_TX_01").val() + ",";
                str_data += $("#ZHC_MO_NHH_FX_01").val() + ",";
                str_data += $("#ZHC_MO_NHH_FL_01").val() + ",";
                str_data += $("#ZHC_MO_NHH_BG_01").val() + ",";
                str_data += $("#ZHC_MO_NHH_BX_01").val() + ",";

                str_data += $("#ZHC_MO_MHH_TX_01").val() + ",";
                str_data += $("#ZHC_MO_MHH_FX_01").val() + ",";
                str_data += $("#ZHC_MO_MHH_FL_01").val() + ",";
                str_data += $("#ZHC_MO_MHH_BG_01").val() + ",";
                str_data += $("#ZHC_MO_MHH_BX_01").val() + ",";

                str_data += $("#ZHC_MO_SHH_TX_01").val() + ",";
                str_data += $("#ZHC_MO_SHH_FX_01").val() + ",";
                str_data += $("#ZHC_MO_SHH_FL_01").val() + ",";
                str_data += $("#ZHC_MO_SHH_BG_01").val() + ",";
                str_data += $("#ZHC_MO_SHH_BX_01").val() + ",";
                //=============================================

                str_data += $("#ZHC_MO_BH_TX_02").val() + ",";
                str_data += $("#ZHC_MO_BH_FX_02").val() + ",";
                str_data += $("#ZHC_MO_BH_FL_02").val() + ",";
                str_data += $("#ZHC_MO_BH_BG_02").val() + ",";
                str_data += $("#ZHC_MO_BH_BX_02").val() + ",";

                str_data += $("#ZHC_MO_NHH_TX_02").val() + ",";
                str_data += $("#ZHC_MO_NHH_FX_02").val() + ",";
                str_data += $("#ZHC_MO_NHH_FL_02").val() + ",";
                str_data += $("#ZHC_MO_NHH_BG_02").val() + ",";
                str_data += $("#ZHC_MO_NHH_BX_02").val() + ",";

                str_data += $("#ZHC_MO_MHH_TX_02").val() + ",";
                str_data += $("#ZHC_MO_MHH_FX_02").val() + ",";
                str_data += $("#ZHC_MO_MHH_FL_02").val() + ",";
                str_data += $("#ZHC_MO_MHH_BG_02").val() + ",";
                str_data += $("#ZHC_MO_MHH_BX_02").val() + ",";

                str_data += $("#ZHC_MO_SHH_TX_02").val() + ",";
                str_data += $("#ZHC_MO_SHH_FX_02").val() + ",";
                str_data += $("#ZHC_MO_SHH_FL_02").val() + ",";
                str_data += $("#ZHC_MO_SHH_BG_02").val() + ",";
                str_data += $("#ZHC_MO_SHH_BX_02").val() + ",";
                //==============================================

                str_data += $("#ZHC_MO_BH_TX_03").val() + ",";
                str_data += $("#ZHC_MO_BH_FX_03").val() + ",";
                str_data += $("#ZHC_MO_BH_FL_03").val() + ",";
                str_data += $("#ZHC_MO_BH_BG_03").val() + ",";
                str_data += $("#ZHC_MO_BH_BX_03").val() + ",";

                str_data += $("#ZHC_MO_NHH_TX_03").val() + ",";
                str_data += $("#ZHC_MO_NHH_FX_03").val() + ",";
                str_data += $("#ZHC_MO_NHH_FL_03").val() + ",";
                str_data += $("#ZHC_MO_NHH_BG_03").val() + ",";
                str_data += $("#ZHC_MO_NHH_BX_03").val() + ",";

                str_data += $("#ZHC_MO_MHH_TX_03").val() + ",";
                str_data += $("#ZHC_MO_MHH_FX_03").val() + ",";
                str_data += $("#ZHC_MO_MHH_FL_03").val() + ",";
                str_data += $("#ZHC_MO_MHH_BG_03").val() + ",";
                str_data += $("#ZHC_MO_MHH_BX_03").val() + ",";

                str_data += $("#ZHC_MO_SHH_TX_03").val() + ",";
                str_data += $("#ZHC_MO_SHH_FX_03").val() + ",";
                str_data += $("#ZHC_MO_SHH_FL_03").val() + ",";
                str_data += $("#ZHC_MO_SHH_BG_03").val() + ",";
                str_data += $("#ZHC_MO_SHH_BX_03").val();

                //str_data = str_data.substring(0, str_data.length - 1);
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

            //表单42 下午二十二、 东营埕岛-未来三天高/低潮预报
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
                //alert(str_data);
                submit_ajax(id, data);
                submitDYForeastNo();
            }

            //表单43  东营埕岛预报编号
            function submitDYForeastNo() {
                var str_data = "";
                var proyear = $('#ProYear').val() + ",";
                var prono = $('#ProNo').val()+ ",";;
                //if (prono * 1 < 10) {
                //    prono = "00" + prono + ",";
                //} else if (prono * 1 > 10 && prono * 1 < 100) {
                //    prono = "0" + prono + ",";
                //} else {
                //    prono = prono + ",";
                //}
                str_data = proyear+prono;
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(43, data);
            }

            //一、海洋牧场-海浪预报
            function submit_47(id) {

                var str_data = "";
                var d = new Date();

                var cq1 = $("#WAVE_CQ_24_D").val();
                var cq2 = $("#WAVE_CQ_24_N").val();
                var cq3 = $("#WAVE_CQ_48_D").val();
                var cq4 = $("#WAVE_CQ_48_N").val();
                var cq5 = $("#WAVE_CQ_72_D").val();
                var cq6 = $("#WAVE_CQ_72_N").val();

                var rc1 = $("#WAVE_RC_24_D").val();
                var rc2 = $("#WAVE_RC_24_N").val();
                var rc3 = $("#WAVE_RC_48_D").val();
                var rc4 = $("#WAVE_RC_48_N").val();
                var rc5 = $("#WAVE_RC_72_D").val();
                var rc6 = $("#WAVE_RC_72_N").val();

                var xxk1 = $("#WAVE_XXK_24_D").val();
                var xxk2 = $("#WAVE_XXK_24_N").val();
                var xxk3 = $("#WAVE_XXK_48_D").val();
                var xxk4 = $("#WAVE_XXK_48_N").val();
                var xxk5 = $("#WAVE_XXK_72_D").val();
                var xxk6 = $("#WAVE_XXK_72_N").val();

                //==============add by Lian Start=======================//
                //滨州正海底播型海洋牧场（BZZH）
                var bzzh1 = $("#WAVE_BZZH_24_D").val();
                var bzzh2 = $("#WAVE_BZZH_24_N").val();
                var bzzh3 = $("#WAVE_BZZH_48_D").val();
                var bzzh4 = $("#WAVE_BZZH_48_N").val();
                var bzzh5 = $("#WAVE_BZZH_72_D").val();
                var bzzh6 = $("#WAVE_BZZH_72_N").val();

                //山东通和底播型海洋牧场（SDTH）
                var sdth1 = $("#WAVE_SDTH_24_D").val();
                var sdth2 = $("#WAVE_SDTH_24_N").val();
                var sdth3 = $("#WAVE_SDTH_48_D").val();
                var sdth4 = $("#WAVE_SDTH_48_N").val();
                var sdth5 = $("#WAVE_SDTH_72_D").val();
                var sdth6 = $("#WAVE_SDTH_72_N").val();

                //山东莱州太平湾明波国家级海洋牧场（TPW）
                var tpw1 = $("#WAVE_TPW_24_D").val();
                var tpw2 = $("#WAVE_TPW_24_N").val();
                var tpw3 = $("#WAVE_TPW_48_D").val();
                var tpw4 = $("#WAVE_TPW_48_N").val();
                var tpw5 = $("#WAVE_TPW_72_D").val();
                var tpw6 = $("#WAVE_TPW_72_N").val();

                //山东琵琶口富瀚国家级海洋牧场（PPK）
                var ppk1 = $("#WAVE_PPK_24_D").val();
                var ppk2 = $("#WAVE_PPK_24_N").val();
                var ppk3 = $("#WAVE_PPK_48_D").val();
                var ppk4 = $("#WAVE_PPK_48_N").val();
                var ppk5 = $("#WAVE_PPK_72_D").val();
                var ppk6 = $("#WAVE_PPK_72_N").val();

                //山东庙岛群岛东部佳益国家级海洋牧场（MDQD）
                var mdqd1 = $("#WAVE_MDQD_24_D").val();
                var mdqd2 = $("#WAVE_MDQD_24_N").val();
                var mdqd3 = $("#WAVE_MDQD_48_D").val();
                var mdqd4 = $("#WAVE_MDQD_48_N").val();
                var mdqd5 = $("#WAVE_MDQD_72_D").val();
                var mdqd6 = $("#WAVE_MDQD_72_N").val();

                //山东海州湾顺风国家级海洋牧场（HZW）
                var hzw1 = $("#WAVE_HZW_24_D").val();
                var hzw2 = $("#WAVE_HZW_24_N").val();
                var hzw3 = $("#WAVE_HZW_48_D").val();
                var hzw4 = $("#WAVE_HZW_48_N").val();
                var hzw5 = $("#WAVE_HZW_72_D").val();
                var hzw6 = $("#WAVE_HZW_72_N").val();

                //山东岚山东部万泽丰国家级海洋牧场（LSDBWZF）
                var lsdbwzf1 = $("#WAVE_LSDBWZF_24_D").val();
                var lsdbwzf2 = $("#WAVE_LSDBWZF_24_N").val();
                var lsdbwzf3 = $("#WAVE_LSDBWZF_48_D").val();
                var lsdbwzf4 = $("#WAVE_LSDBWZF_48_N").val();
                var lsdbwzf5 = $("#WAVE_LSDBWZF_72_D").val();
                var lsdbwzf6 = $("#WAVE_LSDBWZF_72_N").val();
                //================add by Lian End=====================//


                /*if(cq1 == "" && cq2 == "" &&cq3 == "" &&cq4 == "" &&cq5 == "" &&cq6 == "" &&cq1 == "" &&cq2 == "" &&cq3 == "" &&cq4 == "" &&cq5 == "" &&cq6 == "" &&cq1 == "" &&cq2 == "" &&cq3 == "" &&cq4 == "" &&cq5 == "" &&cq6 == ""){
                    return false;
                }*/

                if(cq1 == "" && cq2 == "" &&cq3 == "" &&cq4 == "" &&cq5 == "" &&cq6 == "" &&rc1 == "" &&rc2 == "" &&rc3 == "" &&rc4 == "" &&rc5 == "" &&rc6 == "" &&xxk1 == "" &&xxk2 == "" &&xxk3 == "" &&xxk4 == "" &&xxk5 == "" &&xxk6 == "" && bzzh1 ==""&& bzzh2 ==""&& bzzh3 ==""&& bzzh4 ==""&& bzzh5 ==""&& bzzh6 =="" && sdth1 == ""&& sdth2 == ""&& sdth3 == ""&& sdth4 == ""&& sdth5 == ""&& sdth6 == "" && tpw1 == ""&& tpw2 == ""&& tpw3 == ""&& tpw4 == ""&& tpw5 == ""&& tpw6 == "" && ppk1 == "" && ppk2 == ""&& ppk3 == ""&& ppk4 == ""&& ppk5 == ""&& ppk6 == "" && mdqd1 == ""&& mdqd2 == ""&& mdqd3 == ""&& mdqd4 == ""&& mdqd5 == ""&& mdqd6 == "" && hzw1 == ""&& hzw2 == ""&& hzw3 == ""&& hzw4 == ""&& hzw5 == ""&& hzw6 == "" && lsdbwzf1 == ""&& lsdbwzf2 == ""&& lsdbwzf3 == ""&& lsdbwzf4 == ""&& lsdbwzf5 == ""&& lsdbwzf6 == ""){
                    return false;
                }

                //str_data += $("#WAVE_CQ_FORCASTDATE").val() + ",";
                str_data += cq1 + ",";
                str_data += cq2 + ",";
                str_data += cq3 + ",";
                str_data += cq4 + ",";
                str_data += cq5 + ",";
                str_data += cq6 + ",";

                //str_data += $("#WAVE_RC_FORCASTDATE").val() + ",";
                str_data += rc1 + ",";
                str_data += rc2 + ",";
                str_data += rc3 + ",";
                str_data += rc4 + ",";
                str_data += rc5 + ",";
                str_data += rc6 + ",";

                //str_data += $("#WAVE_XXK_FORCASTDATE").val() + ",";
                str_data += xxk1 + ",";
                str_data += xxk2 + ",";
                str_data += xxk3 + ",";
                str_data += xxk4 + ",";
                str_data += xxk5 + ",";
                str_data += xxk6 + ",";

                //=========================//
                //新增7个海洋牧场的字符串拼接 start
                str_data += bzzh1 + ",";
                str_data += bzzh2 + ",";
                str_data += bzzh3 + ",";
                str_data += bzzh4 + ",";
                str_data += bzzh5 + ",";
                str_data += bzzh6 + ",";

                str_data += sdth1 + ",";
                str_data += sdth2 + ",";
                str_data += sdth3 + ",";
                str_data += sdth4 + ",";
                str_data += sdth5 + ",";
                str_data += sdth6 + ",";

                str_data += tpw1 + ",";
                str_data += tpw2 + ",";
                str_data += tpw3 + ",";
                str_data += tpw4 + ",";
                str_data += tpw5 + ",";
                str_data += tpw6 + ",";

                str_data += ppk1 + ",";
                str_data += ppk2 + ",";
                str_data += ppk3 + ",";
                str_data += ppk4 + ",";
                str_data += ppk5 + ",";
                str_data += ppk6 + ",";

                str_data += mdqd1 + ",";
                str_data += mdqd2 + ",";
                str_data += mdqd3 + ",";
                str_data += mdqd4 + ",";
                str_data += mdqd5 + ",";
                str_data += mdqd6 + ",";

                str_data += hzw1 + ",";
                str_data += hzw2 + ",";
                str_data += hzw3 + ",";
                str_data += hzw4 + ",";
                str_data += hzw5 + ",";
                str_data += hzw6 + ",";

                str_data += lsdbwzf1 + ",";
                str_data += lsdbwzf2 + ",";
                str_data += lsdbwzf3 + ",";
                str_data += lsdbwzf4 + ",";
                str_data += lsdbwzf5 + ",";
                str_data += lsdbwzf6 + ",";

                //新增7个海洋牧场的字符串拼接 end

               
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //二、海洋牧场-潮汐预报
            //海洋牧场新增7个预报地区
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
                                str_data += $("#TIDE_" + nqyname + "_0" + k  + "_H" + i).val() + ",";
                            }
                        }
                        str_data += $("#TIDE_" + nqyname  + "_0" + k + "_FIRSTHTIME").val() + ",";
                        str_data += $("#TIDE_" + nqyname  + "_0" + k + "_FIRSTHHEIGHT").val() + ",";

                        str_data += $("#TIDE_" + nqyname  + "_0" + k + "_SECONDHTIME").val() + ",";
                        str_data += $("#TIDE_" + nqyname  + "_0" + k + "_SECONDHHEIGHT").val() + ",";

                        str_data += $("#TIDE_" + nqyname  + "_0" + k + "_FIRSTLTIME").val() + ",";
                        str_data += $("#TIDE_" + nqyname  + "_0" + k + "_FIRSTLHEIGHT").val() + ",";

                        str_data += $("#TIDE_" + nqyname  + "_0" + k + "_SECONDLTIME").val() + ",";
                        str_data += $("#TIDE_" + nqyname  + "_0" + k + "_SECONDLHEIGHT").val() + ",";
                    }
                }

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //三、海洋牧场-海温预报
            //新增七个海洋牧场的预报制作
           function submit_49(id) {
                var str_data = "";
                var d = new Date();
                
                var cq1 = $("#TEMP_CQ_24").val();
                var cq2= $("#TEMP_CQ_48").val();
                var cq3 = $("#TEMP_CQ_72").val();

                var rc1 = $("#TEMP_RC_24").val(); 
                var rc2 = $("#TEMP_RC_48").val();
                var rc3 = $("#TEMP_RC_72").val();

                var xxk1 = $("#TEMP_XXK_24").val();
                var xxk2 = $("#TEMP_XXK_48").val();
                var xxk3 = $("#TEMP_XXK_72").val();

               //=================================//
                var bzzh1 = $("#TEMP_BZZH_24").val();
                var bzzh2 = $("#TEMP_BZZH_48").val();
                var bzzh3 = $("#TEMP_BZZH_72").val();

                var sdth1 = $("#TEMP_SDTH_24").val();
                var sdth2 = $("#TEMP_SDTH_48").val();
                var sdth3 = $("#TEMP_SDTH_72").val();

                var tpw1 = $("#TEMP_TPW_24").val();
                var tpw2 = $("#TEMP_TPW_48").val();
                var tpw3 = $("#TEMP_TPW_72").val();

                var ppk1 = $("#TEMP_PPK_24").val();
                var ppk2 = $("#TEMP_PPK_48").val();
                var ppk3 = $("#TEMP_PPK_72").val();

                var mdqd1 = $("#TEMP_MDQD_24").val();
                var mdqd2 = $("#TEMP_MDQD_48").val();
                var mdqd3 = $("#TEMP_MDQD_72").val();

                var hzw1 = $("#TEMP_HZW_24").val();
                var hzw2 = $("#TEMP_HZW_48").val();
                var hzw3 = $("#TEMP_HZW_72").val();

                var lsdbwzf1 = $("#TEMP_LSDBWZF_24").val();
                var lsdbwzf2 = $("#TEMP_LSDBWZF_48").val();
                var lsdbwzf3 = $("#TEMP_LSDBWZF_72").val();

               //=================================//

                if (cq1 == "" && cq2 == "" && cq3 == "" && rc1 == "" && rc2 == "" && rc3 == "" && xxk1 == "" && xxk2 == "" && xxk3 == "" && bzzh1 == "" && bzzh2 == "" && bzzh3 == ""&& sdth1 == ""&& sdth2 == ""&& sdth3 == "" &&tpw1 == "" && tpw2 == "" && tpw3 == "" &&ppk1 == "" && ppk2 == "" && ppk3 =="" &&mdqd1 == "" && mdqd2 == "" && mdqd3=="" && hzw1 == "" && hzw2 == "" && hzw3 == "" && lsdbwzf1 == "" && lsdbwzf2 == "" && lsdbwzf3 == "") {
                    return false;
                }
                //str_data += $("#TEMP_CQ_FORCASTDATE").val() + ",";
                str_data +=  cq1 + ",";
                str_data +=  cq2 + ",";
                str_data +=  cq3 + ",";

                //str_data += $("#TEMP_RC_FORCASTDATE").val() + ",";
                str_data +=  rc1 + ",";
                str_data +=  rc2 + ",";
                str_data +=  rc3 + ",";

                //str_data += $("#TEMP_XXK_FORCASTDATE").val() + ",";
                str_data += xxk1 + ",";
                str_data += xxk2 + ",";
                str_data += xxk3 + ",";

               //==========================//

                str_data += bzzh1 + ",";
                str_data += bzzh2 + ",";
                str_data += bzzh3 + ",";

                str_data += sdth1 + ",";
                str_data += sdth2 + ",";
                str_data += sdth3 + ",";

                str_data += tpw1 + ",";
                str_data += tpw2 + ",";
                str_data += tpw3 + ",";

                str_data += ppk1 + ",";
                str_data += ppk2 + ",";
                str_data += ppk3 + ",";

                str_data += mdqd1 + ",";
                str_data += mdqd2 + ",";
                str_data += mdqd3 + ",";

                str_data += hzw1 + ",";
                str_data += hzw2 + ",";
                str_data += hzw3 + ",";

                str_data += lsdbwzf1 + ",";
                str_data += lsdbwzf2 + ",";
                str_data += lsdbwzf3 + ",";

               //==========================//

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

           function submit_53(id) {
               var str_data = "";
                var proyear = $('#ProSDYear').val() + ",";
                var prono = $('#ProSDNo').val()+ ",";
                str_data = proyear+prono;
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(id, data);
           }

           //七地市
           //潮汐
           //下午二十四、东营广利渔港-未来三天高/低潮预报
           function submit_54(id) {
               var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#DYGL_01G_CS_0" + i).val() + ",";
                    str_data += $("#DYGL_01G_CG_0" + i).val() + ",";
                    str_data += $("#DYGL_02G_CS_0" + i).val() + ",";
                    str_data += $("#DYGL_02G_CG_0" + i).val() + ",";
                    str_data += $("#DYGL_01D_CS_0" + i).val() + ",";
                    str_data += $("#DYGL_01D_CG_0" + i).val() + ",";
                    str_data += $("#DYGL_02D_CS_0" + i).val() + ",";
                    str_data += $("#DYGL_02D_CG_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
           }
           
            //下午二十七、日照桃花岛-未来三天高/低潮预报
            function submit_57(id) {
               var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#RZTHD_01G_CS_0" + i).val() + ",";
                    str_data += $("#RZTHD_01G_CG_0" + i).val() + ",";
                    str_data += $("#RZTHD_02G_CS_0" + i).val() + ",";
                    str_data += $("#RZTHD_02G_CG_0" + i).val() + ",";
                    str_data += $("#RZTHD_01D_CS_0" + i).val() + ",";
                    str_data += $("#RZTHD_01D_CG_0" + i).val() + ",";
                    str_data += $("#RZTHD_02D_CS_0" + i).val() + ",";
                    str_data += $("#RZTHD_02D_CG_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
           }
            //下午三十、潍坊度假区-未来三天高/低潮预报
            function submit_60(id) {
               var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#WFDJQ_01G_CS_0" + i).val() + ",";
                    str_data += $("#WFDJQ_01G_CG_0" + i).val() + ",";
                    str_data += $("#WFDJQ_02G_CS_0" + i).val() + ",";
                    str_data += $("#WFDJQ_02G_CG_0" + i).val() + ",";
                    str_data += $("#WFDJQ_01D_CS_0" + i).val() + ",";
                    str_data += $("#WFDJQ_01D_CG_0" + i).val() + ",";
                    str_data += $("#WFDJQ_02D_CS_0" + i).val() + ",";
                    str_data += $("#WFDJQ_02D_CG_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
           }
            //下午三十三、威海新区-未来三天高/低潮预报
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
            //下午三十六、烟台清泉-未来三天高/低潮预报
            function submit_66(id) {
               var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#YTQQ_01G_CS_0" + i).val() + ",";
                    str_data += $("#YTQQ_01G_CG_0" + i).val() + ",";
                    str_data += $("#YTQQ_02G_CS_0" + i).val() + ",";
                    str_data += $("#YTQQ_02G_CG_0" + i).val() + ",";
                    str_data += $("#YTQQ_01D_CS_0" + i).val() + ",";
                    str_data += $("#YTQQ_01D_CG_0" + i).val() + ",";
                    str_data += $("#YTQQ_02D_CS_0" + i).val() + ",";
                    str_data += $("#YTQQ_02D_CG_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
           }
            //下午三十九、董家口-未来三天高/低潮预报
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
            //下午四十二、东营渔港-未来三天高/低潮预报
            function submit_72(id) {
               var str_data = "";
                for (var i = 1; i <= 3; i++) {
                    str_data += $("#DYFP_01G_CS_0" + i).val() + ",";
                    str_data += $("#DYFP_01G_CG_0" + i).val() + ",";
                    str_data += $("#DYFP_02G_CS_0" + i).val() + ",";
                    str_data += $("#DYFP_02G_CG_0" + i).val() + ",";
                    str_data += $("#DYFP_01D_CS_0" + i).val() + ",";
                    str_data += $("#DYFP_01D_CG_0" + i).val() + ",";
                    str_data += $("#DYFP_02D_CS_0" + i).val() + ",";
                    str_data += $("#DYFP_02D_CG_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
           }

            //七地市
            //风浪
            //下午二十五、东营广利渔港-未来三天的海面风及海浪有效波高预报（20时起报）
            function submit_55(id) {
                var str_data = "";
                for (var i = 1; i < 4; i++) {
                    str_data += $("#DYGL_12H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#DYGL_24H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#DYGL_48H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                   str_data += $("#DYGL_72H_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午二十八、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）
            function submit_58(id) {
                var str_data = "";
                for (var i = 1; i < 4; i++) {
                    str_data += $("#RZTHD_12H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#RZTHD_24H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#RZTHD_48H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                   str_data += $("#RZTHD_72H_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午三十一、潍坊度假区-未来三天的海面风及海浪有效波高预报（20时起报）
            function submit_61(id) {
                var str_data = "";
                for (var i = 1; i < 4; i++) {
                    str_data += $("#WFDJQ_12H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#WFDJQ_24H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#WFDJQ_48H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                   str_data += $("#WFDJQ_72H_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午三十四、威海新区-未来三天的海面风及海浪有效波高预报（20时起报）
            function submit_64(id) {
                var str_data = "";
                for (var i = 1; i < 4; i++) {
                    str_data += $("#WHXQ_12H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#WHXQ_24H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#WHXQ_48H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                   str_data += $("#WHXQ_72H_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午三十七、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）
            function submit_67(id) {
                var str_data = "";
                for (var i = 1; i < 4; i++) {
                    str_data += $("#YTQQ_12H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#YTQQ_24H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#YTQQ_48H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                   str_data += $("#YTQQ_72H_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午四十、董家口-未来三天的海面风及海浪有效波高预报（20时起报）
            function submit_70(id) {
                var str_data = "";
                for (var i = 1; i < 4; i++) {
                    str_data += $("#DJKP_12H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#DJKP_24H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#DJKP_48H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                   str_data += $("#DJKP_72H_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午四十三、东营渔港-未来三天的海面风及海浪有效波高预报（20时起报）
            function submit_73(id) {
                var str_data = "";
                for (var i = 1; i < 4; i++) {
                    str_data += $("#DYFP_12H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#DYFP_24H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                    str_data += $("#DYFP_48H_0" + i).val() + ",";
                }
                 for (var i = 1; i < 4; i++) {
                   str_data += $("#DYFP_72H_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //七地市
            //海温
            //下午二十六、东营广利渔港-未来三天的海面水温预报
            function submit_56(id) {
                var str_data = "";
                str_data += $("#DYGL_SW_01").val() + ",";
                str_data += $("#DYGL_SW_02").val() + ",";
                str_data += $("#DYGL_SW_03").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午二十九、日照桃花岛-未来三天的海面水温预报
            function submit_59(id) {
                var str_data = "";
                str_data += $("#RZTHD_SW_01").val() + ",";
                str_data += $("#RZTHD_SW_02").val() + ",";
                str_data += $("#RZTHD_SW_03").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午三十二、潍坊度假区-未来三天的海面水温预报
            function submit_62(id) {
                var str_data = "";
                str_data += $("#WFDJQ_SW_01").val() + ",";
                str_data += $("#WFDJQ_SW_02").val() + ",";
                str_data += $("#WFDJQ_SW_03").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午三十五、威海新区-未来三天的海面水温预报
            function submit_65(id) {
                var str_data = "";
                str_data += $("#WHXQ_SW_01").val() + ",";
                str_data += $("#WHXQ_SW_02").val() + ",";
                str_data += $("#WHXQ_SW_03").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午三十八、烟台清泉-未来三天的海面水温预报
            function submit_68(id) {
                var str_data = "";
                str_data += $("#YTQQ_SW_01").val() + ",";
                str_data += $("#YTQQ_SW_02").val() + ",";
                str_data += $("#YTQQ_SW_03").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午四十一、董家口-未来三天的海面水温预报
            function submit_71(id) {
                var str_data = "";
                str_data += $("#DJKP_SW_01").val() + ",";
                str_data += $("#DJKP_SW_02").val() + ",";
                str_data += $("#DJKP_SW_03").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //下午四十四、东营渔港-未来三天的海面水温预报
            function submit_74(id) {
                var str_data = "";
                str_data += $("#DYFP_SW_01").val() + ",";
                str_data += $("#DYFP_SW_02").val() + ",";
                str_data += $("#DYFP_SW_03").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
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
            gettabledatacx(curr_time, "f");//获取潮汐数据

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
            }
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
        //ajax加载潮汐数据
        function gettabledatacx(date1, searchType) {//searchType 按填报日期还是预报日期查询 p:填报日期 f:预报日期
            var dates = myformatter(date1);
          
            $.ajax({//综合
                type: "POST",
                url: "/Ajax/gettablelistcx.ashx?method=getbydataPMCX&date=" + dates + "&searchtype=" + searchType,
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
                            
                            case "cx7": gettable07(resjson[j].children, date1); dlgclose("7"); break;
                            case "cx10": gettable10(resjson[j].children, date1); dlgclose("10"); break;
                            case "cx12": gettable12(resjson[j].children, date1); dlgclose("12"); break;                         
                            case "cx18": gettable18(resjson[j].children, date1); dlgclose("18"); break;                   
                            case "cx22": gettable22(resjson[j].children, date1); dlgclose("22"); break;//威海预报单中威海预报取消不用，取下午三威海                          
                            case "cx42": gettable42(resjson[j].children, date1); dlgclose("42"); break;                          
                            case "cx46": gettable46(resjson[j].children, date1); dlgclose("46"); break;
                            case "cx48": gettable48(resjson[j].children, date1); dlgclose("48"); break;                 
                            case "cx54": gettable54(resjson[j].children, date1); dlgclose("54"); break;                            
                            case "cx57": gettable57(resjson[j].children, date1); dlgclose("57"); break;                          
                            case "cx60": gettable60(resjson[j].children, date1); dlgclose("60"); break;
                            case "cx63": gettable63(resjson[j].children, date1); dlgclose("63"); break;                         
                            case "cx66": gettable66(resjson[j].children, date1); dlgclose("66"); break;                         
                            case "cx69": gettable69(resjson[j].children, date1); dlgclose("69"); break;                      
                            case "cx72": gettable72(resjson[j].children, date1); dlgclose("72"); break;                           
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
        {
             function gettable07(resjson, date1) {
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
                    $("#" + nqyname + "_01G_H_00" + num).val(resjson[i].SDOSCTFIRSTHIGHWAVEHOUR+resjson[i].SDOSCTFIRSTHIGHWAVEMINUTE);
                    //$("#" + nqyname + "_01G_MIN").val(resjson[i].SDOSCTFIRSTHIGHWAVEMINUTE);
                    $("#" + nqyname + "_01D_H_00" + num).val(resjson[i].SDOSCTFIRSTLOWWAVEHOUR+resjson[i].SDOSCTFIRSTLOWWAVEMINUTE);
                    //$("#" + nqyname + "_01D_MIN").val(resjson[i].SDOSCTFIRSTLOWWAVEMINUTE);
                    $("#" + nqyname + "_02G_H_00" + num).val(resjson[i].SDOSCTSECONDHIGHWAVEHOUR+resjson[i].SDOSCTSECONDHIGHWAVEMINUTE);
                    //$("#" + nqyname + "_02G_MIN").val(resjson[i].SDOSCTSECONDHIGHWAVEMINUTE);
                    $("#" + nqyname + "_02D_H_00" + num).val(resjson[i].SDOSCTSECONDLOWWAVEHOUR+resjson[i].SDOSCTSECONDLOWWAVEMINUTE);
                    //$("#" + nqyname + "_02D_MIN").val(resjson[i].SDOSCTSECONDLOWWAVEMINUTE);
                }
            }

            //下午三、潮汐数据潮高
            function gettable46(resjson, date1) {
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

	    function gettable18(resjson, date1) {

                for (var i = 0; i < resjson.length; i++) {
                    var stationName = "";
                    if (resjson[i].SEABEACH == "青岛市区") 
                        stationName = "XMD";
                    else if (resjson[i].SEABEACH == "金沙滩") 
                        stationName = "WMT";

                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    //$("#"+stationName+"_01GC_H").val(resjson[i].GB24HTFFIRSTHIGHWAVEHOUR);
                    //$("#"+stationName+"_01GC_MIN").val(resjson[i].GB24HTFFIRSTHIGHWAVEMINUTE);
                    //$("#"+stationName+"_01DC_H").val(resjson[i].GB24HTFFIRSTLOWWAVEHOUR);
                    //$("#"+stationName+"_01DC_MIN").val(resjson[i].GB24HTFFIRSTLOWWAVEMINUTE);
                    //$("#"+stationName+"_02GC_H").val(resjson[i].GB24HTFSECONDHIGHWAVEHOUR);
                    //$("#"+stationName+"_02GC_MIN").val(resjson[i].GB24HTFSECONDHIGHWAVEMINUTE);
                    //$("#"+stationName+"_02DC_H").val(resjson[i].GB24HTFSECONDLOWWAVEHOUR);
                    //$("#"+stationName+"_02DC_MIN").val(resjson[i].GB24HTFSECONDLOWWAVEMINUTE);
                    $("#"+stationName+"_01G_H_00"+num).val(resjson[i].FIRSTHIGHTIME);
                    $("#"+stationName+"_01G_CG_00"+num).val(resjson[i].FIRSTHIGHLEVEL);
                    $("#"+stationName+"_02G_H_00"+num).val(resjson[i].SECONDHIGHTIME);
                    $("#"+stationName+"_02G_CG_00"+num).val(resjson[i].SECONDHEIGHTLEVEL);
                    $("#"+stationName+"_01D_H_00"+num).val(resjson[i].FIRSTLOWTIME);
                    $("#"+stationName+"_01D_CG_00"+num).val(resjson[i].FIRSTLOWLEVEL);
                    $("#"+stationName+"_02D_H_00"+num).val(resjson[i].SECONDLOWTIME);
                    $("#"+stationName+"_02D_CG_00"+num).val(resjson[i].SECONDLOWLEVEL);
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

	    //表单22数据 下午十五、威海48小时潮汐预报
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

	    //下午二十、东营埕岛-未来三天高/低潮预报
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

                    //$("#TIDE_" + nqyname + "_FORCASTDATE").val(resjson[i].FORECASTDATE);
                }
            }

	    //下午二十四、东营广利渔港-未来三天高/低潮预报
           function gettable54(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#DYGL_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
                    $("#DYGL_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
                    $("#DYGL_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
                    $("#DYGL_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
                    $("#DYGL_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
                    $("#DYGL_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
                    $("#DYGL_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
                    $("#DYGL_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
                }
           }

	   //下午二十七、日照桃花岛-未来三天高/低潮预报
            function gettable57(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#RZTHD_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
                    $("#RZTHD_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
                    $("#RZTHD_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
                    $("#RZTHD_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
                    $("#RZTHD_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
                    $("#RZTHD_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
                    $("#RZTHD_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
                    $("#RZTHD_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
                }
           }

	   //下午三十、潍坊度假区-未来三天高/低潮预报
            function gettable60(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#WFDJQ_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
                    $("#WFDJQ_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
                    $("#WFDJQ_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
                    $("#WFDJQ_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
                    $("#WFDJQ_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
                    $("#WFDJQ_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
                    $("#WFDJQ_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
                    $("#WFDJQ_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
                }
           }

	   //下午三十三、威海新区-未来三天高/低潮预报
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

	   function gettable66(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#YTQQ_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
                    $("#YTQQ_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
                    $("#YTQQ_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
                    $("#YTQQ_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
                    $("#YTQQ_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
                    $("#YTQQ_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
                    $("#YTQQ_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
                    $("#YTQQ_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
                }
           }

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

	   //下午四十二、东营渔港-未来三天高/低潮预报
            function gettable72(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#DYFP_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
                    $("#DYFP_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
                    $("#DYFP_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
                    $("#DYFP_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
                    $("#DYFP_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
                    $("#DYFP_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
                    $("#DYFP_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
                    $("#DYFP_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
                }
           }

        }

        //ajax 加载各表数据
        function gettabledata(date1, searchType) { //searchType 按填报日期还是预报日期查询 p:填报日期 f:预报日期

            var dates = myformatter(date1);
            $.ajax({//综合
                type: "POST",
                url: "/Ajax/gettablelist.ashx?method=getbydataPM&date=" + dates + "&searchtype=" + searchType,
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
                            case "t5": gettable05(resjson[j], date1); dlgclose("5"); break;
                            case "t6": gettable06(resjson[j], date1); dlgclose("6"); break;
                            //case "t7": gettable07(resjson[j].children, date1); dlgclose("7"); break;
                            case "t8": gettable08(resjson[j].children, date1); dlgclose("8"); break;//原下午四潮汐取消不用，取下午三青岛24
                            case "t9": gettable09(resjson[j], date1); dlgclose("9"); break;
                            //case "t10": gettable10(resjson[j].children, date1); dlgclose("10"); break;
                            case "t11": gettable11(resjson[j], date1); dlgclose("11"); break;
                            //case "t12": gettable12(resjson[j].children, date1); dlgclose("12"); break;
                            case "t13": gettable13(resjson[j], date1); dlgclose("13"); break;
                            case "t14": gettable14(resjson[j], date1); dlgclose("14"); break;
                            case "t15": gettable15(resjson[j], date1); dlgclose("15"); break;
                            //case "t16": gettable16(resjson[j].children, date1); dlgclose("16"); break;原下午十二取消不用，取下午三潍坊三天
                            case "t17": gettable17(resjson[j], date1); dlgclose("17"); break;
                            //case "t18": gettable18(resjson[j].children, date1); dlgclose("18"); break;
                            case "t19": gettable19(resjson[j], date1); dlgclose("19"); break;
                            //case "t20": gettable20(resjson[j].children, date1); dlgclose("20"); break;原下午五取消不用，取下午三青岛48
                            case "t21": gettable21(resjson[j], date1); dlgclose("21"); break;
                            //case "t22": gettable22(resjson[j].children, date1); dlgclose("22"); break;//威海预报单中威海预报取消不用，取下午三威海
                            case "t23": gettable23(resjson[j].children, date1); break;
                            case "t41": gettable41(resjson[j], date1); dlgclose("41"); break;
                            //case "t42": gettable42(resjson[j].children, date1); dlgclose("42"); break;
                            case "t43": gettable43(resjson[j], date1); dlgclose("43"); break;
                            //case "t46": gettable46(resjson[j].children, date1); dlgclose("46"); break;
                            case "t47": gettable47(resjson[j], date1); dlgclose("47"); break;
                            //case "t48": gettable48(resjson[j].children, date1); dlgclose("48"); break;
                            //case "t83": gettable48(resjson[j].children, date1); dlgclose("48"); break;

                            case "t49": gettable49(resjson[j], date1); dlgclose("49"); break;
                           
                            case "t53": gettable53(resjson[j], date1); dlgclose("53"); break;
                            //case "t54": gettable54(resjson[j].children, date1); dlgclose("54"); break;
                            case "t55": gettable55(resjson[j], date1); dlgclose("55"); break;
                            case "t56": gettable56(resjson[j], date1); dlgclose("56"); break;
                            //case "t57": gettable57(resjson[j].children, date1); dlgclose("57"); break;
                            case "t58": gettable58(resjson[j], date1); dlgclose("58"); break;
                            case "t59": gettable59(resjson[j], date1); dlgclose("59"); break;
                            //case "t60": gettable60(resjson[j].children, date1); dlgclose("60"); break;
                            case "t61": gettable61(resjson[j], date1); dlgclose("61"); break;
                            case "t62": gettable62(resjson[j], date1); dlgclose("62"); break;
                            //case "t63": gettable63(resjson[j].children, date1); dlgclose("63"); break;
                            case "t64": gettable64(resjson[j], date1); dlgclose("64"); break;
                            case "t65": gettable65(resjson[j], date1); dlgclose("65"); break;
                            //case "t66": gettable66(resjson[j].children, date1); dlgclose("66"); break;
                            case "t67": gettable67(resjson[j], date1); dlgclose("67"); break;
                            case "t68": gettable68(resjson[j], date1); dlgclose("68"); break;
                            //case "t69": gettable69(resjson[j].children, date1); dlgclose("69"); break;
                            case "t70": gettable70(resjson[j], date1); dlgclose("70"); break;
                            case "t71": gettable71(resjson[j], date1); dlgclose("71"); break;
                            //case "t72": gettable72(resjson[j].children, date1); dlgclose("72"); break;
                            case "t73": gettable73(resjson[j], date1); dlgclose("73"); break;
                            case "t74": gettable74(resjson[j], date1); dlgclose("74"); break;
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
            //表单05数据
            function gettable05(result, date1) {
                 var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                        if (waveForecast[j].FORECASTAREA == "渤海") {
                            $("#BH_GDE_401").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海北部") {
                           $("#HHBB_GDE_401").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海中部") {
                           $("#HHZB_GDE_401").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海南部") {
                            $("#HHNB_GDE_401").val(waveForecast[j].WAVE24FORECAST);
                        }
                    }
                    var dt5 = result.dt5;
                    for(var j = 0;j<dt5.length;j++){
                        $("#BH_GDF_401").val(dt5[j].EFWWBHLOWESTWAVE);
                        $("#BH_GDE_401").val(dt5[j].EFWWBHHIGHESTWAVE);
                        $("#BH_HL_401").val(dt5[j].EFWWBHWAVETYPE);
                        $("#HHBB_GDF_401").val(dt5[j].EFWWBHNORTHLOWESTWAVE);
                        $("#HHBB_GDE_401").val(dt5[j].EFWWBHNORTHHIGHESTWAVE);
                        $("#HHBB_HL_401").val(dt5[j].EFWWBHNORTHWAVETYPE);
                    }
                    var dt10 = result.dt10;
                    for (var j = 0; j < dt10.length; j++) {
                        var HHBBDATA = dt10[j].SA24HWFNORTHOFYSWAVEHEIGHT;
                        var HHZBDATA = dt10[j].SA24HWFMIDDLEOFYSWAVEHEIGHT;
                        var HHNBDATA = dt10[j].SA24HWFSOUTHOFYSWAVEHEIGHT;

                        var splitstr1 = "-";
                        var splitstr2 = "增至";
                        var splitstr3 = "减至";

                        var HHBBF="";
                        var HHBBE="";
                        var HHZBF="";
                        var HHZBE="";
                        var HHNBF="";
                        var HHNBE="";
                        if(typeof(HHBBDATA) != "undefined")
                        {
                            if (HHBBDATA.indexOf(splitstr1) != -1)
                            {
                                HHBBF = HHBBDATA.split(splitstr1)[0];
                                HHBBE = HHBBDATA.split(splitstr1)[1];
                            }
                            else if (HHBBDATA.indexOf(splitstr2) != -1) {
                                HHBBF = HHBBDATA.split(splitstr2)[0];
                                HHBBE = HHBBDATA.split(splitstr2)[1];
                            }
                            else if (HHBBDATA.indexOf(splitstr3) != -1) {
                                //HHBBF = HHBBDATA.split(splitstr3)[0];
                                //HHBBE = HHBBDATA.split(splitstr3)[1];
                                HHBBF = HHBBDATA.split(splitstr3)[1];
                                HHBBE = HHBBDATA.split(splitstr3)[0];
                            }
                            else{
                                HHBBF = "";
                                HHBBE = HHBBDATA;
                            }
                            $("#HHBB_GDF_401").val(HHBBF);
                            $("#HHBB_GDE_401").val(HHBBE);
                        }
                        if (typeof (HHZBDATA) != "undefined") {
                             if (HHZBDATA.indexOf(splitstr1) != -1)
                            {
                                HHZBF = HHZBDATA.split(splitstr1)[0];
                                HHZBE = HHZBDATA.split(splitstr1)[1];
                            }
                            else if (HHZBDATA.indexOf(splitstr2) != -1) {
                                HHZBF = HHZBDATA.split(splitstr2)[0];
                                HHZBE = HHZBDATA.split(splitstr2)[1];
                            }
                            else if (HHZBDATA.indexOf(splitstr3) != -1) {
                                //HHZBF = HHZBDATA.split(splitstr3)[0];
                                //HHZBE = HHZBDATA.split(splitstr3)[1];
                                HHZBF = HHZBDATA.split(splitstr3)[1];
                                HHZBE = HHZBDATA.split(splitstr3)[0];
                            }
                            else{
                                HHZBF = "";
                                HHZBE = HHZBDATA;
                            }
                            $("#HHZB_GDF_401").val(HHZBF);
                            $("#HHZB_GDE_401").val(HHZBE);
                        }
                       

                         if (typeof (HHNBDATA) != "undefined") {
                            if (HHNBDATA.indexOf(splitstr1) != -1)
                            {
                                HHNBF = HHNBDATA.split(splitstr1)[0];
                                HHNBE = HHNBDATA.split(splitstr1)[1];
                            }
                            else if (HHNBDATA.indexOf(splitstr2) != -1) {
                                HHNBF = HHNBDATA.split(splitstr2)[0];
                                HHNBE = HHNBDATA.split(splitstr2)[1];
                            }
                            else if (HHNBDATA.indexOf(splitstr3) != -1) {
                                //HHNBF = HHNBDATA.split(splitstr3)[0];
                                //HHNBE = HHNBDATA.split(splitstr3)[1];
                                HHNBF = HHNBDATA.split(splitstr3)[1];
                                HHNBE = HHNBDATA.split(splitstr3)[0];
                            }
                            else {
                                HHNBF = "";
                                HHNBE = HHNBDATA;
                            }
                            $("#HHNB_GDF_401").val(HHNBF);
                            $("#HHNB_GDE_401").val(HHNBE);
                        }
                        
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
            }

            //表单06数据
            function gettable06(result, date1) {
                 var pbtypes = result.pbtype;
                 if (pbtypes == "bys") {
                      var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                        if (waveForecast[j].FORECASTAREA == "日照近海") {
                            $("#RZJH_LG_24h").val(waveForecast[j].WAVE24FORECAST);
                            $("#RZJH_LG_48h").val(waveForecast[j].WAVE48FORECAST);
                            $("#RZJH_LG_72h").val(waveForecast[j].WAVE72FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "青岛近海") {
                            $("#QDJH_LG_24h").val(waveForecast[j].WAVE24FORECAST);
                            $("#QDJH_LG_48h").val(waveForecast[j].WAVE48FORECAST);
                            $("#QDJH_LG_72h").val(waveForecast[j].WAVE72FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "威海近海") {
                            $("#WHJH_LG_24h").val(waveForecast[j].WAVE24FORECAST);
                            $("#WHJH_LG_48h").val(waveForecast[j].WAVE48FORECAST);
                            $("#WHJH_LG_72h").val(waveForecast[j].WAVE72FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "烟台近海") {
                            $("#YTJH_LG_24h").val(waveForecast[j].WAVE24FORECAST);
                            $("#YTJH_LG_48h").val(waveForecast[j].WAVE48FORECAST);
                            $("#YTJH_LG_72h").val(waveForecast[j].WAVE72FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "潍坊近海") {
                            $("#WFJH_LG_24h").val(waveForecast[j].WAVE24FORECAST);
                            $("#WFJH_LG_48h").val(waveForecast[j].WAVE48FORECAST);
                            $("#WFJH_LG_72h").val(waveForecast[j].WAVE72FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "东营近海") {
                            $("#DYJH_LG_24h").val(waveForecast[j].WAVE24FORECAST);
                            $("#DYJH_LG_48h").val(waveForecast[j].WAVE48FORECAST);
                            $("#DYJH_LG_72h").val(waveForecast[j].WAVE72FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "滨州近海") {
                            $("#BZJH_LG_24h").val(waveForecast[j].WAVE24FORECAST);
                            $("#BZJH_LG_48h").val(waveForecast[j].WAVE48FORECAST);
                            $("#BZJH_LG_72h").val(waveForecast[j].WAVE72FORECAST);
                        }
                    }

                    var sw = result.sw;
                    for(var k = 0;k<sw.length;k++){
                        switch (sw[k].NAME) {
                            case "日照": nqyname = "RZJH"; break;
                            case "青岛": nqyname = "QDJH"; break;
                            case "威海": nqyname = "WHJH"; break;
                            case "烟台": nqyname = "YTJH"; break;
                            case "潍坊": nqyname = "WFJH"; break;
                            case "东营": nqyname = "DYJH"; break;
                            case "滨州": nqyname = "BZJH"; break;
                            default:
                                nqyname = "";
                                break;
                        }
                        $("#" + nqyname + "_BCSW_24h").val(sw[k].MEAN_24H);
                        $("#" + nqyname + "_BCSW_48h").val(sw[k].MEAN_48H);
                        $("#" + nqyname + "_BCSW_72h").val(sw[k].MEAN_72H);
                    }
                   var dt5 = result.dt5;
                    for(var j = 0;j<dt5.length;j++){
                        $("#DYJH_LG_24h").val(dt5[j].EFWWDYGWAVEHEIGHT);
                    }
                    var dt10 = result.dt10;
                    for (var j = 0; j < dt10.length; j++) {
                       $("#WFJH_LG_24h").val(dt10[j].SA24HWFOFFSHOREWAVEHEIGHT);
                    }
                 }
                 else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
                        $("#" + nqyname + "_LG_24h").val(resjson[i].lg24);
                        $("#" + nqyname + "_LG_48h").val(resjson[i].lg48);
                        $("#" + nqyname + "_LG_72h").val(resjson[i].lg72);
                        $("#" + nqyname + "_BCSW_24h").val(resjson[i].sw24);
                        $("#" + nqyname + "_BCSW_48h").val(resjson[i].sw48);
                        $("#" + nqyname + "_BCSW_72h").val(resjson[i].sw72);
                    }
                }
            }

           //下午三、潮汐数据潮时
            //function gettable07(resjson, date1) {
            //    for (var i = 0; i < resjson.length; i++) {
            //        date = new Date(resjson[i].FORECASTDATE);
            //        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
            //        switch (resjson[i].SDOSCTCITY) {
            //            case "日照": nqyname = "RZ"; break;
            //            case "青岛": nqyname = "QD"; break;
            //            case "威海": nqyname = "WH"; break;
            //            case "烟台": nqyname = "YT"; break;
            //            case "潍坊": nqyname = "WF"; break;
            //            case "东营": nqyname = "DY"; break;
            //            case "滨州": nqyname = "BZ"; break;
            //            default:
            //        }
            //        $("#" + nqyname + "_01G_H_00" + num).val(resjson[i].SDOSCTFIRSTHIGHWAVEHOUR+resjson[i].SDOSCTFIRSTHIGHWAVEMINUTE);
            //        //$("#" + nqyname + "_01G_MIN").val(resjson[i].SDOSCTFIRSTHIGHWAVEMINUTE);
            //        $("#" + nqyname + "_01D_H_00" + num).val(resjson[i].SDOSCTFIRSTLOWWAVEHOUR+resjson[i].SDOSCTFIRSTLOWWAVEMINUTE);
            //        //$("#" + nqyname + "_01D_MIN").val(resjson[i].SDOSCTFIRSTLOWWAVEMINUTE);
            //        $("#" + nqyname + "_02G_H_00" + num).val(resjson[i].SDOSCTSECONDHIGHWAVEHOUR+resjson[i].SDOSCTSECONDHIGHWAVEMINUTE);
            //        //$("#" + nqyname + "_02G_MIN").val(resjson[i].SDOSCTSECONDHIGHWAVEMINUTE);
            //        $("#" + nqyname + "_02D_H_00" + num).val(resjson[i].SDOSCTSECONDLOWWAVEHOUR+resjson[i].SDOSCTSECONDLOWWAVEMINUTE);
            //        //$("#" + nqyname + "_02D_MIN").val(resjson[i].SDOSCTSECONDLOWWAVEMINUTE);
            //    }
            //}

            ////下午三、潮汐数据潮高
            //function gettable46(resjson, date1) {
            //    for (var i = 0; i < resjson.length; i++) {
            //        date = new Date(resjson[i].FORECASTDATE);
            //        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
            //        switch (resjson[i].SDOSCTCITY) {
            //            case "日照": nqyname = "RZ"; break;
            //            case "青岛": nqyname = "QD"; break;
            //            case "威海": nqyname = "WH"; break;
            //            case "烟台": nqyname = "YT"; break;
            //            case "潍坊": nqyname = "WF"; break;
            //            case "东营": nqyname = "DY"; break;
            //            case "滨州": nqyname = "BZ"; break;
            //            default:
            //        }
            //        $("#" + nqyname + "_01G_CG_00" + num).val(resjson[i].FIRSTHIGHWAVETIDEDATA);
            //        $("#" + nqyname + "_01D_CG_00" + num).val(resjson[i].FIRSTLOWWAVETIDEDATA);
            //        $("#" + nqyname + "_02G_CG_00" + num).val(resjson[i].SECONDHIGHWAVETIDEDATA);
            //        $("#" + nqyname + "_02D_CG_00" + num).val(resjson[i].SECONDLOWWAVETIDEDATA);
            //    }
            //}

            //表单08数据
            function gettable08(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    //$("#01GC_H").val(resjson[i].QDTLFIRSTHIGHWAVEHOUR);
                    //$("#01GC_MIN").val(resjson[i].QDTLFIRSTHIGHWAVEMINUTE);
                    //$("#01GC_CM").val(resjson[i].QDTLFIRSTHIGHWAVEHEIGHT);
                    //$("#01DC_H").val(resjson[i].QDTLFIRSTLOWWAVEHOUR);
                    //$("#01DC_MIN").val(resjson[i].QDTLFIRSTLOWWAVEMINUTE);
                    //$("#01DC_CM").val(resjson[i].QDTLFIRSTLOWWAVEHEIGHT);
                    //$("#02GC_H").val(resjson[i].QDTLSECONDHIGHWAVEHOUR);
                    //$("#02GC_MIN").val(resjson[i].QDTLSECONDHIGHWAVEMINUTE);
                    //$("#02GC_CM").val(resjson[i].QDTLSECONDHIGHWAVEHEIGHT);
                    //$("#02DC_H").val(resjson[i].QDTLSECONDLOWWAVEHOUR);
                    //$("#02DC_MIN").val(resjson[i].QDTLSECONDLOWWAVEMINUTE);
                    //$("#02DC_CM").val(resjson[i].QDTLSECONDLOWWAVEHEIGHT);
                    $("#MRHBLG").val(resjson[i].QDTLTOMORROWWAVEHEIGHT);
                    $("#MRHBLX").val(resjson[i].QDTLTOMORROWWAVEDIR);
                }
            }

            //表单09数据
            function gettable09(result, date1) {
                 var pbtypes = result.pbtype;
                 if (pbtypes == "bys") {
                     var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                       $("#BG_01").val(waveForecast[j].WAVE24FORECAST);
                       $("#BG_02").val(waveForecast[j].WAVE48FORECAST);
                       $("#BG_03").val(waveForecast[j].WAVE72FORECAST);
                    }

                    var windForecast = result.wind;
                    for(var k = 0;k<windForecast.length;k++){
                        $("#BX_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                        $("#FX_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                        $("#FL_01").val(windForecast[k].WINDFORCE24FORECAST);

                        $("#BX_02").val(windForecast[k].WINDDIRECTION48FORECAST);
                        $("#FX_02").val(windForecast[k].WINDDIRECTION48FORECAST);
                        $("#FL_02").val(windForecast[k].WINDFORCE48FORECAST);

                        $("#BX_03").val(windForecast[k].WINDDIRECTION72FORECAST);
                        $("#FX_03").val(windForecast[k].WINDDIRECTION72FORECAST);
                        $("#FL_03").val(windForecast[k].WINDFORCE72FORECAST);
                    }
                 }
                 else if (pbtypes == "bydb") {
                    var resjson=result.children;
                    for (var i = 0; i < resjson.length; i++) {
                        date = new Date(resjson[i].yb);
                        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        $("#BG_0" + num).val(resjson[i].bg);
                        $("#BX_0" + num).val(resjson[i].bx);
                        $("#FX_0" + num).val(resjson[i].fx);
                        $("#FL_0" + num).val(resjson[i].fl);
                    }
                }
            }

            ////表单10数据
            //function gettable10(resjson, date1) {
            //    for (var i = 0; i < resjson.length; i++) {
            //        date = new Date(resjson[i].yb);
            //        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
            //        $("#MZZ_01G_CW_0" + num).val(resjson[i].g1c);
            //        $("#MZZ_01G_SJ_0" + num).val(resjson[i].g1s);
            //        $("#MZZ_01D_CW_0" + num).val(resjson[i].d1c);
            //        $("#MZZ_01D_SJ_0" + num).val(resjson[i].d1s);
            //        $("#MZZ_02G_CW_0" + num).val(resjson[i].g2c);
            //        $("#MZZ_02G_SJ_0" + num).val(resjson[i].g2s);
            //        $("#MZZ_02D_CW_0" + num).val(resjson[i].d2c);
            //        $("#MZZ_02D_SJ_0" + num).val(resjson[i].d2s);
            //    }
            //}

            //表单11数据
            function gettable11(result, date1) {
                var pbtypes = result.pbtype;
                if (pbtypes == "bys"){
                    var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                       $("#NBYT_BG_01").val(waveForecast[j].WAVE24FORECAST);
                       $("#NBYT_BG_02").val(waveForecast[j].WAVE48FORECAST);
                       $("#NBYT_BG_03").val(waveForecast[j].WAVE72FORECAST);
                    }

                    var windForecast = result.wind;
                    for(var k = 0;k<windForecast.length;k++){
                        $("#NBYT_BX_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                        $("#NBYT_FX_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                        $("#NBYT_FL_01").val(windForecast[k].WINDFORCE24FORECAST);

                        $("#NBYT_BX_02").val(windForecast[k].WINDDIRECTION48FORECAST);
                        $("#NBYT_FX_02").val(windForecast[k].WINDDIRECTION48FORECAST);
                        $("#NBYT_FL_02").val(windForecast[k].WINDFORCE48FORECAST);

                        $("#NBYT_BX_03").val(windForecast[k].WINDDIRECTION72FORECAST);
                        $("#NBYT_FX_03").val(windForecast[k].WINDDIRECTION72FORECAST);
                        $("#NBYT_FL_03").val(windForecast[k].WINDFORCE72FORECAST);
                    }
                    var sw = result.sw;
                    if(sw.length > 0){
                        $("#NBYT_SW_01").val(sw[0].MEAN_24H);
                        $("#NBYT_SW_02").val(sw[0].MEAN_48H);
                        $("#NBYT_SW_03").val(sw[0].MEAN_72H);
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
            }

            //表单12数据
            //function gettable12(resjson, date1) {
            //    for (var i = 0; i < resjson.length; i++) {
            //        date = new Date(resjson[i].FORECASTDATE);
            //        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
            //        $("#NBYT_01G_CW_0" + num).val(resjson[i].NOTFFIRSTHIGHWAVEHEIGHT);
            //        $("#NBYT_01G_SJ_0" + num).val(resjson[i].NOTFFIRSTHIGHWAVETIME);
            //        $("#NBYT_01D_CW_0" + num).val(resjson[i].NOTFFIRSTLOWWAVEHEIGHT);
            //        $("#NBYT_01D_SJ_0" + num).val(resjson[i].NOTFFIRSTLOWWAVETIME);
            //        $("#NBYT_02G_CW_0" + num).val(resjson[i].NOTFSECONDHIGHWAVEHEIGHT);
            //        $("#NBYT_02G_SJ_0" + num).val(resjson[i].NOTFSECONDHIGHWAVETIME);
            //        $("#NBYT_02D_CW_0" + num).val(resjson[i].NOTFSECONDLOWWAVEHEIGHT);
            //        $("#NBYT_02D_SJ_0" + num).val(resjson[i].NOTFSECONDLOWWAVETIME);
            //    }
            //}

            //表单13数据
            function gettable13(result, date1) {

                var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                       if (waveForecast[j].FORECASTAREA == "渤海") {
                            $("#BH13_BG").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海北部") {
                           $("#HHBB_BG").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海中部") {
                           $("#HHZB_BG").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海南部") {
                            $("#HHNB_BG").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "青岛近海") {
                            $("#QDJA_BG").val(waveForecast[j].WAVE24FORECAST);
                        }
                    }

                    var windForecast = result.wind;
                     for(var j = 0;j<windForecast.length;j++){
                       if (windForecast[j].FORECASTAREA == "渤海") {
                            $("#BH13_BX").val(windForecast[j].WINDDIRECTION24FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "黄海北部") {
                           $("#HHBB_BX").val(windForecast[j].WINDDIRECTION24FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "黄海中部") {
                           $("#HHZB_BX").val(windForecast[j].WINDDIRECTION24FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "黄海南部") {
                            $("#HHNB_BX").val(windForecast[j].WINDDIRECTION24FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "青岛近海") {
                            $("#QDJA_BX").val(windForecast[j].WINDDIRECTION24FORECAST);
                        }
                    }
                    var dt10 = result.dt10;
                    for (var j = 0; j < dt10.length; j++) {
                       $("#BH13_BG").val(dt10[j].SA24HWFBOHAIWAVEHEIGHT);
                       $("#HHBB_BG").val(dt10[j].SA24HWFNORTHOFYSWAVEHEIGHT);
                       $("#HHZB_BG").val(dt10[j].SA24HWFMIDDLEOFYSWAVEHEIGHT);
                       $("#HHNB_BG").val(dt10[j].SA24HWFSOUTHOFYSWAVEHEIGHT);
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
            }

            //表单14数据
            function gettable14(result, date1) {
                 var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                       if (waveForecast[j].FORECASTAREA == "渤海") {
                            $("#BH14_BG").val(waveForecast[j].WAVE48FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海北部") {
                           $("#HHBB14_BG").val(waveForecast[j].WAVE48FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海中部") {
                           $("#HHZB14_BG").val(waveForecast[j].WAVE48FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海南部") {
                            $("#HHNB14_BG").val(waveForecast[j].WAVE48FORECAST);
                        }
                    }

                    var windForecast = result.wind;
                     for(var j = 0;j<windForecast.length;j++){
                       if (windForecast[j].FORECASTAREA == "渤海") {
                            $("#BH14_BX").val(windForecast[j].WINDDIRECTION48FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "黄海北部") {
                           $("#HHBB14_BX").val(windForecast[j].WINDDIRECTION48FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "黄海中部") {
                           $("#HHZB14_BX").val(windForecast[j].WINDDIRECTION48FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "黄海南部") {
                            $("#HHNB14_BX").val(windForecast[j].WINDDIRECTION48FORECAST);
                        }
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
            }

            //表单15数据
            function gettable15(result, date1) {
                 var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                       if (waveForecast[j].FORECASTAREA == "渤海") {
                            $("#BH15_BG").val(waveForecast[j].WAVE72FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海北部") {
                           $("#HHBB15_BG").val(waveForecast[j].WAVE72FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海中部") {
                           $("#HHZB15_BG").val(waveForecast[j].WAVE72FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄海南部") {
                            $("#HHNB15_BG").val(waveForecast[j].WAVE72FORECAST);
                        }
                    }

                    var windForecast = result.wind;
                     for(var j = 0;j<windForecast.length;j++){
                       if (windForecast[j].FORECASTAREA == "渤海") {
                            $("#BH15_BX").val(windForecast[j].WINDDIRECTION72FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "黄海北部") {
                           $("#HHBB15_BX").val(windForecast[j].WINDDIRECTION72FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "黄海中部") {
                           $("#HHZB15_BX").val(windForecast[j].WINDDIRECTION72FORECAST);
                        }
                        else if (windForecast[j].FORECASTAREA == "黄海南部") {
                            $("#HHNB15_BX").val(windForecast[j].WINDDIRECTION72FORECAST);
                        }
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
            function gettable17(result, date1) {
                var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                        //$("#DYHSYC_LG").val((((waveForecast[j].WAVE24FORECAST)) - (0.3)));
                        //$("#DLHSYC_LG").val((((waveForecast[j].WAVE24FORECAST) * 1) - (0.3 * 1)));
                        //$("#SLRHSYC_LG").val((((waveForecast[j].WAVE24FORECAST) * 1) - (0.2 * 1)));
                        //$("#JSTHSYC_LG").val((((waveForecast[j].WAVE24FORECAST) * 1) - (0.1 * 1)));
                        $("#DYHSYC_LG").val(accSub(waveForecast[j].WAVE24FORECAST,0.3));
                        $("#DLHSYC_LG").val(accSub(waveForecast[j].WAVE24FORECAST,0.3));
                        $("#SLRHSYC_LG").val(accSub(waveForecast[j].WAVE24FORECAST,0.2));
                        $("#JSTHSYC_LG").val(accSub(waveForecast[j].WAVE24FORECAST,0.1));
                    }
                    var sw = result.sw;
                    for(var k = 0;k<sw.length;k++){
                        var area = "";
                        switch (sw[k].NAME) {
                            case "第一海水浴场": area = "DYHSYC"; break;
                            case "第六海水浴场": area = "DLHSYC"; break;
                            case "石老人海水浴场": area = "SLRHSYC"; break;
                            case "金沙滩海水浴场": area = "JSTHSYC"; break;
                            default: area = ""; break;
                        }
                        $("#"+area+"_SW").val(sw[k].MEAN_24H);
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
            }
            function accSub(arg1, arg2) {
                if (typeof (arg1) == "undefined") {
                    return "";
                }
                var r1, r2, m, n;
                try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
                try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
                m = Math.pow(10, Math.max(r1, r2));
                n = (r1 >= r2) ? r1 : r2;
                return ((arg1 * m - arg2 * m) / m).toFixed(n);
            }
            //表单18数据
            //function gettable18(resjson, date1) {

            //    for (var i = 0; i < resjson.length; i++) {
            //        var stationName = "";
            //        if (resjson[i].SEABEACH == "青岛市区") 
            //            stationName = "XMD";
            //        else if (resjson[i].SEABEACH == "金沙滩") 
            //            stationName = "WMT";

            //        date = new Date(resjson[i].FORECASTDATE);
            //        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
            //        //$("#"+stationName+"_01GC_H").val(resjson[i].GB24HTFFIRSTHIGHWAVEHOUR);
            //        //$("#"+stationName+"_01GC_MIN").val(resjson[i].GB24HTFFIRSTHIGHWAVEMINUTE);
            //        //$("#"+stationName+"_01DC_H").val(resjson[i].GB24HTFFIRSTLOWWAVEHOUR);
            //        //$("#"+stationName+"_01DC_MIN").val(resjson[i].GB24HTFFIRSTLOWWAVEMINUTE);
            //        //$("#"+stationName+"_02GC_H").val(resjson[i].GB24HTFSECONDHIGHWAVEHOUR);
            //        //$("#"+stationName+"_02GC_MIN").val(resjson[i].GB24HTFSECONDHIGHWAVEMINUTE);
            //        //$("#"+stationName+"_02DC_H").val(resjson[i].GB24HTFSECONDLOWWAVEHOUR);
            //        //$("#"+stationName+"_02DC_MIN").val(resjson[i].GB24HTFSECONDLOWWAVEMINUTE);
            //        $("#"+stationName+"_01G_H_00"+num).val(resjson[i].FIRSTHIGHTIME);
            //        $("#"+stationName+"_01G_CG_00"+num).val(resjson[i].FIRSTHIGHLEVEL);
            //        $("#"+stationName+"_02G_H_00"+num).val(resjson[i].SECONDHIGHTIME);
            //        $("#"+stationName+"_02G_CG_00"+num).val(resjson[i].SECONDHEIGHTLEVEL);
            //        $("#"+stationName+"_01D_H_00"+num).val(resjson[i].FIRSTLOWTIME);
            //        $("#"+stationName+"_01D_CG_00"+num).val(resjson[i].FIRSTLOWLEVEL);
            //        $("#"+stationName+"_02D_H_00"+num).val(resjson[i].SECONDLOWTIME);
            //        $("#"+stationName+"_02D_CG_00"+num).val(resjson[i].SECONDLOWLEVEL);
            //    }
            //}

            //表单19数据
            function gettable19(result, date1) {
                var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                       if (waveForecast[j].FORECASTAREA == "青岛近海") {
                            $("#QDJH_LG19").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "即墨近海") {
                           $("#JMJH_LG19").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "胶州湾") {
                           $("#JZJH_LG19").val(waveForecast[j].WAVE24FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "黄岛近海") {
                            $("#JNJH_LG19").val(waveForecast[j].WAVE24FORECAST);
                        }
                    }

                    var sw = result.sw;
                    for(var k = 0;k<sw.length;k++){
                        switch (sw[k].NAME) {
                        case "青岛近海": $("#QDJH_SW19").val(sw[k].MEAN_24H); break;
                        case "即墨近海": $("#JMJH_SW19").val(sw[k].MEAN_24H); break;
                        case "胶州近海": $("#JZJH_SW19").val(sw[k].MEAN_24H); break;
                        case "黄岛近海": $("#JNJH_SW19").val(sw[k].MEAN_24H); break;
                        default: break;
                        }
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
            }

            ////表单20数据
            //function gettable20(resjson, date1) {

            //    for (var i = 0; i < resjson.length; i++) {
            //        $("#01GC_H20").val(resjson[i].g1s);
            //        $("#01GC_MIN20").val(resjson[i].g1m);
            //        $("#01GC_CM20").val(resjson[i].g1g);
            //        $("#01DC_H20").val(resjson[i].d1s);
            //        $("#01DC_MIN20").val(resjson[i].d1m);
            //        $("#01DC_CM20").val(resjson[i].d1g);
            //        $("#02GC_H20").val(resjson[i].g2s);
            //        $("#02GC_MIN20").val(resjson[i].g2m);
            //        $("#02GC_CM20").val(resjson[i].g2g);
            //        $("#02DC_H20").val(resjson[i].d2s);
            //        $("#02DC_MIN20").val(resjson[i].d2m);
            //        $("#02DC_CM20").val(resjson[i].d2g);

            //    }
            //}

            //表单21数据
            function gettable21(result, date1) {
                var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var waveForecast = result.wave;
                    for(var j = 0;j<waveForecast.length;j++){
                       if (waveForecast[j].FORECASTAREA == "乳山近海") {
                           $("#RSS_LG_24").val(waveForecast[j].WAVE24FORECAST);
                           $("#RSS_LG_48").val(waveForecast[j].WAVE48FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "文登近海") {
                            $("#WDQ_LG_24").val(waveForecast[j].WAVE24FORECAST);
                            $("#WDQ_LG_48").val(waveForecast[j].WAVE48FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "石岛近海") {
                            $("#SDJH_LG_24").val(waveForecast[j].WAVE24FORECAST);
                            $("#SDJH_LG_48").val(waveForecast[j].WAVE48FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "成山头") {
                            $("#CST_LG_24").val(waveForecast[j].WAVE24FORECAST);
                            $("#CST_LG_48").val(waveForecast[j].WAVE48FORECAST);
                        }
                        else if (waveForecast[j].FORECASTAREA == "威海近海") {
                            $("#WHJH_LG_48").val(waveForecast[j].WAVE48FORECAST);
                        }
                    }

                    var sw = result.sw;
                     for(var k = 0;k<sw.length;k++){
                        var nqyname= "";
                        switch (sw[k].NAME) {
                            case "文登常规预报点":  nqyname = "WDQ"; break;
                            case "石岛常规预报点":  nqyname = "SDJH"; break;
                            case "成山头":  nqyname = "CST"; break;
                            case "南黄岛":  nqyname = "RSS"; break;
                            default: nqyname = ""; break;
                        }
                        $("#"+nqyname+"_SW_24_1").val(sw[k].MEAN_24H);
                        $("#"+nqyname+"_SW_48_1").val(sw[k].MEAN_48H);
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
            }

            ////表单22数据 下午十五、威海48小时潮汐预报
            //function gettable22(resjson, date1) {
            //    for (var i = 0; i < resjson.length; i++) {
            //        switch (resjson[i].qy) {
            //            case "石岛": nqyname = "SD"; break;
            //            case "文登": nqyname = "WD"; break;
            //            case "威海": nqyname = "WH"; break;
            //            case "成山头": nqyname = "CST"; break;
            //            case "乳山": nqyname = "RS"; break;
            //            default:
            //        }
            //        date = new Date(resjson[i].yb);
            //        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
            //        $("#" + nqyname + "_01G_CW_0" + num).val(resjson[i].g1c);
            //        $("#" + nqyname + "_01G_SJ_0" + num).val(resjson[i].g1s);
            //        $("#" + nqyname + "_01D_CW_0" + num).val(resjson[i].d1c);
            //        $("#" + nqyname + "_01D_SJ_0" + num).val(resjson[i].d1s);
            //        $("#" + nqyname + "_02G_CW_0" + num).val(resjson[i].g2c);
            //        $("#" + nqyname + "_02G_SJ_0" + num).val(resjson[i].g2s);
            //        $("#" + nqyname + "_02D_CW_0" + num).val(resjson[i].d2c);
            //        $("#" + nqyname + "_02D_SJ_0" + num).val(resjson[i].d2s);
            //    }
            //}

            //表单01数据 一、洋牧场-海浪预报
            function gettable47(result, date1) {
                var pbtypes = result.pbtype;
                var resjson=result.children;
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].OCEANRANCHNAME) {
                        case "寻山海洋牧场": nqyname = "CQ"; break;
                        case "荣成烟墩角游钓型海洋牧场": nqyname = "RC"; break;
                        case "西霞口集团国家级海洋牧场": nqyname = "XXK"; break;

                        case "滨州正海底播型海洋牧场": nqyname = "BZZH"; break;
                        case "山东通和底播型海洋牧场": nqyname = "SDTH"; break;
                        case "山东莱州太平湾明波国家级海洋牧场": nqyname = "TPW"; break;
                        case "山东琵琶口富瀚国家级海洋牧场": nqyname = "PPK"; break;
                        case "山东庙岛群岛东部佳益国家级海洋牧场": nqyname = "MDQD"; break;
                        case "山东海州湾顺风国家级海洋牧场": nqyname = "HZW"; break;
                        case "山东岚山东部万泽丰国家级海洋牧场": nqyname = "LSDBWZF"; break;
                        default:
                    }
                    if (pbtypes == "bys") {
                        $("#WAVE_" + nqyname + "_24_D").val(resjson[i].WAVE48HDAY);
                        $("#WAVE_" + nqyname + "_24_N").val(resjson[i].WAVE48HNEIGHT);
                        $("#WAVE_" + nqyname + "_48_D").val(resjson[i].WAVE72HDAY);
                        $("#WAVE_" + nqyname + "_48_N").val(resjson[i].WAVE72HNEIGHT);
                        $("#WAVE_" + nqyname + "_72_D").val(resjson[i].WAVE96HDAY);
                        $("#WAVE_" + nqyname + "_72_N").val(resjson[i].WAVE96HNEIGHT);
                    }
                    else if (pbtypes == "bydb") {
                        $("#WAVE_" + nqyname + "_24_D").val(resjson[i].WAVE24HDAY);
                        $("#WAVE_" + nqyname + "_24_N").val(resjson[i].WAVE24HNEIGHT);
                        $("#WAVE_" + nqyname + "_48_D").val(resjson[i].WAVE48HDAY);
                        $("#WAVE_" + nqyname + "_48_N").val(resjson[i].WAVE48HNEIGHT);
                        $("#WAVE_" + nqyname + "_72_D").val(resjson[i].WAVE72HDAY);
                        $("#WAVE_" + nqyname + "_72_N").val(resjson[i].WAVE72HNEIGHT);
                    }
                }
            }
            //表单02数据 二、海洋牧场-潮汐预报
            //function gettable48(resjson, date1) {
            //    for (var i = 0; i < resjson.length; i++) {
            //        var nqyname = "";
            //        var date = new Date(resjson[i].FORECASTDATE);
            //        switch (resjson[i].OCEANRANCHNAME) {
            //            case "寻山海洋牧场": nqyname = "CQ"; break;
            //            case "荣成烟墩角游钓型海洋牧场": nqyname = "RC"; break;
            //            case "西霞口集团国家级海洋牧场": nqyname = "XXK"; break;

            //            //新增加7个
            //            case "滨州正海底播型海洋牧场": nqyname = "BZZH"; break;
            //            case "山东通和底播型海洋牧场": nqyname = "SDTH"; break;
            //            case "山东莱州太平湾明波国家级海洋牧场": nqyname = "TPW"; break;
            //            case "山东琵琶口富瀚国家级海洋牧场": nqyname = "PPK"; break;
            //            case "山东庙岛群岛东部佳益国家级海洋牧场": nqyname = "MDQD"; break;
            //            case "山东海州湾顺风国家级海洋牧场": nqyname = "HZW"; break;
            //            case "山东岚山东部万泽丰国家级海洋牧场": nqyname = "LSDBWZF"; break;

            //            default:
            //        }

            //        var value = "";
            //        var num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;

            //        for (var j = 0; j < 24; j++) {
            //            if (j < 10) {
            //                value = "TIDE24H0" + j;
            //                $("#TIDE_" + nqyname + "_0" + num + "_H0" + j).val(resjson[i][value]);
            //            }
            //            else {
            //                value = "TIDE24H" + j;
            //                $("#TIDE_" + nqyname + "_0" + num + "_H" + j).val(resjson[i][value]);
            //            }
            //        }
            //        $("#TIDE_" + nqyname + "_0" + num + "_FIRSTHTIME").val(resjson[i].TIDEFIRSTHTIME);
            //        $("#TIDE_" + nqyname + "_0" + num + "_FIRSTHHEIGHT").val(resjson[i].TIDEFIRSTHHEIGHT);

            //        $("#TIDE_" + nqyname + "_0" + num + "_SECONDHTIME").val(resjson[i].TIDESECONDHTIME);
            //        $("#TIDE_" + nqyname + "_0" + num + "_SECONDHHEIGHT").val(resjson[i].TIDESECONDHHEIGHT);

            //        $("#TIDE_" + nqyname + "_0" + num + "_FIRSTLTIME").val(resjson[i].TIDEFIRSTLTIME);
            //        $("#TIDE_" + nqyname + "_0" + num + "_FIRSTLHEIGHT").val(resjson[i].TIDEFIRSTLHEIGHT);

            //        $("#TIDE_" + nqyname + "_0" + num + "_SECONDLTIME").val(resjson[i].TIDESECONDLTIME);
            //        $("#TIDE_" + nqyname + "_0" + num + "_SECONDLHEIGHT").val(resjson[i].TIDESECONDLHEIGHT);

            //        //$("#TIDE_" + nqyname + "_FORCASTDATE").val(resjson[i].FORECASTDATE);
            //    }
            //}
            //表单03数据 三、海洋牧场-海温预报
            function gettable49(result, date1) {
                var pbtypes = result.pbtype;
                var resjson = result.children;
                for (var i = 0; i < resjson.length; i++) {
                    var OCEANRANCHNAMES = resjson[i].OCEANRANCHNAME;
                    var nqyname = "";
                    switch (OCEANRANCHNAMES) {
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

                    if (pbtypes == "bys") {
                        $("#TEMP_" + nqyname + "_24").val(resjson[i].TEMP48H);
                        $("#TEMP_" + nqyname + "_48").val(resjson[i].TEMP72H);
                        $("#TEMP_" + nqyname + "_72").val(resjson[i].TEMP96H);
                    }
                    else if (pbtypes == "bydb") {
                        $("#TEMP_" + nqyname + "_24").val(resjson[i].TEMP24H);
                        $("#TEMP_" + nqyname + "_48").val(resjson[i].TEMP48H);
                        $("#TEMP_" + nqyname + "_72").val(resjson[i].TEMP72H);
                    }
                    
                }
            }


            //function gettable49(result, date1) {
            //    var pbtypes = result.pbtype;
            //    var resjson = result.children;
            //    for (var i = 0; i < resjson.length; i++) {
            //        var OCEANRANCHNAMES = resjson[i].OCEANRANCHNAME;
            //        if (OCEANRANCHNAMES == "寻山海洋牧场") {
            //            nqyname = "CQ";
            //        }
            //        else if (OCEANRANCHNAMES == "荣成烟墩角游钓型海洋牧场") {
            //            nqyname = "RC";
            //        }
            //        else if (OCEANRANCHNAMES == "西霞口集团国家级海洋牧场") {
            //            nqyname = "XXK";
            //        } else if (OCEANRANCHNAMES == "滨州正海底播型海洋牧场"){
            //            nqyname = "BZZH";
            //        }
            //        if (pbtypes == "bys") {
            //            $("#TEMP_" + nqyname + "_24").val(resjson[i].TEMP48H);
            //            $("#TEMP_" + nqyname + "_48").val(resjson[i].TEMP72H);
            //            $("#TEMP_" + nqyname + "_72").val(resjson[i].TEMP96H);
            //        }
            //        else if (pbtypes == "bydb") {
            //            $("#TEMP_" + nqyname + "_24").val(resjson[i].TEMP24H);
            //            $("#TEMP_" + nqyname + "_48").val(resjson[i].TEMP48H);
            //            $("#TEMP_" + nqyname + "_72").val(resjson[i].TEMP72H);
            //        }
            //    }
            //}

            function gettable53(result, date1) {
                var effect = result.pbtime;
                var resjson = result.children;
                var proyear = resjson[0].PROYEAR;
                var prono = resjson[0].PRONO;
                if (effect == "today") {
                    $('#ProSDYear').val(proyear);
                    if (prono * 1 < 10) {
                        prono = "00" + prono * 1;
                    } else if (prono * 1 >= 10 && prono * 1 < 100) {
                        prono = "0" + prono * 1;
                    } else {
                        prono = prono;
                    }
                    $('#ProSDNo').val(prono);
                }
                else {
                    var mydate = new Date();
                    var YearTime = mydate.getFullYear() + "-" + mydate.getMonth() + "-" + mydate.getDate();
                    var now = mydate.getFullYear() + "01-01";
                    if (YearTime == now) {
                        $('#ProSDYear').val(mydate.getFullYear());
                        $('#ProSDNo').val("001");
                    } else {
                        $('#ProSDYear').val(proyear);
                        prono = prono * 1 + 1;
                        if (prono * 1 < 10) {
                            prono = "00" + prono * 1;
                        } else if (prono * 1 > 10 && prono * 1 < 100) {
                            prono = "0" + prono * 1;
                        } else {
                            prono = prono;
                        }
                        $('#ProSDNo').val(prono);
                    }
                }
            }

           //七地市
           //潮汐
           ////下午二十四、东营广利渔港-未来三天高/低潮预报
           //function gettable54(resjson, date1) {
           //     for (var i = 0; i < resjson.length; i++) {
           //         date = new Date(resjson[i].FORECASTDATE);
           //         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
           //         $("#DYGL_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
           //         $("#DYGL_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
           //         $("#DYGL_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
           //         $("#DYGL_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
           //         $("#DYGL_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
           //         $("#DYGL_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
           //         $("#DYGL_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
           //         $("#DYGL_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
           //     }
           //}
           
           // //下午二十七、日照桃花岛-未来三天高/低潮预报
           // function gettable57(resjson, date1) {
           //     for (var i = 0; i < resjson.length; i++) {
           //         date = new Date(resjson[i].FORECASTDATE);
           //         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
           //         $("#RZTHD_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
           //         $("#RZTHD_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
           //         $("#RZTHD_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
           //         $("#RZTHD_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
           //         $("#RZTHD_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
           //         $("#RZTHD_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
           //         $("#RZTHD_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
           //         $("#RZTHD_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
           //     }
           //}
           // //下午三十、潍坊度假区-未来三天高/低潮预报
           // function gettable60(resjson, date1) {
           //     for (var i = 0; i < resjson.length; i++) {
           //         date = new Date(resjson[i].FORECASTDATE);
           //         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
           //         $("#WFDJQ_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
           //         $("#WFDJQ_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
           //         $("#WFDJQ_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
           //         $("#WFDJQ_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
           //         $("#WFDJQ_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
           //         $("#WFDJQ_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
           //         $("#WFDJQ_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
           //         $("#WFDJQ_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
           //     }
           //}
           // //下午三十三、威海新区-未来三天高/低潮预报
           // function gettable63(resjson, date1) {
           //     for (var i = 0; i < resjson.length; i++) {
           //         date = new Date(resjson[i].FORECASTDATE);
           //         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
           //         $("#WHXQ_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
           //         $("#WHXQ_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
           //         $("#WHXQ_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
           //         $("#WHXQ_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
           //         $("#WHXQ_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
           //         $("#WHXQ_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
           //         $("#WHXQ_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
           //         $("#WHXQ_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
           //     }
           //}
            //下午三十六、烟台清泉-未来三天高/低潮预报
           // function gettable66(resjson, date1) {
           //     for (var i = 0; i < resjson.length; i++) {
           //         date = new Date(resjson[i].FORECASTDATE);
           //         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
           //         $("#YTQQ_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
           //         $("#YTQQ_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
           //         $("#YTQQ_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
           //         $("#YTQQ_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
           //         $("#YTQQ_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
           //         $("#YTQQ_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
           //         $("#YTQQ_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
           //         $("#YTQQ_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
           //     }
           //}
            //下午三十九、董家口-未来三天高/低潮预报
           // function gettable69(resjson, date1) {
           //     for (var i = 0; i < resjson.length; i++) {
           //         date = new Date(resjson[i].FORECASTDATE);
           //         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
           //         $("#DJKP_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
           //         $("#DJKP_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
           //         $("#DJKP_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
           //         $("#DJKP_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
           //         $("#DJKP_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
           //         $("#DJKP_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
           //         $("#DJKP_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
           //         $("#DJKP_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
           //     }
           //}
           // //下午四十二、东营渔港-未来三天高/低潮预报
           // function gettable72(resjson, date1) {
           //     for (var i = 0; i < resjson.length; i++) {
           //         date = new Date(resjson[i].FORECASTDATE);
           //         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
           //         $("#DYFP_01G_CS_0" + num).val(resjson[i].FIRSTHIGHTIME);
           //         $("#DYFP_01G_CG_0" + num).val(resjson[i].FIRSTHIGHLEVEL);
           //         $("#DYFP_01D_CS_0" + num).val(resjson[i].FIRSTLOWTIME);
           //         $("#DYFP_01D_CG_0" + num).val(resjson[i].FIRSTLOWLEVEL);
           //         $("#DYFP_02G_CS_0" + num).val(resjson[i].SECONDHIGHTIME);
           //         $("#DYFP_02G_CG_0" + num).val(resjson[i].SECONDHIGHLEVEL);
           //         $("#DYFP_02D_CS_0" + num).val(resjson[i].SECONDLOWTIME);
           //         $("#DYFP_02D_CG_0" + num).val(resjson[i].SECONDLOWLEVEL);
           //     }
           //}

            //七地市
            //风浪
            //下午二十五、东营广利渔港-未来三天的海面风及海浪有效波高预报（20时起报）
            function gettable55(result, date1) {
                 var pbtype = result.pbtype;
                 if(pbtype == "bydb"){
                     var resjson=result.children;
                     for (var i = 0; i < resjson.length; i++) {
                         date = new Date(resjson[i].FORECASTDATE);
                         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                         switch (num) {
                             case 1:
                                 $("#DYGL_12H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DYGL_12H_02").val(resjson[i].WINDFORCE);
                                 $("#DYGL_12H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 2:
                                 $("#DYGL_24H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DYGL_24H_02").val(resjson[i].WINDFORCE);
                                 $("#DYGL_24H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 3:
                                 $("#DYGL_48H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DYGL_48H_02").val(resjson[i].WINDFORCE);
                                 $("#DYGL_48H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 4:
                                 $("#DYGL_72H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DYGL_72H_02").val(resjson[i].WINDFORCE);
                                 $("#DYGL_72H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                         }
                     }
                 }
                 else if (pbtype == "bys") {
                     var waveForecast = result.wave;
                     for(var j = 0;j<waveForecast.length;j++){
                         $("#DYGL_12H_03").val(waveForecast[j].WAVE00_24H);
                         $("#DYGL_24H_03").val(waveForecast[j].WAVE12_24H);
                         $("#DYGL_48H_03").val(waveForecast[j].WAVE24_48H);
                         $("#DYGL_72H_03").val(waveForecast[j].WAVE48_72H);
                     }

                     var windForecast = result.wind;
                     for(var k = 0;k<windForecast.length;k++){
                         $("#DYGL_12H_01").val(windForecast[k].WINDDIRECTION12H);//12h的风向
                         $("#DYGL_24H_01").val(windForecast[k].WINDDIRECTION24H);//24的风向
                         $("#DYGL_48H_01").val(windForecast[k].WINDDIRECTION48H);//48的风向
                         $("#DYGL_72H_01").val(windForecast[k].WINDDIRECTION72H);//72的风向

                         $("#DYGL_12H_02").val(windForecast[k].WINDSPEED12H);//12的风速
                         $("#DYGL_24H_02").val(windForecast[k].WINDSPEED24H);//24的风速
                         $("#DYGL_48H_02").val(windForecast[k].WINDSPEED48H);//48的风速
                         $("#DYGL_72H_02").val(windForecast[k].WINDSPEED72H);//72的风速


                         //原来的
                         //$("#DYGL_12H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#DYGL_12H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#DYGL_24H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#DYGL_24H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#DYGL_48H_01").val(windForecast[k].WINDDIRECTION48FORECAST);
                         //$("#DYGL_48H_02").val(windForecast[k].WINDFORCE48FORECAST);

                         //$("#DYGL_72H_01").val(windForecast[k].WINDDIRECTION72FORECAST);
                         //$("#DYGL_72H_02").val(windForecast[k].WINDFORCE72FORECAST);
                     }
                 }
            }
            //下午二十八、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）
            function gettable58(result, date1) {
                 var pbtype = result.pbtype;
                 if(pbtype == "bydb"){
                     var resjson=result.children;
                     for (var i = 0; i < resjson.length; i++) {
                         date = new Date(resjson[i].FORECASTDATE);
                         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                         switch (num) {
                             case 1:
                                 $("#RZTHD_12H_01").val(resjson[i].WINDDIRECTION);
                                 $("#RZTHD_12H_02").val(resjson[i].WINDFORCE);
                                 $("#RZTHD_12H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 2:
                                 $("#RZTHD_24H_01").val(resjson[i].WINDDIRECTION);
                                 $("#RZTHD_24H_02").val(resjson[i].WINDFORCE);
                                 $("#RZTHD_24H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 3:
                                 $("#RZTHD_48H_01").val(resjson[i].WINDDIRECTION);
                                 $("#RZTHD_48H_02").val(resjson[i].WINDFORCE);
                                 $("#RZTHD_48H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 4:
                                 $("#RZTHD_72H_01").val(resjson[i].WINDDIRECTION);
                                 $("#RZTHD_72H_02").val(resjson[i].WINDFORCE);
                                 $("#RZTHD_72H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                         }
                     }
                 }
                 else if (pbtype == "bys") {
                     var waveForecast = result.wave;
                     for(var j = 0;j<waveForecast.length;j++){
                         $("#RZTHD_12H_03").val(waveForecast[j].WAVE00_24H);
                         $("#RZTHD_24H_03").val(waveForecast[j].WAVE12_24H);
                         $("#RZTHD_48H_03").val(waveForecast[j].WAVE24_48H);
                         $("#RZTHD_72H_03").val(waveForecast[j].WAVE48_72H);
                     }

                     var windForecast = result.wind;
                     for(var k = 0;k<windForecast.length;k++){
                         $("#RZTHD_12H_01").val(windForecast[k].WINDDIRECTION12H);//12h的风向
                         $("#RZTHD_24H_01").val(windForecast[k].WINDDIRECTION24H);//24的风向
                         $("#RZTHD_48H_01").val(windForecast[k].WINDDIRECTION48H);//48的风向
                         $("#RZTHD_72H_01").val(windForecast[k].WINDDIRECTION72H);//72的风向
                             
                         $("#RZTHD_12H_02").val(windForecast[k].WINDSPEED12H);//12的风速
                         $("#RZTHD_24H_02").val(windForecast[k].WINDSPEED24H);//24的风速
                         $("#RZTHD_48H_02").val(windForecast[k].WINDSPEED48H);//48的风速
                         $("#RZTHD_72H_02").val(windForecast[k].WINDSPEED72H);//72的风速

                         //原来的
                         //$("#RZTHD_12H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#RZTHD_12H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#RZTHD_24H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#RZTHD_24H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#RZTHD_48H_01").val(windForecast[k].WINDDIRECTION48FORECAST);
                         //$("#RZTHD_48H_02").val(windForecast[k].WINDFORCE48FORECAST);

                         //$("#RZTHD_72H_01").val(windForecast[k].WINDDIRECTION72FORECAST);
                         //$("#RZTHD_72H_02").val(windForecast[k].WINDFORCE72FORECAST);
                     }
                 }
            }
            //下午三十一、潍坊度假区-未来三天的海面风及海浪有效波高预报（20时起报）
            function gettable61(result, date1) {
                 var pbtype = result.pbtype;
                 if(pbtype == "bydb"){
                     var resjson=result.children;
                     for (var i = 0; i < resjson.length; i++) {
                         date = new Date(resjson[i].FORECASTDATE);
                         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                         switch (num) {
                             case 1:
                                 $("#WFDJQ_12H_01").val(resjson[i].WINDDIRECTION);
                                 $("#WFDJQ_12H_02").val(resjson[i].WINDFORCE);
                                 $("#WFDJQ_12H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 2:
                                 $("#WFDJQ_24H_01").val(resjson[i].WINDDIRECTION);
                                 $("#WFDJQ_24H_02").val(resjson[i].WINDFORCE);
                                 $("#WFDJQ_24H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 3:
                                 $("#WFDJQ_48H_01").val(resjson[i].WINDDIRECTION);
                                 $("#WFDJQ_48H_02").val(resjson[i].WINDFORCE);
                                 $("#WFDJQ_48H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 4:
                                 $("#WFDJQ_72H_01").val(resjson[i].WINDDIRECTION);
                                 $("#WFDJQ_72H_02").val(resjson[i].WINDFORCE);
                                 $("#WFDJQ_72H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                         }
                     }
                 }
                 else if (pbtype == "bys") {
                     var waveForecast = result.wave;
                     for(var j = 0;j<waveForecast.length;j++){
                         $("#WFDJQ_12H_03").val(waveForecast[j].WAVE00_24H);
                         $("#WFDJQ_24H_03").val(waveForecast[j].WAVE12_24H);
                         $("#WFDJQ_48H_03").val(waveForecast[j].WAVE24_48H);
                         $("#WFDJQ_72H_03").val(waveForecast[j].WAVE48_72H);
                     }

                     var windForecast = result.wind;
                     for(var k = 0;k<windForecast.length;k++){
                         $("#WFDJQ_12H_01").val(windForecast[k].WINDDIRECTION12H);//12h的风向
                         $("#WFDJQ_24H_01").val(windForecast[k].WINDDIRECTION24H);//24的风向
                         $("#WFDJQ_48H_01").val(windForecast[k].WINDDIRECTION48H);//48的风向
                         $("#WFDJQ_72H_01").val(windForecast[k].WINDDIRECTION72H);//72的风向
                             
                         $("#WFDJQ_12H_02").val(windForecast[k].WINDSPEED12H);//12的风速
                         $("#WFDJQ_24H_02").val(windForecast[k].WINDSPEED24H);//24的风速
                         $("#WFDJQ_48H_02").val(windForecast[k].WINDSPEED48H);//48的风速
                         $("#WFDJQ_72H_02").val(windForecast[k].WINDSPEED72H);//72的风速

                         //原来的
                         //$("#WFDJQ_12H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#WFDJQ_12H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#WFDJQ_24H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#WFDJQ_24H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#WFDJQ_48H_01").val(windForecast[k].WINDDIRECTION48FORECAST);
                         //$("#WFDJQ_48H_02").val(windForecast[k].WINDFORCE48FORECAST);

                         //$("#WFDJQ_72H_01").val(windForecast[k].WINDDIRECTION72FORECAST);
                         //$("#WFDJQ_72H_02").val(windForecast[k].WINDFORCE72FORECAST);
                     }
                 }
            }
            //下午三十四、威海新区-未来三天的海面风及海浪有效波高预报（20时起报）
            function gettable64(result, date1) {
                 var pbtype = result.pbtype;
                 if(pbtype == "bydb"){
                     var resjson=result.children;
                     for (var i = 0; i < resjson.length; i++) {
                         date = new Date(resjson[i].FORECASTDATE);
                         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                         switch (num) {
                             case 1:
                                 $("#WHXQ_12H_01").val(resjson[i].WINDDIRECTION);
                                 $("#WHXQ_12H_02").val(resjson[i].WINDFORCE);
                                 $("#WHXQ_12H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 2:
                                 $("#WHXQ_24H_01").val(resjson[i].WINDDIRECTION);
                                 $("#WHXQ_24H_02").val(resjson[i].WINDFORCE);
                                 $("#WHXQ_24H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 3:
                                 $("#WHXQ_48H_01").val(resjson[i].WINDDIRECTION);
                                 $("#WHXQ_48H_02").val(resjson[i].WINDFORCE);
                                 $("#WHXQ_48H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 4:
                                 $("#WHXQ_72H_01").val(resjson[i].WINDDIRECTION);
                                 $("#WHXQ_72H_02").val(resjson[i].WINDFORCE);
                                 $("#WHXQ_72H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                         }
                     }
                 }
                 else if (pbtype == "bys") {
                     var waveForecast = result.wave;
                     for(var j = 0;j<waveForecast.length;j++){
                         $("#WHXQ_12H_03").val(waveForecast[j].WAVE00_24H);
                         $("#WHXQ_24H_03").val(waveForecast[j].WAVE12_24H);
                         $("#WHXQ_48H_03").val(waveForecast[j].WAVE24_48H);
                         $("#WHXQ_72H_03").val(waveForecast[j].WAVE48_72H);
                     }

                     var windForecast = result.wind;
                     for(var k = 0;k<windForecast.length;k++){
                         $("#WHXQ_12H_01").val(windForecast[k].WINDDIRECTION12H);//12h的风向
                         $("#WHXQ_24H_01").val(windForecast[k].WINDDIRECTION24H);//24的风向
                         $("#WHXQ_48H_01").val(windForecast[k].WINDDIRECTION48H);//48的风向
                         $("#WHXQ_72H_01").val(windForecast[k].WINDDIRECTION72H);//72的风向
                             
                         $("#WHXQ_12H_02").val(windForecast[k].WINDSPEED12H);//12的风速
                         $("#WHXQ_24H_02").val(windForecast[k].WINDSPEED24H);//24的风速
                         $("#WHXQ_48H_02").val(windForecast[k].WINDSPEED48H);//48的风速
                         $("#WHXQ_72H_02").val(windForecast[k].WINDSPEED72H);//72的风速

                         //原来的
                         //$("#WHXQ_12H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#WHXQ_12H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#WHXQ_24H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#WHXQ_24H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#WHXQ_48H_01").val(windForecast[k].WINDDIRECTION48FORECAST);
                         //$("#WHXQ_48H_02").val(windForecast[k].WINDFORCE48FORECAST);

                         //$("#WHXQ_72H_01").val(windForecast[k].WINDDIRECTION72FORECAST);
                         //$("#WHXQ_72H_02").val(windForecast[k].WINDFORCE72FORECAST);
                     }
                 }
            }
            //下午三十七、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）
            function gettable67(result, date1) {
                 var pbtype = result.pbtype;
                 if(pbtype == "bydb"){
                     var resjson=result.children;
                     for (var i = 0; i < resjson.length; i++) {
                         date = new Date(resjson[i].FORECASTDATE);
                         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                         switch (num) {
                             case 1:
                                 $("#YTQQ_12H_01").val(resjson[i].WINDDIRECTION);
                                 $("#YTQQ_12H_02").val(resjson[i].WINDFORCE);
                                 $("#YTQQ_12H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 2:
                                 $("#YTQQ_24H_01").val(resjson[i].WINDDIRECTION);
                                 $("#YTQQ_24H_02").val(resjson[i].WINDFORCE);
                                 $("#YTQQ_24H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 3:
                                 $("#YTQQ_48H_01").val(resjson[i].WINDDIRECTION);
                                 $("#YTQQ_48H_02").val(resjson[i].WINDFORCE);
                                 $("#YTQQ_48H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 4:
                                 $("#YTQQ_72H_01").val(resjson[i].WINDDIRECTION);
                                 $("#YTQQ_72H_02").val(resjson[i].WINDFORCE);
                                 $("#YTQQ_72H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                         }
                     }
                 }
                 else if (pbtype == "bys") {
                     var waveForecast = result.wave;
                     for(var j = 0;j<waveForecast.length;j++){
                         $("#YTQQ_12H_03").val(waveForecast[j].WAVE00_24H);
                         $("#YTQQ_24H_03").val(waveForecast[j].WAVE12_24H);
                         $("#YTQQ_48H_03").val(waveForecast[j].WAVE24_48H);
                         $("#YTQQ_72H_03").val(waveForecast[j].WAVE48_72H);
                     }

                     var windForecast = result.wind;
                     for(var k = 0;k<windForecast.length;k++){
                         $("#YTQQ_12H_01").val(windForecast[k].WINDDIRECTION12H);//12h的风向
                         $("#YTQQ_24H_01").val(windForecast[k].WINDDIRECTION24H);//24的风向
                         $("#YTQQ_48H_01").val(windForecast[k].WINDDIRECTION48H);//48的风向
                         $("#YTQQ_72H_01").val(windForecast[k].WINDDIRECTION72H);//72的风向
                             
                         $("#YTQQ_12H_02").val(windForecast[k].WINDSPEED12H);//12的风速
                         $("#YTQQ_24H_02").val(windForecast[k].WINDSPEED24H);//24的风速
                         $("#YTQQ_48H_02").val(windForecast[k].WINDSPEED48H);//48的风速
                         $("#YTQQ_72H_02").val(windForecast[k].WINDSPEED72H);//72的风速

                         //原来的
                         //$("#YTQQ_12H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#YTQQ_12H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#YTQQ_24H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#YTQQ_24H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#YTQQ_48H_01").val(windForecast[k].WINDDIRECTION48FORECAST);
                         //$("#YTQQ_48H_02").val(windForecast[k].WINDFORCE48FORECAST);

                         //$("#YTQQ_72H_01").val(windForecast[k].WINDDIRECTION72FORECAST);
                         //$("#YTQQ_72H_02").val(windForecast[k].WINDFORCE72FORECAST);
                     }
                 }
            }
            //下午四十、董家口-未来三天的海面风及海浪有效波高预报（20时起报）
            function gettable70(result, date1) {
                 var pbtype = result.pbtype;
                 if(pbtype == "bydb"){
                     var resjson=result.children;
                     for (var i = 0; i < resjson.length; i++) {
                         date = new Date(resjson[i].FORECASTDATE);
                         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                         switch (num) {
                             case 1:
                                 $("#DJKP_12H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DJKP_12H_02").val(resjson[i].WINDFORCE);
                                 $("#DJKP_12H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 2:
                                 $("#DJKP_24H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DJKP_24H_02").val(resjson[i].WINDFORCE);
                                 $("#DJKP_24H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 3:
                                 $("#DJKP_48H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DJKP_48H_02").val(resjson[i].WINDFORCE);
                                 $("#DJKP_48H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 4:
                                 $("#DJKP_72H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DJKP_72H_02").val(resjson[i].WINDFORCE);
                                 $("#DJKP_72H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                         }
                     }
                 }
                 else if (pbtype == "bys") {
                     var waveForecast = result.wave;
                     for(var j = 0;j<waveForecast.length;j++){
                         //$("#DJKP_12H_03").val(waveForecast[j].WAVE24FORECAST);
                         //$("#DJKP_24H_03").val(waveForecast[j].WAVE24FORECAST);
                         //$("#DJKP_48H_03").val(waveForecast[j].WAVE48FORECAST);
                         //$("#DJKP_72H_03").val(waveForecast[j].WAVE72FORECAST);

                         $("#DJKP_12H_03").val(waveForecast[j].WAVE00_24H);
                         $("#DJKP_24H_03").val(waveForecast[j].WAVE12_24H);
                         $("#DJKP_48H_03").val(waveForecast[j].WAVE24_48H);
                         $("#DJKP_72H_03").val(waveForecast[j].WAVE48_72H);
                     }

                     var windForecast = result.wind;
                     for(var k = 0;k<windForecast.length;k++){

                         $("#DJKP_12H_01").val(windForecast[k].WINDDIRECTION12H);//12h的风向
                         $("#DJKP_24H_01").val(windForecast[k].WINDDIRECTION24H);//24的风向
                         $("#DJKP_48H_01").val(windForecast[k].WINDDIRECTION48H);//48的风向
                         $("#DJKP_72H_01").val(windForecast[k].WINDDIRECTION72H);//72的风向
                             
                         $("#DJKP_12H_02").val(windForecast[k].WINDSPEED12H);//12的风速
                         $("#DJKP_24H_02").val(windForecast[k].WINDSPEED24H);//24的风速
                         $("#DJKP_48H_02").val(windForecast[k].WINDSPEED48H);//48的风速
                         $("#DJKP_72H_02").val(windForecast[k].WINDSPEED72H);//72的风速


                         //原来的
                         //$("#DJKP_12H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#DJKP_12H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#DJKP_24H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#DJKP_24H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#DJKP_48H_01").val(windForecast[k].WINDDIRECTION48FORECAST);
                         //$("#DJKP_48H_02").val(windForecast[k].WINDFORCE48FORECAST);

                         //$("#DJKP_72H_01").val(windForecast[k].WINDDIRECTION72FORECAST);
                         //$("#DJKP_72H_02").val(windForecast[k].WINDFORCE72FORECAST);
                     }
                 }
            }
            //下午四十三、东营渔港-未来三天的海面风及海浪有效波高预报（20时起报）
            function gettable73(result, date1) {
                 var pbtype = result.pbtype;
                 if(pbtype == "bydb"){
                     var resjson=result.children;
                     for (var i = 0; i < resjson.length; i++) {
                         date = new Date(resjson[i].FORECASTDATE);
                         num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                         switch (num) {
                             case 1:
                                 $("#DYFP_12H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DYFP_12H_02").val(resjson[i].WINDFORCE);
                                 $("#DYFP_12H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 2:
                                 $("#DYFP_24H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DYFP_24H_02").val(resjson[i].WINDFORCE);
                                 $("#DYFP_24H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 3:
                                 $("#DYFP_48H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DYFP_48H_02").val(resjson[i].WINDFORCE);
                                 $("#DYFP_48H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                             case 4:
                                 $("#DYFP_72H_01").val(resjson[i].WINDDIRECTION);
                                 $("#DYFP_72H_02").val(resjson[i].WINDFORCE);
                                 $("#DYFP_72H_03").val(resjson[i].WAVEHEIGHT);
                                 break;
                         }
                     }
                 }
                 else if (pbtype == "bys") {
                     var waveForecast = result.wave;
                     for(var j = 0;j<waveForecast.length;j++){
                         //$("#DYFP_12H_03").val(waveForecast[j].WAVE24FORECAST);
                         //$("#DYFP_24H_03").val(waveForecast[j].WAVE24FORECAST);
                         //$("#DYFP_48H_03").val(waveForecast[j].WAVE48FORECAST);
                         //$("#DYFP_72H_03").val(waveForecast[j].WAVE72FORECAST);

                         $("#DYFP_12H_03").val(waveForecast[j].WAVE00_24H);
                         $("#DYFP_24H_03").val(waveForecast[j].WAVE12_24H);
                         $("#DYFP_48H_03").val(waveForecast[j].WAVE24_48H);
                         $("#DYFP_72H_03").val(waveForecast[j].WAVE48_72H);
                     }

                     var windForecast = result.wind;
                     for(var k = 0;k<windForecast.length;k++){

                         $("#DYFP_12H_01").val(windForecast[k].WINDDIRECTION12H);//12h的风向
                         $("#DYFP_24H_01").val(windForecast[k].WINDDIRECTION24H);//24的风向
                         $("#DYFP_48H_01").val(windForecast[k].WINDDIRECTION48H);//48的风向
                         $("#DYFP_72H_01").val(windForecast[k].WINDDIRECTION72H);//72的风向
                             
                         $("#DYFP_12H_02").val(windForecast[k].WINDSPEED12H);//12的风速
                         $("#DYFP_24H_02").val(windForecast[k].WINDSPEED24H);//24的风速
                         $("#DYFP_48H_02").val(windForecast[k].WINDSPEED48H);//48的风速
                         $("#DYFP_72H_02").val(windForecast[k].WINDSPEED72H);//72的风速


                         //原来的
                         //$("#DYFP_12H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#DYFP_12H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#DYFP_24H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                         //$("#DYFP_24H_02").val(windForecast[k].WINDFORCE24FORECAST);

                         //$("#DYFP_48H_01").val(windForecast[k].WINDDIRECTION48FORECAST);
                         //$("#DYFP_48H_02").val(windForecast[k].WINDFORCE48FORECAST);

                         //$("#DYFP_72H_01").val(windForecast[k].WINDDIRECTION72FORECAST);
                         //$("#DYFP_72H_02").val(windForecast[k].WINDFORCE72FORECAST);
                     }
                 }
            }

            //七地市
            //海温
            //下午二十六、东营广利渔港-未来三天的海面水温预报
            function gettable56(result, date1) {
                var pbtypes = result.pbtime;
                var resjson=result.children;
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    if (pbtypes == "yesterday") {
                        $("#DYGL_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                    else if (pbtypes == "today") {
                        $("#DYGL_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                }
            }
            //下午二十九、日照桃花岛-未来三天的海面水温预报
            function gettable59(result, date1) {
                var pbtypes = result.pbtime;
                var resjson=result.children;
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    if (pbtypes == "yesterday") {
                        $("#RZTHD_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                    else if (pbtypes == "today") {
                        $("#RZTHD_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                }
            }
            //下午三十二、潍坊度假区-未来三天的海面水温预报
            function gettable62(result, date1) {
                var pbtypes = result.pbtime;
                var resjson=result.children;
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    if (pbtypes == "yesterday") {
                        $("#WFDJQ_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                    else if (pbtypes == "today") {
                        $("#WFDJQ_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                }
            }
            //下午三十五、威海新区-未来三天的海面水温预报
            function gettable65(result, date1) {
                var pbtypes = result.pbtime;
                var resjson=result.children;
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    if (pbtypes == "yesterday") {
                        $("#WHXQ_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                    else if (pbtypes == "today") {
                        $("#WHXQ_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                }
            }
            //下午三十八、烟台清泉-未来三天的海面水温预报
            function gettable68(result, date1) {
                var pbtypes = result.pbtime;
                var resjson=result.children;
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    if (pbtypes == "yesterday") {
                        $("#YTQQ_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                    else if (pbtypes == "today") {
                        $("#YTQQ_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                }
            }
             //下午四十一、董家口-未来三天的海面水温预报
            function gettable71(result, date1) {
                var pbtypes = result.pbtime;
                var resjson=result.children;
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    if (pbtypes == "yesterday") {
                        $("#DJKP_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                    else if (pbtypes == "today") {
                        $("#DJKP_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                }
            }
            //下午四十四、东营渔港-未来三天的海面水温预报
            function gettable74(result, date1) {
                var pbtypes = result.pbtime;
                var resjson=result.children;
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].FORECASTDATE);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    if (pbtypes == "yesterday") {
                        $("#DYFP_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
                    else if (pbtypes == "today") {
                        $("#DYFP_SW_0" + num).val(resjson[i].WATERTEMPERATURE);
                    }
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
                        $("#uniform-Shuiwen span").attr("code",resjson[i].sw);
                        $("#ShuiwenTel").val(resjson[i].swtel);
                    }
                    if (resjson[i].hl != "" && resjson[i].hl != null) {
                     $("#uniform-Hailang span").text(resjson[i].hl);
                     $("#uniform-Hailang span").attr("code",resjson[i].hl);
                     $("#HailangTel").val(resjson[i].hltel);
                    }
                }
            }

            function count(o) {

                var t = typeof o;
                if (t == 'string') {

                    return o.length;

                } else if (t == 'object') {

                    var n = 0;

                    for (var i in o) {
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
                   
            //表单指挥处16点填报数据
            function gettable26(resjson, date1) {
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

            function gettable35(result, date1) {
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
                                    if (effect == "yesterday") {
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

            //下午二十一、东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
            function gettable41(result, date1) {
                var effect = result.pbtime;
                if (effect == "today") {
                    var resjson = result.children;
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
                    var waveForecast = result.wave;
                    for (var j = 0; j < waveForecast.length; j++) {
                        $("#DY_24H_03").val(waveForecast[j].WAVE24FORECAST);
                        $("#DY_48H_03").val(waveForecast[j].WAVE48FORECAST);
                        $("#DY_72H_03").val(waveForecast[j].WAVE72FORECAST);
                    }

                    var windForecast = result.wind;
                    for (var k = 0; k < windForecast.length; k++) {
                        $("#DY_24H_01").val(windForecast[k].WINDDIRECTION24FORECAST);
                        $("#DY_24H_02").val(windForecast[k].WINDFORCE24FORECAST);

                        $("#DY_48H_01").val(windForecast[k].WINDDIRECTION48FORECAST);
                        $("#DY_48H_02").val(windForecast[k].WINDFORCE48FORECAST);

                        $("#DY_72H_01").val(windForecast[k].WINDDIRECTION72FORECAST);
                        $("#DY_72H_02").val(windForecast[k].WINDFORCE72FORECAST);
                    }
                }
            }

            ////下午二十、东营埕岛-未来三天高/低潮预报
            //function gettable42(resjson, date1) {
            //    for (var i = 0; i < resjson.length; i++) {
            //        date = new Date(resjson[i].FORECASTDATE);
            //        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
            //        $("#DY_01G_CS_0" + num).val(resjson[i].NOTFFIRSTHIGHWAVETIME);
            //        $("#DY_01G_CG_0" + num).val(resjson[i].NOTFFIRSTHIGHWAVEHEIGHT);
            //        $("#DY_01D_CS_0" + num).val(resjson[i].NOTFFIRSTLOWWAVETIME);
            //        $("#DY_01D_CG_0" + num).val(resjson[i].NOTFFIRSTLOWWAVEHEIGHT);
            //        $("#DY_02G_CS_0" + num).val(resjson[i].NOTFSECONDHIGHWAVETIME);
            //        $("#DY_02G_CG_0" + num).val(resjson[i].NOTFSECONDHIGHWAVEHEIGHT);
            //        $("#DY_02D_CS_0" + num).val(resjson[i].NOTFSECONDLOWWAVETIME);
            //        $("#DY_02D_CG_0" + num).val(resjson[i].NOTFSECONDLOWWAVEHEIGHT);
            //    }
            //}

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
            }


<%------------------------------------  获取潮汐数据-----------------------------------------%>
        //{
        //    function procTideData(stationCode, i, tide) {
        //        var index, s_01G_SJ, s_01G_CW, s_01D_SJ, s_01D_CW, s_02G_SJ, s_02G_CW, s_02D_SJ, s_02D_CW;
        //        index = i + 1;
        //        s_01G_SJ = "#" + stationCode + "_01G_SJ_0" + index;
        //        s_01G_CW = "#" + stationCode + "_01G_CW_0" + index;
        //        s_01D_SJ = "#" + stationCode + "_01D_SJ_0" + index;
        //        s_01D_CW = "#" + stationCode + "_01D_CW_0" + index;
        //        s_02G_SJ = "#" + stationCode + "_02G_SJ_0" + index;
        //        s_02G_CW = "#" + stationCode + "_02G_CW_0" + index;
        //        s_02D_SJ = "#" + stationCode + "_02D_SJ_0" + index;
        //        s_02D_CW = "#" + stationCode + "_02D_CW_0" + index;
        //        $(s_01G_SJ).val(tide.FSTHIGHWIDETIME.replace(":", ""));
        //        $(s_01G_CW).val(tide.FSTHIGHWIDEHEIGHT);
        //        $(s_01D_SJ).val(tide.FSTLOWWIDETIME.replace(":", ""));
        //        $(s_01D_CW).val(tide.FSTLOWWIDEHEIGHT);
        //        $(s_02G_SJ).val(tide.SCDHIGHWIDETIME.replace(":", ""));
        //        $(s_02G_CW).val(tide.SCDHIGHWIDEHEIGHT);
        //        $(s_02D_SJ).val(tide.SCDLOWWIDETIME.replace(":", ""));
        //        $(s_02D_CW).val(tide.SCDLOWWIDEHEIGHT);
        //    }
        //    //下午四，青岛24小时天文潮数据
        //    function procQD24TideData(tide) {
        //        var fstHTime, fstLTime, scdHTime, scdLTime;
        //        fstHTime = tide.FSTHIGHWIDETIME;
        //        fstLTime = tide.FSTLOWWIDETIME;
        //        scdHTime = tide.SCDHIGHWIDETIME;
        //        scdLTime = tide.SCDLOWWIDETIME;
        //        if (fstHTime.indexOf("-") == -1) {
        //            var arr = fstHTime.split(":");
        //            $("#01GC_H").val(arr[0]);
        //            $("#01GC_MIN").val(arr[1]);
        //            $("#01GC_CM").val(tide.FSTHIGHWIDEHEIGHT);
        //        }
        //        else {
        //            $("#01GC_H").val("-");
        //            $("#01GC_MIN").val("-");
        //            $("#01GC_CM").val(tide.FSTHIGHWIDEHEIGHT);
        //        }
        //        if (fstLTime.indexOf("-") == -1) {
        //            var arr = fstLTime.split(":");
        //            $("#01DC_H").val(arr[0]);
        //            $("#01DC_MIN").val(arr[1]);
        //            $("#01DC_CM").val(tide.FSTLOWWIDEHEIGHT);
        //        }
        //        else {
        //            $("#01DC_H").val("-");
        //            $("#01DC_MIN").val("-");
        //            $("#01DC_CM").val(tide.FSTLOWWIDEHEIGHT);
        //        }
        //        if (scdHTime.indexOf("-") == -1) {
        //            var arr = scdHTime.split(":");
        //            $("#02GC_H").val(arr[0]);
        //            $("#02GC_MIN").val(arr[1]);
        //            $("#02GC_CM").val(tide.SCDHIGHWIDEHEIGHT);
        //        }
        //        else {
        //            $("#02GC_H").val("-");
        //            $("#02GC_MIN").val("-");
        //            $("#02GC_CM").val(tide.SCDHIGHWIDEHEIGHT);
        //        }
        //        if (scdLTime.indexOf("-") == -1) {
        //            var arr = scdLTime.split(":");
        //            $("#02DC_H").val(arr[0]);
        //            $("#02DC_MIN").val(arr[1]);
        //            $("#02DC_CM").val(tide.SCDLOWWIDEHEIGHT);
        //        }
        //        else {
        //            $("#02DC_H").val("-");
        //            $("#02DC_MIN").val("-");
        //            $("#02DC_CM").val(tide.SCDLOWWIDEHEIGHT);
        //        }
        //    }

        //    function proc7City24TideData(i, tide) {
        //        if (i % 3 == 0) {
        //            i = 3;
        //        }
        //        else if (i % 3 == 1) {
        //            i = 1;
        //        }
        //        else if (i % 3 == 2) {
        //            i = 2;
        //        }
        //        //for (var k = 1; k < 4; k++) {
        //        var matchValue, stationName, s_01G_H, s_01G_MIN, s_01D_H, s_01D_MIN, s_02G_H, s_02G_MIN, s_02D_H, s_02D_MIN, fstHTime, fstLTime, scdHTime, scdLTime, fstHValue, fstLValue, scdHValue, scdLValue, s_01G_CG, s_01D_CG, s_02G_CG, s_02D_CG;
        //        matchValue = tide.STATION;
        //        stationName = matchValue === "101wmt" ? "QD" : matchValue === "104rzh" ? "RZ" : matchValue === "109wh" ? "WH" : matchValue === "111zfd" ? "YT" : matchValue === "114wfg" ? "WF" : matchValue === "119hhg" ? "DY" : matchValue === "125bzg" ? "BZ" : "";


        //        s_01G_H = "#" + stationName + "_01G_H_00" + i;
        //        s_01D_H = "#" + stationName + "_01D_H_00" + i;
        //        s_02G_H = "#" + stationName + "_02G_H_00" + i;
        //        s_02D_H = "#" + stationName + "_02D_H_00" + i;

        //        s_01G_CG = "#" + stationName + "_01G_CG_00" + i;
        //        s_01D_CG = "#" + stationName + "_01D_CG_00" + i;
        //        s_02G_CG = "#" + stationName + "_02G_CG_00" + i;
        //        s_02D_CG = "#" + stationName + "_02D_CG_00" + i;

        //        fstHTime = tide.FSTHIGHWIDETIME;
        //        fstLTime = tide.FSTLOWWIDETIME;
        //        scdHTime = tide.SCDHIGHWIDETIME;
        //        scdLTime = tide.SCDLOWWIDETIME;

        //        fstHValue = tide.FSTHIGHWIDEHEIGHT;
        //        fstLValue = tide.FSTLOWWIDEHEIGHT;
        //        scdHValue = tide.SCDHIGHWIDEHEIGHT;
        //        scdLValue = tide.SCDLOWWIDEHEIGHT;

        //        $(s_01G_CG).val(fstHValue);
        //        $(s_01D_CG).val(fstLValue);
        //        $(s_02G_CG).val(scdHValue);
        //        $(s_02D_CG).val(scdLValue);

        //        if (fstHTime.indexOf("-") == -1) {
        //            var arr = fstHTime.split(":");
        //            $(s_01G_H).val(arr[0] + arr[1]);
        //            //$(s_01G_MIN).val(arr[1]);
        //        }
        //        else {
        //            $(s_01G_H).val("--");
        //            //$(s_01G_MIN).val("-");
        //        }
        //        if (fstLTime.indexOf("-") == -1) {
        //            var arr = fstLTime.split(":");
        //            $(s_01D_H).val(arr[0] + arr[1]);
        //            //$(s_01D_MIN).val(arr[1]);
        //        }
        //        else {
        //            $(s_01D_H).val("--");
        //            //$(s_01D_MIN).val("-");
        //        }
        //        if (scdHTime.indexOf("-") == -1) {
        //            var arr = scdHTime.split(":");
        //            $(s_02G_H).val(arr[0] + arr[1]);
        //            //$(s_02G_MIN).val(arr[1]);
        //        }
        //        else {
        //            $(s_02G_H).val("--");
        //            //$(s_02G_MIN).val("-");
        //        }
        //        if (scdLTime.indexOf("-") == -1) {
        //            var arr = scdLTime.split(":");
        //            $(s_02D_H).val(arr[0] + arr[1]);
        //            //$(s_02D_MIN).val(arr[1]);
        //        }
        //        else {
        //            $(s_02D_H).val("--");
        //            //$(s_02D_MIN).val("-");
        //        }
        //        //}
        //    }


        //    function getTideDataWithParams(stationsStr, dayCountInt, procTideDataCallBack, date1) {
        //        $.ajax({
        //            type: "POST",
        //            url: "Ajax/GetTideData.ashx",
        //            data: { stations: stationsStr, dayCount: dayCountInt, date: date1 },
        //            dataType: "json",
        //            success: function (resp) {
        //                procTideDataCallBack(resp);
        //            },
        //            error: function () {
        //                //alert("潮汐数据加载失败");
        //            }
        //        });

        //    }

        //    function getSD7City24HTideData(date1) {
        //        var stationsStr = "'101wmt','104rzh','109wh','111zfd','114wfg','119hhg','125bzg'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (i, value) {
        //                proc7City24TideData(i + 1, value);
        //            });
        //        }
        //        // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }

        //    function getQD24HTideData(date1) {
        //        var stationsStr = "'101wmt'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (i, value) {
        //                procQD24TideData(value);
        //            });
        //        }
        //        //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 1, procCallBack, date1);
        //    }

        //    function getMZZ72HTideData(date1) {
        //        var stationsStr = "'117xdh'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (i, value) {
        //                procTideData("MZZ", i, value);
        //            });
        //        }
        //        //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }

        //    function getNBYT72HTideData(date1) {
        //        var stationsStr = "'128cfd'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (i, value) {
        //                procTideData("NBYT", i, value);
        //            });
        //        }
        //        //  var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }

        //    function getWFG24HTideData(date1) {
        //        var stationsStr = "'114wfg'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (i, value) {
        //                $('#WFG_GCCS_01').val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $('#WFG_GCCS_02').val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $('#WFG_GCCG_01').val(value.FSTHIGHWIDEHEIGHT);
        //                $('#WFG_GCCG_02').val(value.SCDHIGHWIDEHEIGHT);
        //                $('#WFG_DCCS_01').val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $('#WFG_DCCS_02').val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $('#WFG_DCCG_01').val(value.FSTLOWWIDEHEIGHT);
        //                $('#WFG_DCCG_02').val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 1, procCallBack, date1);
        //    }

        //    function getQDBathing24HTideData(date1) {
        //        var stationsStr = "'101wmt','102xmd'";
        //        var num = 0;
        //        var procCallBack = function (data) {
        //            $(data).each(function (i, value) {
        //                var StationName = "";
        //                //if (value.STATION == "101wmt")
        //                //    StationName = "WMT";
        //                //else
        //                if (value.STATION == "102xmd")
        //                    StationName = "XMD";
        //                if (i % 3 == 0) {
        //                    num = 1;
        //                }
        //                else if (i % 3 == 1) {
        //                    num = 2;
        //                }
        //                else if (i % 3 == 2) {
        //                    num = 3;
        //                }
        //                var FSTHIGHWIDETIME = getHourMinute(value.FSTHIGHWIDETIME);
        //                var SCDHIGHWIDETIME = getHourMinute(value.SCDHIGHWIDETIME);
        //                var FSTLOWWIDETIME = getHourMinute(value.FSTLOWWIDETIME);
        //                var SCDLOWWIDETIME = getHourMinute(value.SCDLOWWIDETIME);
        //                $("#" + StationName + "_01G_H_00" + num).val(FSTHIGHWIDETIME);
        //                $("#" + StationName + "_01G_CG_00" + num).val(value.FSTHIGHWIDEHEIGHT);
        //                $("#" + StationName + "_02G_H_00" + num).val(SCDHIGHWIDETIME);
        //                $("#" + StationName + "_02G_CG_00" + num).val(value.SCDHIGHWIDEHEIGHT);
        //                $("#" + StationName + "_01D_H_00" + num).val(FSTLOWWIDETIME);
        //                $("#" + StationName + "_01D_CG_00" + num).val(value.FSTLOWWIDEHEIGHT);
        //                $("#" + StationName + "_02D_H_00" + num).val(SCDLOWWIDETIME);
        //                $("#" + StationName + "_02D_CG_00" + num).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        // var  = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }

        //    function getHourMinute(str) {
        //        var time = "";
        //        if (str.indexOf("-") == -1) {
        //            var arr = str.split(":");
        //            time = arr[0] + arr[1];
        //        }
        //        else {
        //            time = "--";
        //        }
        //        return time;
        //    }

        //    function getQDCoast48HTideData(date1) {
        //        var stationsStr = "'101wmt'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (i, value) {
        //                var fstHTime = value.FSTHIGHWIDETIME;
        //                var fstLTime = value.FSTLOWWIDETIME;
        //                var scdHTime = value.SCDHIGHWIDETIME;
        //                var scdLTime = value.SCDLOWWIDETIME;
        //                if (fstHTime.indexOf("-") == -1) {
        //                    var arr = fstHTime.split(":");
        //                    $("#01GC_H20").val(arr[0]);
        //                    $("#01GC_MIN20").val(arr[1]);
        //                    $("#01GC_CM20").val(value.FSTHIGHWIDEHEIGHT);
        //                }
        //                else {
        //                    $("#01GC_H20").val("-");
        //                    $("#01GC_MIN20").val("-");
        //                    $("#01GC_CM20").val(value.FSTHIGHWIDEHEIGHT);
        //                }
        //                if (fstLTime.indexOf("-") == -1) {
        //                    var arr = fstLTime.split(":");
        //                    $("#01DC_H20").val(arr[0]);
        //                    $("#01DC_MIN20").val(arr[1]);
        //                    $("#01DC_CM20").val(value.FSTLOWWIDEHEIGHT);
        //                }
        //                else {
        //                    $("#01DC_H20").val("-");
        //                    $("#01DC_MIN20").val("-");
        //                    $("#01DC_CM20").val(value.FSTLOWWIDEHEIGHT);
        //                }
        //                if (scdHTime.indexOf("-") == -1) {
        //                    var arr = scdHTime.split(":");
        //                    $("#02GC_H20").val(arr[0]);
        //                    $("#02GC_MIN20").val(arr[1]);
        //                    $("#02GC_CM20").val(value.SCDHIGHWIDEHEIGHT);
        //                }
        //                else {
        //                    $("#02GC_H20").val("-");
        //                    $("#02GC_MIN20").val("-");
        //                    $("#02GC_CM20").val(value.SCDHIGHWIDEHEIGHT);
        //                }
        //                if (scdLTime.indexOf("-") == -1) {
        //                    var arr = scdLTime.split(":");
        //                    $("#02DC_H20").val(arr[0]);
        //                    $("#02DC_MIN20").val(arr[1]);
        //                    $("#02DC_CM20").val(value.SCDLOWWIDEHEIGHT);
        //                }
        //                else {
        //                    $("#02DC_H20").val("-");
        //                    $("#02DC_MIN20").val("-");
        //                    $("#02DC_CM20").val(value.SCDLOWWIDEHEIGHT);
        //                }


        //            });
        //        }
        //        //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 2, procCallBack, date1);
        //    }

        //    function getWH48HTideData(date1) {
        //        var stationsStr = "'109wh','107sdo','106wdn','108cst','105nhd'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (i, value) {
        //                var StationName = "";
        //                if (value.STATION == "109wh")
        //                    StationName = "WH";
        //                else if (value.STATION == "107sdo")
        //                    StationName = "SD";
        //                else if (value.STATION == "106wdn")
        //                    StationName = "WD";
        //                else if (value.STATION == "108cst")
        //                    StationName = "CST";
        //                else if (value.STATION == "105nhd")
        //                    StationName = "RS";
        //                if (i % 2 == 0) i = 1;
        //                else i = 2;
        //                if (StationName == "RS" && i == 1) {
        //                    return true;
        //                }
        //                var S_01G_SJ = "#" + StationName + "_01G_SJ_0" + i;
        //                var S_01G_CW = "#" + StationName + "_01G_CW_0" + i;
        //                var S_01D_SJ = "#" + StationName + "_01D_SJ_0" + i;
        //                var S_01D_CW = "#" + StationName + "_01D_CW_0" + i;
        //                var S_02G_SJ = "#" + StationName + "_02G_SJ_0" + i;
        //                var S_02G_CW = "#" + StationName + "_02G_CW_0" + i;
        //                var S_02D_SJ = "#" + StationName + "_02D_SJ_0" + i;
        //                var S_02D_CW = "#" + StationName + "_02D_CW_0" + i;
        //                $(S_01G_SJ).val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $(S_01G_CW).val(value.FSTHIGHWIDEHEIGHT);
        //                $(S_01D_SJ).val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $(S_01D_CW).val(value.FSTLOWWIDEHEIGHT);
        //                $(S_02G_SJ).val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $(S_02G_CW).val(value.SCDHIGHWIDEHEIGHT);
        //                $(S_02D_SJ).val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $(S_02D_CW).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        //var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 2, procCallBack, date1);
        //    }


        //    //东营埕岛-未来三天高/低潮预报
        //    function getDYTide(date1) {
        //        var stationsStr = "'136dyg'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (j, value) {
        //                i = j + 1;
        //                $('#DY_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $('#DY_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $('#DY_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
        //                $('#DY_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

        //                $('#DY_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $('#DY_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $('#DY_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
        //                $('#DY_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);

        //    }

        //    function getPredicTideData(date1) {

        //        //      <!--表单07.山东省近海七市24小时潮汐预报-->
        //        getSD7City24HTideData(date1);
        //        //      <!--表单08.青岛24小时潮位预报-->
        //        getQD24HTideData(date1);
        //        //      <!--表单10.明泽闸潮位预报-->
        //        getMZZ72HTideData(date1);
        //        //      <!--表单12.南堡油田海域潮汐预报-->

        //        getNBYT72HTideData(date1);

        //        //      <!--表单16.潍坊港24小时潮汐预报-->
        //        //getWFG24HTideData(date1);
        //        //      <!--表单18.青岛海水浴场24小时潮汐预报-->
        //        getQDBathing24HTideData(date1);
        //        //      <!--表单20.青岛沿岸48小时潮汐预报-->
        //        getQDCoast48HTideData(date1);
        //        //      <!--表单22.潮汐预报-->
        //        getWH48HTideData(date1);

        //        //下午二十二、 东营埕岛-未来三天高/低潮预报
        //        getDYTide(date1);


        //        SDdyguangli(date1);
        //        SDrztaohuadao(date1);
        //        SDwfdujiaqu(date1);
        //        SDwhxinqu(date1);
        //        SDytqingquan(date1);
        //        SDdjk(date1);
        //        SDdyyugang(date1);
        //    }
        //    //$(function () {
        //    //    getPredicTideData();
        //    //}); 

        //    //山东七地市
        //    //下午二十四、东营广利渔港-未来三天高/低潮预报
        //    function SDdyguangli(date1) {
        //        var stationsStr = "'117xdh'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (j, value) {
        //                i = j + 1;
        //                $('#DYGL_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $('#DYGL_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $('#DYGL_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
        //                $('#DYGL_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

        //                $('#DYGL_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $('#DYGL_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $('#DYGL_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
        //                $('#DYGL_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }
        //    //下午二十七、日照桃花岛-未来三天高/低潮预报
        //    function SDrztaohuadao(date1) {
        //        var stationsStr = "'104rzh'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (j, value) {
        //                i = j + 1;
        //                $('#RZTHD_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $('#RZTHD_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $('#RZTHD_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
        //                $('#RZTHD_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

        //                $('#RZTHD_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $('#RZTHD_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $('#RZTHD_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
        //                $('#RZTHD_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }
        //    //下午三十、潍坊度假区-未来三天高/低潮预报
        //    function SDwfdujiaqu(date1) {
        //        var stationsStr = "'114wfg'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (j, value) {
        //                i = j + 1;
        //                $('#WFDJQ_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $('#WFDJQ_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $('#WFDJQ_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
        //                $('#WFDJQ_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

        //                $('#WFDJQ_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $('#WFDJQ_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $('#WFDJQ_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
        //                $('#WFDJQ_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }
        //    //下午三十三、威海新区-未来三天高/低潮预报
        //    function SDwhxinqu(date1) {
        //        var stationsStr = "'106wdn'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (j, value) {
        //                i = j + 1;
        //                $('#WHXQ_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $('#WHXQ_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $('#WHXQ_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
        //                $('#WHXQ_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

        //                $('#WHXQ_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $('#WHXQ_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $('#WHXQ_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
        //                $('#WHXQ_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }
        //    //下午三十六、烟台清泉-未来三天高/低潮预报
        //    function SDytqingquan(date1) {
        //        var stationsStr = "'111zfd'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (j, value) {
        //                i = j + 1;
        //                $('#YTQQ_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $('#YTQQ_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $('#YTQQ_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
        //                $('#YTQQ_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

        //                $('#YTQQ_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $('#YTQQ_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $('#YTQQ_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
        //                $('#YTQQ_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }
        //    //下午三十九、董家口-未来三天高/低潮预报
        //    function SDdjk(date1) {
        //        var stationsStr = "'139djk'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (j, value) {
        //                i = j + 1;
        //                $('#DJKP_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $('#DJKP_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $('#DJKP_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
        //                $('#DJKP_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

        //                $('#DJKP_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $('#DJKP_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $('#DJKP_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
        //                $('#DJKP_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }
        //    //下午四十二、东营渔港-未来三天高/低潮预报
        //    function SDdyyugang(date1) {
        //        var stationsStr = "'101wmt'";
        //        var procCallBack = function (data) {
        //            $(data).each(function (j, value) {
        //                i = j + 1;
        //                $('#DYFP_01G_CS_0' + i).val(value.FSTHIGHWIDETIME.replace(":", ""));
        //                $('#DYFP_01D_CS_0' + i).val(value.FSTLOWWIDETIME.replace(":", ""));
        //                $('#DYFP_01G_CG_0' + i).val(value.FSTHIGHWIDEHEIGHT);
        //                $('#DYFP_01D_CG_0' + i).val(value.FSTLOWWIDEHEIGHT);

        //                $('#DYFP_02G_CS_0' + i).val(value.SCDHIGHWIDETIME.replace(":", ""));
        //                $('#DYFP_02D_CS_0' + i).val(value.SCDLOWWIDETIME.replace(":", ""));
        //                $('#DYFP_02G_CG_0' + i).val(value.SCDHIGHWIDEHEIGHT);
        //                $('#DYFP_02D_CG_0' + i).val(value.SCDLOWWIDEHEIGHT);
        //            });
        //        }
        //        // var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
        //        getTideDataWithParams(stationsStr, 3, procCallBack, date1);
        //    }
        //}
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
            var dates1 = myformatter(date1);
            //getPredicTideData(dates1);
            gettabledata(date1, "p");
            gettabledatacx(date1, "p");//获取潮汐的数据


          
            //var date = new Date($("#tianbaoriqi").datebox("getValue"));
            //gettabledata(date, "p");
            //只能修改当前天的数据
            quanxian(type, date);

            //add by xp 2018-10-9 冬季的时候隐藏21号预报单
            setVisiable_21(date1);
           
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


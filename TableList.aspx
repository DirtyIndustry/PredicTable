<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableList.aspx.cs" Inherits="PredicTable.TableList" %>

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
    </style>



</head>
<body>
    <script>
             
              var cx_arry = new Array(2, 4, 7, 8,10, 12, 16, 18, 20, 22);
              var sw_arry = new Array(1, 3, 6, 11, 17, 19, 21,24);
              var fl_arry = new Array(1,3, 5, 6, 8, 9, 11, 13, 14,15,17,19,21);
              var type = "<%=type%>";
          function quanxian(type, date) {
             
              if (getdatenow() == date) {
                  switch (type) {
                      case "cx": all_disabled(); cx_isabled(); tb_isabled(); $("#yby_type").val("潮汐"); all_hide(); show_bytype(cx_arry); break;//潮汐能填写
                      case "fl": all_isabled(); cx_disabled(); sw_disabled(); tb_isabled(); $("#yby_type").val("风、海浪"); all_hide(); show_bytype(fl_arry); break;//风浪能填写
                      case "sw": all_disabled(); sw_isabled(); tb_isabled(); $("#yby_type").val("水温"); all_hide(); show_bytype(sw_arry); break;//水温能填写
                      default: all_disabled(); $("#yby_type").val("无"); break;// 都不能填写
                  }
              } else {//不是当天不能编辑
                  switch (type) {
                      case "cx": all_disabled();  $("#yby_type").val("潮汐"); all_hide(); show_bytype(cx_arry); break;//都不能填写
                      case "fl": all_disabled();   tb_isabled(); $("#yby_type").val("风、海浪"); all_hide(); show_bytype(fl_arry); break;//风浪能填写
                      case "sw": all_disabled();  $("#yby_type").val("水温"); all_hide(); show_bytype(sw_arry); break;//都不能填写
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
                 // getuserinfo(type);
                  show72hOr7d(new Date());
                  show72hOr7dForPortTide(new Date());
                  quanxian(type,getdatenow());

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
            ////  var topWin = window.top.document.getElementById("username").contentWindow;
          }); 

      
           //显示所有
          function all_show() {
              var str = "";
              for (var i = 1; i <= 22; i++) {
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
                for (var i = 1; i <= 22; i++) {
                    if (i <10) {
                        str = "#ddlg_0" + i;
                    } else {
                        str = "#ddlg_" + i;
                    }
                    $(str).css("display","none");
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

          //表单5自动验证
          $(function () {
              $("#BH_HL_01").focus(function () { //dgl_03
                  var numf = $("#BH_GDF_01").val();
                  var nume = $("#BH_GDE_01").val();
                  if (numf != "" && nume != "") {
                      if (setlevel(parseInt(numf)) != setlevel(parseInt(nume))) {
                          $("#BH_HL_01").val(setlevel(parseInt(numf)) + "-" + setlevel(parseInt(nume)));
                      }
                      else {
                          $("#BH_HL_01").val(setlevel(parseInt(numf)));
                      }
                  }
              });
              $("#HHBB_HL_01").focus(function () { //dgl_03
                  var numf = $("#HHBB_GDF_01").val();
                  var nume = $("#HHBB_GDE_01").val();
                  if (numf != "" && nume != "") {
                      if (setlevel(parseInt(numf)) != setlevel(parseInt(nume))) {
                          $("#HHBB_HL_01").val(setlevel(parseInt(numf)) + "-" + setlevel(parseInt(nume)));
                      }
                      else {
                          $("#HHBB_HL_01").val(setlevel(parseInt(numf)));
                      }
                  }
              });

              $("#BH_HL_401").focus(function () {//dgl_05
                  var numf = $("#BH_GDF_401").val();
                  var nume = $("#BH_GDE_401").val();
                  if (numf != "" && nume != "") {
                      if (setlevel(parseInt(numf)) != setlevel(parseInt(nume))) {
                          $("#BH_HL_401").val(setlevel(parseInt(numf)) + "-" + setlevel(parseInt(nume)));
                      }
                      else {
                          $("#BH_HL_401").val(setlevel(parseInt(numf)));
                      }
                  }
              });
              $("#HHBB_HL_401").focus(function () {//dgl_05
                  var numf = $("#HHBB_GDF_401").val();
                  var nume = $("#HHBB_GDE_401").val();
                  if (numf != "" && nume != "") {
                      if (setlevel(parseInt(numf)) != setlevel(parseInt(nume))) {
                          $("#HHBB_HL_401").val(setlevel(parseInt(numf)) + "-" + setlevel(parseInt(nume)));
                      }
                      else {
                          $("#HHBB_HL_401").val(setlevel(parseInt(numf)));
                      }
                  }
              });
              $("#HHZB_HL_401").focus(function () {//dgl_05
                  var numf = $("#HHZB_GDF_401").val();
                  var nume = $("#HHZB_GDE_401").val();
                  if (numf != "" && nume != "") {
                      if (setlevel(parseInt(numf)) != setlevel(parseInt(nume))) {
                          $("#HHZB_HL_401").val(setlevel(parseInt(numf)) + "-" + setlevel(parseInt(nume)));
                      }
                      else {
                          $("#HHZB_HL_401").val(setlevel(parseInt(numf)));
                      }
                  }
              });

              $("#HHNB_HL_401").focus(function () {//dgl_05
                  var numf = $("#HHNB_GDF_401").val();
                  var nume = $("#HHNB_GDE_401").val();
                  if (numf != "" && nume != "") {
                      if (setlevel(parseInt(numf)) != setlevel(parseInt(nume))) {
                          $("#HHNB_HL_401").val(setlevel(parseInt(numf)) + "-" + setlevel(parseInt(nume)));
                      }
                      else {
                          $("#HHNB_HL_401").val(setlevel(parseInt(numf)));
                      }
                  }
              });

          });
                            
         //根据浪高判断海浪级别
         function setlevel(num) {
                                num = parseInt(num);
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

    function createTr(stationCode,i) {

        return    "<td class='SJ_0" + i + "'>*月*号</td>"
                + "<td><input id='" + stationCode + "_BG_0" + i + "' type='text' maxlength='15' /></td>"
                + "<td><input id='" + stationCode + "_BX_0" + i + "' type='text' maxlength='15' /></td>"
                + "<td><input id='" + stationCode + "_FX_0" + i + "' type='text' maxlength='15' /></td>"
                + "<td><input id='" + stationCode + "_FL_0" + i + "' type='text' maxlength='15' /></td>"
                + "<td><input id='" + stationCode + "_SW_0" + i + "' type='text' maxlength='15' /></td>"
                + "</tr>";
    };


    function show72hOr7d(d){ //渤海海区及黄河海港风、浪预报 :周一显示7天预报单，其它显示72h预报单
        //var d=new Date()
        var trCount = 3;
        if(d.getDay()==1)
        {
            trCount = 7;
        }
        var bhHtml = "";
        var hhHtml = "";

        for (var i = 1; i <= trCount ;i++)
        {
            var bhTr = "";
            var hhhgTr = "";
            if(i==1)
            {
                bhTr="<tr><td rowspan='"+trCount+"'>渤海</td>"+createTr("BH",i);
                hhhgTr ="<tr><td rowspan='"+trCount+"'>黄河海港</td>"+createTr("HHHG",i);
            }
            else
            {
                bhTr="<tr>"+createTr("BH",i);
                hhhgTr ="<tr>"+createTr("HHHG",i);

            }
            bhHtml = bhHtml + bhTr;
            hhHtml = hhHtml + hhhgTr;
        }

        //alert(bhHtml+hhHtml);
        $("#ddlg_01 tbody").html(bhHtml+hhHtml);
        //$("#ddlg_01 tbody").html("<tr><td>1</td><td>2</td><td></td><td></td><td></td><td></td><td></td></tr>");
        if(trCount ==3)
        {
            $("#lx_1").text("一、72小时渤海海区及黄河海港风、浪预报");
            $("#ddlg_01title").text("一、72小时渤海海区及黄河海港风、浪预报");

        }
        else if(trCount ==7)
        {
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

    function createTrForPortTide(stationCode,i) { //港口潮位预报

        return    "<td class='SJ_0" + i + "'>*月*号</td>"
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


    function show72hOr7dForPortTide(d){ //港口潮位预报 :周一显示7天预报单，其它显示72h预报单
        //var d=new Date()
        var trCount = 3;
        if(d.getDay()==1)
        {
            trCount = 7;
        }

        var bhHtml = "";
        var hhHtml = "";

        for (var i = 1; i <= trCount;i++)
        {
            var bhTr = "";
            var hhhgTr = "";
            if(i==1)
            {
                bhTr="<tr><td rowspan='"+trCount+"'>龙口港</td>"+createTrForPortTide("LKG",i);
                hhhgTr ="<tr><td rowspan='"+trCount+"'>黄河海港</td>"+createTrForPortTide("HHHG",i);
            }
            else
            {
                bhTr="<tr>"+createTrForPortTide("LKG",i);
                hhhgTr ="<tr>"+createTrForPortTide("HHHG",i);

            }
            bhHtml = bhHtml + bhTr;
            hhHtml = hhHtml + hhhgTr;
        }

        //alert(bhHtml+hhHtml);
        $("#ddlg_02 tbody").html(bhHtml+hhHtml);

        $("input[id*='CW']").blur(function () {
            valisection(this, tb_cw, tb_cw2);
        });
        //潮汐时分
        $("input[id*='SJ']").blur(function () {
            hmsection(this);
        });
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
                    <div style="position: fixed; top: 0px; left: 20px; z-index: 2">
                        <ul id="leixing1">
                            <li id="swbd" style="border-color: #fb9337; background-color: #fb9337; color: white; font-weight: bold">上午预报表单</li>
                            <li id="lx_1" class="dlg" onclick="click_scroll('ddlg_01')">一、72小时渤海海区及黄河海港风、浪预报</li>
                            <li id="lx_2" onclick="click_scroll('ddlg_02')<%--location.href='#ddlg_02'--%>" <%-- onclick="$('#dlg_02').dialog('open')"--%>>二、港口潮位预报</li>
                            <li id="lx_3" onclick="click_scroll('ddlg_03')">三、预计未来24小时海浪、水温预报</li>
                            <li id="lx_4" onclick="click_scroll('ddlg_04')">四、24小时潮位预报</li>
                            <li id="lx_25" onclick="click_scroll('ddlg_25')">五、指挥处上午预报</li>
                            <li id="xwbd" style="clear: both; border-color: #fb9337; background-color: #fb9337; color: white; font-weight: bold">下午预报表单</li>
                            <li id="lx_5" onclick="click_scroll('ddlg_05')">一、各海区24小时海浪预报</li>
                            <li id="lx_6" onclick="click_scroll('ddlg_06')">二、山东近海七市24小时海浪预报</li>
                            <li id="lx_7" onclick="click_scroll('ddlg_07')">三、山东近海七市24小时潮汐预报</li>
                            <li id="lx_8" onclick="click_scroll('ddlg_08')">四、青岛24小时潮位预报</li>
                            <li id="lx_9" onclick="click_scroll('ddlg_09')">五、黄河南海堤附近海域24小时风、浪预报</li>
                            <li id="lx_10" onclick="click_scroll('ddlg_10')">六、明泽闸潮位预报</li>
                            <li id="lx_11" onclick="click_scroll('ddlg_11')">七、南堡油田海域波浪、风、水温预报</li>
                            <li id="lx_12" onclick="click_scroll('ddlg_12')">八、南堡油田海域潮汐预报</li>
                            <li id="lx_13" onclick="click_scroll('ddlg_13')">九、海区24小时海浪预报</li>
                            <li id="lx_14" onclick="click_scroll('ddlg_14')">十、海区48小时海浪预报</li>
                            <li id="lx_15" onclick="click_scroll('ddlg_15')">十一、海区72小时海浪预报</li>
                            <li id="lx_16" onclick="click_scroll('ddlg_16')">十二、潍坊港24小时潮汐预报</li>
                            <li id="lx_17" onclick="click_scroll('ddlg_17')">十三、各海区24小时海浪预报</li>
                            <li id="lx_18" onclick="click_scroll('ddlg_18')">十四、青岛24小时潮汐预报</li>
                            <li id="lx_19" onclick="click_scroll('ddlg_19')">十五、青岛周边海域24小时预报</li>
                            <li id="lx_20" onclick="click_scroll('ddlg_20')">十六、青岛沿岸48小时潮汐预报</li>
                            <li id="lx_21" onclick="click_scroll('ddlg_21')">十七、威海电视台未来24小时预报</li>
                            <li id="lx_22" onclick="click_scroll('ddlg_22')">十八、潮汐预报</li>
                            <li id="lx_23" onclick="click_scroll('ddlg_24')">十九、渤海及黄海北部冰情预报</li>
                            <li id="lx_26" onclick="click_scroll('ddlg_26')">二十、指挥处下午预报</li>
                        </ul>
                    </div>
                </div>
                <!--表单类型-->
                <div style="margin-top: 160px">
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
                        <br />
                    </div>
                </div>
                <!--表单信息-->

                <%--<a href="javascript:void(0)" class="easyui-linkbutton" onclick="$('#dlg').dialog('open')">Open</a>
		<a href="javascript:void(0)" class="easyui-linkbutton" onclick="$('#dlg').dialog('close')">Close</a>--%>


                <div class="dlgs" id="ddlg_01" style="width: 930px; height: 390px; padding: 10px;">
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
                        </colgroup>
                        <thead>
                            <tr>
                                <th id="ddlg_01title" class="head0" colspan="7">一、72小时渤海海区及黄河海港风、浪预报</th>
                            </tr>
                            <tr>
                                <th class="head0">区域</th>
                                <th class="head1">日期</th>
                                <th class="head0">波高（h）</th>
                                <th class="head1">波向（方位）</th>
                                <th class="head0">风向（方位）</th>
                                <th class="head1">风力（级）</th>
                                <th class="head0">水温（℃）</th>
                            </tr>
                        </thead>
                        <tbody id="ddlg_01_tbody" style="text-align: center">
                            <tr>
                                <td rowspan="3">渤海</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="BH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_BX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_SW_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="BH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_BX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_SW_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="BH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_BX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_SW_03" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">黄河海港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_BX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_SW_01" type="text" maxlength="15" /></td>

                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_BX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_SW_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_BX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_SW_03" type="text" maxlength="15" /></td>
                            </tr>

                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(1)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(1)" value="提交" />
                </div>


                <div class="dlgs" id="ddlg_02" <%--id="dlg_02" class="easyui-dialog" title="二、港口潮位预报" data-options="iconCls:'icon-save'"--%> style="width: 1300px; height: 430px; padding: 10px;">
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
                                <th class="head1" colspan="10">二、港口潮位预报</th>
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
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td rowspan="3">龙口港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_01" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_02" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_03" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">黄河海港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_01" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_02" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_03" type="text" maxlength="20" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(2)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(2)" value="提交" />
                </div>
                <!--表单02.港口潮位预报-->

                <div class="dlgs" id="ddlg_03" <%--id="dlg_03" class="easyui-dialog" title="三、预计未来24小时海浪、水温预报" data-options="iconCls:'icon-save'"--%> style="width: 700px; height: 460px; padding: 10px;">
                    <div style="border: 1px solid #ddd; width: 698px; line-height: 36px; font-weight: bold; text-align: center">三、预计未来24小时海浪、水温预报</div>
                    <div style="border: 1px solid #ddd">
                        <br />
                        <div style="width: 100px; float: left; text-align: right">渤海有</div>
                        &nbsp;
                        <input maxlength="20" id="BH_GDF_01" onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" <%--onkeyup="(this.v=function(){this.value=this.value.replace(/[^0-9-]+/,'');}).call(this)" onblur="this.v();"--%> type="text" />&nbsp;m&nbsp; 到&nbsp;
                        <input maxlength="20" id="BH_GDE_01" onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" />
                        &nbsp;m&nbsp;的&nbsp;<input maxlength="20" id="BH_HL_01" type="text" />&nbsp;浪
                          <br />
                        <br />

                        <div style="width: 100px; float: left; text-align: right">黄海北部有</div>
                        &nbsp;
                        <input maxlength="20" id="HHBB_GDF_01" onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" />&nbsp;m&nbsp; 到&nbsp;
                        <input maxlength="20" id="HHBB_GDE_01" onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" />
                        &nbsp;m&nbsp;的&nbsp;<input maxlength="20" id="HHBB_HL_01" type="text" />&nbsp;浪
                        <br />

                        <br />
                        <div style="width: 100px; float: left; text-align: right; margin-right: 5px">刁口海域</div>
                        &nbsp;浪高 
                            <input id="DKHY_LG_01" type="text" maxlength="80" />
                        米&nbsp; 水温
                            <input id="DKHY_SW_01" type="text" maxlength="20" />℃
                        <br />
                        <br />
                        <div style="width: 100px; float: left; text-align: right; margin-right: 5px">黄河口海域</div>
                        &nbsp;浪高 
                            <input id="HHK_LG_01" type="text" maxlength="80" />
                        米&nbsp; 水温
                            <input id="HHK_SW_01" type="text" maxlength="20" />℃
                     <br />
                        <br />
                        <div style="width: 100px; float: left; text-align: right; margin-right: 5px">广东港海域</div>
                        &nbsp;浪高 
                            <input id="GDG_LG_01" type="text" maxlength="80" />
                        米&nbsp; 水温
                            <input id="GDG_SW_01" type="text" maxlength="20" />℃
                     <br />
                        <br />
                        <div style="width: 100px; float: left; text-align: right; margin-right: 5px">东营港海域</div>
                        &nbsp;浪高 
                            <input id="DYG_LG_01" type="text" maxlength="80" />
                        米&nbsp; 水温
                            <input id="DYG_SW_01" type="text" maxlength="20" />℃
                     <br />
                        <br />
                        <div style="width: 100px; float: left; text-align: right; margin-right: 5px">新户海域</div>
                        &nbsp;浪高 
                            <input id="XH_LG_01" type="text" maxlength="20" />
                        米&nbsp; 水温
                            <input id="XH_SW_01" type="text" maxlength="20" />℃
                     <br />
                        <br />
                        <div style="width: 100px; float: left; text-align: right; margin-right: 5px">埕口海域</div>
                        &nbsp;浪高 
                            <input id="CK_LG_01" type="text" maxlength="20" />
                        米&nbsp; 水温
                            <input id="CK_SW_01" type="text" maxlength="20" />℃
                    </div>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(3)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(3)" value="提交" />

                </div>
                <!--表单03.预计未来24小时海浪、水温预报-->

                <div class="dlgs" id="ddlg_04" <%--id="dlg_04" class="easyui-dialog" title="四、24小时潮位预报" data-options="iconCls:'icon-save'"--%> style="width: 1300px; height: 355px; padding: 10px;">
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

                                <th class="head1" colspan="10">四、24小时潮位预报</th>
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
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>飞雁滩</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="FYT_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="FYT_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="FYT_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="FYT_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="FYT_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="FYT_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="FYT_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="FYT_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td>孤东</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="GD_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="GD_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="GD_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="GD_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="GD_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="GD_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="GD_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="GD_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td>小岛河</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="XDH_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="XDH_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="XDH_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="XDH_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="XDH_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="XDH_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="XDH_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="XDH_02D_CW_01" type="text" /></td>
                            </tr>
                            <tr>
                                <td>东营港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="DYG_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="DYG_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="DYG_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="DYG_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="DYG_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="DYG_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="DYG_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="DYG_02D_CW_01" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(4)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(4)" value="提交" />
                </div>
                <!--表单04.24小时潮位预报-->



                <div class="dlgs" id="ddlg_25"
                    style="width: 1300px; height: 835px; padding: 10px;">
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
                                <th class="head0" colspan="7">五、指挥处上午预报</th>
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
                                <td rowspan="7" class="SJ_0001MO">*月*号08时至*月*号08时</td>
                                <td>青岛市</td>
                                <td>
                                    <input id="ZHC_MO_QD_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_QD_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_QD_FL_01" type="text" maxlength="15" /></td>
                                <td colspan="2">水温：
                                        <input id="ZHC_MO_QD_SW_01" type="text" maxlength="15" /></td>
                                <%--                                    <td>
                                        <input id="ZHC_MO_QD_BX_01" type="text" maxlength="15" /></td>--%>
                            </tr>
                            <tr>
                                <td>青岛近海</td>
                                <td>
                                    <input id="ZHC_MO_QDJH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_QDJH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_QDJH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_QDJH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_QDJH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>渤海</td>
                                <td>
                                    <input id="ZHC_MO_BH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>渤海海峡</td>
                                <td>
                                    <input id="ZHC_MO_BHHX_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BHHX_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BHHX_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BHHX_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BHHX_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="ZHC_MO_NHH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="ZHC_MO_MHH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海南部</td>
                                <td>
                                    <input id="ZHC_MO_SHH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="4" class="SJ_0102MO">*月*号08时至*月*号08时</td>
                                <td>渤海</td>
                                <td>
                                    <input id="ZHC_MO_BH_TX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_BX_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="ZHC_MO_NHH_TX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_BX_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="ZHC_MO_MHH_TX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_BX_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海南部</td>
                                <td>
                                    <input id="ZHC_MO_SHH_TX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_BX_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="4" class="SJ_0203MO">*月*号08时至*月*号08时</td>
                                <td>渤海</td>
                                <td>
                                    <input id="ZHC_MO_BH_TX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_BH_BX_03" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="ZHC_MO_NHH_TX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_NHH_BX_03" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="ZHC_MO_MHH_TX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_MHH_BX_03" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海南部</td>
                                <td>
                                    <input id="ZHC_MO_SHH_TX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_FX_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_FL_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_BG_03" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_MO_SHH_BX_03" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>

                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(25)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(25)" value="提交" />

                </div>
                <!--表单25.指挥处上午预报-->


                <div class="dlgs" id="ddlg_05" <%--id="dlg_05" class="easyui-dialog" title="下午一、各海区24小时海浪预报" data-options="iconCls:'icon-save'"--%> style="width: 800px; height: 285px; padding: 10px;">

                    <table style="border: 1px solid #ddd; width: 100%">

                        <tr style="line-height: 45px;">
                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="4">下午一、各海区24小时海浪预报</td>
                        </tr>

                        <tr style="line-height: 45px">
                            <td>渤海</td>
                            <td>有<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="BH_GDF_401" type="text" />米</td>
                            <td>到<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="BH_GDE_401" type="text" />米</td>
                            <td>的<input id="BH_HL_401" type="text" />浪</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>黄海北部</td>
                            <td>有<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHBB_GDF_401" type="text" />米</td>
                            <td>到<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHBB_GDE_401" type="text" />米</td>
                            <td>的<input id="HHBB_HL_401" type="text" />浪</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>黄海中部</td>
                            <td>有<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHZB_GDF_401" type="text" />米</td>
                            <td>到<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHZB_GDE_401" type="text" />米</td>
                            <td>的<input id="HHZB_HL_401" type="text" />浪</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>黄海南部</td>
                            <td>有<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHNB_GDF_401" type="text" />米</td>
                            <td>到<input onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="HHNB_GDE_401" type="text" />米</td>
                            <td>的<input id="HHNB_HL_401" type="text" />浪</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(5)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(5)" value="提交" />

                </div>
                <!--表单05.各海区24小时海浪预报-->

                <div class="dlgs" id="ddlg_06" <%--id="dlg_06" class="easyui-dialog" title="下午二、山东省近海七市24小时海浪预报" data-options="iconCls:'icon-save'"--%> style="width: 530px; height: 435px; padding: 10px;">

                    <table style="border: 1px solid #ddd; width: 100%">

                        <tr style="line-height: 45px">
                            <td colspan="3" style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center">下午二、山东省近海七市24小时海浪预报</td>
                        </tr>

                        <tr style="line-height: 45px">
                            <td>日照近海</td>
                            <td>浪高<input id="RZJH_LG" type="text" maxlength="80" />米</td>
                            <td>表层水温：<input id="RZJH_BCSW" type="text" maxlength="20" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>青岛近海</td>
                            <td>浪高<input id="QDJH_LG" type="text" maxlength="80" />米</td>
                            <td>表层水温：<input id="QDJH_BCSW" type="text" maxlength="20" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>威海近海</td>
                            <td>浪高<input id="WHJH_LG" type="text" maxlength="80" />米</td>
                            <td>表层水温：<input id="WHJH_BCSW" type="text" maxlength="20" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>烟台近海</td>
                            <td>浪高<input id="YTJH_LG" type="text" maxlength="80" />米</td>
                            <td>表层水温：<input id="YTJH_BCSW" type="text" maxlength="20" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>潍坊近海</td>
                            <td>浪高<input id="WFJH_LG" type="text" maxlength="80" />米</td>
                            <td>表层水温：<input id="WFJH_BCSW" type="text" maxlength="20" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>东营近海</td>
                            <td>浪高<input id="DYJH_LG" type="text" maxlength="80" />米</td>
                            <td>表层水温：<input id="DYJH_BCSW" type="text" maxlength="20" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>滨州近海</td>
                            <td>浪高<input id="BZJH_LG" type="text" maxlength="80" />米</td>
                            <td>表层水温：<input id="BZJH_BCSW" type="text" maxlength="20" />℃</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(6)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(6)" value="提交" />

                </div>
                <!--表单06.山东省近海七市24小时海浪预报-->

                <div class="dlgs" id="ddlg_07" <%--id="dlg_07" class="easyui-dialog" title="下午三、山东省近海七市24小时潮汐预报" data-options="iconCls:'icon-save'"--%> style="width: 1300px; height: 430px; padding: 10px;">
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

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="10">下午三、山东省近海七市24小时潮汐预报</th>
                            </tr>
                            <tr style="text-align: center">
                                <th class="head0" rowspan="2">地市</th>
                                <th class="head0" rowspan="2">时间</th>
                                <th class="head1" colspan="2">第一次高潮时</th>
                                <th class="head0" colspan="2">第二次高潮时</th>
                                <th class="head1" colspan="2">第一次低潮时</th>
                                <th class="head0" colspan="2">第二次低潮时</th>
                            </tr>
                            <tr>
                                <td>（h）</td>
                                <td>(min)</td>
                                <td>（h）</td>
                                <td>(min)</td>
                                <td>（h）</td>
                                <td>(min)</td>
                                <td>（h）</td>
                                <td>(min)</td>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>日照</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="RZ_01G_H" type="text" /></td>
                                <td>
                                    <input id="RZ_01G_MIN" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_H" type="text" /></td>
                                <td>
                                    <input id="RZ_02G_MIN" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_H" type="text" /></td>
                                <td>
                                    <input id="RZ_01D_MIN" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_H" type="text" /></td>
                                <td>
                                    <input id="RZ_02D_MIN" type="text" /></td>
                            </tr>
                            <tr>
                                <td>青岛</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="QD_01G_H" type="text" /></td>
                                <td>
                                    <input id="QD_01G_MIN" type="text" /></td>
                                <td>
                                    <input id="QD_02G_H" type="text" /></td>
                                <td>
                                    <input id="QD_02G_MIN" type="text" /></td>
                                <td>
                                    <input id="QD_01D_H" type="text" /></td>
                                <td>
                                    <input id="QD_01D_MIN" type="text" /></td>
                                <td>
                                    <input id="QD_02D_H" type="text" /></td>
                                <td>
                                    <input id="QD_02D_MIN" type="text" /></td>
                            </tr>
                            <tr>
                                <td>威海</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WH_01G_H" type="text" /></td>
                                <td>
                                    <input id="WH_01G_MIN" type="text" /></td>
                                <td>
                                    <input id="WH_02G_H" type="text" /></td>
                                <td>
                                    <input id="WH_02G_MIN" type="text" /></td>
                                <td>
                                    <input id="WH_01D_H" type="text" /></td>
                                <td>
                                    <input id="WH_01D_MIN" type="text" /></td>
                                <td>
                                    <input id="WH_02D_H" type="text" /></td>
                                <td>
                                    <input id="WH_02D_MIN" type="text" /></td>
                            </tr>
                            <tr>
                                <td>烟台</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="YT_01G_H" type="text" /></td>
                                <td>
                                    <input id="YT_01G_MIN" type="text" /></td>
                                <td>
                                    <input id="YT_02G_H" type="text" /></td>
                                <td>
                                    <input id="YT_02G_MIN" type="text" /></td>
                                <td>
                                    <input id="YT_01D_H" type="text" /></td>
                                <td>
                                    <input id="YT_01D_MIN" type="text" /></td>
                                <td>
                                    <input id="YT_02D_H" type="text" /></td>
                                <td>
                                    <input id="YT_02D_MIN" type="text" /></td>
                            </tr>
                            <tr>
                                <td>潍坊</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WF_01G_H" type="text" /></td>
                                <td>
                                    <input id="WF_01G_MIN" type="text" /></td>
                                <td>
                                    <input id="WF_02G_H" type="text" /></td>
                                <td>
                                    <input id="WF_02G_MIN" type="text" /></td>
                                <td>
                                    <input id="WF_01D_H" type="text" /></td>
                                <td>
                                    <input id="WF_01D_MIN" type="text" /></td>
                                <td>
                                    <input id="WF_02D_H" type="text" /></td>
                                <td>
                                    <input id="WF_02D_MIN" type="text" /></td>
                            </tr>
                            <tr>
                                <td>东营</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="DY_01G_H" type="text" /></td>
                                <td>
                                    <input id="DY_01G_MIN" type="text" /></td>
                                <td>
                                    <input id="DY_02G_H" type="text" /></td>
                                <td>
                                    <input id="DY_02G_MIN" type="text" /></td>
                                <td>
                                    <input id="DY_01D_H" type="text" /></td>
                                <td>
                                    <input id="DY_01D_MIN" type="text" /></td>
                                <td>
                                    <input id="DY_02D_H" type="text" /></td>
                                <td>
                                    <input id="DY_02D_MIN" type="text" /></td>
                            </tr>
                            <tr>
                                <td>滨州</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="BZ_01G_H" type="text" /></td>
                                <td>
                                    <input id="BZ_01G_MIN" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_H" type="text" /></td>
                                <td>
                                    <input id="BZ_02G_MIN" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_H" type="text" /></td>
                                <td>
                                    <input id="BZ_01D_MIN" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_H" type="text" /></td>
                                <td>
                                    <input id="BZ_02D_MIN" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(7)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(7)" value="提交" />
                </div>
                <!--表单07.山东省近海七市24小时潮汐预报-->

                <div class="dlgs" id="ddlg_08" <%--id="dlg_08" class="easyui-dialog" title="下午四、青岛24小时潮位预报" data-options="iconCls:'icon-save'"--%> style="width: 600px; height: 330px; padding: 10px;">

                    <table style="border: 1px solid #ddd; width: 100%">
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
                    </table>
                    <br />
                    明日海滨浪高：<input id="MRHBLG" type="text" style="margin-right: 10px" />
                    &nbsp; &nbsp; 浪向：<input id="MRHBLX" type="text" />
                    <br />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(8)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(8)" value="提交" />

                </div>
                <!--表单08.青岛24小时潮位预报-->

                <div class="dlgs" id="ddlg_09" <%-- id="dlg_09" class="easyui-dialog" title="下午五、黄河南海堤附近海域24小时风、浪预报" data-options="iconCls:'icon-save'"--%> style="width: 800px; height: 280px; padding: 10px;">
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
                                <th class="head0" colspan="5">下午五、黄河南海堤附近海域24小时风、浪预报</th>
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
                <!--表单09.黄河南海堤附近海域24小时风、浪预报-->

                <div class="dlgs" id="ddlg_10" <%--id="dlg_10" class="easyui-dialog" title="下午六、明泽闸潮位预报" data-options="iconCls:'icon-save'"--%> style="width: 1300px; height: 315px; padding: 10px;">
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
                                <th class="head0" colspan="10">下午六、明泽闸潮位预报</th>
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
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
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
                <!--表单10.明泽闸潮位预报-->

                <div class="dlgs" id="ddlg_11" <%-- id="dlg_11" class="easyui-dialog" title="下午七、南堡油田海域波浪、风、水温预报" data-options="iconCls:'icon-save'"--%> style="width: 1040px; height: 245px; padding: 10px;">
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
                                <th class="head0" colspan="7">下午七、南堡油田海域波浪、风、水温预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0">日期</th>
                                <th class="head1">波高(m)</th>
                                <th class="head0">波向(方位)</th>
                                <th class="head1">风向(方位)</th>
                                <th class="head0">风力（级）</th>
                                <th class="head0">水温（℃）</th>
                                <th class="head0">天气</th>
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
                                    <input id="NBYT_SW_01" type="text" /></td>
                                <td>
                                    <input id="NBYT_TQ_01" type="text" /></td>
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
                                    <input id="NBYT_SW_02" type="text" /></td>
                                <td>
                                    <input id="NBYT_TQ_02" type="text" /></td>
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
                                    <input id="NBYT_SW_03" type="text" /></td>
                                <td>
                                    <input id="NBYT_TQ_03" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(11)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(11)" value="提交" />
                </div>
                <!--表单11.南堡油田海域波浪、风、水温预报-->

                <div class="dlgs" id="ddlg_12" <%--id="dlg_12" class="easyui-dialog" title="下午八、南堡油田海域潮汐预报" data-options="iconCls:'icon-save'"--%> style="width: 1300px; height: 315px; padding: 10px;">
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

                                <th class="head0" colspan="10">下午八、南堡油田海域潮汐预报</th>
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
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
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
                <!--表单12.南堡油田海域潮汐预报-->

                <div class="dlgs" id="ddlg_13" <%-- id="dlg_13" class="easyui-dialog" title="下午九、海区24小时海浪预报" data-options="iconCls:'icon-save'"--%> style="width: 1040px; height: 425px; padding: 10px;">
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
                                <th class="head0" colspan="6">下午九、海区24小时海浪预报</th>
                            </tr>

                            <tr style="text-align: center">
                                <th class="head0"></th>
                                <th class="head1">波高(m)</th>
                                <th class="head0">波向(方位)</th>
                                <th class="head1">波级(浪)</th>
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
                                <td>
                                    <input id="BH13_HL" type="text" /></td>
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
                                <td>
                                    <input id="HHBB_HL" type="text" /></td>
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
                                <td>
                                    <input id="HHZB_HL" type="text" /></td>
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
                                <td></td>
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
                                <td></td>
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
                <!--表单13.海区24小时海浪预报-->

                <div class="dlgs" id="ddlg_14" <%--id="dlg_14" class="easyui-dialog" title="下午十、海区48小时海浪预报" data-options="iconCls:'icon-save'"--%> style="width: 1040px; height: 425px; padding: 10px;">
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
                                <th class="head0" colspan="5">下午十、海区48小时海浪预报</th>
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
                <!--表单14.海区48小时海浪预报-->

                <div class="dlgs" id="ddlg_15" <%-- id="dlg_15" class="easyui-dialog" title="下午十一、海区72小时海浪预报" data-options="iconCls:'icon-save'"--%> style="width: 1040px; height: 425px; padding: 10px;">
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
                                <th class="head0" colspan="5">下午十一、海区72小时海浪预报</th>
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
                <!--表单15.海区72小时海浪预报-->

                <div class="dlgs" id="ddlg_16" <%-- id="dlg_16" class="easyui-dialog" title="下午十二、潍坊港24小时潮汐预报" data-options="iconCls:'icon-save'"--%> style="width: 1140px; height: 285px; padding: 10px;">
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
                                <td>潮时（时、分）</td>
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
                                <td>潮时（时、分）</td>
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
                </div>
                <!--表单16.潍坊港24小时潮汐预报-->

                <div class="dlgs" id="ddlg_17" <%-- id="dlg_17" class="easyui-dialog" title="下午十三、各海区24小时海浪预报" data-options="iconCls:'icon-save'" --%>style="width: 800px; height: 285px; padding: 10px;">
                    <table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">
                            <td colspan="4" style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center">下午十三、各海区24小时海浪预报</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第一海水浴场</td>
                            <td>浪高<input id="DYHSYC_LG" type="text" />米</td>
                            <td>水温<input id="DYHSYC_SW" type="text" />℃</td>
                            <td>
                                <input id="DYHSYC_YY" type="text" />游泳</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第六海水浴场</td>
                            <td>浪高<input id="DLHSYC_LG" type="text" />米</td>
                            <td>水温<input id="DLHSYC_SW" type="text" />℃</td>
                            <td>
                                <input id="DLHSYC_YY" type="text" />游泳</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>石老人海水浴场</td>
                            <td>浪高<input id="SLRHSYC_LG" type="text" />米</td>
                            <td>水温<input id="SLRHSYC_SW" type="text" />℃</td>
                            <td>
                                <input id="SLRHSYC_YY" type="text" />游泳</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>金沙滩海水浴场</td>
                            <td>浪高<input id="JSTHSYC_LG" type="text" />米</td>
                            <td>水温<input id="JSTHSYC_SW" type="text" />℃</td>
                            <td>
                                <input id="JSTHSYC_YY" type="text" />游泳</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(17)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(17)" value="提交" />
                </div>
                <!--表单17.各海区24小时海浪预报-->

                <div class="dlgs" id="ddlg_18" <%-- id="dlg_18" class="easyui-dialog" title="下午十四、小麦岛24小时潮汐预报" data-options="iconCls:'icon-save'"--%> style="width: 600px; height: 285px; padding: 10px;">
                    <table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">
                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="3">下午十四、小麦岛24小时潮汐预报</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第一次高潮时</td>
                            <td>
                                <input id="XMD_01GC_H" type="text" />时</td>
                            <td>
                                <input id="XMD_01GC_MIN" type="text" />分</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第二次高潮时</td>
                            <td>
                                <input id="XMD_02GC_H" type="text" />时</td>
                            <td>
                                <input id="XMD_02GC_MIN" type="text" />分</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第一次低潮时</td>
                            <td>
                                <input id="XMD_01DC_H" type="text" />时</td>
                            <td>
                                <input id="XMD_01DC_MIN" type="text" />分</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>第二次低潮时</td>
                            <td>
                                <input id="XMD_02DC_H" type="text" />时</td>
                            <td>
                                <input id="XMD_02DC_MIN" type="text" />分</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(18)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(18)" value="提交" />
                </div>
                <!--表单18.小麦岛24小时潮汐预报-->

                <div class="dlgs" id="ddlg_19" <%-- id="dlg_19" class="easyui-dialog" title="下午十五、青岛周边海域24小时潮汐预报" data-options="iconCls:'icon-save'" --%>style="width: 600px; height: 285px; padding: 10px;">
                    <table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">
                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="3">下午十五、青岛周边海域24小时潮汐预报</td>
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
                            <td>胶州近海</td>
                            <td>浪高<input id="JZJH_LG19" type="text" />米</td>
                            <td>水温<input id="JZJH_SW19" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>胶南近海</td>
                            <td>浪高<input id="JNJH_LG19" type="text" />米</td>
                            <td>水温<input id="JNJH_SW19" type="text" />℃</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(19)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(19)" value="提交" />
                </div>
                <!--表单19.青岛周边海域24小时潮汐预报-->

                <div class="dlgs" id="ddlg_20" <%-- id="dlg_20" class="easyui-dialog" title="下午十六、青岛沿岸48小时潮汐预报" data-options="iconCls:'icon-save'"--%> style="width: 600px; height: 285px; padding: 10px;">
                    <table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">
                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="4">下午十六、青岛沿岸48小时潮汐预报</td>
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

                </div>
                <!--表单20.青岛沿岸48小时潮汐预报-->

                <div class="dlgs" id="ddlg_21" <%-- id="dlg_21" class="easyui-dialog" title="下午十七、威海电视台未来24小时预报" data-options="iconCls:'icon-save'"--%> style="width: 600px; height: 520px; padding: 10px;">
                    <table style="border: 1px solid #ddd; width: 100%">
                        <tr style="line-height: 45px">

                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="5">下午十七、威海电视台未来24小时预报</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td rowspan="4">预计未来24小时</td>
                            <td>石岛近海</td>
                            <td>浪高<input id="SDJH_LG_24" type="text" />m</td>
                            <td>水温<input id="SDJH_SW_24" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>文登区</td>
                            <td>浪高<input id="WDQ_LG_24" type="text" />m</td>
                            <td>水温<input id="WDQ_SW_24" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>成山头</td>
                            <td>浪高<input id="CST_LG_24" type="text" />m</td>
                            <td>水温<input id="CST_SW_24" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>乳山市</td>
                            <td>浪高<input id="RSS_LG_24" type="text" />m</td>
                            <td>水温<input id="RSS_SW_24" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td rowspan="5">预计未来48小时</td>
                            <td>威海近海</td>
                            <td>浪高<input id="WHJH_LG_48" type="text" />m</td>
                            <td>水温<input id="WHJH_SW_48" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>石岛近海</td>
                            <td>浪高<input id="SDJH_LG_48" type="text" />m</td>
                            <td>水温<input id="SDJH_SW_48" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>文登区</td>
                            <td>浪高<input id="WDQ_LG_48" type="text" />m</td>
                            <td>水温<input id="WDQ_SW_48" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>成山头</td>
                            <td>浪高<input id="CST_LG_48" type="text" />m</td>
                            <td>水温<input id="CST_SW_48" type="text" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>乳山市</td>
                            <td>浪高<input id="RSS_LG_48" type="text" />m</td>
                            <td>水温<input id="RSS_SW_48" type="text" />℃</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(21)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(21)" value="提交" />
                </div>
                <!--表单21.威海电视台未来24小时预报-->

                <div class="dlgs" id="ddlg_22" <%--  id="dlg_22" class="easyui-dialog" title="下午十八、潮汐预报" data-options="iconCls:'icon-save'"--%> style="width: 1300px; height: 640px; padding: 10px;">
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

                                <th class="head0" colspan="10">下午十八、潮汐预报</th>
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
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                                <td>（时、分）</td>
                                <td>(cm)</td>
                            </tr>
                        </thead>
                        <tbody style="text-align: center">
                            <tr>
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
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(22)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(22)" value="提交" />
                </div>
                <!--表单22.潮汐预报-->


                <div class="dlgs" id="ddlg_24"
                    style="width: 530px; height: 335px; padding: 10px;">
                    <div style="height: 10px"></div>
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head0" colspan="7">下午十九、渤海及黄海北部冰情预报</th>
                            </tr>
                            <tr>
                                <th class="head0">海区</th>
                                <th class="head1">最大结冰范围（n mile）</th>
                                <th class="head0">一般冰厚（cm）</th>
                                <th class="head1">最大冰厚（cm）</th>
                            </tr>
                        </thead>

                        <tbody style="text-align: center">
                            <tr>
                                <td>辽东湾</td>
                                <td>
                                    <input id="LDW_ICE_MAXAREA" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="LDW_ICE_COMMONTHICKNESS" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="LDW_ICE_MAXTHICKNESS" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>渤海湾</td>
                                <td>
                                    <input id="BHW_ICE_MAXAREA" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BHW_ICE_COMMONTHICKNESS" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BHW_ICE_MAXTHICKNESS" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>莱州湾</td>
                                <td>
                                    <input id="LZW_ICE_MAXAREA" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="LZW_ICE_COMMONTHICKNESS" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="LZW_ICE_MAXTHICKNESS" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="NHH_ICE_MAXAREA" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="NHH_ICE_COMMONTHICKNESS" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="NHH_ICE_MAXTHICKNESS" type="text" maxlength="15" /></td>

                            </tr>
                        </tbody>
                    </table>

                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(24)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(24)" value="提交" />

                </div>
                <!--表单24.下午十九、渤海及黄海北部冰情预报-->

                <div class="dlgs" id="ddlg_26"
                    style="width: 1300px; height: 1235px; padding: 10px;">
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
                                <th class="head0" colspan="7">下午二十、指挥处下午预报</th>
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
                                <td rowspan="7" class="SJ_0001XW">*月*号20时至*月*号20时</td>
                                <td>青岛市</td>
                                <td>
                                    <input id="ZHC_XW_QD_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QD_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_QD_FL_01" type="text" maxlength="15" /></td>
                                <td colspan="2">水温：
                                        <input id="ZHC_XW_QD_SW_01" type="text" maxlength="15" /></td>
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
                                <td>黄海南部</td>
                                <td>
                                    <input id="ZHC_XW_SHH_TX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_BG_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_BX_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="4" class="SJ_0102XW">*月*号20时至*月*号20时</td>
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
                                <td>黄海南部</td>
                                <td>
                                    <input id="ZHC_XW_SHH_TX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_BG_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SHH_BX_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="4" class="SJ_0203XW">*月*号20时至*月*号20时</td>
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

                    <%-- <table id="addtable" cellpadding="0" cellspacing="0" border="0" class="stdtable" style="margin-top: 20px">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head1">海区</th>
                                <th class="head0">时效</th>
                                <th class="head1">风向(方位)</th>
                                <th class="head0">风力(级)</th>
                                <th class="head1">波高(米)</th>
                            </tr>
                        </thead>

                        <tbody style="text-align: center">
                            <tr>
                                <td rowspan="2">
                                    <input id="ZHC_XW_SA01" type="text" maxlength="15" /></td>
                                <td class="SJ_01">*日</td>
                                <td>
                                    <input id="ZHC_XW_SA01_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA01_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA01_BG_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*日</td>
                                <td>
                                    <input id="ZHC_XW_SA01_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA01_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA01_BG_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="2">
                                    <input id="ZHC_XW_SA02" type="text" maxlength="15" /></td>
                                <td class="SJ_01">*日</td>
                                <td>
                                    <input id="ZHC_XW_SA02_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA02_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA02_BG_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*日</td>
                                <td>
                                    <input id="ZHC_XW_SA02_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA02_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA02_BG_02" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td rowspan="2">
                                    <input id="ZHC_XW_SA03" type="text" maxlength="15" /></td>
                                <td class="SJ_01">*日</td>
                                <td>
                                    <input id="ZHC_XW_SA03_FX_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA03_FL_01" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA03_BG_01" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*日</td>
                                <td>
                                    <input id="ZHC_XW_SA03_FX_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA03_FL_02" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="ZHC_XW_SA03_BG_02" type="text" maxlength="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(26)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(26)" value="提交" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="addtr()" value="增加海区" />
                    --%>
                    <table id="addtable"  cellpadding="0" cellspacing="0" border="0" class="stdtable" style="margin-top: 20px">
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
                                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="addtr()" value="增加海区" /></th>
                            </tr>
                        </thead>
                    </table>
                     <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(26)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(26)" value="提交" />
                </div>
                <!--表单26.下午二十、指挥处下午预报-->




                <div id="dlg_23" class="dlgs ">
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
                    海浪预报员：<input id="Hailang" type="text" style="width: 200px" />
                    &nbsp;&nbsp;
                        潮汐预报员：<input id="Chaoxi" type="text" style="width: 200px" />
                    &nbsp;&nbsp;
                        水温预报员：<input id="Shuiwen" type="text" style="width: 200px" />
                    &nbsp;&nbsp;
                </div>
                <!--填报信息-->

                <div id="dlg_czrz" class="easyui-dialog" title="操作日志" data-options="iconCls:'icon-save'" style="width: 800px; height: 530px; padding: 10px;">
                    <iframe width="100%" id="win" height="435" name="czrz" frameborder="0" src="Logbyuser.aspx"></iframe>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_czrz').dialog('close'); " value="取消" />
                </div>
                <!--操作日志-->
                <!--<div id="basicform" style="clear: both" class="subcontent">-->
                <div id="basicform" style="position: fixed; bottom: 0px; left: 20px; z-index: 2">
                </div>
                <div id="dlg_xzmb" class="easyui-dialog" title="选择模版" data-options="iconCls:'icon-save'" style="width: 320px; height: 650px; padding: 10px;">
                    <div>
                        <asp:CheckBox ID="selectAll" runat="server" Text="全选" />
                        <asp:CheckBox ID="reverse" runat="server" Text="反选" />
                        <asp:CheckBox ID="unselect" runat="server" Text="取消" />
                    </div>
                    <hr />

                    <div id="modellist">
                        <div>
                            <asp:CheckBox ID="CheckBox3" runat="server" Text="1号山东省电视台预报单" />
                        </div>
                        <%--  <div>
                                <asp:CheckBox ID="CheckBox1" runat="server" Text=" 2号明日海洋预报(a4)" />
                            </div>--%>
                        <div>
                            <asp:CheckBox ID="CheckBox5" runat="server" Text="3号黄河南海堤2006(a4)-3hao" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox6" runat="server" Text="4号预报单(a4)-2014" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox7" runat="server" Text="5号预报单（a4）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox8" runat="server" Text="6号预报单（a4）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox9" runat="server" Text="7号海洋水温海冰预报" />
                        </div>

                        <div>
                            <asp:CheckBox ID="CheckBox11" runat="server" Text="9号预报单(a4)" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox12" runat="server" Text="10号预报单(A4)-南堡" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox13" runat="server" Text="11号预报单（a4）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox14" runat="server" Text="12号预报单(a4)" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox15" runat="server" Text="13号预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox16" runat="server" Text="14号海上山东（18a4）-gai14" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox17" runat="server" Text="15号预报单（a4）" />
                        </div>
                        <%-- <div>
                                <asp:CheckBox ID="CheckBox18" runat="server" Text="16号赤潮预报单(a4)" />
                            </div>--%>
                        <div>
                            <asp:CheckBox ID="CheckBox19" runat="server" Text="19号预报单--周（动态内容同月）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox20" runat="server" Text="19号预报单--年（红笔为动态内容）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox21" runat="server" Text="19号预报单--旬（动态内容同月）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox22" runat="server" Text="19号预报单--月（红笔为动态内容）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="20号潍坊市海洋预报台专项预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox23" runat="server" Text="21号青岛海水浴场预报-电视台播出" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox24" runat="server" Text="22号海洋预报-电视台播出-非游泳季节" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox25" runat="server" Text="24号东营专项预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox26" runat="server" Text="25号预报单" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="上午的指挥处预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="下午的指挥处预报" />
                        </div>
                    </div>

                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_xzmb').dialog('close'); $('#modellist div div span').removeClass('checked'); <%--$('#checkmodel').val('选择模版(' + $('#modellist .checked').length + ')');--%>" value="取消" />
                    <input id="Releasetable" style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" <%--onclick="$('#dlg_xzmb').dialog('close'); $('#checkmodel').val('选择模版(' + $('#modellist .checked').length + ')');"--%> value="发布表单" />
                </div>
                <!--选择模版-->
                <script type="text/javascript" language="javascript">
        function addtr() {
         
             var tab = document.getElementById("addtable") ;
		      //表格行数
		        var rows = tab.rows.length +2;    
		      
		        var i = 0;
           
                i=(rows-1)/2
      
               var tr= "<tr id ="+i+"><td rowspan='2'><input id='ZHC_XW_SA0"+i+"' type='text' maxlength='15' /></td>"+
                      "<td class='SJ_01'>*日</td>"+ 
                      "<td> <input id='ZHC_XW_SA0"+i+"_FX_01' type='text' maxlength='15' />ZHC_XW_SA0"+i+"_FX_01</td>"+
                      "<td> <input id='ZHC_XW_SA0"+i+"_FL_01' type='text' maxlength='15' />ZHC_XW_SA0"+i+"_FL_01</td>"+
                      "<td> <input id='ZHC_XW_SA0"+i+"_BG_01' type='text' maxlength='15' />ZHC_XW_SA0"+i+"_BG_01</td>"+
                      "<td rowspan='2'><input class='button' id ="+i+" type='button' value='删除' onclick='deletr(this)'></td> </tr>"+
                      "<tr id="+i+"><td class='SJ_02'>*日</td>"+
                      "<td> <input id='ZHC_XW_SA0"+i+"_FX_02' type='text' maxlength='15' />ZHC_XW_SA0"+i+"_FX_02</td>"+
                      "<td> <input id='ZHC_XW_SA0"+i+"_FL_02' type='text' maxlength='15' />ZHC_XW_SA0"+i+"_FL_02</td>"+
                      "<td> <input id='ZHC_XW_SA0"+i+"_BG_02' type='text' maxlength='15' />ZHC_XW_SA0"+i+"_BG_02</td>  </tr>";
          
　  　        $("#addtable").append(tr);
             var today = new Date(); // 获取今天时间
            GetDate(today); //根据当前年月日 推算未来年月日
           
        }
        function deletr(obj) {
            for (j = 1; j >= 0; j--) {
                $("#" + obj.id).remove();
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
            type: "Get",
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
                var datas = { datas: strlist, dates: date,hours: phour};
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
                case 7: submit_7(id); break;//表单7提交
                case 8: submit_8(id); break;//表单8提交
                case 9: submit_9(id); break;//表单9提交
                case 10: submit_10(id); break;//表单10提交
                case 11: submit_11(id); break;//表单11提交
                case 12: submit_12(id); break;//表单12提交
                case 13: submit_13(id); break;//表单13提交
                case 14: submit_14(id); break;//表单14提交
                case 15: submit_15(id); break;//表单15提交
                case 16: submit_16(id); break;//表单16提交
                case 17: submit_17(id); break;//表单17提交
                case 18: submit_18(id); break;//表单18提交
                case 19: submit_19(id); break;//表单19提交
                case 20: submit_20(id); break;//表单20提交
                case 21: submit_21(id); break;//表单21提交
                case 22: submit_22(id); break;//表单22提交
                case 23: submit_23(id); break;//表单23提交
                case 24: submit_24(id); break;//表单24提交
                case 25: submit_25(id); break;//表单25提交
                case 26: submit_26(id); break;//表单26提交
                default:
            }
        }

        //保存所有
        function alldlg_Submit() {//（无当天数据保存、有当天数据修改）↓
            submit_1(1); //表单1提交
            submit_2(2); //表单2提交
            submit_3(3); //表单3提交
            submit_4(4); //表单4提交
            submit_5(5); //表单5提交
            submit_6(6); //表单6提交
            submit_7(7); //表单7提交
            submit_8(8); //表单8提交
            submit_9(9); //表单9提交
            submit_10(10); //表单10提交
            submit_11(11); //表单11提交
            submit_12(12); //表单12提交
            submit_13(13); //表单13提交
            submit_14(14); //表单14提交
            submit_15(15); //表单15提交
            submit_16(16); //表单16提交
            submit_17(17); //表单17提交
            submit_18(18); //表单18提交
            submit_19(19); //表单19提交
            submit_20(20); //表单20提交
            submit_21(21); //表单21提交
            submit_22(22); //表单22提交
            submit_23(23); //表单23提交
            submit_24(24); //表单24提交
            submit_25(25); //表单25提交
            submit_26(26); //表单26提交
        }

        //表单数据拼接 从左至右 从上至下
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

            //表单06
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
                    str_data += $("#" + nqyname + "_LG").val() + ",";
                    str_data += $("#" + nqyname + "_BCSW").val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单07
            function submit_7(id) {
                var str_data = "";
                for (var i = 0; i < 7; i++) {
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
                    str_data += $("#" + nqyname + "_01G_H").val() + ",";
                    str_data += $("#" + nqyname + "_01G_MIN").val() + ",";
                    str_data += $("#" + nqyname + "_02G_H").val() + ",";
                    str_data += $("#" + nqyname + "_02G_MIN").val() + ",";
                    str_data += $("#" + nqyname + "_01D_H").val() + ",";
                    str_data += $("#" + nqyname + "_01D_MIN").val() + ",";
                    str_data += $("#" + nqyname + "_02D_H").val() + ",";
                    str_data += $("#" + nqyname + "_02D_MIN").val() + ",";
                }

                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单08
            function submit_8(id) {
                var str_data = "";
                str_data += $("#01GC_H").val() + ",";
                str_data += $("#01GC_MIN").val() + ",";
                str_data += $("#01GC_CM").val() + ",";
                str_data += $("#01DC_H").val() + ",";
                str_data += $("#01DC_MIN").val() + ",";
                str_data += $("#01DC_CM").val() + ",";
                str_data += $("#02GC_H").val() + ",";
                str_data += $("#02GC_MIN").val() + ",";
                str_data += $("#02GC_CM").val() + ",";
                str_data += $("#02DC_H").val() + ",";
                str_data += $("#02DC_MIN").val() + ",";
                str_data += $("#02DC_CM").val() + ",";
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
                str_data += $("#BH13_HL").val() + ",";
                str_data += $("#HHBB_HL").val() + ",";
                str_data += $("#HHZB_HL").val() + ",";
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

            //表单18
            function submit_18(id) {
                var str_data = "";
                str_data += $("#XMD_01GC_H").val() + ",";
                str_data += $("#XMD_01GC_MIN").val() + ",";
                str_data += $("#XMD_01DC_H").val() + ",";
                str_data += $("#XMD_01DC_MIN").val() + ",";
                str_data += $("#XMD_02GC_H").val() + ",";
                str_data += $("#XMD_02GC_MIN").val() + ",";
                str_data += $("#XMD_02DC_H").val() + ",";
                str_data += $("#XMD_02DC_MIN").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
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
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //表单22
            function submit_22(id) {
                var str_data = "";
                for (var i = 0; i < 5; i++) {
                    switch (i) {
                        case 0: nqyname = "WH"; break;
                        case 1: nqyname = "SD"; break;
                        case 2: nqyname = "WD"; break;
                        case 3: nqyname = "CST"; break;
                        case 4: nqyname = "RS"; break;
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
                // str_data += $("#Fabushijian").val() + ",";
                str_data += $("#Chuanzhen").val() + ",";
                str_data += $("#Hailang").val() + ",";
                str_data += $("#Chaoxi").val() + ",";
                str_data += $("#Shuiwen").val() + ",";
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
                str_data += "水温:,";
                str_data += $("#ZHC_MO_QD_SW_01").val() + ",";//青岛WAVEDIRECTION字段存水温
                //str_data += $("#ZHC_MO_QDJH_BG_01").val() + ",";
                //str_data += $("#ZHC_MO_QD_BX_01").val() + ",";

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
                str_data += "水温:,";
                str_data += $("#ZHC_XW_QD_SW_01").val() + ",";//青岛WAVEDIRECTION字段存水温
                //str_data += $("#ZHC_XW_QDJH_BG_01").val() + ",";
                //str_data += $("#ZHC_XW_QD_BX_01").val() + ",";

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

                var tab = document.getElementById("addtable") ;
		        var rows = tab.rows.length;    
		        var count = (rows - 1) / 2;
		        for (var i = 1; i <= count;i++)
		        {
                    str_data += $("#ZHC_XW_SA0"+i).val() + ",";
                    str_data += $("#ZHC_XW_SA0"+i+"_FX_01").val() + ",";
                    str_data += $("#ZHC_XW_SA0"+i+"_FL_01").val() + ",";
                    str_data += $("#ZHC_XW_SA0"+i+"_BG_01").val() + ",";
                    str_data += $("#ZHC_XW_SA0"+i+"_FX_02").val() + ",";
                    str_data += $("#ZHC_XW_SA0"+i+"_FL_02").val() + ",";
                    str_data += $("#ZHC_XW_SA0"+i+"_BG_02").val() + ",";
		        }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                //alert(str_data);
                submit_ajax(id, data);
            }



            //ajax公共方法(表单类型，post数组)
            function submit_ajax(types, datas1) {
                //   alert(datas[0].datas);
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
                type: "GET",
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

            var today = new Date(); // 获取今天时间
            GetDate(today); //根据当前年月日 推算未来年月日

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
                type: "GET",
                url: "/Ajax/gettablelist.ashx?method=getbydata&date=" + dates + "&searchtype=" + searchType,
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
                            case "t4": gettable04(resjson[j].children, date1); dlgclose("4"); break;
                            case "t5": gettable05(resjson[j].children, date1); dlgclose("5"); break;
                            case "t6": gettable06(resjson[j].children, date1); dlgclose("6"); break;
                            case "t7": gettable07(resjson[j].children, date1); dlgclose("7"); break;
                            case "t8": gettable08(resjson[j].children, date1); dlgclose("8"); break;
                            case "t9": gettable09(resjson[j].children, date1); dlgclose("9"); break;
                            case "t10": gettable10(resjson[j].children, date1); dlgclose("10"); break;
                            case "t11": gettable11(resjson[j].children, date1); dlgclose("11"); break;
                            case "t12": gettable12(resjson[j].children, date1); dlgclose("12"); break;
                            case "t13": gettable13(resjson[j].children, date1); dlgclose("13"); break;
                            case "t14": gettable14(resjson[j].children, date1); dlgclose("14"); break;
                            case "t15": gettable15(resjson[j].children, date1); dlgclose("15"); break;
                            case "t16": gettable16(resjson[j].children, date1); dlgclose("16"); break;
                            case "t17": gettable17(resjson[j].children, date1); dlgclose("17"); break;
                            case "t18": gettable18(resjson[j].children, date1); dlgclose("18"); break;
                            case "t19": gettable19(resjson[j].children, date1); dlgclose("19"); break;
                            case "t20": gettable20(resjson[j].children, date1); dlgclose("20"); break;
                            case "t21": gettable21(resjson[j].children, date1); dlgclose("21"); break;
                            case "t22": gettable22(resjson[j].children, date1); dlgclose("22"); break;
                            case "t23": gettable23(resjson[j].children, date1); break;
                            case "t24": gettable24(resjson[j].children, date1); dlgclose("24"); break;
                            case "t25": gettable25(resjson[j].children, date1); dlgclose("25"); break;
                            case "t26": gettable26(resjson[j].children, date1); dlgclose("26"); break;

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
            //表单01数据
            function gettable01(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    if (resjson[i].qy == "渤海") {
                        date = new Date(resjson[i].yb);
                        //  date.getDate() - date1.getDate();
                        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        //  return intDays;
                        $("#BH_BG_0" + num).val(resjson[i].bg);
                        $("#BH_BX_0" + num).val(resjson[i].bx);
                        $("#BH_FX_0" + num).val(resjson[i].fx);
                        $("#BH_FL_0" + num).val(resjson[i].fl);
                        $("#BH_SW_0" + num).val(resjson[i].sw);
                        //$("#BH_BG_01").val(resjson[i].bg);
                        //$("#BH_BX_01").val(resjson[i].bx);
                        //$("#BH_FX_01").val(resjson[i].fx);
                        //$("#BH_FL_01").val(resjson[i].fl);
                        //$("#BH_SW_01").val(resjson[i].sw);

                    } else if (resjson[i].qy == "黄河海港") {
                        date = new Date(resjson[i].yb);
                        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        $("#HHHG_BG_0" + num).val(resjson[i].bg);
                        $("#HHHG_BX_0" + num).val(resjson[i].bx);
                        $("#HHHG_FX_0" + num).val(resjson[i].fx);
                        $("#HHHG_FL_0" + num).val(resjson[i].fl);
                        $("#HHHG_SW_0" + num).val(resjson[i].sw);
                    }
                }
            }

            //表单02数据
            function gettable02(resjson, date1) {//LKG_01G_SJ_01
                for (var i = 0; i < resjson.length; i++) {
                    if (resjson[i].qy == "龙口港") {

                        date = new Date(resjson[i].yb);
                        //   num = date.getDate() - date1.getDate();
                        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        $("#LKG_01G_CW_0" + num).val(resjson[i].g1c);
                        $("#LKG_01G_SJ_0" + num).val(resjson[i].g1s);
                        $("#LKG_01D_CW_0" + num).val(resjson[i].d1c);
                        $("#LKG_01D_SJ_0" + num).val(resjson[i].d1s);
                        $("#LKG_02G_CW_0" + num).val(resjson[i].g2c);
                        $("#LKG_02G_SJ_0" + num).val(resjson[i].g2s);
                        $("#LKG_02D_CW_0" + num).val(resjson[i].d2c);
                        $("#LKG_02D_SJ_0" + num).val(resjson[i].d2s);
                    } else if (resjson[i].qy == "黄河海港") {
                        date = new Date(resjson[i].yb);
                        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        $("#HHHG_01G_CW_0" + num).val(resjson[i].g1c);
                        $("#HHHG_01G_SJ_0" + num).val(resjson[i].g1s);
                        $("#HHHG_01D_CW_0" + num).val(resjson[i].d1c);
                        $("#HHHG_01D_SJ_0" + num).val(resjson[i].d1s);
                        $("#HHHG_02G_CW_0" + num).val(resjson[i].g2c);
                        $("#HHHG_02G_SJ_0" + num).val(resjson[i].g2s);
                        $("#HHHG_02D_CW_0" + num).val(resjson[i].d2c);
                        $("#HHHG_02D_SJ_0" + num).val(resjson[i].d2s);
                    }
                }
            }

            //表单03数据
            function gettable03(resjson, date1) {//LKG_01G_SJ_01
                for (var i = 0; i < resjson.length; i++) {
                    $("#BH_GDF_01").val(resjson[i].bdl);
                    $("#BH_GDE_01").val(resjson[i].bgl);
                    $("#BH_HL_01").val(resjson[i].blx);
                    $("#HHBB_GDF_01").val(resjson[i].hdl);
                    $("#HHBB_GDE_01").val(resjson[i].hgl);
                    $("#HHBB_HL_01").val(resjson[i].hlx);
                    $("#DKHY_LG_01").val(resjson[i].dl);
                    $("#DKHY_SW_01").val(resjson[i].ds);
                    $("#HHK_LG_01").val(resjson[i].hl);
                    $("#HHK_SW_01").val(resjson[i].hs);
                    $("#GDG_LG_01").val(resjson[i].gl);
                    $("#GDG_SW_01").val(resjson[i].gs);
                    $("#DYG_LG_01").val(resjson[i].dyl);
                    $("#DYG_SW_01").val(resjson[i].dys);
                    $("#XH_LG_01").val(resjson[i].xl);
                    $("#XH_SW_01").val(resjson[i].xs);
                    $("#CK_LG_01").val(resjson[i].cl);
                    $("#CK_SW_01").val(resjson[i].cs);
                }
            }

            //表单04数据
            function gettable04(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].qy) {
                        case "飞雁滩": nqyname = "FYT"; break;
                        case "孤东": nqyname = "GD"; break;
                        case "小岛河": nqyname = "XDH"; break;
                        case "东营港": nqyname = "DYG"; break;
                        default:
                    }
                    $("#" + nqyname + "_01G_CW_01").val(resjson[i].g1c);
                    $("#" + nqyname + "_01G_SJ_01").val(resjson[i].g1s);
                    $("#" + nqyname + "_01D_CW_01").val(resjson[i].d1c);
                    $("#" + nqyname + "_01D_SJ_01").val(resjson[i].d1s);
                    $("#" + nqyname + "_02G_CW_01").val(resjson[i].g2c);
                    $("#" + nqyname + "_02G_SJ_01").val(resjson[i].g2s);
                    $("#" + nqyname + "_02D_CW_01").val(resjson[i].d2c);
                    $("#" + nqyname + "_02D_SJ_01").val(resjson[i].d2s);
                }
            }

            //表单05数据
            function gettable05(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].qy) {
                        case "渤海": nqyname = "BH"; break;
                        case "黄海北部": nqyname = "HHBB"; break;
                        case "黄海中部": nqyname = "HHZB"; break;
                        case "黄海南部": nqyname = "HHNB"; break;
                        default:
                    }
                    $("#" + nqyname + "_GDF_401").val(resjson[i].zdl);
                    $("#" + nqyname + "_GDE_401").val(resjson[i].zgl);
                    $("#" + nqyname + "_HL_401").val(resjson[i].lx);
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
                    switch (resjson[i].qy) {
                        case "日照": nqyname = "RZ"; break;
                        case "青岛": nqyname = "QD"; break;
                        case "威海": nqyname = "WH"; break;
                        case "烟台": nqyname = "YT"; break;
                        case "潍坊": nqyname = "WF"; break;
                        case "东营": nqyname = "DY"; break;
                        case "滨州": nqyname = "BZ"; break;
                        default:
                    }
                    $("#" + nqyname + "_01G_H").val(resjson[i].g1s);
                    $("#" + nqyname + "_01G_MIN").val(resjson[i].g1m);
                    $("#" + nqyname + "_01D_H").val(resjson[i].d1s);
                    $("#" + nqyname + "_01D_MIN").val(resjson[i].d1m);
                    $("#" + nqyname + "_02G_H").val(resjson[i].g2s);
                    $("#" + nqyname + "_02G_MIN").val(resjson[i].g2m);
                    $("#" + nqyname + "_02D_H").val(resjson[i].d2s);
                    $("#" + nqyname + "_02D_MIN").val(resjson[i].d2m);
                }
            }

            //表单08数据
            function gettable08(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    $("#01GC_H").val(resjson[i].g1s);
                    $("#01GC_MIN").val(resjson[i].g1m);
                    $("#01GC_CM").val(resjson[i].g1g);
                    $("#01DC_H").val(resjson[i].d1s);
                    $("#01DC_MIN").val(resjson[i].d1m);
                    $("#01DC_CM").val(resjson[i].d1g);
                    $("#02GC_H").val(resjson[i].g2s);
                    $("#02GC_MIN").val(resjson[i].g2m);
                    $("#02GC_CM").val(resjson[i].g2g);
                    $("#02DC_H").val(resjson[i].d2s);
                    $("#02DC_MIN").val(resjson[i].d2m);
                    $("#02DC_CM").val(resjson[i].d2g);
                    $("#MRHBLG").val(resjson[i].bh);
                    $("#MRHBLX").val(resjson[i].lx);
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
                    date = new Date(resjson[i].yb);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#NBYT_BG_0" + num).val(resjson[i].bg);
                    $("#NBYT_BX_0" + num).val(resjson[i].bx);
                    $("#NBYT_FX_0" + num).val(resjson[i].fx);
                    $("#NBYT_FL_0" + num).val(resjson[i].fl);
                    $("#NBYT_SW_0" + num).val(resjson[i].sw);
                    $("#NBYT_TQ_0" + num).val(resjson[i].tq);
                }
            }

            //表单12数据
            function gettable12(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    date = new Date(resjson[i].yb);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#NBYT_01G_CW_0" + num).val(resjson[i].g1c);
                    $("#NBYT_01G_SJ_0" + num).val(resjson[i].g1s);
                    $("#NBYT_01D_CW_0" + num).val(resjson[i].d1c);
                    $("#NBYT_01D_SJ_0" + num).val(resjson[i].d1s);
                    $("#NBYT_02G_CW_0" + num).val(resjson[i].g2c);
                    $("#NBYT_02G_SJ_0" + num).val(resjson[i].g2s);
                    $("#NBYT_02D_CW_0" + num).val(resjson[i].d2c);
                    $("#NBYT_02D_SJ_0" + num).val(resjson[i].d2s);
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
                    $("#BH13_HL").val(resjson[i].bj);
                    $("#HHBB_HL").val(resjson[i].hbbj);
                    $("#HHZB_HL").val(resjson[i].hzbj);
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
                    $("#XMD_01GC_H").val(resjson[i].g1s);
                    $("#XMD_01GC_MIN").val(resjson[i].g1f);
                    $("#XMD_01DC_H").val(resjson[i].d1s);
                    $("#XMD_01DC_MIN").val(resjson[i].d1f);
                    $("#XMD_02GC_H").val(resjson[i].g2s);
                    $("#XMD_02GC_MIN").val(resjson[i].g2f);
                    $("#XMD_02DC_H").val(resjson[i].d2s);
                    $("#XMD_02DC_MIN").val(resjson[i].d2f);
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

                }
            }

            //表单22数据
            function gettable22(resjson, date1) {//LKG_01G_SJ_01
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
                    $("#Fabushijian").val(myformatter1(new Date(resjson[i].rq)) + resjson[i].xs + "时");
                    $("#Chuanzhen").val(resjson[i].cz);
                    $("#Hailang").val(resjson[i].hl);
                    $("#Chaoxi").val(resjson[i].cx);
                    $("#Shuiwen").val(resjson[i].sw);
                }
            }


            //表单24数据
            function gettable24(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].sa) {
                        case "辽东湾": nqyname = "LDW"; break;
                        case "渤海湾": nqyname = "BHW"; break;
                        case "莱州湾": nqyname = "LZW"; break;
                        case "黄海北部": nqyname = "NHH"; break;
                        default:
                    }

                    $("#" + nqyname + "_ICE_MAXAREA").val(resjson[i].ma);
                    $("#" + nqyname + "_ICE_COMMONTHICKNESS").val(resjson[i].ct);
                    $("#" + nqyname + "_ICE_MAXTHICKNESS").val(resjson[i].mt);
                }
            }

        }//表单1-24

        //表单指挥处07点填报数据
        function gettable25(resjson, date1) {
            //var date2 = date1.setDate(date1.getDate() - 1);
            if (resjson.length > 0) {
                var publishDate = resjson[0].PUBLISHDATE;
                var pdate = new Date(publishDate);
                pdate.setHours(0);

                for (var i = 0; i < resjson.length; i++) {
                    var seaArea = resjson[i].SEAAREA;
                    //var publishDate = resjson[i].PUBLISHDATE;
                    var forecastDate = resjson[i].FORECASTDATE;
                    var fdate = new Date(forecastDate);
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
                            break;
                    }
                    var TXInputId = "#ZHC_MO_" + areaCode + "_TX_0" + num;
                    var FXInputId = "#ZHC_MO_" + areaCode + "_FX_0" + num;
                    var FLInputId = "#ZHC_MO_" + areaCode + "_FL_0" + num;
                    if (areaCode == "QD") {
                        var SWInputId = "#ZHC_MO_QD_SW_01";
                        $(SWInputId).val(resjson[i].WAVEDIRECTION);
                    }
                    else {
                        var BGInputId = "#ZHC_MO_" + areaCode + "_BG_0" + num;
                        var BXInputId = "#ZHC_MO_" + areaCode + "_BX_0" + num;
                        $(BGInputId).val(resjson[i].WAVEHEIGHT);
                        $(BXInputId).val(resjson[i].WAVEDIRECTION);
                    }
                    $(TXInputId).val(resjson[i].WEATHERAPPEARANCE);
                    $(FXInputId).val(resjson[i].WINDDIRECTION);
                    $(FLInputId).val(resjson[i].WINDFORCE);
                }
            }
        }
        //表单指挥处16点填报数据
        function gettable26(resjson, date1) {

            if (resjson.length > 1) {

                var addedSeaAreaCount = (resjson.length -15)/2;
                for (var k = 0; k < addedSeaAreaCount;k++)
                {
                    addtr();
                }
                var publishDate = resjson[0].PUBLISHDATE;
                var pdate = new Date(publishDate);
                pdate.setHours(0);
                
                for (var i = 0; i < resjson.length; i++) {
                    var seaArea = resjson[i].SEAAREA;
                    var forecastDate = resjson[i].FORECASTDATE;
                    var fdate = new Date(forecastDate);
                    var num = 0;
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
                            num = parseInt(Math.abs(fdate - pdate) / 1000 / 60 / 60 / 24);
                            for (var m = 1; m <= addedSeaAreaCount;m++)
                            {
                                if($("#ZHC_XW_SA0"+m).val()==""||$("#ZHC_XW_SA0"+m).val()==seaArea)
                                {
                                    $("#ZHC_XW_SA0"+m).val(seaArea);
                                    $("#ZHC_XW_SA0"+m+"_FX_0"+num).val(resjson[i].WINDDIRECTION);
                                    $("#ZHC_XW_SA0"+m+"_FL_0"+num).val(resjson[i].WINDFORCE);
                                    $("#ZHC_XW_SA0"+m+"_BG_0"+num).val(resjson[i].WAVEHEIGHT);
                                    break;
                                }
                            }
                            break;
                    }
                    var TXInputId = "#ZHC_XW_" + areaCode + "_TX_0" + num;
                    var FXInputId = "#ZHC_XW_" + areaCode + "_FX_0" + num;
                    var FLInputId = "#ZHC_XW_" + areaCode + "_FL_0" + num;
                    if (areaCode == "QD") {
                        var SWInputId = "#ZHC_XW_QD_SW_01";
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
            var date = new Date($("#tianbaoriqi").datebox("getValue"));
            gettabledata(date, "p");
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
                show72hOr7d(selectDate);
                show72hOr7dForPortTide(selectDate);
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

<%-- 
    变更记录1/**/
变更时间：10180719    
变更内容：下午四潮汐取消，取数从下午三青岛24小时预报取
          下午五取消，取数从下午三青岛48小时预报取
          下午十二潮汐预报取消，从下午三潍坊24小时预报取数
          下午十四的金沙滩预报取消，从下午三三天预报中取数
          下午十八威海取消，从下午三威海24小时预报取数
          周报十一潍坊港取消，从周报五海上丝绸之路三天潮汐预报中潍坊港取
变更人员：Yuy     
    
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeekTableList.aspx.cs" Inherits="PredicTable.WeekTableList" %>

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

    <script type="text/javascript" src="js/MsgBox.js"></script>

    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/Gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />

    <%--    <script src="js/crossdomain.js"></script>--%>
    <script>
        //$(function () {
            
        //    getuserinfo("-1");
        //});

    </script>
    <style>
        body {
            color:#333333 !important;
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
    <script>
      

        var cx_arry = new Array( "4", "28","31","30","36","38","51");
        var sw_arry = new Array( "3","32","37","50");
        var fl_arry = new Array("25","3", "30", "29","28","39","37","50","52");
        var hb_arry = new Array("24");//24
        var type = "<%=Session["type"]%>";
        var makewordtime = "am";
        
          function quanxian(type, date) {
              if (getdatenow() == date) {
                  switch (type) { //all_hide(); show_bytype(cx_arry);
                      case "cx": all_disabled(); cx_isabled(); tb_isabled(); $("#yby_type").val("潮汐");  break;//潮汐能填写
                      case "fl": all_isabled(); cx_disabled(); sw_disabled(); tb_isabled(); $("#yby_type").val("风、海浪"); fl_isabled(); break;//风浪能填写
                      case "sw": all_disabled(); sw_isabled(); tb_isabled(); $("#yby_type").val("水温"); break;//水温能填写
                      case "hb": all_disabled(); hb_isabled(); tb_isabled(); $("#yby_type").val("海冰"); break;//海冰能填写
                      default: all_disabled(); $("#yby_type").val("无"); break;// 都不能填写
                  }
              } else {//不是当天不能编辑
                  switch (type) {
                      case "cx": all_disabled();  $("#yby_type").val("潮汐");  break;//都不能填写
                      case "fl": all_disabled();   tb_isabled(); $("#yby_type").val("风、海浪");  break;//风浪能填写
                      case "sw": all_disabled();  $("#yby_type").val("水温");  break;//都不能填写
                      case "hb": all_disabled();  tb_isabled(); $("#yby_type").val("海冰"); break;//海冰能填写
                      default: all_disabled(); $("#yby_type").val("无"); break;// 都不能填写
                  }
              }
          }
        $(function () {
                  //getuserinfo(type);
                  quanxian(type,getdatenow());
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
                              case "hb": all_hide(); show_bytype(hb_arry);break;//海冰能填写
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
                              case "hb": all_hide(); show_bytype(hb_arry);break;//海冰能填写
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
            $("#lx_1").text("一、72小时渤海海区及黄河海港风、浪预报");            $("#ddlg_01title").text("一、72小时渤海海区及黄河海港风、浪预报");

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
            $("#YT_HL_01").focus(function () { //dgl_50
                
                var numf = $("#YT_GDF_01").val();
                //var nume = $("#YT_GDE_01").val();
                if (numf != "") {
                    $("#YT_HL_01").val(setlevel(numf));
                }
            });
        });
    </script>
</head>
<body>
     

<iframe width="0" height="0" src="SessionKeeper.asp"> 

</iframe>  

 

    <form id="form2" runat="server">

        <%--<div class="bodywrapper">--%>

        <div <%--class="centercontent"--%>>
            <div id="contentwrapper" class="contentwrapper" >
                <div>
                    <div style="position: fixed; top: 0px; left: 20px; z-index: 2 ;display: none">
                        <ul id="leixing1">
                            <li id="swbd" style="border-color: #fb9337; background-color: #fb9337; color: white; font-weight: bold">上午预报表单</li>
                           
                            <li id="lx_3" onclick="click_scroll('ddlg_03')">四、预计未来24小时海浪、水温预报</li>
                            <li id="lx_4" onclick="click_scroll('ddlg_04')">五、24小时潮位预报</li>
                            <li id="lx_28" onclick="click_scroll('ddlg_28')">六、24小时水文气象预报综述</li>
                            <li id="lx_39" onclick="click_scroll('ddlg_39')">七、海上丝绸之路三天海浪、气象预报</li>
                            <li id="lx_38" onclick="click_scroll('ddlg_38')">八、海上丝绸之路三天潮汐预报</li>
                            <li id="lx_29" onclick="click_scroll('ddlg_29')">九、7天渤海海区及黄河海港风、浪预报</li>
                            <li id="lx_31" onclick="click_scroll('ddlg_31')">十、7天港口潮位预报</li>
                            <li id="lx_25" onclick="click_scroll('ddlg_25')">十一、指挥处上午预报</li>
                            <li id="lx_23" onclick="click_scroll('ddlg_24')">十二、东营胜利油田专项海冰周报</li>
                            <li id="lx_06" onclick="click_scroll('ddlg_32')">十三、东营胜利油田专项海温周报</li>
                          
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
                        </select>&nbsp; 填报日期：<input id="tianbaoriqi" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser"/>
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
                        <input type="button" id="checkmodel" onclick="$('#dlg_xzmb').dialog('open'); click_scroll('dlg_xzmb') " class="stdbtn" value="选择模版并发布" />
                        <input type="button" id="setall" onclick="alldlg_Submit()" class="stdbtn" value="保存所有" />
                        <input type="button" id="btnrole" class="stdbtn" value="验证所有" />
                        <input type="button" id ="ReleasetableAll" onclick="All_Releasetable()" class="stdbtn" value="发布周一表单" />
                         <%--<input type="button" id ="btn_check " onclick=" check()" class="stdbtn" value="查看预报单" />--%>
                        <br />
                    </div>
                </div>
                <!--表单信息-->
                
                <%--周报一、预计未来24小时海浪一--%>
                <div class="dlgs" id="ddlg_03" style="height: 460px; padding: 10px; width: auto;">

                    <table style="border: 1px solid #ddd; width: 100%;">
                        <tr style="line-height: 45px;">
                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="7">周报一、预计未来24小时海浪、水温预报</td>
                        </tr>
                        <tr style="line-height: 45px; display: none;">
                            <td style="width: 60px;"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="width: 100px; text-align: center;">渤海</td>
                            <td colspan="2">有<input maxlength="20" id="BH_GDF_01" onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="width: 80%" type="text" />m</td>
                            <td colspan="2">到<input maxlength="20" id="BH_GDE_01" onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" style="width: 80%" />m</td>
                            <td colspan="2">的<input maxlength="20" id="BH_HL_01" type="text" style="width: 80%" /></td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="text-align: center;">黄海北部</td>
                            <td colspan="2">有<input maxlength="20" id="HHBB_GDF_01" onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" style="width: 80%" />m</td>
                            <td colspan="2">到<input maxlength="20" id="HHBB_GDE_01" onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" style="width: 80%" />m</td>
                            <td colspan="2">的<input maxlength="20" id="HHBB_HL_01" type="text" style="width: 80%" /></td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="text-align: center;">刁口海域</td>
                            <td colspan="3">浪高 
                                <input id="DKHY_LG_01" type="text" maxlength="80" style="width: 80%" />米</td>
                            <td colspan="3">水温
                                <input id="DKHY_SW_01" type="text" maxlength="20" style="width: 80%" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="text-align: center;">黄河口海域</td>
                            <td colspan="3">浪高 
                                <input id="HHK_LG_01" type="text" maxlength="80" style="width: 80%" />米</td>
                            <td colspan="3">水温
                                <input id="HHK_SW_01" type="text" maxlength="20" style="width: 80%" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="text-align: center;">广利港海域</td>
                            <td colspan="3">浪高 
                                <input id="GDG_LG_01" type="text" maxlength="80" style="width: 80%" />米</td>
                            <td colspan="3">水温
                                <input id="GDG_SW_01" type="text" maxlength="20" style="width: 80%" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="text-align: center;">东营港海域</td>
                            <td colspan="3">浪高 
                                <input id="DYG_LG_01" type="text" maxlength="80" style="width: 80%" />米</td>
                            <td colspan="3">水温
                                <input id="DYG_SW_01" type="text" maxlength="20" style="width: 80%" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="text-align: center;">新户海域</td>
                            <td colspan="3">浪高
                                <input id="XH_LG_01" type="text" maxlength="20" style="width: 80%" />米</td>
                            <td colspan="3">水温
                                <input id="XH_SW_01" type="text" maxlength="20" style="width: 80%" />℃</td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="text-align: center;">埕口海域</td>
                            <td colspan="3">浪高 
                                <input id="CK_LG_01" type="text" maxlength="20" style="width: 80%" />米</td>
                            <td colspan="3">水温
                                <input id="CK_SW_01" type="text" maxlength="20" style="width: 80%" />℃</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(3)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(3)" value="提交" />

                </div>
                <!--表单04.预计未来24小时海浪、水温预报-->

                <%--周报二、24小时水文气象预报综述--%>
                <div class="dlgs" id="ddlg_28" style="height: auto; padding: 10px;">
                    <div style="border: 1px solid #ddd; width: auto; line-height: 36px; font-weight: bold; text-align: center; background-color:#d2ffe7;">周报二、24小时水文气象预报综述</div>
                    <div style="border: 1px solid #ddd; background-color:#d2ffe7;">
                      海浪：<textarea name="txt_SWQX_ZS_24HOURS" style="margin: 10px 10px 10px 10px; width: 90%; height: 20px;font-size:18px; font-weight:500;" id="SWQX_ZS_24HOURS"></textarea>
                    </div>
                     <div style="border: 1px solid #ddd; background-color:#d2ffe7;">
                        潮汐：<textarea name="txt_CXQX_ZS_24HOURS" style="margin: 10px 10px 10px 10px; width: 90%; height: 20px;font-size:18px; font-weight:500;" id="CXQX_ZS_24HOURS"></textarea>
                    </div>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(28)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(28)" value="提交" />

                </div>
                <!--表单06.24小时水文气象预报综述-->

                <%--周报三、24小时潮位预报--%>
                <div class="dlgs" id="ddlg_04" style="height: 355px; padding: 10px;">
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

                                <th class="head1" colspan="10">周报三、24小时潮位预报</th>
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
                        <tbody class="textStyle" style="text-align: center">
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
                            <tr>
                                <td>桩西</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="ZX_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="ZX_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="ZX_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="ZX_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="ZX_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="ZX_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="ZX_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="ZX_02D_CW_01" type="text" /></td>
                            </tr>
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
                                <td>新户</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="XH_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="XH_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="XH_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="XH_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="XH_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="XH_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="XH_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="XH_02D_CW_01" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(4)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(4)" value="提交" />
                </div>
                <!--表单05.24小时潮位预报-->

                <%--周报四、海上丝绸之路三天海浪、气象预报--%>
                <div class="dlgs" id="ddlg_39" style="height: 390px; padding: 10px;">
                    <div style="height: 10px"></div>
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th id="ddlg_34title" class="head0" colspan="7">周报四、海上丝绸之路三天海浪、气象预报</th>
                            </tr>
                            <tr>
                                <th class="head0">区域</th>
                                <th class="head1">日期</th>
                                <th class="head0">波高（h）</th>
                                <th class="head1">波向（方位）</th>
                                <th class="head0">风向（方位）</th>
                                <th class="head1">风力（级）</th>
                            </tr>
                        </thead>
                        <tbody id="" style="text-align: center">
                            <tr>
                                <td rowspan="3">青岛港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="SILK_QD_BG_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_QD_BX_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_QD_FX_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_QD_FL_01" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="SILK_QD_BG_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_QD_BX_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_QD_FX_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_QD_FL_02" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="SILK_QD_BG_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_QD_BX_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_QD_FX_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_QD_FL_03" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">潍坊港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="SILK_WF_BG_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_WF_BX_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_WF_FX_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_WF_FL_01" type="text" maxlength="20" /></td>

                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="SILK_WF_BG_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_WF_BX_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_WF_FX_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_WF_FL_02" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="SILK_WF_BG_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_WF_BX_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_WF_FX_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_WF_FL_03" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">营口港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="SILK_YK_BG_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_YK_BX_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_YK_FX_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_YK_FL_01" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="SILK_YK_BG_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_YK_BX_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_YK_FX_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_YK_FL_02" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="SILK_YK_BG_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_YK_BX_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_YK_FX_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="SILK_YK_FL_03" type="text" maxlength="20" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(39)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(39)" value="提交" />
                </div>

                <%--周报五、海上丝绸之路三天潮汐预报--%>
                <div class="dlgs" id="ddlg_38" style="height: 430px; padding: 10px;">
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
                                <th class="head1" colspan="10">周报五、海上丝绸之路三天潮汐预报</th>
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
                        <tbody class="textStyle" style="text-align: center">
                            <tr>
                                <td rowspan="3">青岛港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="QDG_01G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_01G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_01D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_01D_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02D_CW_01" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="QDG_01G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_01G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_01D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_01D_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02D_CW_02" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="QDG_01G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_01G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_01D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_01D_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="QDG_02D_CW_03" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">潍坊港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="WFG_01G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_01G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_01D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_01D_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02D_CW_01" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="WFG_01G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_01G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_01D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_01D_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02D_CW_02" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="WFG_01G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_01G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_01D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_01D_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="WFG_02D_CW_03" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td rowspan="3">营口港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="YKG_01G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_01G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_01D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_01D_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02G_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02G_CW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02D_SJ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02D_CW_01" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="YKG_01G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_01G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_01D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_01D_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02G_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02G_CW_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02D_SJ_02" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02D_CW_02" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="YKG_01G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_01G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_01D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_01D_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02G_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02G_CW_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02D_SJ_03" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YKG_02D_CW_03" type="text" maxlength="20" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(38)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(38)" value="提交" />
                </div>
             
                <%--周报六、7天渤海海区及黄河海港风、浪预报--%>
                <div class="dlgs" id="ddlg_29" style="height: 390px; padding: 10px;">
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
                                <th id="ddlg_01title" class="head0" colspan="7">周报六、7天渤海海区及黄河海港风、浪预报</th>
                            </tr>
                            <tr>
                                <th class="head0">区域</th>
                                <th class="head1">日期</th>
                                <th class="head0">波高（h）</th>
                                <th class="head1">波向（方位）</th>
                                <th class="head0">风向（方位）</th>
                                <th class="head1">风力（级）</th>
                                <th class="head0"  style="display:none">水温（℃）</th>
                            </tr>
                        </thead>
                        <tbody id="ddlg_01_tbody" style="text-align: center" class="week_report">
                            <tr>
                                <td rowspan="7">渤海</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="BH_BG_01_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_BX_01_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FX_01_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FL_01_7Days" type="text" maxlength="15" /></td>
                                <td style="display:none">
                                    <input id="BH_SW_01_7Days" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="BH_BG_02_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_BX_02_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FX_02_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FL_02_7Days" type="text" maxlength="15" /></td>
                                <td style="display:none">
                                    <input id="BH_SW_02_7Days" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="BH_BG_03_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_BX_03_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FX_03_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FL_03_7Days" type="text" maxlength="15" /></td>
                                <td style="display:none">
                                    <input id="BH_SW_03_7Days" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_04">*月*号</td>
                                <td>
                                    <input id="BH_BG_04_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_BX_04_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FX_04_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="BH_FL_04_7Days" type="text" maxlength="15" /></td>
                               <td style="display:none">
                                    <input id="BH_SW_04_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_05">*月*号</td>
                                <td>
                                    <input id="BH_BG_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="BH_BX_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="BH_FX_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="BH_FL_05_7Days" type="text" maxlength="20" /></td>
                                <td style="display:none">
                                    <input id="BH_SW_05_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_06">*月*号</td>
                                <td>
                                    <input id="BH_BG_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="BH_BX_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="BH_FX_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="BH_FL_06_7Days" type="text" maxlength="20" /></td>
                               <td style="display:none">
                                    <input id="BH_SW_06_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_07">*月*号</td>
                                <td>
                                    <input id="BH_BG_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="BH_BX_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="BH_FX_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="BH_FL_07_7Days" type="text" maxlength="20" /></td>
                                <td  style="display:none">
                                    <input id="BH_SW_07_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td rowspan="7">黄河海港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_BX_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_FX_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_FL_01_7Days" type="text" maxlength="20" /></td>
                                <td style="display:none">
                                    <input id="HHHG_SW_01_7Days" type="text" maxlength="20" /></td>

                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_BX_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_FX_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_FL_02_7Days" type="text" maxlength="20" /></td>
                                <td style="display:none">
                                    <input id="HHHG_SW_02_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_03_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_BX_03_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FX_03_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FL_03_7Days" type="text" maxlength="15" /></td>
                                <td style="display:none">
                                    <input id="HHHG_SW_03_7Days" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_04">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_04_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_BX_04_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FX_04_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FL_04_7Days" type="text" maxlength="15" /></td>
                              <td style="display:none">
                                    <input id="HHHG_SW_04_7Days" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_05">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_05_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_BX_05_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FX_05_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FL_05_7Days" type="text" maxlength="15" /></td>
                               <td style="display:none">
                                    <input id="HHHG_SW_05_7Days" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_06">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_06_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_BX_06_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FX_06_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FL_06_7Days" type="text" maxlength="15" /></td>
                                <td style="display:none">
                                    <input id="HHHG_SW_06_7Days" type="text" maxlength="15" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_07">*月*号</td>
                                <td>
                                    <input id="HHHG_BG_07_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_BX_07_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FX_07_7Days" type="text" maxlength="15" /></td>
                                <td>
                                    <input id="HHHG_FL_07_7Days" type="text" maxlength="15" /></td>
                                <td style="display:none">
                                    <input id="HHHG_SW_07_7Days" type="text" maxlength="15" /></td>
                            </tr>

                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(29)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(29)" value="提交" />
                </div>
                <!--表单07.7天渤海海区及黄河海港风、浪预报-->

                <%--周报七、7天海洋水文气象预报综述--%>
                <div class="dlgs" id="ddlg_30" style="height: auto; padding: 10px;">
                    <div style="border: 1px solid #ddd; width: auto; line-height: 36px; font-weight: bold; text-align: center; background-color:#d2ffe7;">周报七、7天海洋水文气象预报综述</div>
                    <div style="border: 1px solid #ddd; background-color:#d2ffe7;">
                        海浪：<textarea name="txt_SWQX_ZS_7DAYS" style="margin: 10px 10px 10px 10px; width: 90%; height: 20px;font-size:18px; font-weight:500;" id="SWQX_ZS_7DAYS"></textarea>
                    </div>
                    <div style="border: 1px solid #ddd; background-color:#d2ffe7;">
                        潮汐：<textarea name="txt_CXQX_ZS_7DAYS" style="margin: 10px 10px 10px 10px; width: 90%; height: 20px;font-size:18px; font-weight:500;" id="CXQX_ZS_7DAYS"></textarea>
                    </div>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(30)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(30)" value="提交" />

                </div>
                <!--表单08.7天海洋水文气象预报综述-->

                <%--周报八、7天港口潮位预报--%>
                <div class="dlgs" id="ddlg_31" style="height: 430px; padding: 10px;">
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
                                <th class="head1" colspan="10">周报八、7天港口潮位预报</th>
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
                        <tbody class="week_report" style="text-align: center">
                            <tr>
                                <td rowspan="7">龙口港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_01_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_02_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_03_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_04">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_04_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_05">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_05_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_06">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_06_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_07">*月*号</td>
                                <td>
                                    <input id="LKG_01G_SJ_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01G_CW_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_SJ_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_01D_CW_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_SJ_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02G_CW_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_SJ_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="LKG_02D_CW_07_7Days" type="text" maxlength="20" /></td>
                            </tr>

                            <tr>
                                <td rowspan="7">黄河海港</td>
                                <td class="SJ_01">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_01_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_01_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_02">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_02_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_02_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_03">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_03_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_03_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_04">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_04_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_04_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_05">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_05_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_05_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_06">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_06_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_06_7Days" type="text" maxlength="20" /></td>
                            </tr>
                            <tr>
                                <td class="SJ_07">*月*号</td>
                                <td>
                                    <input id="HHHG_01G_SJ_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01G_CW_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_SJ_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_01D_CW_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_SJ_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02G_CW_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_SJ_07_7Days" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="HHHG_02D_CW_07_7Days" type="text" maxlength="20" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(31)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(31)" value="提交" />
                </div>
                <!--表单09.7天港口潮位预报-->

                <%--周报九、东营胜利油田专项海冰周报--%>
                <div class="dlgs" id="ddlg_24" style="height: 335px; padding: 10px;">
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
                                <th class="head0" colspan="7">周报九、东营胜利油田专项海冰周报</th>
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
                 <!--表单11.东营胜利油田专项海冰周报-->

                <%--周报十、东营胜利油田专项海温周报--%>
                <%--改 1129--%>
                <div class="dlgs" id="ddlg_32" style=" height: 335px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
                            <col class="con0" style="width:200px;" />
                            <col class="con1" style="width:300px;"/>
                        </colgroup>
                        <thead>
                            <tr>
                                <th class="head1" colspan="2">周报十、东营胜利油田专项海温周报</th>
                            </tr>
                            <tr>
                                <th class="head0">海域</th>
                                <th class="head1">表层水温（℃）</th>
                            </tr>
                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td style="display:none;">
                                    <input id="RZJH_LG" type="text" maxlength="80" hidden="hidden" value="0" />
                                    <input id="RZJH_BCSW" type="text" maxlength="20" hidden="hidden" value="0"/>
                                    <input id="QDJH_LG" type="text" maxlength="80" hidden="hidden" value="0"  />
                                    <input id="QDJH_BCSW" type="text" maxlength="20" hidden="hidden" value="0" />
                                    <input id="WHJH_LG" type="text" maxlength="80"  hidden="hidden" value="0" />
                                    <input id="YTJH_LG" type="text" maxlength="80"  hidden="hidden" value="0" />
                                    <input id="WFJH_LG" type="text" maxlength="80" hidden="hidden" value="0"  />
                                    <input id="DYJH_LG" type="text" maxlength="80" hidden="hidden" value="0"  />
                                    <input id="BZJH_LG" type="text" maxlength="80" hidden="hidden" value="0"  />
                                </td>
                            </tr>
                            <tr>
                                <td>威海近海</td>
                                <td>
                                    <input id="WHJH_BCSW" type="text" maxlength="20"/>
                                </td>
                            </tr>
                            <tr>
                                <td>烟台近海</td>
                                <td>
                                    <input id="YTJH_BCSW" type="text" maxlength="20" />
                                </td>
                            </tr>
                            <tr>
                                <td>潍坊近海</td>
                                    <td>
                                        <input id="WFJH_BCSW" type="text" maxlength="20"/>
                                    </td>
                            </tr>
                            <tr>
                                <td>东营近海</td>
                                <td>
                                    <input id="DYJH_BCSW" type="text" maxlength="20" />
                                </td>
                            </tr>
                            <tr>
                                <td>滨州近海</td>
                                <td>
                                    <input id="BZJH_BCSW" type="text" maxlength="20"/>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(32)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(32)" value="提交" />
                </div>
                <!--表单12.东营胜利油田专项海温周报-->

                <%-- 180719潮汐修改删除 edit by Yuy --%>
                 <!--div class="dlgs" id="ddlg_36" style="width: auto; height: 285px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />
                            <col class="con1" />

                        </colgroup>
                        <thead>
                            <tr style="text-align: center">
                                <th class="head0" colspan="4">周报十一、潍坊港24小时潮汐预报</th>
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
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(36)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(36)" value="提交" />
                </div-->
                <!--表单36.潍坊港24小时潮汐预报-->

                <!--新添加-->
                <%--周报十一、海区24小时海浪、水温预报--%>
                <div class="dlgs" id="ddlg_37" style="width: auto; height: 425px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable" style="text-align: center">
                        <colgroup>
                            <col class="con0" />
                            <col class="con1" />
                            <col class="con0" />


                        </colgroup>
                        <thead>
                            <tr style="text-align: center">

                                <th class="head0" colspan="6">周报十一、海区24小时海浪、水温预报</th>

                            </tr>

                            <tr style="text-align: center">
                                <th class="head0"></th>
                                <th class="head1">浪高(m)</th>

                                <th class="head1">水温</th>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>渤海</td>
                                <td>
                                    <input id="BH13_HL" type="text" /></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>黄海北部</td>
                                <td>
                                    <input id="HHBB_HL" type="text" /></td>
                                <td></td>

                            </tr>
                            <tr>
                                <td>黄海中部</td>
                                <td>
                                    <input id="HHZB_HL" type="text" /></td>
                                <td></td>

                            </tr>
                            <tr>
                                <td>黄海南部</td>
                                <td>
                                    <input id="HHNB_HL" type="text" /></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>潍坊近海</td>
                                <td>
                                    <input id="WFJH_HL" type="text" /></td>

                                <td>
                                    <input id="WFJH_SW" type="text" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(37)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(37)" value="提交" />
                </div>

                <%--周报十二、海阳海浪、水温预报--%>
                <div class="dlgs" id="ddlg_50">
                    <table style="border: 1px solid #ddd; width: 100%;">
                        <tr style="line-height: 45px;">
                            <td style="border-bottom: 1px solid #ddd; font-weight: bold; text-align: center" colspan="7">周报十二、海阳海浪、水温预报</td>
                        </tr>
                        <tr style="line-height: 45px; display: none;">
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td style="width:120px;">有</td>
                            <td><input maxlength="20" id="YT_GDF_01" onkeyup="if(isNaN(value))execCommand('undo')" onblur="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="width: 80%" type="text" />m</td>
                            <td>的<input maxlength="20" id="YT_HL_01" type="text" style="width: 80%" /></td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>风浪向： </td>
                            <td><input id="YT_LX_01" type="text" maxlength="80" style="width: 80%" /></td>
                        </tr>
                        <tr style="line-height: 45px">
                            <td>日平均水温：</td> 
                            <td><input id="YT_SW_01" type="text" maxlength="20" style="width: 80%" />℃</td>
                        </tr>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(50)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(50)" value="提交" />

                </div>
                <!--表单50.海阳海浪、水温预报-->

                <%--周报十三、海阳近岸海域潮汐预报--%>
                 <div class="dlgs" id="ddlg_51" style="margin: 80px 0px 10px 0px;width:auto;">
                    <table cellpadding="0" cellspacing="0" border="0" class="stdtable">
                        <colgroup>
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

                                <th class="head1" colspan="10">周报十三、海阳近岸海域潮汐预报</th>
                            </tr>

                            <tr>
                                <th class="head1" colspan="2">第一次高潮</th>
                                <th class="head1" colspan="2">第二次高潮</th>
                                <th class="head0" colspan="2">第一次低潮</th>
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
                        <tbody class="textStyle" style="text-align: center">
                            <tr>
                                <td>
                                    <input id="HY_01G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="HY_01G_CW_01" type="text" /></td>
                                <td>
                                    <input id="HY_02G_SJ_01" type="text" /></td>
                                <td>
                                    <input id="HY_02G_CW_01" type="text" /></td>
                                <td>
                                    <input id="HY_01D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="HY_01D_CW_01" type="text" /></td>
                                <td>
                                    <input id="HY_02D_SJ_01" type="text" /></td>
                                <td>
                                    <input id="HY_02D_CW_01" type="text" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(51)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(51)" value="提交" />
                </div>
                <!--表单51.海阳近岸海域潮汐预报-->

                <%-- 周报十四、海阳万米海滩海水浴场风、浪预报 --%>
                <div class="dlgs" id="ddlg_52"  style="margin: 10px 0px 10px 0px;width:auto;height: 200px;">
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
                            <tr>
                                <th class="head0" colspan="7">周报十四、海阳万米海滩海水浴场风、浪预报</th>
                            </tr>
                            <tr>
                               
                                <th class="head1">天气状况</th>
                                <th class="head0">气温（℃）</th>
                                <th class="head1">风速</th>
                                <th class="head0">风向</th>
                                <th class="head0">浪高（m）</th>
                                
                            </tr>
                        </thead>
                        <tbody style="text-align: center">
                            <tr>
                                <td>
                                    <input id="YC_TQ_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YC_QW_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YC_FS_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YC_FX_01" type="text" maxlength="20" /></td>
                                <td>
                                    <input id="YC_LG_01" type="text" maxlength="20" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlgclose(52)" value="取消" />
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="dlg_Submit(52)" value="提交" />
                </div>
                <!--表单52.海阳万米海滩海水浴场风、浪预报-->

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
                                 <select id="Hailang" class="uniformselect" style="width:100%;height:25px;">
                                     <option value="">请选择</option>
                                </select>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        潮汐预报员：<%--<input id="Chaoxi" type="text" style="width: 200px" />--%>
                                  <select id="Chaoxi" class="uniformselect" style="width:100%;height:25px;">
                                      <option value="">请选择</option>
                                 </select>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        水温预报员：<%--<input id="Shuiwen" type="text" style="width: 200px" />--%>
                                <select id="Shuiwen" class="uniformselect" style="width:100%;height:25px;">
                                    <option value="">请选择</option>
                                </select>
                     <br />
                    海浪预报员电话：<input id="HailangTel" type="text" style="width: 150px" />&nbsp;&nbsp;
                    潮汐预报员电话：<input id="ChaoxiTel" type="text" style="width: 150px" />&nbsp;&nbsp;
                    水温预报员电话：<input id="ShuiwenTel" type="text" style="width: 150px" />




                    &nbsp;&nbsp;
                    <input style="margin:3px 5px 0px 40px" type="button" class="stdbtn" onclick="dlg_Submit(23)" value="填报信息提交" />
                    <input type="hidden" id="iHailang" value=""/>
                    <input type="hidden" id="iChaoxi" value=""/>
                    <input type="hidden" id="iShuiwen" value=""/>
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
                <div id="dlg_xzmb" class="easyui-dialog" title="选择模版" data-options="iconCls:'icon-save'" style="width: 320px; height:350px; padding: 10px;">
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
                            <asp:CheckBox ID="CheckBox7" runat="server" Text="5号预报单（a4）" name="5号预报单（a4）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox8" runat="server" Text="6号预报单（a4）" name="6号预报单（a4）" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox9" runat="server" Text="7号海洋水温海冰预报" name="7号海洋水温海冰预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="20号潍坊市海洋预报台专项预报(10时)" name="20号潍坊市海洋预报台专项预报(10时)" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox25" runat="server" Text="24号东营专项预报" name="24号东营专项预报" />
                        </div>
                        <div>
                            <asp:CheckBox ID="CheckBox28" runat="server" Text="海上丝绸之路预报" name="海上丝绸之路预报" />
                        </div>
                         <div>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="海阳近岸专项预报单" name="海阳近岸专项预报单"/>
                        </div>
                    </div>
                    <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_xzmb').dialog('close'); $('#modellist div div span').removeClass('checked');" value="取消" />
                    <input id="Releasetable" style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" value="发布表单" />
                </div>
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
                <!--选择模版-->
                <script type="text/javascript" language="javascript">
                    function addtr() {

                        var tab = document.getElementById("addtable");
                        //表格行数
                        var rows = tab.rows.length + 2;

                        var i = 0;

                        i = (rows - 1) / 2

                        var tr = "<tr id =" + i + "><td rowspan='2'><input id='ZHC_XW_SA0" + i + "' type='text' maxlength='15' /></td>" +
                               "<td class='SJ_01'>*日</td>" +
                               "<td> <input id='ZHC_XW_SA0" + i + "_FX_01' type='text' maxlength='15' /></td>" +
                               "<td> <input id='ZHC_XW_SA0" + i + "_FL_01' type='text' maxlength='15' /></td>" +
                               "<td> <input id='ZHC_XW_SA0" + i + "_BG_01' type='text' maxlength='15' /></td>" +
                               "<td rowspan='2'><input class='button' id =" + i + " type='button' value='删除' onclick='deletr(this)'></td> </tr>" +
                               "<tr id=" + i + "><td class='SJ_02'>*日</td>" +
                               "<td> <input id='ZHC_XW_SA0" + i + "_FX_02' type='text' maxlength='15' /></td>" +
                               "<td> <input id='ZHC_XW_SA0" + i + "_FL_02' type='text' maxlength='15' /></td>" +
                               "<td> <input id='ZHC_XW_SA0" + i + "_BG_02' type='text' maxlength='15' /></td>  </tr>";

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

                <br />
            </div>

            <!--subcontent-->
        </div>


    </form>

    '<script>
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
        //直接点击发布按钮 （根据用户提供的信息，将预报单分类。点击此按钮根据分类直接生成上午预报单，特殊情况只有周一生成周报，其他时候不生成）
        //function check()
        //{
        // window.open("ftp://用户名:密码@ip地址:端口号");
        //}

        function All_Releasetable() {
          var d = new Date();
          var week= d.getDay();
          var strlist = "";
          if (week == 1) {
              strlist = "5号预报单（a4）,6号预报单（a4）,7号海洋水温海冰预报,20号潍坊市海洋预报台专项预报(10时),24号东营专项预报,海上丝绸之路预报,海阳近岸专项预报单";//周报
          }
          else {
              strlist = "";//生成文件中不含周报
              strlist = "5号预报单（a4）,6号预报单（a4）,7号海洋水温海冰预报,20号潍坊市海洋预报台专项预报(10时),24号东营专项预报,海上丝绸之路预报,海阳近岸专项预报单";//周报
          }

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
                var datas = { datas: strlist, dates: date, hours: phour,  makewordtime :makewordtime };
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
                    makewordtime :makewordtime

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
               
                case 3: submit_3(id); break;//表单3提交
                case 4: submit_4(id); break;//表单4提交
                case 24: submit_24(id); break;//表单24提交
                case 28: submit_28(id); break;//六、24小时水文气象预报综述
                case 29: submit_29(id); break;//七、7天渤海海区及黄河海港风、浪预报
                case 30: submit_30(id); break;//八、7天海洋水文气象预报综述
                case 31: submit_31(id); break;//九、7天港口潮位预报
                case 32: submit_32(id); break;//十二、东营胜利油田专项海温周报
                case 23: submit_23(23); break; //表单23提交
                case 39: submit_39(id); break;//上午七、海上丝绸之路三天海浪、气象预报
                case 38: submit_38(id); break;//上午八、海上丝绸之路三天潮汐预报
                case 37: submit_37(id); break; 
                //case 36: submit_36(id); break;//上午十一、潍坊港 20180718取消不用 edit by Yuy
                case 50: submit_50(id); break;
                case 51: submit_51(id); break;
                case 52: submit_52(id); break;
                default:
            }
        }

        //保存所有
        function alldlg_Submit() {//（无当天数据保存、有当天数据修改）↓
         
            submit_3(3); //表单3提交
            submit_4(4); //表单4提交
            submit_24(24); //表单24提交
            submit_28(28); //六、24小时水文气象预报综述
            submit_29(29); //七、7天渤海海区及黄河海港风、浪预报
            submit_30(30); //八、7天海洋水文气象预报综述
            submit_31(31); //九、7天港口潮位预报
            submit_32(32); //十二、东营胜利油田专项海温周报
            submit_23(23);
            submit_39(39);//上午七、海上丝绸之路三天海浪、气象预报
            submit_38(38);//上午八、海上丝绸之路三天潮汐预报
            //submit_36(36); //上午十一、潍坊港 20180718取消不用 edit by Yuy
            submit_37(37); 
            submit_50(50);
            submit_51(51);
            submit_52(52);
        }

        //表单数据拼接 从左至右 从上至下
        { //表单01
            function submit_1(id) {
                var str_data = "";
                var d = new Date();
                var dayCount = 3;
                //if (d.getDay() == 1)
                //    dayCount = 7;
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
                for (var i = 0; i < 6; i++) {
                    switch (i) {
                        case 0: nqyname = "FYT"; break;
                        case 1: nqyname = "GD"; break;
                        case 2: nqyname = "XDH"; break;
                        case 3: nqyname = "DYG"; break;
                        case 4: nqyname = "ZX"; break;
                        case 5: nqyname = "XH"; break;
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
                            break;
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
                str_data += $("#ZHC_MO_QD_BX_01").val() + ",";//青岛WAVEDIRECTION字段存水温
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

            //三、3天海洋水文气象预报综述
            function submit_27(id) {
                var str_data = $.trim($('#SWQX_ZS_3DAYS').val())+","+$.trim($('#CXQX_ZS_3DAYS').val());
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //六、24小时水文气象预报综述
            function submit_28(id) {
                var str_data = $.trim($('#SWQX_ZS_24HOURS').val())+","+$.trim($('#CXQX_ZS_24HOURS').val());
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //七、7天渤海海区及黄河海港风、浪预报
            function submit_29(id){
                var str_data = "";
                var d = new Date();
                var dayCount = 7;
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#BH_BG_0" + i + "_7Days").val() + ",";
                    str_data += $("#BH_BX_0" + i + "_7Days").val() + ",";
                    str_data += $("#BH_FX_0" + i + "_7Days").val() + ",";
                    str_data += $("#BH_FL_0" + i + "_7Days").val() + ",";
                    str_data += $("#BH_SW_0" + i + "_7Days").val() + ",";
                }
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#HHHG_BG_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_BX_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_FX_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_FL_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_SW_0" + i + "_7Days").val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //八、7天海洋水文气象预报综述
            function submit_30(id) {
                var str_data = $.trim($('#SWQX_ZS_7DAYS').val())+","+$.trim($('#CXQX_ZS_7DAYS').val());
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //九、7天港口潮位预报
            function submit_31(id) {
                var str_data = "";
                var d = new Date();
                dayCount = 7;
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#LKG_01G_SJ_0" + i + "_7Days").val() + ",";
                    str_data += $("#LKG_01G_CW_0" + i + "_7Days").val() + ",";
                    str_data += $("#LKG_01D_SJ_0" + i + "_7Days").val() + ",";
                    str_data += $("#LKG_01D_CW_0" + i + "_7Days").val() + ",";
                    str_data += $("#LKG_02G_SJ_0" + i + "_7Days").val() + ",";
                    str_data += $("#LKG_02G_CW_0" + i + "_7Days").val() + ",";
                    str_data += $("#LKG_02D_SJ_0" + i + "_7Days").val() + ",";
                    str_data += $("#LKG_02D_CW_0" + i + "_7Days").val() + ",";
                }
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#HHHG_01G_SJ_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_01G_CW_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_01D_SJ_0" + i + "_7Days").val() + ",";  
                    str_data += $("#HHHG_01D_CW_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_02G_SJ_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_02G_CW_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_02D_SJ_0" + i + "_7Days").val() + ",";
                    str_data += $("#HHHG_02D_CW_0" + i + "_7Days").val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }


           //十二、东营胜利油田专项海温周报
            function submit_32(id) {
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
                            break;
                    }
                    str_data += $("#" + nqyname + "_LG").val() + ",";
                    str_data += $("#" + nqyname + "_BCSW").val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
            //表单33
            function submit_33(id) {
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

            //表单34
            function submit_34(id) {
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
                //表单37
            function submit_37(id) {
                var str_data = "";
                str_data += $("#BH13_HL").val() + ",";
                str_data += $("#HHBB_HL").val() + ",";
                str_data += $("#HHZB_HL").val() + ",";
                str_data += $("#HHNB_HL").val() + ",";
                str_data += $("#WFJH_HL").val() + ",";
                str_data += $("#WFJH_SW").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }
             //表单16
            function submit_36(id) {
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
            //上午七、海上丝绸之路三天海浪、气象预报
            function submit_39(id) {
                var str_data = "";
                var d = new Date();
                var dayCount = 3;
                //if (d.getDay() == 1)
                //    dayCount = 7;
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#SILK_QD_BG_0" + i).val() + ",";
                    str_data += $("#SILK_QD_BX_0" + i).val() + ",";
                    str_data += $("#SILK_QD_FX_0" + i).val() + ",";
                    str_data += $("#SILK_QD_FL_0" + i).val() + ",";
                }
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#SILK_WF_BG_0" + i).val() + ",";
                    str_data += $("#SILK_WF_BX_0" + i).val() + ",";
                    str_data += $("#SILK_WF_FX_0" + i).val() + ",";
                    str_data += $("#SILK_WF_FL_0" + i).val() + ",";
                }
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#SILK_YK_BG_0" + i).val() + ",";
                    str_data += $("#SILK_YK_BX_0" + i).val() + ",";
                    str_data += $("#SILK_YK_FX_0" + i).val() + ",";
                    str_data += $("#SILK_YK_FL_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //上午八、海上丝绸之路三天潮汐预报
            function submit_38(id) {
                var str_data = "";
                var d = new Date();
                var dayCount = 3;
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#QDG_01G_SJ_0" + i).val() + ",";
                    str_data += $("#QDG_01G_CW_0" + i).val() + ",";
                    str_data += $("#QDG_01D_SJ_0" + i).val() + ",";
                    str_data += $("#QDG_01D_CW_0" + i).val() + ",";
                    str_data += $("#QDG_02G_SJ_0" + i).val() + ",";
                    str_data += $("#QDG_02G_CW_0" + i).val() + ",";
                    str_data += $("#QDG_02D_SJ_0" + i).val() + ",";
                    str_data += $("#QDG_02D_CW_0" + i).val() + ",";
                }
                for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#WFG_01G_SJ_0" + i).val() + ",";
                    str_data += $("#WFG_01G_CW_0" + i).val() + ",";
                    str_data += $("#WFG_01D_SJ_0" + i).val() + ",";  
                    str_data += $("#WFG_01D_CW_0" + i).val() + ",";
                    str_data += $("#WFG_02G_SJ_0" + i).val() + ",";
                    str_data += $("#WFG_02G_CW_0" + i).val() + ",";
                    str_data += $("#WFG_02D_SJ_0" + i).val() + ",";
                    str_data += $("#WFG_02D_CW_0" + i).val() + ",";
                }
                 for (var i = 1; i <= dayCount; i++) {
                    str_data += $("#YKG_01G_SJ_0" + i).val() + ",";
                    str_data += $("#YKG_01G_CW_0" + i).val() + ",";
                    str_data += $("#YKG_01D_SJ_0" + i).val() + ",";  
                    str_data += $("#YKG_01D_CW_0" + i).val() + ",";
                    str_data += $("#YKG_02G_SJ_0" + i).val() + ",";
                    str_data += $("#YKG_02G_CW_0" + i).val() + ",";
                    str_data += $("#YKG_02D_SJ_0" + i).val() + ",";
                    str_data += $("#YKG_02D_CW_0" + i).val() + ",";
                }
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }


            //上午八、海上丝绸之路三天潮汐预报
            function submit_50(id) {
                var str_data = "";
                str_data += $("#YT_GDF_01").val() + ",";
                str_data += $("#YT_HL_01").val() + ",";
                str_data += $("#YT_LX_01").val() + ",";
                str_data += $("#YT_SW_01").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //上午八、海上丝绸之路三天潮汐预报
            function submit_51(id) {
                var str_data = "";
                str_data += $("#HY_01G_SJ_01").val() + ",";
                str_data += $("#HY_01G_CW_01").val() + ",";
                str_data += $("#HY_02G_SJ_01").val() + ",";
                str_data += $("#HY_02G_CW_01").val() + ",";
                str_data += $("#HY_01D_SJ_01").val() + ",";
                str_data += $("#HY_01D_CW_01").val() + ",";
                str_data += $("#HY_02D_SJ_01").val() + ",";
                str_data += $("#HY_02D_CW_01").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
                submit_ajax(id, data);
            }

            //上午八、海上丝绸之路三天潮汐预报
            function submit_52(id) {
                var str_data = "";
                var d = new Date();
                var dayCount = 3;
                str_data += $("#YC_TQ_01").val() + ",";
                str_data += $("#YC_QW_01").val() + ",";
                str_data += $("#YC_FS_01").val() + ",";
                str_data += $("#YC_FX_01").val() + ",";
                str_data += $("#YC_LG_01").val() + ",";
                str_data = str_data.substring(0, str_data.length - 1);
                var data = { datas: str_data };
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
                }else{
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
            $("#select_hour").val(10);
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
                type: "GET",
                url: "/Ajax/gettablelist.ashx?method=getweekdata&date=" + dates + "&searchtype=" + searchType+"&t="+Math.random(),
                 cache:false,
                beforeSend: function () {
                    $('#w').window('open');
                    $("#btn_select").attr({ disabled: "disabled" });
                     getPredicTideData(dates);
                },
                success: function (result) {
                    var resjson = JSON.parse(result);
                    for (var j = 0; j < resjson.length; j++) {
                        switch (resjson[j].type) {
                            case "t1": gettable01(resjson[j].children, date1); dlgclose("1"); break;
                            case "t2": gettable02(resjson[j].children, date1); dlgclose("2"); break;
                            case "t3": gettable03(resjson[j], date1); dlgclose("3"); break;
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
                            //3天海洋水文气象预报综述
                            case "t27": gettable27(resjson[j].children, date1); dlgclose("27"); break;
                            //24小时水文气象预报综述
                            case "t28": gettable28(resjson[j].children, date1); dlgclose("28"); break;
                            case "t29": gettable29(resjson[j], date1); dlgclose("29"); break;
                            //据获取7天海洋水文气象预报综述
                            case "t30": gettable30(resjson[j].children, date1); dlgclose("30"); break;
                            case "t31": gettable31(resjson[j].children, date1); dlgclose("31"); break;
                                 //胜利油田海温周报
                            case "t32": gettable32(resjson[j].children, date1); dlgclose("32"); break;
                            //case "t33": gettable33(resjson[j].children, date1); dlgclose("33"); break;
                            //case "t34": gettable34(resjson[j].children, date1); dlgclose("34"); break;
                            //case "t36": gettable36(resjson[j].children, date1); dlgclose("36"); break;//潍坊潮汐预报取消不用10719
                            case "t37": gettable37(resjson[j], date1); dlgclose("37"); break;
                            case "t39": gettable39(resjson[j], date1); dlgclose("39"); break;
                            case "t38": gettable38(resjson[j].children, date1); dlgclose("38"); break;
                            case "t50": gettable50(resjson[j], date1); dlgclose("50"); break;
                            case "t51": gettable51(resjson[j].children, date1); dlgclose("51"); break;
                            case "t52": gettable52(resjson[j], date1); dlgclose("52"); break;
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
            function gettable02(resjson, date1) {
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
            function gettable03(result, date1) {
                var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var windwaveForecast = result.windwave;
                    for(var j = 0;j<windwaveForecast.length;j++){
                        if (windwaveForecast[j].FORECASTAREA == "渤海") {
                            $("#BH_GDE_01").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "黄海北部") {
                           $("#HHBB_GDE_01").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "刁口近海") {
                           $("#DKHY_LG_01").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "黄河口海域") {
                            $("#HHK_LG_01").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "广利港海域") {
                            $("#GDG_LG_01").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "东营港海域") {
                            $("#DYG_LG_01").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "新户近海") {
                            $("#XH_LG_01").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "埕岛近海") {
                            $("#CK_LG_01").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                    }

                     var sw = result.sw;
                    for(var k = 0;k<sw.length;k++){
                      if (sw[k].NAME == "刁口") {
                            $("#DKHY_SW_01").val(sw[k].MEAN_24H);
                        }
                        else if (sw[k].NAME == "黄河口") {
                            $("#HHK_SW_01").val(sw[k].MEAN_24H);
                        }
                        else if (sw[k].NAME == "广利（羊口）") {
                            $("#GDG_SW_01").val(sw[k].MEAN_24H);
                        }
                        else if (sw[k].NAME == "东营") {
                            $("#DYG_SW_01").val(sw[k].MEAN_24H);
                        }
                        else if (sw[k].NAME == "新户") {
                            $("#XH_SW_01").val(sw[k].MEAN_24H);
                        }
                        else if (sw[k].NAME == "埕口海洋站") {
                            $("#CK_SW_01").val(sw[k].MEAN_24H);
                        }
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
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
                
            }

            //表单04数据
            function gettable04(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    switch (resjson[i].qy) {
                        case "飞雁滩": nqyname = "FYT"; break;
                        case "孤东": nqyname = "GD"; break;
                        case "小岛河": nqyname = "XDH"; break;
                        case "东营港": nqyname = "DYG"; break;
                        case "桩西": nqyname = "ZX"; break;
                        case "新户": nqyname = "XH"; break;
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
                    //var ptime = myformatter1(new Date(resjson[i].rq)) + resjson[i].xs + "时";
                    //$("#Fabushijian").val(ptime);
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
                        $("#uniform-Shuiwen span").attr("code",resjson[i].sw);
                        $("#iShuiwen").val(resjson[i].sw);
                        $("#ShuiwenTel").val(resjson[i].swtel);
                    }
                    if (resjson[i].hl != "" && resjson[i].hl != null) {
                     $("#uniform-Hailang span").text(resjson[i].hl);
                     $("#uniform-Hailang span").attr("code",resjson[i].hl);
                     $("#iHailang").val(resjson[i].hl);
                     $("#HailangTel").val(resjson[i].hltel);
                    }
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
           if (resjson.length > 0) {
                //var publishDate = resjson[0].PUBLISHTIME;
                //var mydate = new Date();
                //var t=mydate.getFullYear() + "/" + (mydate.getMonth()+1) + "/" + mydate.getDate() + " 7:00:00";
                //var pdate = new Date(t);
                //pdate.setHours(0);
               // alert(pdate);
                for (var i = 0; i < resjson.length; i++) {
                    var seaArea = resjson[i].SEAAREA;
                    var publishDate = resjson[i].PUBLISHTIME;
                    var forecastDate = resjson[i].FORECASTTIME;
                 //   alert(forecastDate);

                     var fdate = new Date(Date.parse(forecastDate.replace(/-/g,"/"))).getTime();
                    var pdate = new Date(Date.parse(publishDate.replace(/-/g,"/"))).getTime();
                  //  alert(fdate);
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
                        var SWInputId = "#ZHC_MO_QD_BX_01";
                        
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
       
      
        // 获取3天海洋水文气象预报综述
        function gettable27(resjson, date1) {
                var SWQX_ZS_3DAYS = resjson[0].meteorologicalreview;
                var SWQX_ZS_3DAYSCX = resjson[0].meteorologicalreviewcx;
                $('#SWQX_ZS_3DAYS').val(SWQX_ZS_3DAYS);
                $('#CXQX_ZS_3DAYS').val(SWQX_ZS_3DAYSCX);
        }
        //24小时水文气象预报综述
        function gettable28(resjson, date1) {
                var SWQX_ZS_24HOURS = resjson[0].meteorologicalreview24hour;
               var SWQX_ZS_24HOURSCX = resjson[0].meteorologicalreview24hourcx;
                $('#SWQX_ZS_24HOURS').val(SWQX_ZS_24HOURS);
                $('#CXQX_ZS_24HOURS').val(SWQX_ZS_24HOURSCX);
        }
        //表单29数据7天渤海海区及黄河海港风、浪预报
        function gettable29(result, date1) {
                var pbtypes = result.pbtype;
                if (pbtypes == "bys") { 
                    var dtWindWave = result.dtWindWave;
                    for (var i = 0; i < dtWindWave.length; i++) {
                        if (dtWindWave[i].FORECASTAREA == "渤海") {
                            $("#BH_BG_01_7Days").val(dtWindWave[i].WAVE24FORECAST);
                            $("#BH_BX_01_7Days").val(dtWindWave[i].WINDDIRECTION24FORECAST);
                            $("#BH_FX_01_7Days").val(dtWindWave[i].WINDDIRECTION24FORECAST);
                            $("#BH_FL_01_7Days").val(dtWindWave[i].WINDFORCE24FORECAST);

                            $("#BH_BG_02_7Days").val(dtWindWave[i].WAVE48FORECAST);
                            $("#BH_BX_02_7Days").val(dtWindWave[i].WINDDIRECTION48FORECAST);
                            $("#BH_FX_02_7Days").val(dtWindWave[i].WINDDIRECTION48FORECAST);
                            $("#BH_FL_02_7Days").val(dtWindWave[i].WINDFORCE48FORECAST);

                            $("#BH_BG_03_7Days").val(dtWindWave[i].WAVE72FORECAST);
                            $("#BH_BX_03_7Days").val(dtWindWave[i].WINDDIRECTION72FORECAST);
                            $("#BH_FX_03_7Days").val(dtWindWave[i].WINDDIRECTION72FORECAST);
                            $("#BH_FL_03_7Days").val(dtWindWave[i].WINDFORCE72FORECAST);

                            $("#BH_BG_04_7Days").val(dtWindWave[i].WAVE96FORECAST);
                            $("#BH_BX_04_7Days").val(dtWindWave[i].WINDDIRECTION96FORECAST);
                            $("#BH_FX_04_7Days").val(dtWindWave[i].WINDDIRECTION96FORECAST);
                            $("#BH_FL_04_7Days").val(dtWindWave[i].WINDFORCE96FORECAST);

                            $("#BH_BG_05_7Days").val(dtWindWave[i].WAVE120FORECAST);
                            $("#BH_BX_05_7Days").val(dtWindWave[i].WINDDIRECTION120FORECAST);
                            $("#BH_FX_05_7Days").val(dtWindWave[i].WINDDIRECTION120FORECAST);
                            $("#BH_FL_05_7Days").val(dtWindWave[i].WINDFORCE120FORECAST);

                            $("#BH_BG_06_7Days").val(dtWindWave[i].WAVE144FORECAST);
                            $("#BH_BX_06_7Days").val(dtWindWave[i].WINDDIRECTION144FORECAST);
                            $("#BH_FX_06_7Days").val(dtWindWave[i].WINDDIRECTION144FORECAST);
                            $("#BH_FL_06_7Days").val(dtWindWave[i].WINDFORCE144FORECAST);

                            $("#BH_BG_07_7Days").val(dtWindWave[i].WAVE168FORECAST);
                            $("#BH_BX_07_7Days").val(dtWindWave[i].WINDDIRECTION168FORECAST);
                            $("#BH_FX_07_7Days").val(dtWindWave[i].WINDDIRECTION168FORECAST);
                            $("#BH_FL_07_7Days").val(dtWindWave[i].WINDFORCE168FORECAST);
                        }
                        else if (dtWindWave[i].FORECASTAREA == "黄河海港") {
                            $("#HHHG_BG_01_7Days").val(dtWindWave[i].WAVE24FORECAST);
                            $("#HHHG_BX_01_7Days").val(dtWindWave[i].WINDDIRECTION24FORECAST);
                            $("#HHHG_FX_01_7Days").val(dtWindWave[i].WINDDIRECTION24FORECAST);
                            $("#HHHG_FL_01_7Days").val(dtWindWave[i].WINDFORCE24FORECAST);

                            $("#HHHG_BG_02_7Days").val(dtWindWave[i].WAVE48FORECAST);
                            $("#HHHG_BX_02_7Days").val(dtWindWave[i].WINDDIRECTION48FORECAST);
                            $("#HHHG_FX_02_7Days").val(dtWindWave[i].WINDDIRECTION48FORECAST);
                            $("#HHHG_FL_02_7Days").val(dtWindWave[i].WINDFORCE48FORECAST);

                            $("#HHHG_BG_03_7Days").val(dtWindWave[i].WAVE72FORECAST);
                            $("#HHHG_BX_03_7Days").val(dtWindWave[i].WINDDIRECTION72FORECAST);
                            $("#HHHG_FX_03_7Days").val(dtWindWave[i].WINDDIRECTION72FORECAST);
                            $("#HHHG_FL_03_7Days").val(dtWindWave[i].WINDFORCE72FORECAST);

                            $("#HHHG_BG_04_7Days").val(dtWindWave[i].WAVE96FORECAST);
                            $("#HHHG_BX_04_7Days").val(dtWindWave[i].WINDDIRECTION96FORECAST);
                            $("#HHHG_FX_04_7Days").val(dtWindWave[i].WINDDIRECTION96FORECAST);
                            $("#HHHG_FL_04_7Days").val(dtWindWave[i].WINDFORCE96FORECAST);

                            $("#HHHG_BG_05_7Days").val(dtWindWave[i].WAVE120FORECAST);
                            $("#HHHG_BX_05_7Days").val(dtWindWave[i].WINDDIRECTION120FORECAST);
                            $("#HHHG_FX_05_7Days").val(dtWindWave[i].WINDDIRECTION120FORECAST);
                            $("#HHHG_FL_05_7Days").val(dtWindWave[i].WINDFORCE120FORECAST);

                            $("#HHHG_BG_06_7Days").val(dtWindWave[i].WAVE144FORECAST);
                            $("#HHHG_BX_06_7Days").val(dtWindWave[i].WINDDIRECTION144FORECAST);
                            $("#HHHG_FX_06_7Days").val(dtWindWave[i].WINDDIRECTION144FORECAST);
                            $("#HHHG_FL_06_7Days").val(dtWindWave[i].WINDFORCE144FORECAST);

                            $("#HHHG_BG_07_7Days").val(dtWindWave[i].WAVE168FORECAST);
                            $("#HHHG_BX_07_7Days").val(dtWindWave[i].WINDDIRECTION168FORECAST);
                            $("#HHHG_FX_07_7Days").val(dtWindWave[i].WINDDIRECTION168FORECAST);
                            $("#HHHG_FL_07_7Days").val(dtWindWave[i].WINDFORCE168FORECAST);
                        }
                    }
                }
                else if(pbtypes == "bydb"){
                    var resjson=result.children;
                    var labelNum_BH = 0;
                    var labelNum_HH = 0;
                    var num = 0;
                    for (var i = 0; i < resjson.length; i++) {
                        if (resjson[i].qy == "渤海") {
                            date = new Date(resjson[i].yb);
                            //  date.getDate() - date1.getDate();
                            //num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                            //  return intDays;
                            labelNum_BH++;
                            num = labelNum_BH;
                            $("#BH_BG_0" + num + "_7Days").val(resjson[i].bg);
                            $("#BH_BX_0" + num + "_7Days").val(resjson[i].bx);
                            $("#BH_FX_0" + num + "_7Days").val(resjson[i].fx);
                            $("#BH_FL_0" + num + "_7Days").val(resjson[i].fl);
                            $("#BH_SW_0" + num + "_7Days").val(resjson[i].sw);
                            //$("#BH_BG_01").val(resjson[i].bg);
                            //$("#BH_BX_01").val(resjson[i].bx);
                            //$("#BH_FX_01").val(resjson[i].fx);
                            //$("#BH_FL_01").val(resjson[i].fl);
                            //$("#BH_SW_01").val(resjson[i].sw);

                        } else if (resjson[i].qy == "黄河海港") {
                            date = new Date(resjson[i].yb);
                            //num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                            labelNum_HH++;
                            num = labelNum_HH;
                            $("#HHHG_BG_0" + num + "_7Days").val(resjson[i].bg);
                            $("#HHHG_BX_0" + num + "_7Days").val(resjson[i].bx);
                            $("#HHHG_FX_0" + num + "_7Days").val(resjson[i].fx);
                            $("#HHHG_FL_0" + num + "_7Days").val(resjson[i].fl);
                            $("#HHHG_SW_0" + num + "_7Days").val(resjson[i].sw);
                        }
                    }
                }
            }

        //表单30数据获取7天海洋水文气象预报综述
        function gettable30(resjson, date1) {
            var SWQX_ZS_7DAYS = resjson[0].meteorologicalreview7Days;
             var CXQX_ZS_7DAYS = resjson[0].meteorologicalreview7Dayscx;
            $('#SWQX_ZS_7DAYS').val(SWQX_ZS_7DAYS);
            $('#CXQX_ZS_7DAYS').val(CXQX_ZS_7DAYS);
        }
        //表单31数据7天港口潮位预报
        function gettable31(resjson, date1) {
                var labelNum_LKG = 0;
                var labelNum_HHHG = 0;
                var num = 0;
                for (var i = 0; i < resjson.length; i++) {
                    if (resjson[i].qy == "龙口港") {

                        //date = new Date(resjson[i].yb);
                        //   num = date.getDate() - date1.getDate();
                        //num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        labelNum_LKG++;
                        num = labelNum_LKG;
                        $("#LKG_01G_CW_0" + num + "_7Days").val(resjson[i].g1c);
                        $("#LKG_01G_SJ_0" + num + "_7Days").val(resjson[i].g1s);
                        $("#LKG_01D_CW_0" + num + "_7Days").val(resjson[i].d1c);
                        $("#LKG_01D_SJ_0" + num + "_7Days").val(resjson[i].d1s);
                        $("#LKG_02G_CW_0" + num + "_7Days").val(resjson[i].g2c);
                        $("#LKG_02G_SJ_0" + num + "_7Days").val(resjson[i].g2s);
                        $("#LKG_02D_CW_0" + num + "_7Days").val(resjson[i].d2c);
                        $("#LKG_02D_SJ_0" + num + "_7Days").val(resjson[i].d2s);
                    } else if (resjson[i].qy == "黄河海港") {
                        labelNum_HHHG++;
                        num = labelNum_HHHG;
                        //date = new Date(resjson[i].yb);
                        //num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        $("#HHHG_01G_CW_0" + num + "_7Days").val(resjson[i].g1c);
                        $("#HHHG_01G_SJ_0" + num + "_7Days").val(resjson[i].g1s);
                        $("#HHHG_01D_CW_0" + num + "_7Days").val(resjson[i].d1c);
                        $("#HHHG_01D_SJ_0" + num + "_7Days").val(resjson[i].d1s);
                        $("#HHHG_02G_CW_0" + num + "_7Days").val(resjson[i].g2c);
                        $("#HHHG_02G_SJ_0" + num + "_7Days").val(resjson[i].g2s);
                        $("#HHHG_02D_CW_0" + num + "_7Days").val(resjson[i].d2c);
                        $("#HHHG_02D_SJ_0" + num + "_7Days").val(resjson[i].d2s);
                    }
                }
            }

        function gettable32(resjson, date1) {
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
         //表单33数据
        function gettable33(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {

                    date = new Date(resjson[i].yb);
                    num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                    $("#BG_0" + num).val(resjson[i].bg);
                    $("#BX_0" + num).val(resjson[i].bx);
                    $("#FX_0" + num).val(resjson[i].fx);
                    $("#FL_0" + num).val(resjson[i].fl);

                }
            }

            //表单34数据
        function gettable34(resjson, date1) {
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
         //表单16数据
        function gettable36(resjson, date1) {
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
         //表单37数据
        function gettable37(result, date1) {
                var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var windwaveForecast = result.windwave;
                    for(var j = 0;j<windwaveForecast.length;j++){
                        if (windwaveForecast[j].FORECASTAREA == "渤海") {
                            $("#BH13_HL").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "黄海北部") {
                           $("#HHBB_HL").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "黄海中部") {
                           $("#HHZB_HL").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "黄海南部") {
                            $("#HHNB_HL").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "潍坊近海") {
                            $("#WFJH_HL").val(windwaveForecast[j].WAVE24FORECAST);
                        }
                    }

                    var sw = result.sw;
                    for(var k = 0;k<sw.length;k++){
                            $("#WFJH_SW").val(sw[k].MEAN_24H);
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
                    for (var i = 0; i < resjson.length; i++) {
                        $("#BH13_HL").val(resjson[i].hl1);
                        $("#HHBB_HL").val(resjson[i].hl2);
                        $("#HHZB_HL").val(resjson[i].hl3);
                        $("#HHNB_HL").val(resjson[i].hl4);
                        $("#WFJH_HL").val(resjson[i].hl5);
                        $("#WFJH_SW").val(resjson[i].sw);
                    }
                }
            }
        //上午七、海上丝绸之路三天海浪、气象预报
        function gettable39(result, date1) {
             var pbtypes = result.pbtype;
                if (pbtypes == "bys") {
                    var windwaveForecast = result.windwave;
                    for(var j = 0;j<windwaveForecast.length;j++){
                        if (windwaveForecast[j].FORECASTAREA == "青岛近海") {
                            $("#SILK_QD_BG_01").val(windwaveForecast[j].WAVE24FORECAST);
                            $("#SILK_QD_BX_01").val(windwaveForecast[j].WINDDIRECTION24FORECAST);
                            $("#SILK_QD_FX_01").val(windwaveForecast[j].WINDDIRECTION24FORECAST);
                            $("#SILK_QD_FL_01").val(windwaveForecast[j].WINDFORCE24FORECAST);

                            $("#SILK_QD_BG_02").val(windwaveForecast[j].WAVE48FORECAST);
                            $("#SILK_QD_BX_02").val(windwaveForecast[j].WINDDIRECTION48FORECAST);
                            $("#SILK_QD_FX_02").val(windwaveForecast[j].WINDDIRECTION48FORECAST);
                            $("#SILK_QD_FL_02").val(windwaveForecast[j].WINDFORCE48FORECAST);

                            $("#SILK_QD_BG_03").val(windwaveForecast[j].WAVE72FORECAST);
                            $("#SILK_QD_BX_03").val(windwaveForecast[j].WINDDIRECTION72FORECAST);
                            $("#SILK_QD_FX_03").val(windwaveForecast[j].WINDDIRECTION72FORECAST);
                            $("#SILK_QD_FL_03").val(windwaveForecast[j].WINDFORCE72FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "潍坊近海") {
                            $("#SILK_WF_BG_01").val(windwaveForecast[j].WAVE24FORECAST);
                            $("#SILK_WF_BX_01").val(windwaveForecast[j].WINDDIRECTION24FORECAST);
                            $("#SILK_WF_FX_01").val(windwaveForecast[j].WINDDIRECTION24FORECAST);
                            $("#SILK_WF_FL_01").val(windwaveForecast[j].WINDFORCE24FORECAST);

                            $("#SILK_WF_BG_02").val(windwaveForecast[j].WAVE48FORECAST);
                            $("#SILK_WF_BX_02").val(windwaveForecast[j].WINDDIRECTION48FORECAST);
                            $("#SILK_WF_FX_02").val(windwaveForecast[j].WINDDIRECTION48FORECAST);
                            $("#SILK_WF_FL_02").val(windwaveForecast[j].WINDFORCE48FORECAST);

                            $("#SILK_WF_BG_03").val(windwaveForecast[j].WAVE72FORECAST);
                            $("#SILK_WF_BX_03").val(windwaveForecast[j].WINDDIRECTION72FORECAST);
                            $("#SILK_WF_FX_03").val(windwaveForecast[j].WINDDIRECTION72FORECAST);
                            $("#SILK_WF_FL_03").val(windwaveForecast[j].WINDFORCE72FORECAST);
                        }
                        else if (windwaveForecast[j].FORECASTAREA == "营口港") {
                            $("#SILK_YK_BG_01").val(windwaveForecast[j].WAVE24FORECAST);
                            $("#SILK_YK_BX_01").val(windwaveForecast[j].WINDDIRECTION24FORECAST);
                            $("#SILK_YK_FX_01").val(windwaveForecast[j].WINDDIRECTION24FORECAST);
                            $("#SILK_YK_FL_01").val(windwaveForecast[j].WINDFORCE24FORECAST);

                            $("#SILK_YK_BG_02").val(windwaveForecast[j].WAVE48FORECAST);
                            $("#SILK_YK_BX_02").val(windwaveForecast[j].WINDDIRECTION48FORECAST);
                            $("#SILK_YK_FX_02").val(windwaveForecast[j].WINDDIRECTION48FORECAST);
                            $("#SILK_YK_FL_02").val(windwaveForecast[j].WINDFORCE48FORECAST);

                            $("#SILK_YK_BG_03").val(windwaveForecast[j].WAVE72FORECAST);
                            $("#SILK_YK_BX_03").val(windwaveForecast[j].WINDDIRECTION72FORECAST);
                            $("#SILK_YK_FX_03").val(windwaveForecast[j].WINDDIRECTION72FORECAST);
                            $("#SILK_YK_FL_03").val(windwaveForecast[j].WINDFORCE72FORECAST);
                        }
                    }
                }
                else if (pbtypes == "bydb") {
                    var resjson=result.children;
                    for (var i = 0; i < resjson.length; i++) {
                        if (resjson[i].qy == "青岛港") {
                            date = new Date(resjson[i].yb);
                            //  date.getDate() - date1.getDate();
                            num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                            $("#SILK_QD_BG_0" + num).val(resjson[i].bg);
                            $("#SILK_QD_BX_0" + num).val(resjson[i].bx);
                            $("#SILK_QD_FX_0" + num).val(resjson[i].fx);
                            $("#SILK_QD_FL_0" + num).val(resjson[i].fl);
                        } else if (resjson[i].qy == "潍坊港") {
                            date = new Date(resjson[i].yb);
                            num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                            $("#SILK_WF_BG_0" + num).val(resjson[i].bg);
                            $("#SILK_WF_BX_0" + num).val(resjson[i].bx);
                            $("#SILK_WF_FX_0" + num).val(resjson[i].fx);
                            $("#SILK_WF_FL_0" + num).val(resjson[i].fl);
                        }
                        else if (resjson[i].qy == "营口港") {
                            date = new Date(resjson[i].yb);
                            num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                            $("#SILK_YK_BG_0" + num).val(resjson[i].bg);
                            $("#SILK_YK_BX_0" + num).val(resjson[i].bx);
                            $("#SILK_YK_FX_0" + num).val(resjson[i].fx);
                            $("#SILK_YK_FL_0" + num).val(resjson[i].fl);
                        }
                    }
                }
            }

            //上午八、海上丝绸之路三天潮汐预报
        function gettable38(resjson, date1) {
                for (var i = 0; i < resjson.length; i++) {
                    if (resjson[i].qy == "青岛港") {

                        date = new Date(resjson[i].yb);
                        //   num = date.getDate() - date1.getDate();
                        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        $("#QDG_01G_CW_0" + num).val(resjson[i].g1c);
                        $("#QDG_01G_SJ_0" + num).val(resjson[i].g1s);
                        $("#QDG_01D_CW_0" + num).val(resjson[i].d1c);
                        $("#QDG_01D_SJ_0" + num).val(resjson[i].d1s);
                        $("#QDG_02G_CW_0" + num).val(resjson[i].g2c);
                        $("#QDG_02G_SJ_0" + num).val(resjson[i].g2s);
                        $("#QDG_02D_CW_0" + num).val(resjson[i].d2c);
                        $("#QDG_02D_SJ_0" + num).val(resjson[i].d2s);
                    } else if (resjson[i].qy == "潍坊港") {
                        date = new Date(resjson[i].yb);
                        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        $("#WFG_01G_CW_0" + num).val(resjson[i].g1c);
                        $("#WFG_01G_SJ_0" + num).val(resjson[i].g1s);
                        $("#WFG_01D_CW_0" + num).val(resjson[i].d1c);
                        $("#WFG_01D_SJ_0" + num).val(resjson[i].d1s);
                        $("#WFG_02G_CW_0" + num).val(resjson[i].g2c);
                        $("#WFG_02G_SJ_0" + num).val(resjson[i].g2s);
                        $("#WFG_02D_CW_0" + num).val(resjson[i].d2c);
                        $("#WFG_02D_SJ_0" + num).val(resjson[i].d2s);
                    }
                    else if (resjson[i].qy == "营口港") {
                        date = new Date(resjson[i].yb);
                        num = parseInt(Math.abs(date - date1) / 1000 / 60 / 60 / 24) + 1;
                        $("#YKG_01G_CW_0" + num).val(resjson[i].g1c);
                        $("#YKG_01G_SJ_0" + num).val(resjson[i].g1s);
                        $("#YKG_01D_CW_0" + num).val(resjson[i].d1c);
                        $("#YKG_01D_SJ_0" + num).val(resjson[i].d1s);
                        $("#YKG_02G_CW_0" + num).val(resjson[i].g2c);
                        $("#YKG_02G_SJ_0" + num).val(resjson[i].g2s);
                        $("#YKG_02D_CW_0" + num).val(resjson[i].d2c);
                        $("#YKG_02D_SJ_0" + num).val(resjson[i].d2s);
                    }
                }
            }

        //上午十一、烟台南部海浪、水温预报
        function gettable50(result, date1) {
            var pbtypes = result.pbtype;
            var resjson=result.children;
            if (pbtypes == "bys") {
                $("#YT_GDF_01").val(resjson[0]["WAVE24FORECAST"]);
                $("#YT_HL_01").val(resjson[0]["WAVELEVELTYPE"]);
                $("#YT_LX_01").val(resjson[0]["WINDDIRECTION24FORECAST"]);
                $("#YT_SW_01").val(resjson[0]["WATERTEMPERATURE"]);
            }
            else if (pbtypes == "bydb") {
                $("#YT_GDF_01").val(resjson[0]["WAVELEVELONE"]);
                $("#YT_HL_01").val(resjson[0]["WAVELEVELTYPE"]);
                $("#YT_LX_01").val(resjson[0]["WAVEDIRECTION"]);
                $("#YT_SW_01").val(resjson[0]["WATERTEMPERATURE"]);
            }
        }

        //上午十二、海阳近岸海域潮汐预报
        function gettable51(resjson, date1) {
                $("#HY_01G_SJ_01").val(resjson[0]["FIRSTHIGHTIME"]);
                $("#HY_01G_CW_01").val(resjson[0]["FIRSTHIGHLEVEL"]);
                $("#HY_02G_SJ_01").val(resjson[0]["SECONDHIGHTIME"]);
                $("#HY_02G_CW_01").val(resjson[0]["SECONDHIGHLEVEL"]);
                $("#HY_01D_SJ_01").val(resjson[0]["FIRSTLOWTIME"]);
                $("#HY_01D_CW_01").val(resjson[0]["FIRSTLOWLEVEL"]);
                $("#HY_02D_SJ_01").val(resjson[0]["SECONDLOWTIME"]);
                $("#HY_02D_CW_01").val(resjson[0]["SECONDLOWLEVEL"]);
        }
        //上午十三、海阳万米海滩海水浴场风、浪预报
        function gettable52(result, date1) {
            var pbtypes = result.pbtype;
            var resjson=result.children;
            if (pbtypes == "bys") {
                $("#YC_TQ_01").val(resjson[0]["WEATERSTATE"]);
                $("#YC_QW_01").val(resjson[0]["TEMPERATURE"]);
                $("#YC_FS_01").val(resjson[0]["WINDFORCE24FORECAST"]);
                $("#YC_FX_01").val(resjson[0]["WINDDIRECTION24FORECAST"]);
                $("#YC_LG_01").val(resjson[0]["WAVE24FORECAST"]);
            }
            else if (pbtypes == "bydb") {
                $("#YC_TQ_01").val(resjson[0]["WEATERSTATE"]);
                $("#YC_QW_01").val(resjson[0]["TEMPERATURE"]);
                $("#YC_FS_01").val(resjson[0]["WINDSPEED"]);
                $("#YC_FX_01").val(resjson[0]["WINDDIRECTION"]);
                $("#YC_LG_01").val(resjson[0]["WAVEHEIGHT"]);
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
            //var date = new Date($("#tianbaoriqi").datebox("getValue"));
              var date1 = new Date($("#tianbaoriqi").datebox("getValue"));
            var d = new Date(date1);
            var date=d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();  
            //getPredicTideData(date);
           
            gettabledata(date1, "p");
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
                 for (var i in hb_arry) {
                    if (id.toString() == hb_arry[i].toString()) {
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
                                 getValue();
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

                    ////由于加载顺序，此处为覆盖预报员信息
                    function getValue() {
                        var chaoxi = $("#iChaoxi").val();
                        var hailang = $("#iHailang").val();
                        var shuiwen = $("#iShuiwen").val();
                        if (chaoxi != "" && chaoxi != null) {
                            $("#uniform-Chaoxi span").text(chaoxi);
                            $("#uniform-Chaoxi span").attr("code", chaoxi);
                        }
                        if (shuiwen != "" && shuiwen != null) {
                            $("#uniform-Shuiwen span").text(shuiwen);
                            $("#uniform-Shuiwen span").attr("code",shuiwen);
                        }
                        if (hailang != "" && hailang != null) {
                            $("#uniform-Hailang span").text(hailang);
                            $("#uniform-Hailang span").attr("code",hailang);
                        }
                    }
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


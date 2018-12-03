<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableRule.aspx.cs" Inherits="PredicTable.TableRule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>预报表单</title>

    <link rel="stylesheet" href="css/style.default.css" type="text/css" />
    <script type="text/javascript" src="js/plugins/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.cookie.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.flot.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.slimscroll.js"></script>
    <script type="text/javascript" src="js/custom/general.js"></script>
    <script type="text/javascript" src="js/custom/dashboard.js"></script>
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="js/plugins/excanvas.min.js"></script><![endif]-->
    <!--[if IE 9]>
    <link rel="stylesheet" media="screen" href="css/style.ie9.css"/>
<![endif]-->
    <!--[if IE 8]>
    <link rel="stylesheet" media="screen" href="css/style.ie8.css"/>
<![endif]-->
    <!--[if lt IE 9]>
	<script src="js/plugins/css3-mediaqueries.js"></script>
<![endif]-->

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/demo/demo.css" />

    <script type="text/javascript" src="/Scripts/jquery-1.10.2.min.js"></script>

    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>


</head>
<body class="withvernav">






    <div <%--class="centercontent"--%>>
        <div class="pageheader">
            <h1 class="pagetitle">预报表单验证规则</h1>
            <span class="pagedesc">预报表单验证规则</span>

            <ul class="hornav">
                <%--   <li class="current"><a href="#updates">功能组管理</a></li>
                <li><a href="#activities">子功能管理</a></li>--%>
            </ul>
        </div>

        <div id="contentwrapper" class="contentwrapper">
          

            <div style="margin: 20px 0;"></div>
            <div style="width: 100%">
              
                <div style="margin: 0 auto; padding-left: 10%; border-color: #95B8E7; border-style: solid; min-height: 600px; text-align: center">
                    <div id="basicform" class="subcontent">

                        <script>
                            window.onload = getall;
                            function getall() {
                                $.ajax({
                                    type: "Get",
                                    url: "/Ajax/GetRole.ashx?method=getall&s=" + new Date().toTimeString() + "",
                                    success: function (data) {
                                        //alert(data);
                                        var resjson = JSON.parse(data);

                                        //潮汐
                                        $("#tb_cw").val(resjson.tb_cw);
                                        $("#tb_cw2").val(resjson.tb_cw2);
                                        //水温
                                        $("#tb_sw").val(resjson.tb_sw);
                                        $("#tb_sw2").val(resjson.tb_sw2);
                                        //海浪
                                        $("#tb_lg").val(resjson.tb_lg);
                                        $("#tb_lg2").val(resjson.tb_lg2);
                                        $("#tb_lx").val(resjson.tb_lx);
                                        $("#tb_yx").val(resjson.tb_yx);

                                        //$("#tb_bg").val(resjson.tb_bg);
                                        //$("#tb_bx").val(resjson.tb_bx);
                                        //$("#tb_yx").val(resjson.tb_yx);
                                        //$("#tb_lg").val(resjson.tb_lg);
                                        //$("#tb_tq").val(resjson.tb_tq);
                                        //$("#tb_bg2").val(resjson.tb_bg2);
                                        //$("#tb_lg2").val(resjson.tb_lg2);

                                        //气象预报 
                                        $("#tb_fl").val(resjson.tb_fl);
                                        //$("#tb_fl2").val(resjson.tb_fl2);
                                        $("#tb_fx").val(resjson.tb_fx);
                                    }
                                });
                            }
                    
                            $(document).ready(function () {
                                $("#BtnEdit").click(function () {
                                    
                                    var tb_cw = $("#tb_cw").val();
                                    var tb_cw2 = $("#tb_cw2").val();

                                    var tb_sw = $("#tb_sw").val();
                                    var tb_sw2 = $("#tb_sw2").val();

                                    var tb_lg = $("#tb_lg").val();
                                    var tb_lg2 = $("#tb_lg2").val();

                                    var tb_lx = $("#tb_lx").val();  //后添加：浪向
                                    
                                    var tb_yx = $("#tb_yx").val();
                                    var tb_fx = $("#tb_fx").val();
                                    var tb_fl = $("#tb_fl").val();
                                    //var tb_fl2 = $("#tb_fl2").val();

                                    //var tb_tq = $("#tb_tq").val();
                                    //var tb_bg2 = $("#tb_bg2").val();
                                   
                                    //var tb_bg = $("#tb_bg").val();
                                    //var tb_bx = $("#tb_bx").val();
                                   
                                    
                                                    
                                    //var keys = "tb_cw,tb_cw2,tb_bg,tb_bg2,tb_bx,tb_yx,tb_fx,tb_fl,tb_fl2,tb_lg,tb_lg2,tb_tq,tb_sw,tb_sw2";
                                    //var values = tb_cw + "," + tb_cw2 + "," + tb_bg + "," + tb_bg2 + "," + tb_bx + "," + tb_yx + "," + tb_fx + "," + tb_fl + "," + tb_fl2 + "," + tb_lg + "," + tb_lg2 + "," + tb_tq + "," + tb_sw + "," + tb_sw2;
                                    var keys = "tb_cw,tb_cw2,tb_sw,tb_sw2,tb_lg,tb_lg2,tb_lx,tb_yx,tb_fx,tb_fl";
                                    var values = tb_cw + "," + tb_cw2 + "," + tb_sw + "," + tb_sw2 + "," + tb_lg + "," + tb_lg2 + "," + tb_lx + "," + tb_yx + "," + tb_fx + "," + tb_fl;
                                    $.ajax({
                                        type: "POST",
                                        url: "/Ajax/GetRole.ashx?method=editsys&s=" + new Date().toTimeString() + "",
                                        data: { keys: keys, values: values },
                                        success: function (data) {
                                            if (data == "Success") {
                                                $.messager.show({
                                                    title: '提交成功',
                                                    msg: "修改规则配置成功"
                                                });


                                            } else {
                                                $.messager.show({
                                                    title: '提交失败',
                                                    msg: data
                                                });
                                            }
                                        }
                                    });
                                });
                            });
                        </script>

                        <form class="stdform stdform2" method="post" action="" style="width: 80%">
                            <p>
                                <label>潮汐预报</label>
                                <span class="field">
                                  潮高：<input type="text" name="tb_cw" id="tb_cw" style="width:200px" /> -<input type="text" name="tb_cw2" id="tb_cw2" style="width:200px"  />
                                </span>
                            </p>
                            <p>
                                <label>水温预报</label>
                                <span class="field">
                                  水温：<input type="text" name="tb_sw" id="tb_sw" style="width:200px" /> - <input type="text" name="tb_sw2" id="tb_sw2" style="width:200px" /></span>
                            </p>
                            <p>
                                <label>海浪预报</label>
                             <%--   <span class="field">
                                     <span id="weblogolab"  style="padding: 25px;"></span>  
                                    <%--   <input type="text" name="lastname" id="lastname2" class="longinput" />
                                    <%-- <img id="weblogo" src="" style="max-height: 30px; width: auto; margin-right: 20px" /> 
                                    <%--<iframe id="iframeId" width="720" height="27" frameborder="0" scrolling="no" src="iframe.aspx"></iframe> 
                                </span>--%>
                                 <span class="field">
                                       <%--波高：<input type="text" name="tb_bg" id="tb_bg" style="width:200px" /> - <input type="text" name="tb_bg2" id="tb_bg2" style="width:200px"  /><br />--%>
                                       <%--波向：<input type="text" name="tb_bx" id="tb_bx" style="width:420px" /><br />--%>
                                       <%--天气：<input type="text" name="tb_tq" id="tb_tq" style="width:420px" /> <br />--%>
                                     浪高：<input type="text" name="tb_lg" id="tb_lg" style="width:200px" /> - <input type="text" name="tb_lg2" id="tb_lg2" style="width:200px"  /><br />
                                     浪向：<input type="text" name="tb_lx" id="tb_lx" style="width:420px" /><br />
                                     涌向：<input type="text" name="tb_yx" id="tb_yx" style="width:420px" /><br />
                                </span>
                            </p>
                            <p>
                                <label>气象预报</label>
                                <span class="field">
                                  风力：
                                    <%--<input type="text" name="tb_fl" id="tb_fl" style="width:200px" /> - <input type="text" name="tb_fl2" id="tb_fl2" style="width:200px"  /><br />--%>
                                    <input type="text" name="tb_fl" id="tb_fl" style="width:420px" /><br />
                                  风向：<input type="text" name="tb_fx" id="tb_fx" style="width:420px" /><br />
                                </span>
                            </p>
                           
                            <p class="stdformbutton">
                               <%-- <button id="BtnEdit" class="submit radius2">提交</button>--%>
                                  <input type="button" id="BtnEdit" class="stdbtn" value="提交"/>
                                 <input type="button" class="stdbtn" id="restbtn" onclick="getall()" value="重置" />
                            </p>
                        </form>
                    </div>


                    <!--subcontent-->
                </div>
                <!--contentwrapper-->
                <!-- centercontent -->
            </div>
          
             
          
            <div id="activities" class="subcontent" style="display: none;">
                &nbsp;
            </div>
        </div>
    </div>
   
    <style type="text/css">
        body.withvernav {
            background: none;
        }

        #fm {
            margin: 0;
            padding: 10px 30px;
        }

        .ftitle {
            font-size: 14px;
            font-weight: bold;
            padding: 5px 0;
            margin-bottom: 10px;
            border-bottom: 1px solid #ccc;
        }

        .fitem {
            margin-bottom: 5px;
        }

            .fitem label {
                display: inline-block;
                width: 80px;
            }

            .fitem input {
                width: 160px;
            }
    </style>


</body>
</html>
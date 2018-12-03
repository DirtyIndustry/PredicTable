<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="longertable.aspx.cs" Inherits="PredicTable.longertable" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228" Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>中长期预报单</title>
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
    <script type="text/javascript" src="js/custom/tables.js"></script>d

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
        .selects {
        }
    </style>
    <script>
        //$(function () {
        //    getuserinfo("-1");
        //});


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
                    预警报类别：<select id="l1" class="uniformselect">
                        <option value="YB">预报</option>
                        <option value="JB">警报</option>
                        <option value="JC">警报解除通报</option>
                        <option value="XX">消息</option>
                        <option value="SK">实况速报</option>
                        <option value="YJ">应急预测简报</option>
                    </select>&nbsp;&nbsp;
                预警报区域：<select id="l2" class="uniformselect">
                    <option value="1">大洋级</option>
                    <option value="2">海区级</option>
                    <option value="3">海湾级</option>
                    <option value="4">各省市近海海域</option>
                    <option value="5">重点港口</option>
                    <option value="6">中心渔港</option>
                    <option value="7">油田</option>
                    <option value="8">核电站</option>
                    <option value="9">工业园区</option>
                    <option value="10">其他</option>
                </select>&nbsp;&nbsp;<!--自动生成海洋的 联动-->
                    <select id="l8" class="uniformselect">
                        <option value="GLB">全球/大洋</option>
                        <option value="PAC">太平洋</option>
                        <option value="NWP">西北太平洋</option>
                    </select>&nbsp;&nbsp;
                    预警报内容：<select id="l3" class="uniformselect">
                        <option value="HJ">环境预报</option>
                        <option value="SW">海洋水文</option>
                        <option value="FBC">风暴潮</option>
                        <option value="HL">海浪</option>
                        <option value="HX">海啸</option>
                        <option value="HB">海冰</option>
                        <option value="CX">潮汐</option>
                        <option value="HW">海温</option>
                        <option value="ZH">海洋灾害趋势</option>
                        <option value="LC">绿潮</option>
                        <option value="LCDX">绿潮短信</option>
                        <option value="CC">赤潮</option>
                        <option value="YY">溢油</option>
                        <option value="SJ">搜救</option>
                        <option value="GDC">高低潮</option>
                        <option value="ZSC">逐时潮</option>
                        <option value="YCHJ">渔场环境</option>
                        <option value="DSTZX">电视台专项</option>
                        <option value="YBTZX">海洋预报台专项</option>
                        <option value="WZZX">网站专项</option>
                        <option value="SCSW">赛场海域海洋水文</option>
                    </select>
                    &nbsp;&nbsp;
                预警报内容类型：<select id="l4" class="uniformselect">
                    <option value="">非特别</option>
                    <option value="ZX">专项预报</option>
                    <option value="JX">精细化预报</option>
                </select>
                    &nbsp;&nbsp;
                预警报时效：<select id="l5" class="uniformselect">
                    <option value="24hr">24小时</option>
                    <option value="36hr">36小时</option>
                    <option value="48hr">48小时</option>
                    <option value="72hr">72小时</option>
                    <option value="7day">1周</option>
                    <option value="10day">1旬</option>
                    <option value="1mon">1月</option>
                    <option value="1yr">1年</option>
                </select>
                    &nbsp;&nbsp;
                预警报发布时间：
                    <input id="l6" name="16" style="z-index: 1000; position: absolute" class="easyui-datebox" editable="false" data-options="formatter:myformatter,parser:myparser"></input>
                    <script type="text/javascript">
                        function myformatter(date) {
                            var y = date.getFullYear();
                            var m = date.getMonth() + 1;
                            var d = date.getDate();
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
                            //var ss = (s.split('-'));
                            //var y = parseInt(ss[0], 10);
                            //var m = parseInt(ss[1], 10);
                            //var d = parseInt(ss[2], 10);

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
                    &nbsp;&nbsp;
                预警报发布单位：<select id="l7" class="uniformselect">
                    <option value="NMFC">北海预报中心</option>
                    <option value="SDMF">山东省海洋预报台</option>
                    <option value="QDMF">青岛海洋预报台</option>
                    <option value="RZMF">日照海洋环境监测站</option>
                </select>
                    &nbsp;&nbsp;
                     <input runat="server" id="type" name="type" size="33" type="hidden" value="0" />
                    <input runat="server" id="uploadname" name="uploadname" size="33" type="hidden" />
                    <input runat="server" id="newname" name="newname" size="33" type="hidden" />
                    模板选择：<select runat="server" id="list" class="uniformselect"></select>
                    <input runat="server" type="checkbox" id="checkbox1" name="checkbox1" value="checkbox" />
                    使用模板 
                   <%-- <input id="startoperation" value="开始编写文档" onclick="" class="stdbtn" type="button" style="display: none" />--%>
                    <asp:Button runat="server" ID="startoperation" Text="开始编写文档" OnClick="startoperation_Click" />


                </div>
                <div class="contenttitle2" style="z-index: 1000;">
                    <h3 id="mbcz">编写预报文档（ctrl+s保存）</h3>
                </div>
                
                <div runat="server" id="Div1" style="height: 900px">
                    <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" Menubar="False" Titlebar="False"></po:PageOfficeCtrl>
                </div>

            </div>
        </div>
        <script type="text/javascript">

            $("#l2").change(function () {

                var str_key, str_value;
                switch ($("#l2").val()) {
                    case "1": str_key = "全球/大洋,太平洋,西北太平洋"; str_value = "GLB,PAC,NWP"; break;
                    case "2": str_key = "中国海,渤黄东海,渤黄海,北海区,东中国海,南中国海,渤海,黄海,北黄海,南黄海,黄海中部"; str_value = "CS,BHDS,BHS,NCS,ECS,SCS,BS,YS,NYS,SYS,MYS"; break;
                    case "3": str_key = "莱州湾,渤海湾,辽东湾,胶州湾"; str_value = "LZB,BHB,LDB,JZB"; break;
                    case "4": str_key = "山东近海,青岛近海,日照近海,东营近海,威海近海,潍坊近海,岚山近海,虎山近海"; str_value = "SD,QD,RZ,DY,WH,WF,LS,HS"; break;
                    case "5": str_key = "秦皇岛港,青岛港,岚山港,虎山港"; str_value = "QHDP,QDP,LSP,HSP"; break;
                    case "6": str_key = "天津中心渔港"; str_value = "TJF"; break;
                    case "7": str_key = "胜利油田,南堡油田"; str_value = "SLO,NPOF"; break;
                    case "8": str_key = "海阳核电站,荣成核电站"; str_value = "HYN,RCN"; break;
                    case "9": str_key = "天津双港工业园区"; str_value = "SGI"; break;
                    case "10": str_key = "黄河南海堤附近海域"; str_value = "SSWHH"; break;
                    default:
                }
                var data = { key: str_key, value: str_value };
                editselect(data);
            });

            //l2-l8联动方法
            function editselect(data) {
                $("#l8").empty();//先清空再绑定
                var text = data.key.split(',');
                var value = data.value.split(',');
                optionstr = "";
                $("#uniform-l8 span").html(text[0]);//更改选中值
                for (var i = 0; i < text.length; i++) {
                    optionstr += "<option value='" + value[i] + "'>" + text[i] + "</option>"
                }
                jQuery("#l8").append(optionstr);
            }


            $("#l1").change(function () {
                change();
            });
            $("#l8").change(function () {
                change();
            });
            $("#l3").change(function () {
                change();
            });
            $("#l4").change(function () {
                change();
            });
            $("#l5").change(function () {
                change();
            });
            $("#l7").change(function () {
                change();
            });
            // 选择时间事件 onSelect
            $('#l6').datebox({
                onSelect: function (date) {
                    change();
                }
            });
            $(document).ready(function () {
                $('#16').datebox('setValue', formatterDate(new Date()));
                change();

            });
            function change() {
                var newname = "";
                newname += $("#l1").val() + "_";
                newname += $("#l8").val() + "_";
                newname += $("#l3").val();
                newname += $("#l4").val() + "_";
                newname += $("#l5").val() + "_";
                if ($("#l6").datebox("getValue") != "") {
                    newname += $("#l6").datebox("getValue") + "_";
                } else {
                    newname += formatterDate(new Date()) + "_";
                }
                newname += $("#l7").val();
                var uploadname = "";
                uploadname += $("#l1").val() + "_";
                uploadname += $("#l8").val() + "_";
                uploadname += $("#l3").val();
                uploadname += $("#l4").val() + "_";
                uploadname += $("#l5").val() + "_";
                uploadname += $("#l7").val();
                $("#uploadname").val(uploadname);
                $("#newname").val(newname);
            }


            formatterDate = function (date) {
                var day = date.getDate() > 9 ? date.getDate() : "0" + date.getDate();
                var month = (date.getMonth() + 1) > 9 ? (date.getMonth() + 1) : "0"
                + (date.getMonth() + 1);
                return date.getFullYear() + month + day;

            };

            //window.onload = function () {
            //    $('#16').datebox('setValue', formatterDate(new Date()));
            //}


        </script>
    </form>
</body>
</html>

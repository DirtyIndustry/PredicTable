<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadLongerword.aspx.cs" Inherits="PredicTable.UploadLongerword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>中长期预报单模板管理</title>
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
        .selects {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bodywrapper">
            <script>
                //$(function () {
                //    getuserinfo("-1");
                //});


            </script>
            <div id="contentwrapper" class="contentwrapper">
                <h3 id="contenttitle1">模板导入</h3>

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
                预警报发布单位：<select id="l7" class="uniformselect">
                    <option value="NMFC">北海预报中心</option>
                    <option value="SDMF">山东省海洋预报台</option>
                    <option value="QDMF">青岛海洋预报台</option>
                    <option value="RZMF">日照海洋环境监测站</option>
                </select>
                </div>

                <div>

                    <input runat="server" id="type" name="type" size="33" type="hidden" value="0" />
                    <iframe id="iframeId" width="800px" height="70px" frameborder="0" scrolling="no" src="iframe.aspx"></iframe>
                </div>
                <div>
                    <h3 id="contenttitle2">模板管理</h3>
                    <div <%--class="centercontent"--%> style="padding: 10px">
                    </div>
                    <div>
                        <table id="dg" title="模板管理" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%" url="/Ajax/UploadLongerWorddate.ashx?method=getall&type=0" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
                            <thead>
                                <tr>
                                    <%--	<th  width="20" data-options="field:'status',width:60,align:'center',editor:{type:'checkbox',options:{on:'P',off:''}}">Status</th>--%>
                                    <th field="id" style="display: none">编号</th>
                                    <th field="oldname" width="40">原文件名称</th>
                                    <th field="newname" width="40">转换名称</th>


                                </tr>
                            </thead>
                        </table>
                        <div id="toolbar">

                            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyUser()">删除模板</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            //删除
            function destroyUser() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('提示', '确定要删除这个模板吗?', function (r) {
                        if (r) {

                            $.ajax({//综合
                                type: "GET",
                                url: "/Ajax/UploadLongerWorddate.ashx?method=del&type=0&id=" + row.id,

                                success: function (result) {
                                    if (result == "Success") {
                                       

                                        $('#dlg').dialog('close');       
                                        $('#dg').datagrid('reload');    
                                        $.messager.show({
                                            title: '删除成功',
                                            msg: "删除成功"
                                        });
                                    } else {
                                        $('#dg').datagrid('reload');
                                        $.messager.show({
                                            title: '删除失败',
                                            msg: "删除失败"
                                        });
                                    }
                                }
                            });

                        }
                    });
                }
            }


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


            //$("#l1").change(function () {
            //    change();
            //});
            //$("#l8").change(function () {
            //    change();
            //});
            //$("#l3").change(function () {
            //    change();
            //});
            //$("#l4").change(function () {
            //    change();
            //});
            //$("#l5").change(function () {
            //    change();
            //});
            //$("#l7").change(function () {
            //    change();
            //});
            //$(document).ready(function () {

            //    change();

            //});

            //function change() {

            //    var uploadname = "";
            //    uploadname += $("#l1").val() + "_";
            //    uploadname += $("#l8").val() + "_";
            //    uploadname += $("#l3").val();
            //    uploadname += $("#l4").val() + "_";
            //    uploadname += $("#l5").val() + "_";
            //    uploadname += $("#l7").val();
            //    $("#uploadname").val(uploadname);

            //}



        </script>
    </form>
</body>
</html>

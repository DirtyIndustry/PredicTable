<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpLoadMediumAndLong.aspx.cs" Inherits="PredicTable.UpLoadMediumAndLong" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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



  
    <script type="text/javascript">
        $(function () {
            //$('#16').datebox('setValue', formatterDate(new Date()));
            //判断是中文预报单还是英文预报单
            $("#14").change(function () {
                var str_key, str_val;
                switch ($('#14').val()) {
                    case "CN": str_key = "东营环境预报,南堡油田,胜利油田"; str_val = "东营环境预报,南堡油田,胜利油田";
                        break;
                    case "EN": str_key = "国家海洋局北海预报中心,山东省海洋预报台"; str_val = "NMFC,SDMF";
                        break;
                    default:
                }
                var data = { key: str_key, value: str_val };
                $("#hid_type").val($('#14').val());
                editSelecter(data);
            });
            //联动填充select
            function editSelecter(data) {
                $("#11").empty();//先清空再绑定
                var text = data.key.split(',');
                var value = data.value.split(',');
                var optionstr = "";
                $("#uniform-11 span").html(text[0]);//更改选中值 ;
                for (var i = 0; i < text.length; i++) {
                    optionstr += "<option value='" + value[i] + "'>" + text[i] + "</option>"
                }
                $("#11").append(optionstr);
                //触发change事件成联动效果，给文件生成新文件名
                $("#11").change();
            }
        });
        
   </script>
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
                预警报内容类型：<select id="14" class="uniformselect">
                <option value="CN">专项预报</option>
                <option value="EN">精细化预报</option>
            </select>
            <input id="hid_type" type="hidden" value="CN" /> 
            &nbsp;&nbsp;
            预警报发布单位：
                <select id="11" class="uniformselect">
                    <option value="东营环境预报">东营环境预报</option>
                    <option value="南堡油田">南堡油田</option>
                    <option value="胜利油田">胜利油田</option>
               </select>&nbsp;&nbsp;
            预警报内容：
            <select id="13" class="uniformselect">
                 <option value="HJ">环境预报</option>
                 <option value="ZH">海洋灾害趋势</option>
            </select>
            预警报时效：
            <select id="12" class="uniformselect">
                    <option value="10day">1旬</option>
                    <option value="1mon">1月</option>
                    <%--<option value="1yr">1年</option>--%>
            </select>
            &nbsp;&nbsp;
                发布时间：
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
            </div>

             <div>
                <input runat="server" id="type" name="type" size="33" type="hidden" value="0" />、
                <iframe id="iframeId" width="800px" height="70px" frameborder="0" scrolling="no" src="iframUpLoad.aspx"></iframe>
            </div>

            <div>
                    <h3 id="contenttitle2">模板管理</h3>
                    <div style="padding: 10px">
                    </div>
                    <div>
                        <table id="dg" title="模板管理" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%" url="/Ajax/UpLoadMediumAndLong.ashx?method=getall&type='EN' OR TYPE= 'CN'" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
                            <thead>
                                <tr>
                                    <th field="id" style="display: none">编号</th>
                                    <th field="oldname" width="40">原文件名称</th>
                                    <th field="newname" width="40">转换名称</th>
                                </tr>
                            </thead>
                        </table>
                        <div id="toolbar">
                            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="deleteUser()">删除模板</a>
                        </div>
                    </div>
                </div>
        </div>
    </div>
       <script type="text/javascript">
           //删除模板
           function deleteUser() {
               var row = $('#dg').datagrid('getSelected');
               if (row) {
                   $.messager.confirm('提示', '确定要删除这个模板吗?', function (r) {
                       if (r) {

                           $.ajax({//综合
                               type: "GET",
                               url: "/Ajax/UpLoadMediumAndLong.ashx?type=del&method=del&id=" + row.id,

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
       </script>
    </form>
</body>
</html>

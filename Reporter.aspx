<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporter.aspx.cs" Inherits="PredicTable.Reporter" %>
<%-- 
变更记录1：
    变更内容：修改预报员信息，新添加预报员电话的功能（包括显示、添加、修改）    
    变更时间：2018.9.7
    变更人员：Lian
 --%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <title></title>
    <link rel="stylesheet" href="css/style.default.css" type="text/css" />
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
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/demo/demo.css" />

    <script type="text/javascript" src="/Scripts/jquery-1.10.2.min.js"></script>

    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>
    <script>
        $(function () {
            //getuserinfo("-1");
        });
    </script>
</head>
<body class="withvernav">
    <div style="padding: 10px;">
        <table id="dg" title="预报员操作" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%"  url="/Ajax/Reporter.ashx?method=GetReporterByQX" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
               <tr>
                 <th field="ID" hidden="hidden"></th>
                 <th field="reportername" width="24%" >预报员姓名</th>
                 <th field="reportercode" width="24%">预报员编号</th>
                 <th field="reportertype" width="24%">预报员分组</th>
                 <th field="reportertel" width="24%">预报员电话</th>

                 <th field="reportertypeid" hidden="hidden"></th>
               </tr>
            </thead>
       </table>
    </div>
    <div id="toolbar">
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="addReporter()">添加预报员</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editReporter()">修改预报员</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="delReporter()">删除预报员</a>
    </div>
    <form  runat="server" name="upfrom" class="stdform stdform2" method="post" action="" style="width: 95%" enctype="multipart/form-data ">
         <div id="dlg" class="easyui-dialog" style="width: 400px; height: 210px; padding: 30px 30px" closed="true" buttons="#dlg-buttons">
             <div style="width: 100%;">
                 <div style="float: left; width: 100%; height: 100%;">
                     <table>
                         <tr>
                             <td style="width:24%;"></td>
                             <td style="width:24%;"></td>
                             <td style="width:24%;"></td>
                             <td style="width:24%;"></td>
                         </tr>
                          <tr>  
                            <td>
                              <label>预报员姓名：</label>
                            </td>
                            <td colspan="2">
                              <input type="text" id="iput_ReporterName" style="width:100%;" class="easyui-textbox"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>预报员编号：</label>

                            </td>
                            <td colspan="2">
                                <input type="text" id="iput_ReporterCode" style="width:100%;" class="easyui-textbox"/>
                            </td>
                        </tr>      
                         <tr>
                            <td>
                                <label>预报员分组：</label>
                            </td>
                            <td colspan="2">
                                <select id="iput_ReporterType" class="uniformselect" style="width:100%;height:25px;">
                                    <option value="HLYB">海浪预报组</option>
                                    <option value="FBHBYB">风暴潮、海冰预报组</option>
                                </select>
                            </td>
                        </tr>  
                         <tr>
                             <td>
                                 <label>预报员电话：</label>
                             </td>
                             <td colspan="2">
                                <input type="text" id="iput_ReporterTel" style="width:100%;" class="easyui-textbox"/>
                            </td>
                         </tr>
                         
                        <tr>
                            <td style="height:20px;">

                            </td>
                        </tr>             
                        <tr>
                           <td>            
                           </td>
                           <td>
                              <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="commitReporter()" style="width: 90px">提交</a>
                           </td>
                           <td>
                              <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
                           </td>
                        </tr>
                    </table>
                 </div>
              </div>
          </div>
    </form>
      <script>
            var opratiotype = ""; //操作类型：添加、修改
            var oprationUrl = "";
            var reporterName = "";
            var reporterCode = "";
            var reporterType = "";
            var reporterTel = "";
            var rowid = "";
            //添加预报员
            function addReporter() {
                $('#dlg').dialog('open').dialog('setTitle', '添加预报员');
                $("input[type='text']").val("");
                opratiotype = "add";
            }
            //修改预报员
            function editReporter() {
                $('#dlg').dialog('open').dialog('setTitle', '修改预报员信息');
                //电话
                var reporterTel = $('#dg').datagrid('getSelected').reportertel;
                $('#iput_ReporterTel').textbox("setValue", reporterTel);
                //姓名
                var reporterName = $('#dg').datagrid('getSelected').reportername;
                $('#iput_ReporterName').textbox("setValue", reporterName);
                //编号
                var reportCoide = $('#dg').datagrid('getSelected').reportercode;
                $('#iput_ReporterCode').textbox("setValue", reportCoide);
                //类型
                var reporterTypeId = $('#dg').datagrid('getSelected').reportertypeid;
                
                $("#iput_ReporterType").val(reporterTypeId);
               

                //原来程序的
                //$('#iput_ReporterType option').removeAttr("selected");
                //$("#iput_ReporterType").find("option[value='" + reporterTypeId + "']").attr("selected", true);
                rowid = $('#dg').datagrid('getSelected').ID;
                opratiotype = "edit";
            }
            //删除预报员
            function delReporter() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('提示', '确定要删该预报员信息?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "GET",
                                url: '/Ajax/Reporter.ashx?method=DeleteReporter&id=' + row.ID,

                                success: function (result) {
                                    if (result == "Success") {
                                        $('#dlg').dialog('close');        // close the dialog
                                        $('#dg').datagrid('reload');    // reload the user data
                                        $.messager.show({
                                            title: '删除预报员信息提示',
                                            msg: "删除预报员信息成功"
                                        });
                                    } else {
                                        $.messager.show({
                                            title: '删除预报员信息提示',
                                            msg: "删除预报员信息失败"
                                        });
                                    }
                                }
                            });

                        }
                    });
                }
            }
            //提交数据
            function commitReporter() {
                reporterName = $('#iput_ReporterName').val();
                reporterCode = $('#iput_ReporterCode').val();
                reporterType = $('#iput_ReporterType').val();
                reporterTel = $('#iput_ReporterTel').val();
                if (opratiotype == "add") {
                    oprationUrl = '/Ajax/Reporter.ashx?method=SubmitReporter&reporterName=' + encodeURI(encodeURI(reporterName)) + "&reporterCode="
                        + encodeURI(encodeURI(reporterCode)) + "&reporterType=" + encodeURI(encodeURI(reporterType)) + "&reporterTel=" + encodeURI(encodeURI(reporterTel));
                }
                else if (opratiotype == "edit") {
                    oprationUrl = '/Ajax/Reporter.ashx?method=EditReporter&id=' + encodeURI(encodeURI(rowid)) + "&reporterName=" + encodeURI(encodeURI(reporterName)) + "&reporterCode=" + encodeURI(encodeURI(reporterCode)) + "&reporterType=" + encodeURI(encodeURI(reporterType)) + "&reporterTel=" + encodeURI(encodeURI(reporterTel));
                }
                $.ajax({
                    type: "GET",
                    url: oprationUrl,
                    success: function (result) {
                        $('#dlg').dialog('close');        // close the dialog
                        $('#dg').datagrid('reload');    // reload the user data
                        if (opratiotype == "add") {
                            if (result == "Success") {
                                $.messager.show({
                                    title: '新增预报员信息提示',
                                    msg: "新增预报员成功"
                                });
                            } else {
                                $.messager.show({
                                    title: '新增预报员信息提示',
                                    msg: "新增预报员失败"
                                });
                            }
                        } else if (opratiotype == "edit") {
                            if (result == "Success") {
                                $.messager.show({
                                    title: '修改预报员信息提示',
                                    msg: "修改预报员信息成功"
                                });
                            } else {
                                $.messager.show({
                                    title: '修改预报员信息提示',
                                    msg: "修改预报员信息失败"
                                });
                            }
                        }
                    }
                });
            }
        </script>
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
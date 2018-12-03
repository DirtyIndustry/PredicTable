<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contects.aspx.cs" Inherits="PredicTable.Contects" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>联系人管理</title>
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
</head>
<body class="withvernav">
    <div style="padding: 10px;">
        <table id="dg" title="联系人管理" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%"  url="/Ajax/Contents.ashx?method=GetContents" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
               <tr>
                 <th field="ID" hidden="hidden"></th>
                 <th field="CONTENTSNAME" width="50%" >联系人姓名</th>
                 <th field="CONTENTSCODE" width="49%">联系人编号</th>
               </tr>
            </thead>
       </table>
    </div>
    <div id="toolbar">
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="addContents()">添加联系人</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editContents()">修改联系人</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="delContents()">删除联系人</a>
    </div>
    <form  runat="server" name="upfrom" class="stdform stdform2" method="post" action="" style="width: 95%" enctype="multipart/form-data ">
         <div id="dlg" class="easyui-dialog" style="width: 400px; height: 210px; padding: 30px 30px" closed="true" buttons="#dlg-buttons">
             <div style="width: 100%;">
                 <div style="float: left; width: 100%; height: 100%;">
                     <table>
                         <tr>
                             <td style="width:32%;"></td>
                             <td style="width:32%;"></td>
                             <td style="width:32%;"></td>
                         </tr>
                          <tr>
                            <td>
                              <label>联系人姓名：</label>
                            </td>
                            <td colspan="2">
                              <input type="text" id="iput_ContentsName" style="width:100%;" class="easyui-textbox"/>
                            </td>
                        </tr>
                         <tr>
                            <td style="height:10px;">

                            </td>
                        </tr>     
                        <tr >
                            <td>
                                <label>联系人编号：</label>

                            </td>
                            <td colspan="2">
                                <input type="text" id="iput_ContentsCode" style="width:100%;" class="easyui-textbox"/>
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
                              <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="commitContents()" style="width: 90px">提交</a>
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
            var rowid = "";
            //添加预报员
            function addContents() {
                $('#dlg').dialog('open').dialog('setTitle', '添加联系人');
                $('#iput_ContentsName').textbox("setValue", '');
                $('#iput_ContentsCode').textbox("setValue", '');
                opratiotype = "add";
            }
            //修改预报员
            function editContents() {
                $('#dlg').dialog('open').dialog('setTitle', '修改联系人信息');
                //姓名
                var contentsName = $('#dg').datagrid('getSelected').CONTENTSNAME;
                $('#iput_ContentsName').textbox("setValue", contentsName);
                //编号
                var contentsCode = $('#dg').datagrid('getSelected').CONTENTSCODE;
                $('#iput_ContentsCode').textbox("setValue", contentsCode);
                //类型
                rowid = $('#dg').datagrid('getSelected').ID;
                opratiotype = "edit";
            }
            //删除预报员
            function delContents() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('提示', '确定要删该联系人信息?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "GET",
                                url: '/Ajax/Contents.ashx?method=DeleteContents&id=' + row.ID,

                                success: function (result) {
                                    if (result == "Success") {
                                        $('#dlg').dialog('close');        // close the dialog
                                        $('#dg').datagrid('reload');    // reload the user data
                                        $.messager.show({
                                            title: '删除联系人信息提示',
                                            msg: "删除联系人信息成功"
                                        });
                                    } else {
                                        $.messager.show({
                                            title: '删除联系人信息提示',
                                            msg: "删除联系人信息失败"
                                        });
                                    }
                                }
                            });

                        }
                    });
                }
            }
            //提交数据
            function commitContents() {
                contentsName = $('#iput_ContentsName').val();
                contentsCode = $('#iput_ContentsCode').val();
                if (opratiotype == "add") {
                    oprationUrl = '/Ajax/Contents.ashx?method=SubmitContents&contentsName=' + encodeURI(encodeURI(contentsName)) + "&contentsCode="
                        + encodeURI(encodeURI(contentsCode));
                }
                else if (opratiotype == "edit") {
                    oprationUrl = '/Ajax/Contents.ashx?method=EditContents&id=' + encodeURI(encodeURI(rowid)) + "&contentsName=" + encodeURI(encodeURI(contentsName)) + "&contentsCode="
                        + encodeURI(encodeURI(contentsCode));
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
                                    title: '新增联系人信息提示',
                                    msg: "新增联系人成功"
                                });
                            } else {
                                $.messager.show({
                                    title: '新增联系人信息提示',
                                    msg: "新增联系人失败"
                                });
                            }
                        } else if (opratiotype == "edit") {
                            if (result == "Success") {
                                $.messager.show({
                                    title: '修改联系人信息提示',
                                    msg: "修改联系人信息成功"
                                });
                            } else {
                                $.messager.show({
                                    title: '修改联系人信息提示',
                                    msg: "修改联系人信息失败"
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

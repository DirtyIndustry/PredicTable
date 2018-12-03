<!--变更记录 add by yy on 2018-04-16 -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeRecord.aspx.cs" Inherits="PredicTable.ChangeRecord" %>

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

</head>
<body class="withvernav">
    <div style="padding:10px;">
        <table id="dg" title="变更记录" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%" url="/Ajax/ChangeRecord.ashx?method=GetChangeRecord" fitcolumns="true" singleselect="true" toolbar="#toolbar" pagination="true" rownumbers="true">
            <thead>
                <tr>
                    <th field="ID" hidden="hidden"></th>
                    <th field="ChangeContent" width="60%">变更内容</th>
                    <th field="ChangePerson" width="20%">变更人员</th>
                    <th field="ChangeDate" width="20%">变更日期</th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="addChangeRecord()">添加变更记录</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editChangeRecord()">修改变更记录</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="delChangeRecord()">删除变更记录</a>
    </div>
    <form name="upform" runat="server" class="stdform stdform2" method="post" style="width:95%" enctype="multipart/form-data">
        <tag><div id="dlg" class="easyui-dialog form-style" style=" padding: 30px 30px;width:600px;height:400px;" closed="true" buttons="#dlg-buttons">
            <div id="div_ChangeContent" class="fitem" >
                <label id="Label1" style="width:100px; height: 100px;">变更内容：</label>
                <input style="height:150px" type="text" name="ChangeContent" data-options="multiline:true" id="Input_ChangeContent" class="easyui-textbox" required="true" />
            </div>
            <div id="div_ChangePerson" class="fitem">
                <label id="la_ChangePerson" style="width:100px; display:inline-block;">变更人员：</label>
                <input type="text" name="ChangePerson" id="input_ChangePerson" class="easyui-textbox" required="true" style="margin-left:2px"/>
            </div>
            
            <div style="padding:60px 0 0 180px;">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="commitData()" style="width: 80px" id ="a_sure">提交</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 80px" id="a_cancel">取消</a>
            </div>
        </div></tag>
    </form>
    <script type="text/javascript">
        var oprationtype = "";//操作类型：添加、修改
        var rowid = "";
        //添加变更记录
        function addChangeRecord() {
            $("#dlg").dialog('open').dialog('setTitle', '添加变更记录');
            $("#input_ChangePerson").textbox("setValue", "");
            $("#Input_ChangeContent").textbox("setValue", "");
            oprationtype = "add";//操作类型
        }
        //修改变更记录
        function editChangeRecord() {
            //获取编号
            var row = $("#dg").datagrid('getSelected');
            if (row)
            {
                $('#dlg').dialog('open').dialog('setTitle', '修改变更记录');
                $("#Input_ChangeContent").textbox('setValue', row.ChangeContent);
                $("#input_ChangePerson").textbox('setValue', row.ChangePerson);
                rowid = $('#dg').datagrid('getSelected').ID;
                oprationtype = "edit";//操作类型 (编辑)
            }
        }
        //删除记录
        function delChangeRecord() {
            var row = $("#dg").datagrid("getSelected");
            if (row)
            {
                var recordID = row.ID;
                $.messager.confirm("提示", "确实删除此变更记录", function (r) {
                    if (r) {
                        $.ajax({
                            type: "GET",
                            url: "/Ajax/ChangeRecord.ashx?method=DeleteChange&id=" + recordID,
                            success: function (res) {
                                $('#dg').datagrid('reload');
                                if (res == "success") {
                                    $.messager.show({
                                        title: '提示',
                                        msg: '删除该变更记录成功'
                                    });
                                    
                                } else {
                                    $.messager.show({
                                        title: '提示',
                                        msg: '删除该变更记录失败'
                                    });
                                }
                                
                            }
                        });
                        }
                });
            }
        }
        //提交
        function commitData() {
            var ChangeRecordContent = $("#Input_ChangeContent").val();
            var ChangeRecordPerson = $("#input_ChangePerson").val();
            var SubmitUrl = "";//ajax路径
            if (oprationtype == "add") { //添加
                SubmitUrl = '/Ajax/ChangeRecord.ashx?method=SubmitChange&ChangeRecordContent=' + encodeURI(encodeURI(ChangeRecordContent)) + "&ChangeRecordPerson="
                        + encodeURI(encodeURI(ChangeRecordPerson));
            } else if (oprationtype == "edit") {    
                //修改
                SubmitUrl = '/Ajax/ChangeRecord.ashx?method=EditChange&id=' + encodeURI(encodeURI(rowid)) + "&ChangeRecordContent=" + (encodeURI(ChangeRecordContent)) + "&ChangeRecordPerson="
                        + encodeURI(encodeURI(ChangeRecordPerson)) ;
            }
            $.ajax({
                type:"GET",
                url: SubmitUrl,
                success: function (res) {
                    $('#dlg').dialog('close');        // close the dialog
                    $('#dg').datagrid('reload');    // reload the user data
                    if (oprationtype == "add") { //添加
                        if (res == "success")
                        {  
                            $.messager.show({
                                title: '提示',
                                msg: "添加变更记录成功"
                            });
                        } else {
                            $.messager.show({
                                title: '提示',
                                msg: "添加变更记录失败"
                            });
                        }
                    } else if (oprationtype == "edit") {
                        //修改
                        if (res == "success") {
                            $.messager.show({
                                title: '提示',
                                msg: "修改变更记录成功"
                            });
                        } else {
                            $.messager.show({
                                title: '提示',
                                msg: "修改变更记录失败"
                            });
                        }
                        
                    }
                }
            })
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
                width: 408px;
            }
    </style>
</body>
</html>

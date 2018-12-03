<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupUnit.aspx.cs" Inherits="PredicTable.GroupUnit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>发送单位分组</title>
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
</head>
<body class="withvernav">
    <div style="padding: 10px;">
        <table id="dg" title="发布单位分组操作" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%"  url="/Ajax/GroupUnit.ashx?method=getdata" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
               <tr>
                 <th field="ID" hidden="hidden"></th>
                 <th field="GROUPNAME" width="20%" >分组名称</th>
                 <th field="UNITNAME" width="79%">发布单位名称</th>
               </tr>
            </thead>
       </table>
    </div>
    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="addUnitGroup()">添加分组</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUnitGroup()">修改分组</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="delUnitGroup()">删除分组</a>
    </div>
    <script type="text/javascript">
        var opratiotype = "";
        var rowid = "";
        //添加分组
        function addUnitGroup() {
            $('#dlg').dialog('open').dialog('setTitle', '添加发布单位分组');
            $("input[type='text']").val("")
            //获取发送单位
            getUnit();
            opratiotype = "add";
            //清空选中的checkbox
            $("#selectAll").prop("checked", false);
            $("#reverse").prop("checked", false);
            $("#unselect").prop("checked", false);
        }
        
        //修改分组
        function editUnitGroup() {
            $('#dlg').dialog('open').dialog('setTitle', '修改发布单位分组');
            //获取分组名称
            var rowName = $('#dg').datagrid('getSelected').GROUPNAME;
            $('#txtUnitGroupName').textbox("setValue", rowName);
            //获取发送单位
            getUnit();
            rowid = $('#dg').datagrid('getSelected').ID;
            opratiotype = "edit";
            //清空选中的checkbox
            $("#selectAll").prop("checked", false);
            $("#reverse").prop("checked", false);
            $("#unselect").prop("checked", false);
        }
        //删除分组
        function delUnitGroup() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('提示', '确定要删除这个分组?', function (r) {
                    if (r) {
                        $.ajax({
                            type: "GET",
                            url: '/Ajax/GroupUnit.ashx?method=delete&id=' + row.ID,

                            success: function (result) {
                                if (result == "Success") {
                                    $('#dlg').dialog('close');        // close the dialog
                                    $('#dg').datagrid('reload');    // reload the user data
                                    $.messager.show({
                                        title: '删除信息提示',
                                        msg: "删除分组成功"
                                    });
                                } else {
                                    $.messager.show({
                                        title: '删除信息提示',
                                        msg: "删除分组失败"
                                    });
                                }
                            }
                        });

                    }
                });
            }
        }

        //提交操作
        function commitUnitGroup(unitGroupName, grouplist) {
            var datas = "";
            if (opratiotype == "add") {
                oprationUrl = '/Ajax/GroupUnit.ashx?method=add';
                datas = { unitGroupName: unitGroupName, grouplist: grouplist };
            }
            else if (opratiotype == "edit") {
                oprationUrl = '/Ajax/GroupUnit.ashx?method=edit';
                datas = { id: rowid, unitGroupName: unitGroupName, grouplist: grouplist };
            }
            $.ajax({
                type: "POST",
                url: oprationUrl,
                data: datas,
                success: function (result) {
                    $('#dlg').dialog('close');        // close the dialog
                    $('#dg').datagrid('reload');    // reload the user data
                    if (opratiotype == "add") {
                        if (result == "Success") {
                            $.messager.show({
                                title: '新增信息提示',
                                msg: "新增发布单位成功"
                            });
                        } else {
                            $.messager.show({
                                title: '新增信息提示',
                                msg: "新增发布单位失败"
                            });
                        }
                    } else if (opratiotype == "edit") {
                        if (result == "Success") {
                            $.messager.show({
                                title: '修改信息提示',
                                msg: "修改发布单位成功"
                            });
                        } else {
                            $.messager.show({
                                title: '修改信息提示',
                                msg: "修改发布单位失败"
                            });
                        }
                    }
                }
            });
        }
        
    </script>
    <form  runat="server" name="upfrom" class="stdform stdform2" method="post" action="" style="width: 95%" enctype="multipart/form-data ">
         <div id="dlg" class="easyui-dialog" style="width: 400px; height: 650px; padding: 30px 30px" closed="true" buttons="#dlg-buttons">
              <div>
                   分组名称：
                   <input type="text" id="txtUnitGroupName" value="" class="easyui-textbox" style="width:70%;" />
               </div>
              <div style="height:20px;"></div>
              <div>
                   <asp:CheckBox ID="selectAll" runat="server" Text="全选" />
                   <asp:CheckBox ID="reverse" runat="server" Text="反选" />
                    <asp:CheckBox ID="unselect" runat="server" Text="取消" />
               </div>
               <hr />
               
               <div id="unitlist">
               </div>
               <input style="float: right; margin: 3px 5px 0px 0px" type="button"  onclick="$('#dlg').dialog('close'); $('#dlg_unit div div span').removeClass('checked');" value="取消" />
               <input id="Releasetable" style="float: right; margin: 3px 5px 0px 0px" type="button" value="确定" />
               <input type="hidden" value="" id="hidGroupUnit" />
               <div style="height:40px;"></div>
          </div>
        <%--操作发送单位分组弹出框--%>
        <script type="text/javascript">
            //获取发送单位，生成checkbox
            function getUnit() {
                $.ajax({
                    type: "GET",
                    url: '/Ajax/OprationSendUnit.ashx?method=getdataall',

                    success: function (result) {
                        if (result != null) {
                            //添加发送单位
                            var Unit = addUnit(result);
                            $('#unitlist').html(Unit);
                        }
                    }
                });
            }
            //生成发送单位checkbox
            function addUnit(result) {
                var unitHtml = "";
                for (var i = 0; i < result.length; i++) {
                    unitHtml += "<div><span><input type='checkbox' value='" + result[i]['SENDUNIT'] + "' id='" + result[i]['SENDUNIT'] + "' onclick='setCheck(this)'><span style='margin-left:5px;'>" + result[i]['SENDUNIT'] + "</span></input></span></div>";
                }
                return unitHtml;
            }
            //全选
            $('#selectAll').click(function () {
                if ($("#uniform-selectAll span").attr("class") != "checked") {
                    $(this).prop("checked", true);
                    $("#reverse").prop("checked", false);
                    $("#unselect").prop("checked", false);
                    $("#unitlist input:checkbox").each(function () {
                        $(this).prop("checked", true);
                    });
                }
            });
            //反选
            $('#reverse').click(function () {
                if ($("#uniform-reverse span").attr("class") != "checked") {
                    $(this).prop("checked", true);
                    $("#selectAll").prop("checked", false);
                    $("#unselect").prop("checked", false);
                    $("#unitlist input:checkbox").each(function () {
                        if ($(this).prop("checked")) {
                            $(this).prop("checked", false);
                        } else {
                            $(this).prop("checked", true);
                        }
                    });
                }
            });
            //取消
            $('#unselect').click(function () {
                if ($("#uniform-unselect span").attr("class") != "checked") {
                    $(this).prop("checked", true);
                    $("#unitlist input:checkbox").each(function () {
                        $(this).prop("checked", false);
                    });
                    $("#selectAll").prop("checked", false);
                    $("#reverse").prop("checked", false);
                }
            });
        </script>

        <%--数据提交--%>
        <script type="text/javascript">
            $('#Releasetable').click(function () {
                var unitGroupName = "";
                if ($("#txtUnitGroupName").val() == "") {
                    alert("请填写发送单位分组名称");
                    return false;
                }
                unitGroupName = $("#txtUnitGroupName").val();
                if ($("input[type='checkbox']:checked").length > 0) {
                    var grouplist = " ";
                    var groupName = "";
                    $("#unitlist input:checkbox").each(function () {
                        if ($(this).prop("checked")) {
                            grouplist += $(this).val() + ",";
                        }
                    });
                    grouplist = grouplist.substring(grouplist.length - 1, 1);
                    //保存选择的发送单位
                    $('#hidGroupUnit').val(grouplist);
                }
                $('#dlg').dialog('close');
                commitUnitGroup(unitGroupName, grouplist);
            });
        </script>
    </form>
</body>
</html>

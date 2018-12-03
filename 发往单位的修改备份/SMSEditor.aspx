<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMSEditor.aspx.cs" Inherits="PredicTable.SMSEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>短信编辑</title>
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
    <style type="text/css">
        #userlist li {
            float: left;
            list-style-type: none;
            margin: 2px;
            border: 3px solid #ccc;
            font-weight: bold;
        }

            #userlist li:hover {
                /*border:3px solid #ff6a00;*/
                cursor: pointer;
            }

        .contactlist li a {
            padding: 0px;
        }

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
<body>
    <div <%--class="centercontent"--%> style="padding: 10px">

        <table id="dg" title="微信平台信息管理" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%"
            url="/Ajax/getSMS.ashx?method=getdata" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">

            <thead>
                <tr>
                    <th field="time" width="150">时间</th>
                    <th field="mestype" width="40">消息类型</th>
                    <th field="DXGROUP" width="150">短信组</th>

                    <th field="state" width="40">状态</th>
                    <th field="userid" width="100">上传作者</th>
                    <th field="filemane" width="150">文件名</th>
                    <th field="type" width="50">文件类型</th>

                </tr>
            </thead>
        </table>
    </div>
    <div id="toolbar">

        <script type="text/javascript">
            function mergeCellsByField(tableID, colList) {
                var ColArray = colList.split(",");
                var tTable = $("#" + tableID);
                var TableRowCnts = tTable.datagrid("getRows").length;
                var tmpA;
                var tmpB;
                var PerTxt = "";
                var CurTxt = "";
                var alertStr = "";
                for (j = ColArray.length - 1; j >= 0; j--) {
                    PerTxt = "";
                    tmpA = 1;
                    tmpB = 0;

                    for (i = 0; i <= TableRowCnts; i++) {
                        if (i == TableRowCnts) {
                            CurTxt = "";
                        }
                        else {
                            CurTxt = tTable.datagrid("getRows")[i][ColArray[j]];
                        }
                        if (PerTxt == CurTxt) {
                            tmpA += 1;
                        }
                        else {
                            tmpB += tmpA;

                            tTable.datagrid("mergeCells", {
                                index: i - tmpA,
                                field: ColArray[j],　　//合并字段
                                rowspan: tmpA,
                                colspan: null
                            });
                            tTable.datagrid("mergeCells", { //根据ColArray[j]进行合并
                                index: i - tmpA,
                                field: "Ideparture",
                                rowspan: tmpA,
                                colspan: null
                            });

                            tmpA = 1;
                        }
                        PerTxt = CurTxt;
                    }
                }
            }
            $('#dg').datagrid({
                pagination: true,
                onLoadSuccess: function (data) {

                    if (data.rows.length > 0) {
                        //调用mergeCellsByField()合并单元格
                        //mergeCellsByField("dg", "documentcontent,time);
                        mergeCellsByField("dg", "time");

                    }
                    columns: [[{ width: 80, align: 'center', styler: function (value, row, index) { return 'vertical-align:middle;'; } }]]

                }

            });
            $("#dg").datagrid('hideColumn', 'id');
            $("#dg").datagrid('hideColumn', ' vid');
        </script>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="delAll()">删除整条信息</a>
       <%-- <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="delFuJian()">删除附件</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="addFuJian()">添加附件</a>--%>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="addAll()">添加信息</a>
    </div>
    <form runat="server" name="upfrom" class="stdform stdform2" method="post" action="" style="width: 95%" enctype="multipart/form-data ">
       <div id="dlg" class="easyui-dialog" style="width: 1200px; height: 600px; padding: 10px 20px;overflow-y: hidden" closed="true" buttons="#dlg-buttons">
            <div title="短信" style="padding: 0px;">
                <div style="margin: 0 auto; padding-left: 1%; border-color: #95B8E7; border-style: solid; min-height: 600px; text-align: center">
                    <div id="basicformdx" class="subcontent">
                        <div style="width: 800px;">
                            <div style="width: 800px; height: 490px;">
                                <div style="float: left; width: 150px; height: 100%; line-height: 100px; background-color: #fcfcfc;">短信内容：</div>
                                <div style="float: left; width: 640px; height: 100%;">
                                    <iframe id="iframeIddx" width="1000" height="560" frameborder="0" scrolling="yes" src="smsiframe.aspx"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script>
        function addAll() {
            $('#dlg').dialog('open').dialog('setTitle', '添加信息');
        }
        // 获取选中的标签页面板（tab panel）和它的标签页（tab）对象
        //var pp = $('#tt').tabs('getSelected');
        //var tab = pp.panel('options').tab; // 相应的标签页（tab）对象 
        //function addAll() {
            
        //}
        //删除整条信息（文档和附件）
        function delAll() {
            var row = $('#dg').datagrid('getSelected');
            //alert(row.id);
            if (row) {
                $.messager.confirm('提示', '确定要删除吗？文档内容和附件都将被删除！', function (r) {
                    if (r) {
                        $.ajax({
                            type: "GET",
                            url: "/Ajax/getSMS.ashx?method=delall&id=" + row.id,
                            success: function (result) {
                                if (result == "Success") {
                                    $('#dlg').dialog('close');        // close the dialog
                                    $('#dg').datagrid('reload');    // reload the user data
                                    $.messager.show({
                                        title: '删除成功',
                                        msg: "删除成功"
                                    });
                                } else {
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
            else {
                $.messager.show({
                    title: '提示',
                    msg: "请选择要删除的文档内容！"
                });
            }
        }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendUnit.aspx.cs" Inherits="PredicTable.SendUnit" %>

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
        <table id="dg" title="发布单位操作" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%"  url="/Ajax/OprationSendUnit.ashx?method=getdata" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
               <tr>
                 <th field="ID" hidden="hidden"></th>
                 <th field="SENDUNIT" width="32%" >发布单位名称</th>
                 <th field="CREATEDATE" width="32%">创建时间</th>
                 <th field="UPDATEDATE" width="32%">修改时间</th>
               </tr>
            </thead>
       </table>
    </div>
    <div id="toolbar">
       <script type="text/javascript">
            //function mergeCellsByField(tableID, colList) {
            //    var ColArray = colList.split(",");
            //    var tTable = $("#" + tableID);
            //    var TableRowCnts = tTable.datagrid("getRows").length;
            //    var tmpA;
            //    var tmpB;
            //    var PerTxt = "";
            //    var CurTxt = "";
            //    var alertStr = "";
            //    for (j = ColArray.length - 1; j >= 0; j--) {
            //        PerTxt = "";
            //        tmpA = 1;
            //        tmpB = 0;

            //        for (i = 0; i <= TableRowCnts; i++) {
            //            if (i == TableRowCnts) {
            //                CurTxt = "";
            //            }
            //            else {
            //                CurTxt = tTable.datagrid("getRows")[i][ColArray[j]];
            //            }
            //            if (PerTxt == CurTxt) {
            //                tmpA += 1;
            //            }
            //            else {
            //                tmpB += tmpA;

            //                tTable.datagrid("mergeCells", {
            //                    index: i - tmpA,
            //                    field: ColArray[j],　　//合并字段
            //                    rowspan: tmpA,
            //                    colspan: null
            //                });
            //                tTable.datagrid("mergeCells", { //根据ColArray[j]进行合并
            //                    index: i - tmpA,
            //                    field: "Ideparture",
            //                    rowspan: tmpA,
            //                    colspan: null
            //                });

            //                tmpA = 1;
            //            }
            //            PerTxt = CurTxt;
            //        }
            //    }
            //}
            //$('#dg').datagrid({
            //    pagination: true,
            //    onLoadSuccess: function (data) {

            //        if (data.rows.length > 0) {
            //            //调用mergeCellsByField()合并单元格
            //            //mergeCellsByField("dg", "documentcontent,time);
            //            mergeCellsByField("dg", "time");
                     
            //        }
            //        columns: [[{ width: 80, align: 'center', styler: function (value, row, index) { return 'vertical-align:middle;'; } }]]
                   
            //    }
              
            //});
            //$("#dg").datagrid('hideColumn', 'id');
            //$("#dg").datagrid('hideColumn', ' vid');
        </script>
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="addUnit()">添加发布单位</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUnit()">修改发布单位</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="delUnit()">删除发布单位</a>
    </div>
    <form  runat="server" name="upfrom" class="stdform stdform2" method="post" action="" style="width: 95%" enctype="multipart/form-data ">
         <div id="dlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 30px 30px" closed="true" buttons="#dlg-buttons">
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
                              <label>单位名称：</label>
                            </td>
                            <td colspan="2">
                              <input type="text" id="iput_UnitName" style="width:100%;" class="easyui-textbox"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="height:30px"></td>
                            <td></td>
                            <td></td>
                        </tr>                
                        <tr>
                           <td>            
                           </td>
                           <td>
                              <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="commitUnit()" style="width: 90px">提交</a>
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
            var opratiotype = "";
            var oprationUrl = "";
            var unitName = "";
            var rowid = "";
            //添加发送单位
            function addUnit() {
                $('#dlg').dialog('open').dialog('setTitle', '添加发布单位');
                opratiotype = "add";
            }
            //修改发送单位
            function editUnit() {
                var rowName = $('#dg').datagrid('getSelected').SENDUNIT;
                $('#iput_UnitName').val(rowName);
                $('#dlg').dialog('open').dialog('setTitle', '修改发布单位');
                rowid = $('#dg').datagrid('getSelected').ID;
                
                opratiotype = "edit";
            }
            //删除发送单位
            function delUnit() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('提示', '确定要删除这个发布单位?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "GET",
                                url: '/Ajax/OprationSendUnit.ashx?method=delete&id=' + row.ID,

                                success: function (result) {
                                    if (result == "Success") {
                                        $('#dlg').dialog('close');        // close the dialog
                                        $('#dg').datagrid('reload');    // reload the user data
                                        $.messager.show({
                                            title: '删除信息提示',
                                            msg: "删除发布单位成功"
                                        });
                                    } else {
                                        $.messager.show({
                                            title: '删除信息提示',
                                            msg: "删除发布单位失败"
                                        });
                                    }
                                }
                            });

                        }
                    });
                }
            }
            //提交数据
            function commitUnit() {
                unitName = $('#iput_UnitName').val();
                if (opratiotype == "add") {
                    oprationUrl = '/Ajax/OprationSendUnit.ashx?method=add&unitName=' + encodeURI(encodeURI(unitName));
                }
                else if (opratiotype == "edit") {
                    oprationUrl = '/Ajax/OprationSendUnit.ashx?method=edit&id=' + encodeURI(encodeURI(rowid)) + "&unitName=" + encodeURI(encodeURI(unitName));
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

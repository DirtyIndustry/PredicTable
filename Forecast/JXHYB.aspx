<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JXHYB.aspx.cs" Inherits="PredicTable.Forecast.JXHYB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../EasyUI/themes/metro/easyui.css" rel="stylesheet" />
    <link href="../EasyUI/themes/icon.css" rel="stylesheet" />
    <script src="../EasyUI/jquery.min.js"></script>
    <script src="../EasyUI/jquery.easyui.min.js"></script>
    
    <link href="../Css/StyleSheet_JXH.css" rel="stylesheet" />
     

</head>
<body>
    <form id="form1" runat="server">

        <div id="head">
            <div id="ybfbsj">

                <label>预报发布时间：</label>
                <input class="easyui-datetimebox" data-options="sharedCalendar:'#cc'" id="txtcx">
                &nbsp;&nbsp;&nbsp;&nbsp; <input  type="button"  onclick="dlg_Submit()" value="查询" />
                <div id="cc" class="easyui-calendar">
                </div>
            </div>
        </div>
        <div id="table">
            <table id="dg" title="精细化预报数据" style="width: 100%; height: 400px" data-options="
				rownumbers:true,
				singleSelect:true,
				autoRowHeight:false,
				pagination:true,
				pageSize:10">
                <thead>
                    <tr>
                        <th field="FILENAME" width="80">文件名</th>
                        <th field="PUBLISHDATE" width="100">预报发布时间</th>
                        <th field="TARGETTYPE" width="80">目标类型</th>
                        <th field="TARGETNAME" width="80"  >目标名称</th>
                        <th field="FILENUMBER" width="80"  >预报单编号</th>
                        <th field="FD_GC1_CS" width="100"  >第一天第一次高潮潮时</th>
                        <th field="FD_GC1_CG" width="110">第一天第一次高潮潮高</th>
                        <th field="FD_GC2_CS" width="100"  >第一天第二次高潮潮时</th>
                        <th field="FD_GC2_CG" width="110">第一天第二次高潮潮高</th>
                        <th field="FD_GC3_CS" width="100"  >第一天第三次高潮潮时</th>
                        <th field="FD_GC3_CG" width="110">第一天第三次高潮潮高</th>
                        <th field="FD_DC1_CS" width="100"  >第一天第一次低潮潮时</th>
                        <th field="FD_DC1_CG" width="110">第一天第一次低潮潮高</th>
                        <th field="FD_DC2_CS" width="100"  >第一天第二次低潮潮时</th>
                        <th field="FD_DC2_CG" width="110">第一天第二次低潮潮高</th>
                        <th field="FD_DC3_CS" width="100"  >第一天第三次低潮潮时</th>
                        <th field="FD_DC3_CG" width="110">第一天第三次低潮潮高</th>
                        <th field="SD_GC1_CS" width="100"  >第二天第一次高潮潮时</th>
                        <th field="SD_GC1_CG" width="110">第二天第一次高潮潮高</th>
                        <th field="SD_GC2_CS" width="100"  >第二天第二次高潮潮时</th>
                        <th field="SD_GC2_CG" width="110">第二天第二次高潮潮高</th>
                        <th field="SD_GC3_CS" width="100"  >第二天第三次高潮潮时</th>
                        <th field="SD_GC3_CG" width="110">第二天第三次高潮潮高</th>
                        <th field="SD_DC1_CS" width="100"  >第二天第一次低潮潮时</th>
                        <th field="SD_DC1_CG" width="110">第二天第一次低潮潮高</th>
                        <th field="SD_DC2_CS" width="100"  >第二天第二次低潮潮时</th>
                        <th field="SD_DC2_CG" width="110">第二天第二次低潮潮高</th>
                        <th field="SD_DC3_CS" width="100"  >第二天第三次低潮潮时</th>
                        <th field="SD_DC3_CG" width="110">第二天第三次低潮潮高</th>
                        <th field="TD_GC1_CS" width="100"  >第三天第一次高潮潮时</th>
                        <th field="TD_GC1_CG" width="110">第三天第一次高潮潮高</th>
                        <th field="TD_GC2_CS" width="100"  >第三天第二次高潮潮时</th>
                        <th field="TD_GC2_CG" width="110">第三天第二次高潮潮高</th>
                        <th field="TD_GC3_CS" width="100"  >第三天第三次高潮潮时</th>
                        <th field="TD_GC3_CG" width="110">第三天第三次高潮潮高</th>
                        <th field="TD_DC1_CS" width="100"  >第三天第一次低潮潮时</th>
                        <th field="TD_DC1_CG" width="110">第三天第一次低潮潮高</th>
                        <th field="TD_DC2_CS" width="100"  >第三天第二次低潮潮时</th>
                        <th field="TD_DC2_CG" width="110">第三天第二次低潮潮高</th>
                        <th field="TD_DC3_CS" width="100"  >第三天第三次低潮潮时</th>
                        <th field="TD_DC3_CG" width="110">第三天第三次低潮潮高</th>
                        <th field="LG_DATA1" width="110">浪高数据1</th>
                        <th field="LG_DATA2" width="100"  >浪高数据2</th>
                        <th field="LG_DATA3" width="110">浪高数据3</th>
                        <th field="LG_DATA4" width="100"  >浪高数据4</th>
                        <th field="LG_DATA5" width="110">浪高数据5</th>
                        <th field="LG_DATA6" width="100"  >浪高数据6</th>
                        <th field="DT1" width="110">第一列时间</th>
                        <th field="SW_DATA1" width="100"  >水温数据</th>
                        <th field="DT2" width="110">第二列时间</th>
                        <th field="SW_DATA2" width="100"  >水温数据</th>
                        <th field="DT3" width="110">第三列时间</th>
                        <th field="SW_DATA3" width="110">水温数据</th>
                        <th field="LINKMAN" width="100"  >联系人</th>
                        <th field="LINKPHONE" width="110">联系人电话</th>

                    </tr>
                </thead>
            </table>
            <script>
                function dlg_Submit()
                {
                    $(function () {
                        $('#dg').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', getData());
                    });
                }
                 
                function getData() {
                   // var datetime = $("#cc").val();
                    // alert(datetime);
                    // var datetime = new datetime();
                    var time = $("#txtcx").datebox('getValue');
                    var rows = [];
                    if (time == null||time=="") {
                       
                        $.ajax({
                            async: false,
                            url: "../Ajax/JXHYBCX.ashx?method=getbyDate",
                            success: function (data) {
                               
                                var resjson = JSON.parse(data);

                                for (var i = 0; i < resjson.length; i++) {
                                    // alert(resjson[i].DT1);                              
                                    rows.push({
                                        FILENAME: resjson[i].FILENAME,
                                        PUBLISHDATE: resjson[i].PUBLISHDATE,
                                        TARGETTYPE: resjson[i].TARGETTYPE,
                                        TARGETNAME: resjson[i].TARGETNAME,
                                        FILENUMBER: resjson[i].FILENUMBER,
                                        FD_GC1_CS: resjson[i].FD_GC1_CS,

                                        FD_GC1_CG: resjson[i].FD_GC1_CG,
                                        FD_GC2_CS: resjson[i].FD_GC2_CS,
                                        FD_GC2_CG: resjson[i].FD_GC2_CG,
                                        FD_GC3_CS: resjson[i].FD_GC3_CS,
                                        FD_GC3_CG: resjson[i].FD_GC3_CG,
                                        FD_GC1_CS: resjson[i].FD_GC1_CS,
                                        FD_DC1_CS: resjson[i].FD_DC1_CS,
                                        FD_DC1_CG: resjson[i].FD_DC1_CG,
                                        FD_DC2_CS: resjson[i].FD_DC2_CS,
                                        FD_DC2_CG: resjson[i].FD_DC2_CG,
                                        FD_DC3_CS: resjson[i].FD_DC3_CS,
                                        SD_GC1_CS: resjson[i].SD_GC1_CS,
                                        SD_GC1_CG: resjson[i].SD_GC1_CG,
                                        SD_GC2_CS: resjson[i].SD_GC2_CS,
                                        SD_GC2_CG: resjson[i].SD_GC2_CG,
                                        SD_GC3_CS: resjson[i].SD_GC3_CS,
                                        SD_GC3_CG: resjson[i].SD_GC3_CG,
                                        SD_DC1_CS: resjson[i].SD_DC1_CS,
                                        SD_DC1_CG: resjson[i].SD_DC1_CG,
                                        SD_DC2_CS: resjson[i].SD_DC2_CS,
                                        SD_DC2_CG: resjson[i].SD_DC2_CG,
                                        SD_DC3_CS: resjson[i].SD_DC3_CS,
                                        SD_DC3_CG: resjson[i].SD_DC3_CG,
                                        TD_GC1_CS: resjson[i].TD_GC1_CS,
                                        TD_GC1_CG: resjson[i].TD_GC1_CG,
                                        TD_GC2_CS: resjson[i].TD_GC2_CS,
                                        TD_GC2_CG: resjson[i].TD_GC2_CG,
                                        TD_GC3_CS: resjson[i].TD_GC3_CS,
                                        TD_GC3_CG: resjson[i].TD_GC3_CG,
                                        TD_DC1_CS: resjson[i].TD_DC1_CS,
                                        TD_DC1_CG: resjson[i].TD_DC1_CG,
                                        TD_DC2_CS: resjson[i].TD_DC2_CS,
                                        TD_DC2_CG: resjson[i].TD_DC2_CG,
                                        TD_DC3_CS: resjson[i].TD_DC3_CS,
                                        TD_DC3_CG: resjson[i].TD_DC3_CG,
                                        LG_DATA1: resjson[i].LG_DATA1,
                                        LG_DATA2: resjson[i].LG_DATA2,
                                        LG_DATA3: resjson[i].LG_DATA3,
                                        LG_DATA4: resjson[i].LG_DATA4,
                                        LG_DATA5: resjson[i].LG_DATA5,
                                        LG_DATA6: resjson[i].LG_DATA6,
                                        DT1: resjson[i].DT1,
                                        SW_DATA1: resjson[i].SW_DATA1,
                                        DT2: resjson[i].DT2,
                                        SW_DATA2: resjson[i].SW_DATA2,
                                        DT3: resjson[i].DT3,
                                        SW_DATA3: resjson[i].SW_DATA3,
                                        LINKMAN: resjson[i].LINKMAN,
                                        LINKPHONE: resjson[i].LINKPHONE,
                                    });
                                }
                            }, error: function (XMLHttpRequest, textStatus, errorThrown) {

                            }
                        });
                    }
                    else
                    {
                       
                        $.ajax({
                            async: false,
                            url: "../Ajax/JXHYBCX.ashx?method=getbyDateTime&datetime=" + time,
                            success: function (data) {
                              
                                var resjson = JSON.parse(data);

                                for (var i = 0; i < resjson.length; i++) {

                                    rows.push({
                                        FILENAME: resjson[i].FILENAME,

                                        PUBLISHDATE: resjson[i].PUBLISHDATE,
                                        TARGETTYPE: resjson[i].TARGETTYPE,
                                        TARGETNAME: resjson[i].TARGETNAME,
                                        FILENUMBER: resjson[i].FILENUMBER,
                                        FD_GC1_CS: resjson[i].FD_GC1_CS,

                                        FD_GC1_CG: resjson[i].FD_GC1_CG,
                                        FD_GC2_CS: resjson[i].FD_GC2_CS,
                                        FD_GC2_CG: resjson[i].FD_GC2_CG,
                                        FD_GC3_CS: resjson[i].FD_GC3_CS,
                                        FD_GC3_CG: resjson[i].FD_GC3_CG,
                                        FD_GC1_CS: resjson[i].FD_GC1_CS,
                                        FD_DC1_CS: resjson[i].FD_DC1_CS,
                                        FD_DC1_CG: resjson[i].FD_DC1_CG,
                                        FD_DC2_CS: resjson[i].FD_DC2_CS,
                                        FD_DC2_CG: resjson[i].FD_DC2_CG,
                                        FD_DC3_CS: resjson[i].FD_DC3_CS,
                                        SD_GC1_CS: resjson[i].SD_GC1_CS,
                                        SD_GC1_CG: resjson[i].SD_GC1_CG,
                                        SD_GC2_CS: resjson[i].SD_GC2_CS,
                                        SD_GC2_CG: resjson[i].SD_GC2_CG,
                                        SD_GC3_CS: resjson[i].SD_GC3_CS,
                                        SD_GC3_CG: resjson[i].SD_GC3_CG,
                                        SD_DC1_CS: resjson[i].SD_DC1_CS,
                                        SD_DC1_CG: resjson[i].SD_DC1_CG,
                                        SD_DC2_CS: resjson[i].SD_DC2_CS,
                                        SD_DC2_CG: resjson[i].SD_DC2_CG,
                                        SD_DC3_CS: resjson[i].SD_DC3_CS,
                                        SD_DC3_CG: resjson[i].SD_DC3_CG,
                                        TD_GC1_CS: resjson[i].TD_GC1_CS,
                                        TD_GC1_CG: resjson[i].TD_GC1_CG,
                                        TD_GC2_CS: resjson[i].TD_GC2_CS,
                                        TD_GC2_CG: resjson[i].TD_GC2_CG,
                                        TD_GC3_CS: resjson[i].TD_GC3_CS,
                                        TD_GC3_CG: resjson[i].TD_GC3_CG,
                                        TD_DC1_CS: resjson[i].TD_DC1_CS,
                                        TD_DC1_CG: resjson[i].TD_DC1_CG,
                                        TD_DC2_CS: resjson[i].TD_DC2_CS,
                                        TD_DC2_CG: resjson[i].TD_DC2_CG,
                                        TD_DC3_CS: resjson[i].TD_DC3_CS,
                                        TD_DC3_CG: resjson[i].TD_DC3_CG,
                                        LG_DATA1: resjson[i].LG_DATA1,
                                        LG_DATA2: resjson[i].LG_DATA2,
                                        LG_DATA3: resjson[i].LG_DATA3,
                                        LG_DATA4: resjson[i].LG_DATA4,
                                        LG_DATA5: resjson[i].LG_DATA5,
                                        LG_DATA6: resjson[i].LG_DATA6,
                                        DT1: resjson[i].DT1,
                                        SW_DATA1: resjson[i].SW_DATA1,
                                        DT2: resjson[i].DT2,
                                        SW_DATA2: resjson[i].SW_DATA2,
                                        DT3: resjson[i].DT3,
                                        SW_DATA3: resjson[i].SW_DATA3,
                                        LINKMAN: resjson[i].LINKMAN,
                                        LINKPHONE: resjson[i].LINKPHONE,
                                    });

                                }
                            }, error: function (XMLHttpRequest, textStatus, errorThrown) {

                            }
                        });

                    }
                    return rows;
                }

                function pagerFilter(data) {
                    if (typeof data.length == 'number' && typeof data.splice == 'function') {	// is array
                        data = {
                            total: data.length,
                            rows: data
                        }
                    }
                    var dg = $(this);
                    var opts = dg.datagrid('options');
                    var pager = dg.datagrid('getPager');
                    pager.pagination({
                        onSelectPage: function (pageNum, pageSize) {
                            opts.pageNumber = pageNum;
                            opts.pageSize = pageSize;
                            pager.pagination('refresh', {
                                pageNumber: pageNum,
                                pageSize: pageSize
                            });
                            dg.datagrid('loadData', data);
                        }
                    });
                    if (!data.originalRows) {
                        data.originalRows = (data.rows);
                    }
                    var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
                    var end = start + parseInt(opts.pageSize);
                    data.rows = (data.originalRows.slice(start, end));
                    return data;
                }
                
                $(function () {
                    $('#dg').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', getData());
                });
            </script>


        </div>
    </form>
</body>
</html>

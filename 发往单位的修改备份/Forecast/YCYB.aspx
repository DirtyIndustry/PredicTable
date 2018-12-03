<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YCYB.aspx.cs" Inherits="PredicTable.Forecast.YCYB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

     <link href="../EasyUI/themes/metro/easyui.css" rel="stylesheet" />
    <link href="../EasyUI/themes/icon.css" rel="stylesheet" />
    <script src="../EasyUI/jquery.min.js"></script>
    <script src="../EasyUI/jquery.easyui.min.js"></script>
    
    <link href="../Css/StyleSheet_YC.css" rel="stylesheet" />

    

   
</head>
<body>
    <form id="form1" runat="server">
      <div id="head">
            <div id="ybfbsj">

                <label>海浪预报发布时间：</label>
                <input class="easyui-datetimebox" data-options="sharedCalendar:'#cc'" id="txtcx">
                &nbsp;&nbsp;&nbsp;&nbsp; <input  type="button"  onclick="dlg_Submit()" value="查询" />
                <div id="cc" class="easyui-calendar">
                </div>

            </div>
        </div>
        <div id="table">
            <table id="dg" title="海浪预报数据" style="height:350px" data-options="
				rownumbers:true,
				singleSelect:true,
				autoRowHeight:false,
				pagination:true,
				pageSize:10">
                <thead>
                    <tr>
                        <th field="FILENAME" width="80">文件名</th>
                        <th field="REGION" width="100">预报区域</th>
                        <th field="FILETYPE" width="80">预报类型</th>
                        <th field="PUBLISHDATA" width="180"  >预报时间</th>
                        <th field="FLAG" width="130"  >渔场预报标识符</th>

                        <th field="FISHINGGROUNDID" width="100"  >渔场编号</th>
                        <th field="FISHINGGROUNDNAME" width="110">渔场名称</th>
                        <th field="EFFECTIVETIME" width="100" >预报时效</th>
                        <th field="EFFECTIVEWAVEHEIGHT1" width="110">有效波高1</th>
                        <th field="TREND" width="100" >趋势</th>
                        <th field="EFFECTIVEWAVEHEIGHT2" width="110">有效波高2</th>
                      
                    </tr>
                </thead>
            </table>

              <div id="head1">
            <div id="ycyb">

                <label>风预报发布时间：</label>
                <input class="easyui-datetimebox" data-options="sharedCalendar:'#ccc'" id="txtcxX">
                &nbsp;&nbsp;&nbsp;&nbsp; <input  type="button"  onclick="dlg_Submit1()" value="查询" />
                <div id="ccc" class="easyui-calendar">
                </div>

            </div>
        </div>
        <div id="table1">
            <table id="dg1" title="风预报数据" style="min-width: 900px; height:350px" data-options="
				rownumbers:true,
				singleSelect:true,
				autoRowHeight:false,
				pagination:true,
				pageSize:10">
                <thead>
                    <tr>
                      <th field="FILENAME" width="80">文件名</th>
                        <th field="REGION" width="100">预报区域</th>
                        <th field="FILETYPE" width="80">预报类型</th>
                        <th field="PUBLISHDATA" width="180"  >预报时间</th>
                        <th field="FLAG" width="130"  >渔场预报标识符</th>


                        <th field="FISHINGGROUNDID" width="100"  >渔场编号</th>
                        <th field="FISHINGGROUNDNAME" width="110">渔场名称</th>
                        <th field="EFFECTIVETIME" width="100"  >预报时效</th>
                        <th field="WINDDIRECTION" width="110">风向</th>
                        <th field="WINDFORCE" width="100"  >风力（级）</th>
                        <th field="TREND" width="110">趋势</th>
                      
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
                function dlg_Submit1() {
                    $(function () {
                        $('#dg1').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', getData1());
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
                            url: "../Ajax/YCYB.ashx?method=getbyALLDate_YCYB_WAVE",
                            success: function (data) {
                                // alert(data);
                                var resjson = JSON.parse(data);

                                for (var i = 0; i < resjson.length; i++) {
                                   //  alert(resjson[i].DT1);                              
                                    rows.push({
                                        FILENAME: resjson[i].FILENAME,
                                        REGION: resjson[i].REGION,
                                        FILETYPE: resjson[i].FILETYPE,
                                        PUBLISHDATA: resjson[i].PUBLISHDATA,
                                        FLAG: resjson[i].FLAG,
                                        FISHINGGROUNDID: resjson[i].FISHINGGROUNDID,

                                        FISHINGGROUNDNAME: resjson[i].FISHINGGROUNDNAME,
                                        EFFECTIVETIME: resjson[i].EFFECTIVETIME,
                                        EFFECTIVEWAVEHEIGHT1: resjson[i].EFFECTIVEWAVEHEIGHT1,
                                        TREND: resjson[i].TREND,
                                        EFFECTIVEWAVEHEIGHT2: resjson[i].EFFECTIVEWAVEHEIGHT2,                                      
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
                            url: "../Ajax/YCYB.ashx?method=getbyDate_YCYB_WAVE&datetime=" + time,
                            success: function (data) {

                                var resjson = JSON.parse(data);

                                for (var i = 0; i < resjson.length; i++) {

                                    rows.push({
                                        FILENAME: resjson[i].FILENAME,
                                        REGION: resjson[i].REGION,
                                        FILETYPE: resjson[i].FILETYPE,
                                        PUBLISHDATA: resjson[i].PUBLISHDATA,
                                        FLAG: resjson[i].FLAG,
                                        FISHINGGROUNDID: resjson[i].FISHINGGROUNDID,

                                        FISHINGGROUNDNAME: resjson[i].FISHINGGROUNDNAME,
                                        EFFECTIVETIME: resjson[i].EFFECTIVETIME,
                                        EFFECTIVEWAVEHEIGHT1: resjson[i].EFFECTIVEWAVEHEIGHT1,
                                        TREND: resjson[i].TREND,
                                        EFFECTIVEWAVEHEIGHT2: resjson[i].EFFECTIVEWAVEHEIGHT2,
                                    });

                                }
                            }, error: function (XMLHttpRequest, textStatus, errorThrown) {

                            }
                        });

                    }
                    return rows;
                }


                function getData1() {
                    // var datetime = $("#cc").val();
                    // alert(datetime);
                    // var datetime = new datetime();
                    var time = $("#txtcxX").datebox('getValue');
                    var rows = [];
                    if (time == null || time == "") {

                        $.ajax({
                            async: false,
                            url: "../Ajax/YCYB.ashx?method=getbyALLDate_YCYB_WIND",
                            success: function (data) {
                                // alert(data);
                                var resjson = JSON.parse(data);

                                for (var i = 0; i < resjson.length; i++) {
                                    // alert(resjson[i].DT1);                              
                                    rows.push({
                                        FILENAME: resjson[i].FILENAME,
                                        REGION: resjson[i].REGION,
                                        FILETYPE: resjson[i].FILETYPE,
                                        PUBLISHDATA: resjson[i].PUBLISHDATA,
                                        FLAG: resjson[i].FLAG,

                                        FISHINGGROUNDID: resjson[i].FISHINGGROUNDID,
                                        FISHINGGROUNDNAME: resjson[i].FISHINGGROUNDNAME,
                                        EFFECTIVETIME: resjson[i].EFFECTIVETIME,
                                        WINDDIRECTION: resjson[i].WINDDIRECTION,
                                        WINDFORCE: resjson[i].WINDFORCE,
                                        TREND: resjson[i].TREND,
                                      
                                    });
                                }
                            }, error: function (XMLHttpRequest, textStatus, errorThrown) {

                            }
                        });
                    }
                    else {

                        $.ajax({
                            async: false,
                            url: "../Ajax/YCYB.ashx?method=getbyDate_YCYB_WIND&datetime=" + time,
                            success: function (data) {

                                var resjson = JSON.parse(data);

                                for (var i = 0; i < resjson.length; i++) {

                                    rows.push({
                                        FILENAME: resjson[i].FILENAME,
                                        REGION: resjson[i].REGION,
                                        FILETYPE: resjson[i].FILETYPE,
                                        PUBLISHDATA: resjson[i].PUBLISHDATA,
                                        FLAG: resjson[i].FLAG,

                                        FISHINGGROUNDID: resjson[i].FISHINGGROUNDID,
                                        FISHINGGROUNDNAME: resjson[i].FISHINGGROUNDNAME,
                                        EFFECTIVETIME: resjson[i].EFFECTIVETIME,
                                        WINDDIRECTION: resjson[i].WINDDIRECTION,
                                        WINDFORCE: resjson[i].WINDFORCE,
                                        TREND: resjson[i].TREND,
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
                $(function () {
                    $('#dg1').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', getData1());
                });
            </script>


        </div>
    </form>
</body>
</html>

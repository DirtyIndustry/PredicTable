<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckTideData.aspx.cs" Inherits="PredicTable.CheckTideData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="css/style.default.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
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
</head>
<body class="withvernav">
    <form runat="server" name="upfrom" class="stdform stdform2" method="post" action="" style="width: 95%" enctype="multipart/form-data ">
        <div style="padding: 10px;">
            <table id="dg" title="天文潮数据查看" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%" url="/Ajax/CheckTideData.ashx?method=getdata" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
                <thead>
                    <tr>
                        <th field="STATION" width="10%">站点</th>
                        <th field="PREDICTIONDATE" width="10%">日期</th>
                        <th field="FSTHIGHWIDETIME" width="10%">第一次高潮时</th>
                        <th field="FSTHIGHWIDEHEIGHT" width="10%">第一次高潮高</th>
                        <th field="FSTLOWWIDETIME" width="10%">第一次低潮时</th>
                        <th field="FSTLOWWIDEHEIGHT" width="10%">第一次低潮高</th>
                        <th field="SCDHIGHWIDETIME" width="10%">第二次高潮时</th>
                        <th field="SCDHIGHWIDEHEIGHT" width="10%">第二次高潮高</th>
                        <th field="SCDLOWWIDETIME" width="10%">第二次低潮时</th>
                        <th field="SCDLOWWIDEHEIGHT" width="10%">第二次低潮高</th>
                    </tr>
                </thead>
            </table>
            <div id="toolbar">
                &nbsp;时间段:<input id="fistdata" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser"></input>
                --<input id="enddata" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser"></input>
                <script type="text/javascript">
                    //将01/02/2014 转化为 2014-01-02
                    function myformatter(date) {
                        var y = date.getFullYear();
                        var m = date.getMonth() + 1;
                        var d = date.getDate();
                        return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
                    }
                    function myparser(s) {
                        if (!s) return new Date();
                        var ss = (s.split('-'));
                        var y = parseInt(ss[0], 10);
                        var m = parseInt(ss[1], 10);
                        var d = parseInt(ss[2], 10);
                        if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                            return new Date(y, m - 1, d);
                        } else {
                            return new Date();
                        }
                    }
                </script>
                &nbsp; <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchs()">查询</a>






            </div>
        </div>



    </form>
    <script>
           
        //根据条件查询 把参数post给后台 ashx页面
        function searchs() {

            var fdata = $('#fistdata').datebox('getValue');
            var ldata = $('#enddata').datebox('getValue');
            if (fdata != "" && ldata != "") {
                if (isDate(fdata) && isDate(ldata)) {
                    if (!checkdate(fdata, ldata)) {
                        //时间区间不正确
                        $.messager.show({
                            title: '错误',
                            msg: "时间区间不正确"
                        });
                        return;
                    }
                }
                else {
                    //时间格式不正确
                    $.messager.show({
                        title: '错误',
                        msg: "时间格式不正确",
                    });
                    return;
                }
            }
            var queryParams = $('#dg').datagrid('options').queryParams;
            queryParams.firstdata = fdata;//开始时间
            queryParams.enddata = ldata;//结束时加
            $('#dg').datagrid({
                url: '/Ajax/CheckTideData.ashx?method=getbywhere'
            });
            //重新加载datagrid的数据  
            //   $("#dg").datagrid('reload');
        }
          
        //判断时间是否符合要求
        function isDate(dateString) {
            if (dateString.trim() == "") return true;
            var r = dateString.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
            if (r == null) {
                // alert("请输入格式正确的日期\n\r日期格式：yyyy-mm-dd\n\r例    如：2008-08-08\n\r");
                return false;
            }
            var d = new Date(r[1], r[3] - 1, r[4]);
            var num = (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4]);
            if (num == 0) {
                return false;
                // alert("请输入格式正确的日期\n\r日期格式：yyyy-mm-dd\n\r例    如：2008-08-08\n\r");
            }
            return (num != 0);
        }//判断时间是否符合要求
        //判断日期大小
        function checkdate(fdata, ldata) {
            var sDate = new Date(fdata.replace("-", "//"));
            var eDate = new Date(ldata.replace("-", "//"));
            if (sDate > eDate) {
                return false;
            } else {
                return true;
            }
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

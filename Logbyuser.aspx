<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logbyuser.aspx.cs" Inherits="PredicTable.Logbyuser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台管理系统</title>

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
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="js/plugins/excanvas.min.js"></script><![endif]-->
    <!--[if IE 9]>
    <link rel="stylesheet" media="screen" href="css/style.ie9.css"/>
<![endif]-->
    <!--[if IE 8]>
    <link rel="stylesheet" media="screen" href="css/style.ie8.css"/>
<![endif]-->
    <!--[if lt IE 9]>
	<script src="js/plugins/css3-mediaqueries.js"></script>
<![endif]-->

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/demo/demo.css" />

    <script type="text/javascript" src="/Scripts/jquery-1.10.2.min.js"></script>

    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>
<%--    <script src="js/crossdomain.js"></script>--%>






</head>
<body class="withvernav">
    <script>
        //$(function () {
        //    getuserinfo("-1");
        //})
    </script>
     
    <div <%--class="centercontent"--%> style="padding: 10px">
        <table id="dg" title="当前操作日志" class="easyui-datagrid" style="width: 100%; min-height:400px; height: 100%" url="/Ajax/RiZhiManage.ashx?id=getdata" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="yhzh" width="50">用户账号</th>
                    <th field="yhmc" width="50">用户名称</th>
                    <th field="czsj" width="80">操作时间</th>
                    <th field="zldm" width="60">指令代码</th>
                    <th field="xxsm" width="200">详细说明</th>
                    
                </tr>
            </thead>
        </table>
        <div id="toolbar">
         &nbsp;时间段:<input  id="fistdata" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser"></input>
	 --<input id="enddata" class="easyui-datebox"  data-options="formatter:myformatter,parser:myparser"></input>
	<script type="text/javascript">
        //将01/02/2014 转化为 2014-01-02
		function myformatter(date){
			var y = date.getFullYear();
			var m = date.getMonth()+1;
			var d = date.getDate();
			return y+'-'+(m<10?('0'+m):m)+'-'+(d<10?('0'+d):d);
		}
		function myparser(s){
			if (!s) return new Date();
			var ss = (s.split('-'));
			var y = parseInt(ss[0],10);
			var m = parseInt(ss[1],10);
			var d = parseInt(ss[2],10);
			if (!isNaN(y) && !isNaN(m) && !isNaN(d)){
				return new Date(y,m-1,d);
			} else {
				return new Date();
			}
		}

       
	</script>
        &nbsp;类型:<select  editable="false" id="cmb_leixing" class="easyui-combobox" name="state" style="width:200px;">
            <option value="">--请选择--</option>
            <option value="Error">系统错误</option>
            <option value="add_table">添加表单</option>
            <option value="edit_table">修改表单</option>
            <option value="release_table">发布表单</option>
           
            
                   </select>
      <%-- &nbsp; 用户名:<input id="userid" class="easyui-textbox" >--%>
            &nbsp; <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchs()">筛选</a>
           <%-- <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUser()">更改部门</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyUser()">删除部门</a>--%>
        </div>

         
    </div>
    <script type="text/javascript">

        var url;
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
                else
                {
                    //时间格式不正确
                    $.messager.show({
                        title: '错误',
                        msg: "时间格式不正确"
                    });
                    return;
                }
            }
             var queryParams = $('#dg').datagrid('options').queryParams;
             queryParams.firstdata = fdata;//开始时间
             queryParams.enddata = ldata;//结束时加
            //  queryParams.type = "table";//类型
             queryParams.type = $('#cmb_leixing').combobox('getValue');//类型
             //queryParams.userid = userid;//用户账号
            
             $('#dg').datagrid({
                 url: '/Ajax/RiZhiManage.ashx?id=getbywhere'
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
        }

        //判断日期大小
        function checkdate(fdata,ldata)
        {   
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

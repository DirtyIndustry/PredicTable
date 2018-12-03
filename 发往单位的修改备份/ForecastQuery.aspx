<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForecastQuery.aspx.cs" Inherits="PredicTable.ForecastQuery" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228" Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>短期预报单查询</title>
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
    <script type="text/javascript">
        $(function () {
            $('#dlg_show').dialog('close');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px">
        <table id="dg" title="短期预报表单查询" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%" url="/Ajax/gettablequery.ashx?method=getdata" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                     <th data-options="field:'ck',checkbox:true"></th> 
                     <th field="id" width="50" style="display:none">编号</th>
                     <th field="wjm" width="150">文件名</th>
                     <th field="wjdx" width="50">文件大小</th>
                     <th field="sczz" width="100">上传作者</th>
                     <th field="scsj" width="150">上传时间</th>
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
            
     &nbsp; <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchs()">查询</a>
     &nbsp; <%--<a href="javascript:void(0)" class="easyui-linkbutton"  plain="true" onclick="downloads()">下载</a>--%>
            <asp:LinkButton ID="lbtnDownLoad" class="easyui-linkbutton"  plain="true" runat="server"  OnClientClick="javascript:return downloads()" OnClick="lbtnDownLoad_Click">下载</asp:LinkButton>
            <asp:HiddenField ID="hidFileID" runat="server" Value="" />
            <asp:HiddenField ID="hidFileName" runat="server" Value="" />
     &nbsp; <asp:LinkButton ID="ss" class="easyui-linkbutton"  plain="true" runat="server" OnClick="lbtnShow_Click" OnClientClick="javascript:return showdoc()">预览</asp:LinkButton>
            <asp:HiddenField ID="hidRowId" runat="server" Value="" />
     </div>
    </div>
    <div id="dlg_show" onload="" class="easyui-dialog" title="模板预览" data-options="iconCls:'icon-save'" style="width: 1067px; ">
        <%--<iframe width="100%" id="show"    height="710" name="show" frameborder="0" src=""></iframe>--%>
        <div class="contenttitle2" style="z-index: 1000;">
            <h3 id="mbcz">编写预报文档（ctrl+s保存）</h3>
        </div>
        <div runat="server" id="Div1" style="height: 840px">
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" Menubar="False" Titlebar="False"></po:PageOfficeCtrl>
        </div>
        <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="cancel()" value="取消" />
    </div>     
        <script>
            $(function () {
                $('#dlg_show').dialog('close');
                $(".window").css("top", "50px");
                $(".window-shadow").css("background", "transparent");
                $(".window-shadow").css("top", "50px");
            });
            function cancel() {
                $("#PageOfficeCtrl1").visible = false;
                $('#dlg_show').dialog('close');
            }
        //下载
        function downloads() {
           
            //根据id 每次请求下载
            var ss = [];
            var rows = $('#dg').datagrid('getChecked');

            if (rows.length <= 0) {
                //请选择要下载的文件
                $.messager.show({
                    title: '选择',
                    msg: "请选择要下载的文件"
                });
                return false;
            } else {
                var ids = "";
                var names = "";
            for (var i = 0; i < rows.length; i++) {
                ids += rows[i].id + ",";
                names += rows[i].wjm+","
            }
            $("#hidFileID").val(ids.substring(0, ids.length - 1));
            $("#hidFileName").val(names.substring(0, names.length - 1));
            return true;
            //ajaxbyid(ids.substring(0, ids.length - 1), names.substring(0, names.length - 1));
            }
        }

        //请求 预览
        function showdoc() {
            //根据id 每次请求预览
            var ss = [];
            var rows = $('#dg').datagrid('getChecked');

            if (rows.length <= 0) {
                //请选择要预览的文件
                $.messager.show({
                    title: '选择',
                    msg: "请选择要预览的文件"
                });
                return false;
            } else {
                var rowId = rows[0].id;
                $("#hidRowId").val(rowId);
                return true;
                //showbyid(rows[0].id);
            }
        }

        //请求
        function ajaxbyid(id,name) {
            //location.href = '/Ajax/gettablequery.ashx?method=getbyid&id=' + id;
            $.ajax({
                type: "POST",
                url: '/Ajax/gettablequery.ashx?method=getbyid&id=' + id,
                success: function (result) {
                    if (result == "success") {
                        $.messager.show({
                            title: '预报单下载',
                            msg: id + "号预报单(" + name + ")下载成功",
                            height: '150px',
                            width: '350px'
                        });
                        return;
                    }
                    else {
                        $.messager.show({
                            title: '预报单下载',
                            msg: id + "号预报单(" + name + ")下载失败",
                            height: '150px',
                            width:'350px'
                        });
                    }
                }
            });
        }

        //请求2
        function showbyid(id) {
            
            $.ajax({
                type: "POST",
                url: "/Ajax/gettablequery.ashx?method=showbyid&id=" +id,
                //beforeSend: function () {
                //  //  $('#w').window('open');
                //},
                success: function (result) {
                   
                    $('#dlg_show').dialog('open');
                    $('#show').attr("src", "/download/show.html");
                } 
                //complete: function () {
                //  //  $('#w').window('close');
                //}
            });
        }

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
                url: '/Ajax/gettablequery.ashx?method=getbywhere'
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
    </form>
</body>
</html>


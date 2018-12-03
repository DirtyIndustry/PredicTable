<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableQuery.aspx.cs" Inherits="PredicTable.TableQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>报表查询</title>
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




    <!--[if IE 9]>
    <link rel="stylesheet" media="screen" href="css/style.ie9.css"/>
<![endif]-->
    <!--[if IE 8]>
    <link rel="stylesheet" media="screen" href="css/style.ie8.css"/>
<![endif]-->
    <!--[if lt IE 9]>
	<script src="js/plugins/css3-mediaqueries.js"></script>
<![endif]-->
</head>
<body>
   <script>
       $(function () {
           $('#dg').datagrid({ selectOnCheck: $(this).is(':checked') });
           $('#dlg_show').dialog('close');
           // $('#dg').datagrid({ singleSelect: 1 });
         //  alert("d");
       })

   </script>
     <div <%--class="centercontent"--%> style="padding: 10px">
        
        <table id="dg" title="预报表单查询" class="easyui-datagrid" style="width: 100%; min-height: 700px; height: 100%" url="/Ajax/gettablequery.ashx?method=getdata" toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                     <th data-options="field:'ck',checkbox:true"></th> 
                   <%--	<th  width="20" data-options="field:'status',width:60,align:'center',editor:{type:'checkbox',options:{on:'P',off:''}}">Status</th>--%>
                
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
              &nbsp;  <a href="javascript:void(0)" class="easyui-linkbutton"  plain="true" onclick="downloads()">下载</a>
              &nbsp;  <a href="javascript:void(0)" class="easyui-linkbutton"  plain="true" onclick="showdoc()">预览</a>
           <%-- <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUser()">更改部门</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyUser()">删除部门</a>--%>
        
             </div>

      
    </div>
                    <div id="dlg_show" onload="" class="easyui-dialog" title="模板预览" data-options="iconCls:'icon-save'" style="width: 705px; ">
                     <iframe width="100%" id="show"    height="710" name="show" frameborder="0" src=""></iframe>
                     <input style="float: right; margin: 3px 5px 0px 0px" type="button" class="stdbtn" onclick="$('#dlg_show').dialog('close'); " value="取消" />
                         </div>     
    <script>
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
            } else {
                var ids = "";
            for (var i = 0; i < rows.length; i++) {
                ids+=rows[i].id+",";
            }
            ajaxbyid(ids.substring(0,ids.length-1));
            }
          //  $.messager.alert('Info', ss.join('<br/>'));
           
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
            } else {
               // ajaxbyid(rows[0].id);
                showbyid(rows[0].id);
              //  location.href = '/Ajax/gettablequery.ashx?method=showbyid&id=' + rows[0].id;
            }
            //  $.messager.alert('Info', ss.join('<br/>'));
        }

        //请求
        function ajaxbyid(id) {
          
            //$.ajax({
            //    type: "POST",
            //    url: "/Ajax/gettablequery.ashx?method=getbyid&id=" + id,
            //    beforeSend: function () {
            //      //  $('#w').window('open');
            //    },
            //    success: function (result) {
            //        alert(result);
            //    },
            //    complete: function () {
            //      //  $('#w').window('close');
            //    }
            //});

            location.href = '/Ajax/gettablequery.ashx?method=getbyid&id=' + id;

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
                    //location.href = '/download/show.html';
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
                        msg: "时间格式不正确"
                    });
                    return;
                }
            }
            var queryParams = $('#dg').datagrid('options').queryParams;
            queryParams.firstdata = fdata;//开始时间
            queryParams.enddata = ldata;//结束时加
            //queryParams.type = $('#cmb_leixing').combobox('getValue');//类型
            //queryParams.userid = $('#userid').val();//用户账号
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
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="smsiframe.aspx.cs" Inherits="PredicTable.smsiframe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $.ajax({
                type: "GET",
                url: "/Ajax/getSMS.ashx?method=getgroup",
                dataType: "json",
                success: function (data) {
                    var group = data;
                    //var vgroup = [group];
                    genCheck(group);
                },
                error: function () {
                    alert("error");
                }


            });

        });


        function genCheck(vgroup) {
            var content = "content";
            var checkText = "checkbox";
            var link = "link";
            var size;
            //$("#show").html("<span class='bigfont'>选择短信组：</span>");
            //var group = vgroup;
            //size = group.length;
            var group_bh = vgroup.dtBH;
            var group_sd = vgroup.dtSD;
            var len_group_bh = group_bh.length;
            var len_group_sd = group_sd.length;
            var lenId = 0;
            if (group_bh != null && group_bh != "") {
                var divTextbh = "<input type='checkbox' style='width:20px;height:20px;' onclick='checkAll(divgroupBH,this,divgroupSD)' checked='checked'>" + group_bh[0].SMSGROUPNAME + "：</input><BR/><div id='divgroupBH'></div><BR/>";
                $("#show").append(divTextbh);
                for (var j = 0; j < len_group_bh; j++) {
                    genShowContent("divgroupBH", checkText + j, group_bh[j].SMSGROUP, group_bh[j].SMSGROUP, content + j, "checked='checked'");
                }
                
            }
            if (group_sd != null && group_sd != "") {
                lenId = len_group_bh;
                var divTextsd = "<input type='checkbox' style='width:20px;height:20px;' onclick='checkAll(divgroupSD,this,divgroupBH)'>" + group_sd[0].SMSGROUPNAME + "：</input><BR/><div id='divgroupSD'></div><BR/>";
                $("#show").append(divTextsd);
                for (var k = 0; k < len_group_sd ; k++) {
                    lenId++;
                    genShowContent("divgroupSD", checkText + lenId, group_sd[k].SMSGROUP, group_sd[k].SMSGROUP, content + lenId,"");
                }
                
            }
        }
        function checkAll(divId, obj, divId2) {
            $(obj).prop("checked", true).siblings().prop("checked", false);
            $("#" + divId.id + " input:checkbox").each(function () {
                $(this).prop("checked", true);
                //if ($(obj).is(":checked")) {
                //    $(this).prop("checked", true);
                //}
                //else {
                //    $(this).prop("checked", false);
                //}
            });
            $("#" + divId2.id + " input:checkbox").each(function () {
                $(this).prop("checked", false);
            });
        }

        function genShowContent(id, checkboxId, index, showText, idName,isChecked) {
            var checkbox = "<input type='checkbox' style='width:20px;height:20px;' name='checkboxTest' " + isChecked + " value='" + showText + "'>" + showText + "</input>";
            $("#" + id).append(checkbox);
        }

        //获取被选择的短信组
        function dosomething(value) {

            $("input[type='checkbox']").click(function () {
                if ($("input[type='checkbox']:checked").length > 0) {
                    var grouplist = " ";
                    for (var i = 0; i < $("input[type='checkbox']:checked").length; i++) {
                        grouplist += $("input[type='checkbox']:checked")[i].value + ";";
                    }
                    grouplist = grouplist.substring(grouplist.length - 1, 1);
                    $('#group').val(grouplist);

                }
            })
        }
        function GetGroup() {
            var parentDiv = "";
            var grouplist = " ";
            var len = $("[name='checkboxTest']").length;
            if (len > 0) {
                var objs = $("[name='checkboxTest']");
                for (var i = 0; i < len; i++) {
                    if ($(objs[i]).is(":checked")) {
                        grouplist += objs[i].value + ";";
                        var div = objs[i].parentElement.id;
                        $('#parentDiv').val(div);
                    }
                }
            }
            grouplist = grouplist.substring(grouplist.length - 1, 1);
            $('#group').val(grouplist);
            return true;
        }
    </script>
    <style type="text/css" >
        #DXButton2{
            width:129px;
            height:42px;
        }
    </style>
</head>
<body>
     <form id="form1" runat="server">
        <div style="text-align:center;">
            <textarea runat="server" style="width: 90%;" id="duanxin" name="duanxin" cols="20" maxlength="2000" rows="7"></textarea>

            <br />
            <br />
            文档类型:<select id="DOCTYPEdx" runat="server" style="width:100px;">
                <option value="预警">预警</option>
                <option value="预报">预报</option>
            </select>&nbsp;&nbsp;
                                    <br />
            <br />
            <div class="show" id="show" style="text-align:left;padding-left:40px;"></div>
            <input runat="server" id="group" name="group" size="50" type="hidden" />
            <input runat="server" id="parentDiv" name="group" size="50" type="hidden" />
            <asp:Button runat="server" Text="提交数据" ID="DXButton2" OnClick="DXButton2_Click" Font-Size="15"></asp:Button>

        </div>
    </form>
</body>
</html>

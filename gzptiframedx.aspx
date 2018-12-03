<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gzptiframedx.aspx.cs" Inherits="PredicTable.gzptiframedx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="js/plugins/jquery-1.7.min.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $.ajax({
                type: "GET",
                url: "/Ajax/getGZPTDate.ashx?method=getgroup",
                dataType: "json",
                success: function (data) {
                    var group = data;
                    var vgroup = [group];
                    genCheck(vgroup);
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
            $("#show").html("<span class='bigfont'>选择短信组：</span>");
            var group = vgroup[0];
            size = group.length;

            for (var i = 0; i < group.length; i++) {
                genShowContent("show", checkText + i, group[i], group[i], content + i);
            }
        }

        function genShowContent(id, checkboxId, index, showText, idName) {

            var checkbox = "<input type='checkbox' id=".concat(index).concat("  name='checkbox' onchange='dosomething(value)'   value=").concat(index).concat(" alt=").concat(showText).concat(" /><span>").concat(showText).concat("</span>");
            $("#" + id).append(checkbox);
            // alert(checkbox);
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
                    //grouplist = grouplist.substring(0, grouplist.Length - 1);
                    //alert(grouplist1);
                    //alert(grouplist);
                    $('#group').val(grouplist);

                } else {

                }
            })

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <textarea runat="server" style="width: 90%;" id="duanxin" name="duanxin" cols="20" maxlength="2000" rows="7"></textarea>

            <br />
            <br />
            文档类型:<select id="DOCTYPEdx" runat="server">
                <option value="预报">预报</option>
                <option value="预警">预警</option>
            </select>&nbsp;&nbsp;
                                    <br />
            <br />
            <div class="show" id="show"></div>
            <input runat="server" id="group" name="group" size="50" type="hidden" />
            <asp:Button runat="server" Text="提交数据" ID="DXButton" OnClick="DXButton_Click" UseSubmitBehavior="false"></asp:Button>

        </div>
    </form>
</body>
</html>

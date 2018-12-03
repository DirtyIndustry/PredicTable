<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CNModel.ascx.cs" Inherits="PredicTable.Ascx.CNModel" %>
    <input type="button" ID="SaveContent" value="保存中文表单"/>
   <div style="width:650px; height:950px; background-size:100%;overflow:hidden;background-image:url('');" runat="server" id="bg_img">
        <div style="margin-top:190px;margin-left:153px;">
            <input type="text" value="<%=Session["ENPublishTime"] %>" disabled="disabled"  />
        </div>
           <input id="hid_publishTime"  type="hidden" value="" />
            <input id="hid_publishCompanyName"  type="hidden" value="" />
        <div style="margin-top:40px;margin-left:180px;">
           <input type="text" ID="txt_fbtime" style="width:160px" ></input>
        </div>
        <div style="margin-top:47px;margin-left:70px;">
           <textarea ID="atxt_content" style="width:490px;height:433px;" ></textarea>
        </div>
       <div style="margin-top:23px;margin-left:50px;">
           <div style="float:left;margin-left:145px;">
                    <select id="sel_header" class="uniformselect"></select>
            </div>
           <div style="float:right;margin-right:120px;">
                <select id="sel_deputyer" class="uniformselect"></select>
           </div>
       </div>
</div>
<script type="text/javascript">
    $(function () {
        $.ajax({
            type: "GET",
            url: "/Ajax/UpLoadModelInfo.ashx?method=HeadReporter",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    $("#sel_header").empty();
                    var option = "";
                    var first_txt = "";
                    for (var i = 0; i < data.list.length; i++) {
                        option += "<option value=" + data.list[i].REPORTERCODE + ">" + data.list[i].REPORTERNAME + "</option>";
                        first_txt = data.list[0].REPORTERNAME;
                    }
                    $("#uniform-sel_header span").html(first_txt);//更改选中值 ;
                    $("#sel_header").append(option);
                } else {
                    alert("下拉框数据获取失败！");
                }
            }

        });
        $.ajax({
            type: "GET",
            url: "/Ajax/UpLoadModelInfo.ashx?method=DeputyReporter",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    $("#sel_deputyer").empty();
                    var option = "";
                    var first_txt = "";
                    for (var i = 0; i < data.list.length; i++) {
                        option += "<option value=" + data.list[i].REPORTERCODE + ">" + data.list[i].REPORTERNAME + "</option>";
                        first_txt = data.list[0].REPORTERNAME;
                    }
                    $("#uniform-sel_deputyer span").html(first_txt);//更改选中值 ;
                    $("#sel_deputyer").append(option);
                } else {
                    alert("下拉框数据获取失败！");
                }
            }

        });
        $('#SaveContent').click(function () {
            var pbtime, ybtime, ybcontent,publishCompanyName;
            var sendDepartment = "黄岛区";
            var headReporter = $("#sel_header").val();
            var deputyReporter = $("#sel_deputyer").val();

            pbtime = $('#hid_publishTime').val();
            if ($("#txt_fbtime").val() != "") {
                ybtime = $("#txt_fbtime").val();
            } else {
                alert("请填写预报单名称");
                return;
            }
            if ($("#atxt_content").val() != "") {
                ybcontent = $("#atxt_content").val();
            } else {
                alert("请填写预报单内容");
                return;
            }
            publishCompanyName = $('#hid_publishCompanyName').val();
            $.ajax({
                type: "GET",
                url: "/Ajax/UpLoadModelInfo.ashx?method=SaveCNModel&pbtime=" + encodeURI(encodeURI(pbtime)) + "&ybtime=" + encodeURI(encodeURI(ybtime)) + "&ybcontent=" + encodeURI(encodeURI(ybcontent)) + "&headReporter="
                    + encodeURI(encodeURI(headReporter)) + "&deputyReporter=" + encodeURI(encodeURI(deputyReporter)) + "&sendDepartment=" + encodeURI(encodeURI(sendDepartment)) + "&publishCompanyName=" + encodeURI(encodeURI(publishCompanyName)),
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data == "success") {
                        alert("预报单生成成功！");
                    } else {
                        alert("预报单生成失败！");
                    }
                },
                error: function (e) {
                    alert("预报单生成失败！");
                }
            });
        });
    });
</script>
<script type="text/javascript">
    $(function () {
        var fbCompany = $("#hid_publishCompanyName").val();
        var publishTime = $("#hid_publishTime").val();
        $.ajax({
            type: "GET",
            url: "/Ajax/UpLoadModelInfo.ashx?method=CNForecastInfo&publishTime=" + encodeURI(encodeURI(publishTime)) + "&publishCompany=" + encodeURI(encodeURI(fbCompany)),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    $('#txt_fbtime').val(data.list[0].REPORTNAME);
                    $('#atxt_content').val(data.list[0].REPORTCONTENT);
                    if (data.list[0].HEADREPORTER != "" && data.list[0].DEPUTYREPORTER != "") {
                        $("#uniform-sel_header span").html(data.list[0].HEADREPORTER);
                        $("#uniform-sel_deputyer span").html(data.list[0].DEPUTYREPORTER);
                    }
                }
            }
        });
    });
</script>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ENModelDay.ascx.cs" Inherits="PredicTable.Ascx.ENModelDay" %>
<input type="button" ID="SaveContent" value="保存英文旬月表单"/>
   <div style="width:650px; height:950px; background-size:100%;overflow:hidden;background-image:url('');" runat="server" id="bg_img">
        <div style="float:right;margin-top:129px;margin-right:64px;">
            <input type="text" ID="txt_reportNo" style="width:19px; margin-left: 154px;" ></input>
        </div>
        <div style="margin-top:189px;margin-left:150px;">
            <input type="text" value="<%=Session["ENPublishTime"] %>"  disabled="disabled"  />
        </div>
       <input id="hid_fbdw"  type="hidden" value="" />
       <input id="hid_effectTime"  type="hidden" value="" />
       <input id="hid_publishTime"  type="hidden" value="" />
        <div style="margin-top:40px;margin-left:190px;">
           <input type="text" ID="txt_reportTitle" style="width:150px" ></input>
        </div>
       <div style="margin-top:18px;margin-left:160px;">
           <input type="text" ID="txt_reportTime" style="width:127px" ></input>
        </div>
      <div style="margin-top:57px;margin-left:76px;">
           <textarea ID="txt_reportNorth" style="width:483px;height:150px;" ></textarea>
        </div>
        <div style="margin-top:55px;margin-left:76px;">
           <textarea ID="txt_reportSouth" style="width:483px;height:150px;" ></textarea>
        </div>
       <div style="margin-top:28px;margin-left:50px;">
           <div style="float:left;margin-left:150px;">
                    <select id="sel_header" class="uniformselect"></select>
            </div>
           <div style="float:right;margin-right:120px;">
                <select id="sel_deputyer" class="uniformselect"></select>
           </div>
       </div>
    </div>
<script>
    //获取主、副预报员
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

    });
</script>
<script type="text/javascript">
    $(function () {
        //属性值入库
        $('#SaveContent').click(function () {
            var reportNo, publishTime, reportTitle,reportTime, reportNorth, reportSouth;
            var sendDepartment = "黄岛区";
            var headReporter = $("#sel_header").val();
            var deputyReporter = $("#sel_deputyer").val();
            var fbdw = $("#hid_fbdw").val();
            var pbtime = $("#hid_publicTime").val();
            var pbeffect = $("#hid_effectTime").val();
            if ($("#txt_reportNo").val() != "") {
                reportNo = $("#txt_reportNo").val();
            } else {
                alert("请填写预报单编号");
                return;
            }
            publishTime = $('#hid_publishTime').val();
            if ($("#txt_reportTitle").val() != "") {
                reportTitle = $("#txt_reportTitle").val();
            } else {
                alert("请填写预报单名称");
                return;
            }
            reportTime = $("#txt_reportTime").val();
            if ($("#txt_reportNorth").val() != "") {
                reportNorth = $("#txt_reportNorth").val();
            } else {
                alert("请填写预报单渤海、黄海北部预告");
                return;
            }
            if ($("#txt_reportSouth").val() != "") {
                reportSouth = $("#txt_reportSouth").val();
            } else {
                alert("请填写预报单黄海中部、黄海南部预告");
                return;
            }
            $.ajax({
                type: "GET",
                url: "/Ajax/UpLoadModelInfo.ashx?method=SaveENModelDay&reportNo=" + reportNo + "&publishTime=" + encodeURI(encodeURI(publishTime)) + "&reportTitle="
                    + encodeURI(encodeURI(reportTitle)) + "&reportTime=" + encodeURI(encodeURI(reportTime)) + "&reportNorth=" + encodeURI(encodeURI(reportNorth))
                    + "&reportSouth=" + encodeURI(encodeURI(reportSouth)) + "&headReporter=" + encodeURI(encodeURI(headReporter)) + "&deputyReporter=" + encodeURI(encodeURI(deputyReporter))
                    + "&sendDepartment=" + encodeURI(encodeURI(sendDepartment)) + "&publishCompany=" + encodeURI(encodeURI(fbdw)) + "&publishEffect=" + encodeURI(encodeURI(pbeffect)),
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

    //若是山东预报台预报，获取海洋局北海预报中心数据
    $(function () {
        var fbCompany = $("#hid_fbdw").val();
        var publishTime = $("#hid_publishTime").val();
        var publishEffect = $("#hid_effectTime").val();
        $.ajax({
            type: "GET",
            url: "/Ajax/UpLoadModelInfo.ashx?method=ENGetForecastInfo&publishTime=" + encodeURI(encodeURI(publishTime)) + "&publishCompany=" + encodeURI(encodeURI(fbCompany)) + "&publishEffect=" + encodeURI(encodeURI(publishEffect)),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    $("#txt_reportNo").val(data.list[0].REPORTNO);
                    //$("#t_publishTime").val(data.list[0].PUBLISHTIME);
                    $("#txt_reportTitle").val(data.list[0].REPORTTITLE);
                    $("#txt_reportTime").val(data.list[0].REPORTTIME);
                    $("#txt_reportNorth").val(data.list[0].REPORTNORTH);
                    $("#txt_reportSouth").val(data.list[0].REPORTSOUTH);
                    $("#uniform-sel_header span").html(data.list[0].HEADREPORTERNAME);
                    $("#uniform-sel_deputyer span").html(data.list[0].DEPUTYREPORTERNAME);
                }
            }
        });
    });
</script>
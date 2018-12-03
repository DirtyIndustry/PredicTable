<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ENModelYear.ascx.cs" Inherits="PredicTable.Ascx.ENModelYear" %>
<input type="button" ID="SaveContent" value="保存英文年表单"/>
   <div style="width:650px; height:950px; background-size:100%;overflow:hidden;background-image:url('');" runat="server" id="bg_img">
       <div style="float:right;margin-right:64px;margin-top:129px;">
            <input type="text" ID="txt_reportNo" style="width:19px; margin-left: 154px;" ></input>
        </div>
        <div style=";margin-left:150px;margin-top:187px;">
           <input type="text" value="<%=Session["ENPublishTime"] %>"  disabled="disabled"  />
        </div>
       <input id="hid_fbdw"  type="hidden" value="" />
       <input id="hid_publishTime"  type="hidden" value="" />
        <div style="margin-top:48px;margin-left:233px;">
           <input type="text" ID="txt_reportTitle" style="width:169px" ></input>
        </div>
        <div style="margin-top:40px;margin-left:70px;">
           <textarea ID="txt_stormSurge" style="width:472px;height:56px;" ></textarea>
        </div>
        <div style="margin-top:28px;margin-left:70px;">
           <textarea ID="txt_seaWave" style="width:472px;height:56px;" ></textarea>
        </div>
       <div style="margin-top:25px;margin-left:70px;">
           <textarea ID="txt_redTide" style="width:472px;height:56px;" ></textarea>
        </div>
       <div style="margin-top:25px;margin-left:70px;">
           <textarea ID="txt_greebTide" style="width:472px;height:56px;" ></textarea>
        </div>
       <div style="margin-top:25px;margin-left:70px;">
           <textarea ID="txt_tropicalCyclone" style="width:472px;height:56px;" ></textarea>
        </div>
       <div style="margin-top:7px;margin-left:50px;">
           <div style="float:left;margin-left:155px;">
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
                    //var result = eval(data);
                    //console.log(result);

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
                    //var result = eval(data);
                    //console.log(result);

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
        
        $('#SaveContent').click(function () {
            var reportNo, publishTime, reportTitle, stormSurge, seaWave, redTide, greebTide, tropicalCyclone;
            var sendDepartment = "黄岛区";
            var headReporter = $("#sel_header").val();
            var deputyReporter = $("#sel_deputyer").val();
            var publishCompany = $("#hid_fbdw").val();
            if ($("#txt_reportNo").val() != "") {
                reportNo = $("#txt_reportNo").val();
            } else {
                alert("请填写预报单编号");
                return;
            }
            //if ($("#t_publishTime").datebox("getValue") != "") {
            //    publishTime = $("#t_publishTime").datebox("getValue");
            //} else {
            //    alert("请填写预报单发布时间");
            //    return;
            //}
            publishTime = $("#hid_publishTime").val();
            if ($("#txt_reportTitle").val() != "") {
                reportTitle = $("#txt_reportTitle").val();
            } else {
                alert("请填写预报单名称");
                return;
            }
            if ($("#txt_stormSurge").val() != "") {
                stormSurge = $("#txt_stormSurge").val();
            } else {
                alert("请填写预报单风暴潮预告");
                return;
            }
            if ($("#txt_seaWave").val() != "") {
                seaWave = $("#txt_seaWave").val();
            } else {
                alert("请填写预报单海浪预告");
                return;
            }
            if ($("#txt_redTide").val() != "") {
                redTide = $("#txt_redTide").val();
            } else {
                alert("请填写预报单赤潮预告");
                return;
            }
            if ($("#txt_greebTide").val() != "") {
                greebTide = $("#txt_greebTide").val();
            } else {
                alert("请填写预报单绿潮预告");
                return;
            }
            if ($("#txt_tropicalCyclone").val() != "") {
                tropicalCyclone = $("#txt_tropicalCyclone").val();
            } else {
                alert("请填写预报单热带气旋预告");
                return;
            }
            $.ajax({
                type: "GET",
                url: "/Ajax/UpLoadModelInfo.ashx?method=SaveENModelYear&reportNo=" + reportNo + "&publishTime=" + encodeURI(encodeURI(publishTime)) + "&reportTitle=" + encodeURI(encodeURI(reportTitle)) + "&stormSurge="
                    + encodeURI(encodeURI(stormSurge)) + "&seaWave=" + encodeURI(encodeURI(seaWave)) + "&redTide=" + encodeURI(encodeURI(redTide)) + "&greebTide=" + encodeURI(encodeURI(greebTide)) + "&tropicalCyclone=" + encodeURI(encodeURI(tropicalCyclone)) + "&headReporter="
                    + encodeURI(encodeURI(headReporter)) + "&deputyReporter=" + encodeURI(encodeURI(deputyReporter)) + "&sendDepartment=" + encodeURI(encodeURI(sendDepartment)) + "&publishCompany=" + encodeURI(encodeURI(publishCompany)),
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
                    alert("预报单生成失败！!!!");
                }
            });
        });
    });
    //若是山东预报台预报，获取海洋局北海预报中心数据
    $(function () {
        var fbCompany = $("#hid_fbdw").val();
        var publishTime = $("#hid_publishTime").val();
        $.ajax({
            type: "GET",
            url: "/Ajax/UpLoadModelInfo.ashx?method=ENYearGetForecastInfo&publishTime=" + encodeURI(encodeURI(publishTime)) + "&publishCompany=" + encodeURI(encodeURI(fbCompany)),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    $("#txt_reportNo").val(data.list[0].REPORTNO);
                    //$("#t_publishTime").val(data.list[0].PUBLISHTIME);
                    $("#txt_reportTitle").val(data.list[0].REPORTTITLE);
                    $("#txt_stormSurge").val(data.list[0].STORMSURGE);
                    $("#txt_seaWave").val(data.list[0].SEAWAVE);
                    $("#txt_redTide").val(data.list[0].REDTIDE);
                    $("#txt_greebTide").val(data.list[0].GREENTIDE);
                    $("#txt_tropicalCyclone").val(data.list[0].TROPICALCYCLONE);
                    $("#uniform-sel_header span").html(data.list[0].HEADREPORTERNAME);
                    $("#uniform-sel_deputyer span").html(data.list[0].DEPUTYREPORTERNAME);
                }
            }
        });
    });
</script>
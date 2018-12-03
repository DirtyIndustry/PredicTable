//页面取值
$(function () {
    //获取主、副预报员
    $.ajax({
        type: "POST",
        url: "/Ajax/NewMediumAndLong.ashx?method=HeadReporter",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (data) {
            if (data != null) {
                $("#sel_header").empty();
                var option = "";
                var first_txt = "";
                for (var i = 0; i < data.list.length; i++) {
                    option += "<option value=" + data.list[i].REPORTERCODE + ">" + data.list[i].REPORTERNAME + "</option>";

                }
                //first_txt = "<option value=''>黎舸</option>";
                first_txt = "HBY2006009";
                $("#sel_headerNCS").append(option);
                $("#sel_headerNCS").val(first_txt);//更改默认值
                
                $("#sel_headerSD").append(option);
                $("#sel_headerSD").val(first_txt);
                
                $("#sel_headerM").append(option);
                $("#sel_headerM").val(first_txt);
                

                $("#sel_headerX").append(option);
                $("#sel_headerX").val(first_txt);
                
                
            } else {
                alert("下拉框数据获取失败！");
            }
        }

    });
    $.ajax({
        type: "POST",
        url: "/Ajax/NewMediumAndLong.ashx?method=DeputyReporter",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (data) {
            if (data != null) {
                $("#sel_deputyer").empty();
                var option = "";
                var first_txt = "";
                for (var i = 0; i < data.list.length; i++) {
                    option += "<option value=" + data.list[i].REPORTERCODE + ">" + data.list[i].REPORTERNAME + "</option>";
                }
                //first_txt = "<option value='HBY2006018'>焦艳</option>";
                first_txt = "HBY2006018";
                $("#sel_deputyerNCS").append(option);
                $("#sel_deputyerNCS").val(first_txt);//更改默认值

                $("#sel_deputyerSD").append(option);
                $("#sel_deputyerSD").val(first_txt);//更改默认值
                
                $("#sel_deputyerM").append(option);
                $("#sel_deputyerM").val(first_txt);//更改默认值
                
                $("#sel_deputyerX").append(option);
                $("#sel_deputyerX").val(first_txt);//更改默认值
                
            } else {
                alert("下拉框数据获取失败！");
            }
        }

    });
});

//获取年数据
function GetYear(ptime,company) {
    $.ajax({
        type: "POST",
        url: "/Ajax/NewMediumAndLong.ashx?method=GetYearData&ptime=" + ptime + "&company=" + company,
        cache: false,
        success: function (data) {
            if (data != null) {
                var resjson = JSON.parse(data)["data"];
                if (company == "NCS") {
                    $('#txtNoNCS').val(resjson[0].REPORTNO);
                    $('#txtTitleNCS').val(resjson[0].REPORTTITLE);
                    $('#txt_stormSurgeNCS').val(resjson[0].STORMSURGE);
                    $('#txt_seaWaveNCS').val(resjson[0].SEAWAVE);
                    $('#txt_redTideNCS').val(resjson[0].REDTIDE);
                    $('#txt_greebTideNCS').val(resjson[0].GREENTIDE);
                    $('#txt_tropicalCycloneNCS').val(resjson[0].TROPICALCYCLONE);
                    $('#sel_headerNCS').val(resjson[0].HEADREPORTER);
                    $('#sel_deputyerNCS').val(resjson[0].DEPUTYREPORTER);
                }
                else if (company == "SD") {
                    $('#txtNoSD').val(resjson[0].REPORTNO);
                    $('#txtTitleSD').val(resjson[0].REPORTTITLE);
                    $('#txt_stormSurgeSD').val(resjson[0].STORMSURGE);
                    $('#txt_seaWaveSD').val(resjson[0].SEAWAVE);
                    $('#txt_redTideSD').val(resjson[0].REDTIDE);
                    $('#txt_greebTideSD').val(resjson[0].GREENTIDE);
                    $('#txt_tropicalCycloneSD').val(resjson[0].TROPICALCYCLONE);
                    $('#sel_headerSD').val(resjson[0].HEADREPORTER);
                    $('#sel_deputyerSD').val(resjson[0].DEPUTYREPORTER);
                }
            } else {
                alert("数据获取失败！");
            }
        }

    });
}
//获取月、旬数据，update by Durriya start
/*function GetMonthOrDays(ptime) {
    var type = "";
    var time = new Date(ptime.substring(0, 4) + "/" + ptime.substring(4, 6) + "/" + ptime.substring(6, 8));
    var nian = time.getFullYear();
    var yue = time.getMonth() + 1;
    var ri = time.getDate();
    if (ri == 9 || ri == 19 || ri == 29 || (yue == 2 && ri == 28)) {
        type = "X";
    } else if (ri == 25 || ri == 26) {
        type = "M";
    }

    $.ajax({//start ajax
        type:"POST",
        url: "/Ajax/NewMediumAndLong.ashx?method=GetMonthOrDays&ptime=" + ptime + "&type=" + type,
        cache: false,
        success: function (data) {
            if (data != null) {

                var resjson = JSON.parse(data)["data"];
                if ( typeof (resjson) != "undefined") {
                    jiexiMandX(resjson);
                } else {
                    $('#txtNoXorM').val(JSON.parse(data)["reportNo"]);
                    $('#txtReportNCSX').val("");
                    $('#txtReportSDX').val("");
                    $('#txtContentNPOILXorM').val("");
                    $('#txtContentSLOILXorM').val("");
                    $('#txtContentDYOILX').val("");
                }
                //var resjson = JSON.parse(data)["data"];
                //jiexiMandX(resjson);
            } else {
                alert("数据获取失败！");
            }
        }//end success

    });//end ajax
}*/

//获取月、旬数据
/*function GetMonthOrDays(ptime, type) {
    $.ajax({
        type: "POST",
        url: "/Ajax/NewMediumAndLong.ashx?method=GetMonthOrDays&ptime=" + ptime + "&type=" + type,
        cache: false,
        success: function (data) {
            if (data != null) {
                var resjson = JSON.parse(data)["data"];
                if (type == "M") {
                    jiexiM(resjson);
                } else if (type == "X") {
                    jiexiX(resjson);
                }
            } else {
                alert("数据获取失败！");
            }
        }

    });
}*/

//旬、月预报数据解析显示
/*function jiexiMandX(resjson) {
    $('#txtNoXorM').val(resjson[0].REPORTNO);
    $('#sel_headerX').val(resjson[0].HEADREPORTER);
    $('#sel_deputyerX').val(resjson[0].DEPUTYREPORTER);

    for (var i = 0; i < resjson.length; i++) {
        var companyName = resjson[i].PUBLISHCOMPANY;
        switch (companyName) {
            case "NCS":
                $('#txtNCSTitleXorM').val(resjson[i].REPORTTITLE);
                $('#txtReportNCSX').val(resjson[i].REPORTCONTENT);
                break;
            case "SD":
                $('#txtSDTitleXorM').val(resjson[i].REPORTTITLE);
                $('#txtReportSDX').val(resjson[i].REPORTCONTENT);
                break;
            case "南堡油田":
                $('#txtNPOILTitleXorM').val(resjson[i].REPORTTITLE);
                $('#txtContentNPOILXorM').val(resjson[i].REPORTCONTENT);
                break;
            case "胜利油田":
                $('#txtSLOILTitleXorM').val(resjson[i].REPORTTITLE);
                $('#txtContentSLOILXorM').val(resjson[i].REPORTCONTENT);
                break;
            case "东营环境预报":
                $('#txtDYOILTitleXorM').val(resjson[i].REPORTTITLE);
                $('#txtContentDYOILX').val(resjson[i].REPORTCONTENT);
                break;
            default:
                break;
        }
    }
}*/

//获取月、旬数据，update by Durriya 
function GetMonthOrDays(ptime, type) {
    $.ajax({
        type: "POST",
        url: "/Ajax/NewMediumAndLong.ashx?method=GetMonthOrDays&ptime=" + ptime + "&type=" + type,
        cache: false,
        success: function (data) {
            if (data != null) {
                var resjson = JSON.parse(data)["data"];
                if (typeof (resjson) != "undefined") {
                    if (type == "M") {
                        jiexiM(resjson);
                    } else if (type == "X") {
                        jiexiX(resjson);
                    }
                } else {
                    if (type == "M") {
                        $('#txtNoM').val(JSON.parse(data)["reportNo"]);
                        $('#txtReportNCSM').val("");
                        $('#txtReportSDM').val("");
                        $('#txtContentNPOILM').val("");
                        $('#txtContentSLOILM').val("");
                        $('#txtContentDYOILM').val("");
                    } else if (type == "X") {
                        $('#txtNoX').val(JSON.parse(data)["reportNo"]);
                        $('#txtReportNCSX').val("");
                        $('#txtReportSDX').val("");
                        $('#txtContentNPOILX').val("");
                        $('#txtContentSLOILX').val("");
                        $('#txtContentDYOILX').val("");
                    }
                }
            } else {
                alert("数据获取失败！");
            }
        }

    });
}

//月预报数据解析显示
function jiexiM(resjson) {
    $('#txtNoM').val(resjson[0].REPORTNO);
    $('#sel_headerM').val(resjson[0].HEADREPORTER);
    $('#sel_deputyerM').val(resjson[0].DEPUTYREPORTER);

    for (var i = 0; i < resjson.length; i++) {
        var companyName = resjson[i].PUBLISHCOMPANY;
        switch (companyName) {
            case "NCS":
                $('#txtNCSTitleM').val(resjson[i].REPORTTITLE);
                $('#txtReportNCSM').val(resjson[i].REPORTCONTENT);
                break;
            case "SD":
                $('#txtSDTitleM').val(resjson[i].REPORTTITLE);
                $('#txtReportSDM').val(resjson[i].REPORTCONTENT);
                break;
            case "南堡油田":
                $('#txtNPOILTitleM').val(resjson[i].REPORTTITLE);
                $('#txtContentNPOILM').val(resjson[i].REPORTCONTENT);
                break;
            case "胜利油田":
                $('#txtSLOILTitleM').val(resjson[i].REPORTTITLE);
                $('#txtContentSLOILM').val(resjson[i].REPORTCONTENT);
                break;
            case "东营环境预报":
                $('#txtDYOILTitleM').val(resjson[i].REPORTTITLE);
                $('#txtContentDYOILM').val(resjson[i].REPORTCONTENT);
                break;
            default:
                break;
        }
    }
}
//旬预报数据解析显示
function jiexiX(resjson) {
    $('#txtNoX').val(resjson[0].REPORTNO);
    $('#sel_headerX').val(resjson[0].HEADREPORTER);
    $('#sel_deputyerX').val(resjson[0].DEPUTYREPORTER);

    for (var i = 0; i < resjson.length; i++) {
        var companyName = resjson[i].PUBLISHCOMPANY;
        switch (companyName) {
            case "NCS":
                $('#txtNCSTitleX').val(resjson[i].REPORTTITLE);
                $('#txtReportNCSX').val(resjson[i].REPORTCONTENT);
                break;
            case "SD":
                $('#txtSDTitleX').val(resjson[i].REPORTTITLE);
                $('#txtReportSDX').val(resjson[i].REPORTCONTENT);
                break;
            case "南堡油田":
                $('#txtNPOILTitleX').val(resjson[i].REPORTTITLE);
                $('#txtContentNPOILX').val(resjson[i].REPORTCONTENT);
                break;
            case "胜利油田":
                $('#txtSLOILTitleX').val(resjson[i].REPORTTITLE);
                $('#txtContentSLOILX').val(resjson[i].REPORTCONTENT);
                break;
            case "东营环境预报":
                $('#txtDYOILTitleX').val(resjson[i].REPORTTITLE);
                $('#txtContentDYOILX').val(resjson[i].REPORTCONTENT);
                break;
            default:
                break;
        }
    }
}
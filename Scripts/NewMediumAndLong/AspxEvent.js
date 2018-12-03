//页面事件处理
$(function () {
    var t = myformatter(new Date());
    $("#publishTime").datebox("setValue", t);
    
    // 选择时间事件 onSelect
    //$('#l6').datebox({
    //    onSelect: function () {
    //        //$("#hid_field_time").val($("#l6").datebox("getValue"));
    //    }
    //});

    $("#list").change(function () {
        //$("#hidd_model").val($("#list").find("option:selected").text());
        //SetForcastTypeAndVisiable();
    });

    //编写模板
    $("#startoperation").click(function () {
        SetForcastTypeAndVisiable();
    });
});

//通过选择的预报单类型，控制不同div的显隐
//设置默认title
//获取当天数据
function SetForcastTypeAndVisiable() {
    var forcastType = $("#list").find("option:selected").val();
    var ptime = $("#publishTime").datebox("getValue");
    var title = "";
    switch (forcastType) {
        case "NCS":
            SetDivDisplay(1);
            GetYear(ptime, "NCS");
            break;
        case "SD":
            SetDivDisplay(2);
            GetYear(ptime, "SD");
            break;
        case "Month":
            var time = new Date(ptime.substring(0, 4) + "/" + ptime.substring(4, 6) + "/" + ptime.substring(6, 8));          
            var ri = time.getDate();
            if (ri != 25 && ri != 26)
            {
                $("#div" + 3).css("display", "none");
                alert("请正确选择月预报的时间");
                break;
            }
                

            SetDivDisplay(3);
            title = SetTitleM(ptime);
            //title = SetTitle(ptime);
            $('#txtNCSTitleM').val(title);
            $('#txtSDTitleM').val(title);
            $('#txtNPOILTitleM').val(title);
            $('#txtSLOILTitleM').val(title);
            $('#txtDYOILTitleM').val(title);
            GetMonthOrDays(ptime, "M");
            break;
        case "Days":
            var time = new Date(ptime.substring(0, 4) + "/" + ptime.substring(4, 6) + "/" + ptime.substring(6, 8));
            var nian = time.getFullYear();
            var yue = time.getMonth() + 1;
            var ri = time.getDate();
            if (ri == 9 || ri == 19 || ri == 29 || (ri == 28 && yue == 2)) {
                SetDivDisplay(4);
                title = SetTitleX(ptime);
                $('#txtNCSTitleX').val(title);
                $('#txtSDTitleX').val(title);
                $('#txtNPOILTitleX').val(title);
                $('#txtSLOILTitleX').val(title);
                $('#txtDYOILTitleX').val(title);
                GetMonthOrDays(ptime, "X");
            } else {
                $("#div" + 4).css("display", "none");
                    alert("请正确选择旬预报的时间");
                    break;
            }

            
            break;
        //case "MonthAndXun":
        //    SetDivDisplay(5);
        //    fixed = SetFixed(ptime);//获取模板固定部分
        //    title = SetNewTitle(ptime);//获取标题文本value

        //    $('#txtNCSTitleFixed').html(fixed);
        //    $('#txtSDTitleFixed').html(fixed);
        //    $('#txtNPOILTitleFixed').html(fixed);
        //    $('#txtSLOILTitleFixed').html(fixed);
        //    $('#txtDYOILTitleFixed').html(fixed);
            
        //    $('#txtNCSTitleXorM').val(title);
        //    $('#txtSDTitleXorM').val(title);
        //    $('#txtNPOILTitleXorM').val(title);
        //    $('#txtSLOILTitleXorM').val(title);
        //    $('#txtDYOILTitleXorM').val(title);

        //    GetMonthOrDays(ptime);
        //    break;
        default:break;
    }
}
//设置ascx显隐
function SetDivDisplay(num) {
    for(var i = 1 ;i < 5 ; i++){
        $("#div" + i).css("display", "none");
    }
    $("#div" + num).css("display", "");
}

//设置默认标题
function SetTitle(ptime) {
    var title = "";
    var time = new Date(ptime.substring(0, 4) + "/" + ptime.substring(4, 6) + "/" + ptime.substring(6, 8));
    var nian = time.getFullYear();
    var yue = time.getMonth() + 1;
    var ri = time.getDate();
    if (ri * 1 < 11) {
        title = nian + "年" + yue + "月中旬";
    }
    else if (ri * 1 > 10 && ri * 1 < 21) {
        title = nian + "年" + yue + "月下旬";
    }
    else if (ri * 1 > 20) {
        //var time2 = new Date(ptime.substring(0, 4) + "/" + ptime.substring(4, 6) + "/" + ptime.substring(6, 8));
        //var timeN = new Date(time2);
        //timeN.setMonth((1));
        //nian = timeN.getFullYear();
        //yue = timeN.getMonth() + 1;
        if (yue == 12) {
            nian += 1;
            yue = 1;
        } else {
            yue += 1;
        }
      
        title = nian + "年" + yue + "月及上旬";
    }
    return title;
}

//旬预报自动匹配标题
function SetTitleX(ptime) {
    var title = "";
    var time = new Date(ptime.substring(0, 4) + "/" + ptime.substring(4, 6) + "/" + ptime.substring(6, 8));
    var nian = time.getFullYear();
    var yue = time.getMonth() + 1;
    var ri = time.getDate();
    if (ri == 9) {
        title = nian + "年" + yue + "月中旬";
    } else if (ri == 19) {
        title = nian + "年" + yue + "月下旬";
    } else if (ri == 29 || (yue == 2 && ri == 28)) {
        if (yue == 12) {
            nian += 1;
            yue = 1;
        } else {
            yue += 1
        }
        title = nian + "年" + yue + "月上旬";
    } else {
        alert('请正确选择旬预报发布时间');
    }
    return title;
}
//月预报自动匹配标题
function SetTitleM(ptime) {
    var title = "";
    var time = new Date(ptime.substring(0, 4) + "/" + ptime.substring(4, 6) + "/" + ptime.substring(6, 8));
    var nian = time.getFullYear();
    var yue = time.getMonth() + 1;
    var ri = time.getDate();
    if (ri == 25 || ri == 26) {
        if (yue == 12) {
            nian += 1;
            yue = 1;
        } else {
            yue += 1
        }
        title = nian + "年" + yue + "月"
    } else {
        alert("请正确选择月预报的时间");
    }
    return title;
    
}

/**
    根据选择的时间自动获取标题
    add by Durriya
*/
function SetNewTitle(ptime) {
    var title = "";
    var time = new Date(ptime.substring(0, 4) + "/" + ptime.substring(4, 6) + "/" + ptime.substring(6, 8));
    var nian = time.getFullYear();
    var yue = time.getMonth() + 1;
    var ri = time.getDate();
    if (ri == 9) {
        title = nian + "年" + yue + "月中旬";
    } else if (ri == 19) {
        title = nian + "年" + yue + "月下旬";
    } else if (ri == 29 ||(yue == 2 && ri == 28)) {
        if (yue == 12) {
            nian += 1;
            yue = 1;
        } else {
            yue += 1
        }
        title = nian + "年" + yue + "月上旬";
    } else if (ri == 25 || ri == 26) {
        if (yue == 12) {
            nian += 1;
            yue = 1;
        } else {
            yue += 1
        }
        title = nian + "年" + yue + "月"
    }
    return title;
}

//判断时间尾数选择模板固定部分
//含9为旬报 含25为月报
function SetFixed(ptime) {
    var fixed = '';
    var time = new Date(ptime.substring(0, 4) + "/" + ptime.substring(4, 6) + "/" + ptime.substring(6, 8));
    var nian = time.getFullYear();
    var yue = time.getMonth() + 1;
    var ri = time.getDate();

    if (ri == 9 || ri == 19 || ri == 29 || (yue == 2 && ri == 28)) {
        fixed = '预报'
    } else if (ri == 25 || ri == 26) {
        fixed = '趋势预测';
    } else {
        //fixed = '时间不符合要求';
        alert("时间不符合要求");
    }
    return fixed;
}

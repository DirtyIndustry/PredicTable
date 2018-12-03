//页面赋值


//页面数据提交
function SendData(param, Url) {
    $.ajax({
        type: "POST",
        url: Url,
        data: param,
        success: function (result) {
            alert(result);
        },
        error: function (data) {
            alert(result);
        }

    })
}

//生成文件名称
//年
function NianModel(publishCompany, publishTime) {
    var docName = "YB_";
    if (publishCompany == "NCS")
    {
        docName += "NCS_HJ_1yr_" + publishTime + "_NMFC.doc";
    }
    else if (publishCompany == "SD")
    {
        docName += "SD_HJ_1yr_" + publishTime + "_SDMF.doc";
    }
    return docName;
}
//月、旬
function YueModel(type, publishCompany, publishTime) {
    var docName ="";
    if (type == "EN")
    {
        docName = "YB_";
        if (publishCompany == "NCS") {
            docName += "NCS_HJ_1mon_" + publishTime + "_NMFC.doc";
        }
        else if (publishCompany == "SD") {
            docName += "SD_HJ_1mon_" + publishTime + "_SDMF.doc";
        }
    }
    else if (type == "CN")
    {
        docName = CNModel(publishTime) + "-" + publishCompany + ".doc";;
    }
    return docName;
}
//旬
function XunModel(type, publishCompany, publishTime) {
    var docName = "";
    if (type == "EN") {
        docName = "YB_";
        if (publishCompany == "NCS") {
            docName += "NCS_HJ_10day_" + publishTime + "_NMFC.doc";
        }
        else if (publishCompany == "SD") {
            docName += "SD_HJ_10day_" + publishTime + "_SDMF.doc";
        }
    }
    else if (type == "CN") {
        docName = CNModel(publishTime) + "-" + publishCompany + ".doc";;
    }
    return docName;
}

//add by Durriya 
//旬、月预报两者合一,
//这里要根据提交的时间判读是10day 还是1mon
function XunAndMonthModel(type,publishCompany, publishTime) {
    var docName = "";
    var time = new Date(publishTime.substring(0, 4) + "/" + publishTime.substring(4, 6) + "/" + publishTime.substring(6, 8));
    var nian = time.getFullYear();
    var yue = time.getMonth() + 1;
    var ri = time.getDate();
    //根据日期判断是月报还是旬报；
    //日期中逢九为旬报，25或26时为月报
    if (ri == 9 || ri == 19 || ri == 29 || (yue == 2 && ri == 28)) {
        if (type == "EN") {
            docName = "YB_";
            if (publishCompany == "NCS") {
                docName += "NCS_HJ_10day_" + publishTime + "_NMFC.doc";
            }
            else if (publishCompany == "SD") {
                docName += "SD_HJ_10day_" + publishTime + "_SDMF.doc";
            }
        } else if (type == "CN") {
            docName = CNModel(publishTime) + "-" + publishCompany + ".doc";
        }

    } else if (ri == 25 || ri == 26) {
        if (type == "EN") {
            docName = "YB_";
            if (publishCompany == "NCS") {
                docName += "NCS_HJ_1mon_" + publishTime + "_NMFC.doc";
            }
            else if (publishCompany == "SD") {
                docName += "SD_HJ_1mon_" + publishTime + "_SDMF.doc";
            }
        } else if (type == "CN") {
            docName = CNModel(publishTime) + "-" + publishCompany + ".doc";
        }
    }
    return docName;
}


//中文表单名称生成
function CNModel(publishTime) {
    var CNTimeModel = "";
    var time = new Date(publishTime.substring(0, 4) + "/" + publishTime.substring(4, 6) + "/" + publishTime.substring(6, 8));
    var nian = time.getFullYear();
    var yue = time.getMonth() + 1;
    var day = time.getDate();

    if (day == 9)
    {
        CNTimeModel = nian + "年" + yue + "月中旬预报";
    }
    else if (day == 19)
    {
        CNTimeModel = nian + "年" + yue + "月下旬预报";
    }
    else if (day == 29 || (day == 28 && yue == 2))
    {
        if (yue == 12) {
            nian += 1;
            yue = 1;
        }
        else
        {
            yue += 1;
        }
        CNTimeModel = nian + "年" + yue + "月上旬预报";
    }
    else if (day == 25 || day == 26)
    {
        if (yue == 12) {
            nian += 1;
            yue = 1;
        }
        else {
            yue += 1;
        }
        CNTimeModel = nian + "年" + yue + "月预报";
    }
    return CNTimeModel;

    //if (ri * 1 < 11)
    //{
    //    CNTimeModel = nian + "年" + yue + "月中旬预报";
    //}
    //else if (ri * 1 > 10 && ri * 1 < 21)
    //{
    //    CNTimeModel = nian + "年" + yue + "月下旬预报";
    //}
    //else if (ri * 1 > 20)
    //{
    //    //var timeN = new Date(time.setMonth(yue));
    //    //nian = timeN.getFullYear();
    //    //yue = timeN.getMonth();
        
    //    if (yue == 12) {
    //        nian += 1;
    //        yue = 1;
    //    } else {
    //        yue += 1;
    //    }

    //    title = nian + "年" + yue + "月及上旬";
    //    CNTimeModel = nian + "年" + yue + "月及上旬预报";
    //}
    //return CNTimeModel;
}


//警报预报表单js文件
//创建人：韩萌真
//创建时间:2016.12.05
var tt;
var filePath = [];
var filenames = [];
var imgPath = [];
var imgnames = [];
var listVal;
var listtypes;
$(function () {
    function myformatters(date) {
        var y = date.getFullYear();
        var m = date.getMonth() + 1;
        var d = date.getDate();
        return y + '-' + m + '-' + d;
    }
    function myformatter1(date) {
        var y = date.getFullYear() + 1;
        var m = date.getMonth();
        var d = date.getDate();
        return y + '-' + m + '-' + d;
    }
    //选择文件       
    InitWebUploader();
    //选择图片       
    InitWebUploader1();
    //GroupBing();//发往分组绑定
    UserBind();//联系人绑定
    UserBind1();//联系人1绑定
    //发往首次绑定
    wainGroupAction("中长期北海海冰");
   //保存模板文档
    $("#btn_save1").click(function () {
        param = {FILENAME:$("#list").val(), CONTENT: UE.getEditor('ueditor').getContent() }
        Url = "Ajax/HaiBingTable.ashx?method=AddDocContent";

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
    })
    
    //显示隐藏
    $("#Shows").click(function () {
        $("#bodys").toggle();
        $("#bianji").toggle();
        $("#UploadFiles").toggle();
        $("#WordName1").toggle();
    })
   
    //开始编辑
    $("#startoperation").click(function () {
        UE.getEditor('ueditor').setContent("");
        ContentBind($("#list").val());
    })
    function Word() {
        var listValue = $("#list").find("option:selected").val();
       
        var datetime1 = $("#warnPBTime").datebox("getValue").split('-')[1];
        var dateri = $("#warnPBTime").datebox("getValue").split('-')[2];
        
        if (datetime1 < 10)
        {
            datetime1 = datetime1.substring(1, 2);
        }
        var datetime2 =Number(datetime1)+1;
        //文件名修 改
        if (listValue == "周") {
            listVal = "7day";
            listtypes = new Date().getFullYear() + "年" + datetime1+ "月" + new Date().getDate() + "日海冰周预报";
        }
        else if (listValue == "旬") {
            listVal = "10day";
            if (dateri > 1 && dateri <= 10) {
                listtypes = new Date().getFullYear() + "年" + datetime1 + "月中旬冰情预报";
            }
            else if (dateri > 10 && dateri <= 20) {
                listtypes = new Date().getFullYear() + "年" + datetime1 + "月下旬冰情预报";
            }
            else if (dateri > 20 && dateri <= 31) {
                listtypes = new Date().getFullYear() + "年" + datetime2 + "月上旬冰情预报";
            }
        }
        else if (listValue == "月") {
            listVal = "1mon";
            listtypes = new Date().getFullYear() + "年" + datetime1 + "月冰情月预报";
        }
        else if (listValue == "年") { 
            listVal = "1yr";
            listtypes = myformatters(new Date()).split('-')[0] +"-"+ myformatter1(new Date()).split('-')[0] + "年冬季冰情趋势预测";
        }
    }
    //模板切换  
    $("#list").change(function () {
        Word();
        var datetime = $("#warnPBTime").datebox("getValue").split('-')[0] + $("#warnPBTime").datebox("getValue").split('-')[1] + $("#warnPBTime").datebox("getValue").split('-')[2];   
        var WainArea = $("#WainArea").val();
        if (WainArea == "北海区") {
            //文件名修改
            $("#WordName").val("YB_NCS_HB_" + listVal + "_" + datetime + "_NMFC");
        }
        else if (WainArea == "山东近海") {
            //文件名修改
            $("#WordName").val("YB_SD_HB_" + listVal + "_" + datetime + "_NMFC");
        }
        else if (WainArea == "冀东油田") {
            //文件名修改
            $("#WordName").val(listtypes + "（冀东）");
        }
        else if (WainArea == "东营胜利油田") {
            //文件名修改
            $("#WordName").val(listtypes + "（胜利）");
        }
        else if (WainArea == "东营近海") {
            //文件名修改
            $("#WordName").val(listtypes + "（东营）");
        }
        else if (WainArea == "青岛近海") {
            //文件名修改
            $("#WordName").val(listtypes + "（青岛海洋局）"); 
        }

    })
    //选择区域切换
    $("#WainArea").change(function () {
        Word();
        var datetime = $("#warnPBTime").datebox("getValue").split('-')[0] + $("#warnPBTime").datebox("getValue").split('-')[1] + $("#warnPBTime").datebox("getValue").split('-')[2];
        var WainArea = $("#WainArea").val();
        //if (WainArea != "北海区" && WainArea != "青岛近海") {
        //    $("#list option[value='周']").remove();
        //}
        //else {
        //    $("#list").append("<option value='周'>预报单--周.doc</option>");
        //}
        
        if (WainArea == "北海区") {
            //发往绑定
            wainGroupAction("中长期北海海冰");
            //文件名修改
            $("#WordName").val("YB_NCS_HB_" + listVal + "_" + datetime + "_NMFC");
        }
        else if (WainArea == "山东近海") {
            //发往绑定
            wainGroupAction("中长期山东海冰");
            //文件名修改
            $("#WordName").val("YB_SD_HB_" + listVal + "_" + datetime + "_NMFC");
        }
        else if (WainArea == "冀东油田") {
            //发往绑定
            wainGroupAction("中长期冀东海冰专项");
            //文件名修改
            $("#WordName").val(listtypes + "（冀东）");
        }
        else if (WainArea == "东营胜利油田") {
            //发往绑定
            wainGroupAction("中长期胜利海冰专项");
            //文件名修改
            $("#WordName").val(listtypes + "（胜利）");
        }
        else if (WainArea == "东营近海") {
            //发往绑定
            wainGroupAction("中长期东营海冰专项");
            //文件名修改
            $("#WordName").val(listtypes + "（东营）");
        }
        else if (WainArea == "青岛近海") {
            //发往绑定
            wainGroupAction("中长期青岛海冰专项");
            //文件名修改
            $("#WordName").val(listtypes + "（青岛海洋局）");
        }

    })

    //联系人切换 
    $("#wainUser").change(function () {
        $("#content").html($("#wainUser").val());
    })
    //联系人1切换
    $("#wainUser1").change(function () {
        $("#content1").html($("#wainUser1").val());
    })

    //日期切换
    $('#warnPBTime').datebox({
        onSelect: function (date) {
            var waintime = $("#warnPBTime").datebox("getValue");
            $('#times').html(waintime.split('-')[0] + "年" + waintime.split('-')[1] + "月" + waintime.split('-')[2] + "日");
            Word();
            var datetime = $("#warnPBTime").datebox("getValue").split('-')[0] + $("#warnPBTime").datebox("getValue").split('-')[1] + $("#warnPBTime").datebox("getValue").split('-')[2];
            var WainArea = $("#WainArea").val();
            if (WainArea != "北海区") {
                $("#list option[value='周']").remove();
            }
            else {
                $("#list").append("<option value='周'>预报单--周.doc</option>");
            }
            if (WainArea == "北海区") {
                //发往绑定
                wainGroupAction("中长期北海海冰");
                //文件名修改
                $("#WordName").val("YB_NCS_HB_" + listVal + "_" + datetime + "_NMFC");
            }
            else if (WainArea == "山东近海") {
                //发往绑定
                wainGroupAction("中长期山东海冰");
                //文件名修改
                $("#WordName").val("YB_SD_HB_" + listVal + "_" + datetime + "_NMFC");
            }
            else if (WainArea == "冀东油田") {
                //发往绑定
                wainGroupAction("中长期冀东海冰专项");
                //文件名修改
                $("#WordName").val(listtypes + "（冀东）");
            }
            else if (WainArea == "东营胜利油田") {
                //发往绑定
                wainGroupAction("中长期胜利海冰专项");
                //文件名修改
                $("#WordName").val(listtypes + "（胜利）");
            }
            else if (WainArea == "东营近海") {
                //发往绑定
                wainGroupAction("中长期东营海冰专项");
                //文件名修改
                $("#WordName").val(listtypes + "（东营）");
            }
            else if (WainArea == "青岛近海") {
                //发往绑定
                wainGroupAction("中长期青岛海冰专项");
                //文件名修改
                $("#WordName").val(listtypes + "（青岛海洋局）");
            }

        } });

    //保存
    $("#btn_save").click(function () {
        var param;
        var Url;

        //if ($("#wainGroup").val() == "") {
        //    alert("请选择发往分组。");
        //    return false;
        //}
        if ($("#wainUser").val() == "") {
            alert("请选择联系人。");
            return false;
        }
        if ($("#wainUser1").val() == "") {
            alert("请选择联系人。");
            return false;
        }
        var waintime = $("#warnPBTime").datebox("getValue");
       
        //警报文件内容
        CONTENT = UE.getEditor('ueditor').getContent();
        SENTTO = $("#Fwang").val();
      
        ListType = $("#list").val();
        JBSHIJIAN = $("#warnPBTime").datebox("getValue");
        fileName= $("#WordName").val();
        //传参
        param = {
            CONTENT: CONTENT, SENTTO: SENTTO, JBSHIJIAN: JBSHIJIAN, Time: waintime.split('-')[0] + waintime.split('-')[1] + waintime.split('-')[2], XXTime: waintime.split('-')[0] + waintime.split('-')[1] + waintime.split('-')[2], times: $("#times").html(), DOCNAME: $("#list").val(), ListType: ListType, Nos: $("#cols").val(), User: $("#wainUser").val() + " " + $("#wainUser1").val(), fileName: fileName
                }
        
        Url = "Ajax/HaiBingTable.ashx?method=AddHaiBing";

        $.ajax({
            type: "POST",
            url: Url,
            data: param,
            success: function (result) {
                var s = result.split('|');
                alert(s[0]);
                var filepath = "";
                window.location.assign(s[1]);
            },
            error: function (data) {
                alert(result);
            }

        }) 
    })
   
    //内容绑定数据
    function ContentBind() {
        var param = { FILENAME: $("#list").val() };
        $.ajax({
            type: "POST",
            url: "Ajax/HaiBingTable.ashx?method=getContents",
            data: param,
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.length > 0) {
                    UE.getEditor('ueditor').setContent(data[0]["CONTENT"]);
                }
               
            },
            error: function (data) {
                UE.getEditor('ueditor').setContent("");
            }

        });

    }
    //联系人绑定数据
    function UserBind() {
        $.ajax({
            type: "POST",
            url: "Ajax/WarningQuery.ashx?method=getContents",
            data: {},
            dataType: "json",
            async: false,
            success: function (data) {
                $("#wainUser").html("");
                if (data.length > 0) {
                    var strs = "";
                    for (var i = 0; i < data.length; i++) {
                        strs += "<option value='" + data[i]["CONTENTSNAME"] + "'>" + data[i]["CONTENTSNAME"] + "</option>";
                    }
                    $("#wainUser").append(strs);
                    $("#wainUser").find("option[value='黎舸']").attr("selected", true);
                    $("#content").html($("#wainUser").val());
                }
               
            },
            error: function (data) { }

        });

    }
    //联系人绑定数据
    function UserBind1() {
        $.ajax({
            type: "POST",
            url: "Ajax/WarningQuery.ashx?method=getContents",
            data: {},
            dataType: "json",
            async: false,
            success: function (data) {
                $("#wainUser1").html("");
                if (data.length > 0) {
                    var strs = "";
                    for (var i = 0; i < data.length; i++) {
                        strs += "<option value='" + data[i]["CONTENTSNAME"] + "'>" + data[i]["CONTENTSNAME"] + "</option>";
                    }
                    $("#wainUser1").append(strs);
                    $("#wainUser1").find("option[value='焦艳']").attr("selected", true);
                    $("#content1").html($("#wainUser1").val());
                }
             
            },
            error: function (data) { }

        });

    }
    //发往分组绑定
    function GroupBing() {
        $.ajax({
            type: "POST",
            url: "Ajax/WarningQuery.ashx?method=getGroup",
            data: {},
            dataType: "json",
            async: false,
            success: function (data) {
                $("#wainGroup").html("");
                if (data.length > 0) {
                    var strs = "";
                    for (var i = 0; i < data.length; i++) {
                        strs += "<option value='" + data[i]["UNITNAME"] + "'>" + data[i]["GROUPNAME"] + "</option>";
                    }
                }
                $("#wainGroup").append(strs);
                $("#Fwang").val($("#wainGroup").val());

            },
            error: function (data) { }

        });
    }
    //发往绑定
    function wainGroupAction(wainTypeVale) {
        var param = {};
        param.FAXGROUP = wainTypeVale;

        $.ajax({
            type: "POST",
            url: "Ajax/WarningQuery.ashx?method=getGroup",
            data: param,
            dataType: "json",
            async: false,
            success: function (data) {
                var FAXGROUP = "";
                if (data.length > 0) {
                    //for (var i = 0; i < data.length; i++) {
                    //    FAXGROUP += data[i].USERNAME + ";";
                    //}
                    //var str = FAXGROUP.substring(0, FAXGROUP.length - 1);
                    FAXGROUP = data[0].USERNAME;
                    $("#Fwang").val(FAXGROUP);
                }
                else { $("#Fwang").val(""); }


            },
            error: function (data) { }

        });
    }
    //上传附件封装方法
    function InitWebUploader() {
        var ret = 0;
        var BZ = $("#fawangbz").val();
        var uploader = WebUploader.create({
            auto: true,
            // swf文件路径
            swf: '/Scripts/WebUploader/Uploader.swf',
            // 文件接收服务端。
            server: 'Ajax/FileUploader.ashx',
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: {
                id:$("#picker"),
                multiple: false
            },
            // chunkSize: '100M',
            fileSingleSizeLimit: 1024 * 1024 * 100,
            // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
            // fileNumLimit :1,          
            resize: false,
            //上传文件格式
            accept: {
                title: 'Images',
                extensions: 'doc',
                // mimeTypes: 'image/*'
            },
            formData: { FaWangbz: BZ }
        });

        uploader.on('fileQueued', function (file) {
        uploader.options.formData.fawangbz =$("#fawangbz").val();
            filenames.push(file.name);
            var $list = $("#fileList");
            $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
                '<span class="info" style="font-size:16px"  id="' + file.name + '">' + file.name + '</span>' +
                '&nbsp;&nbsp;' +
                //'<a class="delete" style="cursor: pointer;font-size:16px">删除</a>' +
                '<p class="state"  style="font-size:16px">等待上传...</p>' +
            '</div>');

            $list.on('click', '.delete', function () {
                var id = $(this).parent().attr("id");
                uploader.removeFile(id, true);
                $(this).parent().remove();
                var name = $(this).prev().attr("id");
                var filename = $(this).parent().attr("filename").match(/[^\/]*$/)[0];//通过正则表达式获取字符串路径下的文件名称
                var param = { fileName: filename };

                $.ajax({
                    type: "POST",
                    url: "Ajax/FileUploader.ashx?method=DeleteFile",
                    data: param,
                    dataType: "json",
                    async: false,
                    success: function (data) {
                    },
                    error: function (data) { }

                });

                //删除文件后删除数组中的文件名
                filenames.splice($.inArray(filename, filenames), 1);
            });
        });

        uploader.on('uploadProgress', function (file, percentage) {

            var $li = $('#' + file.id),
                $percent = $li.find('.progress .progress-bar');

            // 避免重复创建
            if (!$percent.length) {
                $percent = $('<div class="progress progress-striped active">' +
                  '<div class="progress-bar" role="progressbar" style="width: 0%">' +
                  '</div>' +
                '</div>').appendTo($li).find('.progress-bar');
            }

            $li.find('p.state').text('上传中');

            $percent.css('width', percentage * 100 + '%');
        });

        uploader.on('uploadSuccess', function (file, response) {
            $("#conlist").val(response._raw.split(']')[1]);
            $('#' + file.id).find('p.state').text('已上传');
          //  $('#' + file.id).attr("fileName", response[0].filePath);
           // filePath.push(response[0].filePath);
           // var fliename = response[0].fileName;
        });

        uploader.on('uploadError', function (file, reason) {

            $('#' + file.id).find('p.state').text('上传出错');
        });

        uploader.on('uploadComplete', function (file) {
            $('#' + file.id).find('.progress').fadeOut();
        });
        uploader.on('error', function (type) {
            if (type == "Q_TYPE_DENIED")
            {
                alert("您选择上传文件的格式不正确，请重新选择!文件后缀例:doc");
                ret = 1;
            }
            else if (type == "F_DUPLICATE") {
                alert("您选择的文件已经上传，请重新选择!");
                ret = 1;
            }
            else if (type == "F_EXCEED_SIZE") {
                alert("您选择的文件大于100M，请重新选择!");
                ret = 1;
            }
            else if (type == "Q_EXCEED_NUM_LIMIT") {
                alert("您只能选择一个文件，请重新选择!");
                ret = 1;
            }
        });
        if (ret == 1) {
            return false;
        }
        else {
            uploader.upload();
        }
       
    }
    //上传附件封装方法
    function InitWebUploader1() {
       
        var uploader = WebUploader.create({
            auto: true,
            // swf文件路径
            swf: '/Scripts/WebUploader/Uploader.swf',
            // 文件接收服务端。
            server: 'Ajax/ImgUploader.ashx',
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: {
                id: $("#picker1"),
                multiple: false
            },
            // chunkSize: '100M',
            fileSingleSizeLimit: 1024 * 1024 * 100,
            // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
            resize: false,
            //上传文件格式
            accept: {
                title: 'Images',
                extensions: 'gif,jpg,jpeg,bmp,png',
                mimeTypes: 'image/*'
            }
        });
       
        uploader.on('fileQueued', function (file) {
            filenames.push(file.name);
            var $list = $("#fileList1");
            $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
                '<span class="info" style="font-size:16px" id="' + file.name + '">' + file.name + '</span>' +
                '&nbsp;&nbsp;' +
                //'<a class="delete1" style="cursor: pointer;font-size:16px">删除</a>' +
                '<p class="state" style="font-size:16px">等待上传...</p>' +
            '</div>');

            $list.on('click', '.delete1', function () {
                var id = $(this).parent().attr("id");  
                uploader.removeFile(id, true);
                $(this).parent().remove();
                var name = $(this).prev().attr("id");
                var filename = $(this).parent().attr("filename").match(/[^\/]*$/)[0];//通过正则表达式获取字符串路径下的文件名称
                var param = { fileName: filename };
                $.ajax({
                    type: "POST",
                    url: "Ajax/ImgUploader.ashx?method=DeleteImg",
                    data: param,
                    dataType: "json",
                    async: false,
                    success: function (data) {
                    },
                    error: function (data) { }

                });
               
                //删除文件后删除数组中的文件名
                filenames.splice($.inArray(filename, filenames), 1);
            });

        });
      
        uploader.on('uploadProgress', function (file, percentage) {

            var $li = $('#' + file.id),
                $percent = $li.find('.progress .progress-bar');

            // 避免重复创建
            if (!$percent.length) {
                $percent = $('<div class="progress progress-striped active">' +
                  '<div class="progress-bar" role="progressbar" style="width: 0%">' +
                  '</div>' +
                '</div>').appendTo($li).find('.progress-bar');
            }

            $li.find('p.state').text('上传中');

            $percent.css('width', percentage * 100 + '%');
        });

        uploader.on('uploadSuccess', function (file, response) {
            $("#conlist1").html(response._raw.split(']')[1]);
            $('#' + file.id).find('p.state').text('已上传');
           // $('#' + file.id).attr("fileName", response.jdata.filePath);
           // filePath.push(response.jdata.filePath);
           // var fliename = response.jdata.fileName;
            //$("#filePathName").val(fliename);
        });

        uploader.on('uploadError', function (file, reason) {

            $('#' + file.id).find('p.state').text('上传出错');
        });

        uploader.on('uploadComplete', function (file) {
            $('#' + file.id).find('.progress').fadeOut();
        });
        uploader.on('error', function (type) {
            if (type == "Q_TYPE_DENIED")
            { common.showErrorMsg("您选择上传文件的格式不正确，请重新选择!文件后缀例:gif,jpg,bmp,png", null); }
            else if (type == "F_DUPLICATE") {
                common.showErrorMsg("您选择的文件已经上传，请重新选择!", null);
            }
            else if (type == "F_EXCEED_SIZE") {
                common.showErrorMsg("您选择的文件大于100M，请重新选择!", null);
            }
        });
      
         uploader.upload();
        

    }
})
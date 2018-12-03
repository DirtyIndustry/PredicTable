//中长期海冰上传word解析html的js文件
//创建时间：2018.11.14
var tt;
var filePath = [];
var filenames = [];

var imgPath = [];
var imgnames = [];

var upLoadImgFlag = false;
$(function () {
    //选择文件
    InitWebUploader();
    InitWebUploader_Month_NCS();
    InitWebUploader_Month_SD();
    InitWebUploader_Month_NP();
    InitWebUploader_Month_SL();
    InitWebUploader_Month_DY();
})

//上传附件封装方法--北海年
function InitWebUploader() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf文件路径
        swf: '/Scripts/WebUploader/Uploader.swf',
        // 文件接收服务端。
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_Year',
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: {
            id: $("#picker"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
        resize: false,
        //上传文件格式
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("确认上传文件名为" + file.name + "的文件吗？");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //文件名弹框提示
        filenames.push(file.name);
        var $list = $("#fileList");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">删除</a>' +
            '<p class="state">等待上传...</p>' +
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
                url: "Ajax/NewMediumAndLongHBUploader.ashx?method=DeleteFile",
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

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('已上传');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("您选择上传文件的格式不正确，请重新选择!<br><span>文件后缀例:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("您选择的文件已经上传，请重新选择!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("您选择的文件大于100M，请重新选择!");
        }
    });
    uploader.upload();
}


//上传附件封装方法--月
function InitWebUploader_Month_NCS() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf文件路径
        swf: '/Scripts/WebUploader/Uploader.swf',
        // 文件接收服务端。
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: {
            id: $("#picker_Month_NCS"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
        resize: false,
        //上传文件格式
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "NCS" }
    });


    uploader.on('fileQueued', function (file) {
        var confirms = confirm("确认上传文件名为" + file.name + "的文件吗？");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //文件名弹框提示
        filenames.push(file.name);
        var $list = $("#fileList_Month_NCS");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">删除</a>' +
            '<p class="state">等待上传...</p>' +
        '</div>');
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

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('已上传');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("您选择上传文件的格式不正确，请重新选择!<br><span>文件后缀例:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("您选择的文件已经上传，请重新选择!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("您选择的文件大于100M，请重新选择!");
        }
    });
    uploader.upload();
}

//上传附件封装方法--月
function InitWebUploader_Month_SD() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf文件路径
        swf: '/Scripts/WebUploader/Uploader.swf',
        // 文件接收服务端。
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: {
            id: $("#picker_Month_SD"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
        resize: false,
        //上传文件格式
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "SD" }
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("确认上传文件名为" + file.name + "的文件吗？");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //文件名弹框提示
        filenames.push(file.name);
        var $list = $("#fileList_Month_SD");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">删除</a>' +
            '<p class="state">等待上传...</p>' +
        '</div>');
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

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('已上传');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("您选择上传文件的格式不正确，请重新选择!<br><span>文件后缀例:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("您选择的文件已经上传，请重新选择!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("您选择的文件大于100M，请重新选择!");
        }
    });
    uploader.upload();
}

//上传附件封装方法--月
function InitWebUploader_Month_NP() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf文件路径
        swf: '/Scripts/WebUploader/Uploader.swf',
        // 文件接收服务端。
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: {
            id: $("#picker_Month_NP"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
        resize: false,
        //上传文件格式
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "NP" }
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("确认上传文件名为" + file.name + "的文件吗？");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //文件名弹框提示
        filenames.push(file.name);
        var $list = $("#fileList_Month_NP");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">删除</a>' +
            '<p class="state">等待上传...</p>' +
        '</div>');
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

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('已上传');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("您选择上传文件的格式不正确，请重新选择!<br><span>文件后缀例:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("您选择的文件已经上传，请重新选择!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("您选择的文件大于100M，请重新选择!");
        }
    });
    uploader.upload();
}

//上传附件封装方法--月
function InitWebUploader_Month_SL() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf文件路径
        swf: '/Scripts/WebUploader/Uploader.swf',
        // 文件接收服务端。
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: {
            id: $("#picker_Month_SL"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
        resize: false,
        //上传文件格式
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "SL" }
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("确认上传文件名为" + file.name + "的文件吗？");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //文件名弹框提示
        filenames.push(file.name);
        var $list = $("#fileList_Month_SL");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">删除</a>' +
            '<p class="state">等待上传...</p>' +
        '</div>');
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

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('已上传');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("您选择上传文件的格式不正确，请重新选择!<br><span>文件后缀例:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("您选择的文件已经上传，请重新选择!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("您选择的文件大于100M，请重新选择!");
        }
    });
    uploader.upload();
}

//上传附件封装方法--月
function InitWebUploader_Month_DY() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf文件路径
        swf: '/Scripts/WebUploader/Uploader.swf',
        // 文件接收服务端。
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: {
            id: $("#picker_Month_DY"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
        resize: false,
        //上传文件格式
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "DY" }
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("确认上传文件名为" + file.name + "的文件吗？");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //文件名弹框提示
        filenames.push(file.name);
        var $list = $("#fileList_Month_DY");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">删除</a>' +
            '<p class="state">等待上传...</p>' +
        '</div>');
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

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('已上传');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("您选择上传文件的格式不正确，请重新选择!<br><span>文件后缀例:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("您选择的文件已经上传，请重新选择!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("您选择的文件大于100M，请重新选择!");
        }
    });
    uploader.upload();
}
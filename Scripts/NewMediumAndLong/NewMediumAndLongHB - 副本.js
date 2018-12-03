//�г��ں����ϴ�word����html��js�ļ�
//����ʱ�䣺2018.11.14
var tt;
var filePath = [];
var filenames = [];

var imgPath = [];
var imgnames = [];

var upLoadImgFlag = false;
$(function () {
    //ѡ���ļ�
    InitWebUploader();
    InitWebUploader_Month_NCS();
    InitWebUploader_Month_SD();
    InitWebUploader_Month_NP();
    InitWebUploader_Month_SL();
    InitWebUploader_Month_DY();
})

//�ϴ�������װ����--������
function InitWebUploader() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf�ļ�·��
        swf: '/Scripts/WebUploader/Uploader.swf',
        // �ļ����շ���ˡ�
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_Year',
        // ѡ���ļ��İ�ť����ѡ��
        // �ڲ����ݵ�ǰ�����Ǵ�����������inputԪ�أ�Ҳ������flash.
        pick: {
            id: $("#picker"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // ��ѹ��image, Ĭ�������jpeg���ļ��ϴ�ǰ��ѹ��һ�����ϴ���
        resize: false,
        //�ϴ��ļ���ʽ
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("ȷ���ϴ��ļ���Ϊ" + file.name + "���ļ���");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //�ļ���������ʾ
        filenames.push(file.name);
        var $list = $("#fileList");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">ɾ��</a>' +
            '<p class="state">�ȴ��ϴ�...</p>' +
        '</div>');

        $list.on('click', '.delete', function () {

            var id = $(this).parent().attr("id");
            uploader.removeFile(id, true);
            $(this).parent().remove();
            var name = $(this).prev().attr("id");
            var filename = $(this).parent().attr("filename").match(/[^\/]*$/)[0];//ͨ��������ʽ��ȡ�ַ���·���µ��ļ�����
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

            //ɾ���ļ���ɾ�������е��ļ���
            filenames.splice($.inArray(filename, filenames), 1);
        });
    });

    uploader.on('uploadProgress', function (file, percentage) {

        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');

        // �����ظ�����
        if (!$percent.length) {
            $percent = $('<div class="progress progress-striped active">' +
              '<div class="progress-bar" role="progressbar" style="width: 0%">' +
              '</div>' +
            '</div>').appendTo($li).find('.progress-bar');
        }

        $li.find('p.state').text('�ϴ���');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('���ϴ�');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('�ϴ�����');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("��ѡ���ϴ��ļ��ĸ�ʽ����ȷ��������ѡ��!<br><span>�ļ���׺��:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("��ѡ����ļ��Ѿ��ϴ���������ѡ��!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("��ѡ����ļ�����100M��������ѡ��!");
        }
    });
    uploader.upload();
}


//�ϴ�������װ����--��
function InitWebUploader_Month_NCS() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf�ļ�·��
        swf: '/Scripts/WebUploader/Uploader.swf',
        // �ļ����շ���ˡ�
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // ѡ���ļ��İ�ť����ѡ��
        // �ڲ����ݵ�ǰ�����Ǵ�����������inputԪ�أ�Ҳ������flash.
        pick: {
            id: $("#picker_Month_NCS"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // ��ѹ��image, Ĭ�������jpeg���ļ��ϴ�ǰ��ѹ��һ�����ϴ���
        resize: false,
        //�ϴ��ļ���ʽ
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "NCS" }
    });


    uploader.on('fileQueued', function (file) {
        var confirms = confirm("ȷ���ϴ��ļ���Ϊ" + file.name + "���ļ���");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //�ļ���������ʾ
        filenames.push(file.name);
        var $list = $("#fileList_Month_NCS");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">ɾ��</a>' +
            '<p class="state">�ȴ��ϴ�...</p>' +
        '</div>');
    });

    uploader.on('uploadProgress', function (file, percentage) {

        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');

        // �����ظ�����
        if (!$percent.length) {
            $percent = $('<div class="progress progress-striped active">' +
              '<div class="progress-bar" role="progressbar" style="width: 0%">' +
              '</div>' +
            '</div>').appendTo($li).find('.progress-bar');
        }

        $li.find('p.state').text('�ϴ���');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('���ϴ�');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('�ϴ�����');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("��ѡ���ϴ��ļ��ĸ�ʽ����ȷ��������ѡ��!<br><span>�ļ���׺��:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("��ѡ����ļ��Ѿ��ϴ���������ѡ��!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("��ѡ����ļ�����100M��������ѡ��!");
        }
    });
    uploader.upload();
}

//�ϴ�������װ����--��
function InitWebUploader_Month_SD() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf�ļ�·��
        swf: '/Scripts/WebUploader/Uploader.swf',
        // �ļ����շ���ˡ�
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // ѡ���ļ��İ�ť����ѡ��
        // �ڲ����ݵ�ǰ�����Ǵ�����������inputԪ�أ�Ҳ������flash.
        pick: {
            id: $("#picker_Month_SD"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // ��ѹ��image, Ĭ�������jpeg���ļ��ϴ�ǰ��ѹ��һ�����ϴ���
        resize: false,
        //�ϴ��ļ���ʽ
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "SD" }
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("ȷ���ϴ��ļ���Ϊ" + file.name + "���ļ���");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //�ļ���������ʾ
        filenames.push(file.name);
        var $list = $("#fileList_Month_SD");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">ɾ��</a>' +
            '<p class="state">�ȴ��ϴ�...</p>' +
        '</div>');
    });

    uploader.on('uploadProgress', function (file, percentage) {

        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');

        // �����ظ�����
        if (!$percent.length) {
            $percent = $('<div class="progress progress-striped active">' +
              '<div class="progress-bar" role="progressbar" style="width: 0%">' +
              '</div>' +
            '</div>').appendTo($li).find('.progress-bar');
        }

        $li.find('p.state').text('�ϴ���');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('���ϴ�');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('�ϴ�����');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("��ѡ���ϴ��ļ��ĸ�ʽ����ȷ��������ѡ��!<br><span>�ļ���׺��:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("��ѡ����ļ��Ѿ��ϴ���������ѡ��!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("��ѡ����ļ�����100M��������ѡ��!");
        }
    });
    uploader.upload();
}

//�ϴ�������װ����--��
function InitWebUploader_Month_NP() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf�ļ�·��
        swf: '/Scripts/WebUploader/Uploader.swf',
        // �ļ����շ���ˡ�
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // ѡ���ļ��İ�ť����ѡ��
        // �ڲ����ݵ�ǰ�����Ǵ�����������inputԪ�أ�Ҳ������flash.
        pick: {
            id: $("#picker_Month_NP"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // ��ѹ��image, Ĭ�������jpeg���ļ��ϴ�ǰ��ѹ��һ�����ϴ���
        resize: false,
        //�ϴ��ļ���ʽ
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "NP" }
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("ȷ���ϴ��ļ���Ϊ" + file.name + "���ļ���");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //�ļ���������ʾ
        filenames.push(file.name);
        var $list = $("#fileList_Month_NP");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">ɾ��</a>' +
            '<p class="state">�ȴ��ϴ�...</p>' +
        '</div>');
    });

    uploader.on('uploadProgress', function (file, percentage) {

        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');

        // �����ظ�����
        if (!$percent.length) {
            $percent = $('<div class="progress progress-striped active">' +
              '<div class="progress-bar" role="progressbar" style="width: 0%">' +
              '</div>' +
            '</div>').appendTo($li).find('.progress-bar');
        }

        $li.find('p.state').text('�ϴ���');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('���ϴ�');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('�ϴ�����');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("��ѡ���ϴ��ļ��ĸ�ʽ����ȷ��������ѡ��!<br><span>�ļ���׺��:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("��ѡ����ļ��Ѿ��ϴ���������ѡ��!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("��ѡ����ļ�����100M��������ѡ��!");
        }
    });
    uploader.upload();
}

//�ϴ�������װ����--��
function InitWebUploader_Month_SL() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf�ļ�·��
        swf: '/Scripts/WebUploader/Uploader.swf',
        // �ļ����շ���ˡ�
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // ѡ���ļ��İ�ť����ѡ��
        // �ڲ����ݵ�ǰ�����Ǵ�����������inputԪ�أ�Ҳ������flash.
        pick: {
            id: $("#picker_Month_SL"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // ��ѹ��image, Ĭ�������jpeg���ļ��ϴ�ǰ��ѹ��һ�����ϴ���
        resize: false,
        //�ϴ��ļ���ʽ
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "SL" }
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("ȷ���ϴ��ļ���Ϊ" + file.name + "���ļ���");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //�ļ���������ʾ
        filenames.push(file.name);
        var $list = $("#fileList_Month_SL");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">ɾ��</a>' +
            '<p class="state">�ȴ��ϴ�...</p>' +
        '</div>');
    });

    uploader.on('uploadProgress', function (file, percentage) {

        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');

        // �����ظ�����
        if (!$percent.length) {
            $percent = $('<div class="progress progress-striped active">' +
              '<div class="progress-bar" role="progressbar" style="width: 0%">' +
              '</div>' +
            '</div>').appendTo($li).find('.progress-bar');
        }

        $li.find('p.state').text('�ϴ���');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('���ϴ�');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('�ϴ�����');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("��ѡ���ϴ��ļ��ĸ�ʽ����ȷ��������ѡ��!<br><span>�ļ���׺��:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("��ѡ����ļ��Ѿ��ϴ���������ѡ��!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("��ѡ����ļ�����100M��������ѡ��!");
        }
    });
    uploader.upload();
}

//�ϴ�������װ����--��
function InitWebUploader_Month_DY() {
    var ret = 0;
    var uploader = WebUploader.create({

        auto: true,
        // swf�ļ�·��
        swf: '/Scripts/WebUploader/Uploader.swf',
        // �ļ����շ���ˡ�
        server: 'Ajax/NewMediumAndLongHBUploader.ashx?method=UploadFile_MonthOrXun',
        // ѡ���ļ��İ�ť����ѡ��
        // �ڲ����ݵ�ǰ�����Ǵ�����������inputԪ�أ�Ҳ������flash.
        pick: {
            id: $("#picker_Month_DY"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // ��ѹ��image, Ĭ�������jpeg���ļ��ϴ�ǰ��ѹ��һ�����ϴ���
        resize: false,
        //�ϴ��ļ���ʽ
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { YuBaoArea: "DY" }
    });

    uploader.on('fileQueued', function (file) {
        var confirms = confirm("ȷ���ϴ��ļ���Ϊ" + file.name + "���ļ���");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        //�ļ���������ʾ
        filenames.push(file.name);
        var $list = $("#fileList_Month_DY");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">ɾ��</a>' +
            '<p class="state">�ȴ��ϴ�...</p>' +
        '</div>');
    });

    uploader.on('uploadProgress', function (file, percentage) {

        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');

        // �����ظ�����
        if (!$percent.length) {
            $percent = $('<div class="progress progress-striped active">' +
              '<div class="progress-bar" role="progressbar" style="width: 0%">' +
              '</div>' +
            '</div>').appendTo($li).find('.progress-bar');
        }

        $li.find('p.state').text('�ϴ���');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('���ϴ�');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('�ϴ�����');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("��ѡ���ϴ��ļ��ĸ�ʽ����ȷ��������ѡ��!<br><span>�ļ���׺��:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("��ѡ����ļ��Ѿ��ϴ���������ѡ��!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("��ѡ����ļ�����100M��������ѡ��!");
        }
    });
    uploader.upload();
}
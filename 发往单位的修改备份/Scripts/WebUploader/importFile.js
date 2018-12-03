/*自定义文件导入组件*/
(function ($) {
    /*相关函数*/
    function initImportFile(target) {
        var state = $.data(target, 'importFile');
        var opts = state.options;
       

        initDialog(target);
        initUploader(target);
    }
   
    //显示模态框
    function showModal(target) {
        var state = $.data(target, 'importFile');

        if (state) {
            initDialog(target);
            initUploader(target);
            state.dialog.showModal();
        }
    }
    //初始化dialog对话框
    function initDialog(target) {
        var state = $.data(target, 'importFile');
        var opts = state.options;
        var dg = state.dialog;

        if (dg) {
            //销毁之前的对话框
            dg.remove();
            dg = null;
        }
        var templateHtml = $.fn.importFile.templateHtml;

        dg = dialog({
            id: opts.dialog.id,
            title: opts.dialog.title,
            width: 300,
            content: templateHtml,
            okValue: "开始导入",
            ok: function () {
                uploadFile(target);
                return false;
            },
            cancelValue: "取消",
            cancel: function () {

            },
            onclose: function () {
                var data = this.returnValue;
                if (opts.dialog.onclose) {
                    opts.dialog.onclose(data);
                }
            },
            onshow: function () {
                if (opts.dialog.onshow) {
                    opts.dialog.onshow();
                }
            }
        });
        state.dialog = dg;

        //绑定下载模版事件
        $(".downloadExcel").on('click','a',function () {
            downloadTemplate(target);
        });
    }

    //下载模版
    function downloadTemplate(target) {
        var state = $.data(target, 'importFile');
        var opts = state.options;

        //如果不自动导入的，则采用默认提供的模版，自己写导入逻辑
        if (!opts.template.autoImport) {
            $("#download").attr("href", opts.auto.template.templateUrl);
            return;
        }


        var template = opts.template;
        var templateStr = JSON.stringify(opts.template);
        var param = { template: templateStr };
        var url = "/WebControl/FileTemplate/CreateTemplate";

        common.AjaxPost(url, param, function (responseData) {
            if (responseData.state) {
                var iframe = $("#ifile");
                if (iframe.length < 1) {
                    iframe = $('<iframe id="ifile" style="display:none"></iframe>');
                    $("body").append(iframe);
                }
                iframe.attr("src", responseData.jdata);
                return;
            }
            common.showFaceMsg(responseData.message);
        })

    }
    //开始上传
    function initUploader(target) {
        var state = $.data(target, 'importFile');
        var opts = state.options;
        var dg = state.dialog;

        var uploader = state.uploader;
        if (uploader) {
            state.uploader = null;
            uploader = null;
        }

        uploader = WebUploader.create({
            auto: false,
            // swf文件路径
            swf: '/Scripts/WebUploader/Uploader.swf',
            // 文件接收服务端。
            server: '/WebControl/FileUploader/UploadFile',
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: '#picker',
            // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
            resize: false
        });

        uploader.on('fileQueued', function (file) {
            var $list = $("#fileList");
            $list.append('<div id="' + file.id + '" class="item">' +
                '<h4 class="info">' + file.name + '</h4>' +
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

            //$li.find('p.state').text('上传中');

            $percent.css('width', percentage * 100/2 + '%');
        });

        uploader.on('uploadSuccess', function (file, response) {
            //$('#' + file.id).find('p.state').text('已上传');
            if (opts.template.autoImport) {
                //开始执行导入数据逻辑
                importFile(target, file, response);
            }
            else if (opts.uploader.onuploadcomplet) {
                opts.uploader.onuploadcomplet(target, file, response);

                if (opts.uploader.onimportcomplet) {
                    opts.uploader.onimportcomplet();
                }
            }
        });

        uploader.on('uploadError', function (file) {
            //$('#' + file.id).find('p.state').text('上传出错');
        });

        uploader.on('uploadComplete', function (file, response) {
            //文件上传完成
            var $li = $('#' + file.id),
                $percent = $li.find('.progress .progress-bar');
            //更新进度条100%;
            //if (opts.uploader.onimportcomplet) {
            //    opts.uploader.onimportcomplet();
            //}
        });

        state.uploader = uploader;
    }
    //文件上传
    function uploadFile(target) {
        var state = $.data(target, 'importFile');
        var opts = state.options;
        var dg = state.dialog;

        var uploader = state.uploader;
        if (uploader == undefined) {
            return;
        }
        uploader.upload();
    }
    //文件导入
    function importFile(target, file, response) {
        var state = $.data(target, 'importFile');
        var opts = state.options;
        var dg = state.dialog;

        var url = "/WebControl/FileImport/ImportFile";
        var param = { fileName: response.jdata.fileName, template: JSON.stringify(opts.template) };
        //执行文件的导入
        common.AjaxPost(url, param, function (responseData) {
            var $li = $('#' + file.id),
               $percent = $li.find('.progress .progress-bar');
            $li.find('p.state').text('文件已上传，等待导入...');

            if (responseData.state) {
                $li.find('p.state').text('文件导入成功');
                //更新进度条60%;
                $percent.css('width', 100 + '%');
                common.showFaceMsg("文件导入成功!<a target='_blank' href='" + responseData.jdata + "'>点此查看执行结果。</a>");
                return;
            }

            $li.find('p.state').text('文件导入失败：' + responseData.message);
            $percent.css('width', 0 + '%');
        })
    }
    //构造函数
    $.fn.importFile = function (options) {
        if (typeof options == 'string') {
            var method = $.fn.importFile.methods[options];
            if (method) {
                return method(this);
            }
            else {
                return '无效方法';
            }
        }

        options = options || {};

        return this.each(function () {
            var state = $.data(this, 'importFile');
            if (state) {
                $.extend(state.options, options);
                initImportFile(this);
            }
            else {
                state = $.data(this, 'importFile', {
                    options: $.extend({}, $.fn.importFile.Defaults, options),
                    dialog: null, //对话框
                    uploader: null //上传组建
                })
                initImportFile(this);
            }
        })
    }
    //默认属性
    $.fn.importFile.Defaults = {
        dialog: {
            id: "importUser",
            title: '文件导入',             //弹窗标题
            onshow: function () { },
            onclose: function () { }
        },
        template: {
            autoImport:true,        //是否执行自动导入
            templatName: '',        //模版文件名称
            templatUrl: '',      //下载指定路径下的模版文件
            mainTable: {                    //主表结构
                tableName: '',              //主表表名
                sheetName: '',            //sheet显示名，为空则使用tableName
                primaryKey: '',       //主表主键    
                checkExist: true,          //导入时检查是否存在
                existColumn: '',            //检查存在的表名
                ifExist: '',           //ifexit update:更新Ignore：忽略
                columns: [                  //数据列配置
                    {
                        name: '',           //列名
                        displayName: '',    //显示名
                        dataType: '',       //数据类型
                        excelTable: '',
                    }
                ]
            },
            detailsTable: [                       //明细表结构       
                {
                    tableName: '',          //明细表名
                    sheetName: '',        //sheet名
                    relation: '',    //与主表对应关系 one-one one-many many-one
                    foreignKey: '',         //外键
                    checkExist: false,      //导入时是否检查数据是否存在
                    primaryKey: '',
                    existColumn: '',        //用于检查存在数据的列名
                    ifExist: '',           //ifexit update:更新Ignore：忽略
                    columns: [
                        {
                            name: '',         //列名
                            displayName: '',  //列明显示
                            dataType: '',     //数据类型
                            excelTable: '',
                        }
                    ]
                }
            ],
            importFile: ''
        },
        uploader: {
            onuploadcomplet: function (importUrl) {
                //文件上传完成事件
            }
        }
    };
    $.fn.importFile.methods = {
        showModal: function (jq) {
            return jq.each(function () {
                showModal(this);
            })
        }
    };
    //模版Html
    $.fn.importFile.templateHtml = '<div class="form-group downloadExcel" style="margin-bottom:5px;">' +
                                        '<label for="exampleInputFile">下载模版：</label>' +
                                        '<a id="download" href="#" class="btn btn-link" type="button">模版下载</a>' +
                                    '</div>' +
                                    '<div>' +
                                    '<div id="uploader" class="wu-example">' +
                                        '<div>' +
                                            '<label for="exampleInputFile">选择文件：</label>' +
                                            '<div id="picker">选择文件</div>' +
                                        '</div>' +
                                        '<div style="margin-top:10px;font-size:20px;color:red">注：登录密码默认为真实姓名的拼音首字母(大写)</div>' +
                                        '<div id="fileList" class="uploader-list"></div>' +
                                    '</div>' +
                                '</div>';
}(jQuery));
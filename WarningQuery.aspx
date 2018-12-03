<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WarningQuery.aspx.cs" Inherits="PredicTable.WarningQuery" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>灾害警报</title>
    <link rel="stylesheet" href="css/style.default.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>


    <!--Ueditor-->
    <script type="text/javascript" src="Scripts/UEditor/ueditor.config.js"></script>
    <script type="text/javascript" src="Scripts/UEditor/ueditor.all.min.js"></script>
    <script type="text/javascript" src="Scripts/JingBao/JingBao.js"></script>

    <script type="text/javascript" src="js/plugins/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.cookie.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.validate.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.tagsinput.min.js"></script>
    <script type="text/javascript" src="js/plugins/charCount.js"></script>
    <script type="text/javascript" src="js/plugins/ui.spinner.min.js"></script>
    <script type="text/javascript" src="js/plugins/chosen.jquery.min.js"></script>

    <script type="text/javascript" src="js/custom/forms.js"></script>
    <script type="text/javascript" src="js/custom/widgets.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="js/custom/general.js"></script>
    <script type="text/javascript" src="js/custom/tables.js"></script>



    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/Gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />
    <!--webuploader-->
    <link href="Scripts/WebUploader/webuploader.css" rel="stylesheet" />
    <script src="Scripts/WebUploader/webuploader.js"></script>
    <style type="text/css">
        div.selector select {
            font-size: 12px;
            width: auto;
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
        }

        input[type=button] {
            width: auto;
            margin: 0;
            font-weight: bold;
            color: #eee;
            background: #FB9337;
            border: 1px solid #F0882C;
            padding: 7px 10px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            cursor: pointer;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            border-radius: 2px;
        }

            input[type=button]:hover {
                background: #485B79;
                border: 1px solid #3f526f;
            }

        #docTitle {
            font-family: "Microsoft YaHei",微软雅黑,"MicrosoftJhengHei",华文细黑,STHeiti,MingLiu;
            font-size: 26px;
        }
    </style>
    <script>
        $(function () {
            $("#ckbUpLoadImg").attr("checked", "checked");
        })
    </script>
</head>
<body>
    <div class="bodywrapper">
        <div id="contentwrapper" class="contentwrapper" style="margin-left: 50px; margin-top: 60px">
            <div class="selects" style="width: 900px">
                <div style="float: left;" id="div_wainWarningType">
                    警报类型：<select id="wainWarningType" class="uniformselect">
                        <option value="HL">海浪</option>
                        <option value="HB">海冰</option>
                        <option value="FBC">风暴潮</option>
                    </select>
                </div>
                <div style="float: left; margin-left: 15px;">
                    发布类型：<select id="wainType" class="uniformselect">
                        <option value="XX">消息</option>
                        <option value="JB">警报</option>
                        <option value="JC">警报解除通报</option>
                    </select>
                </div>
                <div style="float: left; margin-left: 15px;">
                    警报发布单位：<select id="wainPBUnit" class="uniformselect">
                        <option value="NCS">国家海洋局北海预报中心</option>
                        <option value="SD">山东省海洋预报台</option>
                    </select>
                </div>

                <div style="float: left; margin-left: 15px; display: none;" id="div_wainFBType">
                    风暴潮类型：<select id="wainFBType" class="uniformselect">
                        <option value="WD">温带</option>
                        <option value="TY">台风</option>
                    </select>
                </div>

                <div style="float: left; margin-left: 15px; display: none;" id="div_wainLevel">
                    警报级别：<select id="wainLevel" class="uniformselect">
                        <option value="Ⅳ">Ⅳ级</option>
                        <option value="Ⅲ">Ⅲ级</option>
                        <option value="Ⅱ">Ⅱ级</option>
                        <option value="Ⅰ">Ⅰ级</option>
                    </select>
                </div>
                <input type="hidden" id="div_wainColor" colorvalue="红" value="R" />
                <%--<div style="float: left; display: none; margin-left: 15px;" id="div_wainColor"> 
                          警报级别颜色：
                         <select id="wainColor" class="uniformselect">
                             <option value="B">蓝</option>
                             <option value="Y">黄</option>
                             <option value="O">橙</option>
                             <option value="R">红</option>
                         </select>
                    </div>--%>
                <%--<div style="float: left; margin-left: 15px;">
                        发往分组：<select id="wainGroup" class="uniformselect">
                            <option value="">请选择</option>
                        </select>
                    </div>--%>
                <div style="float: left; margin-left: 15px;">
                    <div style="float: left; margin-left: 5px">联系人：</div>
                    <div style="float: left; margin-left: 5px">
                        <select id="wainUser" class="uniformselect">
                            <option value="">请选择</option>
                        </select>
                    </div>
                    <div style="float: left; margin-left: 5px" id="wuser">
                        <select id="wainUser1" class="uniformselect">
                            <option value="">请选择</option>
                        </select>
                    </div>
                </div>
                 <%--  <div style="float: left; margin-left: 15px;">
                        发往分组：<select id="wainGroup" class="uniformselect">
                            <option value="">请选择</option>
                        </select>
                    </div>--%>
                <div style="float: left; margin-left: 15px; margin-top: 7px;">
                    发布日期：<input id="warnPBTime" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser" />
                </div>

                <div style="float: left; margin-left: 15px;">
                    时间：
                        <select id="select_hour" name="select" class="uniformselect" style="height: 21px;">
                        </select>
                    时
                        <script>
                            //加载日期 ‘时’ 
                            var hour_str = "";
                            for (var i = 0; i < 24; i++) {
                                if (i < 10) {
                                    i = "0" + i;
                                }
                                hour_str += "<option>" + i + "</option>";
                            }
                            $("#select_hour").append(hour_str);
                            //获取当前时
                            var myDate = new Date();
                            var hour = myDate.getHours();
                            if (hour < 10) {
                                hour = "0" + hour;
                            }
                            $('#select_hour').val(hour);
                        </script>
                </div>
                <div style="clear: both">
                    <input type="button" id="Shows" value="显示隐藏" /></div>
               <%-- <div style="float: left; margin-left: 20px;display:none" id="bianji">
                    <input type="button" id="startoperation" value="开始编写文档" />
                </div>--%>
                <div style="margin-left:200px;margin-top:100px; width: 500px;" id="upload">
                    <div>
                        <input type="checkbox" value="上传图片" id="ckbUpLoadImg"/>上传图片
                    </div>
                    <div>
                        <div>发往备注</div>
                        <textarea id="fawangbz" style="width: 450px; height: 80px; resize: none;"></textarea>
                    </div>

                    <div id="conlist" style="width: 450px; height: 60px; overflow:hidden;overflow-y:auto; margin-top: 10px; font-size: 16px"></div>
                    <div id="uploader" class="wu-example">
                        <!--用来存放文件信息-->
                        <div id="fileList" class="uploader-list"></div>
                        <div class="btns" id="tjbtn">
                            <div id="picker" style="width: 100px; height: 40px; font-size: 20px">选择文件</div>

                        </div>
                    </div>

                    <div id="conlist1" style="width: 450px; height: 40px; overflow:hidden;overflow-y:auto; font-size: 16px"></div>
                    <div id="uploader1" class="wu-example">
                        <!--用来存放文件信息-->
                        <div id="fileList1" class="uploader-list"></div>
                        <div class="btns" id="tjbtn1">
                            <div id="picker1" style="font-size: 20px;">选择图片</div>
                            <div>注：图片名同word文件名，扩展名为.jpg或.png或.bmp</div>
                        </div>
                    </div>
                </div>
                <div style="clear: both;"></div>
            </div>
        </div>
        <div style="margin-top: 50px;display:none" id="bodys">
            <div style="float: left; margin-left: 100px">
                <h2 id="Dwei">国家海洋局北海预报中心</h2>
            </div>
            <div style="float: left; margin-left: 20px; margin-left: 223px; display: none;" id="colors">
                <input type="text" style="width: 25px;" value="红" id="cols" />色
            </div>
            <div style="clear: left; margin-left: 300px; padding-top: 30px">
                <h1 id="waves" style="float: left">海浪</h1>
                <h1 id="Types">消息</h1>
            </div>
            <div style="margin-left: 100px; margin-top: 75px; font-size: 15px">时间： <span id="times"></span><span id="TM"></span></div>

            <div style="margin-left: 585px; font-size: 15px; float: left; margin-top: -80px">
                签发：
                        <img id="Qfaimg" style="width: 150px;" src="Images/JingBaoImg/quanfa.png" />
            </div>
            <div id="wainNo" style="margin-left: 100px; font-size: 15px; clear: left; display: none;">编号：<span id="Fbchao">海浪</span><span id="Yse">B</span><span id="FBType" style="display: none">WD</span><input type="text" id="Rqi" style="width: 75px" />-<input type="text" id="Bhao" value="01" style="width: 18px" /></div>
            <%--<div style="clear:both"></div>--%>
            <div style="height: 20px;">
            </div>
            <div id="docTitle" style="margin-left: 270px;">
                海浪消息
            </div>
            <div style="margin-top: 35px; margin-left: 100px; width: 800px; clear: left">
                <div id="ueditor" style="height: 600px; width: 800px"></div>
            </div>
            <div>
            </div>
            <div style="margin-left: 100px; width: 800px; font-size: 15px; margin-top: 30px">
                <span><strong>发往：</strong></span><textarea id="Fwang" style="width: 800px; height: 200px; font-size: 16px; resize: none"></textarea>
            </div>
            <div style="margin-left: 100px; margin-top: 30px; font-size: 15px">
                <hr style="margin-left: 0px; width: 800px; text-align: left" />
                <div>联系人：<span id="content"></span><span id="content1" style="margin-left: 15px"></span><span style="margin-left: 300px">电  话：0532-58750688</span></div>
                <div><span>传  真：0532-58750682 </span><span style="margin-left: 252px">网  址：www.nmfc.org.cn</span></div>
                <hr style="margin-left: 0px; width: 800px; text-align: left" />
            </div>
             <div style="float: left; margin-left: 855px; margin-top: 20px">
             <input type="button" id="btn_save" value="提交" />
             </div>
        </div>

        <script type="text/javascript">
            function myformatter(date) {
                var y = date.getFullYear();
                var m = date.getMonth() + 1;
                var d = date.getDate();
                return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
            }

            function myformatter1(date) {
                var y = date.getFullYear();
                var m = date.getMonth() + 1;
                var d = date.getDate();
                return y + '年' + (m < 10 ? ('0' + m) : m) + '月' + (d < 10 ? ('0' + d) : d) + '日';
            }

            function myparser(s) {
                if (!s) return new Date();
                var ss = (s.split('-'));
                var y = parseInt(ss[0], 10);
                var m = parseInt(ss[1], 10);
                var d = parseInt(ss[2], 10);
                if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                    return new Date(y, m - 1, d);
                } else {
                    return new Date();
                }
            }
            $(function () {
                $('#warnPBTime').datebox('setValue', myformatter(new Date()));
                $('#times').html(myformatter(new Date()).split('-')[0] + "年" + myformatter(new Date()).split('-')[1] + "月" + myformatter(new Date()).split('-')[2] + "日");
                $("#TM").html($('#select_hour').val() + "时");
                $("#Rqi").val(myformatter(new Date()).split('-')[0].substring(2, 4) + myformatter(new Date()).split('-')[1] + myformatter(new Date()).split('-')[2]);
            })
        </script>

        <script>

            //富文本框初始化
            var ue = UE.getEditor('ueditor');

        </script>
    </div>
</body>
</html>

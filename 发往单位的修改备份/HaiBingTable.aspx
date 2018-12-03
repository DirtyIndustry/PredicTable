<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HaiBingTable.aspx.cs" Inherits="PredicTable.HaiBingTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>海冰预报单</title>
    <link rel="stylesheet" href="css/style.default.css" type="text/css" />

    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>

    <!--webuploader-->
    <link href="Scripts/WebUploader/webuploader.css" rel="stylesheet" />
    <script src="Scripts/WebUploader/webuploader.js"></script>
   <%-- <script src="Scripts/WebUploader/importFile.js"></script>--%>
    <!--Ueditor-->
    <script type="text/javascript" src="Scripts/UEditor/ueditor.config.js"></script>
    <script type="text/javascript" src="Scripts/UEditor/ueditor.all.min.js"></script>
    <script type="text/javascript" src="Scripts/HaiBing/HaiBing.js"></script>

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
</head>
<body>
    <div class="bodywrapper">
        <div id="contentwrapper" class="contentwrapper" style="margin-left: 50px; margin-top: 60px">
            <div class="contenttitle2">
                <h3 id="tx1">海冰预报单</h3>
            </div>
            <div class="selects">
                  <div style="float: left; margin-left: 15px;">
                    选择区域 ：<select id="WainArea" class="uniformselect">
                        <option value="北海区">北海</option>
                        <option value="山东近海">山东</option>
                        <option value="冀东油田">冀东</option>
                        <option value="东营胜利油田">胜利</option>
                        <option value="东营近海">东营</option>
                          <option value="青岛近海">青岛</option>
                    </select>
                </div> 
                  <div style="float: left; margin-left: 15px;">
                    模板选择：<select runat="server" id="list" class="uniformselect">
                        <option value="年">预报单--年.doc</option>
                        <option value="月">预报单--月.doc</option>
                        <option value="旬">预报单--旬.doc</option>
                        <option value="周">预报单--周.doc</option>
                    </select>
                </div>
              
                <div style="float: left; margin-left: 15px; margin-top: 7px;">
                    发布日期：<input id="warnPBTime" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser" />
                </div>
                <div style="float: left; margin-left: 15px;">
                    联系人：<select id="wainUser" class="uniformselect">
                        <option value="">请选择</option>
                    </select>
                    <select id="wainUser1" class="uniformselect">
                        <option value="">请选择</option>
                    </select>
                </div>
                <div style="clear: both"><input type="button" id="Shows" value="显示隐藏" /></div>
                <div style="clear: both;display:none" id="bianji">
                    <input type="button" id="startoperation" value="开始编写文档" />
                </div>
                <div style="clear:both;float:left;margin-left:200px;margin-top:50px;display:none" id="WordName1">文件名称: <input type="text"style=" width :300px;height:30px" id="WordName"/>.doc</div>
               
                <div style="width:500px; margin-left:200px;margin-top:100px" id="UploadFiles">
                     <div>
                        <div style="font-size:16px">发往备注</div>
                       <textarea id="fawangbz" style="width:450px;height:80px;resize:none;"></textarea>
                    </div>
                    
                    <div id="conlist" style="width:450px;height:60px;overflow:hidden;overflow-y:auto;margin-top:10px;font-size:16px"></div>
                    <div id="uploader" class="wu-example">
                        <!--用来存放文件信息-->
                        <div id="fileList" class="uploader-list"></div>
                        <div class="btns" id="tjbtn">
                            <div id="picker" style="width:100px;height:40px;font-size: 20px;">选择文件</div>
                          
                        </div>
                    </div> 
                    
                    <div id="conlist1" style="width:450px;height:40px;overflow:hidden;overflow-y:auto;font-size:16px"></div>
                    <div id="uploader1" class="wu-example">
                        <!--用来存放文件信息-->
                        <div id="fileList1" class="uploader-list"></div>
                        <div class="btns" id="tjbtn1">
                            <div id="picker1" style="font-size: 20px;">选择图片</div>

                        </div>
                    </div>
                   
                </div>
                <div style="clear: both;"></div>
            </div>
            <div style="margin-top: 50px;display:none" id="bodys">
                <div style="float: left; margin-left: 585px">
                    <h3 style="float: left">HBY-19</h3>
                </div>
                <div style="clear: left; margin-left: 200px">
                    <h1 id="Dwei" style="font-size: 40px"><strong>海洋环境预报</strong></h1>
                </div>

                <div style="clear: left; margin-left: 300px; padding-top: 30px">
                    <h3 id="waves" style="float: left">（海冰）</h3>
                </div>
                <div style="float: left; margin-left: 20px; margin-left: 192px;" id="colors">
                    <strong style="font-size: 18px">NO.</strong><input type="text" style="width: 25px;" value="" id="cols" />
                </div>

                <div style="margin-left: 100px; margin-top: 75px; font-size: 15px">通信地址: (266061)青岛市云岭路27号</div>
                <div style="margin-left: 100px; font-size: 15px">电    话: (0532) 58750619 58750688</div>
                <div style="margin-left: 100px; font-size: 15px">传    真: (0532) 58750682</div>

                <div id="wainNo" style="margin-left: 100px; font-size: 15px; clear: left;">国家海洋局北海预报中心</div>
                <div style="margin-left: 585px; font-size: 15px; float: left; margin-top: -20px">
                    <span id="times"></span>发布
                </div>
                <div style="height: 20px;">
                </div>
                <%-- <div id="docTitle" style="margin-left:270px;">
                        <input type="text" style="width: 25px;" value="红" id="cols" />
                    </div>--%>
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
                    <div>
                        联系人：<span id="content"></span><span id="content1" style="margin-left: 15px"></span><span style="margin-left: 300px">电  话：0532-58750619 &nbsp&nbsp&nbsp 0532-58750688
                        </span>
                    </div>
                    <div><span>传  真：0532-58750682 </span><span style="margin-left: 295px">网  址：www.nmfc.org.cn</span></div>
                    <hr style="margin-left: 0px; width: 800px; text-align: left" />
                </div>
                 <div style="float: left; margin-left: 855px; margin-top: 20px">
                <input type="button" id="btn_save" value="提交" />
            </div>
            <div style="float: left; margin-left: 1000px; margin-top: 20px; display: none">
                <input type="button" id="btn_save1" value="保存模板文档" />
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
                    $("#WordName").val("YB_NCS_HB_1yr_" + myformatter(new Date()).split('-')[0] + myformatter(new Date()).split('-')[1] + myformatter(new Date()).split('-')[2] + "_NMFC");
                })
            </script>

            <script>

                //富文本框初始化
                var ue = UE.getEditor('ueditor');
            </script>
        </div>
    </div>
</body>
</html>

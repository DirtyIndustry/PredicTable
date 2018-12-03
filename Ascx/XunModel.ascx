<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XunModel.ascx.cs" Inherits="PredicTable.Ascx.XunModel" %>
<style>
    #pickerXunNCS div:nth-child(2),#pickerXunSD div:nth-child(2),#pickerXunNP div:nth-child(2),#pickerXunSL div:nth-child(2),#pickerXunDY div:nth-child(2) {
        width:100% !important;
        height:100% !important;
    }
</style>
<div class="forcastBody">
    <div id="fbNo">
        编号：&nbsp;<input type="text" ID="txtNoX" style="width:160px" />
    </div>

    <div>
        <div class="formNo">
            (一)北海区预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtNCSTitleX" style="width:180px" />预报
        </div>
        <div class="fbDescription">
            预计未来10天，渤、黄海海域的天气过程和海况如下：
        </div>
        <div class="fbContainer">
            <div class="fbContent">
            <textarea ID="txtReportNCSX" style="width:95%;height:70px;" ></textarea>
            </div>
        </div>
         <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploaderXunNCS" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileListXunNCS" class="uploader-list"></div>
                    <div class="btns" id="tjbtnXunNCS">
                        <div id="pickerXunNCS" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
                        <div style="margin-top:10px">注：请选择一个只含表格的word文件</div>
                    </div>
                </div>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
    </div>
    

    <div>
        <div class="formNo">
            (二)山东省预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtSDTitleX" style="width:180px" />预报
        </div>
        <div class="fbDescription">
            预计未来10天，山东附近海域的天气过程和海况如下：
        </div>
        <div class="fbContainer">
            <textarea ID="txtReportSDX" style="width:95%;height:70px;" ></textarea>
        </div>
         <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploaderXunSD" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileListXunSD" class="uploader-list"></div>
                    <div class="btns" id="tjbtnXunSD">
                        <div id="pickerXunSD" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
                        <div style="margin-top:10px">注：请选择一个只含表格的word文件</div>
                    </div>
                </div>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
    </div>
    
    <div>
        <div class="formNo">
            (三)南堡油田预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtNPOILTitleX" style="width:180px" />预报
        </div>
        <div class="fbDescription">
            预计未来10天，南堡油田海域的天气过程和海况如下：
        </div>
        <div class="fbContainer">
            <div class="fbContent">
                <textarea ID="txtContentNPOILX"  style="width:95%;height:70px;" ></textarea>
            </div>
        </div>
         <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploaderXunNP" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileListXunNP" class="uploader-list"></div>
                    <div class="btns" id="tjbtnXunNP">
                        <div id="pickerXunNP" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
                        <div style="margin-top:10px">注：请选择一个只含表格的word文件</div>
                    </div>
                </div>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
    </div>

    <div>
        <div class="formNo">
            (四)胜利油田预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtSLOILTitleX" style="width:180px" />预报
        </div>
        <div class="fbDescription">
            预计未来10天，胜利油田海域的天气过程和海况如下：
        </div>
        <div class="fbContainer">
            <div class="fbContent">
                <textarea ID="txtContentSLOILX"  style="width:95%;height:70px;" ></textarea>
            </div>
        </div>
         <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploaderXunSL" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileListXunSL" class="uploader-list"></div>
                    <div class="btns" id="tjbtnXunSL">
                        <div id="pickerXunSL" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
                        <div style="margin-top:10px">注：请选择一个只含表格的word文件</div>
                    </div>
                </div>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
    </div>

    <div>
        <div class="formNo">
            (五)东营预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtDYOILTitleX" style="width:180px" />预报
        </div>
        <div class="fbDescription">
            预计未来10天，东营海域的天气过程和海况如下：
        </div>
        <div class="fbContainer">
            <div class="fbContent">
                <textarea ID="txtContentDYOILX"  style="width:95%;height:70px;"></textarea>
            </div>
        </div>
         <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploaderXunDY" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileListXunDY" class="uploader-list"></div>
                    <div class="btns" id="tjbtnXunDY">
                        <div id="pickerXunDY" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
                        <div style="margin-top:10px">注：请选择一个只含表格的word文件</div>
                    </div>
                </div>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
    </div>
    <div class="fbFooter">
        <div class="fbHeader">
           <select id="sel_headerX"></select>
        </div>
        <div class="fbDeputyer">
           <select id="sel_deputyerX"></select>
        </div>
    </div>
    <div class="fbButton">
        <input type="button" ID="SaveContentX" value="提交表单"/>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        var PUBLISHTIME = $("#publishTime").datebox("getValue");
        var paramX = "";
        //旬数据提交
        $("#SaveContentX").click(function () {
            paramX = getParamX();
            var url = "/Ajax/NewMediumAndLong.ashx?method=SetMonthOrXun";
            SendData(paramX, url);
        });

        //获取发送参数
        function getParamX() {
            var parasX = {};
            var REPORTNO = $("#txtNoX").val();
            var PUBLISHTIME = $("#publishTime").datebox("getValue");
            var PUBLISHCOMPANY = "";
            var REPORTTITLE = ""; //标题名称，根据时间自动获取的
            var REPORTCONTENT = "";//预报内容
            var REPORTNORTH = "";
            var REPORTSOUTH = "";

            var HEADREPORTER = $("#sel_headerX").val();
            var DEPUTYREPORTER = $("#sel_deputyerX").val();

            //北海
            PUBLISHCOMPANY = "NCS";
            REPORTTITLE = $("#txtNCSTitleX").val();
            REPORTCONTENT = $("#txtReportNCSX").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunModel("EN", "NCS", PUBLISHTIME);
            var NCSM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT,  HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //山东
            PUBLISHCOMPANY = "SD";
            REPORTTITLE = $("#txtSDTitleX").val();
            REPORTCONTENT = $("#txtReportSDX").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunModel("EN", "SD", PUBLISHTIME);
            var SDM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //南堡油田
            PUBLISHCOMPANY = "南堡油田";
            REPORTTITLE = $("#txtNPOILTitleX").val();
            REPORTCONTENT = $("#txtContentNPOILX").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunModel("CN", "南堡油田", PUBLISHTIME);
            var NPOILM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT,  HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //胜利油田
            PUBLISHCOMPANY = "胜利油田";
            REPORTTITLE = $("#txtSLOILTitleX").val();
            REPORTCONTENT = $("#txtContentSLOILX").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunModel("CN", "胜利油田", PUBLISHTIME);
            var SLOILM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT,  HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //东营环境预报
            PUBLISHCOMPANY = "东营环境预报";
            REPORTTITLE = $("#txtDYOILTitleX").val();
            REPORTCONTENT = $("#txtContentDYOILX").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunModel("CN", "东营环境预报", PUBLISHTIME);
            var ContentDYOILM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };
            parasX = {
                NCSM: JSON.stringify(NCSM),
                SDM: JSON.stringify(SDM),
                NPOILM: JSON.stringify(NPOILM),
                SLOILM: JSON.stringify(SLOILM),
                ContentDYOILM: JSON.stringify(ContentDYOILM),
                reportType: JSON.stringify("X"),
                PUBLISHTIME: PUBLISHTIME
            }
            return parasX;
        }
    });
</script>
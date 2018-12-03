<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthModel.ascx.cs" Inherits="PredicTable.Ascx.MonthModel" %>
<style>
    #picker_Month_NCS div:nth-child(2),#picker_Month_SD div:nth-child(2),#picker_Month_NP div:nth-child(2),#picker_Month_SL div:nth-child(2),#picker_Month_DY div:nth-child(2) {
        width:100% !important;
        height:100% !important;
    }
</style>
<div class="forcastBody">
    <div id="fbNo">
        编号：&nbsp;<input type="text" ID="txtNoM" style="width:160px" />
    </div>

    <div>
        <div class="formNo">
            (一)北海区预报
        </div>
        <div class="fbTitle">
            <input type="text" id="txtNCSTitleM" style="width: 180px" />趋势预测
        </div>
        <div class="fbContainer">
            <div class="fbContent">
                <textarea id="txtReportNCSM" style="width: 95%; height: 70px;"></textarea>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploader_Month_NCS" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileList_Month_NCS" class="uploader-list"></div>
                    <div class="btns" id="tjbtn_Month_NCS">
                        <div id="picker_Month_NCS" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
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
            <input type="text" ID="txtSDTitleM" style="width:180px" />趋势预测
        </div>
        <div class="fbContainer">
            <textarea ID="txtReportSDM" style="width:95%;height:70px;" ></textarea>
        </div>
        <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploader_Month_SD" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileList_Month_SD" class="uploader-list"></div>
                    <div class="btns" id="tjbtn_Month_SD">
                        <div id="picker_Month_SD" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
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
            <input type="text" ID="txtNPOILTitleM" style="width:180px" />趋势预测
        </div>
        
        <div class="fbContainer">
            <div class="fbContent">
                <textarea ID="txtContentNPOILM"  style="width:95%;height:70px;" ></textarea>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploader_Month_NP" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileList_Month_NP" class="uploader-list"></div>
                    <div class="btns" id="tjbtn_Month_NP">
                        <div id="picker_Month_NP" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
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
            <input type="text" ID="txtSLOILTitleM" style="width:180px" />趋势预测
        </div>
       
        <div class="fbContainer">
            <div class="fbContent">
                <textarea ID="txtContentSLOILM"  style="width:95%;height:70px;" ></textarea>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploader_Month_SL" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileList_Month_SL" class="uploader-list"></div>
                    <div class="btns" id="tjbtn_Month_SL">
                        <div id="picker_Month_SL" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
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
            <input type="text" ID="txtDYOILTitleM" style="width:180px" />趋势预测
        </div>
        
        <div class="fbContainer">
            <div class="fbContent">
                <textarea ID="txtContentDYOILM"  style="width:95%;height:70px;"></textarea>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
        <div class="fbContainer">
            <div class="fbContentTitle">
                海冰
            </div>
            <div class="fbContent">
                <div id="uploader_Month_DY" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="fileList_Month_DY" class="uploader-list"></div>
                    <div class="btns" id="tjbtn_Month_DY">
                        <div id="picker_Month_DY" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
                        <div style="margin-top:10px">注：请选择一个只含表格的word文件</div>
                    </div>
                </div>
            </div>
        </div>
        <%-- 新添加海冰的word上传 --%>
    </div>
    <div class="fbFooter">
        <div class="fbHeader">
           <select id="sel_headerM" ></select>
        </div>
        <div class="fbDeputyer">
           <select id="sel_deputyerM"></select>
        </div>
    </div>
    <div class="fbButton">
        <input type="button" ID="SaveContentMonth" value="提交表单"/>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        var PUBLISHTIME = $("#publishTime").datebox("getValue");
        var paramM = "";
        //月数据提交
        $("#SaveContentMonth").click(function () {
            paramM = getParamM();
            var url = "/Ajax/NewMediumAndLong.ashx?method=SetMonthOrXun";
            SendData(paramM, url);
        });
        
        //获取发送参数
        function getParamM() {
            var parasM = {};
            var REPORTNO = $("#txtNoM").val();
            var PUBLISHCOMPANY = "";
            var REPORTTITLE = "";
            var REPORTNORTH = "";
            var REPORTSOUTH = "";

            var REPORTCONTENT = "";

            var HEADREPORTER = $("#sel_headerM").val();
            var DEPUTYREPORTER = $("#sel_deputyerM").val();
            var PUBLISHTIME = $("#publishTime").datebox("getValue");

            //北海
            PUBLISHCOMPANY = "NCS";
            REPORTTITLE = $("#txtNCSTitleM").val();
            REPORTCONTENT = $("#txtReportNCSM").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = YueModel("EN", "NCS", PUBLISHTIME);
            var NCSM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //山东
            PUBLISHCOMPANY = "SD";
            REPORTTITLE = $("#txtSDTitleM").val(); 
            REPORTCONTENT = $("#txtReportSDM").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = YueModel("EN", "SD", PUBLISHTIME);
            var SDM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //南堡油田
            PUBLISHCOMPANY = "南堡油田";
            REPORTTITLE = $("#txtNPOILTitleM").val();
            REPORTCONTENT = $("#txtContentNPOILM").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = YueModel("CN", "南堡油田", PUBLISHTIME);
            var NPOILM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //胜利油田
            PUBLISHCOMPANY = "胜利油田";
            REPORTTITLE = $("#txtSLOILTitleM").val();
            REPORTCONTENT = $("#txtContentSLOILM").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = YueModel("CN", "胜利油田", PUBLISHTIME);
            var SLOILM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //东营环境预报
            PUBLISHCOMPANY = "东营环境预报";
            REPORTTITLE = $("#txtDYOILTitleM").val();
            REPORTCONTENT = $("#txtContentDYOILM").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = YueModel("CN", "东营环境预报", PUBLISHTIME);
            var ContentDYOILM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT,  HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };
            parasM = {
                NCSM: JSON.stringify(NCSM),
                SDM: JSON.stringify(SDM),
                NPOILM: JSON.stringify(NPOILM),
                SLOILM: JSON.stringify(SLOILM),
                ContentDYOILM: JSON.stringify(ContentDYOILM),
                reportType: JSON.stringify("M"),
                PUBLISHTIME: PUBLISHTIME
            }
            return parasM;
        }
    });
</script>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthAndXunModel.ascx.cs" Inherits="PredicTable.Ascx.MonthAndXunModel" %>
<div class="forcastBody">
    <div id="fbNo">
        编号：&nbsp;<input type="text" ID="txtNoXorM" style="width:160px" />
    </div>
    <div>
        <div class="formNo">
            (一)北海区预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtNCSTitleXorM" style="width:180px" />
            <span id="txtNCSTitleFixed"></span>
        </div>
        <div class="fbContainer">
            <div class="fbContent">
            <textarea ID="txtReportNCSX" style="width:95%;height:70px;" ></textarea>
            </div>
        </div>
    </div>

    <div>
        <div class="formNo">
            (二)山东省预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtSDTitleXorM" style="width:180px" />
            <span id="txtSDTitleFixed"></span>
        </div>
        <div class="fbContainer">
            <textarea ID="txtReportSDX" style="width:95%;height:70px;" ></textarea>
        </div>
    </div>

    <div>
        <div class="formNo">
            (三)南堡油田预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtNPOILTitleXorM" style="width:180px" />
            <span id="txtNPOILTitleFixed"></span>
        </div>
        <div class="fbDescription">
            预计未来10天，南堡油田海域的天气过程和海况如下：
        </div>
        <div class="fbContainer">
            <div class="fbContent">
                <textarea ID="txtContentNPOILXorM"  style="width:95%;height:70px;" ></textarea>
            </div>
        </div>
    </div>

    <div>
        <div class="formNo">
            (四)胜利油田预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtSLOILTitleXorM" style="width:180px" />
            <span id="txtSLOILTitleFixed"></span>
        </div>
        <div class="fbDescription">
            预计未来10天，胜利油田海域的天气过程和海况如下：
        </div>
        <div class="fbContainer">
            <div class="fbContent">
                <textarea ID="txtContentSLOILXorM"  style="width:95%;height:70px;" ></textarea>
            </div>
        </div>
    </div>

    <div>
        <div class="formNo">
            (五)东营预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtDYOILTitleXorM" style="width:180px" />
            <span id="txtDYOILTitleFixed"></span>
        </div>
        <div class="fbDescription">
            预计未来10天，东营海域的天气过程和海况如下：
        </div>
        <div class="fbContainer">
            <div class="fbContent">
                <textarea ID="txtContentDYOILX"  style="width:95%;height:70px;"></textarea>
            </div>
        </div>
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
        <input type="button" ID="SaveContentXAndM" value="提交表单"/>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        var PUBLISHTIME = $("#publishTime").datebox("getValue");
        var paramXandM = "";
        //月、旬数据提交
        $("#SaveContentXAndM").click(function () {
            paramXandM = getParamXandM();
            var url = "/Ajax/NewMediumAndLong.ashx?method=SetMonthOrXun";
            SendData(paramXandM, url);
        });

        //获取页面参数
        function getParamXandM() {
            var parasXandM = {};
            var REPORTNO = $("#txtNoXorM").val();//模板编码
            var PUBLISHTIME = $("#publishTime").datebox("getValue");//时间
            var PUBLISHCOMPANY = ""; //预报地区
            var REPORTTITLE = ""; //标题名称，根据时间自动获取的
            var REPORTCONTENT = "";//预报内容
            var HEADREPORTER = $("#sel_headerX").val(); //预报员
            var DEPUTYREPORTER = $("#sel_deputyerX").val(); //预报员
            var REPORTNORTH = "";
            var REPORTSOUTH = "";
           
            //北海
            PUBLISHCOMPANY = "NCS";
            REPORTTITLE = $("#txtSDTitleXorM").val();
            REPORTCONTENT = $("#txtReportNCSX").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunAndMonthModel("EN", "NCS", PUBLISHTIME);
            var NCSXandM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //山东
            PUBLISHCOMPANY = "SD";
            REPORTTITLE = $("#txtNCSTitleXorM").val();
            REPORTCONTENT = $("#txtReportSDX").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunAndMonthModel("EN", "SD", PUBLISHTIME);
            var SDXandM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //南堡油田预报
            PUBLISHCOMPANY = "南堡油田";
            REPORTTITLE = $("#txtNPOILTitleXorM").val();
            REPORTCONTENT = $("#txtContentNPOILXorM").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunAndMonthModel("CN", "南堡油田", PUBLISHTIME);
            var NPOILXandM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //胜利油田预报
            PUBLISHCOMPANY = "胜利油田";
            REPORTTITLE = $("#txtSLOILTitleXorM").val();
            REPORTCONTENT = $("#txtContentSLOILXorM").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunAndMonthModel("CN", "胜利油田", PUBLISHTIME);
            var SLOILXandM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //东营环境预报
            PUBLISHCOMPANY = "东营环境预报";
            REPORTTITLE = $("#txtDYOILTitleXorM").val();
            REPORTCONTENT = $("#txtContentDYOILX").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = XunAndMonthModel("CN", "东营环境预报", PUBLISHTIME);
            var ContentDYOILXandM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            parasXandM = {
                NCSXandM: JSON.stringify(NCSXandM),
                SDXandM: JSON.stringify(SDXandM),
                NPOILXandM: JSON.stringify(NPOILXandM),
                SLOILXandM: JSON.stringify(SLOILXandM),
                ContentDYOILXandM: JSON.stringify(ContentDYOILXandM),
                reportType: JSON.stringify("XandM"),
                PUBLISHTIME: PUBLISHTIME
            }
            return parasXandM;
        }//end 获取页面参数
    })//end js
</script>
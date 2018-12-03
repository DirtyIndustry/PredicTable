<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthModel.ascx.cs" Inherits="PredicTable.Ascx.MonthModel" %>
<div class="forcastBody">
    <div id="fbNo">
        编号：&nbsp;<input type="text" ID="txtNoM" style="width:160px" />
    </div>

    <div>
        <div class="formNo">
            (一)北海区预报
        </div>
        <div class="fbTitle">
            <input type="text" ID="txtNCSTitleM" style="width:180px" />趋势预测
        </div>
        <div class="fbContainer">
            <div class="fbContent">
            <textarea ID="txtReportNCSM" style="width:95%;height:70px;" ></textarea>
            </div>
        </div>
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
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT,  HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };

            //山东
            PUBLISHCOMPANY = "SD";
            REPORTTITLE = $("#txtSDTitleM").val(); 
            REPORTCONTENT = $("#txtReportSDM").val();
            REPORTNORTH = "";
            REPORTSOUTH = "";
            var DOCNAME = YueModel("EN", "SD", PUBLISHTIME);
            var SDM = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, REPORTNORTH: REPORTNORTH, REPORTSOUTH: REPORTSOUTH, REPORTCONTENT: REPORTCONTENT,  HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
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
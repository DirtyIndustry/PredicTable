﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SDModel.ascx.cs" Inherits="PredicTable.Ascx.SDModel" %>
<style>
    #picker1 div:nth-child(2) {
        width:100% !important;
        height:100% !important;
    }
</style>
<div class="forcastBody">
    <div id="fbUnit">山东海洋环境年预报</div>
    <div class="fbNo">
        编号：&nbsp;<input type="text" ID="txtNoSD" style="width:160px" />
    </div>
    <div class="fbTitle">
        <input type="text" ID="txtTitleSD" style="width:180px" />预测
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            风暴潮
        </div>
        <div class="fbContent">
           <textarea ID="txt_stormSurgeSD" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            海浪
        </div>
        <div class="fbContent">
            <textarea ID="txt_seaWaveSD" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            赤潮
        </div>
        <div class="fbContent">
            <textarea ID="txt_redTideSD" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            绿潮
        </div>
        <div class="fbContent">
            <textarea ID="txt_greebTideSD" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            热带气旋
        </div>
        <div class="fbContent">
            <textarea ID="txt_tropicalCycloneSD" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>

    <%-- 新添加海冰的word上传 --%>
    <div class="fbContainer">
        <div class="fbContentTitle">
            海冰
        </div>
        <div class="fbContent">
            <div id="uploader1" class="wu-example">
                <!--用来存放文件信息-->
                <div id="fileList1" class="uploader-list"></div>
                <div class="btns" id="tjbtn1">
                    <div id="picker1" style="width: 120px; height: 40px; font-size: 20px">选择文件</div>
                    <div style="margin-top:10px">注：请选择一个只含表格的word文件</div>
                </div>
            </div>
        </div>
    </div>


    <div class="fbFooter">
        <div class="fbHeader">
           <select id="sel_headerSD" ></select>
        </div>
        <div class="fbDeputyer">
           <select id="sel_deputyerSD" ></select>
        </div>
    </div>
    <div class="fbButton">
        <input type="button" ID="SaveContentSD" value="提交表单"/>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        var PUBLISHTIME = $("#publishTime").datebox("getValue");
        var paramSD = "";
        //山东年数据提交
        $("#SaveContentSD").click(function () {
            paramSD = getParamSD();
            var url = "/Ajax/NewMediumAndLong.ashx?method=SetYear";
            SendData(paramSD, url);
        });

        //获取发送参数
        function getParamSD() {
            //var PUBLISHTIME = $("#publishTime").datebox("getValue");
            var PUBLISHCOMPANY  = "SD";
            var REPORTNO        = $("#txtNoSD").val();
            var REPORTTITLE     = $("#txtTitleSD").val();
            var STORMSURGE      = $("#txt_stormSurgeSD").val();
            var SEAWAVE         = $("#txt_seaWaveSD").val();
            var REDTIDE         = $("#txt_redTideSD").val();
            var GREENTIDE       = $("#txt_greebTideSD").val();
            var TROPICALCYCLONE = $("#txt_tropicalCycloneSD").val();
            var HEADREPORTER    = $("#sel_headerSD").val();
            var DEPUTYREPORTER  = $("#sel_deputyerSD").val();

            var DOCNAME = NianModel(PUBLISHCOMPANY, PUBLISHTIME);
            var params = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, STORMSURGE: STORMSURGE, SEAWAVE: SEAWAVE, REDTIDE: REDTIDE, GREENTIDE: GREENTIDE,
                TROPICALCYCLONE: TROPICALCYCLONE, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };
            return params;
        }
    });
</script>
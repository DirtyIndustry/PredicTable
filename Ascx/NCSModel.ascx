<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCSModel.ascx.cs" Inherits="PredicTable.Ascx.NCSModel" %>
<div class="forcastBody">
    <div id="fbUnit">国家海洋环境年预报</div>
    <div class="fbNo">
        编号：&nbsp;<input type="text" ID="txtNoNCS" style="width:160px" />
    </div>
    <div class="fbTitle">
        <input type="text" ID="txtTitleNCS" style="width:180px" />预测
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            风暴潮
        </div>
        <div class="fbContent">
           <textarea ID="txt_stormSurgeNCS" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            海浪
        </div>
        <div class="fbContent">
            <textarea ID="txt_seaWaveNCS" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            赤潮
        </div>
        <div class="fbContent">
            <textarea ID="txt_redTideNCS" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            绿潮
        </div>
        <div class="fbContent">
            <textarea ID="txt_greebTideNCS" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>
    <div class="fbContainer">
        <div class="fbContentTitle">
            热带气旋
        </div>
        <div class="fbContent">
            <textarea ID="txt_tropicalCycloneNCS" style="width:95%;height:56px;" ></textarea>
        </div>
    </div>


    <%-- 新添加海冰的word上传 --%>
    <div class="fbContainer">
        <div class="fbContentTitle">
            海冰
        </div>
        <div class="fbContent">
            <div id="uploader" class="wu-example">
                <!--用来存放文件信息-->
                <div id="fileList" class="uploader-list"></div>
                <div class="btns" id="tjbtn">
                    <div id="picker" style="width: 100px; height: 40px; font-size: 20px">选择文件</div>
                    <div style="margin-top:10px">注：请选择一个只含表格的word文件</div>
                </div>
            </div>
        </div>
    </div>



    <div class="fbFooter">
        <div class="fbHeader">
           <select id="sel_headerNCS"></select>
        </div>
        <div class="fbDeputyer">
           <select id="sel_deputyerNCS"></select>
        </div>
    </div>
    <div class="fbButton">
        <input type="button" ID="SaveContentNCS" value="提交表单"/>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        var PUBLISHTIME = $("#publishTime").datebox("getValue");
        var paramNCS = "";
        //北海年数据提交
        $("#SaveContentNCS").click(function () {
            paramNCS = getParamNCS();
            var url = "/Ajax/NewMediumAndLong.ashx?method=SetYear";
            SendData(paramNCS, url);
        });

        //获取发送参数
        function getParamNCS() {
            //var PUBLISHTIME = $("#publishTime").datebox("getValue");
            var PUBLISHCOMPANY  = "NCS";
            var REPORTNO        = $("#txtNoNCS").val();
            var REPORTTITLE     = $("#txtTitleNCS").val();
            var STORMSURGE      = $("#txt_stormSurgeNCS").val();
            var SEAWAVE         = $("#txt_seaWaveNCS").val();
            var REDTIDE         = $("#txt_redTideNCS").val();
            var GREENTIDE       = $("#txt_greebTideNCS").val();
            var TROPICALCYCLONE = $("#txt_tropicalCycloneNCS").val();
            var HEADREPORTER    = $("#sel_headerNCS").val();
            var DEPUTYREPORTER  = $("#sel_deputyerNCS").val();

            var DOCNAME = NianModel(PUBLISHCOMPANY, PUBLISHTIME);
            var params = {
                PUBLISHTIME: PUBLISHTIME, PUBLISHCOMPANY: PUBLISHCOMPANY, REPORTNO: REPORTNO, REPORTTITLE: REPORTTITLE, STORMSURGE: STORMSURGE, SEAWAVE: SEAWAVE, REDTIDE: REDTIDE, GREENTIDE: GREENTIDE,
                TROPICALCYCLONE: TROPICALCYCLONE, HEADREPORTER: HEADREPORTER, DEPUTYREPORTER: DEPUTYREPORTER, DOCNAME: DOCNAME
            };
            return params;
        }
    });
</script>
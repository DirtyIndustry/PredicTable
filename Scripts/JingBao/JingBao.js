//警报预报表单js文件
//创建人：韩萌真
//创建时间:2016.11.08
var tt;
var filePath = [];
var filenames = [];

var imgPath = [];
var imgnames = [];

var upLoadImgFlag = false;
$(function () {
    //选择文件       
    InitWebUploader();
    //选择图片       
    InitWebUploader1();
    UserBind();//联系人绑定
    UserBind1();//联系人1绑定
    //  GroupBing();//发往分组绑定
    tt = '<table border="1" cellspacing="0" cellpadding="0"><tbody><tr class="firstRow"><td width="109" style="border-width: 1px; border-color: black; padding: 0px 7px;"><p style="text-align:center;line-height:29px"><strong><span style=";font-family:宋体">验潮站</span></strong></p></td><td width="109" style="border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-top-color: black; border-right-color: black; border-bottom-color: black; border-left: none; padding: 0px 7px;"><p style="text-align:center;line-height:29px"><strong><span style=";font-family:宋体">日期</span></strong></p></td><td width="109" style="border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-top-color: black; border-right-color: black; border-bottom-color: black; border-left: none; padding: 0px 7px;"><p style="text-align:center;line-height:29px"><strong><span style=";font-family:宋体">高潮时</span></strong></p></td><td width="109" style="border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-top-color: black; border-right-color: black; border-bottom-color: black; border-left: none; padding: 0px 7px;"><p style="text-align:center;line-height:29px"><strong><span style=";font-family:宋体">高潮值（cm）</span></strong></p></td><td width="127" style="border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-top-color: black; border-right-color: black; border-bottom-color: black; border-left: none; padding: 0px 7px;"><p style="text-align:center;line-height:29px"><strong><span style=";font-family:宋体">警戒潮位（cm）</span></strong></p></td><td width="92" style="border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-top-color: black; border-right-color: black; border-bottom-color: black; border-left: none; padding: 0px 7px;"><p style="text-align:center;line-height:29px"><strong><span style=";font-family:宋体">警报级别</span></strong></p></td></tr><tr><td width="109" valign="top" style="border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-right-color: black; border-bottom-color: black; border-left-color: black; border-top: none; padding: 0px 7px;"><br/></td><td width="109" valign="top" style="border-top: none; border-left: none; border-bottom-width: 1px; border-bottom-color: black; border-right-width: 1px; border-right-color: black; padding: 0px 7px;"><br/></td><td width="109" style="border-top: none; border-left: none; border-bottom-width: 1px; border-bottom-color: black; border-right-width: 1px; border-right-color: black; padding: 0px 7px;"><br/></td><td width="109" style="border-top: none; border-left: none; border-bottom-width: 1px; border-bottom-color: black; border-right-width: 1px; border-right-color: black; padding: 0px 7px;"><br/></td><td width="127" style="border-top: none; border-left: none; border-bottom-width: 1px; border-bottom-color: black; border-right-width: 1px; border-right-color: black; padding: 0px 7px;"><br/></td><td width="92" valign="top" style="border-top: none; border-left: none; border-bottom-width: 1px; border-bottom-color: black; border-right-width: 1px; border-right-color: black; padding: 0px 7px;"><br/></td></tr></tbody></table><p><br/></p>';
    //初始化赋值
    FirstBind();
    //显示隐藏
    $("#Shows").click(function () {
        $("#bodys").toggle();
        $("#bianji").toggle();
        $("#upload").toggle();
    })
    //开始编辑
    $("#startoperation").click(function () {
        UE.getEditor('ueditor').setContent("");
        //  var img = '<p><img src="/Images/JingBaoImg/JingBaos.png" title="JingBaos.png" alt="JingBaos.png"/></p>';
        //UE.getEditor('ueditor').setContent(tt,true);
        if ($("#wainType").val() == "XX")
        {
            //消息绑定
            XiaoXi();
        }
        else if ($("#wainType").val() == "JB")
        {
            //警报绑定
            JingBao();
        }
        else
        {
            //解除警报绑定
            JCJingBao();
        }
     
    })
    //发布类型切换
    $("#wainType").change(function () {
        var wainTypeVale = $("#wainType").val();
        var wainWarningType = $("#wainWarningType").val();
        
        if (wainTypeVale == "JB")
        {
            //警报显示级别、编号、颜色
            $('#div_wainLevel').show();
            $("#wainNo").show();
            $("#colors").show();
            $("#Types").html();
            $("#Types").html("警报");
        }
        else if (wainTypeVale == "JC") {
            //解除警报显示编号
            $("#wainNo").show();
            $('#div_wainLevel').show();
            $("#colors").hide();
            $("#Types").html("警报解除通报");
        }
        else
        {//消息全部隐藏
            $('#div_wainLevel').hide();
            $("#wainNo").hide();
            $("#colors").hide();
            $("#Types").html("消息");
        }
        //标题联动
        TitleAction();
    });

    //警报类型切换
    $("#wainWarningType").change(function () {
        var wainTypeVale = $("#wainType").val();
        var wainWarningType = $("#wainWarningType").val();

        //风暴潮显示风暴潮类型
        if (wainWarningType == "FBC")
        {//风暴潮 
            $('#div_wainFBType').show();
            $('#FBType').show();
            $("#wuser").hide();
            UserBind();
            UserBind1();
            $("#content1").hide();
            wainGroupAction("FBC");

        } else if (wainWarningType == "HL")
        {//海浪
            $('#div_wainFBType').hide();
            $('#FBType').hide();
            $("#wuser").hide();
            UserBind();
            UserBind1();
            $("#content1").hide();
            wainGroupAction("HL");
        }
        else if (wainWarningType == "HB")
        {//海冰
            $('#div_wainFBType').hide();
            $('#FBType').hide();
            $("#wuser").show();
            UserBind();
            UserBind1();
            $("#content1").show();
            wainGroupAction("HB");
        }
        $('#waves').html($("#wainWarningType").find("option:selected").text());
        $('#Fbchao').html($("#wainWarningType").find("option:selected").text());
        //标题联动
        TitleAction();
        //发往联动
        wainGroupAction(wainWarningType);
    });

    //警报发布单位切换
    $("#wainPBUnit").change(function () {
        $('#Dwei').html($("#wainPBUnit").find("option:selected").text());
    })

    //风暴潮类型切换
    $("#wainFBType").change(function () {
        $('#FBType').html($("#wainFBType").val());
    })

    //联系人切换
    $("#wainUser").change(function () {
        $("#content").html($("#wainUser").val());
    })

    //联系人切换
    $("#wainUser1").change(function () {
        $("#content1").html($("#wainUser1").val());
    })
    //发往组切换
    //$("#wainGroup").change(function () {
    //    $("#Fwang").val($("#wainGroup").val());
    //})

    //警报级别切换
    $("#wainLevel").change(function () {
        var level = $('#wainLevel').val();
        LevelColor(level);
        var colorValue = $('#div_wainColor').attr("colorValue");
        var color = $('#div_wainColor').val();
        $('#Yse').html(color);
        $('#cols').val(colorValue);
        TitleAction();
    })

    //日期切换
    $('#warnPBTime').datebox({
        onSelect: function (date) {
            var waintime = $("#warnPBTime").datebox("getValue");
            $('#times').html(waintime.split('-')[0] + "年" + waintime.split('-')[1] + "月" + waintime.split('-')[2] + "日");
        }
    });

    //时间切换
    $("#select_hour").change(function () {
        var select_hour = $("#select_hour").val();
        $('#TM').html(select_hour+"时");
    })

    //警报保存
    $("#btn_save").click(function () {
        var param;
        var Url;
        var aa = UE.getEditor('ueditor').getPlainTxt();
      
        //if ($("#wainGroup").val() == "")
        //{
        //    alert("请选择发往分组。");
        //    return false;
        //}
        //if ($("#wainUser").val() == "") {
        //    alert("请选择联系人。");
        //    return false;
        //}

        var waintime = $("#warnPBTime").datebox("getValue");
        var wainPBUnit=$("#wainPBUnit").val();
        var pb;
        var PBU = $("#wainPBUnit").val();
        if(wainPBUnit=="NCS")
        {//国家海洋局北海预报中心
            pb="_NMFC";
        }
        else
        {//山东省海洋预报台
            pb="_SDMF";
        }
        if ($("#wainPBUnit").val() == "SD" && $("#wainType").val() == "XX")
        {
            PBU = "NCS";
        }
       
        //警报文件内容
        UE.getEditor('ueditor').addInputRule(function (root) {
            $.each(root.getNodesByTagName('div'), function (i, node) {
                node.tagName = "p";
            });
        });
        CONTENT = UE.getEditor('ueditor').getContent().replace(new RegExp('<p>', 'gm'), '<div>').replace(new RegExp('</p>', 'gm'), '</div>').replace(new RegExp('<p', 'gm'), '<div');
        SENTTO = $("#Fwang").val();
        ISSUEPICTURE = $("#Qfaimg").attr('src');
        
       
      
        //警报文件属性
        if ($("#wainPBUnit").val() == "NCS") {
            JBQUYU = "北海区";
        }
        else {
            JBQUYU = "北海区";
        }

        JBNEIRONG = $("#wainWarningType").find("option:selected").text();
        // var waintime = $("#warnPBTime").datebox("getValue");
        //  var time = waintime.split('-')[0]+ waintime.split('-')[1] + waintime.split('-')[2];

        if ($("#wainWarningType").val() == "FBC") {//风暴潮
            LINKMAN = $("#wainUser").val();
            if ($("#wainType").val() == "XX") {//消息
                bianhao = "";
                JBBIANHAO = "";
                JBWENJIANMING = $("#wainType").val() + '_' + PBU + '_' + $("#wainWarningType").val() + '_' + waintime.split('-')[0] + waintime.split('-')[1] + waintime.split('-')[2]  + '_' + waintime.split('-')[0] + waintime.split('-')[1] + waintime.split('-')[2] + $('#select_hour').val() + pb + '.doc';

            }
            else {
                //警报
                bianhao =$("#Yse").html() + $("#FBType").html() + $("#Rqi").val() + $("#Bhao").val();
                JBBIANHAO = $("#Fbchao").html() + $("#FBType").html() + $("#Rqi").val()+"-"+ $("#Bhao").val();
                JBWENJIANMING = $("#wainType").val() + '_' + PBU + '_' + $("#wainWarningType").val() + '_' + $("#Yse").html() + $("#FBType").html() + $("#Rqi").val() + $("#Bhao").val() + '_' + waintime.split('-')[0] + waintime.split('-')[1] + waintime.split('-')[2] + $('#select_hour').val() + pb + '.doc';
            }

        }
        else if ($("#wainWarningType").val() == "HL") {//海浪
            LINKMAN = $("#wainUser").val();
            if ($("#wainType").val() == "XX") {//消息
                bianhao = "";
                JBBIANHAO = "";
                JBWENJIANMING = $("#wainType").val() + '_' + PBU + '_' + $("#wainWarningType").val() + '_' + waintime.split('-')[0].substring(2, 4) + waintime.split('-')[1] + waintime.split('-')[2] + $('#select_hour').val() + pb + '.doc';
            }
            else {//警报
                bianhao =$("#Yse").html() + "20"+$("#Rqi").val() + $("#Bhao").val();
                JBBIANHAO = $("#Fbchao").html() + $("#Rqi").val() + "-"+$("#Bhao").val();
                JBWENJIANMING = $("#wainType").val() + '_' + PBU + '_' + $("#wainWarningType").val() + '_' + $("#Yse").html() +"20"+ $("#Rqi").val() + $("#Bhao").val() + '_' + waintime.split('-')[0] + waintime.split('-')[1] + waintime.split('-')[2] + $('#select_hour').val() + pb + '.doc';
            }

        }
        else {//海冰
            LINKMAN = $("#wainUser").val() + " " + $("#wainUser1").val();
            if ($("#wainType").val() == "XX") {//消息
                bianhao = "";
                JBBIANHAO = "";
                JBWENJIANMING = $("#wainType").val() + '_' + PBU + '_' + $("#wainWarningType").val() + '_' + waintime.split('-')[0].substring(2, 4) + waintime.split('-')[1] + waintime.split('-')[2] + $('#select_hour').val() + pb + '.doc';
            }
            else {//警报
                bianhao = $("#Yse").html() + $("#Rqi").val();
                JBBIANHAO = $("#Fbchao").html() + $("#Rqi").val()+"-"+ $("#Bhao").val();
                JBWENJIANMING = $("#wainType").val() + '_' + PBU + '_' + $("#wainWarningType").val() + '_' + $("#Yse").html() + $("#Rqi").val() + $("#Bhao").val() + '_' + waintime.split('-')[0] + waintime.split('-')[1] + waintime.split('-')[2] + $('#select_hour').val() + pb + '.doc';
            }
        }
        JBJIBIE = $('#div_wainColor').attr("colorValue") + "色预警";
        JBSHIJIAN = $("#warnPBTime").datebox("getValue") +" "+$('#select_hour').val()+":00:00";
        JBDANWEI = $("#wainPBUnit").find("option:selected").text();
        if (JBDANWEI == "国家海洋局北海预报中心")
        {
            JBDANWEI = "北海预报中心";
        }
        var DOCTITLE = $('#docTitle').html();
        //传参
        param = {
            JBWENJIANMING: JBWENJIANMING, CONTENT: CONTENT, SENTTO: SENTTO, ISSUEPICTURE: ISSUEPICTURE, LINKMAN: LINKMAN,
            JBQUYU: JBQUYU, JBNEIRONG: JBNEIRONG, JBBIANHAO: JBBIANHAO, JBJIBIE: JBJIBIE, JBSHIJIAN: JBSHIJIAN, JBDANWEI: JBDANWEI,
            Time: waintime.split('-')[0] + "-" + waintime.split('-')[1] + "-" + waintime.split('-')[2], XXTime: waintime.split('-')[0] + waintime.split('-')[1] + waintime.split('-')[2], Hour: $('#select_hour').val(), Year: waintime.split('-')[0], times: $("#times").html(), Types: $("#Types").html(), TM: $('#select_hour').val(), Yse: $("#wainColor").val(),
            DOCTITLE: DOCTITLE, bianhao: bianhao
        }

        if ($("#wainType").val() == "XX")
        {//消息
            Url = "Ajax/WarningQuery.ashx?method=AddXiaoXi";
        }
        else if ($("#wainType").val() == "JB")
        {//警报
            Url = "Ajax/WarningQuery.ashx?method=AddJingBao";
        }
        else
        {//警报解除通报
            Url = "Ajax/WarningQuery.ashx?method=AddJieChuJingBao";
        }

        $.ajax({
            type: "POST",
            url: Url,
            data: param,

            success: function (result) {
                alert(result);
            },
            error: function (data) {
                alert(result);
            }

        })
        
    })
    //首次加载数据绑定
    function FirstBind() {
        //发布类型
        if ($("#wainType").val() == "JB")
        {
            $('#div_wainLevel').show();
            $("#wainNo").show();
            $("#colors").show();
            $("#Types").html();
            $("#Types").html("警报");
        }
        else if ($("#wainType").val() == "JC") {
            $("#wainNo").show();
            $('#div_wainLevel').show();
            $("#colors").hide();
            $("#Types").html("警报解除通报");
        }
        else {
            $('#div_wainLevel').hide();
            $("#wainNo").hide();
            $("#colors").hide();
            $("#Types").html("消息");
        }
        //警报级别
        var level = $('#wainLevel').val();
        LevelColor(level);
        var colorValue = $('#div_wainColor').attr("colorValue");
        var color = $('#div_wainColor').val();
        $('#Yse').html(color);
        $('#cols').val(colorValue);
        TitleAction();
        //警报类型
        if ($("#wainWarningType").val() == "FBC") {
            //风暴潮显示风暴潮类型
            $('#div_wainFBType').show();
            $('#FBType').show();
            $('#wuser').hide();
            $("#content1").hide();
          
        }
        else if($("#wainWarningType").val() == "HL") {
            $('#div_wainFBType').hide();
            $('#FBType').hide();
            $('#wuser').hide();
            $("#content1").hide();
           
        }
        else {
            $('#div_wainFBType').hide();
            $('#FBType').hide();
            $('#wuser').show();
            $("#wainUser").find("option[value='黎舸']").attr("selected", true);
            $("#wainUser1").find("option[value='焦艳']").attr("selected", true);
            $("#content1").show();
            }
       

        //发往首次绑定
        wainGroupAction("HL");
// $("#Fwang").val($("#wainGroup").val());
$("#content").html($("#wainUser").val());//联系人切换
$("#content1").html($("#wainUser1").val());//联系人切换
$('#FBType').html($("#wainFBType").val());
$('#Dwei').html($("#wainPBUnit").find("option:selected").text());
$('#waves').html($("#wainWarningType").find("option:selected").text());
$('#Fbchao').html($("#wainWarningType").find("option:selected").text());
}
//联系人绑定数据
function UserBind() {
    $.ajax({
        type: "POST",
        url: "Ajax/WarningQuery.ashx?method=getContents",
        data: {},
        dataType: "json",
        async:false,
        success: function (data) {
            $("#wainUser").html("");
            if (data.length > 0) {
                var strs = "";
                for (var i = 0; i < data.length; i++) {
                    strs += "<option value='" + data[i]["CONTENTSNAME"] + "'>" + data[i]["CONTENTSNAME"] + "</option>";
                }
                $("#wainUser").append(strs);
                $("#content").html($("#wainUser").val());

                if ($("#wainWarningType").val() == "HB") {
                    $("#wainUser").prev().text("黎舸");
                }
                else {
                    var user = $("#wainUser").get(0)[0].text;
                    $("#wainUser").prev().text(user);
                }
            }
           
        },
        error: function (data) {}

    });
 
}
//联系人绑定数据
function UserBind1() {
    $.ajax({
        type: "POST",
        url: "Ajax/WarningQuery.ashx?method=getContents",
        data: {},
        dataType: "json",
        async: false,
        success: function (data) {
            $("#wainUser1").html("");
            if (data.length > 0) {
                var strs = "";
                for (var i = 0; i < data.length; i++) {
                    strs += "<option value='" + data[i]["CONTENTSNAME"] + "'>" + data[i]["CONTENTSNAME"] + "</option>";
                }
                $("#wainUser1").append(strs);
                $("#content1").html($("#wainUser1").val());

                if ($("#wainWarningType").val() == "HB") {
                    $("#wainUser1").prev().text("焦艳");
                }
            }
           
        },
        error: function (data) { }

    });

}
//发往分组绑定
function GroupBing() {
    $.ajax({
        type: "POST",
        url: "Ajax/WarningQuery.ashx?method=getGroup",
        data: {},
        dataType: "json",
        async: false,
        success: function (data) {
            $("#wainGroup").html("");
            if (data.length > 0) {
                var strs = "";
                for (var i = 0; i < data.length; i++) {
                    strs += "<option value='" + data[i]["UNITNAME"] + "'>" + data[i]["GROUPNAME"] + "</option>";
                }
            }
            $("#wainGroup").append(strs);
            $("#Fwang").val($("#wainGroup").val());
        
        },
        error: function (data) { }

    });
}
    
//消息绑定
function XiaoXi() {
    var param = { XXDANWEI: $("#wainPBUnit").find("option:selected").text(), XXNEIRONG: $("#wainWarningType").find("option:selected").text() }
    $.ajax({
        type: "POST",
        url: "Ajax/WarningQuery.ashx?method=getXinXi",
        data: param,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.length > 0) {
                UE.getEditor('ueditor').setContent(data[0]["CONTENT"]);
                $("#Fwang").val(data[0]["SENTTO"]);
                $("#content").html(data[0]["LINKMAN"]);
                //   UE.getEditor('ueditor').setContent(tt, true);//表格
            }
            else {
                //  UE.getEditor('ueditor').setContent(tt, true);//表格
            }
        },
        error: function (data) { }
    });
}
//警报绑定
function JingBao() {
    var param = { JBDANWEI: $("#wainPBUnit").find("option:selected").text(), JBNEIRONG: $("#wainWarningType").find("option:selected").text() }
    $.ajax({
        type: "POST",
        url: "Ajax/WarningQuery.ashx?method=getJingBao",
        data: param,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.length > 0) {
                UE.getEditor('ueditor').setContent(data[0]["CONTENT"]);
                $("#Fwang").val(data[0]["SENTTO"]);
                $("#content").html(data[0]["LINKMAN"]);
                   
                if (data[0]["JBNEIRONG"]!= "风暴潮") {
                    //海浪 or 海冰"海浪R16111701"
                    $("#Fbchao").html(data[0]["JBNEIRONG"]);//海浪
                    $("#Yse").html(data[0]["JBBIANHAO"].substring(0, 1));//B
                    $("#Rqi").val(data[0]["JBBIANHAO"].substring(1, 7));//161117
                    $("#Bhao").val(data[0]["JBBIANHAO"].substring(7, 9));//01
                }
                else {
                    //风暴潮"风暴潮RTY16111701"
                    $("#Fbchao").html(data[0]["JBNEIRONG"]);//风暴潮
                    $("#Yse").html(data[0]["JBBIANHAO"].substring(0, 1));//B
                    $("#FBType").html(data[0]["JBBIANHAO"].substring(1, 3));//TY
                    $("#Rqi").val(data[0]["JBBIANHAO"].substring(3, 9));//161117
                    $("#Bhao").val(data[0]["JBBIANHAO"].substring(9, 11));//01

                }
            }
            else {
                UE.getEditor('ueditor').setContent(tt, true);//表格
            }
        },
        error: function (data) { }
    });

}
//解除警报绑定
function JCJingBao() {
    var param = { JCDANWEI: $("#wainPBUnit").find("option:selected").text(), JCNEIRONG: $("#wainWarningType").find("option:selected").text() }
    $.ajax({
        type: "POST",
        url: "Ajax/WarningQuery.ashx?method=getJCJingBao",
        data: param,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.length > 0) {
                UE.getEditor('ueditor').setContent(data[0]["CONTENT"]);
                $("#Fwang").val(data[0]["SENTTO"]);
               // $("#content").html(data[0]["LINKMAN"]);
                if (data[0]["JBBIANHAO"]!= "风暴潮") {
                    //海浪"海浪R16111701"
                    $("#Fbchao").html(data[0]["JBNEIRONG"]);//海浪
                    $("#Yse").html(data[0]["JBBIANHAO"].substring(0, 1));//B
                    $("#Rqi").val(data[0]["JBBIANHAO"].substring(1, 7));//161117
                    $("#Bhao").val(data[0]["JBBIANHAO"].substring(7, 9));//01


                }
                else {//风暴潮"风暴潮RTY16111701"
                    $("#Fbchao").html(data[0]["JBNEIRONG"]);//风暴潮
                    $("#Yse").html(data[0]["JBBIANHAO"].substring(0, 1));//B
                    $("#FBType").html(data[0]["JBBIANHAO"].substring(1, 3));//TY
                    $("#Rqi").val(data[0]["JBBIANHAO"].substring(3, 9));//161117
                    $("#Bhao").val(data[0]["JBBIANHAO"].substring(9, 11));//01
                }
                   
            }
        },
        error: function (data) { }
    });

}

//标题联动
function TitleAction() {
    var wainWarningType = $("#wainWarningType").val();//警报类型（HL、FBC）
        
    var docTitle;
    switch (wainWarningType) {
        case "HL":
            docTitle = HLTitle();
            break;
        case "HB":
            docTitle = HBTitle();
            break;
        case "FBC":
            docTitle = FBCTitle();
            break;
        default:
            break;
    }
    $('#docTitle').html(docTitle);
}
//生成海浪标题
function HLTitle() {
    var wainTypeVale = $("#wainType").val();//发布类型（XX、JB、JC）
    var titleHL;
    switch (wainTypeVale) {
        case "XX":
            titleHL = "海浪消息";
            break;
        case "JB":
            titleHL = "海浪" + $("#wainLevel").val() + "级警报(" + $('#div_wainColor').attr("colorValue") +"色)";
            break;
        case "JC":
            titleHL = "海浪" + $("#wainLevel").val() + "级警报(" + $('#div_wainColor').attr("colorValue") +"色)解除通报";
            break;
        default:
            break;
    }
    return titleHL;
}
//生成海冰标题
function HBTitle() {
    var wainTypeVale = $("#wainType").val();//发布类型（XX、JB、JC）
    var titleHB;
    switch (wainTypeVale) {
        case "XX":
            titleHB = "海冰消息";
            break;
        case "JB":
            titleHB = "海冰" + $("#wainLevel").val() + "级警报(" + $('#div_wainColor').attr("colorValue") + "色)";
            break;
        case "JC":
            titleHB = "海冰" + $("#wainLevel").val() + "级警报(" + $('#div_wainColor').attr("colorValue") + "色)解除通报";
            break;
        default:
            break;
    }
    return titleHB;
}
//生成风暴潮标题
function FBCTitle() {
    var wainTypeVale = $("#wainType").val();//发布类型（XX、JB、JC）
    var titleFBC;
    switch (wainTypeVale) {
        case "XX":
            titleFBC = "风暴潮消息";
            break;
        case "JB":
            titleFBC = "风暴潮" + $("#wainLevel").val() + "级警报(" + $('#div_wainColor').attr("colorValue") +"色)";
            break;
        case "JC":
            titleFBC = "风暴潮警报解除通报";
            break;
        default:
            break;
    }
    return titleFBC;
}

//等级、颜色联动
//放到隐藏元素，方便取值
function LevelColor(level) {
    var color, colorValue;

    switch (level) {
        case "Ⅰ":
            colorValue = "红";
            color = "R";
            break;
        case "Ⅱ":
            colorValue = "黄";
            color = "Y";
            break;
        case "Ⅲ":
            colorValue = "橙";
            color = "O";
            break;
        case "Ⅳ":
            colorValue = "蓝";
            color = "B";
            break;
        default:
            break;
    }
    $('#div_wainColor').attr("colorValue", colorValue);
    $('#div_wainColor').val(color);
}
//发往绑定
function wainGroupAction(wainTypeVale) {
    var param = {};
    if (wainTypeVale == "FBC") {//风暴潮 
        param.FAXGROUP = "警报风暴潮";
    } else if (wainTypeVale == "HL") {//海浪
        param.FAXGROUP = "警报海浪";
    }
    else if (wainTypeVale == "HB") {//海冰
        param.FAXGROUP = "警报海冰";
    }

    $.ajax({
        type: "POST",
        url: "Ajax/WarningQuery.ashx?method=getGroup",
        data: param,
        dataType: "json",
        async: false,
        success: function (data) {
            var FAXGROUP="";
            if (data.length > 0)
            {
                for (var i = 0; i < data.length; i++) {
                    FAXGROUP += data[i].USERNAME + ";";
                }
                $("#Fwang").val(FAXGROUP.substring(0, FAXGROUP.length - 1));
            }
            else
            { $("#Fwang").val(""); }
             

        },
        error: function (data) { }

    });
}
})
//上传附件封装方法
function InitWebUploader() {
    var ret = 0;
    var BZ = $("#fawangbz").val();
    var uploader = WebUploader.create({
        auto: true,
        // swf文件路径
        swf: '/Scripts/WebUploader/Uploader.swf',
        // 文件接收服务端。
        //server: 'Ajax/FileUploader.ashx',
        server: 'Ajax/HBWarningUploader.ashx',
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: {
            id: $("#picker"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
        resize: false,
        //上传文件格式
        accept: {
            title: 'Images',
            extensions: 'doc',
            // mimeTypes: 'image/*'
        },
        formData: { FaWangbz: BZ }
        //,
        //formData: { WainArea: $("#WainArea").val(), ListType: $("#list").val(),JBSHIJIAN :$("#warnPBTime").datebox("getValue") }
    });

    uploader.on('fileQueued', function (file) {
        if ($('#ckbUpLoadImg').is(":checked")) {
            if (upLoadImgFlag == false) {
                alert("请先上传图片！");
                uploader.cancelFile(file);
                return false;
            }
        }
        var confirms = confirm("确认上传文件名为" + file.name + "的文件吗？");
        if (confirms==false) {
            uploader.cancelFile(file);
            return false;
        }
        //文件名弹框提示
        //CheckFileName(file.name, false);
        uploader.options.formData.fawangbz = $("#fawangbz").val();
        filenames.push(file.name);
        var $list = $("#fileList");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete" style="cursor: pointer">删除</a>' +
            '<p class="state">等待上传...</p>' +
        '</div>');

        $list.on('click', '.delete', function () {

            var id = $(this).parent().attr("id");
            uploader.removeFile(id, true);
            $(this).parent().remove();
            var name = $(this).prev().attr("id");
            var filename = $(this).parent().attr("filename").match(/[^\/]*$/)[0];//通过正则表达式获取字符串路径下的文件名称
            var param = { fileName: filename };

            // var url = "Ajax/FileUploader.ashx?method=DeleteFile";

            $.ajax({
                type: "POST",
                url: "Ajax/HBWarningUploader.ashx?method=DeleteFile",
                data: param,
                dataType: "json",
                async: false,
                success: function (data) {
                },
                error: function (data) { }

            });

            //删除文件后删除数组中的文件名
            filenames.splice($.inArray(filename, filenames), 1);
        });
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

        $li.find('p.state').text('上传中');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {

        $("#conlist").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('已上传');
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { alert("您选择上传文件的格式不正确，请重新选择!<br><span>文件后缀例:doc</span>"); }
        else if (type == "F_DUPLICATE") {
            alert("您选择的文件已经上传，请重新选择!");
        }
        else if (type == "F_EXCEED_SIZE") {
            alert("您选择的文件大于100M，请重新选择!");
        }
    });
    uploader.upload();
}


//上传附件封装方法
function InitWebUploader1() {

    var uploader = WebUploader.create({
        auto: true,
        // swf文件路径
        swf: '/Scripts/WebUploader/Uploader.swf',
        // 文件接收服务端。
        server: 'Ajax/WarningImgUploader.ashx',
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: {
            id: $("#picker1"),
            multiple: false
        },
        // chunkSize: '100M',
        fileSingleSizeLimit: 1024 * 1024 * 100,
        // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
        resize: false,
        //上传文件格式
        accept: {
            title: 'Images',
            extensions: 'gif,jpg,jpeg,bmp,png',
            mimeTypes: 'image/*'
        }
    });
    
    uploader.on('fileQueued', function (file) {
        var confirms = confirm("确认上传文件名为" + file.name + "的图片吗？");
        if (confirms == false) {
            uploader.cancelFile(file);
            return false;
        }
        filenames.push(file.name);
        var $list = $("#fileList1");
        $list.append('<div style="float:left; margin-left:10px" id="' + file.id + '" class="item">' +
            '<span class="info" id="' + file.name + '">' + file.name + '</span>' +
            '&nbsp;&nbsp;' +
            //'<a class="delete1" style="cursor: pointer">删除</a>' +
            '<p class="state">等待上传...</p>' +
        '</div>');

        $list.on('click', '.delete1', function () {
            var id = $(this).parent().attr("id");
            uploader.removeFile(id, true);
            $(this).parent().remove();
            var name = $(this).prev().attr("id");
            var filename = $(this).parent().attr("filename").match(/[^\/]*$/)[0];//通过正则表达式获取字符串路径下的文件名称
            var param = { fileName: filename };
            $.ajax({
                type: "POST",
                url: "Ajax/WarningImgUploader.ashx?method=DeleteImg",
                data: param,
                dataType: "json",
                async: false,
                success: function (data) {
                },
                error: function (data) { }

            });

            //删除文件后删除数组中的文件名
            filenames.splice($.inArray(filename, filenames), 1);
        });
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

        $li.find('p.state').text('上传中');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {
        $("#conlist1").html(response._raw.split(']')[1]);
        $('#' + file.id).find('p.state').text('已上传');
        upLoadImgFlag = true;
        // $('#' + file.id).attr("fileName", response.jdata.filePath);
        // filePath.push(response.jdata.filePath);
        // var fliename = response.jdata.fileName;
        //$("#filePathName").val(fliename);
    });

    uploader.on('uploadError', function (file, reason) {

        $('#' + file.id).find('p.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
    uploader.on('error', function (type) {
        if (type == "Q_TYPE_DENIED")
        { common.showErrorMsg("您选择上传文件的格式不正确，请重新选择!<br><span>文件后缀例:gif,jpg,bmp,png</span>", null); }
        else if (type == "F_DUPLICATE") {
            common.showErrorMsg("您选择的文件已经上传，请重新选择!", null);
        }
        else if (type == "F_EXCEED_SIZE") {
            common.showErrorMsg("您选择的文件大于100M，请重新选择!", null);
        }
    });

    uploader.upload();


}
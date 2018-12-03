<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetTideData.aspx.cs" Inherits="PredicTable.GetTideData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/Gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/color.css" />

    <link rel="stylesheet" href="css/style.default.css" type="text/css" />

    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>
</head>
<body>

        <script type="text/javascript">
            $(function () {
                $.ajax({
                    type: "GET",
                    url: "Ajax/GetTideData.ashx",//?station=test2&preDate=2016-01-01
                    data: { station: 'test2', preDate: '2016-01-01' },
                    dataType: "json",
                    success: function (data) {
                        $(data).each(function (i, val) {
                            alert(val.STATION+":"+val.H1);
                        });
                        //alert(data.H1);
                    },
                    error: function () {
                        alert("error");
                    }


                });

        });
    </script>

</body>
</html>

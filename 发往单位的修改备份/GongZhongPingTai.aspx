<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GongZhongPingTai.aspx.cs" Inherits="PredicTable.GongZhongPingTai" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link rel="stylesheet" href="css/style.default.css" type="text/css" />
    <script type="text/javascript" src="js/plugins/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.cookie.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.flot.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="js/plugins/jquery.slimscroll.js"></script>
    <script type="text/javascript" src="js/custom/general.js"></script>
    <script type="text/javascript" src="js/custom/dashboard.js"></script>


    <script type="text/javascript" src="/Scripts/jquery-1.10.2.min.js"></script>

    <script type="text/javascript" src="/Scripts/EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" >
      
    </script>
</head>
<body class="withvernav">






    <div <%--class="centercontent"--%>>
        <div class="contenttitle2">
            <h3 id="tx1">公众平台</h3>
        </div>

        <div id="contentwrapper" class="contentwrapper">


            <div style="margin: 20px 0;"></div>
            <div style="width: 100%">
                <form runat="server" class="stdform stdform2" method="post" action="" style="width: 80%" enctype= "multipart/form-data " >
                    <div style="margin: 0 auto; padding-left: 10%; border-color: #95B8E7; border-style: solid; min-height: 600px; text-align: center">
                        <div id="basicform" class="subcontent">

                            <div style="width: 1000px;">
                                <div style="width: 1000px; height: 200px; border-width: 1px; border-bottom-style: solid; border-color: #DFDFDF;">
                                    <div style="float: left; width: 150px; height: 100%; line-height: 100px; background-color: #fcfcfc;">文档内容：</div>
                                    <div style="float: left; width: 840px; height: 100%;">
                                        <textarea style="width: 90%; resize: none;" onkeyup="this.value = this.value.slice(0, 2000)" id="DOCUMENTCONTENT" cols="20" maxlength="2000" rows="7"></textarea>

                                        <br />
                                        <br />
                                        <br />
                                        消息类型:<select id="MESTYPE">
                                            <option value="微博">微博</option>
                                            <option value="微信">微信</option>
                                            <option value="传真">传真</option>
                                        </select>&nbsp;&nbsp;
                                          文档类型:<select id="DOCTYPE">
                                            <option value="预报">预报</option>
                                            <option value="预警">预警</option>
                                        </select>&nbsp;&nbsp;
                                        
                                      
                                    </div>
                                </div>
                                <div style="width: 1000px; height: 300px; border-width: 0px; border-bottom-style: solid; border-color: #DFDFDF;">
                                    <div style="float: left; float: left; width: 150px; height: 100%; line-height: 100px; background-color: #fcfcfc;">添加附件：</div>
                                    <div style="float: left; width: 840px; height: 100%;">
                                        <div id="weblogolab" style="text-align: left; padding-left: 60px;"></div>
                                        <div id ="aa">
                                            <iframe id="iframeId" width="800px" height="290px" frameborder="0" scrolling="yes" src="gzptiframe.aspx"></iframe>

                                         <%-- <p id="MyFile">
                                                <input type="file" size="50" name="File" />
                                                选择类型:<select id="TYPE">
                                                    <option value="下载附件">下载附件</option>
                                                    <option value="展示文件">展示文件</option>
                                                </select>
                                            </p>
                                            <p>
                                                <input type="button" value="增加一个" onclick="addForm()" />


                                                <input id="reset" onclick="aa()" type="button" value="重s 置" />
                                                <input onclick="this.form.reset()" type="button" value="重 置" />
                                                <br />
                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                            </p>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                           

                        </div>
                       

                        <!--subcontent-->
                    </div>
                </form>
                <!--contentwrapper-->
                <!-- centercontent -->
            </div>



            <div id="activities" class="subcontent" style="display: none;">
                &nbsp;
            </div>
        </div>
    </div>

    <style type="text/css">
        #userlist li {
            float: left;
            list-style-type: none;
            margin: 2px;
            border: 3px solid #ccc;
            font-weight: bold;
        }

            #userlist li:hover {
                /*border:3px solid #ff6a00;*/
                cursor: pointer;
            }

        .contactlist li a {
            padding: 0px;
        }

        body.withvernav {
            background: none;
        }

        #fm {
            margin: 0;
            padding: 10px 30px;
        }

        .ftitle {
            font-size: 14px;
            font-weight: bold;
            padding: 5px 0;
            margin-bottom: 10px;
            border-bottom: 1px solid #ccc;
        }

        .fitem {
            margin-bottom: 5px;
        }

            .fitem label {
                display: inline-block;
                width: 80px;
            }

            .fitem input {
                width: 160px;
            }
    </style>
      <script type="text/javascript" language="javascript">
        
        $(document).ready(function () {

          

        });
    </script>
</body>
</html>


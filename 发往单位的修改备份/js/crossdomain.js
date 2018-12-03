
var tableurl = " http://123.234.129.240:8777";
var frameurl = "http://www.huitenginfo.com:50002";
//防止单独打开 与不是在自己的网站中打开
$(function () {
      getuserinfo();
});

//跨域获取用户信息 网址需配置成当前域名
function getuserinfo() {

    var state = 0;
    iframe = document.createElement('iframe');
    loadfn = function () {
        if (state === 1) {
            var data = iframe.contentWindow.name.split(',');    // 读取数据
            if (data.length > 1) {
                if (data[1] == "-1") {
                    top_url_login();  //超时 跳转到登陆页重新登录
                } else {
                    $.ajax({//设置session
                        type: "POST",
                        url: "/Ajax/gettablelist.ashx?method=setsession",
                        data: {
                            userids: data[1],
                            types: data[2]
                        },
                        success: function (result) {
                            alert( tableurl + "/AMTableList.aspx");
                            window.location.href = tableurl + "/AMTableList.aspx";
                        }
                    });
                }
            }
        
        } else if (state === 0) {
            state = 1;
            iframe.contentWindow.location = tableurl + "/proxy.html";    // 设置的代理文件
        }
    };
    iframe.src = frameurl + '/admin/userinfo.aspx';
    if (iframe.attachEvent) {
        iframe.attachEvent('onload', loadfn);
    } else {
        iframe.onload = loadfn;
    }
    document.body.appendChild(iframe);
}
//跳转到登陆页重新登录
function top_url_login() {
    top.location.href = frameurl + "/login.aspx";
}


1.把下载的UEditor目录拷贝到自己的项目中
2.修改ueditor.config.js配置为自己想要的配置重命名为：ueditor.myconfig.js
3.修改上传路径为程序根目录的方法 修改 UEditor/net/config.json 文件
把imageUrlPrefix配置为："imageUrlPrefix": ""
把imagePathFormat配置为： "imagePathFormat": "/UploadFiles/image/{yyyy}{mm}{dd}/{time}{rand:6}", 


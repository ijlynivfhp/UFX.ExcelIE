# UFX.ExcelIE


后台启动配置注意事项：

  一，appsetting配置文件中添加以下配置信息（ExcelEDownLoad导出配置）
  
  "SysConfig": {
    "ExcelEDownLoad": {
      "DeployType": 0,//布置方式字段：默认0代表本地布置，1代表外网远程布置（oss暂未对接）
      "RemoteUrl": "http://localhost:5080"//如果DeployType配置了远程该链接必填远程oss文件路径前缀
    }
  }
  
  
  二，RabbitMQ已启用。redis未加入无需要配置
  //存放数据库连接字符串
  "ConnectionStrings": {
    "CAPConnection": "rabbitmq连接所需数据库",
    "SCMConnection": "数据库连接;"
  },
  //RabbitMQ连接字符串
  "RabbitMQ": {
    "HostName": "localhost",
    "Port": "10002",
    "UserName": "admin",
    "Password": "admin",
    "FailedRetryCount": "5",
    "SucceedMessageExpiredAfter": "1296000"
  },
  
  三，WEB端：需配置连接导出任务连接地址：
    <!--Excel导出服务地址-->
    <add key="ExcelIEUrl" value="http://localhost:8010/Home/PushExcelExportMsg" />
    具体域名根据实际调整（依据导出服务域名对应）
  

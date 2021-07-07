## rainbow-site

`rainbow-site`是 一朵彩虹的网站服务器,提供了一朵彩虹的展示网站服务。

该网站服务由 `C#` 编写，基于`ASP.NET Core 2.2`，为跨平台的`Web APP`，采用`MVC`的网页设计模式.

`wwwroot/` 为静态文件目录

`Controllers/` 为控制器目录

`Models/` 为数据模型目录

`Views/` 为 `cshtml` 模板目录

## Qixi专题

网站开通了七夕专题，目前包括三个页面：
1. https://rainbow.eatrice.top/Qixi/Type1
2. https://rainbow.eatrice.top/Qixi/Type2
3. https://rainbow.eatrice.top/Qixi/Type3
4. https://rainbow.eatrice.top/Qixi/Type4


直接访问可以进行预览，访问[https://rainbow.eatrice.top/Qixi/](https://rainbow.eatrice.top/Qixi/)以获取更多信息。

对于Type2，可以修改网页呈现的一部分内容：
`MyLove`决定信的开头
`MyName`决定信的落款
`Year`, `Month`, `Day`, `Hour`, `Minute`, `Second`决定日期的呈现。
预览中为默认值，其对应的URL为：https://rainbow.eatrice.top/Qixi/Type2?MyLove=Qiqi&MyName=EatRice&Year=2019&Month=6&Day=24&Hour=20&Minute=0&Second=0

## 运行

网站运行依赖于:

代码基于C#编写，运行依赖 `.NET Core 2.2` 运行时，Ubuntu安装 `.NET Core 2.2 SDK` 如下：

``` 
$ sudo apt-get update
$ sudo apt-get install apt-transport-https
$ sudo apt-get update
$ sudo apt-get install dotnet-sdk-2.2 -y
```
其他操作系统版本请参考：[安装 .NET Core SDK](https://docs.microsoft.com/zh-cn/dotnet/core/install/sdk?pivots=os-linux)

1. 先克隆代码仓库至本地
2. `cd` 至本仓库 `cd rainbow-site`
3. 运行`dotnet run`
4. 网站在 `http://127.0.0.1:5002`和`http://127.0.0.1:5003`启动

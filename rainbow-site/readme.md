## rainbow-site

`rainbow-site`是 一朵彩虹的网站服务器,提供了一朵彩虹的展示网站服务。

该网站服务由 `C#` 编写，基于`ASP.NET Core 2.2`，为跨平台的`Web APP`，采用`MVC`的网页设计模式.

`wwwroot/` 为静态文件目录

`Controllers/` 为控制器目录

`Models/` 为数据模型目录

`Views/` 为 `cshtml` 模板目录

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
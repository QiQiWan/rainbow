
## Rainbow, 一朵彩虹

一朵彩虹是我们在平时的学习阅读或其他文学作品中记录的优美的名言句子，希望它能像彩虹一样，美丽天空，温暖人心。

`resource/` 为句子库，数据文件格式为 `yml` ，补充新增句子请参照 `yml` 数组的语法格式。[YAML 语言教程](https://www.ruanyifeng.com/blog/2016/07/yaml.html)

其中：
`reading.yml` 为英语书籍作品中的句子;
`moives.yml` 为英语影视作品中的句子;
`songs.yml` 为英语音乐作品中的句子;

`log/` 为日志文件夹。

`server/` 为服务器程序文件夹

`data/` 为数据文件夹


## 安装


代码基于C#编写，运行依赖 `.NET Core 2.2` 运行时，Ubuntu安装 `.NET Core 2.2 SDK` 如下：

``` 
$ sudo apt-get update
$ sudo apt-get install apt-transport-https
$ sudo apt-get update
$ sudo apt-get install dotnet-sdk-2.2 -y
```

其他系统版本请参考：[安装 .NET Core SDK](https://docs.microsoft.com/zh-cn/dotnet/core/install/sdk?pivots=os-linux)


`cd` 进入 `rainbow.csproj` 目录执行下面命令运行即可：

1. 先克隆代码仓库至本地
2. `cd` 至本仓库 `cd rainbow`
3. 运行`dotnet run`
4. 网站在 `http://127.0.0.1:8888` 启动

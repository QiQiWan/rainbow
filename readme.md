###  介绍

Rainbow - 一朵彩虹是 `EatRiceTeam`建立的一个旨在收集优美英语句子的一套网站。

我们希望能够与大家分享我们在日常的学习生活中遇到的优美的英语句子，希望它能像彩虹一样，美丽天空，温暖人心。

其由`C#`开发，基于`ASP.NET Core 2.2`框架。包括`Web API`提供导出`JSON`的数据接口，和基于`MVC`的动态展示网站。

### 关于Rainbow

Rainbow 收集的英语句子的要求为：

1. 读起来感觉很优美的文章句子段落或诗歌节选；
2. 含义特别丰富且引人深思的鸡汤或哲学句子；
3. 句子奇怪，但意义完整且显得很有个性的电影台词；
4. 你特别喜欢，且引起你感情上共鸣的英语歌词。

Rainbow 创建的初衷是替代我们的个人网站目前正在使用的 一言 ，我们希望自己能够自己定义一句话的意思和表现形式。目前句子库不是特别丰富，收集的资源有限，所以欢迎大家投稿，并发表自己的看法。

### 给Rainbow投稿

我们希望找到小伙伴们和我们一起充实我们的句子库，希望大家能够将自己珍藏的句子分享给我们：

**投稿要求**：

1. 提供完整的句子
2. 提供句子的作者
3. 提供句子的来源，如书名、文章名、电影名、歌曲名等。

投稿方式：

1. [EatRice的邮箱：qiqi@eatrice.top](mailto:qiqi@eatrice.top)

2. [Courir的邮箱：dipper.ruru@gmail.com](dipper.ruru@gmail.com)

3. 在本页下方留言
4. 好友QQ或微信直接发送

### 使用方法

#### 数据接口

目前语句库饱含了三种类型的语句：reading、movies、songs

需要从语句库中随机获得语句的`Json`格式的接口：`https://api.eatrice.top/`

需要按照三个单独分类请求语句的接口：
`https://api.eatrice.top/reading/`
`https://api.eatrice.top/movies/`
`https://api.eatrice.top/songs/`

需要根据语句ID请求语句的接口：`https://api.eatrice.top/?ID=1001`
ID编号从1001开始增加，若该ID不存在则随机返回语句，同`https://api.eatrice.top/`

返回的数据格式如下：
``` json
{
    "Content": "Because I am your mom,It counts the most because I know you the most.",
    "Author": "Stephen Chbosky",
    "Source": "Wonder",
    "ID": "1009"
}
```
其中，`Content`为句子内容
`Author`为句子作者
`Source`为句子来源
`ID`为句子ID

#### 展示网站

Rainbow的展示网站为：https://rainbow.eatrice.top/
欢迎大家访问和提供意见😊😊😊。

### 贡献者

[EatRice-https://eatrice.top](https://eatrice.top)
[上屋顶看北斗七星-https://ruru.eatrice.top](https://ruru.eatrice.top)
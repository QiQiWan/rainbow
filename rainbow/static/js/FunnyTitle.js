var OriginTitle = document.title;
var titleTime;
document.addEventListener('visibilitychange', function () {
    if (document.hidden) {
        //$('[rel="icon"]').attr('href', "img/funny.ico");
        document.title = '你不爱我了吗?இ௰இ';
        clearTimeout(titleTime);
    }
    else {
        //$('[rel="icon"]').attr('href', "img/favicon.png");
        document.title = '我是小彩虹😁' + OriginTitle;
        titleTime = setTimeout(function () {
            document.title = OriginTitle;
        }, 2000);
    }
});

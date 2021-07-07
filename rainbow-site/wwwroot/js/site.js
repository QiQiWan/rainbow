function PageReplace(Id, jsonString) {
    if (!document.getElementById(Id))
        return;
    var container = document.getElementById(Id);
    var SModel = typeof(jsonString) == 'string' ? JSON.parse(jsonString) : jsonString;
    container.innerHTML = "";
    container.innerHTML += SModel.Content + "<br /><br />";
    container.innerHTML += "<span class='h5 float-right'>————" + SModel.Author + "《" + SModel.Source + "》</span>";
}

function GetRequest(url) {
    var request = new XMLHttpRequest();
    var ID = getQueryString("ID") || "";
    request.open('GET', url + "?ID=" + ID);
    request.send();
    var result = "";
    request.onreadystatechange = (res) => {
        if (request.readyState == 4 && request.status == 200)
            PageReplace("subtitle", request.responseText);
    }
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}

function TimeTick() {
    // GetRequest("/GetJson");
    // //设置定时切换
    // setTimeout(() => {
    //     TimeTick();
    // }, 60000);

    // 设置定时切换
    GetRainbow();
    setTimeout(() => {
        TimeTick();
    }, 60000);
}

function GetRainbow() {
    fetch('https://api.eatrice.top')
        .then(response => response.json())
        .then(data => {
            PageReplace("subtitle", data);
        })
        .catch(console.error)
}
TimeTick();
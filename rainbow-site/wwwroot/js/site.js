// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.



window.addEventListener("load", res => {
   setTimeout(() => {
       
   }, 60000);  GetRequest("/Home/GetJson")
})

function PageReplace(Id, jsonString) {
    var container = document.getElementById(Id);
    var SModel = JSON.parse(jsonString);
    container.innerHTML = "";
    container.innerHTML += SModel.Content + "<br /><br />";
    container.innerHTML += "<span class='h5 float-right'>————" + SModel.Author + "《" + SModel.Source + "》</span>";
}

function GetRequest(url) {
    var request = new XMLHttpRequest();
    request.open('GET', url);
    request.send();
    var result = "";
    request.onreadystatechange = (res) => {
        if (request.readyState == 4 && request.status == 200)
            PageReplace("subtitle", request.responseText);
    }
}


if (location.href.indexOf("embed") > 0) {
    setTimeout(f2, 1000, location.href);
}
else if (location.href.indexOf("lixgrab") > 0) {
    setTimeout(f1, 1000);
}


function f1() {
    let vidov = $("#videooverlay");
    if (vidov) {
        vidov.click();
        f();
    }
    else {
        setTimeout(f1, 500)
    }
}

function f() {
    let url = $("#olvideo_html5_api").attr("src");
    if (url) {
        console.log("https://" + window.location.hostname + url);
        //f2(url);
    }
    else {
        setTimeout(f, 500);
    }
}

function f2(url) {
    try {
        var xhttp = new XMLHttpRequest();
        xhttp.open("POST", "http://localhost:12785?openload", true);

        xhttp.onreadystatechange = function () {
            if (xhttp.responseText === location.href) {
                console.log(xhttp.responseText);
                window.close();
            }
            else {
                setTimeout(f2, 60000, url);
            }
        };
        xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xhttp.send(url);
        xhttp.close();
    }
    catch (e) {

    }
}